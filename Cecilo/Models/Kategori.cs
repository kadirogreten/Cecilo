using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class Kategori
    {
        public int Id { get; set; }

        public int? UstKategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public string CategoryTitle { get; set; }
        public ICollection<Urun> Urunler { get; set; }

        [ForeignKey("UstKategoriId")]
        public virtual Kategori UstKategori { get; set; }

        public virtual ICollection<Kategori> AltKategoriler { get; set; }

        public short Sira { get; set; }
        public LanguageId Lang { get; set; }

    }
}