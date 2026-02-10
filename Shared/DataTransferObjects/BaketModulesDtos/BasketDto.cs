using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.BaketModulesDtos
{
    public class BasketDto
    {
        public string Id { get; set; }   // GUID Type => Created By Client [ By FrontEnd ]
        public ICollection<BasketItemDto> Items { get; set; } = [];
        public string? clientSecret { get; set; } 
        public string? paymentIntentId { get; set; } 
        public int? deliveryMethodId { get; set; } 
        public decimal? shippingPrice { get; set; } 
    }
}


//