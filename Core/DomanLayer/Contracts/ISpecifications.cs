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



//Steps 

//1- Create ISpecifications inside Repository interfaces layer
//2- Create Abstract Class "Base Specification" inside Sevice Impelementation Layer  "way to initaite values => Constructor or by fuction"
//3- Create Product Specification Class     => هنا بحذذ TEntity , TKey               "Logic => the sent Expression "        inside ServiceLayer 
//4- Create Specification Evaluator inside Repository Impelementation Layer