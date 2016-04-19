CREATE PROCEDURE [dbo].[GetPlayersByLeagueIdRaceId] @LeagueId int, @RaceId int = null AS
SELECT p.PlayerId, p.Name, p.TeamName, p.BasePoints, RANK() OVER (ORDER BY SUM(BasePoints) + SUM(DriverPoints) + SUM(EnginePoints) + SUM(ConstructorPoints) + SUM(QuestionPoints) DESC) as [Position], SUM(BasePoints) BasePoints, SUM(DriverPoints) DriverPoints, SUM(EnginePoints) EnginePoints, SUM(ConstructorPoints) ConstructorPoints, SUM(QuestionPoints) QuestionPoints FROM Player p 
INNER JOIN PlayerLeague pl ON p.PlayerId = pl.PlayerId 
INNER JOIN PlayerRaceDriverPoints d ON p.PlayerId = d.PlayerId AND d.RaceId = COALESCE(@RaceId, d.RaceId)
INNER JOIN PlayerRaceEnginePoints e ON p.PlayerId = e.PlayerId AND e.RaceId = COALESCE(@RaceId, e.RaceId)
INNER JOIN PlayerRaceConstructorPoints c ON p.PlayerId = c.PlayerId AND c.RaceId = COALESCE(@RaceId, c.RaceId)
INNER JOIN PlayerRaceQuestionPoints q ON p.PlayerId = q.PlayerId AND q.RaceId = COALESCE(@RaceId, q.RaceId)
WHERE pl.LeagueId = @LeagueId
GROUP BY p.PlayerId, p.Name, p.TeamName, p.BasePoints
ORDER BY SUM(BasePoints) + SUM(DriverPoints) + SUM(EnginePoints) + SUM(ConstructorPoints) + SUM(QuestionPoints) desc