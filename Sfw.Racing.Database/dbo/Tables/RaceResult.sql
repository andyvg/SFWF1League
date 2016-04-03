CREATE TABLE [dbo].[RaceResult] (
    [RaceResultId]     INT IDENTITY (1, 1) NOT NULL,
    [RaceId]           INT NOT NULL,
    [DriverId]         INT NOT NULL,
    [Position]         INT NOT NULL,
    [Disqualified]     BIT NOT NULL,
    [Classified]       BIT NOT NULL,
    [PositionPoints]   INT DEFAULT ((0)) NOT NULL,
    [FastestLapPoints] INT DEFAULT ((0)) NOT NULL,
    [TeammatePoints]   INT DEFAULT ((0)) NOT NULL,
    [RaceFinishPoints] INT DEFAULT ((0)) NOT NULL,
    [TotalPoints]      AS  ((([PositionPoints]+[RaceFinishPoints])+[FastestLapPoints])+[TeammatePoints]),
    CONSTRAINT [PK_RaceResult_Id] PRIMARY KEY CLUSTERED ([RaceResultId] ASC),
    CONSTRAINT [FK_RaceResult_Driver] FOREIGN KEY ([DriverId]) REFERENCES [dbo].[Driver] ([DriverId]),
    CONSTRAINT [FK_RaceResult_Race] FOREIGN KEY ([RaceId]) REFERENCES [dbo].[Race] ([RaceId]),
    CONSTRAINT [UQ_RaceResult_Driver] UNIQUE NONCLUSTERED ([RaceId] ASC, [DriverId] ASC),
    CONSTRAINT [UQ_RaceResult_Position] UNIQUE NONCLUSTERED ([RaceId] ASC, [Position] ASC)
);



