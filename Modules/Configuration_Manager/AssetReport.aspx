<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="AssetReport.aspx.cs" Inherits="Modules_Configuration_Manager_AssetReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadScriptBlock ID="radsc" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function openNotes(AssetRid) {
                // alert(AssetRid);
                var status = "NA";
                var url = "../../Modules/Configuration_Manager/MainteneceNotes.aspx?Assetid=" + AssetRid + "&status=" + status + "";
                // alert(url);
                document.getElementById('<%=iframe2.ClientID %>').src = url;
                window.radopen(null, "RadWindow2");
                return false;
            }
            //openNotescomp
            function openNotescomp(Componentid) {
                // alert(AssetRid);
                var status = "NA";
                var url = "../../Modules/Configuration_Manager/ComponentsNotes.aspx?Componentid=" + Componentid + "&status=" + status + "";
                // alert(url);
                document.getElementById('<%=iframe3.ClientID %>').src = url;
                window.radopen(null, "RadWindow3");
                return false;
            }

            function validationfortype() {

                if (document.getElementById("<%=radcombo_type.ClientID%>").value == "Select") {
                    radalert('Please Select Type Asset Or Component', 330, 180, 'Alert Box', null, null);
                    return false;

                }
                else {
                    if (document.getElementById("<%=txt_sno.ClientID%>").value == "") {
                        radalert('Enter Serial Number', 330, 180, 'Alert Box', null, null);
                        return false;
                    }
                }

            }
            function validationforcompcat() {
                if (document.getElementById("<%=radcombo_componentcat.ClientID%>").value == "Select" || document.getElementById("<%=radcombo_componentcat.ClientID%>").value == "") {
                    radalert('Select Component Category', 330, 180, 'Alert Box', null, null);
                    return false;
                }
            }
            function validationforconumablecat() {
                if (document.getElementById("<%=radcombo_consumables.ClientID%>").value == "Select" || document.getElementById("<%=radcombo_consumables.ClientID%>").value == "") {
                    radalert('Select Consumable Category', 330, 180, 'Alert Box', null, null);
                    return false;
                }
            }
            function validationforassetcat() {
                if (document.getElementById("<%=radcombo_assetcat.ClientID%>").value == "Select" || document.getElementById("<%=radcombo_assetcat.ClientID%>").value == "") {
                    radalert('Select Asset Category', 330, 180, 'Alert Box', null, null);
                    return false;
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxLoadingPanel ID="loading" runat="server" />
 <asp:UpdatePanel runat="server" ID="updPnl1"  UpdateMode="Always">
    <ContentTemplate>  
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                            
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2"></telerik:AjaxUpdatedControl>
                            
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td align="left">
                            Start Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left">
                            End Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_end" Width="130px">
                                <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput2" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
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
                        <td>
                            <table style="border:solid 1px #000000">
                                <tr>
                                    <td align="left">
                                        Select Consumable Category:<br />
                                        <telerik:RadComboBox ID="radcombo_consumables" DataSourceID="SqlDataSource_con" Width="250px" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" EmptyMessage="Select" runat="server" DataTextField="ConCatName" DataValueField="ConCatID">

                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource_con"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                            SelectCommand="select * from ConsumableCategory" >
                                
                                        </asp:SqlDataSource>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btn_conview" runat="server" Text="View" OnClientClick="javascript:return validationforconumablecat();"   OnClick="btn_conview_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border:solid 1px #000000">
                                <tr>
                                    <td align="left">
                                        Select Component Category:<br />
                                        <telerik:RadComboBox ID="radcombo_componentcat" DataSourceID="SqlDataSource_component" Width="250px" 
                                        CheckBoxes="true" EnableCheckAllItemsCheckBox="true" EmptyMessage="Select" runat="server" 
                                        DataTextField="comp_categoryname" DataValueField="comp_categoryid">

                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource_component"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                            SelectCommand="select * from Prism_ComponentCategory" >
                                
                                        </asp:SqlDataSource>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btn_viewforcomponents" runat="server" Text="View" OnClientClick="javascript:return validationforcompcat();"  OnClick="btn_viewforcomponents_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <table style="border:solid 1px #000000">
                                            <tr>
                                                <td>
                                                    Select Type:<br />
                                                    <telerik:RadComboBox ID="radcombo_type" runat="server">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Select" Value="Select" />
                                                            <telerik:RadComboBoxItem Text="Asset" Value="Asset" />
                                                            <telerik:RadComboBoxItem Text="Component" Value="Component" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td align="left">
                                                    Enter Serial Number:<br />
                                                    <asp:TextBox ID="txt_sno" runat="server"></asp:TextBox>
                                                    <%--<telerik:RadComboBox ID="radcombo_serialno" DataSourceID="SqlDataSource_sno" Width="250px" 
                                                    CheckBoxes="true" EnableCheckAllItemsCheckBox="true" EmptyMessage="Select" runat="server" 
                                                    DataTextField="SerialNumber" DataValueField="Id">

                                                    </telerik:RadComboBox>
                                                    <asp:SqlDataSource ID="SqlDataSource_sno"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                        SelectCommand="select * from Prism_Assets" >
                                
                                                    </asp:SqlDataSource>--%>
                                                </td>
                                                <td>
                                                    <br />
                                                    <asp:Button ID="btn_viewsrno" runat="server" Text="View" OnClientClick="javascript:return validationfortype();" OnClick="btn_viewsrno_OnClick" />
                                                </td>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="bottom">
                                        &nbsp;&nbsp;<asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellspacing="5">
                    <tr>
                        
                    </tr>
                </table>
            </td>

        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid1"  runat="server" ShowGroupPanel="True" GridLines="None" 
                            OnItemCommand="RadGrid1_ItemCommand" ShowFooter="true"
                             OnItemDataBound="RadGrid1_ItemDataBound" OnGroupsChanging="RadGrid1_GroupsChanging" Skin="Web20">
                                <MasterTableView  AutoGenerateColumns="false" GroupLoadMode="Server"  CommandItemDisplay="Top" ShowGroupFooter="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                        ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                                        ShowExportToPdfButton="true"></CommandItemSettings>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Category" FieldName="clientAssetName"></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="clientAssetName"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                                        
                                    </GroupByExpressions>
                                    <GroupByExpressions>  
                                       <telerik:GridGroupByExpression>  
                                         <SelectFields>  
                                          <telerik:GridGroupByField FieldName="AssetName" HeaderText=""/>   
                                         </SelectFields>  
                                         <GroupByFields>  
                                              <telerik:GridGroupByField FieldName="AssetName" />  
                                         </GroupByFields>  
                                       </telerik:GridGroupByExpression>  
                                   </GroupByExpressions> 
                                   <GroupByExpressions>  
                                       <telerik:GridGroupByExpression>  
                                         <SelectFields>  
                                          <telerik:GridGroupByField FieldName="AssetRid" HeaderText=""/>   
                                         </SelectFields>  
                                         <GroupByFields>  
                                              <telerik:GridGroupByField FieldName="AssetRid" />  
                                         </GroupByFields>  
                                       </telerik:GridGroupByExpression>  
                                   </GroupByExpressions>
                                    
                                <Columns>
                                                          
                                    <telerik:GridTemplateColumn HeaderText="Asset Category">
                                        
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lbl_category" runat="server" Text='<%# Eval("clientAssetName") %>' />
                                            
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Asset Name" DataField="AssetName" UniqueName="AssetName">
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Serial Number" DataField="SerialNumber" UniqueName="SerialNumber">
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="repairdate" 
                                    HeaderText="Maintenance Start Date Time" SortExpression="repairdate" UniqueName="repairdate">
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridDateTimeColumn>
                                <%--DataFormatString="{0:MM/dd/yyyy}"--%>
                                    <telerik:GridDateTimeColumn DataField="repairfixdate" 
                                    HeaderText="Maintenance Finished Date Time" SortExpression="repairfixdate" UniqueName="repairfixdate">
                                        <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridDateTimeColumn>
                                    <telerik:GridTemplateColumn HeaderText="Consumable Category">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_concategory" runat="server" Text='<%# Eval("ConCatName") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Consumable Name" DataField="ConName" UniqueName="ConName">
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Consumable Maintenence Cost($)" DataField="ConCost" DataFormatString="{0:###,###.00}"  UniqueName="ConCost">
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Consumable Qty" DataField="qty" UniqueName="qty" FooterText="GrandTotal($):">
                                        <FooterStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn dataFormatString="{0:###,##0.00}" DataField="Total" DataType="System.Decimal" NumericType="Currency" HeaderText="Total Maintenence Cost($)" 
                                    SortExpression="Total" UniqueName="Total" Aggregate="Sum" FooterText="GrandTotal" 
                                    FooterAggregateFormatString="{0:C}"></telerik:GridNumericColumn>
                                    <%--<telerik:GridBoundColumn HeaderText="Total Cost($)" Aggregate="Sum" DataField="Total" DataFormatString="{0:###,###.00}"   FooterText="Total:" UniqueName="Total">
                                        
                                    </telerik:GridBoundColumn>--%>

                                        <%--<telerik:GridNumericColumn Aggregate="Sum" DataField="Total" HeaderText="Total Cost($)" DataFormatString="{0:###,###.00}" FooterText="Total: " UniqueName="GrandTotal"/>--%>
                                        <%--<telerik:GridBoundColumn HeaderText="Notes" DataField="Notes" UniqueName="Notes">
                                        
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn HeaderText="View Notes">
                                           <ItemTemplate >                              
                                            <%--<asp:TextBox ID="txt_notes" runat="server" Width="200px" Text='<%# Eval("Notes") %>'></asp:TextBox>--%>
                                               <asp:LinkButton ID="lnkbtn_notes" runat="server" Text="View Notes" OnClientClick='<%# "openNotes(\"" + Eval("AssetID" ) + "\" ); return false;" %>' />
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                                        
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="600px" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid2"  runat="server" ShowGroupPanel="True" GridLines="None" Skin="Web20"  
                            OnItemCommand="RadGrid2_ItemCommand" OnItemDataBound="RadGrid2_ItemDataBound" Height="600px" ShowFooter="true" OnGroupsChanging="RadGrid2_GroupsChanging">
                                <MasterTableView  AutoGenerateColumns="false" GroupLoadMode="Server" 
                                CommandItemDisplay="Top"  ShowGroupFooter="true" >
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                        ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                                        ShowExportToPdfButton="true"></CommandItemSettings>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="ConsumableCategory" FieldName="ConCatName"></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ConCatName"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                                        
                                    </GroupByExpressions>
                                    <GroupByExpressions>  
                                       <telerik:GridGroupByExpression>  
                                         <SelectFields>  
                                          <telerik:GridGroupByField FieldName="ConName" HeaderText="Consumable Name"/>   
                                         </SelectFields>  
                                         <GroupByFields>  
                                              <telerik:GridGroupByField FieldName="ConName" />  
                                         </GroupByFields>  
                                       </telerik:GridGroupByExpression>  
                                   </GroupByExpressions> 
                                   <GroupByExpressions>  
                                       <telerik:GridGroupByExpression>  
                                         <SelectFields>  
                                          <telerik:GridGroupByField FieldName="AssetRid" HeaderText=""/>   
                                         </SelectFields>  
                                         <GroupByFields>  
                                              <telerik:GridGroupByField FieldName="AssetRid" />  
                                         </GroupByFields>  
                                       </telerik:GridGroupByExpression>  
                                   </GroupByExpressions>
                                <Columns>
                                                          
                                    <telerik:GridTemplateColumn HeaderText="Asset Category">
                                        
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lbl_category" runat="server" Text='<%# Eval("clientAssetName") %>' />
                                            
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Asset Name" DataField="AssetName" UniqueName="AssetName">
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Serial Number" DataField="SerialNumber" UniqueName="SerialNumber">
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="repairdate" 
                                        HeaderText="Maintenance Start Date Time" SortExpression="repairdate" UniqueName="repairdate">
                                        <ItemStyle Width="80px" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="repairfixdate" 
                                        HeaderText="Maintenance Stop Date Time" SortExpression="repairfixdate" UniqueName="repairfixdate">
                                            <ItemStyle Width="80px" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridTemplateColumn HeaderText="Consumable Category">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_concategory" runat="server" Text='<%# Eval("ConCatName") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Consumable Name" DataField="ConName" UniqueName="ConName">
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Consumable Maintenece Cost($)" DataField="ConCost" DataFormatString="{0:###,###.00}"  UniqueName="ConCost">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Consumable Qty" DataField="qty" FooterText="GrandTotal:" UniqueName="qty">
                                        <HeaderStyle Width="80px" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn HeaderText="Total Cost($)" Aggregate="Sum" DataField="Total" DataFormatString="{0:###,###.00}" FooterText="Grand&#160;Total:" UniqueName="Total">
                                        
                                    </telerik:GridBoundColumn>--%>
                                    <%--<telerik:GridBoundColumn Aggregate="Sum" DataField="Total" HeaderText="Total Cost($)" FooterText="Total: " UniqueName="Total" />--%>
                                    <telerik:GridNumericColumn dataFormatString="{0:###,##0.00}" DataField="Total" DataType="System.Decimal" NumericType="Currency" HeaderText="Total Maintenece Cost($)" 
                                    SortExpression="Total" UniqueName="Total" Aggregate="Sum" FooterText="GrandTotal" 
                                    FooterAggregateFormatString="{0:C}">
                                    <HeaderStyle Width="140px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridTemplateColumn HeaderText="View Notes">
                                           <ItemTemplate >                              
                                            <%--<asp:TextBox ID="txt_notes" runat="server" Width="200px" Text='<%# Eval("Notes") %>'></asp:TextBox>--%>
                                               <asp:LinkButton ID="lnkbtn_notes" runat="server" Text="View Notes" OnClientClick='<%# "openNotes(\"" + Eval("AssetID" ) + "\" ); return false;" %>' />
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                                        
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid3"  runat="server" ShowGroupPanel="True" GridLines="None" 
                             ShowFooter="true" OnItemCommand="RadGrid3_ItemCommand"
                             OnItemDataBound="RadGrid3_ItemDataBound" OnGroupsChanging="RadGrid3_GroupsChanging" Skin="Web20">
                                <MasterTableView  AutoGenerateColumns="false" GroupLoadMode="Server"  CommandItemDisplay="Top" 
                                ShowGroupFooter="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                        ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                                        ShowExportToPdfButton="true"></CommandItemSettings>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Category" FieldName="comp_categoryname"></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="comp_categoryname"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                                        
                                    </GroupByExpressions>
                                    <GroupByExpressions>  
                                       <telerik:GridGroupByExpression>  
                                         <SelectFields>  
                                          <telerik:GridGroupByField FieldName="ComponentName" HeaderText=""/>   
                                         </SelectFields>  
                                         <GroupByFields>  
                                              <telerik:GridGroupByField FieldName="ComponentName" />  
                                         </GroupByFields>  
                                       </telerik:GridGroupByExpression>  
                                   </GroupByExpressions> 
                                    
                                <Columns>
                                                          
                                    <telerik:GridTemplateColumn HeaderText="Component Category">
                                        <HeaderStyle Width="200px" />
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lbl_category" runat="server" Text='<%# Eval("comp_categoryname") %>' />
                                            
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Component Name" DataField="ComponentName" UniqueName="ComponentName">
                                        <HeaderStyle Width="120px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Serial Number" DataField="serialno" UniqueName="SerialNumber">
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="repairdate" 
                                    HeaderText="Maintenance Start Date Time" SortExpression="repairdate" UniqueName="repairdate">
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="repairfixdate" 
                                    HeaderText="Maintenance Stop Date Time" SortExpression="repairfixdate" FooterText="GrandTotal" UniqueName="repairfixdate">
                                        <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridDateTimeColumn>
                                    
                                    <telerik:GridNumericColumn dataFormatString="{0:###,##0.00}" DataField="Total" DataType="System.Decimal" NumericType="Currency" HeaderText="Total Maintenence Cost($)" 
                                    SortExpression="Total" UniqueName="Total" Aggregate="Sum" FooterText="GrandTotal" 
                                    FooterAggregateFormatString="{0:C}">
                                    <HeaderStyle Width="120px" />
                                    </telerik:GridNumericColumn>
                                    <%--<telerik:GridBoundColumn HeaderText="Total Cost($)" Aggregate="Sum" DataField="Total" DataFormatString="{0:###,###.00}"   FooterText="Total:" UniqueName="Total">
                                        
                                    </telerik:GridBoundColumn>--%>

                                        <%--<telerik:GridNumericColumn Aggregate="Sum" DataField="Total" HeaderText="Total Cost($)" DataFormatString="{0:###,###.00}" FooterText="Total: " UniqueName="GrandTotal"/>--%>
                                        <telerik:GridTemplateColumn HeaderText="View Notes">
                                        <HeaderStyle Width="100px" />
                                           <ItemTemplate >                              
                                            <%--<asp:TextBox ID="txt_notes" runat="server" Width="200px" Text='<%# Eval("Notes") %>'></asp:TextBox>--%>
                                               <asp:LinkButton ID="lnkbtn_notes" runat="server" Text="View Notes" OnClientClick='<%# "openNotescomp(\"" + Eval("MainComponentId" ) + "\" ); return false;" %>' />
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                                        
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="600px" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadWindowManager ID="radwin" runat="server">
            <Windows>
                                 
                                 <telerik:RadWindow ID="RadWindow2" runat="server"  Modal="true" Width="650px" Height="600px">
                                    <ContentTemplate>
                                        <table>
                                             <%--<tr><td style="color:blue;font-weight:bold;cursor:default" align="center"> <asp:Button ID="Button2" runat="server" Text="Close" BackColor="Blue"  onclick="btn_view_Click" /> </td></tr>--%>
                                            <tr>
                                                <td>  <iframe id="iframe2" runat="server" width="650px" height="600px"  ></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                      
                                     </ContentTemplate>
                                 </telerik:RadWindow>
                                 <telerik:RadWindow ID="RadWindow3" runat="server"  Modal="true" Width="650px" Height="600px">
                                    <ContentTemplate>
                                        <table>
                                             <%--<tr><td style="color:blue;font-weight:bold;cursor:default" align="center"> <asp:Button ID="Button2" runat="server" Text="Close" BackColor="Blue"  onclick="btn_view_Click" /> </td></tr>--%>
                                            <tr>
                                                <td>  <iframe id="iframe3" runat="server" width="650px" height="600px"  ></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                      
                                     </ContentTemplate>
                                 </telerik:RadWindow>
                              </Windows>
        </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

