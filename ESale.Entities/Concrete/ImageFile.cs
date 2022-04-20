using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class ImageFile : IEntity
    {
        [Key]
        public int ImageFileId { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        public string ImagePath { get; set; }


        public int ProductId { set; get; }
        public virtual Product Product { set; get; }





    }
}
