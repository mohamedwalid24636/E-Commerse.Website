using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {   
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }    
        public ProductSortingOptions Sort { get; set; }
        public string? Search { get; set; }


        #region Pagination

        private const int MaxPagSize = 10;
        private const int DefaultPageSize = 5;
        private int pageSize = DefaultPageSize;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPagSize ? MaxPagSize : value; }
        }

        #endregion


    }
}
