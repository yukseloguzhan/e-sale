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
    public class OrderManager : IOrderService
    {

        private IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void CreateOrder(Order entity)
        {
            _orderDal.Add(entity);
        }

        public List<Order> GetOrders(string userId)
        {
            return _orderDal.GetOrders(userId);
        }
    }
}
