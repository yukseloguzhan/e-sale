using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Models
{
    public class ProductListModel
    {
        public List<Product>  Products  { get; set; }
        public PageInfo PageInfo { get; set; }
    }


    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }   // Her sayfada kaç eleman
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }



        public int TotalPage()
        {
            return (int)(Math.Ceiling((decimal)TotalItems / ItemsPerPage));
        }


    }


}
