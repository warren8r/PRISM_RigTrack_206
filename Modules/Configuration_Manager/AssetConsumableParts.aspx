<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="AssetConsumableParts.aspx.cs" Inherits="Modules_Configuration_Manager_AssetConsumableParts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">

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
         <tr><td style="line-height:10px"></td></tr>
        <tr>
            <td>
                     <telerik:RadGrid ID="RadGrid1"  OnItemDataBound="RadGrid1_ItemDataBound" runat="server" DataSourceID="sqlGetConsumbales" Width="500px" >
                                                      <MasterTableView  AutoGenerateColumns="False" ShowFooter="true" >
                                                        <Columns>
                                                          
                                                          <telerik:GridTemplateColumn HeaderText="Select">     
                                                               <ItemStyle Width="20px" />
                                                                <HeaderStyle Width="30px" />                                                        
                                                              <ItemTemplate>
                                                                  <asp:CheckBox ID="chk_select" runat="server" />
                                                                  <asp:Label ID="lbl_conid" runat="server" Text='<%# Eval("ConId") %>' Visible="false" />
                                                              </ItemTemplate>
                                                          </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderText="Name" DataField="ConName" UniqueName="ConName">
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridBoundColumn>                                                          
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
                                                                <HeaderStyle Width="60px" />
                                                              <ItemTemplate>
                                                                  <telerik:RadTextBox ID="txt_qty" Width="45px" runat="server" BorderStyle="Solid"></telerik:RadTextBox>
                                                              </ItemTemplate>
                                                                 <FooterTemplate>
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
                <asp:SqlDataSource ID="sqlGetConsumbales" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" SelectCommand="select * from Consumables"></asp:SqlDataSource>
            </td>
        </tr>
        <tr><td style="line-height:20px"></td></tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btn_save" runat="server" Text="Save" OnClick="btn_save_OnClick" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                           

                             <telerik:RadButton ID="btn_savefinalize" runat="server" Text="Maintenance Finished" OnClientClicking="Clicking"
                                           OnClick="btn_savefinalize_OnClick" Visible="false">
                                        </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

