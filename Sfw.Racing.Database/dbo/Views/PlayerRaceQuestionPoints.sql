

CREATE VIEW [dbo].[PlayerRaceQuestionPoints] AS
SELECT p.PlayerId, r.RaceId, COALESCE(a.QuestionPoints, 0) QuestionPoints
FROM Player p CROSS JOIN Race r
LEFT OUTER JOIN
(
	SELECT s.PlayerId, s.SelectionForRaceId [RaceId], SUM(q.Points) QuestionPoints
	FROM Selection s
	INNER JOIN Answer a ON (s.Answer1Id = a.AnswerId OR s.Answer2Id = a.AnswerId OR s.Answer3Id = a.AnswerId) AND a.CorrectAnswer = 1
	INNER JOIN Question q ON a.QuestionId = q.QuestionId
	GROUP BY s.PlayerId, s.SelectionForRaceId
) a ON p.PlayerId = a.PlayerId AND r.RaceId = a.RaceId