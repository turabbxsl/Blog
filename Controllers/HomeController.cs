using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogsayt.Models;
using Blogsayt;

namespace Blogsayt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        databaseblog db = new databaseblog();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_kullanici model)
        {
            try
            {
                var varmi = db.tbl_kullanici.Where(i => i.kullaniciadi == model.kullaniciadi).SingleOrDefault();
                if (varmi == null)
                {
                    return View();
                }
                if (varmi.sifre == model.sifre)
                {
                    Session["username"] = model.kullaniciadi;
                    return RedirectToAction("Index","Kullanici");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }




        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanici/Create
        [HttpPost]
        public ActionResult Create(tbl_kullanici model)
        {
            try
            {
                var varmi = db.tbl_kullanici.Where(i => i.kullaniciadi == model.kullaniciadi).SingleOrDefault();

                if (varmi != null)
                {
                    return View();
                }

                if (string.IsNullOrEmpty(model.sifre))
                {
                    return View();
                }

                model.yetkiid = 1;
                db.tbl_kullanici.Add(model);
                db.SaveChanges();

                Session["username"] = model.kullaniciadi;

                return RedirectToAction("Index","Kullanici");
            }
            catch
            {
                return View();
            }
        }
    }
}