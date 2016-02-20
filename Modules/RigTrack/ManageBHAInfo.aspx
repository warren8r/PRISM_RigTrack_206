<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageBHAInfo.aspx.cs" Inherits="Modules_RigTrack_ManageBHAInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function openAsset() {

            
                
                var url = "../../Modules/RigTrack/CreateBHAType.aspx";
                document.getElementById('<%=iframe3.ClientID %>').src = url;
             window.radopen(null, "window_Asset");
             return false;
         

     }
    </script>
    <script type="text/javascript">
        function ClientClose() {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
                                       }
                                    </script> 
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Always">
        <ContentTemplate>
        <table width="100%">
                <tr>
                    <td style="padding-left:30px">
                        <h2>Create BHA</h2>
                    </td>
                </tr>
               <tr>
                   <td align="center">
                       <table>
                           <tr>
                               <td colspan="6">
                                   <asp:Label ID="lbl_message" runat="server"></asp:Label>
                               </td>
                           </tr>
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
                               <td>
                                    Job/Curve Group ID:<br />
                                    <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                        >
                                        <Items>
                                            <telerik:DropDownListItem Value="0" Text="-Select-" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                               <td>
                                    BHA #:<br />
                                    <telerik:RadTextBox ID="txtBHANumber" runat="server" Width="220px"></telerik:RadTextBox>
                                </td>
                                <td>
                                    BHA Description:<br />
                                    <telerik:RadTextBox ID="txtBHADesc" runat="server" Width="220px"></telerik:RadTextBox>
                                </td>
                               <td>
                                    BHA Type:&#160;&#160;&#160;<asp:LinkButton ID="LinkButton1" runat="server" Text="Create New" OnClientClick="openAsset();return false" /><br />
                                    <telerik:RadComboBox runat="server" ID="comboBHAType"  Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                        >
                                        
                                    </telerik:RadComboBox>
                                   <asp:SqlDataSource ID="SqlGetWarehouse" 
                                                   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                   SelectCommand="SELECT [ID],[BHAType] from tblBHAType  "></asp:SqlDataSource>
                                   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" onajaxrequest="RadAjaxManager1_AjaxRequest"> 
                                      <AjaxSettings> 
                                            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1"> 
                                            <UpdatedControls> 
                                                   <telerik:AjaxUpdatedControl ControlID="comboBHAType" /> 
                                            </UpdatedControls> 
                                            </telerik:AjaxSetting> 
                                      </AjaxSettings> 
                                </telerik:RadAjaxManager>
                                   
                                </td>
                               
                               <td   align="right">
                                   <table>
                           <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                            <br />
                            <asp:Button ID="btnSaveAssetName" runat="server"
                                Text="Create" ToolTip="Save changes" ValidationGroup="EditValidationGroup" OnClick="btnSaveAssetName_Click"
                                 
                                 />
                            
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btnCancelAssetName" runat="server"
                                Text="Clear" ToolTip="Cancel changes"
                                 CausesValidation="False" OnClick="btnCancel_Click" />
                        </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                            
                        </table>
                               </td>
                               
                           </tr>
                          
                       </table>
                    </td>
                </tr>
            <tr><td style="height:20px"></td></tr>
            <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                Select Company<br />
                                <telerik:RadDropDownList ID="ddlsearchcompany" runat="server" OnSelectedIndexChanged="ddlsearchcompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
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
                            <asp:Button ID="btn_view" runat="server" Text="Search" onclick="btn_view_Click" />
                        </td>
                                </tr>
                            </table>
                        </td>
                           
                    </tr>
            <tr>
                <td>
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
                            <%--<telerik:GridBoundColumn HeaderText="BHA Type" DataField="BHAType" UniqueName="BHAType">
                                
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="BHA Type" DataField="BHAType" UniqueName="BHAType">
                                <ItemTemplate>
                                    <asp:Label ID="lblBHAType" runat="server" Text='<%# Eval("BHATypeName") %>'></asp:Label>
                                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblBHATypeEdit" runat="server" Text='<%# Eval("BHATypeID") %>' Visible="false"></asp:Label>
                                     <telerik:RadComboBox runat="server" ID="comboBHAType" DataSourceID="SqlGetWarehouse123"  DataTextField="BHAType" DataValueField="ID" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px"
                                        >
                                        <Items>
                                                            <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                                        </Items>
                                    </telerik:RadComboBox>
                                  
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:SqlDataSource ID="SqlGetWarehouse123" 
                                                   ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                   SelectCommand="SELECT [ID],[BHAType] from tblBHAType  "></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"  Modal="true" Animation="Resize"> 
                                                     <Windows> 
                                                     
                                                      <telerik:RadWindow ID="window_Asset" runat="server"  Modal="true" Width="960px"  height="600px" Title="Create New / Edit BHA Type" OnClientClose="ClientClose">
 
                                                        <ContentTemplate>
                                                           <iframe id="iframe3" runat="server" width="960px" height="600px" >
                                               
                                                            </iframe>
 
                                                         </ContentTemplate>
 
                                                     </telerik:RadWindow>
 
                                                 </Windows> 
                                                 </telerik:RadWindowManager>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
        </table>
    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

