
CREATE FUNCTION GetPlayerChanges (@PlayerId int) RETURNS int 
	AS
	BEGIN

		DECLARE @NumberChanges int = 0;

		DECLARE @CurrentRaceId int;

		SELECT @CurrentRaceId = CurrentRaceId FROM CurrentRace

		IF(@CurrentRaceId = 1)
			SET @NumberChanges = 0; --unlimited changes for race 1
		ELSE
		BEGIN
			DECLARE @PreviousRaceId int;

			SELECT @PreviousRaceId = @CurrentRaceId - 1;

			DECLARE @HasPrevChanges int; 

			SELECT @HasPrevChanges = COALESCE(Driver1Id + Driver2Id + Driver3Id + Driver4Id + Constructor1Id + Constructor2Id + Engine1Id + Engine2Id,0) FROM Selection WHERE PlayerId = @PlayerId AND SelectionForRaceId = @PreviousRaceId;
			
			--IF @HasPrevChanges ==0, then this player didn't have a previous selection
			IF(@HasPrevChanges = 0)
			BEGIN
				RETURN 0;

			END
			ELSE
			BEGIN

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
				SELECT @NumberChanges = COUNT(*) FROM (
					SELECT * FROM CurrentSelection
					EXCEPT 
					SELECT * FROM PastSelection
				) a;

			END

		END

		RETURN @NumberChanges;

	END