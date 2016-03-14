using Insight.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class PlayerSelection
    {
        [RecordId]
        public int SelectionId { get; set; }
        public int PlayerId { get; set; }
        [Required(ErrorMessage = "You must select primary driver")]
        public int Driver1Id { get; set; }
        [Required(ErrorMessage = "You must select a second driver")]
        public int Driver2Id { get; set; }

        [Required(ErrorMessage = "You must select a third driver")]
        public int Driver3Id { get; set; }
        [Required(ErrorMessage = "You must select a fourth driver")]
        public int Driver4Id { get; set; }
        [ChildRecords]
        public List<Driver> Drivers { set; get; }


        [Required(ErrorMessage = "You must select primary constructor")]
        public int Constructor1Id { get; set; }
        [Required(ErrorMessage = "You must select a second constructor")]
        public int Constructor2Id { get; set; }
        [ChildRecords]
        public List<Constructor> Constructors { set; get; }

        [Required(ErrorMessage = "You must select primary engine")]
        public int Engine1Id { get; set; }
        [Required(ErrorMessage = "You must select a second engine")]
        public int Engine2Id { get; set; }
        [ChildRecords]
        public List<Engine> Engines { set; get; }

        public decimal BudgetSpent { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal MaxBudget { get; set; }
        public string RaceName { get; set; }
        public string Country { get; set; }
        public DateTime RaceDate { get; set; }
        public DateTime FinalEntry { get; set; }
        public string PlayerName { get; set; }
        public string TeamName { get; set; }

        [Required(ErrorMessage = "You must answer the first question")]
        public int Answer1Id { get; set; }
        [Required(ErrorMessage = "You must answer the second question")]
        public int Answer2Id { get; set; }
        [Required(ErrorMessage = "You must answer the third question")]
        public int Answer3Id { get; set; }
    }
}
