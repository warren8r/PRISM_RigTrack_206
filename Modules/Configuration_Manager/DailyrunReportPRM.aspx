<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="DailyrunReportPRM.aspx.cs" Inherits="Modules_Configuration_Manager_DailyrunReportPRM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    </style>
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
            <td align="center"  colspan="2">
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center"  colspan="2">
                <table>
                    <tr>
                        <td>
                            Select Job:<br />
                            <telerik:RadComboBox ID="radcombo_job" runat="server" AutoPostBack="false">
                                
                            </telerik:RadComboBox>
                        </td>
                         <td align="left">
                            <br />
                            <asp:Button ID="btn_view" runat="server"  Text="View" onclick="btn_view_Click" />
                        </td>
                    </tr>
                </table>                  
            </td>
        </tr>
        <tr><td style="line-height:20px"></td></tr>
        <tr>
            <td style="width:25%">
                    <table>
                        <tr>
                            <td>
                                <table>
                                     <tr>
                                         <td>
                                            <table runat="server" id="table_top1">
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
                        <td class="td">DATE:</td>
                        <td class="tdv"><telerik:RadDatePicker ID="date_run" runat="server"></telerik:RadDatePicker> </td>
                    </tr>
                     <tr>
                        <td class="td">RUN NUMBER:</td>
                        <td class="tdv"><telerik:RadComboBox ID="cmb_runno" runat="server"></telerik:RadComboBox>  </td>
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
                                   </tr>
                                     <tr>
                                        <td>
                                            <table runat="server"  id="table_top3">
                                                   <tr>
                                                       <td class="td">POPPET&#160;SIZE:</td>
                                                       <td>
                                                           <telerik:RadTextBox ID="txt_poppet" runat="server"></telerik:RadTextBox>
                                                       </td>
                                                      
                                                   </tr>
                                                   <tr>
                                                    <td class="td">ORIFICE&#160;SIZE:</td>
                                                    <td >
                                                        <telerik:RadTextBox ID="txt_orifice" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">PULSE&#160;WIDTH:</td>
                                                    <td >
                                                        <telerik:RadTextBox ID="txt_pulsewidth" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">PULSE&#160;AMPLITUDE:</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_pulseamplitude" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">TOTAL&#160;CONNECTED:</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_totalconnected" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="td">TOTAL CIRC:</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_totalcirc" runat="server"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr><td style="line-height:20px"></td></tr>
                                                     <tr>
                                                    <td class="td">DEPTH-START</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_depthstart" runat="server"></telerik:RadTextBox>
                                                    </td>                                                    
                                                </tr>
                                                 <tr>
                                                    <td class="td">DEPTH-END</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_depthend" runat="server"></telerik:RadTextBox>
                                                    </td>                                                    
                                                </tr>
                                                 <tr>
                                                    <td class="td">INC.</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_inc" runat="server"></telerik:RadTextBox>
                                                    </td>                                                    
                                                </tr>
                                                 <tr>
                                                    <td class="td">AZM.</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_azm" runat="server"></telerik:RadTextBox>
                                                    </td>                                                    
                                                </tr>
                                                 <tr>
                                                    <td class="td">MAGF.</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_magf" runat="server"></telerik:RadTextBox>
                                                    </td>                                                    
                                                </tr>
                                                 <tr>
                                                    <td class="td">GRAV.</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_grav" runat="server"></telerik:RadTextBox>
                                                    </td>                                                    
                                                </tr>
                                                 <tr>
                                                    <td class="td">DIP.</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_dip" runat="server"></telerik:RadTextBox>
                                                    </td>                                                    
                                                </tr>
                                                <tr><td style="line-height:20px"></td></tr>
                                                 <tr>
                                                     <td class="td">TEMPERATURE(C):</td>
                                                     <td>
                                                         <telerik:RadTextBox ID="txt_temp_c" runat="server"></telerik:RadTextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">TEMPERATURE(F):</td>
                                                     <td>
                                                         <telerik:RadTextBox ID="txt_temp_f" runat="server"></telerik:RadTextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">AVER. PUMP PRESSURE:</td>
                                                     <td>
                                                         <telerik:RadTextBox ID="txt_avr_pump_tressure" runat="server"></telerik:RadTextBox>(PSI)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">AVER. FLOW RATE:</td>
                                                     <td>
                                                         <telerik:RadTextBox ID="txt_avre_flow_rate" runat="server"></telerik:RadTextBox>(gpm)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">MUD WEIGHT:</td>
                                                     <td>
                                                         <telerik:RadTextBox ID="txt_mud_weight" runat="server"></telerik:RadTextBox>(ppg)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">SOLIDS:</td>
                                                     <td>
                                                         <telerik:RadTextBox ID="txt_solids" runat="server"></telerik:RadTextBox>(%)
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="td">SAND:</td>
                                                     <td>
                                                         <telerik:RadTextBox ID="txt_sand" runat="server"></telerik:RadTextBox>(%)<span style="color:gray">max 0.5</span>
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
            <td valign="top" align="left"  style="width:75%">
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
                               <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="SELECT [ActivityId], [RunID], [Time], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog]">
                                     </asp:SqlDataSource>--%>
                 <asp:SqlDataSource ID="SqlGet24Hours" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="SELECT [TimeId], [Time] FROM [Prism24Hours]">
                                     </asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlGet24HourActivity" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="SELECT [HourActivityId], [24HourActivity] FROM [Prism24HourActivity]">
                                     </asp:SqlDataSource>
                
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                DeleteCommand="DELETE FROM [PrismJobRun24HourActivityLog] WHERE [ActivityId] = @ActivityId" 
                                InsertCommand="INSERT INTO [PrismJobRun24HourActivityLog] ([RunID], [Time], [24HourActivity],[Comments]) VALUES
                                     (@RunID, @Time, @24HourActivity,@Comments)"
                                SelectCommand="SELECT [ActivityId], [RunID], [Time], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=1"
                                UpdateCommand="UPDATE [PrismJobRun24HourActivityLog] SET [Time] = @Time, [24HourActivity] = @24HourActivity, 
                                    [Comments] = @Comments WHERE [ActivityId] = @ActivityId">
                               <%-- <SelectParameters>
                                    <asp:ControlParameter ControlID="ddl_year" Name="Year" DbType="String" />
                                </SelectParameters>--%>
                                <DeleteParameters>
                                    <asp:Parameter Name="ActivityId" Type="Int32"></asp:Parameter>
                                </DeleteParameters>
                                <InsertParameters>
                                   <asp:Parameter Name="RunID" DefaultValue="1" Type="Int32"></asp:Parameter>
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
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

