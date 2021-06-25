using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Dominio
{
    public class Compromissos : EntidadeBase
    {   

        public string assunto { get; set; }
        public string nome { get; set; }
        public string local { get; set; }
        public string link { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataTermino { get; set; }      
        public int idContato { get; set; }

        public Compromissos(string assunto, string local, string link, DateTime dataInicio, DateTime dataTermino, int idContato)
        {
            this.assunto = assunto;
            this.local = local;
            this.link = link;
            this.dataInicio = dataInicio;
            this.dataTermino = dataTermino;
            this.idContato = idContato;
        }
        public Compromissos(string assunto, string local, string link, DateTime dataInicio, DateTime dataTermino, string nome)
        {
            this.assunto = assunto;
            this.local = local;
            this.link = link;
            this.dataInicio = dataInicio;
            this.dataTermino = dataTermino;
            this.nome = nome;
        }
    }
}
