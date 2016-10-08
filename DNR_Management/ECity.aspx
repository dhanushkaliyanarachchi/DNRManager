<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ECity.aspx.cs" Inherits="DNR_Manager.Web.ECity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#PaymentModal').modal('show');
        };
    </script>
    <style>
    #NewAccountModal {
top:20%;
left:50%;
outline: none;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 50px; background: lightgrey; margin-left: 20px; margin-right: 02px; width: 1300px">
        <div class="row">

            <div class="col-md-2">
                <img src="images/CEB ECity.jpg" class="img-rounded" alt="Cinque Terre" width="202" height="544" />
            </div>

            <div class="col-md-5" style="margin-left: 50px">
                <div class="row" style="height: 10px">
                </div>
                <div class="row" style="margin-top: 20px">
                    <form class="form-horizontal">
                        <div class="form-group">
                            <label for="AccountNo" class="col-sm-2 control-label">Account Number:</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" OnTextChanged="TextBox1_TextChanged1" Type="phone" onkeypress="return NumberOnly()"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnSearch" class="btn btn-default" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>

                    </form>
                </div>
                <div class="row" <%--style="margin-top: 20px"--%>>
                    <form class="form-horizontal">
                        <div class="form-group">
                            <label for="Address" class="col-sm-2 control-label" style="margin-left: 10px">Address:</label>
                            <div class="col-sm-8">
                                <%--<textarea rows="5"></textarea>--%>
                                <asp:TextBox ID="TextBox2" runat="server" Rows="5" TextMode="MultiLine" ReadOnly="true" Enabled="false" cols="35"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Walk Order" class="col-sm-2 control-label" style="margin-left: 10px; padding-top: 0px">Walk Order:</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TextBoxWalkOrder" runat="server" class="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </form>
                </div>

                <div class="row" style="margin-top: 0px">
                    <div class="span12" style="border: 2px solid black">

                        <form class="form-horizontal" style="margin-top: 20px">
                            <div class="form-group">
                                <label for="Status" class="col-sm-2 control-label">Status</label>
                                <div class="col-sm-10" id="TextBoxStatus">
                                    <asp:TextBox ID="TextBoxStaus" runat="server" class="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="Disconnected Date" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Disconnected Date</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxDisconnectedDate" runat="server" class="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="Disconnected Person" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Disconnected By:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxDisconnectedBy" runat="server" class="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="Disconnected Time" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Time:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxTime" runat="server" class="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>

                                </div>
                            </div>

                        </form>


                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-10">
                        <div id="divLable" runat="server"></div>
                    </div>
                </div>
                <div class="row" style="margin: 25px">
                    <div class="form-group">
                        <asp:Button ID="Buttonmoredetails" class="btn btn-secondary btn-sm" runat="server" Text="More Details" OnClick="btnMoreDetails_Click" />
                        <input type ="button" class ="btn btn-secondary btn-UpdatePayment" value ="Update Payment" style ="margin-left:20px; margin-bottom:10px" onclick ="Openmodal()" />
                        <input type ="button" class ="btn btn-secondary btn-newaccount" value ="Add New Account" style ="margin-left:20px; margin-bottom:10px" onclick ="Openmodalnewaccount()" />
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <%--<div class="row"--%>
                <%--                <asp:Table ID="TableMoreDetails" runat="server" CellPadding="2" CellSpacing="2" Style="border: 3px solid black; padding: 3px; margin-top: 20px; margin-left: 30px">
                    <asp:TableHeaderRow ID="Table1HeaderRow" BackColor="Lightgrey" runat="server" BorderStyle="Solid">
                        <asp:TableHeaderCell Scope="Column" Text="D/C No" BorderStyle="Solid" BorderColor="Black" />
                        <asp:TableHeaderCell Scope="Column" Text="Disconnected Date" BorderStyle="Solid" BorderColor="Black" />
                        <asp:TableHeaderCell Scope="Column" Text="Reconnected Date" BorderStyle="Solid" BorderColor="Black" />
                    </asp:TableHeaderRow>


                </asp:Table>--%>
                <table class="table">
                    <thead>
                        <tr style="border: solid">
                            <th>D/C No</th>
                            <th>Disconnected Date</th>
                            <th>Reconnected Date</th>
                        </tr>
                    </thead>
                    <%if (this.connectionLog != null)
                      {
                          int i = 0;
                          foreach (var item in this.connectionLog)
                          {%>
                    <tr style="border: solid">

                        <td><%:i %></td>
                        <td><%: item.DisconnectedDate %></td>
                        <td><%: item.ReconnectedDate %></td>
                    </tr>


                    <%
                              i++;
                          }
                      }%>
                </table>

            </div>


            <div class="modal fade" id="PaymentModal" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Insert Reconnection Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="form-group">
                                    <label for="AccountNo" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Account No:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="TextBoxPaymentDetailsAccountNo" runat="server" class="form-control" ReadOnly="true" Enabled="False" onkeypress="return NumberOnly()"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group">
                                    <label for="PaymentDate" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Payment Date:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="TextBoxPaymentDate" runat="server" class="form-control" ReadOnly="false" Enabled="true" type="date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group">
                                    <label for="PaymentMethod" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Payment Method:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="DListPaymentMethod" runat="server" Width="200px">
                                            <asp:ListItem Text="Select Payment Method" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="CEB Counter" Value="CEB Counter"></asp:ListItem>
                                            <asp:ListItem Text="Food City" Value="Food City"></asp:ListItem>
                                            <asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group">
                                    <label for="ContactNo" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Contact No:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="TextBoxContactNo" runat="server" class="form-control" ReadOnly="false" Enabled="true" onkeypress="return NumberOnly()"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="ButtonUpdate" class="btn btn-primary" runat="server" Text="Update" type="submit" OnClick="btnUpdatePayment_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="NewAccountModal" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Add New Account</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group">
                                <label for="AccountNo" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Account No:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxAccountNo" runat="server" class="form-control" ReadOnly="false" Enabled="true" onkeypress="return NumberOnly()"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="Name" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Name:</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="TextBoxFName" runat="server" class="form-control" ReadOnly="false" Enabled="true" Text="First Name"></asp:TextBox>
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="TextBoxLName" runat="server" class="form-control" ReadOnly="false" Enabled="true" Text="Last Name"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="PhoneNo" class="col-sm-2 control-label" style="padding-left: -3px; margin-top: 4px">Contact No:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxPhoneNo" runat="server" class="form-control" ReadOnly="false" Enabled="true" Type="phone" style="margin-top:4px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="AddressLine1" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Address Line1:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxAddressLine1" runat="server" class="form-control" ReadOnly="false" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="AddressLine2" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Address Line2:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxAddressLine2" runat="server" class="form-control" ReadOnly="false" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <label for="AddressLine3" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Address Line3:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TextBoxAddressLine3" runat="server" class="form-control" ReadOnly="false" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="Walkorder" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Walk Order:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TextBoxReaderCode" runat="server" class="form-control" ReadOnly="false" Enabled="true" Text="Reader Code"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TextBoxDPackNo" runat="server" class="form-control" ReadOnly="false" Enabled="true" Text="D Pack No"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxWalkSeq" runat="server" class="form-control" ReadOnly="false" Enabled="true" Text="Walk Seq"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <label for="Depot" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Depot:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="DepotList" runat="server">
                                        <asp:ListItem Text="Select Depot" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Fullerton" Value="Fullerton"></asp:ListItem>
                                        <asp:ListItem Text="Mathugama" Value="Mathugama"></asp:ListItem>
                                        <asp:ListItem Text="Beruwala" Value="Beruwala"></asp:ListItem>
                                        <asp:ListItem Text="Panadura" Value="Panadura"></asp:ListItem>
                                        <asp:ListItem Text="Agalawatta" Value="Agalawatta"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="ButtonSave" class="btn btn-primary" runat="server" Text="Save" type="submit" OnClick="btnSave_Click" />
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
<script type ="text/javascript">
        function Openmodal() {
            debugger
            $('#PaymentModal').modal('show');
            var AccounNo = document.getElementById('<%= TextBox1.ClientID %>').value;
            //$('#TextBoxPaymentDetailsAccountNo').val('AccounNo');
            document.getElementById('<%=TextBoxPaymentDetailsAccountNo.ClientID%>').value = AccounNo;

        }
 

    function Openmodalnewaccount() {
        $('#NewAccountModal').modal('show');
        var AccounNo = document.getElementById('<%= TextBox1.ClientID %>').value;
    }


</script>
<script type ="text/javascript">
    function NumberOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127))
                event.returnValue=true;
            else
                event.returnValue=false;
        }
    </script>

</asp:Content>
