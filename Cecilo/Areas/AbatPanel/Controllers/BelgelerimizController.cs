using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cecilo.Models;
using IdentitySample.Models;

namespace Cecilo.Areas.AbatPanel.Controllers
{
    [Authorize]
    public class BelgelerimizController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Belgelerimiz
        public ActionResult Index()
        {
            return View(db.Belgelerimiz.ToList());
        }

        // GET: Belgelerimiz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Belgelerimiz belgelerimiz = db.Belgelerimiz.Find(id);
            if (belgelerimiz == null)
            {
                return HttpNotFound();
            }
            return View(belgelerimiz);
        }

        // GET: Belgelerimiz/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Belgelerimiz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,BelgeAdi,Image")] Belgelerimiz belgelerimiz,HttpPostedFileBase file)
        {
            string fileName = string.Empty;

            if (ModelState.IsValid)
            {

                if (file != null && file.ContentLength > 0 && file.ContentLength < 2 * 1024 * 1024)
                {
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("/Assets/belgeler/"), fileName));
                    db.Belgelerimiz.Add(new Belgelerimiz { Image = "/Assets/belgeler/" + fileName, BelgeAdi = belgelerimiz.BelgeAdi});
                    //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;

                }
                else
                {
                    ViewBag.Mesaj = "Dosya seçmeden işleminize devam etmektesiniz. Lütfen 1 Adet Resim Dosyası Seçiniz!";
                    ViewBag.Status = "error";
                    ViewBag.Baslik = "Oops!";

                    return View(belgelerimiz);
                }
                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.SaveChanges();

                return View(belgelerimiz);
            }
            else
            {
                ViewBag.Mesaj = "Hata";
                ViewBag.Status = "error";
                ViewBag.Baslik = "Oops!";

                return View(belgelerimiz);
            }

          
        }

        // GET: Belgelerimiz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Belgelerimiz belgelerimiz = db.Belgelerimiz.Find(id);
            if (belgelerimiz == null)
            {
                return HttpNotFound();
            }
            return View(belgelerimiz);
        }

        // POST: Belgelerimiz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,BelgeAdi,Image")] Belgelerimiz belgelerimiz, HttpPostedFileBase file)
        {
            string fileName = string.Empty;

            if (ModelState.IsValid)
            {

                if (file != null && file.ContentLength > 0)
                {
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    file.SaveAs(Path.Combine(Server.MapPath("/Assets/belgeler/"), fileName));

                    belgelerimiz.Image = "/Assets/belgeler/" + fileName;
                    //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;

                }
                else
                {

                }

            }

            db.Entry(belgelerimiz).State = EntityState.Modified;
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/belgelerimiz/edit/" + belgelerimiz.Id + "';</script>";
            return Content(mesaj);
        }

        // GET: Belgelerimiz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Belgelerimiz belgelerimiz = db.Belgelerimiz.Find(id);
            if (belgelerimiz == null)
            {
                return HttpNotFound();
            }
            return View(belgelerimiz);
        }

        // POST: Belgelerimiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Belgelerimiz belgelerimiz = db.Belgelerimiz.Find(id);
            db.Belgelerimiz.Remove(belgelerimiz);
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Silme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/belgelerimiz/index';</script>";
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
