using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Core
{
    public interface IRepository
    {
        IList<Driver> GetDrivers();
        Driver GetDriverById(int DriverId);
        Engine GetEngineById(int EngineId);
        Constructor GetConstructorById(int ConstructorId);
        IList<Constructor> GetConstructors();
        IList<Engine> GetEngines();
        PlayerSelection GetPlayerSelection(int PlayerId);
        PlayerSelection UpdatePlayerSelection(int PlayerId, int Driver1Id, int Driver2Id, int Driver3Id, int Driver4Id, int Constructor1Id, int Constructor2Id, int Engine1Id, int Engine2Id, int Answer1Id, int Answer2Id, int Answer3Id);
        IList<Question> GetQuestions();
        int CreatePlayer(string Name, string TeamName);
        IList<Player> GetPlayers();
        Player GetPlayerByTeamName(string TeamName);
        DateTime GetFinalEntryTime();
    }
}
