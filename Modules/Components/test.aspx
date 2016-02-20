<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Modules_Configuration_Manager_JobAssignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
<script type="text/javascript">

    function openwin() {


        window.radopen(null, "window_service");

    }
   
     
</script>
<asp:UpdatePanel runat="server" ID="updPnl1"  UpdateMode="Always">
        
            <ContentTemplate>  
           <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
            <div align="center" class="contactmain">
                <img src="../../images/ManageCurveGroups/1_CreateCurveGroup_best.png" alt="Test" /><br />
             </div>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</telerik:RadAjaxPanel>
</asp:Content>

