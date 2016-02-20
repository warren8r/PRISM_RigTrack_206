<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewWinSurvData.aspx.cs" Inherits="Modules_Configuration_Manager_ViewWinSurvData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function GridCreated(sender, args) {
            var scrollArea = sender.GridDataDiv;
            var dataHeight = sender.get_masterTableView().get_element().clientHeight; if (dataHeight < 350) {
                scrollArea.style.height = dataHeight + 17 + "px";
            }
        }
</script>
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">
                <div class="loader2">Loading...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>Start&#160;Date: <br /><telerik:RadDatePicker ID="date_start" runat="server" AutoPostBack="true" OnSelectedDateChanged="date_start_SelectedDateChanged" DateInput-DisplayDateFormat="MM/dd/yyyy"  Width="185px" ></telerik:RadDatePicker></td>
                 
                                <td>End&#160;Date:<br /><telerik:RadDatePicker ID="date_stop" runat="server" AutoPostBack="true" OnSelectedDateChanged="date_stop_SelectedDateChanged" DateInput-DisplayDateFormat="MM/dd/yyyy" Width="185px"></telerik:RadDatePicker></td> 
                            </tr>
                        </table>
                    </td>


                    <td align="left">
                        Select Job:<br />
                            <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="ID"  EmptyMessage="Select JobName" Width="200px"
                                        ></telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select 0 as ID,'Select JobName' as jobname union select [ID],CurveGroupName as Jobname 
from [RigTrack].[tblCurveGroup] where isActive!='0'"></asp:SqlDataSource>
                    </td> 
                                          
                
                    <td align="left">
                        <br />
                        <asp:Button ID="btn_view" runat="server" OnClick="btn_view_OnClick"  Text="View"  />
                    </td> 
                </tr>
            </table>
        </td>
    </tr>
        <tr>
            <td align="center" id="info" runat="server" visible="false">
                <table>
                    <tr><td><b>Job/Curve Group Info</b></td></tr>
                    <tr>
                       
                        <td>
                            <telerik:RadGrid ID="RadGrid1"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
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
                    </tr>
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
                    <%--<tr><td><b>tblJOBDATEitems Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid4"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource4" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource4">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource4" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblJOBDATEitems where [Job ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>TblBHAINFO Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid5"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource5" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource5">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource5" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from TblBHAINFO where [job no id]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblbhaitems Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid6"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource6" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource6">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource6" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblbhaitems where [Job no ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblbhaHydinputs Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid7"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource7" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource7">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource7" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblbhaHydinputs where [Job Id]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblBHAMWDRuns Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid8"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource8" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource8">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource8" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblBHAMWDRuns where [job no id]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblJobCasing Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid9"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource9" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource9">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource9" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblJobCasing where [Job ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblJOBcost Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid10"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource10" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource10">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource10" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblJOBcost where [JOB ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblJobsurveys Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid11"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource11" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource11">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource11" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblJobsurveys where [JobID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblJobSurvinfo Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid12"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource12" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource12">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource12" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblJobSurvinfo where [JobID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tbljobunits Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid13"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource13" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource13">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource13" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tbljobunits where [JOB ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblReturned Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid14"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource14" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource14">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource14" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblReturned where [JOB ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblserreturnjob Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid15"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource15" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="100px"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource15">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource15" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblserreturnjob where [JOB  ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
</table>
        </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

