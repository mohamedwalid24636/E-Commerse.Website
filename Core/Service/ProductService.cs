using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Exceptions;
using DomanLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;

namespace Service
{
    public class ProductService(IUnitOfWork _UnitOfWork, IMapper _mapper) : IProductService
    {


        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams QueryParams)
        {
            //var Specification = new ProductWithBrand_TypesSpacifications();
            
            var Specification = new ProductWithBrand_TypesSpacifications(QueryParams);

            var Repo = _UnitOfWork.GetRepository<Product, int>();

            var Products = await Repo.GetAllAsync(Specification);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);          // var ProductsDto = 
            var DataCount = Data.Count();                                                                  // Actually PageSize
            
            // Used To Count Paginated Result 
            var CountSpec = new ProductCountSpecification(QueryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);                                                 // Actually Total Items Count

            return new PaginatedResult<ProductDto>(DataCount, QueryParams.PageIndex, TotalCount, Data);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var Expression = new ProductWithBrand_TypesSpacifications(Id);
            var Product = await _UnitOfWork.GetRepository<Product, int>().GetByIdAsync(Expression);
            if (Product == null)
                throw new ProductNotFoundException(Id);                                         //here We Used Not Found Exception 
            var ProductDto = _mapper.Map<Product, ProductDto>(Product);
            return ProductDto;
        }


        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repository = _UnitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repository.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }



        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _UnitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }


    }


}


//Notic That U Used Only => GetAllAsync , GetById 
// U Didnot Used Create , Update , Delete 