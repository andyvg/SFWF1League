using Insight.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class Driver
    {
        [RecordId]
        public int DriverId { get; set; }
        public string Name { get; set; }
        public int ConstructorId { get; set; }
        public decimal Cost { get; set; }
        public string ConstructorName { get; set; }
        public string Image { get; set; }

        [ParentRecordId]
        int SelectionId { get; set; }

        public string NameCost
        {
            get
            {
                return $"{Name} ({Cost}m)";
            }
        }

    }
}
