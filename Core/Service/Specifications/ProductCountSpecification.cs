using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.ProductModule;
using Shared;

namespace Service.Specifications
{
    internal class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
        : base( p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                  && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                  && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()))
              )
        {


        }


    }
}
