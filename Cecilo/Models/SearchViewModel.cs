using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Urun> Urunlerimiz { get; set; }
        public IEnumerable<Kategori> Kategoriler { get; set; }
        public IEnumerable<Markalar> Markalar { get; set; }
        public string SearchString { get; set; }
    }
}