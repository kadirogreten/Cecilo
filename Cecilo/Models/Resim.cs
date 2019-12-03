using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    

    public class Resim
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public short Sira { get; set; }
        public bool IsSelected { get; set; }
        public bool Deleted { get; set; }

        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }
    }
}