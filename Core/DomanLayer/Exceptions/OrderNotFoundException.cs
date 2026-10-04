using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exceptions
{
    public class OrderNotFoundException(Guid OrderId) : NotFoundException ($"Order With Id : {OrderId} Not Found ")
    {
    }
}
