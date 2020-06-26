using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class IstatistikController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Istatistik
        public ActionResult Index()
        {
            var toplamUyeler = db.TBLUYELER.Count();
            ViewBag.uyeler = toplamUyeler;

            var toplamKitap = db.TBLKITAP.Count();
            ViewBag.kitaplar = toplamKitap;

            var toplamEmanetKitap = db.TBLKITAP.Where(x=> x.DURUM==false).Count();
            ViewBag.emanetKitap = toplamEmanetKitap;

            var toplamKasa = db.TBLCEZALAR.Sum(x=>x.PARA);
            if (toplamKasa > 0)
            {
                ViewBag.kasa = toplamKasa;
            }
            else
            {
                toplamKasa = 0;
                ViewBag.kasa = toplamKasa;
            }
            
            return View();
        }

        public ActionResult Hava()
        {
            return View();
        }

        public ActionResult HavaKart()
        {
            return View();
        }

        public ActionResult Galeri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResimYukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength>0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"),Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }

        public ActionResult LinqKart()
        {
            var toplamKitap = db.TBLKITAP.Count();
            ViewBag.kitaplar = toplamKitap;

            var toplamUye = db.TBLUYELER.Count();
            ViewBag.uyeler = toplamUye;

            var toplamKasa = db.TBLCEZALAR.Sum(x => x.PARA);
            if (toplamKasa > 0)
            {
                ViewBag.kasa = toplamKasa;
            }
            else
            {
                toplamKasa = 0;
                ViewBag.kasa = toplamKasa;
            }

            var oduncKitap = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            ViewBag.odunc = oduncKitap;

            var kategoriler = db.TBLKATEGORI.Count();
            ViewBag.kategori = kategoriler;

            var enFazlaKitapYazar = db.EnFazlaKitapYazar().FirstOrDefault();
            ViewBag.enfazlakitapyazar = enFazlaKitapYazar;

            var duyuruSayisi = db.TBL_DUYURU.Count();
            ViewBag.duyurusayisi = duyuruSayisi;

            var yayınEvleri = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(z => z.Count()).Select(y => new
            {
                y.Key
            }).FirstOrDefault();

            ViewBag.yayınevi = yayınEvleri;
            
            return View();
        }

    }
}