CREATE TABLE [dbo].[CurrentRace] (
    [CurrentRaceId] INT NOT NULL,
    CONSTRAINT [FK_CurrentRace_Race] FOREIGN KEY ([CurrentRaceId]) REFERENCES [dbo].[Race] ([RaceId])
);

