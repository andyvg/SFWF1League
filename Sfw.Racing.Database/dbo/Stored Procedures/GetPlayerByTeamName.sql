CREATE PROCEDURE GetPlayerByTeamName @TeamName varchar(100) AS
SELECT p.*, [dbo].[GetCost](p.PlayerId) [BudgetSpent] FROM Player p WHERE p.TeamName = @TeamName