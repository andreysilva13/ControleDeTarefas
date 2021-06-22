using System;
using ControleDeTarefas.ConsoleApp.Dominio;
namespace ControleDeTarefas.ConsoleApp
{
    public class Tarefa : EntidadeBase
    {
        public string titulo { get; set; }
        public int prioridade { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataConclusao { get; set; }
        public int percentual { get; set; }

        public Tarefa(string titulo, int prioridade)
        {
            this.titulo = titulo;
            this.prioridade = prioridade;
        }
        public Tarefa(string titulo, int prioridade, DateTime dataCriacao, DateTime dataConclusao, int percentual)
        {
            this.titulo = titulo;
            this.prioridade = prioridade;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.percentual = percentual;
        }

        public string Validar()
        {
            string validacao = "";

            if (prioridade > 3)
                validacao = "invalido";

            if (string.IsNullOrEmpty(validacao))
                validacao = "valido";

            return validacao;
        }
    }
}
