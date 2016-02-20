<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RigTrack.master"  CodeFile="ViewEditSurveys.aspx.cs" Inherits="Modules_RigTrack_ViewEditSurveys" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style=" position:fixed; text-align:center; height:100%; width:100%; top:0; right:0; left:0; padding-top:15%; z-index:9999999;">
                <div class="loader2">Loader...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>
                
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>View/Edit Survey</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                            Curve Group:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                            Curve:
                        </asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList runat="server" ID="ddlCurve" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnSearchCurve" runat="server" OnClick="btnSearchCurve_Click" Text="Search" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                                <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <h4>Survey Entry</h4>
                        </asp:TableCell>
                        
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                            Measurement Depth:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                            Inclination:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                            Azimuth:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                            Survey Comments:
                        </asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtMeasurementDepth" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtInclination" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtAzimuth" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtSurveyComments" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnAddSurvey" runat="server" OnClick="btnAddSurvey_Click" Text="Add New Survey" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <telerik:RadGrid ID="RadGridSurveys" AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="15"
                     OnNeedDataSource="RadGridSurveys_NeedDataSource" >
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No Data to be displayed">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckboxTemplate" HeaderText="Edit" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkbx" runat="server" OnCheckedChanged="chkbx_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="MD" DataField="MD" UniqueName="MD" SortExpression="MD"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Inclination" DataField="Inclination" UniqueName="Inclination" SortExpression="Inclination"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Azimuth" DataField="Azimuth" UniqueName="Azimuth" SortExpression="Azimuth"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="TVD" DataField="TVD" UniqueName="TVD" SortExpression="TVD"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Subseas-TVD" DataField="SubseasTVD" UniqueName="SubseasTVD" SortExpression="SubseasTVD"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="NS" DataField="NS" UniqueName="NS" SortExpression="NS"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="EW" DataField="EW" UniqueName="EW" SortExpression="EW"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Vertical Section" DataField="VerticalSection" UniqueName="VerticalSection" SortExpression="VerticalSection"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="CL" DataField="CL" UniqueName="CL" SortExpression="CL"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Closure Distance" DataField="ClosureDistance" UniqueName="ClosureDistance" SortExpression="ClosureDistance"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Closure Direction" DataField="ClosureDirection" UniqueName="ClosureDirection" SortExpression="ClosureDirection"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="DLS" DataField="DLS" UniqueName="DLS" SortExpression="DLS"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="BR" DataField="BR" UniqueName="BR" SortExpression="BR"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="WR" DataField="WR" UniqueName="WR" SortExpression="WR"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="TFO" DataField="TFO" UniqueName="TFO" SortExpression="TFO"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>




            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>



</asp:Content>