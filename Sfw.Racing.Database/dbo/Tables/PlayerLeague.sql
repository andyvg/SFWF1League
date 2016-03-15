CREATE TABLE [dbo].[PlayerLeague] (
    [PlayerLeageId] INT IDENTITY (1, 1) NOT NULL,
    [PlayerId]      INT NOT NULL,
    [LeagueId]      INT NOT NULL,
    CONSTRAINT [PK_PlayerLeague_Id] PRIMARY KEY CLUSTERED ([PlayerLeageId] ASC),
    CONSTRAINT [FK_PlayerLeague_LEagueId] FOREIGN KEY ([LeagueId]) REFERENCES [dbo].[League] ([LeagueId]),
    CONSTRAINT [FK_PlayerLeague_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([PlayerId]),
    CONSTRAINT [UQ_PlayerLeague_PlayerLeague] UNIQUE NONCLUSTERED ([PlayerId] ASC, [LeagueId] ASC)
);

