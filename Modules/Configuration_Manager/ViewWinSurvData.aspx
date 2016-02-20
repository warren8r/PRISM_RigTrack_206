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
  <%-- // Jd New CSS Loading Animation--%>
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
                            <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="Jobtextname" DataValueField="jobname"  EmptyMessage="Select JobName" Width="200px"
                                        ></telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select 0 as [jobname],'Select JobName' as Jobtextname union select [jobname],jobname + ' ('+jobordercreatedid+')' as Jobtextname from manageJobOrders where status!='Closed' and jobfrom='WinSurv'"></asp:SqlDataSource>
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
                    <tr><td><b>tbljob Info</b></td></tr>
                    <tr>
                       
                        <td>
                            <telerik:RadGrid ID="RadGrid1"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource1" Width="1500px"
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
                                    SelectCommand="select * from tbljob where [Job ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>Tbljobinventory Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid2"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource2" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource2">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource2" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from Tbljobinventory where [JOBID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblJOBDATE Info</b></td></tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid3"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="true"
                                  AllowPaging="true" AllowSorting="true"  DataSourceID="SqlDataSource3" Width="1500px"
                               >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource3">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource3" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select * from tblJOBDATE where [Job ID]=@JOBID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="combo_job" Name="JOBID"    />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr><td><b>tblJOBDATEitems Info</b></td></tr>
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
                    </tr>
                </table>
            </td>
        </tr>
</table>
        </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

