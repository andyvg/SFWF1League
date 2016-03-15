CREATE PROCEDURE GetPlayers AS
SELECT p.*, [dbo].[GetCost](p.PlayerId) [BudgetSpent] FROM Player p