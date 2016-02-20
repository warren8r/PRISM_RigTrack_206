<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DemoCode.master" AutoEventWireup="true" CodeFile="exportexcel.aspx.cs" Inherits="exportexcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock>
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                            args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                            args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxLoadingPanel ID="loader" runat="server" />

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="loader">
        <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" GridLines="None" AllowPaging="True" AllowSorting="false" 
                AutoGenerateColumns="False" OnItemCommand="RadGrid1_ItemCommand" OnPageIndexChanged="RadGrid1_PageIndexChanged">

                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>

                <MasterTableView CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToCsvButton="True" ShowExportToPdfButton="True" ShowExportToWordButton="True" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="column9" FilterControlAltText="Filter column9 column"
                            HeaderText="Time Stamp" SortExpression="column9" UniqueName="column9" DataType="System.DateTime">
                            <ItemStyle CssClass="options" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column20" FilterControlAltText="Filter column20 column"
                            HeaderText="Event Id" SortExpression="column20" UniqueName="column20" DataType="System.Int32">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column7" FilterControlAltText="Filter column7 column"
                            HeaderText="Event Name" SortExpression="column7" UniqueName="column7">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column17" FilterControlAltText="Filter column17 column"
                            HeaderText="Event Code" SortExpression="column17" UniqueName="column17">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column19" FilterControlAltText="Filter column19 column"
                            HeaderText="Event Information" SortExpression="column19" UniqueName="column19">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column1" FilterControlAltText="Filter column1 column"
                            HeaderText="Action Status" SortExpression="column1" UniqueName="column1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column5" FilterControlAltText="Filter column5 column"
                            HeaderText="column5" SortExpression="column5" UniqueName="column5" DataType="System.Int32"
                            ReadOnly="True" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column6" FilterControlAltText="Filter column6 column"
                            HeaderText="Category" SortExpression="column6" UniqueName="column6">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column8" FilterControlAltText="Filter column8 column" HeaderText="id"
                            SortExpression="column8" UniqueName="column8" DataType="System.Int32" ReadOnly="True" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column10" FilterControlAltText="Filter column10 column"
                            HeaderText="column10" SortExpression="column10" UniqueName="column10"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column11" FilterControlAltText="Filter column11 column"
                            HeaderText="Source" SortExpression="column11" UniqueName="column11" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column12" DataType="System.DateTime" FilterControlAltText="Filter column12 column"
                            HeaderText="EndTime" SortExpression="column12" UniqueName="column12" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="column13" DataType="System.Boolean" FilterControlAltText="Filter column13 column"
                            HeaderText="State" SortExpression="column13" UniqueName="column13" Visible="false">
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="column14" DataType="System.Boolean" FilterControlAltText="Filter column14 column"
                            HeaderText="Ongoing" SortExpression="column14" UniqueName="column14" Visible="false">
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="column15" FilterControlAltText="Filter column15 column"
                            HeaderText="Phase" SortExpression="column15" UniqueName="column15" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column16" FilterControlAltText="Filter column16 column"
                            HeaderText="Counter" SortExpression="column16" UniqueName="column16" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column4" FilterControlAltText="Filter column4 column"
                            HeaderText="Contains Task Order" SortExpression="column4" UniqueName="column4"
                            Visible="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="column18" DataType="System.Boolean" FilterControlAltText="Filter column18 column"
                            HeaderText="column18" SortExpression="column18" UniqueName="column18"
                            Visible="false">
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="column21" FilterControlAltText="Filter column21 column"
                            HeaderText="column21" SortExpression="column21" UniqueName="column21"
                            Visible="false" DataType="System.Int32">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column22" DataType="System.Int32" FilterControlAltText="Filter column22 column"
                            HeaderText="column22" SortExpression="column22" UniqueName="column22"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column23" DataType="System.Int32" FilterControlAltText="Filter column23 column"
                            HeaderText="column23" SortExpression="column23" UniqueName="column23"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column24" FilterControlAltText="Filter column24 column"
                            HeaderText="Elster Meter Serial Number" SortExpression="column24"
                            UniqueName="column24">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column25" DataType="System.Int32" FilterControlAltText="Filter column25 column"
                            HeaderText="column25" SortExpression="column25" UniqueName="column25"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column26" FilterControlAltText="Filter column26 column"
                            HeaderText="column26" SortExpression="column26" UniqueName="column26"
                            DataType="System.DateTime" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column27" DataType="System.DateTime"
                            FilterControlAltText="Filter column27 column" HeaderText="column27"
                            SortExpression="column27" UniqueName="column27" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column28" FilterControlAltText="Filter column28 column"
                            HeaderText="Flag Name" SortExpression="column28" UniqueName="column28">
                            <ItemStyle CssClass="options" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="column29" FilterControlAltText="Filter column29 column"
                            HeaderText="Employee Name" SortExpression="column29" UniqueName="column29" ReadOnly="True"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridHyperLinkColumn AllowSorting="False" HeaderText="GIS" DataNavigateUrlFields="column32"
                            DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                            Target="_new" Text="View Map" UniqueName="gisLink">
                            <ItemStyle ForeColor="Blue" />
                        </telerik:GridHyperLinkColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
