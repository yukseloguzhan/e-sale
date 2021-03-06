using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
