using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Abstract
{
    public interface ICartService
    {
        void CreateCart(string userId);

        Cart GetCartByUserId(string userId);

        void AddtoCart(string userId,int productId,int quantity);   // Hangi kullanıcı sepetine ekliycez
        void DeleteFromCart(string userId, int productId);
        void ClearCart(string cartId);
        int ProductCount(string userId);
    }
}
