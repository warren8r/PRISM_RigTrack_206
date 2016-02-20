<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateEditToolGroup.aspx.cs" Inherits="Modules_Configuration_Manager_CreateEditToolGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader5" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


    <fieldset>
        <asp:UpdatePanel ID="up5" runat="server">
            <ContentTemplate>


                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Create/Update Tool Group</h2>
                        </asp:TableCell>
                    </asp:TableRow>


                </asp:Table>

                  <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                      <asp:TableRow>
                          <asp:TableCell Visible="false">
                                 
                                        <asp:Label ID="lbl_assetkitidupdate" runat="server" Style="display: none"></asp:Label>
                          </asp:TableCell>

                          <asp:TableCell>
                               <asp:Panel ID="pnl_dynamic" Visible="false" runat="server"></asp:Panel>
                                        <asp:SqlDataSource ID="SqlDataSource2" ConnectionString="<%$ databaseExpression:client_database %>" runat="server"
                                            SelectCommand="select Id,AssetName from PrismAssetName"></asp:SqlDataSource>
                          </asp:TableCell>
                      </asp:TableRow>
                  </asp:Table>



                <asp:Table ID="Table11" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table12" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						   Group Name
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						 Description
                                        
                                    </asp:TableHeaderCell>

                          
                                </asp:TableRow>


                                <asp:TableRow>


                                    <asp:TableCell>
                                        <asp:TextBox ID="txt_kitname" Width="250px"  runat="server"></asp:TextBox>

                                    </asp:TableCell>

                                    <asp:TableCell>

                                        <asp:TextBox ID="txt_kitdesc" runat="server" Width="250px"></asp:TextBox>
                                    </asp:TableCell>

                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>


                 <asp:Table ID="Table2" runat="server" Width="100%" HorizontalAlign="Center">

                       <asp:TableRow>

                           <asp:TableCell Width="50%"></asp:TableCell>

                         


                           <asp:TableCell>
                                 <asp:Button ID="btn_save" runat="server" OnClientClick="javascript:return validate();" Text="Save" OnClick="btn_save_OnClick" />
                           </asp:TableCell>

                           <asp:TableCell>
                                 <asp:Button ID="btn_saveas" runat="server" OnClientClick="openRadWin(); return false;" Text="Save As" Visible="false" />
                           </asp:TableCell>

                           <asp:TableCell>
                                <asp:Button ID="btn_reset" runat="server" Text="Clear" OnClick="btn_reset_Click" />
                           </asp:TableCell>

                              <asp:TableCell Width="50%"></asp:TableCell>

                       </asp:TableRow>

                   </asp:Table>


                <asp:Table ID="TBRadgrid2" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>


                        <asp:TableCell HorizontalAlign="Center">

                            <telerik:RadGrid ID="RadGrid2" DataSourceID="SqlDataSource3" runat="server" PageSize="15"
                                AllowSorting="True" AllowPaging="True" ShowGroupPanel="false" 
                                AutoGenerateColumns="False"  Width="70%"
                                GridLines="None" OnGroupsChanging="RadGrid2_GroupsChanging"
                                OnPageIndexChanged="RadGrid2_PageIndexChanged"
                                OnPreRender="RadGrid2_PreRender">
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                <MasterTableView GroupLoadMode="Server" DataKeyNames="ID">
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
                                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                                            HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_assets" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn SortExpression="AssetName" HeaderText="Tool Name" HeaderButtonType="TextButton"
                                            DataField="AssetName">
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="SerialNumber" HeaderText="Serial Number" HeaderButtonType="TextButton"
                                            DataField="SerialNumber">
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Quantity" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_qty" Text="1" runat="server" Width="100px" />
                                                <asp:Label runat="server" ID="lbl_assetid" Text='<%# Eval("Id") %>' Style="display: none;"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="false" UseStaticHeaders="true" SaveScrollPosition="True"></Scrolling>
                                </ClientSettings>


                            </telerik:RadGrid>

                              <asp:SqlDataSource ID="SqlDataSource3" ConnectionString="<%$ databaseExpression:client_database %>"
                                ProviderName="System.Data.SqlClient" SelectCommand="select c.ID,c.SerialNumber,a.AssetName,c.SerialNumber,* from PrismAssetName a,clientAssets cat,Prism_Assets c where a.AssetCategoryId=cat.clientAssetID and c.AssetName=a.Id"
                                runat="server"></asp:SqlDataSource>

                          
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                

                  


                <asp:Table ID="Table3" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>

                        <asp:TableCell HorizontalAlign="Center">

                            <telerik:RadGrid ID="radgrid_manageassetkits"  runat="server" Width="70%" CellSpacing="0" GridLines="None" AutoGenerateColumns="False"
                                AllowPaging="true" AllowSorting="true" DataSourceID="SqlDataSource1" OnItemDataBound="radgrid_manageassetkits_ItemDataBound">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView DataSourceID="SqlDataSource1">
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="View/Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_viewedit" runat="server" OnClick="lnk_viewedit_Click" Text="View/Edit"></asp:LinkButton>

                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="kitname"
                                            HeaderText="Group Name" SortExpression="kitname" UniqueName="kitname">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="kitdesc"
                                            HeaderText="Description" SortExpression="kitdesc" UniqueName="kitdesc">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Tool Names">
                                            <ItemTemplate>
                                                <telerik:RadToolTip ID="rttChooseFrom" CssClass="popupradgrid" runat="server" Animation="None" ContentScrolling="Auto"
                                                    Height="300px" HideEvent="ManualClose" Modal="true" RelativeTo="Element"
                                                    ShowEvent="OnClick" TargetControlID="lnk_assetnames" Title=""
                                                    Width="400px">
                                                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                                                        <telerik:RadGrid ID="radgridkitassets" runat="server" ShowHeader="true" AutoGenerateColumns="true">
                                                        </telerik:RadGrid>

                                                    </telerik:RadAjaxPanel>
                                                </telerik:RadToolTip>


                                                <asp:LinkButton ID="lnk_assetnames" runat="server" OnClientClick="return false;" Text="View Tools "></asp:LinkButton>
                                                <asp:Label ID="lbl_assetkitid" runat="server" Style="display: none" Text='<%# Bind("assetkitid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ databaseExpression:client_database %>" runat="server"
                                SelectCommand="select distinct assetkitid,kitname,kitdesc,createddate from PrismAssetKitDetails order by assetkitid desc"></asp:SqlDataSource>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


                <table>
                    <tr>
                        <td colspan="2">
                            <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                                <Windows>
                                    <telerik:RadWindow ID="window_department" runat="server"
                                        Width="500px" Height="300px" Title="Create New Tool Group">

                                        <ContentTemplate>

                                            <table>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Label ID="lblmessagetop" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">Group Name:<br />
                                                        <asp:TextBox ID="txt_topkitname" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>Description:<br />
                                                        <asp:TextBox ID="txt_topdesc" runat="server" Height="35px" Width="250px"></asp:TextBox>
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

            </ContentTemplate>

        </asp:UpdatePanel>

    </fieldset>

      <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>
</asp:Content>

