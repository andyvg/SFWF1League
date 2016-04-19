using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class Race
    {
        public int RaceId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime RaceDate { get; set; }
        public DateTime FinalEntry{ get; set; }
        public string Image { get; set; }
        public int MaxChangesAllowed { get; set; }

    }
}
