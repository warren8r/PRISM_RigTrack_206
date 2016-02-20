<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="CreateToolGroup.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrSetAssetLabels" %>

<%@ MasterType VirtualPath="~/Masters/RigTrack.master" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <!--
     Roger Orne
     * This page allows the client admin to edit asset category names (the quantity of which is supplied by
     * the super admin upon client registration/subscription).  Asset names set to Active are listed as 
     * "Manage (name)" child items in the Manage Assets menu.
-->

    <h2 style="display: none;">Set Tool Category Names
    </h2>
    <%-- // Jd New CSS Loading Animation--%>
        <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
            <ProgressTemplate>

                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                    <div class="loader2">Loading...</div>

                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>


    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server"  Width="100%">
        <telerik:RadNotification ID="n1" runat="server" Text="Initial text" Position="BottomRight"
            AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
            EnableRoundedCorners="true">
        </telerik:RadNotification>
        <asp:HiddenField ID="hdnID" runat="server" />

        <fieldset>

            <asp:Panel ID="panEditAsset" runat="server" Width="100%" HorizontalAlign="Center">



                  <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Create Tool Category</h2>
                            </asp:TableCell>
                        </asp:TableRow>
                       

                      </asp:Table>



                <asp:Table ID="Table11" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table12" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Tool Category Name
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Status
                                        
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						
                                    </asp:TableHeaderCell>




                                </asp:TableRow>


                                <asp:TableRow>



                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="radtxtEditAssetName" runat="server" Width="180px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="EditAssetNameRequiredFieldValidator" runat="server"
                                            ValidationGroup="EditValidationGroup" ControlToValidate="radtxtEditAssetName" ForeColor="Red" ErrorMessage="Tool Category can't be blank"
                                            SetFocusOnError="true" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                    </asp:TableCell>

                                    <asp:TableCell>

                                        <telerik:RadComboBox ID="cmbStatus" runat="server" Width="180px" DropDownWidth="180px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="- Select -" Value="Select" />
                                                <telerik:RadComboBoxItem Text="Active" Value="True" />
                                                <telerik:RadComboBoxItem Text="In-Active" Value="False" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="- Select -"
                                            ValidationGroup="EditValidationGroup" ControlToValidate="cmbStatus" ForeColor="Red" ErrorMessage="Please select status"
                                            SetFocusOnError="true" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>


                                    <asp:TableCell>

                                        <asp:Button ID="btnSaveAssetName" runat="server"
                                            Text="Create" ToolTip="Save changes" ValidationGroup="EditValidationGroup"
                                            OnClick="btnSaveAssetName_Click" />

                                    </asp:TableCell>

                                    <asp:TableCell>

                                        <asp:Button ID="btnCancelAssetName" runat="server"
                                            Text="Cancel" ToolTip="Cancel changes"
                                            OnClick="btnCancelAssetName_Click" CausesValidation="False" />
                                    </asp:TableCell>

                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>


                 <asp:Table ID="Table1" runat="server" Width="100%" HorizontalAlign="Center">

            <asp:TableRow>

                <asp:TableCell HorizontalAlign="Center">
        <telerik:RadGrid ID="radgrdAssetNames" runat="server" CellSpacing="0" Width="60%"
            DataSourceID="sqlClientAssetsList" AutoGenerateColumns="False"
            GridLines="None" AllowSorting="True" AllowPaging="True"
            OnItemDataBound="radgrdAssetNames_ItemDataBound"
            OnItemCommand="radgrdAssetNames_ItemCommand">
            <ValidationSettings EnableValidation="False" />
            <ClientSettings EnablePostBackOnRowClick="true">
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

                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>


            </asp:Panel>

        </fieldset>


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
                <asp:ControlParameter ControlID="cmbStatus" Name="Active"
                    PropertyName="SelectedValue" DefaultValue="" />
            </UpdateParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="radtxtEditAssetName" Name="ClientAssetName" PropertyName="Text" />
                <asp:ControlParameter ControlID="cmbStatus" Name="Active"
                    PropertyName="SelectedValue" DefaultValue="" />
            </InsertParameters>
        </asp:SqlDataSource>


       

         <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>
    </telerik:RadAjaxPanel>

    <asp:SqlDataSource ID="sqlClientAssetsList" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
        SelectCommand="SELECT [clientAssetID] AS [clientAssetID], [clientAssetName] AS [clientAssetName], ISNULL(active, 'false') AS [active], [rank] AS [rank] 
                       FROM clientAssets ORDER BY [clientAssetName]"
        UpdateCommand="UPDATE [clientAssets] SET [active] = @Active WHERE ([clientAssetID] = @ClientAssetID)"
        OnUpdated="sqlClientAssets_Updated">
        <UpdateParameters>
            <asp:Parameter Name="ClientAssetID" />
            <asp:Parameter Name="Active" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
