<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="touConfigureTOU.aspx.cs" Inherits="Modules_TOU_touConfigureTOU" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../../css/style_Sean.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
            Style="padding: 20px 0px 0px 10px;" SelectedIndex="0" CssClass="tabStrip">
            <Tabs>
                <telerik:RadTab Text="Fuel Adjustments" Selected="True" />
                <telerik:RadTab Text="Tax Adjustments" />
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="multiPage">
            <telerik:RadPageView ID="RadPageView1" runat="server" Style="margin: -12px 0px 0px 10px;">
                <fieldset style="width: 830px;">
                    <table>
                        <tr>
                            <td>
                                Fuel Adjustment Code: 
                                <telerik:RadTextBox runat="server" ID="txt_fuelAdjCode" >
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                Fuel Adjustment Charge : ($ per kwh)
                                <telerik:RadTextBox runat="server" ID="txt_fuelAdjCharge" >
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Effective Date:<br />
                                <telerik:RadDatePicker runat="server" ID="rdp_effectiveDate">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td valign="top">
                                <telerik:RadButton runat="server" ID="btnCreate_tab1" Width="85px" Text="Create"
                                    ValidationGroup="tab1" OnClick="btnCreate_tab1_Click" />
                                <telerik:RadButton runat="server" ID="btn_clear_tab1" Width="75px" Text="Clear" CausesValidation="false"
                                    OnClick="btn_clear_tab1_Click" />
                            </td>
                            <td style="padding-left: 25px;">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="#F4F4F4"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small"
                                    BorderColor="Black" ForeColor="Red" ValidationGroup="tab1" DisplayMode="List"
                                    Style="padding-top: 2px; padding-bottom: 2px; vertical-align: middle; min-height: 0px;
                                    max-height: 40px; overflow: hidden; width: 190px;" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label runat="server" ID="lbl_message_tab1" CssClass="success"></asp:Label>
                </fieldset>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txt_fuelAdjCode"
                    OnServerValidate="txt_fuelAdjCode_ServerValidate" Display="None" ValidationGroup="tab1"
                    ErrorMessage="&bull;&nbsp;You must enter a fuel adjust code." ForeColor="#ABADB3"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txt_fuelAdjCharge"
                    OnServerValidate="txt_fuelAdjCharge_ServerValidate" Display="None" ValidationGroup="tab1"
                    ErrorMessage="&bull;&nbsp;You must enter a fuel charge." ForeColor="Red" ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="rdp_effectiveDate"
                    OnServerValidate="rdp_effectiveDate_ServerValidate" Display="None" ValidationGroup="tab1"
                    ErrorMessage="&bull;&nbsp;You must select an effective date." ForeColor="Red"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <telerik:RadGrid ID="rg_fuel" runat="server" Width="852px" DataSourceID="SqlDataSource1"
                    OnItemDataBound="rg_fuel_ItemDataBound" AutoGenerateColumns="False" CellSpacing="0"
                    GridLines="None">
                    <MasterTableView ShowHeadersWhenNoRecords="true">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="isActive" HeaderText="Status" AllowFiltering="false"
                                SortExpression="isActive">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Eval("isActive") %>' AutoPostBack="true"
                                        OnCheckedChanged="checkedChanged" />
                                    <asp:Label ID="Label2" runat="server" ForeColor='<%# (bool)Eval("isActive") ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'
                                        Text='<%# string.Format("{0}", (bool)Eval("isActive") ? "Active" : "Inactive") %>'>
                                    </asp:Label>
                                    <asp:Label runat="server" ID="hidd_ID" Text='<%# Eval("id") %>' Style="display: none;"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="fuelCode" FilterControlAltText="Filter fuelCode column"
                                HeaderText="Fuel Adjustment Code" SortExpression="fuelCode" UniqueName="fuelCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fuelCharge" FilterControlAltText="Filter fuelCharge column"
                                HeaderText="Fuel Charge" SortExpression="fuelCharge" UniqueName="fuelCharge">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="effectiveDate" FilterControlAltText="Filter effectiveDate column"
                                HeaderText="Effective Date" SortExpression="effectiveDate" UniqueName="effectiveDate">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    SelectCommand="SELECT * FROM [touFuelAdjust] ORDER BY [touFuelAdjust].[isActive] DESC">
                </asp:SqlDataSource>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server" Style="margin: -12px 0px 0px 10px;">
                <fieldset style="width: 830px;">
                    <table>
                        <tr>
                            <td>
                                Tax Code :
                                <asp:TextBox ID="txt_taxcode" runat="server" Width="30px" />
                            </td>
                            <td>
                                Tax Description :
                                <asp:TextBox ID="txt_desc" runat="server" Width="50px" />
                            </td>
                            <td>
                                Effective Date :
                                <telerik:RadDatePicker runat="server" ID="rdp_effectiveDate2">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="border: 1px; border-style: solid; width: 385px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_countrytax" runat="server" Text="Country Tax" AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_countrytax" runat="server" Width="30px" />%
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_citytax" runat="server" Text="City Tax" AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_citytax" runat="server" Width="30px" />%
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_othertax" runat="server" Text="Other Tax" AutoPostBack="true" />
                                                <asp:TextBox ID="txt_othertax" runat="server" Width="30px" />%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_statetax" runat="server" Text="State Tax" AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_statetax" runat="server" Width="30px" />%
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_localtax" runat="server" Text="Local Tax" AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_localtax" runat="server" Width="30px" />%
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td valign="top">
                                <telerik:RadButton runat="server" ID="btnCreate_tab2" Width="85px" Text="Create"
                                    ValidationGroup="tab2" OnClick="btnCreate_tab2_Click" />
                                <telerik:RadButton runat="server" ID="btn_clear_tab2" Width="75px" Text="Clear" CausesValidation="false"
                                    OnClick="btn_clear_tab2_Click" />
                            </td>
                            <td style="padding-left: 25px;">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" BackColor="#F4F4F4"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small"
                                    BorderColor="Black" ForeColor="Red" ValidationGroup="tab2" DisplayMode="List"
                                    Style="padding-top: 2px; padding-bottom: 2px; vertical-align: middle; min-height: 0px;
                                    max-height: 40px; overflow: hidden; width: 190px;" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label runat="server" ID="lbl_message_tab2" ForeColor="Red"></asp:Label>
                </fieldset>
                <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txt_taxcode"
                    OnServerValidate="txt_taxcode_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must enter a tax adjust code." ForeColor="#ABADB3"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txt_desc"
                    OnServerValidate="txt_desc_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must enter a description." ForeColor="Red" ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator6" runat="server" ControlToValidate="rdp_effectiveDate2"
                    OnServerValidate="rdp_effectiveDate2_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must select an effective date." ForeColor="Red"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator7" runat="server" ControlToValidate="txt_countrytax"
                    OnServerValidate="txt_countrytax_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must enter a country tax code." ForeColor="#ABADB3"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator8" runat="server" ControlToValidate="txt_citytax"
                    OnServerValidate="txt_citytax_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must enter a country tax code." ForeColor="Red"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator9" runat="server" ControlToValidate="txt_othertax"
                    OnServerValidate="txt_othertax_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must enter a country tax code." ForeColor="Red"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator10" runat="server" ControlToValidate="txt_statetax"
                    OnServerValidate="txt_statetax_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must enter a country tax code." ForeColor="Red"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator11" runat="server" ControlToValidate="txt_localtax"
                    OnServerValidate="txt_localtax_ServerValidate" Display="None" ValidationGroup="tab2"
                    ErrorMessage="&bull;&nbsp;You must enter a country tax code." ForeColor="Red"
                    ValidateEmptyText="True">
                </asp:CustomValidator>
                <telerik:RadGrid ID="rg_tax" runat="server" Width="852px" DataSourceID="SqlDataSource2"
                    OnItemDataBound="rg_tax_ItemDataBound" AutoGenerateColumns="False" CellSpacing="0"
                    GridLines="None">
                    <MasterTableView ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="isActive" HeaderText="Status" AllowFiltering="false"
                                SortExpression="isActive">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Eval("isActive") %>' AutoPostBack="true"
                                        OnCheckedChanged="checkedChangedTax" />
                                    <asp:Label ID="Label1" runat="server" ForeColor='<%# (bool)Eval("isActive") ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'
                                        Text='<%# string.Format("{0}", (bool)Eval("isActive") ? "Active" : "Inactive") %>'>
                                    </asp:Label>
                                    <asp:Label runat="server" ID="hidd_ID" Text='<%# Eval("id") %>' Style="display: none;"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="taxCode" FilterControlAltText="Filter taxCode column"
                                HeaderText="Tax Code" SortExpression="taxCode" UniqueName="taxCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="taxDescription" FilterControlAltText="Filter taxDescription column"
                                HeaderText="Tax Description" SortExpression="taxDescription" UniqueName="taxDescription">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="taxCity" FilterControlAltText="Filter taxCity column"
                                HeaderText="City Tax(%)" SortExpression="taxCity" UniqueName="taxCity">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="taxLocal" FilterControlAltText="Filter taxLocal column"
                                HeaderText="Local Tax(%)" SortExpression="taxLocal" UniqueName="taxLocal">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="taxCountry" FilterControlAltText="Filter taxCountry column"
                                HeaderText="Country Tax(%)" SortExpression="taxCountry" UniqueName="taxCountry">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="taxState" FilterControlAltText="Filter taxState column"
                                HeaderText="State Tax(%)" SortExpression="taxState" UniqueName="taxState">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="taxOther" FilterControlAltText="Filter taxOther column"
                                HeaderText="Other Tax(%)" SortExpression="taxOther" UniqueName="taxOther">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="effectiveDate" FilterControlAltText="Filter effectiveDate column"
                                HeaderText="Effective Date" SortExpression="effectiveDate" UniqueName="effectiveDate">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    SelectCommand="SELECT * FROM [touTaxAdjust] ORDER BY [touTaxAdjust].[isActive] DESC">
                </asp:SqlDataSource>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </telerik:RadAjaxPanel>
</asp:Content>
