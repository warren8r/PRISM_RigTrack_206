<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageRotatorySteerableData.aspx.cs" Inherits="Modules_RigTrack_ManageRotatorySteerableData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdateProgress ID="loader1" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="updPnl">
        <ContentTemplate>
        <table width="100%">
                <tr>
                    <td style="padding-left:30px">
                        <h2>Manage Rotatory Steerable Data</h2>
                    </td>
                </tr>
               <tr>
                   <td align="center">
                       <table>
                           <tr>
                               <td colspan="6">
                                   <asp:Label ID="lbl_message" runat="server"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                             <td>
                                 <table>
                                     <tr>
                                         <td>
                                Select Company<br />
                                <telerik:RadDropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                               <td>
                                    Job/Curve Group ID:<br />
                                    <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="220px" OnSelectedIndexChanged="ddlCurveGroup_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" DropDownHeight="220px"
                                        >
                                        <Items>
                                            <telerik:DropDownListItem Value="0" Text="-Select-" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>  
                         <td>
                            BHA #:<br />
                            <telerik:RadComboBox runat="server" ID="radBHANumber" Width="220px"  
                                DataTextField="BHANumber" DataValueField="ID" AppendDataBoundItems="true" DropDownHeight="220px">
                                <Items>
                                                    <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                                </Items>
                            </telerik:RadComboBox>
                             <asp:HiddenField ID="hdnvalue" runat="server" />
                             <asp:HiddenField ID="hidden_serviceid" runat="server" />
                            <%--DataSourceID="SqlGetAssetcategory"--%>
                            <%--<asp:SqlDataSource ID="SqlGetAssetcategory" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                    SelectCommand="select 0 as [ID],'Select BHA' as BHANumber union select ID, (BHANumber+'-'+BHADesc) as BHANumber from [RigTrack].[tblBHADataInfo]"></asp:SqlDataSource>--%>
                        </td>
                                     </tr>
                                 </table>
                             </td>
                    
                               
                               
                               
                           </tr>
                           
                           <tr><td style="height:15px"></td></tr>
                            <tr>
                               <td colspan="5" >
                                   <table width="100%" style="border: solid 1px #000000">
                                       <tr>
                                           <td style="background-color:#5E7C95;color:white;height:18px" colspan="5">
                                               <table>
                                                   <tr><td align="left"><b>Rotatory Steerable Data</b></td></tr>
                                               </table>
                                           </td>
                                       </tr>
                                       <tr><td style="height:8px"></td></tr>
                                       <tr>
                                           <td>
                                               RS Description:<br />
                                               <telerik:RadTextBox ID="txtRSDesc" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               RS MFG:<br />
                                               <telerik:RadTextBox ID="txtRSMfg" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Push/Point Type:<br />
                                               <telerik:RadTextBox ID="txtPushPointType" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               RS FirmWarever:<br />
                                               <telerik:RadTextBox ID="txtFirmWarever" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Max DLS/100:<br />
                                               <telerik:RadTextBox ID="txtMaxDLS" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               RS Lower Stab OD:<br />
                                               <telerik:RadTextBox ID="txtRSLowerStab" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit to Ctr NB:<br />
                                               <telerik:RadTextBox ID="txtBitNB" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Ctr NB to 3D Blades:<br />
                                               <telerik:RadTextBox ID="txtCtrBlades" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               RS Stab to Top Stab:<br />
                                               <telerik:RadTextBox ID="txtRSStabToTopStab" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Battery ah in/ah out:<br />
                                               <telerik:RadTextBox ID="txtBatteryInAhOut" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               # Blades:<br />
                                               <telerik:RadTextBox ID="txtNumberOfBlades" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Blade Type:<br />
                                               <telerik:RadTextBox ID="txtBladeTypes" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Blade Profile:<br />
                                               <telerik:RadTextBox ID="txtBladeProfile" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               RS Serial Number:<br />
                                               <telerik:RadTextBox ID="txtRSSno" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Wake-up RPM/Drill:<br />
                                               <telerik:RadTextBox ID="txtWakeupRPMDrill" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Blade Collapse Time:<br />
                                               <telerik:RadTextBox ID="txtBladeCollapseTime" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Hardfacing:<br />
                                               <telerik:RadTextBox ID="txtHardfacing" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               RS Guage Length:<br />
                                               <telerik:RadTextBox ID="txtRSGuageLength" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               RS Pad OD:<br />
                                               <telerik:RadTextBox ID="txtRSPadOD" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           
                                       </tr>
                                       <tr>
                                            <td>
                                                <asp:CheckBox ID="chkRSFailure" runat="server" TextAlign="Right" Text="RS Failure" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkRunWithMotor" runat="server" TextAlign="Right" Text="Run With Motor" />
                                            </td>
                                            
                                        </tr>
                                       <tr>
                                           <td colspan="5">
                                               Reason Pulled:<br />
                                               <telerik:RadTextBox ID="txtReasonPulled" runat="server" TextMode="MultiLine" Height="50px" Width="1150px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                      
                                       <tr>
                                           <td colspan="5">
                                               RS Performance:<br />
                                               <telerik:RadTextBox ID="txtRSPerformance" runat="server" TextMode="MultiLine" Height="50px" Width="1150px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="5">
                                               Additional Comments:<br />
                                               <telerik:RadTextBox ID="txtAdditionalComments" runat="server" TextMode="MultiLine" Height="50px" Width="1150px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                    </table>
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
                            
                            <asp:Button ID="btnSaveRotatoryData" runat="server"
                                Text="Create" ToolTip="Save changes" ValidationGroup="EditValidationGroup" 
                                 
                                OnClick="btnSaveRotatoryData_Click"/>
                            
                        </td>
                        <td>
                            
                            <asp:Button ID="btnCancelAssetName" runat="server"
                                Text="Clear" ToolTip="Cancel changes"
                                 CausesValidation="False" OnClick="btnClear_Click"  />
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
                                <telerik:RadGrid ID="RadGridBHARotatory" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30"
                     Width="1250px" OnItemCommand="RadGridBHARotatory_ItemCommand">
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true" Scrolling-AllowScroll="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <ExportSettings>
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView  DataKeyNames="RotatoryID" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                                    Text="Edit" UniqueName="btnEdit">
                                </telerik:GridButtonColumn>
                            
                            <telerik:GridBoundColumn HeaderText="ID" ReadOnly="true" DataField="RotatoryID" UniqueName="RotatoryID" SortExpression="RotatoryID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Job/Curve Group" HeaderStyle-Width="200px" ItemStyle-Width="200px" ReadOnly="true" DataField="JobName" UniqueName="JobName" ></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Number" ReadOnly="true" DataField="BHANumber" UniqueName="BHANumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Desc" DataField="BHADesc" UniqueName="BHADesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RSSno" DataField="RSSno" UniqueName="RSSno"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RSDesc" DataField="RSDesc" UniqueName="RSDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RSMfg" DataField="RSMfg" UniqueName="RSMfg"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="PushPointType" DataField="PushPointType" UniqueName="PushPointType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="FirmWarever" DataField="FirmWarever" UniqueName="FirmWarever"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="MaxDLS" DataField="MaxDLS" UniqueName="MaxDLS"></telerik:GridBoundColumn>

                           
                            <telerik:GridBoundColumn HeaderText="RSLowerStab" DataField="RSLowerStab" UniqueName="RSLowerStab"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitNB" DataField="BitNB" UniqueName="BitNB"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="CtrBlades" DataField="CtrBlades" UniqueName="CtrBlades"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RSStabToTopStab" DataField="RSStabToTopStab" UniqueName="RSStabToTopStab"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BatteryInAhOut" DataField="BatteryInAhOut" UniqueName="BatteryInAhOut"></telerik:GridBoundColumn>

                            <%--<telerik:GridBoundColumn HeaderText="NumberOfBlades" DataField="NumberOfBlades" UniqueName="NumberOfBlades"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BladeTypes" DataField="BladeTypes" UniqueName="BladeTypes"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BladeProfile" DataField="BladeProfile" UniqueName="BladeProfile"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RSSno" DataField="RSSno" UniqueName="RSSno"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="WakeupRPMDrill" DataField="WakeupRPMDrill" UniqueName="WakeupRPMDrill"></telerik:GridBoundColumn>--%>
                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </ContentTemplate>
        <Triggers>

        </Triggers>
        </asp:UpdatePanel>
</asp:Content>

