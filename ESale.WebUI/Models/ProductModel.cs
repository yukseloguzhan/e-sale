using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Models
{
    public class ProductModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(60,MinimumLength = 3 , ErrorMessage ="Ürün ismi en az 3 ile 60 karakter arası olmalı")]
        public string Name { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 3, ErrorMessage = "Ürün açıklaması en az 3 ile 100 karakter arası olmalı")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Fiyat belirtiniz")]
        [Range(1,10000)]
        public decimal Price { get; set; }

        public int[] CategoryIds { get; set; }


        public List<Category> SelectedCategories { get; set; }



    }
}
