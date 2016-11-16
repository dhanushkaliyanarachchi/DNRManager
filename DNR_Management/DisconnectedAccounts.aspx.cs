using System;
using DNR_Manager.Business;
using DNR_Manager.Business.Models;
using DNR_Manager.Models;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace DNR_Manager
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        public static List<LogReportModal> newLogReportModal;
        public static ConnectionService connectionService;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionService = new ConnectionService();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static List<LogReportModal> getReportDetailsToUi(string FromDate, string EndDate)
        {
            newLogReportModal = new List<LogReportModal>();
            newLogReportModal = connectionService.DisconectionNotYetReconnectedReportToUi(FromDate, EndDate);
            return newLogReportModal;
        }
    }
}