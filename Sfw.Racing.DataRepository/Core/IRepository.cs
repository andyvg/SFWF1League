﻿using Sfw.Racing.DataRepository.Model;
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
        IList<Driver> GetActiveDrivers();
        Driver GetDriverById(int DriverId);
        Engine GetEngineById(int EngineId);
        Constructor GetConstructorById(int ConstructorId);
        IList<Constructor> GetConstructors();
        IList<Engine> GetEngines();
        IList<Race> GetRaces();
        PlayerSelection GetPlayerSelection(int PlayerId);
        PlayerSelection GetPlayerSelection(int PlayerId, int RaceId);
        Response<PlayerSelection> UpdatePlayerSelection(int PlayerId, int Driver1Id, int Driver2Id, int Driver3Id, int Driver4Id, int Constructor1Id, int Constructor2Id, int Engine1Id, int Engine2Id, int Answer1Id, int Answer2Id, int Answer3Id);
        IList<Question> GetQuestions();
        IList<Question> GetQuestions(int RaceId);
        int CreatePlayer(string Name, string TeamName);
        IList<Player> GetPlayers();
        IList<Player> GetPlayersByLeagueId(int LeagueId, int? RaceId);
        Player GetPlayerByTeamName(string TeamName);
        DateTime GetFinalEntryTime();
        IList<RaceResult> GetRaceResults();
        Response<IList<RaceResult>> CreateRaceResults(IList<RaceResult> results, int FastestLapDriverId, int DriverofDayId);
        Player GetPlayerById(int PlayerId);
        Response<Player> CreatePlayerLeague(int PlayerId, int LeagueId);
        IList<DriverPoints> GetDriverPointsBySelectionId(int SelectionId);
        IList<EnginePoints> GetEnginePointsBySelectionId(int SelectionId);
        IList<ConstructorPoints> GetConstructorPointsBySelectionId(int SelectionId);
        IList<QuestionPoints> GetQuestionPointsBySelectionId(int SelectionId);
        IList<PenaltyPoints> GetPenaltyPointsBySelectionId(int SelectionId);
        CurrentRace GetCurrentRace();
    }
}
