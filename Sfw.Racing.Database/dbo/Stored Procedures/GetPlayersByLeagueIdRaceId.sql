

CREATE PROCEDURE [dbo].[GetPlayersByLeagueIdRaceId] @LeagueId int, @RaceId int = null AS
SELECT p.PlayerId, p.Name, p.TeamName, 0 [BasePoints], RANK() OVER (ORDER BY SUM(DriverPoints) + SUM(EnginePoints) + SUM(ConstructorPoints) + SUM(QuestionPoints) + SUM(PenaltyPoints) DESC) as [Position], 0 BasePoints, SUM(DriverPoints) DriverPoints, SUM(EnginePoints) EnginePoints, SUM(ConstructorPoints) ConstructorPoints, SUM(QuestionPoints) QuestionPoints, SUM(PenaltyPoints) PenaltyPoints FROM Player p 
INNER JOIN PlayerLeague pl ON p.PlayerId = pl.PlayerId 
INNER JOIN PlayerRaceDriverPoints d ON p.PlayerId = d.PlayerId AND d.RaceId = COALESCE(@RaceId, d.RaceId)
INNER JOIN PlayerRaceEnginePoints e ON p.PlayerId = e.PlayerId AND e.RaceId = COALESCE(@RaceId, e.RaceId)
INNER JOIN PlayerRaceConstructorPoints c ON p.PlayerId = c.PlayerId AND c.RaceId = COALESCE(@RaceId, c.RaceId)
INNER JOIN PlayerRaceQuestionPoints q ON p.PlayerId = q.PlayerId AND q.RaceId = COALESCE(@RaceId, q.RaceId)
INNER JOIN PlayerRacePenaltyPoints n ON p.PlayerId = n.PlayerId and n.RaceId = COALESCE(@RaceId, n.RaceId)
WHERE pl.LeagueId = @LeagueId
GROUP BY p.PlayerId, p.Name, p.TeamName, p.BasePoints
ORDER BY SUM(DriverPoints) + SUM(EnginePoints) + SUM(ConstructorPoints) + SUM(QuestionPoints) + SUM(PenaltyPoints) desc