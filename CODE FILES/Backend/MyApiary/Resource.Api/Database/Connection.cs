using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Database
{
    public class Connection
    {
        private static SqlConnection connection { get; set; }

        public static void Open()
        {
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                connection = new SqlConnection(ConfigurationHelper.config.GetConnectionString("DevConnection"));
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString(), ex.Data.ToString());
                }
            }
        }

        public static SqlConnection Get()
        {
            return connection;
        }

        public static void Close()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
