﻿
CREATE PROCEDURE GetEngines AS SELECT * FROM Engine WHERE Active = 1 ORDER BY Cost desc