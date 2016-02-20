<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageComponentStatus.aspx.cs" Inherits="Modules_Configuration_Manager_JobAssignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
<script type="text/javascript">

    function openwin() {


        window.radopen(null, "window_service");

    }
   
     
</script>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
    <td align="center"><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
</tr>
    <tr>
     <td align="center">
        <table>
            <tr>
                    <td align="left">Select Job<br />
                       <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" AutoPostBack="true" width="200px"
                        DataTextField="jobname" DataValueField="jid" OnSelectedIndexChanged="combo_job_SelectedIndexChanged" ></telerik:RadComboBox>
                        <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                         SelectCommand="select 0 as [jid],'Select JobName' as Jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where jid  in (select jobid from PrismJobAssignedAssets)"></asp:SqlDataSource>
                    </td>
                <td  align="left">Select Asset(s)<br />
                   <telerik:RadComboBox runat="server" ID="combo_assets" CheckBoxes="false"  AutoPostBack="false"
                     Width="300px" EmptyMessage="Please Select Asset(s)" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="false"></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand="select  P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA
  where  P.AssetName=PA.ID and  P.id not in (select AssetId from PrismJobAssignedAssets)  order by P.Id"></asp:SqlDataSource>
                </td>
                <td style="width:5px"></td>
               <td valign="bottom">   <telerik:RadButton ID="btn_view" Text="View" runat="server" 
                            onclick="btn_view_Click"></telerik:RadButton> </td>
                    <td style="width:5px"></td>
                    <td valign="bottom"><telerik:RadButton ID="btn_cancel" runat="server" Text="Cancel"></telerik:RadButton> </td>
            </tr>
        </table>
     </td>
    </tr>
   <tr><td style="height:10px"></td></tr>
    <tr>
        <td align="left">
            <b>Existing Project Assignments</b><br />
              <telerik:RadGrid ID="grdJobList" runat="server" CellSpacing="0" 
                 AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  OnSortCommand="grdJobList_SortCommand"
                 OnPageIndexChanged="grdJobList_PageIndexChanged" GridLines="None"    OnItemCommand="grdJobList_ItemCommand">
                  <%--OnItemDataBound="grdJobList_ItemDataBound"--%>
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
                            <telerik:GridTemplateColumn   HeaderText="Est. Cost($)"   FilterControlAltText="Filter cost column"
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
                              <telerik:GridTemplateColumn   HeaderText="Start Date"   FilterControlAltText="Filter startdate column"
                            SortExpression="startdate" UniqueName="startdate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_startdate"  runat="server" Text='<%#DateTime.Parse(Eval("startdate").ToString()).ToString("MM/dd/yyyy")%>' />
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn   HeaderText="End Date"   FilterControlAltText="Filter enddate column"
                            SortExpression="enddate" UniqueName="enddate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_enddate" runat="server" Text='<%#DateTime.Parse(Eval("enddate").ToString()).ToString("MM/dd/yyyy")%>' />
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>        
                            <telerik:GridTemplateColumn   HeaderText="Job Assigned Date"   FilterControlAltText="Filter JobAssignedDate column"
                            SortExpression="JobAssignedDate" UniqueName="JobAssignedDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_JobAssignedDate" runat="server" Text='<%#DateTime.Parse(Eval("JobAssignedDate").ToString()).ToString("MM/dd/yyyy")%>' />
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>  
                            
                             <telerik:GridTemplateColumn   HeaderText="Job Order Date"   FilterControlAltText="Filter JoborderDate column"
                            SortExpression="JoborderDate" UniqueName="JoborderDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_JoborderDate" runat="server" Text='<%#DateTime.Parse(Eval("JoborderDate").ToString()).ToString("MM/dd/yyyy")%>' />
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>
                                           
                        
                    </Columns>
               <NestedViewTemplate>
                       <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black"  SelectedIndex="0" 
                        Height="100%"  MultiPageID="RadMultiPage1">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="t1"  Text="Assets" Selected="True">
                            </telerik:RadTab>
                           <%-- <telerik:RadTab runat="server" PageViewID="t2" Text="Components" >
                            </telerik:RadTab>  --%>                          
                        </Tabs>
                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage ID="RadMultiPage1" RenderSelectedPageOnly="false" 
                        runat="server" SelectedIndex="0" CssClass="multiPage">
                        <telerik:RadPageView ID="t1" runat="server"  >
                            <telerik:RadGrid ID="gridJobAssets" runat="server" CellSpacing="0"   OnItemCommand="gridJobAssets_ItemCommand"
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10">      
                                    <MasterTableView   Width="100%" EditMode="InPlace"  DataKeyNames="Assetinfoid,jid">                   
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
                                       <NestedViewTemplate>
                                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black"  SelectedIndex="0" 
                                            Height="100%"  MultiPageID="RadMultiPage1">
                                              <Tabs>
                                                <telerik:RadTab runat="server" PageViewID="t2"  Text="Components" Selected="True">
                                                </telerik:RadTab>
                                                </Tabs>    
                                                </telerik:RadTabStrip>

                    <telerik:RadMultiPage ID="RadMultiPage1" RenderSelectedPageOnly="false" 
                        runat="server" SelectedIndex="0" CssClass="multiPage">
                        <telerik:RadPageView ID="t2" runat="server"  >                                                 
                                                    <telerik:RadGrid ID="gridJobComponents" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" 
                OnItemDataBound="gridJobComponents_ItemDataBound" OnItemCommand="gridJobComponents_ItemCommand" >      
                                          <MasterTableView    EditMode="InPlace" DataKeyNames="CompID" >                   
                                             <Columns>     
                                              <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" HeaderText="Edit" >
                                        <ItemStyle Width="50px"></ItemStyle>
                                    </telerik:GridEditCommandColumn>                                 
                                                    <telerik:GridBoundColumn DataField="CompID" DataType="System.Int32" FilterControlAltText="Filter CompID column"
                            ReadOnly="True" SortExpression="CompID" UniqueName="CompID" Display="False"> </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Select">
                                       <%-- <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server"  onclick="CheckAll(this);" />
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>  
                                            <asp:CheckBox ID="chk_select" runat="server" />
                                            <asp:Label ID="lbl_assetassignedid" runat="server" Visible="false" Text='<%# Eval("AssetId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                              <telerik:GridDropDownColumn Display="true" DataType="System.String" EmptyListItemText="-- Select Status --" 
                                    EmptyListItemValue="-1" EnableEmptyListItem="True" FooterText="Comp.Status" DataSourceID="SqlGetAssetStatus"
                                     ListTextField="StatusText" ListValueField="Id" 
 
                                        UniqueName="StatusText" SortExpression="StatusText" HeaderText="Comp.&#160;Status" DataField="Componentstatus"
 
                                        DropDownControlType="DropDownList">
 
                                    </telerik:GridDropDownColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Component&#160;Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Username" runat="server" Text='<%# Eval("ComponentName") %>'></asp:Label>
                            <%--<asp:Label ID="lbl_compstatus" runat="server" Text='<%# Eval("Componentstatus") %>'></asp:Label>--%>
                             <asp:Label ID="lbl_CompID" runat="server" Text='<%# Eval("CompID") %>' Visible="false"></asp:Label>
                             <asp:Label ID="lbl_assetid" runat="server" Text='<%# Eval("AssetId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_jid" runat="server" Text='<%# Eval("JobId") %>' Visible="false"></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                                                    <telerik:GridTemplateColumn HeaderText="component&#160;Description">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn>
                                     <telerik:GridBoundColumn HeaderText="Warehouse&#160;Name" DataField="Warehousename"  FilterControlAltText="Filter Warehousename column"
                            ReadOnly="True" SortExpression="Warehousename" UniqueName="Warehousename" > </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn HeaderText="Comp.Category&#160;Name" DataField="comp_categoryname"  FilterControlAltText="Filter comp_categoryname column"
                            ReadOnly="True" SortExpression="comp_categoryname" UniqueName="comp_categoryname" > </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="Serialno" HeaderText="Serial&#160;#"  FilterControlAltText="Filter Serialno column"
                            ReadOnly="True" SortExpression="Serialno" UniqueName="Serialno" > </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="componenttype" HeaderText="Comp.&#160;Type" FilterControlAltText="Filter componenttype column"
                            ReadOnly="True" SortExpression="componenttype" UniqueName="componenttype" > </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="Make" HeaderText="Make"  FilterControlAltText="Filter Make column"
                            ReadOnly="True" SortExpression="Make" UniqueName="Make" > </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="compcost" HeaderText="Cost" DataType="System.Decimal" FilterControlAltText="Filter compcost column"
                            ReadOnly="True" SortExpression="compcost" UniqueName="compcost" > </telerik:GridBoundColumn>
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
                                             </telerik:RadMultiPage>
                                       </NestedViewTemplate>
                                      
                                    </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>

                                              
                        
                    </telerik:RadMultiPage>
                    </NestedViewTemplate>  
                 </MasterTableView>                 
            </telerik:RadGrid>
              <asp:SqlDataSource ID="SqlGetAssetStatus" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                    SelectCommand="select Id,StatusText from PrsimJobAssetStatus"></asp:SqlDataSource>
        </td>
    </tr>
</table>
</telerik:RadAjaxPanel>
</asp:Content>
