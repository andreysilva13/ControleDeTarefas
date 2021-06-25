using System;
using System.Collections.Generic;
using ControleDeTarefas.ConsoleApp.Controlador;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    class TelaContato
    {
        ControladorContato controlador = new ControladorContato();

        public string ObterOpcao()
        {
            Console.Clear();
            Console.WriteLine("Digite 1 para inserir novo contato");
            Console.WriteLine("Digite 2 para visualizar contatos");
            Console.WriteLine("Digite 3 para editar um contato");
            Console.WriteLine("Digite 4 para excluir um contato");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("Inserindo um novo contato...");

            bool conseguiuGravar = GravaContato();

            if (conseguiuGravar)
            {
                Console.WriteLine("Contato inserido com sucesso");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Falha ao tentar inserir o contato");
                Console.ReadLine();
                Inserir();
            }
        }
        public void VisualizarRegistros()
        {
            List<Contato> c = controlador.Visualizar();
            if (c.Count == 0)
            {
                Console.WriteLine("NENHUM CONTATO ADICIONADO");
            }
            else
            {
                ApresentarTabela(c);
            }
        }
        public void ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine("Excluindo um contato...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do contato que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            controlador.Excluir(idSelecionado);
        }
        public void EditarRegistro()
        {
            Console.Clear();
            Console.WriteLine("Editando um contato...");

            VisualizarRegistros();

            Console.Write("Digite o número do contato que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravaEdicao(idSelecionado);

            if (conseguiuGravar)
            {
                Console.WriteLine("Contato editado com sucesso");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Falha ao tentar editar o contato");
                Console.ReadLine();
                EditarRegistro();
            }
        }

        #region [metodos privados]
        private bool GravaEdicao(int idSelecionado)
        {
            bool retorno;

            Contato item = controlador.SelecionarId(idSelecionado);

            Console.Write("Digite o nome do contato: ");
            item.nome = Console.ReadLine();

            Console.Write("Digite o email do contato: ");
            item.email = Console.ReadLine();

            Console.Write("Digite o telefone do contato: ");
            item.telefone = Console.ReadLine();

            Console.Write("Digite a empresa do contato: ");
            item.empresa = Console.ReadLine();

            Console.Write("Digite o cargo do contato: ");
            item.cargo = Console.ReadLine();

            Contato contato = new Contato(item.nome, item.email, item.telefone, item.empresa, item.cargo);
            string EhValido = contato.Validar();

            if (EhValido == "valido")
            {
                controlador.Editar(item);
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        }
        private bool GravaContato()
        {
            bool retorno;

            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email do contato: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone do contato: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o empresa do contato: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo do contato: ");
            string cargo = Console.ReadLine();

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);

            string EhValido = contato.Validar();

            if (EhValido == "valido")
            {
                controlador.Inserir(contato);
                retorno = true;
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }
        private void ApresentarTabela(List<Contato> registros)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "ID", "NOME", "CARGO");

            foreach (Contato a in registros)
            {
                Console.WriteLine(configuracaoColunasTabela, a.id, a.nome, a.cargo);
            }
        }
        private void MontarCabecalhoTabela(string configuracaoColunasTabela, params object[] colunas)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(configuracaoColunasTabela, colunas);

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        #endregion
    }
}
