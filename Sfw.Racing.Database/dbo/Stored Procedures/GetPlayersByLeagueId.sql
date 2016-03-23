
CREATE PROCEDURE [dbo].[GetPlayersByLeagueId] @LeagueId int AS
SELECT p.*, RANK() OVER (ORDER BY DriverPoints + EnginePoints + ConstructorPoints + QuestionPoints DESC) as [Position], DriverPoints, EnginePoints, ConstructorPoints, QuestionPoints FROM Player p 
INNER JOIN PlayerLeague pl ON p.PlayerId = pl.PlayerId 
INNER JOIN PlayerDriverPoints d ON p.PlayerId = d.PlayerId
INNER JOIN PlayerEnginePoints e ON p.PlayerId = e.PlayerId
INNER JOIN PlayerConstructorPoints c ON p.PlayerId = c.PlayerId
INNER JOIN PlayerQuestionPoints q ON p.PlayerId = q.PlayerId
WHERE pl.LeagueId = @LeagueId
ORDER BY DriverPoints + EnginePoints + ConstructorPoints + QuestionPoints desc