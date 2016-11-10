using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business.Models
{
    public class MeterRemovedAccounListModel
    {
        public string AccountNo { get; set; }
        public DateTime LetterSentDate { get; set; }
        public DateTime OrderCardDate { get; set; }
        public DateTime MeterRemovedDate { get; set; }
    }
}
