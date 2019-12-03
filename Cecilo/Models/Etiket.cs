using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class Etiket
    {
        public int Id { get; set; }
        public string EtiketAdi { get; set; }
        
        public int? Sira { get; set; }

        public ICollection<Urun> Urun { get; set; }
    }
}