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
        private ICacheManager cacheManager;

        public Repository(IConnectionFactory factory, ICacheManager cacheManager)
        {
            this.factory = factory;
            this.cacheManager = cacheManager;
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
            string cacheKey = "GetConstructorsCache";
            IList<Constructor> constructors = cacheManager.GetCache<IList<Constructor>>(cacheKey);

            if (constructors == null)
            {
                using (IDbConnection conn = factory.Create())
                {
                    constructors = conn.Query<Constructor>("GetConstructors");
                    cacheManager.SetCache<IList<Constructor>>(cacheKey, constructors);
                }
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
            string cacheKey = "GetDriversCache";
            IList<Driver> drivers = cacheManager.GetCache<IList<Driver>>(cacheKey);

            if (drivers == null)
            {
                using (IDbConnection conn = factory.Create())
                {
                    drivers = conn.Query<Driver>("GetDrivers");
                    cacheManager.SetCache<IList<Driver>>(cacheKey, drivers);
                }
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
            string cacheKey = "GetEnginesCache";
            IList<Engine> engines = cacheManager.GetCache<IList<Engine>>(cacheKey);

            if (engines == null)
            {
                using (IDbConnection conn = factory.Create())
                {
                    engines = conn.Query<Engine>("GetEngines");
                    cacheManager.SetCache<IList<Engine>>(cacheKey, engines);
                }
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

        public IList<Question> GetQuestions(int RaceId)
        {
            IList<Question> questions = null;

            using (IDbConnection conn = factory.Create())
            {

                var query = conn.Query("GetQuestions", new { RaceId = RaceId },
                    Query.Returns(Some<Question>.Records)
                        .ThenChildren(Some<Answer>.Records));

                questions = query;
            }

            return questions;
        }

        public Response<PlayerSelection> UpdatePlayerSelection(int PlayerId, int Driver1Id, int Driver2Id, int Driver3Id, int Driver4Id, int Constructor1Id, int Constructor2Id, int Engine1Id, int Engine2Id, int Answer1Id, int Answer2Id, int Answer3Id)
        {
            Response<PlayerSelection> response = new Response<PlayerSelection>() { Success = false };

            using (IDbConnection conn = factory.Create())
            {
                try {
                    var query = conn.Execute("UpdatePlayerSelection", new { PlayerId = PlayerId, Driver1Id = Driver1Id, Driver2Id = Driver2Id, Driver3Id = Driver3Id, Driver4Id = Driver4Id, Constructor1Id = Constructor1Id, Constructor2Id = Constructor2Id, Engine1Id = Engine1Id, Engine2Id = Engine2Id, Answer1Id = Answer1Id, Answer2Id = Answer2Id, Answer3Id = Answer3Id });
                    response.Success = true;
                }
                catch (SqlException e)
                {
                    if (e.Message.Contains("BUDGET_ERROR"))
                    {
                        response.Message = "Could not save. You have exceeded your budget.";
                        response.Status = StatusCode.Error_Budget_Exceeded;
                    }
                    else if (e.Message.Contains("CHANGES_ERROR"))
                    {
                        response.Message = "Could not save. You have made too many changes to your team.";
                        response.Status = StatusCode.Error_Too_Many_Changes;
                    }
                    else
                    {
                        response.Message = "Could not save. Unknown Database Exception.";
                        response.Status = StatusCode.Error_Unknown;
                    }
                }
                catch(Exception)
                {
                    response.Message = "Could not save. Unknown Exception.";
                    response.Status = StatusCode.Error_Unknown;
                }
            }

            response.Result = GetPlayerSelection(PlayerId);

            return response;
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

        public IList<RaceResult> GetRaceResults()
        {
            IList<RaceResult> results = null;

            using (IDbConnection conn = factory.Create())
            {
                results = conn.Query<RaceResult>("GetRaceResult");
            }

            return results;
        }

        public Response<IList<RaceResult>> CreateRaceResults(IList<RaceResult> results, int FastestLapDriverId)
        {
            Response<IList<RaceResult>> response = new Response<IList<RaceResult>>() { Success = false };

            using (IDbConnection conn = factory.Create())
            {
                try
                {
                    conn.Open();

                    //delete current race results
                    conn.Execute("DeleteRaceResults");

                    using (var trans = conn.BeginTransaction())
                    {
                        foreach (RaceResult result in results)
                        {
                            var query = conn.Execute("CreateRaceResult", new { DriverId = result.DriverId, Position = result.Position, Disqualified = result.Disqualified, Classified = result.Classified }, transaction: trans);
                        }

                        conn.Execute("UpdateRaceResults", transaction: trans);
                        conn.Execute("UpdateFastestLap", new { DriverId = FastestLapDriverId }, transaction: trans);
                        response.Success = true;
                        trans.Commit();
                    }
                }
                catch (Exception e)
                {
                    response.Message = "Could not save. " + e.Message;
                    response.Status = StatusCode.Error_Unknown;
                }
            }

            response.Result = GetRaceResults();

            return response;
        }

        public Player GetPlayerById(int PlayerId)
        {
            Player player = null;

            using (IDbConnection conn = factory.Create())
            {
                var query = conn.Query("GetPlayerById", new { PlayerId = PlayerId },
                    Query.Returns(Some<Player>.Records)
                        .ThenChildren(Some<League>.Records));

                player = query[0];
            }

            return player;
        }

        public Response<Player> CreatePlayerLeague(int PlayerId, int LeagueId)
        {
            Response<Player> response = new Response<Player>() { Success = false };

            using (IDbConnection conn = factory.Create())
            {
                var query = conn.Execute("CreatePlayerLeague", new { PlayerId = PlayerId, LeagueId = LeagueId });
                response.Success = true;
            }

            response.Result = GetPlayerById(PlayerId);

            return response;
        }

        public IList<Player> GetPlayerByLeagueId(int LeagueId)
        {
            IList<Player> players = null;

            using (IDbConnection conn = factory.Create())
            {
                players = conn.Query<Player>("GetPlayersByLeagueId", new { LeagueId = LeagueId } );
            }

            return players;
        }

        public IList<DriverPoints> GetDriverPointsBySelectionId(int SelectionId)
        {
            IList<DriverPoints> points = null;

            using (IDbConnection conn = factory.Create())
            {
                points = conn.Query<DriverPoints>("GetDriverPointsBySelectionId", new { SelectionId = SelectionId });
            }

            return points;
        }

        public IList<EnginePoints> GetEnginePointsBySelectionId(int SelectionId)
        {
            IList<EnginePoints> points = null;

            using (IDbConnection conn = factory.Create())
            {
                points = conn.Query<EnginePoints>("GetEnginePointsBySelectionId", new { SelectionId = SelectionId });
            }

            return points;
        }

        public IList<ConstructorPoints> GetConstructorPointsBySelectionId(int SelectionId)
        {
            IList<ConstructorPoints> points = null;

            using (IDbConnection conn = factory.Create())
            {
                points = conn.Query<ConstructorPoints>("GetConstructorPointsBySelectionId", new { SelectionId = SelectionId });
            }

            return points;
        }

        public IList<QuestionPoints> GetQuestionPointsBySelectionId(int SelectionId)
        {
            IList<QuestionPoints> points = null;

            using (IDbConnection conn = factory.Create())
            {
                points = conn.Query<QuestionPoints>("GetQuestionPointsBySelectionId", new { SelectionId = SelectionId });
            }

            return points;
        }

        public PlayerSelection GetPlayerSelection(int PlayerId, int RaceId)
        {
            PlayerSelection playerSelection = null;

            using (IDbConnection conn = factory.Create())
            {

                var query = conn.Query("GetPlayerSelectionByPlayerId", new { PlayerId = PlayerId, RaceId = RaceId },
                    Query.Returns(Some<PlayerSelection>.Records)
                        .ThenChildren(Some<Driver>.Records)
                        .ThenChildren(Some<Constructor>.Records)
                        .ThenChildren(Some<Engine>.Records));

                if (query.Count() > 0)
                {
                    playerSelection = query[0];
                }
            }

            return playerSelection;
        }

        public CurrentRace GetCurrentRace()
        {
            CurrentRace Race;

            using (IDbConnection conn = factory.Create())
            {

                Race = conn.Query<CurrentRace>("GetCurrentRaceId").FirstOrDefault();
            }

            return Race;
        }
    }
}
