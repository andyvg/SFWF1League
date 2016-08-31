
CREATE VIEW [dbo].[PlayerRacePenaltyPoints] AS
SELECT p.PlayerId, r.RaceId, COALESCE(a.PenaltyPoints, 0) PenaltyPoints
FROM Player p CROSS JOIN Race r
LEFT OUTER JOIN 
(
	SELECT s.PlayerId, s.SelectionForRaceId RaceId,
	SUM(s.PenaltyPoints) PenaltyPoints
	FROM Selection s
	GROUP BY s.PlayerId, s.SelectionForRaceId
) a ON p.PlayerId = a.PlayerId and r.RaceId = a.RaceId