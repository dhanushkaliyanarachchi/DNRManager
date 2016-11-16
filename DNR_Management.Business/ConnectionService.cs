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
                FirstName = account.FirstName,
                SecondName = account.SecondName,
                OrderCardStatus = connectionLog.OrderCardStatus,
                MeterRemovedStatus = connectionLog.MeterRemovedStatus
            };

            return connectionModel;
        }

        public List<PaymentDetailsModel> GetPaymentDetails()
        {
            var payments = _connectionRepository.GetPaymentDetails();
            var result = new List<PaymentDetailsModel>();

            foreach (var item in payments)
            {
                result.Add(new PaymentDetailsModel { AccountNo = item.AccountNo, PaymentDate = item.PaymentDate, paymentMethod = item.PaymentMethod, status = item.status, AddressLine1 = item.AddressLine1, AddressLine2 = item.AddressLine2, AddressLine3 = item.AddressLine3, Depot = item.Depot,WalkSequence=item.WalkSeq,ReaderCode=item.ReaderCode,DailyPackNo=item.DailyPackNo,ContactNo=item.ContactNo});
            }

            return result;
        }

        public void insertRecoonectionDetails(string accNo, string PaymentDate, string ReconnectedDate, string PaymentMethod, string ReconnectedBy)
        {
            _connectionRepository.insertReconnectionDetails(accNo, PaymentDate, ReconnectedDate, PaymentMethod, ReconnectedBy);
        }
        public bool UpdateReconnection(string accontNo, string paymentDate, string reconnectedDate, string reconnectedBy, string paymentMethod)
        {
            return _connectionLogRepository.UpdateReconnection(accontNo, paymentDate, reconnectedDate, reconnectedBy, paymentMethod);
        }


        public bool setPaymentDetail(string accNo, string pmethod, DateTime Date, string ContactNo)
        {
            return _connectionRepository.AddPaymentDetails(pmethod, accNo, Date, ContactNo); 
            
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
                _connectionRepository.updateConnectionStatus(accNo,0);
                return true;
                
            }

            else
            {
                if (newConnectionLog.DisconnectedDate == null || newConnectionLog.DisconnectedDate==DateTime.MinValue)
                {
                    int lgID = newConnectionLog.logId;
                    _connectionLogRepository.updateDisconnectionDetails(lgID, accNo, DDate, Discconectedby, time);
                    _connectionRepository.updateConnectionStatus(accNo, 1);
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public void setConnectionStatus(string acc,int status)
        {
            _connectionRepository.updateConnectionStatus(acc, status);
        }

        public List<ConnectionLogModel> gertConnectionLogToTable(string accno)
        {
            var log = _connectionLogRepository.GetConnectionLog(accno);
            var newLogModel = new List<ConnectionLogModel>();
            foreach (var item in log)
            {
                newLogModel.Add(new ConnectionLogModel {DisconnectedDate = item.DisconnectedDate, ReconnectedDate = item.ReconnectedDate, OrderCardDate = item.OrderCardDate, MeterRemovedDate = item.MeterRemovedDate, FinalizedDate =item.FinalizedDate });
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
                if(ti>90)
                newLetterSentDetailModel.Add(new LetterSentDetailModel { AccountNo = item.AccountNo, AddressLine1 = item.AddressLine1, AddressLine2 = item.AddressLine2, AddressLine3 = item.AddressLine3, Depot = item.Depot, DisconnectedDate = item.DisconnectedDate});
            }

            return newLetterSentDetailModel;
        }

        public void updateDepetAndWalkSequence(string accountNo, string depot, string readerCode, string dailyPackNo, string walkSequence)
        {
           _connectionRepository.UpdateDepotAndWalkOrder(accountNo,depot,readerCode,dailyPackNo,walkSequence);
        }

        public int insertLetterDetails(string accountNo, string LetterID)
        {
           return _connectionRepository.insertLetterDetailsToTable(accountNo, LetterID);
        }

        public int getnextID(string Depot,string year)
        {
            int maxIdfromConnectionLog = _connectionLogRepository.getnextID(Depot, year);
            int maxIdfromLetterDetails = _connectionRepository.getnextIDfromLetterToBeSent(Depot, year);
            if (maxIdfromConnectionLog > maxIdfromLetterDetails)
            {
                return maxIdfromConnectionLog ++;
            }

            else
            {
                return maxIdfromLetterDetails + 1 ;
            }
        }

        public void updateLetterStatus(string accNo)
        {
            _connectionLogRepository.UpdateLetterStatus(accNo);
        }

        public List<LetterDetailModel> getLetterDetailstoTable()
        {
            var letterDetail = _connectionRepository.getLetterDetailsToUI();
            var result = new List<LetterDetailModel>();
            foreach (var items in letterDetail)
            {
                result.Add(new LetterDetailModel { AccountNo = items.AccountNo, AddressLine1 = items.AddressLine1, AddressLine2 = items.AddressLine2, AddressLine3 = items.AddressLine3, LetterId = items.LetterId});
            }

            return result;
        }

        public void UpdateLetterSentDetails(string accontNo, string LetterID, string LetterDate)
        {
            _connectionLogRepository.updateLetterSentdetails(accontNo, LetterID, LetterDate);
        }

        public List<ThousandListModal> getThousandListToUI(string BillCircle1, string BillCircle2)
        {
            var thousandList = _connectionLogRepository.getThousandList(BillCircle1, BillCircle2);
            var result = new List<ThousandListModal>();
            foreach (var items in thousandList) 
            {
                result.Add(new ThousandListModal {AccountNo = items.AccountNo, DailyPackNo = items.DailyPackNo, ReaderCode = items.ReaderCode, WalkSeq = items.WalkSeq});
            }
            return result;
        }

        public void updateThousndList(string[] id)
        {
            int affectedRows = 0;
           foreach (string s in id)
           {
               string[] accNo = s.Split('/');
               affectedRows = affectedRows + _connectionLogRepository.updateThousandListDetails(accNo[0], accNo[1]); 
           }
           if (affectedRows == id.Length)
           {    
               string[] accNo = id[0].Split('/');
               _connectionLogRepository.insertThousandList(accNo[1]);
           }
        }

        public List<ThousandListDateModel> getThousandListDates()
        {
            var newThaosandListDateModal = new List<ThousandListDateModel>();
            var thousandListDates =_connectionRepository.getThousandListDetails();
            foreach (var items in thousandListDates)
            {
                newThaosandListDateModal.Add(new ThousandListDateModel {ThousandListDates = items.ThousandListDates, BillCircle = items.BillCircle});
            }
            return newThaosandListDateModal;
        }

        public List<ThousandListModal> getSavedThousandListToUI(string BillCircle, string Date)
        {
            var thousandList = _connectionLogRepository.getSavedThousandList(BillCircle, Date);
            var result = new List<ThousandListModal>();
            foreach (var items in thousandList)
            {
                result.Add(new ThousandListModal { AccountNo = items.AccountNo, DailyPackNo = items.DailyPackNo, ReaderCode = items.ReaderCode, WalkSeq = items.WalkSeq });
            }
            return result;
        }

        public int insertConsumertDetails(string accNo, string FName, string LName, string AddressLine1, string AddressLine2, string AddressLine3, string contactNumber, string ReadeCode, string DailyPackNo, string walkSeq,string Depot)
        {
            return _connectionRepository.insertNewAccount(accNo, FName, LName, AddressLine1, AddressLine2, AddressLine3, contactNumber, ReadeCode, DailyPackNo, walkSeq, Depot);
        }

        public void updateConnectionStatus(string accNo)
        {
            _connectionRepository.AddNewconnectionStaus(accNo);
        }

        public List<ContractorListModel> getContractorListToUI()
        {
            var ContractorList = _connectionRepository.GetContractorList();
            var result = new List<ContractorListModel>();
            foreach (var items in ContractorList)
            {
                result.Add(new ContractorListModel { ID = items.ID, DisconnectedBy = items.DisconnectedBy });
            }
            return result;
        }

        public List<ReconnectionModal> getReconnectionModalToUi()
        {
            var result = new List<ReconnectionModal>();
            var reconnection = _connectionRepository.getReconnectionDetails();

            foreach (var item in reconnection)
            {
                result.Add(new ReconnectionModal { AccountNo = item.AccountNo, PaymentDate = item.PaymentDate, PaymentMethod = item.PaymentMethod, ReconnectedDate = item.ReconnectedDate, ReconnectedBy = item.ReconnectedBy, AddressLine1 = item.AddressLine1, AddressLine2 = item.AddressLine2, AddressLine3 = item.AddressLine3, ContactNo = item.ContactNo, DailyPackNo = item.DailyPackNo, Depot = item.Depot, ReaderCode = item.ReaderCode, WalkSeq = item.WalkSeq});
            }

            return result;
        }

        public List<OrderCardListModel> getOrderCardDetails()
        {
            var OrderCardDetails = _connectionLogRepository.getOrderCardListDetails();
            var newOrderCardDetailModel = new List<OrderCardListModel>();
            foreach (var item in OrderCardDetails)
            {
                DateTime t1 = DateTime.Today;
                DateTime t2 = item.LetterSentDate;
                TimeSpan time = t1 - t2;
                double ti = time.TotalDays;
                if (ti >= 14)
                    newOrderCardDetailModel.Add(new OrderCardListModel {AccountNo = item.AccountNo, AddressLine1 = item.AddressLine1, AddressLine2 = item.AddressLine2, AddressLine3 = item.AddressLine3, DailypackNo = item.DailypackNo, Depot = item.Depot, Fname = item.Fname, Lname = item.Lname, LetterID = item.LetterID, LetterSentDate = item.LetterSentDate, ReaderCode = item.ReaderCode, WalkSeq = item.WalkSeq});
            }

            return newOrderCardDetailModel;
        }

        public int getnextOrderCardID(string Depot, string year)
        {
            int maxIdfromConnectionLog = _connectionLogRepository.getnextOrderCardID(Depot, year);
            int maxIdfromLetterDetails = _connectionRepository.getnextIDfromOrderCardList(Depot, year);
            if (maxIdfromConnectionLog > maxIdfromLetterDetails)
            {
                return maxIdfromConnectionLog++;
            }

            else
            {
                return maxIdfromLetterDetails + 1;
            }
        }

        public int insertOrderCardDetails(string accountNo, string OrdecardID, string LetterID)
        {
            return _connectionRepository.insertOrdercardDetailsToTable(accountNo, OrdecardID, LetterID);
        }

        public void UpdateOrderCardStatus(string accNo)
        {
            _connectionLogRepository.UpdateOrderCardStatus(accNo);
        }

        public List<OrderCardListModel> getOrderCardDetailstoUI()
        {
            var OrderCardDetails = _connectionRepository.getOrderCardDetailsToUI();
            var newOrderCardDetailModel = new List<OrderCardListModel>();
            foreach (var item in OrderCardDetails)
            {
                
                    newOrderCardDetailModel.Add(new OrderCardListModel { AccountNo = item.AccountNo, AddressLine1 = item.AddressLine1, AddressLine2 = item.AddressLine2, AddressLine3 = item.AddressLine3, DailypackNo = item.DailypackNo, Depot = item.Depot, Fname = item.Fname, Lname = item.Lname, LetterID = item.LetterID, LetterSentDate = item.LetterSentDate, ReaderCode = item.ReaderCode, WalkSeq = item.WalkSeq, OrderCardID = item.OrderCardID });
            }

            return newOrderCardDetailModel;
        }

        public void CancelOrderCard(string accNo)
        {
            _connectionLogRepository.CancellOrderCard(accNo);
        }

        public int UpdateOrderCard(string accNo, string OrerCardID)
        {
            return _connectionLogRepository.UpdateOrderCardDetails(accNo, OrerCardID);
        }

        public MeterRemoveDetailsModel getMeterRemoveDetails(string accNo)
        {
            var meterRemoveDetails = _connectionLogRepository.getOdercardDetailsForMeterRemoved(accNo);
            var accountDetails = _connectionRepository.GetConsumerAccount(accNo);
            var conStatus = _connectionRepository.GetConnectionStatus(accNo);
            var availability = _connectionRepository.CheckAvailabilityofConsumerDetails(accNo);

            MeterRemoveDetailsModel newModel = new MeterRemoveDetailsModel();
            newModel.availability = availability;
            newModel.ReaderCode = accountDetails.ReaderCode;
            newModel.DailyPackNo = accountDetails.DailypackNo;
            newModel.WalkSeq = accountDetails.WalkSeq;
            newModel.Address1 = accountDetails.AddressLine1;
            newModel.Address2 = accountDetails.AddressLine2;
            newModel.Address3 = accountDetails.AddressLine3;
            newModel.LetterStatus = meterRemoveDetails.LetterStatus;
            newModel.MeterRemovedStatus = meterRemoveDetails.MeterRemovedStatus;
            newModel.OrderCardStatus = meterRemoveDetails.OrderCardStatus;
            newModel.connectionStats = conStatus.connectionStatus;

            return newModel;
        }

        public int UpdateMeterRemovedDate(string accNo, string Date)
        {
            return _connectionLogRepository.UpdateMeterRemoveDate(accNo,Date);
        }

        public List<MeterRemovedAccounListModel> getRemovedMeterList()
        {
            var meterLIst = _connectionLogRepository.getMeterRemovedDetails();
            var newAccountListModel = new List<MeterRemovedAccounListModel>();
            foreach (var item in meterLIst)
            {
                newAccountListModel.Add(new MeterRemovedAccounListModel {AccountNo =item.AccountNo, LetterSentDate = item.LetterSentDate, MeterRemovedDate = item.MeterRemovedDate, OrderCardDate = item.OrderCardDate });
            }

            return newAccountListModel;
        }

        public int UpdateFinalizedDate(string accNo, string FDate) 
        {
            return _connectionLogRepository.updateFinalizedDate(accNo, FDate);
        
        }

        public int UpdateReactivation(string accNo, string reactiveDate)
        {
            return _connectionLogRepository.ActivateAccoount(accNo, reactiveDate);
        }

        public double getTimeSpanBetweenReconnections(string accNo, DateTime Date)
        {
            return _connectionLogRepository.getTimeSpanBetweenReconnections(accNo, Date);
        }

        public int CheckForPaymentDetails(string accNo)
        {
            return _connectionRepository.CheckForPaymentDetails(accNo);
        }

        public CountReportModal getCountModalToUI(string FromDate, string ToDate)
        {
            var CountReport = _connectionLogRepository.getCountReportToUI(FromDate,ToDate);
            var newCountReportModel = new CountReportModal() 
            { 
                DisconnectionCount = CountReport.DisconnectionCount,
                ReconnectionCount = CountReport.ReconnectionCount,
                DisconnectionNotYetReconnectCount = CountReport.DisconnectionNotYetReconnectCount,
                OrderCardCount = CountReport.OrderCardCount,
                MeterRemovalCount = CountReport.MeterRemovalCount,
                FinalizedAccountCount = CountReport.FinalizedAccountCount
            };

            return newCountReportModel;
        }

        public List<LogReportModal> DisconectionReportToUi(string FromDate, string ToDate)
        {
            var DisconnectionDetailsReport = _connectionLogRepository.getDisconnectionDetails(FromDate, ToDate);
            var DisconectionReportModal = new List<LogReportModal>();
            foreach (var item in DisconnectionDetailsReport)
            {
                DisconectionReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, DisconnectedDate = item.DisconnectedDate.ToString("yyyy-MM-dd"), DisconnectedBy = item.DisconnectedBy, DisconnectedTime = item.DisconnectedDate.ToString("HH:mm") });
            }

            return DisconectionReportModal;
        }

        public List<LogReportModal> ReconectionReportToUi(string FromDate, string ToDate)
        {
            var ReconnectionDetailsReport = _connectionLogRepository.getReconnectionDetails(FromDate, ToDate);
            var ReconectionReportModal = new List<LogReportModal>();
            foreach (var item in ReconnectionDetailsReport)
            {
                ReconectionReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, ReconnectedDate = item.ReconnectedDate.ToString("yyyy-MM-dd"), ReconnectedBy = item.ReconnectedBy, PaymentDate = item.PaymentDate.ToString("yyyy-MM-dd"), PaymentMode = item.PaymentMode });
            }
            return ReconectionReportModal; 
        }

        public List<LogReportModal> LetterReportToUi(string FromDate, string ToDate)
        {
            var LetterDetailsReport = _connectionLogRepository.getLetterDetails(FromDate, ToDate);
            var letterReportModal = new List<LogReportModal>();
            foreach (var item in LetterDetailsReport)
            {
                letterReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, DisconnectedDate = item.DisconnectedDate.ToString("yyyy-MM-dd"), LetterSentDate = item.LetterSentDate.ToString("yyyy-MM-dd"), LetterId = item.LetterId });
            }
            return letterReportModal;
        }

        public List<LogReportModal> OrderCardReportToUi(string FromDate, string ToDate)
        {
            var OrderCardDetalsReport = _connectionLogRepository.getOrderCardDetails(FromDate, ToDate);
            var OrderCardReportModal = new List<LogReportModal>();
            foreach (var item in OrderCardDetalsReport)
            {
                OrderCardReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, OrderCardDate = item.OrderCardDate.ToString("yyyy-MM-dd"), LetterSentDate = item.LetterSentDate.ToString("yyyy-MM-dd"), LetterId = item.LetterId, OrderCardID = item.OrderCardID });
            }
            return OrderCardReportModal;
        }

        public List<LogReportModal> MeterRemoveDetailsReportToUi(string FromDate, string ToDate)
        {
            var MeterRemoveDetailsReport = _connectionLogRepository.getOrderCardDetails(FromDate, ToDate);
            var MeterRemoveDetailsReportModal = new List<LogReportModal>();
            foreach (var item in MeterRemoveDetailsReport)
            {
                MeterRemoveDetailsReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, OrderCardDate = item.OrderCardDate.ToString("yyyy-MM-dd"), MeterRemovedDate = item.MeterRemovedDate.ToString("yyyy-MM-dd"), LetterId = item.LetterId, OrderCardID = item.OrderCardID });
            }
            return MeterRemoveDetailsReportModal;
        }

        public List<LogReportModal> FinalizedAccDetailsReportToUi(string FromDate, string ToDate)
        {
            var FinalizedAccDetailReport = _connectionLogRepository.getFinalizedAccountDetails(FromDate, ToDate);
            var FinalizedAccDetailReportModal = new List<LogReportModal>();
            foreach (var item in FinalizedAccDetailReport)
            {
                FinalizedAccDetailReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, OrderCardID = item.OrderCardID, MeterRemovedDate = item.MeterRemovedDate.ToString("yyyy-MM-dd"), FinalizedDate = item.FinalizedDate.ToString("yyyy-MM-dd") });
            }
            return FinalizedAccDetailReportModal;
        }

        public List<LogReportModal> DisconectionNotYetReconnectedReportToUi(string FromDate, string ToDate)
        {
            var DisconnectionDetailsReport = _connectionLogRepository.getDisconnectedNotYetReconnectedDetails(FromDate, ToDate);
            var DisconectionReportModal = new List<LogReportModal>();
            foreach (var item in DisconnectionDetailsReport)
            {
                DisconectionReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, DisconnectedDate = item.DisconnectedDate.ToString("yyyy-MM-dd"), DisconnectedBy = item.DisconnectedBy, DisconnectedTime = item.DisconnectedDate.ToString("HH:mm") });
            }

            return DisconectionReportModal;
        }

        public List<LogReportModal> DisconnectionDetailsTobeAddedReportToUi(string FromDate, string ToDate)
        {
            var ReconnectionDetailsReport = _connectionLogRepository.getDisconnectionDetailsToBeAdded(FromDate, ToDate);
            var ReconectionReportModal = new List<LogReportModal>();
            foreach (var item in ReconnectionDetailsReport)
            {
                ReconectionReportModal.Add(new LogReportModal { AccountNo = item.AccountNo, ReaderCode = item.ReaderCode, DailyPackNo = item.DailyPackNo, WalkSequence = item.WalkSequence, ReconnectedDate = item.ReconnectedDate.ToString("yyyy-MM-dd"), ReconnectedBy = item.ReconnectedBy, PaymentDate = item.PaymentDate.ToString("yyyy-MM-dd"), PaymentMode = item.PaymentMode });
            }
            return ReconectionReportModal;
        }
    }
}

