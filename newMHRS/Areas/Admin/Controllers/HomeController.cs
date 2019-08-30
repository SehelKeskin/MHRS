using newMHRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace newMHRS.Areas.Admin.Controllers
{   //[Authorize]
    public class HomeController : Controller
    {
       
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AdminLogin(AdminClass admin)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                var adminVarmi = db.AdminClasses.Where(x => x.Tc == admin.Tc && x.Sifre == admin.Sifre).FirstOrDefault();

                if (adminVarmi == null)

                {
                    //hasta.LoginErorMsg = "Tc'nizi veya şifrenizi kontrol ediniz.";
                    ViewBag.Mesaj = "Tc'nizi veya şifrenizi kontrol ediniz.";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(adminVarmi.Ad, false);

                    return RedirectToAction("Index", "Home");


                }
            }



        }


            public ActionResult AdminLogout()
            {
            FormsAuthentication.SignOut();
                return RedirectToAction("AdminLogin");
            }



        
    }
}