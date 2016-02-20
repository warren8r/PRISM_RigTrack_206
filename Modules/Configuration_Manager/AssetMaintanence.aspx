<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="AssetMaintanence.aspx.cs" Inherits="Modules_Configuration_Manager_AssetMaintanence" %>

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
                    $find("<%=btn_savefinalize.ClientID %>").click();
                }
            }
            radconfirm("Are you sure you want to Finalize the selected Assets?", callBackFunction, 300, 160, null, "Confirmation Box");
            args.set_cancel(true);


        }
    
        var str = "";
        function RowClicked(sender, args) {
            var grid = $find("<%= RadGrid_Assets.ClientID %>");
            var cb2 = $find("<%= RadGrid_Assets.ClientID%>").get_masterTableView().get_dataItems()[0].findControl("radcombo_consumables");

            var grid = sender;
            var MasterTable = grid.get_masterTableView();
            var row = MasterTable.get_dataItems()[args.get_itemIndexHierarchical()];
            var cell = MasterTable.getCellByColumnUniqueName(row, "ConName");

            //var checkBox = args.get_gridDataItem().findElement("chk_select");
            //var final = "";
            //if (checkBox.checked) {
            //    final = append(cell.innerHTML);
            //}
            var final = append(cell.innerHTML);

            cb2.set_text(final);


        }
        function append(test) {

            str += test + ',';

            return str;
        }
        function openwin(AssetRid) {
            // alert(AssetRid);
            var combo = document.getElementById("<%= radcombo_status.ClientID %>").value;
           // alert(combo);
            var url = "../../Modules/Configuration_Manager/AssetConsumableParts.aspx?Assetid=" + AssetRid + "&status="+combo+"";
           // alert(url);
                document.getElementById('<%=iframe1.ClientID %>').src = url;
                window.radopen(null, "RadWindow1");
               return false;
           }
           function openNotes(AssetRid) {
               // alert(AssetRid);
               var status = "EDIT";
               var url = "../../Modules/Configuration_Manager/MainteneceNotes.aspx?Assetid=" + AssetRid + "&status=" + status + "";
               // alert(url);
               document.getElementById('<%=iframe2.ClientID %>').src = url;
               window.radopen(null, "RadWindow2");
               return false;
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
                                    <telerik:RadComboBoxItem Text="Pending" Value="Pending" />
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
                            <telerik:RadComboBox ID="radcombo_searchassets"  Width="200px" EmptyMessage="Select Asset" 
                                DataTextField="AssetFullName"  DataValueField="assetid"
                                runat="server">

                            </telerik:RadComboBox>
                            
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
                                <MasterTableView >                   
                                    <Columns>
                                     
                                        <telerik:GridBoundColumn DataField="AssetRid" DataType="System.Int32" FilterControlAltText="Filter jobconsumableid column"
                                            ReadOnly="True" SortExpression="AssetRid" UniqueName="AssetRid" Display="False">
                                        </telerik:GridBoundColumn>  
                                        <telerik:GridTemplateColumn HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select_finalize" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                        
                                        <telerik:GridTemplateColumn HeaderText="Asset Name">
                                           <ItemTemplate >                              
                                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("AssetFullName") %>'></asp:Label>
                                               <asp:Label ID="lbl_assetrid" runat="server" Text='<%# Eval("AssetRid") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="lbl_prismassetid" runat="server" Text='<%# Eval("PrismAssetId") %>' Visible="false"></asp:Label>
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Serial Number">
                                           <ItemTemplate >                              
                                            <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("SerialNumber") %>'></asp:Label>
                                               
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Consumables/Parts Used">
                                             <ItemStyle Width="60px" HorizontalAlign="Center" />
                                           <ItemTemplate >                              
                                                <asp:LinkButton ID="LinkButton2" runat="server" Text="View" OnClientClick='<%# "openwin(\"" + Eval("AssetRid" ) + "\" ); return false;" %>' />
                                               <telerik:RadComboBox ID="radcombo_consumables" EnableLoadOnDemand="true"  Width="500px" Visible="false"
                                                    EmptyMessage="Consumables/Parts Used"  runat="server" AllowCustomText="True" >
                                                   <ItemTemplate>
                                                       
                                                          
                                                    <telerik:RadGrid ID="RadGrid1"  OnItemDataBound="RadGrid1_ItemDataBound" runat="server" >
                                                      <MasterTableView  AutoGenerateColumns="False" ShowFooter="true" >
                                                        <Columns>
                                                          
                                                          <telerik:GridTemplateColumn HeaderText="Select">
                                                              <ItemStyle Width="30px" />
                                                                <HeaderStyle Width="30px" />
                                                              <ItemTemplate>
                                                                  <asp:CheckBox ID="chk_select" runat="server" />
                                                                  <asp:Label ID="lbl_conid" runat="server" Text='<%# Eval("ConId") %>' Visible="false" />
                                                                  <%--<asp:Label ID="lbl_assetrid" runat="server" Text='<%# Eval("AssetRid") %>' Visible="false" />--%>
                                                              </ItemTemplate>
                                                          </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderText="Name" DataField="ConName" UniqueName="ConName">
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridBoundColumn>
                                                           <%-- <telerik:GridBoundColumn HeaderText="Cost/Piece ($)" DataField="ConCost" UniqueName="ConCost">
                                                                <ItemStyle Width="50px" />
                                                                <HeaderStyle Width="50px" />
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridTemplateColumn HeaderText="Cost/Piece&#160;($)"  UniqueName="ConCost">
                                                                <HeaderStyle Width="100px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ConCost" runat="server"  Text='<%# Eval("ConCost") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                                 <FooterTemplate  >
                                                                    GrandTotal($):
                                                                     </FooterTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Qty">
                                                                <ItemStyle Width="60px" />
                                                                <HeaderStyle Width="80px" />
                                                              <ItemTemplate>
                                                                  <asp:TextBox ID="txt_qty" Width="45px" runat="server"></asp:TextBox>
                                                              </ItemTemplate>
                                                                 <FooterTemplate  >
                                                                    
                                                                     <asp:Label ID="lbl_footot"  runat="server"></asp:Label>
                                                                     </FooterTemplate>
                                                          </telerik:GridTemplateColumn>
                                                           
                                                            <telerik:GridTemplateColumn HeaderText="Tot&#160;Cost&#160;($)"  UniqueName="ConCost">
                                                                <HeaderStyle Width="80px" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totcost" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate  >
                                                                   
                                                                </FooterTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                      </MasterTableView>
                                                      <ClientSettings>
                                                        
                                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                                                      </ClientSettings>
                                                    </telerik:RadGrid>
                                                  </ItemTemplate>
                                               </telerik:RadComboBox>
                                           </ItemTemplate>
                           
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Tot&#160;Maintenece&#160;Cost&#160;($)"  UniqueName="ConCost">
                                            <HeaderStyle Width="80px" />
                                                     <ItemTemplate>
                                                                 <asp:Label ID="lbl_totcost" runat="server"></asp:Label>
                                                      </ItemTemplate>                                                               
                                                 </telerik:GridTemplateColumn>
                                        <telerik:GridDateTimeColumn DataField="repairdate" 
                                            HeaderText="Maintenance Start Date Time" SortExpression="repairdate" UniqueName="repairdate">
                                            <ItemStyle Width="80px" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridDateTimeColumn>
                                        <%--DataFormatString="{0:MM/dd/yyyy}"--%>
                                         <telerik:GridDateTimeColumn DataField="repairfixdate" 
                                            HeaderText="Maintenance Finished Date Time" SortExpression="repairfixdate" UniqueName="repairfixdate">
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
                                        <telerik:GridTemplateColumn HeaderText="Add Notes">
                                           <ItemTemplate >                              
                                            <%--<asp:TextBox ID="txt_notes" runat="server" Width="200px" Text='<%# Eval("Notes") %>'></asp:TextBox>--%>
                                               <asp:LinkButton ID="lnkbtn_notes" runat="server" Text="Add Notes" OnClientClick='<%# "openNotes(\"" + Eval("PrismAssetId" ) + "\" ); return false;" %>' />
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

                         <%--   <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                               
                            </telerik:RadWindowManager>--%>
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
                        <td>
                           

                             <telerik:RadButton ID="btn_savefinalize" runat="server" Text="Maintenance Completed" OnClientClicking="Clicking"
                                           OnClick="btn_savefinalize_OnClick">
                                        </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        <telerik:RadWindowManager ID="radwin" runat="server">
            <Windows>
                                 <telerik:RadWindow ID="RadWindow1" runat="server"  Modal="true" Width="550px" Height="600px">
                                    <ContentTemplate>
                                        <table>
                                             <tr><td style="color:blue;font-weight:bold;cursor:default" align="center"> <asp:Button ID="Button1" runat="server" Text="Close" BackColor="Blue"  onclick="btn_view_Click" /> </td></tr>
                                            <tr>
                                                <td> <iframe id="iframe1" runat="server" width="550px" height="600px" >
                                        </iframe></td>
                                            </tr>
                                        </table>
                                      
                                     </ContentTemplate>
                                 </telerik:RadWindow>
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

