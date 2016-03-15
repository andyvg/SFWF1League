
CREATE FUNCTION GetCost(@PlayerId int) RETURNS decimal
AS
BEGIN
	DECLARE @DriverCost decimal;
	DECLARE @EngineCost decimal;
	DECLARE @ConstructorCost decimal;

	-- Get Driver Cost
	SELECT @DriverCost = SUM(d.Cost) FROM Driver d INNER JOIN CurrentSelection s ON s.Driver1Id = d.DriverId OR s.Driver2Id = d.DriverId OR s.Driver3Id = d.DriverId OR s.Driver4Id = d.DriverId WHERE s.PlayerId = @PlayerId

	SELECT @EngineCost = SUM(e.Cost) FROM Engine e INNER JOIN CurrentSelection s ON s.Engine1Id = e.EngineId OR s.Engine2Id = e.EngineId WHERE s.PlayerId = @PlayerId

	SELECT @ConstructorCost = SUM(c.Cost) FROM Constructor c INNER JOIN CurrentSelection s ON s.Constructor1Id = c.ConstructorId OR s.Constructor2Id = c.ConstructorId WHERE s.PlayerId = @PlayerId

	RETURN @DriverCost + @EngineCost + @ConstructorCost;
END