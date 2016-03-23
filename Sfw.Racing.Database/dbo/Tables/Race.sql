CREATE TABLE [dbo].[Race] (
    [RaceId]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (60)  NOT NULL,
    [Country]           VARCHAR (60)  NOT NULL,
    [RaceDate]          DATE          NOT NULL,
    [FinalEntry]        DATETIME      NOT NULL,
    [Image]             VARCHAR (100) NULL,
    [MaxChangesAllowed] INT           DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_Race_Id] PRIMARY KEY CLUSTERED ([RaceId] ASC)
);



