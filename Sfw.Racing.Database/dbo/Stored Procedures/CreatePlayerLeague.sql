CREATE PROCEDURE CreatePlayerLeague @PlayerId int, @LeagueId int AS BEGIN 

INSERT INTO PlayerLeague(PlayerId, LeagueId) VALUES (@PlayerId, @LeagueId)

END