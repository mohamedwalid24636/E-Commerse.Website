using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using DomanLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.ProductModuleDtos;


namespace Service.MappingProfiles
{
    
    //Dependancy Injection for IConfiguration to use appsettings.json values (Automatically CLR Allowed it)
    internal class PictureUrlResolver(IConfiguration _Configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            else
            {
                //var Url = $"https://localhost:7147/{source.PictureUrl}";
                var Url = $"{_Configuration.GetSection("Urls")["BaseUrl"]}/{source.PictureUrl}";

                return Url;
            }

        }
    }


}

//To Use IConfiguration install 
//Package Name => Microsoft.Extensions.Configuration 
//Clr Injection Allowed Automaticlly


//Use Resolver لو عاوز ترجع ال معلومة بشكل معين 