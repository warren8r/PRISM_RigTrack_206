<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="Comp_Re_assignment.aspx.cs" Inherits="Modules_Configuration_Manager_Comp_Maintanence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
 <script type="text/javascript">



     function Clicking(sender, args) {


         function callBackFunction(arg) {
             if (arg == true) {
                 $find("<%=btn_movewarehouse.ClientID %>").click();
             }
         }
         radconfirm("Are you sure you want to Move to Warehouse the selected Component?", callBackFunction, 300, 160, null, "Confirmation Box");
         args.set_cancel(true);


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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
                <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td >Asset&#160;Category:<br />
                                <telerik:RadComboBox ID="ddl_assetcategory" runat="server" DataSourceID="SqlAssetCategory" DataTextField="clientAssetName" 
                                    DataValueField="clientAssetID" EmptyMessage="Select Asset Category" OnSelectedIndexChanged="ddl_assetcategory_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlAssetCategory" 
                                        ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                        SelectCommand="SELECT 0 AS [clientAssetID], 'Select Asset Category' AS [clientAssetName] UNION SELECT clientAssetID, 
                                        clientAssetName FROM clientAssets where active='True'"></asp:SqlDataSource>
                            </td>
                        <td>
                            Asset&#160;Name:<br />
                             <telerik:RadComboBox ID="ddl_asset" runat="server" DataSourceID="SqlAssetName" DataTextField="AssetName" 
                                    DataValueField="AID" EmptyMessage="Select Asset Name"  AutoPostBack="true"  OnSelectedIndexChanged="ddl_asset_SelectedIndexChanged"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlAssetName" 
                                        ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" >
                                       
                                    </asp:SqlDataSource>
                            </td>
                        <td>
                             Asset&#160;Serial#:<br />
                             <telerik:RadComboBox ID="ddl_assetserialno" runat="server" DataSourceID="SqlAssetserialno" DataTextField="SerialNumber"  AutoPostBack="true"
                                    DataValueField="AID" EmptyMessage="Select Asset Serial#" OnSelectedIndexChanged="ddl_assetserialno_SelectedIndexChanged" ></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlAssetserialno"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" >
                                    </asp:SqlDataSource>
                            <%--0 AS [AID], 'Select Asset Serial#' AS [SerialNumber] UNION --%>
                                        
                        </td>
                        <td>
                             Component&#160;Name:<br />
                             <telerik:RadComboBox ID="ddl_component" runat="server" DataSourceID="SqlComponent" DataTextField="ComponentName" 
                                    DataValueField="componet_id" EmptyMessage="Select Comp. Name"  ></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlComponent"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" >
                                    </asp:SqlDataSource>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_view" Text="View Components" runat="server" OnClick="btn_view_Click" />
                        </td>
                        <td>
                            <br />
                            <telerik:RadButton ID="btn_movewarehouse" Text="Move to Warehouse" runat="server" OnClientClicking="Clicking" OnClick="btn_movewarehouse_Click" />
                        </td>
                         <td valign="bottom" align="left">
                                        &nbsp;&nbsp;<asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                                    </td>
                    </tr>
                </table>
                </td>
            </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td >Asset&#160;Category:<br />
                                <telerik:RadComboBox ID="ddl_cat_to" runat="server" DataSourceID="SqlAssetCategory" DataTextField="clientAssetName" 
                                    DataValueField="clientAssetID" EmptyMessage="Select Asset Category" OnSelectedIndexChanged="ddl_cat_to_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                                   
                            </td>
                        <td>
                            Asset&#160;Name:<br />
                             <telerik:RadComboBox ID="ddl_asset_to" runat="server" DataSourceID="SqlAssetName" DataTextField="AssetName" 
                                    DataValueField="AID" EmptyMessage="Select Asset Name"  AutoPostBack="true"  OnSelectedIndexChanged="ddl_asset_to_SelectedIndexChanged"></telerik:RadComboBox>
                                  
                            </td>
                        <td>
                             Asset&#160;Serial#:<br />
                             <telerik:RadComboBox ID="ddl_serial_to" runat="server" DataSourceID="SqlAssetserialnoto" DataTextField="SerialNumber"  
                                    DataValueField="AID" EmptyMessage="Select Asset Serial#"  ></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="SqlAssetserialnoto"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" >
                                    </asp:SqlDataSource>
                            <%--0 AS [AID], 'Select Asset Serial#' AS [SerialNumber] UNION --%>
                                        
                        </td>
                       
                        <td>
                            <br />
                            <asp:Button ID="btn_move" Text="Move Components" runat="server" OnClick="btn_move_Click" />
                        </td>
                        
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td>
                 <telerik:RadGrid ID="radgrid_repairstatus"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                                  AllowPaging="true" AllowSorting="true" DataSourceID="SqlDataSource1">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"  >
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView  DataKeyNames="CompID"  DataSourceID="SqlDataSource1">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Condition" >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                    
                                    <asp:Label ID="lbl_statuscheck" runat="server" Text='<%# Bind("comrstatus") %>'></asp:Label>
                                    <asp:Label ID="lbl_compid" runat="server" Text='<%# Bind("CompID") %>' Visible="false" ></asp:Label>
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ComponentName"  HeaderText="Comp.Name" SortExpression="ComponentName" UniqueName="ComponentName"/>
                            <telerik:GridBoundColumn DataField="Serialno" HeaderText="Comp.Serial No." SortExpression="Serialno" UniqueName="Serialno"/>
                            <telerik:GridBoundColumn DataField="Aname"  HeaderText="Asset Name" SortExpression="Aname" UniqueName="Aname"/>
                            <telerik:GridBoundColumn DataField="clientAssetName" HeaderText="Asset Category" SortExpression="clientAssetName" UniqueName="clientAssetName"/>
                            <telerik:GridBoundColumn DataField="SerialNumber"  HeaderText="Serial Number" SortExpression="SerialNumber" UniqueName="SerialNumber"/>
                            <telerik:GridBoundColumn DataField="CType"  HeaderText="Type" SortExpression="CType" UniqueName="CType" />
                            <telerik:GridBoundColumn DataField="CMake"  HeaderText="Make" SortExpression="CMake" UniqueName="CMake"/>
                            <telerik:GridBoundColumn DataField="CCost"  HeaderText="Component Cost($)" SortExpression="CCost" UniqueName="CCost" />
                                                    
                                </Columns>
                                
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                
                            </MasterTableView>
                            
                        </telerik:RadGrid>
                 <asp:SqlDataSource ID="SqlDataSource1"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" >
                                    </asp:SqlDataSource>
            </td>
        </tr>
        </table>
  </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

