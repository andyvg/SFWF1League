
CREATE VIEW CurrentSelection AS SELECT * FROM Selection s INNER JOIN CurrentRace cr ON s.SelectionForRaceId = cr.CurrentRaceId