<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="Comp_MovetoMaintanence.aspx.cs" Inherits="Modules_Configuration_Manager_Comp_Re_assignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    
 
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

             confirmCallBackFn

             function confirmCallBackFn(arg) {
                // radalert("<strong>radconfirm</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Result");
             }
//             function ChkClick(sender, args) {
//                 function callBackFunction(arg) {
//                     if (arg == true) {

//                     }
//                 }
//                 radconfirm("Are you sure want to move Component to Maintanence", callBackFunction, 300, 160, null, "Confirmation Box");
//                 args.set_cancel(true);
//             }

//             function ChkClick() {
//                 var res = window.confirm("Are you sure want to move Component to Maintanence");
//                 //alert(res);
//                 if (res == true) {
//                    // alert("True");
//                     document.getElementById('<%=hidd_acc.ClientID%>').value = "1";
//                     return true;
//                 }
//                 else {
//                     //alert("False");
//                     document.getElementById('<%=hidd_acc.ClientID%>').value = "0";
//                     return false;
//                 }
             //             }
             function OnClientClicking(sender, args) {
                 //alert(sender);
                 var callBackFunction = Function.createDelegate(sender, function (argument) {
                     if (argument) {
                         this.click();
                     }
                 });

                 var text = "Are you sure want to move Component to Maintanence?";
                 radconfirm(text, callBackFunction, 300, 100, null, "Confirm Box");
                 args.set_cancel(true);
             }
        </script>
      

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
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
                <asp:HiddenField ID="hidd_acc" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="700px">
                    <tr>
                        <td align="center">
                            <table  style="border:solid 1px #000000">
                                <tr>
                                    <td align="left">
                                        Select&#160;AssetName:<br />
                                        <telerik:RadComboBox runat="server" ID="combo_asset"  DataSourceID="SqlGetAssets" DataTextField="AssetName" DataValueField="Id"  EmptyMessage="Select AssetName" Width="200px"
                                                ></telerik:RadComboBox>
                                            <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                             SelectCommand="select 0 as Id,CONVERT(varchar, 'Select ALL') as AssetName union Select Id,AssetName from PrismAssetName"></asp:SqlDataSource>
                                    </td>
                                    <td>
                                        Enter&#160;Asset&#160;Serial#:<br />
                                        <telerik:RadTextBox ID="txt_assetserialno" runat="server"></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btn_Assetview" runat="server" Text="View" OnClick="btn_viewasset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center">
                            <table style="border:solid 1px #000000">
                                <tr>
                                    <td align="left">
                                        Comp.&#160;Serial&#160;Number:<br />
                                        <asp:TextBox ID="txt_assetserialno2" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        Comp.&#160;Status:<br />
                                        <telerik:RadComboBox ID="radcombo_rstatus" EmptyMessage="Select Asset Status" runat="server">
                                            <Items>
                                            <telerik:RadComboBoxItem Text="Select ALL" Value="0" />
                                                <telerik:RadComboBoxItem Text="Available" Value="Ok" />
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
                        <td align="center">
                            <br />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lbl_grid" runat="server"></asp:Label>
                             <telerik:RadGrid ID="radgrid_repairstatus"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                                  AllowPaging="true" AllowSorting="true" OnItemDataBound="radgrid_repairstatus_ItemDataBound" OnPageIndexChanged="radgrid_repairstatus_PageIndexChanged">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView  DataKeyNames="CompID" >
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Condition" >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                    
                                    <asp:CheckBox runat="server" ID="isChecked"  Visible="false"  
                                       OnCheckedChanged="isChecked_CheckedChanged" onclick="javascript:return OnClientClicking" />
                                    <asp:Label ID="lbl_statuscheck" runat="server" Text='<%# Bind("comrstatus") %>' ></asp:Label>
                                    <asp:Label ID="lbl_compid" runat="server" Text='<%# Bind("CompID") %>' Visible="false" ></asp:Label>
                                  
                                    
                                    <telerik:RadButton ID="btn_maintain" runat="server" ForeColor="Green" Text="Move to Maintanence" OnClick="btn_maintain_Click" OnClientClicking="OnClientClicking">
                            </telerik:RadButton>
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn DataField="ComponentName"  HeaderText="Comp.Name" SortExpression="ComponentName" UniqueName="ComponentName"/>
                             <telerik:GridBoundColumn DataField="Serialno" HeaderText="Comp.Serial No." SortExpression="Serialno" UniqueName="Serialno"/>
                             <telerik:GridTemplateColumn HeaderText="Asset Name" >                                
                                <ItemTemplate >
                                       <asp:Label ID="lbl_assetname" runat="server"  ></asp:Label>
                                      <asp:Label ID="lbl_AID" runat="server"  Visible="false" ></asp:Label>
                                </ItemTemplate>                            
                            </telerik:GridTemplateColumn>

                             <telerik:GridTemplateColumn HeaderText="Asset Category" >                                
                                <ItemTemplate >
                                       <asp:Label ID="lbl_assetcategory" runat="server"  ></asp:Label>
                                </ItemTemplate>                            
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderText="Serial Number" >                                
                                <ItemTemplate >
                                       <asp:Label ID="lbl_assetserialno" runat="server"  ></asp:Label>
                                </ItemTemplate>                            
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="Type"  HeaderText="Type" SortExpression="Type" UniqueName="Type" />
                            <telerik:GridBoundColumn DataField="Make"  HeaderText="Make" SortExpression="Make" UniqueName="Make"/>
                            <telerik:GridBoundColumn DataField="Cost"  HeaderText="Component Cost($)" SortExpression="Cost" UniqueName="Cost" />
                                                    
                                </Columns>
                                
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                
                            </MasterTableView>
                            
                        </telerik:RadGrid>
                        <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                            SelectCommand="select  PA.AssetName,PA.Id,P.Id as AID ,* from Prism_Assets P,PrismAssetName PA,clientAssets AC
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
    <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
    </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

