using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class Belgelerimiz
    {
        public int Id { get; set; }
        public string BelgeAdi { get; set; }
        public string Image { get; set; }
        public LanguageId Lang { get; set; }
    }
}