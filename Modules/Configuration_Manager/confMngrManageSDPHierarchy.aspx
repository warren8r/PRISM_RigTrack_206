<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="confMngrManageSDPHierarchy.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageSDPHierarchy" %>

<%@ Register TagPrefix="MDM" TagName="SDP" Src="~/controls/configMangrSDP/mngSDP.ascx" %>
<%@ Register TagPrefix="MDM" TagName="Accounts" Src="~/controls/configMangrSDP/mngAccounts.ascx" %>
<%@ Register TagPrefix="MDM" TagName="Collectors" Src="~/controls/configMangrSDP/mngCollectors.ascx" %>
<%@ Register TagPrefix="MDM" TagName="Meters" Src="~/controls/confgMangrMeter/mngMeter.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="20"
        InitialDelayTime="1500">
    </telerik:RadAjaxLoadingPanel>--%>
    <telerik:RadAjaxPanel runat="server" ID="updPnl1">
        <div class="star">(<span class="star">*</span>) indicates mandatory fields</div>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function OnClientShow(sender, args) {
                    var btn = sender.getManualCloseButton();
                    btn.style.left = "0px";
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadNotification ID="n1" runat="server" Text="Initial text" Position="BottomRight"
            AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
            EnableRoundedCorners="true">
        </telerik:RadNotification>
        <telerik:RadMultiPage ID="radmpAll" runat="server" RenderSelectedPageOnly="true"
            SelectedIndex="0" CssClass="multiPage">
            <telerik:RadPageView ID="pvSDPHierarchy" runat="server">
                <table cellpadding="5px">
                    <tr>
                        <td valign="top">
                            <span style="font-weight: bold; font-size: 10pt">Data Aggregators</span>
                            <fieldset style="border: 0;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSelectCollector" runat="server" Text="Select Collector: " Width="135px"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadDropDownList ID="ddlCollectorsList" runat="server" DataSourceID="sqlGetCollectors"
                                                DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" AutoPostBack="true"
                                                DropDownHeight="200px" ToolTip="Select a Collector" OnSelectedIndexChanged="ddlCollectorsList_SelectedIndexChanged">
                                            </telerik:RadDropDownList>
                                            <asp:SqlDataSource ID="sqlGetCollectors" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                SelectCommand="SELECT [id], [bitActive], [Number], [Name] FROM [Collectors] WHERE (IsNull([bitActive], 'false') = 'true') ORDER BY [Name]">
                                            </asp:SqlDataSource>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkNewCollector" runat="server" CausesValidation="false" Text="Create New"
                                                ToolTip="Create a new Collector" OnClick="lnkNewCollector_Click" Width="70px">
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDataAggregatorInfo" runat="server" Width="18px" ImageUrl="~/images/info.png" />
                                            <telerik:RadToolTip ID="tip" runat="server" Position="MiddleRight" RelativeTo="Element"
                                                TargetControlID="lnkDataAggregatorInfo" Width="310px" HideEvent="ManualClose"
                                                OnClientShow="OnClientShow">
                                                To create an aggregator group, select the aggregators (<%= CollectorLabel %>,
                                                <%= SDPLabel %>
                                                and
                                                <%= AccountLabel %>) and/or Meter from the dropdown lists and click the "Save" button.
                                                Rules for creating groups:
                                                <ol style="margin-left: 10px; padding-left: 10px;">
                                                    <li>A meter may be grouped with only one aggregator. </li>
                                                    <li>An aggregator may be grouped with only one higher level aggregator. </li>
                                                </ol>
                                            </telerik:RadToolTip>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSelectSDP" runat="server" Text="Select SDP: " Width="135px"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadDropDownList ID="ddlSDPList" runat="server" DataSourceID="sqlGetSDP"
                                                DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" AutoPostBack="True"
                                                DropDownHeight="200px" ToolTip="Select an SDP" OnSelectedIndexChanged="ddlSDPList_SelectedIndexChanged">
                                            </telerik:RadDropDownList>
                                            <asp:SqlDataSource ID="sqlGetSDP" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                SelectCommand="SELECT sdp.[ID], sdp.[bitActive], sdp.[Number], sdp.[Name]
                                                       FROM [SDP] sdp WHERE (IsNull(sdp.[bitActive], false))
                                                       ORDER BY sdp.[Name]"></asp:SqlDataSource>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkNewSDP" runat="server" CausesValidation="false" Text="Create New"
                                                ToolTip="Create an SDP" OnClick="lnkNewSDP_Click" Width="70px">
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSelectAccount" runat="server" Text="Select Account: " Width="135px">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cbxAccountsList" runat="server" DataSourceID="sqlGetAccounts"
                                                AutoPostBack="true" DataTextField="Number" DataValueField="id" AppendDataBoundItems="true"
                                                Height="250px" DropDownWidth="400px" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                                CssClass="blacktext" ToolTip="Select an Account" OnSelectedIndexChanged="cbxAccountsList_SelectedIndexChanged">
                                                <HeaderTemplate>
                                                    <table style="text-align: left">
                                                        <tr>
                                                            <td style="width: 100px;">
                                                                Number
                                                            </td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                Name
                                                            </td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                First
                                                            </td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                Last
                                                            </td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table>
                                                        <thead>
                                                        </thead>
                                                        <tr>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblAccountNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Number") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblAccountName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblPrimaryFirst" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PrimaryFirst") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblPrimaryLast" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PrimaryLast") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                            <asp:SqlDataSource ID="sqlGetAccounts" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                SelectCommand="SELECT [id] AS [ID], [BitActive] AS [BitActive], [Number] AS [Number], [Name] AS [Name]
                                               , [PrimaryFirst] AS [PrimaryFirst], [PrimaryLast] AS [PrimaryLast]
                                               FROM [Accounts] WHERE (IsNull([BitActive], 'false') = 'true')
                                               ORDER BY [Number]"></asp:SqlDataSource>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkNewAccount" runat="server" CausesValidation="false" Text="Create New"
                                                ToolTip="Create an Account" OnClick="lnkNewAccount_Click" Width="70px">
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="vertical-align: top;">
                            <span style="font-weight: bold; font-size: 10pt">Selected Grouping</span>
                            <fieldset style="border: 0px;">
                                <telerik:RadGrid ID="radgrdSelectedGrouping" runat="server" CellSpacing="0" DataSourceID="sqlSelectedGrouping"
                                    GridLines="None" AutoGenerateColumns="False">
                                    <MasterTableView DataKeyNames="ID" DataSourceID="sqlSelectedGrouping" Width="820px"
                                        ToolTip="Grouping for criteria">
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filter ID column" ReadOnly="True"
                                                SortExpression="ID" UniqueName="ID" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CollectorsName" FilterControlAltText="Filter Collector column"
                                                ReadOnly="true" SortExpression="CollectorsName" UniqueName="CollectorsName">
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SDPName" FilterControlAltText="Filter SDP column"
                                                ReadOnly="true" SortExpression="SDPName" UniqueName="SDPName">
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AccountsNumber" FilterControlAltText="Filter Acct Number column"
                                                ReadOnly="true" HeaderText="Number" SortExpression="AccountsNumber" UniqueName="AccountsNumber">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AccountsName" FilterControlAltText="Filter Acct Name column"
                                                HeaderText="Name" SortExpression="AccountsName" UniqueName="AccountsName">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AccountsPrimaryFirst" FilterControlAltText="Filter Primary First column"
                                                HeaderText="Primary First" SortExpression="AccountsPrimaryFirst" UniqueName="AccountsPrimaryFirst">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AccountsPrimaryLast" FilterControlAltText="Filter Primary Last column"
                                                HeaderText="Primary Last" SortExpression="AccountsPrimaryLast" UniqueName="AccountsPrimaryLast">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="meterName" FilterControlAltText="Filter Meter column"
                                                ReadOnly="true" HeaderText="Meter" SortExpression="meterName" UniqueName="meterName">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle PageSizeControlType="RadComboBox" />
                                    </MasterTableView><PagerStyle PageSizeControlType="RadComboBox" />
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                </telerik:RadGrid>
                                <asp:SqlDataSource ID="sqlSelectedGrouping" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                    SelectCommand="SELECT grp.[ID], col.[Number] AS [CollectorsNumber], col.[Name] AS [CollectorsName], sdp.[Number] AS [SDPNumber], sdp.[Name] AS [SDPName], 
                                               act.[Number] AS [AccountsNumber], mtr.[meterName] AS [MeterName], act.[Name] AS [AccountsName], act.[primaryFirst] AS [AccountsPrimaryFirst],
                                               act.[primaryLast] AS [AccountsPrimaryLast] 
                                               FROM grouping AS grp LEFT OUTER JOIN Collectors AS col ON col.id = grp.collectorsID LEFT OUTER JOIN SDP AS sdp ON sdp.ID = grp.sDPID
                                               LEFT OUTER JOIN Accounts AS act ON act.id = grp.accountsID LEFT OUTER JOIN meter AS mtr ON mtr.id = grp.meterID 
                                               WHERE (ISNULL(grp.bitActive, 'false') = 'true') AND (grp.collectorsID = @CollectorsID) AND (grp.sDPID = @SDPID) 
                                               AND (grp.accountsID = @AccountsID) AND (grp.meterID = @MeterID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlCollectorsList" PropertyName="SelectedValue"
                                            Name="CollectorsID" Type="Int32" DefaultValue="0" />
                                        <asp:ControlParameter ControlID="ddlSDPList" PropertyName="SelectedValue" Name="SDPID"
                                            Type="Int32" DefaultValue="0" />
                                        <asp:ControlParameter ControlID="cbxAccountsList" PropertyName="SelectedValue" Name="AccountsID"
                                            Type="Int32" DefaultValue="0" />
                                        <asp:ControlParameter ControlID="cbxMeterList" PropertyName="SelectedValue" Name="MeterID"
                                            Type="Int32" DefaultValue="0" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSelectMeter" runat="server" Text="Select Meter: " Width="115px"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxMeterList" runat="server" DataSourceID="sqlGetMeters"
                                            AutoPostBack="true" DataTextField="serialNumber" DataValueField="id" AppendDataBoundItems="true"
                                            Height="250px" DropDownWidth="500px" ToolTip="Select a Meter" MarkFirstMatch="true"
                                            CssClass="blacktext" HighlightTemplatedItems="true" OnSelectedIndexChanged="cbxMeterList_SelectedIndexChanged"
                                            CausesValidation="False">
                                            <HeaderTemplate>
                                                <table style="text-align: left">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Serial #
                                                        </td>
                                                        <td style="width: 100px;">
                                                            IRN
                                                        </td>
                                                        <td style="width: 100px;">
                                                            Name
                                                        </td>
                                                        <td style="width: 100px;">
                                                            Type
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <thead>
                                                    </thead>
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            <asp:Label ID="lblSErialNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "serialNumber") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:Label ID="lblMeterIRN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "meterIRN") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:Label ID="lblMeterName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "meterName") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:Label ID="lblMeterType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "meterType") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="sqlGetMeters" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                            SelectCommand="SELECT [id], [isActive], [meterID], [meterName], [meterIRN], [serialNumber], [meterType] FROM [meter] 
                                                           WHERE (ISNULL([isActive], 'false') = 'true') 
                                                           AND (NOT EXISTS (SELECT [ID] FROM [grouping] WHERE (grouping.[meterID] = meter.[id]))) ORDER BY [serialNumber]">
                                        </asp:SqlDataSource>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkNewMeter" runat="server" CausesValidation="false" Text="Create New"
                                            ToolTip="Create a Meter" OnClick="lnkNewMeter_Click" Width="70px">
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnResetSelectDropdowns" runat="server" Text="Reset" ToolTip="Reset Data Aggregator dropdowns"
                                            OnClick="btnResetSelecteDropdowns_Click">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnSaveGrouping" runat="server" Text="Save" ToolTip="Create a new Grouping"
                                OnClick="btnSaveGrouping_Click">
                            </telerik:RadButton>
                            <asp:SqlDataSource ID="sqlCreateGrouping" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                InsertCommand="INSERT INTO grouping(bitActive, collectorsID, sDPID, accountsID, meterID, createDate)
                                                   VALUES ('true', @CollectorsID, @SDPID, @AccountsID, @MeterID, GETDATE()); 
                                                   SELECT @GroupingID = SCOPE_IDENTITY()">
                                <InsertParameters>
                                    <asp:ControlParameter ControlID="ddlCollectorsList" PropertyName="SelectedValue"
                                        Name="CollectorsID" Type="Int32" DefaultValue="0" />
                                    <asp:ControlParameter ControlID="ddlSDPList" PropertyName="SelectedValue" Name="SDPID"
                                        Type="Int32" DefaultValue="0" />
                                    <asp:ControlParameter ControlID="cbxAccountsList" PropertyName="SelectedValue" Name="AccountsID"
                                        Type="Int32" DefaultValue="0" />
                                    <asp:ControlParameter ControlID="cbxMeterList" PropertyName="SelectedValue" Name="MeterID"
                                        Type="Int32" DefaultValue="0" />
                                    <asp:Parameter Name="GroupingID" Type="Int32" Direction="Output" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblMessage" runat="server" Style="color: Red; font-size: 10pt; font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <span style="font-weight: bold; font-size: 10pt;">All Groupings</span>
                <table style="margin: 10px 0px 10px 0px;">
                    <tr>
                        <td>
                            <span style="font-weight: bold;">Filter&nbsp;By</span>&nbsp;-&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCollectorCriteria" runat="server"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtCollectorCriteria" runat="server" ToolTip="Filter criteria for Collector">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblSDPCriteria" runat="server"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtSDPCriteria" runat="server" ToolTip="Filter criteria for SDP">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblAccountNumberCriteria" runat="server"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtAccountNumberCriteria" runat="server" ToolTip="Filter for Acct Number">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblAccountNameCriteria" runat="server"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtAccountNameCriteria" runat="server" ToolTip="Filter criteria for Acct">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Prim First:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtAccountPrimaryFirstCriteria" runat="server" ToolTip="Filter criteria for Primary First">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            Prim Last:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtAccountPrimaryLastCriteria" runat="server" ToolTip="Filter criteria for Primary Last">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            Meter:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtMeterCriteria" runat="server" ToolTip="Filter criteria for Meter">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right;">
                            <telerik:RadButton ID="btnClearFilter" runat="server" Text="Clear" ToolTip="Clear all filter criteria"
                                OnClick="btnClearFilter_Click">
                            </telerik:RadButton>
                            &nbsp;
                            <telerik:RadButton ID="btnViewFilter" runat="server" Text="View" ToolTip="Apply filter criteria"
                                OnClick="btnViewFilter_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="radgrdAllGrouping" runat="server" CellSpacing="0" GridLines="None"
                                AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" OnItemDataBound="radgrdAllGrouping_ItemDataBound"
                                OnPageIndexChanged="radgrdAllGrouping_PageIndexChanged" OnNeedDataSource="radgrdAllGrouping_NeedDataSource">
                                <ExportSettings FileName="AllGroupingExport">
                                </ExportSettings>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <MasterTableView DataKeyNames="ID" Width="1000px" ToolTip="Existing Groupings">
                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                                            ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn FilterControlAltText="Filter Active column" HeaderText="Status"
                                            SortExpression="bitActive" UniqueName="bitActive" DataField="bitActive">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="True" Checked='<%# Eval("bitActive") %>'
                                                    OnClick="if(!confirm('Click &quot;OK&quot; to change the status of this item')) return false;"
                                                    OnCheckedChanged="radgrdAllGrouping_CheckChanged" />
                                                <asp:Label ID="lblActive" runat="server" style='<%# Convert.ToBoolean(Eval("bitActive")) == true ?" color: green": "color: red" %>'
                                                    Text='<%# Convert.ToBoolean(Eval("bitActive")) == true ? "Active" : "In-Active" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CollectorsName" FilterControlAltText="Filter Collector column"
                                            HeaderText="Collector" SortExpression="CollectorsName" UniqueName="CollectorsName">
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SDPName" FilterControlAltText="Filter SDP column"
                                            HeaderText="SDP" SortExpression="SDPName" UniqueName="SDPName">
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountsNumber" FilterControlAltText="Filter Account column"
                                            HeaderText="Acct Number" SortExpression="AccountsNumber" UniqueName="AccountsNumber">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountsName" FilterControlAltText="Filter Acct Name column"
                                            HeaderText="Acct Name" SortExpression="AccountsName" UniqueName="AccountsName">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AccountsPrimaryFirst" FilterControlAltText="Filter Primary First column"
                                            HeaderText="Primary First" SortExpression="AccountsPrimaryFirst" UniqueName="AccountsPrimaryFirst">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FilterControlAltText="Filter Primary Last column" UniqueName="AccountsPrimaryLast"
                                            DataField="AccountsPrimaryLast" HeaderText="Primary Last" SortExpression="AccountsPrimaryLast">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="meterName" FilterControlAltText="Filter Meter column"
                                            HeaderText="Meter" SortExpression="meterName" UniqueName="meterName">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                        </EditColumn>
                                    </EditFormSettings>
                                    <PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True" />
                                </MasterTableView><PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True" />
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </telerik:RadGrid>
                            <asp:SqlDataSource ID="sqlAllGrouping" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                UpdateCommand="UPDATE grouping SET bitActive = @bitActive WHERE (ID = @ID)">
                                <UpdateParameters>
                                    <asp:Parameter Name="bitActive" />
                                    <asp:Parameter Name="ID" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView ID="pvCollectors" runat="server">
                <br />
                <telerik:RadButton ID="btnCloseNewCollector" runat="server" Text="Return to Grouping"
                    OnClick="btnCloseNewCollector_Click" CausesValidation="False">
                </telerik:RadButton>
                <br />
                &nbsp;&nbsp;<MDM:Collectors runat="Server" ID="collectors1" />
            </telerik:RadPageView>
            <telerik:RadPageView ID="pvSDP" runat="server">
                <br />
                <telerik:RadButton ID="btnCloseNewSDP" runat="server" Text="Return to Grouping" OnClick="btnCloseNewSDP_Click"
                    CausesValidation="False">
                </telerik:RadButton>
                <br />
                &nbsp;&nbsp;<MDM:SDP runat="Server" ID="SDP1" />
            </telerik:RadPageView>
            <telerik:RadPageView ID="pvAccounts" runat="server" Width="100%">
                <br />
                <telerik:RadButton ID="btnCloseNewAccount" runat="server" Text="Return to Grouping"
                    OnClick="btnCloseNewAccount_Click" CausesValidation="False">
                </telerik:RadButton>
                <br />
                &nbsp;&nbsp;<MDM:Accounts runat="server" ID="accounts" />
            </telerik:RadPageView>
            <telerik:RadPageView ID="pvMeters" runat="server" Width="100%">
                <br />
                <telerik:RadButton ID="btnCloseNewMeter" runat="server" Text="Return to Grouping"
                    OnClick="btnCloseNewMeter_Click" CausesValidation="False">
                </telerik:RadButton>
                <br />
                &nbsp;&nbsp;<MDM:Meters runat="server" ID="meters" />
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </telerik:RadAjaxPanel>
</asp:Content>
