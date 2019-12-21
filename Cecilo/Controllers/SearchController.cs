using Cecilo.Models;
using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Cecilo.Controllers
{
    public class SearchController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search
        public ActionResult Index(string ara)
        {



            SearchViewModel model = new SearchViewModel();
            model.Urunlerimiz = db.Urun.Include(a => a.Etiketler).Include(a => a.Markalar).Include(a => a.Resimler).Include(a => a.Renkler).ToList();
            model.Kategoriler = db.Kategori.ToList();
            model.Markalar = db.Markalar.ToList();

            model.SearchString = ara;
            if (!String.IsNullOrEmpty(model.SearchString))
            {
                model.Urunlerimiz = model.Urunlerimiz
                    .Where(a => a.UrunAdi
                    .Replace(" ", "-")
                    .Replace("I", "i")
                    .Replace("ı", "i")
                    .ToLower()
                    .Contains(model.SearchString
                    .Replace(" ", "-")
                    .Replace("I", "i")
                    .Replace("ı", "i")
                    .ToLower()));
            }
            if (!String.IsNullOrEmpty(model.SearchString))
            {
                model.Kategoriler = model.Kategoriler
                    .Where(a => a.KategoriAdi
                    .Replace(" ", "-")
                    .Replace("I", "i")
                    .Replace("ı", "i")
                    .ToLower()
                    .Contains(model.SearchString
                    .Replace(" ", "-")
                    .Replace("I", "i")
                    .Replace("ı", "i")
                    .ToLower()));
            }
            if (!String.IsNullOrEmpty(model.SearchString))
            {
                model.Markalar = model.Markalar
                    .Where(a => a.MarkaAdi
                    .Replace(" ", "-")
                    .Replace("I", "i")
                    .Replace("ı", "i")
                    .ToLower()
                    .Contains(model.SearchString
                    .Replace(" ", "-")
                    .Replace("I", "i")
                    .Replace("ı", "i")
                    .ToLower()));
            }

            return View(model);
        }
    }
}