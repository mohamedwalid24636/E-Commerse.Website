using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModulesDtos;

namespace Shared.DataTransferObjects.OrdersDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }    
        public ICollection<OrderItemDto> Items { get; set; } = [];      
        public AddressDto ShipToAddress { get; set; } = default!;
        public decimal deliveryCost { get; set; }
        public string DeliveryMethod { get; set; } = default!;
        public string OrderStatus { get; set; } = default!;
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }                             // Auto Mapper Can piend on it (GetTotal)
    } 
}
