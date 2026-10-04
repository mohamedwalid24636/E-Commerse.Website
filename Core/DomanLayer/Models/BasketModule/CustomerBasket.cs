using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.BaskedModule
{
    public class CustomerBasket
    {
        public string Id { get; set; }   // GUID Type => Created By Client [ By FrontEnd ]
        public ICollection<BasketItem> Items { get; set; } = [];
        public string? clientSecret { get; set; }
        public string? paymentIntentId { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal? shippingPrice { get; set; }
    }
}




//Notes 
//Didn't Inherit Id From Base Entity
// مخلتهوش يورث من البيز انتيتي عشان مش هعتبره كلاس بيتخزن في الداتا بيز (عشان اعرف افرق بينتهم) تمام