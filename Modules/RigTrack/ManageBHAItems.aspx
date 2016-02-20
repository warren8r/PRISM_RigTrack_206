<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageBHAItems.aspx.cs" Inherits="Modules_RigTrack_ManageBHAItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            
            function validateView() {
                
                var comboBox1 = $find('<%=ddlCompany.ClientID %>');
                var catvalue = comboBox1.get_selectedItem().get_value();
               
                if (catvalue == "0") {
                    alert('Please select company');
                    
                    return false;
                }

                var comboJob = $find('<%=combo_job.ClientID %>');
                var jobval = comboJob.get_value();
                if (jobval == "0") {
                    alert('Please select job');
                    
                    return false;
                }

                var comboBHA = $find('<%=radBHANumber.ClientID %>');
                var BHAval = comboBHA.get_value();
                if (BHAval == "0") {
                    alert('Please select BHA#');
                    
                    return false;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <asp:UpdatePanel runat="server" ID="updPnl1"  UpdateMode="Always">
        
            <ContentTemplate>  
           <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
        <table width="100%">
                <tr>
                    <td style="padding-left:30px">
                        <h2>Add Items to BHA</h2>
                    </td>
                </tr>
               <tr>
                   <td align="center">
                       <table>
                           <tr>
                               <td>
                                   <asp:Label ID="lbl_message" runat="server"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                Select Company<br />
                                <telerik:RadDropDownList ID="ddlCompany" runat="server" Width="220px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                             <td align="left">Select Job<br />

                                   <%--<telerik:RadComboBox runat="server" ID="combo_job" width="220px" DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid" ></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                     SelectCommand="select 0 as [jid],'Select JobName' as Jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where jid not in (select jobid from PrismJobAssignedAssets) and jid not in (select jobid from PrismJobAssignedPersonals) and jobordercreatedid<>'' and status<>'Closed'"></asp:SqlDataSource>--%>
                                 <telerik:RadComboBox runat="server" ID="combo_job" Width="220px" OnSelectedIndexChanged="combo_job_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" DropDownHeight="220px"
                                                        >
                                                        <Items>
                                                            <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                </td>
                               <td>
                                    BHA #:<br />
                                    <telerik:RadComboBox runat="server" ID="radBHANumber" Width="220px"  
                                        DataTextField="BHANumber" DataValueField="ID" AppendDataBoundItems="true" DropDownHeight="220px">
                                        <Items>
                                                            <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                                        </Items>
                                    </telerik:RadComboBox>

                                   <%--DataSourceID="SqlGetAssetcategory"--%>
                                   <%--<asp:SqlDataSource ID="SqlGetAssetcategory" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                            SelectCommand="select 0 as [ID],'Select BHA' as BHANumber union select ID, (BHANumber+'-'+BHADesc) as BHANumber from [RigTrack].[tblBHADataInfo]"></asp:SqlDataSource>--%>
                                </td>
                                            <td>
                                                Warehouse:<br />
                        <telerik:RadComboBox ID="ddl_warehouse" runat="server" DataSourceID="SqlGetWarehouse" DataTextField="name" 
                                                                                             DataValueField="ID" Width="250px"></telerik:RadComboBox>
                                                                                               <asp:SqlDataSource ID="SqlGetWarehouse" 
                                                   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                   SelectCommand="SELECT 0 AS [ID], 'Select Warehouse' AS [Name] UNION select [ID],[Name] from PrsimWarehouses  where bitActive='True' "></asp:SqlDataSource>
                                            </td>
                                            <td>
                                                <br />
                                                <asp:Button ID="btnView" runat="server" Text="View" OnClientClick="return validateView();" OnClick="btnView_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                               
                               
                           </tr>
                           <tr><td style="height:15px"></td></tr>
                            <tr>
                    <td align="center">
                        <table>
                            <tr><td colspan="2"><b>Tools List</b></td></tr>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadGrid ID="radgrdMeterList" runat="server" CellSpacing="0" GridLines="None"
         AllowFilteringByColumn="True" 
        OnNeedDataSource="radgrdMeterList_NeedDataSource" OnPageIndexChanged="radgrdMeterList_PageIndexChanged"
            OnPreRender="radgrdMeterList_PreRender">
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
            
        </ClientSettings>
        <MasterTableView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="ID" AllowFilteringByColumn="True" PageSize="10">
            <PagerStyle Position="TopAndBottom" PageSizeControlType="RadComboBox" AlwaysVisible="true"></PagerStyle>
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
                    HeaderText="Tool Group Name" SortExpression="assetcategory" AllowFiltering="false" UniqueName="assetcategory">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AssetName" AllowFiltering="false" FilterControlAltText="Filter meterName column"
                    HeaderText="Tool Type" SortExpression="AssetName" UniqueName="AssetName">
                    <HeaderStyle Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="serialNumber"  AllowFiltering="false" FilterControlAltText="Filter serialNumber column"
                    HeaderText="Serial #" SortExpression="serialNumber" UniqueName="serialNumber">
                    <HeaderStyle Width="80px" />
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="warehouse" AllowFiltering="false" FilterControlAltText="Filter meterIRN column"
                    HeaderText="Warehouse" SortExpression="warehouse" UniqueName="warehouse">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="Length" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Length" SortExpression="Length" UniqueName="Length">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" AllowFiltering="false" FilterControlAltText="Filter Description column"
                    HeaderText="Description" SortExpression="Description" UniqueName="Description">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="ODFrac" AllowFiltering="false" FilterControlAltText="Filter ODFrac column"
                    HeaderText="OD Frac" SortExpression="ODFrac" UniqueName="ODFrac">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IDFrac" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="ID Frac" SortExpression="IDFrac" UniqueName="IDFrac">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TopConnection" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Top Connection" SortExpression="TopConnection" UniqueName="TopConnection">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BottomConnection" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Bottom Connection" SortExpression="BottomConnection" UniqueName="BottomConnection">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PinTop" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Pin Top" SortExpression="PinTop" UniqueName="PinTop" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PinBottom" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Pin Bottom" SortExpression="PinBottom" UniqueName="PinBottom" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Comments" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Comments" SortExpression="Comments" UniqueName="Comments" Visible="false">
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
    <artem:GoogleMap ID="GoogleMap1" runat="server" Visible="false" Style="display: none;"
        EnableOverviewMapControl="false" EnableMapTypeControl="false" EnableZoomControl="false"
        EnableStreetViewControl="false" ShowTraffic="false" ApiVersion="3" EnableScrollWheelZoom="false"
        IsSensor="false" DefaultAddress="430 W Warner Rd. , Tempe AZ" DisableDoubleClickZoom="False"
        DisableKeyboardShortcuts="True" EnableReverseGeocoding="True" Height="650" Zoom="13"
        IsStatic="true" Key="AIzaSyC2lU7S-IMlNUTu8nHAL97_rL06vKmzfhc" MapType="Roadmap"
        StaticFormat="Gif" Tilt="45" Width="550" StaticScale="2">
    </artem:GoogleMap>
    <asp:HiddenField ID="hdnSelectedMeterID" runat="server" />
    <asp:SqlDataSource ID="SqlUserData" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
        SelectCommand="SELECT [userID] AS [userID], [firstName] + ' ' + [lastName] AS [name] FROM Users">
    </asp:SqlDataSource>
    <script type="text/javascript">
        $(document).ready(function () {
            var maxHeight = -1;

            $('.gis-form').each(function () {
                maxHeight = maxHeight > $(this).height() ? maxHeight : $(this).height();
            });

            $('.gis-form').each(function () {
                $(this).height(maxHeight);
            });
        });
    </script>
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
                            <br />
                            <asp:Button ID="btnSaveAssetName" runat="server"
                                Text="Assign/Re-Assign" ToolTip="Save changes" ValidationGroup="EditValidationGroup"
                                 
                                 OnClick="btnAssign_Click"/>
                            <%--onclientclick="var validated = Page_ClientValidate(); if (!validated) return; if(!confirm('Click &quot;OK&quot; to save your changes')) return false;"--%>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btnCancelAssetName" runat="server"
                                Text="Clear" ToolTip="Cancel changes"
                                 CausesValidation="False" OnClick="btnCancel_Click" />
                        </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
        </table>
    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

