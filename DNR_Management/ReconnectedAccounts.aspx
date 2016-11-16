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
                                <input type="button" id="btnGenerate" class="btn btn-default" value="Generate Report" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <table class="table table-bordered tablesorter" id ="DisconnectionDetailsToBeAdded">
                <thead>
                    <tr>
                        <th id="AccountNo">Account No</th>
                        <th>Walk Order</th>
                        <th id ="RCBy">Reconnected By</th>
                        <th id ="RDate">Reconnected Date</th>
                        <th>Paymemt Date</th>
                        <th>Payment Method</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>

     </div>

    <script type="text/javascript">
        $('#btnGenerate').click(function () {
            debugger
            var FromDate = $('#ReportStartDate').val();
            var EndDate = $('#ReportEndDate').val();

            var DateModel = {
                FromDate: FromDate,
                EndDate: EndDate
            };

            $.ajax({
                type: "POST",
                url: "ReconnectedAccounts.aspx/getReportDetailsToUi",
                data: JSON.stringify(DateModel),
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                    Scuccess(response);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        });

        function Scuccess(response) {
            var json = response.d;
            var $table = $("#DisconnectionDetailsToBeAdded");
            //var circleNo = $("#select option:selected").val().toString();
            $("#DisconnectionDetailsToBeAdded tr:gt(0)").remove();
            $(json).each(function () {
                var tr = [];
                tr.push('<tr>');
                tr.push("<td>" + this.AccountNo + "</td>");
                tr.push("<td>" + this.ReaderCode + "/" + this.DailyPackNo + "/" + this.WalkSequence + "</td>");
                tr.push("<td>" + this.ReconnectedBy + "</td>");
                tr.push("<td>" + this.ReconnectedDate + "</td>");
                tr.push("<td>" + this.PaymentDate + "</td>");
                tr.push("<td>" + this.PaymentMode + "</td>");
                tr.push('</tr>');
                $('#DisconnectionDetailsToBeAdded').append($(tr.join('')));
            });
        }
    </script>


<script src="Script/JS/jquery-3.1.1.min.js"></script>
<script src="Script/JS/sortElement.js"></script>
<script type="text/javascript">

    var table = $('table');

    $('#RCBy, #RDate')
        .wrapInner('<span title="sort this column"/>')
        .each(function () {

            var th = $(this),
                thIndex = th.index(),
                inverse = false;

            th.click(function () {

                table.find('td').filter(function () {

                    return $(this).index() === thIndex;

                }).sortElements(function (a, b) {

                    return $.text([a]) > $.text([b]) ?
                        inverse ? -1 : 1
                        : inverse ? 1 : -1;

                }, function () {

                    // parentNode is the element we want to move
                    return this.parentNode;

                });

                inverse = !inverse;

            });

        });

   

</script> 

<script type="text/javascript">



</script>

</asp:Content>
