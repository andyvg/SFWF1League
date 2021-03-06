﻿using Sfw.Racing.DataRepository.Core;
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
using DevTrends.MvcDonutCaching;

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
        public virtual async Task<ActionResult> Index(int? SelectedLeagueId, int? SelectedRaceId)
        {
            PlayerListViewModel model = new PlayerListViewModel()
            { 
                Leagues = await CurrentLeagues(),
                Races = repository.GetRaces()
            };

            if (SelectedLeagueId.HasValue)
            {
                model.SelectedLeagueId = SelectedLeagueId.Value;
            }
            else if(model.Leagues.Count() > 0)
            {
                model.SelectedLeagueId = model.Leagues[0].LeagueId;
            }

            if (SelectedRaceId.HasValue)
            {
                model.SelectedRaceId = SelectedRaceId.Value;
            }

            model.Players = PlayerViewModel.Create(repository.GetPlayersByLeagueId(model.SelectedLeagueId, model.SelectedRaceId <= 0 ? (int?)null : model.SelectedRaceId));

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Detail(string TeamName)
        {
            var player = repository.GetPlayerByTeamName(TeamName);

            var currentRace = repository.GetCurrentRace();
            int RaceId = currentRace.CurrentRaceId;

            //if there is a previous race, and the current race has not yet run, and the next race is not available
            if (currentRace.PrevRaceId.HasValue && currentRace.CurrentRaceDate > DateTime.Now && !currentRace.NextRaceId.HasValue)
            {
                RaceId = currentRace.PrevRaceId.Value;
            }

            PlayerSelectionViewModel model = new PlayerSelectionViewModel()
            {
                PlayerSelection = repository.GetPlayerSelection(player.PlayerId, RaceId),
                Questions = repository.GetQuestions(RaceId),
            };

            if (model.PlayerSelection != null)
            {
                model.DriverPoints = repository.GetDriverPointsBySelectionId(model.PlayerSelection.SelectionId);
                model.ConstructorPoints = repository.GetConstructorPointsBySelectionId(model.PlayerSelection.SelectionId);
                model.EnginePoints = repository.GetEnginePointsBySelectionId(model.PlayerSelection.SelectionId);
                model.QuestionPoints = repository.GetQuestionPointsBySelectionId(model.PlayerSelection.SelectionId);
                model.PenaltyPoints = repository.GetPenaltyPointsBySelectionId(model.PlayerSelection.SelectionId);
            }
            else
            {
                return View(Mvc.Player.Views.NoEntry);
            }

            return View(model);
        }

        [HttpGet]
        public virtual async Task<ActionResult> RaceDetail(string TeamName, int RaceId)
        {
            var player = repository.GetPlayerByTeamName(TeamName);

            int currentPlayerId = await CurrentPlayerId();

            if (player.PlayerId == currentPlayerId)
            {
                var curRace = repository.GetCurrentRace();

                if (RaceId == curRace.CurrentRaceId && curRace.CurrentRaceDate > DateTime.Now)
                {
                    return RedirectToAction(Mvc.Player.Edit());
                }
            }

            PlayerSelectionViewModel model = new PlayerSelectionViewModel()
            {
                PlayerSelection = repository.GetPlayerSelection(player.PlayerId, RaceId),
                Questions = repository.GetQuestions(RaceId),
            };

            if (model.PlayerSelection != null)
            {
                model.DriverPoints = repository.GetDriverPointsBySelectionId(model.PlayerSelection.SelectionId);
                model.ConstructorPoints = repository.GetConstructorPointsBySelectionId(model.PlayerSelection.SelectionId);
                model.EnginePoints = repository.GetEnginePointsBySelectionId(model.PlayerSelection.SelectionId);
                model.QuestionPoints = repository.GetQuestionPointsBySelectionId(model.PlayerSelection.SelectionId);
                model.PenaltyPoints = repository.GetPenaltyPointsBySelectionId(model.PlayerSelection.SelectionId);
            }
            else
            {
                return View(Mvc.Player.Views.NoEntry);
            }

            return View(Mvc.Player.Views.ViewNames.Detail, model);
        }

        // GET: Player
        [HttpGet]
        public virtual async Task<ActionResult> Edit()
        {
            int PlayerId = await CurrentPlayerId();

            PlayerSelectionViewModel model = new PlayerSelectionViewModel()
            {
                PlayerSelection = repository.GetPlayerSelection(PlayerId),
                Drivers = repository.GetActiveDrivers(),
                Constructors = repository.GetConstructors(),
                Engines = repository.GetEngines(),
                Questions = repository.GetQuestions()
            };
            
            model.UpdateSelection();

            CurrentRace race = repository.GetCurrentRace();

            if(race.CurrentRaceDate < DateTime.Now)
            {
                return RedirectToAction(Mvc.Player.Detail(model.PlayerSelection.TeamName));
            }

            if (race.PrevRaceId.HasValue)
            {
                model.PreviousPlayerSelection = repository.GetPlayerSelection(PlayerId, race.PrevRaceId.Value);
            }

            return View(model);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Edit(PlayerSelectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (repository.GetFinalEntryTime() >= DateTime.Now)
                {
                    int PlayerId = await CurrentPlayerId();

                    var result = repository.UpdatePlayerSelection(PlayerId, model.PlayerSelection.Driver1Id, model.PlayerSelection.Driver2Id, model.PlayerSelection.Driver3Id, model.PlayerSelection.Driver4Id,
                        model.PlayerSelection.Constructor1Id, model.PlayerSelection.Constructor2Id, model.PlayerSelection.Engine1Id, model.PlayerSelection.Engine2Id, model.PlayerSelection.Answer1Id, model.PlayerSelection.Answer2Id, model.PlayerSelection.Answer3Id);

                    model = new PlayerSelectionViewModel()
                    {
                        PlayerSelection = result.Success ? result.Result : model.PlayerSelection,
                        Drivers = repository.GetActiveDrivers(),
                        Constructors = repository.GetConstructors(),
                        Engines = repository.GetEngines(),
                        Questions = repository.GetQuestions()
                    };

                    model.UpdateSelection();

                    CurrentRace race = repository.GetCurrentRace();

                    if (race.PrevRaceId.HasValue)
                    {
                        model.PreviousPlayerSelection = repository.GetPlayerSelection(PlayerId, race.PrevRaceId.Value);
                    }

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
            model.Drivers = repository.GetActiveDrivers();
            model.Constructors = repository.GetConstructors();
            model.Engines = repository.GetEngines();
            model.Questions = repository.GetQuestions();
            model.UpdateSelection();
        }
    }
}