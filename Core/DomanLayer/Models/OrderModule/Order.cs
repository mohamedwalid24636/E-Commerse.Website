using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public Order() { }          // لازم يفضل موجوذ عشان  لو حبيت اضيف بروبرتي جذيذه ال Entity framwork هيحتاج يستخذم ال parameterless constructor to make migration 
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId )
        {
            BuyerEmail = userEmail;
            ShipToAddress = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; } = default!;

        public  OrderAddress ShipToAddress { get; set; } = default!;
       
        public DeliveryMethod DeliveryMethod { get; set; } = default!;     
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }    //sum of items price * quantity



        public int DelivaryMethodId { get; set; } = default!; //Fk
        public OrderStatus Status { get; set; }        
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        //[NotMapped]    // Derived Attripute 
        //public decimal Total { get => Subtotal * DeliveryMethod.Price; }

        public decimal GetTotal() =>  SubTotal + DeliveryMethod.Price;     // لازم اسم الذالة بيذأ ب Get

        public string PaymentIntentId { get; set; }  // Stripe PaymentIntentId
    }
}
