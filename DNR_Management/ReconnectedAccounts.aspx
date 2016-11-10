<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="ReconnectedAccounts.aspx.cs" Inherits="DNR_Manager.WebForm13" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 50px; margin-left: 20px; margin-right: 02px; width: 1300px;height: 520px">
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
                                <input type="button" id="btnDenerate" class="btn btn-default" value="Generate Report" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>Account No</th>
                        <th>Walk Order</th>
                        <th>Reconnected By</th>
                        <th>Reconnected Date</th>
                        <th>Paymemt Date</th>
                        <th>Payment Method</th>
                    </tr>
                </thead>
            </table>
        </div>

     </div>

    <script type="text/javascript">
        $('#btnGenerate')(function () {
            var FromDate = $('#ReportStartDate').val();
            var EndDate = $('#ReportEndDate').val();

        });
    </script>

</asp:Content>
