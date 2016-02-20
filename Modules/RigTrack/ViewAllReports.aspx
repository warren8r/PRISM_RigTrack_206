<%@ Page Language="C#" MasterPageFile="~/Masters/ViewAllReports.master" AutoEventWireup="true" CodeFile="ViewAllReports.aspx.cs" Inherits="Modules_RigTrack_ViewAllReports" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function Close() {
            GetRadWindow().close(); // Close the window 
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

            return oWindow;
        }
    </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadWindow ID="RadWindow1" runat="server">
        </telerik:RadWindow>
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

                 
                    <asp:Table ID="GridTable1" Visible="true" runat="server" Width="100%" HorizontalAlign="Center">

                        <asp:TableRow>

                            <%-- <asp:TableCell Width="50%"></asp:TableCell>--%>

                            <asp:TableCell >
                                <telerik:RadGrid ID="RadGrid1" style="width:90%;"  OnPageSizeChanged="RadGrid1_PageSizeChanged" OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowPaging="True" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="true" runat="server" AllowMultiRowSelection="false" ShowFooter="true" OnItemCreated="RadGrid1_ItemCreated" Width="800px">
                                    <PagerStyle Mode="NextPrevAndNumeric" Width="1300px" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                                    <ExportSettings ExportOnlyData="true"></ExportSettings>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                    </ClientSettings>
                                    <MasterTableView PagerStyle-AlwaysVisible="true" CommandItemDisplay="Top">
                                    
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                            ShowExportToCsvButton="true" ShowExportToPdfButton="true"></CommandItemSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>

                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="printButton" runat="server" CssClass="button-PrintButton" ForeColor="Black" Text="Print" OnClientClick="javascript:window.print();" />
                        </td>
                       
                    </tr>
                </table>
        <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
