CREATE TABLE [dbo].[Engine] (
    [EngineId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50)  NOT NULL,
    [Cost]     DECIMAL (18)  NOT NULL,
    [Active]   BIT           DEFAULT ((1)) NOT NULL,
    [Image]    VARCHAR (100) NULL,
    CONSTRAINT [PK_Engine_Id] PRIMARY KEY CLUSTERED ([EngineId] ASC)
);

