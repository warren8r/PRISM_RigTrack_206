<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNotestoDataEntry.aspx.cs" Inherits="Modules_Configuration_Manager_AddNotestoDataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet/less" type="text/css" href="../../css/mdm.less" />
        <link href="../../css/main.css" type="text/css" rel="Stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"></telerik:RadScriptManager>
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
                        <asp:Label ID="lbl_notesdate" runat="server"></asp:Label>
                        <asp:Label ID="lbl_date" runat="server" style="display:none"></asp:Label>
                        <asp:Label ID="lbl_jid" runat="server" style="display:none"></asp:Label>
                    </td>
                </tr>
                                        
                <tr>
                    <td>
                        Enter Notes :<br />
                        <telerik:RadTextBox ID="radtxt_Notes" Height="150px" Width="300px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<telerik:RadButton ID="RadButton2" runat="server" Text="Save" AutoPostBack="true" OnClick="Button2_Click">
                        </telerik:RadButton>--%>
                        <asp:Button ID="btn_addnotes" Text="Add" OnClick="btn_addnotes_Click" runat="server">
                        </asp:Button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset>
                            <legend>Existing Notes</legend>
                            <telerik:RadGrid ID="RadGrid_notes" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="notes" CssClass="mdmGrid active"
                                CellSpacing="0"  GridLines="None" Width="80%" >
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AutoGenerateColumns="False" DataSourceID="notes">
                                                            
                                    <Columns>
                                                                
                                        <telerik:GridBoundColumn DataField="Notes" 
                                            HeaderText="Notes"  SortExpression="Notes"
                                            UniqueName="Notes">
                                        </telerik:GridBoundColumn>
                                                                
                                        <telerik:GridBoundColumn DataField="Date" 
                                            HeaderText="Posted Date" SortExpression="Date" UniqueName="Date">
                                        </telerik:GridBoundColumn>
                                                                             
                                    </Columns>
                                                            
                                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                </MasterTableView>
                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </telerik:RadGrid>
                            <asp:SqlDataSource ID="notes" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                                SelectCommand="select * from DataApprovalNotes where Date=@date and jid=@jid and Notes<>'' order by Date desc">
                                    <SelectParameters>
                                    <asp:ControlParameter ControlID="lbl_date" Name="date" DbType="DateTime" />
                                    <asp:ControlParameter ControlID="lbl_jid" Name="jid" DbType="Int32" />
                                    
                                        <%--<asp:QueryStringParameter Name="jid" QueryStringField="jid" DefaultValue="1" DbType="Int32" />--%>
                                    </SelectParameters>
                            </asp:SqlDataSource>
                        </fieldset>
                    </td>
                </tr>
            </table>
    </div>
    </ContentTemplate>
    <Triggers>
         
        <asp:AsyncPostBackTrigger ControlID="btn_addnotes" EventName="Click"></asp:AsyncPostBackTrigger>
    </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
