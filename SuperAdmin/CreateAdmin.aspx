<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SuperAdmin.master" AutoEventWireup="true" CodeFile="CreateAdmin.aspx.cs" Inherits="CreateAdmin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Invalid email address. Please correct and try again.";
                    return false;
                }
                if ($find("<%=radtxt_address1.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Enter Address";
                    $find("<%=radtxt_address1.ClientID %>").focus();
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
                if (document.getElementById("<%=radcombo_role.ClientID %>").value == "Select") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Select Role";

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

                $find("<%=radtxt_zip.ClientID %>").set_value("");
                $find("<%=radtxt_phone.ClientID %>").set_value("");
                $find("<%=radtxt_cellno.ClientID %>").set_value("");
                document.getElementById("<%=btn_create.ClientID %>").value = "Create";

                document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "";
                return false;
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
                            <td style="color:#4B6C9E; font-size:13px; font-weight:bold" align="left">
                                Manage Admins
                            </td>
                            <td align="right">
                                (<span class="star">*</span>) indicates mandatory fields
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="lbl_errormsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
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
                                                    Address 2<br />
                                                    <telerik:RadTextBox ID="radtxt_address2" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Country<span class="star">*</span><br />
                                                    <%--<telerik:RadTextBox ID="radtxt_country" Width="160px" runat="server" ></telerik:RadTextBox>--%>
                                                    <telerik:RadComboBox ID="radtxt_country" runat="server" Width="160px"  
                                                        AutoPostBack="True" 
                                                        OnSelectedIndexChanged="radtxt_country_SelectedIndexChanged"
                                                     />
                                                     <%--<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                                          SelectCommand="SELECT [CountryID], [CountryName] FROM [Countries] ORDER BY [CountryID]">
                                                     </asp:SqlDataSource>--%>
                                                </td>
                                                
                                               
                                            </tr>
                                           
                                            <tr> 
                                                 <td align="left" class="label_display">
                                                    State/Province<span class="star">*</span><br />
                                                    <telerik:RadComboBox ID="radcombo_state" runat="server" Width="160px"  
                                                     
                                                     />
                                                     <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                                          SelectCommand="SELECT [StateID], [StateName] FROM [States] ORDER BY [StateName]">
                                                     </asp:SqlDataSource>--%>
                                                </td>
                                                <td align="left" class="label_display">
                                                    City<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_city" Width="160px" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Zip<span class="star">*</span><br />
                                                    <telerik:RadMaskedTextBox ID="radtxt_zip" Width="160px" Mask="#####" runat="server" ></telerik:RadMaskedTextBox>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Phone<span class="star">*</span><br />
                                                    <telerik:RadMaskedTextBox ID="radtxt_phone" Width="160px" Mask="(###)###-####" runat="server" ></telerik:RadMaskedTextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" class="label_display">
                                                    Cell Number<span class="star">*</span><br />
                                                    <telerik:RadMaskedTextBox ID="radtxt_cellno"  Mask="(###)###-####" Width="160px" runat="server" ></telerik:RadMaskedTextBox>
                                                    
                                                </td>
                                                <td><br /><asp:CheckBox ID="chk_sendmsg" runat="server" />Send Text Message</td>
                                                <td align="left">
                                                    Role<span class="star">*</span><br />
                                                    <telerik:RadComboBox ID="radcombo_role" runat="server" Width="160px"  
                                                     
                                                     />
                                                     <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                                          SelectCommand="SELECT [userRoleID], [userRole] FROM [SuperUserRoles] ORDER BY [userRoleID]">
                                                     </asp:SqlDataSource>--%>
                                                </td>
                                                <td  align="left">
                                                    Status<br />
                                                    <telerik:RadComboBox ID="radcombo_status" runat="server" Width="160px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Active" Value="Active" />
                                                            <telerik:RadComboBoxItem Text="InActive" Value="InActive" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
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
                            <td align="center" colspan="2">
                                <asp:Button ID="btn_create" OnClientClick="javascript:return validation();"  runat="server" 
                                    Text="Create" onclick="btn_create_Click" ></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btn_reset"  runat="server" 
                                    Text="Reset" OnClick="btn_reset_Click"></asp:Button>
                                    <%--<asp:Button ID="Button1"  runat="server" 
                                    Text="Reset" OnClientClick="javascript:return resetall();"></asp:Button>--%>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr><td style="height:30px"></td></tr>
            <tr>
                <td align="center">
                    
                    <table>
                        <tr>
                            <td>
                                <fieldset class="register"  style="width:900px">
                                    <legend>Existing Accounts</legend>
                                    <telerik:RadGrid  ID="radgrid_admindetails"
                                          AllowPaging="true" AllowSorting="true" GridLines="None" PageSize="5"
                                            DataSourceID="SqlDataSource3" runat="server" AllowFilteringByColumn="false" OnItemCommand="radgrid_admindetails_ItemCommand">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="true"></Selecting>
                                        </ClientSettings>
                                        <SelectedItemStyle BackColor="Yellow"></SelectedItemStyle>
                                        <EditItemStyle CssClass="EditedItem" Height="25px"></EditItemStyle> 
                                        <MasterTableView DataKeyNames="userID" GridLines="Horizontal" AutoGenerateColumns="false" NoMasterRecordsText="No Data have been added." >             
                                          <HeaderStyle HorizontalAlign="left" />
                                          <ItemStyle HorizontalAlign="Left" />
                                          <AlternatingItemStyle HorizontalAlign="Left" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="firstName" HeaderText="FirstName" UniqueName="firstName"
                                                            SortExpression="firstName">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="lastName" HeaderText="LastName" UniqueName="lastName"
                                                            SortExpression="lastName">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="userRole" HeaderText="Role" UniqueName="userRole"
                                                            SortExpression="userRole">
                                                        </telerik:GridBoundColumn>
                                                         <telerik:GridBoundColumn DataField="status" HeaderText="Status" UniqueName="status"
                                                            SortExpression="status">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="phone" HeaderText="Phone" UniqueName="Phone"
                                                            SortExpression="Phone">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="email" HeaderText="Email" UniqueName="email"
                                                            SortExpression="email">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="caddress" HeaderText="Address" UniqueName="caddress"
                                                            SortExpression="caddress">
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridButtonColumn CommandName="Edit" Text="Edit" UniqueName="EditCommandColumn"
                                                        ButtonType="LinkButton" >
                                                      </telerik:GridButtonColumn>
                                                    </Columns> 
                                         </MasterTableView> 
                                        <%--<ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True"> </ClientSettings> --%>
                                    </telerik:RadGrid> 
                                    <asp:SqlDataSource ID="SqlDataSource3" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                        SelectCommand="select *,(address+' '+address2) as caddress from SuperUsers s,SuperUserRoles r where s.userRoleID=r.userRoleID" runat="server"></asp:SqlDataSource>
                                   
                                    
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hid_edituserid" runat="server" />
         </ContentTemplate>
            <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btn_create" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click"></asp:AsyncPostBackTrigger>

            </Triggers>
            </asp:UpdatePanel>
        
        <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
         <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="radtxt_country" >
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="radcombo_state" />
                     
                 </UpdatedControls>
             </telerik:AjaxSetting>
             
         </AjaxSettings> 
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />--%>
</asp:Content>

