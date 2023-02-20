CREATE TABLE [dbo].[Fixtures] (
    [Id]    INT           NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [Count] INT           NOT NULL,
    CONSTRAINT [PK_Fixtures] PRIMARY KEY CLUSTERED ([Id] ASC)
);
