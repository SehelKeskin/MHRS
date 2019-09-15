﻿using System;
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
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Randevu
        public ActionResult Index()
        {

          if(  Session["hastaId"]==null)
            {
                return RedirectToAction("Index","Login");
            }
        
     
            // var randevus = db.Randevus.Include(r => r.Bolum).Include(r => r.Doktor).Include(r => r.Hasta).Include(r => r.Hastahane).Include(r => r.Ilce).Include(r => r.Sehir);
            var id = (int)Session["hastaId"];
            var sonuc = db.Randevus.Where(x=>x.HastaId==id);
          
    
            var tarih = DateTime.Now;


            foreach (var item in sonuc)
            {
                if (item.Tarih < tarih)
                {
                    item.RandevuDurum = "Geçmiş Randevu";
                
                }

                //if (item.RandevuDurum=="Geçmiş Randevu")
                //{
                    
                //}
            }
            db.SaveChanges();
            // return View(randevus.ToList());
            return View(sonuc.ToList());

         
        }

        public ActionResult Create()
        {
            if (Session["hastaId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
                DateTime gelenTarih = cascadingClass.Tarih;
                var randevuSayi = db.Randevus.Where(x => x.HastaId == hasta && (x.Tarih == cascadingClass.Tarih)).Count();
                var bolumSayi = db.Randevus.Where(x => x.HastaId == hasta && x.DoktorId == cascadingClass.DoktorId && (x.BolumId == cascadingClass.BolumId)).Count();
                       
                if (cascadingClass.Tarih > ileriTarih)
                {
                    ViewBag.TarihView = "Alacağınız randevu 15 günü geçemez. " + simdikiTarih.ToString("dd/MM/yyyy") + " ile " + ileriTarih.ToString("dd/MM/yyyy") + " arasında randevu alınız.";
                }

               else if (gelenTarih <= simdikiTarih)
                {
                    ViewBag.tarihKucuk = "Alacağınız randevu geçmiş tarihe ait olduğu için, alınamaz.";
                }

                else if (randevuSayi >= 3)
                {
                    ViewBag.FazlaRandevu = "1 Günde 3'den fazla Randevu Alamazınız.";
                }
                else if (bolumSayi >= 1)

                {
                    ViewBag.FazlaBolum = "Aynı doktordan 1 den fazla randevu alamazsınız.";
                }

               // if (randevuSayi < 3 && bolumSayi < 1 && ((gelenTarih>simdikiTarih)&&(gelenTarih<ileriTarih)))
               else
                {
                    randevu.HastaId = (int)Session["hastaId"];
                    randevu.Tarih = cascadingClass.Tarih;
                    randevu.SehirId = cascadingClass.SehirId;
                    randevu.IlceId = cascadingClass.IlceId;
                    randevu.HastahaneId = cascadingClass.HastahaneId;
                    randevu.BolumId = cascadingClass.BolumId;
                    randevu.DoktorId = cascadingClass.DoktorId;
                    randevu.SaatId = cascadingClass.SaatId;
                    randevu.RandevuDurum = "Aktif";
                    var saatdeneme = db.Saats.Where(x => x.DoktorId == cascadingClass.DoktorId && x.SaatDurum == false).FirstOrDefault();
                    saatdeneme.SaatDurum = true;

                    db.Randevus.Add(randevu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
              

            }

            return View(cascadingClass);
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
            List<Saat> selectList = db.Saats.Where(x => x.DoktorId == DoktorId && x.SaatDurum==false).ToList();

            if (selectList==null)
            {
                ViewBag.SaatDurum = "Seçtiğiniz Doktora uygun saat kalmamıştır.Farklı doktor deneyiniz.";
            }
            else
            {
                ViewBag.SaatList = new SelectList(selectList, "Id", "SaatKac");
      
            }

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
            if (Session["hastaId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["hastaId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Ad");
            ViewBag.DoktorId = new SelectList(db.Doktors, "Id", "Ad");
            ViewBag.HastaId = new SelectList(db.Hastas, "Id", "Tc");
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad");
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad");
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad");
            return View();
        }
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
            if (Session["hastaId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            ViewBag.SaatId = new SelectList(db.Saats, "Id", "SaatKac", randevu.SaatId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tarih,SehirId,IlceId,HastaId,HastahaneId,BolumId,DoktorId,IptalMi,SaatId")] Randevu randevu)
        {
            if (Session["hastaId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            ViewBag.SaatId = new SelectList(db.Saats, "Id", "SaatKac", randevu.SaatId);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["hastaId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Randevu randevu = db.Randevus.Find(id);
            var tarih = DateTime.Now;
            if (randevu.Tarih > tarih)
            {
                randevu.RandevuDurum = "Geçmiş Randevu";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
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
            //var saatDurum = db.Saats.Where(x => x.DoktorId == cascadingClass.DoktorId && x.SaatDurum == false).FirstOrDefault();
            //saatdeneme.SaatDurum = true;

            randevu.RandevuDurum = "İptal";
           // db.Randevus.Remove(randevu);
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
