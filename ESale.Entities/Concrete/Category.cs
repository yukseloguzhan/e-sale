using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class Category  : IEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(maximumLength: 20, ErrorMessage = "En fazla 20 karakter girilebilir.")]
        public string Name { get; set; }
        public string Description { get; set; }


        public List<ProductCategory> ProductCategories  { get; set; }


    }
}
