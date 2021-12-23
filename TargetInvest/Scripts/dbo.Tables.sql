CREATE TABLE [dbo].[Clientes] (
    [Id]             INT           NOT NULL,
    [NomeCompleto]   NVARCHAR (50) NOT NULL,
    [Cpf]            NVARCHAR (50) NOT NULL,
    [RendaMensal]    FLOAT (53)    NOT NULL,
    [DataNascimento] DATE          NOT NULL,
    [DataCadastro]   DATE          NOT NULL,
    [Vip]            BIT           NOT NULL,
    [EnderecoId]     INT           NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Enderecos] FOREIGN KEY ([EnderecoId]) REFERENCES [Enderecos]([ClienteId])
)

GO
CREATE TABLE [dbo].[Enderecos] (
    [ClienteId]   INT           NOT NULL,
    [Cep]         NVARCHAR (50) NOT NULL,
    [Logradouro]  NVARCHAR (50) NOT NULL,
    [Bairro]      NVARCHAR (50) NOT NULL,
    [CIdade]      NVARCHAR (50) NOT NULL,
    [Uf]          NCHAR (10)    NOT NULL,
    [Complemento] NVARCHAR (50) NOT NULL, 
	CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED ([ClienteId] ASC),
    CONSTRAINT [FK_Clientes] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes]([Id])
);

GO

