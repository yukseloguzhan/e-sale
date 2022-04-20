using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class Product  : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public EnumProductSize ProductSize { get; set; }
        public EnumColor Color { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }


        public List<ProductCategory> ProductCategories { get; set; }
        public List<Comment> Comments { get; set; }

        public ICollection<ImageFile> ImageFiles { get; set; }


    }


    public enum EnumProductSize
    {
        XS = 0,
        S = 1,
        M = 2,
        L =3,
        XL = 4
    }
    public enum EnumColor
    {
        Kırmızı = 0,
        Mavi = 1,
        Yeşil = 2,
        Sarı = 3,
        Beyaz = 4,
        Siyah = 5
    }




}
