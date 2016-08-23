<%@ Page Title="" Language="C#" MasterPageFile="~/DNR_Manager.Master" AutoEventWireup="true" CodeBehind="Tester.aspx.cs" Inherits="DNR_Manager.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Script/Jqury/jquery-ui.js"></script>
    <script src="Script/Jqury/jquery-ui.min.js"></script>
    <script src="Script/Jqury/jquery.js"></script>

    <script type="text/javascript">
        $("[id*=butoTest]").live("click", function () {
        $("#dialog").dialog({
            title: "jQuery Dialog Popup",
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
        return false;
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="container" style ="margin-top:20px; background: lightgrey; margin-left: 20px; margin-right: 02px; width: 1300px; height:500px">
        <div class ="row">
            <div class ="col-sm-1">
                <asp:Button id ="butoTest" class="btn btn-default" runat="server" Text="Test"/>
                <div id="dialog" style="display: none">
    This is a simple popup
</div>
            </div>
        </div>
    </div>
</asp:Content>
