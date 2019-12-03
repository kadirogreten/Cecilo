using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Url { get; set; }
        public int? Sira { get; set; }
        public bool IsActive { get; set; } = false;
        public LanguageId Lang { get; set; }
    }
}