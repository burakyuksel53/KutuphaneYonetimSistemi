using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class KitapController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Kitap
        public ActionResult Index(string p,int sayfa=1)
        {
            var kitaplar = from k in db.TBLKITAP select k;

            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(p));
            }
            //var kitaplar = db.TBLKITAP.ToList();
            return View(kitaplar.ToList().OrderByDescending(x=>x.ID).ToPagedList(sayfa,5));
        }

        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList().OrderBy(x=>x.AD)
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList().OrderBy(x => x.AD)
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult KitapEkle(TBLKITAP p, HttpPostedFileBase KITAPRESIM)
        {
            
                WebImage img = new WebImage(KITAPRESIM.InputStream);
                FileInfo imginfo = new FileInfo(KITAPRESIM.FileName);

                string kitapresimname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(500, 500);
                img.Save("~/Uploads/Kitap/" + kitapresimname);

                p.KITAPRESIM = "/Uploads/Kitap/" + kitapresimname;
            
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
                var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
                p.TBLKATEGORI = ktg;
                p.TBLYAZAR = yzr;
                db.TBLKITAP.Add(p);
                db.SaveChanges();                            
            return RedirectToAction("Index");
        }

        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult KitapGetir(int id)
        {
            var kitaplar = db.TBLKITAP.Find(id);

            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            return View("KitapGetir", kitaplar);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult KitapGuncelle(TBLKITAP p, HttpPostedFileBase KITAPRESIM)
        {
            var kitap = db.TBLKITAP.Find(p.ID);

            if (KITAPRESIM != null)
            {
                if (System.IO.File.Exists(Server.MapPath(kitap.KITAPRESIM)))
                {
                    System.IO.File.Delete(Server.MapPath(kitap.KITAPRESIM));
                }
                WebImage img = new WebImage(KITAPRESIM.InputStream);
                FileInfo imginfo = new FileInfo(KITAPRESIM.FileName);

                string hizmetname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(500, 500);
                img.Save("~/Uploads/Kitap/" + hizmetname);

                kitap.KITAPRESIM = "/Uploads/Kitap/" + hizmetname;
            }

            kitap.AD = p.AD;
            kitap.BASIMYIL = p.BASIMYIL;          
            kitap.SAYFA = p.SAYFA;
            kitap.YAYINEVI = p.YAYINEVI;
            kitap.DURUM = true;

            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;

            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}