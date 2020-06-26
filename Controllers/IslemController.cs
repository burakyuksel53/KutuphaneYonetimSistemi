using MvcKutuphane1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane1.Controllers
{
    public class IslemController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        // GET: Islem
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == true).ToList();
            return View(degerler);
            
        }
    }
}