<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewEditBHAInfo.aspx.cs" Inherits="Modules_RigTrack_ViewEditBHAInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">
                <div class="loader2">Loading...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                    <td style="padding-left:30px">
                        <h2>View/Edit BHA Info</h2>
                    </td>
                </tr>
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                Select Company<br />
                                <telerik:RadDropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                                     <td align="left">Select Job<br />
                                
                                 <telerik:RadComboBox runat="server" ID="combo_job" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                                        >
                                                        <Items>
                                                            <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_view" runat="server" Text="View" onclick="btn_view_Click" />
                        </td>
                                </tr>
                            </table>
                        </td>
                           
                    </tr>
                    <tr>
                        <td align="center" >
                            <telerik:RadGrid ID="RadGridBHAInfo" AllowFilteringByColumn="false" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30"
                    OnNeedDataSource="RadGridBHAInfo_NeedDataSource" Width="1250px" OnUpdateCommand="RadGridBHAInfo_UpdateCommand"  OnItemCommand="RadGridBHAInfo_ItemCommand" OnItemDataBound="RadGridBHAInfo_ItemDataBound">
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true" Scrolling-AllowScroll="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <ExportSettings>
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView EditMode="InPlace" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="LinkButton" HeaderText="Edit"></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderText="ID" ReadOnly="true" DataField="BHAID" UniqueName="BHAID" SortExpression="BHAID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Job/Curve Group" HeaderStyle-Width="200px" ItemStyle-Width="200px" ReadOnly="true" DataField="JobName" UniqueName="JobName" ></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Number" ReadOnly="true" DataField="BHANumber" UniqueName="BHANumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Desc" DataField="BHADesc" UniqueName="BHADesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BHA Type" DataField="BHAType" UniqueName="BHAType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bit Sno" DataField="BitSno" UniqueName="BitSno"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bit Desc" DataField="BitDesc" UniqueName="BitDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ODFrac" DataField="ODFrac" UniqueName="ODFrac"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitLength" DataField="BitLength" UniqueName="BitLength"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Connection" DataField="Connection" UniqueName="Connection"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitType" DataField="BitType" UniqueName="BitType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BearingType" DataField="BearingType" UniqueName="BearingType"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitMfg" DataField="BitMfg" UniqueName="BitMfg"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BitNumber" DataField="BitNumber" UniqueName="BitNumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="No. JETS" DataField="NUMJETS" UniqueName="NUMJETS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="InnerRow" DataField="InnerRow" UniqueName="InnerRow"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="OuterRow" DataField="OuterRow" UniqueName="OuterRow"></telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn HeaderText="DullChar" DataField="DullChar" UniqueName="DullChar"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Location" DataField="Location" UniqueName="Location"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BearingSeals" DataField="BearingSeals" UniqueName="BearingSeals"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Guage" DataField="Guage" UniqueName="Guage"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="OtherDullChar" DataField="OtherDullChar" UniqueName="OtherDullChar"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ReasonPulled" DataField="ReasonPulled" UniqueName="ReasonPulled"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="MotorDesc" DataField="MotorDesc" UniqueName="MotorDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="MotorMFG" DataField="MotorMFG" UniqueName="MotorMFG"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="NBStabilizer" DataField="NBStabilizer" UniqueName="NBStabilizer"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Model" DataField="Model" UniqueName="Model"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Revolutions" DataField="Revolutions" UniqueName="Revolutions"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Bend" DataField="Bend" UniqueName="Bend"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RotorJet" DataField="RotorJet" UniqueName="RotorJet"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="BittoBend" DataField="BittoBend" UniqueName="BittoBend"></telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
            
            </asp:UpdatePanel>
    </fieldset>
</asp:Content>

