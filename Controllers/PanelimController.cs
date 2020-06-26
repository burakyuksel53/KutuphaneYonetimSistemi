using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKutuphane1.Controllers
{
    public class PanelimController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();

        public object FormAuthentication { get; private set; }

        // GET: Panelim
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var uyeMail = (string)Session["Mail"];
            var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyeMail);
            return View(degerler);
        }

        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);
            uye.SIFRE = p.SIFRE;
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.OKUL = p.OKUL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.TELEFON = p.TELEFON;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarım()
        {
            var uyeMail = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAIL == uyeMail.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            return View(degerler);
        }

        public ActionResult Duyurular()
        {
            var duyuruListesi = db.TBL_DUYURU.ToList();
            return View(duyuruListesi);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}