

CREATE PROCEDURE [dbo].[GetCurrentRaceId] AS 
SELECT cr.CurrentRaceId, r.RaceDate [CurrentRaceDate], 
(SELECT max(RaceId) FROM Race WHERE RaceId < cr.CurrentRaceId) [PrevRaceId],
(SELECT RaceDate From Race WHERE RaceID = (SELECT max(RaceId) FROM Race WHERE RaceId < cr.CurrentRaceId)) [PrevRaceDate],
(SELECT min(RaceId) FROM Race WHERE RaceId > cr.CurrentRaceId) [NextRaceId],
(SELECT RaceDate From Race WHERE RaceID = (SELECT min(RaceId) FROM Race WHERE RaceId > cr.CurrentRaceId)) [NextRaceDate]
from CurrentRace cr INNER JOIN Race r ON cr.CurrentRaceId = r.RaceId