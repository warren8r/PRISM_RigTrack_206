<%@ Page Title="Manage Family Users" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="CreateClientUsers.aspx.cs" Inherits="CreateAdmin" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

               <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Manage Family Users</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                   

                </asp:Table>


     <table border="0" cellpadding="0" cellspacing="0" width="100%">
            
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                       
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
                                        <table  align="center" >
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
                                                    User Type<span class="star">*</span><br />
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
                       <%--  <tr>
                            <td align="center"  valign="top">
                                <table>
                                    <tr>
                                     <td >
                                   <fieldset class="register"  style="width:900px">
                                    <legend>Event Management</legend>
                                        <table style="text-align:left">
                                            <tr> 
                                                <td align="left" class="label_display">
                                                Event&#160;Management
                                                </td>
                                                <td align="left"><asp:RadioButtonList ID="rb_evenmgr" runat="server" 
                                                        RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList> </td>
                                             </tr>
                                             <tr>
                                                <td align="left" class="label_display">
                                                 Categories
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBoxList ID="chk_categories" runat="server" 
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Item1</asp:ListItem>
                                                        <asp:ListItem Value="2">Item2</asp:ListItem>
                                                        <asp:ListItem Value="3">Item3</asp:ListItem>
                                                        <asp:ListItem Value="4">Item4</asp:ListItem>
                                                        <asp:ListItem Value="5">Item5</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                             </tr>
                                          </table>
                                        </fieldset>
                                </td>
                                  </tr>
                              </table>
                             </td>
                             </tr>                                                --%>
                        <tr>
                            <td align="center" >
                                <asp:Button ID="btn_create" runat="server" 
                                    Text="Create" onclick="btn_create_Click" ></asp:Button>
                                <asp:Button ID="btn_reset" BackColor="Maroon"  runat="server" 
                                    Text="Clear" OnClick="btn_reset_Click"></asp:Button>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr><td style="line-height:10px"></td></tr>
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                <fieldset class="register"  style="width:900px">
                                    <legend>Existing Accounts</legend>
                                    <telerik:RadGrid  ID="radgrid_admindetails"
                                          AllowPaging="true" AllowSorting="true" GridLines="None" PageSize="5"
            DataSourceID="SqlDataSource3" runat="server" AllowFilteringByColumn="false" 
                                        onitemcommand="radgrid_admindetails_ItemCommand" 
                                        onitemdatabound="radgrid_admindetails_ItemDataBound">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="true"></Selecting>
                                        </ClientSettings>
                                        <SelectedItemStyle BackColor="Yellow"></SelectedItemStyle>
                                        <EditItemStyle CssClass="EditedItem" Height="25px"></EditItemStyle> 
                                        <MasterTableView DataKeyNames="userID" GridLines="Horizontal" AutoGenerateColumns="false" NoMasterRecordsText="No Data have been added." >             
                                            <EditFormSettings>
                                                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" 
                                                    UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                            <PagerStyle PageSizeControlType="RadComboBox" />
                                          <HeaderStyle HorizontalAlign="left" />
                                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                                Visible="True">
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                                Visible="True">
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Status" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <label style='<%# string.Format("{0}", Convert.ToString( Eval("status")) == "Active"? "color: green": "color: red")  %>'>
                                                            <%#  Convert.ToString( Eval("status")) == "Active" ? " " : "In-"%>Active
                                                        </label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="Edit" Text="Edit" UniqueName="EditCommandColumn"
                                                    ButtonType="LinkButton" >
                                                    </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="firstName" HeaderText="First" UniqueName="firstName"
                                                    SortExpression="firstName">
                                                           
                                                </telerik:GridBoundColumn>
                                                        
                                                <telerik:GridBoundColumn DataField="lastName" HeaderText="Last" UniqueName="lastName"
                                                    SortExpression="lastName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="userRole" HeaderText="User Type" UniqueName="userRole"
                                                    SortExpression="userRole">
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
                                            </Columns> 
                                            <%--<NestedViewTemplate>
                                                <telerik:RadGrid ID="history" runat="server" DataSourceID="changes" 
                                                    CellSpacing="0" GridLines="None">
                                                    <MasterTableView AutoGenerateColumns="False" DataSourceID="changes">
                                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                                            Visible="True">
                                                            <HeaderStyle Width="20px" />
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                                            Visible="True">
                                                            <HeaderStyle Width="20px" />
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="loginName" 
                                                                FilterControlAltText="Filter loginName column" HeaderText="Username" 
                                                                SortExpression="loginName" UniqueName="loginName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridCheckBoxColumn DataField="systemNote" DataType="System.Boolean" 
                                                                FilterControlAltText="Filter systemNote column" HeaderText="note" 
                                                                SortExpression="systemNote" UniqueName="systemNote" Visible="false" >
                                                            </telerik:GridCheckBoxColumn>
                                                            <telerik:GridBoundColumn DataField="attributeName" 
                                                                FilterControlAltText="Filter attributeName column" HeaderText="attribute" 
                                                                SortExpression="attributeName" UniqueName="attributeName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="description" 
                                                                FilterControlAltText="Filter description column" HeaderText="description" 
                                                                SortExpression="description" UniqueName="description">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="created" DataType="System.DateTime" 
                                                                FilterControlAltText="Filter created column" HeaderText="created" 
                                                                SortExpression="created" UniqueName="created">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="columnId" DataType="System.Int32" 
                                                                FilterControlAltText="Filter columnId column" HeaderText="column id" 
                                                                SortExpression="columnId" UniqueName="columnId" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <EditFormSettings>
                                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                            </EditColumn>
                                                        </EditFormSettings>
                                                        <PagerStyle PageSizeControlType="RadComboBox" />
                                                    </MasterTableView>
                                                    <PagerStyle PageSizeControlType="RadComboBox" />
                                                    <FilterMenu EnableImageSprites="False">
                                                    </FilterMenu>
                                                </telerik:RadGrid>
                                            </NestedViewTemplate>--%>
                                         </MasterTableView> 
                                        <%--<ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True"> </ClientSettings> --%>
                                        <FilterMenu EnableImageSprites="False">
                                        </FilterMenu>
                                    </telerik:RadGrid> 
                                    <asp:SqlDataSource ID="SqlDataSource3"  ConnectionString="<%$ ConnectionStrings:local_database %>"
                                        SelectCommand="select *,(address+' '+address2) as caddress from Users s,UserRoles r where s.userRoleID=r.userRoleID  order by userID desc" runat="server"></asp:SqlDataSource>
                                   
                                    
                                </fieldset>
                                 <div style="text-align: center;" class="DivFooter">
                                    <hr style="width: 777px" />
                                    Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
                                </div>
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
    <asp:SqlDataSource ID="changes" SelectCommand="SELECT Users.firstName + ' ' + Users.lastName as loginName, transactionLog.systemNote, transactionLog.attributeName, transactionLog.description, transactionLog.created, transactionLog.columnId FROM transactionLog INNER JOIN Users ON transactionLog.userId = Users.userID INNER JOIN Modules ON transactionLog.pageId = Modules.moduleID WHERE (transactionLog.columnId IS NOT NULL AND NOT (transactionLog.columnId = 0 ))"
        runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"></asp:SqlDataSource>
</asp:Content>
