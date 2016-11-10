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
            bool status = false;
            string queryCheckAvailability = string.Format("SELECT * FROM ConsumerDetails WHERE [Account No] = '{0}'", accNo);
            try
            {
                connection.Open();
                command.CommandText = queryCheckAvailability;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    status = true;
                }

                else
                {
                    status = false;
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
            return status;
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
            string query = string.Format("SELECT PaymentDetails.AccountNo,PaymentDetails.PaymentMethod,PaymentDetails.PaymentDate,PaymentDetails.UpdateStatus,ConsumerDetails.[Address 1],ConsumerDetails.[Address 2],ConsumerDetails.[Address 3],ConsumerDetails.Depot,ConsumerDetails.[Walk Seq],ConsumerDetails.[Daily Pack No],ConsumerDetails.[Reader Code], ConsumerDetails.ContactNumber FROM PaymentDetails INNER JOIN ConsumerDetails ON PaymentDetails.AccountNo = ConsumerDetails.[Account No]");
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
                    payment.ContactNo = reader["ContactNumber"] != DBNull.Value ? (string)reader["ContactNumber"] : "";
                    payment.status = reader["UpdateStatus"] != DBNull.Value ? (int)reader["UpdateStatus"] : 0;
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
                    newConnectionLog.DisconnectedDate = reader["DisconnectedDate"] != DBNull.Value ? (DateTime)reader["DisconnectedDate"] : DateTime.MinValue;
                    newConnectionLog.DisconnectedBy = reader["DisconnectedBy"] != DBNull.Value ? (string)reader["DisconnectedBy"] : "";
                    newConnectionLog.OrderCardStatus = reader["OrderCardStatus"] != DBNull.Value ? (int)reader["OrderCardStatus"] : 8;
                    newConnectionLog.MeterRemovedStatus = reader["MeterRemovedStatus"] != DBNull.Value ? (int)reader["MeterRemovedStatus"] : 8;
                    newConnectionLog.LetterSentStatus = reader["MeterRemovedStatus"] != DBNull.Value ? (int)reader["MeterRemovedStatus"] : 8;
                    newConnectionLog.LetterSentStatus = reader["LetterStatus"] != DBNull.Value ? (int)reader["LetterStatus"] : 8;
                    
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

            }

            catch (Exception e)
            {
                
            }

            finally
            {
                reader.Close();
                connection.Close();
            }
            return newConnection;
        }

        public bool AddPaymentDetails(string PMethod, string AccNo, DateTime Date, string contactNo)
        {
            bool updatestatus = false;
            string querytoaddPaymentDetais = string.Format("INSERT INTO PaymentDetails VALUES ('{0}','{1}','{2}',{3})",
              AccNo, PMethod, Date.ToString("yyyy-MM-dd HH:mm:ss.fff"), 0);
            string queryUpdate = string.Format("UPDATE ConsumerDetails SET ContactNumber ='{0}' WHERE ConsumerDetails.[Account No] = '{1}'", contactNo, AccNo);
            string querySelect = string.Format("SELECT * FROM ConsumerDetails WHERE ConsumerDetails.[Account No] = '{0}' AND ContactNumber IS NULL", AccNo);


            try
            {
                connection.Open();
                command.CommandText = querytoaddPaymentDetais;
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                        command.CommandText = queryUpdate;
                        command.ExecuteNonQuery();
                        updatestatus = true;
                }

                else
                {
                    updatestatus = false;
                }

                
            }

            catch (Exception e)
            {
                
            }

                finally{
                connection.Close();
                
            }

            return updatestatus; 
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

        public void insertReconnectionDetails(string accNo, string PaymentDate, string ReconnectedDate, string PaymentMethod, string ReconnectedBy)
        {
            int affectedrows = 0;
            DateTime PDate = DateTime.Parse(PaymentDate);
            string quaryforinsertReconnectionDetails = string.Format("INSERT INTO ReconnectionDetails (AccountNo,PaymentDate,ReconnectedDate,PymentMode,ReconnectedBy) Values ('{0}','{1}','{2}','{3}','{4}')", accNo, PDate.ToString("yyyy-MM-dd HH:mm"), ReconnectedDate, PaymentMethod, ReconnectedBy);
            string deletequery = string.Format("DELETE FROM PaymentDetails WHERE AccountNo = '{0}'", accNo);
            try
            {
                connection.Open();
                command.CommandText = quaryforinsertReconnectionDetails;
                affectedrows = command.ExecuteNonQuery();
                if (affectedrows > 0)
                {
                    command.CommandText = deletequery;
                    command.ExecuteNonQuery();
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }
        }

        public void UpdateDepotAndWalkOrder(string accountNo, string depot, string readerCode, string dailyPackNo, string walkSequence)
        {
            string updateQuery = string.Format("UPDATE ConsumerDetails SET [Reader Code] = '{0}',[Daily Pack No] = '{1}', [Walk Seq] = '{2}', Depot = '{3}' WHERE [Account No] = '{4}'", readerCode, dailyPackNo, walkSequence, depot, accountNo);
            string updateStatusQuery = string.Format("UPDATE PaymentDetails SET UpdateStatus = 1 WHERE AccountNo = '{0}'", accountNo);
            try
            {
                connection.Open();
                command.CommandText = updateQuery;
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    command.CommandText = updateStatusQuery;
                    command.ExecuteNonQuery();
                }
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
                connection.Close();
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
        public List<ThousandListDetails> getThousandListDetails()
        {
            var LisThousandListDates = new List<ThousandListDetails>();
            string query = string.Format("SELECT * FROM ThousandList ORDER BY ListDate DESC");
            command.CommandText = query;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ThousandListDetails newThousandDetail = new ThousandListDetails();
                    newThousandDetail.ThousandListDates = (DateTime)reader["ListDate"];
                    newThousandDetail.BillCircle = (Int32)reader["BillCircle"];
                    LisThousandListDates.Add(newThousandDetail);
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
                reader.Close();
            }

            return LisThousandListDates;
        }

        public int insertNewAccount(string accNo, string FName, string LName, string AddressLine1, string AddressLine2, string AddressLine3, string contactNumber, string ReadeCode, string DailyPackNo, string walkSeq,string Depot)
        {
            int affectedRows = 0;
            string insertQuery = string.Format("INSERT INTO ConsumerDetails ([Account No],[Reader Code],[Daily Pack No],[Walk Seq],[Depot],[Cust Fname],[Cust Lname] ,[Address 1],[Address 2],[Address 3], ContactNumber) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", accNo, ReadeCode, DailyPackNo, walkSeq, Depot, FName, LName, AddressLine1, AddressLine2, AddressLine3, contactNumber);
            command.CommandText = insertQuery;
            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
            }

            finally
            {
                connection.Close();
            }

            return affectedRows;
        }

        public void AddNewconnectionStaus(string accNo)
        {
            string readQuery = string.Format("SELECT * FROM Connections WHERE AccountNo = '{0}'", accNo);
            string insertQuery = string.Format("INSERT INTO Connections (AccountNo, connectionStatus) VALUES ('{0}','{1}')", accNo, 1);
            command.CommandText = readQuery;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows == false)
                {
                    command.CommandText = insertQuery;
                    reader.Close();
                    command.ExecuteNonQuery();
                }
            }
            catch(Exception ex){

            }

            finally
            {
                connection.Close();
                reader.Close();
            }
        }

        public List<ContractorList> GetContractorList()
        {
            List<ContractorList> newContractorList = new List<ContractorList>();
            string query = string.Format("SELECT * FROM ContractorDetails");
            command.CommandText = query;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ContractorList newContractor = new ContractorList();
                    newContractor.ID = (int)reader["ID"];
                    newContractor.DisconnectedBy = (string)reader["DnRDoneBy"];
                    newContractorList.Add(newContractor);
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
            return newContractorList;
        }

        public List<ReconnectionDetails> getReconnectionDetails()
        {
            var reconnctionDetails = new List<ReconnectionDetails>();
            string query = string.Format("SELECT ReconnectionDetails.AccountNo,ReconnectionDetails.PaymentDate,ReconnectionDetails.ReconnectedDate,ReconnectionDetails.PymentMode,ReconnectionDetails.ReconnectedBy,ConsumerDetails.[Address 1],ConsumerDetails.[Address 2],ConsumerDetails.[Address 3],ConsumerDetails.Depot,ConsumerDetails.[Walk Seq],ConsumerDetails.[Daily Pack No],ConsumerDetails.[Reader Code], ConsumerDetails.ContactNumber FROM ReconnectionDetails INNER JOIN ConsumerDetails ON ReconnectionDetails.AccountNo = ConsumerDetails.[Account No]");
            command.CommandText = query;
            connection.Open();
            reader = command.ExecuteReader();

            try
            {

                // connection.Open();
                while (reader.Read())
                {
                    var reconnection = new ReconnectionDetails();
                    reconnection.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    reconnection.PaymentDate = (DateTime)reader["PaymentDate"];
                    reconnection.ReconnectedDate = (DateTime)reader["ReconnectedDate"];
                    reconnection.PaymentMethod = reader["PymentMode"] != DBNull.Value ? (string)reader["PymentMode"] : "";
                    reconnection.ReconnectedBy = reader["ReconnectedBy"] != DBNull.Value ? (string)reader["ReconnectedBy"] : "";
                    reconnection.AddressLine1 = reader["Address 1"] != DBNull.Value ? (string)reader["Address 1"] : "";
                    reconnection.AddressLine2 = reader["Address 2"] != DBNull.Value ? (string)reader["Address 2"] : "";
                    reconnection.AddressLine3 = reader["Address 3"] != DBNull.Value ? (string)reader["Address 3"] : "";
                    reconnection.Depot = reader["Depot"] != DBNull.Value ? (string)reader["Depot"] : "";
                    reconnection.WalkSeq = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    reconnection.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    reconnection.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    reconnection.ContactNo = reader["ContactNumber"] != DBNull.Value ? (string)reader["ContactNumber"] : "";
                    reconnctionDetails.Add(reconnection);
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
            return reconnctionDetails;
        }

        public int getnextIDfromOrderCardList(string Depot, string year)
        {
            string querytogetId = string.Format("SELECT OrderCardID FROM OrderCardList WHERE LetterID LIKE '{0}/MR/{1}/%'", Depot, year);
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
                        string OrderCardId = (string)reader["OrderCardID"];
                        Id = Int32.Parse(OrderCardId.Substring(OrderCardId.Length - 4));
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

        public int insertOrdercardDetailsToTable(string accountNo, string OrderCardID, string LetterID)
        {
            string queryForInsertOrderCardDetails = string.Format("INSERT INTO OrderCardList (AccountNo, OrderCardID, LetterID) VALUES ('{0}','{1}','{2}')", accountNo, OrderCardID, LetterID);
            int affectedRows = 0;
            try
            {
                connection.Open();
                command.CommandText = queryForInsertOrderCardDetails;
                affectedRows = command.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }
            return affectedRows;
        }

        public List<OrderCardDetails> getOrderCardDetailsToUI()
        {
            string OrderCardQuery = string.Format("SELECT OrderCardList.AccountNo, OrderCardList.OrderCardID, OrderCardList.LetterID, ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConsumerDetails.[Cust Fname],  ConsumerDetails.[Cust Lname], ConsumerDetails.[Address 1], ConsumerDetails.[Address 2], ConsumerDetails.[Address 3], ConsumerDetails.Depot  FROM OrderCardList INNER JOIN ConsumerDetails ON OrderCardList.AccountNo = ConsumerDetails.[Account No] ORDER BY OrderCardList.OrderCardID");
            List<OrderCardDetails> OrdercardList = new List<OrderCardDetails>();

            try
            {
                command.CommandText = OrderCardQuery;
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderCardDetails newOrderCard = new OrderCardDetails();
                    newOrderCard.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    //newOrderCard.LetterSentDate = (DateTime)reader["LetterSentDate"];
                    newOrderCard.LetterID = reader["LetterID"] != DBNull.Value ? (string)reader["LetterID"] : "";
                    newOrderCard.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newOrderCard.DailypackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newOrderCard.WalkSeq = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newOrderCard.Fname = reader["Cust Fname"] != DBNull.Value ? (string)reader["Cust Fname"] : "";
                    newOrderCard.Lname = reader["Cust Lname"] != DBNull.Value ? (string)reader["Cust Lname"] : "";
                    newOrderCard.AddressLine1 = reader["Address 1"] != DBNull.Value ? (string)reader["Address 1"] : "";
                    newOrderCard.AddressLine2 = reader["Address 2"] != DBNull.Value ? (string)reader["Address 2"] : "";
                    newOrderCard.AddressLine3 = reader["Address 3"] != DBNull.Value ? (string)reader["Address 3"] : "";
                    newOrderCard.Depot = reader["Depot"] != DBNull.Value ? (string)reader["Depot"] : "";
                    newOrderCard.OrderCardID = reader["OrderCardID"] != DBNull.Value ? (string)reader["OrderCardID"] : "";
                    OrdercardList.Add(newOrderCard);
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                reader.Close();
                connection.Close();
            }

            return OrdercardList;
        }

        public int CheckForPaymentDetails(string accNo)
        {
            int status = 0;
            string selectQuery = string.Format("SELECT * FROM ReconnectionDetails WHERE AccountNo = {0}", accNo);
            string selectLogDetails = string.Format("SELECT * FROM ConnectionLogs WHERE AccountNo = {0} AND Completness = 0 AND DisconnectedBy IS NULL AND DisconnectedDate IS NULL AND ReconnectedBy IS NOT NULL", accNo);
            try
            {
                command.CommandText = selectLogDetails;
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    status = 1;
                    reader.Close();
                }

                else
                {
                    reader.Close();
                    command.CommandText = selectQuery;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        status = 2;
                    }

                    else
                    {
                        status = 0;
                    }
                }
            }

            catch(Exception ex){

            }

            finally
            {
                connection.Close();
                reader.Close();
            }
            return status;
        }
    }
}
