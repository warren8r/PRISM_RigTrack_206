<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="OpsManagerDashBoard.aspx.cs" Inherits="Modules_Configuration_Manager_OpsManagerDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function GridCreated(sender, args) {
            var scrollArea = sender.GridDataDiv;
            var dataHeight = sender.get_masterTableView().get_element().clientHeight; if (dataHeight < 350) {
                scrollArea.style.height = dataHeight + 17 + "px";
            }
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
    <telerik:RadScriptBlock ID="radsc" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadScriptBlock>
   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                   <table>
                       <tr>
                           <td align="left">
                               <b>Select Job:</b><br />
                                <telerik:RadComboBox ID="radcombo_job" runat="server"  DataSourceID="SqlGetJobname" Width="300px" 
                                                        EmptyMessage="Select Job" DataTextField="WorkOrder"  
                                                            DataValueField="WorkOrder"   ></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlGetJobname" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                                    SelectCommand="select * from tbljob"></asp:SqlDataSource>
                           </td>
                           <td align="left">
                               <br />
                                <asp:Button ID="btn_view" runat="server"  Text="View" onclick="btn_view_Click"/>
                            </td>
                       </tr>
                   </table>           
                </td>
                
            </tr>
           <tr>
                <td align="center" >
                    <table >
                        <tr>
                             <td>
                                <telerik:RadGrid ID="RadGrid1"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="false"
                                  AllowPaging="true" AllowSorting="true"   Width="100%" OnItemDataBound="RadGrid1_ItemDataBound"
                               >
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                    <ClientEvents OnGridCreated="GridCreated" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"  FrozenColumnsCount="2"></Scrolling>
                                </ClientSettings>
                                <MasterTableView  CommandItemDisplay="Top">
                                     <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                                    ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                                                                    ShowExportToPdfButton="true"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn  HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_District" runat="server" Text='<%# Bind("Location") %>' />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn> 
                                        <telerik:GridTemplateColumn  HeaderText="Start Date" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_StartDate" runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Job Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_JobType" runat="server"  Text='<%# Bind("[Job Type]") %>' />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Coordinator">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Coordinator" runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="DD Hands">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="border-bottom:solid 1px #000000">
                                                            <asp:Label ID="lbl_DDHandsDD1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_DDHandsDD2" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Rig">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Rig" runat="server" Text='<%# Bind("CP_TypeRig") %>' />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Job #">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_workorder" Text='<%# Bind("WorkOrder") %>' runat="server" />
                                                <asp:Label ID="lbl_Jobno" Visible="false" Text='<%# Bind("[Job ID]") %>' runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="EquipmentNeeded">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EquipmentNeeded" runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Run #">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_RunNo" runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="HoleDepth">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_HoleDepth" runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="24 Hrs Ft Drilled">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_HrsFtDrilled" runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Activity">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Activity" runat="server" />
                                                
                                            </ItemTemplate>                             
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                               
                            </td>
                        </tr>
                        
                    </table>
                </td>
           </tr>
            
        </table>
        </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

