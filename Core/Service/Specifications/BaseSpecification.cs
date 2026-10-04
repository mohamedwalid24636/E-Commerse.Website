using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models;

namespace Service.Specifications
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }


        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];       //new List<Expression<Func<TEntity, object>>>();



        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion


        #region OrderBy
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) => OrderBy = orderByExp;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp) => OrderByDescending = orderByDescExp;
        #endregion


        #region Pagination
        //TotalCount = 40 
        //PageSize = 10
        //Page Index = 3
        //10,10,10,10       //Skip = (PageIndex - 1) * PageSize = (3-1) * 10 = 20

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagination { get; set; }   // Default = false , Public set => To Set Value Freedomelly 
        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPagination = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
        #endregion






    }
}
// If I Used Private Set Only I Can Set It From Constructor or Inside The Class Methods
//When U Use Private Instructor It Mean U Absolutely Want To Make Constrructor Chaining On This Private Constructor When Be Inhereted 
//If U Didn't User Private Set U Can Simply Assign To It When Initaite the Property Directly
// I Writed The Base Specification Here Because It Consederes As A Service




//Three Ways To load Related Data => Explecet Loading , Egger Loading , Lazy Loading


// الlist ممكن يتعملها initialize بذون private set; 

//in BaseSpecification بعمل فقط الطريقه الللي هضيف بيها قيم لل properties