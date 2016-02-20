<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="ManageAssetCategories.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrSetAssetLabels" %>

<%@ MasterType VirtualPath="~/Masters/RigTrack.master" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="customCss" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="star">(<span class="star">*</span>) indicates mandatory fields</div>
    <!--
     Roger Orne
     * This page allows the client admin to edit asset category names (the quantity of which is supplied by
     * the super admin upon client registration/subscription).  Asset names set to Active are listed as 
     * "Manage (name)" child items in the Manage Assets menu.
-->
    <div style="margin-left: 200px;">
        <h2 style="display: none;">
            Set Asset Category Names
        </h2>
        <telerik:RadAjaxLoadingPanel ID="radalpLoadingPanel" runat="server" Transparency="50">
            <div class="loading">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading3.gif" AlternateText="loading">
                </asp:Image>
            </div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Height="200px" Width="300px">
            <telerik:RadNotification ID="n1" runat="server" Text="Initial text" Position="BottomRight"
                AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
                EnableRoundedCorners="true">
            </telerik:RadNotification>
            <asp:HiddenField ID="hdnID" runat="server" />
            <asp:Panel ID="panEditAsset" runat="server"  style="margin-bottom: 20px;" width="500px" >
                <table cellpadding="3px">
                    <tr>
                        <td colspan="7">
                            <asp:ValidationSummary ID="EditValidationSummary" runat="server" CssClass="star" 
                                ValidationGroup="EditValidationGroup" ShowSummary="true" DisplayMode="List" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEditStatus" runat="server" width="50px" Text="Status: " ></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEditActive" runat="server" Width="20px" ToolTip="Active/In-Active" TextAlign="Right"
                                AutoPostBack="true" oncheckedchanged="chkEditActive_CheckedChanged"  Checked="true"/>
                        </td>
                        <td>
                            <asp:Label ID="lblEditActive" runat="server" Width="60px" ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditAssetName" runat="server" Width="50px">Name: <span style="color: Red; font-style: bold;">*</span></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="radtxtEditAssetName" runat="server" Width="200px" ToolTip="Asset Category Name" >
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="EditAssetNameRequiredFieldValidator" runat="server" 
                                ValidationGroup="EditValidationGroup" ControlToValidate="radtxtEditAssetName" Display="None" ErrorMessage="Asset name can't be blank" 
                                SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                        </td>
                        <%--<td>
                            <asp:Label ID="lblEditRank" runat="server" Text="Rank:" Width="40px"></asp:Label>
                        </td>--%>
                        <%--<td>
                            <telerik:RadTextBox ID="radtxtEditRank" runat="server" Width="40px" ToolTip="Rank" >
                            </telerik:RadTextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnSaveAssetName" runat="server"
                                Text="Create" ToolTip="Save changes" ValidationGroup="EditValidationGroup"
                                onclientclick="var validated = Page_ClientValidate(); if (!validated) return; if(!confirm('Click &quot;OK&quot; to save your changes')) return false;" 
                                OnClick="btnSaveAssetName_Click" />
                            <asp:Button ID="btnCancelAssetName" runat="server"
                                Text="Cancel" ToolTip="Cancel changes"
                                OnClick="btnCancelAssetName_Click" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:SqlDataSource ID="sqlClientAssetsUpdate" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                UpdateCommand="UPDATE clientAssets SET [clientAssetName] = @ClientAssetName, [active] = @Active WHERE ([clientAssetID] = @ClientAssetID)"
                OnUpdated="sqlClientAssets_Updated" 
                SelectCommand="SELECT clientAssetID, clientAssetName, ISNULL(active, 'false') AS active FROM clientAssets  order by clientAssetName"
                InsertCommand="Insert into clientAssets ([clientAssetName],[active]) values ( @ClientAssetName, @Active)">
                
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="ID" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:ControlParameter ControlID="hdnID" Name="ClientAssetID" PropertyName="Value" />
                    <asp:ControlParameter ControlID="radtxtEditAssetName" Name="ClientAssetName" PropertyName="Text" />
                    <asp:ControlParameter ControlID="chkEditActive" Name="Active" 
                        PropertyName="Checked" DefaultValue="" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:ControlParameter ControlID="radtxtEditAssetName" Name="ClientAssetName" PropertyName="Text" />
                    <asp:ControlParameter ControlID="chkEditActive" Name="Active" 
                        PropertyName="Checked" DefaultValue="" />
                </InsertParameters>
            </asp:SqlDataSource>
            <telerik:RadGrid ID="radgrdAssetNames" runat="server" CellSpacing="0" Width="390px"
                DataSourceID="sqlClientAssetsList" AutoGenerateColumns="False"
                GridLines="None" AllowSorting="True" AllowPaging="True"
                OnItemDataBound="radgrdAssetNames_ItemDataBound"
                onitemcommand="radgrdAssetNames_ItemCommand">
                <ValidationSettings EnableValidation="False" />
                <ClientSettings EnablePostBackOnRowClick="true" >
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
                <SelectedItemStyle CssClass="rgSelectedRow" />
                <MasterTableView DataKeyNames="clientAssetID" DataSourceID="sqlClientAssetsList" ToolTip="Asset Categories">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="clientAssetID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                            ReadOnly="True" SortExpression="clientAssetID" UniqueName="clientAssetID" Display="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="active" DataType="System.Boolean" FilterControlAltText="Filter Active column"
                            UniqueName="Active" HeaderText="Status" SortExpression="active">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("active")) %>' />
                                <asp:Label ID="lblActive" runat="server" Text='<%# Convert.ToBoolean(Eval("active")) == true ? "Active" : "In-Active" %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandArgument="ID" CommandName="EditAsset" 
                            FilterControlAltText="Filter Edit column" Text="Edit" UniqueName="EditAsset">
                            <HeaderStyle Width="40px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="clientAssetName" DataType="System.String" FilterControlAltText="Filter Name column"
                            HeaderText="Name" SortExpression="clientAssetName" UniqueName="clientAssetName">
                            <HeaderStyle Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="rank" Visible="false" 
                            FilterControlAltText="Filter Rank column" HeaderText="Rank" 
                            UniqueName="rank" SortExpression="rank" 
                            EditFormHeaderTextFormat="" DataFormatString="{0:#}">
                            <HeaderStyle Width="40px" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                    <PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True" />
                </MasterTableView>
                <PagerStyle PageSizeControlType="RadComboBox" PageSizes="10;20;50" 
                    AlwaysVisible="True" />
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
    </div>
    <asp:SqlDataSource ID="sqlClientAssetsList" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
        SelectCommand="SELECT [clientAssetID] AS [clientAssetID], [clientAssetName] AS [clientAssetName], ISNULL(active, 'false') AS [active], [rank] AS [rank] 
                       FROM clientAssets ORDER BY [clientAssetName]"
        UpdateCommand="UPDATE [clientAssets] SET [active] = @Active WHERE ([clientAssetID] = @ClientAssetID)" 
        OnUpdated="sqlClientAssets_Updated" >
        <UpdateParameters>
            <asp:Parameter Name="ClientAssetID" />
            <asp:Parameter Name="Active" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
