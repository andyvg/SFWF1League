
CREATE PROCEDURE [dbo].[UpdateDriverOfDay] @DriverId int AS

UPDATE r SET r.DriverOfDayPoints = 0 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId

UPDATE r SET r.DriverOfDayPoints = 10 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId WHERE r.DriverId = @DriverId;