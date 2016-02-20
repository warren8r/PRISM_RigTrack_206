<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainteneceNotes.aspx.cs" Inherits="createjobtypes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet/less" type="text/css" href="css/mdm.less" />
    <link rel="stylesheet/less" type="text/css" href="css/jquery-ui-1.10.3.custom.css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="rad1" runat="server"></telerik:RadScriptManager>

    <div>
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
   <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
        <table style="font-family:Verdana;font-size:10px">
                <tr>
                    <td>
                        <asp:Label ID="lbl_message" runat="server"></asp:Label>
                        <asp:Label ID="lbl_jcrt" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <%-- <td>
                        Job Type:<br />
                        <telerik:RadTextBox ID="radtxt_jname" runat="server"></telerik:RadTextBox> 
                    </td>--%>
                </tr>
                <tr>
                    <td id="td_notes" runat="server">
                        Enter Notes:<br />
                        <telerik:RadTextBox ID="radtxt_jobdesc" Height="100px" Width="500px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:HiddenField ID="hidd_assetridid" runat="server" />
                        <%--<telerik:RadButton ID="RadButton2" runat="server" Text="Save" AutoPostBack="true" OnClick="Button2_Click">
                        </telerik:RadButton>--%>
                        <asp:Button ID="btn_crtjob" Text="Add Notes"  runat="server" OnClick="btn_crtjob_Click">
                        </asp:Button>
                    </td>
                </tr>
                <tr><td style="height:10px"></td></tr>
                <tr>
                    <td>
                        <b>Existing Notes:</b>


                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_updatednotesid" runat="server" style="display:none;"></asp:Label>
                        <telerik:RadGrid ID="radgrid_existingJobs" OnItemDataBound="radgrid_existingJobs_ItemDataBound" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" 
                        CssClass="mdmGrid active" 
                            CellSpacing="0" DataSourceID="SqlDataSource1" GridLines="None"   >
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False"  DataSourceID="SqlDataSource1">
                                <Columns>
                                 <telerik:GridTemplateColumn HeaderText="View/Edit" >
                                        <ItemTemplate> 
                                        <asp:LinkButton ID="lnk_edit" runat="server" OnClick="lnk_edit_Click" Text="View/Edit"></asp:LinkButton>
                                        <asp:Label ID="lbl_notesid" runat="server" Text='<%# Eval("NotesID") %>' style="display:none;"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="name" 
                                        HeaderText="Entered By"  SortExpression="name"
                                        UniqueName="Entered">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="userRole" 
                                        HeaderText="Role"  SortExpression="userRole"
                                        UniqueName="userRole">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Datetime" 
                                        HeaderText="DateTime"  SortExpression="Datetime"
                                        UniqueName="Datetime">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Notes" 
                                        HeaderText="Notes"  SortExpression="Notes"
                                        UniqueName="Notes">
                                    </telerik:GridBoundColumn>
                                   
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                        SelectCommand="SELECT * FROM [jobTypes]"></asp:SqlDataSource>--%>
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ databaseExpression:client_database %>" 
             SelectCommand="SELECT (u.firstname+' '+u.lastname) as name,* FROM MainteneceNotes m,Users u,UserRoles r where m.UserId=u.userID and 
             r.userRoleID=u.userRoleID and AssetMainteneceId=@assetridid and ComponentMainteneceId is null" >
                               
                                    <SelectParameters>
                                      <asp:ControlParameter ControlID="hidd_assetridid" Name="assetridid" />
                                    </SelectParameters>
                                    </asp:SqlDataSource>


        </ContentTemplate>
            <Triggers>
                
                
                <asp:AsyncPostBackTrigger ControlID="btn_crtjob" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
