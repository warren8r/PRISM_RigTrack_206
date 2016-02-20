<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="Login" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
 <script type="text/javascript">
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validation() {

                if ($find("<%=radtxt_username.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Enter Login Name";
                    $find("<%=radtxt_username.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_pwd.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Enter Password";
                    $find("<%=radtxt_pwd.ClientID %>").focus();
                    return false;
                }
                


            }

           

        </script>
     </telerik:RadCodeBlock>
   
   <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr><td style="height:20px"></td></tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lbl_message" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
               <td align="center">
                <table style="border:solid 1px #000000; background-color:#f1f1f1" border="0" cellpadding="0" cellspacing="0"> 
                    <tr>
                        <td class="smallheader" colspan="2">
                            <span style="color:White; padding-left:10px"><b>Login</b></span>
                        </td>
                    </tr>
                    <tr><td style="height:10px"></td></tr>
                    <tr> 
                   
                         <td style="padding-left:10px">Login Name:<span class="star">*</span></td> 
                         <td style="padding-right:10px"><telerik:RadTextBox ID="radtxt_username" Width="160px" runat="server" ></telerik:RadTextBox></td> 
                         
                    </tr>
                    <tr><td style="height:10px"></td></tr>
                    <tr> 
                    
                         <td style="padding-left:10px">Password:<span class="star">*</span></td> 
                         <td  style="padding-right:10px"><telerik:RadTextBox ID="radtxt_pwd" TextMode="Password" Width="160px" runat="server" ></telerik:RadTextBox></td> 
                         
                    </tr>
                    <tr><td style="height:10px"></td></tr>                    
                    <tr> 
                        <td></td>
                        <td  align="left"> 
                        <asp:Button ID="btnSubmit" OnClientClick="javascript:return validation();" runat="server" Text="Login"   onclick="btnSubmit_Click"/>
                        </td> 
                    </tr> 
                    <tr><td style="height:10px"></td></tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:LinkButton ID="lnk_forgotpwd" runat="server" 
                                Text="Can't access your account?" onclick="lnk_forgotpwd_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr><td style="height:10px"></td></tr>
                </table>
               </td>
            </tr>
        </table>
</asp:Content>

