using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ControleDeTarefas.ConsoleApp.Controlador;
using ControleDeTarefas.ConsoleApp.Dominio;

namespace ControleDeTarefas.ConsoleApp.Controlador
{
    public class ControladorCompromisso : ControladorGeneric<Compromissos>
    {
        public override void ObterInsert(Compromissos compromisso)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @"INSERT INTO TB_compromisso
	                (   
                        [ASSUNTO],
		                [LUGAR], 
		                [LINK], 
		                [DATAINICIO], 
		                [DATATERMINO], 
		                [CONTATO_ID]
	                ) 
	                VALUES
	                (   
                        @ASSUNTO,
                        @LUGAR, 
                        @LINK, 
		                @DATAINICIO, 
		                @DATATERMINO,  
		                @CONTATO_ID
	                );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("ASSUNTO", compromisso.assunto);
            if (compromisso.local == null)
                comandoInsercao.Parameters.AddWithValue("LUGAR", DBNull.Value);
            else
            comandoInsercao.Parameters.AddWithValue("LUGAR", compromisso.local);

            if (compromisso.link == null)
                comandoInsercao.Parameters.AddWithValue("LINK", DBNull.Value);
            else
                comandoInsercao.Parameters.AddWithValue("LINK", compromisso.link);

            comandoInsercao.Parameters.AddWithValue("DATAINICIO", compromisso.dataInicio);
            comandoInsercao.Parameters.AddWithValue("DATATERMINO", compromisso.dataTermino);

            if (compromisso.idContato == null)
                comandoInsercao.Parameters.AddWithValue("CONTATO_ID", DBNull.Value);        
            else
                comandoInsercao.Parameters.AddWithValue("CONTATO_ID", compromisso.idContato);
            object id = comandoInsercao.ExecuteScalar();

            compromisso.id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }
        public override void ObterEdicao(Compromissos compromisso)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoEditar = new SqlCommand();
            comandoEditar.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE TB_compromisso
                 SET
                  [ASSUNTO] = @ASSUNTO,
                  [LUGAR] = @LUGAR,
                  [LINK] = @LINK,
                  [DATAINICIO] = @DATAINICIO,      
                  [DATATERMINO] = @DATATERMINO, 
                  [CONTATO_ID] = @CONTATO_ID
                    WHERE 
                  [ID] = @ID";

            comandoEditar.CommandText = sqlAtualizacao;
            comandoEditar.Parameters.AddWithValue("ID", compromisso.id);
            comandoEditar.Parameters.AddWithValue("ASSUNTO", compromisso.assunto);
            comandoEditar.Parameters.AddWithValue("LUGAR", compromisso.local);
            comandoEditar.Parameters.AddWithValue("LINK", compromisso.link);
            comandoEditar.Parameters.AddWithValue("DATAINICIO", compromisso.dataInicio);
            comandoEditar.Parameters.AddWithValue("DATATERMINO", compromisso.dataTermino);
            comandoEditar.Parameters.AddWithValue("CONTATO_ID", compromisso.idContato);

            comandoEditar.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override void ObterExclusao(object tipo, int id)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TB_compromisso	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override List<Compromissos> ObterRegistros()
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        comp.[ID], 
                        [ASSUNTO],
                        [LUGAR], 
                        [LINK],
                        [DATAINICIO],
                        [DATATERMINO],
                        [NOME]
                    FROM 
                        TB_compromisso comp
                    LEFT JOIN TB_contatos con
                    ON CONTATO_ID = con.ID";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            List<Compromissos> compromissos = new List<Compromissos>();

            while (leitorCompromisso.Read())
            {
                string lugar = "SEM LOCAL";
                string link = "SEM LINK";
                string nome = "SEM CONTATO";

                int id = Convert.ToInt32(leitorCompromisso["ID"]);
                string assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);

                if (leitorCompromisso["LUGAR"] != DBNull.Value)
                    lugar = Convert.ToString(leitorCompromisso["LUGAR"]);

                if (leitorCompromisso["LINK"] != DBNull.Value)
                    link = Convert.ToString(leitorCompromisso["LINK"]);

                DateTime dataInicio = Convert.ToDateTime(leitorCompromisso["DATAINICIO"]);
                DateTime dataTermino = Convert.ToDateTime(leitorCompromisso["DATATERMINO"]);

                if (leitorCompromisso["NOME"] != DBNull.Value)
                    nome = Convert.ToString(leitorCompromisso["NOME"]);

                Compromissos comp = new Compromissos(assunto, lugar, link, dataInicio, dataTermino, nome);
                comp.id = id;

                compromissos.Add(comp);
            }

            conexaoComBanco.Close();
            return compromissos;
        }
        public override Compromissos ObterIdSelecionado(int id)
        {
            SqlConnection conexaoComBanco = AbreConexaoComOBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                 @"SELECT 
                        comp.[ID], 
                        [ASSUNTO],
                        [LUGAR], 
                        [LINK],
                        [DATAINICIO],
                        [DATATERMINO],
                        [CONTATO_ID]
                    FROM 
                        TB_compromisso comp";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", id);

            SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            if (leitorCompromisso.Read() == false)
                return null;

            string lugar = "SEM LOCAL";
            string link = "SEM LINK";
            int idContato = 0;

            int idC = Convert.ToInt32(leitorCompromisso["ID"]);
            string assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);

            if (leitorCompromisso["LUGAR"] != DBNull.Value)
                lugar = Convert.ToString(leitorCompromisso["LUGAR"]);

            if (leitorCompromisso["LINK"] != DBNull.Value)
                link = Convert.ToString(leitorCompromisso["LINK"]);

            DateTime dataInicio = Convert.ToDateTime(leitorCompromisso["DATAINICIO"]);
            DateTime dataTermino = Convert.ToDateTime(leitorCompromisso["DATATERMINO"]);

            if (leitorCompromisso["CONTATO_ID"] != DBNull.Value)
                idContato = Convert.ToInt32(leitorCompromisso["CONTATO_ID"]);

            Compromissos comp = new Compromissos(assunto, lugar, link, dataInicio, dataTermino, idContato);
            comp.id = idC;

            conexaoComBanco.Close();

            return comp;
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
        public List<Compromissos> ObterCompromissoNoDia()
        {
            List<Compromissos> recebeLista = ObterRegistros();
            List<Compromissos> listaNoDia = new List<Compromissos>();

            foreach (var dia in recebeLista)
            {
                if (dia.dataInicio.Day == DateTime.Now.Day)
                {
                    listaNoDia.Add(dia);
                }
            }
            return listaNoDia;
        }
        public List<Compromissos> ObterCompromissoNaSemana()
        {
            List<Compromissos> recebeLista = ObterRegistros();
            List<Compromissos> listaNaSemana = new List<Compromissos>();

            foreach (var semana in recebeLista)
            {
                if (semana.dataInicio.Date <= DateTime.Today.AddDays(7) && semana.dataTermino.Date <= DateTime.Today.AddDays(7) && semana.dataInicio.Date >= DateTime.Today)
                {
                    listaNaSemana.Add(semana);
                }
            }
            return listaNaSemana;
        }  
        public bool PodeMarcarEsteCompromisso(Compromissos compromisso)
        {
            List<Compromissos> todos = ObterRegistros();
            bool pode = true;
            foreach (var item in todos)
            {
                if (compromisso.dataInicio >= item.dataInicio && compromisso.dataInicio <= item.dataTermino)
                    pode = false;
                else
                    pode = true;
            }
            return pode;
        }
    }
}
