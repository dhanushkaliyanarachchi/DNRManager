<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="MonthlyReport.aspx.cs" Inherits="DNR_Manager.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 50px; margin-left: 20px; margin-right: 02px; width: 1300px">
        <div class="row">
            <div class="col-lg-10">
                <div class="row">
                    <div class="form-horizontal" style="margin-top: 20px">
                        <div class="form-group">
                            <label for="FromDate" class="col-sm-2 control-label" style="padding-left: 0px; padding-top: 0px">From:</label>
                            <div class="col-sm-2">
                                <input type="date" id="ReportStartDate" class="form-control" />
                            </div>
                            <label for="ToDate" class="col-sm-1 control-label" style="padding-left: 0px; padding-top: 0px">To:</label>
                            <div class="col-sm-2">
                                <input type="date" id="ReportEndDate" class="form-control" />
                            </div>
                            <div class="col-sm-2">
                                <input type="button" id="btnGenerate" class="btn btn-default" value="Generate Report" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="form-horizontal" style="margin-top: 20px">
                <div class="form-group">
                    <label for="NoOfD_ns" class="col-sm-2 control-label" style="padding-left: 0px; padding-top: 0px">No of Disconnections:</label>
                    <div class="col-sm-5">
                        <input type="text" id="NoofDisconnections" class="form-control" readonly="readonly" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="NoOfReCon_n" class="col-sm-2 control-label" style="padding-left: 0px; padding-top: 0px; margin-top: 20px">No of Reconnections:</label>
                    <div class="col-sm-5">
                        <input type="text" id="NoofReconnections" class="form-control" readonly="readonly" style="margin-top: 20px" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="NoOfDisCon_nNot" class="col-sm-2 control-label" style="padding-left: 0px; padding-top: 0px; margin-top: 20px">No of Disconnections Not Yet Reconnected:</label>
                    <div class="col-sm-5">
                        <input type="text" id="NoOfDisCon_nNotR" class="form-control" readonly="readonly" style="margin-top: 20px" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="NoofOrderCards" class="col-sm-2 control-label" style="padding-left: 0px; padding-top: 0px; margin-top: 20px">No of Order Cards Sent:</label>
                    <div class="col-sm-5">
                        <input type="text" id="NoofOrderCards" class="form-control" readonly="readonly" style="margin-top: 20px" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="NoOfMeterRemovals" class="col-sm-2 control-label" style="padding-left: 0px; padding-top: 0px; margin-top: 20px">No of Meter Removals:</label>
                    <div class="col-sm-5">
                        <input type="text" id="NoOfMeterRemovals" class="form-control" readonly="readonly" style="margin-top: 20px" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="NoOfFinalizedAccounts" class="col-sm-2 control-label" style="padding-left: 0px; padding-top: 0px; margin-top: 20px">No of Finalized Accounts:</label>
                    <div class="col-sm-5">
                        <input type="text" id="NoOfFinalizedAccounts" class="form-control" readonly="readonly" style="margin-top: 20px" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#btnGenerate').click(function () {
            debugger
            var FromDate = $('#ReportStartDate').val();
            var EndDate = $('#ReportEndDate').val();

            var reportModel = {
                FromDate: FromDate,
                EndDate: EndDate
            };

            $.ajax({
                type: "POST",
                url: "MonthlyReport.aspx/getReportDetailsToUi",
                data: JSON.stringify(reportModel),
                contentType: "application/json",
                dataType: "json",
                success: callBack,
                failure: function (response) {
                    alert(response.d);
                }

            });

            function callBack(response) {
                var model = response.d;
                $('#NoofDisconnections').val(model.DisconnectionCount);
                $('#NoofReconnections').val(model.ReconnectionCount);
                $('#NoOfDisCon_nNotR').val(model.DisconnectionNotYetReconnectCount);
                $('#NoofOrderCards').val(model.OrderCardCount);
                $('#NoOfMeterRemovals').val(model.MeterRemovalCount);
                $('#NoOfFinalizedAccounts').val(model.FinalizedAccountCount);
            }
        });
    </script>
</asp:Content>
