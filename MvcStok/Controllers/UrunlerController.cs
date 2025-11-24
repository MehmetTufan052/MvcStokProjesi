using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        MvcDbStokEntities db = new MvcDbStokEntities();           
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun() 
        {
            List<SelectListItem> degerler =(from i in db.TBLKATEGORI.ToList()
                                            select new SelectListItem
                                            {
                                                Text=i.KATEGORIAD,                  /*Burada degerler diye bir değişken atayıp sonrasında kategori tablosundaki verileri listeledik.Daha sonra kategori adı direkt olarak text olarak çektik sonrasında value olarak da kategorııdyi kullanarak biz hangi texti seçersek o textin kategorııdsine bakıp ona göre getirecek.*/
                                                Value=i.KATEGORIID.ToString()       

                                            }).ToList();
            ViewBag.dgr = degerler;         /*Daha sonra burada viewbag kullanarak başka sayfaya bu degerleri taşıyabilme işlemini gerçekleştirmiş olacağız.*/
            return View();

        }
        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            var ktg=db.TBLKATEGORI.Where(m=>m.KATEGORIID==p1.TBLKATEGORI.KATEGORIID).FirstOrDefault();  /*FirstOrDefault burada kullandığımız linq komutunda kullanılan bir ifade.İlk veya default ifadeyi çağırmamıza yardımcı oluyor.*/
            p1.TBLKATEGORI= ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");  /*Kaydettikten sonra bizi ındex sayfasına yönlendirmesini sağlamış olduk.*/
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");  

        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,                  /*Burada degerler diye bir değişken atayıp sonrasında kategori tablosundaki verileri listeledik.Daha sonra kategori adı direkt olarak text olarak çektik sonrasında value olarak da kategorııdyi kullanarak biz hangi texti seçersek o textin kategorııdsine bakıp ona göre getirecek.*/
                                                 Value = i.KATEGORIID.ToString()

                                             }).ToList();
            ViewBag.dgr = degerler;         /*Daha sonra burada viewbag kullanarak başka sayfaya bu degerleri taşıyabilme işlemini gerçekleştirmiş olacağız.*/
            

            return View("UrunGetir", urun);

        }

        public ActionResult Guncelle (TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA=p.MARKA;
            urun.STOK = p.STOK;
            urun.FIYAT = p.FIYAT;
            //urun.URUNKATEGORİ=p.URUNKATEGORİ;
            var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == p.TBLKATEGORI.KATEGORIID).FirstOrDefault();  /*FirstOrDefault burada kullandığımız linq komutunda kullanılan bir ifade.İlk veya default ifadeyi çağırmamıza yardımcı oluyor.*/
            urun.URUNKATEGORİ = ktg.KATEGORIID;

            db.SaveChanges();
           
            return RedirectToAction("Index");

        }
    }
}