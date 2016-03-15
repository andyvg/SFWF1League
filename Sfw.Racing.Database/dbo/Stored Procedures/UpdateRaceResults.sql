CREATE PROCEDURE UpdateRaceResults AS

UPDATE r SET r.PositionPoints = p.RacePoints FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId INNER JOIN RacePoints p ON r.Position = p.Position