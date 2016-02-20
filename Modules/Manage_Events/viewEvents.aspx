<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="viewEvents.aspx.cs" Inherits="Modules_Manage_Events_viewEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function mngRequestStarted(ajaxManager, eventArgs) {
                if (eventArgs.get_eventTarget().indexOf("mngBtn") != -1)
                    eventArgs.set_enableAjax(false);
            }
            //            $(document).ready(function () {
            //                var myExportButton = document.getElementById('ctl00$ContentPlaceHolder1$RadGrid1$ctl00$ctl02$ctl00$ExportToExcelButton');
            //                myExportButton.set_enableAjax(false);
            //            });

            //<![CDATA[
            function OpenFileExplorerDialog(obj1) {
                var wnd = $find("<%= ManageFiles.ClientID %>");

                wnd.setUrl("../../ManageTaskOrderDocs.aspx?eventid=" + obj1 + "");
                wnd.show();

                return false;
            }

            function OpenUsersAssigned(eventcode) {
                var users = $find("<%= UsersAssignedView.ClientID %>");
                users.setUrl('../../../UsersAssignedView.aspx?eventid=' + eventcode);
                users.show();

            }


            function OpenTaskLog(objeventamiid) {
                var taskLg = $find("<%= TaskLog.ClientID %>");
                taskLg.setUrl("../../TaskLog.aspx?eventamiid=" + objeventamiid + "");
                taskLg.show();
                return false;
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            //]]>
        </script>
    </telerik:RadScriptBlock>


        <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>

              <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>View Events</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                   

                </asp:Table>
       
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadWindow runat="server" Width="700px" Height="560px" VisibleStatusbar="false"
                ShowContentDuringLoad="false" NavigateUrl="~/ManageFiles.aspx" ID="ManageFiles"
                BackColor="#acb790" Modal="true" Behaviors="Close,Move">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Width="700px" Height="560px" VisibleStatusbar="false"
                ShowContentDuringLoad="false" NavigateUrl="~/TaskLog.aspx" ID="TaskLog" BackColor="#acb790"
                Modal="true" Behaviors="Close,Move">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Width="700px" Height="560px" VisibleStatusbar="false"
                BackColor="#acb790" ShowContentDuringLoad="false" NavigateUrl="~/UsersAssignedView.aspx"
                ID="UsersAssignedView" Modal="true" Behaviors="Close,Move">
            </telerik:RadWindow>
            <table border="0" width="100%">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl_message" ForeColor="Red"></asp:Label>
                
                    <asp:Label runat="server" ID="Label1" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    Start Date:<br />
                                    <telerik:RadDatePicker ID="StartDate" runat="server" Style="float: left;">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    End Date:<br />
                                    <telerik:RadDatePicker ID="EndDate" runat="server" Style="float: left;">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                <br />
                                    <telerik:RadComboBox runat="server" ID="combo_Event"  DataSourceID="SqlGetEvents" DataTextField="eventAndCode" DataValueField="id" ></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlGetEvents" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                        SelectCommand="SELECT eventName + ' ' + '(' + eventCode + ')' AS eventAndCode, events.id from events"></asp:SqlDataSource>
                                </td>
                                <td>
                                <br />
                                    <telerik:RadDropDownList ID="ActionList" runat="server" DataSourceID="UserActionList"
                                    AutoPostBack="true" DataTextField="actionName" DataValueField="id" AppendDataBoundItems="True"
                                    DefaultMessage="-Select Action-" Width="150px" DropDownHeight="200px">
                                    <Items>
                                        <telerik:DropDownListItem runat="server" DropDownList="UserActionList" Text="- Select Action -"
                                            Value="-1" />
                                    </Items>
                                </telerik:RadDropDownList>
                                </td>
                                <td>
                                <br />
                                    <telerik:RadButton ID="ViewFilterSelection" runat="server" Text="View" OnClick="ViewFilterSelection_Click">
                                    </telerik:RadButton>
                                    &nbsp;
                                    <telerik:RadButton ID="ResetFilterBtn" runat="server" Text="Reset" OnClick="ResetFilter">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" GridLines="None" AllowPaging="True"
                    Width="1020px" AllowSorting="True" AutoGenerateColumns="False"
                    OnItemCommand="RadGrid1_ItemCommand" OnPageIndexChanged="RadGrid1_PageIndexChanged">
                    <ExportSettings HideStructureColumns="true">
                        <Excel Format="Biff" />
                    </ExportSettings>
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView CommandItemDisplay="Top" >
                        <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true">
                        </CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                                <telerik:GridBoundColumn DataField="id" FilterControlAltText="Filter id column"
                                HeaderText="Event Id" SortExpression="id" UniqueName="id" DataType="System.Int32" Visible="false">
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="eventTime" FilterControlAltText="Filter eventTime column"  DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="Event Date" SortExpression="eventTime" UniqueName="eventTime">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventName" FilterControlAltText="Filter eventName column"
                                HeaderText="Event Name" SortExpression="eventName" UniqueName="eventName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventCode" FilterControlAltText="Filter v column"
                                HeaderText="Event Code" SortExpression="eventCode" UniqueName="eventCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventInfo" FilterControlAltText="Filter eventInfo column"
                                HeaderText="Event Information" SortExpression="eventInfo" UniqueName="eventInfo">
                                <ItemStyle Width="300px" />
                            </telerik:GridBoundColumn>
                            
                        </Columns>
                       <%-- <NestedViewTemplate>
                            <div class="input">
                                <span class="label"></span>
                                <div class="input">
                                    <asp:Button ID="ManageFilesBtn" runat="server" Text="Manage Task Orders" OnClientClick='<%# Eval("eventcodeid", "OpenFileExplorerDialog({0});return false;") %>'>
                                    </asp:Button>
                                </div>
                            </div>
                            <div class="input">
                                <span class="label"></span>
                                <div class="input">
                                    <asp:Button ID="TaskLogBtn" runat="server" Text="View Task Log" OnClientClick='<%# String.Format("OpenTaskLog({0});return false;",Eval("eventamiid"))%> '>
                                    </asp:Button>
                                </div>
                            </div>
                            <div class="input">
                                <span class="label"></span>
                                <div class="input">
                                    <asp:Button ID="UssersInEventsBtn" runat="server" Text="View Assigned Users" OnClientClick='<%# Eval("EventCode","OpenUsersAssigned(\"{0}\"); return false;") %>'>
                                    </asp:Button>
                                </div>
                            </div>
                            <div class="input">
                                <span class="label"></span>
                                <div class="input">
                                    <telerik:RadDropDownList ID="ChangeAction" DefaultMessage="-Select Action-" AppendDataBoundItems="true"
                                        runat="server" DataSourceID="UserActionList" DataTextField="actionName" DataValueField="id">
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                            <div class="input">
                                <span class="label"></span>
                                <div class="input">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Save"
                                        CommandName="Update1"></asp:Button>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="hidd_id" Text='<%# Eval("eventamiid") %>' Style="display: none;"></asp:TextBox>
                        </NestedViewTemplate>--%>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                        <HeaderStyle VerticalAlign="Bottom" />
                    </MasterTableView>
                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <ExportSettings SuppressColumnDataFormatStrings="false" ExportOnlyData="true">
                        <Excel Format="Biff" />
                    </ExportSettings>
                </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div id="dataSource">
            
                <asp:SqlDataSource ID="MasterTableData" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM events ev,PrismEvent pev where ev.eventCode=pev.eventCode order by eventTime desc"
                    UpdateCommand="UPDATE eventAMI SET userActionId = @userActionId WHERE id = @Eventamiid">
                </asp:SqlDataSource>
                <%--<asp:SqlDataSource ID="users" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT Users.userID, Users.firstName + ' ' + Users.lastName AS fullName FROM Users LEFT JOIN UserRoles ON Users.userRoleID = UserRoles.userRoleID WHERE userID IN (select userId FROM eventUser WHERE eventId=@EventID) ORDER BY fullname ASC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="EventList" Name="EventID" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                <asp:SqlDataSource ID="UserActionList" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT [actionName], [id] FROM [userAction] ORDER BY actionName ASC">
                </asp:SqlDataSource>
            </div>
            <telerik:RadAjaxLoadingPanel runat="server" ID="loader">
            </telerik:RadAjaxLoadingPanel>
        </ContentTemplate>

         
    </asp:UpdatePanel>

    <div style="text-align: center;" class="DivFooter">
        <hr style="width: 777px" />
        Copyright&copy;2015 - Fortech Energy, Inc. - All Rights Reserved<br />
    </div>
</asp:Content>
