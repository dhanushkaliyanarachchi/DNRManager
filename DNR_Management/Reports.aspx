<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="DNR_Manager.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 50px; margin-left: 20px; margin-right: 02px; width: 1300px">
        <div class ="row">
            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Generate Monthly Report" id="ButtonMonthlyReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Disconnection Details" id="ButtonDisconnectionReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Reconnection Details" id="ButtonReconnectionReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

            

        </div>

        <div class ="row">
            
            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Letter Details Report" id="ButtonLetterReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Order Card Detail Repot" id="ButtonOrderCardReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Meter Removal Details Report" id="ButtonMRReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

        </div>

        <div class ="row">
            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Finalized Accounts Details" id="ButtonFinalizedReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Disconnected Not yet Reconnected" id="ButtonDRReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-default">
                    
                    <div class="panel-body" style ="width: 400px ; height: 156px">
                        <input type="button" class="btn btn-default btn-lg btn-block" value="Reconnection List For Adding Disconnection" id="ButtonDDReport" style ="height:130px"/>
                            
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/json2/20130526/json2.min.js"></script>
    <script src="Script/JS/jquery.battatech.excelexport.min.js"></script>
    <script src="Script/JS/customjs.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />
    
    <script type="text/javascript">
        $("#ButtonMonthlyReport").click(function(){
            window.location.href = "MonthlyReport.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonDisconnectionReport").click(function () {
            window.location.href = "DisconnectionDetails.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonReconnectionReport").click(function () {
            window.location.href = "ReconnectionDetails.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonLetterReport").click(function () {
            window.location.href = "LetterDetails.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonOrderCardReport").click(function () {
            window.location.href = "OrderCardDetails.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonMRReport").click(function () {
            window.location.href = "MeterRemovalDetails.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonFinalizedReport").click(function () {
            window.location.href = "FinalizedAccountDetails.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonDRReport").click(function () {
            window.location.href = "DisconnectedAccounts.aspx";
        });
    </script>

    <script type="text/javascript">
        $("#ButtonDDReport").click(function () {
            window.location.href = "ReconnectedAccounts.aspx";
        });
    </script>

</asp:Content>
