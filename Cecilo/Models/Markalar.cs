using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class Markalar
    {
        public int Id { get; set; }
        public string MarkaAdi { get; set; }
        public virtual ICollection<Urun> Urunler { get; set; }
        public LanguageId Lang { get; set; }
    }
}