CREATE PROCEDURE [dbo].[GetPlayers] AS
SELECT p.*, [dbo].[GetCost](p.PlayerId) [BudgetSpent], DriverPoints, EnginePoints, ConstructorPoints FROM Player p
INNER JOIN PlayerDriverPoints d ON p.PlayerId = d.PlayerId
INNER JOIN PlayerEnginePoints e ON p.PlayerId = e.PlayerId
INNER JOIN PlayerConstructorPoints c ON p.PlayerId = c.PlayerId
INNER JOIN PlayerQuestionPoints q ON p.PlayerId = q.PlayerId
ORDER BY DriverPoints + EnginePoints + ConstructorPoints desc