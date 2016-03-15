CREATE TABLE [dbo].[EngineResult] (
    [EngineResultId]   INT IDENTITY (1, 1) NOT NULL,
    [RaceId]           INT NOT NULL,
    [EngineId]         INT NOT NULL,
    [RaceFinishPoints] INT NOT NULL,
    [Top10Points]      INT NOT NULL,
    [TotalPoints]      AS  ([RaceFinishPoints]+[Top10Points]),
    CONSTRAINT [PK_EngineResult_Id] PRIMARY KEY CLUSTERED ([EngineResultId] ASC),
    CONSTRAINT [FK_EngineResult_Engine] FOREIGN KEY ([EngineId]) REFERENCES [dbo].[Engine] ([EngineId]),
    CONSTRAINT [FK_EngineResult_Race] FOREIGN KEY ([RaceId]) REFERENCES [dbo].[Race] ([RaceId]),
    CONSTRAINT [UQ_EngineResult_Engine] UNIQUE NONCLUSTERED ([RaceId] ASC, [EngineId] ASC)
);

