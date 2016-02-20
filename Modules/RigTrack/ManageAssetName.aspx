<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="ManageAssetName.aspx.cs" Inherits="Modules_Configuration_Manager_ManageManufracturer" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
      <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td><asp:Label ID="lbl_message" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="display:none">
                        <asp:HiddenField ID="hidden_serviceid" runat="server" />
                             <h3 style="margin: 3px;">
                            <asp:Label ID="lblMode" runat="server"></asp:Label>
                            Service
                        </h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="5" align="left">
                                        <table border="1">
                                            <tr>
                                                <td>
                                                    Selected Tool Group:
                                                    
                                                </td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lbl_assetcatname" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr><td style="height:10px"></td></tr>
                                <tr>
                                    <td valign="top">Enter Tool Name<br />
                                        <telerik:RadTextBox ID="txt_servicename" runat="server"></telerik:RadTextBox>
                                    </td>                                    
                                    <td  valign="top">Enter Description<br />
                                        <telerik:RadTextBox ID="txt_description" runat="server" TextMode="MultiLine"></telerik:RadTextBox>
                                    </td>
                                    <td>
                                         <telerik:RadButton ID="radbtnSaveMeter" runat="server" Text="Save" ToolTip="Save meter"
                                             OnClick="radbtnSaveMeter_Click">
                                        </telerik:RadButton>
                                        </td>
                                        <td style="width:2px"></td>
                                        <td>
                                             <telerik:RadButton ID="radbtnCancelMeter" runat="server" Text="Reset" CausesValidation="False"
                                            OnClick="radbtnCancelMeter_Click">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <telerik:RadGrid ID="radgrdService" runat="server" CellSpacing="0" GridLines="None"  OnItemCommand="radgrdService_ItemCommand"
                        DataSourceID="SqlServiceData" OnPageIndexChanged="radgrdService_PageIndexChanged">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="ID">
                            <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                                ShowRefreshButton="False" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>               
                                <telerik:GridButtonColumn CommandName="EditMeter" FilterControlAltText="Filter btnEdit column"
                                    Text="Edit" UniqueName="btnEdit">
                                </telerik:GridButtonColumn>
                                <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                                    HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID" Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AssetName" FilterControlAltText="Filter ServiceName column"
                                    HeaderText="Asset Name" SortExpression="AssetName" UniqueName="AssetName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AssetDescription"  FilterControlAltText="Filter ServiceDescription column"
                                    HeaderText="Description" SortExpression="AssetDescription" UniqueName="AssetDescription">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="clientAssetName"  FilterControlAltText="Filter ServiceDescription column"
                                    HeaderText="Asset Category" SortExpression="clientAssetName" UniqueName="clientAssetName">
                                </telerik:GridBoundColumn>
                                
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                            <PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True" />
                        </MasterTableView>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                    </telerik:RadGrid>
                     <asp:SqlDataSource ID="SqlServiceData" runat="server" ConnectionString="<%$ databaseExpression:client_database  %>"
                        SelectCommand="SELECT * FROM PrismAssetName a,clientAssets cat where a.AssetCategoryId=cat.clientAssetID">
                    </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</asp:Content>

