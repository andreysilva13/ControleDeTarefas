using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ControleDeTarefas.ConsoleApp.Controlador;

namespace ControleDeTarefas.ConsoleApp
{
    public class ControladorTarefa : ControladorGeneric<Tarefa>
    {
        public override void ObterInsert(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @"INSERT INTO TB_tarefas
	                (
		                [TITULO], 
		                [PRIORIDADE], 
		                [DATADECRIACAO], 
		                [PERCENTUALDECONCLUSAO]
	                ) 
	                VALUES
	                (
                        @TITULO, 
		                @PRIORIDADE, 
		                @DATADECRIACAO,  
		                @PERCENTUALDECONCLUSAO
	                );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoInsercao.Parameters.AddWithValue("DATADECRIACAO", DateTime.Now);
            comandoInsercao.Parameters.AddWithValue("PERCENTUALDECONCLUSAO", 0);

            object id = comandoInsercao.ExecuteScalar();

            tarefa.id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }
        public override void ObterEdicao(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoEditar = new SqlCommand();
            comandoEditar.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TB_tarefas
                 SET   
                  [TITULO] = @TITULO,
                  [PRIORIDADE] = @PRIORIDADE,
                  [PERCENTUALDECONCLUSAO] = @PERCENTUALDECONCLUSAO           
                    WHERE 
                  [ID] = @ID";

            comandoEditar.CommandText = sqlAtualizacao;
            comandoEditar.Parameters.AddWithValue("ID", tarefa.id);
            comandoEditar.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoEditar.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoEditar.Parameters.AddWithValue("PERCENTUALDECONCLUSAO", tarefa.percentual);

            comandoEditar.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override void ObterExclusao(Object tarefa, int id)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TB_tarefas	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override List<Tarefa> ObterRegistros()
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [TITULO], 
                        [PRIORIDADE], 
                        [DATADECRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUALDECONCLUSAO] 
                    FROM 
                        TB_tarefas ORDER BY prioridade DESC ";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefa.Read())
            {
                DateTime dataConclusao = new DateTime(0001 / 01 / 01);

                int id = Convert.ToInt32(leitorTarefa["ID"]);
                string titulo = Convert.ToString(leitorTarefa["TITULO"]);
                int prioridade = Convert.ToInt32(leitorTarefa["PRIORIDADE"]);
                DateTime dataDeCriacao = Convert.ToDateTime(leitorTarefa["DATADECRIACAO"]);
                if (leitorTarefa["DATACONCLUSAO"] != DBNull.Value)
                    dataConclusao = Convert.ToDateTime(leitorTarefa["DATACONCLUSAO"]);

                int percentual = Convert.ToInt32(leitorTarefa["PERCENTUALDECONCLUSAO"]);

                Tarefa t = new Tarefa(titulo, prioridade, dataDeCriacao, dataConclusao, percentual);
                t.id = id;

                tarefas.Add(t);
            }

            conexaoComBanco.Close();
            return tarefas;
        }
        public override Tarefa ObterIdSelecionado(int id)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [TITULO], 
                        [PRIORIDADE], 
                        [DATADECRIACAO],
                        [DATACONCLUSAO], 
                        [PERCENTUALDECONCLUSAO] 
                    FROM 
                        TB_tarefas
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", id);

            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            if (leitorTarefa.Read() == false)
                return null;
            DateTime dataConclusao = new DateTime(0001 / 01 / 01);
            int idT = Convert.ToInt32(leitorTarefa["ID"]);
            string titulo = Convert.ToString(leitorTarefa["TITULO"]);
            int prioridade = Convert.ToInt32(leitorTarefa["PRIORIDADE"]);
            DateTime dataDeCriacao = Convert.ToDateTime(leitorTarefa["DATADECRIACAO"]);
            if (leitorTarefa["DATACONCLUSAO"] != DBNull.Value)
                dataConclusao = Convert.ToDateTime(leitorTarefa["DATACONCLUSAO"]);

            int percentual = Convert.ToInt32(leitorTarefa["PERCENTUALDECONCLUSAO"]);

            Tarefa tarefinha = new Tarefa(titulo, prioridade, dataDeCriacao, dataConclusao, percentual);
            tarefinha.id = idT;

            conexaoComBanco.Close();

            return tarefinha;
        }
        public void FecharTarefa(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoFechar = new SqlCommand();
            comandoFechar.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TB_tarefas
                 SET   
                  [TITULO] = @TITULO,
                  [PRIORIDADE] = @PRIORIDADE,
                  [PERCENTUALDECONCLUSAO] = @PERCENTUALDECONCLUSAO,         
                  [DATACONCLUSAO] = @DATACONCLUSAO  
                    WHERE 
                  [ID] = @ID";

            comandoFechar.CommandText = sqlAtualizacao;
            comandoFechar.Parameters.AddWithValue("ID", tarefa.id);
            comandoFechar.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoFechar.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoFechar.Parameters.AddWithValue("PERCENTUALDECONCLUSAO", 100);
            comandoFechar.Parameters.AddWithValue("DATACONCLUSAO", DateTime.Now);

            comandoFechar.ExecuteNonQuery();

            conexaoComBanco.Close();
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

