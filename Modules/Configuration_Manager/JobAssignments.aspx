<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="JobAssignments.aspx.cs" Inherits="Modules_Configuration_Manager_JobAssignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">

    function openwin() {


        window.radopen(null, "window_service");

    }


    function assetcheck() {
        
        
        document.getElementById('<%=chk_kit.ClientID %>').checked = false;
        var combo = $find("<%= radcombo_Assetkit.ClientID %>");
        //the combo return null
        combo.set_enabled(false);
        var combo = $find("<%= combo_assetcategory.ClientID %>");
        //the combo return null
        combo.set_enabled(true);
        var combo = $find("<%= combo_assets.ClientID %>");
        //the combo return null
        combo.set_enabled(true);

    }

    function kitcheck() {
        document.getElementById('<%=chk_asset.ClientID %>').checked = false;
        var combo = $find("<%= radcombo_Assetkit.ClientID %>");
        //the combo return null
        combo.set_enabled(true);
        var combo = $find("<%= combo_assetcategory.ClientID %>");
        //the combo return null
        combo.set_enabled(false);
        var combo = $find("<%= combo_assets.ClientID %>");
        //the combo return null
        combo.set_enabled(false);
    }
   
     
</script>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function OnClientShow(sender, args) {
                var btn = sender.getManualCloseButton();
                btn.style.left = "0px";
            }
        </script>
    </telerik:RadCodeBlock>
<telerik:RadNotification ID="radnotMessage" runat="server" Text="Initial text" Position="BottomRight"
                AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
                EnableRoundedCorners="true">
            </telerik:RadNotification>
<asp:UpdatePanel runat="server" ID="updPnl1"  UpdateMode="Always">
        
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
    <td align="center"><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
</tr>
    <tr>
     <td align="center">
        <table>
            <tr>
                <td align="left">
                    <table>
                        <tr>
                             <td align="left">Select Job<br />

                                   <telerik:RadComboBox runat="server" ID="combo_job" width="220px" DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid" ></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                     SelectCommand="select 0 as [jid],'Select JobName' as Jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where jid not in (select jobid from PrismJobAssignedAssets) and jid not in (select jobid from PrismJobAssignedPersonals) and enddate >= getDate() and startdate <= getDate() and jobordercreatedid<>'' and status<>'Closed'"></asp:SqlDataSource>
                                </td>
                   <td valign="bottom">
                                    <asp:ImageButton ID="ImageButton2" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                    <telerik:RadToolTip ID="RadToolTip2" runat="server" Position="MiddleRight" RelativeTo="Element"
                                        TargetControlID="ImageButton2" Width="200px" HideEvent="ManualClose"
                                        OnClientShow="OnClientShow">
                                        Shows all the Available Jobs
                                    </telerik:RadToolTip>
                                </td>
               
                             <td align="left">Select Personnel<br />
                             
                               <telerik:RadComboBox runat="server" ID="combo_personal" CheckBoxes="true" DataSourceID="SqlGetPersonals" DataTextField="Personname"
                                DataValueField="userid" EnableCheckAllItemsCheckBox="true" Width="250px" EmptyMessage="Select Personnel" ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetPersonals" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                             SelectCommand=" select userid, (firstname+' '+lastname)+' ('+userRole+')' as Personname from users u,UserRoles ur
                                  where u.userRoleID in (select UserRoleId from Prism_ProjectPersonnels) and status='Active' and ur.userRoleID=u.userRoleID 
                                  and userid not in (select UserId from PrismJobAssignedPersonals,manageJobOrders where jid=PrismJobAssignedPersonals.JobId and status='Approved')"></asp:SqlDataSource>
                                                </td>
                <td valign="bottom">
                                    <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                    <telerik:RadToolTip ID="RadToolTip1" runat="server" Position="MiddleRight" RelativeTo="Element"
                                        TargetControlID="ImageButton1" Width="200px" HideEvent="ManualClose"
                                        OnClientShow="OnClientShow">
                                       Shows list of Personnel not assigned to any Job
                                    </telerik:RadToolTip>
                                </td>
                            <td align="left">Select Service<br />
                                <%--<asp:LinkButton ID="lnk_Service" runat="server" Text="Create New" OnClientClick="openwin();return false" /> <br />                                --%>
                    <telerik:RadComboBox ID="combo_service" runat="server" CheckBoxes="true" 
                    EnableCheckAllItemsCheckBox="true" DataSourceID="SqlGetService" DataTextField="ServiceName"  
                    EmptyMessage="Select Service(s)" Width="290px"
                                                        DataValueField="ID" SelectedText="- Select -"></telerik:RadComboBox>                                
                                <asp:SqlDataSource ID="SqlGetService"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                   SelectCommand=" select [ID], ServiceName+' ($'+CONVERT(varchar, CAST(Cost AS money), 1)+')'as [ServiceName] from PrismService "></asp:SqlDataSource>
                                        
                            </td>
                            <td align="left">Select Consumables<br />
                               <telerik:RadComboBox runat="server" ID="radcosumable" CheckBoxes="true" DataSourceID="SqlGetConsumables" DataTextField="ConName"
                                DataValueField="ConID" EnableCheckAllItemsCheckBox="true" Width="250px" EmptyMessage="Select Consumable" ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetConsumables"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select ConID,ConName+' ($'+CONVERT(varchar, CAST(ConCost AS money), 1)+')'as ConName from  Consumables order by ConID desc" >
                               </asp:SqlDataSource>
                                <%--SELECT 0 AS ConCatID, 'Select Consumable Category' AS ConCatName union--%>
                                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="border:solid 1px #000000">
                        <tr>
                            <td  style="display:none">
                                <br />
                                <asp:CheckBox ID="chk_asset" OnClick="javascript:return assetcheck();" runat="server" />
                            </td>
                            <td id="td_asset">
                                <table>
                                    <tr>
                                        <td  align="left">Select Asset Category<br />
                                            <telerik:RadComboBox runat="server" ID="combo_assetcategory" AutoPostBack="true" CheckBoxes="true" DataSourceID="SqlGetAssetcategory" Width="200px"
                                            EmptyMessage="Please Select Asset Category(s)"
                                            DataTextField="clientAssetName" DataValueField="clientAssetID" EnableCheckAllItemsCheckBox="true" OnSelectedIndexChanged="combo_assetcategory_SelectedIndexChanged"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetAssetcategory" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                            SelectCommand="select clientAssetName ,clientAssetID from clientAssets where active='True' order by clientAssetID"></asp:SqlDataSource>
                                        </td>
              
                                        <td  align="left">Select Asset(s)<br />
                                            <telerik:RadComboBox runat="server" ID="combo_assets" CheckBoxes="true"  Width="250px" EmptyMessage="Please Select Asset(s)" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                            SelectCommand="select  P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA where  P.AssetName=PA.ID and  P.id not in (select AssetId from PrismJobAssignedAssets where AssignmentStatus='Active')  and P.repairstatus<>'Maintenance'  order by P.Id"></asp:SqlDataSource>
                                        </td>
                                        <td valign="middle">
                                        <br />
                                    <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                    <telerik:RadToolTip ID="RadToolTip3" runat="server" Position="MiddleRight" RelativeTo="Element"
                                        TargetControlID="ImageButton3" Width="200px" HideEvent="ManualClose"
                                        OnClientShow="OnClientShow">
                                       Shows list of Assets not assigned to any Job
                                    </telerik:RadToolTip>
                                </td>
                                    </tr>
                                </table>
                            </td>
                            <td > AND </td>
                            <td style="display:none">
                                <br />
                                <asp:CheckBox ID="chk_kit" OnClick="javascript:return kitcheck();" runat="server" />
                            </td>
                                <td  align="left" id="td_kit">Select Asset Kits<br />
                                <telerik:RadComboBox runat="server" ID="radcombo_Assetkit" DataSourceID="SqlDataSource1" CheckBoxes="true"  Width="250px" EmptyMessage="Please Select Asset Kit" DataTextField="kitname" DataValueField="assetkitid" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                SelectCommand="select * from PrismAssetKitDetails"></asp:SqlDataSource>
                            </td>
                            <td>
                            <br />
                                    <asp:ImageButton ID="ImageButton4" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                    <telerik:RadToolTip ID="RadToolTip4" runat="server" Position="MiddleRight" RelativeTo="Element"
                                        TargetControlID="ImageButton4" Width="200px" HideEvent="ManualClose"
                                        OnClientShow="OnClientShow">
                                       Asset Kit is a Template,Shows list of Kits.
                                    </telerik:RadToolTip>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
     </td>
    </tr>
    <tr>
        <td style="background-color:#00628B;width:1px;height:1px"></td>
    </tr>
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td><asp:Button ID="btn_save" runat="server" Text="Assign"  
                            onclick="btn_save_Click"/> </td>
                    <td style="width:5px"></td>
                    <td><asp:Button ID="btn_cancel" runat="server" Text="Cancel" onclick="btn_cancel_Click"></asp:Button> </td>
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
                            <telerik:RadTab runat="server" PageViewID="t2" Text="Personnel" >
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

                        <telerik:RadPageView ID="t2" runat="server">
                             <telerik:RadGrid ID="gridJobPersonals" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
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
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
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
                         NumericType="Currency" HeaderText="Suggested Daily Rate($)" SortExpression="Cost" UniqueName="Cost"  FooterAggregateFormatString="{0:C}">
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
    <tr>
            <td>
                 <telerik:RadWindowManager ID="radwin" runat="server">
            <Windows></Windows>
            </telerik:RadWindowManager>
            </td>
        </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

