CREATE VIEW PlayerEnginePoints AS
SELECT p.PlayerId, COALESCE(a.EnginePoints, 0) EnginePoints
FROM Player p
LEFT OUTER JOIN
(
	SELECT s.PlayerId, SUM(r.TotalPoints) EnginePoints 
	FROM Engine e INNER JOIN Selection s ON s.Engine1Id = e.EngineId OR s.Engine2Id = e.EngineId 
	INNER JOIN EngineResult r ON r.RaceId = s.SelectionForRaceId AND r.EngineId = e.EngineId 
	GROUP BY s.PlayerId
) a ON p.PlayerId = a.PlayerId