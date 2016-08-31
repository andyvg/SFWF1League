
CREATE PROCEDURE [dbo].[GetPenaltyPointsBySelectionId] @SelectionId int AS
SELECT s.SelectionId, s.PenaltyPoints Points FROM 
Selection s 
WHERE s.SelectionId = @SelectionId