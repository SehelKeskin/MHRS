using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using newMHRS.Areas.Admin.Models;
using newMHRS.Models;

namespace newMHRS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sifre()
        {
            return View();
        }



        [HttpPost]//post işleminde çalışıyor.
        public ActionResult Sifre(HastaView model)
        {
            var sifree = new Guid();
            if (ModelState.IsValid)//hata var mı kontrol ediyor dataanationda.
            {
                try
                {
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                    mailMessage.From = new System.Net.Mail.MailAddress("sehelcigdem@gmail.com", "Sehel KESKİN");
                    mailMessage.Subject = "İletişim Formu: " + model.Ad + " ";

                    mailMessage.To.Add("sehelcigdem@gmail.com");
                    string body;
                    body = "Ad Soyad: " + model.Ad + " " + model.Soyad + "<br />";
                    //body += "E-posta: " + model.Email + "<br />";
                    //body += "Telefon: " + model.Phone + "<br />";
                    body += "Şifreniz: " + sifree + "<br />";
                    body += "Tarih: " + DateTime.Now.ToString("dd MMMM yyyy") + "<br />";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = body;

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new System.Net.NetworkCredential("mhrSytem@gmail.com", "hastaRandevu");
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                    ViewBag.Message = "Mesajınız gönderildi. Teşekkür ederiz.";

                }
                catch
                {
                    ViewBag.Error = "Form gönderimi başarısız oldu tekrar deneriniz.";

                }
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Authorise(Hasta hasta)//hastaya erişebilmek için yukarıya using newmhrs.models eklendi.
        {
            using(ApplicationDbContext db=new ApplicationDbContext())
            {
               
                var hastaDetay = db.Hastas.Where(x => x.Tc == hasta.Tc && x.Sifre == hasta.Sifre).FirstOrDefault() ;
                if (hastaDetay==null)
                    
                {
                    //hasta.LoginErorMsg = "Tc'nizi veya şifrenizi kontrol ediniz.";
                    ViewBag.Message = "Tc'nizi veya şifrenizi kontrol ediniz.";
                    return View("Index",hasta);
                }
                else
                {
                    Session["hastaAd"] = hastaDetay.Ad;
                    Session["hastaSoyad"] = hastaDetay.Soyad;
                    Session["hastaId"] = hastaDetay.Id;
                    //Response.Cookies["hastaId"].Value = hastaDetay.Id.ToString();
                    //Response.Cookies["hastaId"].Expires = DateTime.Now.AddDays(1);

                    //Response.Cookies["hastaTc"].Value = hastaDetay.Tc;
                    //Response.Cookies["hastaTc"].Expires = DateTime.Now.AddDays(1);
                    return RedirectToAction("Create","Randevu");

                }
            }
         //ıss neden devredışı bıraktı neden? ve buradaki bboş view kaldırıldı.

           
        }
        public ActionResult LogOut()
        {
            int hastaId =(int)Session["hastaId"];
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

       
 
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HastaView hastaView)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var bugun = DateTime.Now;
            if (ModelState.IsValid)
            {
                var hasta = new Hasta();
                var hastaVarmi = db.Hastas.Where(x => x.Tc == hastaView.Tc).Count();

                if (hastaVarmi >= 1)
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

    }
}