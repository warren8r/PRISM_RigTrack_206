<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="veeConstants.aspx.cs" Inherits="veeConstants" %>

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
<tr><td style="line-height:10px">&nbsp;</td></tr>
   <%-- <tr>
        <td><strong>Set VEE Parameters</strong></td>
    </tr>
<tr><td style="line-height:10px"></td></tr>--%>
<tr>
    <td align="center">
        <asp:Label ID="lbl_message" runat="server"></asp:Label>
    </td>
</tr>
<tr><td style="line-height:10px"></td></tr>
<tr>
    <td align="center">
        <table border="0">
            <tr>
                <td align="right">Meter Time Tolerance duration (min):</td>
                <td align="left">
                    <telerik:RadTextBox ID="txt_timetolerance" runat="server" />
                    
                </td>
                <td valign="top">
                
                <asp:ImageButton ID="img_ttip" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                <telerik:RadToolTip ID="tip" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="img_ttip"
                    Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                    This parameter is used to check the time difference between meter time and data collection device time.
                </telerik:RadToolTip>
            </tr>
            <tr><td style="line-height:10px"></td></tr>
            <tr>
                <td align="right">Res. Meter Zero Pulse (Raise event) :</td>
                <td align="left"><telerik:RadComboBox ID="rbtn_zeropluse" runat="server" 
                        RepeatDirection="Horizontal">
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Yes"/>
                        <telerik:RadComboBoxItem Value="0" Text="No"/>
                    </Items>
                </telerik:RadComboBox></td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton1" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip1" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton1"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        If 'Yes', an event will be raised when a zero pulse is notified from AMI.
                    </telerik:RadToolTip>
                </td>
            </tr>
             <tr><td style="line-height:10px"></td></tr>
            <tr>
                <td align="right">Daily Interval Data Used for Billing :</td>
                <td  align="left"><telerik:RadComboBox ID="rbtn_dailybilling" runat="server" 
                        RepeatDirection="Horizontal">
                        <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Yes"/>
                        <telerik:RadComboBoxItem Value="0" Text="No"/>
                    </Items>
                </telerik:RadComboBox></td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton2" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip2" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton2"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        If 'Yes', Daily Interval Data will be used for Billing
                    </telerik:RadToolTip>
                </td>
            </tr>
             <tr><td style="line-height:10px"></td></tr>
            <tr>
                <td align="right">Current Transformer Ratio :</td>
                <td  align="left"><telerik:RadTextBox  ID="txt_CTR" runat="server" ></telerik:RadTextBox > </td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton3" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip3" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton3"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        This ratio is used for Sum Check
                    </telerik:RadToolTip>
                </td>
            </tr>
            <tr><td style="line-height:10px"></td></tr>
            <tr>
                <td align="right">Voltage Transformer Ratio :</td>
                <td align="left"><telerik:RadTextBox  ID="txt_VTR" runat="server" ></telerik:RadTextBox > </td>
                <td valign="top">
                    <%--<img src="../../images/info.png" title="" style="width: 20px;" alt="ToolTip.png" />
                    <asp:ImageButton ID="ImageButton4" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip4" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton2"
                        Width="200px" Height="200px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        This ratio is used for Sum Check
                    </telerik:RadToolTip>--%>
                </td>
            </tr>
             <tr>
                <td align="right">Spike Check Threshold :</td>
                <td  align="left"><telerik:RadTextBox  ID="txt_SpikeCheck" runat="server" ></telerik:RadTextBox > </td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton4" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip4" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton4"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        This parameter is used for Spike Check
                    </telerik:RadToolTip>
                </td>
            </tr>
             <tr>
                <td align="right">kVARh Threshold :</td>
                <td  align="left"><telerik:RadTextBox  ID="txt_kvarThreshold" runat="server" ></telerik:RadTextBox > </td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton5" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip5" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton5"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        This parameter is used for kVARh Check
                    </telerik:RadToolTip>
                </td>
            </tr>
             <tr>
                <td align="right">High/Low Constant :</td>
                <td  align="left"><telerik:RadTextBox  ID="txt_highlowconstant" runat="server" ></telerik:RadTextBox > </td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton6" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip6" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton6"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        This parameter is used for High/Low Usage Check
                    </telerik:RadToolTip>
                </td>
            </tr>
            <tr>
                <td align="right">High/Low Usage Percentage (%) :</td>
                <td  align="left"><telerik:RadTextBox  ID="radtxt_percentage" runat="server" ></telerik:RadTextBox > </td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton8" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip8" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton8"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        This parameter is used for High/Low Usage Percentage
                    </telerik:RadToolTip>
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td align="right">Data Migration + Validation + Estimation :</td>
                <td  align="left"><telerik:RadComboBox ID="radcombo_datamig" runat="server" 
                        RepeatDirection="Horizontal">
                        <Items>
                        <telerik:RadComboBoxItem Value="Auto" Text="Auto"/>
                        <telerik:RadComboBoxItem Value="Manual" Text="Manual"/>
                    </Items>
                </telerik:RadComboBox></td>
                <td valign="top">
                    
                    <asp:ImageButton ID="ImageButton7" runat="server" Width="18px"  ImageUrl="~/images/info.png" />
                    <telerik:RadToolTip ID="RadToolTip7" runat="server" Position="MiddleRight" RelativeTo="Element" TargetControlID="ImageButton7"
                        Width="200px" Height="100px" HideEvent="ManualClose" OnClientShow="OnClientShow">
                        Webservice controls is for Data Migration,Validation and Estimation
                    </telerik:RadToolTip>
                </td>
            </tr>
            <tr  runat="server" visible="false">
                <td align="right">Set Web Scheduler :</td>
                <td align="left">
                    <asp:Button ID="btn_start" runat="server" text="On" onclick="Button1_Click" />
                    <asp:Button ID="btn_stop" runat="server" text="Off" onclick="Button2_Click" />
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr><td style="line-height:10px">&nbsp;</td></tr>
<tr>
    <td align="center">
        <table>
            <tr>
                <td><asp:Button ID="btn_save" runat="server" Text="Save" onclick="btn_save_Click" /></td>
                <td style="width:5px"><asp:HiddenField ID="hidden_veeid" runat="server" /></td>
                <td><asp:Button ID="btn_cancel" runat="server" BackColor="Maroon" Text="Cancel" 
                        onclick="btn_cancel_Click" /></td>
            </tr>
        </table>
    </td>
</tr>
<tr><td style="line-height:10px"></td></tr>

</table>
</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_save" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_cancel" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>
</asp:Content>

