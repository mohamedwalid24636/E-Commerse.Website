using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;

namespace ServiceAbstraction
{
    public  interface IProductService
    {
        //GetAllProducts
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        //GetProductById
        Task<ProductDto?> GetProductByIdAsync(int Id);
        //GetAllBrands 
        Task <IEnumerable<BrandDto>> GetAllBrandsAsync();
        //GetAllTypes 
        Task <IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
