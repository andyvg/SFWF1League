CREATE TABLE [dbo].[Question] (
    [QuestionId]   INT           IDENTITY (1, 1) NOT NULL,
    [RaceId]       INT           NOT NULL,
    [QuestionText] VARCHAR (100) NOT NULL,
    [Description]  VARCHAR (MAX) NULL,
    [Image]        VARCHAR (100) NULL,
    [SortOrder]    INT           NOT NULL,
    CONSTRAINT [PK_Question_Id] PRIMARY KEY CLUSTERED ([QuestionId] ASC)
);

