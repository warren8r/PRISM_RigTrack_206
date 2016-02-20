<%@ Page  Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="DaTeplateService.aspx.cs" Inherits="Modules_DA_DaLaunchService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <tr><td style="line-height:20px;"></td></tr>    
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td>Select Service :</td>
                    <td valign="top">
                        <telerik:RadComboBox ID="radcombo_services" runat="server" 
                        onselectedindexchanged="radcombo_services_SelectedIndexChanged" ></telerik:RadComboBox>
                    </td>                   
                    
                    <td  align="center" valign="top">
                        <asp:Button ID="btn_viewservice" runat="server" Text="View Service" 
                            onclick="btn_viewservice_Click" />
                    </td>                  
                </tr>
                              
            </table>
        </td>
    </tr>
    <tr><td style="line-height:5px;"></td></tr>
    <tr>      
        <td  align="center" style="border:1px solid;" runat="server" id="td_info" visible="false">
       
                <table>
                    <tr>
                        <td style="border:solid 1px #000000">Selected Service: <asp:Label ID="lbl_servicename" runat="server" Font-Bold="true" ></asp:Label></td>
                        
                        <td style="border:solid 1px #000000"><asp:Label ID="lbl_serviceurl" runat="server" Font-Bold="true"></asp:Label></td>
                        <td style="border:solid 1px #000000" valign="top">Service Description: <asp:Label ID="txt_description" runat="server" Font-Bold="true"  />
                        </td>                  
                    </tr>
                    <tr><td style="line-height:5px;"></td></tr>
                 <tr>
                        <td id="Td1" align="center" runat="server" colspan="3">
                            <table>
                                <tr>
                                    <td>Enter Templatename:</td>
                                    <td><telerik:RadTextBox ID="txt_templatename" runat="server"></telerik:RadTextBox></td>
                                    
                                </tr>
                            </table>
                        </td>
                     </tr>
                    <tr>                        
                              
                        <td colspan="2" align="left">
                            <asp:Button ID="btn_launch" runat="server" Text="Save Template" 
                                onclick="btn_launch_Click" />
                        </td>                        
                    </tr>
                     <tr><td style="line-height:5px;"></td></tr>
                    <tr>
                        <td  align="center" colspan="3">
       
                            <asp:Panel ID="panel_attributes" runat="server">
            
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            
        </td>
        
    </tr>
   
     <tr><td style="line-height:5px;"></td></tr>
     <tr>
        <td align="center">
            <asp:Label ID="lbl_url" runat="server"></asp:Label>
            <asp:HiddenField ID="hidden_url" runat="server" />
            <asp:HiddenField ID="hidden_serviceurl" runat="server" />
        </td>
     </tr>
     <tr><td style="height:20px"></td></tr>
     <tr>
        <td align="center">
             <telerik:RadGrid ID="radgrid_Template"  AllowMultiRowSelection="true" Width="700px" Visible="false"
                runat="server" AllowSorting="True" GridLines="None" AllowPaging="true" PageSize="10" OnItemDataBound="radgrid_Template_ItemDataBound"  >
                <MasterTableView AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="templateName" HeaderText="Template Name"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="URL" HeaderText="Service URL"></telerik:GridBoundColumn>
                         <telerik:GridTemplateColumn HeaderText="Status" >
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="isChecked"   AutoPostBack="true" OnCheckedChanged="CheckChanged"  />
                                <%--<asp:LinkButton ID="lnk_activeornot" runat="server" CommandName="activeInactive" Text='<%# Bind("status") %>'></asp:LinkButton>--%>
                                 <%--<asp:Label ID="Label2" runat="server" ForeColor='<%# (bool)Eval("active") ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# string.Format("{0}", (bool)Eval("active") ? "Active" : "Inactive") %>'></asp:Label>--%>
                                <%--<asp:Label ID="lbl_status" runat="server" ></asp:Label> --%>
                                <asp:Label ID="lbl_tempid" runat="server" Text='<%# Bind("templateId") %>' Visible="false"></asp:Label> 
                                <asp:Label ID="lbl_statuscheck" runat="server" Text='<%# Bind("status") %>' ></asp:Label>
                            </ItemTemplate>
                            
                        </telerik:GridTemplateColumn>                                           
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                            
                </ClientSettings>
            </telerik:RadGrid>
                    
        </td>
     </tr>
     
     <tr><td align="center"><asp:Label ID="lbl_message" runat="server"></asp:Label></td></tr>
    
    </table>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_viewservice" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="btn_launch" EventName="Click"></asp:AsyncPostBackTrigger>       
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

