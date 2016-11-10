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
    public partial class WebForm6 : System.Web.UI.Page
    {
        public static ConnectionService connectionService;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionService = new ConnectionService();
            if (!IsPostBack)
            {
                DateTime DateNow = DateTime.Today();
                var CountReportModelToUi = new CountReportModal();
                CountReportModelToUi = connectionService.getCountModalToUI(FromDate, EndDate);
                return CountReportModelToUi;
            }

            else
            {

            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static CountReportModal getReportDetailsToUi(string FromDate, string EndDate)
        {
            var  CountReportModelToUi = new CountReportModal();
            CountReportModelToUi = connectionService.getCountModalToUI(FromDate, EndDate);
            return CountReportModelToUi;
        }
    }
}