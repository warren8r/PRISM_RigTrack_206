<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="LaunchVEE.aspx.cs" Inherits="Modules_DA_LaunchVEE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" 
             contentplaceholderid="ContentPlaceHolder1">
    <br />
    <h3>&nbsp;&nbsp;View Pre-VEE Data</h3>

    <div style="">
        <telerik:RadPanelBar ID="RadPanelBar1" runat="server" 
                             style="margin-left:auto;margin-right:auto;" Width="90%">
            <Items>
                <telerik:RadPanelItem runat="server" Expanded="True" 
                                      Selected="True" Text="View Pre-VEE Data">
                    <ContentTemplate>
                        <asp:SqlDataSource ID="dsShowPreVEERoutineMaster" runat="server" 
                                           ConnectionString="<%$ databaseExpression:client_database %>" 
                    
                                           SelectCommand="SELECT [veeRoutinesID], [veeRoutineName], [veeRoutineDescription] FROM [veeRoutines]">
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="dsShowPreVEERoutineDetail" runat="server" 
                                           ConnectionString="<%$ databaseExpression:client_database %>" 
                    
                    
                                           SelectCommand="SELECT veeRoutinesID, veeRoutineName, veeRoutineDescription FROM veeRoutines WHERE (veeRoutinesID = @veeRoutinesID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="RadDropDownList1" DbType="Int32" 
                                                      DefaultValue="" Name="veeRoutinesID" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <fieldset style="margin-left:auto;margin-right:auto;width:90%; vertical-align:middle;">
                            <asp:UpdatePanel ID="RadAjaxPanel1" ChildrenAsTriggers="true" runat="server">
                                <ContentTemplate>
                                    <table border="0" width="100%">
                                        <tr>
                                            <td width="140px">
                                                <asp:Label ID="lblSelectPreveeRoutine" Text="Select Pre-VEE Routine:" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDropDownList ID="RadDropDownList1" runat="server"  EnableEmbeddedBaseStylesheet="true"
                                                                         DataSourceID="dsShowPreVEERoutineMaster" AutoPostBack="true" DataTextField="veeRoutineName" AppendDataBoundItems="true"
                                                                         DataValueField="veeRoutinesID"   onselectedindexchanged="RadDropDownList1_SelectedIndexChanged">
                                                    <Items>
                                                        <telerik:DropDownListItem runat="server" Selected="true" DropDownList="RadDropDownList1" 
                                                                                  Text="- Select Routine -" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" />
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker runat="server" ID="rdStartDt" Width="130px">
                                                    <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Stop Date:" />
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker runat="server" ID="rdStopDt" Width="130px">
                                                    <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp; </td>
                                            <td>
                                                &nbsp;
                                                <telerik:RadButton ID="RadButton1" runat="server" Text="View Pre-VEE Data">
                                                </telerik:RadButton>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="140px">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Repeater ID="rptRoutineDetail" runat="server" DataSourceID="dsShowPreVEERoutineDetail" >
                                                    <ItemTemplate>
                                                        <table style="border:1px; border-style:solid; border-color:Black;" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="veeRoutineDescriptionLabel" runat="server" 
                                                                               Text='<%# Eval("veeRoutineDescription", "{0}") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td> <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server"  Expanded="true" PreventCollapse="false" 
                                      Text="Pre-VEE Data Preview">
                    <ContentTemplate>
                        <!-- elster data -->
                        <asp:SqlDataSource ID="sqlGetPreVEE" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:elster_database %>" SelectCommand="select m.metername, m.metertype, m.serialnumber,ispec.direction, m.timezone, m.timezoneoffset, ispec.[TimestampStart], ispec.[TimestampEnd], ispec.direction, ispec.interval, r.timestamp, r.value from meterreadings mr, meter m, intervaldata id, intervalspec ispec, reading r 
where mr.meterreadings_id = m.meterreadings_id 
and mr.meterreadings_id = id.meterreadings_id 
and id.intervaldata_id = r.intervaldata_id 
and id.intervaldata_id = ispec.intervaldata_id
and CONVERT(VARCHAR,ispec.direction) = 'Delivered' 
and CONVERT(VARCHAR,m.metertype) = 'REX' 
"></asp:SqlDataSource>
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" 
                            AllowPaging="True" AllowSorting="True" CellSpacing="0" 
                            DataSourceID="sqlGetPreVEE" GridLines="None">
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="sqlGetPreVEE">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                    Visible="True">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                    Visible="True">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="metername" 
                                        FilterControlAltText="Filter metername column" HeaderText="metername" 
                                        SortExpression="metername" UniqueName="metername">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="metertype" 
                                        FilterControlAltText="Filter metertype column" HeaderText="metertype" 
                                        SortExpression="metertype" UniqueName="metertype">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="serialnumber" 
                                        FilterControlAltText="Filter serialnumber column" HeaderText="serialnumber" 
                                        SortExpression="serialnumber" UniqueName="serialnumber">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="direction" 
                                        FilterControlAltText="Filter direction column" HeaderText="direction" 
                                        SortExpression="direction" UniqueName="direction">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="timezone" 
                                        FilterControlAltText="Filter timezone column" HeaderText="timezone" 
                                        SortExpression="timezone" UniqueName="timezone">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="timezoneoffset" DataType="System.Int64" 
                                        FilterControlAltText="Filter timezoneoffset column" HeaderText="timezoneoffset" 
                                        SortExpression="timezoneoffset" UniqueName="timezoneoffset">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TimestampStart" 
                                        FilterControlAltText="Filter TimestampStart column" HeaderText="TimestampStart" 
                                        SortExpression="TimestampStart" UniqueName="TimestampStart">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TimestampEnd" 
                                        FilterControlAltText="Filter TimestampEnd column" HeaderText="TimestampEnd" 
                                        SortExpression="TimestampEnd" UniqueName="TimestampEnd">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="direction1" 
                                        FilterControlAltText="Filter direction1 column" HeaderText="direction1" 
                                        SortExpression="direction1" UniqueName="direction1">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="interval" DataType="System.Byte" 
                                        FilterControlAltText="Filter interval column" HeaderText="interval" 
                                        SortExpression="interval" UniqueName="interval">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="timestamp" 
                                        FilterControlAltText="Filter timestamp column" HeaderText="timestamp" 
                                        SortExpression="timestamp" UniqueName="timestamp">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="value" DataType="System.Single" 
                                        FilterControlAltText="Filter value column" HeaderText="value" 
                                        SortExpression="value" UniqueName="value">
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
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" PreventCollapse="false" Expanded="true" Text="VEE Data">
                    <ContentTemplate>
                                            <asp:SqlDataSource ID="sqlGetPreVEE2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:elster_database %>" SelectCommand="select m.metername, m.metertype, m.serialnumber,ispec.direction, m.timezone, m.timezoneoffset, ispec.[TimestampStart], ispec.[TimestampEnd], ispec.direction, ispec.interval, r.timestamp, r.value from meterreadings mr, meter m, intervaldata id, intervalspec ispec, reading r 
where mr.meterreadings_id = m.meterreadings_id 
and mr.meterreadings_id = id.meterreadings_id 
and id.intervaldata_id = r.intervaldata_id 
and id.intervaldata_id = ispec.intervaldata_id
and CONVERT(VARCHAR,ispec.direction) = 'Delivered' 
and CONVERT(VARCHAR,m.metertype) = 'REX' 
"></asp:SqlDataSource>
                                                
                                            <telerik:RadGrid ID="RadGrid2" runat="server" AllowFilteringByColumn="True" 
                                                AllowPaging="True" AllowSorting="True" CellSpacing="0" 
                                                DataSourceID="sqlGetPreVEE2" GridLines="None">
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView DataSourceID="sqlGetPreVEE2">
                                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                                        Visible="True">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                                        Visible="True">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
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
                                            <br />
                                                
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
    </div>

</asp:Content>

