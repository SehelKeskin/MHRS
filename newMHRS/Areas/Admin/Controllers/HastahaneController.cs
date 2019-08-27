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
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Name");
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Name");
            return View();
        }

        // POST: Admin/Hastahane/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad,SehirId,IlceId,Adres,Tel")] Hastahane hastahane)
        {
            if (ModelState.IsValid)
            {
                db.Hastahanes.Add(hastahane);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Name", hastahane.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Name", hastahane.SehirId);
            return View(hastahane);
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
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Name", hastahane.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Name", hastahane.SehirId);
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
            ViewBag.IlceId = new SelectList(db.Ilces, "Id", "Name", hastahane.IlceId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "Id", "Name", hastahane.SehirId);
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
