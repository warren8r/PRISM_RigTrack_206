<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="JobCosting.aspx.cs" Inherits="Modules_Configuration_Manager_JobCosting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">


            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function downloadFile(fileName, downloadLink1) {

                var downloadLink = $get('downloadFileLink');
                var filePath = "../../Documents/" + fileName;
                downloadLink.href = "DownloadHandler.ashx?fileName=" + fileName + "&filePath=" + filePath;
                downloadLink.style.display = 'block';
                downloadLink.style.display.visibility = 'hidden';
                downloadLink.click();

                // return false;

            }
        </script>
    </telerik:RadScriptBlock>
    <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Always">

        <ContentTemplate>
            <%-- // Jd New CSS Loading Animation--%>
            <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
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
            <table width="100%">
                <tr>
                    <td style="padding-left: 30px">
                        <h2>Job Costing</h2>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_message" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>Select Company<br />
                                    <telerik:RadDropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" Width="220px">
                                        <Items>
                                            <telerik:DropDownListItem Value="0" Text="Select All" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td>Select Job:<br />
                                    <telerik:RadComboBox ID="radcombo_job" runat="server" Width="220px" OnSelectedIndexChanged="radcombo_job_SelectedIndexChanged" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="left">Date<span class="star">*</span><br />
                                    <telerik:RadDatePicker runat="server" ID="radtxt_start" Width="130px">
                                        <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                            <DisabledDayStyle CssClass="DisabledClass" />
                                        </Calendar>
                                        <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                                    </telerik:RadDatePicker>
                                </td>
                                <%--<td>
                        <br />
                            <asp:Button ID="btn_view" runat="server" OnClick="btn_view_OnClick" Text="View" />
                        </td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>Cost Group:
                                    <br />
                                    <telerik:RadComboBox ID="RadcomboCostGroup" runat="server" DataSourceID="SqlDataSource2" DataTextField="CostGroupName"
                                        DataValueField="CostGroupID" Height="200px" Width="250px" AppendDataBoundItems="true">
                                    </telerik:RadComboBox>
                                </td>
                                <td>Charge By :<br />
                                    <telerik:RadComboBox ID="RadComboChargeBy" runat="server" DataSourceID="SqlDataSource3" DataTextField="ChargeByDesc"
                                        DataValueField="ChargeBYID" Height="200px" Width="250px" AppendDataBoundItems="true">
                                    </telerik:RadComboBox>
                                </td>
                                <td>Price/Unit:<br />
                                    <telerik:RadTextBox ID="txtPriceperUnit" onChange="caliculateTotal();" runat="server"></telerik:RadTextBox>
                                </td>
                                <td>Qty:<br />
                                    <telerik:RadTextBox ID="txtQty" onChange="caliculateTotal();" runat="server"></telerik:RadTextBox>
                                </td>
                                <td>Total:<br />
                                    <telerik:RadTextBox ID="txtTotal" ReadOnly="true" runat="server"></telerik:RadTextBox>
                                </td>
                                <td>
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                    <script type="text/javascript">
                                        function caliculateTotal() {
                                            var priceperunit = document.getElementById('<%=txtPriceperUnit.ClientID %>').value;
                                    var qty = document.getElementById('<%=txtQty.ClientID %>').value;

                                    if (priceperunit == "")
                                        priceperunit = 1;
                                    if (qty == "")
                                        qty = 1;

                                    var totalVal = parseFloat(priceperunit) * parseFloat(qty);
                                    document.getElementById('<%=txtTotal.ClientID %>').value = parseFloat(totalVal);
                                    return false;
                                }
                                    </script>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hidden_serviceid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px"></td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td align="center" style="border:solid 1px #000000">
                                    <table>
                                        <tr>
                                            <td colspan="4" style="background-color:#083f53;color:white;font-weight:bold;height:20px">
                                                <table>
                                                    <tr><td>Search</td></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td align="left">Select Job<br />

                                                <telerik:RadComboBox  ID="comboSearchJob" runat="server" Width="220px" AppendDataBoundItems="false" DropDownHeight="220px">
                                                    
                                                </telerik:RadComboBox>
                                            </td>
                                            <td align="left">Date<br />
                                                <telerik:RadDatePicker runat="server" ID="DateSearch" Width="130px">
                                                    <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                                        <DisabledDayStyle CssClass="DisabledClass" />
                                                    </Calendar>
                                                    <DateInput ID="DateInput2" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <br />
                                                <asp:Button ID="btn_view" runat="server" Text="Search" OnClick="btn_view_Click" />
                                            </td>
                                            <td>
                                                <br />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="RadGridJobCosting" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30"
                                        Width="1250px" OnItemCommand="RadGridJobCosting_ItemCommand">
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true" Scrolling-AllowScroll="true">
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <ExportSettings>
                                            <Excel Format="Html" />
                                        </ExportSettings>
                                        <MasterTableView ShowFooter="true" DataKeyNames="ID" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                                            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                            <Columns>
                                                <telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                                                    Text="Edit" UniqueName="btnEdit">
                                                </telerik:GridButtonColumn>

                                                <telerik:GridBoundColumn HeaderText="ID" ReadOnly="true" DataField="MotorID" Display="false" UniqueName="MotorID" SortExpression="MotorID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Job/Curve Group" HeaderStyle-Width="200px" ItemStyle-Width="200px" ReadOnly="true" DataField="JobName" UniqueName="JobName"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Date" FilterControlAltText="Filter startdate column"
                                                    SortExpression="Date" UniqueName="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%#DateTime.Parse(Eval("Date").ToString()).ToString("MM/dd/yyyy")%>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridBoundColumn HeaderText="Date" ReadOnly="true" DataField="Date" UniqueName="Date"></telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn HeaderText="Cost Group" DataField="CostGroupName" UniqueName="CostGroupName"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Charge By" DataField="ChargeByDesc" UniqueName="ChargeByDesc"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Price/Unit($)" DataField="PriceperUnit" UniqueName="PriceperUnit"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Qty" DataField="NumberOfUnits" FooterText="Total Price: " UniqueName="NumberOfUnits">
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Total($)" DataField="Total" UniqueName="Total" FooterText="$" DataType="System.Decimal" Aggregate="Sum"></telerik:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                            ConnectionString="<%$ databaseExpression:client_database %>"
                            SelectCommand="SELECT CostGroupID, CostGroupName FROM tlkpCostGroupDetails ORDER BY CostGroupName asc"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                            ConnectionString="<%$ databaseExpression:client_database %>"
                            SelectCommand="SELECT ChargeBYID, ChargeByDesc FROM tlkpChargeByDetails ORDER BY ChargeByDesc asc"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="RadButton1" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

