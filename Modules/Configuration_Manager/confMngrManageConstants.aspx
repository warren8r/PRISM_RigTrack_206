<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="confMngrManageConstants.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageConstants" %>

<%@ Register TagPrefix="tabSaveLabel" TagName="confgMngrTabSaveLabel" Src="~/controls/confPreferences/confSDPHierarchyLabels.ascx" %>
<%@ Register TagPrefix="tabConstantsBoolean" TagName="confgConstantsBoolean" Src="~/controls/confPreferences/confDayLightSavings.ascx" %>
<%@ Register TagPrefix="tabautoModeVee" TagName="confgautoModeVee" Src="~/controls/confPreferences/autoModeVee.ascx" %>
<%@ Register TagPrefix="tabconfEmailFreq" TagName="confEmailFreq" Src="~/controls/confPreferences/confEmailFreq.ascx" %>
<%@ Register TagPrefix="tabconfGrid" TagName="confGrid" Src="~/controls/confPreferences/confGrid.ascx" %>
<%@ Register TagPrefix="tabconfProject" TagName="confProject" Src="~/controls/confPreferences/confProjects.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
    <telerik:RadAjaxPanel ID="fullUpdatePage" LoadingPanelID="loading" runat="server">
        <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="all" runat="server"
            DecorationZoneID="ZoneID1" Skin="Vista"></telerik:RadFormDecorator>
        <div id="ZoneID1 tabPage">
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black" MultiPageID="RadMultiPage1"
                AutoPostBack="True" Style="padding: 20px 0px 0px 10px;" SelectedIndex="1" CssClass="tabStrip"
                >
                <Tabs>
                    <%--<telerik:RadTab Text="Create Constant" Selected="True">
                </telerik:RadTab>--%>
                    <%--<telerik:RadTab Text="View / Edit Constants" PageViewID="editEvent">
                </telerik:RadTab>--%>
                    <%--<telerik:RadTab Text="Dropdown Editor">
                </telerik:RadTab>--%>
                    <telerik:RadTab Text="Event Option" PageViewID="RadPageView2" Selected="True">
                    </telerik:RadTab>
                 <%--   <telerik:RadTab Text="Data Aggregators" PageViewID="dataAggregation">
                    </telerik:RadTab>--%>
                    <telerik:RadTab Text="Daylight Savings" PageViewID="dataDaylightSavings">
                    </telerik:RadTab>
                    <%--<telerik:RadTab Text="Auto-Mode for Data Migration/VEE" PageViewID="dataVEE">
                    </telerik:RadTab>--%>
                    <telerik:RadTab Text="Auto-Email Listener Frequency" PageViewID="emailFreq">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Grid Preferences" PageViewID="RadPagEconfGrid">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Projetcs" PageViewID="RadPagProjetcs">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <div id="tabs-1" align="">
                <fieldset class="content-container" style="min-height: 300px;">
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="1" CssClass="multiPage">
                        
                        <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" Selected="true">
                            <telerik:RadAjaxPanel ID="addEditEvents" runat="server">
                                <telerik:RadTabStrip ID="RadTabStrip_add_edit_events" runat="server" MultiPageID="addAndEditEvent_page"
                                    AutoPostBack="True" Style="padding: 20px 0px 0px 10px;" SelectedIndex="1" 
                                    Skin="Office2010Black">
                                    <Tabs>
                                        <telerik:RadTab Text="Edit Flags" Selected="true">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="Create Flags">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <fieldset class="content-container">
                                    <telerik:RadMultiPage ID="addAndEditEvent_page" runat="server" SelectedIndex="1"
                                        CssClass="multiPage">
                                        <telerik:RadPageView ID="editEvent" runat="server" Width="100%" Selected="true">
                                            <telerik:RadAjaxPanel runat="server" ID="UpdatePanel_edit_flag">
                                                <telerik:RadGrid ID="radListSelect" runat="server" DataSourceID="editSaveEvent" AllowAutomaticDeletes="True"
                                                    AutoGenerateColumns="False" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                                    AllowPaging="True" CellSpacing="0" GridLines="None" ShowStatusBar="True" EnableHeaderContextAggregatesMenu="True"
                                                    EnableHeaderContextMenu="True" OnItemDataBound="radListSelect_ItemDataBound"
                                                    OnItemCommand="radListSelect_ItemCommand">
                                                    <ExportSettings OpenInNewWindow="True">
                                                    </ExportSettings>
                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="editSaveEvent"
                                                        EnableHeaderContextAggregatesMenu="True" AllowAutomaticUpdates="true">
                                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                            <HeaderStyle Width="20px" />
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                            <HeaderStyle Width="20px" />
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="flagName" FilterControlAltText="Filter flagName column"
                                                                HeaderText="Flag name" SortExpression="flagName" UniqueName="flagName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="flagColor" FilterControlAltText="Filter flagColor column"
                                                                HeaderText="Flag color" SortExpression="flagColor" UniqueName="flagColor">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridEditCommandColumn FilterControlAltText="Filter EditCommandColumn column">
                                                            </telerik:GridEditCommandColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                            </EditColumn>
                                                            <FormTemplate>
                                                                <div class="form">
                                                                    <div class="input">
                                                                        <span class="label">Flag Name</span>
                                                                        <div class="input">
                                                                            <telerik:RadTextBox ID="flagName" runat="server" Text='<%# Eval( "flagName" ) %>'
                                                                                LabelWidth="64px" Width="160px">
                                                                            </telerik:RadTextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="input">
                                                                        <span class="label">Flag Color</span>
                                                                        <div class="input">
                                                                            <telerik:RadColorPicker runat="server" ID="flagColor" PaletteModes="WebPalette" ShowEmptyColor="false"
                                                                                CssClass="ColorPickerPreview" KeepInScreenBounds="true" ShowIcon="true">
                                                                            </telerik:RadColorPicker>
                                                                            <asp:Label ID="tmpColor" Text='<%# Eval("flagColor") %>' runat="server" Visible="false" />
                                                                            <asp:Label ID="flagId" Text='<%# Eval("id") %>' runat="server" Visible="false" />
                                                                        </div>
                                                                    </div>
                                                                    <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                        runat="server" CommandName="Update"></asp:Button>
                                                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                        CommandName="Cancel"></asp:Button>
                                                                </div>
                                                            </FormTemplate>
                                                        </EditFormSettings>
                                                        <PagerStyle PageSizeControlType="RadComboBox" />
                                                    </MasterTableView>
                                                    <PagerStyle PageSizeControlType="RadComboBox" />
                                                    <FilterMenu EnableImageSprites="False">
                                                    </FilterMenu>
                                                </telerik:RadGrid>
                                            </telerik:RadAjaxPanel>
                                            <asp:SqlDataSource ID="editSaveEvent" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                                                DeleteCommand="DELETE FROM [flag] WHERE [id] = @id" InsertCommand="INSERT INTO [flag] ( [flagName], [flagColor]) VALUES ( @flagName, COALESCE( @flagColor, '#fff' ))"
                                                SelectCommand="SELECT [id], [flagName], COALESCE( [flagColor], '#ffffff' ) as flagColor FROM [flag]"
                                                UpdateCommand="UPDATE [flag] SET [flagName] = @flagName, [flagColor] = @flagColor WHERE [id] = @id">
                                            </asp:SqlDataSource>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="addEvent" runat="server" Width="100%">
                                            <form style="float: left; text-align: left;">
                                            <div class="input">
                                                <span class="label">Flag Name</span>
                                                <div class="input">
                                                    <telerik:RadTextBox ID="newFlagName" runat="server" Text="" />
                                                </div>
                                            </div>
                                            <div class="input">
                                                <span class="label">Flag color</span>
                                                <div class="input">
                                                    <telerik:RadColorPicker runat="server" ID="newFlagColor" PaletteModes="WebPalette"
                                                        ShowEmptyColor="false" CssClass="ColorPickerPreview" KeepInScreenBounds="true"
                                                        ShowIcon="true" Text='<%# Eval( "flagColor" ) %>'>
                                                    </telerik:RadColorPicker>
                                                </div>
                                            </div>
                                            <div class="input">
                                                <telerik:RadButton ID="btnSaveFlag" runat="server" OnClick="saveFlag" Text="Save" />
                                            </div>
                                            </form>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="pageEventType" runat="server">
                                            <legend title="Event Type"></legend>
                                            <form>
                                            <h1>
                                                Event type</h1>
                                            </form>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </fieldset>
                            </telerik:RadAjaxPanel>
                        </telerik:RadPageView>
                       <%-- <telerik:RadPageView ID="dataAggregation" runat="server">
                            <legend id="Legend1" title="Meter Data Aggregation Labels"></legend>
                            <tabSaveLabel:confgMngrTabSaveLabel runat="server" ID="tabSaveLabel" />
                        </telerik:RadPageView>--%>
                        <telerik:RadPageView ID="dataDaylightSavings" runat="server">
                            <legend id="lgndDaylightSave" title="Daylight Savings"></legend>
                            <tabConstantsBoolean:confgConstantsBoolean runat="server" ID="confgConstantsBoolean" />
                        </telerik:RadPageView>                        
                        <%--<telerik:RadPageView ID="dataVEE" runat="server">
                            <legend id="Legend2" title="Auto-Mode for Data Migration/ VEE"></legend>
                            <tabautoModeVee:confgautoModeVee runat="server" ID="confgautoModeVee" />
                        </telerik:RadPageView>--%>
                        <telerik:RadPageView ID="emailFreq" runat="server">
                            <legend id="Legend3" title="Auto-Email Listener Frequency"></legend>
                            <tabconfEmailFreq:confEmailFreq runat="server" ID="confEmailFreq" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPagEconfGrid" runat="server">
                            <legend id="Legend4" title="Auto-Email Listener Frequency"></legend>
                            <tabconfGrid:confGrid runat="server" ID="confGrid" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPagProjetcs" runat="server">
                            <legend id="Legend5" title="Auto-Email Listener Frequency"></legend>
                             <tabconfProject:confProject runat="server" ID="confProjects" />
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </fieldset>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
