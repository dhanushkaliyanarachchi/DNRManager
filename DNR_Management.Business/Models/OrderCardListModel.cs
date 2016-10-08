using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business.Models
{
    public class OrderCardListModel
    {
        public string AccountNo { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string LetterID { get; set; }
        public DateTime LetterSentDate { get; set; }
        public string ReaderCode { get; set; }
        public string DailypackNo { get; set; }
        public string WalkSeq { get; set; }
        public string Depot { get; set; }
        public string OrderCardID { get; set; }
    }
}
