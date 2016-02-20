<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateMosquito_B.aspx.cs" Inherits="Modules_RigTrack_GenerateMosquito_B" %>

<!DOCTYPE html>
       <script type="text/javascript"> 
           function Close() {
                GetRadWindow().close(); // Close the window 
            } 
            function GetRadWindow() { 
                var oWindow = null; 
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 
 
                return oWindow; 
            } 
        </script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                <img src="../../images/ManageCurveGroups/mosquito.png" alt="GenerateMosquito" /><br />
    </div>
        <table>
            <tr>
                       <asp:Button ID="btnSave" runat="server" OnClientClick="Close();" Text="Save" > </asp:Button>
                        &nbsp;
                        &nbsp;
                       <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" > </asp:Button>
                        &nbsp;
                        &nbsp;
                       <asp:Button ID="btnCancel" runat="server" OnClientClick="Close();" Text="Cancel" > </asp:Button>
            </tr>
        </table>
    </form>
</body>
</html>
