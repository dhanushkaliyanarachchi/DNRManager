using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Data.Customer
{
    public class LetterSentDetails
    {
        public string AccountNo { get; set; }
        public DateTime DisconnectedDate { get; set; }
        public int LetterSentStatus { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Depot { get; set; }
    }
}
