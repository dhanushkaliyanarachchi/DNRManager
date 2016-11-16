using DNR_Manager.Data.Customer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DNR_Manager.Data.Customer.Repositories
{
    public class ConnectionLogReporsitory : IConnectionLogReporsitory
    {
        private Catalog Catalog;
        private SqlCommand command;
        private SqlConnection connection;
        private SqlDataReader reader;
        
        ConnectionLog newConnectionLog = new ConnectionLog();

       public ConnectionLogReporsitory()
        {
            Catalog = new Catalog();
            command = Catalog.GetSqlCommand();
            connection = Catalog.GetSqlConnection();
        }


        public ConnectionLog GetConnectionLogDetailsForAvailabilityofLogEntry(string AccNo)
        {
            ConnectionLog newConnectionLog = new ConnectionLog();
            string query = string.Format("SELECT * FROM ConnectionLogs WHERE AccountNo = '{0}' AND Completness = '0' ", AccNo);
            command.CommandText = query;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newConnectionLog.logId = (int)reader["logId"];
                    newConnectionLog.DisconnectedDate = reader["DisconnectedDate"] != DBNull.Value ? (DateTime)reader["DisconnectedDate"] : DateTime.MinValue;
                    newConnectionLog.DisconnectedBy = reader["DisconnectedDate"] != DBNull.Value ? (string)reader["DisconnectedBy"] : string.Empty;
                    newConnectionLog.PaymentMode = reader["PaymentMode"] != DBNull.Value ? (string)reader["PaymentMode"] : string.Empty;
                    newConnectionLog.ReconnectedBy = reader["ReconnectedBy"] != DBNull.Value ? (string)reader["ReconnectedBy"] : string.Empty;
                    newConnectionLog.ReconnectedDate = reader["ReconnectedBy"] != DBNull.Value ? (DateTime)reader["ReconnectedDate"] : DateTime.MinValue;                   
                }
                
            }

            catch (Exception e){

            }

            finally
            {
                connection.Close();
            }
            return newConnectionLog;
        }

        public void insertDisconnectionDetails(string Accountno,string DDate,string DisconnectedTime, string DisconnectedBy)
        {
            int completness = 0;
            int LetterSent = 0;
            DateTime DisconnectedDate = DateTime.Parse(DDate);
            string[] time = DisconnectedTime.Split(new char[] { ':' });
            TimeSpan time1 = new TimeSpan(0,int.Parse(time[0]),int.Parse(time[1]),0);
            DisconnectedDate = DisconnectedDate.Date + time1;

            string quaryforinsertDisconnectionDetails = string.Format("INSERT INTO ConnectionLogs (AccountNo,DisconnectedDate,DisconnectedBy,Completness,LetterStatus) Values ('{0}','{1}','{2}','{3}','{4}')", Accountno, DisconnectedDate.ToString("yyyy-MM-dd HH:mm"), DisconnectedBy, completness, LetterSent);
            try
            {
                connection.Open();
                command.CommandText = quaryforinsertDisconnectionDetails;
                command.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {

            }

            finally
            {
                connection.Close();
            }
        }

        public void updateDisconnectionDetails(int LogID,string Accountno, string DDate, string DisconnectedBy, string DisconnectedTime)
        {

            DateTime DisconnectedDate = DateTime.Parse(DDate);
            string[] time = DisconnectedTime.Split(new char[] { ':' });
            DateTime DDDate = DisconnectedDate.Date.AddHours(double.Parse(time[0])).AddMinutes(double.Parse(time[1]));

            string quaryforUpdateDisconnectionDetails = string.Format("UPDATE ConnectionLogs SET DisconnectedDate = '{0}',DisconnectedBy ='{1}',Completness = '{2}' WHERE LogID = '{3}' AND AccountNo = '{4}'", DDDate.ToString("yyyy-MM-dd HH:mm"), DisconnectedBy, 1, LogID, Accountno);
            try
            {
                

                connection.Open();
                command.CommandText = quaryforUpdateDisconnectionDetails;
                command.ExecuteNonQuery();
                connection.Close();
            }

            catch (Exception e)
            {

            }

            finally
            {
                connection.Close();
            }
        }

        public List<ConnectionLog> GetConnectionLog(string accNo)
        {
            string connectionLogquery = string.Format("SELECT DisconnectedDate,ReconnectedDate,OrderCardDate,MeterRemovedDate,FinalizedDate FROM ConnectionLogs WHERE AccountNo = '{0}' ORDER BY DisconnectedDate ASC", accNo);
            List<ConnectionLog> Loglist = new List<ConnectionLog>();
            try
            {
                connection.Open();
                command.CommandText = connectionLogquery;
                reader = command.ExecuteReader();
                while(reader.Read()){
                    ConnectionLog newConnectionLogListItem = new ConnectionLog();
                    newConnectionLogListItem.DisconnectedDate = reader["DisconnectedDate"] != DBNull.Value ? (DateTime)reader["DisconnectedDate"] : DateTime.MinValue;
                    newConnectionLogListItem.ReconnectedDate =  reader["ReconnectedDate"] != DBNull.Value ? (DateTime)reader["ReconnectedDate"] : DateTime.MinValue;
                    newConnectionLogListItem.OrderCardDate = reader["OrderCardDate"] != DBNull.Value ? (DateTime)reader["OrderCardDate"] : DateTime.MinValue;
                    newConnectionLogListItem.MeterRemovedDate = reader["MeterRemovedDate"] != DBNull.Value ? (DateTime)reader["MeterRemovedDate"] : DateTime.MinValue;
                    newConnectionLogListItem.FinalizedDate = reader["FinalizedDate"] != DBNull.Value ? (DateTime)reader["FinalizedDate"] : DateTime.MinValue;
                    Loglist.Add(newConnectionLogListItem);
                }   
            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }
            return Loglist;
        }

       

        public bool UpdateReconnection(string accontNo, string paymentDate, string reconnectedDate, string reconnectedBy, string paymentMethod)
        {
            DateTime ReconnectedDate = DateTime.Parse(reconnectedDate);
            DateTime PaymentDate = DateTime.Parse(paymentDate);
            var connectionLog = new ConnectionLog();
            var affectedRows = 0;
            string query = string.Format("SELECT * FROM ConnectionLogs WHERE AccountNo = '{0}' AND Completness = '0'", accontNo);
            string queryUpdate;
            try
            {
                command.CommandText = query;
                connection.Open();
                reader = command.ExecuteReader();
                //connection.Close();
                ConnectionLog newLog = new ConnectionLog();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        newLog.DisconnectedBy = reader["DisconnectedBy"] != DBNull.Value ? (string)reader["DisconnectedBy"] : "";
                    }

                    if (newLog.DisconnectedBy != "")
                    {
                        queryUpdate = string.Format("UPDATE ConnectionLogs SET ReconnectedDate = '{0}', ReconnectedBy = '{1}', PaymentDate ='{2}', PaymentMode ='{3}', Completness = '1',ThousandListStaus = '0' WHERE AccountNo = '{4}'", ReconnectedDate.ToString("yyyy-MM-dd HH:mm:ss"), reconnectedBy, PaymentDate.ToString("yyyy-MM-dd HH:mm:ss"), paymentMethod, accontNo);

                        try
                        {
                            command.CommandText = queryUpdate;
                            reader.Close();
                            //connection.Open();
                            affectedRows = command.ExecuteNonQuery();
                            var deleteQuery = string.Format("DELETE FROM ReconnectionDetails WHERE AccountNo = '{0}'", accontNo);
                            if (affectedRows > 0)
                            {
                                command.CommandText = deleteQuery;
                                affectedRows = command.ExecuteNonQuery();
                                connection.Close();
                            }
                            //connection.Close();
                            return affectedRows > 0;
                        }
                        catch (Exception e)
                        {

                        }
                        finally
                        {
                            connection.Close();
                        }
                    }

                    else
                    {
                        return false;
                    }
                }
                else
                {
                    query = string.Format("INSERT INTO ConnectionLogs (AccountNo, ReconnectedDate, ReconnectedBy, PaymentDate, PaymentMode, Completness,ThousandListStaus) VALUES('{0}','{1}','{2}','{3}', '{4}','{5}','{6}')", accontNo, ReconnectedDate.ToString("yyyy-MM-dd HH:mm:ss"), reconnectedBy, PaymentDate.ToString("yyyy-MM-dd HH:mm:ss"), paymentMethod, false, 0);
                    var deleteQuery = string.Format("DELETE FROM ReconnectionDetails WHERE AccountNo = '{0}'", accontNo);

                    try
                    {
                        reader.Close();
                        command.CommandText = query;
                        //connection.Open();
                        affectedRows = command.ExecuteNonQuery();
                        //connection.Close();

                        if (affectedRows > 0)
                        {
                            command.CommandText = deleteQuery;
                            affectedRows = command.ExecuteNonQuery();
                            connection.Close();
                        }

                        return affectedRows > 0;
                    }
                    catch (Exception exr)
                    {

                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
            catch (Exception exrt)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        public List<LetterSentDetails> getLetterSentDetails()
        {
            var LetterDetails = new List<LetterSentDetails>();
            string queryLettersentDetails = string.Format("SELECT ConnectionLogs.AccountNo, ConnectionLogs.DisconnectedDate, ConsumerDetails.[Address 1], ConsumerDetails.[Address 2], ConsumerDetails.[Address 3],ConsumerDetails.Depot FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConnectionLogs.AccountNo = ConsumerDetails.[Account No] WHERE ConnectionLogs.LetterStatus = 0 AND ConnectionLogs.Completness = 0 ");
            command.CommandText = queryLettersentDetails;
            
            try
            {
                connection.Open();
                    reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLeterdetail = new LetterSentDetails();
                    newLeterdetail.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    newLeterdetail.DisconnectedDate = (DateTime)reader["DisconnectedDate"];
                    newLeterdetail.AddressLine1= reader["Address 1"] != DBNull.Value ? (string)reader["Address 1"] : "";
                    newLeterdetail.AddressLine2 = reader["Address 2"] != DBNull.Value ? (string)reader["Address 2"] : "";
                    newLeterdetail.AddressLine3 = reader["Address 3"] != DBNull.Value ? (string)reader["Address 3"] : "";
                    newLeterdetail.Depot = reader["Depot"] != DBNull.Value ? (string)reader["Depot"] : "";
                    LetterDetails.Add(newLeterdetail);
                } 
            }
            catch(Exception ex){

            }

            finally
            {
                reader.Close();
                connection.Close();
            }
            return LetterDetails;
        }

        public int getnextID(string Depot, string year)
        {
            string querytogetId = string.Format("SELECT LetterId FROM ConnectionLogs WHERE LetterID LIKE '{0}/{1}/%'", Depot, year);
            command.CommandText = querytogetId;
            int Id =0;
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

        public void UpdateLetterStatus(string accNo)
        {
            string updateQuery = string.Format("UPDATE ConnectionLogs SET LetterStatus = '1' WHERE AccountNo ='{0}' AND Completness = '0' AND LetterStatus = '0'", accNo);
            command.CommandText = updateQuery;
            try
            {
                command.CommandText = updateQuery;
                connection.Open();
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

        public void updateLetterSentdetails(string accNo, string letterID, string letterDate)
        {
            string updatequery = string.Format("UPDATE ConnectionLogs SET LetterId = '{0}',LetterSentDate = '{1}',LetterStatus = {2}, OrderCardStatus = {3} WHERE AccountNo = '{4}' AND LetterStatus = {5}", letterID, letterDate, 2, 0, accNo, 1);
            string DeleteQuery = string.Format("DELETE FROM LettersToBesent WHERE AccountNo = '{0}'", accNo);
            try
            {
                command.CommandText = updatequery;
                connection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    command.CommandText = DeleteQuery;
                    command.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }
        }

        public List<ThousandList> getThousandList(string daylypackNo1,string daylypackNo2)
        {
            List<ThousandList> thousandlist = new List<ThousandList>();
            string thousandQuery = string.Format("SELECT ConnectionLogs.AccountNo, ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq] FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConnectionLogs.AccountNo = ConsumerDetails.[Account No] WHERE  ConnectionLogs.ThousandListStaus = 0 AND ConsumerDetails.[Daily Pack No] BETWEEN {0} AND {1}", daylypackNo1, daylypackNo2);
            try
            {
                command.CommandText = thousandQuery;
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ThousandList newthousandList = new ThousandList();
                    newthousandList.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    newthousandList.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newthousandList.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newthousandList.WalkSeq = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    thousandlist.Add(newthousandList);
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

            return thousandlist;
        }

        public int updateThousandListDetails(string accNo, string BillCycle)
        {
            int affectedrows = 0;
            string quaryUpdate = string.Format("UPDATE ConnectionLogs SET ThousandListStaus = 1, ThousandListDate = '{0}', BillRound = {1} WHERE AccountNo = '{2}' AND ThousandListStaus = {3}", DateTime.Today.ToString("yyyy-MM-dd"), BillCycle, accNo, 0);
            command.CommandText = quaryUpdate;
            try
            {
                connection.Open();
                affectedrows = command.ExecuteNonQuery();
                
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }
            return affectedrows;
        }

        public void insertThousandList(string BillCycle)
        {
            DateTime Date = DateTime.Now;
            string queryInsert = string.Format("INSERT INTO ThousandList (ListDate,BillCircle) VALUES ('{0}',{1})", Date.ToString("yyyy-MM-dd"), BillCycle);
            command.CommandText = queryInsert;
            try
            {
                connection.Open();
                int affectedrows = command.ExecuteNonQuery();
                
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            
        }

        public List<ThousandList> getSavedThousandList(string BillCircle, string Date)
        {
            List<ThousandList> thousandlist = new List<ThousandList>();
            DateTime ListDate = Convert.ToDateTime(Date);
            string thousandQuery = string.Format("SELECT ConnectionLogs.AccountNo, ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq] FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConnectionLogs.AccountNo = ConsumerDetails.[Account No] WHERE  ConnectionLogs.ThousandListStaus = 1 AND ConnectionLogs.BillRound = {0} AND ConnectionLogs.ThousandListDate = '{1}'", BillCircle, ListDate.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                command.CommandText = thousandQuery;
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ThousandList newthousandList = new ThousandList();
                    newthousandList.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    newthousandList.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newthousandList.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newthousandList.WalkSeq = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    thousandlist.Add(newthousandList);
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

            return thousandlist;
        }

        public List<OrderCardDetails> getOrderCardListDetails()
        {
            string getOrderCardQuery = string.Format("SELECT ConnectionLogs.AccountNo AS AccountNo, ConnectionLogs.LetterSentDate, ConnectionLogs.LetterId, ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConsumerDetails.[Cust Fname],  ConsumerDetails.[Cust Lname], ConsumerDetails.[Address 1], ConsumerDetails.[Address 2], ConsumerDetails.[Address 3], ConsumerDetails.Depot FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConnectionLogs.AccountNo = ConsumerDetails.[Account No] WHERE ConnectionLogs.LetterStatus = 2 AND ConnectionLogs.Completness = 0");
            List<OrderCardDetails> OrdercardList = new List<OrderCardDetails>();
            
            try
            {
                command.CommandText = getOrderCardQuery;
                connection.Open();
                reader = command.ExecuteReader();

                while(reader.Read()){
                    OrderCardDetails newOrderCard = new OrderCardDetails();
                    newOrderCard.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    newOrderCard.LetterSentDate = (DateTime)reader["LetterSentDate"];
                    newOrderCard.LetterID = reader["LetterId"] != DBNull.Value ? (string)reader["LetterId"] : "";
                    newOrderCard.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newOrderCard.DailypackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newOrderCard.WalkSeq = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newOrderCard.Fname = reader["Cust Fname"] != DBNull.Value ? (string)reader["Cust Fname"] : "";
                    newOrderCard.Lname = reader["Cust Lname"] != DBNull.Value ? (string)reader["Cust Lname"] : "";
                    newOrderCard.AddressLine1 = reader["Address 1"] != DBNull.Value ? (string)reader["Address 1"] : "";
                    newOrderCard.AddressLine2 = reader["Address 2"] != DBNull.Value ? (string)reader["Address 2"] : "";
                    newOrderCard.AddressLine3 = reader["Address 3"] != DBNull.Value ? (string)reader["Address 3"] : "";
                    newOrderCard.Depot = reader["Depot"] != DBNull.Value ? (string)reader["Depot"] : "";
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

        public int getnextOrderCardID(string Depot, string year)
        {
            string querytogetId = string.Format("SELECT OrderCardID FROM ConnectionLogs WHERE OrderCardID LIKE '{0}/MR/{1}/%'", Depot, year);
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
                        string OrderCardID = (string)reader["OrderCardID"];
                        Id = Int32.Parse(OrderCardID.Substring(OrderCardID.Length - 4));
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

        public void UpdateOrderCardStatus(string accNo)
        {
            string updateQuery = string.Format("UPDATE ConnectionLogs SET OrderCardStatus = '1' WHERE AccountNo ='{0}' AND Completness = '0' AND OrderCardStatus = '0'", accNo);
            command.CommandText = updateQuery;
            try
            {
                command.CommandText = updateQuery;
                connection.Open();
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

        public void CancellOrderCard(string accNo)
        {
            DateTime Rdate = DateTime.MinValue;
            int affectedRows = 0;
            int deletedRows = 0;
            string CancellOrderCardQuery = string.Format("UPDATE ConnectionLogs SET ThousandListStaus = '0', Completness = '1', ReconnectedDate = {0} WHERE AccountNo = '{1}' AND Completness = '0' AND OrderCardStatus = '1'", Rdate.ToString("yyyy-MM-dd"), accNo);
            string DeleteOrderCard = string.Format("DELETE FROM OrderCardList WHERE AccountNo = '{0}'", accNo);
            string querytoUpdateconnection = string.Format("UPDATE Connections SET connectionStatus = '1' WHERE AccountNo = '{0}'", accNo);
            try
            {
                command.CommandText = CancellOrderCardQuery;
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    command.CommandText = DeleteOrderCard;
                    deletedRows = command.ExecuteNonQuery();
                    if (deletedRows > 0)
                    {
                        command.CommandText = querytoUpdateconnection;
                        command.ExecuteNonQuery();
                    }
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

        public int UpdateOrderCardDetails(string accNo, string OrerCardID)
        {
            int affectedRows = 0;
            int DeletedRows = 0;
            DateTime OrderCardDate = DateTime.Today;
            string updateOrderCardDetails = string.Format("UPDATE ConnectionLogs SET OrderCardDate = {0}, OrderCardID = '{1}', OrderCardStatus = 2, MeterRemovedStatus = 0 WHERE Completness = 0 AND AccountNo = '{2}' AND OrderCardStatus = 1", OrderCardDate.ToString("yyyy-MM-dd"), OrerCardID, accNo);
            string DeleteOrderCard = string.Format("DELETE FROM OrderCardList WHERE AccountNo = '{0}'", accNo);
            try
            {
                command.CommandText = updateOrderCardDetails;
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                     command.CommandText = DeleteOrderCard;
                    DeletedRows = command.ExecuteNonQuery();
                }

            }
            catch (Exception EX)
            {

            }

            finally
            {
                connection.Close();
            }

            return DeletedRows;
        }


        public MeterRemovedDate getOdercardDetailsForMeterRemoved(string accNo)
        {
            //8 is assigned as the value of cells which are contained null values. 
            MeterRemovedDate newMeterRemoveDetail = new MeterRemovedDate();
            string getDetailsForOrderCardQuery = string.Format("SELECT ConnectionLogs.LetterStatus, ConnectionLogs.OrderCardStatus, ConnectionLogs.MeterRemovedStatus FROM ConnectionLogs WHERE ConnectionLogs.Completness = 0 AND ConnectionLogs.AccountNo = '{0}'", accNo);
            try
            {
                command.CommandText = getDetailsForOrderCardQuery;
                connection.Open();
                reader = command.ExecuteReader();
                while(reader.Read()){
                    newMeterRemoveDetail.LetterStatus = reader["LetterStatus"] != DBNull.Value ? (int)reader["LetterStatus"] : 8;
                    newMeterRemoveDetail.MeterRemovedStatus = reader["MeterRemovedStatus"] != DBNull.Value ? (int)reader["MeterRemovedStatus"] : 8;
                    newMeterRemoveDetail.OrderCardStatus = reader["OrderCardStatus"] != DBNull.Value ? (int)reader["OrderCardStatus"] : 8;
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

            return newMeterRemoveDetail;
        }

        public int UpdateMeterRemoveDate(string accNo, string date)
        {
            int affectedrows = 0;
            string UpdateQuery = string.Format("UPDATE ConnectionLogs SET MeterRemovedDate = '{0}',MeterRemovedStatus = 1 WHERE ConnectionLogs.AccountNo = '{1}' AND ConnectionLogs.Completness = 0 AND ConnectionLogs.OrderCardStatus = 2 AND ConnectionLogs.MeterRemovedStatus = 0", date, accNo);
            try
            {
                connection.Open();
                command.CommandText = UpdateQuery;
                affectedrows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return affectedrows;
        }

        public List<MeterRemovedAccounDetails> getMeterRemovedDetails()
        {
            List<MeterRemovedAccounDetails> RemovedMeterList = new List<MeterRemovedAccounDetails>();
            string AccountDetailsToBeFinalized = string.Format("SELECT AccountNo, OrderCardDate, MeterRemovedDate,LetterSentDate FROM ConnectionLogs WHERE MeterRemovedStatus = 1 AND OrderCardStatus = 2 AND Completness = 0");
            try
            {
                connection.Open();
                command.CommandText = AccountDetailsToBeFinalized;
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var MeterRemovedAccount = new MeterRemovedAccounDetails();
                    MeterRemovedAccount.AccountNo = reader["AccountNo"] != DBNull.Value ? (string)reader["AccountNo"] : "";
                    MeterRemovedAccount.LetterSentDate = reader["LetterSentDate"] != DBNull.Value ? (DateTime)reader["LetterSentDate"] : DateTime.MinValue;
                    MeterRemovedAccount.OrderCardDate = reader["OrderCardDate"] != DBNull.Value ? (DateTime)reader["OrderCardDate"] : DateTime.MinValue;
                    MeterRemovedAccount.MeterRemovedDate = reader["MeterRemovedDate"] != DBNull.Value ? (DateTime)reader["MeterRemovedDate"] : DateTime.MinValue;
                    RemovedMeterList.Add(MeterRemovedAccount);
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

            return RemovedMeterList;
        }

        public int updateFinalizedDate(string accountNo, string FinalizedDate)
        {
            int affectedRows = 0;
            string updateQuery = string.Format("UPDATE ConnectionLogs SET MeterRemovedStatus = 2, FinalizedDate = '{0}' WHERE AccountNo = '{1}' AND OrderCardStatus = 2 AND MeterRemovedStatus = 1", FinalizedDate, accountNo);
            try
            {
                command.CommandText = updateQuery;
                connection.Open();
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

        public int ActivateAccoount(string accountNo,string ReactiveDate)
        {
            int affectedRows = 0;
            int affectedConstatusRows = 0;
            string ReactivateAccountQuery = string.Format("UPDATE ConnectionLogs SET Completness = 1, ReconnectedDate ={0} WHERE AccountNo = '{1}' AND Completness = 0 AND MeterRemovedStatus = 2", ReactiveDate, accountNo);
            string updateConStatusQuery = string.Format("UPDATE Connections SET connectionStatus = 1 WHERE AccountNo = '{0}'", accountNo);
            try
            {
                connection.Open();
                command.CommandText = ReactivateAccountQuery;
                affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    command.CommandText  = updateConStatusQuery;
                    affectedConstatusRows = command.ExecuteNonQuery();
                }
            }

            catch(Exception ex){

            }

            finally
            {
                connection.Close();
            }

            return affectedConstatusRows;
        }

        public double getTimeSpanBetweenReconnections(string accountNo, DateTime ReconDate) 
        {
            DateTime reconDate = DateTime.MinValue;
            string selectQuery = string.Format("SELECT TOP (1) ReconnectedDate FROM ConnectionLogs WHERE AccountNo = '{0}' ORDER BY ReconnectedDate DESC", accountNo);
            try
            {
                command.CommandText = selectQuery;
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    command.CommandText = selectQuery;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        reconDate = reader["ReconnectedDate"] != DBNull.Value ? (DateTime)reader["ReconnectedDate"] : DateTime.MinValue;
                    }
                }

                else
                {
                    reconDate = DateTime.MinValue;
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

            DateTime t1 = ReconDate;
            DateTime t2 = reconDate;
            TimeSpan time = t1 - t2;
            double ti = time.TotalDays;
            return ti;
        }

        public int getDisconnectionCount(string FromDate, string ToDate)
        {
            string DisconnectionCount = string.Format("SELECT * FROM ConnectionLogs WHERE DisconnectedDate >= CONVERT(datetime,'{0}') and DisconnectedDate<= CONVERT(datetime,'{1}')", FromDate, ToDate);
            int count = 0;
            try
            {
                
                connection.Open();
                command.CommandText = DisconnectionCount;
                reader = command.ExecuteReader();
                if(reader.HasRows){
                    while (reader.Read())
                    {
                        count++;
                    }
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
            return count;
        }

        public int getDisconnectionNotYetReconnectedCount(string FromDate, string ToDate)
        {
            string DisconnectionNotYetReconnectedCount = string.Format("SELECT * FROM ConnectionLogs WHERE DisconnectedDate >= CONVERT(datetime,'{0}') and DisconnectedDate<= CONVERT(datetime,'{1}') AND Completness = 0 AND ReconnectedDate IS NULL", FromDate, ToDate);
            int count = 0;
            try
            {

                connection.Open();
                command.CommandText = DisconnectionNotYetReconnectedCount;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
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
            return count;
        }

        public int getReconnectionCount(string FromDate, string ToDate)
        {
            string ReconnectionCount = string.Format("SELECT * FROM ConnectionLogs WHERE ReconnectedDate >= CONVERT(datetime,'{0}') and ReconnectedDate<= CONVERT(datetime,'{1}')", FromDate, ToDate);
            int count = 0;
            try
            {

                connection.Open();
                command.CommandText = ReconnectionCount;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
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
            return count;
        }

        public int getOrderCardCount(string FromDate, string ToDate)
        {
            string OrderCardCount = string.Format("SELECT * FROM ConnectionLogs WHERE OrderCardDate >= CONVERT(datetime,'{0}') and OrderCardDate<= CONVERT(datetime,'{1}')", FromDate, ToDate);
            int count = 0;
            try
            {

                connection.Open();
                command.CommandText = OrderCardCount;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
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
            return count;
        }

        public int getMeterRemovalCount(string FromDate, string ToDate)
        {
            string MeterRemovalCount = string.Format("SELECT * FROM ConnectionLogs WHERE MeterRemovedDate >= CONVERT(datetime,'{0}') and MeterRemovedDate<= CONVERT(datetime,'{1}')", FromDate, ToDate);
            int count = 0;
            try
            {

                connection.Open();
                command.CommandText = MeterRemovalCount;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
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
            return count;
        }

        public int getFinalizedAccountCount(string FromDate, string ToDate)
        {
            string FinalizedAccountCount = string.Format("SELECT * FROM ConnectionLogs WHERE FinalizedDate >= CONVERT(datetime,'{0}') and FinalizedDate<= CONVERT(datetime,'{1}')", FromDate, ToDate);
            int count = 0;
            try
            {

                connection.Open();
                command.CommandText = FinalizedAccountCount;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
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
            return count;
        }

        public CountReport getCountReportToUI(string FromDate, string ToDate)
        {
            var newCountReport = new CountReport()
            {
            DisconnectionCount = getDisconnectionCount(FromDate, ToDate),
            DisconnectionNotYetReconnectCount = getDisconnectionNotYetReconnectedCount(FromDate, ToDate),
            ReconnectionCount = getReconnectionCount(FromDate, ToDate),
            OrderCardCount = getOrderCardCount(FromDate, ToDate),
            MeterRemovalCount = getMeterRemovalCount(FromDate, ToDate),
            FinalizedAccountCount = getFinalizedAccountCount(FromDate, ToDate)
        };
            return newCountReport;
        }

        public List<LogReports> getDisconnectionDetails(string FromDate,string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string DisconnectionDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.DisconnectedDate, ConnectionLogs.DisconnectedBy FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE DisconnectedDate >= CONVERT(datetime,'{0}') and DisconnectedDate<= CONVERT(datetime,'{1}')", FromDate, EndDate);
            try
            {
                command.CommandText = DisconnectionDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var newLogReport = new LogReports();
                     newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                     newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                     newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                     newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                     newLogReport.DisconnectedDate = reader["DisconnectedDate"] != DBNull.Value ? (DateTime)reader["DisconnectedDate"] : DateTime.MinValue;
                     newLogReport.DisconnectedBy = reader["DisconnectedBy"] != DBNull.Value ? (string)reader["DisconnectedBy"] : "";
                     newList.Add(newLogReport);
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

            return newList;
        }


        public List<LogReports> getReconnectionDetails(string FromDate, string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string ReconnectionDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.ReconnectedDate, ConnectionLogs.ReconnectedBy, ConnectionLogs.PaymentDate, ConnectionLogs.PaymentMode FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE ReconnectedDate >= CONVERT(datetime,'{0}') and ReconnectedDate<= CONVERT(datetime,'{1}') AND ConnectionLogs.ReconnectedBy IS NOT NULL AND ConnectionLogs.ReconnectedDate IS NOT NULL", FromDate, EndDate);
            try
            {
                command.CommandText = ReconnectionDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLogReport = new LogReports();
                    newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                    newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newLogReport.ReconnectedBy = reader["ReconnectedBy"] != DBNull.Value ? (string)reader["ReconnectedBy"] : "";
                    newLogReport.ReconnectedDate = reader["ReconnectedDate"] != DBNull.Value ? (DateTime)reader["ReconnectedDate"] : DateTime.MinValue;
                    newLogReport.PaymentDate = reader["PaymentDate"] != DBNull.Value ? (DateTime)reader["PaymentDate"] : DateTime.MinValue;
                    newLogReport.PaymentMode = reader["PaymentMode"] != DBNull.Value ? (string)reader["PaymentMode"] : "";
                    newList.Add(newLogReport);
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

            return newList;
        }

        public List<LogReports> getLetterDetails(string FromDate, string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string LetterDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.DisconnectedDate, ConnectionLogs.LetterSentDate, ConnectionLogs.LetterId FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE LetterSentDate >= CONVERT(datetime,'{0}') and LetterSentDate<= CONVERT(datetime,'{1}')", FromDate, EndDate);
            try
            {
                command.CommandText = LetterDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLogReport = new LogReports();
                    newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                    newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newLogReport.DisconnectedDate = reader["DisconnectedDate"] != DBNull.Value ? (DateTime)reader["DisconnectedDate"] : DateTime.MinValue;
                    newLogReport.LetterSentDate = reader["LetterSentDate"] != DBNull.Value ? (DateTime)reader["LetterSentDate"] : DateTime.MinValue;
                    newLogReport.LetterId = reader["LetterId"] != DBNull.Value ? (string)reader["LetterId"] : "";
                    newList.Add(newLogReport);
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

            return newList;
        }

        public List<LogReports> getOrderCardDetails(string FromDate, string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string OrderCardDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.OrderCardDate, ConnectionLogs.LetterSentDate, ConnectionLogs.LetterId, ConnectionLogs.OrderCardID FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE OrderCardDate >= CONVERT(datetime,'{0}') and OrderCardDate<= CONVERT(datetime,'{1}')", FromDate, EndDate);
            try
            {
                command.CommandText = OrderCardDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLogReport = new LogReports();
                    newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                    newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newLogReport.OrderCardDate = reader["OrderCardDate"] != DBNull.Value ? (DateTime)reader["OrderCardDate"] : DateTime.MinValue;
                    newLogReport.LetterSentDate = reader["LetterSentDate"] != DBNull.Value ? (DateTime)reader["LetterSentDate"] : DateTime.MinValue;
                    newLogReport.LetterId = reader["LetterId"] != DBNull.Value ? (string)reader["LetterId"] : "";
                    newLogReport.OrderCardID = reader["OrderCardID"] != DBNull.Value ? (string)reader["OrderCardID"] : "";
                    newList.Add(newLogReport);
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

            return newList;
        }

        public List<LogReports> getMeterRemovalDetails(string FromDate, string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string MeterRemovalDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.OrderCardDate, ConnectionLogs.MeterRemovedDate, ConnectionLogs.LetterId, ConnectionLogs.OrderCardID FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE MeterRemovedDate >= CONVERT(datetime,'{0}') and MeterRemovedDate<= CONVERT(datetime,'{1}')", FromDate, EndDate);
            try
            {
                command.CommandText = MeterRemovalDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLogReport = new LogReports();
                    newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                    newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newLogReport.OrderCardDate = reader["OrderCardDate"] != DBNull.Value ? (DateTime)reader["OrderCardDate"] : DateTime.MinValue;
                    newLogReport.LetterSentDate = reader["MeterRemovedDate"] != DBNull.Value ? (DateTime)reader["MeterRemovedDate "] : DateTime.MinValue;
                    newLogReport.LetterId = reader["LetterId"] != DBNull.Value ? (string)reader["LetterId"] : "";
                    newLogReport.OrderCardID = reader["OrderCardID"] != DBNull.Value ? (string)reader["OrderCardID"] : "";
                    newList.Add(newLogReport);
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

            return newList;
        }

        public List<LogReports> getFinalizedAccountDetails(string FromDate, string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string FinalizedAccountDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.OrderCardID, ConnectionLogs.MeterRemovedDate, ConnectionLogs.FinalizedDate FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE FinalizedDate >= CONVERT(datetime,'{0}') and FinalizedDate<= CONVERT(datetime,'{1}')", FromDate, EndDate);
            try
            {
                command.CommandText = FinalizedAccountDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLogReport = new LogReports();
                    newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                    newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newLogReport.MeterRemovedDate = reader["MeterRemovedDate"] != DBNull.Value ? (DateTime)reader["MeterRemovedDate"] : DateTime.MinValue;
                    newLogReport.FinalizedDate = reader["FinalizedDate"] != DBNull.Value ? (DateTime)reader["FinalizedDate"] : DateTime.MinValue;
                    newLogReport.OrderCardID = reader["OrderCardID"] != DBNull.Value ? (string)reader["OrderCardID"] : "";
                    newList.Add(newLogReport);
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

            return newList;
        }

        public List<LogReports> getDisconnectedNotYetReconnectedDetails(string FromDate, string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string DisconnectionDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.DisconnectedDate, ConnectionLogs.DisconnectedBy FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE DisconnectedDate >= CONVERT(datetime,'{0}') and DisconnectedDate<= CONVERT(datetime,'{1}') AND Completness = 0 AND ReconnectedBy IS NULL AND ReconnectedDate IS NULL", FromDate, EndDate);
            try
            {
                command.CommandText = DisconnectionDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLogReport = new LogReports();
                    newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                    newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newLogReport.DisconnectedDate = reader["DisconnectedDate"] != DBNull.Value ? (DateTime)reader["DisconnectedDate"] : DateTime.MinValue;
                    newLogReport.DisconnectedBy = reader["DisconnectedBy"] != DBNull.Value ? (string)reader["DisconnectedBy"] : "";
                    newList.Add(newLogReport);
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

            return newList;
        }

        public List<LogReports> getDisconnectionDetailsToBeAdded(string FromDate, string EndDate)
        {
            List<LogReports> newList = new List<LogReports>();
            string ReconnectionDetails = string.Format("SELECT ConsumerDetails.[Account No], ConsumerDetails.[Reader Code], ConsumerDetails.[Daily Pack No], ConsumerDetails.[Walk Seq], ConnectionLogs.ReconnectedDate, ConnectionLogs.ReconnectedBy, ConnectionLogs.PaymentDate, ConnectionLogs.PaymentMode FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConsumerDetails.[Account No] = ConnectionLogs.AccountNo WHERE ReconnectedDate >= CONVERT(datetime,'{0}') and ReconnectedDate<= CONVERT(datetime,'{1}') AND ConnectionLogs.Completness = 0 AND ConnectionLogs.DisconnectedBy IS NULL AND DisconnectedDate IS NULL", FromDate, EndDate);
            try
            {
                command.CommandText = ReconnectionDetails;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var newLogReport = new LogReports();
                    newLogReport.AccountNo = reader["Account No"] != DBNull.Value ? (string)reader["Account No"] : "";
                    newLogReport.ReaderCode = reader["Reader Code"] != DBNull.Value ? (string)reader["Reader Code"] : "";
                    newLogReport.DailyPackNo = reader["Daily Pack No"] != DBNull.Value ? (string)reader["Daily Pack No"] : "";
                    newLogReport.WalkSequence = reader["Walk Seq"] != DBNull.Value ? (string)reader["Walk Seq"] : "";
                    newLogReport.ReconnectedBy = reader["ReconnectedBy"] != DBNull.Value ? (string)reader["ReconnectedBy"] : "";
                    newLogReport.ReconnectedDate = reader["ReconnectedDate"] != DBNull.Value ? (DateTime)reader["ReconnectedDate"] : DateTime.MinValue;
                    newLogReport.PaymentDate = reader["PaymentDate"] != DBNull.Value ? (DateTime)reader["PaymentDate"] : DateTime.MinValue;
                    newLogReport.PaymentMode = reader["PaymentMode"] != DBNull.Value ? (string)reader["PaymentMode"] : "";
                    newList.Add(newLogReport);
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

            return newList;
        }
    }
}
