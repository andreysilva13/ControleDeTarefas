using System;
using System.Collections.Generic;
using ControleDeTarefas.ConsoleApp.Controlador;
using ControleDeTarefas.ConsoleApp.Dominio;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaCompromisso
    {
        ControladorCompromisso controlador = new ControladorCompromisso();
        public string ObterOpcao()
        {
            Console.Clear();
            Console.WriteLine("Digite 1 para inserir novo compromisso");
            Console.WriteLine("Digite 2 para visualizar compromissos");
            Console.WriteLine("Digite 3 para editar um compromisso");
            Console.WriteLine("Digite 4 para excluir um compromisso");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("Inserindo um novo compromisso...");

            bool conseguiuGravar = GravaCompromisso();

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
            List<Compromissos> c = controlador.Visualizar();
            if (c.Count == 0)
            {
                Console.WriteLine("NENHUM COMPROMISSO ADICIONADO");
            }
            else
            {
                ApresentarTabela(c);
            }
        }
        public void ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine("Excluindo um compromisso...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do compromisso que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            controlador.Excluir(idSelecionado);
        }
        public void EditarRegistro()
        {
            Console.Clear();
            Console.WriteLine("Editando um compromisso...");

            VisualizarRegistros();

            Console.Write("Digite o número do compromisso que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravaEdicao(idSelecionado);

            if (conseguiuGravar)
            {
                Console.WriteLine("Compromisso editado com sucesso");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Falha ao tentar editar o compromisso");
                Console.ReadLine();
                EditarRegistro();
            }
        }


        #region [metodos privados]
        private bool GravaEdicao(int idSelecionado)
        {
            bool retorno;

            Compromissos item = controlador.SelecionarId(idSelecionado);

            Console.Write("Digite o assunto do compromisso: ");
            item.assunto = Console.ReadLine();

            Console.Write("Digite o email do compromisso: ");
            item.local = Console.ReadLine();

            Console.Write("Digite o link do compromisso: ");
            item.link = Console.ReadLine();

            Console.Write("Digite a data de inicio do compromisso (EX: yyyy,mm,dd,hh,mm,ss): ");
            item.dataInicio = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a data de termino do compromisso (EX: yyyy,mm,dd,hh,mm,ss): ");
            item.dataTermino = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o ID do contato: ");
            item.idContato = Convert.ToInt32(Console.ReadLine());

            Compromissos compromissos = new Compromissos(item.assunto, item.local, item.link, item.dataInicio, item.dataTermino, item.idContato);

            string EhValido = "valido";

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
        private bool GravaCompromisso()
        {
            bool retorno;

            Console.Write("Digite o assunto do compromisso: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite o email do compromisso: ");
            string local = Console.ReadLine();

            Console.Write("Digite o link do compromisso: ");
            string link = Console.ReadLine();

            Console.Write("Digite a data de inicio do compromisso (EX: yyyy/mm/dd hh:mm:ss): ");
            DateTime dataInicio = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a data de termino do compromisso (EX: yyyy/mm/dd hh:mm:ss): ");
            DateTime dataTermino = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o ID do contato: ");
            int idContato = Convert.ToInt32(Console.ReadLine());

            Compromissos compromissos = new Compromissos(assunto, local, link, dataInicio, dataTermino, idContato);

            string EhValido = "valido";

            if (EhValido == "valido")
            {
                controlador.Inserir(compromissos);
                retorno = true;
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }

        private void ApresentarTabela(List<Compromissos> registros)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-20} | {2,-20} | {3,-20} | {4,-20} | {5,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "ID", "ASSUNTO", "LOCAL", "DATA INICIO", "DATA TERMINO", "NOME");

            foreach (Compromissos a in registros)
            {
                Console.WriteLine(configuracaoColunasTabela, a.id, a.assunto, a.local, a.dataInicio, a.dataTermino, a.nome);
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
