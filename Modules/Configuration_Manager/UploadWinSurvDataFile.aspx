<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="UploadWinSurvDataFile.aspx.cs" Inherits="Modules_Configuration_Manager_UploadWinSurvDataFile" %>

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
<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td align="center">
            <asp:Label ID="lbl_message" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <table>
                <tr>
                    <%--<td align="left">
                        Select Job:<br />
                            <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid"  EmptyMessage="Select JobName" Width="200px"
                                        ></telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                    SelectCommand="select 0 as [jid],'Select JobName' as Jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where status!='Closed' and
                                        (jid in(select jobid from PrismJobKits) or jid in (select jobid from PrismJobConsumables))"></asp:SqlDataSource>
                    </td>--%>                        
                
                    <td align="left">
                    Select File (.MDB Format)<br />
                                <asp:FileUpload ID="file_dayrun" Width="350px" runat="server" />
                    </td>
                    
                    <td align="left">
                        <br />
                        <asp:Button ID="btn_view" runat="server" OnClick="btn_view_OnClick"  Text="Upload"  />
                        <asp:Label ID="combo_job" runat="server" Visible="false"></asp:Label>
                    </td> 
                    <td>
                        <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                        <telerik:RadToolTip ID="RadToolTip2" runat="server" Position="MiddleRight" RelativeTo="Element"
                            TargetControlID="ImageButton2" Width="500px" HideEvent="ManualClose"
                            OnClientShow="OnClientShow">
                            1. The data file used to import contains complete set of data for job.<br />
                            2. All the data from the old data file will be replaced with the data from the new data file.<br />
                            3. Data file uploaded will contain complete set of data for single job.
                        </telerik:RadToolTip>
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
</asp:Content>

