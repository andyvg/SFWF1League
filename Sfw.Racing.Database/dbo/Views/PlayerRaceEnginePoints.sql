

CREATE VIEW [dbo].[PlayerRaceEnginePoints] AS
SELECT p.PlayerId, r.RaceId, COALESCE(a.EnginePoints, 0) EnginePoints
FROM Player p CROSS JOIN Race r
LEFT OUTER JOIN
(
	SELECT s.PlayerId, r.RaceId, SUM(r.TotalPoints) EnginePoints 
	FROM Engine e INNER JOIN Selection s ON s.Engine1Id = e.EngineId OR s.Engine2Id = e.EngineId 
	INNER JOIN EngineResult r ON r.RaceId = s.SelectionForRaceId AND r.EngineId = e.EngineId 
	GROUP BY s.PlayerId, r.RaceId
) a ON p.PlayerId = a.PlayerId AND r.RaceId = a.RaceId