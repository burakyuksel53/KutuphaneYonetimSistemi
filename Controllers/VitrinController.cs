using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane1.Models.Siniflarim;
namespace MvcKutuphane1.Controllers
{
    public class VitrinController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Vitrin
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TBLKITAP.ToList();
            cs.Deger2 = db.TBLHAKKİMİZDA.ToList();           
            return View(cs);
        }

        [HttpPost]
        public ActionResult Index(TBL_ILETISIM t)
        {
            db.TBL_ILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}