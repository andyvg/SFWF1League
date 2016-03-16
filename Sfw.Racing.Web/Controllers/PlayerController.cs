using Sfw.Racing.DataRepository.Core;
using Sfw.Racing.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sfw.Racing.Web.Models;
using System.Threading.Tasks;
using Sfw.Racing.Web.Controllers.Base;
using Sfw.Racing.DataRepository.Model;

namespace Sfw.Racing.Web.Controllers
{
    public partial class PlayerController : AuthorizeController
    {
        private IRepository repository;
        public PlayerController(IRepository repository)
        {
            this.repository = repository;
        }
        protected async Task<IList<League>> CurrentLeagues()
        {
            int PlayerId = await CurrentPlayerId();

            var player = repository.GetPlayerById(PlayerId);

            var leagues = player.Leagues;

            return leagues;
        }

        [HttpGet]
        public virtual async Task<ActionResult> Index(int? SelectedLeagueId)
        {
            PlayerListViewModel model = new PlayerListViewModel()
            {
                Leagues = await CurrentLeagues(),
            };

            if (SelectedLeagueId.HasValue)
            {
                model.SelectedLeagueId = SelectedLeagueId.Value;
            }
            else if(model.Leagues.Count() > 0)
            {
                model.SelectedLeagueId = model.Leagues[0].LeagueId;
            }

            model.Players = PlayerViewModel.Create(repository.GetPlayerByLeagueId(model.SelectedLeagueId));

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Detail(string TeamName)
        {
            var player = repository.GetPlayerByTeamName(TeamName);

            PlayerSelectionViewModel model = new PlayerSelectionViewModel()
            {
                PlayerSelection = repository.GetPlayerSelection(player.PlayerId),
                Questions = repository.GetQuestions()
            };

            return View(model);
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