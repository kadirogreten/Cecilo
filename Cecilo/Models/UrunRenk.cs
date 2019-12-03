using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class UrunRenk
    {
        public int UrunId { get; set; }
        public int RenkId { get; set; }

        public virtual Urun Urun { get; set; }
        public virtual Renk Renk { get; set; }
    }
}