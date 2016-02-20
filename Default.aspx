<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td align="center">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr><td align="left">
                            <telerik:RadButton RenderMode="Lightweight" ID="BtnAdmin" runat="server" Font-Size="Large" Font-Bold="true" Text="Health Record Registration"
                            ButtonType="LinkButton" Skin="WebBlue" Height="45" Width="250px" NavigateUrl="~/AdminLogin.aspx" ToolTip="Automated coming soon!!!">
                            </telerik:RadButton>
                    </td></tr>
                    <tr><td style="height:10px"></td></tr>
                    <tr><td align="left">
                            <telerik:RadButton RenderMode="Lightweight" ID="BtnClient" runat="server" Font-Size="Large" Font-Bold="true" Text="User Login"
                            ButtonType="LinkButton" Skin="WebBlue" Height="45" Width="250px" NavigateUrl="~/ClientLogin.aspx" ToolTip="Automated coming soon!!!">
                            </telerik:RadButton>
                    </td></tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

