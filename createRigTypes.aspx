<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createRigTypes.aspx.cs" Inherits="createRigTypes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet/less" type="text/css" href="css/mdm.less" />
    <link rel="stylesheet/less" type="text/css" href="css/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet/less" type="text/css" href="css/main.less" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rad1" runat="server"></telerik:RadScriptManager>
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


    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbl_rigtypemsg" runat="server"></asp:Label>
                    <asp:Label ID="lbl_updateid" runat="server" style="display:none"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Rig Name:<br />
                    <telerik:RadTextBox ID="radtxt_tigtypename" runat="server"></telerik:RadTextBox> 
                </td>
            </tr>
            <tr>
                <td>
                    Rig Description:<br />
                    <telerik:RadTextBox ID="radtxt_rigtypedesc" Height="100px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<telerik:RadButton ID="RadButton2" runat="server" Text="Save" AutoPostBack="true" OnClick="Button2_Click">
                    </telerik:RadButton>--%>
                    <asp:Button ID="btn_createrig" Text="Create"  runat="server" OnClick="btn_createrig_Click">
                    </asp:Button>
                </td>
            </tr>
            <tr><td style="height:10px"></td></tr>
            <tr><td><b><u>Existing Rigs</u></b></td></tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="radgrid2" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" 
                    CssClass="mdmGrid active"
                        CellSpacing="0" DataSourceID="SqlDataSource4" GridLines="None"   >
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="rigtypeid" DataSourceID="SqlDataSource4">
                            <Columns>
                                <telerik:GridBoundColumn DataField="rigtypename" 
                                    HeaderText="Rig Name"  SortExpression="rigtypename"
                                    UniqueName="rigtypename">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="rigtypedesc" 
                                    HeaderText="Rig Description"  SortExpression="rigtypedesc"
                                    UniqueName="rigtypedesc">
                                </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="View/Edit" >
                                    <ItemTemplate> 
                                    <asp:LinkButton ID="lnk_edit" runat="server" OnClick="lnk_edit_Click" Text="View/Edit"></asp:LinkButton>
                                        <asp:Label ID="lbl_rigtypeid" runat="server" Text='<%# Eval("rigtypeid") %>' style="display:none;"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                    SelectCommand="SELECT * FROM [RigTypes]"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
        </ContentTemplate>
            <Triggers>
                
                
                <asp:AsyncPostBackTrigger ControlID="btn_createrig" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>
    </form>
</body>
</html>
