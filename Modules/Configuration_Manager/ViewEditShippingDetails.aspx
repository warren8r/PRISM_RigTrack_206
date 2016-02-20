<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewEditShippingDetails.aspx.cs" Inherits="Modules_Configuration_Manager_ViewEditShippingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
   <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>Start Date:<br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start" Width="130px">
                                <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                    <DisabledDayStyle CssClass="DisabledClass" />
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                            </telerik:RadDatePicker>
                        </td>
                        <td>Stop Date:<br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_stop" Width="130px">
                                <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                    <DisabledDayStyle CssClass="DisabledClass" />
                                </Calendar>
                                <DateInput ID="DateInput2" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                            </telerik:RadDatePicker>
                        </td>
                        <td>TicketID:<br />
                            <asp:TextBox ID="txt_ticketid" runat="server"></asp:TextBox>
                        </td>
                        <td>Status:<br />
                            <telerik:RadComboBox ID="radcombo_status" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Select" Value="0" />
                                    <telerik:RadComboBoxItem Text="Open" Value="Open" />
                                    <telerik:RadComboBoxItem Text="Closed" Value="Closed" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_view" runat="server" Text="View" OnClick="btn_view_OnClick" />
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
                            <telerik:RadGrid ID="radgridShippingDetails" runat="server" CellSpacing="0"
                                AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" 
                                 GridLines="None" OnItemCommand="grdJobList_ItemCommand" 
                                width="1250" OnItemDataBound="radgridShippingDetails_ItemDataBound">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>

                                <MasterTableView DataKeyNames="ID"   CommandItemStyle-HorizontalAlign="Left" CommandItemDisplay="Top" >

                                    <ItemStyle VerticalAlign="Top" />
                                    <AlternatingItemStyle VerticalAlign="Top" />
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                        ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter jid column"
                                            ReadOnly="True" SortExpression="ID" UniqueName="IntervalMeterDataId" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="TicketID" FilterControlAltText="Filter jobname column"
                                            SortExpression="TicketID" UniqueName="TicketID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TicketID" Font-Bold="true" runat="server" Text='<%# Eval("TicketID") %>' />
                                                <asp:Label ID="lbl_mainid" runat="server" Text='<%# Eval("ID") %>' Visible="false" />
                                            </ItemTemplate>

                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CreatedDate" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="Created Date" SortExpression="CreatedDate" UniqueName="CreatedDate">
                                            <ItemStyle  Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ShippingDate" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="Approx. Shipping Date" SortExpression="ShippingDate" UniqueName="ShippingDate">
                                            <ItemStyle  Font-Bold="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Status" FilterControlAltText="Filter jobtype column"
                                            SortExpression="Status" UniqueName="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Status" Font-Bold="true" runat="server" Text='<%# Eval("Status") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Download/Print" FilterControlAltText="Filter SerialNumber column"
                                        >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_printticket" runat="server" OnClick="lnk_printticket_Click" Text="Download/Print Ticket"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NestedViewTemplate>
                                        
                                                <telerik:RadGrid ID="radgridTicketDetails" runat="server" CellSpacing="0" 
                                                    GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="10"
                                                    OnItemDataBound="radgridTicketDetails_ItemDataBound" OnItemCommand="radgridTicketDetails_OnItemCommand">

                                                    <MasterTableView Width="100%" EditMode="InPlace" DataKeyNames="ID">
                                                        <Columns>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" HeaderText="Edit">
                                                                <ItemStyle Width="50px"></ItemStyle>
                                                            </telerik:GridEditCommandColumn>
                                                            
                                                            
                                                            <telerik:GridBoundColumn DataField="AssetID" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                                                                ReadOnly="True" SortExpression="AssetID" UniqueName="AssetID" Display="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="AssetName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetNameID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_assetid" runat="server" Text='<%# Eval("AssetID") %>' Visible="false"></asp:Label>
                                                                    
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="FromLocation" FilterControlAltText="Filter SerialNumber column"
                                                                SortExpression="FromLocation" UniqueName="FromLocation">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_FromLocation" runat="server" Text='<%# Eval("FromLocation") %>' Visible="false" />
                                                                    <asp:Label ID="lbl_fromlocationid" runat="server" Text='<%# Eval("FromLocationID") %>' Visible="false" />
                                                                    <asp:Label ID="lbl_JobName" runat="server"   />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="From Location Address" FilterControlAltText="Filter SerialNumber column"
                                                                >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_FromLocationAddress" runat="server"  />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="ToLocation" FilterControlAltText="Filter Warehouse column"
                                                                SortExpression="ToLocation" UniqueName="ToLocation">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ToLocation" runat="server" Text='<%# Eval("ToLocation") %>' Visible="false" />
                                                                    <asp:Label ID="lbl_Tolocationid" runat="server" Text='<%# Eval("ToLocationID") %>' Visible="false" />
                                                                    <asp:Label ID="lbl_JobWarehouseName" runat="server"   />
                                                                    
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="To Location Address" FilterControlAltText="Filter SerialNumber column"
                                                                >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ToLocationAddress" runat="server"  />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Status" FilterControlAltText="Filter jobtype column"
                                                                >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AssetMainStatus" runat="server" Text='<%# Eval("AssetMainStatus") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                <telerik:RadComboBox ID="radcombo_type" runat="server" DataSourceID="SqlGetAssetStatus"
                                                                     DataTextField="StatusText" DataValueField="Id" EmptyMessage="- Select -">
                                        
                                                                    </telerik:RadComboBox>
                                                             </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                    </NestedViewTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <%--<asp:SqlDataSource ID="SqlTicket" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                SelectCommand="select * from PrismShippingTicket">
                                    </asp:SqlDataSource>--%>
                            <asp:SqlDataSource ID="SqlGetAssetStatus" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                    SelectCommand="select Id,StatusText from PrsimJobAssetStatus"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

