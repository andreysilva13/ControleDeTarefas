using System.Collections.Generic;
using System.Data.SqlClient;
using ControleDeTarefas.ConsoleApp.Dominio;

namespace ControleDeTarefas.ConsoleApp.Controlador
{
    public abstract class ControladorGeneric<T> where T : EntidadeBase
    {
        public ControladorGeneric()
        {
        }

        public void Inserir(T registro)
        {
            ObterInsert(registro);
        }

        public void Editar(T registro)
        {
            ObterEdicao(registro);
        }

        public void Excluir(int id)
        {
            object tipo = id.GetType();
            ObterExclusao(tipo, id);
        }

        public List<T> Visualizar()
        {
            List<T> lista = ObterRegistros();
            return lista;
        }

        public T SelecionarId(int id)
        {
            T item = ObterIdSelecionado(id);
            return item;
        }

        public abstract void ObterInsert(T registro);
        public abstract void ObterEdicao(T registro);
        public abstract void ObterExclusao(object tipo, int id);
        public abstract List<T> ObterRegistros();
        public abstract T ObterIdSelecionado(int id);
        public void ResetaDadosEIdDB()
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();
            SqlCommand comandoResetar = new SqlCommand();
            comandoResetar.Connection = conexaoComBanco;

            string sqlResetaID = @"DELETE FROM TB_tarefas; 
                                   DBCC CHECKIDENT('TB+tarefas', RESEED, 0)
                                   DELETE FROM TB_compromisso; 
                                   DBCC CHECKIDENT('TB_compromisso', RESEED, 0);
                                   DELETE FROM TB_contatos; 
                                   DBCC CHECKIDENT('TB_contatos', RESEED, 0)";

            comandoResetar.CommandText = sqlResetaID;
            comandoResetar.ExecuteScalar();
        }

        private static SqlConnection AbreConexaoComOBanco()
        {
            string enderecoDBEmpresa =
                            @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=db_ControleDeTarefa;Integrated Security=True;Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBEmpresa;
            conexaoComBanco.Open();
            return conexaoComBanco;
        }
    }

    
}
