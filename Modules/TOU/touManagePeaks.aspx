<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="touManagePeaks.aspx.cs" Inherits="Modules_TOU_touManagePeaks" %>

<%@ Reference Control="WebUserControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
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

<%--    <script type='text/javascript'>
        $(function () {
            var availableTypes = <%= getTypes() %>
            var availableProgs = <%= getProgs() %>
            populateAutoComplete('<%= txtProgType.ClientID %>', availableTypes);
            populateAutoComplete('<%= txtProgName.ClientID %>', availableProgs);
        });
    </script>--%>
    <script>
        //steelblue
        function validation() {
            if (document.getElementById('txtProgName').value == "-- Select --") {
                document.getElementById('txtProgName').focus();
                document.getElementById('lbl_message').innerHTML = "Select Programename";
                document.getElementById('lbl_message').style.color = "red";
                window.scrollTo(0, 0);
                return false;
            }
            if (document.getElementById('txt_peakname').value == "") {
                document.getElementById('txt_peakname').select();
                document.getElementById('txt_peakname').focus();
                document.getElementById('lbl_message').innerHTML = "Enter Peakname";
                document.getElementById('lbl_message').style.color = "red";
                window.scrollTo(0, 0);
                return false;
            }
        }
        function openwindow() {
            var newwindow;
            newwindow = window.open("Popup_peaktype.aspx", "mywindow", "width=300,height=300,scrollbar=no,directories=no,toolbars=no,menubars=no,address=no,status=no");
            newwindow.moveTo(500, 200);

            return false;
        }
        function getValue(control, strhr, stophr) {

            var as = document.getElementById(control.id).value;

            var rowIndex = control.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.rowIndex;

            //var controlid_strhr = "ctl00_ContentPlaceHolder1_ET_CONTROL_MANAGE_PEAKS1_gv_peaks_ctl02_" + strhr;
            //var controlid_stophr = "ctl00_ContentPlaceHolder1_ET_CONTROL_MANAGE_PEAKS1_gv_peaks_ctl02_" + stophr;
            var controlid_strhr = "ContentPlaceHolder1_gv_peaks_" + strhr + "_" + rowIndex;
            var controlid_stophr = "ContentPlaceHolder1_gv_peaks_" + stophr + "_" + rowIndex;


//            if (as == "Select") {
//                document.getElementById(controlid_strhr).disabled = true;
//                document.getElementById(controlid_strhr).value = "";
//                document.getElementById(controlid_stophr).disabled = true;
//                document.getElementById(controlid_stophr).value = "";
//            }
//            else {
//                document.getElementById(controlid_strhr).disabled = false;
//                document.getElementById(controlid_stophr).disabled = false;
//            }

        }

       
    </script>
    <style>
        .clsLgnTxBx
        {
            font-size: 9px !important; 
            WIDTH:30px !important;
            border-right:  1px solid !important;
            border-top:  1px solid !important;
            border-left:  1px solid !important;
            border-bottom:  1px solid !important;
        }
        .dropdown
        {
            font-family: Verdana !important; 
            font-size: 7pt !important; 
            background-color: #DCE7C6;
            border-right:  1px solid !important;
            border-top:  1px solid !important;
            border-left:  1px solid !important;
            border-bottom:  1px solid !important;
        }
        .peakTypesScroll {
            overflow-x: hidden;
            overflow-y: scroll;
        }
        div.scroll {
            overflow-x:hidden !important;
            overflow-y:scroll !important;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadAjaxPanel runat="server" OnLoad="RadAjaxPanel1_Load">
    <center>
    <fieldset style="width:310px;">
    <legend style="margin-bottom:-20px;">Time of Use >> Manage Peaks</legend>
    <table border="0" cellpadding="0" cellspacing="0" align="center" style="width:907px !important;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Program Type:<br />
                            <telerik:RadComboBox ID="txtProgType" runat="server" Width="150px" Height="140px" 
                                                    EmptyMessage="- Select -" DataSourceID="SqlDataSource3" AutoPostBack="true"
                                                    DataTextField="typeName" DataValueField="ID" CausesValidation="false"
                                                    MarkFirstMatch="true" onselectedindexchanged="txtProgType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                                ConnectionString="<%$ databaseExpression:client_database %>"
                                                SelectCommand="SELECT [ID], [typeName] FROM [progType]"></asp:SqlDataSource>
                        </td>
                        <td>
                            Program Name:<br />
                            <telerik:RadComboBox ID="txtProgName" runat="server" Width="150px" Height="140px" Enabled="false"
                                                    EmptyMessage="- Select -" DataSourceID="SqlDataSource4" AutoPostBack="true" CausesValidation="false"
                                                    DataTextField="progName" DataValueField="ID" MarkFirstMatch="true" OnSelectedIndexChanged="txtProgName_SelectedIndexChanged">
                            </telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" 
                                                SelectCommand="SELECT [progName], [ID] FROM [touPrograms] WHERE ([progTypeID] = @progTypeID) AND isActive='true' AND ID IN (SELECT PROGRAM_id FROM touSeasonsEP) AND ID NOT IN (SELECT programID FROM touPeaksEP)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtProgType" DefaultValue="" Name="progTypeID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </fieldset>
    </center>

    <table width="100%">
        <tr>
            <td align="center">
                <div id="div_scroll" runat="server" class="scroll" style="width:907px;">
                    <asp:GridView ID="gv_peaks" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="0px" CellPadding="4" Font-Names="Verdana" Font-Size="9px" AutoGenerateColumns="False" ShowHeader="false" OnRowDataBound="gv_peaks_RowDataBound">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <RowStyle BackColor="#D3E0C2" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                        <table border="0" cellpadding="0" cellspacing="1">
                                        <tr>
                                            <td align="center">
                                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td  align="center" style="font-size:9px; font-family:Verdana; color:#ffffff; font-weight:bold;background-color:#4B6C9E;height:20px;">
                                                            <asp:Label ID="lbl_season" runat="server"  Text='<%# Eval("season_name") %>'></asp:Label>
                                                            <asp:Label ID="lbl_Season_ID" runat="server"  Text='<%# Eval("season_id") %>' Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                                   
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" style="border:solid 1px #000000; background-color:#D1DEB6">
                                                                
                                                    <tr>
                                                        <td>
                                                            <table style="border-right:solid 1px #000000">
                                                                            
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr><td colspan="3" align="center" style="font-size:9px; font-family:Verdana; color:#9F0956; font-weight:bold">Week Days</td></tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <%--<tr><td ><asp:LinkButton ID="lnk_week_peak" runat="server" Font-Size="9px" OnClientClick="javascript:return openwindow();" ForeColor="Blue" >Peak Type</asp:LinkButton></td></tr>--%>
                                                                                        <tr><td >Peak Type</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_weekdays1" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_weekdays11','txt_weekdays12')" >
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_weekdays2" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_weekdays21','txt_weekdays22')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_weekdays3" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_weekdays31','txt_weekdays32')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_weekdays4" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_weekdays41','txt_weekdays42')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_weekdays5" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_weekdays51','txt_weekdays52')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Start&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays11" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays21" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays31" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays41" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays51" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="width:5px;"></td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Stop&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays12" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays22" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays32" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays42" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_weekdays52" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table style="border-right:solid 1px #000000">
                                                                            
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr><td colspan="3" align="center" style="font-size:9px; font-family:Verdana; color:#9F0956; font-weight:bold">
                                                                                Holidays</td></tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <%--<tr><td><asp:LinkButton ID="lnk_holoday" runat="server" Font-Size="9px" ForeColor="Blue" OnClientClick="javascript:return openwindow();">Peak Type</asp:LinkButton></td></tr>--%>
                                                                                        <tr><td >Peak Type</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_holiday1" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_holiday11','txt_holiday12')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_holiday2" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_holiday21','txt_holiday22')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_holiday3" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_holiday31','txt_holiday32')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_holiday4" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_holiday41','txt_holiday42')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_holiday5" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_holiday51','txt_holiday52')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Start&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday11" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday21" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday31" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday41" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday51" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="width:5px;"></td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Stop&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday12" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday22" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday32" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday42" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_holiday52" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table style="border-right:solid 1px #000000">
                                                                            
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr><td colspan="3" align="center" style="font-size:9px; font-family:Verdana; color:#9F0956; font-weight:bold">Saturdays</td></tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <%--<tr><td><asp:LinkButton ID="lnk_saturday" runat="server" Font-Size="9px" ForeColor="Blue" OnClientClick="javascript:return openwindow();">Peak Type</asp:LinkButton></td></tr>--%>
                                                                                        <tr><td >Peak Type</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_saturday1" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_saturday11','txt_saturday12')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_saturday2" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_saturday21','txt_saturday22')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_saturday3" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_saturday31','txt_saturday32')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_saturday4" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_saturday41','txt_saturday42')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_saturday5" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_saturday51','txt_saturday52')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Start&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday11" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday21" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday31" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday41" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday51" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="width:5px;"></td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Stop&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday12" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday22" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday32" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday42" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_saturday52" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                            
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr><td colspan="3" align="center" style="font-size:9px; font-family:Verdana; color:#9F0956; font-weight:bold">Sundays</td></tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <%--<tr><td><asp:LinkButton ID="lnk_sunday" runat="server" Font-Size="9px" ForeColor="Blue" OnClientClick="javascript:return openwindow();">Peak Type</asp:LinkButton></td></tr>--%>
                                                                                        <tr><td >Peak Type</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_sunday1" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_sunday11','txt_sunday12')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_sunday2" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_sunday21','txt_sunday22')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_sunday3" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_sunday31','txt_sunday32')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_sunday4" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_sunday41','txt_sunday42')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                        <tr><td><asp:DropDownList ID="ddl_sunday5" runat="server" CssClass="label dropdown"  onclick="javascript:return getValue(this,'txt_sunday51','txt_sunday52')">
                                                                                                    </asp:DropDownList></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Start&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday11" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday21" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday31" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday41" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday51" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="width:5px;"></td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr><td style="font-size:10px;font-family:Verdana; color:Black">Stop&#160;Hr</td></tr>
                                                                                        <tr><td style="height:5px;"></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday12" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday22" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday32" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday42" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
                                                                                        <tr><td><asp:TextBox ID="txt_sunday52" runat="server" CssClass="clsLgnTxBx" Enabled="true"></asp:TextBox></td></tr>
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
                                                    
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle BackColor="White" ForeColor="White" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <telerik:RadButton runat="server" ID="btnConfirm" Text="Create" onclick="btnConfirm_Click"  OnClientClick="javascript:return validation();" Visible="false"></telerik:RadButton>
                <telerik:RadButton runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" Visible="false"></telerik:RadButton><br />
                <asp:Label runat="server" ID="lbl_message" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table><br /><br />
    <table align="center">
    <tr>
        <td align="center">
        <asp:GridView ID="Gv_Prog_Peaks" runat="server" Font-Names="Verdana" Font-Size="9px" Width="450px" AutoGenerateColumns="False" OnRowCommand="Gv_Prog_Peaks_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Program Name">
                    <ItemTemplate>
                    <table  >
                    <tr>
                            <td align="center">
                                <asp:Label id="lbl_Program_Name" runat="server" Text='<%# Eval("progName") %>' Font-Names="Verdana" Font-Size="9px"   ></asp:Label >
                                <asp:Label id="lbl_Program_id" runat="server"  Text='<%# Eval("programID") %>' Visible="False" ></asp:Label>
                            </td>
                    </tr>
                           
                    </table>
                           
                    </ItemTemplate>
                        <ItemStyle CssClass="label" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#4B6C9E" ForeColor="White" />
                            
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program Type">
                        <ItemTemplate>
                            <asp:Label ID="lbl_prog_type" runat="server" Text='<%# Eval("PROGRAM_type") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle BackColor="#4B6C9E" ForeColor="White" CssClass="label" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton id="LinkButton2" runat="server" Text="View Peaks" CommandName="ed" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"></asp:LinkButton >
                        </ItemTemplate>
                        <HeaderStyle BackColor="#4B6C9E" ForeColor="White" CssClass="label" />
                    </asp:TemplateField>
                             
                </Columns>
            </asp:GridView>
        </td>
    </tr>
<tr>
    <td align="center" style="height: 10px">
    </td>
</tr>
<tr>
    <td align="center">
        <asp:PlaceHolder runat="server" ID="placeholder_master"></asp:PlaceHolder>
    </td>
</tr>
<tr>
    <td align="center">
        <asp:Panel ID="panel_seasons" runat="server" CssClass="placeholder"></asp:Panel>
    </td>
</tr>
</table>
</telerik:RadAjaxPanel>
</asp:Content>

