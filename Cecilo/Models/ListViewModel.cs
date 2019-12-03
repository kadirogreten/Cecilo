using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cecilo.Models
{
    public class ListViewModel
    {

        public int[] SelectedItem { get; set; }
        public IEnumerable<SelectListItem> SelectItemList { get; set; }

        public List<SelectListItem> Renkler()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Siyah", Value = "1" });
            items.Add(new SelectListItem { Text = "Kırmızı", Value = "2" });
            items.Add(new SelectListItem { Text = "Mavi", Value = "3" });
            items.Add(new SelectListItem { Text = "BSarı", Value = "4" });
            return items;
        }
    }

    
}