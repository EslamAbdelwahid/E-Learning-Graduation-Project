using E_Learning.GraduationProject.Core.Service.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(
            IConnectionMultiplexer redis
            )
        {
            _database =  redis.GetDatabase();
        }

        public async Task<string?> GetCacheKeyAsync(string key)
        {
            var response = await _database.StringGetAsync(key);

            return response.IsNullOrEmpty ? null : response.ToString();
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expireTime)
        {

            await _database.StringSetAsync(key, JsonSerializer.Serialize(response), expireTime);
        }
    }
}
