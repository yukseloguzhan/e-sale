using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class FavoryItem : IEntity
    {
        public int Id { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int FavoryBoxId { get; set; }
        public FavoryBox FavoryBox { get; set; }
    }
}
