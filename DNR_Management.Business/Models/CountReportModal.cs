using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business.Models
{
    public class CountReportModal
    {
        public int DisconnectionCount { get; set; }
        public int ReconnectionCount { get; set; }
        public int DisconnectionNotYetReconnectCount { get; set; }
        public int OrderCardCount { get; set; }
        public int MeterRemovalCount { get; set; }
        public int FinalizedAccountCount { get; set; }
    }
}
