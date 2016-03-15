
CREATE PROCEDURE GetQuestions AS SELECT q.* FROM Question q INNER JOIN CurrentRace r ON q.RaceId = r.CurrentRaceId ORDER BY SortOrder

SELECT a.* FROM Answer a INNER JOIN Question q ON a.QuestionId = q.QuestionId INNER JOIN CurrentRace r ON q.RaceId = r.CurrentRaceId ORDER BY QuestionId, SortOrder