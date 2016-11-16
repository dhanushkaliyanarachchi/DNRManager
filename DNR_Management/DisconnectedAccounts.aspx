<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="DisconnectedAccounts.aspx.cs" Inherits="DNR_Manager.WebForm12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 50px; margin-left: 20px; margin-right: 02px; width: 1300px; height: 520px">
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
            <table class="table table-bordered" id="NotYetReconnectedReport">
                <thead>
                    <tr>
                        <th>Account No</th>
                        <th id ="WalkOrder">Walk Order</th>
                        <th id="DisconnectedDate">Disconnected Date</th>
                        <th id="DisconnectedBy">Disconnected By</th>
                        <th>Disconnected time</th>
                    </tr>
                </thead>
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
                 url: "DisconnectedAccounts.aspx/getReportDetailsToUi",
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
             var $table = $("#NotYetReconnectedReport");
             //var circleNo = $("#select option:selected").val().toString();
             $("#NotYetReconnectedReport tr:gt(0)").remove();
             $(json).each(function () {
                 var tr = [];
                 tr.push('<tr>');
                 tr.push("<td>" + this.AccountNo + "</td>");
                 tr.push("<td>" + this.ReaderCode + "/" + this.DailyPackNo + "/" + this.WalkSequence + "</td>");
                 tr.push("<td>" + this.DisconnectedDate + "</td>");
                 tr.push("<td>" + this.DisconnectedBy + "</td>");
                 tr.push("<td>" + this.DisconnectedTime + "</td>");
                 tr.push('</tr>');
                 $('#NotYetReconnectedReport').append($(tr.join('')));
             });
         }
    </script>

     <script src="Script/JS/jquery-3.1.1.min.js"></script>
<script src="Script/JS/sortElement.js"></script>
<script type="text/javascript">

    var table = $('table');

    $('#WalkOrder, #DisconnectedDate,#DisconnectedBy')
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

</asp:Content>
