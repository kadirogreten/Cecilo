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
    [Authorize(Roles = "Admin,Cecilo")]
    public class HakkimizdaMenuController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HakkimizdaMenu
        public ActionResult Index()
        {
            return View(db.HakkimizdaMenu.ToList());
        }

        // GET: HakkimizdaMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HakkimizdaMenu hakkimizdaMenu = db.HakkimizdaMenu.Find(id);
            if (hakkimizdaMenu == null)
            {
                return HttpNotFound();
            }
            return View(hakkimizdaMenu);
        }

        // GET: HakkimizdaMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HakkimizdaMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,MenuAdi,MenuBaslik,Aciklama,Detail,Image,Lang")] HakkimizdaMenu hakkimizdaMenu, HttpPostedFileBase resim)
        {
            string fileName = string.Empty;

            if (ModelState.IsValid)
            {

                if (resim != null && resim.ContentLength > 0 && resim.ContentLength < 2 * 1024 * 1024)
                {
                    fileName = Guid.NewGuid() + Path.GetExtension(resim.FileName);
                    resim.SaveAs(Path.Combine(Server.MapPath("/Assets/hakkimizda/"), fileName));
                    db.HakkimizdaMenu.Add(new HakkimizdaMenu
                    {
                        Image = "/Assets/hakkimizda/" + fileName,
                        MenuAdi = hakkimizdaMenu.MenuAdi,
                        MenuBaslik = hakkimizdaMenu.MenuBaslik,
                        Aciklama = hakkimizdaMenu.Aciklama,
                        Detail = hakkimizdaMenu.Detail,
                        Lang = hakkimizdaMenu.Lang
                    });
                    //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;

                }
                else
                {
                    ViewBag.Mesaj = "Dosya seçmeden işleminize devam etmektesiniz. Lütfen 1 Adet Resim Dosyası Seçiniz!";
                    ViewBag.Status = "error";
                    ViewBag.Baslik = "Oops!";

                    return View(hakkimizdaMenu);
                }
                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                
                db.SaveChanges();

                return View(hakkimizdaMenu);
            }
            else
            {
                ViewBag.Mesaj = "Hata";
                ViewBag.Status = "error";
                ViewBag.Baslik = "Oops!";

                return View(hakkimizdaMenu);
            }


        }

        // GET: HakkimizdaMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HakkimizdaMenu hakkimizdaMenu = db.HakkimizdaMenu.Find(id);
            if (hakkimizdaMenu == null)
            {
                return HttpNotFound();
            }
            return View(hakkimizdaMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResimSil(int? id)
        {
            bool secilen = true;
            Resim resim = db.Resim.Find(id);
            db.Resim.Remove(resim);
            secilen = resim.IsSelected;
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/urun/edit/" + resim.UrunId + "';</script>";
            return Content(mesaj);
        }
        // POST: HakkimizdaMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,MenuAdi,MenuBaslik,Aciklama,Detail,Image,Lang")] HakkimizdaMenu hakkimizdaMenu, HttpPostedFileBase resim)
        {
            string fileName = string.Empty;

            if (ModelState.IsValid)
            {

                if (resim != null && resim.ContentLength > 0)
                {
                    fileName = Guid.NewGuid() + Path.GetExtension(resim.FileName);

                    resim.SaveAs(Path.Combine(Server.MapPath("/Assets/hakkimizda/"), fileName));

                    hakkimizdaMenu.Image = "/Assets/hakkimizda/" + fileName;
                    //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;

                }
                else
                {

                }

            }

            db.Entry(hakkimizdaMenu).State = EntityState.Modified;
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/hakkimizdamenu/edit/" + hakkimizdaMenu.Id + "';</script>";
            return Content(mesaj);
        }

        // GET: HakkimizdaMenu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HakkimizdaMenu hakkimizdaMenu = db.HakkimizdaMenu.Find(id);
            if (hakkimizdaMenu == null)
            {
                return HttpNotFound();
            }
            return View(hakkimizdaMenu);
        }

        // POST: HakkimizdaMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HakkimizdaMenu hakkimizdaMenu = db.HakkimizdaMenu.Find(id);
            db.HakkimizdaMenu.Remove(hakkimizdaMenu);
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Silme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/hakkimizdamenu/index';</script>";
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
