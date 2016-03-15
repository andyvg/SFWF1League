CREATE PROCEDURE CreateRaceResult @DriverId int, @Position int, @Disqualified bit, @Classified bit AS

INSERT INTO RaceResult(RaceId, DriverId, Position, Disqualified, Classified) SELECT CurrentRaceId, @DriverId, @Position, @Disqualified, @Classified FROM CurrentRace