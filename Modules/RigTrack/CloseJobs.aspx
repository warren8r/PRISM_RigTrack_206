<%@ Page Title="Close Jobs" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CloseJobs.aspx.cs" Inherits="Modules_RigTrack_CloseJobs" EnableEventValidation="false" %>

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

            function openRadWindow(CurveGroupID) {
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
                setWindowsize(CurveGroupID);
            }

            $(window).resize(function () {
                if (radwindow.isVisible()) {
                    setSizeForWindow();
                }
            });

            function setSizeForWindow() {
                var viewportWidth = $(window).width();
                var viewportHeight = $(window).height();
                radwindow.setSize(Math.ceil(viewportWidth /4+200), Math.ceil(viewportHeight /4+200));
            }

            function setWindowsize(CurveGroupID) {
                setSizeForWindow();
                radwindow.setUrl("Attachments.aspx?CurveGroupID=" + CurveGroupID);
                radwindow.center();
                radwindow.set_modal(true);
            }
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function RadConfirm(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        //initiate the origianal postback again
                        this.click();
                    }
                });

                var text = "Are you sure you want to submit the page?";
                radconfirm(text, callBackFunction, 300, 200, null, "RadConfirm");
                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                args.set_cancel(true);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close">
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
                            <h2>Close Jobs/Curve Groups</h2>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>&nbsp;</asp:TableCell>
                        </asp:TableRow>


                        <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center"  BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
                                             <font style="color:#f5c739 !important;">Company:</font>
						
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
                                              <font style="color:#f5c739 !important;">Job/Curve Group ID_Name:</font>
						
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
                                            <font style="color:#f5c739 !important;">Status:</font>
						
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlCompany" runat="server" Width="200px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:DropDownListItem Value="0" Text="--Select--" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlCurveGroupID_Name" runat="server" Width="200px" OnSelectedIndexChanged="ddlCurveGroupID_Name_SelectedIndexChanged" AutoPostBack="true">
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlStatus" runat="server" Width="200px" AppendDataBoundItems="true">
                                                <Items>
                                                    <%--                                                    <telerik:DropDownListItem Value="-1" Text="--Select--" />--%>
                                                    <telerik:DropDownListItem Value="1" Text="Open" />
                                                    <telerik:DropDownListItem Value="0" Text="Closed" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="BtnSearch" CssClass="button-SearchMain" ForeColor="Black" runat="server" Text="Search" OnClick="BtnSearch_Click" />
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
                    <telerik:RadGrid ID="RadGrid1" OnPageSizeChanged="RadGrid1_PageSizeChanged" OnItemCommand="RadGrid1_ItemCommand" OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowPaging="True" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" ShowFooter="true" OnItemCreated="RadGrid1_ItemCreated" >
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                        <ExportSettings ExportOnlyData="true"></ExportSettings>
                        <MasterTableView PagerStyle-AlwaysVisible="true" CommandItemDisplay="Top" DataKeyNames="ID, CurveGroupName, JobNumber, JobLocation, CompanyID, LeaseWell, RigName, JobStartDate, JobEndDate, Status, Comments, IsAttachment">
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
                                <telerik:GridBoundColumn HeaderText="Company" DataField="CompanyName" UniqueName="CompanyName" SortExpression="CompanyName"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Lease/Well" DataField="LeaseWell" UniqueName="LeaseWell" SortExpression="LeaseWell"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Job Start Date" DataField="JobStartDate" UniqueName="JobStartDate" SortExpression="JobStartDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Job End Date" DataField="JobEndDate" UniqueName="JobEndDate" SortExpression="JobEndDate"
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

                    <asp:Table ID="ButtonTable2" runat="server" Width="100%" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell Width="50%"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btnCloseCurveGroup" runat="server" Text="Close Selected Job/Curve Groups" OnClick="btnCloseCurveGroup_Click" Enabled="false" />
                            </asp:TableCell>
                            <asp:TableCell Width="50%"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <asp:Table ID="ClosureItems" runat="server" Width="100%" HorizontalAlign="Center" Visible="false">
                        <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table2" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Job End Date
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Upload Attachments
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Comments
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadDatePicker ID="dateJobEndDate" runat="server" Width="200px"></telerik:RadDatePicker>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadAsyncUpload runat="server" ID="AttachmentUpload" MultipleFileSelection="Automatic" Width="235px" />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox runat="server" ID="Comments" Width="216px" EmptyMessage="Enter comment" TextMode="SingleLine"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
                <%--<Triggers>
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
