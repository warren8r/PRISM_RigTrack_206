<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageDataEntry.aspx.cs" Inherits="Modules_Configuration_Manager_ManageDataEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadCodeBlock runat="server" ID="rdbScripts">
          <script type="text/javascript">
              function OnError(sender, args) {
                  alert('Invalid date');

              }

              function togglePopupModality1(objdate,objid) {

                  var url1 = "AddSignaturetoApproveDataEntry.aspx?jid=" + objid + "&date=" + objdate + "";
                  $find("<%=RadWindow2.ClientID %>").setUrl(url1);
                  $find("<%=RadWindow2.ClientID %>").show();
              }

              function togglePopupModality1_notes(objdate,objid) {
                  var url = "AddNotestoDataEntry.aspx?jid=" + objid + "&date=" + objdate + "";

                  $find("<%=radwindow_notes.ClientID %>").setUrl(url);
                  $find("<%=radwindow_notes.ClientID %>").show();
              }


              
          </script>
</telerik:RadCodeBlock>
<script type="text/javascript">
    function AddNotes(objjid,objdate,notes) {


        //send a query to server side to present new content
        $.ajax({
            type: "POST",
            url: "ManageDataEntry.aspx/addnotes",
            data: "{jid:'" + objjid + "',whichdatenotes:'" + objdate + "',notestext:'" + notes + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                

            }

        });
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
                        <td>
                            Select Job:<br />
                            <telerik:RadComboBox ID="radcombo_job" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radcombo_job_SelectedIndexChanged">
                                
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            Start Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left">
                            End Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_end" Width="130px">
                                <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput2" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left">
                            <br />
                            <asp:Button ID="btn_view" runat="server" Text="View"  onclick="btn_view_Click" />
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" id="td_jobdet" runat="server" style="display:none">
                <table style="border:solid 1px #000000">
                    <tr>
                        <td style="border-right:solid 1px #000000">
                            Job Name:<asp:Label ID="lbl_jname" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                        <td style="border-right:solid 1px #000000">
                            Job Type:<asp:Label ID="lbl_jtype" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                        <td style="border-right:solid 1px #000000">
                            StartDate:<asp:Label ID="lbl_sdate" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                        <td>
                            EndDate:<asp:Label ID="lbl_edate" ForeColor="Blue" runat="server"></asp:Label>
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
                            <div style="overflow:scroll; width:1260px">
                            <asp:Panel ID="pnl_addjobs" runat="server" Visible="false"></asp:Panel>

                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" id="td_button" runat="server" style="display:none">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn_saveupdate" runat="server" Text="Save/Update" 
                                onclick="btn_saveupdate_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_export" runat="server" Text="Export To Excel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadWindowManager ID="RadWindowManager2"  runat="server"   
                                            Modal="true"  >
                    <Windows>
                            <telerik:RadWindow ID="RadWindow2" runat="server" VisibleStatusbar="false" Title="Approve"  Width="400px"
                                Height="400px" >
                                
                                        
                            </telerik:RadWindow>

                           
                        </Windows>
                        </telerik:RadWindowManager>
            </td>
        </tr>
        <tr>
            <td>
                 <telerik:RadWindowManager ID="RadWindowManager1"  runat="server"   Modal="true"  >
                 <Windows>
                               <telerik:RadWindow ID="radwindow_notes" VisibleStatusbar="false" Title="Add Notes"  runat="server"  Width="400px"
                                Height="400px" >
                               
                            </telerik:RadWindow>    
                            
                    </Windows>          
                </telerik:RadWindowManager>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="sql_status" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                    SelectCommand="select * from RigStatusDet order by sid desc">
                        <SelectParameters>
                        
                        </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
     </ContentTemplate>
    
    </asp:UpdatePanel>
</asp:Content>

