CREATE TABLE [dbo].[Clientes] (
    [Id]             INT           NOT NULL,
    [NomeCompleto]   NVARCHAR (50) NOT NULL,
    [Cpf]            NVARCHAR (50) NOT NULL,
    [RendaMensal]    FLOAT (53)    NOT NULL,
    [DataNascimento] DATE          NOT NULL,
    [DataCadastro]   DATE          NOT NULL,
    [Vip]            BIT           NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([Id] ASC)
);