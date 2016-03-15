CREATE TABLE [dbo].[Answer] (
    [AnswerId]      INT          IDENTITY (1, 1) NOT NULL,
    [QuestionId]    INT          NOT NULL,
    [AnswerText]    VARCHAR (50) NOT NULL,
    [SortOrder]     INT          NOT NULL,
    [CorrectAnswer] BIT          NULL,
    CONSTRAINT [PK_Answer_Id] PRIMARY KEY CLUSTERED ([AnswerId] ASC),
    CONSTRAINT [FK_Answer_Question] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([QuestionId])
);

