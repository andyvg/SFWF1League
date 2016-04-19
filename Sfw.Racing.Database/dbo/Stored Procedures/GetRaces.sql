
CREATE PROCEDURE [dbo].[GetRaces] AS SELECT [RaceId]
      ,[Name]
      ,[Country]
      ,[RaceDate]
      ,[FinalEntry]
      ,[Image]
      ,[MaxChangesAllowed]
  FROM [dbo].[Race]
  ORDER BY RaceId