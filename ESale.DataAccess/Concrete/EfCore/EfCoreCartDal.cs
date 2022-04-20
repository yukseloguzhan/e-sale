using ESale.DataAccess.Abstract;
using ESale.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.DataAccess.Concrete.EfCore
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, ShopContext>, ICartDal
    {


        public Cart GetByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context.Carts.Include(i => i.CartItems)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(i=>i.UserId == userId);
            }
        }


        public override Cart Update(Cart entity)
        {

            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);  // ilişkili alt tabloları da günceller
                context.SaveChanges();
            }

            return entity;

        }


        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"DELETE FROM CartItem WHERE CartId = @p0  AND   ProductId = @p1";
                context.Database.ExecuteSqlRaw(cmd,cartId,productId);
            }
        }

        public void ClearCart(string cartId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"DELETE FROM CartItem WHERE CartId = @p0";
                context.Database.ExecuteSqlRaw(cmd, cartId);
            }
        }
    }

}
