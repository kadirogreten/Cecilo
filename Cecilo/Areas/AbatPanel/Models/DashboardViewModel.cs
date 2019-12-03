using Cecilo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Areas.AbatPanel.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Belgelerimiz> Belgelerimiz { get; set; }
        public IEnumerable<Urun> Urunlerimiz { get; set; }
        public IEnumerable<Kategori> Kategoriler { get; set; }
        public IEnumerable<Markalar> Markalar { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
    }
}