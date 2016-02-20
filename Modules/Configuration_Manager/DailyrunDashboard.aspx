<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="DailyrunDashboard.aspx.cs" Inherits="Modules_Configuration_Manager_DailyrunDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadScriptBlock ID="radsc" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function OpenFileExplorerDialog(obj1, objeventamiid) {
                var wnd = $find("<%= ManageFiles.ClientID %>");
                
                wnd.setUrl("../../ManageRunReportDocs.aspx?runid=" + obj1 + "");
                wnd.show();

                return false;
            }
        </script>
    </telerik:RadScriptBlock>
    <script type="text/javascript">
        //<![CDATA[
        

        

        //]]>
    </script>
<telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
 <asp:UpdatePanel runat="server" ID="updPnl1"  UpdateMode="Always">
            <ContentTemplate>  
              <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="grid_run">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="grid_run"></telerik:AjaxUpdatedControl>
                            
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr>
                <td align="center" style="padding-left:20px">
                    <table>
                        <tr>
                            <td>
                                <telerik:RadWindow runat="server" OnBiffExporting="RadGrid1_BiffExporting" Width="700px"
                                    Height="560px" VisibleStatusbar="false" ShowContentDuringLoad="false" NavigateUrl="~/ManageFiles.aspx"
                                    ID="ManageFiles" Modal="true" Behaviors="Close,Move">
                                </telerik:RadWindow>
                            </td>
                        </tr>
                        <tr>
                             <td align="left" >
                                Select Job:<br />
                                <telerik:RadComboBox ID="radcombo_job" runat="server"  
                                     DataSourceID="SqlGetJobname" Width="300px" 
                                                        EmptyMessage="Select Job Name" DataTextField="jobname"  CheckBoxes="true"
                                                            DataValueField="jid" 
                                     EnableCheckAllItemsCheckBox="true" AutoPostBack="True" 
                                     onselectedindexchanged="radcombo_job_SelectedIndexChanged"  ></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetJobname" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand="select * from manageJobOrders where (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''"></asp:SqlDataSource>
                                                 
                                
                            </td>
                            <td align="left"  >
                                Select Run:<br />
                                <telerik:RadComboBox ID="combo_run" EmptyMessage="Select Run" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" DataValueField="runnumber" DataTextField="runnumber" runat="server"  ></telerik:RadComboBox>
                            </td>
                            <td valign="bottom">
                                <asp:Button ID="btn_view" runat="server"  Text="View" onclick="btn_view_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
           </tr>
           <tr><td style="line-height:20px"></td></tr>
           <tr>
            <td>
                <div id="ZoneID1">
                    <telerik:RadGrid ID="grid_run" runat="server" CellSpacing="0" GridLines="None"  ShowGroupPanel="True" 
                    AutoGenerateColumns="False" AllowPaging="True"  OnItemCommand="grid_run_ItemCommand"
                            AllowSorting="True" HorizontalAlign="Center"  OnItemDataBound="grid_run_ItemDataBound">
                  <%--  <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>--%>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="runid">
                        <PagerStyle PageSizeControlType="RadComboBox" />
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                      
<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                      
                         <Columns> 
                      <telerik:GridTemplateColumn HeaderText="MWD Coordinator/ Proj Mgr">
                        <ItemTemplate>
                            <asp:Label ID="lbl_manager" runat="server" Text='<%# Eval("Operationmanager") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                      <telerik:GridTemplateColumn HeaderText="Client">
                        <ItemTemplate>
                            <asp:Label ID="lbl_client" runat="server" Text='<%# Eval("ClientName") %>' ></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn>  
                      <telerik:GridTemplateColumn HeaderText="Job Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_job" runat="server" Text='<%# Eval("jobname") %>' ></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn>  
                       
                       <telerik:GridTemplateColumn HeaderText="Job Number" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lbl_jobnumber" runat="server" Text='<%# Eval("jobid") %>' ></asp:Label>
                            <asp:Label ID="lbl_jid" runat="server" Text='<%# Eval("jid") %>' ></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Rig" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="lbl_rig" runat="server" Text='<%# Eval("rigtypename") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Run#" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_runno" runat="server" Text='<%# Eval("runnumber") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Date" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Date" runat="server" Text='<%#DateTime.Parse(Eval("Date").ToString()).ToString("MM/dd/yyyy")%>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Last INC">
                        <ItemTemplate>
                            <asp:Label ID="lbl_LastINC" runat="server" Text='<%# Eval("LastInc") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Last Temp">
                        <ItemTemplate>
                            <asp:Label ID="lbl_LastTemp" runat="server" Text='<%# Eval("lasttemp") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Depth Start">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DepthStart" runat="server" Text='<%# Eval("depthstart") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Depth End">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DepthEnd" runat="server" Text='<%# Eval("depthend") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                      <telerik:GridTemplateColumn HeaderText="Last AZM">
                        <ItemTemplate>
                            <asp:Label ID="lbl_LastAZM" runat="server" Text='<%# Eval("lastazm") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="MWD Operators">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Operators" runat="server"></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Activity">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Activity" runat="server"></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Day# for Job">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DayforJob" runat="server"></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                       
                       <telerik:GridTemplateColumn HeaderText="Days Left for Job">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DaysLeftforJob" runat="server"></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn>                                      
                         
                         <telerik:GridTemplateColumn HeaderText="Daily Charge ($)">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DailyCharge" runat="server" Text='<%# Eval("dailycharges") %>'></asp:Label>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                      <telerik:GridTemplateColumn HeaderText="Documents">
                        <ItemTemplate>
                            <asp:Button ID="ManageFilesBtn" runat="server" Text="View Documents"
                                OnClientClick='<%# String.Format("OpenFileExplorerDialog({0});return false;",Eval("runid"))%>'>
                            </asp:Button>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn> 
                    </Columns>
                        <ItemStyle HorizontalAlign="Center" />
                          <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                         <AlternatingItemStyle HorizontalAlign="Center" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                          <HeaderStyle HorizontalAlign="Center" />
                    </MasterTableView>
                      <HeaderStyle VerticalAlign="Bottom" HorizontalAlign="Center" />
                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                 </div>
            </td>
           </tr>
 </table>
   </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

