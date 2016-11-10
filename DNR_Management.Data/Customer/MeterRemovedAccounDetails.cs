using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Data.Customer
{
    public class MeterRemovedAccounDetails
    {
        public string AccountNo { get; set; }
        public DateTime LetterSentDate { get; set; }
        public DateTime OrderCardDate { get; set; }
        public DateTime MeterRemovedDate { get; set; }
    }
}
