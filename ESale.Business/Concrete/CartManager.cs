using ESale.Business.Abstract;
using ESale.DataAccess.Abstract;
using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Concrete
{
    public class CartManager : ICartService
    {

        private ICartDal _cartDal;

        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void AddtoCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if (cart!=null)
            {
                var index = cart.CartItems.FindIndex(x=>x.ProductId == productId);

                if (index<0)
                {
                    cart.CartItems.Add(new CartItem
                    {
                         ProductId = productId,
                         Quantity = quantity,
                         CartId = cart.Id

                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;   // Daha önce o ürün varsa miktarını artırırız

                }


                _cartDal.Update(cart);


            }
            


        }

        public void ClearCart(string cartId)
        {
            _cartDal.ClearCart(cartId);
        }

        public void CreateCart(string userId)
        {
            _cartDal.Add(new Cart() { UserId = userId });
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {

                _cartDal.DeleteFromCart(cart.Id,productId);


            }

        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartDal.GetByUserId(userId);
        }

        public int ProductCount(string userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {

                return cart.CartItems.Count();
            }

            return 0;

        }
    }
}
