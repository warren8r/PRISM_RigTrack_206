<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="ClientForgotPassword.aspx.cs" Inherits="ForgotPassword" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validation() {

                if ($find("<%=radtxt_email.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Enter Email";
                    $find("<%=radtxt_email.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_fp_clientcode.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Enter Client Code";
                    $find("<%=radtxt_fp_clientcode.ClientID %>").focus();
                    return false;
                }
            }
            function validationloginame() {

                if ($find("<%=radtxt_loginame.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Enter Email";
                    $find("<%=radtxt_loginame.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_clientcode.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Enter Client Code";
                    $find("<%=radtxt_clientcode.ClientID %>").focus();
                    return false;
                }
                //radtxt_fp_clientcode
            }
            </script>
     </telerik:RadCodeBlock>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr><td style="height:20px"></td></tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lbl_message" ForeColor="Red" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                 <td align="center">
                    <table>
                        <tr>
                            <td align="center" valign="top">
                                <table style="border:solid 1px #000000; background-color:#f1f1f1" border="0" cellpadding="0" cellspacing="0"> 
                                    <tr>
                                        <td class="smallheader" colspan="2">
                                            <span style="color:White; padding-left:10px"><b>Forgot Login Name</b></span>
                                        </td>
                                    </tr>
                                    <tr><td style="height:10px"></td></tr>
                                    <tr> 
                   
                                         <td style="padding-left:10px">Email:<span class="star">*</span></td> 
                                         <td style="padding-right:10px"><telerik:RadTextBox ID="radtxt_loginame" Width="160px" runat="server" ></telerik:RadTextBox></td> 
                         
                                    </tr>
                                     <tr><td style="height:10px"></td></tr>
                                    <tr> 
                   
                                         <td style="padding-left:10px">Client&#160;Code:<span class="star">*</span></td> 
                                         <td style="padding-right:10px"><telerik:RadTextBox ID="radtxt_clientcode" Width="160px" runat="server" ></telerik:RadTextBox></td> 
                         
                                    </tr>
                    
                                    <tr><td style="height:20px"></td></tr>                    
                                    <tr> 
                                        <td></td>
                                        <td  align="left"> 
                                        <asp:Button ID="btn_submitlogin"  runat="server" Text="Submit" 
                                                OnClientClick="javascript:return validationloginame();" 
                                                onclick="btn_submitlogin_Click" />
                                        </td> 
                                    </tr> 
                    
                    
                                    <tr><td style="height:10px"></td></tr>
                                </table>
                               </td>
                               <td align="center">
                                <table style="border:solid 1px #000000; background-color:#f1f1f1" border="0" cellpadding="0" cellspacing="0"> 
                                    <tr>
                                        <td class="smallheader" colspan="2">
                                            <span style="color:White; padding-left:10px"><b>Forgot Password</b></span>
                                        </td>
                                    </tr>
                                    <tr><td style="height:10px"></td></tr>
                                    <tr> 
                   
                                         <td style="padding-left:10px">Email:<span class="star">*</span></td> 
                                         <td style="padding-right:10px"><telerik:RadTextBox ID="radtxt_email" Width="160px" runat="server" ></telerik:RadTextBox></td> 
                         
                                    </tr>
                                     <tr><td style="height:10px"></td></tr>
                                    <tr> 
                   
                                         <td style="padding-left:10px">Client&#160;Code:<span class="star">*</span></td> 
                                         <td style="padding-right:10px"><telerik:RadTextBox ID="radtxt_fp_clientcode" Width="160px" runat="server" ></telerik:RadTextBox></td> 
                         
                                    </tr>
                                    <tr><td style="height:20px"></td></tr>
                    
                                                      
                                    <tr> 
                                        <td></td>
                                        <td  align="left"> 
                                        <asp:Button ID="btnSubmit"  runat="server" Text="Submit" OnClientClick="javascript:return validation();"  onclick="btnSubmit_Click"/>
                                        </td> 
                                    </tr> 
                    
                    
                                    <tr><td style="height:10px"></td></tr>
                                </table>
                               </td>
                        </tr>
                    </table>
                 </td>
            </tr>
        </table>
</asp:Content>

