using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business.Models
{
    public class PaymentDetailsModel
    {
        public string AccountNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public string paymentMethod { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Depot { get; set; }
        public string WalkSequence { get; set; }
        public string ReaderCode { get; set; }
        public string DailyPackNo { get; set; }

    }
}
