<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ImportDailyRunReport.aspx.cs" Inherits="Modules_Configuration_Manager_ImportDailyRunReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
 .RadUpload .ruFakeInput
 {
  height: 12px!important;
  width:200px!important;
 }
   .td {
            background-color:#C4D79B;
            text-align:right;
            border:1px solid black;
        }
         .tdv {
            text-align:left;
            border:1px solid black;
        }
</style>
 <script language="javascript" type="text/javascript">
     function Clicking(sender, args) {
         // alert(document.getElementById('<%=file_dayrun.ClientID%>').value);

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
         if (document.getElementById('<%=file_dayrun.ClientID%>').value == "") {
             radalert('Please Select File to Import', 330, 180, 'Alert Box', null, null);
             args.set_cancel(true);
             return false;
         }
     }
    </script>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td  align="center">
                     <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
                        <asp:Label ID="lbl_message" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:30px">
                        <h2>Import Daily Run Report</h2>
                    </td>
                </tr>
                <tr>
                    <td  align="center">
                        <table>
                            <tr>
                                <td align="left">
                                    Select Job:<br />
                                     <asp:HiddenField ID="hid_runid" runat="server" />
                                    
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
                               <td>
                                Select&#160;Sheet <br />
                                          <asp:FileUpload ID="file_dayrun" runat="server" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chk_runfinish" runat="server" Text="Run Finished" />
                                </td>
                                <td align="left">
                                    <br />
                                    <telerik:RadButton ID="btn_view" runat="server"  Text="Import" OnClientClicking="Clicking" onclick="btn_view_Click" />
                                </td> 
                            </tr>
                        </table>
                    </td>
                </tr>
               <tr>
                   <td style="line-height:20px;"></td>
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
                            <asp:HiddenField ID="hid_runno" runat="server" />
                            <asp:Label ID="lbl_runnumber" runat="server" ForeColor="White" Font-Bold="true" Font-Size="16px" Visible="true"></asp:Label>
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
                        <td  align="right" valign="top">
                            <table>
                              
                                 <tr>
                                     <td align="right">
                                            <table runat="server"  id="table_top3">
                                                <tr>
                                                 <td colspan="2" style="background-color:#C4D79B;border:1px solid black;" align="center">
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
                          <td valign="top" align="left"  style="width:75%">
                              <table>
                                   <tr>
                                         <td>   
                                             <fieldset>
                                                 <legend>Asset&#160;Run&#160;Hours</legend>
                                                 <asp:Panel ID="pnl_addjobs" runat="server" Visible="false"></asp:Panel>
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
                                                                <td >
                                                                    <asp:HiddenField ID="hidd_runno" runat="server" />
                                                        <telerik:RadGrid ID="grid_hours" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" Width="600px" AllowAutomaticUpdates="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" DataSourceID="SqlDataSource1"  Skin="Forest" OnItemCommand="grid_hours_ItemCommand">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <MasterTableView Width="100%" CommandItemDisplay="TopAndBottom" DataKeyNames="ActivityId" 
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
                                       
                                                                <telerik:GridDropDownColumn  DataField="Time" DataSourceID="SqlGet24Hours" HeaderText="Time"
                                                                        ListTextField="Time" ListValueField="TimeId" UniqueName="Time">
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
                                                        InsertCommand="INSERT INTO [PrismJobRun24HourActivityLog] ([RunID], [Time], [24HourActivity],[Comments]) VALUES
                                                                (@RunID, @Time, @24HourActivity,@Comments)"
                                                        SelectCommand="SELECT [ActivityId], [RunID], [Time], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=@RunID"
                                                        UpdateCommand="UPDATE [PrismJobRun24HourActivityLog] SET [Time] = @Time, [24HourActivity] = @24HourActivity, 
                                                            [Comments] = @Comments WHERE [ActivityId] = @ActivityId">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="hidd_runno" Name="RunID" DbType="Int32" />
                                                        </SelectParameters>
                                                        <DeleteParameters>
                                                            <asp:Parameter Name="ActivityId" Type="Int32"></asp:Parameter>
                                                        </DeleteParameters>
                                                        <InsertParameters>
                                                            <asp:ControlParameter ControlID="hidd_runno" Name="RunID" DbType="Int32" />
                                                            <asp:Parameter Name="Time" Type="String"></asp:Parameter>
                                                            <asp:Parameter Name="24HourActivity" Type="String"></asp:Parameter>
                                                            <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
                                                            <asp:Parameter Name="ActivityId" Type="Int32"></asp:Parameter>
                                                        </InsertParameters>
                                                        <UpdateParameters>                                    
                                                            <asp:Parameter Name="Time" Type="String"></asp:Parameter>
                                                            <asp:Parameter Name="24HourActivity" Type="String"></asp:Parameter>
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
                                                        <legend>Assets Required</legend>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                     <telerik:RadGrid ID="grid_reAsset" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                AllowAutomaticInserts="True" PageSize="10" Width="600px" AllowAutomaticUpdates="True" AllowPaging="True"
                                AutoGenerateColumns="False" DataSourceID="SqlGetAssetQuantity"  Skin="Forest" OnItemCommand="grid_reAsset_ItemCommand">
                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                <MasterTableView Width="100%" CommandItemDisplay="TopAndBottom" DataKeyNames="AssetQntID" 
                                    DataSourceID="SqlGetAssetQuantity" HorizontalAlign="NotSet" EditMode="InPlace" AutoGenerateColumns="false">
                                    <CommandItemSettings AddNewRecordText="Add New Asset Quantity" />
                                    <Columns>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                            <ItemStyle CssClass="MyImageButton"></ItemStyle>
                                        </telerik:GridEditCommandColumn>
                                      
                                        <telerik:GridDropDownColumn  DataField="AssetID" DataSourceID="SqlGetAssets" HeaderText="Asset&#160;Name"
                                             ListTextField="AssetName" ListValueField="AssetID" UniqueName="AssetID">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" /> </telerik:GridDropDownColumn>
                                      
                                      <telerik:GridBoundColumn DataField="AQntty" HeaderText="Quantity" SortExpression="AQntty"
                                            UniqueName="AQntty" ColumnEditorID="GridTextBoxColumnEditorAQntty"/>

                                         <telerik:GridButtonColumn ConfirmText="Delete this Asset?" ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton"></ItemStyle>
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings ColumnNumber="1" CaptionDataField="Name" CaptionFormatString="Edit properties of Holiday {0}"
                                        InsertCaption="New&#160;Asset&#160;Qunatity">
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
                         <%--<asp:SqlDataSource ID="SqlGetAssets" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                            SelectCommand="select AN.AssetName,A.Id as AssetID,* from PrismJobAssignedAssets JA,PrismAssetName AN,Prism_Assets A where JA.AssetId=A.Id
                                     and A.AssetName=AN.Id and  JA.JobId=@Jobid">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="radcombo_job" Name="Jobid" DbType="Int32" />
                                            </SelectParameters>
                              </asp:SqlDataSource> --%>                
                
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
                                    <asp:Parameter Name="JobId" Type="Int32"></asp:Parameter>
                                    <asp:Parameter Name="AQntty" Type="Int32"></asp:Parameter>
                                    <asp:Parameter Name="AssetQntID" Type="String"></asp:Parameter>
                                </InsertParameters>
                                <UpdateParameters>                                    
                                    <asp:Parameter Name="AssetID" Type="Int32"></asp:Parameter>
                                    <asp:Parameter Name="JobId" Type="Int32"></asp:Parameter>
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
                                               <%-- <td >
                                                    Daily Charges($):<br />
                                                    <asp:TextBox ID="txt_dailycharges"  Width="240px" runat="server"></asp:TextBox>
                                                </td>--%>
                                                <td  runat="server" visible="false">
                                                   
                                                    <fieldset>
                                                        <legend> Upload Documents:</legend>
                                                        <table>

                                                            <tr>
                                                                <td>
                                                                    <telerik:RadAsyncUpload id="RadAsyncUpload1" runat="server" Width="600px" ></telerik:RadAsyncUpload>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                     <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" AllowSorting="True" CssClass="mdmGrid active"
                                                        CellSpacing="0"  GridLines="None" Width="600px" Skin="Forest">
                                                        
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
                 <telerik:RadWindowManager ID="RadWindowManager1" runat="server"   Modal="true" Animation="Resize"> 
                                     <Windows> 
                                     <telerik:RadWindow ID="window_eventlog" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit Event Log">
 
                                        <ContentTemplate>
 
                                           <iframe id="iframe1" runat="server" width="960px" height="600px" src="../../Modules/Configuration_Manager/ManageEventLog.aspx">
                                               
                                            </iframe>
                                            
                                         <%-- <uc1:ManageEventLog ID="ManageEventLog1" runat="server" />--%>
 
                                         </ContentTemplate>
 
                                     </telerik:RadWindow>
                                      <telerik:RadWindow ID="window_activity" runat="server"  Modal="true" Width="300px"  height="150px" Title="Create New Activity">
                                         <ContentTemplate>
                                             
                                        </ContentTemplate>
                                       </telerik:RadWindow>
                                          
                                     </Windows>
                        </telerik:RadWindowManager>
            </td>
        </tr>
                </table>
        </td>
    </tr>
</table>
</asp:Content>

