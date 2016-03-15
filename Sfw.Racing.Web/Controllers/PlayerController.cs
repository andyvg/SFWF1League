using Sfw.Racing.DataRepository.Core;
using Sfw.Racing.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sfw.Racing.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace Sfw.Racing.Web.Controllers
{
    [Authorize]
    public partial class PlayerController : Controller
    {
        private IRepository repository;
        public PlayerController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            var model = PlayerViewModel.Create(repository.GetPlayers());

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Detail(string Id)
        {
            var player = repository.GetPlayerByTeamName(Id);

            PlayerSelectionViewModel model = new PlayerSelectionViewModel()
            {
                PlayerSelection = repository.GetPlayerSelection(player.PlayerId),
                Questions = repository.GetQuestions()
            };

            return View(model);
        }

        private async Task<int> CurrentPlayerId()
        {
            var appManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = await appManager.FindByIdAsync(User.Identity.GetUserId());

            return user.PlayerId;
        }

        // GET: Player
        [HttpGet]
        public virtual async Task<ActionResult> Edit()
        {
            PlayerSelectionViewModel model = new PlayerSelectionViewModel()
            {
                PlayerSelection = repository.GetPlayerSelection(await CurrentPlayerId()),
                Drivers = repository.GetDrivers(),
                Constructors = repository.GetConstructors(),
                Engines = repository.GetEngines(),
                Questions = repository.GetQuestions()
            };

            return View(model);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Edit(PlayerSelectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (repository.GetFinalEntryTime() >= DateTime.Now)
                {

                    var result = repository.UpdatePlayerSelection(await CurrentPlayerId(), model.PlayerSelection.Driver1Id, model.PlayerSelection.Driver2Id, model.PlayerSelection.Driver3Id, model.PlayerSelection.Driver4Id,
                        model.PlayerSelection.Constructor1Id, model.PlayerSelection.Constructor2Id, model.PlayerSelection.Engine1Id, model.PlayerSelection.Engine2Id, model.PlayerSelection.Answer1Id, model.PlayerSelection.Answer2Id, model.PlayerSelection.Answer3Id);

                    model = new PlayerSelectionViewModel()
                    {
                        PlayerSelection = result.Success ? result.Result : model.PlayerSelection,
                        Drivers = repository.GetDrivers(),
                        Constructors = repository.GetConstructors(),
                        Engines = repository.GetEngines(),
                        Questions = repository.GetQuestions()
                    };


                    if (!result.Success)
                    {
                        ModelState.AddModelError("Driver1Id", result.Message);
                        model.UpdateSelection();
                    }
                }
                else
                {
                    ModelState.AddModelError("Driver1Id", "Could not save. Race entries are closed for the upcoming race.");
                    BindModel(model);
                }
            }
            else
            {
                BindModel(model);
            }

            return View(model);
        }

        private void BindModel(PlayerSelectionViewModel model)
        {
            model.Drivers = repository.GetDrivers();
            model.Constructors = repository.GetConstructors();
            model.Engines = repository.GetEngines();
            model.Questions = repository.GetQuestions();
            model.UpdateSelection();
        }
    }
}