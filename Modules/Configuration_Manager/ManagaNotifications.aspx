<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManagaNotifications.aspx.cs" Inherits="Modules_Configuration_Manager_AssetRepairStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <script language="javascript" type="text/javascript">
        

             function OnClientClicking(sender, args) {
                 //alert(sender);
                 var callBackFunction = Function.createDelegate(sender, function (argument) {
                     if (argument) {
                         this.click();
                     }
                 });

                 var text = "Notifications will not be sent to selected Event";
                 radconfirm(text, callBackFunction, 300, 100, null, "Confirm Box");
                 args.set_cancel(true);
             }
    </script>
  <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <telerik:RadWindowManager ID="radwin" runat="server"></telerik:RadWindowManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table>
                 
                    <tr>
                        <td >
                            <asp:HiddenField ID="hidd_acc" runat="server" />
                             <telerik:RadGrid ID="radgrid_notificationstatus"  runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                                DataSourceID="SqlDataSource_Notifications"  AllowPaging="true" AllowSorting="true"  OnPageIndexChanged="radgrid_notificationstatus_PageIndexChanged"
                                OnItemDataBound="radgrid_notificationstatus_ItemDataBound">
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView   >
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Condition" >
                                <ItemStyle Width="100px" />
                                <ItemTemplate >
                                    <asp:CheckBox runat="server" ID="isChecked"    AutoPostBack="true"  onclick="javascript:ChkClick(this);" Visible="false"  OnCheckedChanged="CheckChanged" />
                                    <telerik:RadButton ID="btn_maintain" runat="server" ForeColor="Green" Text="Required" OnClick="btn_maintain_Click" 
                                    OnClientClicking="OnClientClicking"></telerik:RadButton>
                                    <asp:Label ID="lbl_statuscheck" runat="server" Visible="false" ></asp:Label>
                                    <asp:Label ID="lbl_notification" runat="server" Text='<%# Bind("eventnotification") %>' Visible="false" ></asp:Label>
                                    <asp:Label ID="lbl_eventid" runat="server" Text='<%# Bind("id") %>' Visible="false" ></asp:Label>
                                </ItemTemplate>
                            
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="eventName"
                                HeaderText="Event&#160;Name" SortExpression="eventName" UniqueName="eventName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventCode"
                                HeaderText="Event&#160;Code" SortExpression="eventCode" UniqueName="eventCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="flagName" HeaderText="Flag&#160;Name" SortExpression="flagName" UniqueName="flagName">
                            </telerik:GridBoundColumn>
                                                    
                                </Columns>
                                
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                
                            </MasterTableView>
                            
                        </telerik:RadGrid>
                        <asp:SqlDataSource ID="SqlDataSource_Notifications" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server"
                            SelectCommand="SELECT eventName + ' ' + '(' + eventCode + ')' AS eventAndCode,events.eventnotification, events.id, events.eventName,
 events.eventCode, flag.flagName, coalesce( flag.flagColor, '#fff' ) as flagColor FROM 
 events LEFT OUTER JOIN flag ON events.flagId = flag.id ORDER BY eventName ASC"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>
    </ContentTemplate>
            
            </asp:UpdatePanel>
</asp:Content>

