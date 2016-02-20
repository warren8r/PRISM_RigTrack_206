<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="JobAssetsPrint.aspx.cs" Inherits="Modules_Configuration_Manager_JobAssignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
    function onRequestStart(sender, args) {
        if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
            args.set_enableAjax(false);
        }
    }
    //]]>
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
        </ContentTemplate>
        </asp:UpdatePanel>
<telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="grdJobList">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="grdJobList"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
    <td align="center"><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
</tr>
    <tr>
     <td align="center">
        <table>
                <tr>
                    <td align="left">Select Job<br />
                           <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" DataTextField="jobname" DataValueField="jid" Width="200px" ></telerik:RadComboBox>
                        <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                         SelectCommand="select 0 as jid,'Select JobName' as jobname union select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname
                          from manageJobOrders where status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) and jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''"></asp:SqlDataSource>
                    </td>
                   <td  align="left">Select Assets<br />
                        <telerik:RadComboBox runat="server" Width="300px" ID="combo_assets" CheckBoxes="true" DataSourceID="SqlGetAssets" EmptyMessage="Please Select Asset(s)" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                    <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                        SelectCommand="select Distinct P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA where P.id  in (select AssetId from PrismJobAssignedAssets)"></asp:SqlDataSource>
                    </td> 
                    <td align="left">
                    Status<br />
                         <telerik:RadDropDownList DataSourceID="SqlGetAssetStatus" runat="server" ID="ddl_status"  DataValueField="Id" DataTextField="StatusText"> </telerik:RadDropDownList>
                <asp:SqlDataSource ID="SqlGetAssetStatus" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                    SelectCommand="select Id,StatusText from PrsimJobAssetStatus union select 0 as Id, 'Select ALL' as StatusText"></asp:SqlDataSource>
                    </td>
                    <td valign="bottom">
                        <telerik:RadButton ID="btn_view" Text="View" runat="server" 
                            onclick="btn_view_Click"></telerik:RadButton>
                    </td>
                    <td>
                            <br />
                            <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                        </td>
                </tr>
            </table>
     </td>
    </tr>   
    <tr>
        <td align="center">
              <telerik:RadGrid ID="grdJobList" runat="server" CellSpacing="0" 
                 AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  OnSortCommand="grdJobList_SortCommand"
                 OnPageIndexChanged="grdJobList_PageIndexChanged" GridLines="None"    OnItemCommand="grdJobList_ItemCommand">
                  <%--OnItemDataBound="grdJobList_ItemDataBound"--%>
                <ExportSettings HideStructureColumns="true">
                        <Excel Format="Biff" />
                    </ExportSettings>
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>

                <MasterTableView DataKeyNames="jid" CommandItemStyle-HorizontalAlign="Left" CommandItemDisplay="Top"  EditMode="InPlace">
                   
                   <ItemStyle VerticalAlign="Top" />
                        <AlternatingItemStyle VerticalAlign="Top" />
            <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"  
                ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn DataField="jid" DataType="System.Int32" FilterControlAltText="Filter jid column"
                            ReadOnly="True" SortExpression="jid" UniqueName="IntervalMeterDataId" Display="false">
                        </telerik:GridBoundColumn>     
                        <telerik:GridTemplateColumn   HeaderText="Job Name"   FilterControlAltText="Filter jobname column"
                            SortExpression="jobname" UniqueName="jobname">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("jobname") %>'/>
                            </ItemTemplate>
                             
                            </telerik:GridTemplateColumn>  
                            <telerik:GridTemplateColumn   HeaderText="Job ID"   FilterControlAltText="Filter jobid column"
                            SortExpression="jobid" UniqueName="jobid">
                            <ItemTemplate>
                                <asp:Label ID="lbl_jobid" runat="server" Text= '<%# Eval("jobid") %>'/>
                            </ItemTemplate>                             
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn   HeaderText="Shipping&#160;Address"   FilterControlAltText="Filter clientAssetName column"
                    SortExpression="shipaddress" UniqueName="Address">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Address" runat="server" Text='<%# Eval("Address") %>'/>
                    </ItemTemplate>
                             
                    </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn DataField="AssetId" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                    ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="False">
                </telerik:GridBoundColumn>  
                               <telerik:GridTemplateColumn   HeaderText="Asset&#160;Category"   FilterControlAltText="Filter clientAssetName column"
                    SortExpression="clientAssetName" UniqueName="clientAssetName">
                    <ItemTemplate>
                        <asp:Label ID="lbl_clientAssetName" runat="server" Text='<%# Eval("clientAssetName") %>'/>
                    </ItemTemplate>
                             
                    </telerik:GridTemplateColumn>
                     
                <telerik:GridTemplateColumn HeaderText="AssetName">
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
                    <telerik:GridTemplateColumn   HeaderText="Asset&#160;Owner"   FilterControlAltText="Filter Warehouse column"
                    SortExpression="Warehouse" UniqueName="Warehouse">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Warehouse" runat="server" Text='<%# Eval("Warehouse") %>'/>
                        </ItemTemplate>                                
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;BY"   FilterControlAltText="Filter ModifiedBY column"
                    SortExpression="username" UniqueName="username">
                    <ItemTemplate>
                        <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("username") %>'/>
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn  HeaderText="Modified&#160;Date" DataFormatString="{0:MM/dd/yyyy}"  FilterControlAltText="Filter ModifiedDate column"
                    SortExpression="ModifiedDate" UniqueName="ModifiedDate" DataField="ModifiedDate"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn   HeaderText="Modified&#160;Date" Visible="false" FilterControlAltText="Filter ModifiedDate column"
                    SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ModifiedDate" runat="server" Text='<%# Eval("ModifiedDate") %>'/>
                    </ItemTemplate>                             
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn   HeaderText="Asset&#160;Status"   FilterControlAltText="Filter AssetStatus column"
                    SortExpression="AssetStatus" UniqueName="AssetStatus">
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
              
                 </MasterTableView>                 
            </telerik:RadGrid>
             
        </td>
    </tr>
    
</table>
</telerik:RadAjaxPanel>
</asp:Content>

