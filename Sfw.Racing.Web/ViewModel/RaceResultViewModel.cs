using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class RaceResultViewModel
    {
        public IList<RaceResult> Results { get; set; }

        public IList<Driver> Drivers { get; set; }
    }
}