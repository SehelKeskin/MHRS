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
    public class HastaBilgiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HastaBilgi
        public ActionResult Index()
        {
            return View(db.Hastas.ToList());
        }

        // GET: HastaBilgi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hasta hasta = db.Hastas.Find(id);
            if (hasta == null)
            {
                return HttpNotFound();
            }
            return View(hasta);
        }

        // GET: HastaBilgi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HastaBilgi/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tc,Ad,Soyad,Cinsiyet,DogumTarihi,DogumYeri,AnneAdi,BabaAdi,CepTel,Mail,Sifre,TSifre")] Hasta hasta)
        {
            if (ModelState.IsValid)
            {
                db.Hastas.Add(hasta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hasta);
        }

        // GET: HastaBilgi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hasta hasta = db.Hastas.Find(id);
            if (hasta == null)
            {
                return HttpNotFound();
            }
            return View(hasta);
        }

        // POST: HastaBilgi/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tc,Ad,Soyad,Cinsiyet,DogumTarihi,DogumYeri,AnneAdi,BabaAdi,CepTel,Mail,Sifre,TSifre")] Hasta hasta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hasta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hasta);
        }

        // GET: HastaBilgi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hasta hasta = db.Hastas.Find(id);
            if (hasta == null)
            {
                return HttpNotFound();
            }
            return View(hasta);
        }

        // POST: HastaBilgi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hasta hasta = db.Hastas.Find(id);
            db.Hastas.Remove(hasta);
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
