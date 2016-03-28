using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Core
{
    public interface ICacheManager
    {
        T GetCache<T>(string cacheKey) where T : class;

        void SetCache<T>(string cacheKey, T item) where T : class;
    }
}
