CREATE PROCEDURE GetConstructorPointsBySelectionId @SelectionId int AS
SELECT s.SelectionId, r.ConstructorId, r.TotalPoints FROM ConstructorResult r INNER JOIN Selection s ON s.SelectionForRaceId = r.RaceId 
AND (r.ConstructorId = s.Constructor1Id or r.ConstructorId = s.Constructor2Id)
WHERE s.SelectionId = @SelectionId