using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class CurrentRace
    {
        public int CurrentRaceId { get; set; }
        public DateTime CurrentRaceDate { get; set; }
        public int? PrevRaceId { get; set; }
        public DateTime? PrevRaceDate { get; set; }
        public int? NextRaceId { get; set; }
        public DateTime? NextRaceDate { get; set; }
    }
}
