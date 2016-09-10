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
    public partial class WebForm2 : System.Web.UI.Page
    {
        private ConnectionService connectionService;
        public List<LetterDetailModel> letterDetailModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionService = new ConnectionService();
            letterDetailModel = connectionService.getLetterDetailstoTable();
        }

        protected void TextBoxDisconnectedDate_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void TextBoxAccountNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string acc = TextBoxAccountNo.Text;
            string DisconectedDate = TextBoxDisconnectedDate.Text;
            string DisconnectedTime = TextBoxDisconnectedTime.Text;
            string DisconnectedBy = TextBoxDisconnectedBy.Text;
            bool status = connectionService.ConnectionLogDetailsToUI(acc, DisconectedDate, DisconnectedBy, DisconnectedTime);


            if (status == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Database Updated');", true);
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('You have already Entered Disconnection Details');", true);
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

        }
    }
}