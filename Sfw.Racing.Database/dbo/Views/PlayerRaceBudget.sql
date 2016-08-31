CREATE VIEW PlayerRaceBudget AS

SELECT p.PlayerId,p.Name,p.TeamName, s.SelectionForRaceId, s.BudgetSpent [OldBudgetSpent], s.LastUpdated, dbo.GetCost(s.SelectionId) [CurrentBudgetSpent], (CASE WHEN s.BudgetSpent IS NOT NULL THEN 0  WHEN dbo.GetCost(s.SelectionId) > p.MaxBudget THEN 1 ELSE 0 END) OverBudget from Player p INNER JOIN Selection s ON p.PlayerId = s.PlayerId