<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NFVTestPage.aspx.cs" Inherits="Modules_RigTrack_NFVTestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <asp:Button ID="Btn1" runat="server" OnClick="Btn1_Click"
             Text="Upload" />
    
    </div>
    </form>
</body>
</html>
