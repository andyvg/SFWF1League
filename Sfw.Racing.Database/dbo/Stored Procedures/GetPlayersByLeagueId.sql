
CREATE PROCEDURE [dbo].[GetPlayersByLeagueId] @LeagueId int AS
SELECT p.*, RANK() OVER (ORDER BY BasePoints + DriverPoints + EnginePoints + ConstructorPoints + QuestionPoints + PenaltyPoints DESC) as [Position], BasePoints, DriverPoints, EnginePoints, ConstructorPoints, QuestionPoints, PenaltyPoints FROM Player p 
INNER JOIN PlayerLeague pl ON p.PlayerId = pl.PlayerId
INNER JOIN PlayerDriverPoints d ON p.PlayerId = d.PlayerId
INNER JOIN PlayerEnginePoints e ON p.PlayerId = e.PlayerId
INNER JOIN PlayerConstructorPoints c ON p.PlayerId = c.PlayerId
INNER JOIN PlayerQuestionPoints q ON p.PlayerId = q.PlayerId
INNER JOIN PlayerPenaltyPoints n ON p.PlayerId = n.PlayerId
WHERE pl.LeagueId = @LeagueId
ORDER BY BasePoints + DriverPoints + EnginePoints + ConstructorPoints + QuestionPoints + PenaltyPoints desc