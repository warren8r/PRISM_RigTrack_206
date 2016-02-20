<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="Dailyrunsummary.aspx.cs" Inherits="Modules_Configuration_Manager_Dailyrunsummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
       .scroll {
   width: 1300px; 
   overflow: scroll;
}
    </style>
    <telerik:RadScriptBlock ID="radsc" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }

            function conGenarate(sender, args) {

                if (document.getElementById('<%=radcombo_job.ClientID %>').value == "Select") {
                    radalert('Please Select Job to Generate Summary', 330, 180, 'Alert Box', null, null);
                    args.set_cancel(true);
                }
                else {
                    function callBackFunction(arg) {
                        if (arg == true) {
                            $find("<%=btn_generate.ClientID %>").click();
                        }
                    }
                    radconfirm("Are you sure want Generate Summary!", callBackFunction, 300, 160, null, "Confirmation Box");
                    args.set_cancel(true);

                }
            }
            function conShow(sender, args) {
                if (document.getElementById('<%=radcombo_job.ClientID %>').value == "Select") {
                    radalert('Please Select Job to View Summary', 330, 180, 'Alert Box', null, null);
                    args.set_cancel(true);
                }
            }
            //btn_generate
//        function conGenarate() {
//           // alert(document.getElementById('<%=radcombo_job.ClientID %>').value);
//            if (document.getElementById('<%=radcombo_job.ClientID %>').value == "Select") {
//                alert("Please Select Job to Generate Summary");
//                return false;
//            }
//            else {
//                var r = confirm("Are you sure want Generate Summary!");
//                if (r == true) {
//                    return true;
//                }
//                else {
//                    return false;
//                }
//            }
//        }
//        function conShow() {
//            // alert(document.getElementById('<%=radcombo_job.ClientID %>').value);
//            if (document.getElementById('<%=radcombo_job.ClientID %>').value == "Select") {
//                alert("Please Select Job to View Summary");
//                return false;
//            }
//        }
    </script>
  </telerik:RadScriptBlock>
  <telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
 <asp:UpdatePanel runat="server" ID="updPnl1"  UpdateMode="Always">
            <ContentTemplate>  
              <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="grid_hours">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="grid_hours"></telerik:AjaxUpdatedControl>
                            
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
         <table cellpadding="0" cellspacing="0" border="0" width="1024px">
        <tr>
            <td>
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                          <td align="left">
                                    Select Job:<br />
                                     <asp:HiddenField ID="hid_runid" runat="server" />
                                    <asp:Label ID="lbl_runnumber" runat="server" ForeColor="White" Font-Bold="true" Font-Size="16px" Visible="false"></asp:Label>
                                    <telerik:RadComboBox ID="radcombo_job" runat="server" AutoPostBack="false" >
                                
                                    </telerik:RadComboBox>
                                </td>  
                       
                        <td><br />
                            <telerik:RadButton ID="btn_generate" runat="server" Text="Generate Summary" OnClick="btn_generate_Click" OnClientClicking="conGenarate" />
                        </td>
                         <td><br />
                            <telerik:RadButton ID="btn_view" runat="server" Text="View Summary" OnClick="btn_view_Click" OnClientClicking="conShow" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
             <tr><td style="line-height:20px"><br /><br /></td></tr>
        <tr>
            <td align="center" >
                <div class="scroll">
                 <telerik:RadGrid ID="grid_hours" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                    AllowAutomaticInserts="True" PageSize="10"  AllowAutomaticUpdates="True" AllowPaging="True" Width="100%" 
                    AutoGenerateColumns="False" DataSourceID="SqlGetRunsummary" OnItemCommand="grid_hours_ItemCommand" OnUpdateCommand="grid_hours_UpdateCommand" >
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="SummaryId" TableLayout="Auto"
                        DataSourceID="SqlGetRunsummary" HorizontalAlign="NotSet" EditMode="InPlace" AutoGenerateColumns="false">
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                        <Columns>
                               <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" HeaderText="Edit" >
                                    <ItemStyle CssClass="MyImageButton" Width="150px"></ItemStyle>
                                </telerik:GridEditCommandColumn>                              
                                   <%-- <telerik:GridTemplateColumn HeaderText="FaileY/N" UniqueName="chk_faileYN">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_faileyn" runat="server" Text='<%#Eval("[FaileY/N]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chk_faileYN" runat="server" Checked='<%#((bool)Eval("[FaileY/N]"))%>' />
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>--%>
                            <telerik:GridCheckBoxColumn DataField="[FaileY/N]" HeaderText="Failed Y/N"  SortExpression="[FaileY/N]" UniqueName="FaileY"></telerik:GridCheckBoxColumn>

                            <telerik:GridBoundColumn  HeaderText="Run Number" DataField="RunNumber" UniqueName="RunNumber" Visible="true"></telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date" DataField="StartDate" UniqueName="StartDate"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn  HeaderText="Strat Time" DataField="StratTime" UniqueName="StratTime" Visible="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn  HeaderText="Summary Id" DataField="SummaryId" UniqueName="SummaryId" Visible="false"></telerik:GridBoundColumn>            
                                        <telerik:GridBoundColumn DataFormatString="{0:dd/MM/yyyy}" HeaderText="End Date" DataField="EndDate" UniqueName="EndDate"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="EndT ime" DataField="EndTime" UniqueName="EndTime" Visible="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Circ Hours" DataField="CircHours" UniqueName="CircHours"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="End Depth" DataField="EndDepth" UniqueName="EndDepth"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Max Temp" DataField="MaxTemp" UniqueName="MaxTemp"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Flow Rate" DataField="FlowRate" UniqueName="FlowRate"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="MudType" DataField="MudType" UniqueName="MudType"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Sand/Solids" DataField="[Sand/Solids]" UniqueName="SandSolids"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="BHAOD" DataField="BHAOD" UniqueName="BHAOD"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Poppet" DataField="Poppet" UniqueName="Poppet"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Orifice" DataField="Orifice" UniqueName="Orifice"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="PulseWidth" DataField="PulseWidth" UniqueName="PulseWidth"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="PulseAmp" DataField="PulseAmp" UniqueName="PulseAmp"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="GR&#160;Pre-Run&#160;Bkgnd" DataField="[GR Pre-Run Bkgnd]" UniqueName="GRPreRunBkgnd"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Gre&#160;Pre-Run&#160;High" DataField="[Gre Pre-Run High]" UniqueName="GrePreRunHigh"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Gre&#160;Post-Run&#160;Bkgnd" DataField="[Gre Post-Run Bkgnd]" UniqueName="GrePostRunBkgnd"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Gre&#160;Post-Run&#160;High" DataField="[Gre Post-Run High]" UniqueName="GrePostRunHigh"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="CalFactor" DataField="CalFactor" UniqueName="CalFactor"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="GammaOffset" DataField="GammaOffset" UniqueName="GammaOffset"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="SurveyOffset" DataField="SurveyOffset" UniqueName="SurveyOffset"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="StartDepth" DataField="StartDepth" UniqueName="StartDepth"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Batt&#160;#1&#160;Voltage&#160;No&#160;Load" DataField="[Batt #1 Voltage No Load]" UniqueName="Batt1VoltageNoLoad"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Batt&#160;#1&#160;Voltage&#160;Load" DataField="[Batt #1 Voltage Load]" UniqueName="Batt1VoltageLoad"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Batt&#160;#2&#160;Voltage&#160;No&#160;Load" DataField="[Batt #2 Voltage No Load]" UniqueName="Batt2VoltageNoLoad"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Batt&#160;#2&#160;Voltage&#160;Load" DataField="[Batt #2 Voltage Load]" UniqueName="Batt2VoltageLoad"></telerik:GridBoundColumn>
                                    </Columns>   
                        <EditFormSettings ColumnNumber="1" CaptionDataField="Name" CaptionFormatString="Edit properties of Summary {0}" >
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
                    <ClientSettings> 
            <Scrolling AllowScroll="True" UseStaticHeaders="True"   SaveScrollPosition="True"> 
            </Scrolling> 
        </ClientSettings> 
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                        <AlternatingItemStyle HorizontalAlign="Center" />
                </telerik:RadGrid>
                    </div>
                 
                <asp:SqlDataSource ID="SqlGetRunsummary" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"                                                       
                                                        
                  SelectCommand="SELECT SummaryId,JobId,RunNumber,[FaileY/N], CONVERT(varchar,StartDate,101) StartDate,StratTime, CONVERT(varchar,EndDate,101) as EndDate,EndTime,CircHours,EndDepth,
                    MaxTemp,FlowRate,MudType,[Sand/Solids],BHAOD,Poppet,Orifice,PulseWidth,PulseAmp,[GR Pre-Run Bkgnd],[Gre Pre-Run High],
                    [Gre Post-Run Bkgnd],[Gre Post-Run High],CalFactor,GammaOffset,SurveyOffset,StartDepth,[Batt #1 Voltage No Load],
                    [Batt #1 Voltage Load],[Batt #2 Voltage No Load],[Batt #2 Voltage Load] FROM [PrismDailyRunSummary] where JobId=@JobId">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="radcombo_job" Name="JobId" DbType="Int32" />
                    </SelectParameters>                                                                                                           
                   
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
     </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

