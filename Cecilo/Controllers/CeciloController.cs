using Cecilo.Models;
using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text;
using PagedList;
using Cecilo.Models.Mail;
using BotDetect.Web.Mvc;
using Cecilo.Helpers;

namespace Cecilo.Controllers
{
    public class CeciloController : Controller
    {
        StringBuilder sb = new StringBuilder();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cecilo
        public ActionResult Index()
        {
            return View();
        }
        [Route("{title}")]
        public ActionResult Hakkimizda(string title)
        {
            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HakkimizdaMenu menu = db.HakkimizdaMenu.Where(a => a.MenuAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")).FirstOrDefault();
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        public ActionResult Urunlerimiz(string ara, string siralama, string sonArananKelime, int? sayfaNo)
        {

            if (ara != null)
            {
                sayfaNo = 1;
            }
            else
            {
                ara = sonArananKelime;
            }



            ViewBag.SonArananKelime1 = ara;





            if (String.IsNullOrEmpty(siralama))
            {
                ViewBag.AdaGoreSirala2 = "ZdenAya";
            }
            else
            {
                ViewBag.AdaGoreSirala2 = string.Empty;
            }


            var urunler = db.Urun.Include(k => k.Kategori)
                .Include(a => a.Renkler)
                .Include(a => a.Resimler)
                .Include(a => a.Markalar)
                .Include(a => a.Etiketler)
                .Include(a=>a.Kategori).ToList();
            //KategoriAgaciOlustur(kategoriler.ToList());


            if (!String.IsNullOrEmpty(ara))
            {
                urunler = urunler.Where(s => s.UrunAdi.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())
                || s.Aciklama.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())).ToList();
            }

            int sayfaBuyuklugu = 6;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(urunler.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));

        }
        [HttpGet]
        [Route("urun-kategori/{title}")]
        public ActionResult KategoriyeGoreUrunlerimiz(string title, string ara, string siralama, string sonArananKelime, int? sayfaNo)
        {

            if (ara != null)
            {
                sayfaNo = 1;
            }
            else
            {
                ara = sonArananKelime;
            }



            ViewBag.SonArananKelime2 = ara;





            if (String.IsNullOrEmpty(siralama))
            {
                ViewBag.AdaGoreSirala3 = "ZdenAya";
            }
            else
            {
                ViewBag.AdaGoreSirala3 = string.Empty;
            }

            var kategoriUrunleri = db.Urun
                .Include(a => a.Kategori)
                .Include(a=>a.Markalar)
                .Include(a=>a.Renkler)
                .Include(a=>a.Resimler)
                .Include(a => a.Etiketler)
                .Where((a => a.Kategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
                || a.Kategori.UstKategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
                || a.Kategori.UstKategori.UstKategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
                )).ToList();


            if (!String.IsNullOrEmpty(ara))
            {
                kategoriUrunleri = kategoriUrunleri.Where(s => s.UrunAdi.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())
                || s.Aciklama.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())).ToList();
            }

            int sayfaBuyuklugu = 6;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(kategoriUrunleri.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }


        [HttpGet]
        [Route("marka/{title}")]
        public ActionResult MarkayaGoreUrunlerimiz(string title, string ara, string siralama, string sonArananKelime, int? sayfaNo)
        {

            if (ara != null)
            {
                sayfaNo = 1;
            }
            else
            {
                ara = sonArananKelime;
            }



            ViewBag.SonArananKelime3 = ara;





            if (String.IsNullOrEmpty(siralama))
            {
                ViewBag.AdaGoreSirala4 = "ZdenAya";
            }
            else
            {
                ViewBag.AdaGoreSirala4 = string.Empty;
            }

            var markaUrunleri = db.Urun
                .Include(a => a.Kategori)
                .Include(a => a.Markalar)
                .Include(a => a.Renkler)
                .Include(a => a.Resimler)
                .Include(a => a.Etiketler)
                .Where((a => a.Markalar.MarkaAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-"))).ToList();

            if (!String.IsNullOrEmpty(ara))
            {
                markaUrunleri = markaUrunleri.Where(s => s.UrunAdi.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())
                || s.Aciklama.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())).ToList();
            }

            int sayfaBuyuklugu = 6;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(markaUrunleri.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }


        [Route("urunlerimiz/{title}")]
        public ActionResult UrunDetay(string title)
        {
            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun
                .Include(a=>a.Kategori)
                .Include(a=>a.Renkler)
                .Include(a=>a.Resimler)
                .Include(a=>a.Markalar)
                .Include(a=>a.Etiketler)
                .Where(a => a.UrunAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-"))
                .FirstOrDefault();
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        public ActionResult Belgelerimiz()
        {
            return View();
        }
        [Route("cecilo/iletisim")]
        public ActionResult Iletisim(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
               message == ManageMessageId.FormSuccess ? "Talebiniz başarılı bir şekilde gönderildi."
               : message == ManageMessageId.Error ? "Lütfen zorunlu alanları doldurunuz!"             
               : "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("cecilo/iletisim")]
        [CaptchaValidationActionFilter("CaptchaCode", "RegistrationCaptcha",
    "Girdiğiniz Kod Yanlış!")]
        public ActionResult Iletisim([Bind(Include = "Id,AdiSoyadi,Eposta,Telefon,Konu,Mesaj,OkunduMu,CreatedDate")] Iletisim iletisim)
        {

            ManageMessageId? message;

            if (ModelState.IsValid)
            {
                iletisim.CreatedDate = DateTime.Now;
                db.Iletisims.Add(iletisim);
               
                db.SaveChanges();
                MvcCaptcha.ResetCaptcha("RegistrationCaptcha");
                // Mail.MailSender($"{iletisim.AdiSoyadi} kişisinden konusu {iletisim.Konu} olan bir mail aldınız! {iletisim.CreatedDate}");

                message = ManageMessageId.FormSuccess;
                return RedirectToAction("iletisim", "cecilo", new { Message = message });
            }
            else
            {
                message = ManageMessageId.Error;
                MvcCaptcha.ResetCaptcha("RegistrationCaptcha");
                return View(new { Message = message});
            }
        }

        
    }
}