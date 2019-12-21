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
    public class EtiketController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Etiket
        public ActionResult Index()
        {
            return View(db.Etikets.ToList());
        }

        // GET: Etiket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etiket etiket = db.Etikets.Find(id);
            if (etiket == null)
            {
                return HttpNotFound();
            }
            return View(etiket);
        }

        // GET: Etiket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Etiket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EtiketAdi,Sira,Lang")] Etiket etiket)
        {
            if (ModelState.IsValid)
            {

                db.Etikets.Add(etiket);
                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.SaveChanges();

                return View(etiket);
            }
            else
            {
                ViewBag.Mesaj = "Hata";
                ViewBag.Status = "error";
                ViewBag.Baslik = "Oops!";

                return View(etiket);
            }
        }

        // GET: Etiket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etiket etiket = db.Etikets.Find(id);
            if (etiket == null)
            {
                return HttpNotFound();
            }
            return View(etiket);
        }

        // POST: Etiket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EtiketAdi,Sira,Lang")] Etiket etiket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etiket).State = EntityState.Modified;
                db.SaveChanges();

                string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/etiket/edit/" + etiket.Id + "';</script>";
                return Content(mesaj);
            }
            return View(etiket);
        }

        // GET: Etiket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etiket etiket = db.Etikets.Find(id);
            if (etiket == null)
            {
                return HttpNotFound();
            }
            return View(etiket);
        }

        // POST: Etiket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Etiket etiket = db.Etikets.Find(id);
            db.Etikets.Remove(etiket);
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Silme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/etiket/index';</script>";
            return Content(mesaj);
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
