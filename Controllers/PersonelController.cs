using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class PersonelController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var personeller = db.TBLPERSONEL.ToList();
            return View(personeller);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            db.TBLPERSONEL.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelSil(int id)
        {
            var personel = db.TBLPERSONEL.Find(id);
            db.TBLPERSONEL.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        } 

        [HttpGet]
        public ActionResult PersonelGetir(int id)
        {
            var prs = db.TBLPERSONEL.Find(id);
            return View("PersonelGetir", prs);
        }

        public ActionResult PersonelGuncelle(TBLPERSONEL p)
        {
            var prs = db.TBLPERSONEL.Find(p.ID);
            prs.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}