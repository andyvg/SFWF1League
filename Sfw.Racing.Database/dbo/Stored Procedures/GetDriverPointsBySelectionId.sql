CREATE PROCEDURE [dbo].[GetDriverPointsBySelectionId] @SelectionId int AS
SELECT s.SelectionId, r.DriverId, r.TotalPoints, CAST((CASE WHEN r.DriverOfDayPoints > 0 THEN 1 ELSE 0 END) AS BIT) DriverOfTheDay FROM RaceResult r INNER JOIN Selection s ON s.SelectionForRaceId = r.RaceId 
AND (r.DriverId = s.Driver1Id or r.DriverId = s.Driver2Id OR r.DriverId = s.Driver3Id OR r.DriverId = s.Driver4Id)
WHERE s.SelectionId = @SelectionId