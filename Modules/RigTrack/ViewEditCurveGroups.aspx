<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewEditCurveGroups.aspx.cs" Inherits="Modules_RigTrack_ViewEditCurveGroups" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <script type="text/javascript">

            function openwin() {


                window.radopen(null, "window_service");

            }


        </script>

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
                            <h2>View/Edit Curve Groups</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>

           
                        <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group ID
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group Name
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Job Number
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Location
                                        </asp:TableHeaderCell>
                                     

                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <telerik:RadTextBox ID="TxtCurveGroupID" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadDropDownList ID="ddlCurveName" runat="server" Width="200px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        </asp:TableCell>


                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlJobNumber" runat="server" Width="200px" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:DropDownListItem Value="0" Text="--Select--" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                                <telerik:RadDropDownList ID="ddlLocation" runat="server" Width="200px" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:DropDownListItem Value="0" Text="--Select--" />
                                                </Items>
                                            </telerik:RadDropDownList>
                    
                                        </asp:TableCell>

                                      
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>



                             <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Company
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Lease/Well
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Rig Name
                                        </asp:TableHeaderCell>

                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Status
                                        </asp:TableHeaderCell>
                                     

                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                             <telerik:RadDropDownList ID="ddlCompany" runat="server" Width="200px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadDropDownList ID="ddlLeaseWell" runat="server" Width="200px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        </asp:TableCell>


                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlRigName" runat="server" Width="200px" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:DropDownListItem Value="0" Text="--Select--" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                                <telerik:RadDropDownList ID="ddlStatus" runat="server" Width="200px" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:DropDownListItem Value="0" Text="--Select--" />
                                                </Items>
                                            </telerik:RadDropDownList>
                    
                                        </asp:TableCell>

                                      
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>

                </asp:Table>


                    <asp:Table ID="ButtonTable" runat="server" Width="100%" HorizontalAlign="Center">

                        <asp:TableRow>
                            <asp:TableCell>&nbsp;</asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>

                            <asp:TableCell Width="35%"></asp:TableCell>

                            <asp:TableCell>
                                <asp:Button ID="btnEditCurveGroup" runat="server" Text="Edit Selected Curve Group" OnClick="btnEditCurveGroup_Click" Enabled="false"/>
                            </asp:TableCell>

                            <asp:TableCell Width="10%"></asp:TableCell>

                              <asp:TableCell>
                                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                            </asp:TableCell>

                              <asp:TableCell Width="10%"></asp:TableCell>


                              <asp:TableCell>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </asp:TableCell>

                              <asp:TableCell Width="10%"></asp:TableCell>

                              <asp:TableCell>
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                            </asp:TableCell>

                             <asp:TableCell Width="35%"></asp:TableCell>


                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell>&nbsp;</asp:TableCell>
                        </asp:TableRow>


                    </asp:Table>


                   <telerik:RadGrid ID="RadGrid1" AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" ShowFooter="true">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderText="Edit" AllowFiltering="false">
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
                            <telerik:GridDateTimeColumn HeaderText="Rig Name" DataField="RigName" UniqueName="RigName" SortExpression="RigName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn HeaderText="Status" DataField="Status" UniqueName="Status" SortExpression="Status"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridDateTimeColumn>
                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>


                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>

