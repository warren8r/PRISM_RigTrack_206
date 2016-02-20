<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageServices.aspx.cs" Inherits="Modules_Configuration_Manager_ManageServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <td valign="top">Enter Service Name<br />
                                        <telerik:RadTextBox ID="txt_servicename" runat="server"></telerik:RadTextBox>
                                    </td>
                                    <td  valign="top">Enter Suggested Daily Rate($)<br />
                                        <telerik:RadTextBox ID="txt_cost" Width="180px" runat="server"></telerik:RadTextBox>
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
                                <telerik:GridBoundColumn DataField="ServiceName" FilterControlAltText="Filter ServiceName column"
                                    HeaderText="Service Name" SortExpression="ServiceName" UniqueName="ServiceName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ServiceDescription"  FilterControlAltText="Filter ServiceDescription column"
                                    HeaderText="Description" SortExpression="ServiceDescription" UniqueName="ServiceDescription">
                                </telerik:GridBoundColumn>
                                <telerik:GridNumericColumn dataFormatString="{0:$###,##0.00}" DataField="Cost" DataType="System.Decimal"
                                         NumericType="Currency" HeaderText="Suggested Daily Rate($)" SortExpression="Cost" UniqueName="Cost"  FooterAggregateFormatString="{0:C}">
                 </telerik:GridNumericColumn> 
               
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
                        SelectCommand="SELECT * FROM [PrismService]">
                    </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

