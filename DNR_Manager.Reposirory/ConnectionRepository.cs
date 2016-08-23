using DNR_Manager.Data;
using DNR_Manager.Data.Customer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Reposirory
{
    public class ConnectionRepository : IConnectionRepository
    {
        private Catalog Catalog;
        private SqlCommand command;
        private SqlConnection connection;

        public ConnectionRepository()
        {
            Catalog = new Catalog();
            command = Catalog.GetSqlCommand();
            connection = Catalog.GetSqlConnection();
        }

        public void AddAccount(Account account)
        {
            string query = string.Format("INSERT INTO Accounts VALUES ('{0}','{1}','{2}','{3}','{4}')",
                account.AccountNo, account.ReaderCode, account.PackNo, account.Walkseq, account.Address);

            command.CommandText = query;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public ConnectionRep
    }
}
