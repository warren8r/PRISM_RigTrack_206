<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSignaturetoApproveDataEntry.aspx.cs" Inherits="Modules_Configuration_Manager_AddSignaturetoApproveDataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet/less" type="text/css" href="../../css/mdm.less" />
    <link rel="stylesheet/less" type="text/css" href="../../css/jquery-ui-1.10.3.custom.css" />
    <link href="../../css/main.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"></telerik:RadScriptManager>
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <table>
                <tr>
                    <td>
                        <asp:Label ID="lbl_windowapproveddate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        Date:<asp:Label ID="lbl_date" runat="server" ></asp:Label>
                        <asp:Label ID="lbl_jid" runat="server" style="display:none"></asp:Label>
                    </td>
                </tr>
                                        
                <tr>
                    <td>
                        Digital Signature:<br />
                        <telerik:RadTextBox ID="radcombo_approveby" Width="200px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<telerik:RadButton ID="RadButton2" runat="server" Text="Save" AutoPostBack="true" OnClick="Button2_Click">
                        </telerik:RadButton>--%>
                        <asp:Button ID="btn_createrig" Text="Approve" OnClick="btn_createrig_Click"  runat="server">
                        </asp:Button>
                    </td>
                </tr>
            </table>
    </div>
    </ContentTemplate>
    <Triggers>
         
        <asp:AsyncPostBackTrigger ControlID="btn_createrig" EventName="Click"></asp:AsyncPostBackTrigger>
    </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
