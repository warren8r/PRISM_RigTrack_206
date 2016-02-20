<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="confMngrManageAssets.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageAssets" %>

<asp:Content ID="headCss" runat="server" ContentPlaceHolderID="customCss">
    <style type="text/css">
        body .gis-form
        {
            width: 450px;
            border: 1px solid black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lbltest" runat="server" CssClass="breadcrumbs">Configuration Manager » Manage <%= AssetName %></asp:Label>
    <div class="star">(<span class="star">*</span>) indicates mandatory fields</div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadNotification ID="radnotMessage" runat="server" Text="Initial text" Position="BottomRight"
            AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
            EnableRoundedCorners="true">
        </telerik:RadNotification>
        <telerik:RadNotification ID="n1" runat="server" Text="Initial text" Position="BottomRight"
            AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
            EnableRoundedCorners="true">
        </telerik:RadNotification>
        <asp:ValidationSummary ID="valgValidationSummary" runat="server" DisplayMode="List" Font-Size="Small" ForeColor="Red" />
        <asp:Panel ID="pnlTop" runat="server" Width="925px" BorderColor="#618740" BorderWidth="1px">
            <h3 style="margin: 3px;">
                <asp:Label ID="lblPageMode" runat="server"></asp:Label>&nbsp;<%=AssetName %></h3>
            <table style="margin: 3px 0px 3px; 0px;">
                <tr>
                    <td>
                        Status:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActive" runat="server" Width="20px" Checked="true" CausesValidation="false"
                            AutoPostBack="true" OnCheckedChanged="chkActive_CheckedChanged" ToolTip="Active/In-Active" />
                        <asp:Label ID="lblActive" runat="server" Text="Active" Width="90px"></asp:Label>
                    </td>
                    <td>
                        Name: <span class="star">*</span>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtAssetName" runat="server">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="AssetNameRequiredFieldValidator" runat="server"
                            ErrorMessage="Asset Name must not be blank" ControlToValidate="txtAssetName"
                            SetFocusOnError="true" Display="None" />
                    </td>
            </table>
            <table style="border: 0; margin: 3px;" cellpadding="2px">
                <tr>
                    <td style="vertical-align: top; width: 50%;">
                        <!-- start primary contact -->
                        <fieldset class="gis-form">
                            <legend><%# AssetName %> Service Address</legend>
                            <table style="border-width: 0px; margin: 5px 0px 5px 0px; width: 100%;" cellpadding="3px"
                                border="0">
                                <tr>
                                    <td width="100px">
                                        Address 1: <span class="star">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryAddress1" runat="server" LabelWidth="64px" Width="120px" />
                                        <asp:RequiredFieldValidator ID="PrimaryAddress1RequiredFieldValidator" runat="server"
                                            ErrorMessage="Service Address - Address 1 must not be blank" ControlToValidate="txtPrimaryAddress1"
                                            SetFocusOnError="true" Display="None" />
                                    </td>
                                    <td>
                                        Country:
                                    </td>
                                    <td>
                                        <telerik:RadDropDownList ID="ddlPrimaryCountry" runat="server" AutoPostBack="True" DefaultMessage="-Select-"
                                            CausesValidation="False" DropDownHeight="200px" DropDownWidth="180px"
                                            DataSourceID="sqldsPrimaryCountryCode" DataTextField="name" DataValueField="code" OnSelectedIndexChanged="ddlPrimaryCountry_SelectedIndexChanged">
                                        </telerik:RadDropDownList>
                                        <asp:SqlDataSource ID="sqldsPrimaryCountryCode" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                            SelectCommand="SELECT code, name FROM countryCode ORDER BY name">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        Address 2:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryAddress2" runat="server" LabelWidth="64px" Width="120px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPrimaryStateProv" runat="server" Text="State: ">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlPrimaryUS" runat="server">
                                            <telerik:RadDropDownList ID="ddlPrimaryState" runat="server" DefaultMessage="-Select-"
                                                CausesValidation="false" DropDownHeight="200px" DropDownWidth="180px"
                                                DataSourceID="sqldsPrimaryStateCode" DataTextField="name" DataValueField="code">
                                            </telerik:RadDropDownList>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPrimaryNonUS" runat="server" Visible="false">
                                            <telerik:RadTextBox ID="txtPrimaryProvince" runat="server" LabelWidth="64px" Width="160px">
                                            </telerik:RadTextBox>
                                        </asp:Panel>
                                        <asp:SqlDataSource ID="sqldsPrimaryStateCode" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                            SelectCommand="SELECT code, name FROM stateCode ORDER BY name">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style5" valign="top">
                                        City: <span class="star">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryCity" runat="server" LabelWidth="64px" Width="120px" />
                                        <asp:RequiredFieldValidator ID="PrimaryCityRequiredFieldValidator" runat="server"
                                            ErrorMessage="Service Address - City must not be blank" ControlToValidate="txtPrimaryCity"
                                            SetFocusOnError="true" Display="None" />
                                    </td>
                                    <td>
                                        Zip/Postal:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryPostalCode" runat="server" LabelWidth="30px" Width="75px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style5" valign="top">
                                        GIS:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryLatLong" runat="server" EmptyMessage=" Input address.."
                                            Height="20px" LabelWidth="64px" Style="padding: 0px;" Width="120px" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="lnkbtnPrimaryGatherGIS" runat="server" ButtonType="StandardButton"
                                            CausesValidation="false" SingleClick="true" SingleClickText="Please wait..."
                                            Style="top: 3px;" Text="Gather GIS" OnClick="lnkbtnPrimaryGatherGIS_Click" />
                                        <asp:HiddenField ID="hdnPrimaryLatLongAccuracy" runat="server" />
                                        <asp:HyperLink ID="hypPrimaryMapLink" runat="server" NavigateUrl='<%# Eval("primaryLatLong", "http://maps.google.com/?q={0}") %>'
                                            Style="color: Blue;" Target="_new" Visible="False">View Map
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        First: <span class="star">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryFirst" runat="server" LabelWidth="48px" Width="120px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrimaryFirst"
                                            ErrorMessage="Service Address - First Name must not be blank" SetFocusOnError="true"
                                            Display="None" />
                                    </td>
                                    <td>
                                        Last: <span class="star">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryLast" runat="server" LabelWidth="48px" Width="120px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrimaryLast"
                                            ErrorMessage="Service Address - Last Name must not be blank" SetFocusOnError="true" Display="None" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone: <span class="star">*</span>
                                    </td>
                                    <td style="text-align: left">
                                        <telerik:RadTextBox ID="txtPrimaryPhone1" runat="server" LabelWidth="48px" Width="120px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPrimaryPhone1"
                                            ErrorMessage="Service Address - Phone must not be blank" SetFocusOnError="true" Display="None" />
                                    </td>
                                    <td>
                                        Fax:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPrimaryPhone2" runat="server" LabelWidth="48px" Width="120px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email:
                                    </td>
                                    <td colspan="3" style="text-align: left">
                                        <telerik:RadTextBox ID="txtPrimaryEmail" Width="220px" runat="server" LabelWidth="88px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <!-- end primary contact -->
                    </td>
                    <td style="vertical-align: top; width: 50%;">
                        <!-- secondary -->
                        <fieldset class="gis-form">
                            <legend>Business Contact</legend>
                            <table style="border-width: 0px; margin: 5px 0px 5px 0px; width: 100%;" cellpadding="3px">
                                <tr>
                                    <td valign="top" width="76px">
                                        Address 1:
                                    </td>
                                    <td valign="top">
                                        <telerik:RadTextBox ID="txtSecondaryAddress1" runat="server" LabelWidth="64px" Width="120px" />
                                    </td>
                                    <td>
                                        Country:
                                    </td>
                                    <td>
                                        <telerik:RadDropDownList ID="ddlSecondaryCountry" runat="server" AutoPostBack="True"
                                            CausesValidation="false" DropDownHeight="200px" DropDownWidth="180px"
                                            DataSourceID="sqldsSecondaryCountryCode" DataTextField="name" DataValueField="code" 
                                            OnSelectedIndexChanged="ddlSecondaryCountry_SelectedIndexChanged">
                                        </telerik:RadDropDownList>
                                        <asp:SqlDataSource ID="sqldsSecondaryCountryCode" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                            SelectCommand="SELECT code, name FROM countryCode ORDER BY name">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="style5">
                                        Address 2:
                                    </td>
                                    <td valign="top">
                                        <telerik:RadTextBox ID="txtSecondaryAddress2" runat="server" LabelWidth="64px" Width="120px" />
                                    </td>
                                    <td>
                                        State : &nbsp;
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlSecondaryUS" runat="server">
                                            <telerik:RadDropDownList ID="ddlSecondaryState" runat="server" DefaultMessage="-Select-"
                                                CausesValidation="False" DropDownHeight="200px" DropDownWidth="180px"
                                                DataSourceID="sqldsSecondaryStateCode" DataTextField="name" DataValueField="code">
                                            </telerik:RadDropDownList>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSecondaryNonUS" runat="server" Visible="false">
                                            <telerik:RadTextBox ID="txtSecondaryProvince" runat="server" LabelWidth="64px" Width="160px">
                                            </telerik:RadTextBox>
                                        </asp:Panel>
                                        <asp:SqlDataSource ID="sqldsSecondaryStateCode" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                            SelectCommand="SELECT code, name FROM stateCode ORDER BY name">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style5" valign="top">
                                        City :
                                    </td>
                                    <td valign="top">
                                        <telerik:RadTextBox ID="txtSecondaryCity" runat="server" LabelWidth="64px" Width="120px" />
                                    </td>
                                    <td colspan="1" valign="top">
                                        Zip/Postal:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSecondaryPostalCode" runat="server" LabelWidth="30px"
                                            Width="75px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        &nbsp;
                                    </td>
                                    <td colspan="2" valign="top">
                                        <asp:CheckBox ID="chkSecSameAsPrimAddress" runat="server" AutoPostBack="true" Font-Names="Arial"
                                            Font-Size="10px" Text="Same As Primary Address" OnCheckedChanged="chkSecSameAsPrimAddress_CheckedChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        First :&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSecondaryFirst" runat="server" LabelWidth="48px" Width="120px" />
                                    </td>
                                    <td>
                                        Last :&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSecondaryLast" runat="server" LabelWidth="48px" Width="120px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSecondaryPhone1" runat="server" LabelWidth="48px" Width="120px" />
                                    </td>
                                    <td>
                                        Fax:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSecondaryPhone2" runat="server" LabelWidth="48px" Width="120px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email:
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txtSecondaryEmail" Width="220px" runat="server" LabelWidth="88px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <!-- end secondary -->
                    </td>
                </tr>
            </table>
            <table style="margin: 3px 0px 3px 0px;">
                <tr>
                    <td>
                        <telerik:RadButton ID="btnSaveAsset" runat="server" Text="Save" ToolTip="Save meter"
                            OnClientClicked="ConfirmSave" OnClick="btnSaveAsset_Click">
                        </telerik:RadButton>
                    </td>
                    <td>
                        <telerik:RadButton ID="btnCancelAsset" runat="server" Text="Reset" CausesValidation="False"
                            OnClick="btnCancelAsset_Click">
                        </telerik:RadButton>
                    </td>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" CssClass="star">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField ID="hdnAssetType" runat="server" />
        <asp:HiddenField ID="hdnAssetName" runat="server" />
        <asp:HiddenField ID="hdnSelectedAssetID" runat="server" />
        <telerik:RadGrid ID="grdAssetList" runat="server" AllowSorting="True" Width="925px"
            AutoGenerateColumns="False" CellSpacing="0" GridLines="None" OnItemDataBound="grdAssetList_ItemDataBound"
            OnItemCommand="grdAssetList_ItemCommand" OnPageIndexChanged="grdAssetList_PageIndexChanged"
            AllowPaging="True" onneeddatasource="grdAssetList_NeedDataSource">
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
            <MasterTableView DataKeyNames="id" CommandItemDisplay="None">
                <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                    <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="ID" Display="false" UniqueName="ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="bitActive" FilterControlAltText="Filter Active column"
                        HeaderText="Status" SortExpression="bitActivbe" UniqueName="bitActive">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("bitActive") %>' Enabled="false" />
                            <asp:Label ID="lblActive" runat="server" Text='<%# Convert.ToBoolean(Eval("bitActive")) == true ? "Active" : "In-Active" %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn FilterControlAltText="Filter EditButton column" 
                        Text="Edit" UniqueName="EditAsset" CommandName="EditAsset">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column"
                        HeaderText="Name" SortExpression="Name" UniqueName="Name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primFirstName" FilterControlAltText="Filter primFirstName column"
                        HeaderText="First" SortExpression="primFirstName" UniqueName="primFirstName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primLastName" FilterControlAltText="Filter primLastName column"
                        HeaderText="Last" SortExpression="primLastName" UniqueName="primLastName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primAddress1" FilterControlAltText="Filter primAddress1 column"
                        HeaderText="Address1" SortExpression="primAddress1" UniqueName="primAddress1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primAddress2" FilterControlAltText="Filter primAddress2 column"
                        HeaderText="Address2" SortExpression="primAddress2" UniqueName="primyAddress2">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primCity" FilterControlAltText="Filter primCity column"
                        HeaderText="City" SortExpression="primCity" UniqueName="primCity">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primStateName" FilterControlAltText="Filter State column"
                        HeaderText="State" SortExpression="primStateName" UniqueName="primStateName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primCountryName" FilterControlAltText="Filter Country column"
                        HeaderText="Country" SortExpression="primCountryName" UniqueName="primCountryName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primPostalCode" FilterControlAltText="Filter primPostalCode column"
                        HeaderText="Post Cd" SortExpression="primyPostalCode" UniqueName="primPostalCode">
                    </telerik:GridBoundColumn>
                    <telerik:GridHyperLinkColumn AllowSorting="False" DataNavigateUrlFields="primLatLong"
                        DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                        Target="_new" Text="View Map" UniqueName="gisLink">
                        <ItemStyle ForeColor="Blue" />
                    </telerik:GridHyperLinkColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <ItemStyle VerticalAlign="Top" />
                <AlternatingItemStyle VerticalAlign="Top" />
                <PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True" />
            </MasterTableView>
            <PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True"  
                PageSizes="10,20,30,40" />
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
