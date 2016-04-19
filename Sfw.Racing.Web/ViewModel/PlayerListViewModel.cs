using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class PlayerListViewModel
    {
        public IList<PlayerViewModel> Players { get; set; }
        public IList<League> Leagues { get; set; }
        private IList<Race> races;
        public IList<Race> Races
        {
            get
            {
                return races;
            }
            set
            {
                races = value;
                races.Insert(0, new Race() { RaceId = -1, Name = "All Races" });
            }
        }
        public int SelectedLeagueId { get; set; }
        public int SelectedRaceId { get; set; }
    }
}