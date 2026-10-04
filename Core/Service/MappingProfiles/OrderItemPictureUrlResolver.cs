using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.OrdersDtos;

namespace Service.MappingProfiles
{
    internal class OrderItemPictureUrlResolver(IConfiguration _Configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            else
            {
                //var Url = $"https://localhost:7147/{source.PictureUrl}";
                var Url = $"{_Configuration.GetSection("Urls")["BaseUrl"]}/{source.Product.PictureUrl}";

                return Url;
            }
        }
    }
}
