using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class MesajlarController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Mesajlar
        public ActionResult Index()
        {
            var uyeMail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == uyeMail.ToString()).ToList();
            return View(mesajlar);
        }

        public ActionResult Giden()
        {
            var uyeMail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyeMail.ToString()).ToList();
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR t)
        {
            var uyeMail = (string)Session["Mail"].ToString();
            t.GONDEREN = uyeMail.ToString();
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("Giden","Mesajlar");
        }
    }
}