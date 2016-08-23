using DNR_Manager.Business;
using DNR_Manager.Business.Models;
using DNR_Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DNR_Manager.Web
{
    public partial class ECity : System.Web.UI.Page
    {
        private ConnectionService connectionService;
        public List<ConnectionLogModel> connectionLog;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionService = new ConnectionService();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string accNo = TextBox1.Text;
            var availabilty = connectionService.checkAvailability(accNo);
            var acountDetail = connectionService.getConsumerDetailstoUI(accNo);
            if (availabilty == true)
            {
                TextBox2.Text = string.Format("{0},\n{1},\n{2}", acountDetail.AddressL1, acountDetail.AddressL2, acountDetail.AddressL3);
                TextBoxWalkOrder.Text = string.Format("{0}/{1}/{2}", acountDetail.ReaderCode, acountDetail.PackNo, acountDetail.WalkSeq);
                if (acountDetail.Status == 1)
                {
                    TextBoxStaus.Text = "Connected";

                }
                else if (acountDetail.Status == 0)
                {
                    TextBoxStaus.Text = "Disconnected";
                    if (acountDetail.DisconnectedBy != null)
                    {
                        TextBoxDisconnectedBy.Text = acountDetail.DisconnectedBy;
                        TextBoxDisconnectedDate.Text = acountDetail.DisconectedDate.ToString("dd/MM/yyyy");
                        TextBoxTime.Text = acountDetail.DisconectedDate.ToString("HH:mm");

                    }
                    else
                    {
                        Label DynamicLable = new Label();
                        DynamicLable.Text = "*Disconnection Details Should Be Entered";
                        divLable.Controls.Add(DynamicLable);
                    }
                }
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('please check whether your Account No is Correct or wrong please insert New Account Details');", true);
            }

        }

        protected void TextBox1_TextChanged1(object sender, EventArgs e)
        {
        }

        protected void btnMoreDetails_Click(object sender, EventArgs e)
        {
            string acc = TextBox1.Text;
            connectionLog = connectionService.gertConnectionLogToTable(acc);
            int listsize = connectionLog.Count;
            if (listsize == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('No previous Disconnection Details');", true);
            }
            //else
            //{
            //    for (int rowCount = 1; rowCount <= listsize; rowCount++)
            //    {
            //        TableRow tRow = new TableRow();
            //        TableMoreDetails.Rows.Add(tRow);

            //        TableCell[] tCell = new TableCell[2];
            //        foreach (var celvalue in newloglistModel)
            //        {
                        
            //            int i = 0;
            //            //tCell[0].Text = rowCount.ToString();
            //            tCell[0].Text = celvalue.DisconnectedDate.ToString("dd/MM/yyyy");
            //            tCell[0].Text = celvalue.ReconnectedDate.ToString("dd/MM/yyyy");
            //            //tRow.Cells.Add(tCell[0]);
            //            tRow.Cells.Add(tCell[0]);
            //            tRow.Cells.Add(tCell[1]);
            //        }


            //    }
            //}//}
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            TextBoxPaymentDetailsAccountNo.Text = TextBox1.Text;
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            string accNo = TextBoxPaymentDetailsAccountNo.Text;
            //DateTime Date = parss(TextBoxPaymentDate.Text,"d", null);
            DateTime Date = DateTime.Parse(TextBoxPaymentDate.Text);
            string Pmethod = DListPaymentMethod.Text;

            ConnectionService newService = new ConnectionService();
            bool updatestatus = newService.setPaymentDetail(accNo, Pmethod, Date);
            if (updatestatus == true)
            {
                newService.setConnectionStatus(accNo);
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Pamentnt Details Updated');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('You have already Entered Payment Details For this Disconnection');", true);
            }
           
        }
    }
}