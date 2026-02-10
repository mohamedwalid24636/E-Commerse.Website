using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModulesDtos;

namespace Shared.DataTransferObjects.OrdersDtos
{
    public class OrderDto    // Recive Order Data From Client
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; } = default!;       //ShipToAddress
    }
}
