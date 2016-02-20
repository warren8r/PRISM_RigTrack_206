<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CommandCentralDashboard.aspx.cs" Inherits="Modules_Reports_CommandCentralDashboard" %>

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
        </script>
    </telerik:RadScriptBlock>
    <div id="ZoneID1">
      <asp:UpdatePanel runat="server" ID="updPnl1" ChildrenAsTriggers="true" UpdateMode="Always">
            <ContentTemplate>  
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="grid_rig">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="grid_rig"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>    
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
<telerik:RadGrid ID="grid_rig" runat="server" CellSpacing="0" GridLines="None" OnItemCommand="grid_rig_ItemCommand" ShowGroupPanel="True" AutoGenerateColumns="False" AllowPaging="True" 
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
                      
                         <Columns> 
                         <telerik:GridBoundColumn DataField="programManagerId" Visible="false"></telerik:GridBoundColumn>                                                  
                         <telerik:GridBoundColumn DataField="jid" Visible="false"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn  DataField="jobname" SortExpression="jobname" HeaderText="jobname"  UniqueName="jobname"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn   HeaderText="Projectmanager"  DataField="Projectmanager" SortExpression="Projectmanager" UniqueName="Projectmanager"></telerik:GridBoundColumn>                         
                       <telerik:GridBoundColumn  DataField="startdate" SortExpression="startdate" HeaderText="startdate"  UniqueName="startdate"></telerik:GridBoundColumn>
                       <telerik:GridBoundColumn  DataField="enddate" SortExpression="enddate" HeaderText="enddate"  UniqueName="enddate"></telerik:GridBoundColumn>
                       <telerik:GridBoundColumn  DataField="Rigname" SortExpression="Rigname" HeaderText="Rigname"  UniqueName="Rigname"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn   HeaderText="Equipment&#160;Needed"   FilterControlAltText="Filter Equipment column"
                            UniqueName="Equipment">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Equipment" runat="server" />
                            </ItemTemplate>                             
                        </telerik:GridTemplateColumn> 
                         <telerik:GridTemplateColumn   HeaderText="Activity"   FilterControlAltText="Filter Activity column"
                            UniqueName="Activity">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Activity" runat="server" />
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
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="customCss">
    <style type="text/css">
        .style1
        {
            text-decoration: underline;
        }
    </style>
</asp:Content>


