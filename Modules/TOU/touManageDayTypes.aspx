<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="touManageDayTypes.aspx.cs" Inherits="Modules_TOU_touManageDayTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
<script src="../../js/jquery-1.9.1.js"></script>
<script src="../../js/jquery-ui-1.10.2.custom.js"></script>
<script src="../../js/javascript_Sean.js"></script>
<link rel="stylesheet" href="../../css/jquery-ui-1.10.2.custom.css" />
<link rel="stylesheet" href="../../css/style_Sean.css" />

<%--<script>
    $(function () {
        var availableTypes = <%= getTypes() %>
        var availableProgs = <%= getProgs() %>
        populateAutoComplete('<%= txtProgType.ClientID %>', availableTypes);
        populateAutoComplete('<%= txtProgName.ClientID %>', availableProgs);
    });
</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadAjaxPanel runat="server">
        <fieldset style="width: 830px;height: 265px; margin:10px;">
        <legend>Time of Use >> Manage Day Types</legend>
            <%--<table>
                <tr>
                    <td><telerik:RadTextBox ID="txtProgType" runat="server" Label="Program Type:" CssClass="RTBTextBox"  LabelCssClass="RTBLabel" AutoPostBack="true" ontextchanged="txtProgType_TextChanged" /></td>
                    <td><telerik:RadTextBox ID="txtProgName" runat="server" Label="Program Name:" CssClass="RTBTextBox"  LabelCssClass="RTBLabel" Enabled="false" AutoPostBack="true" ontextchanged="txtProgName_TextChanged" /></td>
                    <td>
                        Season:<br />
                        <div runat="server" ID="div_selectSeason">
                            <telerik:RadDropDownList ID="ddl_selectSeason" runat="server" Width="160px" AutoPostBack="true">
                                <Items><telerik:DropDownListItem Text="N/A" Value="0" /></Items>
                            </telerik:RadDropDownList>
                        </div>
                    </td>
                    <td>
                        Season Name:
                        <div runat="server" ID="div_selectSeason">
                            <telerik:RadComboBox ID="ddl_selectSeason" runat="server" Width="150px" Height="140px" CausesValidation="false"
                                                    EmptyMessage="Please select" DataSourceID="SqlDataSource2" AutoPostBack="true"
                                                    DataTextField="seasonName" DataValueField="ID" MarkFirstMatch="true">
                            </telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                                ConnectionString="<%$ databaseExpression:client_database %>" 
                                                SelectCommand="SELECT [ID], [seasonName] FROM [touSeasons] WHERE ([isActive] = @isActive)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="true" Name="isActive" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </td>
                </tr>
            </table>
            <br />--%>

                <fieldset style="width: 650px;height: 115px;">
                    <table width="100%">
                    <tr>
                        <td style="border-right:thin solid #000000;">
                            <telerik:RadTextBox ID="txtDayDefName" runat="server" Label="Day Definition Name:" CssClass="RTBTextBox"  LabelCssClass="RTBLabel" /><br /><br />
                            <asp:RadioButtonList ID="rbl_defineType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_defineType_SelectedIndexChanged">
                                <asp:ListItem Text="Specify Days"  Value="0" Selected="True" />
                                <asp:ListItem Text="Date Range"    Value="1" />
                                <asp:ListItem Text="Specific Date" Value="2" />
                            </asp:RadioButtonList>
                        </td>

                        <td>
                            <telerik:RadMultiPage ID="rmp_definiteType" runat="server" SelectedIndex="0">

                                <telerik:RadPageView ID="RadPageView1" runat="server" Width="450px">
                                    <asp:CheckBoxList ID="cbl_DaysofWeek" runat="server" Width="450px" Height="75px" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4">
                                        <asp:ListItem Text="Monday"></asp:ListItem>
                                        <asp:ListItem Text="Tuesday"></asp:ListItem>
                                        <asp:ListItem Text="Wednesday"></asp:ListItem>
                                        <asp:ListItem Text="Thursday"></asp:ListItem>
                                        <asp:ListItem Text="Friday"></asp:ListItem>
                                        <asp:ListItem Text="Saturday"></asp:ListItem>
                                        <asp:ListItem Text="Sunday"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </telerik:RadPageView>

                                <telerik:RadPageView ID="RadPageView2" runat="server" Width="450px">
                                    <table style="margin:0px auto;">
                                        <tr>
                                            <td>Start Date:<br /><telerik:RadDatePicker ID="rdp_start" runat="server" /></td>
                                            <td>End Date:<br /><telerik:RadDatePicker ID="rdp_end" runat="server" /></td>
                                        </tr>
                                    </table>
                                </telerik:RadPageView>

                                <telerik:RadPageView ID="RadPageView3" runat="server" Width="450px">
                                    <table style="margin:0px auto;">
                                        <tr>
                                            <td>Select Date:<br /><telerik:RadDatePicker ID="rdp_specifydate" runat="server" /></td>
                                        </tr>
                                    </table>
                                </telerik:RadPageView>

                            </telerik:RadMultiPage>
                        </td>
                    </tr>
                </table>
            </fieldset>

            <table>
                <tr>
                    <td valign="top">
                        <telerik:RadButton runat="server" ID="btn_accept" Width="75px" Text="Create" 
                            onclick="btn_accept_Click" />
                        <telerik:RadButton runat="server" ID="btn_clear" Width="75px" Text="Clear" CausesValidation="false"
                            onclick="btn_clear_Click" />
                    </td>
                    <td style="padding-left:25px;">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                            BackColor="#F4F4F4" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Names="Arial" Font-Size="X-Small" BorderColor="Black" ForeColor="Red"
                            DisplayMode="List" Style="padding-top:2px;padding-bottom:2px;vertical-align:middle;min-height:0px;max-height:40px;overflow:hidden; width:190px;" />
                    </td>
                </tr>
            </table><br />
            <asp:Label runat="server" ID="lbl_message" ForeColor="Red"></asp:Label>

        </fieldset>

<%--        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtProgType" 
                onservervalidate="txtProgType_ServerValidate" Display="None" 
                ErrorMessage="&bull;&nbsp;You did not select a Program Type." 
                ForeColor="#ABADB3" ValidateEmptyText="True"></asp:CustomValidator>
        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtProgName" 
                onservervalidate="txtProgName_ServerValidate" Display="None" 
                ErrorMessage="&bull;&nbsp;You did not select a Program Name." 
                ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>
        <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="ddl_selectSeason" 
                onservervalidate="ddl_selectSeason_ServerValidate" Display="None" 
                ErrorMessage="&bull;&nbsp;You did not select a Season." 
                ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>--%>
        <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtDayDefName" 
                onservervalidate="txtDayDefName_ServerValidate" Display="None" 
                ErrorMessage="&bull;&nbsp;You did not select a Definition Name." 
                ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>
        <asp:CustomValidator ID="CustomValidator5" runat="server"
                onservervalidate="rdp_DateRange_ServerValidate" Display="None" 
                ErrorMessage="&bull;&nbsp;You did not select a valid date range." 
                ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>
        <asp:CustomValidator ID="CustomValidator6" runat="server"
                onservervalidate="rdp_SelectDate_ServerValidate" Display="None" 
                ErrorMessage="&bull;&nbsp;You did not select a valid date." 
                ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>
        <asp:CustomValidator ID="CustomValidator7" runat="server"
                onservervalidate="cbl_DaysofWeek_ServerValidate" Display="None" 
                ErrorMessage="&bull;&nbsp;You did not select a day." 
                ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>




        <telerik:RadGrid runat="server" ID="rgPrograms_View" CellSpacing="0" DataSourceID="SqlDataSource1"
                AllowSorting="True" GridLines="None" style="margin: 0px auto; width:1000px;">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1">
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                <Columns>
                    <telerik:GridBoundColumn DataField="name" 
                        FilterControlAltText="Filter name column" HeaderText="Day Definition Name" 
                        SortExpression="name" UniqueName="name">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="progName" 
                        FilterControlAltText="Filter progName column" HeaderText="Program Name" 
                        SortExpression="progName" UniqueName="progName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="seasonName" 
                        FilterControlAltText="Filter seasonName column" HeaderText="Season Name" 
                        SortExpression="seasonName" UniqueName="seasonName">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="type" 
                        FilterControlAltText="Filter type column" HeaderText="Definition Type" 
                        SortExpression="type" UniqueName="type">
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="mon"
                        FilterControlAltText="Filter mon column" HeaderText="Monday" 
                        SortExpression="mon" UniqueName="mon">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="tue" 
                        FilterControlAltText="Filter tue column" HeaderText="Tuesday" 
                        SortExpression="tue" UniqueName="tue">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="wed" 
                        FilterControlAltText="Filter wed column" HeaderText="Wednesday" 
                        SortExpression="wed" UniqueName="wed">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="thur" 
                        FilterControlAltText="Filter thur column" HeaderText="Thursday" 
                        SortExpression="thur" UniqueName="thur">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="fri" 
                        FilterControlAltText="Filter fri column" HeaderText="Friday" 
                        SortExpression="fri" UniqueName="fri">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="sat" 
                        FilterControlAltText="Filter sat column" HeaderText="Saturday" 
                        SortExpression="sat" UniqueName="sat">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="sun" 
                        FilterControlAltText="Filter sun column" HeaderText="Sunday" 
                        SortExpression="sun" UniqueName="sun">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="startDate" dataformatstring="{0:MM/dd/yyyy}"
                        FilterControlAltText="Filter startDate column" HeaderText="Start Date" 
                        SortExpression="startDate" UniqueName="startDate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="stopDate" dataformatstring="{0:MM/dd/yyyy}"
                        FilterControlAltText="Filter stopDate column" HeaderText="End Date" 
                        SortExpression="stopDate" UniqueName="stopDate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Date" dataformatstring="{0:MM/dd/yyyy}"
                        FilterControlAltText="Filter Date column" HeaderText="Date" 
                        SortExpression="Date" UniqueName="Date">
                    </telerik:GridBoundColumn>
                </Columns>

            <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
            </EditFormSettings>

            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
            </MasterTableView>

            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

            <FilterMenu EnableImageSprites="False"></FilterMenu>
        </telerik:RadGrid>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" 
            SelectCommand="SELECT * FROM touDayDefinitions">
            <%--SelectCommand="SELECT *, (SELECT seasonName FROM touSeasons WHERE touSeasons.ID=seasonID) as seasonName FROM touDayDefinitions">--%>
        </asp:SqlDataSource>

    </telerik:RadAjaxPanel>
</asp:Content>

