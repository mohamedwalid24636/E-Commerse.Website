using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork (StoreDbContext  _DbContext): IUnitOfWork 
    {
        private readonly Dictionary<string, object> _repositories = []; //   [] => new Dictionary<string, object>()
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Get Type Name of Entity 
            var typeName = typeof(TEntity).Name;
            //if (_repositories.ContainsKey(typeName))
            //return (IGenericRepository<TEntity, TKey>)_repositories[typeName];



            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;


            else
            {
                //Create  IGenericRepository<TEntity, TKey> instance
                var Repo = new GenericRepository<TEntity, TKey>(_DbContext);
                //Store instance in Dictionary
                _repositories[typeName] = Repo;
                //return instance
                return Repo;
            }




        }

        public async Task<int> SaveChangesAsync() => await _DbContext.SaveChangesAsync();

    }
}
