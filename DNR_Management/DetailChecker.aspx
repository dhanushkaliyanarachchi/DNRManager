<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="DetailChecker.aspx.cs" Inherits="DNR_Manager.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Script/Custom%20JS/Validation.js"></script>
    <style>
        .tablerow {
            min-height: 500px;
        }
    </style>
    <div class="row" style="min-height: 520px; background: lightgrey; margin-left: 20px; margin-right: 02px; margin-top: 50px; width: 1300px">
        <div class="container tablay" style="margin-top: 50px">

            <div class="col-sm-12">


                <div class="col-xs-1">

                    <ul class="nav nav-tabs tabs-left">
                        <li class="but tab3" style="width: 90px"><a href="#home" data-toggle="tab">Update</a></li>
                        <li class="but tab3" style="width: 90px"><a href="#profile" data-toggle="tab">Letter List</a></li>
                        <li class="but tab3" style="width: 90px"><a href="#messages" data-toggle="tab">Thousand List</a></li>
                        <li class="but tab3" style="width: 90px"><a href="#settings" data-toggle="tab">Order Card</a></li>
                        <li class="but tab3" style="width: 90px"><a href="#updateMeterRemoveDetails" data-toggle="tab">Meter Removal</a></li>
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
                                                <asp:DropDownList ID ="DisconnectedByList" runat="server" class="form-control" ></asp:DropDownList>
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
                            <div class ="row">
                                <div class ="col-md-6">
                                    <div class ="row" style ="margin-left:50px; margin-bottom:10px; border-bottom:groove">
                                        <select id="select" class="required">
                                                    <option value="">Select Bill Circle</option>
                                                    <option value="1">Circle 1</option>
                                                    <option value="2">Circle 2</option>
                                                    <option value="3">Circle 3</option>
                                                    <option value="4">Circle 4</option>
                                                </select>
                                        <input type ="button" class ="btn btn-primary btn-billCircle" value ="Generate List" style ="margin-left:20px; margin-bottom:10px" />
                                    </div>
                                    <div id="dvInfo" class ="row" style ="margin-left:50px" runat="server">
                                        <table class="table" id="tableThousandList">
                                            <thead>
                                                <tr>
                                                    <th style="width:200px">Account No</th>
                                                    <th style="width:200px">WalkOrder</th>
                                                </tr>

                                            </thead>
                                        </table>
                                    </div>
                                    <div class="row " id ="buttonRow" >
                                        <div class ="col-sm-3">
                                        <input type="button" class = "btn btn-primary" id ="btn-Save" value ="Save"  style ="margin-left: 70px; display:none"/>
                                        </div>
                                        <div class ="col-sm-3">
                                        <input type="button" class = "btn btn-primary" id ="export_excel_button" value ="print"  style ="margin-left: 70px; display:none"/>
                                        </div>
                                    </div>
                                </div>

                                <div class ="col-md-6">
                                        <div class ="col-sm-3">
                                            <table style ="border:groove; width:100px" id ="thousandListDate">
                                            <thead>
                                                <tr>
                                                    <th>Cercle 1</th>
                                                </tr>
                                            </thead>
                                                <%var thousandDatesListCircle1 = ThousandDates.Where(item => item.BillCircle == 1);
                                                  foreach (var items in thousandDatesListCircle1)
                                                  {%>
                                                      
                                                <tr>
                                                    <td class ="thousandListDate" id ="<%:items.BillCircle%>"><a><%:items.ThousandListDates.Date.ToString("d")%></a></td>
                                                </tr>

                                                <%}
                                                  
                                                   %>
                                                
                                                     
                                        </table>
                                        </div>

                                        <div class ="col-sm-3">
                                            <table style ="border:groove; width:100px" id ="thousandListDate">
                                            <thead>
                                                <tr>
                                                    <th>Cercle 2</th>
                                                </tr>

                                            </thead>
                                                 <%var thousandDatesListCircle2 = ThousandDates.Where(item => item.BillCircle == 2);
                                                   foreach (var items in thousandDatesListCircle2)
                                                   {%>
                                                      
                                                <tr>
                                                    <td class ="thousandListDate" id ="<%:items.BillCircle%>"><a><%:items.ThousandListDates.ToString("d")%></a></td>
                                                </tr>

                                                <%}
                                                  
                                                   %>
                                        </table>
                                        </div>

                                        <div class ="col-sm-3">
                                            <table style ="border:groove; width:100px" id ="thousandListDate">
                                            <thead>
                                                <tr>
                                                    <th>Cercle 3</th>
                                                </tr>

                                            </thead>
                                                 <%var thousandDatesListCircle3 = ThousandDates.Where(item => item.BillCircle == 3);
                                                   foreach (var items in thousandDatesListCircle3)
                                                   {%>
                                                      
                                                <tr>
                                                    <td class ="thousandListDate" id ="<%:items.BillCircle%>""><a><%:items.ThousandListDates.ToString("d")%></a></td>
                                                </tr>

                                                <%}
                                                  
                                                   %>
                                        </table>
                                        </div>

                                        <div class ="col-sm-3">
                                            <table style ="border:groove; width:100px" id ="thousandListDate">
                                            <thead>
                                                <tr>
                                                    <th>Cercle 4</th>
                                                </tr>

                                            </thead>
                                                 <%var thousandDatesListCircle4 = ThousandDates.Where(item => item.BillCircle == 4);
                                                   foreach (var items in thousandDatesListCircle4)
                                                   {%>
                                                      
                                                <tr>
                                                    <td class ="thousandListDate"  id ="<%:items.BillCircle%>" ><a><%:items.ThousandListDates.ToString("d")%></a></td>
                                                </tr>

                                                <%}
                                                  
                                                   %>
                                        </table>
                                        </div>
                                    </div>
                                
                            </div>
                        </div> 

                        <div class="tab-pane" id="settings">
                            <div class="row">
                                <asp:Button ID="ButtonOrderCardList" type="submit" runat="server" Text="Generate Order Card List" Style="margin-left: 50px" OnClick="generateOrderCardList_Click" />
                                </div>
                                <table class="table" id ="table_OrderCardList">
                                <thead>
                                    <tr>
                                        <th>Account No</th>
                                        <th>Name</th>
                                        <th>AddressLine1</th>
                                        <th>AddressLine2</th>
                                        <th>AddressLine3</th>
                                        <th>LetterID</th>
                                        <th>OrderCardID</th>
                                        <th>Walk Order</th>
                                        <th>Cancell</th>
                                    </tr>
                                    </thead>
                                    <%var newOrderCardList = OrderCardList;
                                      foreach (var item in newOrderCardList)
                                      {%>
                                    <tr>
                                        <td class ="accNo_OrderCard"><%:item.AccountNo %></td>
                                        <td><%:item.Fname %> <%:item.Lname %> </td>
                                        <td><%:item.AddressLine1 %></td>
                                        <td><%:item.AddressLine2 %></td>
                                        <td><%:item.AddressLine3 %></td>
                                        <td><%:item.LetterID %></td>
                                        <td><%:item.OrderCardID %></td>
                                        <td><%:item.ReaderCode %>/<%:item.DailypackNo %>/<%:item.WalkSeq %></td>
                                        <td><input type="button" class="btn btn-primary btn_Update_Order" data-accno="<%:item.AccountNo%>" value="Cancell" /></td>
                                    </tr>
                                    <%}
                                       %>
                                
                                    </table>
                            <div class =" row">
                                <input type="button" class = "btn btn-primary" id ="export_excel_OrderCard_button" value ="Export To Excel"  style ="margin-left: 70px"/>
                                <input type="button" class="btn btn-primary btn_OrderCard_Save" id = "btn_OrderCard_Save" data-accno="OrderCard" value="Save" style ="margin-left: 70px; display:none"/>
                            </div>
                            
                        </div>

                        <div class="tab-pane" id="updateMeterRemoveDetails">
                            <div class ="row">
                                    <div class="col-md-5" style="background-color: #C4D5BB; margin-left: 20px">
                                    <div class="form-horizontal" style="margin-top: 20px">
                                        <div class="form-group">
                                            <label for="Account No" class="col-sm-2 control-label" style="padding-top: 0px">Account No:</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="TextBoxAccountNumber" runat="server" class="form-control"  MaxLength="10" onkeypress="return validate(event)" onkeydown="return tabKeyDown(event, this)" onkeyup ="enterKeyUp(event,this.value)"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                        <div class="form-horizontal" style="margin-top: 20px">
                                            <div class="form-group">
                                                <label for="Address" class="col-sm-2 control-label" style="padding-top: 0px">Address:</label>
                                                <div class="col-sm-10">
                                                    <textarea class="form-control" rows="5" id="TextBoxOrderCardAddress" readonly ="readonly"></textarea>
                                                    <%--<asp:TextBox ID="TextBoxOrderCardAddress" runat="server" class="form-control" Rows="5" TextMode="MultiLine" ReadOnly="true" Enabled="false" cols="35"></asp:TextBox>--%>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="form-horizontal" style="margin-top: 20px">
                                        <div class="form-group">
                                            <label for="WalkOrder" class="col-sm-2 control-label" style="padding-top: 0px">Walk Order:</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="TextBoxWalkOrder" readonly="readonly"/>
                                                <%--<asp:TextBox ID="TextBoxWalkOrder" runat="server" class="form-control" ReadOnly ="true"></asp:TextBox>--%>
                                            </div>

                                        </div>
                                    </div>

                                        <div class="form-horizontal" style="margin-top: 20px">
                                        <div class="form-group">
                                            <label for="ConnectionStatus" class="col-sm-2 control-label" style="padding-top: 0px">Status:</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="TextBoxConnectionStatus" readonly="readonly"/>
                                                <%--<asp:TextBox ID="TextBoxConnectionStatus" runat="server" class="form-control" ReadOnly ="true"></asp:TextBox>--%>
                                            </div>

                                        </div>
                                    </div>

                                        <div class="form-horizontal" style="margin-top: 20px">
                                        <div class="form-group">
                                            <label for="MeterRemoveDate" class="col-sm-2 control-label" style="padding-top: 0px">Meter Removed Date:</label>
                                            <div class="col-sm-10">
                                                <input type="date" class="form-control" id="TextBoxMeterRemovedDate" readonly="readonly"/>
                                                <%--<asp:TextBox ID="TextBoxMeterRemovedDate" type ="Date" runat="server" class="form-control" ReadOnly ="true"></asp:TextBox>--%>
                                            </div>

                                        </div>
                                    </div>

                                        <div class="form-horizontal" style="margin-top: 20px">
                                            <div class="form-group">
                                                <button type="button" class="btn btn-primary" id ="ButtonMeterRemoved" disabled="disabled">Update</button>
                                                <%--<asp:Button ID="ButtonMeterRemoved" type="submit" runat="server" Text="Update" Style="margin-left: 50px" Enabled="false"/>--%>
                                                <button type="button" class="btn btn-primary" id ="ButtonActivateFinalized" style = "display:none">Activate</button>
                                            </div>
                                        </div>

                                    </div>


                                <div class="col-md-6">
                                    
                                        <table class="table" id ="table_MeterRemoved_Accounts" style ="width:200px">
                                    <thead>
                                    <tr>
                                        <th>Account No</th>
                                        <th>OrderCard Date</th>
                                        <th>Meter Removed Date</th>
                                        <th>Finalized Date</th>
                                        <th>Finalize</th>
                                    </tr>
                                    </thead>
                                            <%var newRemovedMeterList = RemovedMeterList;
                                              foreach(var item in newRemovedMeterList){%>
                                            <tr>
                                            <td class ="accNo_Finalized"><%:item.AccountNo %></td>
                                            <td><%:item.OrderCardDate %></td>
                                            <td><%:item.MeterRemovedDate %></td>
                                            <td><input data-format="yyyy-MM-dd hh:mm:ss" class="FinalizedDate" type="date" /></td>
                                            <td><input type="button" class="btn btn-primary bton_FinalizeAccount" data-accno="<%:item.AccountNo%>" value="Finalize" /></td>
                                                </tr>
                                            <%} %>
                                            
                                        </table>
                                    </div>
                                
                                </div>
                            </div>
                        </div>
                    </div>

                </div>


            </div>
        </div> 
    <div class="modal" id="AcivateFinalizedAcconts" title="">
        <div class="row">
            <div class="form-group">
                <label for="AccountNo" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px" >Account No:</label>
                <div class="col-sm-9">
                    <input type="text" id="FinalizedAccNo" class="form-control" readonly="readonly" value =""/>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label for="DateReconnected" class="col-sm-4 control-label" style="padding-left: 0px; padding-top: 0px" >Reconnected Date:</label>
                <div class="col-sm-8">
                    <input type="date" data-format="yyyy-MM-dd hh:mm:ss" id="ReactivatedDate" class="form-control"/>
                </div>
            </div>
        </div>
        <div class="row">
            <label for="AccountNo"  style="padding-left: 0px; padding-top: 0px">If you realy want to Reactivate this Account, click on following Button. </label>
        </div>
        <div class="row">
            <input type="button" class="btn btn-primary btn-Activate" value="Activate" id ="ActivateFinalizedAccount" />
        </div>
    </div>
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/json2/20130526/json2.min.js"></script>
    <script src="Script/JS/jquery.battatech.excelexport.min.js"></script>
    <script src="Script/JS/customjs.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />

    <script type ="text/javascript">

        $(".btn_Update").click(function () {
            debugger;
            var $row = $(this).closest("tr");
            var accountNo = $row.find(".accNo").text().trim();
            var letterId = $row.find(".letterId").text().trim();
            var sentDate = $row.find(".LettersentDate .Lettersent").val().trim();

            var LettersentModel = {
                accountNo: accountNo,
                letterID: letterId,
                sentDate: sentDate
            };

            $.ajax({
                type: 'POST',
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
    <script type="text/javascript">

        var accountNumbers = [];

        $(".btn-billCircle").click(function () {
            debugger
            var circleNo = $("#select option:selected").val();
            if (circleNo != "") {
                var LettersentModel = {
                    circleNo: circleNo
                };

                $.ajax({
                    type: 'POST',
                    url: "DetailChecker.aspx/GenerateThousandList",
                    data: JSON.stringify(LettersentModel),
                    contentType: "application/json",
                    dataType: "json",
                    success: function (response) {
                        Callback(response);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }

            else {
                alert("Please insert Circle No");
            }

        });


        //function OnSuccess(response) {
        //    var json = response.d;
        //    var $table = $("#tableThousandList");
        //    var circleNo = $("#select option:selected").val().toString();
        //    $("#tableThousandList tr:gt(0)").remove();
        //    accountNumbers = [];
        //    //$("#tableThousandList").empty();
        //    $(json).each(function () {
        //        var tr = [];
        //        tr.push('<tr>');
        //        tr.push("<td>" + this.AccountNo + "</td>");
        //        tr.push("<td>" + this.ReaderCode + "/" + this.DailyPackNo + "/" + this.WalkSeq + "</td>");
        //        tr.push('</tr>');
        //        $('#tableThousandList').append($(tr.join('')));
        //        accountNumbers.push(this.AccountNo + "/" + circleNo);
        //        //$table.append($row);
        //    });

        //}

        function Callback(response) {
            debugger
            var json = response.d;
            var $table = $("#tableThousandList");
            var circleNo = $("#select option:selected").val().toString();
            $("#tableThousandList tr:gt(0)").remove();
            //$("#tableThousandList").empty();
            $(json).each(function () {
                var tr = [];
                tr.push('<tr>');
                tr.push("<td>" + this.AccountNo + "</td>");
                tr.push("<td>" + this.ReaderCode + "/" + this.DailyPackNo + "/" + this.WalkSeq + "</td>");
                tr.push('</tr>');
                $('#tableThousandList').append($(tr.join('')));
                accountNumbers.push(this.AccountNo + "/" + circleNo);
            });
            $('#export_excel_button').hide();
            $('#btn-Save').show();

        }


        $("#btn-Save").click(function () {
            debugger;
            var accountNo = [];
            var circleNo = $("#select option:selected").val().toString();
            var rowCount = $('#tableThousandList tr').length;
            $('#tableThousandList').each(function (row, tr) {
                accountNo[row] = $(tr).find('td:eq(0)').text() + "/" + circleNo;
            });

            var theIds = JSON.stringify({ ids: accountNo });
            if (rowCount == 1) {
                alert("No Details Found in List.");
            }
            else if (rowCount > 1) {
                $.ajax({
                    type: 'POST',
                    url: "DetailChecker.aspx/SaveThousandsList",
                    data: theIds,
                    contentType: "application/json",
                    dataType: "json",
                    success: function (result) {
                        alert('List Saved');
                    },
                    error: function (result) {
                        alert("Problem Occured");
                    }
                });
            }
        });

    </script>

    <script type ="text/javascript">
        $('#thousandListDate tr td').click(function () {
            var BillCircle = $(this).attr('id');
            var ListDate = $(this).text();
            var parameterModel = {
                BillCircle: BillCircle,
                Date: ListDate
            };

            $.ajax({
                type: 'POST',
                url: 'DetailChecker.aspx/GenerateSavedThousandList',
                data: JSON.stringify(parameterModel),
                contentType: "application/json",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });


        });

        function OnSuccess(response) {
            var json = response.d;
            var $table = $("#tableThousandList");
            var circleNo = $("#select option:selected").val().toString();
            $("#tableThousandList tr:gt(0)").remove();
            //$("#tableThousandList").empty();
            $(json).each(function () {
                var tr = [];
                tr.push('<tr>');
                tr.push("<td>" + this.AccountNo + "</td>");
                tr.push("<td>" + this.ReaderCode + "/" + this.DailyPackNo + "/" + this.WalkSeq + "</td>");
                tr.push('</tr>');
                $('#tableThousandList').append($(tr.join('')));
                //$table.append($row);
            });
            $('#btn-Save').hide();
            $('#export_excel_button').show();
        }
    </script>

    <script type ="text/javascript">

        $(".btn_Update_Order").click(function () {
            debugger;
            var $row = $(this).closest("tr");
            var accountNo = $row.find(".accNo_OrderCard").text().trim();

            var OrderCardModel = {
                accountNo: accountNo
            };

            $.ajax({
                type: 'POST',
                url: "DetailChecker.aspx/RemoveOrderCard",
                data: JSON.stringify(OrderCardModel),
                contentType: "application/json",
                dataType: "json",
                success: CallNow,
                failure: function (response) {
                    alert(response.d);
                }
            });

        });

        function CallNow(response) {
            alert(response.d);
        }

    </script>

    <script type ="text/javascript">
        $("#btn_OrderCard_Save").click(function () {
            debugger
            PostTable();
            //debugger;
            //PostTable();
        });

        function getrow(rowNumber) {
            debugger;
            var tableRow = $('#table_OrderCardList tbody tr').eq(rowNumber);
            var row = {};
            row.AccountNo = tableRow.find('td:eq(0)').text().trim();
            row.OrderCardID = tableRow.find('td:eq(6)').text().trim();
            return row;
        }

        function getAllRows() {
            debugger;
            var dataRows = [];

            $('#table_OrderCardList tbody tr').each(function (index, value) {
                //$('#' + table+ ' tbody tr').each(function (index, value) {
                debugger;
                var currentRow = getrow(index);
                dataRows.push(currentRow);
            });

            return dataRows;
        }

        function PostTable() {
            //var crossId = getParameterByName('id');
            var jsonRequest = { rows: getAllRows() };

            $.ajax({
                type: 'POST',
                url: 'DetailChecker.aspx/UpdateOrdercardDetails',
                data: JSON.stringify(jsonRequest),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false,
                success: function (data, text) {
                    return true;
                },
                error: function (request, status, error) {
                    return false;
                }
            });
        }

    </script>

    <script type ="text/javascript">
       
        function enterKeyUp(event, accountNo) {
            if (event.keyCode == 13) {
                $('#TextBoxOrderCardAddress').text("");
                $('#TextBoxWalkOrder').val("");
                $('#TextBoxConnectionStatus').val("");
                $('#ButtonMeterRemoved').prop('disabled', true);
                $('#TextBoxMeterRemovedDate').prop('readonly', true);
                getMeterRemoveDetailsToUI(accountNo);
            }
            else {
                $('#TextBoxOrderCardAddress').text("");
                $('#TextBoxWalkOrder').val("");
                $('#TextBoxConnectionStatus').val("");
                $('#ButtonMeterRemoved').prop('disabled', true);
                $('#TextBoxMeterRemovedDate').prop('readonly', true);
                $('#ButtonActivateFinalized').hide();
            }
        };

        function getMeterRemoveDetailsToUI(accountNo) {
            debugger;
            
            var MeterRemouedAccNoModel = {
                accountNo: accountNo
            };

            $.ajax({
                type: 'POST',
                url: "DetailChecker.aspx/UpdateMeterRemoveDetails",
                data: JSON.stringify(MeterRemouedAccNoModel),
                contentType: "application/json",
                dataType: "json",
                success: UpdateSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
            function UpdateSuccess(response) {
                debugger;
                var jasonMremoveDetails = response.d;
                if (jasonMremoveDetails.availability == true) {
                    if (jasonMremoveDetails.connectionStats == 1) {
                        $('#TextBoxOrderCardAddress').text(jasonMremoveDetails.Address1 + '\n' + jasonMremoveDetails.Address2 + '\n' + jasonMremoveDetails.Address3);
                        $('#TextBoxWalkOrder').val(jasonMremoveDetails.ReaderCode + '/' + jasonMremoveDetails.DailyPackNo + '/' + jasonMremoveDetails.WalkSeq);
                        $('#TextBoxConnectionStatus').val("Connected");
                        $('#ButtonActivateFinalized').hide();
                    }
                    else if (jasonMremoveDetails.connectionStats == 0) {
                        $('#TextBoxOrderCardAddress').text(jasonMremoveDetails.Address1 + '\n' + jasonMremoveDetails.Address2 + '\n' + jasonMremoveDetails.Address3);
                        $('#TextBoxWalkOrder').val(jasonMremoveDetails.ReaderCode + '/' + jasonMremoveDetails.DailyPackNo + '/' + jasonMremoveDetails.WalkSeq);

                        if (jasonMremoveDetails.LetterStatus == 0 || jasonMremoveDetails.LetterStatus == 1) {
                            $('#TextBoxConnectionStatus').val("Disconnected");
                            $('#ButtonActivateFinalized').hide();
                        }

                        else if (jasonMremoveDetails.LetterStatus == 2 && (jasonMremoveDetails.OrderCardStatus == 1 || jasonMremoveDetails.OrderCardStatus == 0)) {
                            $('#TextBoxConnectionStatus').val("Disconnected/ Letter Sent");
                            $('#ButtonActivateFinalized').hide();
                        }

                        else if (jasonMremoveDetails.OrderCardStatus == 2 && jasonMremoveDetails.MeterRemovedStatus == 0) {
                            $('#TextBoxConnectionStatus').val("Disconnected/ OrderCard Sent");
                            $('#TextBoxMeterRemovedDate').prop('readonly', false);
                            $('#ButtonMeterRemoved').prop('disabled', false);
                            $('#ButtonActivateFinalized').hide();
                        }

                        else if (jasonMremoveDetails.MeterRemovedStatus == 1) {
                            $('#TextBoxConnectionStatus').val("Disconnected/ Meter Removed");
                            $('#ButtonActivateFinalized').hide();
                        }

                        else if (jasonMremoveDetails.MeterRemovedStatus == 2) {
                            $('#TextBoxConnectionStatus').val("Disconnected/ Finalized");
                            $('#ButtonActivateFinalized').show();
                        }
                    }
                }

                else {
                    alert("Invalid Account Number");
                    $('#ButtonActivateFinalized').hide();
                }
            }
    </script>

    <script type ="text/javascript">
        $("#ButtonMeterRemoved").click(function () {
            debugger;
            //var accountNo = $('#TextBoxAccountNumber').val();
            var accountNo = $("#<%=TextBoxAccountNumber.ClientID %>").val();
            var MRDate = $('#TextBoxMeterRemovedDate').val();
            if (MRDate != "") {
                updateMeterRemovedDetails(accountNo, MRDate);
            }
            else {
                alert("Please insert Valid Date");
            }
        });

        function updateMeterRemovedDetails(accountNo, MRDate)  {
            var MeterRemovedUpdateModel = {
                accountNo: accountNo,
                MRDate: MRDate
            };

            $.ajax({
                type: 'POST',
                url: "DetailChecker.aspx/UpdateMeterRemoveDate",
                data: JSON.stringify(MeterRemovedUpdateModel),
                contentType: "application/json",
                dataType: "json",
                success: UpdateDateSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function UpdateDateSuccess(response) {
            var jason = response.d;
            if (jason > 0) {
                alert("Database Updated");
            }

            else {
                alert("Dtabase Not Updated");
            }
        }

    </script>

    <script type ="text/javascript">
        $('.bton_FinalizeAccount').click(function () {
            debugger;
            var $row = $(this).closest("tr");
            var accountNo = $row.find(".accNo_Finalized").text().trim();
            var finalizedDate = $row.find(".FinalizedDate").val().trim();

            if (finalizedDate == "") {
                alert("Please insert Valid Date");
            }
            else{
                var FinalizedAccModal = {
                    accountNo: accountNo,
                    finalizedDate: finalizedDate
                };

                $.ajax({
                    type: 'POST',
                    url: "DetailChecker.aspx/UpdateFinalizedDate",
                    data: JSON.stringify(FinalizedAccModal),
                    contentType: "application/json",
                    dataType: "json",
                    success: confirmUpdate,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
            });
        function confirmUpdate(response) {
            var jason = response.d;
            if(jason >0 ){
                $('.bton_FinalizeAccount').prop('disabled', true);
            }
        }

    </script>
    <script type="text/javascript">
        $('#ButtonActivateFinalized').click(function () {
            debugger;
            $('#AcivateFinalizedAcconts').dialog({
                autoOpen: false,
            });
            $('#AcivateFinalizedAcconts').dialog("open");
            var accountNo = $("#<%=TextBoxAccountNumber.ClientID %>").val();
            $('#FinalizedAccNo').val(accountNo);
        });
    </script> 

    <script type="text/javascript">
        $('#ActivateFinalizedAccount').click(function () {
            debugger;
            var accountNo = $('#FinalizedAccNo').val();
            var RconnectedDate = $('#ReactivatedDate').val();
            if (RconnectedDate == "") {
                alert("Please Insert Reconnected Date");
            }

            else {
                var ReactivateAccModal = {
                    accountNo: accountNo,
                    RconnectedDate: RconnectedDate
                };

                $.ajax({
                    type: 'POST',
                    url: "DetailChecker.aspx/UpdateReactivation",
                    data: JSON.stringify(ReactivateAccModal),
                    contentType: "application/json",
                    dataType: "json",
                    success: confirmReactivation,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
        });

        function confirmReactivation(response) {
            var jason = response.d;
            if (jason > 0) {
                $('#ButtonActivateFinalized').hide();
                $('#AcivateFinalizedAcconts').dialog({
                    autoOpen: false,
                });
                $('#AcivateFinalizedAcconts').dialog("close");
            }
        }

    </script> 

</asp:Content>
