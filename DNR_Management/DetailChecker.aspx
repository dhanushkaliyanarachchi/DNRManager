<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="DetailChecker.aspx.cs" Inherits="DNR_Manager.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Script/Custom%20JS/Validation.js"></script>

    <div class="row" style="min-height: 520px; background: lightgrey; margin-left: 20px; margin-right: 02px; margin-top: 50px; width: 1300px">
        <div class="container tablay" style="margin-top: 50px">

            <div class="col-sm-12">


                <div class="col-xs-1">

                    <ul class="nav nav-tabs tabs-left">
                        <li class="but tab3" style="width: 90px"><a href="#home" data-toggle="tab">Update</a></li>
                        <li class="but tab3" style="width: 90px"><a href="#profile" data-toggle="tab">Invoices</a></li>
                        <li class="but tab3" style="width: 90px"><a href="#messages" data-toggle="tab">Inqueries</a></li>
                        <li class="but tab3" style="width: 90px"><a href="#settings" data-toggle="tab">Retention</a></li>
                    </ul>
                </div>
                <div class="col-xs-11">
                    <div class="tab-content">
                        <div class="tab-pane active" id="home">
                            <div class="row">
                                <div class="col-xs-0">
                                </div>
                                <div class="col-xs-7" style="background-color: #C4D5BB; margin-left: 20px">

                                    <div class="form-horizontal" style="margin-top: 20px">
                                        <div class="form-group">
                                            <label for="Account No" class="col-sm-2 control-label" style="padding-top: 0px">Account No:</label>
                                            <div class="col-sm-10">

                                                <asp:TextBox ID="TextBoxAccountNo" runat="server" class="form-control" onkeypress="return validate(event)" MaxLength="10" OnTextChanged="TextBoxAccountNo_TextChanged" onkeydown="return tabKeyDown(event, this)"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label for="Disconnected Date" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Disconnected Date</label>
                                            <div class="col-sm-10">

                                                <asp:TextBox ID="TextBoxDisconnectedDate" runat="server" class="form-control" OnTextChanged="TextBoxDisconnectedDate_TextChanged" type="Date"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <label for="Disconnected Person" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Disconnected By:</label>
                                            <div class="col-sm-10">

                                                <asp:TextBox ID="TextBoxDisconnectedBy" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="Disconnected Time" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Time:</label>
                                            <div class="col-sm-10">

                                                <asp:TextBox ID="TextBoxDisconnectedTime" runat="server" class="form-control" type="time"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>

                                    <asp:Button ID="ButtonUpdate" type="submit" Style="margin-left: 100px" runat="server" Text="Update" OnClick="ButtonUpdate_Click" />
                                </div>



                                <div class="col-xs-4">

                                    <asp:TextBox ID="TextArea1" class="form-control" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="profile">
                            <div class="row">
                                <asp:Button ID="generateLetterList" type="submit" runat="server" Text="Generate Letter List" Style="margin-left: 50px" OnClick="generateLetterList_Click" />
                            </div>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Account No</th>
                                        <th>Address</th>
                                        <th>LetterID</th>
                                        <th>Sent Date</th>
                                        <th>Send</th>
                                    </tr>
                                </thead>
                                <%var LetterDetailModel = letterDetailModel;
                                  foreach (var item in LetterDetailModel)
                                  {%>
                                <tr>
                                    <td class="accNo">
                                        <%:item.AccountNo %>
                                    </td>
                                    <td class="address"><%:item.AddressLine1%><br />
                                        <%:item.AddressLine2%><br />
                                        <%:item.AddressLine3%>
                                    </td>
                                    <td class="letterId">
                                        <%:item.LetterId %>
                                    </td>
                                    <td class="LettersentDate">
                                        <input data-format="yyyy-MM-dd hh:mm:ss" class="Lettersent" type="date" />
                                    </td>
                                    <td>
                                        <input type="button" class="btn btn-primary btn_Update" data-accno="<%:item.AccountNo%>" value="Update" />
                                    </td>
                                </tr>
                                <%} %>
                            </table>
                            <div class="row">
                            </div>
                        </div>

                        <div class="tab-pane" id="messages">
                        </div> 
                    </div>

                </div>


            </div>
        </div>
    </div>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/json2/20130526/json2.min.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />

    <script type ="text/javascript">
        
        $(".btn_Update").click(function()
        {
            debugger;
            var $row =  $(this).closest("tr");
            var accountNo = $row.find(".accNo").text().trim();
            var letterId = $row.find(".letterId").text().trim();
            var sentDate = $row.find(".LettersentDate .Lettersent").val().trim();

            var LettersentModel = {
                accountNo : accountNo,
                letterID : letterId,
                sentDate : sentDate
            }

            $.ajax({
                type: "POST",
                url: "DetailChecker.aspx/updateLetterDate",
                data: JSON.stringify(LettersentModel),
                contentType: "application/json",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });

        });

        function OnSuccess(response) {
            alert(response.d);
        }

    </script>
</asp:Content>
