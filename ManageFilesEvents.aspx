<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageFilesEvents.aspx.cs" Inherits="Download" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
    <title></title>
       <script type="text/javascript">
            //<![CDATA[
            function OnClientFolderLoaded(sender, args) {
                LogEvent("Grid folder loaded: " + args.get_item().get_name());
            }
            function OnClientFileOpen(sender, args) {
                alert("Iam opened");
                LogEvent("Item open: " + args.get_item().get_name());
            }
            function OnClientItemSelected(sender, args) {
                LogEvent("Item click: " + args.get_item().get_name());
            }
            function OnClientFolderChange(sender, args) {
                LogEvent("Tree folder change: " + args.get_item().get_name());
            }
            function OnClientDelete(sender, args) {
                LogEvent("Delete: " + args.get_item().get_name());
            }
            function OnClientCreateNewFolder(sender, args) {
                LogEvent("Create new folder: " + args.get_newPath());
            }
            function OnClientMove(sender, args) {
                LogEvent("Item moved to: " + args.get_newPath());
            }
            function OnClientLoad(sender, args) {
                LogEvent("File Explorer loaded!");
            }

            function OnClientCopy(sender, args) {
                // args.set_cancel(true);
                var destinationDirectoryPath = args.get_newPath();
                LogEvent(String.format("Item is copying to {0}", destinationDirectoryPath));
            }
            function LogEvent(text) {
                var d = new Date();
                var dateStr = d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + "." + d.getMilliseconds();
                var eventConsole = $get("eventConsole");
                eventConsole.innerHTML += "[" + dateStr + "] " + text + "<br/>";
            }
            //]]>
       </script>
    </head>
    <body>   
        <form id="form1" runat="server">
            <div>
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
            </div>
            <telerik:RadFileExplorer EnableOpenFile="true" runat="server" ID="FileExplorer1" Width="450" EnableCopy="true"
                    OnClientCreateNewFolder="OnClientCreateNewFolder"
                    OnClientDelete="OnClientDelete"
                    OnClientFileOpen="OnClientFileOpen"
                    OnClientFolderChange="OnClientFolderChange"
                    OnClientFolderLoaded="OnClientFolderLoaded"
                    OnClientItemSelected="OnClientItemSelected"
                    OnClientLoad="OnClientLoad"
                    OnClientMove="OnClientMove"
                    OnClientCopy="OnClientCopy">
                <Configuration EnableAsyncUpload="true" ViewPaths="~/App_ClientData/TaskOrders/eventFiles/" UploadPaths="~/App_ClientData/TaskOrders/eventFiles/"/>
            </telerik:RadFileExplorer>
        </form>
    </body>
</html>
