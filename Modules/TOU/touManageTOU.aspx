<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master"
    AutoEventWireup="true" CodeFile="touManageTOU.aspx.cs" Inherits="Modules_TOU_touManageTOU" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/jscript" src="/js/jquery-1.9.1.js"></script>
<script type="text/jscript" src="/js/flot/jquery.flot.min.js"></script>
<script type="text/jscript" src="/js/flot/jquery.flot.pie.min.js"></script>
<script type="text/javascript">
    function drawPie(srcID, startHR, totHR) {
        var highHours = (100 / 12) * totHR;
        var lowHours = 100 - highHours;

        //avoid any errors involving zero or negative numbers
        if (lowHours <= 0)
            lowHours = .1;

        var data = [
        { label: "high", data: highHours, color: '#52B949' },
        { label: "low", data: lowHours, color: '#FFFFFF' }
        //{ label: "high", data: highHours, color: '#EC242A' },
        //{ label: "low", data: lowHours, color: '#52B949' }
        ];

        // 0 starts at 3 o'clock on pie
        if (startHR <= 3)
            startHR += 12;

        var startHour = 0.1666666666666667 * (startHR - 3);
        $.plot(srcID, data, {
            series: {
                pie: {
                    show: true,
                    startAngle: startHour,
                    label: {
                        show: false
                    }
                }
            }, legend: {
                show: false
            }
        });
    }
</script>
<style>
 .detailTable 
    { 
      margin-left:10px !important;
    } 
 .assSeasonHead
    {
      margin: 0;
      padding: 0px 0px 1px 0px !important;
    }

.placeholder table
    {
        width:100%;
        padding:0px;
    }
    
.placeholder table table table td
    {
        padding: 0px 10px;
        border : 0px;
    }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnLoad="RadAjaxPanel1_Load">
    <center>
    <fieldset style="width: 840px;" >
        <table style="text-align: left;" >
            <tr>
                <td>Program Type:
                    <telerik:RadComboBox ID="txtProgType" runat="server" AutoPostBack="true"
                        Width="150px" Height="140px" EmptyMessage="- Select -" 
                        OnSelectedIndexChanged="txtProgType_SelectedIndexChanged" DataSourceID="SqlDataSource3"
                        DataTextField="typeName" DataValueField="ID" MarkFirstMatch="true" />
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ databaseExpression:client_database %>" 
                        SelectCommand="SELECT [typeName], [ID] FROM [progType]">
                    </asp:SqlDataSource>
                </td>
                <td>Program Name:
                    <telerik:RadComboBox ID="txtProgName" runat="server"  AutoPostBack="True" 
                        Width="150px" Height="140px" EmptyMessage="- Select -" Enabled="false" 
                        OnSelectedIndexChanged="txtProgType_SelectedIndexChanged" 
                        DataSourceID="SqlDataSource4" DataTextField="progName" 
                        DataValueField="ID" MarkFirstMatch="true" />
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ databaseExpression:client_database %>"
                        SelectCommand="SELECT * FROM [touPrograms] WHERE id in (select distinct PROGRAM_id from et_slab_rates) AND (([isActive] = @isActive) AND ([progTypeID] = @progTypeID))">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="true" Name="isActive" Type="Boolean" />
                            <asp:ControlParameter ControlID="txtProgType" Name="progTypeID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td valign="top">
                    <asp:Button ID="btn_view" runat="server" Text="View" onclick="btn_view_Click" />
                    <asp:Button ID="btn_clear" Text="Reset" runat="server" onclick="btn_cancel_Click" />
                </td>
            </tr>
        </table>
                 
        <table border="0" cellpadding="0" cellspacing="0" width="970px">
            <tr>   
                <td>

                    <table>
                        <tr>
                            <td style="line-height: 10px;" colspan="2"></td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <fieldset>
                                                            <legend class="label" style="margin-bottom:-15px;">Standard Payment</legend>
                                                            <table width="800px">
                                                                <tr>
                                                                    <td align="left" class="label" style="border-right: 1px solid; color: Black">
                                                                        Monthly Service Charge($):<br />
                                                                        <asp:TextBox ID="txt_Monthly_Service_Charge" runat="server" CssClass="txt" 
                                                                            Width="60px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left" class="label" style="border-right: 1px solid; color: Black">
                                                                        Fuel Adjustment Code:<br />
                                                                        <telerik:RadComboBox ID="ddl_fuelcode" runat="server" CssClass="txt" 
                                                                            DataSourceID="SqlDataSource1" DataTextField="fuelCode" DataValueField="id" 
                                                                            Width="85px" MarkFirstMatch="true">
                                                                        </telerik:RadComboBox>
                                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                                            ConnectionString="<%$ databaseExpression:client_database %>" 
                                                                            SelectCommand="SELECT [fuelCode], [id] FROM [touFuelAdjust] WHERE ([isActive] = @isActive)">
                                                                            <SelectParameters>
                                                                                <asp:Parameter DefaultValue="true" Name="isActive" Type="Boolean" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td align="left" class="label" style="border-right: 1px solid; color: Black">
                                                                        Tax Code:<br />
                                                                        <telerik:RadComboBox ID="ddl_taxcode" runat="server" CssClass="txt" 
                                                                            DataSourceID="SqlDataSource2" DataTextField="taxCode" DataValueField="id" 
                                                                            Width="85px" MarkFirstMatch="true">
                                                                        </telerik:RadComboBox>
                                                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                                                            ConnectionString="<%$ databaseExpression:client_database %>" 
                                                                            SelectCommand="SELECT [id], [taxCode] FROM [touTaxAdjust] WHERE ([isActive] = @isActive)">
                                                                            <SelectParameters>
                                                                                <asp:Parameter DefaultValue="true" Name="isActive" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td align="left" class="label" style="border-right: 1px solid; color: Black" 
                                                                        valign="top">
                                                                        Bill Generation Date:<br />
                                                                        <telerik:RadDatePicker runat="server" ID="txt_billdate" CssClass="txt" Width="150px"></telerik:RadDatePicker>
                                                                    </td>
                                                                    <td align="left" class="label">
                                                                        Payment Due:
                                                                        <br />
                                                                        <asp:TextBox ID="txt_paymentdue" runat="server" CssClass="txt" Width="65px"></asp:TextBox>
                                                                        (Days From Bill-gen Date)
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="label" style="border-right: 1px solid; color: Black;" 
                                                                        valign="top">
                                                                        Late Fee:<br />
                                                                        <asp:TextBox ID="txt_latefee" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                                                                        %
                                                                    </td>
                                                                    <td align="left" class="label" style="border-right: 1px solid; color: Black;">
                                                                        Deposit
                                                                        <br />
                                                                        <asp:TextBox ID="txt_standardDeposit" runat="server" CssClass="txt" 
                                                                            Width="60px"></asp:TextBox>
                                                                        $
                                                                    </td>
                                                                    <td align="left" class="label" colspan="2">
                                                                        Grace Period:<br />
                                                                        <asp:TextBox ID="txt_graceperiod" runat="server" CssClass="txt" 
                                                                            onkeypress="javascript:return onlyNumbers(this);" Width="100px"></asp:TextBox>
                                                                        (Days from Pmt Due date)
                                                                    </td>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="5">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_save" runat="server" Text="Create" 
                                                onclick="btn_save_Click" />
                                        </td>
                                        <td style="width: 3px;">
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_cancel" runat="server" Text="Clear" 
                                                onclick="btn_cancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                                    <asp:Label runat="server" ID="lbl_message" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Panel ID="displayDetails_bak" runat="Server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:HiddenField ID="hidden_category" runat="server" />
                                <asp:HiddenField ID="hidd_dsm_prog_id" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    </center>

    <center>
    <asp:PlaceHolder runat="server" ID="placeholder_master"></asp:PlaceHolder>
    </center>

    <center>
    <asp:PlaceHolder runat="server" ID="placeholder_barchart"></asp:PlaceHolder>
    </center>


    <table align="center">
        <tr align="center">
            <td>
                <asp:Panel ID="displayDetails" runat="Server" CssClass="placeholder"></asp:Panel>
            </td>
        </tr>
    </table>
    </telerik:RadAjaxPanel>
</asp:Content>
