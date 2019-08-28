using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using newMHRS;
using newMHRS.Models;

namespace newMHRS.Controllers
{
    public class RandevuController : Controller
    {        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Randevu
        public ActionResult Index()
        {
            var randevus = db.Randevus.Include(r => r.Bolum).Include(r => r.Doktor).Include(r => r.Hasta).Include(r => r.Hastahane).Include(r => r.Ilce).Include(r => r.Sehir);
            return View(randevus.ToList());

         
        }

        public ActionResult Create()
        {
            ViewBag.SehirList = new SelectList(GetSehirList(), "Id", "Ad");//sehirler tablondaki alanların!!
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CascadingClass cascadingClass)
        {
            ViewBag.SehirList = new SelectList(GetSehirList(), "Id", "Ad");
            DateTime ileriTarih = DateTime.Now.AddDays(15);
            DateTime simdikiTarih = DateTime.Now;

            if (ModelState.IsValid)
            {
                var randevu = new Randevu();
                var hasta = (int)Session["hastaId"];
                var randevuSayi = db.Randevus.Where(x => x.HastaId == hasta && (x.Tarih == cascadingClass.Tarih)).Count();
                var bolumSayi = db.Randevus.Where(x => x.HastaId == hasta && (x.Tarih == cascadingClass.Tarih) && (x.BolumId == cascadingClass.BolumId)).Count();


                // randevu.HastaId = Response.Cookies["hastaId"].Value.ToString();                
                if (cascadingClass.Tarih < simdikiTarih || cascadingClass.Tarih > ileriTarih)
                {
                    ViewBag.TarihView = "Alacağınız randevu 15 günü geçemez. " + simdikiTarih.ToString("dd/MM/yyyy") + " ile " + ileriTarih.ToString("dd/MM/yyyy") + " arasında randevu alınız." + cascadingClass.Tarih;
                }

                if (randevuSayi < 3 && bolumSayi < 1)
                {
                    randevu.HastaId = (int)Session["hastaId"];
                    randevu.Tarih = cascadingClass.Tarih;
                    randevu.SehirId = cascadingClass.SehirId;
                    randevu.IlceId = cascadingClass.IlceId;
                    randevu.HastahaneId = cascadingClass.HastahaneId;
                    randevu.BolumId = cascadingClass.BolumId;
                    randevu.DoktorId = cascadingClass.DoktorId;
                    randevu.SaatId = cascadingClass.SaatId;
                    db.Randevus.Add(randevu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                if (randevuSayi >= 3)
                {
                    ViewBag.FazlaRandevu = "1 Günde 3'den fazla Randevu Alamazınız.";
                }
                if (bolumSayi >= 1)
              //  else
                {
                    ViewBag.FazlaBolum = "Aynı bölüme 1 den fazla randevu alamazsınız.";
                }

            }

            return View(cascadingClass);
        }



        public ActionResult Index1()
        {
            ViewBag.SehirList = new SelectList(GetSehirList(), "Id", "Ad");//sehirler tablondaki alanların!!
            return View();
        }
        public List<Sehir> GetSehirList()
        {
            List<Sehir> sehirler = db.Sehirs.ToList();
            return sehirler;
        }

        public ActionResult GetIlceList(int SehirId)
        {
            List<Ilce> selectList = db.Ilces.Where(x => x.SehirId == SehirId).ToList();
            ViewBag.IlceList = new SelectList(selectList, "Id", "Ad");
            return PartialView("DisplayIlce");
        }

        public ActionResult GetHastahaneList(int IlceId)
        {
            List<Hastahane> selectList = db.Hastahanes.Where(x => x.IlceId == IlceId).ToList();
            ViewBag.HastahaneList = new SelectList(selectList, "Id", "Ad");
            return PartialView("DisplayHastahane");
        }


        public ActionResult GetBolumList(int HastahaneId)
        {
            List<Bolum> selectList = db.Bolums.Where(x => x.HastahaneId == HastahaneId).ToList();
            ViewBag.BolumList = new SelectList(selectList, "Id", "Ad");
            return PartialView("DisplayBolum");
        }



        public ActionResult GetDoktorList(int BolumId)
        {
            List<Doktor> selectList = db.Doktors.Where(x => x.BolumId == BolumId).ToList();
            ViewBag.DoktorList = new SelectList(selectList, "Id", "Ad");
            return PartialView("DisplayDoktor");
        }

        public ActionResult GetSaatList(int DoktorId)
        {
            List<Saat> selectList = db.Saats.Where(x => x.DoktorId == DoktorId).ToList();
            ViewBag.SaatList = new SelectList(selectList, "Id", "SaatKac");
            return PartialView("DisplaySaat");
        }


        [HttpPost]
        public JsonResult GetirIlceler(int SehirId)
        {
            var ilceler = db.Ilces.Where(ilce => ilce.SehirId == SehirId).Select(ilce => new IdAd { Id = ilce.Id, Ad = ilce.Ad }).ToString();
            return Json(ilceler);
        }
        // GET: Randevu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Randevu randevu = db.Randevus.Find(id);
            if (randevu == null)
            {
                return HttpNotFound();
            }
            return View(randevu);
        }

        // GET: Randevu/Create
        public ActionResult Create1()
        {
            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Ad");
            ViewBag.DoktorId = new SelectList(db.Doktors, "Id", "Ad");
            ViewBag.HastaId = new SelectList(db.Hastas, "Id", "Tc");
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad");
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad");
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad");
            return View();
        }

        // POST: Randevu/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "Id,Tarih,SehirId,IlceId,HastaId,HastahaneId,BolumId,DoktorId,IptalMi")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                db.Randevus.Add(randevu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Ad", randevu.BolumId);
            ViewBag.DoktorId = new SelectList(db.Doktors, "Id", "Ad", randevu.DoktorId);
            ViewBag.HastaId = new SelectList(db.Hastas, "Id", "Tc", randevu.HastaId);
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", randevu.HastahaneId);
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad", randevu.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad", randevu.SehirId);
            return View(randevu);
        }

        // GET: Randevu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Randevu randevu = db.Randevus.Find(id);
            if (randevu == null)
            {
                return HttpNotFound();
            }
            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Ad", randevu.BolumId);
            ViewBag.DoktorId = new SelectList(db.Doktors, "Id", "Ad", randevu.DoktorId);
            ViewBag.HastaId = new SelectList(db.Hastas, "Id", "Tc", randevu.HastaId);
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", randevu.HastahaneId);
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad", randevu.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad", randevu.SehirId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tarih,SehirId,IlceId,HastaId,HastahaneId,BolumId,DoktorId,IptalMi")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(randevu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Ad", randevu.BolumId);
            ViewBag.DoktorId = new SelectList(db.Doktors, "Id", "Ad", randevu.DoktorId);
            ViewBag.HastaId = new SelectList(db.Hastas, "Id", "Tc", randevu.HastaId);
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", randevu.HastahaneId);
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad", randevu.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad", randevu.SehirId);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Randevu randevu = db.Randevus.Find(id);
            if (randevu == null)
            {
                return HttpNotFound();
            }
            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Randevu randevu = db.Randevus.Find(id);
            db.Randevus.Remove(randevu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
