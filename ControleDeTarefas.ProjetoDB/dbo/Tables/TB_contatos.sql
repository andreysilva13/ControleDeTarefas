CREATE TABLE [dbo].[TB_contatos] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [EMAIL]    VARCHAR (100) NOT NULL,
    [TELEFONE] CHAR (11)     NOT NULL,
    [EMPRESA]  VARCHAR (100) NOT NULL,
    [CARGO]    VARCHAR (100) NOT NULL,
    [nome]     VARCHAR (150) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

