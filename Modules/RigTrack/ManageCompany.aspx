<%@ Page Title="Manage Company" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageCompany.aspx.cs" Inherits="Modules_RigTrack_ManageCompany" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; text-align:center; height:100%; width:100%; top: 0; right:0; left:0; padding-top:15%; z-index: 9999999;">
                <div class="loader2">Loading...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        function Close() {
            GetRadWindow().close();
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;
        }
    </script>
    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Manage Company</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                
                <asp:Table ID="table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="table3" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        <font style="color:#f5c739 !important;">Company Name</font>
                                        <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Company Address 1 <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Company Address 2
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        <font style="color:#f5c739!important;">City</font>
                                        <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Country <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        <font style="color:#f5c739 !important;">State</font>
                                        <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Zip Code <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtCompanyName" runat="server"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtCompanyAddressOne" runat="server"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCompanyAddressOne" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtCompanyAddressTwo" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtCity" runat="server"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" Width="210px" DropDownHeight="200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  ControlToValidate="ddlCountry" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                       
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" Width="160px" DropDownHeight="200px" Enabled="false">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlState" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtZipcode" Width="164px" runat="server"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtZipcode" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        Contact First Name <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Contact Last Name <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Contact Number <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Email <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Upload Logo
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <b>Status</b> 
                                        <asp:RadioButton runat="server" ID="rbActive" Text="Active" Checked="true" GroupName="Status" />
                                        <asp:RadioButton runat="server" ID="rbInactive" Text="Inactive" GroupName="Status" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:button ID="btnSearch" runat="server" CssClass="button-searchOrange" ForeColor="Black"  Text="Search" OnClick="btnSearch_Click" ToolTip="Search by Company Name, City, or State" Width="170px" />
                                    </asp:TableCell>

                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtContactFirstName" runat="server"></telerik:RadTextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactFirstName" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtContactLastName" runat="server"></telerik:RadTextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContactLastName" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtContactNumber" runat="server"></telerik:RadTextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtEmail" runat="server"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="valGroup"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadAsyncUpload runat="server" ID="AttachmentUpload" AllowedFileExtensions=".gif,.jpg,.jpeg,.png" MultipleFileSelection="Automatic" Width="210px" PostbackTriggers="btnCreate"></telerik:RadAsyncUpload>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnClear" Text="Clear" Width="160px" OnClick="btnClear_Click" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnCreate" Text="Create" Width="164px" OnClick="btnCreate_Click" ValidationGroup="valGroup" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <telerik:RadGrid ID="RadGridCompany" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="15" OnItemCreated="RadGridCompany_ItemCreated"
                     OnNeedDataSource="RadGridCompany_NeedDataSource" OnUpdateCommand="RadGridCompany_UpdateCommand" OnItemDataBound="RadGridCompany_ItemDataBound" OnItemCommand="RadGridCompany_ItemCommand">
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
                            <telerik:GridBoundColumn HeaderText="Company ID" ReadOnly="true" DataField="ID" UniqueName="ID"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Company Name" DataField="CompanyName" UniqueName="CompanyName">
                                <ColumnValidationSettings RequiredFieldValidator-Enabled="true">
                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="*"></RequiredFieldValidator>
                                </ColumnValidationSettings>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Company Address 1" DataField="CompanyAddress1" UniqueName="CompanyAddress1">
                                <ColumnValidationSettings RequiredFieldValidator-Enabled="true">
                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="*"></RequiredFieldValidator>
                                </ColumnValidationSettings>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Company Address 2" DataField="CompanyAddress2" UniqueName="CompanyAddress2"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="City" DataField="City" UniqueName="City"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StateName" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="State" HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StateName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" Width="160px" DropDownHeight="200px">
                                        <Items>
                                            <telerik:DropDownListItem Value="0" Text="--Select--" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="CountryName" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Country" HeaderText="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CountryName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" Width="160px" DropDownHeight="200px">
                                        <Items>
                                            <telerik:DropDownListItem Value="0" Text="--Select--" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Zip Code" DataField="Zip" UniqueName="Zip"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Contact First Name" DataField="CompanyContactFirstName" UniqueName="CompanyContactFirstName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Contact Last Name" DataField="CompanyContactLastName" UniqueName="CompanyContactLastName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Contact Number" DataField="ContactPhone" UniqueName="ContactPhone"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Email" DataField="ContactEmail" UniqueName="ContactEmail"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="isAttachment" UniqueName="isAttachment" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Uploaded Logo:" UniqueName="isAttachmentTemplate">
                                <ItemTemplate>
                                    <asp:Label ID="lblAttachment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "isAttachment") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadAsyncUpload runat="server" ID="GridAttachmentUpload" AllowedFileExtensions=".gif,.jpg,.jpeg,.png" MultipleFileSelection="Automatic" Width="210px"></telerik:RadAsyncUpload>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Active" DataField="isActive" UniqueName="isActive" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Active" UniqueName="isActiveTemplate">
                                <ItemTemplate>
                                    <asp:Label ID="lblActive" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "isActive") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:RadioButton runat="server" ID="rbGridActive" Text="Active" GroupName="GridStatus" />
                                    <asp:RadioButton runat="server" ID="rbGridInactive" Text="Inactive" GroupName="GridStatus" />
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    
                    </MasterTableView>

                </telerik:RadGrid>
                <div style="display: block; float: right;">
                   <asp:Button ID="btnClose" runat="server" CssClass="button-CloseRed" Width="60px" ForeColor="Black" Text="Close" OnClientClick="Close(); return false;" Visible="false"/>
                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <div style="text-align: center;" class="DivFooter">
        <hr style="width: 777px" />
        Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
    </div>



</asp:Content>