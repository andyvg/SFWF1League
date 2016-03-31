
CREATE PROCEDURE [dbo].[GetDrivers] AS SELECT d.*, c.Name [ConstructorName] FROM Driver d INNER JOIN Constructor c ON d.ConstructorId = c.ConstructorId ORDER BY d.Cost desc