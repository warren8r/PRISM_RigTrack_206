<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchedulerWindow.aspx.cs" Inherits="SchedulerWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <telerik:RadScriptManager ID="scscheduler" runat="server"></telerik:RadScriptManager>
        <table>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="grid_rig" runat="server" CellSpacing="0" GridLines="None" 
                                                             ShowGroupPanel="True" 
                                                            AutoGenerateColumns="False" AllowPaging="True" 
                                                                    AllowSorting="True" HorizontalAlign="Center"  OnItemDataBound="grid_rig_ItemDataBound">
                  
                                                            <AlternatingItemStyle VerticalAlign="Top" />
                                                            <MasterTableView CommandItemDisplay="Top"  >
                                                                <PagerStyle PageSizeControlType="RadComboBox" />
                                                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                                    ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false"
                                                                    ShowExportToPdfButton="true"></CommandItemSettings>
                      
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
                                                                <telerik:GridBoundColumn  DataField="jobname" SortExpression="jobname" HeaderText="Job&#160;Name"  UniqueName="jobname" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Middle">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn   HeaderText="Project&#160;Manager"  DataField="Projectmanager" SortExpression="Projectmanager"  HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Middle" UniqueName="Projectmanager">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>                         
                                                               <telerik:GridBoundColumn   HeaderText="Start&#160;Date" SortExpression="startdate"  DataField="startdate" DataFormatString="{0:MM/dd/yyyy}"  HeaderStyle-VerticalAlign="Middle" ItemStyle-VerticalAlign="Middle"  UniqueName="startdate">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                               <telerik:GridBoundColumn  HeaderText="Estimated&#160;End&#160;Date" SortExpression="enddate" DataField="enddate" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-VerticalAlign="Middle"  ItemStyle-VerticalAlign="Middle" UniqueName="enddate">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                               <%--<telerik:GridBoundColumn  HeaderText="Rig&#160;Name" SortExpression="Rigname"  DataField="Rigname" HeaderStyle-VerticalAlign="Middle"  ItemStyle-VerticalAlign="Middle" UniqueName="Rigname">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>--%>
                                                                      <telerik:GridTemplateColumn   HeaderText="Personnel"   FilterControlAltText="Filter Personals column" UniqueName="Personals">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_personals" runat="server" />
                                                                        <asp:Label ID="lbl_primarilostlong" runat="server" Visible="false" Text='<%# Bind("primaryLatLong") %>' ></asp:Label>
                                                                    </ItemTemplate>                             
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn> 
                                                                <telerik:GridTemplateColumn   HeaderText="Assets(Serial #)"   FilterControlAltText="Filter Assets column" UniqueName="Assets">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Assets" runat="server" />
                                                                        <asp:Label ID="lbl_jid" Text='<%# Bind("jid") %>' Visible="false" runat="server" />
                                                                    </ItemTemplate>                             
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn   HeaderText="Last Run Date"   FilterControlAltText="Filter Activity column"
                                                                    UniqueName="rundet">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_runhr" runat="server" />
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
                                                                
                                                                <telerik:GridTemplateColumn   HeaderText="Equipment&#160;Needed"   FilterControlAltText="Filter Equipment column"
                                                                    UniqueName="Equipment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Equipment" runat="server" />
                                                                        
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
                                                                
                                                                <telerik:GridBoundColumn Visible="false" HeaderText="Address" SortExpression="Address"  DataField="Address" HeaderStyle-VerticalAlign="Middle" 
                                                                 ItemStyle-VerticalAlign="Middle" UniqueName="Address">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                                                     </telerik:GridBoundColumn>
                                                                    
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
                                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                        SelectCommand="">
                                                        <SelectParameters>
                                                            <asp:QueryStringParameter Name="jid" DbType="String" QueryStringField="jobid" />
                                                        </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <%--<artem:GoogleMap ID="GoogleMap1" runat="server" Width="800px" Height="600px"></artem:GoogleMap>
                                                        <artem:GoogleMarker Latitude="42.1229" Longitude="24.7879" Text="This is my first google marker here." />--%>
                                                        <artem:GoogleMap ID="GoogleMap1" Visible="true" EnableOverviewMapControl="false"
                                                                EnableMapTypeControl="false" EnableZoomControl="false" EnableStreetViewControl="false"
                                                                ShowTraffic="false" runat="server" ApiVersion="3" EnableScrollWheelZoom="true"
                                                                IsSensor="true" Zoom="10" DisableDoubleClickZoom="False" DisableKeyboardShortcuts="True"
                                                                EnableReverseGeocoding="True" Height="300" IsStatic="false" Key="AIzaSyC2lU7S-IMlNUTu8nHAL97_rL06vKmzfhc"
                                                                MapType="Roadmap" StaticFormat="Gif" Tilt="45" Width="1060px" StaticScale="15"
                                                                DefaultAddress="" Latitude="33.335767" Longitude="-111.944151">
                                                                <Center Latitude="33.335767" Longitude="-111.944151" />
                                                                <MapTypeControlOptions Position="TopRight" ViewStyle="Default" />
                                                                <OverviewMapControlOptions Opened="False" />
                                                                <PanControlOptions Position="TopLeft" />
                                                                <RotateControlOptions Position="TopLeft" />
                                                                <ScaleControlOptions Position="TopLeft" Style="default" />
                                                            </artem:GoogleMap>
                                                             <artem:GoogleMarkers runat="server" ID="GoogleMarker" TargetControlID="GoogleMap1" />
                                                    </td>
                                                </tr>
                                            </table>



                                            
    </div>
    </form>
</body>
</html>
