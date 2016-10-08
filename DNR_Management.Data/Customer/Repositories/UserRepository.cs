using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Data.Customer.Repositories
{
    public class UserRepository
    {
        private Catalog Catalog;
        private SqlCommand command;
        private SqlConnection connection;
        private SqlDataReader reader;

        public UserRepository()
        {
            Catalog = new Catalog();
            command = Catalog.GetSqlCommand();
            connection = Catalog.GetSqlConnection();
        }

        public int GetUser(string userName, string password)
        {
            User user = new User();
            string query = string.Format("SELECT Status FROM Users WHERE [UserName] = '{0}' AND Password = '{1}'", userName, password);
            command.CommandText = query;
            connection.Open();
            //reader = command.ExecuteReader();
            try
            {
                int status = (Int32)command.ExecuteScalar();
                user.Status = status;
                // connection.Open();
            }

            catch (Exception e)
            {
                //string ErrorString = e.Massage;

            }

            finally
            {
                connection.Close();
            }
            return user.Status;
        }

        private static string MyToString(object o)
        {
            if (o == DBNull.Value || o == null)
                return "";

            return o.ToString();
        }
    }
}
