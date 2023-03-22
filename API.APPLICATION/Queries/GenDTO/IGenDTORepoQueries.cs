using API.APPLICATION.Parameters.GenDTO;
using API.DOMAIN.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.GenDTO
{
    public interface IGenDTORepoQueries
    {
        string GetResultClass(ServerConnection pInfo);
        ServerConnection ChuoiKetNoi();

    }
    public class GenDTORepoQueries : IGenDTORepoQueries
    {
        private const string F_CONNECT = "data source={0};initial catalog={1};uid={2};password={3};MultipleActiveResultSets=True;App=EntityFramework";
        protected readonly IConfiguration _configuration;

        public GenDTORepoQueries(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetResultClass(ServerConnection pInfo)
        {
            try
            {
                DataSet ds = new DataSet();
                string connect = string.Format(F_CONNECT, pInfo.ServerName, pInfo.DBName, pInfo.Login, pInfo.Password);
                SqlConnection sqlConn = new SqlConnection(connect);

                SqlCommand cmdReport = new SqlCommand(pInfo.SqlCmd, sqlConn);
                SqlDataAdapter daReport = new SqlDataAdapter(cmdReport);
                using (cmdReport)
                {
                    daReport.Fill(ds);
                }

                if (sqlConn.State != ConnectionState.Closed)
                {
                    sqlConn.Close();
                }
                sqlConn.Dispose();
                if (ds != null && ds.Tables.Count > 0)
                {
                    return GenClassForObject(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
        private string GenClassForObject(DataTable dt)
        {
            StringBuilder str = new StringBuilder();
            str.Append("public class ABC");
            str.AppendLine();
            str.AppendLine("{");
            str.AppendLine();
            string valueNull = "";
            foreach (DataColumn item in dt.Columns)
            {
                string col = item.ColumnName.ToString();
                //item.DataType

                string type = GetTypeByDataType(item.DataType, ref valueNull);
                if (col.Equals("Id") || col.Equals("ID"))
                {
                    valueNull = "";
                }
                else
                {
                    valueNull = (item.AllowDBNull ? valueNull : "");
                }
                str.Append(string.Format("  public {0}{1} {2} {3}", type, valueNull, item.ColumnName.ToString(), "{ get; set; }"));
                str.AppendLine();

            }
            str.AppendLine("}");
            return str.ToString();
        }

        private string GetTypeByDataType(Type pDataType, ref string pValueNull)
        {
            string type = pDataType.ToString().Replace("System.", "");
            string result = "";
            pValueNull = "";
            switch (type)
            {
                case "Boolean":
                    result = "bool";
                    break;
                case "Byte":
                    result = "byte";
                    break;
                case "Byte[]":
                    result = "byte[]";
                    break;
                case "Int16":
                    result = "short";
                    break;
                case "Int32":
                    result = "int";
                    break;
                case "Int64":
                    result = "long";
                    break;
                case "Decimal":
                    result = "decimal";
                    break;
                case "Double":
                    result = "double";
                    break;

                case "DateTime2":
                    result = "DateTime";
                    break;
                case "String":
                    result = "string";
                    break;
                default: result = type; break;

            }

            if (!result.Contains("string") && !result.Contains("Byte[]"))
            {
                pValueNull = "?";
            }

            return result;
        }
        public ServerConnection ChuoiKetNoi()
        {
            ServerConnection connect = new ServerConnection();
            var data = _configuration.GetConnectionString("Default");
            if(data != null)
            {
                try
                {
                    List<string> lstItem = new List<string>();
                    string[] chuoi = data.Split(';');
                    foreach (var item in chuoi)
                    {
                        var getViTri = item.IndexOf("=");
                        var Str1 = item.Substring(getViTri + 1);
                        lstItem.Add(Str1);
                    }

                    connect.ServerName = lstItem[0];
                    connect.DBName = lstItem[1];
                    connect.Login = lstItem[2];
                    connect.Password = lstItem[3];
                }
                catch (Exception)
                {

               
                }
                
            }
            
            return connect;
        }
    }
   
}
