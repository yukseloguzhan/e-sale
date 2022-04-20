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
    public class EfCoreOrderDal : EfCoreGenericRepository<Order, ShopContext>, IOrderDal
    {

        public List<Order> GetOrders(string userId)    // Yönetici ve kullanici sayfası ortak kullanır
        {
            using (var context = new ShopContext())
            {
                var orders = context.Orders
                    .Include(i => i.OrderItems)
                    .ThenInclude(i => i.Product)
                    .AsQueryable();   // database'e hala gitmedi bekliyor


                if (!String.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId == userId);
                }

                return orders.ToList();


            }
        }
    }
}
