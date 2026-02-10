using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityModulesDtos;
using Shared.DataTransferObjects.OrdersDtos;

namespace Service.MappingProfiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, Options => Options.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod,DeliveryMethodDto>();
        }
    }
}
