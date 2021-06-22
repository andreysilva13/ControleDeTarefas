using ControleDeTarefas.ConsoleApp.Dominio;

namespace ControleDeTarefas.ConsoleApp
{
    public class Contato : EntidadeBase
    {
        public string email { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string empresa { get; set; }
        public string cargo  { get; set; }

        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        public string Validar()
        {
            string validacao = "";

            if (!email.Contains("@"))
                validacao =  "invalido";

            if (telefone.Length < 11)
                validacao = "invalido";

            if (string.IsNullOrEmpty(validacao))
                validacao = "valido";

            return validacao;
        }
    }
}
