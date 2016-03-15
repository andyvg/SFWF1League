using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class League
    {
        [RecordId]
        public int LeagueId { get; set; }
        public string Name { get; set; }
        [ParentRecordId]
        public int PlayerId { get; set; }
        [ChildRecords]
        public IList<Player> Players { get; set; }
    }
}
