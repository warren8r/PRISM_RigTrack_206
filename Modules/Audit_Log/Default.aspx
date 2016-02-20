<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Modules_Audit_Log_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
    <telerik:RadAjaxPanel LoadingPanelID="loading" runat="server" ID="ajaxGrid">
        <div id="filters" class="filters">
            <div class="row-container">
                <div class="input-container">
                    <div class="label">
                        Start Date:</div>
                    <telerik:RadDatePicker ID="StartDate" runat="server" Style="float: left;" AutoPostBack="true"
                        OnSelectedDateChanged="SelectedIndexChanged">
                    </telerik:RadDatePicker>
                </div>
                <div class="input-container">
                    <div class="label">
                        End Date:</div>
                    <telerik:RadDatePicker ID="EndDate" runat="server" Style="float: left;" AutoPostBack="true"
                        OnSelectedDateChanged="SelectedIndexChanged">
                    </telerik:RadDatePicker>
                </div>
                <%--       </div>
        <div class="row-container">--%>
                <div class="input-container">
                    <div class="label">
                        Filter User:</div>
                    <telerik:RadDropDownList ID="UsernameList" runat="server" DataSourceID="UsernameListData"
                        AppendDataBoundItems="True" AutoPostBack="True" DataTextField="fullName" DataValueField="userID"
                        Style="float: left" OnSelectedIndexChanged="SelectedIndexChanged" DefaultMessage="- Select User -"
                        Width="300px" DropDownHeight="300px">
                        <Items>
                            <telerik:DropDownListItem runat="server" DropDownList="UserList" Text="-Select User-"
                                Value="-1" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="input-container">
                    <div class="label">
                        Filter Module:</div>
                    <telerik:RadDropDownList ID="moduleSort" runat="server" DataSourceID="moduleList"
                        AppendDataBoundItems="True" AutoPostBack="True" DataTextField="moduleName" DataValueField="moduleID"
                        Style="float: left" OnSelectedIndexChanged="SelectedIndexChanged" DefaultMessage="- Select Module -"
                        Width="300px" DropDownHeight="300px">
                        <Items>
                            <telerik:DropDownListItem runat="server" DropDownList="UserList" Text="- Select Module -"
                                Value="-1" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="input-container">
                    <div class="label">
                        Options</div>
                    <div class="input-container">
                        <telerik:RadButton ID="ResetFilterBtn" runat="server" Text="Reset" OnClick="ResetFilter">
                        </telerik:RadButton>
                    </div>
                </div>
            </div>
            <div class="row-conatiner">
                <asp:Label runat="server" ID="lbl_message" ForeColor="Red"></asp:Label>
            </div>
            <div class="row-conatiner">
                <asp:Label runat="server" ID="Label1" ForeColor="Red"></asp:Label>
            </div>
        </div>
        
        <telerik:RadGrid ID="gridTask" runat="server" DataSourceID="logData" CellSpacing="0"
            GridLines="None" AllowPaging="True" onitemcommand="gridTask_ItemCommand" >
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" DataSourceID="logData">
                <NestedViewTemplate>
                    <telerik:RadGrid ID="history" runat="server" GridLines="None" CellSpacing="0"
                        AllowPaging="True" >                        
                        <MasterTableView AutoGenerateColumns="False" DataSourceID="logData">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                Visible="True">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                Visible="True">
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="username" 
                                    FilterControlAltText="Filter username column" HeaderText="username" 
                                    ReadOnly="True" SortExpression="username" UniqueName="username">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="systemNote" DataType="System.Boolean"  Visible="false"
                                    FilterControlAltText="Filter systemNote column" HeaderText="systemNote" 
                                    SortExpression="systemNote" UniqueName="systemNote">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="attributeName" 
                                    FilterControlAltText="Filter attributeName column" HeaderText="attributeName" 
                                    SortExpression="attributeName" UniqueName="attributeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="description" 
                                    FilterControlAltText="Filter description column" HeaderText="description" 
                                    SortExpression="description" UniqueName="description">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="logged" DataType="System.DateTime" 
                                    FilterControlAltText="Filter logged column" HeaderText="logged" 
                                    SortExpression="logged" UniqueName="logged">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="moduleName" 
                                    FilterControlAltText="Filter moduleName column" HeaderText="moduleName" 
                                    ReadOnly="True" SortExpression="moduleName" UniqueName="moduleName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pagename" 
                                    FilterControlAltText="Filter Pagename column" HeaderText="Pagename" 
                                    SortExpression="Pagename" UniqueName="Pagename">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                            <PagerStyle PageSizeControlType="RadComboBox" />
                        </MasterTableView>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                    </telerik:RadGrid>
                </NestedViewTemplate>
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                    <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn Visible="false" >
                        <ItemTemplate>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:label runat="server" ID="labelID" Text='<%# Eval("superseded") %>'></asp:label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="username" FilterControlAltText="Filter username column"
                        HeaderText="Username" SortExpression="username" UniqueName="username">
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="systemNote" DataType="System.Boolean" FilterControlAltText="Filter systemNote column"
                        HeaderText="systemNote" SortExpression="systemNote" UniqueName="systemNote" Visible="false">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="attributeName" FilterControlAltText="Filter attributeName column"
                        HeaderText="Attribute Name" SortExpression="attributeName" UniqueName="attributeName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="description" FilterControlAltText="Filter description column"
                        HeaderText="Description" SortExpression="description" UniqueName="description">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="logged" FilterControlAltText="Filter logged column"
                        HeaderText="Date Logged" SortExpression="logged" UniqueName="logged" DataType="System.DateTime">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="moduleName" FilterControlAltText="Filter moduleName column"
                        HeaderText="Module Name" SortExpression="moduleName" UniqueName="moduleName"
                        ReadOnly="True">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Pagename" FilterControlAltText="Filter Pagename column"
                        HeaderText="Page Name" SortExpression="Pagename" UniqueName="Pagename">
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle PageSizeControlType="RadComboBox" />
            </MasterTableView>
            <PagerStyle PageSizeControlType="RadComboBox" />
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
    <asp:SqlDataSource ID="logData" runat="server" SelectCommand="SELECT Users.firstName + ' ' + Users.lastName + '(' + Users.loginName + ')' AS username, transactionLog.systemNote, transactionLog.attributeName, transactionLog.description, transactionLog.created AS logged, (SELECT moduleName FROM Modules AS parent WHERE (moduleID = Modules.parentId)) AS moduleName, Modules.moduleName AS Pagename FROM transactionLog INNER JOIN Users ON transactionLog.userId = Users.userID INNER JOIN Modules ON transactionLog.pageId = Modules.moduleID"
        ConnectionString="<%$ databaseExpression:client_database %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="UsernameListData" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
        ProviderName="System.Data.SqlClient" SelectCommand="SELECT Users.userID, Users.firstName + ' ' + Users.lastName + ' (' + loginName + ') ' AS fullName FROM Users LEFT JOIN UserRoles ON Users.userRoleID = UserRoles.userRoleID ORDER BY fullname ASC" />
    <asp:SqlDataSource ID="moduleList" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
        SelectCommand="SELECT moduleID, moduleName FROM Modules WHERE (parentId = '0') ORDER BY moduleName" />
</asp:Content>
