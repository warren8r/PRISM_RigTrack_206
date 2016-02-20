<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="ManageComponentName.aspx.cs" Inherits="Modules_Configuration_Manager_ManageManufracturer" %>

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
                        <td  align="center">
                            <table border="1">
                                <tr>
                                    <td>
                                        Selected Component Category Name:
                                                    
                                    </td>
                                    <td>
                                        &nbsp;<asp:Label ID="lbl_assetcatname" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                            
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td valign="top">Enter Comp. Name<br />
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
                            DataKeyNames="componet_id">
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
                                <telerik:GridBoundColumn DataField="componet_id" DataType="System.Int32" FilterControlAltText="Filter ID column"
                                    HeaderText="componet_id" ReadOnly="True" SortExpression="componet_id" UniqueName="componet_id" Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="[ComponentName]" FilterControlAltText="Filter [ComponentName] column"
                                    HeaderText="Comp. Name" SortExpression="[ComponentName]" UniqueName="[ComponentName]">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Description"  FilterControlAltText="Filter Description column"
                                    HeaderText="Description" SortExpression="Description" UniqueName="Description">
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
                        SelectCommand="SELECT * FROM [Prism_ComponentNames]">
                    </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</asp:Content>

