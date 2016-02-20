<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="ViewPreviousDailyRunReport.aspx.cs" Inherits="Modules_RigTrack_ViewPreviousDailyRunReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">


            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function downloadFile(fileName, downloadLink1) {

                var downloadLink = $get('downloadFileLink');
                var filePath = "../../Documents/" + fileName;
                downloadLink.href = "DownloadHandler.ashx?fileName=" + fileName + "&filePath=" + filePath;
                downloadLink.style.display = 'block';
                downloadLink.style.display.visibility = 'hidden';
                downloadLink.click();

                // return false;

            }

            function openWindowDetails(jobid, runid) {


                var url = "../../Modules/RigTrack/ViewDailyRunReport.aspx?jobID=" + jobid + "&runid=" + runid + "";
                document.getElementById('<%=iframe3.ClientID %>').src = url;
                window.radopen(null, "window_Asset");
                return false;

            }
        </script>
    </telerik:RadScriptBlock>

    
    
    <table width="100%">
        <tr>
                    <td style="padding-left:30px">
                        <h2>View Daily Run Hour Report</h2>
                    </td>
                </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                                Select Company<br />
                                <telerik:RadDropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" Width="220px" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="Select All" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                        <td>
                            Select Job:<br />
                            <telerik:RadComboBox ID="radcombo_job" runat="server" Width="220px"  AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="Select All" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <%--OnSelectedIndexChanged="radcombo_job_SelectedIndexChanged" AutoPostBack="true"<td>
                            Select Date:<br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                    <DisabledDayStyle CssClass="DisabledClass" />
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                            </telerik:RadDatePicker>
                        </td>--%>
                        <td>
                        <br />
                            <asp:Button ID="btn_view" runat="server" OnClick="btn_view_OnClick" Text="View" Visible="false"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" id="td_jobdet" runat="server" style="display:none">
                <table style="background-color:#528ED4;color:White" border="1">
                    <tr>
                        <td >
                            Job Name:
                        </td>
                        <%--<td >
                            Job Type:
                        </td>--%>
                        <td >
                            StartDate:
                        </td>
                        <td >
                            EndDate:
                        </td>
                        
                        <td >
                            Address:
                        </td>
                        <td >
                            City:
                        </td>
                       <%-- <td >
                            State:
                        </td>--%>
                        <td >
                            Country:
                        </td>
                        <%--<td >
                            zip:
                        </td>--%>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_jname" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <%--<td>
                            <asp:Label ID="lbl_jtype" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>--%>
                        <td>
                            <asp:Label ID="lbl_sdate" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_edate" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        
                        <td>
                            <asp:Label ID="lbl_address" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <%--<td>
                            <asp:Label ID="lbl_city" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>--%>
                        <td>
                            <asp:Label ID="lbl_state" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_country" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <%--<td>
                            <asp:Label ID="lbl_zip" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        --%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td align="right">
                            <telerik:RadButton ID="RadButton1" runat="server" Text="Export to excel" OnClick="exl_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" GridLines="None" AllowPaging="True"
                                Width="1020px" AllowSorting="True" AutoGenerateColumns="False" OnItemDataBound="RadGrid1_ItemDataBound"
                                >
                                <%--<ExportSettings HideStructureColumns="false" ExportOnlyData="true">
                                    <Excel Format="Html" />
                                </ExportSettings>--%>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <AlternatingItemStyle VerticalAlign="Top" />
                                
                                <MasterTableView>
                                    <CommandItemTemplate>
                                        <asp:Button ID="Button1" Text="Add new item" runat="server" CommandName="InitInsert">
                                        </asp:Button>
                                    </CommandItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                    <AlternatingItemStyle VerticalAlign="Top" />
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                        ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                                        ShowExportToPdfButton="true">
                                    </CommandItemSettings>
                                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn   HeaderText="Job Name"   FilterControlAltText="Filter enddate column"
                                        SortExpression="CurveGroupName" UniqueName="CurveGroupName">
                                            <ItemTemplate>
                                             <asp:Label ID="lblCurveGroupName" runat="server" Text='<%#Eval("CurveGroupName")%>' />
                                                <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'/>
                                            </ItemTemplate>                               
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn   HeaderText="Date"   FilterControlAltText="Filter enddate column"
                                        SortExpression="Date" UniqueName="Date">
                                            <ItemTemplate>
                                             <asp:Label ID="lbl_date" runat="server" Text='<%#DateTime.Parse(Eval("Date").ToString()).ToString("MM/dd/yyyy")%>' />
                                            </ItemTemplate>                               
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn   HeaderText="Run Number"   FilterControlAltText="Filter projectManager column"
                                        SortExpression="runnumber" UniqueName="runnumber">
                                            <ItemTemplate>
                                             <asp:Label ID="lbl_runnumber" runat="server" Text='<%# Eval("runnumber") %>'/>
                                             <asp:Label ID="lbl_runid" runat="server" Visible="false" Text='<%# Eval("runid") %>'/>
                                            </ItemTemplate>                               
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn   HeaderText="Finished"   FilterControlAltText="Filter projectManager column"
                                        SortExpression="finished" UniqueName="finished">
                                            <ItemTemplate>
                                             <asp:Label ID="lbl_finished" runat="server" Text='<%# Eval("finished") %>'/>
                                            </ItemTemplate>                               
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn   HeaderText="Activity / Time"   FilterControlAltText="Filter projectManager column"  HeaderStyle-HorizontalAlign="Center"
                                        SortExpression="Activity" UniqueName="Activity">
                                            <ItemTemplate>
                                             <telerik:RadGrid ID="radgridactivities" runat="server" HeaderStyle-Width="150" ItemStyle-Width="150" ShowHeader="false" AutoGenerateColumns="true">
                                                <MasterTableView NoDetailRecordsText="No Activity Selected" NoMasterRecordsText="No Activity Selected"></MasterTableView>
                                           </telerik:RadGrid>
                                            </ItemTemplate>                               
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn   HeaderText="Tool Name / Qty. Required"   FilterControlAltText="Filter projectManager column"  HeaderStyle-HorizontalAlign="Center"
                                        SortExpression="AssetsRequired" UniqueName="AssetsRequired">
                                            <ItemTemplate>
                                             <telerik:RadGrid ID="radgridAssetsRequired" runat="server" HeaderStyle-Width="150" ItemStyle-Width="150" ShowHeader="false" AutoGenerateColumns="true">
                                                <MasterTableView NoDetailRecordsText="Additional Tools Not Required" NoMasterRecordsText="No Additional Tools Required"></MasterTableView>
                                           </telerik:RadGrid>
                                            </ItemTemplate>                               
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn   HeaderText="View Documents"   FilterControlAltText="Filter View Documents" HeaderStyle-HorizontalAlign="Center"
                                         UniqueName="viewdocuments">
                                            <ItemTemplate>
                                                    <telerik:RadGrid ID="griddocuments" runat="server"  HeaderStyle-Width="150" ItemStyle-Width="200" ShowHeader="false" >
                                                        
                                                        <MasterTableView AutoGenerateColumns="False"  NoDetailRecordsText="" NoMasterRecordsText="No Documets Uploaded">
                
                                                            <Columns>
                    
                                                                <telerik:GridBoundColumn DataField="runid" Visible="false"
                                                                    HeaderText="runid" ReadOnly="True" SortExpression="runid"
                                                                    UniqueName="runid">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="DocumentName" 
                                                                    HeaderText="Doc Name" SortExpression="DocumentName" UniqueName="DocumentName">
                                                                </telerik:GridBoundColumn> 
                                                                <telerik:GridTemplateColumn HeaderText="Download" >
                                                                    <ItemTemplate> 
                                                                    <asp:LinkButton ID="lnk_download" runat="server"  OnClientClick='<%# String.Format("downloadFile(\"{0}\",\"{1}\");", Eval("DocumentDisplayName"),this) %>' Text="Download"></asp:LinkButton>                          
                                                                    <a id="downloadFileLink"> </a>
                                                                    <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("runid") %>' style="display:none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("documentid") %>' style="display:none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentDisplayName") %>' style="display:none;"></asp:Label>                         
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn> 
                                                                         
                                                            </Columns>
                                                            <EditFormSettings>
                                                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                                </EditColumn>
                                                            </EditFormSettings>
                                                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                                        </MasterTableView>
                                                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                                        <FilterMenu EnableImageSprites="False">
                                                        </FilterMenu>
                                                    </telerik:RadGrid>
                                            </ItemTemplate>                               
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn   HeaderText="View Details"   FilterControlAltText="Filter enddate column"
                                        SortExpression="ViewDetails" UniqueName="ViewDetails">
                                            <ItemTemplate>
                                             <asp:Button ID="btnView" runat="server" Text="View Details" />
                                            </ItemTemplate>
                                                                           
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    
                                    <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                        <HeaderStyle VerticalAlign="Bottom" />
                    </MasterTableView>
                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <ExportSettings SuppressColumnDataFormatStrings="false" ExportOnlyData="true">
                        <Excel Format="Biff" />
                    </ExportSettings>
                </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server"  Modal="true" Animation="Resize"> 
                                                     <Windows> 
                                                     
                                                      <telerik:RadWindow ID="window_Asset" runat="server"  Modal="true" Width="1120px"  height="600px" Title="Create New / Edit Tool">
 
                                                        <ContentTemplate>
                                                           <iframe id="iframe3" runat="server" width="1120px" height="600px" >
                                               
                                                            </iframe>
 
                                                         </ContentTemplate>
 
                                                     </telerik:RadWindow>
 
                                                 </Windows> 
                                                 </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
    
</asp:Content>

