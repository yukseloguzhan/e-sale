using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Models
{
    public class OrderListModel
    {

        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public EnumOrderState OrderState { get; set; }
        public EnumPaymentType PaymentType { get; set; }



        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }


        public List<OrderItemModel> OrderItems { get; set; }


        public decimal TotalPrice()
        {
            return OrderItems.Sum(x => x.Price * x.Quantity);
        }



    }


    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
    }



}
