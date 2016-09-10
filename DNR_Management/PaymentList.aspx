<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="DNR_Manager.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Script/JS/bootstrap.js"></script>
    <%--<script type="text/javascript">
        function updateModal(accNo) {
            document.getElementByName('TextBoxPaymentDetailsAccountNo').Value = accNo;
            ('#DepotUpdateModal').
            
        };--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .ui-widget-header, .ui-state-default, ui-button {
            background: #b9cd6d;
            border: 1px solid #b9cd6d;
            color: #FFFFFF;
            font-weight: bold;
        }
    </style>
    <style>
        .resizedTextbox {width: 60px;}
        .resizedTextbox2 {width: 80px;}
    </style>

    <div class="container" style="margin-top: 50px; background: lightgrey; margin-left: 20px; margin-right: 02px; width: 1300px">
        <div class="row" style="width: 1300px; height: 520px">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-xs-1">
                        <!-- required for floating -->
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs tabs-left sideways">
                            <li class="active " style="width: 95px"><a href="#Fullerton" data-toggle="tab">Fullerton</a></li>
                            <li class="but tab3" style="width: 95px"><a href="#Mathugama" data-toggle="tab">Mathugama</a></li>
                            <li class="but tab3" style="width: 95px"><a href="#Beruwala" data-toggle="tab">Beruwala</a></li>
                            <li class="but tab3" style="width: 95px"><a href="#Panadura" data-toggle="tab">Panadura</a></li>
                            <li class="but tab3" style="width: 95px"><a href="#Agalawatta" data-toggle="tab">Agalawatta</a></li>
                        </ul>
                    </div>

                    <div class="col-xs-11">
                        <div class="tab-content" style="margin-left: 15px">
                            <div class="tab-pane active" id="Fullerton">
                                <div class="row">
                                    <table id="table_fullerton" class="table">
                                        <thead>
                                            <tr>
                                                <th>Account No</th>
                                                <th>Address</th>
                                                <th>Payment Date</th>
                                                <th>Reconnected Date</th>
                                                <th>Reconected By</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <%var FullertonpaymentDetailModel = paymentDetailModel.Where(item => item.Depot == "Fullerton");
                                          foreach (var item in FullertonpaymentDetailModel)
                                          {%>
                                        <tr>
                                            <td class="accNo" onclick="updateDepot(<%:item.AccountNo %>)">
                                                <a class="accNumber"><%:item.AccountNo %></a>
                                                <div class="modal" id="<%:item.AccountNo %>" title="ChangeDepot">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="Select Depot" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Depot:</label>
                                                            <div class="col-sm-9 selectStation">
                                                                <select>
                                                                    <option value="Fullerton">Fullerton</option>
                                                                    <option value="Mathugama">Mathugama</option>
                                                                    <option value="Beruwala">Beruwala</option>
                                                                    <option value="Panadura">Panadura</option>
                                                                    <option value="Agalawatta">Agalawatta</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="changeWalkOrder" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Walk Order:</label>
                                                            <div class="col-sm-2">
                                                                <input type="text" id="ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox"/>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2"/>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class ="row">
                                                        <input type="button" class="btn btn-primary btn-UpdateDepot" data-accno="<%:item.AccountNo%>" value="Update" />
                                                    </div>
                                                </div>
                                            </td>
                                            <%if (item.AddressLine1 == null)
                                              {%>
                                            <td></td>
                                            <%}
                                              else
                                              {%>
                                            <td class="address"><%:item.AddressLine1%><br />
                                                <%:item.AddressLine2%><br />
                                                <%:item.AddressLine3%>
                                            </td>
                                            <%}%>
                                            <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                            <td class="reconnectedDate">
                                                <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                            </td>
                                            <td class="reconnectedBy">
                                                <select>
                                                    <option value="Ruwan">Ruwan</option>
                                                    <option value="Nuwan">Nuwan</option>
                                                    <option value="Kamal">Kamal</option>
                                                    <option value="Piyal">Piyal</option>
                                                </select>

                                            </td>
                                            <td>
                                                <input type="button" class="btn btn-primary btn-fullerton" data-accno="<%:item.AccountNo%>" value="Update" />
                                            </td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </div>
                            </div>



                            <div class="tab-pane" id="Mathugama">
                                <div class="row" >
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>Account No</th>
                                                <th>Address</th>
                                                <th>Payment Date</th>
                                                <th>Reconnected Date</th>
                                                <th>Reconected By</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <%var MathugamapaymentDetailModel = paymentDetailModel.Where(item => item.Depot == "Mathugama");
                                          foreach (var item in MathugamapaymentDetailModel)
                                          {%>
                                        <tr>
                                            <td class="accNo" onclick="updateDepot(<%:item.AccountNo %>)">
                                                <a class="accNumber"><%:item.AccountNo %></a>
                                                <div class="modal" id="<%:item.AccountNo %>" title="ChangeDepot">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="Select Depot" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Depot:</label>
                                                            <div class="col-sm-9 selectStation">
                                                                <select>
                                                                    <option value="Fullerton">Fullerton</option>
                                                                    <option value="Mathugama">Mathugama</option>
                                                                    <option value="Beruwala">Beruwala</option>
                                                                    <option value="Panadura">Panadura</option>
                                                                    <option value="Agalawatta">Agalawatta</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="changeWalkOrder" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Walk Order:</label>
                                                            <div class="col-sm-2">
                                                                <input type="text" id = "ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox"/>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2"/>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class ="row">
                                                        <input type="button" class="btn btn-primary btn-UpdateDepot" data-accno="<%:item.AccountNo%>" value="Update" />
                                                    </div>
                                                </div>
                                            </td>
                                            <%if (item.AddressLine1 == null)
                                              {%>
                                            <td></td>
                                            <%}
                                              else
                                              {%>
                                            <td class="address"><%:item.AddressLine1%><br />
                                                <%:item.AddressLine2%><br />
                                                <%:item.AddressLine3%>
                                            </td>
                                            <%}%>
                                            <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                            <td class="reconnectedDate">
                                                <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                            </td>
                                            <td class="reconnectedBy">
                                                <select>
                                                    <option value="Ruwan">Ruwan</option>
                                                    <option value="Nuwan">Nuwan</option>
                                                    <option value="Kamal">Kamal</option>
                                                    <option value="Piyal">Piyal</option>
                                                </select>

                                            </td>
                                            <td>
                                                <input type="button" class="btn btn-primary btn-fullerton" data-accno="<%:item.AccountNo%>" value="Update" />
                                            </td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </div>
                            </div>
                            <div class="tab-pane" id="Beruwala">
                                <div class="row">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>Account No</th>
                                                <th>Address</th>
                                                <th>Payment Date</th>
                                                <th>Reconnected Date</th>
                                                <th>Reconected By</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <%var BeruwalapaymentDetailModel = paymentDetailModel.Where(item => item.Depot == "Beruwala");
                                          foreach (var item in BeruwalapaymentDetailModel)
                                          {%>
                                        <tr>
                                            <td class="accNo" onclick="updateDepot(<%:item.AccountNo %>)">
                                                <a class="accNumber"><%:item.AccountNo %></a>
                                                <div class="modal" id="<%:item.AccountNo %>" title="ChangeDepot">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="Select Depot" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Depot:</label>
                                                            <div class="col-sm-9 selectStation">
                                                                <select>
                                                                    <option value="Fullerton">Fullerton</option>
                                                                    <option value="Mathugama">Mathugama</option>
                                                                    <option value="Beruwala">Beruwala</option>
                                                                    <option value="Panadura">Panadura</option>
                                                                    <option value="Agalawatta">Agalawatta</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="changeWalkOrder" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Walk Order:</label>
                                                            <div class="col-sm-2">
                                                                <input type="text" id = "ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox"/>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2"/>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class ="row">
                                                        <input type="button" class="btn btn-primary btn-UpdateDepot" data-accno="<%:item.AccountNo%>" value="Update" />
                                                    </div>
                                                </div>
                                            </td>
                                            <%if (item.AddressLine1 == null)
                                              {%>
                                            <td></td>
                                            <%}
                                              else
                                              {%>
                                            <td class="address"><%:item.AddressLine1%><br />
                                                <%:item.AddressLine2%><br />
                                                <%:item.AddressLine3%>
                                            </td>
                                            <%}%>
                                            <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                            <td class="reconnectedDate">
                                                <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                            </td>
                                            <td class="reconnectedBy ">
                                                <select>
                                                    <option value="Ruwan">Ruwan</option>
                                                    <option value="Nuwan">Nuwan</option>
                                                    <option value="Kamal">Kamal</option>
                                                    <option value="Piyal">Piyal</option>
                                                </select>

                                            </td>
                                            <td>
                                                <input type="button" class="btn btn-primary btn-fullerton" data-accno="<%:item.AccountNo%>" value="Update" />
                                            </td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </div>
                            </div>

                            <div class="tab-pane" id="Panadura">
                                <div class="row">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>Account No</th>
                                                <th>Address</th>
                                                <th>Payment Date</th>
                                                <th>Reconnected Date</th>
                                                <th>Reconected By</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <%var PanadurapaymentDetailModel = paymentDetailModel.Where(item => item.Depot == "Panadura");
                                          foreach (var item in PanadurapaymentDetailModel)
                                          {%>
                                        <tr>
                                            <td class="accNo" onclick="updateDepot(<%:item.AccountNo %>)">
                                                <a class="accNumber"><%:item.AccountNo %></a>
                                                <div class="modal" id="<%:item.AccountNo %>" title="ChangeDepot">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="Select Depot" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Depot:</label>
                                                            <div class="col-sm-9 selectStation">
                                                                <select>
                                                                    <option value="Fullerton">Fullerton</option>
                                                                    <option value="Matugama">Mathugama</option>
                                                                    <option value="Beruwala">Beruwala</option>
                                                                    <option value="Panadura">Panadura</option>
                                                                    <option value="Agalawatta">Agalawatta</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="changeWalkOrder" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Walk Order:</label>
                                                            <div class="col-sm-2">
                                                                <input type="text" id = "ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox"/>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2"/>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class ="row">
                                                        <input type="button" class="btn btn-primary btn-UpdateDepot" data-accno="<%:item.AccountNo%>" value="Update" />
                                                    </div>
                                                </div>
                                            </td>
                                            <%if (item.AddressLine1 == null)
                                              {%>
                                            <td></td>
                                            <%}
                                              else
                                              {%>
                                            <td class="address"><%:item.AddressLine1%><br />
                                                <%:item.AddressLine2%><br />
                                                <%:item.AddressLine3%>
                                            </td>
                                            <%}%>
                                            <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                            <td class="reconnectedDate">
                                                <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                            </td>
                                            <td class="reconnectedBy">
                                                <select>
                                                    <option value="Ruwan">Ruwan</option>
                                                    <option value="Nuwan">Nuwan</option>
                                                    <option value="Kamal">Kamal</option>
                                                    <option value="Piyal">Piyal</option>
                                                </select>

                                            </td>
                                            <td>
                                                <input type="button" class="btn btn-primary btn-fullerton" data-accno="<%:item.AccountNo%>" value="Update" />
                                            </td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </div>
                            </div>

                            <div class="tab-pane" id="Agalawatta">
                                <div class="row">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>Account No</th>
                                                <th>Address</th>
                                                <th>Payment Date</th>
                                                <th>Reconnected Date</th>
                                                <th>Reconected By</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <%var AgalawattapaymentDetailModel = paymentDetailModel.Where(item => item.Depot == "Agalawatta");
                                          foreach (var item in AgalawattapaymentDetailModel)
                                          {%>
                                        <tr>
                                            <td class="accNo" onclick="updateDepot(<%:item.AccountNo %>)">
                                                <a class="accNumber"><%:item.AccountNo %></a>
                                                <div class="modal" id="<%:item.AccountNo %>" title="ChangeDepot">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="Select Depot" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Depot:</label>
                                                            <div class="col-sm-9 selectStation">
                                                                <select>
                                                                    <option value="Fullerton">Fullerton</option>
                                                                    <option value="Mathugama">Mathugama</option>
                                                                    <option value="Beruwala">Beruwala</option>
                                                                    <option value="Panadura">Panadura</option>
                                                                    <option value="Agalawatta">Agalawatta</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="changeWalkOrder" class="col-sm-3 control-label" style="padding-left: 0px; padding-top: 0px">Walk Order:</label>
                                                            <div class="col-sm-2">
                                                                <input type="text" id = "ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox ReaderCode"/>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2"/>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class ="row">
                                                        <input type="button" class="btn btn-primary btn-UpdateDepot" data-accno="<%:item.AccountNo%>" value="Update" />
                                                    </div>
                                                </div>
                                            </td>
                                            <%if (item.AddressLine1 == null)
                                              {%>
                                            <td></td>
                                            <%}
                                              else
                                              {%>
                                            <td class="address"><%:item.AddressLine1%><br />
                                                <%:item.AddressLine2%><br />
                                                <%:item.AddressLine3%>
                                            </td>
                                            <%}%>
                                            <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                            <td class="reconnectedDate">
                                                <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                            </td>
                                            <td class="reconnectedBy">
                                                <select>
                                                    <option value="Ruwan">Ruwan</option>
                                                    <option value="Nuwan">Nuwan</option>
                                                    <option value="Kamal">Kamal</option>
                                                    <option value="Piyal">Piyal</option>
                                                </select>

                                            </td>
                                            <td>
                                                <input type="button" class="btn btn-primary btn-fullerton" data-accno="<%:item.AccountNo%>" value="Update" />
                                            </td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </div>
                            </div>

                        </div>


                    </div>




                </div>
            </div>

            <div id="DepotUpdateModal" class="modal" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Update Depot Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="form-group">
                                    <label for="AccountNo" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Account No:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="TextBoxPaymentDetailsAccountNo" runat="server" class="form-control" ReadOnly="true" Enabled="False"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group">
                                    <label for="Depot" class="col-sm-2 control-label" style="padding-left: -3px; padding-top: 0px">Payment Method:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="DepotList" runat="server" Width="200px">
                                            <asp:ListItem Text="Fullerton" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Mathugama" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Beruwala" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Panadura" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Agalawatta" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="ButtonUpdate" class="btn btn-primary btn-UpdateDepot" runat="server" Text="Update" type="submit" />
                        </div>
                    </div>
                </div>



            </div>

        </div>


    </div>
    <%--</div>--%>

    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>--%>




    <%--    <script type="text/javascript">
        $("[id*=btnModalPopup]").live("click", function () {
            $("#modal_dialog").dialog({
                title: "jQuery Modal Dialog Popup",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
            return false;
        });
</script>--%>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/json2/20130526/json2.min.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />

    <script type="text/javascript">UpdatePayment


        $(".btn-fullerton").click(function () {
            debugger;
            var $row = $(this).closest("tr");

            var address = $row.find(".address").text().trim();
            var accountNo = $row.find(".accNo .accNumber").text().trim();
            var paymentDate = $row.find(".paymentDy").text().trim();
            var reconnectedDate = $row.find(".reconnectedDate .recon").val().trim();
            var disconnectedBy = $row.find(".reconnectedBy select option:selected").text().trim();
            var PaymentMode = $row.find(".reconnectedDate .paymentMethod").val().trim();

            var paymentModel = {
                accountNo: accountNo,
                address: address,
                paymentDate: paymentDate,
                reconnectedDate: reconnectedDate,
                disconnectedBy: disconnectedBy,
                PaymentMode: PaymentMode
            };

            $.ajax({
                type: "POST",
                url: "PaymentList.aspx/UpdatePayment",
                data: JSON.stringify(paymentModel),
                contentType: "application/json",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });

        });


        function Submit(acc) {
            ShowCurrentTime(acc);

            return false;
        }

        function ShowCurrentTime(acc) {
            
            //var da = $(this).data('accno');

            $.ajax({
                type: "GET",
                url: "PaymentList.aspx/UpdatePayment",
                data: { 'accNo': acc },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }
    </script>

    <script type="text/javascript">
        
        function updateDepot(accNo) {
            debugger;
            $("#" + accNo).dialog({
                autoOpen: false,
            });

            $("#" + accNo).dialog("open");

        }
    </script>

    <script type="text/javascript">
        debugger;
        $(".btn-UpdateDepot").click(function updateDepotAndWalkOrder() {
            debugger;
            var acc = $(this).data('accno');
            var dialog = $("#" + acc);
            var station = dialog.find(".selectStation select option:selected").text();
            var readerCode = dialog.find("#ReaderCode").val();
            var dailyPackNo = dialog.find("#DailyPackNo").val();
            var walkSequence = dialog.find("#WalkSequence").val();

           // $row.find(".reconnectedBy select option:selected").text().trim();

            var depotModel = {
                accountNo: acc,
                depot: station,
                readerCode: readerCode,
                dailyPackNo: dailyPackNo,
                walkSequence: walkSequence,
            };

            $.ajax({
                type: "POST",
                url: "PaymentList.aspx/UpdateDepot",
                data: JSON.stringify(depotModel),
                contentType: "application/json",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });

        });
        </script>


</asp:Content>

