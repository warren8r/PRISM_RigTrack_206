<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="confMngrSetAttributeInterval.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrSetAttributeInterval" %>

<%@ Register TagPrefix="MDM" TagName="SDP" Src="~/Controls/configMangrSDP/mngSDP.ascx" %>
<%@ Register TagPrefix="MDM" TagName="Accounts" Src="~/Controls/configMangrSDP/mngAccounts.ascx" %>
<%@ Register TagPrefix="MDM" TagName="Collectors" Src="~/Controls/configMangrSDP/mngCollectors.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="star">
        (<span class="star">*</span>) indicates mandatory fields</div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="50"
        InitialDelayTime="1500">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" ID="updPnl1" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadNotification ID="radnotMessage" runat="server" Text="Initial text" Position="BottomRight"
            AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
            EnableRoundedCorners="true">
        </telerik:RadNotification>
        <asp:Panel ID="pnlTopHalf" runat="server" Width="1000px">
            <fieldset>
                <table style="margin: 5px;">
                    <tr>
                        <td style="width: 100px;">
                            Asset Category:
                        </td>
                        <td>
                            <telerik:RadDropDownList ID="ddlAssetCategoryList" runat="server" AutoPostBack="true"
                                DefaultMessage="- Select -" ToolTip="Select a Category" DataSourceID="sqlGetAssetCategory"
                                DataTextField="clientAssetName" DataValueField="clientAssetID" OnSelectedIndexChanged="ddlAssetCategoryList_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </td>
                        <td>
                            Asset:
                        </td>
                        <td>
                            <telerik:RadDropDownList ID="ddlAssetList" runat="server" AutoPostBack="True" DefaultMessage="- Select -"
                                ToolTip="Select an Asset" DataSourceID="sqlGetAsset" DataTextField="Name" DataValueField="ID"
                                OnSelectedIndexChanged="ddlAssetList_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </td>
                        <td>
                            Attribute:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cbxAttributeList" runat="server" Height="250px" DropDownWidth="380px"
                                MarkFirstMatch="true" AutoPostBack="true" HighlightTemplatedItems="true" ToolTip="Select an Attribute"
                                AppendDataBoundItems="True" DataSourceID="sqlGetAttribute" DataTextField="name"
                                DataValueField="id" OnSelectedIndexChanged="cbxAttriubteList_SelectedIndexChanged">
                                <HeaderTemplate>
                                    <table style="text-align: left">
                                        <tr>
                                            <td style="width: 150px;">
                                                Name
                                            </td>
                                            </td>
                                            <td style="width: 50px;">
                                                Interval
                                            </td>
                                            </td>
                                            <td style="width: 80px;">
                                                Upper Limit
                                            </td>
                                            </td>
                                            <td style="width: 80px;">
                                                Lower Limit
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
                                            <td style="width: 150px;">
                                                <asp:Label ID="lblAttributeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'></asp:Label>
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:Label ID="lblInterval" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intervalName") %>'></asp:Label>
                                            </td>
                                            <td style="width: 80px;">
                                                <asp:Label ID="lblUpperControlLimit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "upperControlLimit") %>'></asp:Label>
                                            </td>
                                            <td style="width: 80px;">
                                                <asp:Label ID="lblLowerControlLimit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lowerControlLimit") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                            <asp:SqlDataSource ID="sqlGetAttribute" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                SelectCommand="SELECT atrb.[ID], atrb.[name], lklit.[itemValue] AS [intervalName], atrb.[upperControlLimit], atrb.[lowerControlLimit]
	                                           FROM [attributes] AS atrb LEFT OUTER JOIN [lookupList] AS lklit ON lklit.[itemKey] = atrb.[interval] AND lklit.[listName] = 'TimeInterval'
                                               ORDER BY atrb.[name]"></asp:SqlDataSource>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkNewAttribute" runat="server" Text="Create New" OnClick="lnkNewAttribute_Click"
                                Width="70px">
                            </asp:LinkButton>
                        </td>
                        <td style="width: 80px; text-align: right;">
                            <telerik:RadButton ID="btnClearFilter" runat="server" class="action" Text="Reset"
                                OnClick="btnClearFilter_Click">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnSaveAssetAttribute" runat="server" Text="Save" CausesValidation="false"
                                AutoPostBack="true" OnClick="btnSaveAssetAttribute_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Style="color: Red; font-size: 10pt; font-weight: bold;"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlEditAttribute" runat="server" Visible="false">
            <table>
                <tr>
                    <%--                    <td style="width: 100px;">
                        Asset Category:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEditAssetCategory" runat="server" ReadOnly="true">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        Asset:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEditAsset" runat="server" ReadOnly="true">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Status:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkEditActive" runat="server" OnCheckedChanged="chkEditActive_CheckChanged"
                            AutoPostBack="true" />
                        <asp:Label ID="lblEditActive" runat="server"></asp:Label>
                    </td>
                    --%>
                    <td>
                        Attribute:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEditAttribute" runat="server" ReadOnly="true">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        Interval:
                    </td>
                    <td>
                        <telerik:RadDropDownList ID="ddlEditIntervalList" runat="server" AutoPostBack="True"
                            ToolTip="Select an Interval" DataSourceID="sqlTimeInterval" DataTextField="itemValue"
                            DataValueField="itemKey">
                        </telerik:RadDropDownList>
                    </td>
                    <td>
                        Upper Limit:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEditUpperLimit" runat="server" EmptyMessage="Upper control limit">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        Lower Limit:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEditLowerLimit" runat="server" EmptyMessage="Lower control limit">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <telerik:RadButton ID="btnSaveEditAttribute" runat="server" Text="Save" ToolTip="Save attribute changes"
                            CausesValidation="true" OnClientClicked="ConfirmSave" OnClick="btnSaveEditAttribute_Click">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnCancelEditAttribute" runat="server" Text="Cancel" ToolTip="Cancel attribute changes"
                            OnClick="btnCancelEditAttribute_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <span style="font-weight: bold; font-size: 10pt;">Asset Attributes/Data Intervals</span>
        <table width="100%">
            <tr>
                <td>
                    <telerik:RadGrid ID="grdAssetAttributes" runat="server" CellSpacing="0" GridLines="None"
                        AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" Width="1000px"
                        OnItemDataBound="grdAssetAttributes_ItemDataBound" OnPageIndexChanged="grdAssetAttributes_PageIndexChanged"
                        OnNeedDataSource="grdAssetAttributes_NeedDataSource" OnItemCommand="grdAssetAttributes_ItemCommand">
                        <ExportSettings FileName="AssetAttributesExport">
                        </ExportSettings>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="attributeID">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <%--                                <telerik:GridTemplateColumn FilterControlAltText="Filter Active column" HeaderText="Status"
                                    SortExpression="bitActive" UniqueName="bitActive" DataField="bitActive">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="True" Checked='<%# Eval("attributebitActive") %>'
                                            OnClick="if(!confirm('Click &quot;OK&quot; to change the status of this item')) return false;"
                                            OnCheckedChanged="grdAttributeDataIntervals_CheckChanged" />
                                        <asp:Label ID="lblActive" runat="server" Text='<%# Convert.ToBoolean(Eval("attributebitActive")) == true ? "Active" : "In-Active" %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                --%>
                                <telerik:GridButtonColumn CommandName="EditAttribute" FilterControlAltText="Filter EditAttribute column"
                                    Text="Edit" UniqueName="EditAttribute">
                                </telerik:GridButtonColumn>
                                <telerik:GridBoundColumn DataField="clientAssetName" FilterControlAltText="Filter Asset Category column"
                                    HeaderText="Asset Category" SortExpression="clientAssetName" UniqueName="clientAssetName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="assetName" FilterControlAltText="Filter Asset column"
                                    HeaderText="Asset" SortExpression="assetName" UniqueName="assetName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="attributeID" FilterControlAltText="Filter Attribute ID column"
                                    HeaderText="Attribute ID" SortExpression="attributeID" UniqueName="attributeID"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="attributeName" FilterControlAltText="Filter Attribute column"
                                    HeaderText="Attribute" SortExpression="attributeName" UniqueName="attributeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="attributeDescription" Display="False" FilterControlAltText="Filter Description column"
                                    HeaderText="Description" SortExpression="attributeDescription" UniqueName="attributeDescription">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="intervalValue" FilterControlAltText="Filter Interval column"
                                    HeaderText="Interval" SortExpression="intervalValue" UniqueName="intervalValue">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="attributeUpperControlLimit" FilterControlAltText="Filter Upper Control Limit column"
                                    HeaderText="Upper Control Limit" SortExpression="attributeUpperControlLimit"
                                    UniqueName="attributeUpperControlLimit">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="attributeLowerControlLimit" FilterControlAltText="Filter Lower Control Limit column"
                                    HeaderText="Lower Control Limit" SortExpression="attributeLowerControlLimit"
                                    UniqueName="attributeLowerContorlLimit">
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
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlNewAttribute" runat="server" Visible="false">
            <div class="dialog">
                <telerik:RadTextBox ID="txtAttributeName" runat="server" class="space" Label="*"
                    EmptyMessage="Attribute name" LabelWidth="10">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="AttributeNameRequiredFieldValidator" runat="server"
                    ControlToValidate="txtAttributeName" ValidationGroup="NewAttribute" SetFocusOnError="true"
                    Display="None" />
                <telerik:RadTextBox ID="txtAttributeDescription" runat="server" class="save" Label="*"
                    EmptyMessage="Description" LabelWidth="10">
                </telerik:RadTextBox>
                <asp:Label ID="lblInterval" runat="server" class="space" Width="60px" Text="Interval: "></asp:Label>
                <telerik:RadDropDownList ID="ddlIntervalList" runat="server" DefaultMessage="- Select -"
                    AutoPostBack="True" ToolTip="Select an Interval" DataSourceID="sqlTimeInterval"
                    DataTextField="itemValue" DataValueField="itemKey">
                </telerik:RadDropDownList>
                <%--                <asp:RequiredFieldValidator ID="IntervalRequiredFieldValidator" runat="server"
                    ControlToValidate="ddlIntervalList" SetFocusOnError="true" Display="None" />
                --%>
                <telerik:RadTextBox ID="txtUpperLimit" runat="server" class="space" Label="*" EmptyMessage="Upper control limit"
                    LabelWidth="10">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="UpperLimitRequiredFieldValidator" runat="server"
                    ControlToValidate="txtUpperLimit" ValidationGroup="NewAttribute" SetFocusOnError="true"
                    Display="None" />
                <telerik:RadTextBox ID="txtLowerLimit" runat="server" class="space" Label="*" EmptyMessage="Lower control limit"
                    LabelWidth="10">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="LowerLimitRequiredFieldValidator" runat="server"
                    ControlToValidate="txtLowerLimit" ValidationGroup="NewAttribute" SetFocusOnError="true"
                    Display="None" />
                <telerik:RadButton ID="btnSaveNewAttribute" runat="server" class="save" Text="Save"
                    CausesValidation="true" ValidationGroup="NewAttribute" OnClick="btnSaveNewAttribute_Click"
                    OnClientClicked="ConfirmSave">
                </telerik:RadButton>
                <telerik:RadButton ID="btnCancelNewAttribute" runat="server" class="action" Text="Cancel"
                    CausesValidation="false" OnClick="btnCancelNewAttribute_Click">
                </telerik:RadButton>
            </div>
            <!-- //.dialog -->
        </asp:Panel>
        <asp:HiddenField ID="hdnEditAttributeID" runat="server" />
        <asp:SqlDataSource ID="sqlTimeInterval" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
            SelectCommand="SELECT id, itemKey, itemValue FROM lookupList WHERE (listName = N'TimeInterval') ORDER BY itemKey">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlGetAssetCategory" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
            SelectCommand="SELECT [clientAssetID], [clientAssetName] FROM [clientAssets] WHERE (IsNull([active], 'false') = 'true')">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlGetAsset" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
            SelectCommand="SELECT [ID], [name] FROM [assets] WHERE ([assetLabelId] = @assetLabelId) AND (IsNull([bitActive], 'false') = 'true')">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlAssetCategoryList" Name="assetLabelId" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <%--        <asp:SqlDataSource ID="sqlUpdAttributeStatus" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
            UpdateCommand="UPDATE attributes SET bitActive = @bitActive WHERE (ID = @ID)">
            <UpdateParameters>
                <asp:Parameter Name="bitActive" />
                <asp:Parameter Name="ID" />
            </UpdateParameters>
        </asp:SqlDataSource>
        --%>
        <asp:SqlDataSource ID="sqlUpdateAttribute" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
            UpdateCommand="UPDATE attributes SET interval = @Interval, upperControlLimit = @UpperControlLimit, lowerControlLimit = @LowerControlLimit WHERE (ID = @ID)">
            <UpdateParameters>
                <asp:ControlParameter ControlID="ddlEditIntervalList" Name="Interval" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="txtEditUpperLimit" Name="UpperControlLimit" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtEditLowerLimit" Name="LowerControlLimit" PropertyName="Text" />
                <asp:ControlParameter ControlID="hdnEditAttributeID" Name="ID" PropertyName="Value" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlInsertAttribute" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
            
            InsertCommand="INSERT INTO attributes(name, interval, upperControlLimit, lowerControlLimit, description) 
                           VALUES (@Name, @Interval, @UpperControlLimit, @LowerControlLimit, @Description)">
            <InsertParameters>
                <asp:ControlParameter ControlID="txtAttributeName" Name="Name" PropertyName="Text" />
                <asp:ControlParameter ControlID="ddlIntervalList" Name="Interval" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="txtUpperLimit" Name="UpperControlLimit" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtLowerLimit" Name="LowerControlLimit" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtAttributeDescription" Name="Description" 
                    PropertyName="Text" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlInsertAssetAttribute" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
            InsertCommand="INSERT INTO assetsAttributes([assetID], [attributeID]) 
                           VALUES (@AssetID, @AttributeID)">
            <InsertParameters>
                <asp:ControlParameter ControlID="ddlAssetList" Name="AssetID" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="cbxAttributeList" Name="AttributeID" PropertyName="SelectedValue" />
            </InsertParameters>
        </asp:SqlDataSource>
    </telerik:RadAjaxPanel>
</asp:Content>
