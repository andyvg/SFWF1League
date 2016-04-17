CREATE PROCEDURE [dbo].[UpdateRaceResults] AS

SET XACT_ABORT ON

BEGIN TRANSACTION

--reset all points to 0
UPDATE r SET r.PositionPoints = 0, r.TeammatePoints = 0 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId

DELETE r FROM ConstructorResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId

DELETE r FROM EngineResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId

--set driver finish position points
UPDATE r SET r.PositionPoints = p.RacePoints FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId INNER JOIN RacePoints p ON r.Position = p.Position

-- set teammate points
UPDATE r SET r.TeammatePoints = 5 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId
WHERE Position IN (
SELECT MIN(Position) FROM RaceResult r 
INNER JOIN CurrentRace cr ON r.RaceId = cr.CurrentRaceId 
INNER JOIN Driver d ON r.DriverId = d.DriverId 
INNER JOIN Constructor c ON d.ConstructorId = c.ConstructorId
GROUP BY c.ConstructorId);

-- driver race finish points
UPDATE r SET RaceFinishPoints = 5 FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId
WHERE r.Classified = 1;

--set constructor finish points
WITH ConstructorClassfiedFinish AS (
	SELECT c.ConstructorId, COUNT(*) [ClassifiedCount] FROM Constructor c
	INNER JOIN Driver d ON c.ConstructorId = d.ConstructorId
	LEFT OUTER JOIN RaceResult r ON r.DriverId = d.DriverId AND r.RaceId = (SELECT cr.CurrentRaceId FROM CurrentRace cr) 
	WHERE r.Classified = 1
	GROUP BY c.ConstructorId
)
INSERT INTO ConstructorResult(RaceId, ConstructorId, RaceFinishPoints, Top10Points) SELECT cr.CurrentRaceId, cf.ConstructorId, (CASE WHEN [ClassifiedCount] = 1 THEN 5 WHEN [ClassifiedCount] = 2 THEN 20 ELSE 0 END), 0
FROM ConstructorClassfiedFinish cf CROSS JOIN CurrentRace cr;

--set constructor top 10 points
WITH ConstructorTop10Finish AS (
	SELECT c.ConstructorId, COUNT(*) [Top10Finish] FROM CurrentRace cr
	INNER JOIN RaceResult r ON r.RaceId = cr.CurrentRaceId
	INNER JOIN Driver d ON r.DriverId = d.DriverId
	INNER JOIN Constructor c ON d.ConstructorId = c.ConstructorId
	WHERE r.Position <= 10
	GROUP BY c.ConstructorId
)
UPDATE res SET res.Top10Points = c.[Top10Finish] * 5 FROM ConstructorResult res
INNER JOIN ConstructorTop10Finish c ON res.ConstructorId = c.ConstructorId
INNER JOIN CurrentRace cr ON res.RaceId = res.RaceId;

--set Engine finish points
WITH EngineClassfiedFinish AS (
	SELECT e.EngineId, (CASE WHEN COUNT(*) > 2 THEN 2 ELSE COUNT(*) END) [ClassifiedCount] FROM Engine e
	INNER JOIN Constructor c ON c.EngineId = e.EngineId
	INNER JOIN Driver d ON c.ConstructorId = d.ConstructorId
	LEFT OUTER JOIN RaceResult r ON r.DriverId = d.DriverId AND r.RaceId = (SELECT cr.CurrentRaceId FROM CurrentRace cr) 
	WHERE r.Classified = 1
	GROUP BY e.EngineId
)
INSERT INTO EngineResult(RaceId, EngineId, RaceFinishPoints, Top10Points) SELECT cr.CurrentRaceId, cf.EngineId, (CASE WHEN [ClassifiedCount] = 1 THEN 10 WHEN [ClassifiedCount] = 2 THEN 20 ELSE 0 END), 0
FROM EngineClassfiedFinish cf CROSS JOIN CurrentRace cr;

--set Engine top 10 points
WITH EngineTop10Finish AS (
	SELECT e.EngineId, (CASE WHEN COUNT(*) > 2 THEN 2 ELSE COUNT(*) END) [Top10Finish] FROM CurrentRace cr
	INNER JOIN RaceResult r ON r.RaceId = cr.CurrentRaceId
	INNER JOIN Driver d ON r.DriverId = d.DriverId
	INNER JOIN Constructor c ON d.ConstructorId = c.ConstructorId
	INNER JOIN Engine e ON e.EngineId = c.EngineId
	WHERE r.Position <= 10
	GROUP BY e.EngineId
)
UPDATE res SET res.Top10Points = e.[Top10Finish] * 5 FROM EngineResult res
INNER JOIN EngineTop10Finish e ON res.EngineId = e.EngineId
INNER JOIN CurrentRace cr ON res.RaceId = res.RaceId;

COMMIT TRANSACTION