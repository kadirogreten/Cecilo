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
    public class UrunController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Urun
        public ActionResult Index()
        {
            var urun = db.Urun.Include(u => u.Kategori).Include(u => u.Markalar).Include(a => a.Resimler);
            return View(urun.ToList());
        }

        // GET: Urun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        public ActionResult UruneRenkAta()
        {
            ViewBag.UrunId = new SelectList(db.Urun, "Id", "UrunAdi");
            ViewBag.Renkler = db.Renks;

            return View();
        }

        public ActionResult UruneEtiketAta()
        {
            ViewBag.UrunId = new SelectList(db.Urun, "Id", "UrunAdi");
            ViewBag.Etiketler = db.Etikets;

            return View();
        }
        //public ActionResult UruneRenkAta(int UrunId)
        //{
        //    if (UrunId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var urun = db.Urun.Where(a => a.Id == UrunId).Include(a => a.Renkler).FirstOrDefault();
        //    if (urun == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(urun);
        //}

        [HttpPost]
        public ActionResult UruneRenkAta(int UrunId, int[] RenkId)
        {

            var urun = db.Urun.Where(a => a.Id == UrunId).FirstOrDefault();
            if (RenkId != null)
            {
                foreach (var item in RenkId)
                {
                    Renk urunRenk = db.Renks.Where(a => a.Id == item).FirstOrDefault();

                    urun.Renkler.Add(urunRenk);

                    db.SaveChanges();

                }
            }

            ViewBag.UrunId = new SelectList(db.Urun, "Id", "UrunAdi");
            ViewBag.Renkler = db.Renks;
            ViewBag.Mesaj = "Ekleme Başarılı.";
            ViewBag.Status = "success";
            ViewBag.Baslik = "Harika";
            return View(urun);
        }

        [HttpPost]
        public ActionResult UruneEtiketAta(int UrunId, int[] EtiketId)
        {

            var urun = db.Urun.Where(a => a.Id == UrunId).FirstOrDefault();
            if (EtiketId != null)
            {
                foreach (var item in EtiketId)
                {
                    Etiket urunEtiket = db.Etikets.Where(a => a.Id == item).FirstOrDefault();

                    urun.Etiketler.Add(urunEtiket);

                    db.SaveChanges();

                }
            }

            ViewBag.UrunId = new SelectList(db.Urun, "Id", "UrunAdi");
            ViewBag.Etiketler = db.Etikets;
            ViewBag.Mesaj = "Ekleme Başarılı.";
            ViewBag.Status = "success";
            ViewBag.Baslik = "Harika";
            return View(urun);
        }

        // GET: Urun/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi");
            ViewBag.MarkalarId = new SelectList(db.Markalar, "Id", "MarkaAdi");


            return View();
        }

        // POST: Urun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,UrunAdi,AltBaslik,Aciklama,Detay,Fiyat,KategoriId,MarkalarId,IsNew,IsHome,IsPopular")] Urun urun, IEnumerable<HttpPostedFileBase> urunResim)
        {
            string fileName = string.Empty;
            short sira = 1;
            if (ModelState.IsValid)
            {
                foreach (var file in urunResim)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath("/Assets/urun/"), fileName));
                        db.Resim.Add(new Resim { Path = "/Assets/urun/" + fileName, Sira = sira, UrunId = urun.Id });
                        //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;
                        sira++;
                    }
                    else
                    {
                        ViewBag.Mesaj = "Dosya seçmeden işleminize devam etmektesiniz. Lütfen 2 Adet Resim Dosyası Seçiniz!";
                        ViewBag.Status = "error";
                        ViewBag.Baslik = "Oops!";
                        ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.KategoriId);
                        ViewBag.MarkalarId = new SelectList(db.Markalar, "Id", "MarkaAdi", urun.MarkalarId);
                        return View(urun);
                    }

                }

                ViewBag.Mesaj = "Ekleme Başarılı. Ürün özellik ekleme sayfasına yönlendiriliyorsunuz. Lütfen bekleyiniz!";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.Urun.Add(urun);
                db.SaveChanges();
                ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.KategoriId);
                ViewBag.MarkalarId = new SelectList(db.Markalar, "Id", "MarkaAdi", urun.MarkalarId);
                return View(urun);
            }
            ViewBag.Mesaj = "Hata";
            ViewBag.Status = "error";
            ViewBag.Baslik = "Oops!";
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.KategoriId);
            ViewBag.MarkalarId = new SelectList(db.Markalar, "Id", "MarkaAdi", urun.MarkalarId);

            return View(urun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
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
        // GET: Urun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Where(a=>a.Id== id).Include(u => u.Kategori).Include(u => u.Markalar).Include(a => a.Resimler).Include(a => a.Renkler).FirstOrDefault();
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.KategoriId);
            ViewBag.MarkalarId = new SelectList(db.Markalar, "Id", "MarkaAdi", urun.MarkalarId);
            return View(urun);
        }



        // POST: Urun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,UrunAdi,AltBaslik,Aciklama,Detay,Fiyat,KategoriId,MarkalarId,IsNew,IsHome,IsPopular")] Urun urun, int[] RenkId, int[] EtiketId, IEnumerable<HttpPostedFileBase> urunResim)
        {
            string fileName = string.Empty;
            short sira = 1;
            if (ModelState.IsValid)
            {
                foreach (var file in urunResim)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                        file.SaveAs(Path.Combine(Server.MapPath("/Assets/urun/"), fileName));
                        db.Resim.Add(new Resim { Path = "/Assets/urun/" + fileName, Sira = sira, UrunId = urun.Id });
                        //db.Entry(new ProjeResim { Path = "/images/Projeler/" + fileName, Sira = sira, ProjeId = proje.Id }).State = EntityState.Modified;
                        sira++;
                    }
                    else
                    {

                    }

                }


                var eskiUrun = db.Urun
                    .Include(u => u.Kategori)
                    .Include(u => u.Markalar)
                    .Include(a => a.Resimler)
                    .Include(a => a.Renkler)
                    .Where(a => a.Id == urun.Id).FirstOrDefault();

                var renk = db.Urun.Where(a => a.Id == eskiUrun.Id).FirstOrDefault();


                foreach (var item in renk.Renkler.ToList())
                {
                    eskiUrun.Renkler.Remove(item);
                    
                    
                }

                foreach (var item in renk.Etiketler.ToList())
                {
                    eskiUrun.Etiketler.Remove(item);
                   

                }

                eskiUrun.Id = urun.Id;
                eskiUrun.UrunAdi = urun.UrunAdi;
                eskiUrun.AltBaslik = urun.AltBaslik;
                eskiUrun.Aciklama = urun.Aciklama;
                eskiUrun.Detay = urun.Detay;
                eskiUrun.Fiyat = urun.Fiyat;
                eskiUrun.KategoriId = urun.KategoriId;
                eskiUrun.MarkalarId = urun.MarkalarId;
                eskiUrun.IsNew = urun.IsNew;
                eskiUrun.IsHome = urun.IsHome;
                eskiUrun.IsPopular = urun.IsPopular;


                if (RenkId != null)
                {
                    foreach (var item in RenkId)
                    {

                        Renk urunRenk = db.Renks.Where(a => a.Id == item).FirstOrDefault();

                        eskiUrun.Renkler.Add(urunRenk);

                    }

                }

                if (EtiketId != null)
                {
                    foreach (var item in EtiketId)
                    {

                        Etiket urunEtiket = db.Etikets.Where(a => a.Id == item).FirstOrDefault();

                        eskiUrun.Etiketler.Add(urunEtiket);

                    }
                }



                ViewBag.Mesaj = "Ekleme Başarılı.";
                ViewBag.Status = "success";
                ViewBag.Baslik = "Harika";
                db.Entry(eskiUrun).State = EntityState.Modified;
                db.SaveChanges();
                string mesaj = "<script language='javascript' type='text/javascript'>alert('Düzenleme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/urun/edit/" + urun.Id + "';</script>";
                return Content(mesaj);

            }
            ViewBag.Mesaj = "Hata";
            ViewBag.Status = "error";
            ViewBag.Baslik = "Oops!";
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.KategoriId);
            ViewBag.MarkalarId = new SelectList(db.Markalar, "Id", "MarkaAdi", urun.MarkalarId);
            return View(urun);

        }

        // GET: Urun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Where(a=>a.Id == id).Include(u => u.Kategori).Include(u => u.Markalar).Include(a => a.Resimler).FirstOrDefault();
            db.Urun.Remove(urun);
            db.SaveChanges();
            string mesaj = "<script language='javascript' type='text/javascript'>alert('Silme İşlemi Başarıyla Gerçekleşmiştir!');window.location.href = '/abatpanel/urun/index';</script>";
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
