
CREATE PROCEDURE GetCurrentRaceId AS 
SELECT cr.CurrentRaceId, (SELECT max(RaceId) FROM Race WHERE RaceId < cr.CurrentRaceId) [PrevRaceId], (SELECT min(RaceId) FROM Race WHERE RaceId > cr.CurrentRaceId) [NextRaceId] from CurrentRace cr INNER JOIN Race r ON cr.CurrentRaceId = r.RaceId