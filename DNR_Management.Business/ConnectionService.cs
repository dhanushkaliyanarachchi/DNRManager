using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DNR_Manager.Data.Customer.Repositories;
using DNR_Manager.Models;
using DNR_Manager.Business.Models;

namespace DNR_Manager.Business
{
    public class ConnectionService
    {
        private ConnectionRepository _connectionRepository;
        private ConnectionLogReporsitory _connectionLogRepository;
        public ConnectionService()
        {
            _connectionRepository = new ConnectionRepository();
            _connectionLogRepository = new ConnectionLogReporsitory();
        }

        public bool checkAvailability(string accNo)
        {
           return _connectionRepository.CheckAvailabilityofConsumerDetails(accNo);
        }

        public ConnectionModel getConsumerDetailstoUI(string accNo)
        {
            var account = _connectionRepository.GetConsumerAccount(accNo);
            var connectionLog = _connectionRepository.GetConnectionLog(accNo);
            var connectionStatus = _connectionRepository.GetConnectionStatus(accNo);

            var connectionModel = new ConnectionModel()
            {
                AddressL1 = account.AddressLine1,
                AddressL2 = account.AddressLine2,
                AddressL3 = account.AddressLine3,
                ReaderCode = account.ReaderCode,
                PackNo = account.DailypackNo,
                WalkSeq = account.WalkSeq,
                DisconnectedBy = connectionLog.DisconnectedBy,
                DisconectedDate = connectionLog.DisconnectedDate,
                Status = connectionStatus.connectionStatus,
            };

            return connectionModel;
        }

        public List<PaymentDetailsModel> GetPaymentDetails()
        {
            var payments = _connectionRepository.GetPaymentDetails();
            var result = new List<PaymentDetailsModel>();

            foreach (var item in payments)
            {
                result.Add(new PaymentDetailsModel { AccountNo = item.AccountNo, PaymentDate = item.PaymentDate, paymentMethod = item.PaymentMethod, AddressLine1 = item.AddressLine1, AddressLine2 = item.AddressLine2, AddressLine3 = item.AddressLine3, Depot = item.Depot,WalkSequence=item.WalkSeq,ReaderCode=item.ReaderCode,DailyPackNo=item.DailyPackNo});
            }

            return result;
        }

        public bool UpdateReconnection(string accontNo, string paymentDate, string reconnectedDate, string reconnectedBy, string paymentMethod)
        {
            return _connectionLogRepository.UpdateReconnection(accontNo, paymentDate, reconnectedDate, reconnectedBy, paymentMethod);
        }


        public bool setPaymentDetail(string accNo, string pmethod, DateTime Date)
        {
            return _connectionRepository.AddPaymentDetails(pmethod, accNo, Date); 
            
        }

        public bool ConnectionLogDetailsToUI(string accNo,string DisconnectedDate,string DisconnectedBy,string DisconnectedTime)
        {
            string acc = accNo;
            string DDate = DisconnectedDate;
            string Discconectedby=DisconnectedBy;
            string time = DisconnectedTime;
            var newConnectionLog = _connectionLogRepository.GetConnectionLogDetailsForAvailabilityofLogEntry(acc);
            
            if(newConnectionLog.logId == 0)
            {
                _connectionLogRepository.insertDisconnectionDetails(acc, DDate,time,Discconectedby);
                _connectionRepository.updateConnectionStatus(accNo);
                return true;
                
            }

            else
            {
                if (newConnectionLog.DisconnectedDate == null)
                {
                    int lgID = newConnectionLog.logId;
                    _connectionLogRepository.updateDisconnectionDetails(lgID, accNo, DDate, Discconectedby, time);
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public void setConnectionStatus(string acc)
        {
            _connectionRepository.updateConnectionStatus(acc);
        }

        public List<ConnectionLogModel> gertConnectionLogToTable(string accno)
        {
            var log = _connectionLogRepository.GetConnectionLog(accno);
            var newLogModel = new List<ConnectionLogModel>();
            foreach (var item in log)
            {
                newLogModel.Add(new ConnectionLogModel {DisconnectedDate = item.DisconnectedDate, ReconnectedDate = item.ReconnectedDate });
            }

            return newLogModel;
        }

        public List<LetterSentDetailModel> getLetterDetails()
        {
            var Letterdetail = _connectionLogRepository.getLetterSentDetails();
            var newLetterSentDetailModel = new List<LetterSentDetailModel>();
            foreach (var item in Letterdetail)
            {
                DateTime t1 = DateTime.Today;
                DateTime t2 = item.DisconnectedDate;
                TimeSpan time = t1-t2;
                double ti = time.TotalDays;
                if(ti>30)
                newLetterSentDetailModel.Add(new LetterSentDetailModel { AccountNo = item.AccountNo, AddressLine1 = item.AddressLine1, AddressLine2 = item.AddressLine2, AddressLine3 = item.AddressLine3, Depot = item.Depot, DisconnectedDate = item.DisconnectedDate});
            }

            return newLetterSentDetailModel;
        }

        public void updateDepetAndWalkSequence(string accountNo, string depot, string readerCode, string dailyPackNo, string walkSequence)
        {
           _connectionRepository.UpdateDepotAndWalkOrder(accountNo,depot,readerCode,dailyPackNo,walkSequence);
        }
    }
}

