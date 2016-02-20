<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientActivation.aspx.cs" Inherits="setpassword" %>

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
            function checkPasswordMatch() {
                var text1 = $find("<%=radtxt_password.ClientID %>").get_textBoxValue();
                var text2 = $find("<%=radtxt_confirmpwd.ClientID %>").get_textBoxValue();

                if (text2 == "") {
                    $get("PasswordRepeatedIndicator").innerHTML = "";
                    $get("PasswordRepeatedIndicator").className = "Base L0";
                }
                else if (text1 == text2) {
                    $get("PasswordRepeatedIndicator").innerHTML = "Match";
                    $get("PasswordRepeatedIndicator").className = "Base L5";
                }
                else {
                    $get("PasswordRepeatedIndicator").innerHTML = "No match";
                    $get("PasswordRepeatedIndicator").className = "Base L1";
                }
            }

            function validation() {

                if ($find("<%=radtxt_password.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Password should not be blank";
                    $find("<%=radtxt_password.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_confirmpwd.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Confirm password should not be blank";
                    $find("<%=radtxt_confirmpwd.ClientID %>").focus();
                    return false;
                }
                if ($get("PasswordRepeatedIndicator").innerHTML == "No match") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Password and confirm password should match";
                    return false;
                }
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
                                <b style="color:White; font-size:25px">MDM</b>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td style="height:10px"></td></tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lbl_message" runat="server" Font-Size="11px" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="div_pwdvisible" runat="server">
                    <fieldset class="register" style="width:900px">
                        <legend>Password</legend>
                            <table>
                                <tr> 
                                    
                                    <td align="left" class="label_display">
                                        Password<span class="star">*</span><br />
                                        <telerik:RadTextBox ID="radtxt_password" TextMode="Password" Width="160px" runat="server" ></telerik:RadTextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="left" class="label_display">
                                        Confirm Password<span class="star">*</span><br />
                                        <telerik:RadTextBox ID="radtxt_confirmpwd" Width="160px" TextMode="Password" onkeyup="checkPasswordMatch()" runat="server" ></telerik:RadTextBox>
                                    </td>
                                    <td><br /><span id="CustomIndicator">&nbsp;</span> <span id="PasswordRepeatedIndicator" class="Base L0">&nbsp;</span></td>
                                </tr>

                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Button ID="btn_submit"  runat="server" 
                                    Text="Submit" onclick="btn_submit_Click" OnClientClick="javascript:return validation();" ></asp:Button>
                                    </td>
                                </tr>
                                            
                                            
                            </table>
                    </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
