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

namespace newMHRS.Areas.Admin.Controllers
{
    public class DoktorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Doktor
        public ActionResult Index()
        {
            var doktors = db.Doktors.Include(d => d.Bolum).Include(d => d.Hastahane);
            return View(doktors.ToList());
        }

        // GET: Admin/Doktor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doktor doktor = db.Doktors.Find(id);
            if (doktor == null)
            {
                return HttpNotFound();
            }
            return View(doktor);
        }

        // GET: Admin/Doktor/Create
        public ActionResult Create()
        {
            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Name");
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad");
            return View();
        }

        // POST: Admin/Doktor/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad,Soyad,Cinsiyet,CepTel,Email,HastahaneId,BolumId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                db.Doktors.Add(doktor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Name", doktor.BolumId);
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", doktor.HastahaneId);
            return View(doktor);
        }

        // GET: Admin/Doktor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doktor doktor = db.Doktors.Find(id);
            if (doktor == null)
            {
                return HttpNotFound();
            }
            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Name", doktor.BolumId);
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", doktor.HastahaneId);
            return View(doktor);
        }

        // POST: Admin/Doktor/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,Soyad,Cinsiyet,CepTel,Email,HastahaneId,BolumId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doktor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BolumId = new SelectList(db.Bolums, "Id", "Name", doktor.BolumId);
            ViewBag.HastahaneId = new SelectList(db.Hastahanes, "Id", "Ad", doktor.HastahaneId);
            return View(doktor);
        }

        // GET: Admin/Doktor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doktor doktor = db.Doktors.Find(id);
            if (doktor == null)
            {
                return HttpNotFound();
            }
            return View(doktor);
        }

        // POST: Admin/Doktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doktor doktor = db.Doktors.Find(id);
            db.Doktors.Remove(doktor);
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
