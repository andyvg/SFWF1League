CREATE VIEW PlayerDriverPoints AS
SELECT p.PlayerId, COALESCE(a.DriverPoints, 0) DriverPoints
FROM Player p
LEFT OUTER JOIN 
(
	SELECT s.PlayerId,
	SUM(r.TotalPoints) DriverPoints
	FROM Driver d INNER JOIN Selection s ON s.Driver1Id = d.DriverId OR s.Driver2Id = d.DriverId OR s.Driver3Id = d.DriverId OR s.Driver4Id = d.DriverId
	INNER JOIN RaceResult r ON r.RaceId = s.SelectionForRaceId AND r.DriverId = d.DriverId 
	GROUP BY s.PlayerId
) a ON p.PlayerId = a.PlayerId