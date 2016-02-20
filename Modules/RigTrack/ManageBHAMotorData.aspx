<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageBHAMotorData.aspx.cs" Inherits="Modules_RigTrack_ManageBHAMotorData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        
        function addMoreInfo() {
            
            var btntext=document.getElementById("<%=btnAddMore.ClientID %>").value;
            if (btntext == "Hide More Information") {
                document.getElementById("<%=btnAddMore.ClientID %>").value='Add More Information';
                document.getElementById('divAddMoreInfo').style.display = "none";
                //alert("aaaa");
            }
            else if (btntext == "Add More Information") {
                document.getElementById("<%=btnAddMore.ClientID %>").value='Hide More Information';
                document.getElementById('divAddMoreInfo').style.display = "block";

            }
            
            return false;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="updPnl1">
        <ContentTemplate>
        <table width="100%">
                <tr>
                    <td style="padding-left:30px">
                        <h2>Manage BHA Motor Data</h2>
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
                                                   <tr><td align="left"><b>Motor Data</b></td></tr>
                                               </table>
                                           </td>
                                       </tr>
                                       <tr><td style="height:8px"></td></tr>
                                       <tr>
                                           <td>
                                               Description:<br />
                                               <telerik:RadTextBox ID="txtMotorDesc" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Motor MFG:<br />
                                               <telerik:RadTextBox ID="txtMotorMFG" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               NB Stabilizer:<br />
                                               <telerik:RadTextBox ID="txtNBStabilizer" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Model:<br />
                                               <telerik:RadTextBox ID="txtModel" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Revolutions/Galon:<br />
                                               <telerik:RadTextBox ID="txtRevolutions" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Bend:<br />
                                               <telerik:RadTextBox ID="txtBend" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Rotor Jet:<br />
                                               <telerik:RadTextBox ID="txtRotorJet" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit to Bend:<br />
                                               <telerik:RadTextBox ID="txtBittoBend" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Prop BUR:<br />
                                               <telerik:RadTextBox ID="txtPropBUR" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Real BUR:<br />
                                               <telerik:RadTextBox ID="txtRealBUR" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Pad O.D:<br />
                                               <telerik:RadTextBox ID="txtPadOD" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Average Differential:<br />
                                               <telerik:RadTextBox ID="txtAverageDifferential" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Lobes:<br />
                                               <telerik:RadTextBox ID="txtLobes" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Off Bottom Difference:<br />
                                               <telerik:RadTextBox ID="txtOffBottomDifference" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Stages:<br />
                                               <telerik:RadTextBox ID="txtStages" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Stall Pressure:<br />
                                               <telerik:RadTextBox ID="txtStallPressure" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                    </table>
                               </td>
                           </tr>
                           <tr>
                               <td colspan="5" align="right">
                                   <asp:Button ID="btnAddMore" runat="server" Text="Add More Information" OnClientClick="return addMoreInfo();" />
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   <div style="display:none" id="divAddMoreInfo">
                                   <table width="100%">
                                       <tr>
                                           <td>
                                               <table width="100%" style="border: solid 1px #000000">
                                       <tr>
                                           <td style="background-color:#5E7C95;color:white;height:18px" colspan="5">
                                               <table>
                                                   <tr><td align="left"><b>Additional Motor Information</b></td></tr>
                                               </table>
                                           </td>
                                       </tr>
                                       <tr><td style="height:8px"></td></tr>
                                       <tr>
                                           <td>
                                               Clearence:<br />
                                               <telerik:RadTextBox ID="txtClearence" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Avg On Bottom SPP:<br />
                                               <telerik:RadTextBox ID="txtAvgOnBottomSPP" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Avg Off Bottom SPP:<br />
                                               <telerik:RadTextBox ID="txtAvgOffBottomSPP" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               No. Of Stalls:<br />
                                               <telerik:RadTextBox ID="txtNoOfStalls" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Stall Time:<br />
                                               <telerik:RadTextBox ID="txtStallTime" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Formation:<br />
                                               <telerik:RadTextBox ID="txtFormation" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                              Bent Sub Deg:<br />
                                               <telerik:RadTextBox ID="txtBentSubDeg" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Elastomer:<br />
                                               <telerik:RadTextBox ID="txtElastomer" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bend Type:<br />
                                               <telerik:RadTextBox ID="txtBendType" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Clearence Rng:<br />
                                               <telerik:RadTextBox ID="txtClearenceRng" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               MWD Company:<br />
                                               <telerik:RadTextBox ID="txtMEDCompany" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Number Of MWD Runs:<br />
                                               <telerik:RadTextBox ID="txtNoOfMWDRuns" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Inspection Company:<br />
                                               <telerik:RadTextBox ID="txtInspectionCmpny" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               
                                           </td>
                                           <td>
                                               
                                           </td>
                                       </tr>
                                                   <tr>
                                                       <td>
                                                           <asp:CheckBox ID="chkMotorFailure" runat="server" TextAlign="Right" Text="Motor Failure" />
                                                       </td>
                                                       <td>
                                                           <asp:CheckBox ID="chkExtendedPowerSection" runat="server" TextAlign="Right" Text="Extended Power Section" />
                                                       </td>
                                                       <td>
                                                           <asp:CheckBox ID="chkInspected" runat="server" TextAlign="Right" Text="Inspected?" />
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
                                               BHA Objectives:<br />
                                               <telerik:RadTextBox ID="txtBHAObjectives" runat="server" TextMode="MultiLine" Height="50px" Width="1150px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="5">
                                               BHA Performance:<br />
                                               <telerik:RadTextBox ID="txtBHAPerformance" runat="server" TextMode="MultiLine" Height="50px" Width="1150px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="5">
                                               Additional Comments:<br />
                                               <telerik:RadTextBox ID="txtAdditionalComments" runat="server" TextMode="MultiLine" Height="50px" Width="1150px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Bot Stabilizer Type:<br />
                                               <telerik:RadTextBox ID="txtBotStabilizerType" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                              Bot Stab Blade Type:<br />
                                               <telerik:RadTextBox ID="txtBotStabBladeType" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bot Stab Length:<br />
                                               <telerik:RadTextBox ID="txtBotStabLength" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Lower Stab O.D:<br />
                                               <telerik:RadTextBox ID="txtLowerStabOD" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Even wall:<br />
                                               <telerik:RadTextBox ID="txtEvenWall" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Top Stabilizer Type:<br />
                                               <telerik:RadTextBox ID="txtTopStabilizerType" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                              Top Stab Blade Type:<br />
                                               <telerik:RadTextBox ID="txtTopStabBladeType" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Top Stab Length:<br />
                                               <telerik:RadTextBox ID="txtTopStabLength" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Upper Stab O.D:<br />
                                               <telerik:RadTextBox ID="txtUpperStabOD" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Interference Fit:<br />
                                               <telerik:RadTextBox ID="txtInterferenceFit" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           
                                           
                                           <td>
                                               Interference Tol:<br />
                                               <telerik:RadTextBox ID="txtInterferenceTol" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       
                                    </table>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               <table width="100%" style="border: solid 1px #000000">
                                                   <tr>
                                                       <td style="background-color:#5E7C95;color:white;height:18px" colspan="5">
                                                           <table>
                                                               <tr><td align="left"><b>Wear Pad Information</b></td></tr>
                                                           </table>
                                                       </td>
                                                   </tr>
                                                   <tr><td style="height:8px"></td></tr>
                                                   <tr>
                                                       <td>
                                                           Wearpad Y/N:<br />
                                                           <telerik:RadComboBox ID="comboWearPad" runat="server" Width="220px">
                                                               <Items>
                                                                   <telerik:RadComboBoxItem Text="Select" Value="0" />
                                                                   <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                   <telerik:RadComboBoxItem Text="No" Value="No" />
                                                               </Items>
                                                           </telerik:RadComboBox>
                                                       </td>
                                                       <td>
                                                           Wearpad Type:<br />
                                                           <telerik:RadTextBox ID="txtWearPadType" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                           Wearpad Length:<br />
                                                           <telerik:RadTextBox ID="txtWearpadlength" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                          Wearpad Height:<br />
                                                           <telerik:RadTextBox ID="txtWearpadHeight" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                           Wearpad Width:<br />
                                                           <telerik:RadTextBox ID="txtWearpadWidth" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                           Wearpad Guage:<br />
                                                           <telerik:RadTextBox ID="txtWearpadGuage" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                          Bit to Wearpad:<br />
                                                           <telerik:RadTextBox ID="txtBitToWearpad" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                           
                                                   </tr>
                                       
                                       
                                                </table>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               <table width="100%" style="border: solid 1px #000000">
                                                   <tr>
                                                       <td style="background-color:#5E7C95;color:white;height:18px" colspan="5">
                                                           <table>
                                                               <tr><td align="left"><b>Motor Limits</b></td></tr>
                                                           </table>
                                                       </td>
                                                   </tr>
                                                   <tr><td style="height:8px"></td></tr>
                                                   <tr>
                                                       
                                                       <td>
                                                           Max Surf RPM:<br />
                                                           <telerik:RadTextBox ID="txtMaxSurfRPM" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                           Max DL Rotating:<br />
                                                           <telerik:RadTextBox ID="txtMaxDLRotating" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                          Max DL Sliding:<br />
                                                           <telerik:RadTextBox ID="txtMaxDLSliding" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                           Max Diff. Press:<br />
                                                           <telerik:RadTextBox ID="txtMaxDiffPress" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                       <td>
                                                           Max Flow Rate:<br />
                                                           <telerik:RadTextBox ID="txtMaxFlowRate" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       
                                                       <td>
                                                          Max Torque:<br />
                                                           <telerik:RadTextBox ID="txtMaxTorque" runat="server" Width="220px"></telerik:RadTextBox>
                                                       </td>
                                           
                                                   </tr>
                                       
                                       
                                                </table>
                                           </td>
                                       </tr>
                                   </table>
                                       </div>
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
                            
                            <asp:Button ID="btnSaveMotorData" runat="server"
                                Text="Create" ToolTip="Save changes" ValidationGroup="EditValidationGroup" 
                                 
                                 OnClick="btnSaveMotorData_Click"/>
                            
                        </td>
                        <td>
                            
                            <asp:Button ID="btnCancelAssetName" runat="server"
                                Text="Clear" ToolTip="Cancel changes"
                                 CausesValidation="False" OnClick="btnClear_Click" />
                        </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="RadGridBHAInfo" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30"
                     Width="1250px"   OnItemCommand="RadGridBHAInfo_ItemCommand">
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true" Scrolling-AllowScroll="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <ExportSettings>
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView  DataKeyNames="MotorID" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                                    Text="Edit" UniqueName="btnEdit">
                                </telerik:GridButtonColumn>
                            
                            <telerik:GridBoundColumn HeaderText="ID" ReadOnly="true" DataField="MotorID" UniqueName="MotorID" SortExpression="MotorID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Job/Curve Group" HeaderStyle-Width="200px" ItemStyle-Width="200px" ReadOnly="true" DataField="JobName" UniqueName="JobName" ></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Number" ReadOnly="true" DataField="BHANumber" UniqueName="BHANumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Desc" DataField="BHADesc" UniqueName="BHADesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="MotorDesc" DataField="MotorDesc" UniqueName="MotorDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="MotorMFG" DataField="MotorMFG" UniqueName="MotorMFG"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="NBStabilizer" DataField="NBStabilizer" UniqueName="NBStabilizer"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Model" DataField="Model" UniqueName="Model"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Revolutions" DataField="Revolutions" UniqueName="Revolutions"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn HeaderText="BHA Type" DataField="BHAType" UniqueName="BHAType">
                                
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridTemplateColumn HeaderText="BHA Type" DataField="BHAType" UniqueName="BHAType">
                                <ItemTemplate>
                                    <asp:Label ID="lblBHAType" runat="server" Text='<%# Eval("BHATypeName") %>'></asp:Label>
                                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblBHATypeEdit" runat="server" Text='<%# Eval("BHATypeID") %>' Visible="false"></asp:Label>
                                     <telerik:RadComboBox runat="server" ID="comboBHAType" DataSourceID="SqlGetWarehouse123"  DataTextField="BHAType" DataValueField="ID" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                        >
                                        <Items>
                                                            <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                                        </Items>
                                    </telerik:RadComboBox>
                                  
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>

                </td>
            </tr>
            <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"  Modal="true" Animation="Resize"> 
                                                     <Windows> 
                                                     
                                                      <telerik:RadWindow ID="window_Asset" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit BHA Type" >
 
                                                        <ContentTemplate>
                                                           <iframe id="iframe3" runat="server" width="960px" height="600px" >
                                               
                                                            </iframe>
 
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
    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

