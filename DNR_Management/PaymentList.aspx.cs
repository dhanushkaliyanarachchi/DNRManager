using DNR_Manager.Business;
using DNR_Manager.Business.Models;
using DNR_Manager.Models;
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
        public List<ReconnectionModal> reconnctionlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
                Response.Redirect("Default.aspx");

            connectionService = new ConnectionService();
            paymentDetailModel = connectionService.GetPaymentDetails();
            reconnctionlist = connectionService.getReconnectionModalToUi();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void insertReconnectionDetails(string accountNo, string address, string paymentDate, string reconnectedDate, string disconnectedBy, string PaymentMode)
        {
            connectionService = new ConnectionService();
            connectionService.insertRecoonectionDetails(accountNo, paymentDate, reconnectedDate, PaymentMode, disconnectedBy);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void UpdatePayment(string accountNo, string address, string paymentDate, string reconnectedDate, string disconnectedBy, string PaymentMode)
        {
            
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void UpdateDepot(string accountNo, string depot, string readerCode, string dailyPackNo, string walkSequence)
        {
            connectionService = new ConnectionService();
            connectionService.updateDepetAndWalkSequence(accountNo, depot, readerCode, dailyPackNo, walkSequence);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void UpdatePayment(List<ReconnectionRecord> rows)
        {
            connectionService = new ConnectionService();
            foreach (var item in rows)
            {
                connectionService = new ConnectionService();
                var affectedRows = connectionService.UpdateReconnection(item.AccountNo, item.PaymentDate, item.ReconnectedDate, item.ReconectedBy, item.PaymentMode);
                if (affectedRows == true)
                {
                    connectionService.setConnectionStatus(item.AccountNo, 1);
                }
            
            }
            
        }
    }
}