CREATE TABLE [dbo].[Vips] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Nome]      NVARCHAR (50)  NOT NULL,
    [Descricao] NVARCHAR (255) NOT NULL,
    [Valor]     FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_Vips] PRIMARY KEY CLUSTERED ([Id] ASC)
);

