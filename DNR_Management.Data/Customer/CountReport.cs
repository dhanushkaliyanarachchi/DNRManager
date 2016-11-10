using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Data.Customer
{
    public class CountReport
    {
        public int DisconnectionCount { get; set; }
        public int ReconnectionCount { get; set; }
        public int DisconnectionNotYetReconnectCount { get; set; }
        public int OrderCardCount { get; set; }
        public int MeterRemovalCount { get; set; }
        public int FinalizedAccountCount { get; set; }
    }
}
