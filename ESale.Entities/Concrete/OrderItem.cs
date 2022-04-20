using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class OrderItem : IEntity
    {
        public int Id { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }


        public int ProductId { set; get; }
        public Product Product { set; get; }


        public decimal Price { set; get; }    // Sipariş verdikten sonra ürün fiyatı artsa da o kişiyi etkilemez
        public int Quantity { set; get; }


    }
}
