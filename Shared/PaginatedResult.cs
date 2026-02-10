using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageSize, int pageIndex, int totalCount, IEnumerable<TEntity> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = totalCount;
            Data = data;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}



// Front End Needs To Know 4 Things To Use Pagination 
// 1- PageSize 
// 2- PageIndex
// 3- TotalCount
// 4- Data 
