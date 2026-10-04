using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICachesRepository          // Deals With Redis In Memmory 
    {
        //Get 
        Task<string?> GetAsync(string CacheKey);

        //Set 
        Task SetAsync(string CacehKey, string CacheValue , TimeSpan TimeToLive);
    }
}
