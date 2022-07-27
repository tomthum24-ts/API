using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCode
{
    public class DataAccess
    {
        private SqlConnection _con;
        private string strConnect = string.Empty;

        public DataAccess(string strConnect)
        {
            StrConnection = strConnect;
            _con = new SqlConnection(StrConnection);
        }
        public string StrConnection
        {
            get { return this.strConnect; }
            set { this.strConnect = value; }
        }

        private void OpenConnect()
        {
            if (_con.State == ConnectionState.Closed)
                _con.Open();
        }
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            OpenConnect();
            int number = -1;
            try
            {
                cmd.Connection = _con;
                number = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _con.Close();
            }
            return number;
        }
        public int ExecuteNonQuery(String strSQL)
        {
            OpenConnect();
            int number = -1;
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Connection = _con;
                number = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _con.Close();
            }
            return number;
        }
        public DataSet GetData(SqlCommand cmd)
        {
            OpenConnect();
            DataSet ds = null;
            try
            {
                cmd.Connection = _con;
                ds = new DataSet();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _con.Close();
            }
            return ds;
        }
        public DataSet GetData(String strSQL)
        {
            OpenConnect();
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                SqlDataAdapter adap = new SqlDataAdapter(strSQL, _con);
                adap.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _con.Close();
            }
            return ds;
        }

        public object ExecuteScalar(string pSqlString)
        {
            OpenConnect();

            SqlCommand cmd = new SqlCommand(pSqlString);
            cmd.Connection = _con;

            object result = cmd.ExecuteScalar();
            _con.Close();
            return result;
        }

        public int ExecuteNonQueryStatic(string pSqlString)
        {
            ////try
            ////{
            SqlCommand cmd = new SqlCommand(pSqlString);
            cmd.Connection = _con;
            cmd.CommandTimeout = 3600;
            OpenConnect();
            int result = cmd.ExecuteNonQuery();

            //CloseConnect();

            return result;
            //}
            //catch
            //{
            //    return 0;
            //}
        }

        public int ExecuteNonQuery(string pSqlString, SqlParameter[] paras)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(pSqlString);
                cmd.Parameters.AddRange(paras);
                cmd.Connection = _con;

                OpenConnect();
                int result = cmd.ExecuteNonQuery();

                //CloseConnect();

                return result;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable GetSchema(string tableName)
        {
            OpenConnect();

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(string.Format("SELECT TOP 1 * FROM {0}", tableName));
            cmd.Connection = _con;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            _con.Close();

            return ds.Tables[0];
        }

        /// <summary>
        /// Excutes the select is store.
        /// </summary>
        /// <param name="StrSQL">The STR SQL.</param>
        /// <param name="Fields">The fields.</param>
        /// <param name="Datas">The datas.</param>
        /// <returns></returns>
        /// HongTranh
        /// 16/03/2013 9:35 AM
        public DataSet ExcuteSelectIsStore(string StrSQL, string[] Fields, object[] Datas)
        {
            SqlCommand cmd = new SqlCommand(StrSQL);
            cmd.CommandType = CommandType.StoredProcedure;
            if (Fields != null)
            {
                for (int i = 0; i < Fields.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + Fields[i], Datas[i]));
                }
            }

            OpenConnect();
            DataSet ds = null;
            try
            {
                cmd.Connection = _con;
                ds = new DataSet();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _con.Close();
            }
            return ds;
        }
    }
}
