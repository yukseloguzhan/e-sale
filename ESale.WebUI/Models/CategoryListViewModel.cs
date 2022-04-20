using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Models
{
    public class CategoryListViewModel
    {
        public List<Category>  Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}
