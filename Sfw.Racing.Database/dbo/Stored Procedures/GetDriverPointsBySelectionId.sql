CREATE PROCEDURE GetDriverPointsBySelectionId @SelectionId int AS
SELECT r.DriverId, r.TotalPoints FROM RaceResult r INNER JOIN Selection s ON s.SelectionForRaceId = r.RaceId 
AND (r.DriverId = s.Driver1Id or r.DriverId = s.Driver2Id OR r.DriverId = s.Driver3Id OR r.DriverId = s.Driver4Id)
WHERE s.SelectionId = @SelectionId