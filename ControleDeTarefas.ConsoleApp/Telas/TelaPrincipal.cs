using System;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    class TelaPrincipal
    {
        TelaTarefa telaTarefa = new TelaTarefa();
        TelaContato telaContato = new TelaContato();

        public void ObterTelaPrincipal()
        {
            while (true)
            {
                string opcao = ObterOpcao();

                if (opcao == "1")
                {
                    while (true)
                    {
                        string opcaoTarefa = "";
                        opcaoTarefa = telaTarefa.ObterOpcao();
                        if (opcaoTarefa == "1")
                            telaTarefa.Inserir();
                        else if (opcaoTarefa == "2")
                            telaTarefa.Visualizar();
                        else if (opcaoTarefa == "3")
                            telaTarefa.EditarOuFecharRegistro();
                        else if (opcaoTarefa == "4")
                            telaTarefa.ExcluirRegistro();
                        else if (opcaoTarefa.Equals("s", StringComparison.OrdinalIgnoreCase))
                            break;
                        else if (opcaoTarefa != "1" || opcaoTarefa != "2" || opcaoTarefa != "3" || opcaoTarefa != "4")
                            telaTarefa.ObterOpcao();
                    }
                }
                else if (opcao == "2")
                {
                    while (true)
                    {
                        string opcaoContatos = "";
                        opcaoContatos = telaContato.ObterOpcao();
                        if (opcaoContatos == "1")
                            telaContato.Inserir();
                        else if (opcaoContatos == "2")
                        {
                            Console.Clear();
                            telaContato.VisualizarRegistros();
                            Console.ReadLine();
                        }
                        else if (opcaoContatos == "3")
                            telaContato.EditarRegistro();
                        else if (opcaoContatos == "4")
                            telaContato.ExcluirRegistro();
                        else if (opcaoContatos.Equals("s", StringComparison.OrdinalIgnoreCase))
                            break;
                        else if (opcaoContatos != "1" || opcaoContatos != "2" || opcaoContatos != "3" || opcaoContatos != "4")
                            telaContato.ObterOpcao();
                    }
                }
                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (opcao != "1" || opcao != "2" || opcao != "3" || opcao != "4")
                    ObterOpcao();
                
               
            }
        }
        private static string ObterOpcao()
        {
            string opcao = "";
            Console.Clear();
            Console.WriteLine("DIGITE (1) PARA ENTRAR EM TAREFAS");
            Console.WriteLine("DIGITE (2) PARA ENTRAR EM CONTATOS");
            opcao = Console.ReadLine();

            return opcao;
        }
    }
}

