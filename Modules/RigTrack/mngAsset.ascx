<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mngAsset.ascx.cs" Inherits="controls_confgMangrMeter_mngMeter" %>
 <link href="../../css/main.css" type="text/css" rel="Stylesheet" />
 <script type="text/javascript">

     function openwin() {


         window.radopen(null, "window_department");

     }
     function openwinmanufracturer() {

         window.radopen(null, "window_manufracturer");

     }
     function openAsset() {


         if (document.getElementById('<%=ddl_assetcategory.ClientID %>').value == "Select Asset Category") {
             alert("Please select Asset Category to create Assets");
             return false;
         }
         else {
             var comboBox1 = $find('<%=ddl_assetcategory.ClientID %>');
             var catvalue = comboBox1.get_value();

             var catname = document.getElementById('<%=ddl_assetcategory.ClientID %>').value;
             var url = "../../Modules/Configuration_Manager/ManageAssetName.aspx?catname=" + catname + "&catval=" + catvalue + "";
             document.getElementById('<%=iframe3.ClientID %>').src = url;
             window.radopen(null, "window_Asset");
             return false;
         }

     }
     function downloadFile(fileName, downloadLink1) {

         var downloadLink = $get('downloadFileLink');
         var filePath = "../../Documents/" + fileName;
         downloadLink.href = "DownloadHandler.ashx?fileName=" + fileName + "&filePath=" + filePath;
         downloadLink.style.display = 'block';
         downloadLink.style.display.visibility = 'hidden';
         downloadLink.click();

         return false;

     }

     function yesrd() {
         document.getElementById('<%=compcategory.ClientID %>').style.display = 'block';
         return false;
     }
     function nord() {
         document.getElementById('<%=compcategory.ClientID %>').style.display = 'none';
         return false;
     }
     
</script>
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
          <style type="text/css">
               div.infoBox
               {
                    width: 400px;
                    margin-right: 15px;
                    vertical-align: top;
               }
               #ContentTemplateZone, #NavigateUrlZone
               {
                    height: 400px;
               }
               .contText, .contButton
               {
                    margin: 0;
                    padding: 0 0 10px 5px;
                    font-size: 12px;
               }
               .RadWindow_Black .contText
               {
                    color: #fff;
               }
          </style>
           <style type="text/css">
    .RadUpload .ruCheck 
        { 
            display:none; 
        }
        .RadUpload .ruAdd
        {
            width:200px;
        }
</style>
     </telerik:RadCodeBlock>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div align="center" class="contactmain">
                <img src="../../loading1.gif" alt="Loading" /><br />
                <span style="color:Red">Loading Please Wait....</span>
             </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <telerik:RadNotification ID="radnotMessage" runat="server" Text="Initial text" Position="BottomRight"
        AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
        EnableRoundedCorners="true">
    </telerik:RadNotification>
    <asp:ValidationSummary ID="valgValidationSummary" runat="server" DisplayMode="List" Font-Size="Small" ForeColor="Red" />
   
    <asp:Panel ID="pnlTop" runat="server" >
        <h3 style="margin: 3px;">
            <asp:Label ID="lblMode" runat="server"></asp:Label>
            Tools
        </h3>
        <table style="margin: 3px 0px 3px 0px;" width="100%">
        <tr>
            <td align="center" colspan="8">
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </td>
            
        </tr>
            <tr>
                <td colspan="8">
                <asp:Label ID="lblMessage" runat="server" CssClass="star">
                </asp:Label>
            </td>
            </tr>
            <tr>
            <td align="center">
                <table style="text-align:left">

                    <tr>
                        <td>
                            <%--Select Job<br />--%>
                            <telerik:RadDropDownList ID="ddl_warehouse" runat="server" DataSourceID="SqlGetWarehouse" DataTextField="name" 
                                                                                             DataValueField="ID" SelectedText="- Select -" Visible="false"></telerik:RadDropDownList>
                                                                                               <asp:SqlDataSource ID="SqlGetWarehouse" 
                                                   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                   SelectCommand="SELECT 0 AS [ID], 'Select Job' AS [Name] UNION select [ID],[Name] from PrsimWarehouses  where bitActive='True' "></asp:SqlDataSource>
                        </td>
                         <td >Tool Group<br />
                                <telerik:RadComboBox ID="ddl_assetcategory" runat="server" DataSourceID="SqlAssetCategory" DataTextField="clientAssetName" 
                                    DataValueField="clientAssetID" EmptyMessage="- Select -" OnSelectedIndexChanged="ddl_assetcategory_OnSelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlAssetCategory" 
                                        ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                        SelectCommand="SELECT 0 AS [clientAssetID], 'Select Tool Group' AS [clientAssetName] UNION SELECT clientAssetID, clientAssetName FROM clientAssets where active='True'"></asp:SqlDataSource>
                            </td>
                             <td style="display:none">
                            <telerik:RadMaskedTextBox runat="server" ID="txtAssetNumber"
                                                      LabelWidth="64px" 
                                                      Width="100px" EmptyMessage="0000000000" Mask="##########"/>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ErrorMessage="" runat="server"  SetFocusOnError="true" ControlToValidate="txtAssetNumber" />
                            &nbsp;&nbsp;--%>

                            <!-- less than a billion -->
                        </td>
                            <td style="display:none">
                            <asp:LinkButton ID="lnl_generateassetid" CausesValidation="false" ForeColor="Blue" runat="server" onclick="lnl_generateassetid_Click" 
                                            >Generate ID #</asp:LinkButton>
                        </td>
                        <td > Tool Type:<br />
                            <%--&#160;&#160;&#160;<asp:LinkButton ID="LinkButton1" runat="server" Text="Create New" OnClientClick="openAsset();return false" /> <br />--%>
                                <telerik:RadTextBox ID="radtxtAssetName" runat="server">
                                </telerik:RadTextBox>
                                 <telerik:RadComboBox ID="ddl_assetname" runat="server" DataValueField="Id" DataTextField="AssetName" Visible="false"  SelectedText="- Select -"></telerik:RadComboBox>  
                                                         
                    <asp:SqlDataSource ID="SqlGetAssetname"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                       SelectCommand="SELECT 0 AS Id, 'Select AssetName' AS AssetName UNION select Id,AssetName from PrismAssetName   "></asp:SqlDataSource>
                            </td> 
                              <td > Serial Number: <span class="star">*</span><br />
                                <telerik:RadTextBox ID="radtxtSerialNumber" runat="server">
                                </telerik:RadTextBox>
                                <%--<asp:RequiredFieldValidator ID="SerialNumberRequiredFieldValidator" runat="server"
                                    ErrorMessage="Serial Number must not be blank" ControlToValidate="radtxtSerialNumber"
                                    SetFocusOnError="true" Display="None" />--%>
                            </td>
                        <td >Description<br />
                                <telerik:RadTextBox runat="server" ID="txt_description" TextMode="MultiLine"  Width="300px"></telerik:RadTextBox>
                            </td>
                        <td >Daily Charge($)<br />
                                <telerik:RadTextBox ID="txt_cost" runat="server" ></telerik:RadTextBox>
                            </td> 
                        <td >
                                            <telerik:RadButton ID="radbtnSaveMeter" runat="server" Text="Save" ToolTip="Save meter"
                                                OnClientClicked="ConfirmSave" OnClick="radbtnSaveMeter_Click">
                                            </telerik:RadButton>
                                        </td>
                                         <td >
                                            <telerik:RadButton ID="radbtnCancelMeter" runat="server" Text="Reset" CausesValidation="False"
                                                OnClick="radbtnCancelMeter_Click">
                                            </telerik:RadButton>
                                        </td>
                                         
                    </tr>
                    <tr><td style="height:15px"></td></tr>
                    <tr>
                        <td colspan="9">
                             <telerik:RadGrid ID="radgrdMeterList" runat="server" CellSpacing="0" GridLines="None" Width="800px"
         AllowFilteringByColumn="True" OnItemDataBound="radgrdMeterList_ItemDataBound" OnItemCommand="radgrdMeterList_ItemCommand"
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
                <telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                    Text="Edit" UniqueName="btnEdit">
                    <HeaderStyle Width="50px" />
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="warehouse" AllowFiltering="false" FilterControlAltText="Filter meterIRN column"
                    HeaderText="Job ID" SortExpression="warehouse" UniqueName="warehouse" Display="false">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
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
                
                <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                    HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID" Display="false">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="assetcategory" FilterControlAltText="Filter meterType column"
                    HeaderText="Tool Group ID" SortExpression="assetcategory" UniqueName="assetcategory" AllowFiltering="false">
                    <HeaderStyle Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AssetName" AllowFiltering="false" FilterControlAltText="Filter meterName column"
                    HeaderText="Tool Type" SortExpression="AssetName" UniqueName="AssetName">
                    <HeaderStyle Width="80px" />
                </telerik:GridBoundColumn>
               
                <telerik:GridBoundColumn DataField="serialNumber"  AllowFiltering="false" FilterControlAltText="Filter serialNumber column"
                    HeaderText="Serial #" SortExpression="serialNumber" UniqueName="serialNumber">
                    <HeaderStyle Width="80px" />
                    
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Description" AllowFiltering="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lbl_totalCumulativerunhrs123" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                </telerik:GridTemplateColumn>
                 <telerik:GridBoundColumn DataField="DailyCharge" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Daily Charge($)" SortExpression="DailyCharge" UniqueName="DailyCharge" Display="false">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Run hrs since last maintenance" AllowFiltering="false" Display="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lbl_totalrunhrs" runat="server"></asp:Label>
                    <asp:Label ID="lbl_RunHrsToMaintenance" runat="server" Text='<%# Eval("RunHrsToMaintenance") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lbl_maintenancepercentage" runat="server" Text='<%# Eval("maintenancepercentage") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Total Cumulative Run Hrs" AllowFiltering="false" Display="false">
                    <ItemTemplate >                              
                    <asp:Label ID="lbl_totalCumulativerunhrs" runat="server"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RunHrsToMaintenance" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="RunHrsTo Maintenance" SortExpression="RunHrsToMaintenance" UniqueName="RunHrsToMaintenance" Display="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="maintenancepercentage" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                    HeaderText="Maintenance(% of total run hrs)" SortExpression="maintenancepercentage" UniqueName="maintenancepercentage" Display="false">
                </telerik:GridBoundColumn>
                
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
            <PagerStyle PageSizeControlType="RadComboBox" Position="Bottom" AlwaysVisible="True" />
        </MasterTableView>
        <PagerStyle PageSizeControlType="RadComboBox" />
        <FilterMenu EnableImageSprites="False">
        </FilterMenu>
    </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
            <tr>
               <td align="center" style="display:none">
                    <table  style="text-align:left">
                        <tr>                            
                                                  
                             
                              <td >Type:<br />
                                <telerik:RadTextBox ID="radtxtType" runat="server">
                                </telerik:RadTextBox>
                            </td>
                             <td >Make:<br />
                                <telerik:RadTextBox ID="radtxtMeterMake" runat="server">
                                </telerik:RadTextBox>
                            </td>
                            <%--<td >Daily Charge($)<br />
                                <telerik:RadTextBox ID="txt_cost" runat="server" ></telerik:RadTextBox>
                            </td>--%> 
                            <td >Total Run Hours<br />
                                <telerik:RadTextBox ID="txt_totrunhrs" runat="server" ></telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
              </td>
                
            </tr> 
            <tr>
                <td align="center"  style="display:none">
                    <table  style="text-align:left">
                        <tr>
                            <td>
                                Run hours to Maintenance<br />
                                <asp:TextBox ID="txtrunmaintenance" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Maintenance(% of total run hrs)<br />
                                <asp:TextBox ID="txt_maintenance" runat="server"></asp:TextBox>
                            </td>
                            
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td align="center"  style="display:none">
                    <table  style="text-align:left">
                        <tr>
                            <td>
                                Components Required:
                            </td>
                            <td>
                                <asp:RadioButton ID="rd_yes" onclick="yesrd();" runat="server" GroupName="compreq" Text="Yes" TextAlign="Right" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rd_no" runat="server" onclick="nord();" Checked="true" GroupName="compreq" Text="No" TextAlign="Right" />
                            </td>
                            <td style="width:40px">
                            </td>
                            <td > Status:
                                <asp:CheckBox ID="chkActive" runat="server" Width="20px" Checked="true" CausesValidation="false"
                                    AutoPostBack="true" OnCheckedChanged="chkActive_CheckedChanged" ToolTip="Active/In-Active" />
                                <asp:Label ID="lblActive" runat="server" Text="Active"></asp:Label>
                    
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
              <tr>
                  <td id="compcategory" runat="server" align="center"  style="display:none">
                      <table  style="text-align:left">
                          <tr>
                              <td>
                                  Component Category:<br />
                                  <telerik:RadListBox ID="RadListBox1"  runat="server" Width="220px" Height="120" 

                                    AutoPostBack="true" DataSourceID="DataSourceComponentCategory" DataKeyField="ID" DataValueField="ID"

                                    DataTextField="Name">

                                   

                                </telerik:RadListBox>
                                  <asp:SqlDataSource runat="server" ID="DataSourceComponentCategory" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="select comp_categoryid as ID,comp_categoryname as NAME from Prism_ComponentCategory where status='True'">
                                    
                                </asp:SqlDataSource>
                              </td>
                              <td>
                                  Component Name:<br />
                                  <telerik:RadListBox ID="RadListBox2"  runat="server" Width="220px" Height="120" 

                                    AutoPostBack="true" DataSourceID="DataSourceComponentNames" DataKeyField="ID" DataValueField="ID"

                                    DataTextField="Name">

                                   

                                </telerik:RadListBox>
                                  <asp:SqlDataSource runat="server" ID="DataSourceComponentNames" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="select componet_id as ID, ComponentName as NAME from dbo.Prism_ComponentNames where comp_categoryid=@comp_categoryid">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="RadListBox1" Name="comp_categoryid" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                              </td>
                              <td>
                                  Component Serial Number:<br />
                                  <telerik:RadListBox ID="RadListBox3" EnableDragAndDrop="true" runat="server" Width="220px" Height="120" 

                                    AutoPostBack="true" DataSourceID="DataSourceComponentSerialNumber" DataKeyField="ID" DataValueField="ID"

                                    DataTextField="Name" AllowTransfer="true" TransferToID="RadListBox4" AutoPostBackOnTransfer="false">

                                   

                                </telerik:RadListBox>
                                  <asp:SqlDataSource runat="server" ID="DataSourceComponentSerialNumber" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="select CompID as ID, (ComponentName+'('+Serialno+')') as NAME from Prism_Components p,Prism_ComponentNames pc where pc.componet_id=p.Componentid and compid not in (select compid from PrismAssetComponents where AssignmentStatus='Active') and Componentid=@Componentid">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="RadListBox2" Name="Componentid" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                              </td>
                              <td>
                                  Selected Items:
                                  <br />
                                  <telerik:RadListBox ID="RadListBox4" DataTextField="Name" DataValueField="ID" EnableDragAndDrop="true" runat="server" Width="220px" Height="120">

                                   

                                </telerik:RadListBox>
                              </td>
                          </tr>
                      </table>
                  </td>
              </tr>        
            
            <tr>
                 <td  style="display:none" align="center">
                    <table  style="text-align:left">
                        <tr>
                             <td colspan="3">Part Number<br />
                                <telerik:RadDropDownList runat="server" ID="ddl_partnumber" Width="320px"></telerik:RadDropDownList>
                            </td>
                             
                        </tr>
                        <tr>
                            
                             <td >Plant<br />
                                <telerik:RadDropDownList ID="ddl_plant" runat="server"></telerik:RadDropDownList>
                            </td>
                             <td >Responsible Party<br />
                                <telerik:RadDropDownList ID="ddl_responsibleparty" runat="server"></telerik:RadDropDownList>
                            </td>
                            <td >Depreciation Type?<br />
                                <telerik:RadDropDownList ID="ddl_depreciationtype" runat="server"></telerik:RadDropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td >Size<br />
                                <telerik:RadDropDownList ID="ddl_size" runat="server"></telerik:RadDropDownList>
                            </td>
                             <td >Status<br />
                                <telerik:RadDropDownList ID="ddl_status" runat="server"></telerik:RadDropDownList>
                            </td>
                             <td >Owner<br />
                                <telerik:RadDropDownList ID="ddl_owner" runat="server"></telerik:RadDropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                             <td >
                                <asp:CheckBox id="chk_purchasenew" runat="server" Text="Purchased New?" TextAlign="Right" />
                            </td>
                             <td >
                                <asp:CheckBox id="chk_costadjust" runat="server" Text="Cost Adjusted?" TextAlign="Right" />
                            </td>
                             <td >
                                <asp:CheckBox id="chk_depreciate" runat="server" Text="Depreciate?" TextAlign="Right" />
                            </td>
                        </tr>
                        <tr>
                            <td  colspan="3">
                            <table>
                                <tr>
                                        <td >Manufacturer&#160;&#160;&#160;<asp:LinkButton ID="lnk_manufracturer" runat="server" Text="Create New" OnClientClick="openwinmanufracturer();return false" /> <br />
                                        <telerik:RadDropDownList ID="ddl_manufacturer" runat="server" DataSourceID="SqlGetManufracturer" DataTextField="name" 
                                                                                                        DataValueField="Number" SelectedText="- Select -"></telerik:RadDropDownList>                                
                            <asp:SqlDataSource ID="SqlGetManufracturer"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                SelectCommand="SELECT 0 AS [Number], 'Select Manufracturer' AS [Name] UNION select [Number],[Name] from PrsimManufracturer  where bitActive='True' "></asp:SqlDataSource>
                                 
                                    </td>
                                        <td >Manufacture Country<br />
                                        <telerik:RadDropDownList ID="ddl_manufacturecountry" runat="server"></telerik:RadDropDownList>
                                    </td>
                                        <td >Manufacture Date<br />
                                        <telerik:RadDatePicker ID="date_manufracture" runat="server" ></telerik:RadDatePicker>
                                    </td>
                                        <td >AFE<br />
                                        <telerik:RadTextBox ID="txt_AFE" runat="server"></telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                            <td colspan="3">
                            <table>
                                <tr>
                                        <td >Alt Part Number<br />
                                        <telerik:RadTextBox ID="txt_altpartnumber" runat="server"></telerik:RadTextBox>
                                        </td>
                                        <td >In service Date<br />
                                        <telerik:RadDatePicker ID="date_inservice" runat="server"></telerik:RadDatePicker>
                                    </td>
                                        <td >Cost adjusted Date<br />
                                        <telerik:RadDatePicker ID="date_costadjusted" runat="server"></telerik:RadDatePicker>
                                    </td>
                                        <td >Retire Date<br />
                                        <telerik:RadDatePicker ID="date_retire" runat="server"></telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                        <tr>
                                 <td colspan="3">
                                    <table>
                                        <tr>
                                              <td >Department&#160;&#160;&#160;<asp:LinkButton ID="lnk_department" runat="server" Text="Create New" OnClientClick="openwin();return false" /> <br />                                
                                                <telerik:RadDropDownList ID="ddl_department" runat="server" DataSourceID="SqlGetDepartment" DataTextField="name" 
                                                                                                             DataValueField="Number" SelectedText="- Select -"></telerik:RadDropDownList>                                
                                    <asp:SqlDataSource ID="SqlGetDepartment"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                       SelectCommand="SELECT 0 AS [Number], 'Select Department' AS [Name] UNION select [Number],[Name] from PrsimDepartments  where bitActive='True' "></asp:SqlDataSource>
                                 
                                            </td>
                                             <td>Verified Location<br />
                                                <telerik:RadDropDownList ID="ddl_verfiedlocation" runat="server"></telerik:RadDropDownList>
                                            </td>
                                              <td >Verified Date<br />
                                                <telerik:RadDatePicker ID="date_verified" runat="server"></telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
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
                            <tr>
                                 <td colspan="3">
                                    <table>
                                        <tr>
                                             <td >Months to Depreciate<br />
                                                <telerik:RadTextBox ID="txt_months_depreciate" runat="server"></telerik:RadTextBox>
                                            </td>
                                             <td >Hours Since Last Service<br />
                                                <telerik:RadTextBox ID="txt_hourselastservice" runat="server"></telerik:RadTextBox>
                                            </td>
                                             <td >Net Value<br />
                                                <telerik:RadTextBox ID="txt_netvalue" runat="server"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                 <td colspan="3">
                                    <table>
                                        <tr>
                                             <td >Weight (Lbs)<br />
                                                <telerik:RadTextBox ID="txt_weightlbs" runat="server"></telerik:RadTextBox>
                                            </td>
                                             <td >Weight (kgs)<br />
                                                <telerik:RadTextBox ID="txt_weightkgs" runat="server"></telerik:RadTextBox>
                                            </td>
                                             <td >Schedule B<br />
                                                <telerik:RadDropDownList ID="ddl_scheduleb" runat="server"></telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                 <td colspan="3">
                                    <table>
                                        <tr>
                                             <td >ABC Code<br />
                                                <telerik:RadTextBox ID="txt_abc" is="txt_abccode" runat="server"></telerik:RadTextBox>
                                            </td>
                                                
                                            <td style="width:20px"></td>
                                            <td style="border:1px solid red">
                                                <table>
                                                    <tr>
                                                         <td style="display:none">Repair / Maintenance</td>
                                            <td style="display:none">
                                                <asp:RadioButtonList ID="rb_rpr_man" runat="server" ForeColor="Red" TextAlign="Right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y"><span style="color:Red"> Yes</span></asp:ListItem>
                                                    <asp:ListItem Value="N"><span style="color:Red">No</span></asp:ListItem>
                                                </asp:RadioButtonList>
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
            </tr>
                      
            
            
           <%-- <tr>
              <td >
                <table>
                    <tr>
                         <td ><asp:LinkButton ID="lnk_compliancecodes" runat="server" Text="Compliance Codes"></asp:LinkButton></td>
                         <td  style="width:5px"></td>
                         <td ><asp:LinkButton ID="lnk_purchasinginfo" runat="server" Text="Purchasing Info"></asp:LinkButton></td>
                    </tr>
                </table>
             </td>
            </tr>
            <tr>
              <td  style="padding-left:35px">
                <table>
                    <tr>
                         <td  align="center"><asp:LinkButton ID="lnk_partproperties" runat="server" Text="Part Properties"></asp:LinkButton></td>                        
                    </tr>
                </table>
             </td>
            </tr>--%>
            <tr>
                <td align="center"  style="display:none">
                    <table   style="text-align:left">
                        <tr>
                             <td  align="center">
                                 <fieldset >
                                    <legend>
                                        <asp:Label runat="server" ID="Label1" />Attachments
                                    </legend>
                                        <table>
                                            <tr>
                                                <td  align="left">
                            
                                                    <telerik:RadUpload ID="radupload_docs" runat="server" InputSize="60" Width="500px" ControlObjectsVisibility="CheckBoxes,RemoveButtons,AddButton">
                                                        <Localization Add="Add More" />
                                                    </telerik:RadUpload>
                                                    
                                                </td>
                                                <td>
                                                <asp:Label ID="lbl_docid" runat="server" Visible="false"></asp:Label>
                                                      <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" 
                            CssClass="mdmGrid active"
                                CellSpacing="0" DataSourceID="eventsDocs" ShowHeader="true" GridLines="None" Width="80%"  >
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                               
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="eventsDocs">
                                        <Columns>
                                        <telerik:GridBoundColumn DataField="jid" Visible="false"
                                            HeaderText="jid" ReadOnly="True" SortExpression="eventCode"
                                            UniqueName="jid">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DocumentDisplayName" FilterControlAltText="Filter categoryName column"
                                            HeaderText="Asset Documents" SortExpression="DocumentDisplayName" UniqueName="DocumentDisplayName">
                                        </telerik:GridBoundColumn> 
                                        
                    
                                        
                                        
                                        <telerik:GridTemplateColumn HeaderText="Download" >
                                            <ItemTemplate> 
                                            <asp:LinkButton ID="lnk_download" runat="server"  OnClientClick='<%# String.Format("downloadFile(\"{0}\",\"{1}\");", Eval("DocumentName"),this) %>' Text="Download"></asp:LinkButton>                          
                                            <a id="downloadFileLink"> </a>
                                            <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("Id") %>' style="display:none;"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("documentid") %>' style="display:none;"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentName") %>' style="display:none;"></asp:Label>                         
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
                             <asp:SqlDataSource ID="eventsDocs" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                                SelectCommand="SELECT  etod.jid,etod.documentid,etod.UserID,etod.UploadedDate,(u.firstName+' '+u.lastName) as UserName, e.Id,d.DocumentDisplayName,d.DocumentName from 
                                JobAssetDocuments etod, Prism_Assets e, documents d, Users u where u.userID=etod.UserID and e.Id=etod.jid and d.DocumentID=etod.DocumentID and 
                                e.Id=@Id  order by etod.jid desc">
                                 <SelectParameters>
                                    <asp:ControlParameter ControlID="lbl_docid" Name="Id" DbType="Int32" />
                                     <%--<asp:QueryStringParameter Name="jid" QueryStringField="jid" DefaultValue="1" DbType="Int32" />--%>
                                 </SelectParameters>
                            </asp:SqlDataSource>
                                                </td>
                                              </tr>
                                         </table>
                                   </fieldset>
                            </td>
                            <td  >
                            <table>
                                <tr>
                                    <td>Notes<br />
                                        <telerik:RadTextBox ID="txt_notes" runat="server" TextMode="MultiLine" Height="100px" Width="200px"></telerik:RadTextBox>
                                    </td>
                           
                                </tr>
                            </table>
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="center">
                                <table style="margin: 3px 0px 3px 0px;">
                                    <tr>
                                         
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
            
        </table>
      
        
    </asp:Panel>
   
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
   </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="radbtnSaveMeter" />
                
                <asp:AsyncPostBackTrigger ControlID="radbtnCancelMeter" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>
