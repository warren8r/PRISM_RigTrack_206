<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageConsumableQty.aspx.cs" Inherits="Modules_Configuration_Manager_ManageConsumableQty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="../../js/underscore.js" type="text/javascript" language="javascript"></script>
 <script language="javascript" type="text/javascript">
     function validationfortype() {

         var combo = $find("<%= combo_job.ClientID %>");

         var text = combo.get_text();

         if (text == "Select JobName" || text == "") {
             radalert('Please Select Job', 330, 180, 'Alert Box', null, null);
             return false;

         }
     }
            </script>
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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lbl_message" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Select Job:<br />
                            <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid"  EmptyMessage="Select JobName" Width="200px"
                                ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                               SelectCommand="select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where status!='Closed' and (jid in(select jobid from PrismJobKits) or jid in (select jobid from PrismJobConsumables))"></asp:SqlDataSource>
                            <%--SelectCommand="select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''--%>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_view" runat="server" Text="View" OnClientClick="javascript:return validationfortype();" OnClick="btn_view_OnClick" />
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        
                        <td valign="top">
                            <asp:Panel ID="pnl_consumables" runat="server"></asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="radgrid_manageassetkits" runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                                AllowPaging="true" AllowSorting="true" OnItemDataBound="radgrid_manageassetkits_ItemDataBound">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView  >
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                 <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Select" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select" runat="server" />
                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ConName"
                                            HeaderText="Consumable Name" SortExpression="ConName" UniqueName="ConName" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Qty" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_qty" runat="server" ></asp:TextBox>
                                                <asp:Label ID="lbl_consumableid" runat="server"  Text='<%#Eval("jobconsumableid")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_qty" runat="server"  Text='<%#Eval("qty")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Assigned Date" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AssignedDate" runat="server" ></asp:Label>
                                                <asp:Label ID="lbl_Date" runat="server"  Text='<%#Eval("Assigneddate")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                          </telerik:GridTemplateColumn>
                                        
                                       
                                        <telerik:GridBoundColumn DataField="StatusText"
                                            HeaderText="Status" SortExpression="StatusText" UniqueName="StatusText" >
                                        </telerik:GridBoundColumn>
                                        
                                    </Columns>
                            </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="radcombo_type" runat="server" DataSourceID="SqlGetAssetStatus"
                                DataTextField="StatusText" DataValueField="Id" EmptyMessage="- Select -">
                                        
                            </telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetAssetStatus" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                            SelectCommand="select Id,StatusText from PrsimJobAssetStatus"></asp:SqlDataSource>
                        </td>
                        <td>
                            <asp:Button ID="btn_saving" runat="server" Text="Save" OnClick="btn_saving_OnClick" />
                        </td>
                        
                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                 <telerik:RadWindowManager ID="radwin" runat="server">
            <Windows></Windows>
            </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
        </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

