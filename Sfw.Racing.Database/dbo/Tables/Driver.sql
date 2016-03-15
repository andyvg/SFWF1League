CREATE TABLE [dbo].[Driver] (
    [DriverId]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50)  NOT NULL,
    [ConstructorId] INT           NOT NULL,
    [Cost]          DECIMAL (18)  NOT NULL,
    [Active]        BIT           DEFAULT ((1)) NOT NULL,
    [Image]         VARCHAR (100) NULL,
    CONSTRAINT [PK_Driver_Id] PRIMARY KEY CLUSTERED ([DriverId] ASC),
    CONSTRAINT [FK_Drive_Constructor] FOREIGN KEY ([ConstructorId]) REFERENCES [dbo].[Constructor] ([ConstructorId])
);

