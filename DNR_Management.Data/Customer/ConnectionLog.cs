using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Data.Customer
{
    public class ConnectionLog
    {
        public int logId { get; set; }
        public string AccountNo { get; set; }
        public DateTime DisconnectedDate { get; set; }
        public DateTime ReconnectedDate { get; set; }
        public DateTime DisconnectedTime { get; set; }
        public string DisconnectedBy { get; set; }
        public string Completness { get; set; }
        public string PaymentMode { get; set; }
        public string ReconnectedBy { get; set; }
        public DateTime LetterSentDate { get; set; }
        public string LetterId { get; set; }
        

        

    }
}
