<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="DAServices.aspx.cs" Inherits="ClientAdmin_DAServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function SelectedIndexChanged(sender) {

                if ($find("<%=radcombo_dwm.ClientID %>").get_value() == "Daily") {

                    document.getElementById("weekname").style.display = "none";
                    document.getElementById("date").style.display = "none";
                    return false;
                }
                else if ($find("<%=radcombo_dwm.ClientID %>").get_value() == "Weekly") {

                    document.getElementById("weekname").style.display = "block";
                    document.getElementById("date").style.display = "none";
                    return false;
                }
                else if ($find("<%=radcombo_dwm.ClientID %>").get_value() == "Monthly") {

                    document.getElementById("weekname").style.display = "none";
                    document.getElementById("date").style.display = "block";
                    return false;
                }
                else {
                    document.getElementById("weekname").style.display = "none";
                    document.getElementById("date").style.display = "none";
                    return false;
                }
            }


            function validation() {
                if ($find("<%=radcombo_servicename.ClientID %>").get_value() == "0") {

                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Service Name";
                    return false;
                }
                if ($find("<%=radcombo_temname.ClientID %>").get_value() == "0") {

                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Template Name";
                    return false;
                }
                if ($find("<%=radcombo_dwm.ClientID %>").get_value() == "Daily") {

                    if ($find("<%=radcombo_time.ClientID %>").get_value() == "Select") {
                        document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Time";
                        return false;
                    }
                    
                }
                else if ($find("<%=radcombo_dwm.ClientID %>").get_value() == "Weekly") {

                    if ($find("<%=radcombo_weeknames.ClientID %>").get_value() == "Select") {
                        document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Week Name";
                        return false;
                    }
                }
                else if ($find("<%=radcombo_dwm.ClientID %>").get_value() == "Monthly") {

                    if ($find("<%=radcombo_date.ClientID %>").get_value() == "Select") {
                        document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select date in a month";
                        return false;
                    }
                }
                else {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Interval";
                    return false;
                }

                if ($find("<%=radcombo_time.ClientID %>").get_value() == "Select") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Time";
                    return false;
                }
                if ($find("<%=radcombo_status.ClientID %>").get_value() == "Select") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Status";
                    return false;
                }
            }

             
        </script>
 </telerik:RadCodeBlock>
 <script type="text/javascript">

     function hideobjs() {
         document.getElementById("weekname").style.display = "none";
         document.getElementById("date").style.display = "none";
     }
     window.onload = function () {
         hideobjs();
     };
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
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lbl_message" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr><td style="height:10px"></td></tr>
        <%--<tr>
            <td align="right" style="padding-right:5px; width:200px">
                <table style="border:solid 1px #000000">
                    <tr><td align="center" colspan="2" style="border:solid 1px #000000">Webscheduler</td></tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_start" runat="server" text="Start" onclick="Button1_Click" />
                            
                        </td>
                        <td>
                            <asp:Button ID="btn_stop" runat="server" text="Stop" onclick="Button2_Click" />
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>--%>
        <tr>
            <td align="center" style="width:100%">
                <table border="0" cellpadding="0" cellspacing="2">
                    
                    <tr>
                        <td align="left">
                            Service Name:<br />
                            <telerik:RadComboBox ID="radcombo_servicename" AutoPostBack="true" OnSelectedIndexChanged="radcombo_servicename_SelectedIndexChanged" runat="server">
                                
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            Template Name:<br />
                            <telerik:RadComboBox ID="radcombo_temname" runat="server">
                                
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            Select Interval:<br />
                            <telerik:RadComboBox ID="radcombo_dwm" OnClientSelectedIndexChanged="SelectedIndexChanged" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Select" Value="Select" />
                                    <telerik:RadComboBoxItem Text="Daily" Value="Daily" />
                                    <telerik:RadComboBoxItem Text="Weekly" Value="Weekly" />
                                    <telerik:RadComboBoxItem Text="Monthly" Value="Monthly" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" id="weekname" style="display:none">
                            Select day in a week:<br />
                            <telerik:RadComboBox ID="radcombo_weeknames" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Select" Value="Select" />
                                    <telerik:RadComboBoxItem Text="Monday" Value="Monday" />
                                    <telerik:RadComboBoxItem Text="Tuesday" Value="Tuesday" />
                                    <telerik:RadComboBoxItem Text="Wednesday" Value="Wednesday" />
                                    <telerik:RadComboBoxItem Text="Thursday" Value="Thursday" />
                                    <telerik:RadComboBoxItem Text="Friday" Value="Friday" />
                                    <telerik:RadComboBoxItem Text="Saturday" Value="Saturday" />
                                    <telerik:RadComboBoxItem Text="Sunday" Value="Sunday" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" id="date" style="display:none">
                            Select Date:<br />
                            <telerik:RadComboBox ID="radcombo_date" runat="server">
                                <Items>
                                    
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            Time(Hrs):<br />
                            <telerik:RadComboBox ID="radcombo_time" runat="server">
                                
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            Status:<br />
                            <telerik:RadComboBox ID="radcombo_status" Width="60px" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Select" Value="Select" />
                                    <telerik:RadComboBoxItem Text="Active" Value="Active" />
                                    <telerik:RadComboBoxItem Text="Inactive" Value="Inactive" />
                                    
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClientClick="javascript:return validation();" class="save" onclick="btn_save_Click" />
                        </td>
                        <%--<td>
                            <br />
                            <telerik:RadComboBox ID="radcombo_min" runat="server">
                                
                            </telerik:RadComboBox>(Min)
                        </td>--%>

                    </tr>
                </table>
            </td>
            
        </tr>
        <tr><td style="height:30px"></td></tr>
        <tr>
            <td align="center" colspan="2">
                <telerik:RadGrid  ID="radgrid_daservice" Width="800px" OnItemDataBound="radgrid_daservice_ItemDataBound"
                    AllowPaging="true" AllowSorting="true"  PageSize="5"
                        runat="server" AllowFilteringByColumn="false" >
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <ClientSettings EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="true"></Selecting>
                    </ClientSettings>
                    <%--<SelectedItemStyle BackColor="Gray"></SelectedItemStyle>--%>
                    <EditItemStyle CssClass="EditedItem" Height="25px"></EditItemStyle> 
                    <MasterTableView DataKeyNames="ScheduledServiceID" GridLines="Horizontal" AutoGenerateColumns="false" NoMasterRecordsText="No Data have been added." >             
                        <HeaderStyle HorizontalAlign="left" />
                        <ItemStyle HorizontalAlign="Left" />
                                              
                        <AlternatingItemStyle HorizontalAlign="Left" />
                                <Columns>
                                        
                                    <telerik:GridBoundColumn DataField="serviceName" HeaderText="Service Name" UniqueName="serviceName"
                                        SortExpression="serviceName">
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="templateName" HeaderText="Template Name" UniqueName="templateName"
                                        SortExpression="templateName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="URL" HeaderText="URL" UniqueName="URL"
                                        SortExpression="URL">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="interval" HeaderText="Interval" UniqueName="interval"
                                        SortExpression="interval">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="weekname" HeaderText="Week Day" UniqueName="weekname"
                                        SortExpression="weekname">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="datenumber" HeaderText="Day of month" UniqueName="datenumber"
                                        SortExpression="datenumber">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="time" HeaderText="Time" UniqueName="time"
                                        SortExpression="time">
                                    </telerik:GridBoundColumn>
                                   <telerik:GridTemplateColumn HeaderText="Status" >
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="isChecked"   AutoPostBack="true" OnCheckedChanged="CheckChanged"  />
                                            <%--<asp:LinkButton ID="lnk_activeornot" runat="server" CommandName="activeInactive" Text='<%# Bind("status") %>'></asp:LinkButton>--%>
                                             <%--<asp:Label ID="Label2" runat="server" ForeColor='<%# (bool)Eval("active") ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# string.Format("{0}", (bool)Eval("active") ? "Active" : "Inactive") %>'></asp:Label>--%>
                                            <%--<asp:Label ID="lbl_status" runat="server" ></asp:Label> --%>
                                            <asp:Label ID="lbl_ScheduledServiceID" runat="server" Text='<%# Bind("ScheduledServiceID") %>' Visible="false"></asp:Label> 
                                            <asp:Label ID="lbl_statuscheck" runat="server" Text='<%# Bind("Status") %>' ></asp:Label>
                                            <asp:Label ID="lbl_tempid" runat="server" Text='<%# Bind("templateId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                            
                                    </telerik:GridTemplateColumn> 
                                                        
                                </Columns> 
                        </MasterTableView> 
                    <%--<ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True"> </ClientSettings> --%>
                </telerik:RadGrid> 
            </td>
        </tr>
    </table>
    </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_save" EventName="Click"></asp:AsyncPostBackTrigger>
                
            </Triggers>
            </asp:UpdatePanel>
</asp:Content>

