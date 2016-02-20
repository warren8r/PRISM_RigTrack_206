<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateShippingDoc.aspx.cs" Inherits="Modules_Configuration_Manager_CreateShippingDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadScriptBlock runat="server">
        <script language="javascript" type="text/javascript">
            function combo_kitsOnClientSelectedIndexChanged(sender, eventArgs) {
                
                var radComboBox = <%=combo_kits.ClientID %>;  
               // alert(radComboBox);
               // radComboBox.SetValue("someValue"); 
                //ddl.selectedIndex = 0;

            }
            function combo_assetsOnClientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                var ddl = document.getElementById("<%=combo_assets.ClientID%>");
                //alert(ddl.v);

                //ddl.selectedIndex = 0;

            }
            function OnCommand(sender, args) {

                alert(args.get_commandName());
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }

            function validation()
            {
             var combojob = $find("<%= combo_job.ClientID %>"); 
                combojob.set_text("Select JobName");
             var comboasset = $find("<%= combo_assets.ClientID %>"); 
                comboasset.set_text("Select Assetname");
             var combokit = $find("<%= combo_kits.ClientID %>"); 
                combokit.set_text("Select Kitname");
                document.getElementById('<%=panel_Top1.ClientID %>').style.display = 'none';
                document.getElementById('<%=btn_genarate.ClientID %>').style.display = 'none';
                document.getElementById('<%=RadGrid_kits.ClientID %>').style.display = 'none';
                
                
            return false;
            }
            function docvalidation(sender, args)  
            {
                if(document.getElementById("<%=combo_job.ClientID%>").value=="")
                 {
                     radalert('Please Select Job', 330, 180, 'Alert Box', null, null);
                     args.set_cancel(true);
                     
                 }
               else if(document.getElementById("<%=combo_assets.ClientID%>").value=="" && document.getElementById("<%=combo_kits.ClientID%>").value=="")
                {
                    radalert('Please Select Asset (or) Kit', 330, 180, 'Alert Box', null, null);
                    args.set_cancel(true);
                }
         }
    </script>
    </telerik:RadScriptBlock>
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

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radgrdIntervalMeterData">
                <UpdatedControls>
                   <%-- <telerik:AjaxUpdatedControl ControlID="radgrdIntervalMeterData"></telerik:AjaxUpdatedControl>--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table>
                    <tr>
                         <td align="left" style="font-weight:bold">
                         
                            Select Job:<br />
                            <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid"  EmptyMessage="Select JobName" Width="200px"
                                 AutoPostBack="true" OnSelectedIndexChanged="combo_job_SelectedIndexChanged"></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                               SelectCommand="select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where status!='Closed' and
                                 (jid in(select jobid from PrismJobKits) or jid in (select jobid from PrismJobConsumables))"></asp:SqlDataSource>
                         </td>
                        <td>Select Kit:<br />
                            <telerik:RadComboBox runat="server" ID="combo_kits" OnClientSelectedIndexChanged="combo_kitsOnClientSelectedIndexChanged" 
                                 DataSourceID="SqlGetKits" DataTextField="kitname" DataValueField="Kitid"  EmptyMessage="Select Kitname" Width="200px"
                                ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetKits" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" ></asp:SqlDataSource>
                        
                        </td>
                        <td>(OR)</td>
                        <td>Select Asset:<br />
                            <telerik:RadComboBox runat="server" ID="combo_assets" OnClientSelectedIndexChanged="combo_assetsOnClientSelectedIndexChanged" 
                                 DataSourceID="SqlGetAssets" DataTextField="AssetName" DataValueField="AID"  EmptyMessage="Select Assetname" Width="200px"
                                ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                            
                            ></asp:SqlDataSource>
                        
                        </td>
                        <td>
                            <br />
                            <telerik:RadButton ID="btn_show" Text="Show Shipping Details" runat="server" OnClientClicking="docvalidation"  OnClick="btn_show_Click"/>
                        </td>
                         <td>
                             <br />
                            <asp:Button ID="btn_Reset" Text="Reset" runat="server" BackColor="Red" OnClientClick="javascript:return validation();"  OnClick="btn_Reset_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
            
        </tr>
        <tr>
            <td style="line-height:10px"></td>
        </tr>
        
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td  align="center">
                            <asp:Panel ID="panel_Top1" runat="server"></asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td style="line-height:10px"></td>
        </tr>
        <tr>
            <td  align="center">
                <table>
                    <%--<tr>
                        <td  align="center">
                            <asp:Panel ID="panel_Top2" runat="server"></asp:Panel>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="RadGrid_kits" runat="server" CellSpacing="0" 
                                            GridLines="None" AutoGenerateColumns="true" ShowHeader="true" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
                                            <MasterTableView >                   
                                                <Columns>
                                     
                                                
                         
                                            </Columns>
                                            </MasterTableView>     
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center"><asp:Button ID="btn_genarate" Text="Generate Shipping Details" runat="server"  OnClick="btn_genarate_Click" /></td>
        </tr>
         <tr>
            <td style="line-height:10px"></td>
        </tr>
        <tr>
            <td  align="center">
                <table>
                    <tr>
                        <td  align="center">
                            <asp:Panel ID="panel_Top3" runat="server"></asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_show" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:PostBackTrigger ControlID="btn_genarate" />
            </Triggers>
            </asp:UpdatePanel>
</asp:Content>

