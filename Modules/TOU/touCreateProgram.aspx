<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="touCreateProgram.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <style type="text/css"> 
    .redBorder td
    { 
        box-shadow: 0px 0px 0px 1px red inset;
    } 
</style> 
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <fieldset style="width:470px;">
        <legend style="margin-bottom:-25px;">Time of Use >> Manage Programs</legend>
        <table>
            <tr>
                <td>
                    <table>
                        <tr style="height:50px;">
                            <td style="height:50px;">
                                Program Type:<br />
                                <div style="width:152px; height:24px;">
                                    <telerik:RadComboBox ID="ddl_ProgType" runat="server" Width="150px" Height="140px" 
                                                            EmptyMessage="- Select -" DataSourceID="SqlDataSource2"
                                                            DataTextField="typeName" DataValueField="ID" MarkFirstMatch="true">
                                    </telerik:RadComboBox>
                                    <asp:LinkButton runat="server" ID="btn_createType" Text="Create New" CausesValidation="false" onclick="btn_createType_Click"></asp:LinkButton>
                                </div>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                                    ConnectionString="<%$ databaseExpression:client_database %>" 
                                                    SelectCommand="SELECT [ID], [typeName] FROM [progType]">
                                </asp:SqlDataSource>
                            </td>

                            <td style="width:166px;">
                                Program Name:<br />
                                <div style="width:166px; height:24px;">
                                    <telerik:RadTextBox ID="txtProgName" EmptyMessage="Please type here" runat="server" />
                                </div>
                            </td>

                            <td style="padding-top:10px;">
                                <div style="width:120px;">
                                    <asp:Button runat="server" ID="btnCreateUser" Text="Create" OnClick="btnCreateUser_Click" ValidationGroup="Main" />
                                    <asp:Button runat="server" ID="btnCancel" Text="Clear" CausesValidation="false"  onclick="btnCancel_Click" />
                                </div>
                            </td>

                            <td style="width:0px; overflow:visible;">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="#F4F4F4" BorderStyle="Solid" BorderWidth="1px"  
                                                        Font-Names="Arial" Font-Size="X-Small" BorderColor="Black" ForeColor="Red" DisplayMode="List" ValidationGroup="Main"
                                                        Style="position:relative; top:35px; left:-190px; padding-top:2px;padding-bottom:2px;vertical-align:middle;min-height:0px;max-height:35px;width:198px;overflow:hidden;" />
                            </td>
                        </tr>
                        <tr style="height:15px;">
                            <td colspan="2">
                                <asp:Label runat="server" ID="lbl_message" ForeColor="Red" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    
    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddl_ProgType" ValidationGroup="Main"
                            OnServerValidate="ddl_ProgType_ServerValidate" Display="None" ErrorMessage="&bull;&nbsp;You did not select a Program Type."
                            ForeColor="#ABADB3" ValidateEmptyText="True"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtProgName" ValidationGroup="Main"
                            OnServerValidate="txtProgName_ServerValidate" Display="None" ErrorMessage="&bull;&nbsp;You did not provide a Program Name." 
                            ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator3" runat="server" ValidationGroup="Main"
                            OnServerValidate="txtProgNameUnique_ServerValidate" Display="None" ErrorMessage="&bull;&nbsp;Program Name must be unique per type." 
                            ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>

    <asp:Panel ID="addTypePanel" runat="server" Visible="false">
        <div class="dialog">
            <telerik:RadTextBox class="space" ID="txt_progTypeName" runat="server" EmptyMessage="Program Type"></telerik:RadTextBox>
            <telerik:RadButton class="space" ID="CreateNewType" runat="server" 
                Text="Create" ValidationGroup="Dialog" onclick="CreateNewType_Click"></telerik:RadButton>
            <telerik:RadButton class="space" ID="eventCancel" runat="server" Text="Cancel" OnClick="cancelEvent_Click" CausesValidation="false"></telerik:RadButton>
            <asp:ValidationSummary ID="ValidationSummaryDialog" runat="server" BackColor="#F4F4F4" BorderStyle="Solid" BorderWidth="1px"  
                                   Font-Names="Arial" Font-Size="X-Small" BorderColor="Black" ForeColor="Red" DisplayMode="List" ValidationGroup="Dialog"
                                   Style="padding-top:2px;padding-bottom:2px;vertical-align:middle;min-height:0px;max-height:35px;width:150px;overflow:hidden;margin-top:5px;" />
        </div>
    </asp:Panel>

    <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txt_progTypeName" ValidationGroup="Dialog"
                            OnServerValidate="txt_progTypeName_ServerValidate" Display="None" ErrorMessage="&bull;&nbsp;You did not enter a name."
                            ForeColor="#ABADB3" ValidateEmptyText="True"></asp:CustomValidator>

    <telerik:RadGrid runat="server" ID="rgPrograms" CellSpacing="0" DataSourceID="SqlDataSource1" AllowSorting="True" Width="100%" GridLines="None" OnItemDataBound="rgPrograms_ItemDataBound">
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>

            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>

            <Columns>
                <telerik:GridTemplateColumn HeaderText="Status" AllowFiltering="false" SortExpression="isActive">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Eval("isActive") %>' OnCheckedChanged="checkedChanged" AutoPostBack="true" />
                        <asp:Label ID="Label1" runat="server" ForeColor=<%# (bool)Eval("isActive") ? System.Drawing.Color.Green : System.Drawing.Color.Red %> Text=<%# string.Format("{0}", (bool)Eval("isActive") ? "Active" : "Inactive") %>></asp:Label>
                        <asp:Label runat="server" ID="hidd_ID" Text='<%# Eval("ID") %>' style="display:none;"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="typeName"
                    FilterControlAltText="Filter typeName column" HeaderText="Program Type" 
                    SortExpression="typeName" UniqueName="typeName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="progName" 
                    FilterControlAltText="Filter progName column" HeaderText="Program Name" 
                    SortExpression="progName" UniqueName="progName">
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ databaseExpression:client_database %>" 
        SelectCommand="SELECT [prog].[ID], [type].[typeName], [prog].[progName], [prog].[isActive] FROM [touPrograms] AS prog INNER JOIN [progType] AS type ON [prog].[progTypeID]=[type].[ID] ORDER BY [prog].[isActive] DESC">
    </asp:SqlDataSource>
</telerik:RadAjaxPanel>
</asp:Content>
