using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.OrderModule;

namespace Service.Specifications
{
    public class OrderSpecification : BaseSpecification<Order , Guid> 
    {
        public OrderSpecification(string Email) : base(O => O.BuyerEmail == Email)
        {
            AddInclude(O=>O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }



        public OrderSpecification(Guid Id) : base(O => O.Id == Id)
        {
            AddInclude(O => O.DeliveryMethod);    // DeliveryMethodId   علياء كانت كاتباها كذه
            AddInclude(O => O.Items);
        }
    }
}
