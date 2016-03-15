﻿
CREATE PROCEDURE GetDrivers AS SELECT d.*, c.Name [ConstructorName] FROM Driver d INNER JOIN Constructor c ON d.ConstructorId = c.ConstructorId WHERE d.Active = 1 ORDER BY d.Cost desc