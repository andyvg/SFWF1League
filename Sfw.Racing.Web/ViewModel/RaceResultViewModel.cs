using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class RaceResultViewModel
    {
        public IList<RaceResult> Results { get; set; }

        public IList<Driver> Drivers { get; set; }

        [Required]
        public int FastestLapDriverId { get; set; }

        [Required]
        public int DriverofDayId { get; set; }
    }
}