CREATE TABLE [dbo].[ConstructorResult] (
    [ConstructorResultId] INT IDENTITY (1, 1) NOT NULL,
    [RaceId]              INT NOT NULL,
    [ConstructorId]       INT NOT NULL,
    [RaceFinishPoints]    INT NOT NULL,
    [Top10Points]         INT NOT NULL,
    [TotalPoints]         AS  ([RaceFinishPoints]+[Top10Points]),
    CONSTRAINT [PK_ConstructorResult_Id] PRIMARY KEY CLUSTERED ([ConstructorResultId] ASC),
    CONSTRAINT [FK_ConstructorResult_Constructor] FOREIGN KEY ([ConstructorId]) REFERENCES [dbo].[Constructor] ([ConstructorId]),
    CONSTRAINT [FK_ConstructorResult_Race] FOREIGN KEY ([RaceId]) REFERENCES [dbo].[Race] ([RaceId]),
    CONSTRAINT [UQ_ConstructorResult_Constructor] UNIQUE NONCLUSTERED ([RaceId] ASC, [ConstructorId] ASC)
);

