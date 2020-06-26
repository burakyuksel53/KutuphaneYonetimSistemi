using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class DuyuruController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Duyuru
        public ActionResult Index()
        {
            var duyurular = db.TBL_DUYURU.ToList();
            return View(duyurular);
        }

        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniDuyuru(TBL_DUYURU t)
        {
            db.TBL_DUYURU.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBL_DUYURU.Find(id);
            db.TBL_DUYURU.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DuyuruDetay(TBL_DUYURU p)
        {
            var duyuruDetay = db.TBL_DUYURU.Find(p.ID);
            return View("DuyuruDetay", duyuruDetay);
        }

        [HttpPost]
        public ActionResult DuyuruGuncelle(TBL_DUYURU p)
        {
            var dyr = db.TBL_DUYURU.Find(p.ID);
            dyr.KATEGORI = p.KATEGORI;
            dyr.ICERIK = p.ICERIK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}