<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SuperAdmin.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Role" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div>
             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                       
                        <tr>
                            <td align="center">
                                 <fieldset  style="width:600px;height:100px" >
                                    <legend align="left">Role Information</legend>
                                        <table>
                                        <tr><td  colspan="3">
                                                <asp:Label ID="lbl_message" runat="server"></asp:Label>
                                            </td></tr>
                                            <tr> 
                                                <td align="left" class="label_display">
                                                    Enter&#160;User&#160;Type<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_rolename" Width="160px" runat="server" 
                                                        ontextchanged="radtxt_rolename_TextChanged"  
                                                        CssClass="{required:true, messages:{required:'Rolename is required!'}}" ></telerik:RadTextBox>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td align="left" class="label_display">
                                                    Enter&#160;User&#160;Type&#160;Description<span class="star">*</span><br />
                                                    <telerik:RadTextBox ID="radtxt_roledesc" Width="160px" TextMode="MultiLine" Height="50px" runat="server"  CssClass="{required:true, messages:{required:'Role Description is required!'}}" ></telerik:RadTextBox>
                                                </td>
                                               
                                            </tr>  
                                             <tr>
                                                <td align="right" colspan="3" valign="top">
                                                    (<span class="star">*</span>) indicates mandatory fields
                                                </td>
                                            </tr>                                          
                                        </table>
                                </fieldset>
                            </td>
                            
                        </tr>
                        <tr>
                        
                            <td align="center" style="width:600px;height:100px" valign="top">
                                <fieldset >
                                    <legend align="left">Module's Access</legend>
                                        <table >
                                            <tr><td style="height:3px" colspan="3"></td></tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Panel ID="panel_modules" runat="server" colspan="3"></asp:Panel>
                                                </td>
                                            </tr>
                                            <tr><td style="height:3px" colspan="3"></td></tr>
                                            
                                        </table>
                                 </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                            <asp:HiddenField ID="hidden_roleid" runat="server" />
                                <telerik:RadButton ID="btn_create" ButtonType="StandardButton" runat="server" 
                                    Text="Create" onclick="btn_create_Click" ></telerik:RadButton>&nbsp;&nbsp;
                                <telerik:RadButton ID="btn_reset" ForeColor="Maroon"  ButtonType="StandardButton" runat="server" 
                                    Text="Reset" onclick="btn_reset_Click"></telerik:RadButton>
                                   
                            </td>
                        </tr>
                        <tr>
                            <td style="line-height:3px"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                       
                                          <telerik:RadGrid ShowGroupPanel="true" AutoGenerateColumns="false" ID="grid_roleaccess" 
                                                DataSourceID="SqlDataSource1"  
                                                AllowFilteringByColumn="false" AllowSorting="false"
                                                ShowFooter="false" runat="server" GridLines="Vertical" AllowPaging="true" 
                                                                                    onitemcommand="grid_roleaccess_ItemCommand" 
                                                                                    
                                                onitemdatabound="grid_roleaccess_ItemDataBound"   >
                                                <PagerStyle Mode="NextPrevAndNumeric" />
                                              
                                                <MasterTableView DataKeyNames="userRoleID"  AllowMultiColumnSorting="false" EnableColumnsViewState="false"
                                                EditMode="InPlace" CommandItemDisplay="None"  NoMasterRecordsText="No Data have been added." >             
                                                  <HeaderStyle HorizontalAlign="Center"  />
                                                            <Columns>
                                                            </Columns> 
                                                 </MasterTableView> 
                                        <%--<ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True"> </ClientSettings> --%>
                                    </telerik:RadGrid> 
                                         <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                                SelectCommand="select * from SuperUserRoles ur left outer join SuperUserModules m on ur.userRoleID=m.moduleID" runat="server"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="SQLDataSourceUserAccess" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                                SelectCommand="SELECT * FROM AccessTypes" runat="server"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
    </div>
</asp:Content>

