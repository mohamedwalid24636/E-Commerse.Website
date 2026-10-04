using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;   // You Installed It in Presentaion layer Settings
using Presentation.ActionFilterAttriputes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
   
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        //GetAll Products
        //[Authorize(Roles = "Admin")]
        //[Authorize]
        [HttpGet]
        [Cache(PerUser: false)]
        //Get BaseUrl/api/Products
        public async Task <ActionResult<PaginatedResult<ProductDto>>> GetAllProducts ( [FromQuery] ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        //Get Product By Id
        [HttpGet("{id}")]
        //Get BaseUrl/api/Products/3
        public async Task <ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }


        //Get All Types 
        [HttpGet("types")]
        //Get BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        //GetAll Brands
        [HttpGet("brands")]
        //Get BaseUrl/api/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }


         
    }
}


//ASPNetCore.App => For Controller Base 
//URL Resolver ==> هيبقا معاك ال اسم الصورة وانت محتاج تكون ال url كاملة


// HttpGet Dont Have Body Request
// Api Can't Pind on Parameters of Complix Query  (ProductQueryParams.SortingOption) So Normaly He Go Search in Body  but When Method Is Get He Can't Because (Get) Actualy Don't have Body 
// Sol => Tell Him To go search In Query 



//Take Care U Can't Add Any Business Logic in Controller 
