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
    [Authorize]
    public class MarkalarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Markalar
        public ActionResult Index()
        {
            return View(db.Markalar.ToList());
        }

        // GET: Markalar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markalar markalar = db.Markalar.Find(id);
            if (markalar == null)
            {
                return HttpNotFound();
            }
            return View(markalar);
        }

        // GET: Markalar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Markalar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MarkaAdi")] Markalar markalar)
        {
            if (ModelState.IsValid)
            {

                db.Markalar.Add(markalar);
                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.SaveChanges();
                
                return View(markalar);
            }
            else
            {
                ViewBag.Mesaj = "Hata";
                ViewBag.Status = "error";
                ViewBag.Baslik = "Oops!";
                
                return View(markalar);
            }
        }

        // GET: Markalar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markalar markalar = db.Markalar.Find(id);
            if (markalar == null)
            {
                return HttpNotFound();
            }
            return View(markalar);
        }

        // POST: Markalar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MarkaAdi")] Markalar markalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markalar).State = EntityState.Modified;
                db.SaveChanges();
               
                string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/markalar/edit/" + markalar.Id + "';</script>";
                return Content(mesaj);
            }
            return View(markalar);
        }

        // GET: Markalar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markalar markalar = db.Markalar.Find(id);
            if (markalar == null)
            {
                return HttpNotFound();
            }
            return View(markalar);
        }

        // POST: Markalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Markalar markalar = db.Markalar.Find(id);
            db.Markalar.Remove(markalar);
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Silme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/markalar/index';</script>";
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
