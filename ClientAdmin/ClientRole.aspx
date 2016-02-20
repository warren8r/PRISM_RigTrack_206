<%@ Page Language="C#" Title="Manage User Types/Access" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="ClientRole.aspx.cs" Inherits="Role" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style type="text/css">
        div.RadGrid_Vista .SelectedItem
        {
            background: #CCCCCC;
        }
        div.RadGrid_Vista .SelectedItem td
        {
            border-color: #CCCCCC;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validation() {

                if ($find("<%=radtxt_rolename.ClientID %>").get_textBoxValue() == "") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Enter User Type";
                    $find("<%=radtxt_rolename.ClientID %>").focus();
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


    <asp:UpdatePanel ID="up1" runat="server" >
        <ContentTemplate>

             <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Manage User Types And Access</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                   

                </asp:Table>
          

            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    
                    <tr>
                        <td align="center">
                            <fieldset style="width: 960px;">
                                <legend align="left">User&#160;Type&#160;Information</legend>
                                <table>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <asp:Label ID="lbl_message" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="label_display" valign="top">
                                            Enter User Type <span class="star">*</span><br />
                                            <telerik:RadTextBox ID="radtxt_rolename" Width="160px" runat="server" OnTextChanged="radtxt_rolename_TextChanged"
                                                CssClass="{required:true, messages:{required:'Rolename is required!'}}">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td style="width: 4px">
                                        </td>
                                        <td align="left" class="label_display">
                                            Enter User Type Description<br />
                                            <telerik:RadTextBox ID="radtxt_roledesc" Width="160px" TextMode="MultiLine" Height="50px"
                                                runat="server" CssClass="{required:true, messages:{required:'Role Description is required!'}}">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td align="center" class="label_display" valign="bottom">
                                            <asp:Label ID="lbl_max_roles" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="label_display" valign="bottom" >
                                            <asp:Label ID="lbl_total_usertypes" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                   
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 960px;">
                                <legend align="left">Module Access</legend>
                                <table>
                                    <tr>
                                        <td style="height: 3px" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="panel_modules" runat="server" colspan="3">
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 3px" colspan="3">
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <asp:HiddenField ID="hidden_roleid" runat="server" Visible="false" />
                            <asp:HiddenField ID="hidd_maxcount" Value="0" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <fieldset class="register" style="display: none;width: 960px;" >
                                            <legend>Event&#160;Notification</legend>

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
                            <telerik:GridTemplateColumn HeaderText="Condition" >
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
 events.eventCode, flag.flagName, coalesce( flag.flagColor, '#fff' ) as flagColor FROM 
 events LEFT OUTER JOIN flag ON events.flagId = flag.id  where (events.eventnotification is null OR events.eventnotification <> 'False') ORDER BY eventName ASC"></asp:SqlDataSource>
                                     </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <telerik:RadButton ID="btn_create" runat="server" Text="Create" OnClick="btn_create_Click"
                                OnClientClick="javascript:return validation();" />
                            <telerik:RadButton ID="btn_reset" runat="server" Text="Clear"
                                OnClick="btn_reset_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="line-height: 3px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <fieldset class="register" style="display: inline-block;">
                                            <legend>Existing User Types(s)</legend>
                                            <telerik:RadGrid ShowGroupPanel="true" AutoGenerateColumns="false" ID="grid_roleaccess"
                                                 AllowFilteringByColumn="false" AllowSorting="false"
                                                DataSourceID="SqlDataSource1" ShowFooter="false" runat="server" GridLines="None" AllowPaging="true" OnItemCommand="grid_roleaccess_ItemCommand"
                                                OnItemDataBound="grid_roleaccess_ItemDataBound">
                                               <ClientSettings EnablePostBackOnRowClick="true">
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="false" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                                                <PagerStyle Mode="NextPrevAndNumeric" />
                                                <MasterTableView DataKeyNames="userRoleID" AllowMultiColumnSorting="false" EnableColumnsViewState="false"
                                                    EditMode="InPlace" CommandItemDisplay="None" NoMasterRecordsText="No Data have been added." >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle ForeColor="Black"  />
                                                    <Columns>

                                                    </Columns>
                                                </MasterTableView>
                                                <%--<ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True"> </ClientSettings> --%>
                                            </telerik:RadGrid>
                                            <asp:SqlDataSource ID="SqlDataSource1"
                                             SelectCommand="select distinct u.userRoleID,u.userRole from UserRoles u,UserTypePermissions up,Modules m where u.userRoleID=up.userRoleID and m.moduleID=up.moduleID and m.parentId = '0'"
                                                runat="server"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SQLDataSourceUserAccess" SelectCommand="SELECT * FROM AccessTypes"
                                                runat="server"></asp:SqlDataSource>                                          
                                            <asp:SqlDataSource ID="modules" SelectCommand="SELECT (SELECT moduleName AS parentName FROM Modules AS parent WHERE (child.parentId = moduleID)) AS parentName, moduleName AS name FROM Modules AS child"
                                                runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"></asp:SqlDataSource>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_create" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
