CREATE TABLE [dbo].[Enderecos] (
    [ClienteId]   INT           NOT NULL,
    [Cep]         NVARCHAR (50) NOT NULL,
    [Logradouro]  NVARCHAR (50) NOT NULL,
    [Bairro]      NVARCHAR (50) NOT NULL,
    [CIdade]      NVARCHAR (50) NOT NULL,
    [Uf]          NCHAR (10)    NOT NULL,
    [Complemento] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED ([ClienteId] ASC),
    CONSTRAINT [FK_Clientes] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Clientes] ([Id])
);