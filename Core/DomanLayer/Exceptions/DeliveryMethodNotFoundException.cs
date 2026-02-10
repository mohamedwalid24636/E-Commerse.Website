using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int Id) : NotFoundException($"No Delivery Method Was Found With Id {Id}")
    {
    }
}
