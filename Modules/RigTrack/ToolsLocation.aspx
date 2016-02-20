<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="ToolsLocation.aspx.cs" Debug="true" Inherits="Modules_Manage_Events_eventDashboard" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<telerik:RadScriptBlock ID="radsc" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);

                }
            }


            function movetoassetloc(obj, objassetlocid,objloctype,objaddress) {
                
                var labelval = document.getElementById('<%=lbl_assetids.ClientID %>').innerHTML;
                if (labelval != "") {
                    $find("<%=RadWindow_ContentTemplate.ClientID %>").show();
                    document.getElementById("<%=lbl_loctype.ClientID %>").innerHTML = objloctype;
                    document.getElementById("<%=lbl_locid.ClientID %>").innerHTML = objassetlocid;
                    document.getElementById("<%=lbl_address.ClientID %>").innerHTML = objaddress;
                    var divassign = document.getElementById('div_assetdetails');
                    var divtext = document.getElementById('MainDiv');
                    divassign.innerHTML = divtext.innerHTML;
                }
                else {
                    alert('No Tools are selected');
                }
            }


            function moveselectedassets() {
                
                var assetIds = document.getElementById('<%=lbl_assetids.ClientID %>').innerHTML;
                var loctype=document.getElementById("<%=lbl_loctype.ClientID %>").innerHTML;
                var locid=document.getElementById("<%=lbl_locid.ClientID %>").innerHTML;
                var path = getContextPath();
                var sessionuserid = '<%= Session["userId"] %>';
                $.ajax({
                    type: "POST",
                    url: "../realtimeassetloctable.aspx/moveselectedassets",
                    data: "{assetids:'" + assetIds + "',locid:'" + locid + "',loctype:'" + loctype + "',userid:'" + sessionuserid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        if (data.d == "success") {
                            alert("Selected Tools are moved to current location");
                        }

                    }

                });

                return false;
            }





            var NumOfRow = 1;

            function getAssetIDS(obj) {
                
                // var assets = document.getElementById('<%=lbl_assetids.ClientID %>').value;
                var finalval = "";




                if (obj.checked) {
                    document.getElementById('<%=lbl_assetids.ClientID %>').innerHTML += obj.value + ',';
                    finalval = document.getElementById('<%=lbl_assetids.ClientID %>').innerHTML;

                }
                else {
                    var splitassets = document.getElementById('<%=lbl_assetids.ClientID %>').innerHTML;
                    splitassets.substring(0, splitassets.length - 1);

                    var sval = splitassets.split(',');
                    for (k = 0; k < sval.length; k++) {

                        if (obj.value != sval[k] && sval[k] != "") {
                            finalval += sval[k] + ',';
                        }
                    }

                }

                document.getElementById('<%=lbl_assetids.ClientID %>').innerHTML = finalval;

                var assets = document.getElementById('<%=lbl_assetids.ClientID %>').value;
                var mainDiv = document.getElementById('MainDiv');
                var path = getContextPath();
                
                $.ajax({
                    type: "POST",
                    url: "../realtimeassetloctable.aspx/getHTML_new",
                    data: "{assetids:'" + finalval + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        
                        mainDiv.innerHTML = data.d;

                    }

                });



            }
            function getContextPath() {
                alert("sefwefwef");
                var ctx = window.location.pathname;
                return '/' !== ctx ? ctx.substring(0, ctx.indexOf('/', 1) + 1) : ctx;
            }

            

            function validationforjob() {

                var combo = $find("<%= combo_jobstop.ClientID %>");
                var text = combo.get_text();
                if (text == "Select Job Name" || text == "") {
                    radalert('Please Select Job Name', 330, 180, 'Alert Box', null, null);
                    return false;

                }


            }
            function validationforwarehouse() {

                var combo = $find("<%= combo_warehouse.ClientID %>");
                var text = combo.get_text();
                if (text == "Select Warehouse Name" || text == "") {
                    radalert('Please Select Warehouse Name', 330, 180, 'Alert Box', null, null);
                    return false;

                }


            }
            function validationforasset() {

                var combo = $find("<%= combo_assetstatus.ClientID %>");
                var text = combo.get_text();
                if (text == "-Select-" || text == "") {
                    radalert('Please Select Tool Status', 330, 180, 'Alert Box', null, null);
                    return false;

                }


            }
            function validationforassetno() {

                var text = document.getElementById("<%= txt_assetseralno.ClientID %>").value;
                
                if (text == "") {
                    radalert('Please Enter Tool Serial no.', 330, 180, 'Alert Box', null, null);
                    return false;

                }


            }
            function validationforlocation() {

                var combo = $find("<%= radcombo_assetcat.ClientID %>");
                var text = combo.get_text();
                if (text == "Select Tool Category" || text == "") {
                    radalert('Please Select Tool Category', 330, 180, 'Alert Box', null, null);
                    return false;

                }
                var combo = $find("<%= combo_assetsTop.ClientID %>");
                var text = combo.get_text();
               
                if (text == "Please Select Tool(s)" || text == "") {
                    radalert('Please Select Tool', 330, 180, 'Alert Box', null, null);
                    return false;

                }
                var combo = $find("<%=  COMBO_SERIALNUMBER.ClientID %>");
                var text = combo.get_text();
               
                if (text == "Please Select Serial#(s)" || text == "") {
                    radalert('Please Select Serial', 330, 180, 'Alert Box', null, null);
                    return false;

                }
            }
            
        </script>
    </telerik:RadScriptBlock>
    
    <div id="ZoneID1">
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
               
                <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                          <table width="100%">
                          
                                    <tr>
                                        <td  style="padding-left:10px">
                                        <asp:Label ID="lbl_assetids" runat="server" style="display:none"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" ></asp:Label>
                                            <table style="border:solid 1px Blue"  cellspacing="5" width="100%">
                                                <tr>
                                                    <td align="left">
                                                        Select Tool Category:<br />
                                                        
                                                        <telerik:RadComboBox runat="server" ID="radcombo_assetcat" CheckBoxes="true" 
                                                            DataSourceID="SqlGetAssetCategory" Width="300px" 
                                                        EmptyMessage="Select Asset Category" DataTextField="clientAssetName" 
                                                            DataValueField="clientAssetID" EnableCheckAllItemsCheckBox="true" 
                                                            AutoPostBack="True" onselectedindexchanged="radcombo_assetcat_SelectedIndexChanged" ></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetAssetCategory" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand="select clientAssetID, clientAssetName from clientAssets  where active='True'"></asp:SqlDataSource>
                                                    </td>
                                                    <td align="left">
                                                        Select Tool:<br />
                                                        <telerik:RadComboBox runat="server" ID="combo_assetsTop" CheckBoxes="true"  Width="300px" EmptyMessage="Please Select Tool(s)" DataTextField="AssetName" 
                                                        DataValueField="Id" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="combo_assetsTop_SelectedIndexChanged"></telerik:RadComboBox>
                                                    </td>
                                                    <td align="left">
                                                        Select Serial No. :<br />
                                                        <telerik:RadComboBox ID="COMBO_SERIALNUMBER" runat="server" CheckBoxes="true"  Width="250px" EmptyMessage="Please Select Serial#(s)" DataTextField="SerialNumber" 
                                                        DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                    <br />
                                                        <asp:Button ID="btn_view" runat="server" Text="View" onclick="btn_view_Click"  />&nbsp;
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr><td style="height:20px"></td></tr>
                                    <tr>
                                        <td align="center">
                                        <table>
                                            <tr>
                                                <td style="padding-left:10px">
                                                     <table  style="border:solid 1px Blue"  cellspacing="2">
                                                <tr>
                                                    <td style="border-right:solid 1px Blue" align="left" id="td_rig" runat="server" visible="false">
                                                        Rig Type:<br />
                                                         <telerik:RadComboBox runat="server" ID="combo_Rig" CheckBoxes="true" 
                                                            DataSourceID="SqlGetRigType" Width="200px" 
                                                        EmptyMessage="Select Rig Types" DataTextField="rigtypename" 
                                                            DataValueField="rigtypeid" EnableCheckAllItemsCheckBox="true" 
                                                            onselectedindexchanged="combo_Rig_SelectedIndexChanged" ></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetRigType" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand=" SELECT  rigtypeid, rigtypename FROM [RigTypes] where rigtypeid  in (select rigtypeid from managejoborders where status='Approved')"></asp:SqlDataSource>
                                                        <asp:Button ID="btn_rig" runat="server" Text="View" OnClientClick="javascript:return validationfortype();" onclick="btn_rig_Click" />
                                                    </td>
                                                    <td style="border-right:solid 1px Blue" align="left">
                                                        Tool Serial No. :<br />
                                                        <telerik:RadTextBox ID="txt_assetseralno" runat="server"  ></telerik:RadTextBox>
                                                        <asp:Button ID="asset_serialno" runat="server" Text="View" OnClientClick="javascript:return validationforassetno();"
                                                            onclick="asset_serialno_Click" />
                                                    </td>
                                                    <td style="border-right:solid 1px Blue" align="left">
                                                        <%--Job/Curve Group ID:<br />
                                                        <telerik:RadDropDownList runat="server" ID="combo_jobstop" Width="250px" AppendDataBoundItems="true" DropDownHeight="200px"
                                                            >
                                                            <Items>
                                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                            </Items>
                                                        </telerik:RadDropDownList>--%>
                                                        Job Name:<br />
                                                       <telerik:RadComboBox runat="server" ID="combo_jobstop" CheckBoxes="true" 
                                                            DataSourceID="SqlGetApprovedJobs" Width="200px" 
                                                        EmptyMessage="Select Job Name" DataTextField="jobname" 
                                                            DataValueField="ID" EnableCheckAllItemsCheckBox="true" ></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetApprovedJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand="select 0 as ID,'Select JobName' as jobname union select [ID],CurveGroupName as Jobname 
from [RigTrack].[tblCurveGroup] where  isActive!='0' and ID in (select jobid from PrismJobAssignedAssets)"></asp:SqlDataSource>
                                                        <asp:Button ID="btn_job_view" runat="server" Text="View" OnClientClick="javascript:return validationforjob();"
                                                            onclick="btn_job_view_Click" />
                                                    </td>
                                                    <td style="border-right:solid 1px Blue" align="left">
                                                        Warehouse Name:<br />
                                                       <telerik:RadComboBox runat="server" ID="combo_warehouse" CheckBoxes="true" 
                                                            DataSourceID="SqlGetWarehouse" Width="200px" 
                                                        EmptyMessage="Select Warehouse Name" DataTextField="Name" 
                                                            DataValueField="ID" EnableCheckAllItemsCheckBox="true" ></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetWarehouse" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand="select * from PrsimWarehouses where bitActive='True' "></asp:SqlDataSource>
                                                        <asp:Button ID="btn_viewwarehouse" runat="server" Text="View" OnClientClick="javascript:return validationforwarehouse();"
                                                            onclick="btn_viewwarehouse_Click" />
                                                    </td>
                                                    <td align="left" >
                                                        Tool Status:<br />
                                                        <telerik:RadComboBox ID="combo_assetstatus" runat="server"  EmptyMessage="- Select -">
                                                            <Items>
                                                                <%--<telerik:RadComboBoxItem Text="Select ALL" Value="0" />--%>
                                                                <telerik:RadComboBoxItem Text="Pending Shipmet/In-Transit" Value="Available" />
                                                                <telerik:RadComboBoxItem Text="On Job" Value="In Use" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <asp:Button ID="btn_assetstatus_view" runat="server" Text="View" OnClientClick="javascript:return validationforasset();"
                                                            onclick="btn_assetstatus_view_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_Reset_Top" runat="server" Text="Reset" BackColor="Red"  
                                                        onclick="btn_Reset_Top_Click" BorderStyle="Solid" CssClass="RadButton" 
                                                        Font-Bold="True"/>
                                                 </td>
                                            </tr>
                                        </table>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                         
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center" id="td_hide" style="display:none">
                                                            <!-- filter -->
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>Select Job<br />
                                                                            <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid" ></telerik:RadComboBox>
                                                                        <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                                            SelectCommand="select 0 as jid,'Select Jobname' as jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname
                                                                             from manageJobOrders where status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) and jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''"></asp:SqlDataSource>
                                                                    </td> 
                                                                    <td style="width:3px"></td>
                                                                    <td  align="left">Select Tools<br />
                                                                       <telerik:RadComboBox runat="server"  Width="300px" ID="combo_assets" CheckBoxes="true" DataSourceID="SqlGetAssets" EmptyMessage="Please Select Tool(s)" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                                                                    <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                                                      SelectCommand="select Distinct [Id],AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets  where id  in (select AssetId from PrismJobAssignedAssets)"></asp:SqlDataSource>
                                                                    </td> 
                                                                    <td style="width:3px"></td>                                                                 
                                                                    <td>
                                                                    Select Tool Shipping Status:<br />
                                                                            <telerik:RadDropDownList DataSourceID="SqlGetAssetStatus" runat="server" ID="ddl_status"  DataValueField="Id" DataTextField="StatusText"> </telerik:RadDropDownList>
                                                                <asp:SqlDataSource ID="SqlGetAssetStatus" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                                        SelectCommand="select Id,StatusText from PrsimJobAssetStatus union select 0 as Id, 'Select ALL' as StatusText"></asp:SqlDataSource>
                                                                    </td>
                                                                    <td style="width:3px"></td>
                                                                    <td valign="bottom">
                                                            <telerik:RadButton ID="btnFilterGIS" runat="server" SingleClick="false" SingleClickText="Please wait.."
                                                                Text="View" OnClick="btnFilterGIS_Click" />
                                                            </td>
                                                            <td style="width:3px"></td>
                                                            <td valign="bottom">
                                                            <telerik:RadButton ID="RadButton1" runat="server" Text="Reset" />
                                                            <%--<br />
                                                            <asp:HyperLink ID="hypJumpToData" runat="server" NavigateUrl="#Data" Style="position: relative;
                                                                left: 20px; top: 10px;" Text="View Data" />--%>
                                                        </td>
                                                            </tr>
                                                                
                                                            </table>
                                                        </td>
                                                       
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td width="50%">
                                                            <asp:Label ID="lblSQL" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%" style="z-index:-1;">
                                                                <tr>
                                                                    <td>
                                                                         <div>
                                                        <asp:Panel ID="pnl1" runat="server" >
                                                            <div id="norecords" visible="false" runat="server" style="line-height: 100px; z-index: -1;
                                                                text-align: left; width: 200px; background-color: White; border: 1px solid black;
                                                                text-align: center; position: absolute; top: 156px; display: block; margin-left: auto;
                                                                margin-right: auto; margin-top: auto; margin-bottom: auto; vertical-align: middle;
                                                                color: Red; height: 100px; margin: 0px auto; opacity: .8; left: 400px;">
                                                                <b>Data Not Available</b>
                                                            </div>
                                                            <artem:GoogleMap ID="GoogleMap1" Visible="true" EnableOverviewMapControl="false"
                                                                EnableMapTypeControl="false" EnableZoomControl="false" EnableStreetViewControl="false"
                                                                ShowTraffic="false" runat="server" ApiVersion="3" EnableScrollWheelZoom="true"
                                                                IsSensor="true" Zoom="10" DisableDoubleClickZoom="False" DisableKeyboardShortcuts="True"
                                                                EnableReverseGeocoding="True" Height="450" IsStatic="false" Key="AIzaSyC2lU7S-IMlNUTu8nHAL97_rL06vKmzfhc"
                                                                MapType="Roadmap" StaticFormat="Gif" Tilt="45" Width="1060px" StaticScale="15"
                                                                DefaultAddress="" Latitude="33.335767" Longitude="-111.944151">
                                                                <Center Latitude="33.335767" Longitude="-111.944151" />
                                                                <MapTypeControlOptions Position="TopRight" ViewStyle="Default" />
                                                                <OverviewMapControlOptions Opened="False" />
                                                                <PanControlOptions Position="TopLeft" />
                                                                <RotateControlOptions Position="TopLeft" />
                                                                <ScaleControlOptions Position="TopLeft" Style="default" />
                                                            </artem:GoogleMap>
                                                            <artem:GoogleMarkers runat="server"  ID="GoogleMarker" OnClientClick="handleClick" TargetControlID="GoogleMap1" />
                                                        </asp:Panel>
                                                                             <script type="text/javascript">
                                                                                 function handleClick(sender, e) {
                                                                                     
                                                                                     e.setIcon('https://www.google.com/mapfiles/marker_green.png');
                                                                                 }
                                                                                 
                                                                             </script>
                                                    </div>
                                                                    </td>
                                                                    <td  valign="top">
                                                                        <table>
                                                                             <tr>
                                             <td align="right" style="border:solid 1px Blue" >
                                                <table>
                                                            <tr>
                        <td>
                            Job :
                        </td>
                        <td>
                            <asp:Image ID="image_job" runat="server" ImageUrl="~/images/pin2.png" />
                        </td>
                    </tr>
                                                           <tr>
                        <td>
                            Warehouse :
                        </td>
                        <td>
                            <asp:Image ID="image1" runat="server" ImageUrl="~/images/pin1.png" />
                        </td>
                    </tr>
                     <%--<tr>
                        <td>
                            Rig :
                        </td>
                        <td>
                            <asp:Image ID="image2" runat="server" ImageUrl="~/images/pin8.png" />
                        </td>
                    </tr>--%>
                                                </table>
                                              </td>
                                         </tr>
                                                                            
                                                                             <tr>
                                                                                <td id="td_assets" runat="server">
                                                                                  <div id="MainDiv">
                                                                                  </div>
                                                                                </td>
                                                                             </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                               
                                                   
                                              
                                           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
       SelectCommand="select MJ.jobname,MJ.jid,JA.id as AssetAssignid,MJ.jobid,A.AssetId,AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,
            (u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JAS.StatusText as StatusText,JA.AssetStatus as StatusId,
            WA.primaryLatLong  as WarehouseGIS,MJ.primaryLatLong as JoborderGIS,GIs =  CASE JA.AssetStatus  WHEN '1'  THEN WA.primaryLatLong
             WHEN '2' THEN WA.primaryLatLong     WHEN '3' THEN MJ.primaryLatLong END from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,
             Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS where AT.clientAssetID=A.AssetCategoryId and WA.ID=A.WarehouseId  and MJ.jid=JA.JobId 
            and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and JA.ModifiedBy=u.userID  and ModifiedDate BETWEEN  @StartDate AND @EndDate
            AND JA.AssetStatus LIKE'%' + @status + '%' AND  MJ.jid LIKE'%' + @jobid + '%'" OnSelecting="SqlDataSource1_Selecting"
                                                OnSelected="SqlDataSource1_Selected">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="RadDatePicker1" Name="StartDate" PropertyName="SelectedDate" />
                                                    <asp:ControlParameter ControlID="RadDatePicker2" Name="EndDate" PropertyName="SelectedDate" />
                                                    <asp:ControlParameter ControlID="combo_job" DefaultValue="%" Name="jobid" PropertyName="SelectedValue" />
                                                    <asp:ControlParameter ControlID="ddl_status" DefaultValue="%" Name="status"  PropertyName="SelectedValue" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>--%>
                                        </td>
                                        <!-- WHERE (eventAMI.TimeStamp BETWEEN @StartDate AND @EndDate) -->
                                    </tr>
                                        
                                </table>   
                    </td>
                </tr>
                
                <tr>
                    <td>
                         <telerik:RadGrid ID="grid_1st" runat="server"  Visible="false"
                    CellSpacing="0" GridLines="None" Width="1080px" OnItemDataBound="grid_1st_ItemDataBound" OnItemCommand="grid_rig_ItemCommand"
                    ShowGroupPanel="True" AutoGenerateColumns="False">
                      <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView >
                         <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                         <Columns> 
                          <telerik:GridBoundColumn DataField="CurrentLocationID"  FilterControlAltText="Filter CurrentLocationID column"
                            ReadOnly="True" SortExpression="CurrentLocationID" UniqueName="CurrentLocationID" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AssetStatus" HeaderText="Tool Status"  FilterControlAltText="Filter AssetStatus column"
                            ReadOnly="True" SortExpression="AssetStatus" UniqueName="AssetStatus" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="clientAssetName"  FilterControlAltText="Filter clientAssetName column"
                            ReadOnly="True" SortExpression="clientAssetName" UniqueName="clientAssetName" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AssetId"  FilterControlAltText="Filter clientAssetName column"
                            ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SerialNumber"  FilterControlAltText="Filter SerialNumber column"
                            ReadOnly="True" SortExpression="SerialNumber" UniqueName="SerialNumber" Display="false">
                        </telerik:GridBoundColumn>
                       <telerik:GridBoundColumn DataField="CurrentLocationType"  FilterControlAltText="Filter CurrentLocationType column"
                            ReadOnly="True" SortExpression="CurrentLocationType" UniqueName="CurrentLocationType" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn   HeaderText="Tool&#160;Category"   FilterControlAltText="Filter clientAssetName column"
            SortExpression="clientAssetName" UniqueName="clientAssetName1">
            <ItemTemplate>
                <asp:Label ID="lbl_clientAssetName" runat="server" Text='<%# Eval("clientAssetName") %>'/>
            </ItemTemplate>                             
            </telerik:GridTemplateColumn>                            
                        <telerik:GridTemplateColumn HeaderText="Tool Name" >
            <ItemTemplate>                              
            <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
            </ItemTemplate>
        </telerik:GridTemplateColumn>  
                        <telerik:GridTemplateColumn   HeaderText="Serial&#160;Number"   FilterControlAltText="Filter SerialNumber column"
            SortExpression="SerialNumber" UniqueName="SerialNumber1">
                <ItemTemplate>
                    <asp:Label ID="lbl_SerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                </ItemTemplate>                                
            </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn   HeaderText="Tool&#160;Status"   FilterControlAltText="Filter Warehouse column"
            SortExpression="AssetStatus" UniqueName="AssetStatus1">
                <ItemTemplate>
                    <asp:Label ID="lbl_AssetStatus" runat="server" Text='<% #GetLabelText(Eval("AssetStatus")) %>' />                           
                </ItemTemplate>                                
            </telerik:GridTemplateColumn>   
            
                        <telerik:GridTemplateColumn   HeaderText="Current&#160;Location"   FilterControlAltText="Filter AssetStatus column"
            SortExpression="CurrentLocationType" UniqueName="CurrentLocationType1" >
            <ItemTemplate>
                <asp:Label ID="lbl_CurrentLocationType" runat="server" Text='<%# Eval("CurrentLocationType") %>'/>
            </ItemTemplate>
                             
            </telerik:GridTemplateColumn>  
                    
                    </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <HeaderStyle VerticalAlign="Bottom" />
                    <ItemStyle VerticalAlign="Top" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                     <telerik:RadGrid ID="grid_warehouse" runat="server"   Visible="false"
                    CellSpacing="0" GridLines="None" Width="1080px"  OnItemDataBound="grid_warehouse_ItemDataBound"
                    ShowGroupPanel="True" AutoGenerateColumns="False" AllowPaging="True" 
                            AllowSorting="True">
                    <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView CommandItemDisplay="Top">
                          <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                         <Columns> 
                         
                         <telerik:GridTemplateColumn   HeaderText="Tool&#160;Category"   FilterControlAltText="Filter ID column"
            SortExpression="ID" UniqueName="ID">
            <ItemTemplate>
                <asp:Label ID="lbl_warehouseid" runat="server" Text='<%# Eval("ID") %>'/>
            </ItemTemplate>                             
            </telerik:GridTemplateColumn>  
                    </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <HeaderStyle VerticalAlign="Bottom" />
                    <ItemStyle VerticalAlign="Top" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                <telerik:RadGrid ID="grid_job" runat="server"   Visible="false"
                    CellSpacing="0" GridLines="None" Width="1080px"  OnItemDataBound="grid_job_ItemDataBound"
                    ShowGroupPanel="True" AutoGenerateColumns="False" AllowPaging="True" 
                            AllowSorting="True">
                    <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView CommandItemDisplay="Top">
                          <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                         <Columns> 
                         
                         <telerik:GridTemplateColumn   HeaderText="Tool&#160;Category"   FilterControlAltText="Filter ID column"
            SortExpression="ID" UniqueName="ID">
            <ItemTemplate>
                <asp:Label ID="lbl_jobid" runat="server" Text='<%# Eval("ID") %>'/>
            </ItemTemplate>                             
            </telerik:GridTemplateColumn>  
                    </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <HeaderStyle VerticalAlign="Bottom" />
                    <ItemStyle VerticalAlign="Top" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                    </td>
                  </tr>
                 <tr>
                    <td>
                     <telerik:RadGrid ID="grid_rig" runat="server"     Visible="false"
                    CellSpacing="0" GridLines="None" Width="1080px"  OnItemDataBound="grid_rig_ItemDataBound"
                    ShowGroupPanel="True" AutoGenerateColumns="False" AllowPaging="True" 
                            AllowSorting="True">
                    <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView CommandItemDisplay="Top">
                          <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                         <Columns> 
                          <telerik:GridBoundColumn DataField="CurrentLocationID"  FilterControlAltText="Filter CurrentLocationID column"
                            ReadOnly="True" SortExpression="CurrentLocationID" UniqueName="CurrentLocationID" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AssetStatus"  FilterControlAltText="Filter AssetStatus column"
                            ReadOnly="True" SortExpression="AssetStatus" UniqueName="AssetStatus" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="clientAssetName"  FilterControlAltText="Filter clientAssetName column"
                            ReadOnly="True" SortExpression="clientAssetName" UniqueName="clientAssetName" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AssetId"  FilterControlAltText="Filter clientAssetName column"
                            ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SerialNumber"  FilterControlAltText="Filter SerialNumber column"
                            ReadOnly="True" SortExpression="SerialNumber" UniqueName="SerialNumber" Display="false">
                        </telerik:GridBoundColumn>
                       <telerik:GridBoundColumn DataField="CurrentLocationType"  FilterControlAltText="Filter CurrentLocationType column"
                            ReadOnly="True" SortExpression="CurrentLocationType" UniqueName="CurrentLocationType" Display="false">
                        </telerik:GridBoundColumn>
                         <telerik:GridTemplateColumn   HeaderText="Rig&#160;name"   FilterControlAltText="Filter Rigname column"
                            SortExpression="Rigname" UniqueName="Rigname">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Rigname" runat="server" Text='<%# Eval("Rigname") %>'/>
                            </ItemTemplate>                             
                        </telerik:GridTemplateColumn>                           
                        
                         <telerik:GridTemplateColumn   HeaderText="job&#160;name"   FilterControlAltText="Filter jobname column"
                            SortExpression="jobname" UniqueName="jobname">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("jobname") %>'/>
                            </ItemTemplate>                             
                        </telerik:GridTemplateColumn>                            
                        
                        <telerik:GridTemplateColumn   HeaderText="Tool&#160;Category"   FilterControlAltText="Filter clientAssetName column"
                            SortExpression="clientAssetName" UniqueName="clientAssetName1">
                            <ItemTemplate>
                                <asp:Label ID="lbl_clientAssetName" runat="server" Text='<%# Eval("clientAssetName") %>'/>
                            </ItemTemplate>                             
                        </telerik:GridTemplateColumn>                            
                        <telerik:GridTemplateColumn HeaderText="Tool Name" >
                        <ItemTemplate>                              
                        <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                        <telerik:GridTemplateColumn   HeaderText="Serial&#160;Number"   FilterControlAltText="Filter SerialNumber column"
            SortExpression="SerialNumber" UniqueName="SerialNumber1">
                <ItemTemplate>
                    <asp:Label ID="lbl_SerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                </ItemTemplate>                                
            </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn   HeaderText="Tool&#160;Status"   FilterControlAltText="Filter Warehouse column"
            SortExpression="AssetStatus" UniqueName="AssetStatus1">
                <ItemTemplate>
                    <asp:Label ID="lbl_AssetStatus" runat="server" Text='<% #GetLabelText(Eval("AssetStatus")) %>' />                           

                </ItemTemplate>                                
            </telerik:GridTemplateColumn>                    
                        <telerik:GridTemplateColumn   HeaderText="Current&#160;Location"   FilterControlAltText="Filter AssetStatus column"
            SortExpression="CurrentLocationType" UniqueName="CurrentLocationType1" >
            <ItemTemplate>
                <asp:Label ID="lbl_CurrentLocationType" runat="server" Text='<%# Eval("CurrentLocationType") %>'/>
            </ItemTemplate>
                             
            </telerik:GridTemplateColumn>  
                    
                    </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <HeaderStyle VerticalAlign="Bottom" />
                    <ItemStyle VerticalAlign="Top" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                    </td>
                  </tr>
                 <tr>
                    <td>
                     <telerik:RadGrid ID="grid_ex_rig" runat="server"    Visible="false" 
                    CellSpacing="0" GridLines="None" Width="1080px"  OnItemDataBound="grid_ex_rig_ItemDataBound"
                    ShowGroupPanel="True" AutoGenerateColumns="False" AllowPaging="True" 
                            AllowSorting="True">
                    <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView CommandItemDisplay="Top">
                          <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                            ShowExportToPdfButton="true"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                         <Columns> 
                          
                         <telerik:GridTemplateColumn   HeaderText="Rig&#160;name"   FilterControlAltText="Filter Rigname column"
                            SortExpression="Rigname" UniqueName="Rigname">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Rigname" runat="server" Text='<%# Eval("Rigname") %>'/>
                                <asp:Label ID="lbl_gis" runat="server" Text='<%# Eval("primaryLatLong") %>' Visible="false"/>
                                <asp:Label ID="lbl_Address" runat="server" Text='<%# Eval("Address") %>' Visible="false"/>
                                
                            </ItemTemplate>                             
                        </telerik:GridTemplateColumn>                           
                        
                         <telerik:GridTemplateColumn   HeaderText="job&#160;name"   FilterControlAltText="Filter jobname column"
                            SortExpression="jobname" UniqueName="jobname">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("jobname") %>'/>
                            </ItemTemplate>                             
                        </telerik:GridTemplateColumn>                            
                        <telerik:GridHyperLinkColumn AllowSorting="False" DataNavigateUrlFields="primaryLatLong" 
                                DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                                HeaderText="Rig Location" Target="_new" Text="View&#160;Map" UniqueName="gisLink">
                                <ItemStyle ForeColor="Blue" />
                            </telerik:GridHyperLinkColumn> 
                    
                    </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <HeaderStyle VerticalAlign="Bottom" />
                    <ItemStyle VerticalAlign="Top" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                    </td>
                  </tr>
                 <tr>
                    <td>
                         <telerik:RadGrid ID="RadGrid1" runat="server"  AllowPaging="True"    Visible="false"
                    CellSpacing="0" GridLines="None" Width="1080px" AllowSorting="True" OnGroupsChanging="RadGrid1_GroupsChanging"
                    OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                    ShowGroupPanel="True" AutoGenerateColumns="False">
                    <ClientSettings AllowDragToGroup="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <AlternatingItemStyle VerticalAlign="Top" />
                    <MasterTableView >
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                         <Columns>
                        <telerik:GridBoundColumn DataField="jid" DataType="System.Int32" FilterControlAltText="Filter jid column"
                            ReadOnly="True" SortExpression="jid" UniqueName="IntervalMeterDataId" Display="false">
                        </telerik:GridBoundColumn> 
                          <telerik:GridBoundColumn DataField="GIs"  FilterControlAltText="Filter jid column"
                            ReadOnly="True" SortExpression="GIs" UniqueName="GIs" Display="false">
                        </telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn DataField="AssetId"  FilterControlAltText="Filter AssetId column"
                            ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="false">
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn DataField="StatusId"  FilterControlAltText="Filter StatusId column"
                            ReadOnly="True" SortExpression="StatusId" UniqueName="StatusId" Display="false">
                        </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="WarehouseGIS"  FilterControlAltText="Filter WarehouseGIS column"
                            ReadOnly="True" SortExpression="WarehouseGIS" UniqueName="WarehouseGIS" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn   HeaderText="Job Name"   FilterControlAltText="Filter jobname column"
                            SortExpression="jobname" UniqueName="jobname">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("jobname") %>'/>
                            </ItemTemplate>
                             
                            </telerik:GridTemplateColumn>  
                            <telerik:GridTemplateColumn   HeaderText="Job ID"   FilterControlAltText="Filter jobid column"
                            SortExpression="jobid" UniqueName="jobid" >
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobid" runat="server" Text= '<%# Eval("jobid") %>'/>
                            </ItemTemplate>                             
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn DataField="AssetId" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                    ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="False" >
                </telerik:GridBoundColumn>    
                <telerik:GridTemplateColumn   HeaderText="Tool&#160;Category"   FilterControlAltText="Filter clientAssetName column"
                    SortExpression="clientAssetName" UniqueName="clientAssetName">
                    <ItemTemplate>
                        <asp:Label ID="lbl_clientAssetName" runat="server" Text='<%# Eval("clientAssetName") %>'/>
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn>                            
                <telerik:GridTemplateColumn HeaderText="Tool Name" >
                    <ItemTemplate>                              
                    <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn   HeaderText="Serial&#160;Number"   FilterControlAltText="Filter SerialNumber column"
                    SortExpression="SerialNumber" UniqueName="SerialNumber">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                        </ItemTemplate>                                
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Warehouse&#160;Name"   FilterControlAltText="Filter Warehouse column"
                    SortExpression="Warehouse" UniqueName="Warehouse">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Warehouse" runat="server" Text='<%# Eval("Warehouse") %>'/>                           
                        </ItemTemplate>                                
                    </telerik:GridTemplateColumn> 
                                                           
                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;BY"   FilterControlAltText="Filter ModifiedBY column"
                    SortExpression="username" UniqueName="username" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("username") %>'/>
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;Date"   FilterControlAltText="Filter ModifiedDate column"
                    SortExpression="ModifiedDate" UniqueName="ModifiedDate" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_ModifiedDate" runat="server" Text='<%# Eval("ModifiedDate") %>'/>
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn   HeaderText="Tool&#160;Status"   FilterControlAltText="Filter AssetStatus column"
                    SortExpression="AssetStatus" UniqueName="AssetStatus" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_AssetStatus" runat="server" Text='<%#Eval("AssetStatus") %>'/>
                    </ItemTemplate>
                             
                    </telerik:GridTemplateColumn>  
                     <telerik:GridHyperLinkColumn AllowSorting="False" DataNavigateUrlFields="WarehouseGIS" 
                                DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                                HeaderText="Warehouse Location" Target="_new" Text="View&#160;Map" UniqueName="gisLink">
                                <ItemStyle ForeColor="Blue" />
                            </telerik:GridHyperLinkColumn>  
                             <telerik:GridHyperLinkColumn AllowSorting="False" DataNavigateUrlFields="JoborderGIS" 
                                DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                                HeaderText="Job Location" Target="_new" Text="View&#160;Map" UniqueName="gisLink">
                                <ItemStyle ForeColor="Blue" />
                            </telerik:GridHyperLinkColumn>                            
                    <telerik:GridHyperLinkColumn AllowSorting="False" DataNavigateUrlFields="GIs" 
                                DataNavigateUrlFormatString="http://maps.google.com/?q={0}" FilterControlAltText="Filter gisLink column"
                                HeaderText="Current Location" Target="_new" Text="View&#160;Map" UniqueName="gisLink">
                                <ItemStyle ForeColor="Blue" />
                            </telerik:GridHyperLinkColumn>    
                    </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                    <HeaderStyle VerticalAlign="Bottom" />
                    <ItemStyle VerticalAlign="Top" />
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid><asp:Label ID="lblCount" Text="0" runat="server" Visible="false" />
               
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadWindowManager ID="RadWindowManager1"  runat="server"   
                                Modal="true" Animation="Resize" >
                            <Windows>
                                <telerik:RadWindow ID="RadWindow_ContentTemplate" runat="server"  Width="600px"
                                    Height="500px" >
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_loctype" runat="server" style="display:none"></asp:Label>
                                                    <asp:Label ID="lbl_locid" runat="server" style="display:none"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr><td  style="font-size:13px;font-weight:bold">Are you sure you want to move the following selected Tools:</td></tr>
                                            
                                            <tr>
                                                <td>
                                                    <div id="div_assetdetails"></div>
                                                </td>
                                            </tr>
                                            <tr><td style="height:5px"></td></tr>
                                            <tr><td  style="font-size:13px;font-weight:bold">To Following Location:</td></tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_address" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <%--<telerik:RadButton ID="RadButton2" runat="server" Text="Save" AutoPostBack="true" OnClick="Button2_Click">
                                                    </telerik:RadButton>--%>
                                                    <asp:Button ID="btn_crtjob" BackColor="Green" Text="Move" OnClientClick="javascript:return moveselectedassets();" runat="server">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                        
                                </telerik:RadWindow>
                            </Windows>
                            </telerik:RadWindowManager>
                    </td>
                </tr>
                <tr>
            <td>
                 <telerik:RadWindowManager ID="radwin" runat="server">
            <Windows></Windows>
            </telerik:RadWindowManager>
            </td>
        </tr>
            </table>
                    
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="customCss">
    
</asp:Content>
