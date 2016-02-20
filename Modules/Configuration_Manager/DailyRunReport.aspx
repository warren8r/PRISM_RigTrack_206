<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="DailyRunReport.aspx.cs" Inherits="Modules_Configuration_Manager_DailyRunReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function OnClientSelectedIndexChanged(sender, eventArgs) {
        var item = eventArgs.get_item();


        var comboh1 = $find('<%=radcombo_hr1.ClientID %>');
        comboh1.trackChanges();
        comboh1.set_value(item.get_value());
        comboh1.set_text(item.get_text());

        var comboh2 = $find('<%=radcombo_hr2.ClientID %>');
        comboh2.trackChanges();
        comboh2.set_value(item.get_value());
        comboh2.set_text(item.get_text());

        var comboh3 = $find('<%=radcombo_hr3.ClientID %>');
        comboh3.trackChanges();
        comboh3.set_value(item.get_value());
        comboh3.set_text(item.get_text());

        var comboh4 = $find('<%=radcombo_hr4.ClientID %>');
        comboh4.trackChanges();
        comboh4.set_value(item.get_value());
        comboh4.set_text(item.get_text());

        var comboh5 = $find('<%=radcombo_hr5.ClientID %>');
        comboh5.trackChanges();
        comboh5.set_value(item.get_value());
        comboh5.set_text(item.get_text());

        var comboh6 = $find('<%=radcombo_hr6.ClientID %>');
        comboh6.trackChanges();
        comboh6.set_value(item.get_value());
        comboh6.set_text(item.get_text());

        var comboh7 = $find('<%=radcombo_hr7.ClientID %>');
        comboh7.trackChanges();
        comboh7.set_value(item.get_value());
        comboh7.set_text(item.get_text());

        var comboh8 = $find('<%=radcombo_hr8.ClientID %>');
        comboh8.trackChanges();
        comboh8.set_value(item.get_value());
        comboh8.set_text(item.get_text());

        var comboh9 = $find('<%=radcombo_hr9.ClientID %>');
        comboh9.trackChanges();
        comboh9.set_value(item.get_value());
        comboh9.set_text(item.get_text());

        var comboh10 = $find('<%=radcombo_hr10.ClientID %>');
        comboh10.trackChanges();
        comboh10.set_value(item.get_value());
        comboh10.set_text(item.get_text());

        var comboh11 = $find('<%=radcombo_hr11.ClientID %>');
        comboh11.trackChanges();
        comboh11.set_value(item.get_value());
        comboh11.set_text(item.get_text());

        var comboh12 = $find('<%=radcombo_hr12.ClientID %>');
        comboh12.trackChanges();
        comboh12.set_value(item.get_value());
        comboh12.set_text(item.get_text());

        var comboh13 = $find('<%=radcombo_hr13.ClientID %>');
        comboh13.trackChanges();
        comboh13.set_value(item.get_value());
        comboh13.set_text(item.get_text());

        var comboh14 = $find('<%=radcombo_hr14.ClientID %>');
        comboh14.trackChanges();
        comboh14.set_value(item.get_value());
        comboh14.set_text(item.get_text());

        var comboh15 = $find('<%=radcombo_hr15.ClientID %>');
        comboh15.trackChanges();
        comboh15.set_value(item.get_value());
        comboh15.set_text(item.get_text());

        var comboh16 = $find('<%=radcombo_hr16.ClientID %>');
        comboh16.trackChanges();
        comboh16.set_value(item.get_value());
        comboh16.set_text(item.get_text());

        var comboh17 = $find('<%=radcombo_hr17.ClientID %>');
        comboh17.trackChanges();
        comboh17.set_value(item.get_value());
        comboh17.set_text(item.get_text());

        var comboh18 = $find('<%=radcombo_hr18.ClientID %>');
        comboh18.trackChanges();
        comboh18.set_value(item.get_value());
        comboh18.set_text(item.get_text());

        var comboh19 = $find('<%=radcombo_hr19.ClientID %>');
        comboh19.trackChanges();
        comboh19.set_value(item.get_value());
        comboh19.set_text(item.get_text());

        var comboh20 = $find('<%=radcombo_hr20.ClientID %>');
        comboh20.trackChanges();
        comboh20.set_value(item.get_value());
        comboh20.set_text(item.get_text());

        var comboh21 = $find('<%=radcombo_hr21.ClientID %>');
        comboh21.trackChanges();
        comboh21.set_value(item.get_value());
        comboh21.set_text(item.get_text());

        var comboh22 = $find('<%=radcombo_hr22.ClientID %>');
        comboh22.trackChanges();
        comboh22.set_value(item.get_value());
        comboh22.set_text(item.get_text());

        var comboh23 = $find('<%=radcombo_hr23.ClientID %>');
        comboh23.trackChanges();
        comboh23.set_value(item.get_value());
        comboh23.set_text(item.get_text());

        var comboh24 = $find('<%=radcombo_hr24.ClientID %>');
        comboh24.trackChanges();
        comboh24.set_value(item.get_value());
        comboh24.set_text(item.get_text());
        comboh24.set_text(item.get_text());

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
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td align="left">
                            Select Job:<br />
                            <telerik:RadComboBox ID="radcombo_job" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radcombo_job_SelectedIndexChanged">
                                
                            </telerik:RadComboBox>
                        </td>
                        
                        <td align="left">
                            Select Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        
                        <td align="left">
                            <br />
                            <asp:Button ID="btn_view" runat="server"  Text="View" onclick="btn_view_Click" />
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
                            Job Name:
                        </td>
                        <td >
                            Job Type:
                        </td>
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
                        <td >
                            City:
                        </td>
                        <td >
                            State:
                        </td>
                        <td >
                            Country:
                        </td>
                        <td >
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
                            <asp:Label ID="lbl_jname" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_jtype" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_sdate" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_edate" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_rigtype" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_address" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_city" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_state" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_country" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_zip" ForeColor="Yellow" runat="server"></asp:Label>
                        </td>
                        <td style="background-color:Red" align="center">
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
            <td align="center" id="td_dailylog" runat="server" visible="false">
                <table>
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        Select Personnel:
                                        <telerik:RadComboBox runat="server" ID="combo_ex_personal" CheckBoxes="true"
                                             EnableCheckAllItemsCheckBox="true" Width="300px" DataTextField="Personname" DataValueField="userid" EmptyMessage="- Select -">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td align="right" >
                                        <asp:Button ID="btn_saveupdate2" Visible="false" runat="server" Text="Save/Update" OnClick="btn_save_OnClick" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_exporttoexceltop" runat="server" Text="Export to excel" OnClick="btn_export_OnClick" />
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <%--<div style="overflow:scroll; width:600px">--%>
                            <asp:Panel ID="pnl_addjobs" runat="server" Visible="false"></asp:Panel>

                            <%--</div>--%>
                        </td>
                        <td align="left" valign="top" >
                            <table >
                                
                                <tr>
                                    <td>
                                        <table style="border:solid 1px #000000" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td colspan="6" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>
                                                    <table width="100%">
                                                        <tr>
                                                            <td  >
                                                                Daily Activity Log
                                                            </td>
                                                            <td align="center">&#160;&#160;&#160;<asp:LinkButton ID="lnk_eventlog" Font-Bold="true" ForeColor="White" runat="server" Text="Create&#160;New Activity" OnClientClick="openwin();return false" /> <br /> </td>
                                                            <td align="right"  >
                                                                Select All:
                                                                <telerik:RadComboBox ID="radcombo_selectall" runat="server" Width="100px" EmptyMessage="- Select -" 
                                                                DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid" onclientselectedindexchanged="OnClientSelectedIndexChanged">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>6:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr1" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>7:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr2" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>8:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr3" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>9:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr4" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>10:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr5" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>11:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr6" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                    
                                            </tr>
                                
                                            <tr>
                                               <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>12:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr7" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>13:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr8" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>14:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr9" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>15:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr10" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>16:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr11" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>17:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr12" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>18:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr13" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>19:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr14" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>20:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr15" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>21:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr16" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>22:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr17" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>23:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr18" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>0:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr19" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>1:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr20" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>2:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr21" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>3:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr22" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>4:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr23" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr><td align="center" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>5:00</td></tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_hr24" runat="server" Width="100px" EmptyMessage="- Select -" DataSourceID="sql_status" DataTextField="rigstatuses" DataValueField="sid">
                                                        
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table >
                                            <tr>
                                                <td valign="top">
                                                    <table style="border:solid 1px #000000" width="270px">
                                                        <tr>
                                                            <td colspan="2" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>
                                                                Daily Progress
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Depth Start:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_depthstrt" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Depth End:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_depthend" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Last INC:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_lastinc" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Last AZM:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_lastazm" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Last Temp:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_lasttemp" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                    <table>
                                                        <tr>
                                                            <td  align="left" valign="top">
                                                                Comments:<br />
                                                                <asp:TextBox ID="txt_cmts" runat="server" TextMode="MultiLine" Height="40px" Width="240px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <table style="border:solid 1px #000000" width="270px">
                                                        <tr>
                                                            <td colspan="2" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>
                                                                Drilling Parameters
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Pump Pressure:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_pumppressure" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Flow Rate:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_flowrate" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Mud Weight:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_mudwght" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Chlorides:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_floride" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Pulse Amp:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_pulseamp" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                % Sand:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_sand" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                % Solid:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_solid" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="border:solid 1px #000000" width="270px">
                                                        <tr>
                                                            <td colspan="2" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>
                                                                Assets Needed
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_assetneeded1" runat="server" Width="235px" EmptyMessage="- Select -" DataSourceID="sqlassets" DataTextField="AssetName" DataValueField="Id">
                                                                    
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_assetneeded2" runat="server" Width="235px" EmptyMessage="- Select -" DataSourceID="sqlassets" DataTextField="AssetName" DataValueField="Id">
                                                                    
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_assetneeded3" Width="235px" runat="server" EmptyMessage="- Select -" DataSourceID="sqlassets" DataTextField="AssetName" DataValueField="Id">
                                                                    
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_assetneeded4" runat="server" Width="235px" EmptyMessage="- Select -" DataSourceID="sqlassets" DataTextField="AssetName" DataValueField="Id">
                                                                    
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadComboBox ID="radcombo_assetneeded5" runat="server" Width="235px" EmptyMessage="- Select -" DataSourceID="sqlassets" DataTextField="AssetName" DataValueField="Id">
                                                                    
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <table style="border:solid 1px #000000"  width="270px">
                                                        <tr>
                                                            <td colspan="2" style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>
                                                                Other Supplies Needed
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txt_othersuppliesneeded1"  Width="240px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txt_othersuppliesneeded2"  Width="240px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txt_othersuppliesneeded3"  Width="240px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txt_othersuppliesneeded4"  Width="240px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txt_othersuppliesneeded5"  Width="240px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Daily Charges($):<br />
                                                    <asp:TextBox ID="txt_dailycharges"  Width="240px" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Upload Documents:<br />
                                                    <telerik:RadAsyncUpload id="RadAsyncUpload1" runat="server"></telerik:RadAsyncUpload>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" AllowSorting="True" CssClass="mdmGrid active"
                                                        CellSpacing="0"  GridLines="None" Width="80%">
                                                        
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
                                            <tr>
                                                
                                                <td align="center" colspan="2">
                                                    <asp:CheckBox ID="chk_runfinish" runat="server" Text="Run Finished" />&nbsp;
                                                    <asp:Button ID="btn_save" Visible="false" runat="server" OnClick="btn_save_OnClick" Text="Save/Update" />
                                                    &nbsp;<asp:Button ID="btn_export" runat="server" Text="Export to excel" OnClick="btn_export_OnClick" />
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
                 <telerik:RadWindowManager ID="RadWindowManager1" runat="server"  Modal="true" Animation="Resize"> 
                                     <Windows> 
                                     <telerik:RadWindow ID="window_eventlog" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit Event Log">
 
                                        <ContentTemplate>
 
                                           <iframe id="iframe1" runat="server" width="960px" height="600px" src="../../Modules/Configuration_Manager/ManageEventLog.aspx">
                                               
                                            </iframe>
                                            
                                         <%-- <uc1:ManageEventLog ID="ManageEventLog1" runat="server" />--%>
 
                                         </ContentTemplate>
 
                                     </telerik:RadWindow>
                                     </Windows>
                        </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hid_runid" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_exporttoexceltop" />
        <asp:PostBackTrigger ControlID="btn_export" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

