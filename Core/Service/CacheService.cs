using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceAbstraction;

namespace Service
{
    public class CacheService(ICachesRepository cachesRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
           return await cachesRepository.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacehKey, object CacheValue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(CacheValue);
            await cachesRepository.SetAsync(CacehKey, Value, TimeToLive);
        }
    }
}
