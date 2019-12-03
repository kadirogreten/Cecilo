using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class Renk
    {
        public int Id { get; set; }
        public string RenkKodu { get; set; }
        public string RenkAdi { get; set; }

        public virtual ICollection<Urun> Urunler { get; set; }
        public LanguageId Lang { get; set; }
    }
}