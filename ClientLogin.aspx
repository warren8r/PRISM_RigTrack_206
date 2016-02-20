<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="ClientLogin.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validation() {

                if ($find("<%=radtxt_username.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Email should not be blank";
                    $find("<%=radtxt_username.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_pwd.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Password should not be blank";
                    $find("<%=radtxt_pwd.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_clientcode.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Client Code  should not be blank";
                    $find("<%=radtxt_clientcode.ClientID %>").focus();
                    return false;
                }
            }

        </script>
    </telerik:RadCodeBlock>



    <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
        <ContentTemplate>

            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 20px"></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lbl_message" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table style="border: solid 1px #000000; background-color: #f1f1f1" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="smallheader" colspan="2">
                                    <span style="color: White; padding-left: 10px"><b>User&#160;Login</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>

                                <td style="padding-left: 10px">Username:<span class="star">*</span></td>
                                <td style="padding-right: 10px">
                                    <telerik:RadTextBox ID="radtxt_username" Width="160px" runat="server"></telerik:RadTextBox></td>

                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>

                                <td style="padding-left: 10px">Password:<span class="star">*</span></td>
                                <td style="padding-right: 10px">
                                    <telerik:RadTextBox ID="radtxt_pwd" Width="160px" runat="server" TextMode="Password"></telerik:RadTextBox></td>

                            </tr>
                            <tr visible="false">

                                <td visible="false" style="padding-right: 10px">
                                    <telerik:RadTextBox visible="false" ID="radtxt_clientcode" Width="160px" runat="server" Text="Client_1"></telerik:RadTextBox></td>

                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="left">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Login" OnClientClick="javascript:return validation();" OnClick="btnSubmit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:LinkButton ID="lnk_forgotpwd" runat="server"
                                        Text="Can't access your account?" OnClick="lnk_forgotpwd_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

