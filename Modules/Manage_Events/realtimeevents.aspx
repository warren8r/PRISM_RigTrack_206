<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="realtimeevents.aspx.cs" Debug="true" Inherits="Modules_Manage_Events_eventDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="ZoneID1">
        <asp:UpdatePanel runat="server" ID="updPnl1" ChildrenAsTriggers="true" UpdateMode="Always">
            <ContentTemplate>
                <telerik:RadTabStrip CausesValidation="False" ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    AutoPostBack="True" Style="padding: 20px 0px 0px 10px;" SelectedIndex="0" CssClass="tabStrip"
                    Skin="Office2010Black">
                    <Tabs>
                        <telerik:RadTab Text="Event Dashboard" ForeColor="White" Selected="True" PageViewID="pvEventDashboard">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage RenderSelectedPageOnly="true" ID="RadMultiPage1" runat="server"
                    SelectedIndex="0" CssClass="multiPage" Width="1080px">
                    <telerik:RadPageView ID="pvEventDashboard" runat="server">
                        <telerik:RadSplitter ID="RadSplitter1" Width="90%" Height="620px" runat="server"
                            Orientation="Vertical">
                            <telerik:RadPane ID="gridPane" runat="server" Scrolling="Y" Width="1080px">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend>Filter</legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="50%">
                                                            <!-- filter -->
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        Start:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="en-US" OnSelectedDateChanged="RadDatePicker1_SelectedDateChanged">
                                                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                            </Calendar>
                                                                            <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="40%" value="6/10/2013">
                                                                            </DateInput>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                    <td>
                                                                        Stop
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadDatePicker ID="RadDatePicker2" runat="server" AutoPostBack="True" Culture="en-US"
                                                                            OnSelectedDateChanged="RadDatePicker2_SelectedDateChanged">
                                                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                            </Calendar>
                                                                            <DateInput AutoPostBack="True" DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy"
                                                                                LabelWidth="40%" value="6/11/2013">
                                                                            </DateInput>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                    <td valign="top" rowspan="2">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:SqlDataSource ID="sqldsGetCats" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                                            SelectCommand="SELECT [id], [categoryName] FROM [category]"></asp:SqlDataSource>
                                                                        Category:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadDropDownList ID="ddlCatNames" runat="server" DataSourceID="sqldsGetCats"
                                                                            DataTextField="categoryName" AllowPostBack="true" DataValueField="categoryName"
                                                                            AppendDataBoundItems="True" CausesValidation="False" SelectedText="- Select All -"
                                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCatNames_SelectedIndexChanged">
                                                                            <Items>
                                                                                <telerik:DropDownListItem runat="server" Selected="True" Text="- Select All -" Value="" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                    </td>
                                                                    <td>
                                                                        Events:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadDropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2"
                                                                            DataTextField="eventName" AllowPostBack="true" DataValueField="eventName" AppendDataBoundItems="True"
                                                                            CausesValidation="False" SelectedText="- Select All -" AutoPostBack="True" OnSelectedIndexChanged="ddlCatNames2_SelectedIndexChanged">
                                                                            <Items>
                                                                                <telerik:DropDownListItem runat="server" DropDownList="DropDownList2" Selected="True"
                                                                                    Text="- Select All -" Value="" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSource2" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                                            runat="server" SelectCommand="SELECT DISTINCT events.eventName
FROM         eventAMI INNER JOIN
                      events ON eventAMI.EventCode = events.eventCode INNER JOIN
                      flag ON events.flagId = flag.id INNER JOIN
                      eventCategory ON eventCategory.eventId = events.id INNER JOIN
                      category ON eventCategory.categoryId = category.id INNER JOIN
                      meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber"></asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="text-align: left; vertical-align: top;">
                                                            <!-- show legend -->
                                                            <asp:SqlDataSource ID="getLegend" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                                DeleteCommand="DELETE FROM [flag] WHERE [id] = @id" InsertCommand="INSERT INTO [flag] ( [flagName], [flagColor]) VALUES ( @flagName, COALESCE( @flagColor, '#fff' ))"
                                                                SelectCommand="SELECT id, flagName, COALESCE (SUBSTRING(flagColor, 2, LEN(flagColor) - 1), 'ffffff') AS flagColor FROM flag"
                                                                UpdateCommand="UPDATE [flag] SET [flagName] = @flagName, [flagColor] = @flagColor WHERE [id] = @id">
                                                            </asp:SqlDataSource>
                                                            <span class="style1"><strong>Legend</strong></span>
                                                            <asp:GridView ID="GridView1" Width="200px" runat="server" AutoGenerateColumns="False"
                                                                DataSourceID="getLegend" ShowHeader="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="flagName" HeaderText="Name" SortExpression="flagName" />
                                                                    <asp:TemplateField SortExpression="flagColor">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("flagColor") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("flagColor") %>'></asp:Label>--%>
                                                                            <asp:Image ID="Image1" AlternateText="" Alt="" runat="server" ImageUrl='<%# Eval("flagColor", "~\\AlertMarker.aspx?color={0}") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <!-- end legend -->
                                                        </td>
                                                        <td style="text-align: center">
                                                            <telerik:RadButton ID="btnFilterGIS" runat="server" SingleClick="false" SingleClickText="Please wait.."
                                                                Text="View" OnClick="btnFilterGIS_Click" />
                                                            &nbsp;
                                                            <telerik:RadButton ID="RadButton1" runat="server" Text="Reset" />
                                                            <br />
                                                            <asp:HyperLink ID="hypJumpToData" runat="server" NavigateUrl="#Data" Style="position: relative;
                                                                left: 20px; top: 10px;" Text="View Data" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%">
                                                            <asp:Label ID="lblSQL" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <fieldset>
                                                    <div>
                                                        <asp:Panel ID="pnl1" runat="server" Style="position: relative;">
                                                            <div id="norecords" visible="false" runat="server" style="line-height: 100px; z-index: 100000;
                                                                text-align: left; width: 200px; background-color: White; border: 1px solid black;
                                                                text-align: center; position: absolute; top: 156px; display: block; margin-left: auto;
                                                                margin-right: auto; margin-top: auto; margin-bottom: auto; vertical-align: middle;
                                                                color: Red; height: 100px; margin: 0px auto; opacity: .8; left: 400px;">
                                                                <b>Data Not Available</b>
                                                            </div>
                                                            <artem:GoogleMap ID="GoogleMap1" Visible="true" EnableOverviewMapControl="false"
                                                                EnableMapTypeControl="false" EnableZoomControl="false" EnableStreetViewControl="false"
                                                                ShowTraffic="false" runat="server" ApiVersion="3" EnableScrollWheelZoom="true"
                                                                IsSensor="true" Zoom="10" DisableDoubleClickZoom="False" DisableKeyboardShortcuts="True"
                                                                EnableReverseGeocoding="True" Height="450" IsStatic="false" Key="AIzaSyC2lU7S-IMlNUTu8nHAL97_rL06vKmzfhc"
                                                                MapType="Roadmap" StaticFormat="Gif" Tilt="45" Width="1000px" StaticScale="15"
                                                                DefaultAddress="" Latitude="33.335767" Longitude="-111.944151">
                                                                <Center Latitude="33.335767" Longitude="-111.944151" />
                                                                <MapTypeControlOptions Position="TopRight" ViewStyle="Default" />
                                                                <OverviewMapControlOptions Opened="False" />
                                                                <PanControlOptions Position="TopLeft" />
                                                                <RotateControlOptions Position="TopLeft" />
                                                                <ScaleControlOptions Position="TopLeft" Style="default" />
                                                            </artem:GoogleMap>
                                                            <artem:GoogleMarkers runat="server" ID="GoogleMarker" TargetControlID="GoogleMap1" />
                                                        </asp:Panel>
                                                    </div>
                                                </fieldset>
                                            </fieldset>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                SelectCommand="SELECT category.id AS catId, category.categoryName, events.eventName, eventAMI.id, eventAMI.TimeStamp, eventAMI.DiscoveredAt, 
                                                               eventAMI.Source, eventAMI.EndTime, eventAMI.State, eventAMI.Ongoing, eventAMI.Phase, eventAMI.Counter, eventAMI.EventCode,
                                                               eventAMI.AlarmTrigger, eventAMI.EventInfo, eventAMI.Event_Id, eventAMI.EventData_Id, eventAMI.eventAMI_Id, eventAMI.ElsterMeter_Id,
                                                               eventAMI.ElsterMeterSerialNumber, eventAMI.userAssignedId, eventAMI.userAssignedTimestamp, eventAMI.userCompletedTimestamp, 
                                                               events.eventName AS Expr1, flag.flagName, eventAMI.TimeStamp AS Expr2, meter.manufacturer, meter.meterIRN, meter.primaryLatLong, 
                                                               flag.flagColor, userAction.actionName, CAST(CASE WHEN containsTaskOrder = 'True' THEN 'Yes' ELSE 'No' END AS varchar) AS containsTaskOrder
                                                               FROM eventAMI 
                                                               INNER JOIN events ON eventAMI.EventCode = events.eventCode 
                                                               INNER JOIN flag ON events.flagId = flag.id
                                                               LEFT JOIN Users ON Users.userID = eventAMI.userAssignedId 
                                                               INNER JOIN eventCategory ON eventCategory.eventId = events.id 
                                                               INNER JOIN category ON eventCategory.categoryId = category.id
                                                               LEFT JOIN userAction ON userAction.id = eventAMI.userActionId  
                                                               LEFT JOIN meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber
                                                               WHERE (eventAMI.TimeStamp BETWEEN @StartDate AND @EndDate) 
                                                               AND (category.categoryName LIKE '%' + @catName + '%') 
                                                               AND (events.eventName LIKE '%' + @eventName + '%') 
                                                               ORDER BY eventAMI.TimeStamp, eventAMI.id DESC" OnSelecting="SqlDataSource1_Selecting"
                                                OnSelected="SqlDataSource1_Selected">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="RadDatePicker1" Name="StartDate" PropertyName="SelectedDate" />
                                                    <asp:ControlParameter ControlID="RadDatePicker2" Name="EndDate" PropertyName="SelectedDate" />
                                                    <asp:ControlParameter ControlID="ddlCatNames" DefaultValue="%" Name="catName" PropertyName="SelectedValue" />
                                                    <asp:ControlParameter ControlID="DropDownList2" DefaultValue="%" Name="eventName"
                                                        PropertyName="SelectedValue" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                        <!-- WHERE (eventAMI.TimeStamp BETWEEN @StartDate AND @EndDate) -->
                                    </tr>
                                </table>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <a name="Data"></a>
                <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True"
                    CellSpacing="0" GridLines="None" Width="1080px" AllowSorting="True" OnGroupsChanging="RadGrid1_GroupsChanging"
                    OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand"
                    ShowGroupPanel="True" AutoGenerateColumns="False">
                    <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView DataSourceID="SqlDataSource1">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="TimeStamp" DataType="System.DateTime" FilterControlAltText="Filter TimeStamp column"
                                HeaderText="Time Stamp" SortExpression="TimeStamp" UniqueName="TimeStamp">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Event_Id" DataType="System.Int32" FilterControlAltText="Filter Event_Id column"
                                HeaderText="Event ID" SortExpression="Event_Id" UniqueName="Event_Id">
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventName" FilterControlAltText="Filter eventName column"
                                HeaderText="Event Name" SortExpression="eventName" UniqueName="eventName">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EventCode" FilterControlAltText="Filter EventCode column"
                                HeaderText="Event Code" SortExpression="EventCode" UniqueName="EventCode">
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EventInfo" FilterControlAltText="Filter EventInfo column"
                                HeaderText="Event Info" SortExpression="EventInfo" UniqueName="EventInfo">
                                <HeaderStyle Width="300px" />
                                <ItemStyle Width="300px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="actionName" FilterControlAltText="Filter EventInfo column"
                                HeaderText="Action Name" SortExpression="actionName" UniqueName="actionName">
                            </telerik:GridBoundColumn> 
                            <telerik:GridBoundColumn DataField="categoryName" FilterControlAltText="Filter categoryName column"
                                HeaderText="Category Name" SortExpression="categoryName" UniqueName="categoryName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="containsTaskOrder" FilterControlAltText="Filter Contains Taxk Order column"
                                HeaderText="Contains Task Order" SortExpression="containsTaskOrder" UniqueName="containsTaskOrder">
                            </telerik:GridBoundColumn> 
                            <telerik:GridBoundColumn DataField="ElsterMeterSerialNumber" FilterControlAltText="Filter ElsterMeterSerialNumber column"
                                HeaderText="Elster Meter Serial Number" SortExpression="ElsterMeterSerialNumber"
                                UniqueName="ElsterMeterSerialNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="flagName" FilterControlAltText="Filter flagName column"
                                HeaderText="Flag Name" SortExpression="flagName" UniqueName="flagName">
                            </telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn AllowSorting="False" DataNavigateUrlFields="primaryLatLong"
                                DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                                HeaderText="GIS" Target="_new" Text="View Map" UniqueName="gisLink">
                                <ItemStyle ForeColor="Blue" />
                            </telerik:GridHyperLinkColumn>

<%--                            <telerik:GridBoundColumn DataField="catId" DataType="System.Int32" FilterControlAltText="Filter catId column"
                                HeaderText="catId" ReadOnly="True" SortExpression="catId" UniqueName="catId">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="id" DataType="System.Int32" FilterControlAltText="Filter id column"
                                HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DiscoveredAt" FilterControlAltText="Filter DiscoveredAt column"
                                HeaderText="DiscoveredAt" SortExpression="DiscoveredAt" UniqueName="DiscoveredAt">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Source" FilterControlAltText="Filter Source column"
                                HeaderText="Source" SortExpression="Source" UniqueName="Source">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EndTime" DataType="System.DateTime" FilterControlAltText="Filter EndTime column"
                                HeaderText="EndTime" SortExpression="EndTime" UniqueName="EndTime">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="State" DataType="System.Boolean" FilterControlAltText="Filter State column"
                                HeaderText="State" SortExpression="State" UniqueName="State">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="Ongoing" DataType="System.Boolean" FilterControlAltText="Filter Ongoing column"
                                HeaderText="Ongoing" SortExpression="Ongoing" UniqueName="Ongoing">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="Phase" FilterControlAltText="Filter Phase column"
                                HeaderText="Phase" SortExpression="Phase" UniqueName="Phase">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Counter" FilterControlAltText="Filter Counter column"
                                HeaderText="Counter" SortExpression="Counter" UniqueName="Counter">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="AlarmTrigger" DataType="System.Boolean" FilterControlAltText="Filter AlarmTrigger column"
                                HeaderText="AlarmTrigger" SortExpression="AlarmTrigger" UniqueName="AlarmTrigger">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="EventData_Id" DataType="System.Int32" FilterControlAltText="Filter EventData_Id column"
                                HeaderText="EventData_Id" SortExpression="EventData_Id" UniqueName="EventData_Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventAMI_Id" DataType="System.Int32" FilterControlAltText="Filter eventAMI_Id column"
                                HeaderText="eventAMI_Id" SortExpression="eventAMI_Id" UniqueName="eventAMI_Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ElsterMeter_Id" DataType="System.Int32" FilterControlAltText="Filter ElsterMeter_Id column"
                                HeaderText="ElsterMeter_Id" SortExpression="ElsterMeter_Id" UniqueName="ElsterMeter_Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="userAssignedId" DataType="System.Int32" FilterControlAltText="Filter userAssignedId column"
                                HeaderText="userAssignedId" SortExpression="userAssignedId" UniqueName="userAssignedId">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="userAssignedTimestamp" DataType="System.DateTime"
                                FilterControlAltText="Filter userAssignedTimestamp column" HeaderText="userAssignedTimestamp"
                                SortExpression="userAssignedTimestamp" UniqueName="userAssignedTimestamp">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="userCompletedTimestamp" DataType="System.DateTime"
                                FilterControlAltText="Filter userCompletedTimestamp column" HeaderText="userCompletedTimestamp"
                                SortExpression="userCompletedTimestamp" UniqueName="userCompletedTimestamp">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Expr1" FilterControlAltText="Filter Expr1 column"
                                HeaderText="Expr1" SortExpression="Expr1" UniqueName="Expr1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Expr2" DataType="System.DateTime" FilterControlAltText="Filter Expr2 column"
                                HeaderText="Expr2" SortExpression="Expr2" UniqueName="Expr2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="manufacturer" FilterControlAltText="Filter manufacturer column"
                                HeaderText="manufacturer" SortExpression="manufacturer" UniqueName="manufacturer">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="meterIRN" DataType="System.Int64" FilterControlAltText="Filter meterIRN column"
                                HeaderText="meterIRN" SortExpression="meterIRN" UniqueName="meterIRN">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="primaryLatLong" FilterControlAltText="Filter primaryLatLong column"
                                HeaderText="primaryLatLong" SortExpression="primaryLatLong" UniqueName="primaryLatLong">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="flagColor" FilterControlAltText="Filter flagColor column"
                                HeaderText="flagColor" SortExpression="flagColor" UniqueName="flagColor">
                            </telerik:GridBoundColumn>
--%>                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <HeaderStyle VerticalAlign="Bottom" />
                    <ItemStyle VerticalAlign="Top" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid><asp:Label ID="lblCount" Text="0" runat="server" />
                <asp:GridView runat="server" ID="gridGRO" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                    OnRowDataBound="gridGRO_RowDataBound" Visible="False" OnDataBound="gridGRO_DataBound">
                    <Columns>
                        <asp:BoundField DataField="catId" HeaderText="Category ID" SortExpression="catId"
                            InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="categoryName" HeaderText="Category Name" SortExpression="categoryName" />
                        <asp:BoundField DataField="eventName" HeaderText="Event Name" SortExpression="eventName" />
                        <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" InsertVisible="False"
                            ReadOnly="True" />
                        <asp:BoundField DataField="TimeStamp" HeaderText="Time" SortExpression="TimeStamp" />
                        <asp:BoundField DataField="DiscoveredAt" HeaderText="Discovered" SortExpression="DiscoveredAt" />
                        <asp:BoundField DataField="Source" HeaderText="Source" SortExpression="Source" />
                        <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="EndTime" />
                        <asp:CheckBoxField DataField="State" HeaderText="State" SortExpression="State" />
                        <asp:CheckBoxField DataField="Ongoing" HeaderText="Ongoing" SortExpression="Ongoing" />
                        <asp:BoundField DataField="Phase" HeaderText="Phase" SortExpression="Phase" />
                        <asp:BoundField DataField="Counter" HeaderText="Counter" SortExpression="Counter" />
                        <asp:BoundField DataField="EventCode" HeaderText="Event Code" SortExpression="EventCode" />
                        <asp:CheckBoxField DataField="AlarmTrigger" HeaderText="Alarm" SortExpression="AlarmTrigger" />
                        <asp:BoundField DataField="EventInfo" HeaderText="Info" SortExpression="EventInfo" />
                        <asp:BoundField DataField="Event_Id" HeaderText="Event ID" SortExpression="Event_Id" />
                        <asp:BoundField DataField="EventData_Id" HeaderText="Data ID" SortExpression="EventData_Id" />
                        <asp:BoundField DataField="eventAMI_Id" HeaderText="AMI ID" SortExpression="eventAMI_Id" />
                        <asp:BoundField DataField="ElsterMeter_Id" HeaderText="Meter ID" SortExpression="ElsterMeter_Id" />
                        <asp:BoundField DataField="ElsterMeterSerialNumber" HeaderText="Meter Serial Number"
                            SortExpression="ElsterMeterSerialNumber" />
                        <asp:BoundField DataField="userAssignedId" HeaderText="User Assigned ID" SortExpression="userAssignedId" />
                        <asp:BoundField DataField="userAssignedTimestamp" HeaderText="User Assigned Time"
                            SortExpression="userAssignedTimestamp" />
                        <asp:BoundField DataField="userCompletedTimestamp" HeaderText="User Completed Time"
                            SortExpression="userCompletedTimestamp" />
                        <asp:BoundField DataField="Expr1" HeaderText="Misc 1" SortExpression="Expr1" />
                        <asp:BoundField DataField="flagName" HeaderText="Flag Name" SortExpression="flagName" />
                        <asp:BoundField DataField="Expr2" HeaderText="Misc 2" SortExpression="Expr2" />
                        <asp:BoundField DataField="manufacturer" HeaderText="Manufacturer" SortExpression="manufacturer" />
                        <asp:BoundField DataField="meterIRN" HeaderText="Internal Ref Number" SortExpression="meterIRN" />
                        <asp:BoundField DataField="primaryLatLong" HeaderText="GIS" SortExpression="primaryLatLong" />
                        <asp:BoundField DataField="flagColor" HeaderText="flagColor" SortExpression="flagColor" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="customCss">
    <style type="text/css">
        .style1
        {
            text-decoration: underline;
        }
    </style>
</asp:Content>
