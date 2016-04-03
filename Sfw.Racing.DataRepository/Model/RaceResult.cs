using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class RaceResult
    {
        public int RaceResultId { get; set; }
        public int RaceId { get; set; }
        public int DriverId { get; set; }
        public int Position { get; set; }
        public bool Disqualified { get; set; }
        public bool Classified { get; set; }
        public int PositionPoints { get; set; }
        public int FastestLapPoints { get; set; }
        public int RaceFinishPoints { get; set; }
        public int TeammatePoints { get; set; }
        public int TotalPoints { get; set; }
    }
}
