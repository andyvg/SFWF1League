CREATE VIEW PlayerConstructorPoints AS
SELECT p.PlayerId, COALESCE(a.ConstructorPoints, 0) ConstructorPoints
FROM Player p
LEFT OUTER JOIN
(
	SELECT s.PlayerId, SUM(r.TotalPoints) ConstructorPoints 
	FROM Constructor c 
	INNER JOIN Selection s ON s.Constructor1Id = c.ConstructorId OR s.Constructor2Id = c.ConstructorId
	INNER JOIN ConstructorResult r ON r.RaceId = s.SelectionForRaceId AND r.ConstructorId = c.ConstructorId
	GROUP BY s.PlayerId
) a ON p.PlayerId = a.PlayerId