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
    public class SliderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Path,Baslik,Aciklama,Url,Sira,IsActive")] Slider slider, HttpPostedFileBase sliderResim)
        {
            string fileName = string.Empty;

            if (ModelState.IsValid)
            {

                if (sliderResim != null && sliderResim.ContentLength > 0 && sliderResim.ContentLength < 2 * 1024 * 1024)
                {
                    fileName = Guid.NewGuid() + Path.GetExtension(sliderResim.FileName);
                    sliderResim.SaveAs(Path.Combine(Server.MapPath("/Assets/slider/"), fileName));
                    db.Slider.Add(new Slider
                    {
                        Path = "/Assets/slider/" + fileName,
                        Baslik = slider.Baslik,
                        Sira = slider.Sira,
                        Aciklama = slider.Aciklama,
                        Url = slider.Url,
                        IsActive = true
                    });
                    //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;

                }
                else
                {
                    ViewBag.Mesaj = "Dosya seçmeden işleminize devam etmektesiniz. Lütfen 1 Adet Resim Dosyası Seçiniz!";
                    ViewBag.Status = "error";
                    ViewBag.Baslik = "Oops!";

                    return View(slider);
                }
                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.SaveChanges();

                return View(slider);
            }
            else
            {
                ViewBag.Mesaj = "Hata";
                ViewBag.Status = "error";
                ViewBag.Baslik = "Oops!";

                return View(slider);
            }
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Path,Baslik,Aciklama,Url,Sira,IsActive")] Slider slider, HttpPostedFileBase sliderResim)
        {
            string fileName = string.Empty;

            if (ModelState.IsValid)
            {

                if (sliderResim != null && sliderResim.ContentLength > 0)
                {
                    fileName = Guid.NewGuid() + Path.GetExtension(sliderResim.FileName);

                    sliderResim.SaveAs(Path.Combine(Server.MapPath("/Assets/slider/"), fileName));

                    slider.Path = "/Assets/slider/" + fileName;
                    //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;

                }
                else
                {

                }

            }

            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/slider/edit/" + slider.Id + "';</script>";
            return Content(mesaj);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            db.Slider.Remove(slider);
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Silme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/slider/index';</script>";
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
