using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Abstract
{
    public interface IFavoryBoxService  
    {
        void CreateFavoryBox(string userId);

        FavoryBox GetFavoryBoxByUserId(string userId);

        void AddtoFavoryBox(string userId, int productId);   // Hangi kullanıcı Favorilerine ekliycez

        int FavoryCount(string userId);

        void DeleteFromFavoryBox(string userId, int productId);

        //void DeleteFromCart(string userId, int productId);
        //void ClearCart(string cartId);
    }
}
