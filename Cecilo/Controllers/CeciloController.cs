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
using System.Globalization;

namespace Cecilo.Controllers
{

    public class CeciloController : BaseController
    {
        StringBuilder sb = new StringBuilder();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cecilo
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
        [Route("SetCulture")]
        public ActionResult SetCulture(string culture, string returnUrl)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        public ActionResult Index()
        {
            return View();
        }
        [Route("{title}")]
        public ActionResult Hakkimizda(string title)
        {
            HakkimizdaMenu menu = new HakkimizdaMenu();

            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (CultureHelper.GetCurrentNeutralCulture() == "tr")
            {
                menu = db.HakkimizdaMenu.Where(a => a.MenuAdi
               .Replace(" ", "-")
               .Replace(".", "")
               .Replace("İ", "i")
               .Replace("I", "i")
               .Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") && a.Lang == LanguageId.Tr).FirstOrDefault();
            }
            else if (CultureHelper.GetCurrentNeutralCulture() == "en")
            {
                menu = db.HakkimizdaMenu.Where(a => a.MenuAdi
                .Replace(" ", "-")
                .Replace(".", "")
                .Replace("İ", "i")
                .Replace("I", "i")
                .Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") && a.Lang == LanguageId.En).FirstOrDefault();
            }


            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        [Route("Urunlerimiz")]
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

            List<Urun> urunler = new List<Urun>();

            if (CultureHelper.GetCurrentNeutralCulture() == "en")
            {
                urunler = db.Urun.Include(k => k.Kategori)
                .Include(a => a.Renkler)
                .Include(a => a.Resimler)
                .Include(a => a.Markalar)
                .Include(a => a.Etiketler)
                .Include(a => a.Kategori).Where(a => a.Lang == LanguageId.En).ToList();
            }
            else if (CultureHelper.GetCurrentNeutralCulture() == "tr")
            {
                urunler = db.Urun.Include(k => k.Kategori)
                .Include(a => a.Renkler)
                .Include(a => a.Resimler)
                .Include(a => a.Markalar)
                .Include(a => a.Etiketler)
                .Include(a => a.Kategori).Where(a => a.Lang == LanguageId.Tr).ToList();
            }


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
        [Route("c-{title}")]
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

            List<Urun> kategoriUrunleri = new List<Urun>();

            if (CultureHelper.GetCurrentNeutralCulture() == "tr")
            {
                kategoriUrunleri = db.Urun
               .Include(a => a.Kategori)
               .Include(a => a.Markalar)
               .Include(a => a.Renkler)
               .Include(a => a.Resimler)
               .Include(a => a.Etiketler)
               .Where((a => a.Kategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
               || a.Kategori.UstKategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
               || a.Kategori.UstKategori.UstKategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
               )).Where(a=>a.Lang == LanguageId.Tr).ToList();
            }
            else if (CultureHelper.GetCurrentNeutralCulture() == "en")
            {
                kategoriUrunleri = db.Urun
               .Include(a => a.Kategori)
               .Include(a => a.Markalar)
               .Include(a => a.Renkler)
               .Include(a => a.Resimler)
               .Include(a => a.Etiketler)
               .Where((a => a.Kategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
               || a.Kategori.UstKategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
               || a.Kategori.UstKategori.UstKategori.KategoriAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")
               )).Where(a => a.Lang == LanguageId.En).ToList();
            }

            


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
        [Route("m-{title}")]
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

            List<Urun> markaUrunleri = new List<Urun>();

            if (CultureHelper.GetCurrentNeutralCulture() == "tr")
            {
                markaUrunleri = db.Urun
                .Include(a => a.Kategori)
                .Include(a => a.Markalar)
                .Include(a => a.Renkler)
                .Include(a => a.Resimler)
                .Include(a => a.Etiketler)
                .Where((a => a.Markalar.MarkaAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")))
                .Where(a=>a.Lang == LanguageId.Tr)
                .ToList();
            }
            else if (CultureHelper.GetCurrentNeutralCulture() == "en")
            {
                markaUrunleri = db.Urun
                 .Include(a => a.Kategori)
                 .Include(a => a.Markalar)
                 .Include(a => a.Renkler)
                 .Include(a => a.Resimler)
                 .Include(a => a.Etiketler)
                 .Where((a => a.Markalar.MarkaAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-")))
                 .Where(a => a.Lang == LanguageId.En)
                 .ToList();
            }




             

            if (!String.IsNullOrEmpty(ara))
            {
                markaUrunleri = markaUrunleri.Where(s => s.UrunAdi.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())
                || s.Aciklama.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower().Contains(ara.Replace(" ", "-").Replace("I", "i").Replace("ı", "i").ToLower())).ToList();
            }

            int sayfaBuyuklugu = 6;
            int sayfaNumarasi = (sayfaNo ?? 1);


            return View(markaUrunleri.ToPagedList(sayfaNumarasi, sayfaBuyuklugu));
        }


        [Route("p-{title}")]
        public ActionResult UrunDetay(string title)
        {
            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Urun urun = new Urun();

            if (CultureHelper.GetCurrentNeutralCulture() == "tr")
            {
               urun = db.Urun
                .Include(a => a.Kategori)
                .Include(a => a.Renkler)
                .Include(a => a.Resimler)
                .Include(a => a.Markalar)
                .Include(a => a.Etiketler)
                .Where(a => a.UrunAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-"))
                .Where(a=>a.Lang == LanguageId.Tr)
                .FirstOrDefault();
            }else if (CultureHelper.GetCurrentNeutralCulture() == "en")
            {
                urun = db.Urun
                .Include(a => a.Kategori)
                .Include(a => a.Renkler)
                .Include(a => a.Resimler)
                .Include(a => a.Markalar)
                .Include(a => a.Etiketler)
                .Where(a => a.UrunAdi.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-") == title.Replace(" ", "-").Replace(".", "").Replace("İ", "i").Replace("I", "i").Replace("&", "-"))
                .Where(a => a.Lang == LanguageId.En)
                .FirstOrDefault();
            }
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }
        [Route("belgelerimiz")]
        public ActionResult Belgelerimiz()
        {
            return View();
        }
        [Route("iletisim")]
        public ActionResult Iletisim(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
               message == ManageMessageId.FormSuccess ? $"{Resources.Resources.FormSuccess}"
               : message == ManageMessageId.Error ? $"{Resources.Resources.Error}"
               : "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("iletisim")]
        [CaptchaValidationActionFilter("CaptchaCode", "RegistrationCaptcha", "Girdiğiniz Kod Yanlış!")]
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

                //return Redirect($"~/{CultureHelper.GetCurrentCulture()}/iletisim?Message={message}");
                return RedirectToAction("iletisim", new { Message = message });
            }
            else
            {
                message = ManageMessageId.Error;
                MvcCaptcha.ResetCaptcha("RegistrationCaptcha");
                return View(new { Message = message });
            }
        }


    }
}