﻿CREATE TABLE [dbo].[League] (
    [LeagueId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_League_Id] PRIMARY KEY CLUSTERED ([LeagueId] ASC)
);

