<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageFiles.aspx.cs" Inherits="Download"  MasterPageFile="~/Masters/DialogMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //<![CDATA[
        function OnClientFolderLoaded(sender, args) {
            //alert("OnClientFolderLoaded");
        }
        function OnClientFileOpen(sender, args) {
            //var item = args.get_item();
            PageMethods.linkFileEvent(1, 'test');
        }
        function OnClientItemSelected(sender, args) {
            //alert("OnClientItemSelected");
        }
        function OnClientFolderChange(sender, args) {
            args.get_path();
        }
        function OnClientDelete(sender, args) {
            alert("OnClientDelete");
            alert("You cant Delete...");
        }
        function OnClientCreateNewFolder(sender, args) {
            //alert("OnClientCreateNewFolder");
        }
        function OnClientMove(sender, args) {
            //alert("OnClientMove");
        }
        function OnClientLoad(sender, args) {
            //alert("Opening File Manager");
            //alert("OnClientLoad");
        }

        function OnClientCopy(sender, args) {
            //alert("OnClientCopy");
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" 
        EnablePageMethods="true" 
        EnablePartialRendering="true" runat="server" />
    <telerik:RadFileExplorer EnableOpenFile="true" runat="server" ID="FileExplorer1" Width="100%" EnableCopy="true"
            OnClientFileOpen="OnClientFileOpen" VisibleControls="AddressBox, ContextMenus, FileList, Grid, ListView, Toolbar"
            OnClientCreateNewFolder="OnClientCreateNewFolder"
            OnClientDelete="OnClientDelete"
            OnClientFolderChange="OnClientFolderChange"
            OnClientFolderLoaded="OnClientFolderLoaded"
            OnClientItemSelected="OnClientItemSelected"
            OnClientLoad="OnClientLoad"
            OnClientMove="OnClientMove"
            OnClientCopy="OnClientCopy">
        <Configuration EnableAsyncUpload="true" ViewPaths="~/App_ClientData/TaskOrders/OrdersCompleted" UploadPaths="~/App_ClientData/TaskOrders/OrdersCompleted/" />
    </telerik:RadFileExplorer>
</asp:Content>
