using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db= new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler=from d in db.TBLMUSTERI select d;

            if (!string.IsNullOrEmpty(p))
            {
                degerler=degerler.Where(m=>m.MUSTERIAD.Contains(p));
            }



            return View(degerler.ToList());
           // var degerler = db.TBLMUSTERI.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERI p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }

            db.TBLMUSTERI.Add(p1);
            db.SaveChanges();
            return View(p1);


        }
        public ActionResult Sil(int id)
        {
            var musteri=db.TBLMUSTERI.Find(id);
            db.TBLMUSTERI.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index"); 

        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERI.Find(id);
            return View("MusteriGetir",mus);

        }
        public ActionResult Guncelle (TBLMUSTERI p1) 
        {
            var musteri2 = db.TBLMUSTERI.Find(p1.MUSTERIID);   
                                                                /*Buradaki p1 view tarafında eşleşecek*/
            musteri2.MUSTERIAD=p1.MUSTERIAD;
            musteri2.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction ("Index");
        }

    }
}