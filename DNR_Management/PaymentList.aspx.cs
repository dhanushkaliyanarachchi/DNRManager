using DNR_Manager.Business;
using DNR_Manager.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DNR_Manager
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        public static ConnectionService connectionService;
        public List<PaymentDetailsModel> paymentDetailModel;
        protected void Page_Load(object sender, EventArgs e)
        {

            connectionService = new ConnectionService();
            paymentDetailModel = connectionService.GetPaymentDetails();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void UpdatePayment(string accountNo, string address, string paymentDate, string reconnectedDate, string disconnectedBy, string PaymentMode)
        {
            connectionService = new ConnectionService();
            var affectedRows = connectionService.UpdateReconnection(accountNo, paymentDate, reconnectedDate, disconnectedBy, PaymentMode);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void UpdateDepot(string accountNo, string depot, string readerCode, string dailyPackNo, string walkSequence)
        {
            connectionService = new ConnectionService();
            connectionService.updateDepetAndWalkSequence(accountNo, depot, readerCode, dailyPackNo, walkSequence);
        }
    }
}