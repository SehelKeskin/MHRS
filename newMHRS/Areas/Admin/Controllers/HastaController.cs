using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using newMHRS;
using newMHRS.Areas.Admin.Models;
using newMHRS.Models;

namespace newMHRS.Areas.Admin.Controllers
{
    [Authorize]
    public class HastaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Hasta
     
        public ActionResult Index()
        {
            return View(db.Hastas.ToList());
        }



        // GET: Admin/Hasta/Details/5
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

        // GET: Admin/Hasta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hasta/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HastaView hastaView)
        {
            var bugun = DateTime.Now;
            if (ModelState.IsValid)
            {
                var hasta = new Hasta();
                var hastaVarmi = db.Hastas.Where(x => x.Tc == hastaView.Tc).Count();

                if(hastaVarmi>=1)
                {
                    ViewBag.hastaVarmis = "Sistemimizde girmiş olduğunuz hastanın kaydı vardır.";
                }
                else if (hastaView.DogumTarihi < bugun)
                {
                    ViewBag.buyukTarih = "Doğum tarihiniz bugünden küçük bir tarih olamaz.";
                }
                else
                {
                    hasta.Tc = hastaView.Tc;
                    hasta.Ad = hastaView.Ad;
                    hasta.Soyad = hastaView.Soyad;
                    hasta.CepTel = hastaView.CepTel;
                    hasta.Mail = hastaView.Mail;
                    hasta.Sifre = hastaView.Sifre;
                    hasta.AnneAdi = hastaView.AnneAdi;
                    hasta.BabaAdi = hastaView.BabaAdi;
                    hasta.Cinsiyet = hastaView.Cinsiyet;
                    hasta.DogumTarihi = hastaView.DogumTarihi;
                    hasta.DogumYeri = hastaView.DogumYeri;
                    hasta.TSifre = hastaView.TSifre;
                    db.Hastas.Add(hasta);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

               
            }

            return View(hastaView);
        }

        // GET: Admin/Hasta/Edit/5
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

        // POST: Admin/Hasta/Edit/5
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

        // GET: Admin/Hasta/Delete/5
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

        // POST: Admin/Hasta/Delete/5
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
