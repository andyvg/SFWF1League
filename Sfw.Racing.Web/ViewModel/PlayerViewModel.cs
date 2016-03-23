﻿using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class PlayerViewModel
    {
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
        [Display(Name="Player")]
        public string Name { get; set; }
        [Display(Name="Total Points")]
        public int Points { get; set; }
        [Display(Name="Pos")]
        public int Position { get; set; }
        [Display(Name = "Budget Spent")]
        [DisplayFormat(DataFormatString ="{0:0.##}")]
        public decimal BudgetSpent { get; set; }
        [Display(Name = "Drivers Pts")]
        public int DriverPoints { get; set; }
        [Display(Name = "Engine Pts")]
        public int EnginePoints { get; set; }
        [Display(Name = "Constructor Pts")]
        public int ConstructorPoints { get; set; }
        [Display(Name = "Question Pts")]
        public int QuestionPoints { get; set; }

        public PlayerViewModel(Player player)
        {
            this.TeamName = player.TeamName;
            this.Name = player.Name;
            this.Points = player.Points;
            this.BudgetSpent = player.BudgetSpent;
            this.Position = player.Position;
            this.DriverPoints = player.DriverPoints;
            this.EnginePoints = player.EnginePoints;
            this.ConstructorPoints = player.ConstructorPoints;
            this.QuestionPoints = player.QuestionPoints;
        }

        public static List<PlayerViewModel> Create(IList<Player> players)
        {
            var model = new List<PlayerViewModel>();
            foreach(Player p in players)
            {
                model.Add(new PlayerViewModel(p));
            }
            return model;
        }

        public IList<League> Leagues { get; set; }
    }
}