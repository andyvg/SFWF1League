using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class EngineScoreViewModel
    {
        public EngineScoreViewModel()
        {
            Engine = new Engine();
            EnginePoints = new EnginePoints();
        }

        public Engine Engine { get; set; }
        public EnginePoints EnginePoints { get; set; }
    }
}