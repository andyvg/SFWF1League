CREATE TABLE [dbo].[Player] (
    [PlayerId]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [TeamName]  VARCHAR (100) NOT NULL,
    [Created]   DATETIME      NOT NULL,
    [Approved]  BIT           DEFAULT ((0)) NOT NULL,
    [MaxBudget] DECIMAL (18)  DEFAULT ((100)) NOT NULL,
    CONSTRAINT [PK_Player_Id] PRIMARY KEY CLUSTERED ([PlayerId] ASC)
);

