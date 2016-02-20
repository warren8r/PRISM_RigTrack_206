<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageJobToolInventory.aspx.cs" Inherits="Modules_RigTrack_ManageJobToolInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <telerik:RadAjaxLoadingPanel ID="radalpLoadingPanel" runat="server" Transparency="50">
            <div class="loading">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading3.gif" AlternateText="loading">
                </asp:Image>
            </div>
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            
            <table width="100%">
                <tr>
                    <td style="padding-left:30px">
                        <h2>Manage Job Tool Inventory</h2>
                    </td>
                </tr>
               <tr>
                   <td align="center">
                       <table>
                            <tr>
                    <td>
                        Job/Curve Group ID:<br />
                        <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="250px" AppendDataBoundItems="true" DropDownHeight="200px"
                            >
                            <Items>
                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                            </Items>
                        </telerik:RadDropDownList>
                    </td>
                     <td >Tool Category<br />
                                <telerik:RadComboBox ID="ddl_assetcategory" runat="server" DataSourceID="SqlAssetCategory" DataTextField="clientAssetName" 
                                    DataValueField="clientAssetID" EmptyMessage="- Select -" OnSelectedIndexChanged="ddl_assetcategory_OnSelectedIndexChanged" AutoPostBack="true" Width="250px"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlAssetCategory" 
                                        ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                        SelectCommand="SELECT 0 AS [clientAssetID], 'Select Tool Group' AS [clientAssetName] UNION SELECT clientAssetID, clientAssetName FROM clientAssets where active='True'"></asp:SqlDataSource>
                            </td>
                             
                        <td> Tool Name:
                            &#160;&#160;&#160;<asp:LinkButton ID="LinkButton1" runat="server" Text="Create New" OnClientClick="openAsset();return false" /> <br />
                                
                                 <telerik:RadComboBox ID="ddl_assetname" runat="server" DataValueField="Id" DataTextField="AssetName"   SelectedText="- Select -" Width="250px"></telerik:RadComboBox>  
                                                         
                    
                            </td>
                    <td>
                        Serial Number:<br />
                        <telerik:RadTextBox ID="txtSno" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td>
                        Manufacturer:<br />
                        <telerik:RadDropDownList runat="server" ID="RadDropDownList3" Width="200px" AppendDataBoundItems="true" DropDownHeight="200px"
                            >
                            <Items>
                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                            </Items>
                        </telerik:RadDropDownList>
                    </td>
                    
                </tr>
                <tr><td style="height:15px"></td></tr>
                <tr>
                    <td>
                        Description:<br />
                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        OD Frac:<br />
                        <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td>
                        ID Frac:<br />
                        <telerik:RadTextBox ID="RadTextBox3" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td>
                        Length:<br />
                        <telerik:RadTextBox ID="RadTextBox4" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td>
                        Top Connection:<br />
                        <telerik:RadTextBox ID="RadTextBox5" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    
                   
                </tr>
                <tr><td style="height:15px"></td></tr>
                <tr>
                    <td>
                        Bottom Connection:<br />
                        <telerik:RadTextBox ID="RadTextBox6" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        Pin Top:<br />
                        <telerik:RadTextBox ID="RadTextBox7" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                     <td>
                        Pin Bottom:<br />
                        <telerik:RadTextBox ID="RadTextBox8" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                     <td>
                        Comments:<br />
                        <telerik:RadTextBox ID="RadTextBox9" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                     <td>
                        On Site:<br />
                        <asp:CheckBox ID="chkEditActive" runat="server" Width="20px" />
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
                                Text="Create" ToolTip="Save changes" ValidationGroup="EditValidationGroup"
                                 
                                 />
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
         AllowFilteringByColumn="True">
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
                
                <telerik:GridBoundColumn DataField="warehouse" AllowFiltering="false" FilterControlAltText="Filter meterIRN column"
                    HeaderText="Job ID" SortExpression="warehouse" UniqueName="warehouse">
                    <HeaderStyle Width="50px" />
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
                
                 <telerik:GridBoundColumn DataField="DailyCharge" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Length" SortExpression="DailyCharge" UniqueName="DailyCharge">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Description" AllowFiltering="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lbl_totalrunhrs" runat="server"></asp:Label>
                    <asp:Label ID="lbl_RunHrsToMaintenance" runat="server" Text='<%# Eval("RunHrsToMaintenance") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lbl_maintenancepercentage" runat="server" Text='<%# Eval("maintenancepercentage") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="OD Frac" AllowFiltering="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lbl_totalCumulativerunhrs" runat="server"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RunHrsToMaintenance" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="ID Frac" SortExpression="RunHrsToMaintenance" UniqueName="RunHrsToMaintenance">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="maintenancepercentage" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Top Connection" SortExpression="maintenancepercentage" UniqueName="maintenancepercentage">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Bottom Connection" AllowFiltering="false">
                <HeaderStyle Width="130px" />
                    <ItemTemplate>
                                                
                        <%--<telerik:RadToolTip ID="rttChooseFrom" runat="server" Animation="None" ContentScrolling="Auto" 
                            Height="300px" HideEvent="ManualClose" Modal="true" RelativeTo="Element" 
                            ShowEvent="OnClick" TargetControlID="lnk_assetnames" Title="" 
                            Width="400px">  
                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">  
                                <asp:Label ID="lbl_assetnamepopup" runat="server"></asp:Label>
                                <telerik:RadGrid ID="radgridcomponents" runat="server" ShowHeader="true" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Name"
                                                HeaderText="Component Name" SortExpression="Name" UniqueName="Name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Serialno"
                                                HeaderText="Serial Number" SortExpression="Serialno" UniqueName="Serialno">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                                        
                            </telerik:RadAjaxPanel> 
                        </telerik:RadToolTip>--%>
                                                
                        <asp:Label ID="lbl_comp" runat="server"></asp:Label>                   
                        <%--<asp:LinkButton ID="lnk_assetnames" runat="server" OnClientClick="return false;" Text="View Components"></asp:LinkButton>--%>
                                                
                        <%--<telerik:RadToolTip ID="RadToolTip1" TargetControlID="radgridkitassets" Text="View Asset Names" Title="Title" runat="server"> 
                        </telerik:RadToolTip>--%>
                        
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="On Site" AllowFiltering="false">
                    <ItemTemplate >                              
                    <asp:CheckBox ID="lbl_chkonsite" runat="server" Checked="true"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Pin Top" AllowFiltering="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lblPinTop" runat="server"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Pin Bottom" AllowFiltering="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lblPinTop1" runat="server"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Comments" AllowFiltering="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lblPinTop2" runat="server"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
              <%--  <telerik:GridBoundColumn DataField="installDate" DataType="System.DateTime" FilterControlAltText="Filter installDate column"
                    HeaderText="Installed" SortExpression="installDate" DataFormatString="{0:d}"
                    UniqueName="installDate">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="removalDate" DataType="System.DateTime" FilterControlAltText="Filter removalDate column"
                    HeaderText="Un-Installed" SortExpression="removalDate" DataFormatString="{0:d}"
                    UniqueName="removalDate">
                </telerik:GridBoundColumn>--%>
               <%-- <telerik:GridHyperLinkColumn AllowSorting="False" DataNavigateUrlFields="primaryLatLong"
                    DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                    Target="_new" Text="View Map" UniqueName="gisLink">
                    <ItemStyle ForeColor="Blue" />
                </telerik:GridHyperLinkColumn>--%>
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
                                                      <telerik:RadWindow ID="window_manufracturer" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit Manufracturer">
 
                                                        <ContentTemplate>
                                                           <iframe id="iframe2" runat="server" width="960px" height="600px" src="../../Modules/Configuration_Manager/ManageManufracturer.aspx">
                                               
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
    </telerik:RadAjaxPanel>
</asp:Content>

