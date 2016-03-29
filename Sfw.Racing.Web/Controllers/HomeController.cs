using Sfw.Racing.DataRepository;
using Sfw.Racing.DataRepository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Racing.Web.Controllers
{
    public partial class HomeController : Controller
    {
        private IRepository repository;
        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult LoginStatus()
        {
            return PartialView();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}