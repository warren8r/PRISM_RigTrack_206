<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageToolInventory.aspx.cs" Inherits="Modules_RigTrack_ManageToolInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function openAsset() {

            var comboBox1 = $find('<%=ddl_assetcategory.ClientID %>');
            var catvalue = comboBox1.get_value();
            
            if (catvalue == "") {
                alert("Please select Tool Category to create Tool");
                return false;
            }
            else {
                
             

             var catname = document.getElementById('<%=ddl_assetcategory.ClientID %>').value;
             var url = "../../Modules/RigTrack/ManageAssetName.aspx?catname=" + catname + "&catval=" + catvalue + "";
             document.getElementById('<%=iframe3.ClientID %>').src = url;
             window.radopen(null, "window_Asset");
             return false;
         }

        }
        function openManufacturer() {

            
               
                var url = "../../Modules/RigTrack/CreateToolManufacturer.aspx";
                document.getElementById('<%=iframe2.ClientID %>').src = url;
            window.radopen(null, "window_manufracturer");
             return false;
         
     }
    </script>
    <script type="text/javascript">
        function ClientClose() {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
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
                    <td style="padding-left:30px">
                        <h2>Manage  Tool Inventory</h2>
                    </td>
                </tr>
                    <tr>
                        <td align="right">
                            (<span style="color: Red; font-weight:bold;">*</span>) indicates mandatory fields
                        </td>
                    </tr>
                    <tr><td align="center"><asp:Label ID="lblMode" runat="server" Visible="false"></asp:Label></td></tr>
                    <tr><td><asp:Label ID="lblMessage" runat="server" CssClass="star">
                                            </asp:Label>
                        <asp:HiddenField ID="hdnSelectedMeterID" runat="server" />
                        </td></tr>
               <tr>
                   <td align="center">
                       <table>
                           <tr>
                               <td colspan="5" align="center">
                                   <asp:ValidationSummary ID="EditValidationSummary" runat="server" CssClass="star" 
                                ValidationGroup="TextBoxValidationGroup" ShowMessageBox="true" ShowSummary="false"  ForeColor="Red" />
                               </td>
                           </tr>
                            <tr>
                    <td>
                        Warehouse:<span style="color: Red; font-weight:bold;">*</span><br />
                        <telerik:RadComboBox ID="ddl_warehouse" runat="server" DataSourceID="SqlGetWarehouse" DataTextField="name" 
                                                                                             DataValueField="ID" Width="250px"></telerik:RadComboBox>
                                                                                               <asp:SqlDataSource ID="SqlGetWarehouse" 
                                                   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                   SelectCommand="SELECT 0 AS [ID], 'Select Warehouse' AS [Name] UNION select [ID],[Name] from PrsimWarehouses  where bitActive='True' "></asp:SqlDataSource>
                        <br />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddl_warehouse"
        CssClass="customValidatorClass" Display="None" ErrorMessage="Select Warehouse" InitialValue="Select Warehouse" ForeColor="Red"
        ValidationGroup="TextBoxValidationGroup" />
                        <telerik:RadMaskedTextBox runat="server" ID="txtAssetNumber" Visible="false"
                                                      LabelWidth="64px" 
                                                      Width="100px" EmptyMessage="0000000000" Mask="##########"/>
                    </td>
                     <td >Tool Type<span style="color: Red; font-weight:bold;">*</span><br />
                                <telerik:RadComboBox ID="ddl_assetcategory" runat="server" DataSourceID="SqlAssetCategory" DataTextField="clientAssetName" 
                                    DataValueField="clientAssetID" OnSelectedIndexChanged="ddl_assetcategory_OnSelectedIndexChanged" AutoPostBack="true" Width="200px"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlAssetCategory" 
                                        ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                        SelectCommand="SELECT 0 AS [clientAssetID], 'Select Tool Type' AS [clientAssetName] UNION SELECT clientAssetID, clientAssetName FROM clientAssets where active='True'"></asp:SqlDataSource>
                         <br />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddl_assetcategory"
        CssClass="customValidatorClass" Display="None" ErrorMessage="Select Tool Category" InitialValue="Select Tool Category" ForeColor="Red"
        ValidationGroup="TextBoxValidationGroup" />
                            </td>
                             
                        <td> Tool Name:<span style="color: Red; font-weight:bold;">*</span>
                            &#160;&#160;&#160;<asp:LinkButton ID="LinkButton1" runat="server" Text="Create New" OnClientClick="openAsset();return false" /> <br />
                                
                                 <telerik:RadComboBox ID="ddl_assetname" runat="server" DataValueField="Id" DataTextField="AssetName"   SelectedText="- Select -" Width="200px"></telerik:RadComboBox>  
                                   <br />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddl_assetname"
        CssClass="customValidatorClass" Display="None" ErrorMessage="Select Tool" InitialValue="" ForeColor="Red"
        ValidationGroup="TextBoxValidationGroup" />                      
                    
                            </td>
                    <td>
                        Serial Number:<span style="color: Red; font-weight:bold;">*</span><br />
                        <telerik:RadTextBox ID="radtxtSerialNumber" runat="server" Width="200px"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="radtxtSerialNumber"
        CssClass="customValidatorClass" Display="None" ErrorMessage="Enter Serial Number"  ForeColor="Red"
        ValidationGroup="TextBoxValidationGroup" />
                    </td>
                    <td>
                        Manufacturer:&#160;&#160;&#160;<asp:LinkButton ID="lnkManufacturer" runat="server" Text="Create New" OnClientClick="openManufacturer();return false" /> <br />
                        <telerik:RadComboBox runat="server" ID="ddl_manufacturer" Width="200px" AppendDataBoundItems="true" DropDownHeight="200px"
                            >
                            <Items>
                                
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" onajaxrequest="RadAjaxManager1_AjaxRequest"> 
                                      <AjaxSettings> 
                                            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1"> 
                                            <UpdatedControls> 
                                                   <telerik:AjaxUpdatedControl ControlID="ddl_manufacturer" /> 
                                            </UpdatedControls> 
                                            </telerik:AjaxSetting> 
                                      </AjaxSettings> 
                                </telerik:RadAjaxManager>
                    </td>
                    
                </tr>
                
                <tr>
                    <td>
                        Description:<br />
                        <telerik:RadTextBox ID="txt_description" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        OD Frac:<span style="color: Red; font-weight:bold;">*</span><br />
                        <telerik:RadTextBox ID="txtODFrac" runat="server" Width="200px"></telerik:RadTextBox><br />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtODFrac"
        CssClass="customValidatorClass" Display="None" ErrorMessage="Enter OD Frac"  ForeColor="Red"
        ValidationGroup="TextBoxValidationGroup" />
                    </td>
                    <td>
                        ID Frac:<span style="color: Red; font-weight:bold;">*</span><br />
                        <telerik:RadTextBox ID="txtIDFrac" runat="server" Width="200px"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtIDFrac"
        CssClass="customValidatorClass" Display="None" ErrorMessage="Enter ID Frac"  ForeColor="Red"
        ValidationGroup="TextBoxValidationGroup" />
                    </td>
                    <td>
                        Length:<span style="color: Red; font-weight:bold;">*</span><br />
                        <telerik:RadTextBox ID="txtLength" runat="server" Width="200px"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtLength"
        CssClass="customValidatorClass" Display="None" ErrorMessage="Enter Length"  ForeColor="Red"
        ValidationGroup="TextBoxValidationGroup" />
                    </td>
                    <td>
                        Top Connection:<br />
                        <telerik:RadTextBox ID="txtTopConnection" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    
                   
                </tr>
                
                <tr>
                    <td>
                        Bottom Connection:<br />
                        <telerik:RadTextBox ID="txtBottomConnection" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        Fishing Neck:<br />
                        <telerik:RadTextBox ID="txtFishingNeck" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                     <td>
                        Stab Center Point:<br />
                        <telerik:RadTextBox ID="txtStabCenterPoint" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                     <td>
                        Stab Blade OD:<br />
                        <telerik:RadTextBox ID="txtStabBladeOD" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td>
                        Weight:<br />
                        <telerik:RadTextBox ID="txtWeight" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    
                     <%--<td>
                        On Site:<br />
                        <asp:CheckBox ID="chkEditActive" runat="server" Width="20px" />
                    </td>--%>
                </tr>
                           <tr>
                               <td>
                                EI:<br />
                                <telerik:RadTextBox ID="txtEI" runat="server" Width="250px"></telerik:RadTextBox>
                            </td>
                               <td>
                                    Size Category:<br />
                                    <telerik:RadTextBox ID="txtSizeCategory" runat="server" Width="200px"></telerik:RadTextBox>
                                </td>
                               <td >Daily Charge($)<br />
                                <telerik:RadTextBox ID="txt_cost" runat="server" Width="200px"></telerik:RadTextBox>
                            </td> 
                               <td >Cumulative Hours<br />
                                <telerik:RadTextBox ID="txtCumulativeHrs" runat="server" Width="200px"></telerik:RadTextBox>
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
                                Text="Create" ToolTip="Save changes"  ValidationGroup="TextBoxValidationGroup"
                                 
                                 OnClick="btnSaveAssetName_Click" />
                            <%--onclientclick="var validated = Page_ClientValidate(); if (!validated) return; if(!confirm('Click &quot;OK&quot; to save your changes')) return false;"--%>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btnCancelAssetName" runat="server"
                                Text="Clear" ToolTip="Cancel changes"
                                 CausesValidation="False" />
                        </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadGrid ID="radgrdMeterList" runat="server" CellSpacing="0" GridLines="None"
         AllowFilteringByColumn="True" OnItemCommand="radgrdMeterList_ItemCommand"
        OnNeedDataSource="radgrdMeterList_NeedDataSource" OnPageIndexChanged="radgrdMeterList_PageIndexChanged">
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
        <MasterTableView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="ID" AllowFilteringByColumn="True">
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
                    HeaderText="Status" SortExpression="Status" AllowFiltering="false" UniqueName="Status" Display="false">
                    <HeaderStyle Width="80px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Status") %>' Enabled="false" />
                        <asp:Label ID="lblActive" runat="server" Text='<%# Convert.ToBoolean(Eval("Status")) == true ? "Active" : "In-Active" %>'></asp:Label>
                        <asp:Label ID="lbl_assetid" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lbl_assetname" runat="server" Text='<%# Eval("AssetName") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                    Text="Edit" UniqueName="btnEdit" HeaderText="Edit">
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                    HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID" Display="false">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="warehouse" AllowFiltering="true" FilterControlWidth="100px" FilterControlAltText="Filter warehouse column"
                    HeaderText="Warehouse" SortExpression="warehouse" UniqueName="warehouse">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="assetcategory" FilterControlAltText="Filter meterType column"
                    HeaderText="Tool Type" SortExpression="assetcategory" AllowFiltering="false" UniqueName="assetcategory">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AssetName" AllowFiltering="false" FilterControlAltText="Filter meterName column"
                    HeaderText="Tool Name" SortExpression="AssetName" UniqueName="AssetName">
                    <HeaderStyle Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="serialNumber"  AllowFiltering="false" FilterControlAltText="Filter serialNumber column"
                    HeaderText="Serial #" SortExpression="serialNumber" UniqueName="serialNumber">
                    <HeaderStyle Width="80px" />
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Manufacturer"  AllowFiltering="false" FilterControlAltText="Filter Manufacturer column"
                    HeaderText="Manufacturer" SortExpression="Manufacturer" UniqueName="Manufacturer">
                    <HeaderStyle Width="80px" />
                    
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
                <telerik:GridBoundColumn DataField="FishingNeck" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Fishing Neck" SortExpression="FishingNeck" UniqueName="FishingNeck">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StabCenterPoint" AllowFiltering="false" FilterControlAltText="Filter StabCenterPoint column"
                    HeaderText="Stab Center Point" SortExpression="StabCenterPoint" UniqueName="StabCenterPoint">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StabBladeOD" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Stab Blade OD" SortExpression="StabBladeOD" UniqueName="StabBladeOD">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Weight" AllowFiltering="false" FilterControlAltText="Filter Weight column"
                    HeaderText="Weight" SortExpression="Weight" UniqueName="Weight">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EI" AllowFiltering="false" FilterControlAltText="Filter EI column"
                    HeaderText="EI" SortExpression="EI" UniqueName="EI">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SizeCategory" AllowFiltering="false" FilterControlAltText="Filter SizeCategory column"
                    HeaderText="SizeCategory" SortExpression="SizeCategory" UniqueName="SizeCategory">
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
            <PagerStyle PageSizeControlType="RadComboBox" Position="TopAndBottom" AlwaysVisible="True" />
        </MasterTableView>
        <PagerStyle PageSizeControlType="RadComboBox" />
        <FilterMenu EnableImageSprites="False">
        </FilterMenu>
    </telerik:RadGrid>
    <%--<artem:GoogleMap ID="GoogleMap1" runat="server" Visible="false" Style="display: none;"
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
    </script>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"  Modal="true" Animation="Resize"> 
                                                     <Windows> 
                                                     <telerik:RadWindow ID="window_department" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit Department">
 
                                                        <ContentTemplate>
 
                                                           <iframe id="iframe1" runat="server" width="960px" height="600px" src="../../Modules/Configuration_Manager/ManageDepartments.aspx">
                                               
                                                            </iframe>
 
                                                         </ContentTemplate>
 
                                                     </telerik:RadWindow>
                                                      <telerik:RadWindow ID="window_manufracturer" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit Manufracturer" OnClientClose="ClientClose">
 
                                                        <ContentTemplate>
                                                           <iframe id="iframe2" runat="server" width="960px" height="600px">
                                               
                                                            </iframe>
 
                                                         </ContentTemplate>
 
                                                     </telerik:RadWindow>
                                                      <telerik:RadWindow ID="window_Asset" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit Tool">
 
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

