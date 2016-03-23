using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public int Points
        {
            get
            {
                return DriverPoints + EnginePoints + ConstructorPoints + QuestionPoints;
            }
        }
        public int DriverPoints { get; set; }
        public int EnginePoints { get; set; }
        public int ConstructorPoints { get; set; }
        public int QuestionPoints { get; set; }
        public decimal BudgetSpent { get; set; }
        public int Position { get; set; }
        public IList<League> Leagues { get; set; }
    }
}
