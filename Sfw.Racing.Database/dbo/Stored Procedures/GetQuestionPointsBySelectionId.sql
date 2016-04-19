CREATE PROCEDURE GetQuestionPointsBySelectionId @SelectionId int AS
SELECT s.SelectionId, q.QuestionId, (CASE WHEN a.CorrectAnswer = 1 THEN q.Points ELSE 0 END) Points FROM Question q
INNER JOIN Selection s ON s.SelectionForRaceId = q.RaceId
INNER JOIN Answer a ON a.QuestionId = q.QuestionId
AND (a.AnswerId = s.Answer1Id or a.AnswerId = s.Answer2Id or a.AnswerId = s.Answer3Id)
WHERE s.SelectionId = @SelectionId