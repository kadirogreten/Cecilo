using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cecilo.Models
{
    
    public class Urun
    {
 
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public string AltBaslik { get; set; }
        public string Aciklama { get; set; }
        public string Detay { get; set; }
        public decimal? Fiyat { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsHome { get; set; } = false;
        public bool IsPopular { get; set; } = false;

        public int MarkalarId { get; set; }
        public Markalar Markalar { get; set; }

        public ICollection<Resim> Resimler { get; set; }
        public virtual ICollection<Renk> Renkler { get; set; }
        public virtual ICollection<Etiket> Etiketler { get; set; }

        
    }
}