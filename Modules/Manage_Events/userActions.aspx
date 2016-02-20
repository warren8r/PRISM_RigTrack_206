<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="userActions.aspx.cs" Inherits="Modules_Manage_Events_userActions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .bold
        {
            font-weight: bold;
            float: left;
            margin-left: 20px;
        }
        th, td
        {
            padding: 10;
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        //<![CDATA[
        function OpenFileExplorerDialog(obj1, objeventamiid) {
            var wnd = $find("<%= ManageFiles.ClientID %>");

            wnd.setUrl("../../ManageUserActionDocument.aspx?eventid=" + obj1 + "&eventamiid=" + objeventamiid + "");
            wnd.show();

            return false;
        }

        function OpenTaskLog(objeventamiid) {
            var taskLg = $find("<%= TaskLog.ClientID %>");
            taskLg.setUrl("../../TaskLog.aspx?eventamiid=" + objeventamiid + "");
            taskLg.show();
            return false;
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptBlock ID="radsc" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
           <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
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
            <telerik:RadWindow runat="server" OnBiffExporting="RadGrid1_BiffExporting" Width="700px"
                Height="560px" VisibleStatusbar="false" ShowContentDuringLoad="false" NavigateUrl="~/ManageFiles.aspx"
                ID="ManageFiles" Modal="true" Behaviors="Close,Move">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Width="700px" Height="560px" VisibleStatusbar="false"
                ShowContentDuringLoad="false" NavigateUrl="~/TaskLog.aspx" ID="TaskLog" Modal="true"
                Behaviors="Close,Move">
            </telerik:RadWindow>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                                    <telerik:RadDatePicker ID="EndDate" runat="server" Style="float: left;" AutoPostBack="true"
                                        OnSelectedDateChanged="EndDate_SelectedDateChanged">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <br />
                                    <telerik:RadDropDownList ID="ActionList" runat="server" DataSourceID="UserActionList"
                                        DataTextField="actionName" DataValueField="id" AppendDataBoundItems="true" Width="150px"
                                        DropDownHeight="150px">
                                        <Items>
                                            <telerik:DropDownListItem Text="- Select User Action -" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td>
                                    <br />
                                    <telerik:RadDropDownList ID="FlagList" runat="server" DataSourceID="flags" DataTextField="flagName"
                                        AppendDataBoundItems="true" DataValueField="id" Width="150px" DropDownHeight="150px">
                                        <Items>
                                            <telerik:DropDownListItem Text="- Select Flag -" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td>
                                    <br />
                                    <telerik:RadButton ID="ViewFilterSelection" runat="server" Text="View" OnClick="ViewFilterSelection_Click">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="ResetFilter" runat="server" Text="Reset" OnClick="ResetFilter1">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div>
                <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" GridLines="None" AllowPaging="True"
                    AllowSorting="True" DataSourceID="MasterTableData" AutoGenerateColumns="False"
                    OnItemCommand="RadGrid1_ItemCommand" 
                    OnPageIndexChanged="RadGrid1_PageIndexChanged">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataSourceID="MasterTableData">
                        <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="TimeStamp" FilterControlAltText="Filter TimeStamp column"
                                HeaderText="Time Stamp" SortExpression="TimeStamp" UniqueName="TimeStamp" DataType="System.DateTime">
                                <ItemStyle CssClass="options" Width="80px" />
                            </telerik:GridBoundColumn>
                            <%--                        <telerik:GridBoundColumn DataField="Event_Id" FilterControlAltText="Filter Event_Id column"
                            HeaderText="Event Id" SortExpression="Event_Id" UniqueName="Event_Id" DataType="System.Int32">
                        </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="eventName" FilterControlAltText="Filter eventName column"
                                HeaderText="Event Name" SortExpression="eventName" UniqueName="eventName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EventCode" FilterControlAltText="Filter EventCode column"
                                HeaderText="Event Code" SortExpression="EventCode" UniqueName="EventCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EventInfo" FilterControlAltText="Filter EventInfo column"
                                HeaderText="Event Information" SortExpression="EventInfo" UniqueName="EventInfo">
                                <ItemStyle Width="300px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="actionName" FilterControlAltText="Filter actionName column"
                                HeaderText="Action Status" SortExpression="actionName" UniqueName="actionName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="catId" FilterControlAltText="Filter catId column"
                                HeaderText="catId" SortExpression="catId" UniqueName="catId" DataType="System.Int32"
                                ReadOnly="True" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="categoryName" FilterControlAltText="Filter categoryName column"
                                HeaderText="Category" SortExpression="categoryName" UniqueName="categoryName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventTypeName" FilterControlAltText="Filter eventTypeName column"
                                HeaderText="Event Type" SortExpression="eventTypeName" UniqueName="eventTypeName"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="id" FilterControlAltText="Filter id column" HeaderText="id"
                                SortExpression="id" UniqueName="id" DataType="System.Int32" ReadOnly="True" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DiscoveredAt" FilterControlAltText="Filter DiscoveredAt column"
                                HeaderText="DiscoveredAt" SortExpression="DiscoveredAt" UniqueName="DiscoveredAt"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Source" FilterControlAltText="Filter Source column"
                                HeaderText="Source" SortExpression="Source" UniqueName="Source" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EndTime" DataType="System.DateTime" FilterControlAltText="Filter EndTime column"
                                HeaderText="EndTime" SortExpression="EndTime" UniqueName="EndTime" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="State" DataType="System.Boolean" FilterControlAltText="Filter State column"
                                HeaderText="State" SortExpression="State" UniqueName="State" Visible="false">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="Ongoing" DataType="System.Boolean" FilterControlAltText="Filter Ongoing column"
                                HeaderText="Ongoing" SortExpression="Ongoing" UniqueName="Ongoing" Visible="false">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="Phase" FilterControlAltText="Filter Phase column"
                                HeaderText="Phase" SortExpression="Phase" UniqueName="Phase" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Counter" FilterControlAltText="Filter Counter column"
                                HeaderText="Counter" SortExpression="Counter" UniqueName="Counter" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="containsTaskOrder" FilterControlAltText="Filter containsTaskOrder column"
                                HeaderText="Task Order" SortExpression="containsTaskOrder" UniqueName="containsTaskOrder"
                                Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="AlarmTrigger" DataType="System.Boolean" FilterControlAltText="Filter AlarmTrigger column"
                                HeaderText="AlarmTrigger" SortExpression="AlarmTrigger" UniqueName="AlarmTrigger"
                                Visible="false">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="EventData_Id" FilterControlAltText="Filter EventData_Id column"
                                HeaderText="EventData_Id" SortExpression="EventData_Id" UniqueName="EventData_Id"
                                Visible="false" DataType="System.Int32">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventAMI_Id" DataType="System.Int32" FilterControlAltText="Filter eventAMI_Id column"
                                HeaderText="eventAMI_Id" SortExpression="eventAMI_Id" UniqueName="eventAMI_Id"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ElsterMeter_Id" DataType="System.Int32" FilterControlAltText="Filter ElsterMeter_Id column"
                                HeaderText="ElsterMeter_Id" SortExpression="ElsterMeter_Id" UniqueName="ElsterMeter_Id"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ElsterMeterSerialNumber" FilterControlAltText="Filter ElsterMeterSerialNumber column"
                                HeaderText="Elster Meter Serial Number" SortExpression="ElsterMeterSerialNumber"
                                UniqueName="ElsterMeterSerialNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="userAssignedId" DataType="System.Int32" FilterControlAltText="Filter userAssignedId column"
                                HeaderText="userAssignedId" SortExpression="userAssignedId" UniqueName="userAssignedId"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="userAssignedTimestamp" FilterControlAltText="Filter userAssignedTimestamp column"
                                HeaderText="userAssignedTimestamp" SortExpression="userAssignedTimestamp" UniqueName="userAssignedTimestamp"
                                DataType="System.DateTime" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="userCompletedTimestamp" DataType="System.DateTime"
                                FilterControlAltText="Filter userCompletedTimestamp column" HeaderText="userCompletedTimestamp"
                                SortExpression="userCompletedTimestamp" UniqueName="userCompletedTimestamp" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="flagName" FilterControlAltText="Filter flagName column"
                                HeaderText="Flag Name" SortExpression="flagName" UniqueName="flagName">
                                <ItemStyle CssClass="options" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fullName" FilterControlAltText="Filter fullName column"
                                HeaderText="Employee Name" SortExpression="fullName" UniqueName="fullName" ReadOnly="True"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn AllowSorting="False" HeaderText="GIS" DataNavigateUrlFields="primaryLatLong"
                                DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                                Target="_new" Text="View Map" UniqueName="gisLink">
                                <ItemStyle ForeColor="Blue" Width="70px" />
                            </telerik:GridHyperLinkColumn>
                        </Columns>
                        <NestedViewTemplate>
                            <div class="input">
                                <span class="label"></span>
                                <div class="input">
                                    <asp:Button ID="ManageFilesBtn" runat="server" Text="View/Upload Task Documents"
                                        OnClientClick='<%# String.Format("OpenFileExplorerDialog({0},{1});return false;",Eval("eventcodeid"), Eval("eventamiid"))%> '>
                                    </asp:Button></div>
                            </div>
                            <div class="input">
                                <span class="label"></span>
                                <div class="input">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="TaskLogBtn"
                                        runat="server" Text="View Task Log" OnClientClick='<%# String.Format("OpenTaskLog({0});return false;",Eval("eventamiid"))%> '>
                                    </asp:Button></div>
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
                                        CommandName="Update"></asp:Button>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="hidd_id" Text='<%# Eval("eventamiid") %>' Style="display: none;"></asp:TextBox>
                        </NestedViewTemplate>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <HeaderStyle VerticalAlign="Bottom" />
                    </MasterTableView>
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
            <div id="dataSource">
                <asp:SqlDataSource ID="events" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    SelectCommand="SELECT eventName + ' ' + eventCode AS eventDetail, id FROM events ORDER BY eventDetail ASC">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="categories" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    SelectCommand="SELECT [categoryName], [id] FROM [category] ORDER BY categoryName ASC">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="flags" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    SelectCommand="SELECT [flagName], [id] FROM [flag] ORDER BY flagName ASC"></asp:SqlDataSource>
                <%--<asp:SqlDataSource ID="MasterTableData" runat="server" connectionString="<%$ databaseExpression:client_database %>"
            providerName="System.Data.SqlClient"
            
            SelectCommand="SELECT DISTINCT userAction.actionName, CAST(CASE WHEN containsTaskOrder = 'True' THEN 'Yes' ELSE 'No' END AS varchar) AS containsTaskOrder, category.id AS catId, category.categoryName, primaryLatLong, events.eventName, eventAMI.id, TimeStamp, DiscoveredAt, Source, EndTime, eventAMI.State, Ongoing, Phase, Counter, eventAMI.EventCode, AlarmTrigger, EventInfo, Event_Id, EventData_Id, eventAMI_Id, ElsterMeter_Id, ElsterMeterSerialNumber,
                                      userAssignedId, userAssignedTimestamp, userCompletedTimestamp, events.eventName, flag.flagName, Users.firstName + ' ' + Users.lastName AS fullName, meter.manufacturer, meter.meterIRN, meter.primaryLatLong
                            FROM eventAMI
                            LEFT JOIN events ON eventAMI.EventCode = events.eventCode
                            LEFT JOIN flag ON events.flagId = flag.id
                            LEFT JOIN Users ON Users.userID = eventAMI.userAssignedId
                            LEFT JOIN eventCategory ON eventCategory.eventId = events.id
                            LEFT JOIN category ON eventCategory.categoryId = category.id 
                            LEFT JOIN userAction ON userAction.id = eventAMI.userActionId
                            LEFT JOIN meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber
                            ORDER BY TimeStamp DESC"
                        
            UpdateCommand = "UPDATE eventAMI SET userActionId = @userActionId WHERE Event_Id = @Event_Id">
        </asp:SqlDataSource>--%>
                <asp:SqlDataSource ID="MasterTableData" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT  userAction.actionName,events.id as eventcodeid,eventAMI.id as eventamiid, CAST(CASE WHEN containsTaskOrder = 'True' THEN 'Yes' ELSE 'No' END AS varchar) AS containsTaskOrder, category.id AS catId, category.categoryName, events.eventName, eventAMI.id, TimeStamp, DiscoveredAt, Source, EndTime, eventAMI.State, Ongoing, Phase, Counter, eventAMI.EventCode, AlarmTrigger, EventInfo, Event_Id, EventData_Id, eventAMI_Id, ElsterMeter_Id, ElsterMeterSerialNumber, 
                                       userAssignedId, userAssignedTimestamp, userCompletedTimestamp, events.eventName, flag.flagName, Users.firstName + ' ' + Users.lastName AS fullName, meter.manufacturer, meter.meterIRN, meter.primaryLatLong 
                                       FROM eventAMI 
                                       INNER JOIN events ON eventAMI.EventCode = events.eventCode 
                                       INNER JOIN flag ON events.flagId = flag.id
                                       LEFT JOIN Users ON Users.userID = eventAMI.userAssignedId
                                       INNER JOIN eventCategory ON eventCategory.eventId = events.id
                                       INNER JOIN category ON eventCategory.categoryId = category.id
                                       LEFT JOIN userAction ON userAction.id = eventAMI.userActionId 
                                       LEFT JOIN meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber
                                       ORDER BY TimeStamp DESC" UpdateCommand="UPDATE eventAMI SET userActionId = @userActionId WHERE id = @Event_Id">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="UserActionList" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT [actionName], [id] FROM [userAction] where role='user' ORDER BY actionName ASC">
                </asp:SqlDataSource>
            </div>
            <telerik:RadAjaxLoadingPanel runat="server" ID="loader">
            </telerik:RadAjaxLoadingPanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
