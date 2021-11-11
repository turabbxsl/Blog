using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogsayt;
using Blogsayt.Helpers;
using Blogsayt.Models;
using Blogsayt.ViewModels;

namespace Blogsayt.Controllers
{
    public class MakaleController : YetkiliController
    {
        databaseblog db = new databaseblog();
        // GET: Makale
        public ActionResult Index(string AxtarisEt = null/*,int Kategoriyaid = 0*/)
        {
            ViewBag.kategoriyaid = new SelectList(db.tbl_kategoriya, "kategoriyaid", "kategoriyaadi");

            var meqalelerinhamisi = from i in db.tbl_makale
                                    select i;

            //if (Kategoriyaid != 0)
            //{
            //    meqalelerinhamisi = meqalelerinhamisi.Where(i => i.kategoriyaid == Kategoriyaid);
            //}

            if (!string.IsNullOrEmpty(AxtarisEt))
            {
                meqalelerinhamisi = meqalelerinhamisi.Where(i => i.basliq.Contains(AxtarisEt));
            }

            return View(meqalelerinhamisi);
        }

        // GET: Makale/Details/5
        public ActionResult Details(int ID)
        {

            var meqale = db.tbl_makale.Where(i => i.id == ID).SingleOrDefault();

            if (meqale == null)
            {
                return HttpNotFound();
            }

            SonatilanmeqaleVIEWMODEL vm = new SonatilanmeqaleVIEWMODEL();
            vm.Meqalem = meqale;
            vm.Sonmeqaleler = db.tbl_makale.OrderByDescending(i => i.tarix).Take(5).ToList();


            return View(vm);
        }

        public ActionResult KisiMakaleListele()
        {
            var kullaniciadi = Session["username"].ToString();
            var meqaleler = db.tbl_kullanici.Where(a => a.kullaniciadi == kullaniciadi).SingleOrDefault().tbl_makale.ToList();


            return View(meqaleler);
        }


        // GET: Makale/Create
        public ActionResult Create()
        {
            ViewBag.kategoriyaid = new SelectList(db.tbl_kategoriya, "kategoriyaid", "kategoriyaadi");
            return View();
        }

        // POST: Makale/Create
        [HttpPost]
        public ActionResult Create(tbl_makale model, string etiketler)
        {
            try
            {
                string kullaniciadi = Session["username"].ToString();
                var kullanici = db.tbl_kullanici.Where(i => i.kullaniciadi == kullaniciadi).SingleOrDefault();
                model.kullaniciid = kullanici.id;

                db.tbl_makale.Add(model);

                if (!string.IsNullOrEmpty(etiketler))
                {
                    string[] etiketmassiv = etiketler.Split(',');
                    foreach (var item in etiketmassiv)
                    {
                        var yenietiket = new tbl_etiket { etiketad = item };
                        db.tbl_etiket.Add(yenietiket);
                        model.tbl_etiket.Add(yenietiket);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index", "Kullanici");
            }
            catch
            {
                return View();
            }
        }

        // GET: Makale/Edit/5
        public ActionResult Edit(int ID)
        {
            string strkullaniciadi = Session["username"].ToString();
            var meqale = db.tbl_makale.Where(i => i.id == ID).SingleOrDefault();
            if (meqale == null)
            {
                return HttpNotFound();
            }

            if (meqale.tbl_kullanici.kullaniciadi == strkullaniciadi)
            {
                ViewBag.KATEGORIYAID = new SelectList(db.tbl_kategoriya, "kategoriyaid", "kategoriyaadi");
                return View(meqale);
            }
            return HttpNotFound();
        }

        // POST: Makale/Edit/5
        [HttpPost]
        public ActionResult Edit(int ID, tbl_makale model)
        {
            try
            {
                var meqale = db.tbl_makale.Where(i => i.id == ID).SingleOrDefault();
                meqale.basliq = model.basliq;
                meqale.icerik = model.icerik;
                meqale.kategoriyaid = model.kategoriyaid;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }


        public ActionResult Delete(int ID)
        {
            try
            {
                var strkullaniciadi = Session["username"].ToString();
                var kullanici = db.tbl_kullanici.Where(i => i.kullaniciadi == strkullaniciadi).SingleOrDefault();
                var meqale = db.tbl_makale.Where(i => i.id == ID).SingleOrDefault();
                if (kullanici.id == meqale.kullaniciid)
                {
                    //foreach (var item1 in meqale.tbl_yorum.ToList())
                    //{
                    //    db.tbl_yorum.Remove(item1);
                    //    //db.SaveChanges();
                    //}
                    //foreach (var item2 in meqale.tbl_etiket.ToList())
                    //{
                    //    db.tbl_etiket.Remove(item2);
                    //    //db.SaveChanges();
                    //}
                    db.tbl_makale.Remove(meqale);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Xeta", "Yetkili", new { yazilacaq = "Meqale Siline Bilmedi" });

            }
            catch
            {
                return RedirectToAction("Xeta", "Yetkili", new { yazilacaq = "Meqale Siline Bilmedi" });
            }
        }
        public JsonResult yorumyaz(string yorum, int makaleid)
        {
            var KullaniciAdi = Session["username"].ToString();
            var kullanici = db.tbl_kullanici.Where(i => i.kullaniciadi == KullaniciAdi).SingleOrDefault();
            if (yorum == "")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.tbl_yorum.Add(new tbl_yorum { yorumkullaniciID = kullanici.id, yorummakaleID = makaleid, tarix = DateTime.Now, yorumicerik = yorum });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YorumDelete(int ID)
        {

            var strkullaniciadi = Session["username"].ToString();
            var kullanici = db.tbl_kullanici.Where(i => i.kullaniciadi == strkullaniciadi).SingleOrDefault();
            var yorum = db.tbl_yorum.Where(i => i.id == ID).SingleOrDefault();
            //var meqale = db.tbl_makale.Where(i => i.id == yorum.yorummakaleID).SingleOrDefault();

            if (yorum == null)
            {
                return RedirectToAction("Xeta", "Yetkili", new { yazilacaq = "Yorum  Tapila Bilmedi." });
            }

            if (Ortaqsinif.DeleteizinyetkiVARMI(ID, kullanici)/* || meqale.kullaniciid == kullanici.id*/)
            {
                db.tbl_yorum.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("Details", "Makale", new { id = yorum.yorummakaleID });
            }

            return RedirectToAction("Xeta", "Yetkili", new { yazilacaq = "Yorum Siline Bilmedi." });

        }
    }
}
