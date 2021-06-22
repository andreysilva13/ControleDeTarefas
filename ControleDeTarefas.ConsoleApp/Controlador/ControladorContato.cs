using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleDeTarefas.ConsoleApp.Controlador
{
    public class ControladorContato : ControladorGeneric<Contato>
    {
        public override void ObterInsert(Contato contato)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @"INSERT INTO TB_contatos
	                (   
                        [NOME],
		                [EMAIL], 
		                [TELEFONE], 
		                [EMPRESA], 
		                [CARGO]
	                ) 
	                VALUES
	                (   
                        @NOME,
                        @EMAIL, 
		                @TELEFONE, 
		                @EMPRESA,  
		                @CARGO
	                );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("NOME", contato.email);
            comandoInsercao.Parameters.AddWithValue("EMAIL", contato.email);
            comandoInsercao.Parameters.AddWithValue("TELEFONE", contato.telefone);
            comandoInsercao.Parameters.AddWithValue("EMPRESA", contato.empresa);
            comandoInsercao.Parameters.AddWithValue("CARGO", contato.cargo);

            object id = comandoInsercao.ExecuteScalar();

            contato.id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }
        public override void ObterEdicao(Contato contato)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoEditar = new SqlCommand();
            comandoEditar.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TB_contatos
                 SET
                  [NOME] = @NOME,
                  [EMAIL] = @EMAIL,
                  [TELEFONE] = @TELEFONE,
                  [EMPRESA] = @EMPRESA,      
                  [CARGO] = @CARGO     
                    WHERE 
                  [ID] = @ID";

            comandoEditar.CommandText = sqlAtualizacao;
            comandoEditar.Parameters.AddWithValue("ID", contato.id);
            comandoEditar.Parameters.AddWithValue("NOME", contato.nome);
            comandoEditar.Parameters.AddWithValue("EMAIL", contato.email);
            comandoEditar.Parameters.AddWithValue("TELEFONE", contato.telefone);
            comandoEditar.Parameters.AddWithValue("EMPRESA", contato.empresa);
            comandoEditar.Parameters.AddWithValue("CARGO", contato.cargo);

            comandoEditar.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override void ObterExclusao(Object contatos, int id)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TB_contatos	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override List<Contato> ObterRegistros()
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID], 
                        [NOME],
                        [EMAIL], 
                        [TELEFONE], 
                        [EMPRESA],
                        [CARGO]
                    FROM 
                        TB_contatos ORDER BY cargo";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                Contato c = new Contato(nome, email, telefone, empresa, cargo);
                c.id = id;

                contatos.Add(c);
            }

            conexaoComBanco.Close();
            return contatos;
        }
        public override Contato ObterIdSelecionado(int id)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                 @"SELECT 
                        [ID], 
                        [NOME], 
                        [EMAIL], 
                        [TELEFONE], 
                        [EMPRESA],
                        [CARGO]
                    FROM 
                        TB_contatos
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", id);

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            if (leitorContatos.Read() == false)
                return null;

            int idC = Convert.ToInt32(leitorContatos["ID"]);
            string nome = Convert.ToString(leitorContatos["NOME"]);
            string email = Convert.ToString(leitorContatos["EMAIL"]);
            string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
            string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
            string cargo = Convert.ToString(leitorContatos["CARGO"]);


            Contato c = new Contato(nome, email, telefone, empresa, cargo);
            c.id = idC;

            conexaoComBanco.Close();

            return c;
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
