<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="DailyRunReport_PRISM.aspx.cs" Inherits="Modules_Configuration_Manager_DailyRunReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function OnClientSelectedIndexChanged(sender, eventArgs) {
        var item = eventArgs.get_item();


    }
    function OnClientAdded(sender, args) {
        if (sender.getFileInputs().length == sender.get_maxFileCount()) {
            $telerik.$(".ruDelete").remove();
            $telerik.$(".ruAdd").remove();
        }
    }
    function downloadFile(fileName, downloadLink1) {

        var downloadLink = $get('downloadFileLink');
        var filePath = "../../Documents/" + fileName;
        downloadLink.href = "DownloadHandler.ashx?fileName=" + fileName + "&filePath=" + filePath;
        downloadLink.style.display = 'block';
        downloadLink.style.display.visibility = 'hidden';
        downloadLink.click();

        return false;

    }
    function openwin() {


        window.radopen(null, "window_eventlog");

    }
    function openwinactivity() {


        window.radopen(null, "window_activity");

    }

    function actvalidation() {
        if (document.getElementById('<%=txt_activity.ClientID %>').value == "") {
            document.getElementById('<%=lbl_mes.ClientID %>').innerHTML = "Please Enter Event Name";
            return false;
        }

    }
    function Clicking(sender, args) {


        if (document.getElementById('<%=radcombo_job.ClientID%>').value == "Select") {
            radalert('Please Select Job', 330, 180, 'Alert Box', null, null);
            args.set_cancel(true);
            return false;
        }
        if (document.getElementById('<%=radtxt_start.ClientID%>').value == "") {
            radalert('Please Select Date', 330, 180, 'Alert Box', null, null);
            args.set_cancel(true);
            return false;
        }

    }
    
</script>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function openPreviousHistory() {



                var comboBox1 = $find('<%=radcombo_job.ClientID %>');
                var jobid = comboBox1.get_value();


                var curveGroupID = $find("<%= ddlCompany.ClientID %>");

                var companyID = curveGroupID.get_selectedItem().get_value();
                var url = "ViewPreviousDailyRunReport.aspx?jobID=" + jobid + "&companyID=" + companyID + "";
                var oWnd = $find("<%=radWindowViewHistory.ClientID%>");
                oWnd.setUrl(url);
                oWnd.show();
        
        return false;

    }
        </script>
    </telerik:RadScriptBlock>
    <script src="../../js/PDFCreator.js" type="text/javascript"></script>
    <script type="text/javascript">
        var doc = new jsPDF();
        var specialElementHandlers = {
            '#editor': function (element, renderer) {
                return true;
            }
        };
        function exportPDF()
        {
            var divElements = document.getElementById('content').innerHTML;

            var WindowObject = window.open("", "PrintWindow", "width=750,height=650,top=50,left=50,toolbars=no,scrollbars=yes,status=no,resizable=yes");
            WindowObject.document.writeln(divElements);
            WindowObject.document.close();
            WindowObject.focus();
            WindowObject.print();
            WindowObject.close();
            //doc.fromHTML("<html>"+$('#content').html()+"</html>", 15, 15, {
            //    'width': 170,
            //    'elementHandlers': specialElementHandlers
            //});
            //doc.save('DailyRunReport.pdf');
            return false;
        }
        
    </script>
     <style type="text/css">
        .td {
            background-color:#C4D79B;
            text-align:right;
            border:1px solid black;
        }
         .tdv {
            text-align:left;
            border:1px solid black;
        }
         .RadCalendar_Default .rcMainTable tr .DisabledClass a
        {
            color: #ebe6ca;
        }
    </style>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div align="center" class="contactmain">
                <%--<img src="../../loading1.gif" alt="Loading" /><br />--%>
                <span style="color:Red">Loading Please Wait....</span>
             </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div align="center">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
             <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
                    <td style="padding-left:30px">
                        <h2>Daily Run Report</h2>
                    </td>
                </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                         <td>
                                Select Company<br />
                                <telerik:RadDropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="Select All" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                        <td align="left">
                            Select Job:<br />
                            <telerik:RadComboBox runat="server" ID="radcombo_job" Width="220px"  AutoPostBack="true" OnSelectedIndexChanged="radcombo_job_SelectedIndexChanged" DropDownHeight="220px" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="Select" />
                                </Items>
                            </telerik:RadComboBox>
                            <%--<telerik:RadComboBox ID="radcombo_job" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radcombo_job_SelectedIndexChanged">
                                
                            </telerik:RadComboBox>--%>
                        </td>
                        
                        <td align="left">
                            Select Submission Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                    <DisabledDayStyle CssClass="DisabledClass" />
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                            </telerik:RadDatePicker>
                        </td>
                        
                        <td align="left">
                            <br />
                            <telerik:RadButton ID="btn_view" runat="server"  Text="View" OnClientClicking="Clicking" onclick="btn_view_Click" />
                        </td>
                         <td valign="bottom" align="left">
                                       <asp:Button ID="Button1" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                                    </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" id="td_jobdet" runat="server" style="display:none">
                <table style="background-color:#528ED4;color:White" border="1">
                    <tr>
                        <td >
                            Company:
                        </td>
                        <td >
                            Job Name:
                        </td>
                        <%--<td >
                            Job Type:
                        </td>--%>
                        <td >
                            StartDate:
                        </td>
                        <td >
                            EndDate:
                        </td>
                        <td >
                            Rig Type:
                        </td>
                        <td >
                            Address:
                        </td>
                        <td  style="display:none">
                            City:
                        </td>
                        <td >
                            State:
                        </td>
                        <td >
                            Country:
                        </td>
                        <td  style="display:none">
                            zip:
                        </td>
                        <td style="background-color:Red" align="center">
                            RUN NUMBER
                        </td>
                        <td style="background-color:Red" align="center">
                            DAY NUMBER
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCompany" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_jname" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <%--<td>
                            <asp:Label ID="lbl_jtype" ForeColor="Yellow" runat="server" Visible="false"></asp:Label>
                        </td>--%>
                        <td>
                            <asp:Label ID="lbl_sdate" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lbl_edate" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_rigtype" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_address" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td style="display:none">
                            <asp:Label ID="lbl_city" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_state" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_country" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td style="display:none">
                            <asp:Label ID="lbl_zip" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td style="background-color:Red" align="center">
                            <asp:HiddenField ID="hid_runno" runat="server" />
                            <asp:Label ID="lbl_runnumber" runat="server" ForeColor="White" Font-Bold="true" Font-Size="16px"></asp:Label>
                        </td>
                        <td style="background-color:Green" align="center">
                            <asp:Label ID="lbl_daynumber" runat="server" ForeColor="White" Font-Bold="true" Font-Size="16px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" id="tdtopbuttons" runat="server" visible="false">
                <table>
                    <tr>
                        <td align="right" >
                            <asp:Button ID="btn_saveupdate2" Visible="false" runat="server" Text="Save/Update" OnClick="btn_save_OnClick" />
                        </td>
                        <td>
                            <asp:Button ID="btn_exporttoexceltop" runat="server" Text="Print Report" OnClientClick="return exportPDF();" />
                        </td>
                        <td>
                            <asp:Button ID="btnViewPrevious" runat="server" Text="View Run Report History" OnClientClick="return openPreviousHistory();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            
            <td align="center" id="td_dailylog" runat="server" visible="false">
                <div id="editor"></div>
            <div id="content">
                <table>
                    <tr>
                        <td  align="center" >
                            <table>
                                <tr>
                                    <td id="Td1" align="left" runat="server" visible="false">
                                        Select Personnel:
                                        <telerik:RadComboBox runat="server" ID="combo_ex_personal" CheckBoxes="true"
                                             EnableCheckAllItemsCheckBox="true"  DataTextField="Personname" DataValueField="userid" EmptyMessage="- Select -">
                                        </telerik:RadComboBox>
                                    </td>
                                    
                                </tr>
                                
                            </table>
                        </td>
                    </tr>
                    <tr>
                          <td align="center">
                        <table>
                            <tr>
                                 <td  align="right" valign="top">
                                     <table>
                              
                                           <tr>
                                     <td align="right">
                                            <table runat="server"  id="table_top3">
                                                <tr>
                                                 <td colspan="2" style="background-color:#528ED4;border:1px solid black;color:white;font-weight:bold" align="center">
                                                    <asp:Label ID="lbl_date" runat="server"></asp:Label>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td class="td">OPERATOR:</td>
                                                <td class="tdv"><asp:Label ID="lbl_operator" runat="server"></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td class="td">WELL:</td>
                                                <td class="tdv"><asp:Label ID="lbl_well" runat="server"></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td class="td">LOCATION:</td>
                                                <td class="tdv"><asp:Label ID="lbl_location" runat="server"></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td class="td">RIG:</td>
                                                <td class="tdv"><asp:Label ID="lbl_rig" runat="server"></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td class="td">COMPANY REP:</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txt_companyrep" runat="server"></telerik:RadTextBox> </td>
                                            </tr>
                                             <tr>
                                                <td class="td">JOB No.:</td>
                                                <td class="tdv"><asp:Label ID="lbl_jobsno" runat="server"></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td class="td">DIRECTIONAL DRILLERS:</td>
                                                <td class="tdv"><asp:Label ID="lbl_dir_drillers" runat="server"></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td class="td" valign="top">MWD OPERATORS</td>
                                                <td class="tdv">
                                                     <telerik:RadGrid ID="radgrid_mwdopertors" runat="server" CellSpacing="0" GridLines="None"
                                                         DataSourceID="Sqlmwdopertors"  ShowHeader="false"  AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true"  >                
                                                    <MasterTableView  >
                                                         <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                                                        <Columns>
                                                             <telerik:GridBoundColumn DataField="firstName" HeaderText="First&#160;Name" SortExpression="firstName" UniqueName="firstName" />
                                                             <telerik:GridBoundColumn DataField="lastName" HeaderText="Last&#160;Name" SortExpression="lastName" UniqueName="lastName" />
                                                             <telerik:GridBoundColumn DataField="email" HeaderText="Email" SortExpression="email" UniqueName="email"  Visible="false"/>
                                                         </Columns>
                                                    </MasterTableView>                          
                                                </telerik:RadGrid>
                                                     <asp:SqlDataSource ID="Sqlmwdopertors" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" />
                                                </td>
                                            </tr>
                                                <tr>
                                                    <td colspan="2" style="background-color:#528ED4;border:1px solid black;color:white;font-weight:bold" align="center">
                                                    MUD PULSE
                                                </td>
                                                </tr>
                                                   <tr>
                                                       <td class="td">POPPET&#160;SIZE:</td>
                                                       <td class="tdv">
                                                           <telerik:RadTextBox ID="txt_poppet" runat="server"></telerik:RadTextBox>
                                                       </td>
                                                      
                                                   </tr>
                                                   <tr>
                                                    <td class="td">ORIFICE&#160;SIZE:</td>
                                                    <td class="tdv">
                                                        <telerik:RadTextBox ID="txt_orifice" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">PULSE&#160;WIDTH:</td>
                                                    <td class="tdv">
                                                        <telerik:RadTextBox ID="txt_pulsewidth" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">PULSE&#160;AMPLITUDE:</td>
                                                    <td class="tdv">
                                                        <telerik:RadTextBox ID="txt_pulseamplitude" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">TOTAL&#160;CONNECTED:</td>
                                                    <td class="tdv">
                                                        <telerik:RadTextBox ID="txt_totalconnected" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">TOTAL CIRC:</td>
                                                    <td class="tdv">
                                                        <telerik:RadTextBox ID="txt_totalcirc" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr><td style="line-height:20px"></td></tr>
                                                     <tr>
                                                         <td colspan="2" style="padding-left:85px">
                                                             <table>
                                                                 <tr>
                                                                     <td></td>
                                                                     <td style="background-color: #C4D79B;  text-align:center; border:1px solid black;">START</td>
                                                                     <td style="background-color: #C4D79B;  text-align:center; border:1px solid black;">END</td>                                                                    
                                                                 </tr>
                                                                 <tr>
                                                                     <td class="td">DEPTH</td>
                                                                     <td class="tdv">
                                                                        <telerik:RadTextBox ID="txt_depthstart" runat="server" Width="80px"></telerik:RadTextBox>
                                                                    </td> 
                                                                     <td class="tdv">
                                                                        <telerik:RadTextBox ID="txt_depthend1" runat="server" Width="80px"></telerik:RadTextBox>
                                                                    </td> 
                                                                 </tr>
                                                                 <tr>
                                                                      <td class="td">INC.</td>
                                                                       <td class="tdv">
                                                                        <telerik:RadTextBox ID="txt_incstart" runat="server" Width="80px"></telerik:RadTextBox>
                                                                       </td> 
                                                                      <td class="tdv">
                                                                        <telerik:RadTextBox ID="txt_incend" runat="server" Width="80px"></telerik:RadTextBox>
                                                                       </td>
                                                                 </tr>
                                                                 <tr>
                                                                      <td class="td">AZM.</td>
                                                                        <td class="tdv">
                                                                            <telerik:RadTextBox ID="txt_azmstart" runat="server" Width="80px"></telerik:RadTextBox>
                                                                        </td>  
                                                                      <td class="tdv">
                                                                            <telerik:RadTextBox ID="txt_azmend" runat="server" Width="80px"></telerik:RadTextBox>
                                                                        </td> 
                                                                 </tr>
                                                                 <tr>
                                                                      <td class="td">MAGF.</td>
                                                                        <td class="tdv">
                                                                            <telerik:RadTextBox ID="txt_magfstart" runat="server" Width="80px"></telerik:RadTextBox>
                                                                        </td>
                                                                     <td class="tdv">
                                                                            <telerik:RadTextBox ID="txt_magfend" runat="server" Width="80px"></telerik:RadTextBox>
                                                                        </td>  
                                                                 </tr>
                                                                  <tr>
                                                                        <td class="td">GRAV.</td>
                                                                        <td class="tdv">
                                                                            <telerik:RadTextBox ID="txt_gravstart" runat="server" Width="80px"></telerik:RadTextBox>
                                                                        </td> 
                                                                        <td class="tdv">
                                                                            <telerik:RadTextBox ID="txt_gravend" runat="server" Width="80px"></telerik:RadTextBox>
                                                                        </td>                                                    
                                                                   </tr>
                                                                 <tr>
                                                                    <td class="td">DIP.</td>
                                                                    <td class="tdv">
                                                                        <telerik:RadTextBox ID="txt_dipstart" runat="server" Width="80px"></telerik:RadTextBox>
                                                                    </td>  
                                                                     <td class="tdv">
                                                                        <telerik:RadTextBox ID="txt_dipend" runat="server" Width="80px"></telerik:RadTextBox>
                                                                    </td>                                                   
                                                                </tr>
                                                             </table>
                                                         </td>                                                                                                       
                                                </tr>                                             
                                                 
                                                <tr><td style="line-height:20px"></td></tr>
                                                <tr>
                                                 <td colspan="2" style="background-color:#528ED4;border:1px solid black;color:white;font-weight:bold" align="center">
                                                    MUD INFORMATION
                                                </td>
                                             </tr>
                                                 <tr>
                                                     <td class="td">TEMPERATURE(C):</td>
                                                     <td class="tdv">
                                                         <telerik:RadTextBox ID="txt_temp_c" runat="server"></telerik:RadTextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">TEMPERATURE(F):</td>
                                                     <td class="tdv">
                                                         <telerik:RadTextBox ID="txt_temp_f" runat="server"></telerik:RadTextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">AVER. PUMP PRESSURE:</td>
                                                     <td class="tdv">
                                                         <telerik:RadTextBox ID="txt_avr_pump_tressure" runat="server"></telerik:RadTextBox>(PSI)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">AVER. FLOW RATE:</td>
                                                     <td class="tdv">
                                                         <telerik:RadTextBox ID="txt_avre_flow_rate" runat="server"></telerik:RadTextBox>(gpm)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">MUD WEIGHT:</td>
                                                     <td class="tdv">
                                                         <telerik:RadTextBox ID="txt_mud_weight" runat="server"></telerik:RadTextBox>(ppg)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">SOLIDS:</td>
                                                     <td class="tdv">
                                                         <telerik:RadTextBox ID="txt_solids" runat="server"></telerik:RadTextBox>(%)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">SAND:</td>
                                                     <td class="tdv">
                                                         <telerik:RadTextBox ID="txt_sand1" runat="server"></telerik:RadTextBox>(%)<span style="color:gray">max 0.5</span>
                                                     </td>
                                                 </tr>
                                            </table>
                                        </td>
                                </tr>
                              
                                      </table>
                            
                                 </td>
                                <td>
                                    
                                </td>
                                 <td valign="top" align="left"  style="width:75%">
                              <table style="vertical-align:top">
                                    <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td valign="top">
                                        <table id="Table1" runat="server" >
                                                <tr>
                                                 <td colspan="2" style="background-color:#528ED4;border:1px solid black;color:white;font-weight:bold" align="center">
                                                    Daily Information
                                                </td>
                                             </tr>
                                            <tr>
                                                <td class="td">Hole Size:</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtHoleSize" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="td">Day Start Depth:</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtDayStartDepth" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="td">Midnight Depth:</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtMidNightDepth" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="td">Agitator In Use?:</td>
                                                <td class="tdv">
                                                    <telerik:RadComboBox ID="ddlAgrigatorInUse" runat="server" Width="200px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Select" Value="0" />
                                                            <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                            <telerik:RadComboBoxItem Text="No" Value="No" />
                                                        </Items>
                                                    </telerik:RadComboBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">Agitator Distance:</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtAgrigatorDistance" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                    </table>
                                        <fieldset style="display:none">
                                            <legend>MWD Hands</legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Day:<br />
                                                        <asp:TextBox ID="txt_day" Width="300px" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Night:<br />
                                                        <asp:TextBox ID="txt_night" Width="300px" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </fieldset>
                                    </td>
                                        <td>
                                            <table id="Table2" runat="server" >
                                                <tr>
                                                 <td colspan="2" style="background-color:#528ED4;border:1px solid black;color:white;font-weight:bold" align="center">
                                                    Battery Information
                                                </td>
                                             </tr>
                                            <tr>
                                                <td class="td">Day Start Amp. Hrs (EM):</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtDayStartAmpHrs" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="td">Amp Hrs Used Today (EM):</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtAmpHrsUsedTodat" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="td">Amp Hrs Remaining (EM):</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtAmpHrsRemaining" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="td">Day End Volts: (Bat1):</td>
                                                <td class="tdv">
                                                    <telerik:RadTextBox ID="txtDayEndVoltsBat1" runat="server" Width="200px"></telerik:RadTextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">Day Start Volts: (Bat2):</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtDayStartVoltsBat2" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                                <tr>
                                                <td class="td">Day Start Volts: (Bat1):</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtDayStartVoltsBat1" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                                 <tr>
                                                <td class="td">Day End Volts: (Bat2):</td>
                                                <td class="tdv"><telerik:RadTextBox ID="txtDayEndVoltsBat2" runat="server" Width="200px"></telerik:RadTextBox></td>
                                            </tr>
                                    </table>
                                        </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                   <tr>
                                         <td valign="top">   
                                             <fieldset>
                                                 <legend>Tool&#160;Circulation&#160;Information</legend>
                                                 <div style="vertical-align:top">
                                                      <asp:Panel ID="pnl_addjobs" runat="server" Visible="false"></asp:Panel>
                                                 </div>
                                               
                                             </fieldset>                                        
                                                 
                                        </td>
                                    
                                </tr>
                                  <tr>
                                      <td>
                                          <fieldset>
                                                 <legend>BHA&#160;Information</legend>
                                          <telerik:RadGrid ID="radgrdMeterList" runat="server" CellSpacing="0" GridLines="None"
                                                 AllowFilteringByColumn="True" width="800px" Visible="false"
                                                OnCustomAggregate="radgrdMeterList_CustomAggregate">
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
            
                                                </ClientSettings>
                                                <MasterTableView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" AllowFilteringByColumn="True" PageSize="10">
                                                    <PagerStyle Position="Bottom" PageSizeControlType="RadComboBox" AlwaysVisible="true"></PagerStyle>
                                                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                                                        ShowRefreshButton="False" />
                                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Status" DataType="System.Boolean" FilterControlAltText="Filter Status column"
                                                            HeaderText="Status" SortExpression="Status" AllowFiltering="false" UniqueName="Status">
                                                            <HeaderStyle Width="80px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkActive" runat="server" />
                        
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <%--<telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                                                            Text="Edit" UniqueName="btnEdit" HeaderText="Edit">
                                                        </telerik:GridButtonColumn>--%>
                                                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                                                            HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID" Display="false">
                                                        </telerik:GridBoundColumn>
                
                
                                                        <telerik:GridBoundColumn DataField="assetcategory" FilterControlAltText="Filter meterType column"
                                                            HeaderText="Item" SortExpression="assetcategory" AllowFiltering="false" UniqueName="assetcategory">
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridBoundColumn DataField="AssetName" AllowFiltering="false" FilterControlAltText="Filter meterName column"
                                                            HeaderText="Tool Type" SortExpression="AssetName" UniqueName="AssetName">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="serialNumber"  AllowFiltering="false" FilterControlAltText="Filter serialNumber column"
                                                            HeaderText="Serial #" SortExpression="serialNumber" UniqueName="serialNumber">
                                                            <HeaderStyle Width="80px" />
                    
                                                        </telerik:GridBoundColumn>--%>
                                                        <telerik:GridBoundColumn DataField="TopConnection" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                                                            HeaderText="Top Connection" SortExpression="TopConnection" UniqueName="TopConnection">
                                                        </telerik:GridBoundColumn>
                                                         
                                                       <%-- <telerik:GridBoundColumn DataField="Description" AllowFiltering="false" FilterControlAltText="Filter Description column"
                                                            HeaderText="Description" SortExpression="Description" UniqueName="Description">
                                                        </telerik:GridBoundColumn>--%>
                
                                                        <telerik:GridBoundColumn DataField="ODFrac" AllowFiltering="false" FilterControlAltText="Filter ODFrac column"
                                                            HeaderText="OD Frac" SortExpression="ODFrac" UniqueName="ODFrac">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="IDFrac" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                                                            HeaderText="ID Frac" SortExpression="IDFrac" UniqueName="IDFrac">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Length" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                                                            HeaderText="Length" SortExpression="Length" UniqueName="Length">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Length" AllowFiltering="false" HeaderText="Total"
                                                            UniqueName="Length" Aggregate="Custom">
                                                         </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                        </EditColumn>
                                                    </EditFormSettings>
            
                                                </MasterTableView>
        
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                              </fieldset>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <table>
                                              <tr>
                                                  <td>
                                                      <fieldset>
                                                          <legend>24&#160;Hour&#160;Activity</legend>
                                                          <table>
                                                                                                                      
                                                            <tr>
                                                                <td>
                                                                    <asp:HiddenField ID="hidd_runno" runat="server" />
                                                        <telerik:RadGrid ID="grid_hours" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" Width="800px" AllowAutomaticUpdates="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" DataSourceID="SqlDataSource1"  OnItemCommand="grid_hours_ItemCommand">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="ActivityId" 
                                                            DataSourceID="SqlDataSource1" HorizontalAlign="NotSet" EditMode="InPlace" AutoGenerateColumns="false">
                                                            <CommandItemSettings AddNewRecordText="Add New 24 Hour Activity" />
                                                            <Columns>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                                                    <ItemStyle CssClass="MyImageButton"></ItemStyle>
                                                                </telerik:GridEditCommandColumn>
                                                                <%--<telerik:GridBoundColumn DataField="Name" HeaderText="Holiday Name" SortExpression="Name"
                                                                    UniqueName="Name" ColumnEditorID="GridTextBoxColumnEditor1">
                                            
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridDateTimeColumn  UniqueName="Date" DataFormatString="{0:MM/dd/yyyy}"  ColumnEditorID="GridTextBoxColumnEditor2"  DataField="Date" HeaderText="Date"  ></telerik:GridDateTimeColumn>
                                                                --%>
                                       
                                                                <telerik:GridDropDownColumn  DataField="StartTime" DataSourceID="SqlGet24Hours" HeaderText="Start Time"
                                                                        ListTextField="Time" ListValueField="TimeId" UniqueName="StartTime">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" /> </telerik:GridDropDownColumn>
                                                                <telerik:GridDropDownColumn  DataField="EndTime" DataSourceID="SqlGet24Hours" HeaderText="End Time"
                                                                        ListTextField="Time" ListValueField="TimeId" UniqueName="EndTime">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" /> </telerik:GridDropDownColumn>

                                                                <telerik:GridDropDownColumn  DataField="24HourActivity" DataSourceID="SqlGet24HourActivity" HeaderText="24&#160;Hour&#160;Activity"
                                                                        ListTextField="24HourActivity" ListValueField="HourActivityId" UniqueName="24HourActivity">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" /> </telerik:GridDropDownColumn>
                                      
                                                                <telerik:GridBoundColumn DataField="Comments" HeaderText="Comments" SortExpression="Comments"
                                                                    UniqueName="Comments" ColumnEditorID="GridTextBoxColumnEditor2"/>

                                                                    <telerik:GridButtonColumn ConfirmText="Delete this Activity?" ConfirmDialogType="RadWindow"
                                                                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                                    UniqueName="DeleteColumn">
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton"></ItemStyle>
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <EditFormSettings ColumnNumber="1" CaptionDataField="Name" CaptionFormatString="Edit properties of Holiday {0}"
                                                                InsertCaption="New 24&#160;Hour&#160;Activity">
                                                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3"
                                                                    Width="100%"></FormMainTableStyle>
                                                                <FormTableStyle CellSpacing="0" CellPadding="2" Height="110px">
                                                                </FormTableStyle>
                                                                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                <EditColumn ButtonType="ImageButton" InsertText="Insert Holiday" UpdateText="Update record"
                                                                    UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                                                                </EditColumn>
                                                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                            </EditFormSettings>
                                                        </MasterTableView>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                            <AlternatingItemStyle HorizontalAlign="Center" />
                                                    </telerik:RadGrid>
                                            <asp:SqlDataSource ID="SqlGet24Hours" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                            SelectCommand="select 0 as [TimeId], 'Select Time' as [Time] union SELECT [TimeId], convert(varchar,[Time]) as [Time] FROM [Prism24Hours]">
                                                                </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlGet24HourActivity" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                            SelectCommand="select 0 as [HourActivityId], 'Select Activity' as [24HourActivity] union  SELECT [HourActivityId], [24HourActivity] FROM [Prism24HourActivity]">
                                                                </asp:SqlDataSource>
                
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                        DeleteCommand="DELETE FROM [PrismJobRun24HourActivityLog] WHERE [ActivityId] = @ActivityId" 

                                                        InsertCommand="INSERT INTO [PrismJobRun24HourActivityLog] ([RunID], [StartTime],[EndTime], [24HourActivity],[Comments]) VALUES
                                                                (@RunID, @StartTime,@EndTime, @24HourActivity,@Comments)"

                                                        SelectCommand="SELECT [ActivityId], [RunID], [StartTime],[EndTime], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=@RunID"
                                                        UpdateCommand="UPDATE [PrismJobRun24HourActivityLog] SET [StartTime] = @StartTime,[EndTime] = @EndTime, [24HourActivity] = @24HourActivity, 
                                                            [Comments] = @Comments WHERE [ActivityId] = @ActivityId">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="hidd_runno" Name="RunID" DbType="Int32" />
                                                        </SelectParameters>
                                                        <DeleteParameters>
                                                            <asp:Parameter Name="ActivityId" Type="Int32"></asp:Parameter>
                                                        </DeleteParameters>
                                                        <InsertParameters>
                                                            <asp:ControlParameter ControlID="hidd_runno" Name="RunID" DbType="Int32" />
                                                            <asp:Parameter Name="StartTime" Type="Int32"></asp:Parameter>
                                                            <asp:Parameter Name="EndTime" Type="Int32"></asp:Parameter>
                                                            <asp:Parameter Name="24HourActivity" Type="Int32"></asp:Parameter>
                                                            <asp:Parameter Name="Comments" Type="String"></asp:Parameter>                                                           
                                                        </InsertParameters>
                                                        <UpdateParameters>                                    
                                                            <asp:Parameter Name="StartTime" Type="Int32"></asp:Parameter>
                                                            <asp:Parameter Name="EndTime" Type="Int32"></asp:Parameter>
                                                            <asp:Parameter Name="24HourActivity" Type="Int32"></asp:Parameter>
                                                            <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
                                                            <asp:Parameter Name="ActivityId" Type="Int32"></asp:Parameter>
                                                        </UpdateParameters>
                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                              
                                                          </table>
                                                      </fieldset>
                                                  </td>
                                              </tr>
                                          </table>
                                      </td>
                                  </tr>
                                 
                                  
                                  <tr><td style="line-height:20px"></td></tr>
                                   <tr>
                                                <td >
                                                    <fieldset>
                                                        <legend>Tools Required</legend>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                     <telerik:RadGrid ID="grid_reAsset" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                AllowAutomaticInserts="True" PageSize="10" Width="800px" AllowAutomaticUpdates="True" AllowPaging="True"
                                AutoGenerateColumns="False" DataSourceID="SqlGetAssetQuantity"   OnItemCommand="grid_reAsset_ItemCommand">
                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="AssetQntID" 
                                    DataSourceID="SqlGetAssetQuantity" HorizontalAlign="NotSet" EditMode="InPlace" AutoGenerateColumns="false">
                                    <CommandItemSettings AddNewRecordText="Add New Tool Quantity" />
                                    <Columns>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                            <ItemStyle CssClass="MyImageButton"></ItemStyle>
                                        </telerik:GridEditCommandColumn>
                                      
                                        <telerik:GridDropDownColumn  DataField="AssetID" DataSourceID="SqlGetAssets" HeaderText="Tool&#160;Name"
                                             ListTextField="AssetName" ListValueField="AssetID" UniqueName="AssetID">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" /> </telerik:GridDropDownColumn>
                                      
                                      <telerik:GridBoundColumn DataField="AQntty" HeaderText="Quantity" SortExpression="AQntty"
                                            UniqueName="AQntty" ColumnEditorID="GridTextBoxColumnEditorAQntty"/>

                                         <telerik:GridButtonColumn ConfirmText="Delete this Tool?" ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton"></ItemStyle>
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings ColumnNumber="1" CaptionDataField="Name" CaptionFormatString="Edit properties of Holiday {0}"
                                        InsertCaption="New&#160;Tool&#160;Qunatity">
                                        <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                        <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                        <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3"
                                            Width="100%"></FormMainTableStyle>
                                        <FormTableStyle CellSpacing="0" CellPadding="2" Height="110px">
                                        </FormTableStyle>
                                        <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                        <EditColumn ButtonType="ImageButton" InsertText="Insert Holiday" UpdateText="Update record"
                                            UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                                        </EditColumn>
                                        <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                    </EditFormSettings>
                                </MasterTableView>
                                <HeaderStyle HorizontalAlign="Center" />
                               <ItemStyle HorizontalAlign="Center" />
                                    <AlternatingItemStyle HorizontalAlign="Center" />
                            </telerik:RadGrid>
                         <asp:SqlDataSource ID="SqlGetAssets" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                        
                                              SelectCommand="select AssetName,Id as AssetID,* from PrismAssetName ">
                                           
                              </asp:SqlDataSource>                 
                  <%--  SelectCommand="select AN.AssetName,A.Id as AssetID,* from PrismJobAssignedAssets JA,PrismAssetName AN,Prism_Assets A where JA.AssetId=A.Id
                                     and A.AssetName=AN.Id and  JA.JobId=@Jobid">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="radcombo_job" Name="Jobid" DbType="Int32" />
                                            </SelectParameters>--%>
                                <asp:SqlDataSource ID="SqlGetAssetQuantity" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                DeleteCommand="DELETE FROM [PrismJobRunAssetsRequired] WHERE [AssetQntID] = @AssetQntID" 
                                InsertCommand="INSERT INTO [PrismJobRunAssetsRequired] ([RunID], [AssetID], [JobId],[AQntty]) VALUES
                                     (@RunID, @AssetID, @JobId,@AQntty)"
                                SelectCommand="SELECT [AssetQntID],[RunID], [AssetID], [JobId],[AQntty] FROM [PrismJobRunAssetsRequired] where RunID=@RunID"
                                UpdateCommand="UPDATE [PrismJobRunAssetsRequired] SET [AssetID] = @AssetID, [JobId] = @JobId, 
                                    [AQntty] = @AQntty WHERE [AssetQntID] = @AssetQntID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="hidd_runno" Name="RunID" DbType="Int32" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="AssetQntID" Type="Int32"></asp:Parameter>
                                </DeleteParameters>
                                <InsertParameters>
                                   <asp:ControlParameter ControlID="hidd_runno" Name="RunID" DbType="Int32" />
                                    <asp:Parameter Name="AssetID" Type="Int32"></asp:Parameter>
                                    <asp:ControlParameter ControlID="radcombo_job" Name="JobId"  PropertyName="SelectedValue" DbType="Int32" />
                                    <%--<asp:Parameter Name="JobId" Type="Int32"></asp:Parameter>--%>
                                    <asp:Parameter Name="AQntty" Type="Int32"></asp:Parameter>
                                    <asp:Parameter Name="AssetQntID" Type="String"></asp:Parameter>
                                </InsertParameters>
                                <UpdateParameters>                                    
                                    <asp:Parameter Name="AssetID" Type="Int32"></asp:Parameter>
                                    <%--<asp:Parameter Name="JobId" Type="Int32"></asp:Parameter>--%>
                                    <asp:ControlParameter ControlID="radcombo_job" Name="JobId"  PropertyName="SelectedValue" DbType="Int32" />
                                    <asp:Parameter Name="AQntty" Type="Int32"></asp:Parameter>
                                    <asp:Parameter Name="AssetQntID" Type="String"></asp:Parameter>
                                </UpdateParameters>
                            </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                    
                                                </td>
                                            </tr>
                                  <tr><td style="line-height:20px"></td></tr>
                                  <tr>
                                               <td>
                                                   <table>
                                                       <tr>
                                                            <td >
                                                    Daily Charges($):<br />
                                                    <asp:TextBox ID="txt_dailycharges"  Width="240px" runat="server"></asp:TextBox>
                                                </td>
                                      <td >
                                                    Entered By:<br />
                                                    <asp:TextBox ID="txtEnteredUser"  Width="240px" runat="server"></asp:TextBox>
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
                </div>
            </td>
                
        </tr>
       <tr>
                                                
            <td align="center" id="tdbottmombuttons" runat="server" visible="false">
                <table>
                    <tr>
                        <td align="right">
                            <fieldset>
                                                        <legend> Upload Documents:</legend>
                                                        <table>

                                                            <tr>
                                                                <td>
                                                                    <telerik:RadAsyncUpload id="RadAsyncUpload1" runat="server" Width="800px" ></telerik:RadAsyncUpload>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                     <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" AllowSorting="True" CssClass="mdmGrid active"
                                                        CellSpacing="0"  GridLines="None" Width="800px" Skin="Forest">
                                                        
                                                        <MasterTableView AutoGenerateColumns="False" >
                
                                                            <Columns>
                    
                                                                <telerik:GridBoundColumn DataField="runid" Visible="false"
                                                                    HeaderText="runid" ReadOnly="True" SortExpression="runid"
                                                                    UniqueName="runid">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="DocumentName" 
                                                                    HeaderText="Doc Name" SortExpression="DocumentName" UniqueName="DocumentName">
                                                                </telerik:GridBoundColumn> 
                                        
                    
                                        
                                        
                                                                <telerik:GridTemplateColumn HeaderText="Download" >
                                                                    <ItemTemplate> 
                                                                    <asp:LinkButton ID="lnk_download" runat="server"  OnClientClick='<%# String.Format("downloadFile(\"{0}\",\"{1}\");", Eval("DocumentDisplayName"),this) %>' Text="Download"></asp:LinkButton>                          
                                                                    <a id="downloadFileLink"> </a>
                                                                    <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("runid") %>' style="display:none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("documentid") %>' style="display:none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentDisplayName") %>' style="display:none;"></asp:Label>                         
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn> 
                                                                         
                                                            </Columns>
                                                            <EditFormSettings>
                                                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                                </EditColumn>
                                                            </EditFormSettings>
                                                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                                        </MasterTableView>
                                                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                                        <FilterMenu EnableImageSprites="False">
                                                        </FilterMenu>
                                                    </telerik:RadGrid>
                                                        <%--<asp:SqlDataSource ID="sql_runrpt" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                                                        SelectCommand="SELECT  etod.runid,etod.DocumentID,d.DocumentDisplayName,d.DocumentName from 
                                                        DailyRunReportDocs etod, documents d where d.DocumentID=etod.DocumentID and etod.Uploadeddate=@runid">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="hid_runid" Name="runid" DbType="DateTime" />
                                                            </SelectParameters>
                                                    </asp:SqlDataSource>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:CheckBox ID="chk_runfinish" runat="server" Text="Run Finished" />&nbsp;
                            <asp:Button ID="btn_save" Visible="false" runat="server" OnClick="btn_save_OnClick" Text="Save/Update" />
                            &nbsp;<asp:Button ID="btn_export" runat="server" Text="Print Report" OnClientClick="return exportPDF();"  />
                        </td>
                    
                    </tr>
                </table>
            </td>                            
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="sql_status" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                    SelectCommand="select * from RigStatusDet order by sid desc">
                        <SelectParameters>
                        
                        </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqlassets" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                    SelectCommand="select * from PrismAssetName">
                        <SelectParameters>
                        
                        </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
           <tr>
            <td>
                
                 <telerik:RadWindowManager ID="RadWindowManager_Daily" runat="server"   Modal="true" Animation="Resize"> 
                                     <Windows> 
                                     <telerik:RadWindow ID="radWindowViewHistory" runat="server"  Modal="true" Width="960px"  height="600px" Title="View Previous Daily Run Report">
 
                <%--<ContentTemplate>
 
                    <iframe id="ifrmeHistory" runat="server" width="960px" height="600px">
                                               
                    </iframe>
                                           
                    </ContentTemplate>--%>
 
                </telerik:RadWindow>
                                         
                                      <telerik:RadWindow ID="window_activity" runat="server"  Modal="true" Width="300px"  height="150px" Title="Create New Activity">
                                         <ContentTemplate>
                                             <table>
                                                  <tr>
                                                     <td><asp:Label ID="lbl_mes" runat="server"></asp:Label> </td>
                                                   </tr>
                                                 <tr>
                                                     <td>Activity&#160;Name:</td>
                                                     <td><telerik:RadTextBox ID="txt_activity" runat="server"></telerik:RadTextBox></td>
                                                 </tr>
                                                 <tr><td style="line-height:10px"></td></tr>
                                                 <tr>
                                                      <td><asp:Button ID="btn_Create" Text="Create" OnClientClick="javascript:return actvalidation();" runat="server" OnClick="btn_Create_Click" /></td>
                                                      <td><asp:Button ID="btn_reset" Text="Reset" runat="server" OnClick="btn_reset_Click" /></td>                                                       
                                                 </tr>
                                             </table>
                                        </ContentTemplate>
                                       </telerik:RadWindow>
                                          
                                     </Windows>
                        </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="hid_runid" runat="server" />
    </ContentTemplate>
    <Triggers>
        <%--<asp:PostBackTrigger ControlID="btn_exporttoexceltop" />
        <asp:PostBackTrigger ControlID="btn_export" />--%>
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

