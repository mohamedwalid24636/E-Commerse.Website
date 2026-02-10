using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICacheService
    {
        //Get 
        Task<string?> GetAsync(string CacheKey);

        //Set 
        Task SetAsync(string CacehKey, object CacheValue, TimeSpan TimeToLive);
    }
}
