CREATE PROCEDURE [dbo].[GetQuestions] @RaceId int = null AS

IF(@RaceId IS NULL)
	SELECT @RaceId = CurrentRaceId FROM CurrentRace;

SELECT q.* FROM Question q WHERE q.RaceId = @RaceId ORDER BY SortOrder

SELECT a.* FROM Answer a INNER JOIN Question q ON a.QuestionId = q.QuestionId WHERE q.RaceId = @RaceId ORDER BY QuestionId, SortOrder