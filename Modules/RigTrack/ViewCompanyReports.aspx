<%@ Page Language="C#" Title="View Job Reports" MasterPageFile="~/Masters/RigTrack_viewcompanyreport.master" AutoEventWireup="true" CodeFile="ViewCompanyReports.aspx.cs" Inherits="Modules_RigTrack_ViewCompanyReports" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var $ = $telerik.$;
            var radwindow;

            function pageLoad() {
                radwindow = $find('<%= RadWindow1.ClientID%>');
            }


            $(window).resize(function () {
                if (radwindow.isVisible()) {
                    setSizeForWindow();
                }
            });

            function openRadWindow() {
                var masterTable = $find("<%=RadGrid1.ClientID%>").get_masterTableView();
                //var CurveGroupID = masterTable.getCellByColumnUniqueName(masterTable.get_dataItems()[0], "ID").innerHTML; //raddropdownlist.get_selectedItem().get_value();
                var CurveGroupID = masterTable.getCellByColumnUniqueName(masterTable.get_selectedItems()[0], "ID").innerHTML;
                radwindow.show();
                setWindowsize(CurveGroupID);
              }

          
            function setSizeForWindow() {
                var viewportWidth = $(window).width();
                var viewportHeight = $(window).height();
                radwindow.setSize(Math.ceil(viewportWidth - 90 ), Math.ceil(viewportHeight-90 ));
            }

            function setWindowsize(CurveGroupID) {
                setSizeForWindow();
                radwindow.setUrl("ViewJobReports.aspx?CurveGroupID=" + CurveGroupID);
                radwindow.center();
                radwindow.set_modal(true);
            }

            function OnClientPageLoad(wnd) {
                wnd.set_autoSize(false);
            }
          
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadWindow ID="RadWindow1" runat="server" OnClientPageLoad="OnClientPageLoad" Behaviors="Close">
        </telerik:RadWindow>

        <%-- // Jd New CSS Loading Animation--%>
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

                    <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>View Job Reports</h2>
                            </asp:TableCell>
                        </asp:TableRow>



                    </asp:Table>


                    <asp:Table ID="Table6" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table7" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
						                  <input type="radio" runat="server" id="CompanyActive" name="Search" value="1"/> 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell>
						                  <input type="radio" runat="server" id="StateActive" name="Search" value="2"/> 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell>
						                  <input type="radio" runat="server" id="DateActive" name="Search" value="3" checked="true"/> 
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Company 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						State
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter" HorizontalAlign="Left">
						&nbsp&nbsp&nbsp&nbsp&nbsp Start Date &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
						End Date
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Job Status
                                    </asp:TableHeaderCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="150px" AutoPostBack="false" DropDownWidth="160px" ID="ddlCompany"  Width="160px"></telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlState" Width="160px" DropDownHeight="200px" AutoPostBack="false"></telerik:RadDropDownList>                                       
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="date_StartDate" runat="server" Width="150px" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>
                                         <telerik:RadDatePicker ID="date_EndDate" runat="server" Width="150px" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <input type="radio" runat="server" id="StatusActive" name="Status" value="1" checked="true"/>Open
                                        <input type="radio" runat="server" id="StatusInActive" name="Status" value="0"/>Close
                                   </asp:TableCell>
                                    
                                    <asp:TableCell>
                                         <Telerik:RadButton ID="btnViewReport" runat="server" Text="View Report" OnClick="btnViewReport_Click" AutoPostBack="true"   />
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

                    <telerik:RadGrid ID="RadGrid1" OnPageSizeChanged="RadGrid1_PageSizeChanged" OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowPaging="True" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" ShowFooter="true" OnItemCreated="RadGrid1_ItemCreated">
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                        <ExportSettings ExportOnlyData="true"></ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Scrolling AllowScroll="false" UseStaticHeaders="false" />
                        </ClientSettings>
                        <MasterTableView PagerStyle-AlwaysVisible="true" CommandItemDisplay="Top">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                ShowExportToCsvButton="true" ShowExportToPdfButton="true"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderText="Select" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Curve Group ID" DataField="ID" UniqueName="ID" SortExpression="ID"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Curve Group Name" DataField="CurveGroupName" UniqueName="CurveGroupName" SortExpression="CurveGroupName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Job Number" DataField="JobNumber" UniqueName="JobNumber" SortExpression="JobNumber"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Job Location" DataField="JobLocation" UniqueName="JobLocation" SortExpression="JobLocation"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Company" DataField="Company" UniqueName="Company" SortExpression="Company"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Lease/Well" DataField="LeaseWell" UniqueName="LeaseWell" SortExpression="LeaseWell"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Job Start Date" DataField="Job Start Date" UniqueName="JobStartDate" SortExpression="JobStartDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Job End Date" DataField="Job End Date" UniqueName="JobEndDate" SortExpression="JobEndDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn HeaderText="Rig Name" DataField="RigName" UniqueName="RigName" SortExpression="RigName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" UniqueName="Status" SortExpression="Status"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Comments" DataField="Comments" UniqueName="Comments" SortExpression="Comments"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Attachments" DataField="IsAttachment" UniqueName="IsAttachment" SortExpression="IsAttachment"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <div align="center">
                        <Telerik:RadButton ID="btnView" runat="server" Text="View Job Details" OnClientClicked="openRadWindow" Enabled="false" />
                        <Telerik:RadButton ID="btnView2D" runat="server" Text="View 2D Graph" Enabled="false" OnClick="btnView2D_Click" />
                        <Telerik:RadButton ID="btnView3D" runat="server" Text="View 3D Graph" Enabled="false" OnClick="btnView3D_Click" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
