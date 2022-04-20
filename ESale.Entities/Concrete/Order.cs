using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class Order : IEntity
    {

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public EnumOrderState OrderState { get; set; }
        public EnumPaymentType PaymentType { get; set; }


        public string UserId { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }


        public string PaymentId { set; get; } // API ye bağlanıp bilgi almak için
        public string PaymentToken { set; get; } 
        public string ConversationId { set; get; } // IYZİCO 


        public List<OrderItem> OrderItems { set; get; }



    }


    public enum EnumOrderState
    {
        waiting = 0,
        unpaid = 1,
        completed = 2
    }

    public enum EnumPaymentType
    {
        CreditCart = 0,
        Eft = 1
    }



}
