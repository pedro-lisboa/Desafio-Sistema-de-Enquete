using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;


namespace Desafio_DataBase
{
    public class Conexao
    {
        //string StrConexao = (@"data source=localhost\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=DB_ENQUETE");
        string StrConexao = (@"data source=localhost\SQLSERVER;Integrated Security=SSPI;Initial Catalog=DB_ENQUETE");
        private SqlConnection AbrirBanco()
        {
            SqlConnection cn = new SqlConnection(StrConexao);
            cn.Open();
            return cn;
        }


        public void ExecutarComando(StringBuilder strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SqlDataReader RetornoReader(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                return cmd.ExecuteReader();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
