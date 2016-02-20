<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageBHABitData.aspx.cs" Inherits="Modules_RigTrack_ManageBHABitData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            var intcount = parseInt(eventArgs.get_newValue());
            if (intcount > 10) {
                alert('Max number of jets are exceeded, Please enter less than 10');
                sender.set_value('');
                eventArgs.set_cancel(true);
            }
            else {
                $("#TextBoxesGroup").empty();
                for (i = 1; i < intcount + 1; i++) {
                    
                    var newTextBoxDiv = $(document.createElement('div')).attr("id", 'TextBoxDiv' + i);
                    
                    newTextBoxDiv.after().html('<label>Jet #' + i + ' : </label>' +
                          '<input type="text" style="border:solid 1px #768CA5;height:17px;width:200px" name="txtJet' + i +
                          '" id="txtJet' + i + '"><div style="height:2px"></div>');

                    newTextBoxDiv.appendTo("#TextBoxesGroup");
                }
            }
        }
        function getJutValues() {
            var jet1, jet2, jet3, jet4, jet5, jet6, jet7, jet8, jet9, jet10;
            if(document.getElementById('txtJet1')==null)
                jet1 = 0;
            else
                jet1 = document.getElementById('txtJet1').value;

            if (document.getElementById('txtJet2') == null)
                jet2 = 0;
            else
                jet2 = document.getElementById('txtJet2').value;

            if (document.getElementById('txtJet3') == null)
                jet3 = 0;
            else
                jet3 = document.getElementById('txtJet3').value;

            if (document.getElementById('txtJet4') == null)
                jet4 = 0;
            else
                jet4 = document.getElementById('txtJet4').value;

            if (document.getElementById('txtJet5') == null)
                jet5 = 0;
            else
                jet5 = document.getElementById('txtJet5').value;

            if (document.getElementById('txtJet6') == null)
                jet6 = 0;
            else
                jet6 = document.getElementById('txtJet6').value;

            if (document.getElementById('txtJet7') == null)
                jet7 = 0;
            else
                jet7 = document.getElementById('txtJet7').value;

            if (document.getElementById('txtJet8') == null)
                jet8 = 0;
            else
                jet8 = document.getElementById('txtJet8').value;

            if (document.getElementById('txtJet9') == null)
                jet9 = 0;
            else
                jet9 = document.getElementById('txtJet9').value;

            if (document.getElementById('txtJet10') == null)
                jet10 = 0;
            else
                jet10 = document.getElementById('txtJet10').value;



            
            document.getElementById("<%=hdnvalue.ClientID %>").value=jet1+","+jet2+","+jet3+","+jet4+","+jet5+","+jet6+","+jet7+","+jet8+","+jet9+","+jet10;
            
        }
    </script>
     <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Always">
        <ContentTemplate>
    <table width="100%">
        <tr>
            <td>
                <table>
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
                                            <td align="center" colspan="5"><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
                                        </tr>
                                       <tr>
                                           <td style="background-color:#5E7C95;color:white;height:18px" colspan="5">
                                               <table>
                                                   <tr><td align="left"><b>Bit Data</b></td></tr>
                                               </table>
                                           </td>
                                       </tr>
                                       <tr><td style="height:8px"></td></tr>
                                       <tr>
                                           <td>
                                               Bit Serial #:<br />
                                               <telerik:RadTextBox ID="txtBitSno" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit Description:<br />
                                               <telerik:RadTextBox ID="txtBitDesc" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               OD:<br />
                                               <telerik:RadTextBox ID="txtODFrac" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit Length:<br />
                                               <telerik:RadTextBox ID="txtBitLength" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Connection:<br />
                                               <telerik:RadTextBox ID="txtConnection" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                               
                                       </tr>
                                       <tr>
                                           <td>
                                                Bit Type:<br />
                                               <telerik:RadTextBox ID="txtBitType" runat="server" Width="220px"></telerik:RadTextBox>
                                                <%--<telerik:RadDropDownList runat="server" ID="RadDropDownList3" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                                    >
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>--%>
                                            </td>
                                           <td>
                                                Bearing Type:<br />
                                               <telerik:RadTextBox ID="txtBearingType" runat="server" Width="220px"></telerik:RadTextBox>
                                                <%--<telerik:RadDropDownList runat="server" ID="RadDropDownList4" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                                    >
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>--%>
                                            </td>
                                           <td>
                                                Bit MFG:<br />
                                               <telerik:RadTextBox ID="txtBitMfg" runat="server" Width="220px"></telerik:RadTextBox>
                                                <%--<telerik:RadDropDownList runat="server" ID="RadDropDownList5" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                                    >
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>--%>
                                            </td>
                                           <td>
                                               Bit Number:<br />
                                               <telerik:RadTextBox ID="txtBitNumber" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                                Reason Pulled:<br />
                                               <telerik:RadTextBox ID="txtReasonPulled" runat="server" Width="220px"></telerik:RadTextBox>
                                                <%--<telerik:RadDropDownList runat="server" ID="RadDropDownList8" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                                    >
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>--%>
                                            </td>
                                           
                                       </tr>
                                       <tr>
                                           <td>
                                               Inner Row:<br />
                                               <telerik:RadTextBox ID="txtInnerRow" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Outer Row:<br />
                                               <telerik:RadTextBox ID="txtOuterRow" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Dull Char:<br />
                                               <telerik:RadTextBox ID="txtDullChar" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                                Location:<br />
                                               <telerik:RadTextBox ID="txtLocation" runat="server" Width="220px"></telerik:RadTextBox>
                                                <%--<telerik:RadDropDownList runat="server" ID="RadDropDownList6" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                                    >
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>--%>
                                            </td>
                                           <td>
                                                Bearing Seals:<br />
                                               <telerik:RadTextBox ID="txtBearingSeals" runat="server" Width="220px"></telerik:RadTextBox>
                                                <%--<telerik:RadDropDownList runat="server" ID="RadDropDownList7" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                                    >
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>--%>
                                            </td>
                                           
                                       </tr>
                                       <tr>
                                           <td>
                                               Guage:<br />
                                               <telerik:RadTextBox ID="txtGuage" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Other Dull Char:<br />
                                               <telerik:RadTextBox ID="txtOtherDullChar" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Number of Jets:<br />
                                               <telerik:RadTextBox ID="txtNUMJETS" runat="server" MaxLength="10" Width="220px">
                                                   <ClientEvents OnValueChanged="PreventPostback" />
                                               </telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="5">
                                               <table>
                                                   <tr>
                                                       <td>
                                                           <div id='TextBoxesGroup'></div>
                                                       </td>
                                                   </tr>
                                               </table>
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
                                                   <tr><td align="left"><b>Sensor Distances</b></td></tr>
                                               </table>
                                           </td>
                                       </tr>
                                       <tr><td style="height:8px"></td></tr>
                                       <tr>
                                           <td>
                                               Bit to Sensor:<br />
                                               <telerik:RadTextBox ID="txtBittoSensor" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit to Gamma:<br />
                                               <telerik:RadTextBox ID="txtBittoGamma" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit to Resistivity:<br />
                                               <telerik:RadTextBox ID="txtBittoResistivity" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit to Porosity:<br />
                                               <telerik:RadTextBox ID="txtBittoPorosity" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           <td>
                                               Bit to DNSC:<br />
                                               <telerik:RadTextBox ID="txtBittoDNSC" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               Bit to Gyro:<br />
                                               <telerik:RadTextBox ID="txtBittoGyro" runat="server" Width="220px"></telerik:RadTextBox>
                                           </td>
                                           
                                       </tr>
                                       
                                    </table>
                               </td>
                           </tr>
                    <tr><td style="height:10px"></td></tr>
                </table>
            </td>
        </tr>
        <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                            <br />
                            <asp:Button ID="btnSaveBitData" runat="server"
                                Text="Create" ToolTip="Save changes" ValidationGroup="EditValidationGroup"
                                 
                                 OnClick="btnSaveBitData_Click" OnClientClick="return getJutValues();" />
                            <%--onclientclick="var validated = Page_ClientValidate(); if (!validated) return; if(!confirm('Click &quot;OK&quot; to save your changes')) return false;"--%>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btnCancel" runat="server"
                                Text="Clear" ToolTip="Cancel changes"
                                 CausesValidation="False" OnClick="btnCancel_Click"  />
                        </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
        <tr>
                        <td align="center" >
                            <telerik:RadGrid ID="RadGridBITInfo" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="10"
                      OnItemCommand="RadGridBITInfo_ItemCommand" DataSourceID="SqlServiceData">
                    <ClientSettings EnableRowHoverStyle="true" >
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <ExportSettings>
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView EditMode="InPlace" DataKeyNames="ID" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                                    Text="Edit" UniqueName="btnEdit">
                                </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn HeaderText="ID" ReadOnly="true" DataField="ID" UniqueName="ID" SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Number" ReadOnly="true" DataField="BHANumber" UniqueName="BHANumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Desc" DataField="BHADesc" UniqueName="BHADesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bit Sno" DataField="BitSno" UniqueName="BitSno"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bit Desc" DataField="BitDesc" UniqueName="BitDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ODFrac" DataField="ODFrac" UniqueName="ODFrac"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitLength" DataField="BitLength" UniqueName="BitLength"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Connection" DataField="Connection" UniqueName="Connection"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitType" DataField="BitType" UniqueName="BitType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BearingType" DataField="BearingType" UniqueName="BearingType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitMfg" DataField="BitMfg" UniqueName="BitMfg"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitNumber" DataField="BitNumber" UniqueName="BitNumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="No. JETS" DataField="NUMJETS" UniqueName="NUMJETS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="InnerRow" DataField="InnerRow" UniqueName="InnerRow"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="OuterRow" DataField="OuterRow" UniqueName="OuterRow"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="DullChar" DataField="DullChar" UniqueName="DullChar"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Location" DataField="Location" UniqueName="Location"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BearingSeals" DataField="BearingSeals" UniqueName="BearingSeals"></telerik:GridBoundColumn>
                            

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlServiceData" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                        SelectCommand="SELECT * FROM [RigTrack].[tblBHABitData] bit,[RigTrack].[tblBHADataInfo] bha where bha.ID=bit.BHAID">
                    </asp:SqlDataSource>
                        </td>
                    </tr>
    </table>
            </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

