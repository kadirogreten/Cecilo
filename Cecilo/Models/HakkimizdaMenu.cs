using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class HakkimizdaMenu
    {
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public string MenuBaslik { get; set; }
        public string Aciklama { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public LanguageId Lang { get; set; }
    }
}