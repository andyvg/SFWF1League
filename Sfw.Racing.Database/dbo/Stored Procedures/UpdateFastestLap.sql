
CREATE PROCEDURE UpdateFastestLap @DriverId int AS

UPDATE r SET r.FastestLapPoints = 0 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId

UPDATE r SET r.FastestLapPoints = 5 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId WHERE r.DriverId = @DriverId;