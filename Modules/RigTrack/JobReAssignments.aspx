<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="JobReAssignments.aspx.cs" Inherits="Modules_Configuration_Manager_JobAssignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function OnClientShow(sender, args) {
                var btn = sender.getManualCloseButton();
                btn.style.left = "0px";
            }
        </script>
    </telerik:RadCodeBlock>
<script type="text/javascript">

    function openwin() {


        window.radopen(null, "window_service");

    }

    function confirmnotinuse(sender, args) {
        function callBackFunction(arg) {
            if (arg == true) {
                $find("<%=btn_notinuse.ClientID %>").click();
            }
        }
        radconfirm("Are you sure you want to change the Status of selected Tool to \n'Not In Use'", callBackFunction, 300, 160, null, "Confirmation Box");
        args.set_cancel(true);
    }

    function confirmmovetowarehouse(sender, args) {
        function callBackFunction(arg) {
            if (arg == true) {
                $find("<%=radbutton_movetowarehouse.ClientID %>").click();
            }
        }
        radconfirm("Are you sure you want to change the Status of selected Tool to \n'WareHouse'", callBackFunction, 300, 160, null, "Confirmation Box");
        args.set_cancel(true);
    }

    function confirmmovetomaintenance(sender, args) {
        function callBackFunction(arg) {
            if (arg == true) {
                $find("<%=btn_movemaintenance.ClientID %>").click();
            }
        }
        radconfirm("Are you sure you want to change the Status of selected Tools to \n'Maintenance'", callBackFunction, 300, 160, null, "Confirmation Box");
        args.set_cancel(true);
    }

    function confirmreassign(sender, args) {
        function callBackFunction(arg) {
            if (arg == true) {
                $find("<%=btn_reassign.ClientID %>").click();
            }
        }
        var comboexistJob = $find('<%=combo_job.ClientID %>');
        var jobval = comboexistJob.get_text();

        var combonewJob = $find('<%=combo_newjob.ClientID %>');
        var jobvalnew = combonewJob.get_text();
        
        var message="Are you sure you want to Re-Assign selected Tools from '" + jobval +"' Job to the '" + jobvalnew + "' Job?"; 
        radconfirm(message, callBackFunction, 600, 160, null, "Confirmation Box");
        args.set_cancel(true);
    }

//    function ss() {

//        var r = confirm("Are you sure you want to move to Warehouse?");
//        if (r == true) {
//            return true;
//        }
//        else {
//            return false;
//        }
//    }
//    function confirmnotinuse() {
//        var r = confirm("Are you sure you want to change the Status of selected Assets/Personels to \n'Not In Use'");
//        if (r == true) {
//            return true;
//        }
//        else {
//            return false;
//        }
//    }
//    function confirmmovetowarehouse() {
//        var r = confirm("Are you sure you want to change the Status of selected Assets to \n'WareHouse'");
//        if (r == true) {
//            return true;
//        }
//        else {
//            return false;
//        }
//    }
//    function confirmmovetomaintenance() {
//        var r = confirm("Are you sure you want to change the Status of selected Assets to \n'Maintenance'");
//        if (r == true) {
//            return true;
//        }
//        else {
//            return false;
//        }
//    }
//    function confirmreassign() {
//        var r = confirm("Are you sure you want to Re-Assign selected Assets/Personnels to the selected new Job");
//        if (r == true) {
//            return true;
//        }
//        else {
//            return false;
//        }
//    }
</script>
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
                    <td style="padding-left:30px">
                        <h2>Re-Assign Tools to Job</h2>
                    </td>
                </tr>
<tr>
    <%--<td><asp:Label ID="lbltest" runat="server" CssClass="breadcrumbs">Configuration Manager » Manage <%= AssetName %></asp:Label></td>--%>
</tr>
<tr>
    <td align="center">
        <table>
            <tr>
                <td><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
                <td>
                    <asp:Button ID="btn_print" runat="server" Text="Download Ticket" Visible="false" OnClick="btn_print_OnClick" />
                                        <asp:HiddenField ID="hid_ticketid" runat="server" />
                </td>
            </tr>
        </table>

    </td>
</tr>
    <tr><td style="height:10px"></td></tr>
 <tr>
     <td style="padding-left:5px">
        <table>
           
            <tr>
                <td>
                    Select Company<br />
                    <telerik:RadDropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="Select All" />
                                </Items>
                            </telerik:RadDropDownList>
                            <%--<asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                </td>
                <td valign="bottom">Select&#160;Existing&#160;Job<br />
                           <telerik:RadComboBox runat="server" ID="combo_job"   DataTextField="jobname" DataValueField="ID"  EmptyMessage="Select JobName" Width="250px"
                           AutoPostBack="true" OnSelectedIndexChanged="combo_job_SelectedIndexChanged" ></telerik:RadComboBox>
                        <%--<asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                         SelectCommand="select [ID],CurveGroupName + ' ('+JobNumber+')' as Jobname from [RigTrack].tblCurveGroup where isActive!='0' and 
                         (ID in (select jobid from PrismJobAssignedAssets) or ID in (select jobid from PrismJobAssignedPersonals)) and CreateDate<>''"></asp:SqlDataSource>--%>
                    </td>
                    <td valign="bottom">
                                    <asp:ImageButton ID="ImageButton2" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                    <telerik:RadToolTip ID="RadToolTip2" runat="server" Position="MiddleRight" RelativeTo="Element"
                                        TargetControlID="ImageButton2" Width="250px" HideEvent="ManualClose"
                                        OnClientShow="OnClientShow">
                                        Select Existing job For Re-assignments
                                    </telerik:RadToolTip>
                                </td>
                    </tr> 
            
        </table>
     </td>
    </tr>
    <tr>
        <td >
            <table width="100%">
                <tr>
                    <td align="left">
                        <b>Select Existing Tools to Re-Assign</b>
                    </td>
                </tr>
                <tr  >
                    <td align="left" style="border:solid 1px #000000">
                            
                            <table cellpadding="0" cellspacing="5">
                                <tr>
                   
                                    <td  align="left">Select&#160;Existing&#160;Tools<br />
                                        <telerik:RadComboBox runat="server" ID="combo_ex_assets" CheckBoxes="true" Width="300px" EmptyMessage="Please Select Tool" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource2" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                                SelectCommand="select  P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA
                                                where  P.AssetName=PA.ID and  P.id not in (select AssetId from PrismJobAssignedAssets) order by AssetName ASC"></asp:SqlDataSource>
                                    </td>
                                     <td align="left" style="display:none">Select&#160;Existing&#160;Personnel<br />
                                       <telerik:RadComboBox runat="server" ID="combo_ex_personal" CheckBoxes="true" DataTextField="Personname"
                                                DataValueField="userid" EnableCheckAllItemsCheckBox="true" Width="300px"  EmptyMessage="Please Select Personnel" ></telerik:RadComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource3" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                            SelectCommand=" select userid, (firstname+lastname)+' ('+userRole+')' as Personname from users u,UserRoles ur
                                            where u.userRoleID in (select UserRoleId from Prism_ProjectPersonnels) and status='Active' and ur.userRoleID=u.userRoleID"></asp:SqlDataSource>
                                    </td>
                                    <td align="left" style="display:none">
                                        Select Service <br />                                
                                        <telerik:RadComboBox ID="combo_ex_service" runat="server" CheckBoxes="true" Width="300px" EnableCheckAllItemsCheckBox="true" DataSourceID="SqlGetService" DataTextField="ServiceName"  EmptyMessage="Please Select Service(s)" 
                                                                                             DataValueField="ID" SelectedText="- Select -"></telerik:RadComboBox>                                
                                        <asp:SqlDataSource ID="SqlDataSource4"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                           SelectCommand=" select [ID], ServiceName+' ($'+CONVERT(varchar, CAST(Cost AS money), 1)+')'as [ServiceName] from PrismService "></asp:SqlDataSource>
                          
                                    </td>
                                    <td align="left">Select&#160;New&#160;Job&#160;To&#160;Assign<br />
                                               <telerik:RadComboBox runat="server" ID="combo_newjob" Width="300px"  DataTextField="CurveGroupName" DataValueField="ID"  EmptyMessage="Select Jobname" ></telerik:RadComboBox>
                                        </td>
                                        <td align="left"><br /><telerik:RadButton  ID="btn_reassign" runat="server" Text="ReAssign" 
                                                onclick="btn_reassign_Click" OnClientClicking="confirmreassign" /> </td>
                                                 
                                                            <td align="left"><br /><asp:Button ID="btn_ex_cear" runat="server" 
                                                            Text="Clear" OnClick="btn_ex_cear_Click"/> </td>
                                </tr>
                                <tr>
                                     <%--<telerik:RadButton ID="btn_savefinalize" runat="server" Text="Maintenance Finished" OnClientClicking="Clicking"
                                           OnClick="btn_savefinalize_OnClick">
                                        </telerik:RadButton>--%>
                                    <td align="right" colspan="4">
                                        <table  border="0">
                                            <tr>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        
                    </td>
                </tr>
                <tr><td style="height:10px"></td></tr>
                
                <tr  >
                    <td>
                        <table>
                            
                            <tr>
                                <td align="left" >
                            
                            <table cellpadding="0" cellspacing="5">
                                <tr>
                                    <td align="left" colspan="2">
                                        <b>Select Existing Tools to Change Status</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border:solid 1px #000000">
                                        <table>
                                             <tr>
                   
                                    <td  align="left">Select&#160;Existing&#160;Tools<br />
                                        <telerik:RadComboBox runat="server" ID="comboToolstoWH" CheckBoxes="true" Width="300px" EmptyMessage="Please Select Tool" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                                SelectCommand="select  P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA
                                                where  P.AssetName=PA.ID and  P.id not in (select AssetId from PrismJobAssignedAssets) order by AssetName ASC"></asp:SqlDataSource>
                                    </td>
                                     
                                    
                                       
                                                 <td align="left"><br /><telerik:RadButton ID="btn_notinuse" runat="server" Visible="false" Text="Not In Use" 
                                                            OnClick="btn_notinuse_Click" OnClientClicking="confirmnotinuse"/> </td>
                                                    <td align="left"><br /><telerik:RadButton  ID="radbutton_movetowarehouse" runat="server" Text="Move to warehouse"
                                                     OnClientClicking="confirmmovetowarehouse" OnClick="radbutton_movetowarehouse_Click"/> </td>
                                                <td align="left"><br /><telerik:RadButton  ID="btn_movemaintenance" runat="server" Text="Move to maintenance"
                                                 OnClientClicking="confirmmovetomaintenance" OnClick="btn_movemaintenance_OnClick" /> </td>
                                </tr>
                                <tr>
                                     <%--<telerik:RadButton ID="btn_savefinalize" runat="server" Text="Maintenance Finished" OnClientClicking="Clicking"
                                           OnClick="btn_savefinalize_OnClick">
                                        </telerik:RadButton>--%>
                                    <td align="right" colspan="4">
                                        <table  border="0">
                                            <tr>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                        </table>
                                    </td>
                                </tr>
                               
                            </table>
                        
                    </td>
                                <td>
                        <table>
                            <tr>
                    <td><b>Add additional tools to selected Job or Tool</b></td>
                </tr>
                <tr>
                    <td align="left" style="border:solid 1px #000000">
                        
                            <table>
                                     <tr>
                   
                <td  align="left">Select&#160;Additional&#160;Tools<br />
                   <telerik:RadComboBox runat="server" ID="combo_assets" Width="300px" CheckBoxes="true" DataSourceID="SqlGetAssets" EmptyMessage="Please Select Tool(s)" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand="select P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA
  where  P.AssetName=PA.ID and  P.id not in (select AssetId from PrismJobAssignedAssets,[RigTrack].tblCurveGroup J where 
  j.ID=PrismJobAssignedAssets.JobId and j.isActive=1  and PrismJobAssignedAssets.AssignmentStatus='Active')"></asp:SqlDataSource>
                </td>
                 <td align="left" style="display:none">Select&#160;Additional&#160;Personnel<br />
                   <telerik:RadComboBox runat="server" ID="combo_personal" Width="300px" CheckBoxes="true" DataSourceID="SqlGetPersonals" DataTextField="Personname"
                    DataValueField="userid" EnableCheckAllItemsCheckBox="true" EmptyMessage="Please Select Personnel" ></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetPersonals" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                 SelectCommand=" select userid, (firstname+lastname)+' ('+userRole+')' as Personname from users u,UserRoles ur 
  where u.userRoleID in (select UserRoleId from Prism_ProjectPersonnels) and status='Active' and ur.userRoleID=u.userRoleID 
  and userid not in (select UserId from PrismJobAssignedPersonals,manageJobOrders where jid=PrismJobAssignedPersonals.JobId and status='Approved')"></asp:SqlDataSource>
                </td>
                <td style="display:none">
                    Select Service <br />                                
        <telerik:RadComboBox ID="combo_service" runat="server" CheckBoxes="true" Width="300px" EnableCheckAllItemsCheckBox="true" DataSourceID="SqlGetService" DataTextField="ServiceName"  EmptyMessage="Please Select Service(s)" 
                                                                                             DataValueField="ID" SelectedText="- Select -"></telerik:RadComboBox>                                
                    <asp:SqlDataSource ID="SqlGetService"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                       SelectCommand=" select [ID], ServiceName+' ($'+CONVERT(varchar, CAST(Cost AS money), 1)+')'as [ServiceName] from PrismService "></asp:SqlDataSource>
                        <%--<telerik:RadWindowManager ID="RadWindowManager1" runat="server"  > 
                                     <Windows> 
                                     <telerik:RadWindow ID="window_service" runat="server"  Modal="true" Width="960px"  height="600px" Title="New/Edit Service" >
 
                                        <ContentTemplate>
 
                                           <iframe id="iframe1" runat="server" width="960px" height="600px" src="mngService.aspx">
                                               
                                            </iframe>
 
                                         </ContentTemplate>
 
                                     </telerik:RadWindow>
                              </Windows>
                 </telerik:RadWindowManager> --%>         
                </td>
                <td align="left" style="display:none">Select Consumables<br />
                        <telerik:RadComboBox runat="server" ID="radcosumable" CheckBoxes="true" DataSourceID="SqlGetConsumables" DataTextField="ConName"
                        DataValueField="ConID" EnableCheckAllItemsCheckBox="true" Width="250px" EmptyMessage="Select Consumable" ></telerik:RadComboBox>
                    <asp:SqlDataSource ID="SqlGetConsumables"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                            SelectCommand="select ConID,ConName+' ($'+CONVERT(varchar, CAST(ConCost AS money), 1)+')'as ConName from  Consumables" >
                        </asp:SqlDataSource>
                        <%--SELECT 0 AS ConCatID, 'Select Consumable Category' AS ConCatName union--%>
                </td>
                 <td valign="bottom"><telerik:RadButton ID="btn_save" runat="server" Text="Assign" 
                            onclick="btn_save_Click"></telerik:RadButton> </td>
                   
                     <td valign="bottom" align="left"><telerik:RadButton ID="btn_cancel" runat="server" Text="Clear" OnClick="btn_cancel_Click"></telerik:RadButton> </td>
            </tr>
                            </table>
                        
                    </td>
                </tr>
                        </table>
                    </td>
                    
                            </tr>
                        </table>
                    </td>
                    
                </tr>
                <tr><td style="height:10px"></td></tr>
                
            </table>
        </td>
    </tr>
<tr><td style="line-height:5px">&nbsp;</td></tr>
    <tr>
        <td>
              <telerik:RadGrid ID="grdJobList" runat="server" CellSpacing="0" Width="100%" 
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
                        <telerik:GridTemplateColumn   HeaderText="Job Name"   FilterControlAltText="Filter jobname column"
                            SortExpression="CurveGroupName" UniqueName="CurveGroupName">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("JOB") %>'/>
                            </ItemTemplate>
                             
                            </telerik:GridTemplateColumn>  
                            <telerik:GridTemplateColumn   HeaderText="Job Number"   FilterControlAltText="Filter jobid column"
                            SortExpression="JobNumber" UniqueName="JobNumber">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobid" runat="server" Text= '<%# Eval("JobNumber") %>'/>
                                <%--'<%# Eval("TimeStamp") %>'/>--%>
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
                             <%--<telerik:GridTemplateColumn   HeaderText="Job Type"   FilterControlAltText="Filter jobtype column"
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
                            </telerik:GridTemplateColumn>--%>                           
                              <telerik:GridTemplateColumn   HeaderText="Start Date"   FilterControlAltText="Filter startdate column"
                            SortExpression="JobStartDate" UniqueName="JobStartDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_startdate" runat="server" Text='<%#DateTime.Parse(Eval("JobStartDate").ToString()).ToString("MM/dd/yyyy")%>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn   HeaderText="End Date"   FilterControlAltText="Filter enddate column"
                            SortExpression="JobEndDate" UniqueName="JobEndDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_enddate" runat="server" Text='<%#(String.IsNullOrEmpty(Eval("JobEndDate").ToString()) ? "" : DateTime.Parse(Eval("JobEndDate").ToString()).ToString("MM/dd/yyyy"))%>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>
                                
                            <%--<telerik:GridTemplateColumn   HeaderText="Job Assigned Date"   FilterControlAltText="Filter JobAssignedDate column"
                            SortExpression="JobAssignedDate" UniqueName="JobAssignedDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_JobAssignedDate" runat="server" Text='<%#DateTime.Parse(Eval("JobAssignedDate").ToString()).ToString("MM/dd/yyyy")%>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>  
                            
                             <telerik:GridTemplateColumn   HeaderText="Job Order Date"   FilterControlAltText="Filter JoborderDate column"
                            SortExpression="JoborderDate" UniqueName="JoborderDate">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_JoborderDate" runat="server" Text='<%#DateTime.Parse(Eval("JoborderDate").ToString()).ToString("MM/dd/yyyy")%>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>--%>
                                           
                        
                    </Columns>
               <NestedViewTemplate>
                       <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black"  SelectedIndex="0" 
                        Height="100%"  MultiPageID="RadMultiPage1">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="t1"  Text="Tools" Selected="True">
                            </telerik:RadTab>
                            <%--<telerik:RadTab runat="server" PageViewID="t2" Text="Personnel" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t3" Text="Services" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t4" Text="Consumables" >
                            </telerik:RadTab>--%>
                            <telerik:RadTab runat="server" PageViewID="t5" Text="Tool Groups" >
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage ID="RadMultiPage1" RenderSelectedPageOnly="false" 
                        runat="server" SelectedIndex="0" CssClass="multiPage">
                        <telerik:RadPageView ID="t1" runat="server" >
                            <telerik:RadGrid ID="gridJobAssets" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" OnItemDataBound="gridJobAssets_ItemDataBound" AllowPaging="True"  PageSize="10">      
                                    <MasterTableView   Width="100%" EditMode="InPlace" >                   
                                    <Columns>
                                     
                <telerik:GridBoundColumn DataField="AssetId" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                    ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="False">
                </telerik:GridBoundColumn>  
                              
                <telerik:GridTemplateColumn HeaderText="ToolName">
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
                    <telerik:GridTemplateColumn   HeaderText="Tool&#160;Owner"   FilterControlAltText="Filter Warehouse column"
                    SortExpression="Warehouse" UniqueName="Warehouse">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Warehouse" runat="server" Text='<%# Eval("Warehouse") %>'/>
                        </ItemTemplate>                                
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Tool&#160;Category"   FilterControlAltText="Filter clientAssetName column"
                    SortExpression="clientAssetName" UniqueName="clientAssetName">
                    <ItemTemplate>
                        <asp:Label ID="lbl_clientAssetName" runat="server" Text='<%# Eval("clientAssetName") %>'/>
                    </ItemTemplate>
                             
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Tool Group&#160;Name"   FilterControlAltText="Filter clientAssetName column"
                     UniqueName="KitName" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbl_KitName" runat="server" Text='<%#Eval("KitName") %>' />
                    </ItemTemplate>
                             
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Tool&#160;Status"   FilterControlAltText="Filter AssetStatus column"
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
                        <asp:Label ID="lbl_ModifiedDate" runat="server" Text='<%#DateTime.Parse(Eval("ModifiedDate").ToString()).ToString("MM/dd/yyyy")%>' />
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn>
            </Columns>
                                    </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>

                        <%--<telerik:RadPageView ID="t2" runat="server">
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
                        </telerik:RadPageView>--%>
                        <telerik:RadPageView ID="t5" runat="server">
                             <telerik:RadGrid ID="RadGrid_kits" OnItemDataBound="RadGrid_kits_ItemDataBound" runat="server" CellSpacing="0" 
                                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
                            <MasterTableView     >                   
                                <Columns>
                                     
                        <telerik:GridBoundColumn DataField="jobkitid" DataType="System.Int32" FilterControlAltText="Filter jobconsumableid column"
                            ReadOnly="True" SortExpression="jobkitid" UniqueName="jobkitid" Display="False">
                        </telerik:GridBoundColumn>  
                              
                        <%--<telerik:GridTemplateColumn HeaderText="Job&#160;Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Username" runat="server" Text='<%# Eval("jobname") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> --%>
                        <telerik:GridTemplateColumn HeaderText="Tool Group Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("kitname") %>'></asp:Label>
                               <asp:Label ID="lbl_kitid" runat="server" Text='<%# Eval("kitid") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lbl_jobid" runat="server" Text='<%# Eval("jobid") %>' Visible="false"></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                         <telerik:GridTemplateColumn HeaderText="Group Status">
                                       <ItemTemplate>                              
                                            <asp:Label ID="lbl_kitstatusid" runat="server" Text='<%# Eval("KitDeliveryStatus") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="lbl_kitstatus" runat="server" ></asp:Label>
                                       </ItemTemplate>
                           
                                    </telerik:GridTemplateColumn> 
                        <telerik:GridTemplateColumn HeaderText="Tools">
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
</table>
 <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_print" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>

