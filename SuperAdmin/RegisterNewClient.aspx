<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SuperAdmin.master" AutoEventWireup="true" CodeFile="RegisterNewClient.aspx.cs" Inherits="RegisterNewClient" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validation() {

                if ($find("<%=radtxt_clientcode.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Client Code";
                    $find("<%=radtxt_clientcode.ClientID %>").focus();
                    return false;
                }
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
                if ($find("<%=radtxt_company.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Company Name";
                    $find("<%=radtxt_company.ClientID %>").focus();
                    return false;
                }

                if ($find("<%=radtxt_email.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Email";
                    $find("<%=radtxt_email.ClientID %>").focus();
                    return false;
                }
                var aaa = validateEmail($find("<%=radtxt_email.ClientID %>").get_textBoxValue());
                if (aaa == false) {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Invalid email address. Please correct and try again.";
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
                if (document.getElementById("<%=radtxt_country.ClientID %>").value == "Select") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Select Country";

                    return false;
                }
                if (document.getElementById("<%=radcombo_state.ClientID %>").value == "Select") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Select State";

                    return false;
                }

                if ($find("<%=radtxt_city.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter City";
                    $find("<%=radtxt_city.ClientID %>").focus();
                    return false;
                }

                if ($find("<%=radtxt_zip.ClientID %>").get_textBoxValue() == "_____") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Zip";
                    $find("<%=radtxt_zip.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_phone.ClientID %>").get_textBoxValue() == "(___)___-____") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Phone Number";
                    $find("<%=radtxt_phone.ClientID %>").focus();
                    return false;
                }
                if ($find("<%=radtxt_cellno.ClientID %>").get_textBoxValue() == "(___)___-____") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Cell Number";
                    $find("<%=radtxt_cellno.ClientID %>").focus();
                    return false;
                }


            }

            function validateEmail(elementValue) {
                var emailRegEx = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
                return emailRegEx.test(elementValue);

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
      <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
     <table border="0" cellpadding="0" cellspacing="0" width="100%">
           
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
                                 <fieldset class="register"  >
                                    <legend>Account Details</legend>
                                        <table>
                                            <tr>
                                                <td colspan="2" align="left">
                                                    <table>
                                                        <tr> 
                                                             <td align="left" class="label_display">
                                                                Client Code<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_clientcode" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                            <td align="left" class="label_display">
                                                                Login Name<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_login" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>                                                          
                                                            <td align="left" class="label_display">
                                                                Company<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_company" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>                                                
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                            <%--primary address--%>
                                                 <td valign="top" width="50%" class="style1">
                                                <!-- start primary contact -->
                                                <fieldset class="gis-form">
                                                    <legend>
                                                        Primary&#160;Address
                                                    </legend>
                                                    <table width="100%"  border="0">
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                Address<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_address1" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                            <td align="left" class="label_display">
                                                                Country<span class="star">*</span></td><td>
                                                                <telerik:RadComboBox ID="radtxt_country" runat="server" Width="160px"  
                                                                    AutoPostBack="True" 
                                                                    OnSelectedIndexChanged="radtxt_country_SelectedIndexChanged"/>                                                    
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:2px"></td></tr>
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                Address&#160;2<span class="star">*</span></td><td>
                                                               <telerik:RadTextBox ID="radtxt_address2" Width="160px" runat="server" ></telerik:RadTextBox> </td>
                                                          
                                                             <td align="left" class="label_display">
                                                                State/Province<span class="star">*</span></td><td>
                                                                <telerik:RadComboBox ID="radcombo_state" runat="server" Width="160px" />
                                                    
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:2px"></td></tr>
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                City<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_city" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                
                                                            <td align="left" class="label_display">
                                                                Zip<span class="star">*</span></td><td>
                                                                <telerik:RadMaskedTextBox ID="radtxt_zip" Width="160px" Mask="#####" runat="server" ></telerik:RadMaskedTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:2px"></td></tr>
                                                        <tr>
                                                              <td align="left" class="label_display">
                                                                First&#160;Name<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_fname" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                            <td align="left" class="label_display">
                                                                Last&#160;Name<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_lastname" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:2px"></td></tr>
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                Phone<span class="star">*</span></td><td>
                                                                <telerik:RadMaskedTextBox ID="radtxt_phone" Width="160px" Mask="(###)###-####" runat="server" ></telerik:RadMaskedTextBox>
                                                            </td>
                                                              <td align="left" class="label_display">
                                                                Fax<span class="star">*</span></td><td>
                                                                <telerik:RadMaskedTextBox ID="radtxt_fax" Width="160px" Mask="(###)###-####" runat="server" ></telerik:RadMaskedTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:2px"></td></tr>
                                                         <tr> 
                                                            <td align="left" class="label_display">
                                                                Email<span class="star">*</span></td><td>
                                                                <telerik:RadTextBox ID="radtxt_email" Width="160px" runat="server" ></telerik:RadTextBox>
                                                    
                                                            </td>
                                                              <td align="left" class="label_display">
                                                                    Cell Number<span class="star">*</span></td><td>
                                                                    <telerik:RadMaskedTextBox ID="radtxt_cellno"  Mask="(###)###-####" Width="160px" runat="server" ></telerik:RadMaskedTextBox>
                                                    
                                                                </td>
                                                          </tr>
                                                          <tr><td style="line-height:2px"></td></tr>
                                                          <tr>
                                                            <td colspan="4" align="left" valign="top"><asp:CheckBox ID="chk_sendmsg" runat="server" Text="Send Text Message" /></td>
                                                          </tr>
                                                    </table>
                                                </fieldset>
                                                </td>
                                                  <!-- end primary contact -->
                                                <%--secondary address--%>
                                                 <td width="50%" valign="top" class="style1">
                                                    <!-- secondary -->
                                                    <fieldset class="gis-form">
                                                        <legend>Secondary&#160;Contact</legend>
                                                        <table width="100%" cellpadding="0" border="0">
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                Address</td><td>
                                                                <telerik:RadTextBox ID="rad2_address" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                            <td align="left" class="label_display">
                                                                Country</td><td>
                                                                <telerik:RadComboBox ID="rad2_country" runat="server" Width="160px"  
                                                                    AutoPostBack="True" 
                                                                    OnSelectedIndexChanged="rad2_country_SelectedIndexChanged"/>                                                    
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:6px"></td></tr>
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                Address&#160;2</td><td>
                                                                <telerik:RadTextBox ID="rad2_address2" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                             <td align="left" class="label_display">
                                                                State/Province</td><td>
                                                                <telerik:RadComboBox ID="rad2_state" runat="server" Width="160px" />
                                                    
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:6px"></td></tr>
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                City</td><td>
                                                                <telerik:RadTextBox ID="rad2_city" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                
                                                            <td align="left" class="label_display">
                                                                Zip</td><td>
                                                                <telerik:RadMaskedTextBox ID="rad2_zip" Width="160px" Mask="#####" runat="server" ></telerik:RadMaskedTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:6px"></td></tr>
                                                        <tr>
                                                              <td align="left" class="label_display">
                                                                First&#160;Name</td><td>
                                                                <telerik:RadTextBox ID="rad2_firstname" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                            <td align="left" class="label_display">
                                                                Last&#160;Name</td><td>
                                                                <telerik:RadTextBox ID="rad2_lastname" Width="160px" runat="server" ></telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:6px"></td></tr>
                                                        <tr>
                                                             <td align="left" class="label_display">
                                                                Phone</td><td>
                                                                <telerik:RadMaskedTextBox ID="rad2_phone" Width="160px" Mask="(###)###-####" runat="server" ></telerik:RadMaskedTextBox>
                                                            </td>
                                                              <td align="left" class="label_display">
                                                                Fax</td><td>
                                                                <telerik:RadMaskedTextBox ID="rad2_fax" Width="160px" Mask="(###)###-####" runat="server" ></telerik:RadMaskedTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr><td style="line-height:6px"></td></tr>
                                                         <tr> 
                                                            <td align="left" class="label_display">
                                                                Email</td><td>
                                                                <telerik:RadTextBox ID="rad2_email" Width="160px" runat="server" ></telerik:RadTextBox>
                                                    
                                                            </td>
                                                              <td align="left" class="label_display">
                                                                    Cell Number</td><td>
                                                                    <telerik:RadMaskedTextBox ID="rad2_mobile"  Mask="(###)###-####" Width="160px" runat="server" ></telerik:RadMaskedTextBox>
                                                    
                                                                </td>
                                                          </tr>
                                                          <tr><td style="line-height:6px"></td></tr>
                                                          <tr>
                                                            <td colspan="4" align="left" valign="top"><asp:CheckBox ID="chk2_sendmsg" 
                                                                    runat="server"  Text="Send Text Message"/></td>
                                                          </tr>
                                                    </table>
                                                     </fieldset>
                            <!-- end secondary -->
                                                </td>
                                                <%--secondary address--%>
                                            </tr>
                                              <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                               
                                                                    <td align="left">
                                                                        No. of Assets for the data collection:<br />
                                                                        <%--<asp:TextBox ID="txt_nofassets" runat="server" onchange="return formtextboxes(this);"></asp:TextBox>--%>
                                                                        <telerik:RadTextBox  ID="txt_nofassets" runat="server" ></telerik:RadTextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        Allowed Users:<br />
                                                                        <telerik:RadTextBox ID="radtxt_allowedusers" runat="server" ></telerik:RadTextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                         No. of Users Types:<br />
                                                                        <telerik:RadTextBox ID="radtxt_usertypes" runat="server"  ></telerik:RadTextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        Subscription Period(Months):<br />
                                                                        <telerik:RadTextBox ID="radtxt_subperiod" runat="server" ></telerik:RadTextBox>
                                                                    </td>
                                               
                                                                </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>

                                </fieldset>
                            </td>
                            
                        </tr>
                        <%--<tr>
                            <td align="center">
                                
                                <div style="width: 400px;">
                                     <telerik:RadCaptcha ID="RadCaptcha1" runat="server" ErrorMessage="Page not valid. The code you entered is not valid."
                                          ValidationGroup="Group">
                                     </telerik:RadCaptcha>
                                </div>
                                <asp:Label ID="lblCorrectCode" runat="server" ForeColor="Green"></asp:Label>
                                <br />
                                
                            </td>
                        </tr>--%>
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
                                <asp:Button ID="btn_reset"  runat="server" ForeColor="Maroon" 
                                    Text="Reset" OnClick="btn_reset_Click"></asp:Button>
                            </td>
                        </tr>

                        
                    </table>
                </td>
            </tr>
        </table>
        </ContentTemplate>
            <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btn_create" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click"></asp:AsyncPostBackTrigger>

            </Triggers>
            </asp:UpdatePanel>
</asp:Content>

