using Sfw.Racing.DataRepository.Core;
using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Racing.Web.Controllers
{
    public partial class EngineController : Controller
    {
        private IRepository repository;
        public EngineController(IRepository repository)
        {
            this.repository = repository;
        }
        // GET: Driver
        [OutputCache(Duration = 3600, Location = System.Web.UI.OutputCacheLocation.Server, VaryByParam = "EngineId")]
        public virtual JsonResult Engine(int EngineId)
        {
            Engine d = repository.GetEngineById(EngineId);

            return Json(new { success = true, model = d }, JsonRequestBehavior.AllowGet);
        }
    }
}