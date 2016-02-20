<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="SetupWebService.aspx.cs" Inherits="Modules_DA_SetupWebService" %>

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
    <%--<tr><td><asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" /></td></tr>--%>
     <div>
        
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td style="height:1px"></td></tr>
                         <tr><td colspan="2" align="center" ><asp:Label ID="lbl_message" runat="server" ></asp:Label></td></tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman"><td style="height: 10px"></td></tr>
                        <tr  >
                            <td align="center" style="border-style:solid; border:1px; ">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                   <tr>
                                        <td valign="top" align="center">
                                             <table runat="server" id="table_webservice" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top" align="center">
                                                                    <table border="0" cellpadding="0" cellspacing="3">
                                                                        <tr>
                                                                            <td align="left"  valign="top">
                                                                                Service Name :<br />
                                                                                <telerik:RadTextBox ID="txt_servicename"  runat="server" 
                                                                                    AutoPostBack="True" ontextchanged="txt_servicename_TextChanged"/></td>
                                                                            
                                                                            <td align="left"  valign="top">
                                                                                URL :&nbsp;<br />
                                                                                <telerik:RadTextBox  ID="txt_serviceurl" runat="server" Width="250px" /></td>
                                                                            <td align="left" valign="top">
                                                                                            User Name :<br />
                                                                                <telerik:RadTextBox  ID="txt_serviceusername" runat="server"  /></td>
                                                                            <td align="left" valign="top">
                                                                                Password :<br />
                                                                                <telerik:RadTextBox  ID="txt_servicepassword" runat="server" /></td>
                                                                            <td align="left" valign="top">
                                                                                &nbsp;Tech Description :<br />
                                                                            <telerik:RadTextBox  ID="txt_servicedesc" runat="server"  
                                                                                    Width="250px"/></td>
                                                                        </tr>
                                                                        
                                                                        <tr><td style="height:5px"></td></tr>
                                                                        <tr>
                                                                            <td colspan="5" align="center" >
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table border="1">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        Attributes :
                                                                                                    </td>
                                                                                                    <td align="left">
                                                                                                        XML Names : 
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <telerik:RadTextBox  runat="server" ID="txt_parameters_1" />
                                                                                                    </td>
                                                                                                
                                                                                                    <td  valign="top">
                                                                                                        <telerik:RadTextBox  runat="server" ID="txt_attr_1" Width="350px" /></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <telerik:RadTextBox  runat="server" ID="txt_parameters_2" />
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <telerik:RadTextBox   runat="server" ID="txt_attr_2" Width="350px" /></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <telerik:RadTextBox  runat="server" ID="txt_parameters_3" />
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <telerik:RadTextBox   runat="server" ID="txt_attr_3" Width="350px" /></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    
                                                                </td>
                                                               
                                                                
                                                            </tr>
                                                        </table>    
                                         </td>
                                   </tr>
                                   <tr><td style="height:10px"></td></tr>
                                    
                                    
                                    
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hidden_serviceid" runat="server" />
                    <asp:HiddenField ID="hidden_serviceattr1id" runat="server" />
                    <asp:HiddenField ID="hidden_serviceattr2id" runat="server" />
                    <asp:HiddenField ID="hidden_serviceattr3id" runat="server" />
                </td>
            </tr>
            
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td>

                                <table>
                                    <tr><td ><asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" /></td></tr>
                                </table>
                                
                            </td>
                            <td>
                                <table>
                                    <tr><td><asp:Button ID="btn_reset" BackColor="Maroon" runat="server" Text="Reset" 
                                            onclick="btn_reset_Click1"/></td></tr>
                                    <%--<tr><td><asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" /></td></tr>--%>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- End of Second row-->
            <tr><td style="height:10px"></td></tr>
            
            <tr>
                <td align="center">
                    <telerik:RadGrid ID="radgrid_services" Width="960px"  AllowMultiRowSelection="true"
                                runat="server" AllowSorting="True" GridLines="None" 
                        AutoGenerateColumns="False" onitemcommand="radgrid_services_ItemCommand" OnItemDataBound="radgrid_services_ItemDataBound" 
                        >
                                <MasterTableView DataKeyNames="serviceId"  AllowMultiColumnSorting="false" EnableColumnsViewState="false"
                                                EditMode="InPlace" CommandItemDisplay="None"  NoMasterRecordsText="No Data have been added." >             
                                                  <HeaderStyle HorizontalAlign="Left"  />
                                    <Columns>
                                        <telerik:GridButtonColumn CommandName="Edit" Text="Edit" UniqueName="EditCommandColumn"
                                                        ButtonType="LinkButton" />
                                        <telerik:GridTemplateColumn HeaderText="Status" >
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="isChecked"   AutoPostBack="true" OnCheckedChanged="CheckChanged"  />
                                
                                                <asp:Label ID="lbl_tempid" runat="server" Text='<%# Bind("serviceId") %>' Visible="false"></asp:Label> 
                                                <asp:Label ID="lbl_statuscheck" runat="server" Text='<%# Bind("status") %>' ></asp:Label>
                                            </ItemTemplate>
                            
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="serviceName" HeaderText="Service Name" ItemStyle-Width="140px"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="serviceDescription" HeaderText="Service Description" ItemStyle-Width="140px"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="URL" HeaderText="Service URL" ItemStyle-Width="400px"></telerik:GridBoundColumn>
                                                                     
                                    </Columns>
                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true">
                                    
                                </ClientSettings>

                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

                                <FilterMenu EnableImageSprites="False"></FilterMenu>
                            </telerik:RadGrid>
                </td>
            </tr>
            <tr><td style="height:30px"></td></tr>
        </table>
    
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_submit" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click"></asp:AsyncPostBackTrigger>       
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

