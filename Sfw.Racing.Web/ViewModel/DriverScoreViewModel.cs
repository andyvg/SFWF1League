using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class DriverScoreViewModel
    {
        public DriverScoreViewModel()
        {
            Driver = new Driver();
            DriverPoints = new DriverPoints();
        }

        public Driver Driver { get; set; }
        public DriverPoints DriverPoints { get; set; }
    }
}