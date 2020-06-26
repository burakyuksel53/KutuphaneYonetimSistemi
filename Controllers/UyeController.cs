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
    public class UyeController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Uye

        public ActionResult Index(int sayfa=1)
        {
            var uyeler = db.TBLUYELER.ToList().ToPagedList(sayfa,5);
            return View(uyeler);
        }

        [HttpGet]
        public ActionResult UyeEkle()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult UyeEkle(TBLUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYELER.Find(id);
            db.TBLUYELER.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UyeGetir(int id)
        {
            var uyeler = db.TBLUYELER.Find(id);
            return View("UyeGetir", uyeler);
        }

        [HttpPost]
        public ActionResult UyeGuncelle(TBLUYELER uye)
        {
            var uyeler = db.TBLUYELER.Find(uye.ID);
            uyeler.AD = uye.AD;
            uyeler.SOYAD = uye.SOYAD;
            uyeler.MAIL = uye.MAIL;
            uyeler.KULLANICIADI = uye.KULLANICIADI;
            uyeler.SIFRE = uye.SIFRE; 
            uyeler.FOTOGRAF = uye.FOTOGRAF; 
            uyeler.TELEFON = uye.TELEFON; 
            uyeler.OKUL = uye.OKUL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeKitapGecmis(int id)
        {
            var gecmisKitap = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            var uyeAd = db.TBLUYELER.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.uyead = uyeAd;
            return View(gecmisKitap);
        }
    }
}