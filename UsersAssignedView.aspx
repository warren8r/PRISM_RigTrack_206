<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UsersAssignedView.aspx.cs"
    Inherits="UsersAssignedView" MasterPageFile="~/Masters/DialogMaster.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="Server">
    <telerik:RadScriptManager ID="scriptManager" runat="server" />
    <h1>
        Assigned Users (<%= EventName() %>)</h1>
    <asp:Label runat="server" ID="lbl_eventid" Text="0" Style="display: none;" />
    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="eventsAssignedToUsers"
        CellSpacing="0" GridLines="None">
        <MasterTableView DataSourceID="eventsAssignedToUsers" AutoGenerateColumns="False">
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="id" DataType="System.Int32" FilterControlAltText="Filter id column"
                    HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="active" DataType="System.Boolean" FilterControlAltText="Filter active column"
                    HeaderText="active" SortExpression="active" UniqueName="active" Visible="False">
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="categoryName" FilterControlAltText="Filter categoryName column"
                    HeaderText="categoryName" SortExpression="categoryName" UniqueName="categoryName"
                    Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="fullName" FilterControlAltText="Filter fullName column"
                    HeaderText="fullName" ReadOnly="True" SortExpression="fullName" UniqueName="fullName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="eventDetail" FilterControlAltText="Filter eventDetail column"
                    HeaderText="eventDetail" ReadOnly="True" SortExpression="eventDetail" UniqueName="eventDetail">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserID" DataType="System.Int32" FilterControlAltText="Filter UserID column"
                    HeaderText="UserID" ReadOnly="True" SortExpression="UserID" UniqueName="UserID"
                    Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventId" DataType="System.Int32" FilterControlAltText="Filter EventId column"
                    HeaderText="EventId" ReadOnly="True" SortExpression="EventId" UniqueName="EventId"
                    Visible="False">
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
        </MasterTableView>
        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
        <FilterMenu EnableImageSprites="False">
        </FilterMenu>
    </telerik:RadGrid>
    <asp:SqlDataSource ID="eventsAssignedToUsers" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
        SelectCommand="SELECT eventUser.id, eventUser.active, category.categoryName, Users.firstName + ' ' + Users.lastName AS fullName, events.eventName + ' ' + '(' + events.eventCode + ')' AS eventDetail, Users.userID as UserID, events.id as EventId
	FROM category 
	LEFT JOIN eventCategory ON category.id = eventCategory.categoryId
	LEFT JOIN events ON eventCategory.eventId = events.id
	INNER JOIN eventUser ON events.id = eventUser.eventId
	INNER JOIN Users ON eventUser.userId = Users.userID"></asp:SqlDataSource>
</asp:Content>
