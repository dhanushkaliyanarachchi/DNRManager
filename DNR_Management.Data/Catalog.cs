using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DNR_Manager.Data
{
    public class Catalog
    {
        public SqlConnection conn;
        public SqlCommand command;
        public Catalog()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DNR_Manager"].ToString();
            conn = new SqlConnection(connectionString);
            command = new SqlCommand("",conn);
        }

        public SqlConnection GetSqlConnection()
        {
            return conn;
        }

        public SqlCommand GetSqlCommand()
        {
            return command;
        }
    }
}
