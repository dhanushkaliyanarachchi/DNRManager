using DNR_Manager.Data.Customer.Interfaces;
using System;
using System.Collections.Generic;
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
            DisconnectedDate.Date.AddHours(double.Parse(time[0])).AddMinutes(double.Parse(time[1]));

            string quaryforUpdateDisconnectionDetails = string.Format("UPDATE ConnectionLogs SET DisconnectedDate = '{0}',DisconnectedBy ='{1}',Completness = '{2}' WHERE LogID = '{3}' AND AccountNo = '{4}'", DisconnectedDate,  DisconnectedBy,0, LogID, Accountno);
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
        }

        public List<ConnectionLog> GetConnectionLog(string accNo)
        {
            string connectionLogquery = string.Format("SELECT DisconnectedDate,ReconnectedDate FROM ConnectionLogs WHERE Completness = 1 AND AccountNo = '{0}'",accNo);
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

        //public void insertReconnectionDetails(string accNo, string ReconnectedDate, string ReconnectedBy,string PaymentMethod)
        //{
        //    int completeness = 0;
        //    DateTime RDate = DateTime.Parse(ReconnectedDate);
        //    string quaryforinsertReconnectionDetails = string.Format("INSERT INTO ConnectionLogs (AccountNo,ReconnectedDate,ReconnectedBy,PaymentDate,PaymentMode,Completness) Values ('{0}','{1}','{2}','{3}','{4}',{5})", accNo, RDate.ToString("yyyy-MM-dd HH:mm"), ReconnectedBy, PaymentMethod, completeness);
        //    try
        //    {
        //        connection.Open();
        //        command.CommandText = quaryforinsertReconnectionDetails;
        //        command.ExecuteNonQuery();
        //    }

        //    catch(Exception ex){

        //    }

        //    finally
        //    {

        //    }
        //}

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

                if (reader.HasRows)
                {
                    queryUpdate = string.Format("UPDATE ConnectionLogs SET ReconnectedDate = '{0}', ReconnectedBy = '{1}', PaymentDate ='{2}', PaymentMode ='{3}', Completness = '1' WHERE AccountNo = '{4}'", reconnectedDate, reconnectedBy, PaymentDate.ToString("yyyy-MM-dd HH:mm:ss"), paymentMethod, accontNo);

                    try
                    {
                        command.CommandText = queryUpdate;
                        reader.Close();
                        //connection.Open();
                        affectedRows = command.ExecuteNonQuery();
                        var deleteQuery = string.Format("DELETE FROM PaymentDetails WHERE AccountNo = '{0}'", accontNo);
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
                    query = string.Format("INSERT INTO ConnectionLogs (AccountNo, ReconnectedDate, ReconnectedBy, PaymentDate, PaymentMode, Completness) VALUES('{0}','{1}','{2}','{3}', '{4}','{5}')", accontNo, reconnectedDate, reconnectedBy, PaymentDate.ToString("yyyy-MM-dd HH:mm:ss"), paymentMethod, false);
                    var deleteQuery = string.Format("DELETE FROM PaymentDetails WHERE AccountNo = '{0}'", accontNo);

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
            string queryLettersentDetails = string.Format("SELECT ConnectionLogs.AccountNo, ConnectionLogs.DisconnectedDate, ConsumerDetails.[Address 1], ConsumerDetails.[Address 2], ConsumerDetails.[Address 3],ConsumerDetails.Depot WHERE ConnectionLogs.LetterStatus = 0 AND ConnectionLogs.Completness = 0 FROM ConnectionLogs INNER JOIN ConsumerDetails ON ConnectionLogs.AccountNo = ConsumerDetails.[Account No]");
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
                } 
            }
            catch(Exception ex){

            }

            finally
            {
                connection.Close();
            }
            return LetterDetails;
        }

        

    }
}
