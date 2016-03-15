﻿CREATE PROCEDURE GetRaceResult AS
SELECT RaceResultId, RaceId, DriverId, Position, Disqualified, Classified, PositionPoints, FastestLapPoints, TeammatePoints, TotalPoints FROM RaceResult r INNER JOIN CurrentRace c ON r.RaceId = c.CurrentRaceId ORDER BY Position;