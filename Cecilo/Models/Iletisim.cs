using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cecilo.Models
{
    public class Iletisim
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string AdiSoyadi { get; set; }
        [EmailAddress]
        [StringLength(150)]
        public string Eposta { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }
        [StringLength(100)]
        public string Konu { get; set; }
        [StringLength(250)]
        public string Mesaj { get; set; }

        public bool OkunduMu { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}