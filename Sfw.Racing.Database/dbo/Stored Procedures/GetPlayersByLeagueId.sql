CREATE PROCEDURE GetPlayersByLeagueId @LeagueId int AS

SELECT p.*, [dbo].[GetCost](p.PlayerId) [BudgetSpent] FROM Player p INNER JOIN PlayerLeague pl ON p.PlayerId = pl.PlayerId WHERE pl.LeagueId = @LeagueId;