<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ComponentMaintanence.aspx.cs" Inherits="Modules_Configuration_Manager_AssetMaintanence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        //        function confirmfinalize() {
        //            var r = confirm("Are you sure you want to Finalize the selected Assets");
        //            if (r == true) {
        //                return true;
        //            }
        //            else {
        //                return false;
        //            }
        //        }
        function confirmfinalize(sender, args) {
            var grid = $find("<%=RadGrid_Assets.ClientID %>");
            var countofselect = 0;
            var MasterTable = grid.get_masterTableView();
            var items = grid.get_masterTableView().get_dataItems();
            for (i = 0; i < items.length; i++) {
                var drop1 = items[i].findElement('chk_select_finalize');
                if (drop1.checked)
                    countofselect++;

            }

            if (countofselect > 0) {
                function callBackFunction(arg) {
                    if (arg == true) {
                        $find("<%=btn_savefinalize.ClientID %>").click();
                    }
                }
                radconfirm("Are you sure you want to Finalize the selected Components?", callBackFunction, 300, 160, null, "Confirmation Box");
                args.set_cancel(true);
            }
            else {
                radalert('Select Component(s) for Finalize', 330, 180, 'Alert Box', null, null);
                args.set_cancel(true);
            }
        }
        function openNotes(ComponentRid) {
            // alert(AssetRid);
            var status = "EDIT";
            var url = "../../Modules/Configuration_Manager/ComponentsNotes.aspx?Componentid=" + ComponentRid + "&status=" + status + "";
            // alert(url);
            document.getElementById('<%=iframe2.ClientID %>').src = url;
            window.radopen(null, "RadWindow2");
            return false;
        }
    </script>
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
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td align="left">
                            Start Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left">
                            End Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_end" Width="130px">
                                <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput2" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left">
                            Maintenance Status<br />
                            <telerik:RadComboBox ID="radcombo_status" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Select All" Value="Select All" />
                                    <telerik:RadComboBoxItem Text="Pending" Selected="true" Value="Pending" />
                                    <telerik:RadComboBoxItem Text="Completed" Value="Finished" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            <br />
                            <asp:Button ID="btn_view" runat="server" Text="View"  onclick="btn_view_Click" />
                        </td>
                         <td valign="bottom" align="left">
                                        &nbsp;&nbsp;<asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                                    </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td align="center" id="search" runat="server">
                <table style="border:solid 1px #000000">
                    <tr>
                        <td>
                           Search by: 
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radcombo_searchassets"  Width="200px" EmptyMessage="Select Component" 
                                DataTextField="ComponentName"  DataValueField="componet_id"
                                runat="server"/>
                            
                        </td>
                        <td>
                            <asp:TextBox ID="txtassetserialno" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGrid_Assets"  runat="server" CellSpacing="0" 
                                GridLines="None" AutoGenerateColumns="False" AllowSorting="True"  AllowPaging="True"  PageSize="10" Width="100%" 
                                OnItemDataBound="RadGrid_Assets_ItemDataBound">      
                                <MasterTableView     >                   
                                    <Columns>
                                     
                                        <telerik:GridBoundColumn DataField="AssetRid" DataType="System.Int32" FilterControlAltText="Filter jobconsumableid column"
                                            ReadOnly="True" SortExpression="AssetRid" UniqueName="AssetRid" Display="False">
                                        </telerik:GridBoundColumn>  
                                        <telerik:GridTemplateColumn HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select_finalize" runat="server" Checked='<%# Eval("Final").ToString() == "1" ? true:false %>'/>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                        
                                        <telerik:GridTemplateColumn HeaderText="Component Name">
                                           <ItemTemplate >                              
                                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("ComponentFullName") %>'></asp:Label>
                                               <asp:Label ID="lbl_ComponentRid" runat="server" Text='<%# Eval("ComponentRid") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="lbl_prismassetid" runat="server" Text='<%# Eval("PrismCompId") %>' Visible="false"></asp:Label>
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Serial Number">
                                           <ItemTemplate >                              
                                            <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("Serialno") %>'></asp:Label>
                                               
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                        
                                        
                                        <telerik:GridDateTimeColumn DataField="repairdate" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="Maintenance Start Date" SortExpression="repairdate" UniqueName="repairdate">
                                            <ItemStyle Width="80px" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridDateTimeColumn>
                                         <telerik:GridDateTimeColumn DataField="repairfixdate" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="Maintenance Finished Date" SortExpression="repairfixdate" UniqueName="repairfixdate">
                                             <ItemStyle Width="80px" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridDateTimeColumn>
                                         <telerik:GridTemplateColumn HeaderText="Verified By">
                                           <ItemTemplate >                              
                                            <asp:Label ID="lbl_verifiedby" runat="server" ></asp:Label>
                                               
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Status">
                                           <ItemTemplate >                              
                                            <asp:Label ID="lbl_ststus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                               
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Component  Maintenece Cost($)">
                                             <HeaderStyle HorizontalAlign="Center" />
                                           <ItemTemplate >                              
                                            <asp:TextBox ID="txt_cost" runat="server" Width="200px" Text='<%# Eval("Cost") %>'></asp:TextBox>
                                               
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Add Notes">
                                           <ItemTemplate >                              
                                            <%--<asp:TextBox ID="txt_notes" runat="server" Width="200px" Text='<%# Eval("Notes") %>'></asp:TextBox>--%>
                                               <asp:LinkButton ID="lnkbtn_notes" runat="server" Text="Add Notes" OnClientClick='<%# "openNotes(\"" + Eval("PrismCompId" ) + "\" ); return false;" %>' />
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>     
                            </telerik:RadGrid>
                           <%-- <asp:SqlDataSource ID="SqlGetassetstatus"  ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                            SelectCommand="select * from Consumables c,PrismAssetMaintenanceDetails m where c.ConID=m.ConId and m.AssetRid=@assetRid" >
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lbl_assetrid" Name="assetRid" />
                                </SelectParameters>
                        </asp:SqlDataSource>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_OnClick" />
                        </td>
                        <td>&nbsp;</td>
                        <td><telerik:RadButton ID="btn_savefinalize" runat="server" Text="Maintenance Completed" OnClientClicking="confirmfinalize" OnClick="btn_savefinalize_OnClick" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   
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
</asp:Content>

