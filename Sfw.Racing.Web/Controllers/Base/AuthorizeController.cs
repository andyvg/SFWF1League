using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Racing.Web.Controllers.Base
{
    [Authorize]
    public partial class AuthorizeController : Controller
    {
        protected async Task<int> CurrentPlayerId()
        {
            int? CachedPlayerId = GetPlayerId();

            if (CachedPlayerId.HasValue)
            {
                return CachedPlayerId.Value;
            }

            var appManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = await appManager.FindByIdAsync(User.Identity.GetUserId());

            int PlayerId = user.PlayerId;

            SetPlayerId(PlayerId);

            return PlayerId;
        }

        private int? GetPlayerId()
        {
            var cacheKey = User.Identity.Name + "PlayerId";

            var cache = MemoryCache.Default;
            var item = cache.Get(cacheKey);
            if (item != null)
            {
                return item as int?;
            }
            else
            {
                return null;
            }
        }

        private void SetPlayerId(int PlayerId)
        {
            var cacheKey = User.Identity.Name + "PlayerId";
            var cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddDays(7); //7 days 

            cache.Set(cacheKey, PlayerId, policy);
        }
    }
}