CREATE TABLE [dbo].[Clientes] (
    [Id]             INT           IDENTITY(1, 1) NOT NULL,
    [NomeCompleto]   NVARCHAR (50) NOT NULL,
    [Cpf]            NVARCHAR (50) NOT NULL,
    [EnderecoId]     INT           NOT NULL,
    [RendaMensal]    FLOAT (53)    NOT NULL,
    [DataNascimento] DATE          NOT NULL,
    [DataCadastro]   DATE          NOT NULL,
    [VipId]          INT           NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Enderecos] FOREIGN KEY ([EnderecoId]) REFERENCES [dbo].[Enderecos] ([Id]),
    CONSTRAINT [FK_Vips] FOREIGN KEY ([VipId]) REFERENCES [dbo].[Vips] ([Id])
);

