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
        public int Points { get; set; }
        public decimal BudgetSpent { get; set; }
        public IList<League> Leagues { get; set; }
    }
}
