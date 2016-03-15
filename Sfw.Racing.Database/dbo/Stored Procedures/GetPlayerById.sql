CREATE PROCEDURE GetPlayerById @PlayerId int AS

SELECT p.*, [dbo].[GetCost](p.PlayerId) [BudgetSpent] FROM Player p WHERE p.PlayerId = @PlayerId;

SELECT l.*, pl.PlayerId FROM League l INNER JOIN PlayerLeague pl on l.LeagueId = pl.LeagueId WHERE pl.PlayerId = @PlayerId;