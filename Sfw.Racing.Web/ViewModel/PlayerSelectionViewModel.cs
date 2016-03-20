using Sfw.Racing.DataRepository.Model;
using Sfw.Racing.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Racing.Web.ViewModel
{
    public class PlayerSelectionViewModel
    {
        //TODO replace with ViewModels
        public PlayerSelection PlayerSelection { get; set; }

        public IList<Driver> Drivers { get; set; }
        public IList<Constructor> Constructors { get; set; }
        public IList<Engine> Engines { get; set; }
        public IList<Question> Questions { get; set; }

        public IList<DriverPoints> DriverPoints { get; set; }
        public IList<ConstructorPoints> ConstructorPoints { get; set; }
        public IList<EnginePoints> EnginePoints { get; set; }
        public IList<QuestionPoints> QuestionPoints { get; set; }

        public int TotalScore
        {
            get
            {
                return DriverPoints.Sum(p => p.TotalPoints) + ConstructorPoints.Sum(p => p.TotalPoints) + EnginePoints.Sum(p => p.TotalPoints) + QuestionPoints.Sum(p => p.Points);
            }
        }

        [EnforceTrueAttribute(ErrorMessage = "You cannot select the same driver twice")]
        public bool AreDriversUnique {
            get
            {
                if(PlayerSelection != null)
                {
                    var drivers = new int[] { PlayerSelection.Driver1Id, PlayerSelection.Driver2Id, PlayerSelection.Driver3Id, PlayerSelection.Driver4Id };
                    return drivers.Distinct().Count() == drivers.Count();
                }
                return true;
            }
        }

        [EnforceTrueAttribute(ErrorMessage = "You cannot select the same constructor twice")]
        public bool AreConstructorsUnique
        {
            get
            {
                if (PlayerSelection != null)
                {
                    return PlayerSelection.Constructor1Id != PlayerSelection.Constructor2Id;
                }
                return true;
            }
        }

        [EnforceTrueAttribute(ErrorMessage = "You cannot select the same engine twice")]
        public bool AreEnginesUnique
        {
            get
            {
                if (PlayerSelection != null)
                {
                    return PlayerSelection.Engine1Id != PlayerSelection.Engine2Id;
                }
                return true;
            }
        }

        public void UpdateSelection()
        {
            Driver driver1 = Drivers.Where(p => p.DriverId == PlayerSelection.Driver1Id).FirstOrDefault() ?? new Driver();
            Driver driver2 = Drivers.Where(p => p.DriverId == PlayerSelection.Driver2Id).FirstOrDefault() ?? new Driver();
            Driver driver3 = Drivers.Where(p => p.DriverId == PlayerSelection.Driver3Id).FirstOrDefault() ?? new Driver();
            Driver driver4 = Drivers.Where(p => p.DriverId == PlayerSelection.Driver4Id).FirstOrDefault() ?? new Driver();

            Engine engine1 = Engines.Where(e => e.EngineId == PlayerSelection.Engine1Id).FirstOrDefault() ?? new Engine();
            Engine engine2 = Engines.Where(e => e.EngineId == PlayerSelection.Engine2Id).FirstOrDefault() ?? new Engine();

            Constructor constructor1 = Constructors.Where(e => e.ConstructorId == PlayerSelection.Constructor1Id).FirstOrDefault() ?? new Constructor();
            Constructor constructor2 = Constructors.Where(e => e.ConstructorId == PlayerSelection.Constructor2Id).FirstOrDefault() ?? new Constructor();

            PlayerSelection.BudgetSpent = driver1.Cost + driver2.Cost + driver3.Cost + driver4.Cost + engine1.Cost + engine2.Cost + constructor1.Cost + constructor2.Cost;

            PlayerSelection.Drivers = new List<Driver>() { driver1, driver2, driver3, driver4 };
            PlayerSelection.Engines = new List<Engine>() { engine1, engine2 };
            PlayerSelection.Constructors = new List<Constructor>() { constructor1, constructor2 };
        }
    }
}