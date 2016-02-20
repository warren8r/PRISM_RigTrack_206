<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateEditAssetKit.aspx.cs" Inherits="Modules_Configuration_Manager_CreateEditAssetKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        function validate() {
            
            if (document.getElementById('<%=txt_kitname.ClientID %>').value == "") {
                document.getElementById('<%=lbl_message.ClientID %>').innerHTML = "Enter Kit Name";
                document.getElementById('<%=txt_kitname.ClientID %>').focus();
                return false;
            }
            
        }
        //function openwin() {

        //    alert("fff");
        //    window.radopen(null, "window_department");

        //}
        function openRadWin() {
          
            window.radopen(null, "window_department");
            return false;
        }

        function OpenWindow() {
            Sys.Application.add_load(ow);
        }

        function ow() {
            var oWnd = radopen(null, 'window_department');
            Sys.Application.remove_load(ow);
        }
    </script>
    </telerik:RadCodeBlock>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" valign="top" style="padding-left:20px">
                <fieldset>
                <legend style="text-align:left">Create/Update Asset Kits</legend>
                <table style="text-align:left;">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lbl_message" ForeColor="Red" runat="server"></asp:Label>
                            <asp:Label ID="lbl_assetkitidupdate" runat="server" style="display:none"  ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Kit Name:<br />
                            <asp:TextBox ID="txt_kitname"  runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Description:<br />
                            <asp:TextBox ID="txt_kitdesc" runat="server"  Height="35px" Width="250px"></asp:TextBox>
                        </td>
                        
                        
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td valign="top">
                                        Asset Names and Categories:<br />
                                        <telerik:RadListBox ID="radcombo_assetnames"   EmptyMessage="- Select -" runat="server" Width="320px" Height="250px" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" 
                                        DataSourceID="SqlGetAssetname" DataTextField="nameandcategory" DataValueField="Id"></telerik:RadListBox>
                                        <asp:SqlDataSource ID="SqlGetAssetname"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                        SelectCommand="select Id,(AssetName+' ('+clientAssetName+')') as nameandcategory from PrismAssetName a,clientAssets cat where a.AssetCategoryId=cat.clientAssetID order by cat.clientAssetID asc"></asp:SqlDataSource>
                                        <br />
                                        <asp:Button ID="btn_adddet" runat="server" Text="Add details to selected assets" OnClick="btn_adddet_OnClick" />
                                    </td>
                                    <td valign="top">
                                        <asp:Panel ID="pnl_dynamic" runat="server"></asp:Panel>
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>--%>
                    <tr><td colspan="2" style="height:20px"></td></tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnl_dynamic" Visible="false" runat="server"></asp:Panel>
                            <asp:SqlDataSource ID="SqlDataSource2"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                            SelectCommand="select Id,AssetName from PrismAssetName"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--<div style='overflow:scroll;height:400px;width:400px'>--%>
                            <table>
                                <tr>
                                    <td>
                                        
                                        <telerik:RadGrid ID="RadGrid2" DataSourceID="SqlDataSource3" runat="server" PageSize="15"
                                                AllowSorting="True" AllowPaging="True" ShowGroupPanel="True" Skin="Web20" 
                                                AutoGenerateColumns="False" Width="600px"  
                                                GridLines="None" OnGroupsChanging="RadGrid2_GroupsChanging">
                                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                                <MasterTableView Width="600px" GroupLoadMode="Server">
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
                                                    
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_assets" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn SortExpression="AssetName"   HeaderText="Asset Name" HeaderButtonType="TextButton"
                                                            DataField="AssetName">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_qty" runat="server" Width="100px" />
                                                                <asp:Label runat="server" ID="lbl_assetid" Text='<%# Eval("Id") %>' style="display:none;"></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                              <ClientSettings>  
        <Scrolling AllowScroll="True"  UseStaticHeaders="true" SaveScrollPosition="True"></Scrolling>  
    </ClientSettings>  

                                                
                                            </telerik:RadGrid>
                                        
 
                                            <asp:SqlDataSource ID="SqlDataSource3" ConnectionString="<%$ databaseExpression:client_database %>"
                                                ProviderName="System.Data.SqlClient" SelectCommand="select * from PrismAssetName a,clientAssets cat where a.AssetCategoryId=cat.clientAssetID"
                                                runat="server"></asp:SqlDataSource>
                                    </td>

                                </tr>
                            </table>
                            <%--</div>--%>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <telerik:RadComboBox ID="RadListBox1"  DataSourceID="SqlDataSource2" EmptyMessage="Select Components"  Width="500px" Height="150px" runat="server" >
                                
                                
                                <ItemTemplate>
                                    <table width="500px" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:CheckBox ID="chk_1" runat="server" />
                                            </td>
                                            <td  align="left" style="width:150px">
                                                <asp:Label ID="Label1" runat="server" Text= '<%# Eval("AssetName") %>'></asp:Label>
                                            </td>
                                            <td  align="left">
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                
                                
                               </ItemTemplate>
                                
                                 
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlDataSource2"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                            SelectCommand="select Id,AssetName from PrismAssetName"></asp:SqlDataSource>
                        </td>
                    </tr>--%>
                    <tr><td colspan="2" style="height:10px;"></td></tr>
                    <tr>
                        <td valign="top" align="center" colspan="2">
                            <table>

                                <tr>
                                    <td align="right" ><asp:Label ID="lbl_messageforassigned" runat="server"></asp:Label><br />
                           </td>
                                </tr>
                                <tr>
                                <td align="center">
                                <asp:Button ID="btn_save" runat="server" OnClientClick="javascript:return validate();" Text="Save" OnClick="btn_save_OnClick" />
                                &#160;
                                <asp:Button ID="btn_saveas" runat="server" OnClientClick="openRadWin(); return false;" Text="Save As" Visible="false" />
                                &#160;
                                <asp:Button ID="btn_reset" runat="server" Text="Clear" OnClick="btn_reset_Click" />
                                </td>
                                   
                                </tr> 
                                
                            </table>
                            
                        </td>
                       
                    </tr>
                </table>
                </fieldset>
            </td>
            <td align="center" valign="top">
                <table>
                    <tr><td align="left"><b><u>Existing Asset Kits</u></b></td></tr>
                    <tr><td style="height:10px"></td></tr>
                    <tr>
                        <td align="center">
                            <telerik:RadGrid ID="radgrid_manageassetkits" runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                                AllowPaging="true" AllowSorting="true" DataSourceID="SqlDataSource1" OnItemDataBound="radgrid_manageassetkits_ItemDataBound">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView   DataSourceID="SqlDataSource1">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                 <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="View/Edit" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_viewedit" runat="server" OnClick="lnk_viewedit_Click"  Text="View/Edit"></asp:LinkButton>
                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="kitname"
                                            HeaderText="Kit Name" SortExpression="kitname" UniqueName="kitname" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="kitdesc"
                                            HeaderText="Description" SortExpression="kitdesc" UniqueName="kitdesc" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Asset Names" >
                                            <ItemTemplate>
                                                <telerik:RadToolTip ID="rttChooseFrom" runat="server" Animation="None" ContentScrolling="Auto" 
                                                    Height="300px" HideEvent="ManualClose" Modal="true" RelativeTo="Element" 
                                                    ShowEvent="OnClick" TargetControlID="lnk_assetnames" Title="" 
                                                    Width="400px">  
                                                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">  
                                                        <telerik:RadGrid ID="radgridkitassets" runat="server" ShowHeader="true" AutoGenerateColumns="true">

                                                        </telerik:RadGrid>
                                                        
                                                    </telerik:RadAjaxPanel> 
                                                </telerik:RadToolTip>
                                                
                                                
                                                <asp:LinkButton ID="lnk_assetnames" runat="server" OnClientClick="return false;" Text="View Assets "></asp:LinkButton>
                                                <asp:Label ID="lbl_assetkitid" runat="server" style="display:none" Text='<%# Bind("assetkitid") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                    </Columns>
                            </MasterTableView>
                            </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource1"   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                            SelectCommand="select distinct assetkitid,kitname,kitdesc,createddate from PrismAssetKitDetails order by assetkitid desc"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                 <telerik:RadWindowManager ID="RadWindowManager1" runat="server"   > 
                                                     <Windows> 
                                                     <telerik:RadWindow ID="window_department" runat="server"  
                                                          Width="500px"  height="300px" Title="Create New Kit">
 
                                                        <ContentTemplate>
 
                                                           <table>
                                                               <tr>
                                                                   <td colspan="2" align="center">
                                                                       <asp:Label ID="lblmessagetop" runat="server"></asp:Label>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                    <td valign="top">
                                                                        Kit Name:<br />
                                                                        <asp:TextBox ID="txt_topkitname"  runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        Description:<br />
                                                                        <asp:TextBox ID="txt_topdesc" runat="server"  Height="35px" Width="250px"></asp:TextBox>
                                                                    </td>
                                                               </tr>
                                                               <tr>
                                                                   <td align="center">
                                                                       <asp:Button ID="btn_savenew" runat="server" Text="Save" OnClick="btn_savenew_Click" />
                                                                   </td>
                                                               </tr>
                                                           </table>
                                               
                                                            </iframe>
 
                                                         </ContentTemplate>
 
                                                     </telerik:RadWindow>
                                                         </Windows>
                                  </telerik:RadWindowManager>
            </td>
        </tr>
        
    </table>
    <%--<telerik:RadToolTipManager ID="RadToolTipManager1" AutoCloseDelay="5000" Position="MiddleRight" RelativeTo="Element" runat="server" AutoTooltipify="true" BackColor="BlueViolet" Skin="Web20">
</telerik:RadToolTipManager>--%>
</ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

