CREATE TABLE [dbo].[TB_tarefas] (
    [ID]                    INT           IDENTITY (1, 1) NOT NULL,
    [TITULO]                VARCHAR (200) NULL,
    [PRIORIDADE]            INT           NULL,
    [DATADECRIACAO]         DATE          NULL,
    [DATACONCLUSAO]         DATE          NULL,
    [PERCENTUALDECONCLUSAO] INT           NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

