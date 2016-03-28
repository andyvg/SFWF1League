using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Sfw.Racing.DataRepository.Core;

namespace Sfw.Racing.DataRepository
{
    public class CacheManager: ICacheManager
    {
        public T GetCache<T>(string cacheKey) where T : class
        {
            var cache = MemoryCache.Default;
            var item = cache.Get(cacheKey);
            if (item != null)
            {
                return item as T;
            }
            else
            {
                return null;
            }
        }

        public void SetCache<T>(string cacheKey, T item) where T : class
        {
            var cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddDays(7); //7 days 

            cache.Set(cacheKey, item, policy);
        }
    }
}
