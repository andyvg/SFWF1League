﻿using Sfw.Racing.DataRepository.Core;
using Sfw.Racing.DataRepository.Model;
using Sfw.Racing.Web.Controllers.Base;
using Sfw.Racing.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Racing.Web.Controllers
{
    public partial class RaceController : AuthorizeController
    {
        private IRepository repository;
        public RaceController(IRepository repository)
        {
            this.repository = repository;
        }

        public virtual ActionResult Index()
        {
            var results = repository.GetRaceResults();

            RaceResultViewModel model = new RaceResultViewModel()
            {
                Results = results,
                Drivers = repository.GetDrivers()
            };

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            IList<RaceResult> results = repository.GetRaceResults();

            var driverCount = repository.GetDrivers().Count();

            if (results.Count() == 0)
            {
                for (int i = 0; i < driverCount; i++)
                {
                    results.Add(new RaceResult() { Position = i + 1, DriverId = i + 1, Classified = true });
                }
            }

            RaceResultViewModel model = new RaceResultViewModel()
            {
                Results = results,
                Drivers = repository.GetDrivers()
            };

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Create(RaceResultViewModel model)
        {
            IList<RaceResult> results = null;

            if (ModelState.IsValid)
            {
                var response = repository.CreateRaceResults(model.Results);
                if (response.Success)
                {
                    return RedirectToAction(Mvc.Race.Actions.Index());
                }
                else
                {
                    ModelState.AddModelError("RaceResult", response.Message);
                }
            }

            if (results == null)
            {
                results = model.Results;
            }

            model = new RaceResultViewModel()
            {
                Results = results,
                Drivers = repository.GetDrivers()
            };

            return View(model);
        }
    }
}