using DNR_Manager.Business;
using DNR_Manager.Business.Models;
using DNR_Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNR_Manager.Models;

namespace DNR_Manager
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public static ConnectionService connectionService;
        public List<LetterDetailModel> letterDetailModel;
        public static List<ThousandListModal> thousandList;
        public List<ThousandListDateModel> ThousandDates;
        public List<ContractorListModel> contractorListModal;
        public List<OrderCardListModel> OrderCardList;
        public List<MeterRemovedAccounListModel> RemovedMeterList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if ((int)Session["User_Level"] != (int)UserType.Admin)
                Response.Redirect("Default.aspx");

            connectionService = new ConnectionService();
            letterDetailModel = connectionService.getLetterDetailstoTable();
            ThousandDates = connectionService.getThousandListDates();
            OrderCardList = connectionService.getOrderCardDetailstoUI();
            RemovedMeterList = connectionService.getRemovedMeterList();
            if (!IsPostBack)
            {
                contractorListModal = connectionService.getContractorListToUI();
                DisconnectedByList.DataSource = contractorListModal;
                DisconnectedByList.DataTextField = "DisconnectedBy";
                DisconnectedByList.DataBind();
            }

        }

        protected void TextBoxDisconnectedDate_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void TextBoxAccountNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            connectionService = new ConnectionService();
            string acc = TextBoxAccountNo.Text;
            string DisconectedDate = TextBoxDisconnectedDate.Text;
            string DisconnectedTime = TextBoxDisconnectedTime.Text;
            string DisconnectedBy = DisconnectedByList.Text;
            bool availability = connectionService.checkAvailability(acc);
            if (availability == true)
            {
                if (DisconectedDate != "")
                {
                    if (DisconnectedTime != "")
                    {
                        if (DisconnectedBy == "Select Disconnector")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Select Valid Disconnector');", true);
                        }
                        else
                        {
                            bool status = connectionService.ConnectionLogDetailsToUI(acc, DisconectedDate, DisconnectedBy, DisconnectedTime);


                            if (status == true)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Database Updated');", true);
                                TextBoxAccountNo.Text = string.Empty;
                                TextBoxDisconnectedDate.Text = string.Empty;
                                TextBoxDisconnectedTime.Text = string.Empty;
                                DisconnectedByList.SelectedIndex = 0;

                            }

                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('You have already Entered Disconnection Details');", true);
                                TextBoxAccountNo.Text = string.Empty;
                                TextBoxDisconnectedDate.Text = string.Empty;
                                TextBoxDisconnectedTime.Text = string.Empty;
                                DisconnectedByList.SelectedIndex = 0;
                            }

                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('please Enter Disconnected time.');", true);
                    }
                }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('please Enter Disconnected Date.');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Pleace check whether your account Number is correct or wrong, if it is correct Please add account details.');", true);
            }
        }

        protected void generateLetterList_Click(object sender, EventArgs e)
        {
            connectionService = new ConnectionService();
            var letterDetails = connectionService.getLetterDetails();
            foreach (var item in letterDetails)
            {
                string letterID;
                string accNo = item.AccountNo;
                
                if (item.Depot == "Fullerton")
                {
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextID("F", currentYear);
                    letterID = "F/" +currentYear+ "/" +id.ToString("0000")+ "";
                    int affctedrows = connectionService.insertLetterDetails(accNo, letterID);
                    if (affctedrows > 0)
                    {
                        connectionService.updateLetterStatus(accNo);
                    }
                }
                else if (item.Depot == "Mathugama")
                {
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextID("M", currentYear);
                    letterID = "M/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertLetterDetails(accNo, letterID);
                    if (affctedrows > 0)
                    {
                        connectionService.updateLetterStatus(accNo);
                    }
                }

                else if (item.Depot == "Beruwala")
                {
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextID("B", currentYear);
                    letterID = "B/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertLetterDetails(accNo, letterID);
                    if (affctedrows > 0)
                    {
                        connectionService.updateLetterStatus(accNo);
                    }
                }

                else if (item.Depot == "Panadura")
                {
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextID("P", currentYear);
                    letterID = "P/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertLetterDetails(accNo, letterID);
                    if (affctedrows > 0)
                    {
                        connectionService.updateLetterStatus(accNo);
                    }
                }

                else if (item.Depot == "Agalawatta")
                {
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextID("A", currentYear);
                    letterID = "A/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertLetterDetails(accNo, letterID);
                    if (affctedrows > 0)
                    {
                        connectionService.updateLetterStatus(accNo);
                    }
                }

            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void updateLetterDate(string accountNo, string letterID, string sentDate)
        {
            connectionService = new ConnectionService();
            connectionService.UpdateLetterSentDetails(accountNo, letterID, sentDate);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static List<ThousandListModal> GenerateThousandList(string circleNo)
        {
            connectionService = new ConnectionService();
            thousandList = new List<ThousandListModal>();
            if(circleNo == "1"){
                thousandList = connectionService.getThousandListToUI("1", "6");
            }

            if (circleNo == "2")
            {
                thousandList = connectionService.getThousandListToUI("7", "12");
            }

            if (circleNo == "3")
            {
                thousandList = connectionService.getThousandListToUI("13", "18");
            }

            if (circleNo == "4")
            {
                thousandList = connectionService.getThousandListToUI("19", "24");
            }

            return thousandList;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void SaveThousandsList(string[] ids)
        {
            connectionService = new ConnectionService();
            connectionService.updateThousndList(ids);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static List<ThousandListModal> GenerateSavedThousandList(string BillCircle,string Date)
        {
            connectionService = new ConnectionService();
            thousandList = new List<ThousandListModal>();
            thousandList = connectionService.getSavedThousandListToUI(BillCircle, Date);
            return thousandList;
        }

        //StringBuilder StrHtmlGenerate = new StringBuilder();
        //StringBuilder StrExport = new StringBuilder();

        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        //    StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        //    StrExport.Append("<DIV  style='font-size:12px;'>");
        //    StrExport.Append(dvInfo.InnerHtml);
        //    StrExport.Append("</div></body></html>");
        //    string strFile = "StudentInformations_CODESCRATCHER.xls";
        //    string strcontentType = "application/excel";
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.BufferOutput = true;
        //    Response.ContentType = strcontentType;
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        //    Response.Write(StrExport.ToString());
        //    Response.Flush();
        //    Response.Close();
        //    Response.End();
        //}

        protected void generateOrderCardList_Click(object sender, EventArgs e)
        {
            connectionService = new ConnectionService();
            var OrderCardListDetails = connectionService.getOrderCardDetails();
            foreach (var item in OrderCardListDetails)
            {
                string OrdeCardID;
                string accNo = item.AccountNo;

                if (item.Depot == "Fullerton")
                {
                    string LetterID = item.LetterID;
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextOrderCardID("514.10", currentYear);
                    OrdeCardID = "514.10/MR/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertOrderCardDetails(accNo, OrdeCardID, LetterID);
                    if (affctedrows > 0)
                    {
                        connectionService.UpdateOrderCardStatus(accNo);
                    }
                }
                else if (item.Depot == "Mathugama")
                {
                    string LetterID = item.LetterID;
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextOrderCardID("514.20", currentYear);
                    OrdeCardID = "514.20/MR/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertOrderCardDetails(accNo, OrdeCardID, LetterID);
                    if (affctedrows > 0)
                    {
                        connectionService.UpdateOrderCardStatus(accNo);
                    }
                }

                else if (item.Depot == "Beruwala")
                {
                    string LetterID = item.LetterID;
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextOrderCardID("514.30", currentYear);
                    OrdeCardID = "514.30/MR/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertOrderCardDetails(accNo, OrdeCardID, LetterID);
                    if (affctedrows > 0)
                    {
                        connectionService.UpdateOrderCardStatus(accNo);
                    }
                }

                else if (item.Depot == "Panadura")
                {
                    string LetterID = item.LetterID;
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextOrderCardID("514.40", currentYear);
                    OrdeCardID = "514.40/MR/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertOrderCardDetails(accNo, OrdeCardID, LetterID);
                    if (affctedrows > 0)
                    {
                        connectionService.UpdateOrderCardStatus(accNo);
                    }
                }
                else if (item.Depot == "Agalawatta")
                {
                    string LetterID = item.LetterID;
                    string currentYear = DateTime.Today.Year.ToString();
                    int id = connectionService.getnextOrderCardID("514.50", currentYear);
                    OrdeCardID = "514.50/MR/" + currentYear + "/" + id.ToString("0000") + "";
                    int affctedrows = connectionService.insertOrderCardDetails(accNo, OrdeCardID, LetterID);
                    if (affctedrows > 0)
                    {
                        connectionService.UpdateOrderCardStatus(accNo);
                    }
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void RemoveOrderCard(string accountNo)
        {
            connectionService = new ConnectionService();
            connectionService.CancelOrderCard(accountNo);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void UpdateOrdercardDetails(List<OrderCardModel> rows)
        {
            connectionService = new ConnectionService();
            foreach(var items in rows){
                connectionService.UpdateOrderCard(items.AccountNo, items.OrderCardID);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static MeterRemoveDetailsModel UpdateMeterRemoveDetails(string accountNo)
        {
            connectionService = new ConnectionService();
            var MeterRemoveDetailModel = connectionService.getMeterRemoveDetails(accountNo);
            return MeterRemoveDetailModel;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static int UpdateMeterRemoveDate(string accountNo, string MRDate)
        {
            connectionService = new ConnectionService();
            int response = connectionService.UpdateMeterRemovedDate(accountNo, MRDate);
            return response;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static int UpdateFinalizedDate(string accountNo, string finalizedDate)
        {
            connectionService = new ConnectionService();
            int response = connectionService.UpdateFinalizedDate(accountNo, finalizedDate);
            return response;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static int UpdateReactivation(string accountNo, string RconnectedDate)
        {
            connectionService = new ConnectionService();
            return connectionService.UpdateReactivation(accountNo, RconnectedDate);
        }

    }
}