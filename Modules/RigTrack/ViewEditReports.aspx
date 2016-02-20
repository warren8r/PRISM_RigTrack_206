<%@ Page Title="View/Edit Reports" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewEditReports.aspx.cs" Inherits="Modules_RigTrack_ViewEditReports" EnableEventValidation="false" %>

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
                radwindow = $find("<%= RadWindow1.ClientID%>");
            }

            function openRadWindow() {
                var reportid = $find("<%=RadGrid1.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("ID");
                var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
                // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
                var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
                var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
                // At least Safari 3+: "[object HTMLElementConstructor]"
                var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
                var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6


                if (isFirefox) {
                }
                else {
                    event.preventDefault();
                }
                radwindow.show();
                setWindowsize(reportid);
            }

            function openReportWindow() {
                var reportid = $find("<%=RadGrid1.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("ID");
                var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
                // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
                var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
                var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
                // At least Safari 3+: "[object HTMLElementConstructor]"
                var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
                var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6


                if (isFirefox) {
                }
                else {
                    event.preventDefault();
                }
                radwindow.show();
                setReportWindowsize(reportid);
            }

            function openTargetWindow() {
                var targetid = $find("<%=RadGrid1.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("TargetID");
                var curveid = $find("<%=RadGrid1.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("CurveID");
                var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
                // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
                var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
                var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
                // At least Safari 3+: "[object HTMLElementConstructor]"
                var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
                var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6


                if (isFirefox) {
                }
                else {
                    event.preventDefault();
                }
                radwindow.show();
                setTargetWindowsize(targetid, curveid);
            }

            $(window).resize(function () {
                if (radwindow.isVisible()) {
                    setSizeForWindow();
                }
            });

            function setSizeForWindow() {
                var viewportWidth = $(window).width();
                var viewportHeight = $(window).height();
                radwindow.setSize(Math.ceil(viewportWidth - 90), Math.ceil(viewportHeight - 90));
            }

            function setWindowsize(ReportID) {
                setSizeForWindow();
                radwindow.setUrl("ViewReport.aspx?ReportID=" + ReportID);
                radwindow.center();
                radwindow.set_modal(true);
            }

            function setReportWindowsize(ReportID) {
                setSizeForWindow();
                radwindow.setUrl("CreateReports.aspx?ReportID=" + ReportID);
                radwindow.center();
                radwindow.set_modal(true);
            }
            function setTargetWindowsize(TargetID, CurveID) {
                setSizeForWindow();
                radwindow.setUrl("TargetReport.aspx?TargetID=" + TargetID + "&CurveID=" + CurveID);
                radwindow.center();
                radwindow.set_modal(true);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadWindow ID="RadWindow1" runat="server">
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
                            <h2>View/Edit Report Templates</h2>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>&nbsp;</asp:TableCell>
                        </asp:TableRow>


                        <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
                                               <font style="color:#f5c739 !important;">Company:</font>
						
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
                                               <font style="color:#f5c739 !important;">Job/Curve Group:</font>
						
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
                                            <font style="color:#f5c739 !important;">TargetID_Name:</font>
						
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlCompany" runat="server" Width="180px" DropDownHeight="200px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlCurveGroupID_Name" Width="180px" DropDownHeight="200px" OnSelectedIndexChanged="ddlCurveGroupID_Name_SelectedIndexChanged" runat="server" AutoPostBack="true" Enabled="true">
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlTarget" runat="server" Width="180px" DropDownHeight="200px" OnSelectedIndexChanged="ddlTarget_SelectedIndexChanged" AutoPostBack="true" Enabled="true">
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="BtnSearch" CssClass="button-SearchMain" ForeColor="Black" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <telerik:RadGrid ID="RadGrid1" OnPageSizeChanged="RadGrid1_PageSizeChanged" OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowPaging="True" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" ShowFooter="true" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated">
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                        <ExportSettings IgnorePaging="true" ExportOnlyData="true"></ExportSettings>
                        <MasterTableView PagerStyle-AlwaysVisible="true" CommandItemDisplay="Top" ClientDataKeyNames="ID, CurveID, TargetID" DataKeyNames="ID, ReportName, CurveGroupID, CurveGroupName, CurveID, CurveName, TargetID, TargetName, Company, ReportCreateDate">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                ShowExportToCsvButton="true" ShowExportToPdfButton="true"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderText="Select" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Report ID" DataField="ID" UniqueName="ID" SortExpression="ID"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Report Name" DataField="ReportName" UniqueName="ReportName" SortExpression="ReportName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Curve Group ID" DataField="CurveGroupID" UniqueName="CurveGroupID" SortExpression="CurveGroupID"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Curve Group Name" DataField="CurveGroupName" UniqueName="CurveGroupName" SortExpression="CurveGroupName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Curve ID" DataField="CurveID" UniqueName="CurveID" SortExpression="CurveID"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Curve Name" DataField="CurveName" UniqueName="CurveName" SortExpression="CurveName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Target ID" DataField="TargetID" UniqueName="TargetID" SortExpression="TargetID"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Target Name" DataField="TargetName" UniqueName="TargetName" SortExpression="TargetName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Company" DataField="Company" UniqueName="Company" SortExpression="Company"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Report Created" DataField="ReportCreateDate" UniqueName="ReportCreateDate" SortExpression="ReportCreateDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridDateTimeColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <asp:Table ID="ButtonTable2" runat="server" Width="100%" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell Width="50%"></asp:TableCell>
                            <asp:TableCell>
                                <Telerik:RadButton ID="btnViewReport" runat="server" Text="View Selected Report" OnClientClicked="openRadWindow" Enabled="false" AutoPostBack="false" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <Telerik:RadButton ID="btnViewTargetReport" runat="server" Text="View Selected Target Report" OnClientClicked="openTargetWindow" Enabled="false" AutoPostBack="false" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <Telerik:RadButton ID="btnEditReport" runat="server" Text="Edit Selected Report" OnClientClicked="openReportWindow" Enabled="false" AutoPostBack="false" />
                            </asp:TableCell>
                            <asp:TableCell Width="50%"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
<%--                <Triggers>
                    <asp:PostBackTrigger ControlID="RadGrid1" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </fieldset>
        <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
