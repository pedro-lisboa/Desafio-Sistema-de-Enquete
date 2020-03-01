using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Desafio_Dominio;
using Desafio_DataBase;

namespace Desafio_DAL
{
    public class Enquete
    {
        public int Inserir(TB_Enquete enquete)
        {
            try
            {
                var strQueryT = string.Format("SELECT COUNT(*)+1 AS ID_ENQUETE FROM T_ENQUETE");

                Conexao dadosEnquete = new Conexao();
                SqlDataReader retornoReader = dadosEnquete.RetornoReader(strQueryT);

                var idEnquete = 0;

                while (retornoReader.Read())
                {
                    idEnquete = int.Parse(retornoReader["ID_ENQUETE"].ToString());
                }

                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO T_ENQUETE");
                strQuery.Append("(");
                strQuery.Append(" ID, ");
                strQuery.Append(" NM_DESCRICAO,");
                strQuery.Append(" NU_VIEW ) Values (");
                strQuery.Append("'" + idEnquete + "',");
                //strQuery.Append(" NEXT VALUE FOR SEQ_ENQUETE,");
                //strQuery.Append(" (SELECT COUNT(*)+1 FROM T_ENQUETE),");
                strQuery.Append("'" + enquete.poll_description + "',");
                strQuery.Append("0 )");

                dadosEnquete.ExecutarComando(strQuery);

                StringBuilder strQueryO = new StringBuilder();
                for (int i = 0; i < 3; i++)
                {
                    strQueryO.Append("INSERT INTO T_OPCAO");
                    strQueryO.Append("(");
                    strQueryO.Append(" ID_ENQUETE, ");
                    strQueryO.Append(" ID_OPCAO, ");
                    strQueryO.Append(" NM_OPCAO_DESCRICAO, ");
                    strQueryO.Append(" NU_VOTO) Values (");
                    strQueryO.Append("'" + idEnquete + "',");
                    strQueryO.Append("'" + enquete.options[i].option_id + "',");
                    strQueryO.Append("'" + enquete.options[i].option_description + "',");
                    strQueryO.Append("0)");
                    dadosEnquete.ExecutarComando(strQueryO);
                    strQueryO = new StringBuilder();
                }
                return idEnquete;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Alterar(TB_Enquete enquete)
        {

            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("UPDATE T_ENQUETE");
                strQuery.Append(" SET NM_DESCRICAO ='");
                strQuery.Append(enquete.poll_description + "'");
                strQuery.Append(" WHERE ID = ");
                strQuery.Append(enquete.poll_id);



                Conexao dadosEnquete = new Conexao();
                dadosEnquete.ExecutarComando(strQuery);



                StringBuilder strQueryO = new StringBuilder();
                for (int i = 0; i < 3; i++)
                {
                    strQueryO.Append("UPDATE T_OPCAO");
                    strQueryO.Append(" SET NM_OPCAO_DESCRICAO ='");
                    strQueryO.Append(enquete.options[i].option_description + "'");
                    strQueryO.Append(" WHERE ID_ENQUETE = ");
                    strQueryO.Append(enquete.poll_id);
                    strQueryO.Append(" AND ID_OPCAO = ");
                    strQueryO.Append(enquete.options[i].option_id);
                    dadosEnquete.ExecutarComando(strQueryO); 
                    strQueryO = new StringBuilder();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<TB_Enquete> ListarTodos()
        {
            try
            {

                var strQuery = string.Format("SELECT E.ID, E.NM_DESCRICAO, E.NU_VIEW " +
                                              " FROM T_ENQUETE E");
                var strQueryOpcao = string.Format("");


                Conexao DadosEnquete = new Conexao();
                SqlDataReader retornoReader = DadosEnquete.RetornoReader(strQuery);

                TB_Enquete obj = null;
                List<TB_Enquete> listenquete = new List<TB_Enquete>();




                while (retornoReader.Read())
                {
                    obj = new TB_Enquete();

                    obj.poll_id = int.Parse(retornoReader["ID"].ToString());

                    TB_Opcao objOpcao = null;
                    List<TB_Opcao> listopcao = new List<TB_Opcao>();
                    strQueryOpcao = string.Format("SELECT ID_OPCAO, NM_OPCAO_DESCRICAO, NU_VOTO FROM T_OPCAO WHERE ID_ENQUETE =" + obj.poll_id + " ORDER BY ID_OPCAO");
                    SqlDataReader retornoReaderOpcao = DadosEnquete.RetornoReader(strQueryOpcao);
                    while (retornoReaderOpcao.Read())
                    {
                        objOpcao = new TB_Opcao();
                        objOpcao.poll_id = obj.poll_id;
                        objOpcao.option_id = int.Parse(retornoReaderOpcao["ID_OPCAO"].ToString());
                        objOpcao.option_description = retornoReaderOpcao["NM_OPCAO_DESCRICAO"].ToString();
                        objOpcao.votes = int.Parse(retornoReaderOpcao["NU_VOTO"].ToString());
                        listopcao.Add(objOpcao);
                    }
                    obj.options = listopcao;
                    obj.poll_description = retornoReader["NM_DESCRICAO"].ToString();
                    obj.views = int.Parse(retornoReader["NU_VIEW"].ToString());

                    listenquete.Add(obj);
                }
                return listenquete;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TB_Enquete ListarPorId(int id)
        {
            try
            {
                var strQuery = string.Format("SELECT E.ID, E.NM_DESCRICAO, E.NU_VIEW" +
                                              " FROM T_ENQUETE E " +
                                              " WHERE ID={0}", id);

                var strQueryO = string.Format("SELECT E.ID_OPCAO, E.NM_OPCAO_DESCRICAO, E.NU_VOTO " +
                                              " FROM T_OPCAO E " +
                                              " WHERE ID_ENQUETE={0} ORDER BY E.ID_OPCAO", id);


                Conexao DadosEnquete = new Conexao();
                SqlDataReader retornoReader = DadosEnquete.RetornoReader(strQuery);

                TB_Enquete obj = null;

                if (retornoReader.Read())
                {
                    obj = new TB_Enquete
                    {
                        poll_id = int.Parse(retornoReader["ID"].ToString()),
                        poll_description = retornoReader["NM_DESCRICAO"].ToString(),
                        views = int.Parse(retornoReader["NU_VIEW"].ToString()),
                    };
                    TB_Opcao objO = null;
                    List<TB_Opcao> objOList = new List<TB_Opcao> { };
                    SqlDataReader retornoReaderO = DadosEnquete.RetornoReader(strQueryO);
                    while (retornoReaderO.Read())
                    {
                        objO = new TB_Opcao
                        {
                            poll_id = obj.poll_id,
                            option_id = int.Parse(retornoReaderO["ID_OPCAO"].ToString()),
                            option_description = retornoReaderO["NM_OPCAO_DESCRICAO"].ToString(),
                            votes = int.Parse(retornoReaderO["NU_VOTO"].ToString())
                        };
                        objOList.Add(objO);
                    }
                    obj.options = objOList;
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public VO_Enquete ListPollById(int id)
        {
            try
            {
                var strQuery = string.Format("SELECT E.ID, E.NM_DESCRICAO, E.NU_VIEW" +
                                              " FROM T_ENQUETE E " +
                                              " WHERE ID={0}", id);

                var strQueryO = string.Format("SELECT E.ID_OPCAO, E.NM_OPCAO_DESCRICAO, E.NU_VOTO " +
                                              " FROM T_OPCAO E " +
                                              " WHERE ID_ENQUETE={0} ORDER BY E.ID_OPCAO", id);

                StringBuilder strQueryView = new StringBuilder();
                strQueryView.Append("UPDATE T_ENQUETE SET NU_VIEW = NU_VIEW + 1 ");
                strQueryView.Append(" WHERE ID=");
                strQueryView.Append(id);

                Conexao DadosEnquete = new Conexao();
                DadosEnquete.ExecutarComando(strQueryView);
                SqlDataReader retornoReader = DadosEnquete.RetornoReader(strQuery);

                VO_Enquete obj = null;

                if (retornoReader.Read())
                {
                    obj = new VO_Enquete
                    {
                        poll_id = int.Parse(retornoReader["ID"].ToString()),
                        poll_description = retornoReader["NM_DESCRICAO"].ToString(),
                    };
                    VO_Enquete_Opcao objO = null;
                    List<VO_Enquete_Opcao> objOList = new List<VO_Enquete_Opcao> { };
                    SqlDataReader retornoReaderO = DadosEnquete.RetornoReader(strQueryO);
                    while (retornoReaderO.Read())
                    {
                        objO = new VO_Enquete_Opcao
                        {
                            option_id = int.Parse(retornoReaderO["ID_OPCAO"].ToString()),
                            option_description = retornoReaderO["NM_OPCAO_DESCRICAO"].ToString(),
                        };
                        objOList.Add(objO);
                    }
                    obj.options = objOList;
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TB_Enquete ListarStats(int id)
        {
            try
            {
                var strQuery = string.Format("SELECT E.ID, E.NM_DESCRICAO, E.NU_VIEW" +
                                              " FROM T_ENQUETE E " +
                                              " WHERE ID={0}", id);

                var strQueryO = string.Format("SELECT E.ID_OPCAO, E.NM_OPCAO_DESCRICAO, E.NU_VOTO " +
                                              " FROM T_OPCAO E " +
                                              " WHERE ID_ENQUETE={0} ORDER BY E.ID_OPCAO", id);
                
                Conexao DadosEnquete = new Conexao();
                SqlDataReader retornoReader = DadosEnquete.RetornoReader(strQuery);

                TB_Enquete obj = null;

                if (retornoReader.Read())
                {
                    obj = new TB_Enquete
                    {
                        poll_id = int.Parse(retornoReader["ID"].ToString()),
                        poll_description = retornoReader["NM_DESCRICAO"].ToString(),
                        views = int.Parse(retornoReader["NU_VIEW"].ToString()),
                    };
                    TB_Opcao objO = null;
                    List<TB_Opcao> objOList = new List<TB_Opcao> { };
                    SqlDataReader retornoReaderO = DadosEnquete.RetornoReader(strQueryO);
                    while (retornoReaderO.Read())
                    {
                        objO = new TB_Opcao
                        {
                            poll_id = obj.poll_id,
                            option_id = int.Parse(retornoReaderO["ID_OPCAO"].ToString()),
                            option_description = retornoReaderO["NM_OPCAO_DESCRICAO"].ToString(),
                            votes = int.Parse(retornoReaderO["NU_VOTO"].ToString())
                        };
                        objOList.Add(objO);
                    }
                    obj.options = objOList;
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool Excluir(int id)
        {
            try
            {
                StringBuilder exsql = new StringBuilder();

                exsql.Append(" DELETE FROM T_OPCAO");
                exsql.Append(" WHERE ID_ENQUETE =");
                exsql.Append(id);

                StringBuilder exsql2 = new StringBuilder();

                exsql2.Append(" DELETE FROM T_ENQUETE");
                exsql2.Append(" WHERE ID =");
                exsql2.Append(id);

                Conexao dadosEnquete = new Conexao();
                dadosEnquete.ExecutarComando(exsql);
                dadosEnquete.ExecutarComando(exsql2);

                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void Votar(TB_Opcao opcao)
        {

            try
            {
                if (opcao.option_id < 1 || opcao.option_id > 3)
                {
                    throw new Exception("404 Not Found.");
                }

                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("UPDATE T_OPCAO");
                strQuery.Append(" SET NU_VOTO = NU_VOTO+1");
                strQuery.Append(" WHERE ID_ENQUETE = ");
                strQuery.Append(opcao.poll_id);
                strQuery.Append(" AND ID_OPCAO = ");
                strQuery.Append(opcao.option_id);

                Conexao dadosEnquete = new Conexao();
                dadosEnquete.ExecutarComando(strQuery);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}