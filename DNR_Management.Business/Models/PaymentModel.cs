using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business.Models
{
    public class PaymentModel
    {
        public string AccountNo { get; set; }
        public string Address { get; set; }
        public string PaymentDate { get; set; }
        public string ReconnectedDate { get; set; }
        public string DisconnectedBy { get; set; }
    }
}
