using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;

namespace DomanLayer.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;    // ✅ 2. تعريف Generic على مستوى الفنكشن فقط After Function Name 
        Task<int> SaveChangesAsync();
    }
}
