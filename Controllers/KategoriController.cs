using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane1.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class KategoriController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Kategori
        public ActionResult Index(int sayfa=1)
        {
            var kategoriler = db.TBLKATEGORI.ToList().Where(x=>x.DURUM==true).ToPagedList(sayfa,10);
            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI p)
        {
            p.DURUM = true;
            db.TBLKATEGORI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var kategoriler = db.TBLKATEGORI.Find(id);            
            kategoriler.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var kategoriler = db.TBLKATEGORI.Find(id);
            return View("KategoriGetir",kategoriler);
        }

        [HttpPost]
        public ActionResult Guncelle(TBLKATEGORI ktgr)
        {
            var kategoriler = db.TBLKATEGORI.Find(ktgr.ID);
            kategoriler.AD = ktgr.AD;db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}