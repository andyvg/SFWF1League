CREATE TABLE [dbo].[Constructor] (
    [ConstructorId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50)  NOT NULL,
    [EngineId]      INT           NOT NULL,
    [Cost]          DECIMAL (18)  NOT NULL,
    [Active]        BIT           DEFAULT ((1)) NOT NULL,
    [Image]         VARCHAR (100) NULL,
    CONSTRAINT [PK_Constructor_Id] PRIMARY KEY CLUSTERED ([ConstructorId] ASC),
    CONSTRAINT [FK_Constructor_Engine] FOREIGN KEY ([EngineId]) REFERENCES [dbo].[Engine] ([EngineId])
);

