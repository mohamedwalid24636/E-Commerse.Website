using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> inputQuery , ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
          
            #region Apply criteria

            if (specifications.Criteria != null)
            {
                query = query.Where(specifications.Criteria);
            }

            #endregion

            #region Apply OrderBy

            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending != null)
                query = query.OrderBy(specifications.OrderByDescending);

            #endregion

            #region Apply includes


            // <-- ByForeach Method -->  

            //foreach (var includeExpression in specifications.IncludeExpressions)
            //{
            //    query = query.Include(includeExpression);
            //} 



            // <-- ByAggregate Function --> 

            if (specifications.IncludeExpressions != null)
            {
                query = specifications.IncludeExpressions.Aggregate(query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }


            // Aggregate ( Seek => "The Start", CurrentQuery , New Part That Will Be Added)


            #endregion

            #region Apply Pagination 
            if (specifications.IsPagination)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            #endregion

            return query;
        }
    }
}


// helper class that i will use it in Repositories عشان هو ببساطه  Presistence حطيته في ال
//تلقائيا <TEntity , Tkey> هيقرا ال Repositories و بالتالي لما تطلبا هناك في ال  Function علي مستوي ال  GenariceType لاحظ انك معرف ال 