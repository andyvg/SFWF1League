
CREATE PROCEDURE UpdateNextRace AS

	DECLARE @CurrentRaceId int;

	SELECT @CurrentRaceId = CurrentRaceId FROM CurrentRace

	INSERT INTO [dbo].[Selection]
           ([SelectionForRaceId]
           ,[PlayerId]
           ,[Driver1Id]
           ,[Driver2Id]
           ,[Driver3Id]
           ,[Driver4Id]
           ,[Constructor1Id]
           ,[Constructor2Id]
           ,[Engine1Id]
           ,[Engine2Id]
           ,[Answer1Id]
           ,[Answer2Id]
           ,[Answer3Id])
	SELECT  @CurrentRaceId + 1
           ,[PlayerId]
           ,[Driver1Id]
           ,[Driver2Id]
           ,[Driver3Id]
           ,[Driver4Id]
           ,[Constructor1Id]
           ,[Constructor2Id]
           ,[Engine1Id]
           ,[Engine2Id]
           ,null
           ,null
           ,null
	FROM
		Selection 
	WHERE
		[SelectionForRaceId] = @CurrentRaceId;
	
	UPDATE CurrentRace SET CurrentRaceId = @CurrentRaceId + 1;