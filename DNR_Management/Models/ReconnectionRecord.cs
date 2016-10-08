using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNR_Manager.Models
{
    public class ReconnectionRecord
    {
        public string AccountNo { get; set; }
        public string Address { get; set; }
        public string WalkOrder { get; set; }
        public string ContactNo { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string ReconnectedDate { get; set; }
        public string ReconectedBy { get; set; }
    }
}