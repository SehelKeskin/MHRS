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
    public class BolumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Bolum
        public ActionResult Index()
        {
            var bolums = db.Bolums.Include(b => b.Hastahane);
            return View(bolums.ToList());
        }

        // GET: Admin/Bolum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolums.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // GET: Admin/Bolum/Create
        public ActionResult Create()
        {
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad");
            return View();
        }

        // POST: Admin/Bolum/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BolumView bolum)
        {
           


            if (ModelState.IsValid)
            {
                var bolumler = new Bolum();
                var bolumVarmi = db.Bolums.Where(x => x.Ad == bolum.Ad && x.HastahaneId == bolum.HastahaneId).Count();

                if (bolumVarmi >= 1)
                {
                    ViewBag.AynıBolum = "Girmiş olduğunuz bölüm aynı hastahanede bulunmaktadır.";
                }
                else
                {
                    bolumler.Ad = bolum.Ad;
                    bolumler.HastahaneId = bolum.HastahaneId;
                   
                    db.Bolums.Add(bolumler);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", bolum.HastahaneId);
            return View(bolum);
        }

        // GET: Admin/Bolum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolums.Find(id);
            var bolumVarmi = db.Bolums.Where(x => x.Ad == bolum.Ad && x.HastahaneId == bolum.HastahaneId).Count();
            if (bolum == null && bolumVarmi>=1)
            {
                return HttpNotFound();
            }
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", bolum.HastahaneId);
            return View(bolum);
        }

        // POST: Admin/Bolum/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,HastahaneId")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bolum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", bolum.HastahaneId);
            return View(bolum);
        }

        // GET: Admin/Bolum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolums.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // POST: Admin/Bolum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bolum bolum = db.Bolums.Find(id);
            db.Bolums.Remove(bolum);
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
