CREATE PROCEDURE GetFinalEntryTime AS
SELECT r.FinalEntry FROM CurrentRace c INNER JOIN Race r ON c.CurrentRaceId = r.RaceId;