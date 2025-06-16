using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface ICacheService
    {
        Task<string?> GetCacheKeyAsync(string key);
        Task SetCacheKeyAsync(string key, object response, TimeSpan expireTime);
    }
}
