using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNR_Manager.Models
{
    public class ConnectionModel
    {
        public string AccountNumber { get; set; }
        public string AddressL1 { get; set; }
        public string AddressL2 { get; set; }
        public string AddressL3 { get; set; }
        public string ReaderCode { get; set; }
        public string PackNo { get; set; }
        public string WalkSeq { get; set; }
        public int Status { get; set; }
        public DateTime DisconectedDate { get; set; }
        public string DisconnectedBy { get; set; }
        public DateTime Time { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int OrderCardStatus { get; set; }
        public int MeterRemovedStatus { get; set; }
        public int LetterSentStatus { get; set; }
        //public string WalkOrder { get; set; } walkOder should be loaded when enter an acc no.
    } 
}