using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLKATEGORI.ToList();
            var degerler = db.TBLKATEGORI.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
            //Burada HttpGet ve HttpPost kullanımıyla beraber sayfanın refresh edilmesiyle Add işlemi gerçekleşip otomatik olarak ıd eklemesi yapması engellenmiştir.
            //HttpGet ile sayfa yüklenince yapılması gerekenler,HttpPost ile de belirttiğimiz ActionResult üzerinden bir view içerisinde post kullanımı olmadığı sürece bir şey yapılmamasını söylemiş olduk.
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORI p1) {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
           }
            db.TBLKATEGORI.Add(p1);
            db.SaveChanges();
            return View();
        
        }
        public ActionResult Sil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORI.Find(id);
            return View("KategoriGetir", ktgr);

        }
        public ActionResult Guncelle(TBLKATEGORI p1)
        {
            var ktgr = db.TBLKATEGORI.Find(p1.KATEGORIID);
            ktgr.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}