CREATE PROCEDURE GetEnginePointsBySelectionId @SelectionId int AS
SELECT s.SelectionId, r.EngineId, r.TotalPoints FROM EngineResult r INNER JOIN Selection s ON s.SelectionForRaceId = r.RaceId 
AND (r.EngineId = s.Engine1Id or r.EngineId = s.Engine2Id)
WHERE s.SelectionId = @SelectionId