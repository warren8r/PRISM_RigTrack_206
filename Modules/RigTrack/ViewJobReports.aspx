<%@ Page Language="C#" Title="View Job Reports" MasterPageFile="~/Masters/ViewAllReports.master" AutoEventWireup="true" CodeFile="ViewJobReports.aspx.cs" Inherits="Modules_RigTrack_ViewJobReports" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Masters/ViewAllReports.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function Close() {
            GetRadWindow().close(); // Close the window 
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

            return oWindow;
        }
        function GridCreated(sender, args) {
            sender.style.width = "1400px"
            sender.style.height = "600px"
        }

        function ChangeLabel(sender, eventArgs) {
            var item = eventArgs.get_item();
            document.getElementById('<%=Master.FindControl("lblCurveName").ClientID %>').innerText = "Curve Name: " + item.get_text();
            
        }
    </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadWindow ID="RadWindow1" runat="server">
        </telerik:RadWindow>
        <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
            <ProgressTemplate>

                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                    <div class="loader2">Loading...</div>

                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>


        <fieldset>

            <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label runat="server">Selected Curve:</asp:Label>
                    <telerik:RadDropDownList ID="ddlCurve" runat="server" Width="160px" AppendDataBoundItems="true" AutoPostBack="true" OnClientItemSelected="ChangeLabel" OnSelectedIndexChanged="ddlCurve_SelectedIndexChanged">
                        <Items>
                            <telerik:DropDownListItem Value="0" Text="-Select-" />
                        </Items>
                    </telerik:RadDropDownList>
                    <telerik:RadGrid ID="RadGrid1" OnPageSizeChanged="RadGrid1_PageSizeChanged" OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowPaging="True" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="true" runat="server" 
                        AllowMultiRowSelection="false" ShowFooter="true" OnItemCreated="RadGrid1_ItemCreated" Width="1640" Height="600px" OnNeedDataSource="RadGrid1_NeedDataSource"  OnItemCommand="RadGrid1_ItemCommand"  >
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                        <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        </ClientSettings>
                        <MasterTableView PagerStyle-AlwaysVisible="true" CommandItemDisplay="Top" NoMasterRecordsText="No Curve Selected" >
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                ShowExportToCsvButton="true" ShowExportToPdfButton="true"></CommandItemSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
                <table>
                    <tr>
                        <td>
                            <Telerik:RadButton ID="btnExportToASCII" Skin="Black" runat="server" Text="Export To ASCII" OnClick="btnExportToASCII_Click" />
                        </td>
                    </tr>
                </table>
        <table>
            <tr>
                <td style="height:10px"></td>
            </tr>
            <tr>
                <td>
                    <table>
                    <%--<tr><td><b>Job/Curve Group Info</b></td></tr>
                    <tr>
                       
                        <td>
                            <telerik:RadGrid ID="RadGrid2"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource1"  Width="1250px" 
                               >
                
                            
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource1">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from [RigTrack].[tblCurveGroup] where [ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>--%>
                    <tr><td><b>Job Tools/Tool Group Details</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdJobList" runat="server" CellSpacing="0"  Width="1250px"   
                 AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  OnSortCommand="grdJobList_SortCommand"
                 OnPageIndexChanged="grdJobList_PageIndexChanged" GridLines="None"    OnItemCommand="grdJobList_ItemCommand">
                  <%--OnItemDataBound="grdJobList_ItemDataBound"--%>
                <ExportSettings HideStructureColumns="true">
        </ExportSettings>
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>

                <MasterTableView DataKeyNames="ID" CommandItemStyle-HorizontalAlign="Left" CommandItemDisplay="Top"  EditMode="InPlace">
                   
                   <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
            <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"  
                ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter jid column"
                            ReadOnly="True" SortExpression="ID" UniqueName="IntervalMeterDataId" Display="false">
                        </telerik:GridBoundColumn>     
                        <telerik:GridTemplateColumn   HeaderText="Job/Curve Group Name"   FilterControlAltText="Filter JOB column"
                            SortExpression="JOB" UniqueName="JOB">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("JOB") %>'/>
                                <asp:Label ID="lbl_jobid" runat="server" Text= '<%# Eval("ID") %>' Visible="false"/>
                            </ItemTemplate>
                             
                            </telerik:GridTemplateColumn>  
                            <telerik:GridTemplateColumn   HeaderText="Job Number"   FilterControlAltText="Filter jobid column"
                            SortExpression="JobNumber" UniqueName="JobNumber">
                            <ItemTemplate>
                                
                               <asp:Label ID="lbl_jobnumber" runat="server" Text= '<%# Eval("JobNumber") %>'/>
                            </ItemTemplate>
                             
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn   HeaderText="Job Location"   FilterControlAltText="Filter jobtype column"
                            SortExpression="JobLocation" UniqueName="JobLocation">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_joblocation" runat="server" Text='<%# Eval("JobLocation") %>'/>
                                </ItemTemplate>                                
                            </telerik:GridTemplateColumn> 
                        <telerik:GridTemplateColumn   HeaderText="Lease/Well"   FilterControlAltText="Filter RigName column"
                            SortExpression="LeaseWell" UniqueName="LeaseWell">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_LeaseWell" runat="server" Text='<%# Eval("LeaseWell") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn   HeaderText="RigName"   FilterControlAltText="Filter RigName column"
                            SortExpression="RigName" UniqueName="RigName">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_RigName" runat="server" Text='<%# Eval("RigName") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>  
                                                   
                              <telerik:GridTemplateColumn   HeaderText="Job Start Date"   FilterControlAltText="Filter startdate column"
                            SortExpression="JobStartDate" UniqueName="JobStartDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_startdate"  runat="server" Text='<%#DateTime.Parse(Eval("JobStartDate").ToString()).ToString("MM/dd/yyyy")%>' />
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>
                             
                                           
                        
                    </Columns>
               <NestedViewTemplate>
                       <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black"  SelectedIndex="0" 
                        Height="100%"  MultiPageID="RadMultiPage1">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="t1"  Text="Tools" Selected="True">
                            </telerik:RadTab>
                           <%-- <telerik:RadTab runat="server" PageViewID="t2" Text="Personnel" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t3" Text="Services" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t4" Text="Consumables" >
                            </telerik:RadTab>--%>
                            <telerik:RadTab runat="server" PageViewID="t5" Text="Tool Group" >
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage ID="RadMultiPage1" RenderSelectedPageOnly="false" 
                        runat="server" SelectedIndex="0" CssClass="multiPage">
                        <telerik:RadPageView ID="t1" runat="server" >
                            <telerik:RadGrid ID="gridJobAssets" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" OnItemDataBound="gridJobAssets_ItemDataBound"  AllowSorting="True" AllowPaging="True"  PageSize="10">      
                                    <MasterTableView   Width="100%" EditMode="InPlace" >                   
                                    <Columns>
                                     
                <telerik:GridBoundColumn DataField="AssetId" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                    ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="False">
                </telerik:GridBoundColumn>  
                              
                <telerik:GridTemplateColumn HeaderText="AssetName">
                    <ItemTemplate>                              
                    <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn   HeaderText="Serial&#160;Number"   FilterControlAltText="Filter SerialNumber column"
                    SortExpression="SerialNumber" UniqueName="SerialNumber">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                        </ItemTemplate>                                
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Asset&#160;Owner"   FilterControlAltText="Filter Warehouse column"
                    SortExpression="Warehouse" UniqueName="Warehouse">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Warehouse" runat="server" Text='<%# Eval("Warehouse") %>'/>
                        </ItemTemplate>                                
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Asset&#160;Category"   FilterControlAltText="Filter clientAssetName column"
                    SortExpression="clientAssetName" UniqueName="clientAssetName">
                    <ItemTemplate>
                        <asp:Label ID="lbl_clientAssetName" runat="server" Text='<%# Eval("clientAssetName") %>'/>
                    </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Kit&#160;Name" Visible="false"   FilterControlAltText="Filter clientAssetName column"
                     UniqueName="KitName">
                    <ItemTemplate>
                        <asp:Label ID="lbl_KitName" runat="server" Text='<%#Eval("KitName") %>' />
                    </ItemTemplate>
                             
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Asset&#160;Status"   FilterControlAltText="Filter AssetStatus column"
                    SortExpression="AssetStatus" UniqueName="AssetStatus">
                    <ItemTemplate>
                        <asp:Label ID="lbl_AssetStatus" runat="server" Text='<%#Eval("AssetStatus") %>'/>
                    </ItemTemplate>
                             
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;BY"   FilterControlAltText="Filter ModifiedBY column"
                    SortExpression="username" UniqueName="username">
                    <ItemTemplate>
                        <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("username") %>'/>
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;Date"   FilterControlAltText="Filter ModifiedDate column"
                    SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ModifiedDate" runat="server" Text='<%#DateTime.Parse(Eval("ModifiedDate").ToString()).ToString("MM/dd/yyyy")%>'/>
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn>
            </Columns>
                                    </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="t5" runat="server">
                             <telerik:RadGrid ID="RadGrid_kits" OnItemDataBound="RadGrid_kits_ItemDataBound" runat="server" CellSpacing="0" 
                                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
                            <MasterTableView     >                   
                                <Columns>
                                     
                                    <telerik:GridBoundColumn DataField="jobkitid" DataType="System.Int32" FilterControlAltText="Filter jobconsumableid column"
                                        ReadOnly="True" SortExpression="jobkitid" UniqueName="jobkitid" Display="False">
                                    </telerik:GridBoundColumn>  
                                    
                        
                                    <telerik:GridTemplateColumn HeaderText="Kit Name">
                                       <ItemTemplate >                              
                                        <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("kitname") %>'></asp:Label>
                                           <asp:Label ID="lbl_kitid" runat="server" Text='<%# Eval("kitid") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="lbl_jobid" runat="server" Text='<%# Eval("jobid") %>' Visible="false"></asp:Label>
                               
                                       </ItemTemplate>
                           
                                    </telerik:GridTemplateColumn> 
                                    <telerik:GridTemplateColumn HeaderText="Kit Status">
                                       <ItemTemplate>                              
                                            <asp:Label ID="lbl_kitstatusid" runat="server" Text='<%# Eval("KitDeliveryStatus") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="lbl_kitstatus" runat="server" ></asp:Label>
                                       </ItemTemplate>
                           
                                    </telerik:GridTemplateColumn> 
                                    <telerik:GridTemplateColumn HeaderText="Assets">
                                       <ItemTemplate >                              
                            
                                           <telerik:RadGrid ID="radgridkitassets" runat="server" ShowHeader="false" AutoGenerateColumns="true">

                                           </telerik:RadGrid>
                                       </ItemTemplate>
                           
                                    </telerik:GridTemplateColumn>
                         
                    </Columns>
                    </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>

                               
                    </NestedViewTemplate>  
                 </MasterTableView>                 
            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr><td><b>BHA Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGridBHAInfo" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30"
                     Width="1250px"  >
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true" Scrolling-AllowScroll="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <ExportSettings>
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView EditMode="InPlace" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <Columns>
                            <%--<telerik:GridEditCommandColumn ButtonType="LinkButton" HeaderText="Edit"></telerik:GridEditCommandColumn>--%>
                            <telerik:GridBoundColumn HeaderText="ID" ReadOnly="true" Display="false" DataField="ID" UniqueName="ID" SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Job/Curve Group" ReadOnly="true" DataField="JobName" UniqueName="JobName" ></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Number" ReadOnly="true" DataField="BHANumber" UniqueName="BHANumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Desc" DataField="BHADesc" UniqueName="BHADesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Type" DataField="BHAType" UniqueName="BHAType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bit Sno" DataField="BitSno" UniqueName="BitSno"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bit Desc" DataField="BitDesc" UniqueName="BitDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ODFrac" DataField="ODFrac" UniqueName="ODFrac"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitLength" DataField="BitLength" UniqueName="BitLength"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Connection" DataField="Connection" UniqueName="Connection"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitType" DataField="BitType" UniqueName="BitType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BearingType" DataField="BearingType" UniqueName="BearingType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitMfg" DataField="BitMfg" UniqueName="BitMfg"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitNumber" DataField="BitNumber" UniqueName="BitNumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="No. JETS" DataField="NUMJETS" UniqueName="NUMJETS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="InnerRow" DataField="InnerRow" UniqueName="InnerRow"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="OuterRow" DataField="OuterRow" UniqueName="OuterRow"></telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn HeaderText="DullChar" DataField="DullChar" UniqueName="DullChar"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Location" DataField="Location" UniqueName="Location"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BearingSeals" DataField="BearingSeals" UniqueName="BearingSeals"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Guage" DataField="Guage" UniqueName="Guage"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="OtherDullChar" DataField="OtherDullChar" UniqueName="OtherDullChar"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ReasonPulled" DataField="ReasonPulled" UniqueName="ReasonPulled"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="MotorDesc" DataField="MotorDesc" UniqueName="MotorDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="MotorMFG" DataField="MotorMFG" UniqueName="MotorMFG"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="NBStabilizer" DataField="NBStabilizer" UniqueName="NBStabilizer"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Model" DataField="Model" UniqueName="Model"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Revolutions" DataField="Revolutions" UniqueName="Revolutions"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bend" DataField="Bend" UniqueName="Bend"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RotorJet" DataField="RotorJet" UniqueName="RotorJet"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BittoBend" DataField="BittoBend" UniqueName="BittoBend"></telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                        </td>
                    </tr>
                        <tr><td><b>BHA Item Details</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="radgrdMeterList" runat="server" CellSpacing="0" GridLines="None"
         AllowFilteringByColumn="True" >
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
            
        </ClientSettings>
        <MasterTableView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="ID" AllowFilteringByColumn="True" PageSize="10">
            <PagerStyle Position="TopAndBottom" PageSizeControlType="RadComboBox" AlwaysVisible="true"></PagerStyle>
            <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                ShowRefreshButton="False" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                <HeaderStyle Width="20px" />
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                <HeaderStyle Width="20px" />
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn DataField="Status" DataType="System.Boolean" FilterControlAltText="Filter Status column"
                    HeaderText="Status" SortExpression="Status" AllowFiltering="false" UniqueName="Status">
                    <HeaderStyle Width="80px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkActive" runat="server" />
                        
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <%--<telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                    Text="Edit" UniqueName="btnEdit" HeaderText="Edit">
                </telerik:GridButtonColumn>--%>
                <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                    HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID" Display="false">
                </telerik:GridBoundColumn>
                
                
                <telerik:GridBoundColumn DataField="assetcategory" FilterControlAltText="Filter meterType column"
                    HeaderText="Tool Group Name" SortExpression="assetcategory" AllowFiltering="false" UniqueName="assetcategory">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AssetName" AllowFiltering="false" FilterControlAltText="Filter meterName column"
                    HeaderText="Tool Type" SortExpression="AssetName" UniqueName="AssetName">
                    <HeaderStyle Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="serialNumber"  AllowFiltering="false" FilterControlAltText="Filter serialNumber column"
                    HeaderText="Serial #" SortExpression="serialNumber" UniqueName="serialNumber">
                    <HeaderStyle Width="80px" />
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="warehouse" AllowFiltering="false" FilterControlAltText="Filter meterIRN column"
                    HeaderText="Warehouse" SortExpression="warehouse" UniqueName="warehouse">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="Length" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Length" SortExpression="Length" UniqueName="Length">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" AllowFiltering="false" FilterControlAltText="Filter Description column"
                    HeaderText="Description" SortExpression="Description" UniqueName="Description">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="ODFrac" AllowFiltering="false" FilterControlAltText="Filter ODFrac column"
                    HeaderText="OD Frac" SortExpression="ODFrac" UniqueName="ODFrac">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IDFrac" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="ID Frac" SortExpression="IDFrac" UniqueName="IDFrac">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TopConnection" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Top Connection" SortExpression="TopConnection" UniqueName="TopConnection">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BottomConnection" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Bottom Connection" SortExpression="BottomConnection" UniqueName="BottomConnection">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PinTop" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Pin Top" SortExpression="PinTop" UniqueName="PinTop">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PinBottom" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Pin Bottom" SortExpression="PinBottom" UniqueName="PinBottom">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Comments" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Comments" SortExpression="Comments" UniqueName="Comments">
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
            
        </MasterTableView>
        
        <FilterMenu EnableImageSprites="False">
        </FilterMenu>
    </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>

                <td>
                    <asp:Button ID="printButton" runat="server" Text="Print" OnClientClick="javascript:window.print();" />
                </td>
                <td>
                    <asp:Button ID="btnClose" CssClass="button-CloseRed" ForeColor="Black" runat="server" OnClientClick="Close();" Text="Close"></asp:Button>
                </td>
            </tr>
        </table>
        <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
