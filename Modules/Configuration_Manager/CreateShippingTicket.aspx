<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateShippingTicket.aspx.cs" Inherits="Modules_Configuration_Manager_CreateShippingTicket_" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function OnClientSelectedIndexChangedJob(sender, args) {
            var combowarehouse = $find('<%=radcombo_warehouse.ClientID %>');
            if (args.get_item().get_text() == "Select JobName") {
                combowarehouse.enable();
            }
            else {
                combowarehouse.disable();
            }
        }
        function OnClientSelectedIndexChangedWareHouse(sender, args1) {
            var combojob = $find('<%=combo_job.ClientID %>');
            if (args1.get_item().get_text() == "Select WareHouse") {
                combojob.enable();
            }
            else {
                combojob.disable();
            }
        }


        function OnClientSelectedIndexChangedBottomJob(sender, args) {
            var combowarehouse = $find('<%=radcombo_bottomwarehouse.ClientID %>');
            if (args.get_item().get_text() == "Select JobName") {
                combowarehouse.enable();
            }
            else {
                combowarehouse.disable();
            }
        }
        function OnClientSelectedIndexChangedBottomWareHouse(sender, args1) {
            var combojob = $find('<%=radcombo_bottomjob.ClientID %>');
            if (args1.get_item().get_text() == "Select WareHouse") {
                combojob.enable();
            }
            else {
                combojob.disable();
            }
        }
    </script>
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
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="600px">
                    <tr>
                        
                        <td style="font-weight:bold" id="jobdisplay"  runat="server">
                            Select Job:<br />
                            <telerik:RadComboBox runat="server" ID="combo_job" OnClientSelectedIndexChanged="OnClientSelectedIndexChangedJob" DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid"  EmptyMessage="Select JobName" Width="200px"
                                    ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                SelectCommand="select 0 as [jid],'Select JobName' as Jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where status!='Closed' and
                                    (jid in(select jobid from PrismJobKits) or jid in (select jobid from PrismJobConsumables))"></asp:SqlDataSource>
                        </td>
                        <td id="warehousedisplay" runat="server">
                            <b>Select WareHouse:</b><br />
                            <telerik:RadComboBox runat="server" ID="radcombo_warehouse" OnClientSelectedIndexChanged="OnClientSelectedIndexChangedWareHouse" OnDataBound="radcombo_warehouse_DataBound" DataSourceID="SqlGetWarehouse" DataTextField="Name" DataValueField="ID"  EmptyMessage="Select WareHouse" Width="200px"
                                    ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetWarehouse" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                SelectCommand="select Distinct wa.Name,wa.ID from PrsimWarehouses wa,Prism_Assets A where wa.ID=A.WarehouseId"></asp:SqlDataSource>
                        </td>
                        <td id="viewbutton"  runat="server">
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_viewinventory" runat="server" Text="View Inventory" OnClick="btn_viewinventory_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" />
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
                            <telerik:RadGrid ID="grdJobList" runat="server" CellSpacing="0" 
                                 AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  
                                 GridLines="None"     Visible="false">
                                  <%-- OnItemDataBound="grdJobList_ItemDataBound" OnItemDataBound="grdJobList_ItemDataBound" OnItemCommand="grdJobList_ItemCommand"   OnPageIndexChanged="grdJobList_PageIndexChanged"  OnSortCommand="grdJobList_SortCommand"--%>
                                <ExportSettings HideStructureColumns="true">
                                        <Excel Format="Biff" />
                                    </ExportSettings>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>

                                <MasterTableView  CommandItemStyle-HorizontalAlign="Left" CommandItemDisplay="Top"  EditMode="InPlace">
                   
                                   <ItemStyle VerticalAlign="Top" />
                                        <AlternatingItemStyle VerticalAlign="Top" />
                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"  
                                ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"></CommandItemSettings>
                                    <Columns>
                                  <telerik:GridTemplateColumn  HeaderText="Select" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                        
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </telerik:GridTemplateColumn>   
                                <telerik:GridTemplateColumn HeaderText="AssetName">
                                    <ItemTemplate>                              
                                    <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>  
                                    <telerik:GridTemplateColumn   HeaderText="Serial&#160;Number"   FilterControlAltText="Filter SerialNumber column"
                                    SortExpression="SerialNumber" UniqueName="SerialNumber">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                                            <asp:Label ID="lbl_assetId" runat="server" Visible="false" Text='<%# Eval("AssetMainID") %>'/>
                                        </ItemTemplate>                                
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn   HeaderText="Job/WareHouse Name"   FilterControlAltText="Filter jobname column"
                                        SortExpression="jobname" UniqueName="jobname">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("jobname") %>'/>
                                        </ItemTemplate>
                             
                                        </telerik:GridTemplateColumn> 
                                    <telerik:GridTemplateColumn   HeaderText="Location"   FilterControlAltText="Filter clientAssetName column"
                                    SortExpression="shipaddress" UniqueName="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Address" runat="server" Text='<%# Eval("Address") %>'/>
                                    </ItemTemplate>
                             
                                    </telerik:GridTemplateColumn>
                                   <%-- <telerik:GridTemplateColumn   HeaderText="Asset&#160;Owner"   FilterControlAltText="Filter Warehouse column"
                                    SortExpression="Warehouse" UniqueName="Warehouse">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Warehouse" runat="server" Text='<%# Eval("Warehouse") %>'/>
                                        </ItemTemplate>                                
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;BY"   FilterControlAltText="Filter ModifiedBY column"
                                    SortExpression="username" UniqueName="username">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("username") %>'/>
                                    </ItemTemplate>                             
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn  HeaderText="Modified&#160;Date" DataFormatString="{0:MM/dd/yyyy}"  FilterControlAltText="Filter ModifiedDate column"
                                    SortExpression="ModifiedDate" UniqueName="ModifiedDate" DataField="ModifiedDate"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;Date" Visible="false" FilterControlAltText="Filter ModifiedDate column"
                                    SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ModifiedDate" runat="server" Text='<%# Eval("ModifiedDate") %>'/>
                                    </ItemTemplate>                             
                                    </telerik:GridTemplateColumn>--%> 
                                    <telerik:GridTemplateColumn   HeaderText="Status"   FilterControlAltText="Filter AssetStatus column"
                                    SortExpression="StatusText" UniqueName="StatusText">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_AssetStatus" runat="server" Text='<%#Eval("StatusText") %>'/>
                                    </ItemTemplate>
                             
                                    </telerik:GridTemplateColumn>                            
                                     
                                    </Columns>
              
                                 </MasterTableView>                 
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td >
                                        <br />
                                        <asp:ImageButton ID="btn_update" runat="server" Text="Update" ImageUrl="~/images/arrow-down-icon.png" Width="30px" Height="30px" OnClick="btn_update_Click"  />
                                        <asp:HiddenField ID="hid_selectedids" runat="server" />
                                        <asp:HiddenField ID="hid_selectedbottom" runat="server" />
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:ImageButton ID="img_updatetotop" runat="server"  ImageUrl="~/images/arrow-up-icon.png" OnClick="img_updatetotop_OnClick" Width="30px" Height="30px"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadgridForRemove" runat="server" CellSpacing="0" 
                                 AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  
                                 GridLines="None"  Visible="false" >
                                <ExportSettings HideStructureColumns="true">
                                        <Excel Format="Biff" />
                                    </ExportSettings>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>

                                <MasterTableView ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No Data Found" CommandItemDisplay="Top">
                   
                                   <ItemStyle VerticalAlign="Top" />
                                        <AlternatingItemStyle VerticalAlign="Top" />
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"  
                                ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"></CommandItemSettings>
                                    <Columns>
                                  <telerik:GridTemplateColumn  HeaderText="Remove" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect1" runat="server" />
                                        
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </telerik:GridTemplateColumn>   
                                <telerik:GridTemplateColumn HeaderText="AssetName">
                                    <ItemTemplate>                              
                                    <asp:Label ID="lbl_AssetName1" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>  
                                    <telerik:GridTemplateColumn   HeaderText="Serial&#160;Number"   FilterControlAltText="Filter SerialNumber column"
                                    SortExpression="SerialNumber" UniqueName="SerialNumber1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SerialNumber1" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                                            <asp:Label ID="lbl_assetId1" runat="server" Visible="false" Text='<%# Eval("AssetMainID") %>'/>
                                        </ItemTemplate>                                
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn   HeaderText="Job/WareHouse Name"   FilterControlAltText="Filter jobname column"
                                        SortExpression="jobname" UniqueName="jobname1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_jobname1" runat="server" Text='<%# Eval("jobname") %>'/>
                                        </ItemTemplate>
                             
                                        </telerik:GridTemplateColumn> 
                                    <telerik:GridTemplateColumn   HeaderText="Location"   FilterControlAltText="Filter clientAssetName column"
                                    SortExpression="shipaddress" UniqueName="Address1">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Address1" runat="server" Text='<%# Eval("Address") %>'/>
                                    </ItemTemplate>
                             
                                    </telerik:GridTemplateColumn>
                                   
                                    <telerik:GridTemplateColumn   HeaderText="Status"   FilterControlAltText="Filter AssetStatus column"
                                    SortExpression="StatusText" UniqueName="StatusText1">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_AssetStatus1" runat="server" Text='<%#Eval("StatusText") %>'/>
                                    </ItemTemplate>
                             
                                    </telerik:GridTemplateColumn>                            
                                     
                                    </Columns>
              
                                 </MasterTableView>                 
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td id="td_bottom" runat="server" visible="false">
                            <table>
                                <tr>
                                    
                                    <td style="font-weight:bold" >
                                        Select Job:<br />
                                        <telerik:RadComboBox runat="server" ID="radcombo_bottomjob" OnClientSelectedIndexChanged="OnClientSelectedIndexChangedBottomJob" DataSourceID="SqlGetbottomJobs" DataTextField="jobname" DataValueField="jid"  EmptyMessage="Select JobName" Width="200px"
                                                ></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetbottomJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                            SelectCommand="select 0 as [jid],'Select JobName' as Jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where status!='Closed' and
                                                (jid in(select jobid from PrismJobKits) or jid in (select jobid from PrismJobConsumables))"></asp:SqlDataSource>
                                    </td>
                                    <td id="Td2" runat="server">
                                        <b>Select WareHouse:</b><br />
                                        <telerik:RadComboBox runat="server" ID="radcombo_bottomwarehouse" OnDataBound="radcombo_bottomwarehouse_DataBound" OnClientSelectedIndexChanged="OnClientSelectedIndexChangedBottomWareHouse" DataSourceID="SqlGetbottomWarehouse" DataTextField="Name" DataValueField="ID"  EmptyMessage="Select WareHouse" Width="200px"
                                                ></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetbottomWarehouse" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                            SelectCommand="select Distinct wa.Name,wa.ID from PrsimWarehouses wa,Prism_Assets A where wa.ID=A.WarehouseId"></asp:SqlDataSource>
                                    </td>
                                    <td>
                                        <td align="left">
                                        Ship Date<br />
                                        <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                            <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                                <DisabledDayStyle CssClass="DisabledClass" />
                                            </Calendar>
                                            <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                                        </telerik:RadDatePicker>
                                    </td>
                                    </td>
                                    <td id="Td3"  runat="server">
                                        <br />
                                        <asp:Button ID="btn_createticket" runat="server" Text="Create Ticket" OnClick="btn_createticket_OnClick" />
                                        <asp:Button ID="btn_print" runat="server" Text="Download Ticket" Visible="false" OnClick="btn_print_OnClick" />
                                        <asp:HiddenField ID="hid_ticketid" runat="server" />
                                    </td>
                        
                        
                         
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

