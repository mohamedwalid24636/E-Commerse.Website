using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;

namespace DomanLayer.Contracts
{
    public interface ISpecifications<TEntity , TKey > where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity , object>> OrderByDescending { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagination { get; }
    }
}
// Notic We Used Only Get Properties Because Specifications Are ReadOnly