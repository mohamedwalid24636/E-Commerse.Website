using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;
using DomanLayer.Models.ProductModule;
using Shared;

namespace Service.Specifications
{
    public class ProductWithBrand_TypesSpacifications : BaseSpecification<Product, int>
    {
        //public ProductWithBrand_TypesSpacifications() : base(null)
        //{
        //    AddInclude(P => P.ProductBrand);
        //    AddInclude(P => P.ProductType);
        //}


        public ProductWithBrand_TypesSpacifications(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()))
            )
        // Where(p => p.BrandId == BrandId &&  p.TypeId == TypeId )
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);

        }


        public ProductWithBrand_TypesSpacifications(int Id) : base(P => P.Id == Id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }






    
}


// Notic that !BrandId.HasValue Will Return true Or false  
/* Notic If BrandId == null It Will Filter Where (P => P.BrandId == Null)
 ---->The Return Data Is Product That Has BrandId Value = null ;  */