using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class ConstructorScoreViewModel
    {
        public ConstructorScoreViewModel()
        {
            Constructor = new Constructor();
            ConstructorPoints = new ConstructorPoints();
        }

        public Constructor Constructor { get; set; }
        public ConstructorPoints ConstructorPoints { get; set; }
    }
}