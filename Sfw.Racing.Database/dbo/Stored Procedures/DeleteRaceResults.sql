CREATE PROCEDURE DeleteRaceResults AS
DELETE r FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId