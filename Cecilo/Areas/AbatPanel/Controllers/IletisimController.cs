using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cecilo.Models;
using IdentitySample.Models;

namespace Cecilo.Areas.AbatPanel.Controllers
{
    [Authorize(Roles = "Admin,Cecilo")]
    public class IletisimController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AbatPanel/Iletisim
        public ActionResult Index()
        {
            return View(db.Iletisims.Where(a=>a.OkunduMu == false).ToList());
        }

        public ActionResult Okunanlar()
        {
            return View(db.Iletisims.Where(a=>a.OkunduMu == true).ToList());
        }

        // GET: AbatPanel/Iletisim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisims.Find(id);
            iletisim.OkunduMu = true;

            db.SaveChanges();
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // GET: AbatPanel/Iletisim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AbatPanel/Iletisim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdiSoyadi,Eposta,Telefon,Konu,Mesaj,OkunduMu,CreatedDate")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                iletisim.CreatedDate = DateTime.Now;
                db.Iletisims.Add(iletisim);
                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.SaveChanges();

                return View(iletisim);
            }
            else
            {
                ViewBag.Mesaj = "Hata";
                ViewBag.Status = "error";
                ViewBag.Baslik = "Oops!";

                return View(iletisim);
            }
        }

        // GET: AbatPanel/Iletisim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisims.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: AbatPanel/Iletisim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdiSoyadi,Eposta,Telefon,Konu,Mesaj,OkunduMu,CreatedDate")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iletisim).State = EntityState.Modified;
                db.SaveChanges();

                string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/iletisim/edit/" + iletisim.Id + "';</script>";
                return Content(mesaj);
            }
            return View(iletisim);
        }

        // GET: AbatPanel/Iletisim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisims.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: AbatPanel/Iletisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Iletisim iletisim = db.Iletisims.Find(id);
            db.Iletisims.Remove(iletisim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
