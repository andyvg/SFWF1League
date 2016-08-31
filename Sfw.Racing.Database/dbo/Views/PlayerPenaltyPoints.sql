CREATE VIEW [dbo].[PlayerPenaltyPoints] AS
SELECT p.PlayerId, COALESCE(a.PenaltyPoints, 0) PenaltyPoints
FROM Player p
LEFT OUTER JOIN 
(
	SELECT s.PlayerId,
	SUM(s.PenaltyPoints) PenaltyPoints
	FROM Selection s
	GROUP BY s.PlayerId
) a ON p.PlayerId = a.PlayerId