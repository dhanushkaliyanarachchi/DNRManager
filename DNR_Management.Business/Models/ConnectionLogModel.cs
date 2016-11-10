using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business.Models
{
    public class ConnectionLogModel
    {
        public string AccountNo { get; set; }
        public int logId { get; set; }
        public DateTime DisconnectedDate { get; set; }
        public DateTime ReconnectedDate { get; set; }
        public string ReconnectedBy { get; set; }
        public string DisconnectedBy { get; set; }
        public DateTime OrderCardDate { get; set; }
        public DateTime MeterRemovedDate { get; set; }
        public DateTime FinalizedDate { get; set; }

    }
}
