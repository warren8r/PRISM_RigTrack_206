<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="MasterCoOrdinatorDashBoard.aspx.cs" Inherits="Modules_Configuration_Manager_MasterCoOrdinatorDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        .bold
        {
            font-weight: bold;
            float: left;
            margin-left: 20px;
        }
        th, td
        {
            padding: 10;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <telerik:RadScriptBlock ID="radsc" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadScriptBlock>
   
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr>
                <td align="center" style="padding-left:350px">
                    <table >
                        <tr>
                             <td >
                                Select Job:<br />
                                <telerik:RadComboBox ID="radcombo_job" runat="server"  DataSourceID="SqlGetJobname" Width="300px" 
                                                        EmptyMessage="Select Job Name" DataTextField="jobname"  CheckBoxes="true"
                                                            DataValueField="jid" EnableCheckAllItemsCheckBox="true"  ></telerik:RadComboBox>
                <asp:SqlDataSource ID="SqlGetJobname" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                  SelectCommand="select * from manageJobOrders where (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''"></asp:SqlDataSource>
                                                 
                                
                            </td>
                            <td align="left">
                            Select Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_start"  Width="130px">
                                <Calendar ID="Calendar1" runat="server"  EnableKeyboardNavigation="true">
                                    <DisabledDayStyle CssClass="DisabledClass" />
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>

                            </telerik:RadDatePicker>
                        </td>
                            <td valign="bottom">
                                <asp:Button ID="btn_view" runat="server"  Text="View" onclick="btn_view_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
           </tr>
            <tr><td style="line-height:10px"></td></tr>
            <tr>
                <td align="center">
                   <table width="980px">
                        <tr>
                            <td style="color:Red" align="right">REd color indicates data not entered for previous day</td>
                        </tr>
                   </table> 
                </td>
            </tr>
            <tr>
                <td>
                                                        <telerik:RadGrid ID="grid_rig" runat="server" CellSpacing="0" GridLines="None" 
                                                             ShowGroupPanel="True" 
                                                            AutoGenerateColumns="False" AllowPaging="True" 
                                                                    AllowSorting="True" HorizontalAlign="Center" OnSortCommand="grid_rig_SortCommand"  OnItemDataBound="grid_rig_ItemDataBound">
                  
                                                            <AlternatingItemStyle VerticalAlign="Top" />
                                                            <MasterTableView CommandItemDisplay="Top"  >
                                                                <PagerStyle PageSizeControlType="RadComboBox" />
                                                                <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="true"
                                                                    ShowExportToCsvButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                                                                    ShowExportToPdfButton="false"></CommandItemSettings>
                      
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                      
                                                                 <Columns> 
                                                                 <telerik:GridBoundColumn DataField="programManagerId" Visible="false">
                                                                     <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle HorizontalAlign="Center" />
                                                                     </telerik:GridBoundColumn>                                                  
                                                                 <telerik:GridBoundColumn DataField="jid" Visible="false">
                                                                     <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle HorizontalAlign="Center" />
                                                                     </telerik:GridBoundColumn>
                                                                     <telerik:GridBoundColumn  HeaderText="District/State" SortExpression="Address"  DataField="Address" HeaderStyle-VerticalAlign="Middle" 
                                                                 ItemStyle-VerticalAlign="Middle" UniqueName="Address">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                                     <telerik:GridBoundColumn   HeaderText="Start&#160;Date" SortExpression="startdate"  DataField="startdate" DataFormatString="{0:MM/dd/yyyy}"  HeaderStyle-VerticalAlign="Middle" ItemStyle-VerticalAlign="Middle"  UniqueName="startdate">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                               <telerik:GridBoundColumn  HeaderText="Projected&#160;End&#160;Date" SortExpression="enddate" DataField="enddate" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-VerticalAlign="Middle"  ItemStyle-VerticalAlign="Middle" UniqueName="enddate">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                                     <telerik:GridBoundColumn   HeaderText="Coordinator"  DataField="Projectmanager" SortExpression="Projectmanager"  HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Middle" UniqueName="Projectmanager">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn> 
                                                                     <telerik:GridTemplateColumn   HeaderText="MWD Hands"   FilterControlAltText="Filter Personals column" UniqueName="Personals">
                                                                    <ItemTemplate>
                                                                        <%--<asp:Label ID="lbl_personals" runat="server" />--%>
                                                                        <asp:Label ID="lbl_day" runat="server"  /><br />
                                                                        <asp:Label ID="lbl_night" runat="server"   />
                                                                        <%--<asp:Label ID="lbl_primarilostlong" runat="server" Visible="false" Text='<%# Bind("primaryLatLong") %>' ></asp:Label>--%>
                                                                    </ItemTemplate>                             
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn  HeaderText="Rig&#160;Name" SortExpression="Rigname"  DataField="Rigname" 
                                                               HeaderStyle-VerticalAlign="Middle"  ItemStyle-VerticalAlign="Middle" UniqueName="Rigname">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn> 
                                                                <telerik:GridBoundColumn  DataField="jobname" SortExpression="jobname" HeaderText="Job&#160;Name"  UniqueName="jobname" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Middle">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                                                        
                                                               
                                                               <telerik:GridTemplateColumn   HeaderText="Equipment&#160;Needed"   FilterControlAltText="Filter Equipment column"
                                                                    UniqueName="Equipment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Equipment" runat="server" />
                                                                        
                                                                    </ItemTemplate>                             
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn   HeaderText="Run Number"   FilterControlAltText="Filter Activity column"
                                                                    UniqueName="runnumber">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_runnumber" runat="server" />
                                                                    </ItemTemplate>                             
                                                                     <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>    
                                                                <telerik:GridTemplateColumn   HeaderText="Circulating Hours"   FilterControlAltText="Filter Activity column"
                                                                    UniqueName="runnumber">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_hrs" runat="server" />
                                                                    </ItemTemplate>                             
                                                                     <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn> 
                                                                <telerik:GridTemplateColumn   HeaderText="Assets(Serial #)" Visible="false"  FilterControlAltText="Filter Assets column" UniqueName="Assets">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Assets" runat="server" />
                                                                        <asp:Label ID="lbl_jid" Text='<%# Bind("jid") %>' Visible="false" runat="server" />
                                                                    </ItemTemplate>                             
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn   HeaderText="Report Submission Date"   FilterControlAltText="Filter Activity column"
                                                                    UniqueName="rundet">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_runhr" runat="server" />
                                                                    </ItemTemplate>                             
                                                                     <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                
                                                                
                                                                 
                                                                 <telerik:GridTemplateColumn   HeaderText="Activity"   FilterControlAltText="Filter Activity column"
                                                                    UniqueName="Activity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Activity" runat="server" />
                                                                    </ItemTemplate>                             
                                                                     <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                
                                                                
                                                                    
                                                            </Columns>
                                                                <EditFormSettings>
                                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                                    </EditColumn>
                                                                </EditFormSettings>
                                                                  <ItemStyle VerticalAlign="Top" />
                                                                <AlternatingItemStyle VerticalAlign="Top" />
                                                                <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                  <HeaderStyle HorizontalAlign="Center" />
                                                            </MasterTableView>
                                                              <HeaderStyle VerticalAlign="Bottom" HorizontalAlign="Center" />
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                                            <PagerStyle PageSizeControlType="RadComboBox" />
                                                            <FilterMenu EnableImageSprites="False">
                                                            </FilterMenu>
                                                        </telerik:RadGrid>
                                                        <%--<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                        SelectCommand="">
                                                        <SelectParameters>
                                                            <asp:QueryStringParameter Name="jid" DbType="String" QueryStringField="jobid" />
                                                        </SelectParameters>
                                                        </asp:SqlDataSource>--%>
                                                    </td>
            </tr>
        </table>
        </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

