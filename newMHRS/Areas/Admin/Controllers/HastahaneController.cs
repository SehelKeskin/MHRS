using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using newMHRS;
using newMHRS.Areas.Admin.Models;
using newMHRS.Models;

namespace newMHRS.Areas.Admin.Controllers
{
    [Authorize]
    public class HastahaneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Hastahane
      
        public ActionResult Index()
        {
            var hastahanes = db.Hastahanes.Include(h => h.Ilce).Include(h => h.Sehir);
            return View(hastahanes.ToList());
        }

        // GET: Admin/Hastahane/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hastahane hastahane = db.Hastahanes.Find(id);
            if (hastahane == null)
            {
                return HttpNotFound();
            }
            return View(hastahane);
        }
     
        // GET: Admin/Hastahane/Create
        public ActionResult Create()
        {
            //ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad");
            //ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad");
            ViewBag.SehirList = new SelectList(GetSehirList(), "Id", "Ad");//sehirler tablondaki alanların!!
            return View();
        }

        // POST: Admin/Hastahane/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HastahaneView hastahaneView)
        {
           ViewBag.SehirList = new SelectList(GetSehirList(), "Id", "Ad");
            if (ModelState.IsValid)
            {
               var hastahane = new Hastahane();
                var aynıHastahane = db.Hastahanes.Where(x => x.Ad == hastahaneView.Ad).Count();

                if (aynıHastahane>=1)
                {
                    ViewBag.AynıAd = "Daha önce böyle bir hastahane kaydı gerçekleştirilmiş.";
                }
                else
                {
                    hastahane.Ad = hastahaneView.Ad;
                    hastahane.Adres = hastahaneView.Adres;
                    hastahane.SehirId = hastahaneView.SehirId;
                    hastahane.IlceId = hastahaneView.IlceId;
                    hastahane.Tel = hastahaneView.Tel;
                    db.Hastahanes.Add(hastahane);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
         
            
             }
            return View(hastahaneView);
  /*  ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad", hastahane.IlceId);
           ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad", hastahane.SehirId);
            return View(hastahane);*/
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
            return PartialView("IlceGoster");
        }

        // GET: Admin/Hastahane/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hastahane hastahane = db.Hastahanes.Find(id);
            if (hastahane == null)
            {
                return HttpNotFound();
            }
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad", hastahane.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad", hastahane.SehirId);
            return View(hastahane);
        }

        // POST: Admin/Hastahane/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,SehirId,IlceId,Adres,Tel")] Hastahane hastahane)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hastahane).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Ad", hastahane.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Ad", hastahane.SehirId);
            return View(hastahane);
        }

        // GET: Admin/Hastahane/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hastahane hastahane = db.Hastahanes.Find(id);
            if (hastahane == null)
            {
                return HttpNotFound();
            }
            return View(hastahane);
        }

        // POST: Admin/Hastahane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hastahane hastahane = db.Hastahanes.Find(id);
            db.Hastahanes.Remove(hastahane);
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
