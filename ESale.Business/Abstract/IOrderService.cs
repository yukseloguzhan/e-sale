using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Abstract
{
    public interface IOrderService
    {
        void CreateOrder(Order entity);

        List<Order> GetOrders(string userId);    // string nullable değer olabildiği için boş da göndersek(admin tümünü getirmesi için) sorun olmaz

    }
}
