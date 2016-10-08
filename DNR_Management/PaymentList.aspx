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
        .resizedTextbox {
            width: 60px;
        }

        .resizedTextbox2 {
            width: 80px;
        }
    </style>

    <div class="container" style="margin-top: 50px; background: lightgrey; margin-left: 20px; margin-right: 02px; width: 1300px">
        <div class="row" style="width: 1300px; height: 520px">
            <div class="col-sm-12">
                <div class="row">
                    <div class="row">
                        <ul class="nav nav-tabs" style="margin-left: 30px">
                            <li class="active"><a data-toggle="tab" href="#PaymentDetails">Payment Details</a></li>
                            <li><a data-toggle="tab" href="#ReconnectionRecords">Reconnection Records</a></li>
                        </ul>
                    </div>
                    <div class="row" style="margin-top: 25px">
                        <div class="tab-content" style="margin-left: 15px">
                            <div id="PaymentDetails" class="tab-pane fade in active">

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
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                            <th>Status</th>
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
                                                                            <input type="text" id="ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
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
                                                        <td class="ContactNo"><%:item.ContactNo %></td>
                                                        <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                                        <td class="reconnectedDate">
                                                            <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                            <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                                        </td>
                                                        <td class="reconnectedBy">
                                                            <select>
                                                                <option value="CEB Area Office">CEB Area Office</option>
                                                                <option value="CEB Fullerton">CEB Fullerton</option>
                                                                <option value="CEB Mathugama">CEB Mathugama</option>
                                                                <option value="CEB Beruwala">CEB Beruwala</option>
                                                                <option value="CEB Panadura">CEB Panadura</option>
                                                                <option value="CEB Agalawatta">CEB Agalawatta</option>
                                                                <option value="Edirisinghe H.W.B.D">Edirisinghe H.W.B.D</option>
                                                                <option value="Abeysena M.P.A.K">Abeysena M.P.A.K</option>
                                                                <option value="Sanjeewa M.">Sanjeewa M.</option>
                                                                <option value="Wipula K.H.V">Wipula K.H.V</option>
                                                                <option value="Jayawardana S.G">Jayawardana S.G</option>
                                                                <option value="Wijenayake K.">Wijenayake K.</option>
                                                            </select>

                                                        </td>

                                                        <td class="updateStatus">
                                                            <%:item.status%>
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
                                            <div class="row">
                                                <table class="table" id="table_Mathugama">
                                                    <thead>
                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                            <th>Status</th>
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
                                                                            <input type="text" id="ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
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
                                                        <td class="ContactNo"><%:item.ContactNo %></td>
                                                        <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                                        <td class="reconnectedDate">
                                                            <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                            <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                                        </td>
                                                        <td class="reconnectedBy">
                                                            <select>
                                                                <option value="CEB Area Office">CEB Area Office</option>
                                                                <option value="CEB Fullerton">CEB Fullerton</option>
                                                                <option value="CEB Mathugama">CEB Mathugama</option>
                                                                <option value="CEB Beruwala">CEB Beruwala</option>
                                                                <option value="CEB Panadura">CEB Panadura</option>
                                                                <option value="CEB Agalawatta">CEB Agalawatta</option>
                                                                <option value="Edirisinghe H.W.B.D">Edirisinghe H.W.B.D</option>
                                                                <option value="Abeysena M.P.A.K">Abeysena M.P.A.K</option>
                                                                <option value="Sanjeewa M.">Sanjeewa M.</option>
                                                                <option value="Wipula K.H.V">Wipula K.H.V</option>
                                                                <option value="Jayawardana S.G">Jayawardana S.G</option>
                                                                <option value="Wijenayake K.">Wijenayake K.</option>
                                                            </select>

                                                        </td>
                                                        <td class="updateStatus">
                                                            <%:item.status%>
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
                                                <table class="table" id="table_Beruwala">
                                                    <thead>
                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                            <th>Status</th>
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
                                                                            <input type="text" id="ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
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
                                                        <td class="ContactNo"><%:item.ContactNo %></td>
                                                        <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                                        <td class="reconnectedDate">
                                                            <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                            <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                                        </td>
                                                        <td class="reconnectedBy ">
                                                            <select>
                                                                <option value="CEB Area Office">CEB Area Office</option>
                                                                <option value="CEB Fullerton">CEB Fullerton</option>
                                                                <option value="CEB Mathugama">CEB Mathugama</option>
                                                                <option value="CEB Beruwala">CEB Beruwala</option>
                                                                <option value="CEB Panadura">CEB Panadura</option>
                                                                <option value="CEB Agalawatta">CEB Agalawatta</option>
                                                                <option value="Edirisinghe H.W.B.D">Edirisinghe H.W.B.D</option>
                                                                <option value="Abeysena M.P.A.K">Abeysena M.P.A.K</option>
                                                                <option value="Sanjeewa M.">Sanjeewa M.</option>
                                                                <option value="Wipula K.H.V">Wipula K.H.V</option>
                                                                <option value="Jayawardana S.G">Jayawardana S.G</option>
                                                                <option value="Wijenayake K.">Wijenayake K.</option>
                                                            </select>

                                                        </td>
                                                        <td class="updateStatus">
                                                            <%:item.status%>
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
                                                <table class="table" id="table_Panadura">
                                                    <thead>
                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                            <th>Status</th>
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
                                                                            <input type="text" id="ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
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
                                                        <td class="ContactNo"><%:item.ContactNo %></td>
                                                        <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                                        <td class="reconnectedDate">
                                                            <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                            <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                                        </td>
                                                        <td class="reconnectedBy">
                                                            <select>
                                                                <option value="CEB Area Office">CEB Area Office</option>
                                                                <option value="CEB Fullerton">CEB Fullerton</option>
                                                                <option value="CEB Mathugama">CEB Mathugama</option>
                                                                <option value="CEB Beruwala">CEB Beruwala</option>
                                                                <option value="CEB Panadura">CEB Panadura</option>
                                                                <option value="CEB Agalawatta">CEB Agalawatta</option>
                                                                <option value="Edirisinghe H.W.B.D">Edirisinghe H.W.B.D</option>
                                                                <option value="Abeysena M.P.A.K">Abeysena M.P.A.K</option>
                                                                <option value="Sanjeewa M.">Sanjeewa M.</option>
                                                                <option value="Wipula K.H.V">Wipula K.H.V</option>
                                                                <option value="Jayawardana S.G">Jayawardana S.G</option>
                                                                <option value="Wijenayake K.">Wijenayake K.</option>
                                                            </select>

                                                        </td>
                                                        <td class="updateStatus">
                                                            <%:item.status%>
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
                                                <table class="table" id="table_Agalawatta">
                                                    <thead>
                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                            <th>Status</th>
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
                                                                            <input type="text" id="ReaderCode" name="ReaderCode" value="<%:item.ReaderCode%>" class="resizedTextbox ReaderCode" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="DailyPackNo" name="DailyPackNo" value="<%:item.DailyPackNo%>" class="resizedTextbox" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <input type="text" id="WalkSequence" name="WalkSequence" value="<%:item.WalkSequence%>" class="resizedTextbox2" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
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
                                                        <td class="ContactNo"><%:item.ContactNo %></td>
                                                        <td class="paymentDy"><%:item.PaymentDate.Date %></td>
                                                        <td class="reconnectedDate">
                                                            <input data-format="yyyy-MM-dd hh:mm:ss" class="recon" type="date" />
                                                            <input type="hidden" value="<%:item.paymentMethod%>" class="paymentMethod" />
                                                        </td>
                                                        <td class="reconnectedBy">
                                                            <select>
                                                                <option value="CEB Area Office">CEB Area Office</option>
                                                                <option value="CEB Fullerton">CEB Fullerton</option>
                                                                <option value="CEB Mathugama">CEB Mathugama</option>
                                                                <option value="CEB Beruwala">CEB Beruwala</option>
                                                                <option value="CEB Panadura">CEB Panadura</option>
                                                                <option value="CEB Agalawatta">CEB Agalawatta</option>
                                                                <option value="Edirisinghe H.W.B.D">Edirisinghe H.W.B.D</option>
                                                                <option value="Abeysena M.P.A.K">Abeysena M.P.A.K</option>
                                                                <option value="Sanjeewa M.">Sanjeewa M.</option>
                                                                <option value="Wipula K.H.V">Wipula K.H.V</option>
                                                                <option value="Jayawardana S.G">Jayawardana S.G</option>
                                                                <option value="Wijenayake K.">Wijenayake K.</option>
                                                            </select>
                                                        </td>
                                                        <td class="updateStatus">
                                                            <%:item.status%>
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
                            <div id="ReconnectionRecords" class="tab-pane fade">

                                <div class="col-xs-1">
                                    <!-- required for floating -->
                                    <!-- Nav tabs -->
                                    <ul class="nav nav-tabs tabs-left sideways">
                                        <li class="active " style="width: 95px"><a href="#FullertonExcel" data-toggle="tab">Fullerton</a></li>
                                        <li class="but tab3" style="width: 95px"><a href="#MathugamaExcel" data-toggle="tab">Mathugama</a></li>
                                        <li class="but tab3" style="width: 95px"><a href="#BeruwalaExcel" data-toggle="tab">Beruwala</a></li>
                                        <li class="but tab3" style="width: 95px"><a href="#PanaduraExcel" data-toggle="tab">Panadura</a></li>
                                        <li class="but tab3" style="width: 95px"><a href="#AgalawattaExcel" data-toggle="tab">Agalawatta</a></li>
                                    </ul>
                                </div>
                                <div class="col-xs-11">
                                    <div class="tab-content" style="margin-left: 15px">

                                        <div class="tab-pane active" id="FullertonExcel">
                                            <div class="row">
                                                <table id="table_fullertonExcel" class="table">
                                                    <thead>

                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Walk Order</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Payment Mode</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                        </tr>
                                                    </thead>
                                                    <%var FullertonReconnectionList = reconnctionlist.Where(item => item.Depot == "Fullerton");
                                                      foreach (var item in FullertonReconnectionList)
                                                      {%>
                                                    <tr>
                                                        <td><%:item.AccountNo %></td>
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
                                                        <td><%:item.ReaderCode %> / <%:item.DailyPackNo %> /<%:item.WalkSeq %> </td>
                                                        <td><%:item.ContactNo %></td>
                                                        <td><%:item.PaymentDate %></td>
                                                        <td><%:item.PaymentMethod %></td>
                                                        <td><%:item.ReconnectedDate %></td>
                                                        <td><%:item.ReconnectedBy %></td>
                                                    </tr>
                                                    <%}
                                                    %>
                                                </table>
                                            </div>
                                            <div class ="row">
                                                <input type="button" class = "btn btn-primary" id ="btn-Excell-fullerton" value ="Download Excel"  style ="margin-left: 70px"/>
                                                <input type="button" class = "btn btn-primary" id ="btn-Save-fullerton" data-table="table_fullertonExcel" value ="Save"  style ="margin-left: 70px; display:none"/>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="MathugamaExcel">
                                            <div class="row">
                                                <table id="table_MathugamaExcel" class="table">
                                                    <thead>

                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Walk Order</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Payment Mode</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                        </tr>
                                                    </thead>
                                                    <%var MathugamaReconnectionList = reconnctionlist.Where(item => item.Depot == "Mathugama");
                                                      foreach (var item in MathugamaReconnectionList)
                                                      {%>
                                                    <tr>
                                                        <td><%:item.AccountNo %></td>
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
                                                        <td><%:item.ReaderCode %> / <%:item.DailyPackNo %> /<%:item.WalkSeq %> </td>
                                                        <td><%:item.ContactNo %></td>
                                                        <td><%:item.PaymentDate %></td>
                                                        <td><%:item.PaymentMethod %></td>
                                                        <td><%:item.ReconnectedDate %></td>
                                                        <td><%:item.ReconnectedBy %></td>
                                                    </tr>
                                                    <%}
                                                    %>
                                                </table>
                                            </div>
                                            <div class ="row">
                                                <input type="button" class = "btn btn-primary" id ="btn-Excell-Mathugama" value ="Download Excel"  style ="margin-left: 70px"/>
                                                <input type="button" class = "btn btn-primary" id ="btn-Save-Mathugama" data-table="table_MathugamaExcel" value ="Save"  style ="margin-left: 70px; display:none"/>
                                            </div>
                                        </div>

                                        <div class="tab-pane" id="BeruwalaExcel">
                                            <div class="row">
                                                <table id="table_BeruwalaExcel" class="table">
                                                    <thead>

                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Walk Order</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Payment Mode</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                        </tr>
                                                    </thead>
                                                    <%var BeruwalaReconnectionList = reconnctionlist.Where(item => item.Depot == "Beruwala");
                                                      foreach (var item in BeruwalaReconnectionList)
                                                      {%>
                                                    <tr>
                                                        <td><%:item.AccountNo %></td>
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
                                                        <td><%:item.ReaderCode %> / <%:item.DailyPackNo %> /<%:item.WalkSeq %> </td>
                                                        <td><%:item.ContactNo %></td>
                                                        <td><%:item.PaymentDate %></td>
                                                        <td><%:item.PaymentMethod %></td>
                                                        <td><%:item.ReconnectedDate %></td>
                                                        <td><%:item.ReconnectedBy %></td>
                                                    </tr>
                                                    <%}
                                                    %>
                                                </table>
                                            </div>
                                            <div class ="row">
                                                <input type="button" class = "btn btn-primary" id ="btn-Excell-Beruwala" value ="Download Excel"  style ="margin-left: 70px"/>
                                                <input type="button" class = "btn btn-primary" id ="btn-Save-Beruwala" value ="Save"  style ="margin-left: 70px; display:none"/>
                                            </div>
                                        </div>

                                        <div class="tab-pane" id="PanaduraExcell">
                                            <div class="row">
                                                <table id="table_PanaduraExcell" class="table">
                                                    <thead>
                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Walk Order</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Payment Mode</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                        </tr>
                                                    </thead>
                                                    <%var PanaduraReconnectionList = reconnctionlist.Where(item => item.Depot == "Panadura");
                                                      foreach (var item in PanaduraReconnectionList)
                                                      {%>
                                                    <tr>
                                                        <td><%:item.AccountNo %></td>
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
                                                        <td><%:item.ReaderCode %> / <%:item.DailyPackNo %> /<%:item.WalkSeq %> </td>
                                                        <td><%:item.ContactNo %></td>
                                                        <td><%:item.PaymentDate %></td>
                                                        <td><%:item.PaymentMethod %></td>
                                                        <td><%:item.ReconnectedDate %></td>
                                                        <td><%:item.ReconnectedBy %></td>
                                                    </tr>
                                                    <%}
                                                    %>
                                                </table>
                                            </div>
                                            <div class ="row">
                                                <input type="button" class = "btn btn-primary" id ="btn-Excell-Panadura" value ="Download Excel"  style ="margin-left: 70px"/>
                                                <input type="button" class = "btn btn-primary" id ="btn-Save-Panadura" data-table="table_PanaduraExcell" value ="Save"  style ="margin-left: 70px; display:none"/>
                                            </div>
                                        </div>

                                        <div class="tab-pane" id="AgalawattaExcel">
                                            <div class="row">
                                                <table id="table_AgalawattaExcel" class="table">
                                                    <thead>
                                                        <tr>
                                                            <th>Account No</th>
                                                            <th>Address</th>
                                                            <th>Walk Order</th>
                                                            <th>Contact No</th>
                                                            <th>Payment Date</th>
                                                            <th>Payment Mode</th>
                                                            <th>Reconnected Date</th>
                                                            <th>Reconected By</th>
                                                        </tr>
                                                    </thead>
                                                    <%var AgalawattaReconnectionList = reconnctionlist.Where(item => item.Depot == "Agalawatta");
                                                      foreach (var item in AgalawattaReconnectionList)
                                                      {%>
                                                    <tr>
                                                        <td><%:item.AccountNo %></td>
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
                                                        <td><%:item.ReaderCode %> / <%:item.DailyPackNo %> /<%:item.WalkSeq %> </td>
                                                        <td><%:item.ContactNo %></td>
                                                        <td><%:item.PaymentDate %></td>
                                                        <td><%:item.PaymentMethod %></td>
                                                        <td><%:item.ReconnectedDate %></td>
                                                        <td><%:item.ReconnectedBy %></td>
                                                    </tr>
                                                    <%}
                                                    %>
                                                </table>
                                            </div>
                                            <div class ="row">
                                                <input type="button" class = "btn btn-primary" id ="btn-Excell-Agalawatta" value ="Download Excel"  style ="margin-left: 70px"/>
                                                <input type="button" class = "btn btn-primary" id ="btn-Save-Agalawatta" data-table="table_AgalawattaExcel" value ="Save"  style ="margin-left: 70px; display:none"/>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <%-- END OF DIVE ROW --%>
        </div>
        <%-- END OF DIVE COL --%>
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
    <script src="Script/JS/jquery.battatech.excelexport.min.js"></script>
    <script src="Script/JS/customjs.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />

    <script type="text/javascript">


        $(".btn-fullerton").click(function () {
            debugger;
            var $row = $(this).closest("tr");

            var address = $row.find(".address").text().trim();
            var accountNo = $row.find(".accNo .accNumber").text().trim();
            var paymentDate = $row.find(".paymentDy").text().trim();
            var reconnectedDate = $row.find(".reconnectedDate .recon").val().trim();
            var disconnectedBy = $row.find(".reconnectedBy select option:selected").text().trim();
            var PaymentMode = $row.find(".reconnectedDate .paymentMethod").val().trim();

            if (reconnectedDate != "") {
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
                    url: "PaymentList.aspx/insertReconnectionDetails",
                    data: JSON.stringify(paymentModel),
                    contentType: "application/json",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
            else {
                alert("Please Select Reconnected Date");
            }

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

    <script type ="text/javascript">

        
        $("#btn-Save-fullerton").click(function () {
            debugger;
            var table = $(this).attr('data-table');
            debugger
            PostTable(table);
        });

        $("#btn-Save-Mathugama").click(function () {
            debugger;
            var table = $(this).attr('data-table');
            debugger
            PostTable(table);
        });

        $("#btn-Save-Beruwala").click(function () {
            debugger;
            var table = $(this).attr('data-table');
            debugger
            PostTable(table);
        });

        $("#btn-Save-Panadura").click(function () {
            debugger;
            var table = $(this).attr('data-table');
            debugger
            PostTable(table);
        });

        $("#btn-Save-Agalawatta").click(function () {
            debugger;
            var table = $(this).attr('data-table');
            debugger
            PostTable(table);
        });


        function GetRow(rowNum, table) {

            //var tableRow = $('#table_fullertonExcel tbody tr').eq(rowNum);
            var tableRow = $('#'+table+ ' tbody tr').eq(rowNum);


            var row = {};
            //row.ChangeType = tableRow.find('td:eq(1)').text();
            //row.UpdateType = tableRow.find('td:eq(2)').text();
            //row.Part = tableRow.find('td:eq(5)').text();

            row.AccountNo = tableRow.find('td:eq(0)').text();
            row.Address = tableRow.find('td:eq(1)').text();
            row.WalkOrder = tableRow.find('td:eq(2)').text();
            row.ContactNo = tableRow.find('td:eq(3)').text();
            row.PaymentDate = tableRow.find('td:eq(4)').text();
            row.PaymentMode = tableRow.find('td:eq(5)').text();
            row.ReconnectedDate = tableRow.find('td:eq(6)').text();
            row.ReconectedBy = tableRow.find('td:eq(7)').text();


            return row;
        }

            // Read all rows
        function GetAllRows(table) {
                var dataRows = [];

                $('#'+ table+ ' tbody tr').each(function (index, value) {
                    var currentRow = GetRow(index, table);
                    dataRows.push(currentRow);
                });

                return dataRows;
            }

            function PostTable(table) {
                    //var crossId = getParameterByName('id');
                var jsonRequest = { rows: GetAllRows(table) };

                    $.ajax({
                        type: 'POST',
                        url: 'PaymentList.aspx/UpdatePayment',
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


</asp:Content>

