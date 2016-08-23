using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNR_Manager.Business;


namespace DNR_Manager
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private ConnectionService connectionService;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionService = new ConnectionService(); 
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
    }
}