using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class FavoryBox : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }


        public List<FavoryItem> FavoryItems { get; set; }

    }
}
