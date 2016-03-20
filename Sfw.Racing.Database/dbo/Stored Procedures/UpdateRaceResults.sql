
CREATE PROCEDURE [dbo].[UpdateRaceResults] AS

SET XACT_ABORT ON

BEGIN TRANSACTION

UPDATE r SET r.PositionPoints = 0, r.TeammatePoints = 0 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId

UPDATE r SET r.PositionPoints = (CASE WHEN p.RacePoints > 0 THEN p.RacePoints + 5 ELSE 0 END) FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId INNER JOIN RacePoints p ON r.Position = p.Position

UPDATE r SET r.TeammatePoints = 5 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId
WHERE Position IN (
SELECT MIN(Position) FROM RaceResult r 
INNER JOIN CurrentRace cr ON r.RaceId = cr.CurrentRaceId 
INNER JOIN Driver d ON r.DriverId = d.DriverId 
INNER JOIN Constructor c ON d.ConstructorId = c.ConstructorId
GROUP BY c.ConstructorId)

COMMIT TRANSACTION