<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssetRptPopup.aspx.cs" Inherits="Modules_Configuration_Manager_AssetRptPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet/less" type="text/css" href="../../css/mdm.less" />
    <link rel="stylesheet/less" type="text/css" href="../../css/jquery-ui-1.10.3.custom.css" />
    <telerik:RadCodeBlock runat="server" ID="rdbScripts">
    <script type="text/javascript">

        function openNotes(AssetRid) {
            // alert(AssetRid);
            var status = "NA";
            var url = "../../Modules/Configuration_Manager/MainteneceNotes.aspx?Assetid=" + AssetRid + "&status=" + status + "";
            // alert(url);
            document.getElementById('<%=iframe2.ClientID %>').src = url;
            window.radopen(null, "RadWindow2");
            return false;
        }
    </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rad1" runat="server"></telerik:RadScriptManager>
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
    <div>
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
    </div>


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
                                 
                              </Windows>
        </telerik:RadWindowManager>
        </ContentTemplate>
           
            </asp:UpdatePanel>
    </form>
</body>
</html>
