<%@ Page  Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="DaLaunchService.aspx.cs" Inherits="Modules_DA_DaLaunchService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    function OnClientShow(sender, args) {
        var btn = sender.getManualCloseButton();
        btn.style.left = "0px";
    }
</script>
</telerik:RadCodeBlock>
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
                    <td>Select Service :</td>
                    <td valign="top">
                        <telerik:RadComboBox ID="radcombo_services" runat="server" 
                        OnSelectedIndexChanged="radcombo_services_SelectedIndexChanged" ></telerik:RadComboBox>
                    </td>                   
                    
                    <td  align="center" valign="top">
                        <asp:Button ID="btn_viewservice" runat="server" Text="View/Launch Service" 
                            onclick="btn_viewservice_Click" />
                    </td>                  
                </tr>
                              
            </table>
        </td>
    </tr>
    <tr><td style="line-height:5px;"></td></tr>
    <tr>      
       <td align="center">
            <table>
                <tr>
                   
                     <td  align="center" style="border:1px solid #000000;width:700px" runat="server" id="td_info" visible="false">
       
                <table>
                    <tr>
                       <td colspan="5">
                        <table border="1" cellspacing="3">
                            <tr>
                                 <td >Selected Service: <asp:Label ID="lbl_servicename" runat="server" Font-Bold="true" ></asp:Label></td>
                        
                                <td><asp:Label ID="lbl_serviceurl" runat="server"></asp:Label></td>
                                <td valign="top">Service Description: <asp:Label ID="txt_description" runat="server" Font-Bold="true"  />
                                </td>
                            </tr>
                        </table>
                       </td>
                                          
                    </tr>
                    
                     <tr><td style="line-height:5px;"></td></tr>
                    <tr>
                     <td>Template Name:<br />
                            <telerik:RadComboBox ID="radcombo_temname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radcombo_temname_SelectedIndexChanged">
                                
                            </telerik:RadComboBox></td>
                            <td>
                                <br />
                            <asp:Button 
                                              ID="Button1" runat="server" Text="Launch Service" 
                                onclick="btn_launch_Click" />
                                <%--<asp:ImageButton ID="ImageButton1" runat="server" ToolTip="On Clicking 'Launch Service' Button, web service is invoked ON-DEMAND. Web Service gets the ZIP files from AMI System, extracts the XML files, stores the data to MDM, Validates and Estimats the Data." ImageUrl="~/images/info.png" />--%>
                            </td>
                    <td>(OR)</td>
                        <td  align="center">
       
                            <asp:Panel ID="panel_attributes" runat="server">
            
                            </asp:Panel>
                        </td>
                        <td  align="left">
                            
                                                                                         <asp:Button 
                                             ID="btn_launch" runat="server" Text="Launch Service" 
                                onclick="btn_launch_Click" />

                                <asp:HyperLink ID="img_ttip" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                                <telerik:RadToolTip ID="tip" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="img_ttip"
                                    Width="200px" Height="200px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                                    On Clicking 'Launch Service' Button, web service is invoked manually. Web Service gets the ZIP files from AMI System, extracts the XML files, stores the data to MDM, Validates and Estimats the Data.
                                </telerik:RadToolTip>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" align="left">
                            <table>
                                <tr><td><asp:Label ID="lbl_direcion" runat="server"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbl_mtype" runat="server"></asp:Label></td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
            
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
        </td>
     </tr>
     <tr><td style="line-height:5px;"></td></tr>
     <tr>
        <td align="center" runat="server" id="td_template" visible="false">
            <table>
                <tr>
                    <td>Enter Templatename:</td>
                    <td><telerik:RadTextBox ID="txt_templatename" runat="server"></telerik:RadTextBox></td>
                    <td><asp:Button ID="btn_savetemplate" runat="server" Text="Save Template" 
                            onclick="btn_savetemplate_Click" /></td>
                </tr>
            </table>
        </td>
     </tr>
     <tr><td style="line-height:5px;"></td></tr>
     <tr>
        <td align="center">
            <table  cellpadding="2" cellspacing="2">
                <tr>
                    <td align="center"><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
                </tr>
                <tr>
                   <td id="id_msg" runat="server" visible="false">
                        <table cellpadding="2" style="color:Green">
                            <tr><td style="text-decoration:underline; font-weight:bold">Following validations are performed</td></tr>
                            
                            <tr><td>&nbsp;&nbsp;1.Meter identification check</td></tr>
                            <tr><td>&nbsp;&nbsp;2.Time tolerance on data</td></tr>
                            <tr><td>&nbsp;&nbsp;3.Time tolerance on meter</td></tr>
                            <tr><td>&nbsp;&nbsp;4.Spike check</td></tr>
                            <tr><td>&nbsp;&nbsp;5.Sum check</td></tr>
                            <tr><td>&nbsp;&nbsp;6.Quality flags</td></tr>
                            
                            <tr><td style="text-decoration:underline; font-weight:bold">Estimations are done on following</td></tr>
                            <tr><td>&nbsp;&nbsp;1.Missing Intervals</td></tr>
                            <tr><td>&nbsp;&nbsp;2.Spike check</td></tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
     </tr>
    <tr>
        <td>
            <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
          Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
          Height="10">
     </telerik:RadToolTipManager>
        </td>
    </tr>
    </table>
</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_launch" EventName="Click"></asp:AsyncPostBackTrigger>
                
            </Triggers>
            </asp:UpdatePanel>

</asp:Content>

