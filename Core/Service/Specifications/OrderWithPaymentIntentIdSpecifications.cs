using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.OrderModule;

namespace Service.Specifications
{
    internal class OrderWithPaymentIntentIdSpecifications : BaseSpecification<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
          
        }
    }
}
