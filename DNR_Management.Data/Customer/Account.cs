using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Data.Customer
{
    public class Account
    {
        public string AccountNo { get; set; }
        public int ReaderCode { get; set; }
        public int PackNo { get; set; }
        public int Walkseq { get; set; }
        public string Address { get; set; }

        public Account(string accountNo, int readerCode, int packNo, int walkseq, string address)
        {
            this.AccountNo = accountNo;
            this.ReaderCode = readerCode;
            this.PackNo = packNo;
            this.Walkseq = walkseq;
            this.Address = address;
        }
    }
}
