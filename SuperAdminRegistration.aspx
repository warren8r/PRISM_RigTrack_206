<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SuperAdminRegistration.aspx.cs" Inherits="SuperAdminRegistration" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MDM: Super Admin Registration</title>
    <link href="css/main.css" type="text/css" rel="Stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validation() {
                
                if ($find("<%=radtxt_login.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Login Name";
                    $find("<%=radtxt_login.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_fname.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter First Name";
                    $find("<%=radtxt_fname.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_lastname.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Last Name";
                    $find("<%=radtxt_lastname.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_email.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Email";
                    $find("<%=radtxt_email.ClientID %>").focus();
                    return false;
                }
                var aaa = validateEmail($find("<%=radtxt_email.ClientID %>").get_textBoxValue());
                if (aaa == false) {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML="Invalid email address. Please correct and try again.";
                    return false;
                }
                if ($find("<%=radtxt_address1.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Address";
                    $find("<%=radtxt_address1.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_city.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter City";
                    $find("<%=radtxt_city.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_country.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Country";
                    $find("<%=radtxt_country.ClientID %>").focus();
                    return false;
                }
                
                if ($find("<%=radtxt_zip.ClientID %>").get_textBoxValue() == "__________") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Zip";
                    $find("<%=radtxt_zip.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_phone.ClientID %>").get_textBoxValue() == "(___)-______") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Phone Number";
                    $find("<%=radtxt_phone.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_cellno.ClientID %>").get_textBoxValue() == "__________") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Cell Number";
                    $find("<%=radtxt_cellno.ClientID %>").focus();
                    return false;
                }


            }

            function validateEmail(elementValue) {
                var emailRegEx = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
                return emailRegEx.test(elementValue);

            }

            function resetall() {
                $find("<%=radtxt_login.ClientID %>").set_value("");
                $find("<%=radtxt_fname.ClientID %>").set_value("");
                $find("<%=radtxt_lastname.ClientID %>").set_value("");
                $find("<%=radtxt_email.ClientID %>").set_value("");
                $find("<%=radtxt_address1.ClientID %>").set_value("");
                $find("<%=radtxt_address2.ClientID %>").set_value("");
                $find("<%=radtxt_city.ClientID %>").set_value("");
                $find("<%=radtxt_country.ClientID %>").set_value("");
                $find("<%=radtxt_zip.ClientID %>").set_value("");
                $find("<%=radtxt_phone.ClientID %>").set_value("");
                $find("<%=radtxt_cellno.ClientID %>").set_value("");
                document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "";
                return false;
            }

            function isNumberKey(evt) {
                alert(evt.which);
                var charCode = (evt.which) ? evt.which : event.keyCode;
                if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            } 
        </script>
     </telerik:RadCodeBlock>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="background-color:#4B6C9E; height:60px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left" style="padding-left:10px">
                                <b ><a href="Index.aspx" style="color:White; font-size:25px; text-decoration:none">MDM</a></b>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right">
                                (<span class="star">*</span>) indicates mandatory fields
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lbl_errormsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                 <fieldset class="register"  style="width:900px">
                                    <legend>Account Details</legend>
                                        <table>
                                            <tr> 
                                                <td align="left" class="label_display">
                                                    Login Name<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_login" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    First Name<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_fname" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Last Name<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_lastname" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Title<br />
                                                    <telerik:RadTextBox ID="radtxt_title" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                
                                            </tr>
                                           
                                            <tr> 
                                                <td align="left" class="label_display">
                                                    Email<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_email" Width="160px" runat="server" ></telerik:RadTextBox>
                                                    <%--<asp:RegularExpressionValidator ID="emailValidator" runat="server" Display="Dynamic"
                                                    ErrorMessage="Please, enter valid e-mail address." ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                                    ControlToValidate="radtxt_email">
                                                </asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" Display="Dynamic"
                                                    ControlToValidate="radtxt_email" ErrorMessage="Please, enter an e-mail!"></asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Address<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_address1" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Address 2<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_address2" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    City<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_city" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                               
                                            </tr>
                                           
                                            <tr> 
                                                 <td align="left" class="label_display">
                                                    State/Province<span class="star">*</span><br />
                                                    <telerik:RadComboBox ID="radcombo_state" runat="server" Width="160px"  
                                                     DataSourceID="SqlDataSource1" DataTextField="StateName" DataValueField="StateID"
                                                     />
                                                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                                          SelectCommand="SELECT [StateID], [StateName] FROM [MyState] ORDER BY [StateName]">
                                                     </asp:SqlDataSource>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Country<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_country" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Zip<span class="star">*</span><br />
                                                    <telerik:RadMaskedTextBox ID="radtxt_zip" Width="160px" Mask="##########" runat="server" ></telerik:RadMaskedTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Phone<span class="star">*</span><br />
                                                    <telerik:RadMaskedTextBox ID="radtxt_phone" Width="160px" Mask="(###)-######" runat="server" ></telerik:RadMaskedTextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" class="label_display">
                                                    Cell Number<span class="star">*</span><br />
                                                    <telerik:RadMaskedTextBox ID="radtxt_cellno"  Mask="##########" Width="160px" runat="server" ></telerik:RadMaskedTextBox>
                                                    
                                                </td>
                                                <td><br /><asp:CheckBox ID="chk_sendmsg" runat="server" />Send Text Message</td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            
                                        </table>
                                </fieldset>
                            </td>
                            
                        </tr>
                        <%--<tr>
                            <td align="center">
                                <fieldset class="register" style="width:900px">
                                    <legend>Account Information</legend>
                                        <table>
                                            <tr> 
                                                <td align="left" class="label_display">
                                                    Login Name<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_login" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Passord<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_password" TextMode="Password" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Confirm Password<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_confirmpwd" Width="160px" TextMode="Password" onkeyup="checkPasswordMatch()" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td><br /><span id="CustomIndicator">&nbsp;</span> <span id="PasswordRepeatedIndicator" class="Base L0">&nbsp;</span></td>
                                            </tr>
                                            
                                            
                                        </table>
                                </fieldset>
                            </td>
                        </tr>--%>
                        <tr><td style="height:10px"></td></tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btn_create" OnClientClick="javascript:return validation();"  runat="server" 
                                    Text="Create" onclick="btn_create_Click" ></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btn_reset"  runat="server" 
                                    Text="Reset" OnClientClick="javascript:return resetall();"></asp:Button>
                            </td>
                        </tr>

                        
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    </form>
</body>
</html>
