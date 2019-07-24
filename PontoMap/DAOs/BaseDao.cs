using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace PontoMap.DAOs
{
    public class BaseDao
    {

        //https://stackoverflow.com/questions/9218847/how-do-i-handle-database-connections-with-dapper-in-net

        protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected List<T> Query<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Query<T>(sql, parameters).ToList();
            }
        }

        protected int Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Execute(sql, parameters);
            }
        }

        protected int Execute(string sql, CommandType tipoComando, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Execute(sql, parameters, commandType: tipoComando);
            }
        }

        // Other Helpers...

        private IDbConnection CreateConnection()
        {
            string dbConnectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["dataBase"].ConnectionString;
            return new SqlConnection(dbConnectionString);
        }

        //public IEnumerable<T> Query<T>(Func<T> typeBuilder, string sql, object parameters = null)
        //{
        //    using (var connection = CreateConnection())
        //    {
        //        connection.Open();
        //        return connection.Query<T>(sql, parameters);
        //    }
        //}


    }
}
