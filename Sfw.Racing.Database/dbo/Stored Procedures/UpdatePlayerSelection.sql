
CREATE PROCEDURE [dbo].[UpdatePlayerSelection] @PlayerId int, @Driver1Id int null, @Driver2Id int null, @Driver3Id int null, @Driver4Id int null, @Constructor1Id int null, @Constructor2Id int null, @Engine1Id int null, @Engine2Id int null, @Answer1Id int null, @Answer2Id int null, @Answer3Id int null AS
BEGIN
	
	SET XACT_ABORT ON

	BEGIN TRANSACTION

		DECLARE @RaceId int;

		SELECT @RaceId = c.CurrentRaceId FROM CurrentRace c; 

		DECLARE @MaxChangesAllowed int;

		SELECT @MaxChangesAllowed = MaxChangesAllowed FROM Race WHERE RaceId = @RaceId;

		UPDATE Selection SET Driver1Id = @Driver1Id, Driver2Id = @Driver2Id, Driver3Id = @Driver3Id, Driver4Id = @Driver4Id, Constructor1Id = @Constructor1Id, Constructor2Id = @Constructor2Id, Engine1Id = @Engine1Id, Engine2Id = @Engine2Id, Answer1Id = @Answer1Id, Answer2Id = @Answer2Id, Answer3Id = @Answer3Id, LastUpdated = GETDATE() WHERE PlayerId = @PlayerId AND SelectionForRaceId = @RaceId

		DECLARE @ChangesMade int;

		EXEC @ChangesMade = GetPlayerChanges @PlayerId;

		SELECT @ChangesMade, @MaxChangesAllowed;

		IF(@ChangesMade <= @MaxChangesAllowed)
		BEGIN

			DECLARE @MaxBudget decimal;

			SELECT @MaxBudget = p.MaxBudget FROM Player p WHERE p.PlayerId = @PlayerId;

			DECLARE @CurrentCost decimal;

			DECLARE @SelectionId int;

			SELECT @SelectionId = SelectionId FROM Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @RaceId

			SELECT @CurrentCost = dbo.GetCost(@SelectionId)

			SELECT @CurrentCost, @MaxBudget

			IF (@CurrentCost > @MaxBudget)
			BEGIN
				ROLLBACK TRANSACTION
				RAISERROR('BUDGET_ERROR: Cost exceeds player budget',16,1)
			END
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION
			RAISERROR('CHANGES_ERROR: Too many changes made',16,1)
		END

	COMMIT TRANSACTION
END