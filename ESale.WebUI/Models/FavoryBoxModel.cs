using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Models
{
    public class FavoryBoxModel
    {
        public int FavoryBoxId { get; set; }
        public List<FavoryItemModel> FavoryItems { get; set; }


    }


    public class FavoryItemModel
    {
        public int FavoryItemId { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

    }


}
