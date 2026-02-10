using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.OrderModule
{
    public enum OrderStatus
    {
        Pending =0 ,
        PaymentReceived =1 ,
        paymentFailed =2
    }
}
