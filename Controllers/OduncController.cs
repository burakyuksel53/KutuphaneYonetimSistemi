using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class OduncController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Odunc
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x=> x.ISLEMDURUM==false).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> uyeAd = (from x in db.TBLUYELER.ToList().OrderBy(x=>x.AD)
                                          select new SelectListItem
                                          {
                                              Text = x.AD + " " + x.SOYAD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.uyead = uyeAd;

            List<SelectListItem> kitapAd = (from x in db.TBLKITAP.Where(x=>x.DURUM==true).ToList().OrderBy(x=>x.AD)
                                          select new SelectListItem
                                          {
                                              Text = x.AD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.kitapad = kitapAd;

            List<SelectListItem> personelAd = (from x in db.TBLPERSONEL.ToList().OrderBy(x => x.PERSONEL)
                                            select new SelectListItem
                                            {
                                                Text = x.PERSONEL,
                                                Value = x.ID.ToString()
                                            }).ToList();
            ViewBag.personelad = personelAd;
            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            var uyeAdi = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var kitapAdi = db.TBLKITAP.Where(y => y.ID == p.TBLKITAP.ID).FirstOrDefault();
            var personelAdi = db.TBLPERSONEL.Where(z => z.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            p.TBLUYELER = uyeAdi;
            p.TBLKITAP = kitapAdi;
            p.TBLPERSONEL = personelAdi;
            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Odunciade (TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());            
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;

            ViewBag.dgr = d3.TotalDays;
            return View("Odunciade", odn);
        }

        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}