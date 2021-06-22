using System;
using System.Collections.Generic;

namespace ControleDeTarefas.ConsoleApp
{
    class TelaTarefa
    {
        ControladorTarefa controlador = new ControladorTarefa();

        public string ObterOpcao()
        {
            Console.Clear();
            Console.WriteLine("DIGITE (1) PARA INSERIR NOVO TAREFA");
            Console.WriteLine("DIGITE (2) PARA VISUALIZAR TAREFAS");
            Console.WriteLine("DIGITE (3) PARA EDITAR UM TAREFA");
            Console.WriteLine("DIGITE (4) PARA EXCLUIR UM TAREFA");

            Console.WriteLine("DIGITE (S) PARA SAIR");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("Inserindo uma nova tarefa...");

            bool conseguiuGravar = GravaTarefa();

            if (conseguiuGravar)
            {
                Console.WriteLine("Tarefa inserida com sucesso");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Falha ao tentar inserir a tarefa");
                Console.ReadLine();
                Inserir();
            }
        }
        public void VisualizarRegistros()
        {
            List<Tarefa> t = controlador.Visualizar();
            if (t.Count == 0)
            {
                Console.WriteLine("NENHUMA TAREFA ADICIONADA");
            }
            else
            {
                ApresentarTabela(t);
            }
        }
        public void Visualizar()
        {
            List<Tarefa> t = controlador.Visualizar();
            if (t.Count > 1)
            {
                List<Tarefa> tarefasAbertas = new List<Tarefa>();
                List<Tarefa> tarefasFechadas = new List<Tarefa>();

                foreach (Tarefa item in t)
                {
                    if (item.percentual != 100)
                    {
                        tarefasAbertas.Add(item);
                    }
                    else
                    {
                        tarefasFechadas.Add(item);
                    }
                }
                string opcao = "";
                Console.Clear();
                Console.WriteLine("DIGITE 1 PARA VISUALIZAR TAREFAS ABERTAS");
                Console.WriteLine("DIGITE 2 PARA VISUALIZAR TAREFAS FECHADAS");
                opcao = Console.ReadLine();
                while (true)
                {
                    if (opcao == "1")
                    {
                        ApresentarTabela(tarefasAbertas);
                        Console.ReadLine();
                        break;
                    }
                    else if (opcao == "2")
                    {
                        ApresentarTabela(tarefasFechadas);
                        Console.ReadLine();
                        break;
                    }
                    else if (opcao != "1" || opcao != "2")
                        Visualizar();
                }
            }
            else
            {
                Console.WriteLine("NENHUMA TAREFA ADICIONADA");
                Console.ReadLine();
            }
        }
        public void ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine("Excluindo uma tarefa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da tarefa que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            controlador.Excluir(idSelecionado);
        }
        public void EditarOuFecharRegistro()
        {
            string opcao = "";
            Console.Clear();
            Console.WriteLine("Digite 1 para fechar uma tarefa");
            Console.WriteLine("Digite 2 para editar uma tarefa");
            opcao = Console.ReadLine();
            while (true)
            {
                if (opcao == "1")
                {
                    Fechar();
                    break;
                }
                else if (opcao == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Editando uma tarefa...");

                    VisualizarRegistros();

                    Console.Write("Digite o número da tarefa que deseja editar: ");
                    int idSelecionado = Convert.ToInt32(Console.ReadLine());

                    bool conseguiuGravar = GravaEdicao(idSelecionado);

                    if (conseguiuGravar)
                    {
                        Console.WriteLine("Tarefa editada com sucesso");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Falha ao tentar editar a tarefa");
                        Console.ReadLine();
                        break;
                    }   
                }
                else if (opcao != "1" || opcao != "2")
                    EditarOuFecharRegistro();
            }
        }

        #region [metodos privados]
        private bool GravaEdicao(int idSelecionado)
        {
            bool retorno;
            Tarefa item = controlador.SelecionarId(idSelecionado);

            Console.Write("Digite o titulo da tarefa: ");
            item.titulo = Console.ReadLine();

            Console.Write("Digite a prioridade da tarefa (1-BAIXA/2-MEDIA/3-ALTA): ");
            item.prioridade = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a percentual da tarefa: ");
            item.percentual = Convert.ToInt32(Console.ReadLine());

            if (item.percentual > 100)
            {   
                controlador.FecharTarefa(item);
                retorno = true;
            }
            else
            {
                Tarefa tarefa = new Tarefa(item.titulo, item.prioridade);
                tarefa.Validar();

                string EhValido = tarefa.Validar();

                if (EhValido == "valido")
                {
                    controlador.Editar(tarefa); 
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            }
            return retorno;
        }
        private bool GravaTarefa()
        {
            bool retorno;

            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a prioridade da tarefa (1-BAIXA/2-MEDIA/3-ALTA): ");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefa = new Tarefa(titulo, prioridade);

            string EhValido = tarefa.Validar();

            if (EhValido == "valido")
            {
                controlador.Inserir(tarefa);
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        }
        private void ApresentarTabela(List<Tarefa> registros)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "ID", "TITULO", "PRIORIDADE", "DATA DE CRIAÇÃO");

            foreach (Tarefa a in registros)
            {
                string prioridadeString = "";
                if(a.prioridade == 1)
                {
                    prioridadeString = "Baixa";
                }else if (a.prioridade == 2)
                {
                    prioridadeString = "Média";
                }else if (a.prioridade == 3)
                {
                    prioridadeString = "Alta";
                }
                Console.WriteLine(configuracaoColunasTabela, a.id, a.titulo, prioridadeString, a.dataCriacao);
            }
        }
        private void MontarCabecalhoTabela(string configuracaoColunasTabela, params object[] colunas)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(configuracaoColunasTabela, colunas);

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        private void Fechar()
        {
            Console.Clear();
            Console.WriteLine("Fechando uma tarefa...");

            VisualizarRegistros();

            Console.Write("Digite o número da tarefa que deseja fechar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());
            Tarefa item = controlador.SelecionarId(idSelecionado);
            controlador.FecharTarefa(item);
        }
        #endregion
    }
}
