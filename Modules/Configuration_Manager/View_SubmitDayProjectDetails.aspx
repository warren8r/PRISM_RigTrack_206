<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="View_SubmitDayProjectDetails.aspx.cs" Inherits="Modules_Configuration_Manager_InsertProjectDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
    .container {
		  width: 1060px;
		  height: 300px;
		  background-color: #DCDCDC;
		  overflow: scroll; /* showing scrollbars */
	}

</style>

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
        </script>
      
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td>
                    <div class="container">
                    <artem:GoogleMap ID="GoogleMap1" Visible="false" style="display:none;" 
                                 EnableOverviewMapControl="false" EnableMapTypeControl="false"
                                 EnableZoomControl="false" EnableStreetViewControl="false"  ShowTraffic="false" runat="server"
                                 ApiVersion="3" EnableScrollWheelZoom="false" IsSensor="false"
                                 DefaultAddress="430 W Warner Rd. , Tempe AZ"
                                 DisableDoubleClickZoom="False" DisableKeyboardShortcuts="True" 
                                 EnableReverseGeocoding="True" Height="650" Zoom="13"  IsStatic="true" 
                                 Key="AIzaSyC2lU7S-IMlNUTu8nHAL97_rL06vKmzfhc" 
                                 MapType="Roadmap"
                                 StaticFormat="Gif" Tilt="45"  Width="550"
                                 StaticScale="2"> </artem:GoogleMap>
                          <telerik:RadGrid ShowGroupPanel="true" AutoGenerateColumns="false" ID="grid_project"
                                                DataSourceID="SqlDataSource1" AllowFilteringByColumn="false" AllowSorting="false"
                                                ShowFooter="false" runat="server" GridLines="None" AllowPaging="true" OnItemCommand="grid_project_ItemCommand"
                                                OnItemDataBound="grid_project_ItemDataBound">
                                                <PagerStyle Mode="NextPrevAndNumeric" />
                                                <MasterTableView DataKeyNames="jid" AllowMultiColumnSorting="false" EnableColumnsViewState="false"
                                                    EditMode="InPlace" CommandItemDisplay="None" NoMasterRecordsText="No Data have been added.">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle ForeColor="Black" />
                                                    <Columns>
                                                   
                                                    </Columns>
                                                </MasterTableView>
                          </telerik:RadGrid>
                          </div>
                           <asp:SqlDataSource ID="SqlDataSource1"
                                             SelectCommand="select * from manageJobOrders where datasource='IMPORT' order by jid"          runat="server"></asp:SqlDataSource>
                                           
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td style="line-height:10px"></td></tr>
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td><asp:Button ID="btn_insert" runat="server" Text="Save" 
                            onclick="btn_insert_Click" /> 
                             </td>
                    <td style="width:5px"></td>
                    <td><asp:Button ID="btn_reset" runat="server" Text="Reset"   onclick="btn_reset_Click" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

