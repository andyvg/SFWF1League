
--TODO Needs to return total Cost
CREATE PROCEDURE GetPlayerSelectionByPlayerId @PlayerId int AS 
SELECT s.*, r.Name AS [RaceName], r.Country, r.RaceDate, r.FinalEntry, dbo.GetCost(@PlayerId) [BudgetSpent], p.MaxBudget, p.Name AS [PlayerName], p.TeamName
FROM CurrentSelection s 
INNER JOIN 
Race r ON s.SelectionForRaceId = r.RaceId 
INNER JOIN
Player p
ON p.PlayerId = s.PlayerId
WHERE s.PlayerId = @PlayerId

SELECT 1 [SortOrder], S.SelectionId, d1.*, c.Name [ConstructorName] FROM Driver d1 INNER JOIN CurrentSelection S ON d1.DriverId = s.Driver1Id INNER JOIN Constructor c ON d1.ConstructorId = c.ConstructorId WHERE S.PlayerId = @PlayerId
UNION 
SELECT 2, S.SelectionId, d2.*, c.Name [ConstructorName] FROM Driver d2 INNER JOIN CurrentSelection S ON d2.DriverId = s.Driver2Id INNER JOIN Constructor c ON d2.ConstructorId = c.ConstructorId WHERE S.PlayerId = @PlayerId
UNION 
SELECT 3, S.SelectionId, d3.*, c.Name [ConstructorName] FROM Driver d3 INNER JOIN CurrentSelection S ON d3.DriverId = s.Driver3Id INNER JOIN Constructor c ON d3.ConstructorId = c.ConstructorId WHERE S.PlayerId = @PlayerId
UNION 
SELECT 4, S.SelectionId, d4.*, c.Name [ConstructorName] FROM Driver d4 INNER JOIN CurrentSelection S ON d4.DriverId = s.Driver4Id INNER JOIN Constructor c ON d4.ConstructorId = c.ConstructorId WHERE S.PlayerId = @PlayerId
ORDER BY 1;

SELECT 1 [SortOrder], S.SelectionId, c1.* FROM Constructor c1 INNER JOIN CurrentSelection S ON c1.ConstructorId = s.Constructor1Id WHERE S.PlayerId = @PlayerId
UNION 
SELECT 2, S.SelectionId, c2.* FROM Constructor c2 INNER JOIN CurrentSelection S ON c2.ConstructorId = s.Constructor2Id WHERE S.PlayerId = @PlayerId
ORDER BY 1;

SELECT 1 [SortOrder], S.SelectionId, e1.* FROM Engine e1 INNER JOIN CurrentSelection S ON e1.EngineId = s.Engine1Id WHERE S.PlayerId = @PlayerId
UNION 
SELECT 2, S.SelectionId, e2.* FROM Engine e2 INNER JOIN CurrentSelection S ON e2.EngineId = s.Engine2Id WHERE S.PlayerId = @PlayerId
ORDER BY 1;