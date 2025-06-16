using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace E_Learning.GraduationProject.APIs.Attributes
{
    public class CachedAttribute : Attribute,IAsyncActionFilter
    {
        private readonly int _durationInSeconds = 60;

        public CachedAttribute(int durationInSeconds)
        {
            _durationInSeconds = durationInSeconds;
        }
        public CachedAttribute()
        {
            
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var key = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedData = await _cacheService.GetCacheKeyAsync(key);

            if(!cachedData.IsNullOrEmpty())
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedData,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result =contentResult ;
                return;
            }

            // Execute the endpoint and cache its result

            var executionResult =  await next.Invoke();

            if(executionResult.Result is OkObjectResult response)
            {
                await _cacheService.SetCacheKeyAsync(key, response.Value, TimeSpan.FromSeconds(_durationInSeconds));
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append(request.Path);

            foreach (var (key , value) in request.Query.OrderBy(X => X.Key))
            {
                cacheKey.Append($"|{key}-{value}");
            }

            return cacheKey.ToString();
        }


    }
}
