using DNR_Manager.Data.Customer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Data.Customer.Repositories
{
    public class ConnectionRepository : IConnectionRepository
    {
        private Catalog Catalog;
        private SqlCommand command;
        private SqlConnection connection;
        private SqlDataReader reader;

        public ConnectionRepository()
        {
            Catalog = new Catalog();
            command = Catalog.GetSqlCommand();
            connection = Catalog.GetSqlConnection();
        }

        private static string MyToString(object o)
        {
            if (o == DBNull.Value || o == null)
                return "";

            return o.ToString();
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

        public bool CheckAvailabilityofConsumerDetails(string accNo)
        {
            string queryCheckAvailability = string.Format("SELECT * FROM ConsumerDetails WHERE [Account No] = {0}", accNo);
            try
            {
                connection.Open();
                command.CommandText = queryCheckAvailability;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }

            catch
            {
                return false;
            }

            finally
            {
                reader.Close();
                connection.Close();
            }
        }

        #region Method (Get Consumer Details To ECITY)
        public ConsumerDetail GetConsumerAccount(string accountNo)
        {
            ConsumerDetail newConsumer = new ConsumerDetail();
            string query = string.Format("SELECT * FROM ConsumerDetails WHERE [Account No] = '{0}'", accountNo);
            command.CommandText = query;
            connection.Open();
            reader = command.ExecuteReader();
            try
            {
                // connection.Open();
               
                    while (reader.Read())
                    {

                        newConsumer.ReaderCode = MyToString(reader["Reader Code"]);
                        newConsumer.DailypackNo = MyToString(reader["Daily Pack No"]);
                        newConsumer.WalkSeq = MyToString(reader["Walk Seq"]);
                        newConsumer.Depot = MyToString(reader["Depot"]);
                        newConsumer.FirstName = MyToString(reader["Cust Fname"]);
                        newConsumer.SecondName = MyToString(reader["Cust Lname"]);
                        newConsumer.AddressLine1 = MyToString(reader["Address 1"]);
                        newConsumer.AddressLine2 = MyToString(reader["Address 2"]);
                        newConsumer.AddressLine3 = MyToString(reader["Address 3"]);

                    }
                }
                
                
            

            catch (Exception e)
            {
                //string ErrorString = e.Massage;
               
            }

            finally
            {
                connection.Close();
            }
            return newConsumer;
        } 
        #endregion

        public List<PaymentDetails> GetPaymentDetails()
        {
            var paymentDetails = new List<PaymentDetails>();
            string query = string.Format("SELECT PaymentDetails.AccountNo,PaymentDetails.PaymentMethod,PaymentDetails.PaymentDate,ConsumerDetails.[Address 1],ConsumerDetails.[Address 2],ConsumerDetails.[Address 3],ConsumerDetails.Depot,ConsumerDetails.[Walk Seq],ConsumerDetails.[Daily Pack No],ConsumerDetails.[Reader Code] FROM PaymentDetails INNER JOIN ConsumerDetails ON PaymentDetails.AccountNo = ConsumerDetails.[Account No]");
            command.CommandText = query;
            connection.Open();
            reader = command.ExecuteReader();

            try
            {
                
                // connection.Open();
                while (reader.Read())
                {
                    var payment = new PaymentDetails();
                    payment.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    payment.PaymentDate = (DateTime)reader["PaymentDate"];
                    payment.PaymentMethod = reader["PaymentMethod"] != DBNull.Value ? (string)reader["PaymentMethod"] : "";
                    payment.AddressLine1 = reader["Address 1"] != DBNull.Value ? (string)reader["Address 1"] : "";
                    payment.AddressLine2 = reader["Address 2"] != DBNull.Value ? (string)reader["Address 2"] : "";
                    payment.AddressLine3 = reader["Address 3"] != DBNull.Value ? (string)reader["Address 3"] : "";
                    payment.Depot = reader["Depot"] != DBNull.Value ? (string)reader["Depot"]:"";
                    payment.WalkSeq = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    payment.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    payment.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    paymentDetails.Add(payment);
                }
                
            }

            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
                reader.Close();
            }
            return paymentDetails;
        }

        public ConnectionLog GetConnectionLog(string accountNo)
        {
            ConnectionLog newConnectionLog = new ConnectionLog();
            string query = string.Format("SELECT * FROM ConnectionLogs WHERE AccountNo = '{0}' AND Completness = '0' ", accountNo);
            command.CommandText = query;
           
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newConnectionLog.logId = (int)reader["logId"];
                    //string rCode = (string)reader["[Reader Code]"];
                    newConnectionLog.DisconnectedDate = (DateTime)reader["DisconnectedDate"];
                    newConnectionLog.DisconnectedBy = MyToString(reader["DisconnectedBy"]);
                    //newConnectionLog.DisconnectedBy = (string)reader["DisconnectedBy"];
                    //newConnectionLog.PaymentMode = (string)reader["PaymentMode"];
                    //newConnectionLog.DisconnectedBy = MyToString(reader["ReconnectedBy"]);
                    //newConnectionLog.ReconnectedBy = (string)reader["ReconnectedBy"];
                }
                
                
            }

            catch (Exception e)
            {
                //string ErrorString = e.Massage;
                
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            return newConnectionLog;
        }

        public Connection GetConnectionStatus(string accountNo)
        {
            Connection newConnection = new Connection();
            string quary = string.Format("SELECT * FROM Connections WHERE AccountNo = '{0}'", accountNo);
            
            try
            {
                command.CommandText = quary;
                connection.Open();
                reader = command.ExecuteReader(); 
                while(reader.Read())
                {
                    newConnection.connectionStatus = (int)reader["connectionStatus"];
                }

                return newConnection;
            }

            catch (Exception e)
            {
                return newConnection;
            }
 
        }

        public bool AddPaymentDetails(string PMethod, string AccNo, DateTime Date)
        {
            //PaymentDetails newPaymentDetail = new PaymentDetails();
            //string Error = "You have already insert Payment Details";
            string querytoaddPaymentDetais = string.Format("INSERT INTO PaymentDetails VALUES ('{0}','{1}','{2}')",
              PMethod, AccNo, Date.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            try
            {
                command.CommandText = querytoaddPaymentDetais;
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }

            catch (Exception e)
            {
                return false;    
            }

                finally{
                connection.Close();
            }
        }

       

        public void updateConnectionStatus(string accNo,int status)
        {
            string querytoUpdateconnection = string.Format("UPDATE Connections SET connectionStatus = '{0}' WHERE AccountNo = '{1}'", status, accNo);
            try
            {
                command.CommandText = querytoUpdateconnection;
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
        }

        public void UpdateDepotAndWalkOrder(string accountNo, string depot, string readerCode, string dailyPackNo, string walkSequence)
        {
            string updateQuery = string.Format("UPDATE ConsumerDetails SET [Reader Code] = '{0}',[Daily Pack No] = '{1}', [Walk Seq] = '{2}', Depot = '{3}' WHERE [Account No] = '{4}'", readerCode, dailyPackNo, walkSequence, depot, accountNo);
            try
            {
                connection.Open();
                command.CommandText = updateQuery;
                command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                
            }

            finally
            {
                connection.Close();
            }
        }

        public int insertLetterDetailsToTable(string accountNo, string LetterID)
        {
            string queryForInsertLetterDetails = string.Format("INSERT INTO LettersToBesent VALUES ('{0}','{1}')", accountNo, LetterID);
            int affectedRows = 0;
            try
            {
                connection.Open();
                command.CommandText = queryForInsertLetterDetails;
                affectedRows = command.ExecuteNonQuery();
                
            }

            catch(Exception ex)
            {

            }

            finally
            {

            }
            return affectedRows;
        }

        public int getnextIDfromLetterToBeSent(string Depot, string year)
        {
            string querytogetId = string.Format("SELECT LetterId FROM LettersToBesent WHERE LetterID LIKE '{0}/{1}/%'", Depot, year);
            command.CommandText = querytogetId;
            int Id = 0;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                ArrayList idList = new ArrayList();
                if ((reader.HasRows) == true)
                {
                    while (reader.Read())
                    {
                        string letterId = (string)reader["LetterId"];
                        Id = Int32.Parse(letterId.Substring(letterId.Length - 4));
                        idList.Add(Id);
                    }
                    int max = 0; //assumes + numbers
                    int temp;
                    foreach (int items in idList)
                    {
                        temp = items;
                        if (max < temp)
                        { max = temp; }
                    }
                    Id = max;
                }

                else if ((reader.HasRows) == false)
                {
                    Id = 0;
                }
            }

            catch (Exception ex)
            {
            }

            finally
            {
                connection.Close();
            }

            return Id;
        }

        public List<LetterDetails> getLetterDetailsToUI()
        {
            string letterQuery = string.Format("SELECT LettersToBesent.AccountNo, LettersToBesent.LetterId, ConsumerDetails.[Address 1], ConsumerDetails.[Address 2], ConsumerDetails.[Address 3] FROM LettersToBesent INNER JOIN ConsumerDetails ON LettersToBesent.AccountNo = ConsumerDetails.[Account No] ORDER BY LettersToBesent.LetterId");
            var letterDetail = new List<LetterDetails>();
            try
            {
                connection.Open();
                command.CommandText = letterQuery;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LetterDetails newletterDetail = new LetterDetails();
                    newletterDetail.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    newletterDetail.AddressLine1 = reader["Address 1"] != DBNull.Value ? (string)reader["Address 1"] : "";
                    newletterDetail.AddressLine2 = reader["Address 2"] != DBNull.Value ? (string)reader["Address 2"] : "";
                    newletterDetail.AddressLine3 = reader["Address 3"] != DBNull.Value ? (string)reader["Address 3"] : "";
                    newletterDetail.LetterId = reader["LetterId"] != DBNull.Value ? (string)reader["LetterId"] : "";
                    letterDetail.Add(newletterDetail);
                }
            }

            catch(Exception ex)
            {

            }

            finally
            {
                reader.Close();
                connection.Close();
            }

            return letterDetail;
        }
    }
}
