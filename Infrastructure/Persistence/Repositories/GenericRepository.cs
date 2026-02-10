using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _DbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) => await _DbContext.Set<TEntity>().AddAsync(entity);



        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _DbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(TKey id) => await _DbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => _DbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => _DbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
           return await SpecificationEvaluator.CreateQuery(_DbContext.Set<TEntity>() , specifications).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_DbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
            //Notic that u used Where Condition Inside The SpecificationEvaluator
            //FirstOrDefaultAsync  => return single entity or null
            //why not FindAsync  => The Return Data Is IQueryable Not From DbSet (DbContext)
            //IQuerable Dont Have Find Method
        }


        public async Task<int> CountAsync (ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_DbContext.Set<TEntity>(), specifications).CountAsync();
        }



    }
}
