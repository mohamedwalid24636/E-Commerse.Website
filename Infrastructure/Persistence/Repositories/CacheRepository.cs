using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction;
using StackExchange.Redis;

namespace Service
{
    public class CacheRepository(IConnectionMultiplexer ConnectionMultplexer) : ICachesRepository
    {
        private readonly IDatabase _DataBase = ConnectionMultplexer.GetDatabase();
        public async Task<string?> GetAsync(string CacheKey)
        {
           var CacheValue = await _DataBase.StringGetAsync(CacheKey);
           return CacheValue.IsNullOrEmpty ? null : CacheValue.ToString();
        }

        public async Task SetAsync(string CacehKey, string CacheValue, TimeSpan TimeToLive)
        {
           await  _DataBase.StringSetAsync(CacehKey, CacheValue, TimeToLive);
        }
    }
}
