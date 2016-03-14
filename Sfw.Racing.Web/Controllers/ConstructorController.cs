using Sfw.Racing.DataRepository.Core;
using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Racing.Web.Controllers
{
    public partial class ConstructorController : Controller
    {
        private IRepository repository;
        public ConstructorController(IRepository repository)
        {
            this.repository = repository;
        }
        // GET: Driver
        public virtual JsonResult Constructor(int ConstructorId)
        {
            Constructor d = repository.GetConstructorById(ConstructorId);

            return Json(new { success = true, model = d }, JsonRequestBehavior.AllowGet);
        }
    }
}