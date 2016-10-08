using DNR_Manager.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DNR_Manager
{
    public partial class DNR_Manager_Login : System.Web.UI.MasterPage
    {

        private UserService userService;
        protected void Page_Load(object sender, EventArgs e)
        {
            userService = new UserService();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string userName = UserName.Text;
            string password = Password.Text;

            var status = userService.GetUser(userName, password);
            if (status != 0)
            {
                Session["User_Id"] = "userName";
                Session["User_Level"] = status;
                Response.Redirect("ECity.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            } 
        }
    }
}