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
        public int SelectedLeagueId { get; set; }
    }
}