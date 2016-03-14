using Insight.Database;
using Sfw.Racing.DataRepository.Core;
using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository
{
    public class Repository: IRepository
    {
        private IConnectionFactory factory;

        public Repository(IConnectionFactory factory)
        {
            this.factory = factory;
        }

        public static void Register()
        {
            SqlInsightDbProvider.RegisterProvider();
        }

        public int CreatePlayer(string Name, string TeamName)
        {
            int PlayerId = -1;

            using (IDbConnection conn = factory.Create())
            {
                var output = new OutputParams();
                var x = conn.Execute("CreatePlayer", new { Name = Name, TeamName = TeamName, NewId = 0 }, outputParameters: output);
                PlayerId = output.NewId;
            }

            return PlayerId;
        }

        public Constructor GetConstructorById(int ConstructorId)
        {
            Constructor constructor = null;

            using (IDbConnection conn = factory.Create())
            {
                constructor = conn.Query<Constructor>("GetConstructorById", new { ConstructorId = ConstructorId }).FirstOrDefault();
            }

            return constructor;
        }

        public IList<Constructor> GetConstructors()
        {
            IList<Constructor> constructors = null;

            using (IDbConnection conn = factory.Create())
            {
                constructors = conn.Query<Constructor>("GetConstructors");
            }

            return constructors;
        }

        public Driver GetDriverById(int DriverId)
        {
            Driver driver = null;

            using (IDbConnection conn = factory.Create())
            {
                driver = conn.Query<Driver>("GetDriverById", new { DriverId = DriverId }).FirstOrDefault();
            }

            return driver;
        }

        public IList<Driver> GetDrivers()
        {
            IList<Driver> drivers = null;

            using (IDbConnection conn = factory.Create())
            {
                drivers = conn.Query<Driver>("GetDrivers");
            }

            return drivers;
        }

        public Engine GetEngineById(int EngineId)
        {
            Engine engine = null;

            using (IDbConnection conn = factory.Create())
            {
                engine = conn.Query<Engine>("GetEngineById", new { EngineId = EngineId }).FirstOrDefault();
            }

            return engine;
        }

        public IList<Engine> GetEngines()
        {
            IList<Engine> engines = null;

            using (IDbConnection conn = factory.Create())
            {
                engines = conn.Query<Engine>("GetEngines");
            }

            return engines;
        }

        public PlayerSelection GetPlayerSelection(int PlayerId)
        {
            PlayerSelection playerSelection = null;

            using (IDbConnection conn = factory.Create())
            { 
                
                var query = conn.Query("GetPlayerSelectionByPlayerId", new { PlayerId = PlayerId }, 
                    Query.Returns(Some<PlayerSelection>.Records)
                        .ThenChildren(Some<Driver>.Records)
                        .ThenChildren(Some<Constructor>.Records)
                        .ThenChildren(Some<Engine>.Records));

                playerSelection = query[0];
            }

            return playerSelection;
        }

        public IList<Question> GetQuestions()
        {
            IList<Question> questions = null;

            using (IDbConnection conn = factory.Create())
            {

                var query = conn.Query("GetQuestions", null,
                    Query.Returns(Some<Question>.Records)
                        .ThenChildren(Some<Answer>.Records));

                questions = query;
            }

            return questions;
        }

        public PlayerSelection UpdatePlayerSelection(int PlayerId, int Driver1Id, int Driver2Id, int Driver3Id, int Driver4Id, int Constructor1Id, int Constructor2Id, int Engine1Id, int Engine2Id, int Answer1Id, int Answer2Id, int Answer3Id)
        {
            using (IDbConnection conn = factory.Create())
            {
                try {
                    var query = conn.Execute("UpdatePlayerSelection", new { PlayerId = PlayerId, Driver1Id = Driver1Id, Driver2Id = Driver2Id, Driver3Id = Driver3Id, Driver4Id = Driver4Id, Constructor1Id = Constructor1Id, Constructor2Id = Constructor2Id, Engine1Id = Engine1Id, Engine2Id = Engine2Id, Answer1Id = Answer1Id, Answer2Id = Answer2Id, Answer3Id = Answer3Id });
                }
                catch (SqlException e)
                {
                    //TODO handle other types of errors
                    if (e.Message.Contains("BUDGET_ERROR"))
                    {
                        return null;
                    }
                }
            }

            return GetPlayerSelection(PlayerId);
        }

        public IList<Player> GetPlayers()
        {
            IList<Player> players = null;

            using (IDbConnection conn = factory.Create())
            {
                players = conn.Query<Player>("GetPlayers");
            }

            return players;
        }

        public Player GetPlayerByTeamName(string TeamName)
        {
            Player player = null;

            using (IDbConnection conn = factory.Create())
            {
                player = conn.Query<Player>("GetPlayerByTeamName", new { TeamName = TeamName }).FirstOrDefault();
            }

            return player;
        }

        public DateTime GetFinalEntryTime()
        {
            DateTime datetime = DateTime.Now;

            using (IDbConnection conn = factory.Create())
            {
                datetime = conn.Query<DateTime>("GetFinalEntryTime").FirstOrDefault();
            }

            return datetime;
        }
    }
}
