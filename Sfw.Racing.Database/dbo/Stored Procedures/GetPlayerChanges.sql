
CREATE PROCEDURE GetPlayerChanges @PlayerId int AS

	DECLARE @NumberChanges int = 0;

	DECLARE @CurrentRaceId int;

	SELECT @CurrentRaceId = CurrentRaceId FROM CurrentRace

	IF(@CurrentRaceId = 1)
		SET @NumberChanges = 0; --unlimited changes for race 1
	ELSE
	BEGIN
		DECLARE @PreviousRaceId int;

		SELECT @PreviousRaceId = @CurrentRaceId - 1;

		WITH CurrentSelection AS (
		SELECT 'D'+CAST(Driver1Id as varchar(2)) [ComponentId] From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId
		UNION
		SELECT 'D'+CAST(Driver2Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId
		UNION
		SELECT 'D'+CAST(Driver3Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId
		UNION
		SELECT 'D'+CAST(Driver4Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId
		UNION
		SELECT 'C'+CAST(Constructor1Id as varchar(2)) [ComponentId] From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId
		UNION
		SELECT 'C'+CAST(Constructor2Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId
		UNION
		SELECT 'E'+CAST(Engine1Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId
		UNION
		SELECT 'E'+CAST(Engine2Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @CurrentRaceId),
		PastSelection AS (
		SELECT 'D'+CAST(Driver1Id as varchar(2)) [ComponentId]  From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		UNION
		SELECT 'D'+CAST(Driver2Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		UNION
		SELECT 'D'+CAST(Driver3Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		UNION
		SELECT 'D'+CAST(Driver4Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		UNION
		SELECT 'C'+CAST(Constructor1Id as varchar(2)) [ComponentId] From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		UNION
		SELECT 'C'+CAST(Constructor2Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		UNION
		SELECT 'E'+CAST(Engine1Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		UNION
		SELECT 'E'+CAST(Engine2Id as varchar(2)) From Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId
		)
		SELECT * FROM CurrentSelection
		EXCEPT 
		SELECT * FROM PastSelection

	END