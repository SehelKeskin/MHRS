using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        //[HttpPost]
        //public ActionResult EkleHasta(string Tc, string Ad, string Soyad, Gender Cinsiyet, DateTime DogumTarihi, string DogumYeri, string AnneAdi, string BabaAdi, string CepTel, string SabitTel, string Mail, string Sifre)
        //{
        //    try
        //    {
        //        //if (ModelState.IsValid)
        //        //{

        //            using (var db = new ApplicationDbContext())
        //            {
        //                Hasta yeniHasta = new Hasta();
        //                yeniHasta.Tc = Tc;
        //                yeniHasta.Ad = Ad;
        //                yeniHasta.Soyad = Soyad;
        //                //  yeniHasta.Cinsiyet = (Gender)Enum.Parse(typeof(Gender), Cinsiyet, true);
        //                yeniHasta.Cinsiyet = Cinsiyet;

        //                yeniHasta.DogumTarihi = DogumTarihi;
        //                yeniHasta.DogumYeri = DogumYeri;
        //                yeniHasta.AnneAdi = AnneAdi;
        //                yeniHasta.BabaAdi = BabaAdi;
        //                yeniHasta.CepTel = CepTel;
        //                yeniHasta.SabitTel = SabitTel;
        //                yeniHasta.Mail = Mail;
        //                yeniHasta.Sifre = Sifre;

        //                db.Hastas.Add(yeniHasta);
        //                db.SaveChanges();
        //             return Json(true);

        //            }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(false);
        //    }
        //}


        //public ActionResult EkleHasta()
        //{
        //    var hasta = new Hasta();
        //    return View(hasta);
        //}

      

        public ActionResult Create()
        {
            var hasta = new Hasta();
            return View(hasta);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Hasta hasta)
        {
            if (ModelState.IsValid)
            {

                using (var db = new ApplicationDbContext())
                {
                    var tcVarmi = db.Hastas.Where(x => x.Tc.Contains(hasta.Tc)).FirstOrDefault();
                    //   var sifreEs = db.Hastas.Where(x=>x.Sifre.CompareTo(hasta.TSifre));
                    // var query = db.Hastas.Where(x=>x.Sifre==hasta.Sifre&& x.TSifre==hasta.TSifre).FirstOrDefault();
                    //var sifreEs = db.Hastas.Where(x => x.Sifre == hasta.Sifre && x.TSifre == hasta.TSifre).FirstOrDefault();
                    // var sifreEs = db.Hastas.Where(x => x.Sifre==x.TSifre ).FirstOrDefault();
                   // var query = from c in db.Hastas where c.Sifre.Equals(c.TSifre) select c;

                   // var query = from c in db.Hastas where hasta.Sifre.Equals(hasta.TSifre) select c;
                    // var sifreEs=  db.Hastas.Where(x=>x.)

                    if (tcVarmi != null)
                    {
                        ViewBag.tcVar = "tc var";
                        return View("Create", hasta);

                    }

                    //if (query!=null)
                    //{
                    //    ViewBag.SifreEslesmiyor = "Şifreler eşleşmiyor";
                    //}

                    if((tcVarmi==null)/*&&(query==null)*/) 
                    {
                        db.Hastas.Add(hasta);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }

                    //return Json(true);
                }
            }

            return View(hasta);
           // return RedirectToAction("Index");
            //return Json(false);
        }

    }
}