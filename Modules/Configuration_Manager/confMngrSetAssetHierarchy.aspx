<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="confMngrSetAssetHierarchy.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrSetAssetHierarchy" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxPanel ID="radajxPanel" runat="server">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
            SelectCommand="SELECT clientAssetID AS chiID, clientAssetName AS Name FROM clientAssets">
        </asp:SqlDataSource>
        <div id="divAssetDetails" runat="server">
        </div>
        <table border="0">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="chiID"
                        DataSourceID="SqlDataSource1" Height="177px" Width="354px">
                        <Columns>
                            <asp:BoundField DataField="chiID" HeaderText="chiID" InsertVisible="False" ReadOnly="True"
                                SortExpression="chiID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
