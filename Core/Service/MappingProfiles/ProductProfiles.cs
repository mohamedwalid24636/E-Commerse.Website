using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Models.ProductModule;
using Service.MappingProfiles;
using Shared.DataTransferObjects.ProductModuleDtos;

namespace Service.ProductProfiles
{
    public class ProductProfiles : Profile
    {
        public  ProductProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.ProductBrand, Options => Options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.ProductType, Options => Options.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());    //==> Over Load Uses PicterUrl Reslover 
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}


//Download AutoMapper Package 