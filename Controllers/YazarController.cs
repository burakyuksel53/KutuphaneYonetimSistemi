using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class YazarController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Yazar
        public ActionResult Index(int sayfa=1)
        {
            var yazarlar = db.TBLYAZAR.ToList().ToPagedList(sayfa,10);
            return View(yazarlar);
        }

        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR yazar)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarSil(int id)
        {
            var yazarlar = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(yazarlar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult YazarGetir(int id)
        {
            var yazarlar = db.TBLYAZAR.Find(id);
            return View("YazarGetir", yazarlar);
        }

        [HttpPost]
        public ActionResult YazarGuncelle(TBLYAZAR yazar)
        {
            var yazarlar = db.TBLYAZAR.Find(yazar.ID);
            yazarlar.AD = yazar.AD;
            yazarlar.SOYAD = yazar.SOYAD;
            yazarlar.DETAY = yazar.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TBLKITAP.Where(x => x.YAZAR == id).ToList();
            var yazarAd = db.TBLYAZAR.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.yazarad = yazarAd;
            return View(yazar);
        }
    }
}