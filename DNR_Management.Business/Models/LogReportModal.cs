using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business.Models
{
    public class LogReportModal
    {
        public string AccountNo { get; set; }
        public string DisconnectedDate { get; set; }
        public string ReconnectedDate { get; set; }
        public string DisconnectedTime { get; set; }
        public string DisconnectedBy { get; set; }
        public string Completness { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string ReconnectedBy { get; set; }
        public string LetterSentDate { get; set; }
        public string LetterId { get; set; }
        public string OrderCardDate { get; set; }
        public string OrderCardID { get; set; }
        public string MeterRemovedDate { get; set; }
        public int OrderCardStatus { get; set; }
        public int MeterRemovedStatus { get; set; }
        public int LetterSentStatus { get; set; }
        public string FinalizedDate { get; set; }
        public string ReaderCode { get; set; }
        public string DailyPackNo { get; set; }
        public string WalkSequence { get; set; }
    }
}
