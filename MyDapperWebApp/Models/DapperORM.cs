using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyDapperWebApp.Models
{
    public static class DapperORM
    {
        private static string connectionString = "Data Source=EC2AMAZ-EAKN5QJ;Database=DapperDB;Integrated Security=True";


        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null) { 
        
           using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

            }
        
        }

        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
                return (T)Convert.ChangeType(
                    sqlCon.ExecuteScalar(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof (T));

            }

        }


        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
               return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

            }

        }

    }
}
