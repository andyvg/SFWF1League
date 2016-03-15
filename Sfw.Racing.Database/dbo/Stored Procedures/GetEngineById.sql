
CREATE PROCEDURE GetEngineById @EngineId int AS SELECT e.* FROM Engine e WHERE e.Active = 1 AND e.EngineId = @EngineId