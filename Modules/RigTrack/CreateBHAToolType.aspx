<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="CreateBHAToolType.aspx.cs" Inherits="Modules_RigTrack_CreateBHAToolType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Label ID="lbl_message" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="display: none">
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
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td valign="top">Select BHA Tool Group Name<br />
                                        <telerik:RadComboBox ID="ddl_toolGroup" runat="server" DataSourceID="SqlGetBHAGroupname" DataTextField="BHAGroupName"
                                            
                                            DataValueField="ID" Width="250px">
                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetBHAGroupname"
                                            ConnectionString="<%$ databaseExpression:client_database %>" runat="server"
                                            SelectCommand="SELECT 0 AS [ID], 'Select Groupname' AS [BHAGroupName] UNION select [ID],[BHAGroupName] from [RigTrack].[tblCreateBHAToolGroup] "></asp:SqlDataSource>
                                        <br />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddl_toolGroup"
                                            CssClass="customValidatorClass" Display="None" ErrorMessage="Select BHA Tool Group" InitialValue="Select Tool Group" ForeColor="Red"
                                            ValidationGroup="TextBoxValidationGroup" />
                                    </td>
                                   <td valign="top">Enter BHA Tool Type<br />
                                        <telerik:RadTextBox ID="txt_tooltype" runat="server"></telerik:RadTextBox>
                                    </td>  
                                    <td>
                                        <br />
                                        <telerik:RadButton ID="radbtnSaveMeter" runat="server" Text="Save" ToolTip="Save meter"
                                            OnClick="radbtnSaveMeter_Click">
                                        </telerik:RadButton>
                                    </td>
                                    <td style="width: 2px"></td>
                                    <td>
                                        <br />
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
                            <telerik:RadGrid ID="radgrdService" runat="server" CellSpacing="0" GridLines="None" OnItemCommand="radgrdService_ItemCommand"
                                DataSourceID="SqlServiceData" OnPageIndexChanged="radgrdService_PageIndexChanged">
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="ToolTypeID">
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
                                        <telerik:GridBoundColumn DataField="ToolTypeID" DataType="System.Int32" FilterControlAltText="Filter ID column"
                                            HeaderText="ToolTypeID" ReadOnly="True" SortExpression="ToolTypeID" UniqueName="ToolTypeID" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ToolTypeName" FilterControlAltText="Filter ToolTypeName column"
                                            HeaderText="BHA Tool Type" SortExpression="ToolTypeName" UniqueName="ToolTypeName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BHAGroupName" FilterControlAltText="Filter BHAGroupName column"
                                            HeaderText="BHA GroupName" SortExpression="BHAGroupName" UniqueName="BHAGroupName">
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
                                SelectCommand="SELECT * FROM [RigTrack].[tblCreateBHAToolGroup] tg inner join [RigTrack].[BHAToolTypes] ty on tg.ID=ty.GroupID"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

