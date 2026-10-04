using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomanLayer.Models.BaskedModule;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository (IConnectionMultiplexer Connection) : IBasketRepository
    {
        private readonly IDatabase _database = Connection.GetDatabase();   // In-Memmory DataBase ( Redis Database )
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket); // Convert From CustomerBasket Type To String
            bool IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id , JsonBasket,TimeToLive ?? TimeSpan.FromDays(30));        // هتبهت Basket id + و السلة كلها
            if (IsCreatedOrUpdated)                                                                                             // , TimeSpan.FromDays(30) ==> After 30 Day From Now 
                return await GetBasketAsync(basket.Id);
            else 
                return null;
        }

        public async Task<bool> Deletebasketasync(string basketId)
        {
           return await _database.KeyDeleteAsync(basketId);    // Send String Key And Will Be Converted To Redis.Key Automatic
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var Basket = await _database.StringGetAsync(Key);
            // Redise Consist of Redis.Key And Redis.Value 
            // Value Is Struct 
            // So we Check if Redis.value Contains Null value (We Don't Check => that the value بتشاور point To Null)
            if (Basket.IsNullOrEmpty)
                return null; 
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);  
                //Convert From Redis Value To CustomerBasket type
            }





        }
    }
}


// install package => StackExchange.Redis
// U Need To Use Redis Package here ( In Persisence Layer ) so download the package here 
// To Open Connection With Redis Use IConnectionMultiplexer ذه اللي مسؤول عن ال connection ثم اللي جواه ذاتا بيز 
// روح Register Service of Using IConnectionMultiplexer In Program 