using Sfw.Racing.DataRepository.Core;
using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Racing.Web.Controllers
{
    public partial class DriverController : Controller
    {
        private IRepository repository;
        public DriverController(IRepository repository)
        {
            this.repository = repository;
        }
        // GET: Driver
        public virtual JsonResult Driver(int DriverId)
        {
            Driver d = repository.GetDriverById(DriverId);

            return Json(new { success = true, model = d }, JsonRequestBehavior.AllowGet);
        }
    }
}