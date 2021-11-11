using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogsayt.Models;
using Blogsayt;
using Blogsayt.Helpers;

namespace Blogsayt.Controllers
{
    public class KullaniciController : YetkiliController
    {
        databaseblog db = new databaseblog();
        
        public ActionResult Index()
        {
            string strkullaniciadi = Session["username"].ToString();
            var kullanici = db.tbl_kullanici.Where(i => i.kullaniciadi == strkullaniciadi).SingleOrDefault();
            //ViewBag.deneme = kullanici.isim; 
            return View(kullanici);
        }

        // GET: Kullanici/Details/5
        public ActionResult Details(int id)
        {
            var kisi = db.tbl_kullanici.Where(i => i.id == id).SingleOrDefault();

            return View(kisi);
        }

        public ActionResult Profil()
        {
            string kullaniciadi = Session["username"].ToString();
            var kisi = db.tbl_kullanici.Where(i => i.kullaniciadi == kullaniciadi).SingleOrDefault();

            return View(kisi);
        }


     

        public ActionResult Logout()
         {
            Session["username"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int ID)
        {
            string strkullaniciadi = Session["username"].ToString();
            var user = db.tbl_kullanici.Where(i => i.kullaniciadi == strkullaniciadi).SingleOrDefault();
            if (Ortaqsinif.EditizinyetkiVARMI(ID,user))
            {
                var kisi = db.tbl_kullanici.Where(i => i.id == ID).SingleOrDefault();
                return View(kisi);
            }
            return HttpNotFound();
        }

        // POST: Kullanici/Edit/5
        [HttpPost]
        public ActionResult Edit(int ID, tbl_kullanici model)
        {
            try
            {
                //Buda Olar
                //if (ModelState.IsValid)
                //{
                //    db.Entry(model).State = System.Data.Entity.EntityState.Modified;

                //    return RedirectToAction("Index","Kullanici");
                //}

                var kisi = db.tbl_kullanici.Where(i => i.id == ID).SingleOrDefault();
                kisi.isim = model.isim;
                kisi.kullaniciadi = model.kullaniciadi;
                kisi.soyisim = model.soyisim;
                kisi.sifre = model.sifre;
                kisi.email = model.email;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            catch
            {
                return View(model);
            }
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kullanici/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
