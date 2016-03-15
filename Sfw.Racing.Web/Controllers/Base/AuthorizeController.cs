using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var appManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = await appManager.FindByIdAsync(User.Identity.GetUserId());

            return user.PlayerId;
        }
    }
}