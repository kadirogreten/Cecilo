using Cecilo.Areas.AbatPanel.Models;
using IdentitySample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.Entity;

namespace Cecilo.Areas.AbatPanel.Controllers
{
    [Authorize(Roles = "Admin,Cecilo")]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AbatPanel/Home
        public ActionResult Index()
        {

            DashboardViewModel model = new DashboardViewModel()
            {
                Urunlerimiz = db.Urun.Include(a=>a.Etiketler),
                Belgelerimiz = db.Belgelerimiz,
                Sliders = db.Slider,
                Kategoriler = db.Kategori,
                Markalar = db.Markalar.Include(a=>a.Urunler)
            };
           

            //string response = JsonConvert.SerializeObject(model.Urunlerimiz.Select(a=> new { UrunAdi = a.UrunAdi}).ToList(), Formatting.Indented, new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            ViewBag.json = model.Urunlerimiz.OrderByDescending(a => a.Id).Select(a =>a.UrunAdi).Take(4).ToArray();
            ViewBag.count = model.Urunlerimiz.OrderByDescending(a => a.Id).Select(a => Convert.ToInt32(a.Fiyat)).Take(4).ToArray();

            ViewBag.marka = model.Markalar.OrderByDescending(a => a.Urunler.Count).Select(a => a.MarkaAdi).Take(4).ToArray();

            ViewBag.markaUrun = model.Markalar.OrderByDescending(a => a.Urunler.Count).Take(4).ToArray();


            return View(model);
        }
    }
}