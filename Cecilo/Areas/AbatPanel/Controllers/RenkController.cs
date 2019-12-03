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
    public class RenkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Renk
        public ActionResult Index()
        {
            return View(db.Renks.ToList());
        }

        // GET: Renk/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renk renk = db.Renks.Find(id);
            if (renk == null)
            {
                return HttpNotFound();
            }
            return View(renk);
        }

        // GET: Renk/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Renk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RenkKodu,RenkAdi")] Renk renk)
        {
            if (ModelState.IsValid)
            {

                db.Renks.Add(renk);
                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.SaveChanges();
                
                return View(renk);
            }
            else
            {
                ViewBag.Mesaj = "Hata";
                ViewBag.Status = "error";
                ViewBag.Baslik = "Oops!";
                
                return View(renk);
            }
        }

        // GET: Renk/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renk renk = db.Renks.Find(id);
            if (renk == null)
            {
                return HttpNotFound();
            }
            return View(renk);
        }

        // POST: Renk/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RenkKodu,RenkAdi")] Renk renk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(renk).State = EntityState.Modified;
                db.SaveChanges();
               
                string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/renk/edit/" + renk.Id + "';</script>";
                return Content(mesaj);
            }
            return View(renk);
        }

        // GET: Renk/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renk renk = db.Renks.Find(id);
            if (renk == null)
            {
                return HttpNotFound();
            }
            return View(renk);
        }

        // POST: Renk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Renk renk = db.Renks.Find(id);
            db.Renks.Remove(renk);
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Silme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/renk/index';</script>";
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
