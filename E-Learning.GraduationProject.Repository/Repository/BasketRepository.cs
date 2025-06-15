using E_Learning.GraduationProject.Core.Entities.Baskets;
using E_Learning.GraduationProject.Core.Repository.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(
            IConnectionMultiplexer redis
            )
        {
            _database =  redis.GetDatabase();
        }
        public async Task<Basket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            
            // redis return json so u need to deserialize it  
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket);
        }

        public async Task<Basket?> SetBasketAsync(Basket basket)
        {
            var res =await _database.StringSetAsync(basket.Id,  JsonSerializer.Serialize(basket),TimeSpan.FromDays(14));

            return res ? await GetBasketAsync(basket.Id) : null;
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        
    }
}
