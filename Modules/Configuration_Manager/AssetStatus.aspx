<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="AssetStatus.aspx.cs" Inherits="Modules_Configuration_Manager_AssetRepairStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>--%>
    
 
       <link rel="stylesheet/less" type="text/css" href="../../css/mdm.less" />
    <link rel="stylesheet/less" type="text/css" href="../../css/jquery-ui-1.10.3.custom.css" />
    <script src="../../js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <script type="text/javascript">
        less = {
            env: "development", // or "production"
            async: false,       // load imports async
            fileAsync: false,   // load imports async when in a page under
            // a file protocol
            poll: 1000,         // when in watch mode, time in ms between polls
            functions: {},      // user functions, keyed by name
            dumpLineNumbers: "comments", // or "mediaQuery" or "all"
            relativeUrls: false // whether to adjust url's to be relative
            // if false, url's are already relative to the
            // entry less file
            //rootpath: ":/mdm.com/"// a path to add on to the start of every url
            //resource
        }
    </script>
    <script src="../../js/lesscss.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&amp;language=en"></script>
    <script type="text/javascript" src="../../js/maps.google.com/gmap3.js"></script>
         <script type="text/javascript">

             function viewMap(link) {
                 alert(link);
                 try {
                     link_params = link.match(/\?.=(.+)/)[1];
                     //alert(link_params);
                     //SIZE
                     height = $(window).height() * .75;
                     width = $(window).width() * .75;

                     $("<div id='map_dialog'/>")
                        .dialog({
                            //show: {effect: 'fade', duration: 2000},
                            //hide: {effect: 'explode', duration: 3000},
                            height: height,
                            width: width,
                            modal: true
                        }).gmap3({
                            map: {
                                options: {
                                    maxZoom: 14
                                }
                            },
                            marker: {
                                address: link_params
                            }
                        }, "autofit")
                        .show();
                 } catch (e) {
                     alert(e);
                     alert("Missing GIS Information.");
                 }
             };

             function update_view_map() {
                 try {
                     $("[href^='http://maps.google.com'], [href^='https://maps.google.com']").each(function () {
                         link = $(this).attr("href");

                         $(this).attr({
                             href: "javascript:void('View map')",
                             target: "",
                             onclick: "viewMap( '" + link + "' )"
                         });
                     });
                 } catch (e) { }
             }

             $(function () {
                 window.setInterval(function () {
                     update_view_map();
                 }, 100);

                 update_view_map();

                 $("[closeMap]").fadeOut("fast");

                 $("input[text='Save']").attr("style", "background: light-green");
                 $("input[text='Reset']").attr("style", "background: maroon");
             });


             function validationforassetcat() {
                 if (document.getElementById("<%=radcombo_assetcat.ClientID%>").value == "Select" || document.getElementById("<%=radcombo_assetcat.ClientID%>").value == "") {
                     radalert('Select Asset Category', 330, 180, 'Alert Box', null, null);
                     return false;
                 }


                 function OnClientClicking(sender, args) {
                     //alert(sender);
                     var callBackFunction = Function.createDelegate(sender, function (argument) {
                         if (argument) {
                             this.click();
                         }
                     });

                     var text = "Are you sure want to move Asset to Maintanence?";
                     radconfirm(text, callBackFunction, 300, 100, null, "Confirm Box");
                     args.set_cancel(true);
                 }
             }
             function openReport(AssetId) {
                 // alert(AssetRid);
                 var status = "EDIT";
                 var url = "../../Modules/Configuration_Manager/AssetRptPopup.aspx?AssetId=" + AssetId + "";
                 // alert(url);
                 document.getElementById('<%=iframe2.ClientID %>').src = url;
                 window.radopen(null, "RadWindow2");
                 return false;
             }
        </script>
    
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
            <td align="center">
                <table cellpadding="0" cellspacing="0" >
                    <tr>
                        <td align="center">
                            <asp:HiddenField ID="hidd_asset" runat="server" />
                            <table  >
                                <tr>
                                    <td>
                            <table style="border:solid 1px #000000">
                                <tr>
                                    <td align="left">
                                        Select Asset Category:<br />
                                        <telerik:RadComboBox ID="radcombo_assetcat" CheckBoxes="true" Width="250px" EnableCheckAllItemsCheckBox="true" DataSourceID="SqlGetassetstatus" EmptyMessage="Select" runat="server" DataTextField="clientAssetName" DataValueField="clientAssetID">

                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetassetstatus"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                            SelectCommand="select * from clientAssets" >
                                
                                        </asp:SqlDataSource>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btn_viewassetcat" runat="server" Text="View" OnClientClick="javascript:return validationforassetcat();"  OnClick="btn_viewassetcat_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                         <td align="left">
                            <table style="border:solid 1px #000000">
                                <tr>
                                    <td align="left">
                                        Asset Serial Number:<br />
                                        <asp:TextBox ID="txt_assetsno" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        Asset&#160;Status:<br />
                                        <telerik:RadComboBox ID="radcombo_rstatus" EmptyMessage="Select Asset Status" runat="server">
                                            <Items>
                                            <telerik:RadComboBoxItem Text="Select ALL" Value="0" />
                                                <telerik:RadComboBoxItem Text="Available" Value="Ok" />
                                                <telerik:RadComboBoxItem Text="On Job" Value="Job" />
                                                <telerik:RadComboBoxItem Text="Maintenance" Value="Maintenance" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btn_view" runat="server" Text="View" OnClick="btn_view_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                         <td valign="bottom" align="left">
                                        &nbsp;&nbsp;<asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="3">
                             <telerik:RadGrid ID="radgrid_repairstatus"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                                  AllowPaging="true" AllowSorting="true"  OnPageIndexChanged="radgrid_clientdetails_PageIndexChanged"
                                OnItemDataBound="radgrid_repairstatus_ItemDataBound" OnItemCommand="radgrid_repairstatus_ItemCommand">
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView   >
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                        <Columns>
                            
                            
                            <telerik:GridBoundColumn DataField="AssetName"
                                HeaderText="AssetName" SortExpression="AssetName" UniqueName="AssetName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="clientAssetName"
                                HeaderText="Asset Category" SortExpression="clientAssetName" UniqueName="clientAssetName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SerialNumber" HeaderText="SerialNumber" SortExpression="SerialNumber" UniqueName="SerialNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Type" 
                                HeaderText="Type" SortExpression="Type" UniqueName="Type"  Visible="false">
                                <ItemStyle CssClass="options" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Make" 
                                HeaderText="Make" SortExpression="Make" UniqueName="Make" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DailyCharge" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                                HeaderText="Daily Charge($)" SortExpression="DailyCharge" UniqueName="DailyCharge" Visible="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Run hrs since last maintenance" AllowFiltering="false">
                                <ItemTemplate >                              
                                <asp:Label ID="lbl_totalrunhrs" runat="server"></asp:Label>
                                <asp:Label ID="lbl_RunHrsToMaintenance" runat="server" Text='<%# Eval("runhrMaintenance") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbl_maintenancepercentage" runat="server" Text='<%# Eval("maintenancepercentage") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbl_assetname" runat="server" Text='<%# Eval("AssetName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("SerialNumber") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbl_assetcat" runat="server" Text='<%# Eval("clientAssetName") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Total Cumulative Run Hrs" AllowFiltering="false">
                                <ItemTemplate >                              
                                <asp:Label ID="lbl_totalCumulativerunhrs" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="RunHrsToMaintenance" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                                HeaderText="RunHrsTo Maintenance" SortExpression="RunHrsToMaintenance" UniqueName="RunHrsToMaintenance">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="maintenancepercentage" AllowFiltering="false" FilterControlAltText="Filter meterType column"
                                HeaderText="Maintenance(% of total run hrs)" SortExpression="maintenancepercentage" UniqueName="maintenancepercentage">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Maintenance Records" >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                       <asp:LinkButton ID="lnkbtn_rpt" runat="server" Text="View" OnClientClick='<%# "openReport(\"" + Eval("ID" ) + "\" ); return false;" %>' />
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Asset Status" >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                    <asp:CheckBox runat="server" ID="isChecked"    AutoPostBack="true" onclick="javascript:ChkClick();" OnCheckedChanged="CheckChanged"  Visible="false"/>
                                    <asp:Label ID="lbl_statuscheck" runat="server" Text='<%# Bind("repairstatus") %>'  Visible="false"></asp:Label>
                                    <%--<asp:Label ID="lbl_currentloctype" runat="server" Text='<%# Bind("CurrentLocationType") %>'  Visible="false"></asp:Label>--%>
                                    <asp:Label ID="lbl_location" runat="server" ></asp:Label>
                                    <asp:Label ID="lbl_assetid" runat="server" Text='<%# Bind("MainAssetID") %>' Visible="false" ></asp:Label>
                                    <%--<telerik:RadButton ID="btn_maintain" runat="server" ForeColor="Green" Text="Move to Maintanence" OnClick="btn_maintain_Click" OnClientClicking="OnClientClicking"/>--%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn HeaderText="Current&#160;Location" >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                       <asp:Label ID="lbl_locationame" runat="server"  ></asp:Label>
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridTemplateColumn HeaderText="View&#160;Map"  >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                       <%--<asp:LinkButton ID="lnk_map" runat="server"></asp:LinkButton>--%>
                                       <asp:Label ID="lnk_map" runat="server" ></asp:Label>
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn HeaderText="Work Order" Visible="false" >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                       <asp:LinkButton ID="lnk_downloaddoc" runat="server" OnClick="lnk_downloaddoc_OnClick"></asp:LinkButton>
                                       <%--<asp:Label ID="lnk_worder" runat="server"></asp:Label>--%>
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridHyperLinkColumn AllowSorting="False" 
                                         DataNavigateUrlFields="primaryLatLong" 
                                         DataNavigateUrlFormatString="http://maps.google.com/?q={0}" 
                                         FilterControlAltText="Filter gisLink column" Target="_new" Text="View Map" 
                                         UniqueName="primaryLatLong">
                <ItemStyle ForeColor="Blue" />
            </telerik:GridHyperLinkColumn>--%>
                           <%-- <telerik:GridBoundColumn DataField="startdate" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="Start Date" SortExpression="startdate" UniqueName="startdate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="enddate" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="End Date" SortExpression="enddate" UniqueName="enddate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="cost" 
                                HeaderText="Cost($)" SortExpression="cost" UniqueName="cost">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Name"
                                HeaderText="Customer" SortExpression="Name" UniqueName="Name" >
                            </telerik:GridBoundColumn>--%>
                                    
                                    
                                </Columns>
                                
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                
                            </MasterTableView>
                            
                        </telerik:RadGrid>
                        <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                            SelectCommand="select  PA.AssetName,PA.Id,* from Prism_Assets P,PrismAssetName PA,clientAssets AC
                            where  P.AssetName=PA.ID and P.AssetCategoryId=AC.clientAssetID  order by P.Id"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="map" runat="server" ></div>
            </td>
        </tr>
    </table>
    <telerik:RadWindowManager ID="radwin" runat="server">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server"  Modal="true" Width="750px" Height="600px">
                                    <ContentTemplate>
                                        <table>
                                             <%--<tr><td style="color:blue;font-weight:bold;cursor:default" align="center"> <asp:Button ID="Button2" runat="server" Text="Close" BackColor="Blue"  onclick="btn_view_Click" /> </td></tr>--%>
                                            <tr>
                                                <td>  <iframe id="iframe2" runat="server" width="750px" height="600px"  ></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                      
                                     </ContentTemplate>
                                 </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

