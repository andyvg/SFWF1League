
CREATE PROCEDURE CreatePlayer @Name varchar(100), @TeamName varchar(100), @NewId int OUTPUT AS BEGIN 

INSERT INTO Player (Name, TeamName, Created, MaxBudget) VALUES (@Name, @TeamName, GETDATE(), 100);

SET @NewId = SCOPE_IDENTITY()

DECLARE @RaceId int;

SELECT @RaceId = CurrentRaceId FROM CurrentRace;

INSERT INTO Selection (PlayerId, SelectionForRaceId) VALUES (@NewId,@RaceId);

END