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
            if (Session["User_Id"] == null)
                Response.Redirect("Default.aspx");
            connectionService = new ConnectionService();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
            TextBoxWalkOrder.Text = "";
            TextBoxStaus.Text = "";
            TextBoxDisconnectedBy.Text = "";
            TextBoxDisconnectedDate.Text = "";
            TextBoxTime.Text = "";

            string accNo = TextBox1.Text;
            if (accNo != "")
            {
                var availabilty = connectionService.checkAvailability(accNo);
                var acountDetail = connectionService.getConsumerDetailstoUI(accNo);
                if (availabilty == true)
                {
                    TextBox2.Text = string.Format("{0} {1},\n{2},\n{3},\n{4}",acountDetail.FirstName,acountDetail.SecondName, acountDetail.AddressL1, acountDetail.AddressL2, acountDetail.AddressL3);
                    TextBoxWalkOrder.Text = string.Format("{0}/{1}/{2}", acountDetail.ReaderCode, acountDetail.PackNo, acountDetail.WalkSeq);
                    if (acountDetail.Status == 1)
                    {
                        TextBoxStaus.Text = "Connected";

                    }
                    else if (acountDetail.Status == 0)
                    {
                        if (acountDetail.DisconnectedBy == null)
                        {
                            Label DynamicLable = new Label();
                            DynamicLable.Text = "*Disconnection and Reconnection Details Should Be Entered";
                            divLable.Controls.Add(DynamicLable);
                            TextBoxStaus.Text = "Disconnected";
                        }
                        else if (acountDetail.DisconnectedBy != null && acountDetail.DisconnectedBy != "")
                        {
                            if (acountDetail.LetterSentStatus == 0 || acountDetail.LetterSentStatus == 1)
                            {
                                TextBoxStaus.Text = "Disconnected";
                            }
                            else if (acountDetail.LetterSentStatus == 2 && (acountDetail.OrderCardStatus == 0 || acountDetail.OrderCardStatus == 1))
                            {
                                TextBoxStaus.Text = "Disconnected/LetterSent";
                            }
                            else if (acountDetail.OrderCardStatus == 2 && acountDetail.MeterRemovedStatus == 0)
                            {
                                TextBoxStaus.Text = "Disconnected/OrderCardSent";
                            }

                            else if (acountDetail.MeterRemovedStatus == 1)
                            {
                                TextBoxStaus.Text = "Disconnected/MeterRemoved";
                            }

                            else if (acountDetail.MeterRemovedStatus == 2)
                            {
                                TextBoxStaus.Text = "Disconnected/Finalized";
                            }

                            TextBoxDisconnectedBy.Text = acountDetail.DisconnectedBy;
                            TextBoxDisconnectedDate.Text = acountDetail.DisconectedDate.ToString("dd/MM/yyyy");
                            TextBoxTime.Text = acountDetail.DisconectedDate.ToString("HH:mm");
                        }

                        else if (acountDetail.DisconnectedBy == "")
                        {
                            Label DynamicLable = new Label();
                            DynamicLable.Text = "*Disconnection Details Should Be Entered";
                            divLable.Controls.Add(DynamicLable);
                        }

                    }

                    //else if (acountDetail.Status == 0)
                    //{
                    //    if (acountDetail.OrderCardStatus == 8) { 
                    //    TextBoxStaus.Text = "Disconnected";
                    //    if (acountDetail.DisconnectedBy != "")
                    //    {
                    //        TextBoxDisconnectedBy.Text = acountDetail.DisconnectedBy;
                    //        TextBoxDisconnectedDate.Text = acountDetail.DisconectedDate.ToString("dd/MM/yyyy");
                    //        TextBoxTime.Text = acountDetail.DisconectedDate.ToString("HH:mm");

                    //    }
                    //    else
                    //    {
                    //        Label DynamicLable = new Label();
                    //        DynamicLable.Text = "*Disconnection Details Should Be Entered";
                    //        divLable.Controls.Add(DynamicLable);
                    //    }
                    //    }

                    //    else if (acountDetail.OrderCardStatus == 0 || acountDetail.OrderCardStatus == 1)
                    //    {
                    //        TextBoxStaus.Text = "Disconnected/Letter Sent";
                    //        if (acountDetail.DisconnectedBy != "")
                    //        {
                    //            TextBoxDisconnectedBy.Text = acountDetail.DisconnectedBy;
                    //            TextBoxDisconnectedDate.Text = acountDetail.DisconectedDate.ToString("dd/MM/yyyy");
                    //            TextBoxTime.Text = acountDetail.DisconectedDate.ToString("HH:mm");

                    //        }
                    //        else
                    //        {
                    //            Label DynamicLable = new Label();
                    //            DynamicLable.Text = "*Disconnection Details Should Be Entered";
                    //            divLable.Controls.Add(DynamicLable);
                    //        }
                    //    }

                    //    else if (acountDetail.OrderCardStatus == 2)
                    //    {
                    //        if (acountDetail.MeterRemovedStatus == 0)
                    //        {
                    //            TextBoxStaus.Text = "Disconnected/OrderCard Sent";
                    //            if (acountDetail.DisconnectedBy != "")
                    //            {
                    //                TextBoxDisconnectedBy.Text = acountDetail.DisconnectedBy;
                    //                TextBoxDisconnectedDate.Text = acountDetail.DisconectedDate.ToString("dd/MM/yyyy");
                    //                TextBoxTime.Text = acountDetail.DisconectedDate.ToString("HH:mm");

                    //            }
                    //            else
                    //            {
                    //                Label DynamicLable = new Label();
                    //                DynamicLable.Text = "*Disconnection Details Should Be Entered";
                    //                divLable.Controls.Add(DynamicLable);
                    //            }
                    //        }

                    //        else if (acountDetail.MeterRemovedStatus == 1)
                    //        {
                    //            TextBoxStaus.Text = "Disconnected/Meter Removed";
                    //            if (acountDetail.DisconnectedBy != "")
                    //            {
                    //                TextBoxDisconnectedBy.Text = acountDetail.DisconnectedBy;
                    //                TextBoxDisconnectedDate.Text = acountDetail.DisconectedDate.ToString("dd/MM/yyyy");
                    //                TextBoxTime.Text = acountDetail.DisconectedDate.ToString("HH:mm");

                    //            }
                    //            else
                    //            {
                    //                Label DynamicLable = new Label();
                    //                DynamicLable.Text = "*Disconnection Details Should Be Entered";
                    //                divLable.Controls.Add(DynamicLable);
                    //            }
                    //        }

                    //        else if (acountDetail.MeterRemovedStatus == 2)
                    //        {
                    //            TextBoxStaus.Text = "Disconnected/Finalized";
                    //            if (acountDetail.DisconnectedBy != "")
                    //            {
                    //                TextBoxDisconnectedBy.Text = acountDetail.DisconnectedBy;
                    //                TextBoxDisconnectedDate.Text = acountDetail.DisconectedDate.ToString("dd/MM/yyyy");
                    //                TextBoxTime.Text = acountDetail.DisconectedDate.ToString("HH:mm");

                    //            }
                    //            else
                    //            {
                    //                Label DynamicLable = new Label();
                    //                DynamicLable.Text = "*Disconnection Details Should Be Entered";
                    //                divLable.Controls.Add(DynamicLable);
                    //            }
                    //        }
                    //    }
                    //}
                }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('please check whether your Account No is Correct or wrong. If it is correct please insert New Account Details');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('please insert Valid Account Number');", true);
            }
        }

        protected void TextBox1_TextChanged1(object sender, EventArgs e)
        {
        }

        protected void btnMoreDetails_Click(object sender, EventArgs e)
        {
            string acc = TextBox1.Text;
            if (acc != "")
            {
                connectionLog = connectionService.gertConnectionLogToTable(acc);
                int listsize = connectionLog.Count;
                if (listsize == 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('No previous Disconnection Details');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Please insert valid account number, Then Search!');", true);
            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //TextBoxPaymentDetailsAccountNo.Text = TextBox1.Text;
        }

        

        protected void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            string accNo = TextBox1.Text;
            if (accNo != "")
            {

                if (TextBoxPaymentDate.Text != "")
                {
                    if (DListPaymentMethod.Text != "0")
                    {
                        ConnectionService newService = new ConnectionService();
                        bool reconstatus = newService.CheckForPaymentDetails(accNo);
                        if (reconstatus == false)
                        {
                            DateTime Date = DateTime.Parse(TextBoxPaymentDate.Text);
                            string Pmethod = DListPaymentMethod.Text;
                            string contactNo = TextBoxContactNo.Text;
                            Double time = newService.getTimeSpanBetweenReconnections(accNo, Date);
                            if (time > 30)
                            {
                                updatePaymentDetails(accNo, Pmethod, Date, contactNo);
                            }

                            else
                            {
                                string acc = accNo;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox("+acc+");", true);
                            }
                        }

                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('You have already enterd details related to this account Number');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Please select Payment method');", true);
                    }
                }

                else{
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Please select Payment Date');", true);
                }
            }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Please Enter a Valid Accoun Number');", true);
                }
            }


        public void updatePaymentDetails(string accNo,string Pmethod,DateTime Date,string contactNo) 
        {
            ConnectionService newService = new ConnectionService();
            bool updatestatus = newService.setPaymentDetail(accNo, Pmethod, Date, contactNo);
            if (updatestatus == true)
            {
                newService.setConnectionStatus(accNo, 0);
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Pamentnt Details Updated');", true);
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBoxWalkOrder.Text = string.Empty;
                TextBoxStaus.Text = string.Empty;
                TextBoxDisconnectedDate.Text = string.Empty;
                TextBoxDisconnectedBy.Text = string.Empty;
                TextBoxTime.Text = string.Empty;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('You have already Entered Payment Details For this Disconnection');", true);
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBoxWalkOrder.Text = string.Empty;
                TextBoxStaus.Text = string.Empty;
                TextBoxDisconnectedDate.Text = string.Empty;
                TextBoxDisconnectedBy.Text = string.Empty;
                TextBoxTime.Text = string.Empty;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string accNo = TextBoxAccountNo.Text;
            string FName = TextBoxFName.Text;
            string LName = TextBoxLName.Text;
            string AddressLine1 = TextBoxAddressLine1.Text;
            string AddressLine2 = TextBoxAddressLine2.Text;
            string AddressLine3 = TextBoxAddressLine3.Text;
            string contactNumber = TextBoxPhoneNo.Text;
            string ReadeCode = TextBoxReaderCode.Text;
            string DailyPackNo = TextBoxDPackNo.Text;
            string walkSeq = TextBoxWalkSeq.Text;
            string Depot = DepotList.SelectedItem.Text;

            int i = connectionService.insertConsumertDetails(accNo, FName, LName, AddressLine1, AddressLine2, AddressLine3, contactNumber, ReadeCode, DailyPackNo, walkSeq, Depot);
            if (i > 0)
            {
                connectionService.updateConnectionStatus(accNo);
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Successfully Saved');", true);
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('You have already enterd details related to this account Number');", true);
            }
        }

       
    }
}