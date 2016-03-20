CREATE VIEW PlayerQuestionPoints AS
SELECT p.PlayerId, COALESCE(a.QuestionPoints, 0) QuestionPoints
FROM Player p
LEFT OUTER JOIN
(
	SELECT s.PlayerId, SUM(q.Points) QuestionPoints
	FROM Selection s
	INNER JOIN Answer a ON (s.Answer1Id = a.AnswerId OR s.Answer2Id = a.AnswerId OR s.Answer3Id = a.AnswerId) AND a.CorrectAnswer = 1
	INNER JOIN Question q ON a.QuestionId = q.QuestionId
	GROUP BY s.PlayerId
) a ON p.PlayerId = a.PlayerId