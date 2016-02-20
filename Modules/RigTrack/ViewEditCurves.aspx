<%@ Page Title="View Edit Curves" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewEditCurves.aspx.cs" Inherits="Modules_RigTrack_ViewEditCurves" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">
                <div class="loader2">Loading...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>View Edit Curves</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        Job/Curve Group:
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Target:
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" OnSelectedIndexChanged="ddlCurveGroup_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlTarget" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" Enabled="false" OnSelectedIndexChanged="ddlTarget_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnSearchCurves" runat="server" OnClick="btnSearchCurves_Click" Text="Search" Enabled="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <h4>Edit Curve Entry</h4>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            Curve Name:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell>
                            Curve Type:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell>
                            North Offset:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell>
                            East Offset:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell>
                            VS Direction:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell>
                            RKB Elevation:
                        </asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtCurveName" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList runat="server" ID="ddlCurveType" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtNorthOffset" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtEastOffset" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtVSDirection" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtRKBElevation" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnClear" Text="Clear" Width="160px" OnClick="btnClear_Click" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnUpdate" Text="Update" Width="160px" OnClick="btnUpdate_Click" Enabled="false"/>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>

                        </asp:TableCell>
                        <asp:TableCell>

                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="doubleValidator1" ControlToValidate="txtNorthOffset" ForeColor="Red"
                                Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="doubleValidator2" ControlToValidate="txtEastOffset" ForeColor="Red"
                                Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="doubleValidator3" ControlToValidate="txtVSDirection" ForeColor="Red"
                                Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="doubleValidator4" ControlToValidate="txtRKBElevation" ForeColor="Red"
                                Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <telerik:RadGrid ID="RadGridCurves" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added.">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" AllowFiltering="false" HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Curve ID" DataField="ID" UniqueName="ID"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Group ID" DataField="CurveGroupID" UniqueName="CurveGroupID"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve #" DataField="Number" UniqueName="Number"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Name" DataField="Name" UniqueName="Name"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Type" DataField="CurveTypeName" UniqueName="CurveTypeName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="North Offset" DataField="NorthOffset" UniqueName="NorthOffset"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="East Offset" DataField="EastOffset" UniqueName="EastOffset"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="VS Direction" DataField="VSDirection" UniqueName="VSDirection"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RKB Elevation" DataField="RKBElevation" UniqueName="RKBElevation"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>
