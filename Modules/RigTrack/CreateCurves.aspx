<%@ Page Title="Create Curves" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateCurves.aspx.cs" Inherits="Modules_RigTrack_CreateCurves" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Manage Curves</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        Company: 
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Job/Curve Group:
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Target:
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCompany" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="160px" AppendDataBoundItems="true" DropDownWidth="200px" DropDownHeight="200px" OnSelectedIndexChanged="ddlCurveGroup_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlTarget" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" Enabled="false" OnSelectedIndexChanged="ddlTarget_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <%--<asp:TableCell>
                                        <asp:Button ID="btnSearchCurves" runat="server" OnClick="btnSearchCurves_Click" Text="Search" Enabled="false" />
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                 

                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <h4>New Curve Entry</h4>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <font color="Black"> <b>Curve Name:</b> </font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCurveName" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CreateCurve"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="Black"> <b>Curve Type:</b></font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlCurveType" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CreateCurve"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="Black"><b>North Offset:</b></font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNorthOffset" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CreateCurve"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="Black"> <b>East Offset:</b></font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEastOffset" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CreateCurve"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="Black"> <b>VS Direction:</b></font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtVSDirection" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CreateCurve"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="Black"> <b>RKB Elevation:</b></font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRKBElevation" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CreateCurve"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <font color="Black"> <b>Curve Color:</b></font>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtCurveName" Width="160px"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList runat="server" ID="ddlCurveType" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" DefaultMessage="-Select-">
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtNorthOffset" Width="160px" Text="0.00"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtEastOffset" Width="160px" Text="0.00"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtVSDirection" Width="160px" Text="0.00"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtRKBElevation" Width="160px" Text="0.00"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadColorPicker runat="server" ID="RadColorPicker1" PaletteModes="WebPalette" KeepInScreenBounds="true" ShowIcon="true" ShowEmptyColor="false" SelectedColor="Black" ></telerik:RadColorPicker>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnClear" Text="Clear" Width="160px" OnClick="btnClear_Click" CausesValidation="false"/>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnCreate" Text="Create" Width="160px" OnClick="btnCreate_Click" Enabled="false" ValidationGroup="CreateCurve"/>
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

                            

                <%--<telerik:RadGrid ID="RadGridCurves" runat="server" PageSize="30" OnNeedDataSource="RadGridCurves_NeedDataSource"></telerik:RadGrid>--%>

                <telerik:RadGrid ID="RadGridCurves" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30" OnItemCreated="RadGridCurves_ItemCreated"
                    OnNeedDataSource="RadGridCurves_NeedDataSource" OnUpdateCommand="RadGridCurves_UpdateCommand" OnItemDataBound="RadGridCurves_ItemDataBound" OnItemCommand="RadGridCurves_ItemCommand">
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <ExportSettings ExportOnlyData="true">
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView EditMode="InPlace" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToCsvButton="true" ShowExportToWordButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="LinkButton" HeaderText="Edit"></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderText="Curve ID" ReadOnly="true" DataField="ID" UniqueName="ID"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Group ID" ReadOnly="true" DataField="CurveGroupID" UniqueName="CurveGroupID" ></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Target ID" ReadOnly="true" DataField="TargetID" UniqueName="TargetID"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TargetName" UniqueName="TargetName" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Target Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblTargetName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TargetName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList ID="ddlTargetName" runat="server"></telerik:RadDropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Curve #" ReadOnly="true" DataField="Number" UniqueName="Number"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Name" DataField="CurveName" UniqueName="CurveName">
                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="*"></RequiredFieldValidator>
                                </ColumnValidationSettings>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Type" DataField="CurveTypeName" UniqueName="CurveTypeName" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CurveType" HeaderText="Curve Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurveType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurveTypeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList ID="ddlCurveType" runat="server"></telerik:RadDropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="North Offset" DataField="NorthOffset" UniqueName="NorthOffset">
                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="*"></RequiredFieldValidator>
                                </ColumnValidationSettings>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="East Offset" DataField="EastOffset" UniqueName="EastOffset">
                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="*"></RequiredFieldValidator>
                                </ColumnValidationSettings>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="VS Direction" DataField="VSDirection" UniqueName="VSDirection">
                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="*"></RequiredFieldValidator>
                                </ColumnValidationSettings>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RKB Elevation" DataField="RKBElevation" UniqueName="RKBElevation">
                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="*"></RequiredFieldValidator>
                                </ColumnValidationSettings>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Color" DataField="Color" UniqueName="Color" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Color">
                                <ItemTemplate>
                                    <asp:Label ID="lblColor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Color") %>' Visible="false"></asp:Label>
                                    <telerik:RadColorPicker runat="server" ID="gridColorPickerDisabled" PaletteModes="WebPalette" KeepInScreenBounds="true" ShowIcon="true" ShowEmptyColor="false" Enabled="false"></telerik:RadColorPicker>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadColorPicker runat="server" ID="gridColorPicker" PaletteModes="WebPalette" KeepInScreenBounds="true" ShowIcon="true" ShowEmptyColor="false"></telerik:RadColorPicker>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="RadGridCurves" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </fieldset>
    <div style="text-align: center;" class="DivFooter">
        <hr style="width: 777px" />
        Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
    </div>
</asp:Content>
