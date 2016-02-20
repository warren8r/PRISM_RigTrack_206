<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageAssetStatus.aspx.cs" Inherits="Modules_Configuration_Manager_JobAssignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function CheckAll(id) {
        alert(id);
        var masterTable = $find('ctl00_ContentPlaceHolder1_grdJobList').get_masterTableView();
        var row = masterTable.get_dataItems();
        if (id.checked == true) {
            for (var i = 0; i < row.length; i++) {
                masterTable.get_dataItems()[i].set_selected(true);
            }
        }
        else {
            for (var i = 0; i < row.length; i++) {
                masterTable.get_dataItems()[i].set_selected(false);
            }
        }
    }
</script>
<telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="center"><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td align="left" valign="bottom">Select Job<br />
                           <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid" Width="200px" ></telerik:RadComboBox>
                        <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                         SelectCommand="select 0 as jid,'Select JobName' as jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname 
                         from manageJobOrders where  status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) and jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''"></asp:SqlDataSource>
                    </td>
                    <td align="left"  valign="bottom">Job&#160;Assigned&#160;Start&#160;Date<br />
                        <telerik:RadDatePicker ID="date_start" runat="server">                                               
                            <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" runat="server"> </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td align="left"  valign="bottom">Job&#160;Assigned&#160;End&#160;Date<br />
                        <telerik:RadDatePicker ID="date_end" runat="server">
                            <DateInput ID="DateInput2" DateFormat="MM/dd/yyyy"  runat="server"> </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td  align="left" valign="bottom">
                        <telerik:RadButton ID="btn_view" Text="View" runat="server" 
                            onclick="btn_view_Click"></telerik:RadButton>
                    </td>
                    <td>
                            <br />
                            <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                        </td>
                </tr>
            </table>
        </td>
    </tr>   
   
    <tr>
        <td align="center">
              <telerik:RadGrid ID="grdJobList" runat="server" CellSpacing="0" 
                 AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  OnSortCommand="grdJobList_SortCommand"
                 OnPageIndexChanged="grdJobList_PageIndexChanged" GridLines="None"    OnItemCommand="grdJobList_ItemCommand"
                  OnItemDataBound="grdJobList_ItemDataBound">
                <ExportSettings HideStructureColumns="true">
        </ExportSettings>
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>

                <MasterTableView DataKeyNames="jid" CommandItemStyle-HorizontalAlign="Left" CommandItemDisplay="Top"  EditMode="InPlace">
                   
                   <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
            <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"  
                ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn DataField="jid" DataType="System.Int32" FilterControlAltText="Filter jid column"
                            ReadOnly="True" SortExpression="jid" UniqueName="IntervalMeterDataId" Display="false">
                        </telerik:GridBoundColumn>     
                        <telerik:GridTemplateColumn   HeaderText="Job Name"   FilterControlAltText="Filter jobname column"
                            SortExpression="jobname" UniqueName="jobname">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("jobname") %>'/>
                                <asp:Label ID="lbl_mainjid" runat="server" Text='<%# Eval("jid") %>' Visible="false"/>
                            </ItemTemplate>
                             
                            </telerik:GridTemplateColumn>  
                            <telerik:GridTemplateColumn   HeaderText="Job ID"   FilterControlAltText="Filter jobid column"
                            SortExpression="jobid" UniqueName="jobid">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobid" runat="server" Text= '<%# Eval("jobid") %>'/>
                                <%--'<%# Eval("TimeStamp") %>'/>--%>
                            </ItemTemplate>
                             
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn   HeaderText="Job Type"   FilterControlAltText="Filter jobtype column"
                            SortExpression="jobtype" UniqueName="jobtype">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_jobtype" runat="server" Text='<%# Eval("jobtype") %>'/>
                                </ItemTemplate>                                
                            </telerik:GridTemplateColumn> 
                            <telerik:GridTemplateColumn   HeaderText="Suggested Cost($)"   FilterControlAltText="Filter cost column"
                            SortExpression="cost" UniqueName="cost">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_cost" runat="server" Text='<%# Eval("cost") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>  
                             <telerik:GridTemplateColumn   HeaderText="Operation Manager"   FilterControlAltText="Filter operationsManager column"
                            SortExpression="operationsManager" UniqueName="operationsManager">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_operationsManager" runat="server" Text='<%# Eval("operationsManager") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn> 
                             <telerik:GridTemplateColumn   HeaderText="Project Manager"   FilterControlAltText="Filter projectManager column"
                            SortExpression="projectManager" UniqueName="projectManager">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_projectManager" runat="server" Text='<%# Eval("projectManager") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>                           
                              <%--<telerik:GridTemplateColumn   HeaderText="Start Date"   FilterControlAltText="Filter startdate column"
                            SortExpression="startdate" UniqueName="startdate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_startdate" runat="server" Text='<%# Eval("startdate") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn DataField="startdate" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="Start Date" SortExpression="startdate" UniqueName="startdate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="enddate" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="End Date" SortExpression="enddate" UniqueName="enddate">
                            </telerik:GridBoundColumn>
                             <%--<telerik:GridTemplateColumn   HeaderText="End Date"   FilterControlAltText="Filter enddate column"
                            SortExpression="enddate" UniqueName="enddate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_enddate" runat="server" Text='<%# Eval("enddate") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>  --%>   
                            <telerik:GridBoundColumn DataField="JobAssignedDate" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="Job Assigned Date"   FilterControlAltText="Filter JobAssignedDate column"
                            SortExpression="JobAssignedDate" UniqueName="JobAssignedDate">
                            </telerik:GridBoundColumn>   
                            <%--<telerik:GridTemplateColumn   HeaderText="Job Assigned Date"   FilterControlAltText="Filter JobAssignedDate column"
                            SortExpression="JobAssignedDate" UniqueName="JobAssignedDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_JobAssignedDate" runat="server" Text='<%# Eval("JobAssignedDate") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>  --%>
                            <telerik:GridBoundColumn DataField="JoborderDate" DataFormatString="{0:MM/dd/yyyy}"
                                 HeaderText="Job Order Date"   FilterControlAltText="Filter JoborderDate column"
                            SortExpression="JoborderDate" UniqueName="JoborderDate">
                            </telerik:GridBoundColumn> 
                             <%--<telerik:GridTemplateColumn   HeaderText="Job Order Date"   FilterControlAltText="Filter JoborderDate column"
                            SortExpression="JoborderDate" UniqueName="JoborderDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_JoborderDate" runat="server" Text='<%# Eval("JoborderDate") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>--%>
                                           
                        
                    </Columns>
               <NestedViewTemplate>
               <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black"  SelectedIndex="0" 
                        Height="100%"  MultiPageID="RadMultiPage1">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="t1"  Text=" Assets" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t2" Text=" Personnel">
                            </telerik:RadTab>
                             <telerik:RadTab runat="server" PageViewID="t3" Text="Services" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t4" Text="Consumables" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t5" Text="Kits" >
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage ID="RadMultiPage1" RenderSelectedPageOnly="false" 
                        runat="server" SelectedIndex="0" CssClass="multiPage">
                        <telerik:RadPageView ID="t1" runat="server" >
                          <telerik:RadGrid ID="gridJobAssets" runat="server" CellSpacing="0"   OnItemCommand="gridJobAssets_ItemCommand"
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10"
                 OnItemDataBound="gridJobAssets_ItemDataBound"  >   
                   
                                    <MasterTableView    Width="100%" EditMode="InPlace" DataKeyNames="AssetAssignid">                   
                                    <Columns>     
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" HeaderText="Edit" >
                                        <ItemStyle Width="50px"></ItemStyle>
                                    </telerik:GridEditCommandColumn> 
                                    <%--<telerik:GridCheckBoxColumn HeaderText="Select">
                                        
                                    </telerik:GridCheckBoxColumn>--%>
                                     <telerik:GridTemplateColumn HeaderText="Select">
                                       <%-- <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server"  onclick="CheckAll(this);" />
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>  
                                            <asp:CheckBox ID="chk_select" runat="server" />
                                            <asp:Label ID="lbl_assetassignedid" runat="server" Visible="false" Text='<%# Eval("AssetAssignid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                   <%-- <telerik:GridDropDownColumn HeaderText="Asset&#160;Status"  ListTextField="StatusText" ListValueField="Id" DataSourceID="SqlGetAssetStatus">
                                        
                                    </telerik:GridDropDownColumn>--%>
                                  
                                    <telerik:GridDropDownColumn Display="true" DataType="System.String" EmptyListItemText="-- Select Status --" 
                                    EmptyListItemValue="-1" EnableEmptyListItem="True" FooterText="AssetStatus" DataSourceID="SqlGetAssetStatus"
                                     ListTextField="StatusText" ListValueField="Id" 
 
                                        UniqueName="StatusText" SortExpression="StatusText" HeaderText="Asset&#160;Status" DataField="StatusId"
 
                                        DropDownControlType="DropDownList">
 
                                    </telerik:GridDropDownColumn>
                                                            
                                        <telerik:GridBoundColumn DataField="AssetId" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                                            ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="False">
                                        </telerik:GridBoundColumn>                                
                                        <telerik:GridTemplateColumn HeaderText="AssetName">
                                            <ItemTemplate>                              
                                            <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                                            <asp:Label ID="lbl_assetid" runat="server" Text='<%# Eval("Assetuniqueid") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_jid" runat="server" Text='<%# Eval("jid") %>' Visible="false"></asp:Label>
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
                                              
                                       <%-- <telerik:GridTemplateColumn   HeaderText="Modified&#160;BY"   FilterControlAltText="Filter ModifiedBY column"
                                    SortExpression="username" UniqueName="username">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("username") %>'/>
                                    </ItemTemplate>                             
                                    </telerik:GridTemplateColumn>--%>
                                    <telerik:GridBoundColumn DataField="ModifiedDate" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="Modified&#160;Date"   FilterControlAltText="Filter ModifiedDate column"
                                    SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridTemplateColumn   HeaderText="Modified&#160;Date"   FilterControlAltText="Filter ModifiedDate column"
                                    SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ModifiedDate" runat="server" Text='<%# Eval("ModifiedDate") %>'/>
                                    </ItemTemplate>                             
                                    </telerik:GridTemplateColumn>--%>
               
                                    </Columns>
                    </MasterTableView>     
                        </telerik:RadGrid>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="radcombo_type" runat="server" DataSourceID="SqlGetAssetStatus"
                                     DataTextField="StatusText" DataValueField="Id" EmptyMessage="- Select -">
                                        
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_save" runat="server" OnClick="btn_save_OnClick" Text="Save" />
                                </td>
                            </tr>
                        </table>
                         </telerik:RadPageView>

                        <telerik:RadPageView ID="t2" runat="server">
                         <telerik:RadGrid ID="gridJobPersonals" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="600px" >      
                                          <MasterTableView    EditMode="InPlace" >                   
                                             <Columns>
                                     
                        <telerik:GridBoundColumn DataField="Id" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                            ReadOnly="True" SortExpression="Id" UniqueName="Id" Display="False">
                        </telerik:GridBoundColumn>  
                              
                        <telerik:GridTemplateColumn HeaderText="User&#160;Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Username" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn>  
                         <telerik:GridTemplateColumn HeaderText="User&#160;Role">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("userRole") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                    </Columns>
                                            </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="t3" runat="server">
                             <telerik:RadGrid ID="gridServices" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="200px" >      
                                          <MasterTableView    EditMode="InPlace" >                   
                                             <Columns>
                                     
                        <telerik:GridBoundColumn DataField="AssignID" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                            ReadOnly="True" SortExpression="AssignID" UniqueName="AssignID" Display="False">
                        </telerik:GridBoundColumn>  
                              
                        <telerik:GridTemplateColumn HeaderText="Service&#160;Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_ServiceName" runat="server" Text='<%# Eval("ServiceName") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                          <telerik:GridTemplateColumn HeaderText="Service&#160;Description">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_ServiceDescription" runat="server" Text='<%# Eval("ServiceDescription") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn>  
                        <telerik:GridNumericColumn dataFormatString="{0:$###,##0.00}" DataField="Cost" DataType="System.Decimal"
                         NumericType="Currency" HeaderText="Suggested&#160;Daily&#160;Rate($)" SortExpression="Cost" UniqueName="Cost"  FooterAggregateFormatString="{0:C}">
 </telerik:GridNumericColumn> 
                         
                    </Columns>
                                            </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="t4" runat="server">
                             <telerik:RadGrid ID="RadGrid_con" runat="server" CellSpacing="0" 
                                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
                            <MasterTableView     >                   
                                <Columns>
                                     
                        <telerik:GridBoundColumn DataField="jobconsumableid" DataType="System.Int32" FilterControlAltText="Filter jobconsumableid column"
                            ReadOnly="True" SortExpression="jobconsumableid" UniqueName="jobconsumableid" Display="False">
                        </telerik:GridBoundColumn>  
                              
                        
                        <telerik:GridTemplateColumn HeaderText="Consumable Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("ConName") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                        <telerik:GridTemplateColumn HeaderText="Qty">
                            <ItemTemplate >                              
                            <asp:Label ID="lbl_Username" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
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
                                    <telerik:GridTemplateColumn HeaderText="Select">
                                       <%-- <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server"  onclick="CheckAll(this);" />
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>  
                                            <asp:CheckBox ID="chk_select_kit" runat="server" />
                                            <asp:Label ID="lbl_jobkitid" runat="server" Visible="false" Text='<%# Eval("jobkitid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                        
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
                            
                                           <telerik:RadGrid ID="radgridkitassets" runat="server" ShowHeader="false" AutoGenerateColumns="false">
                                               <MasterTableView>                   
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="AssetName">
                                                       <ItemTemplate >                              
                            
                                                           <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("assetdet") %>'></asp:Label>
                                                           <asp:Label ID="lbl_assetid" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                           <asp:Label ID="lbl_jobassetid" runat="server" Text='<%# Eval("jobassetid") %>' Visible="false"></asp:Label>
                                                           
                                                       </ItemTemplate>
                           
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                </MasterTableView>
                                           </telerik:RadGrid>
                                       </ItemTemplate>
                           
                                    </telerik:GridTemplateColumn>
                         
                    </Columns>
                    </MasterTableView>     
                        </telerik:RadGrid>
                            <table>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="radcombo_kitstatus" runat="server" DataSourceID="SqlGetAssetStatus"
                                     DataTextField="StatusText" DataValueField="Id" EmptyMessage="- Select -">
                                        
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_kitstatus" runat="server" OnClick="btn_kitstatus_OnClick" Text="Save" />
                                </td>
                            </tr>
                        </table>
                        </telerik:RadPageView>
                         </telerik:RadMultiPage>
                            
                    </NestedViewTemplate>  
                 </MasterTableView>                 
            </telerik:RadGrid>
             <asp:HiddenField ID="hidd_jobid" runat="server" />
              <asp:SqlDataSource ID="SqlGetAssetStatus" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                    SelectCommand="select Id,StatusText from PrsimJobAssetStatus"></asp:SqlDataSource>
        </td>
    </tr>
</table>
</telerik:RadAjaxPanel>
</asp:Content>

