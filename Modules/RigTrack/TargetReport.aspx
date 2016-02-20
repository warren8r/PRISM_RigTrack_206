<%@ Page Language="C#" Title="Target Report" MasterPageFile="~/Masters/RigTrack_viewreport.master" AutoEventWireup="true" CodeFile="TargetReport.aspx.cs" Inherits="Modules_RigTrack_TargetReport" EnableEventValidation="false" %>

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
        <div style="margin-left: auto; margin-right: auto; text-align: center; width: 15%">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbl_Header1" runat="server" Style="font-weight: bold"></asp:Label><br />
                        <asp:Label ID="Header1" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_Header2" runat="server" Style="display: none; font-weight: bold"></asp:Label>
                        <asp:Label ID="Header2" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <fieldset>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Table runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="Table1" runat="server" HorizontalAlign="Left">
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3">
                                            <asp:Label ID="lbl1" runat="server" Text="Projection made 60.00 Ft from Sensor @ Bit" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Width="100px"></asp:TableCell>
                                        <asp:TableCell>
                            <font color="black"><b>Sensor</b></font>
                                        </asp:TableCell>
                                        <asp:TableCell>
                            <font color="black"><b>Bit</b></font>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>MD</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblMDSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblMDBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>INC</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblINCSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblINCBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>AZM</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblAZMSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblAZMBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>TVD</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTVDSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTVDBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>EW</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblEWSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblEWBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>NS</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblNSSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblNSBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>VS</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblVSSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblVSBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>DLS</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblDLSSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblDLSBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Build Rate</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblBRSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblBRBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Walk Rate</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblWRSensor" runat="server"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblWRBit" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="Table2" runat="server" HorizontalAlign="Left">
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3">
                                            <asp:Label ID="lbl2" runat="server" Text="Target #1 is a Square with a side of 1.00 Ft" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Width="100px">Target TVD</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTargetTVD" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Target EW</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTargetEW" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Target NS</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTargetNS" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Target Direction</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTargetDirection" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Target Disp</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTargetDisp" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="65%">
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="false" AutoGenerateColumns="false" AllowMultiRowSelection="false" PageSize="30"
                                    Width="100%" OnNeedDataSource="RadGrid1_NeedDataSource">
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView Caption="Protractor Method of Projection" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                                        <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Pt." DataField="Pt" UniqueName="Pt"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="MD" DataField="MD" UniqueName="MD"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="INC" DataField="INC" UniqueName="INC"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="AZM" DataField="AZM" UniqueName="AZM"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="E-W" DataField="EW" UniqueName="EW"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="N-S" DataField="NS" UniqueName="NS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avg. WR" DataField="AvgWR" UniqueName="AvgWR"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avg. BR" DataField="AvgBR" UniqueName="AvgBR"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:TableCell>
                            <asp:TableCell Width="35%">
                                <asp:Table runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                                            <font color="black"><b>Proximity Data</b></font>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3">
                                            <asp:Label ID="lbl3" runat="server" Text="Compared to Curve 1"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Distance</asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtDistance" runat="server"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Comp. TVD</asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtCompTVD" runat="server"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Survey TVD</asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtSurveyTVD" runat="server"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>Right of Target Line</asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtRightOfTargetLine" runat="server"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <telerik:RadGrid ID="RadGrid2" runat="server" AllowFilteringByColumn="false" AutoGenerateColumns="false" AllowMultiRowSelection="false" PageSize="30"
                                    Width="100%" OnNeedDataSource="RadGrid2_NeedDataSource">
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView Caption="Minimum DLS Curvature Method of Projection" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                                        <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Pt." DataField="Pt" UniqueName="Pt"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="MD" DataField="MD" UniqueName="MD"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="INC" DataField="INC" UniqueName="INC"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="AZM" DataField="AZM" UniqueName="AZM"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="E-W" DataField="EW" UniqueName="EW"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="N-S" DataField="NS" UniqueName="NS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avg. WR" DataField="AvgWR" UniqueName="AvgWR"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avg. BR" DataField="AvgBR" UniqueName="AvgBR"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DLS" DataField="DLS" UniqueName="DLS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="TFO" DataField="TFO" UniqueName="TFO"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <telerik:RadGrid ID="RadGrid3" runat="server" AllowFilteringByColumn="false" AutoGenerateColumns="false" AllowMultiRowSelection="false" PageSize="30"
                                    Width="100%" OnNeedDataSource="RadGrid3_NeedDataSource">
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView Caption="DLS Method of Projection" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                                        <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Pt." DataField="Pt" UniqueName="Pt"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="MD" DataField="MD" UniqueName="MD"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="INC" DataField="INC" UniqueName="INC"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="AZM" DataField="AZM" UniqueName="AZM"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="E-W" DataField="EW" UniqueName="EW"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="N-S" DataField="NS" UniqueName="NS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avg. WR" DataField="AvgWR" UniqueName="AvgWR"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avg. BR" DataField="AvgBR" UniqueName="AvgBR"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DLS" DataField="DLS" UniqueName="DLS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="TFO" DataField="TFO" UniqueName="TFO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Hold Len." DataField="HoldLen" UniqueName="HoldLen"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>

        <div>
            <img src="../../images/ManageCurveGroups/TargetReport.png" alt="TargetReport" /><br />
        </div>
        <table>
            <tr>
                <asp:Button ID="btnClose" runat="server" OnClientClick="Close();" Text="Close"></asp:Button>
                &nbsp;
                        &nbsp;
                       <asp:Button ID="btnCancel" runat="server" OnClientClick="Close();" Text="Cancel"></asp:Button>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
