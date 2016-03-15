
CREATE PROCEDURE [dbo].[UpdatePlayerSelection] @PlayerId int, @Driver1Id int null, @Driver2Id int null, @Driver3Id int null, @Driver4Id int null, @Constructor1Id int null, @Constructor2Id int null, @Engine1Id int null, @Engine2Id int null, @Answer1Id int null, @Answer2Id int null, @Answer3Id int null AS
BEGIN
	
	SET XACT_ABORT ON

	BEGIN TRANSACTION

	DECLARE @RaceId int;

	SELECT @RaceId = c.CurrentRaceId FROM CurrentRace c; 

	UPDATE Selection SET Driver1Id = @Driver1Id, Driver2Id = @Driver2Id, Driver3Id = @Driver3Id, Driver4Id = @Driver4Id, Constructor1Id = @Constructor1Id, Constructor2Id = @Constructor2Id, Engine1Id = @Engine1Id, Engine2Id = @Engine2Id, Answer1Id = @Answer1Id, Answer2Id = @Answer2Id, Answer3Id = @Answer3Id WHERE PlayerId = @PlayerId AND SelectionForRaceId = @RaceId

	DECLARE @MaxBudget decimal;

	SELECT @MaxBudget = p.MaxBudget FROM Player p WHERE p.PlayerId = @PlayerId;

	DECLARE @CurrentCost decimal;

	SELECT @CurrentCost = dbo.GetCost(@PlayerId)

	IF (@CurrentCost > @MaxBudget)
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('BUDGET_ERROR: Cost exceeds player budget',16,1)
	END

	COMMIT TRANSACTION
END