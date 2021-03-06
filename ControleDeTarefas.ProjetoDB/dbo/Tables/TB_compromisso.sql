CREATE TABLE [dbo].[TB_compromisso] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [ASSUNTO]     VARCHAR (150) NOT NULL,
    [LUGAR]       VARCHAR (150) NULL,
    [LINK]        VARCHAR (300) NULL,
    [DATAINICIO]  SMALLDATETIME NULL,
    [DATATERMINO] SMALLDATETIME NULL,
    [CONTATO_ID]  INT           NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CompromissoContato] FOREIGN KEY ([CONTATO_ID]) REFERENCES [dbo].[TB_contatos] ([ID])
);

