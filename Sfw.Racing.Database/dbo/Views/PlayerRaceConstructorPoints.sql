

CREATE VIEW [dbo].[PlayerRaceConstructorPoints] AS
SELECT p.PlayerId, r.RaceId, COALESCE(a.ConstructorPoints, 0) ConstructorPoints
FROM Player p CROSS JOIN Race r
LEFT OUTER JOIN
(
	SELECT s.PlayerId, r.RaceId, SUM(r.TotalPoints) ConstructorPoints 
	FROM Constructor c 
	INNER JOIN Selection s ON s.Constructor1Id = c.ConstructorId OR s.Constructor2Id = c.ConstructorId
	INNER JOIN ConstructorResult r ON r.RaceId = s.SelectionForRaceId AND r.ConstructorId = c.ConstructorId
	GROUP BY s.PlayerId, r.RaceID
) a ON p.PlayerId = a.PlayerId AND r.RaceId = a.RaceId