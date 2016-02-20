<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="Modules_MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                document.getElementById("<%=btn_create.ClientID %>").value = "Create";

                document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "";
                return false;
            }         
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
    <telerik:RadAjaxPanel ID="up1" runat="server" LoadingPanelID="loading">
        <contenttemplate>
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
                            <td align="center" >
                                <asp:Label ID="lbl_errormsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center"  valign="top">
                                <table>
                                    <tr>
                                     <td >
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
                                                     <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:local_database %>"
                                                          SelectCommand="SELECT [StateID], [StateName] FROM [MyState] ORDER BY [StateName]">
                                                     </asp:SqlDataSource>
                                                </td>
                                                <td align="left" class="label_display">
                                                    Country<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_country" Width="160px" runat="server" ></telerik:RadTextBox>
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
                                                <td>
                                                    Role<span class="star">*</span><br />
                                                    <telerik:RadComboBox ID="radcombo_role" runat="server" Width="160px"  
                                                     DataSourceID="SqlDataSource2" DataTextField="userRole" 
                                                        DataValueField="userRoleID" 
                                                        onselectedindexchanged="radcombo_role_SelectedIndexChanged" AutoPostBack="True"
                                                     />
                                                      <asp:SqlDataSource ID="SqlDataSource2" runat="server"  ConnectionString="<%$ ConnectionStrings:local_database %>"
                                                          SelectCommand="SELECT  0 AS [userRoleID], 'Select' AS [userRole] UNION SELECT [userRoleID], [userRole] FROM [UserRoles] ORDER BY [userRoleID]">
                                                     </asp:SqlDataSource>
                                                </td>
                                                <td>
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
                                     <tr>
                                        <td>
                                            <fieldset class="register"  style="width:900px">
                                            <legend>Event&#160;Notification&#160;Settings</legend>
                                            <table>
                                                <tr>
                                                    <td align="left">
                                                       <telerik:RadGrid ID="radgrid_notificationstatus"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                                DataSourceID="SqlDataSource_Notifications"  AllowPaging="true" AllowSorting="true"  OnPageIndexChanged="radgrid_notificationstatus_PageIndexChanged"
                                OnItemDataBound="radgrid_notificationstatus_ItemDataBound">
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView   >
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                        <Columns>
                        <telerik:GridTemplateColumn HeaderText="Notification"  >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                    <asp:DropDownList ID="ddl_notification" runat="server" DataValueField="notificationid" DataTextField="notificationType"
                                    DataSourceID="SqlGetnotification"></asp:DropDownList>
                                </ItemTemplate>
                         </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Condition"  Visible="false">
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                    <asp:CheckBox runat="server" ID="isChecked"  />
                                    <asp:Label ID="lbl_statuscheck" runat="server" ></asp:Label>
                                    <asp:Label ID="lbl_notification" runat="server" Text='<%# Bind("eventnotification") %>' Visible="false" ></asp:Label>
                                    <asp:Label ID="lbl_eventid" runat="server" Text='<%# Bind("id") %>' Visible="false" ></asp:Label>
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="eventName"
                                HeaderText="Event&#160;Name" SortExpression="eventName" UniqueName="eventName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventCode"
                                HeaderText="Event&#160;Code" SortExpression="eventCode" UniqueName="eventCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="flagName" HeaderText="Flag&#160;Name" SortExpression="flagName" UniqueName="flagName">
                            </telerik:GridBoundColumn>                                                    
                                </Columns>
                                
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                
                            </MasterTableView>
                            
                        </telerik:RadGrid>
                        <asp:SqlDataSource ID="SqlDataSource_Notifications" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                            SelectCommand="SELECT eventName + ' ' + '(' + eventCode + ')' AS eventAndCode,events.eventnotification, events.id, events.eventName,
 events.eventCode, flag.flagName, coalesce( flag.flagColor, '#fff' ) as flagColor,PUR.status FROM  events , flag , Prism_UserRole_Notification PUR
  where events.flagId = flag.id and events.id=PUR.id  and PUR.userRoleID=@userRoleID  ORDER BY eventName ASC">
  <selectparameters>
		<asp:sessionparameter name="userRoleID" sessionfield="userRoleID" type="Int32" />
	</selectparameters>

  </asp:SqlDataSource>
                                                         <asp:SqlDataSource ID="SqlGetnotification" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                    SelectCommand="select PN.notificationid,PN.notificationType from Prism_NotificationType PN"></asp:SqlDataSource>
                                                       <%-- <asp:RadioButtonList ID="rb_event" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="S">SMS</asp:ListItem>
                                                            <asp:ListItem Value="E">E-mail</asp:ListItem>
                                                            <asp:ListItem Value="B">Both</asp:ListItem>
                                                            <asp:ListItem Value="N">None</asp:ListItem>
                                                        </asp:RadioButtonList>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                            </fieldset>
                                        </td>
                                     </tr>
                              </table>
                             </td>
                            
                        </tr>                      
                        <tr><td style="height:10px"></td></tr>
                        <tr>
                            <td  align="center">
                                <asp:Panel ID="panel_modules" runat="server"></asp:Panel>
                            </td>
                        </tr>
                        <tr><td style="height:10px"></td></tr>
                        <tr>
                            <td align="center" >
                                <asp:Button ID="btn_create" runat="server" 
                                    Text="Update" onclick="btn_create_Click" ></asp:Button>
                                <asp:Button ID="btn_reset" BackColor="Maroon"  runat="server" 
                                    Text="Clear" OnClick="btn_reset_Click"></asp:Button>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>            
        </table>
        <asp:HiddenField ID="hid_edituserid" runat="server" />
        </contenttemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_create" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click"></asp:AsyncPostBackTrigger>
        </triggers>
    </telerik:RadAjaxPanel>
    
</asp:Content>

