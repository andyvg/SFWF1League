
CREATE PROCEDURE GetConstructorById @ConstructorId int AS SELECT c.* FROM Constructor c WHERE c.Active = 1 AND c.ConstructorId = @ConstructorId