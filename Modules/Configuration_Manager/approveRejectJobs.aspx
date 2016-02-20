<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="approveRejectJobs.aspx.cs" Inherits="Modules_Configuration_Manager_approveRejectJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validation_approve() {


                if (document.getElementById("<%=radcombo_pmanager.ClientID %>").value == "") {
                    document.getElementById("<%=lbl_message_pm.ClientID %>").innerHTML = "Select Project Manager to Approve";

                    return false;
                }
            }
        </script>
        <script type="text/javascript">
            function validview() {

                if ($find("<%=radcombo_userstatus.ClientID %>").get_value() == "Select") {
                    document.getElementById("<%=lbl_message.ClientID %>").innerHTML = "Select Status";

                    return false;
                }
            }
            function togglePopupModality1() {

                document.getElementById('<%=lbl_rigtypemsg.ClientID %>').innerHTML = "";
                $find("<%=radtxt_tigtypename.ClientID %>").set_value("");
                $find("<%=radtxt_rigtypedesc.ClientID %>").set_value("");
                $find("<%=RadWindow2.ClientID %>").show();
            } 
            
             
        </script>
     </telerik:RadCodeBlock>
    <style type="text/css">
    .RadUpload .ruCheck 
        { 
            display:none; 
        }
        .RadUpload .ruAdd
        {
            width:200px;
        }
</style>
<script type="text/javascript">
    function downloadFile(fileName, downloadLink1) {

        var downloadLink = $get('downloadFileLink');
        var filePath = "../../Documents/" + fileName;
        downloadLink.href = "DownloadHandler.ashx?fileName=" + fileName + "&filePath=" + filePath;
        downloadLink.style.display = 'block';
        downloadLink.style.display.visibility = 'hidden';
        downloadLink.click();

        return false;

    }
    function downloadFile_opmng(fileName, downloadLink1) {

        var downloadLink_a = $get('downloadFileLink_opmng');
        var filePath = "../../Documents/" + fileName;
        downloadLink_a.href = "DownloadHandler.ashx?fileName=" + fileName + "&filePath=" + filePath;
        downloadLink_a.style.display = 'block';
        downloadLink_a.style.display.visibility = 'hidden';
        downloadLink_a.click();

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
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td align="center">
                <table border="0" cellpadding="0" cellspacing="5">
                    <tr>
                        <td align="left" class="label_display">
                            Select Status<span class="star">*</span><br />
                            <telerik:RadComboBox ID="radcombo_userstatus" runat="server" Width="160px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Select" Value="Select" />
                                    <telerik:RadComboBoxItem Text="Approved" Value="Approved" />
                                    <telerik:RadComboBoxItem Text="Rejected" Value="Rejected" />
                                    <telerik:RadComboBoxItem Text="Pending" Value="Pending" />
                                                
                                </Items>
                            </telerik:RadComboBox>
                                            
                        </td>
                        <td align="left">
                            Submission Start Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_from" Width="130px">
                                <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left">
                            Submission End Date<span class="star">*</span><br />
                            <telerik:RadDatePicker runat="server" ID="radtxt_to" Width="130px">
                                <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                </Calendar>
                                <DateInput ID="DateInput2" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_view" OnClientClick="javascript:return validview();" runat="server" Text="View" onclick="btn_view_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:20px"></td></tr>
        <tr>
            <td align="center">
                <artem:GoogleMap ID="GoogleMap1" Visible="false" style="display:none;" 
                                 EnableOverviewMapControl="false" EnableMapTypeControl="false"
                                 EnableZoomControl="false" EnableStreetViewControl="false"  ShowTraffic="false" runat="server"
                                 ApiVersion="3" EnableScrollWheelZoom="false" IsSensor="false"
                                 DefaultAddress="430 W Warner Rd. , Tempe AZ"
                                 DisableDoubleClickZoom="False" DisableKeyboardShortcuts="True" 
                                 EnableReverseGeocoding="True" Height="650" Zoom="13"  IsStatic="true" 
                                 Key="AIzaSyC2lU7S-IMlNUTu8nHAL97_rL06vKmzfhc" 
                                 MapType="Roadmap"
                                 StaticFormat="Gif" Tilt="45"  Width="550"
                                 StaticScale="2"> </artem:GoogleMap>
                <telerik:RadGrid ID="radgrid_managejobs" runat="server" CellSpacing="0" GridLines="None"   AutoGenerateColumns="False"
                  AllowPaging="true" AllowSorting="true" OnItemCommand="radgrid_managejobs_ItemCommand" OnItemDataBound="radgrid_managejobs_ItemDataBound" >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView  >
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                        <Columns>
                         <telerik:GridBoundColumn DataField="rigtypeid" HeaderText="rigtypeid" SortExpression="rigtypeid" UniqueName="rigtypeid" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Rig&#160;Name" SortExpression="rigtypename" UniqueName="rigtypename">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_rigname" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_rigtypeid" runat="server" Text='<%# Bind("rigtypeid") %>'   Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <%--<telerik:GridBoundColumn DataField="rigtypename" HeaderText="Rig&#160;Name" SortExpression="rigtypename" UniqueName="rigtypename">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="jobsource"
                                        HeaderText="Job Source" SortExpression="jobsource" UniqueName="jobsource" 
                                        >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="jobname" FilterControlAltText="Filter TimeStamp column"
                                        HeaderText="Job Name" SortExpression="jobname" UniqueName="jobname" >
                                        <ItemStyle CssClass="options" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Jobtypename" 
                                        HeaderText="Job Type" SortExpression="Jobtypename" UniqueName="Jobtypename">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="startdate" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="Start Date" SortExpression="startdate" UniqueName="startdate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="enddate" DataFormatString="{0:MM/dd/yyyy}" 
                                        HeaderText="End Date" SortExpression="enddate" UniqueName="enddate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cost" 
                                        HeaderText="Estimated Project Cost($)" SortExpression="cost" UniqueName="cost">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Name"
                                        HeaderText="Customer" SortExpression="Name" UniqueName="Name" 
                                        >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="View Details">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_viewedit" runat="server" CommandName="viewedit" Text="View Details"></asp:LinkButton>
                                            <asp:Label ID="lbl_jobid" runat="server" Visible="false" Text='<%# Bind("jid") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridHyperLinkColumn AllowSorting="False"  HeaderText="View Maps"
                                         DataNavigateUrlFields="primaryLatLong" 
                                         DataNavigateUrlFormatString="http://maps.google.com/?q={0}" 
                                         FilterControlAltText="Filter gisLink column" Target="_new" Text="View Map" 
                                         UniqueName="gisLink">
                                        <ItemStyle ForeColor="Blue" />
                                    </telerik:GridHyperLinkColumn>
                                    
                                </Columns>
                                
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <%--<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>--%>
                            </MasterTableView>
                            <%--<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                            <ExportSettings SuppressColumnDataFormatStrings="false" ExportOnlyData="true">
                                <Excel Format="Biff" />
                            </ExportSettings>--%>
                        </telerik:RadGrid>
                <%--<asp:SqlDataSource ID="MasterTableData" runat="server" connectionString="<%$ databaseExpression:client_database %>" 
                            providerName="System.Data.SqlClient"
                            SelectCommand="SELECT (firstName+' '+lastName) as Username,j.jobType as Jobtypename,* from manageJobOrders m,PrsimCustomers c,Users u,jobTypes j where c.ID=m.Customer and m.opManagerId=u.userID and m.jobtype=j.jobtypeid order by jid desc"

                    >
                    <SelectParameters>
                        <asp:ControlParameter ControlID="" Name="" DbType="String" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
            </td>
        </tr>
        <tr>
            <td style="height:20px"></td>
        </tr>
        <tr>
            <td id="id_viewpart" style="border:solid 1px #000000" runat="server" visible="false">
                <table border="0">
                    <tr>
                        <td align="right">
                            <span style="background-color:#FFFF99;height:10px;width:30px">
                                &nbsp;
                            </span> Color Indicates Editable Fields
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table border="0">
                                <tr>
                                    <td>
                                        Active:
                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                                         />
                                    </td>
            
                                    <td>
                                        Job Name: <span style="color:Red; font-style:bold;">*</span><br />
                                        <telerik:RadTextBox runat="server" ReadOnly="True" ID="radtxt_jobname"></telerik:RadTextBox>
                                                 
                           
                                    </td>
            
                                    <td>
                                        Job ID: <span style="color:Red; font-style:bold;">*</span><br />
                                        <telerik:RadMaskedTextBox runat="server" ID="txtAssetNumber"
                                                                  LabelWidth="64px" 
                                                                  Width="100px" EmptyMessage="0000000000" Mask="##########"/>
                            
                                        
                            
                                    </td>
            
                                    <td>
                                        Job Type:<br />
                                        <telerik:RadComboBox ID="radcombo_jobtype" DataSourceID="SqlDataSource1" DataTextField="jobtype" DataValueField="jobtypeid" runat="server" EmptyMessage="- Select -"></telerik:RadComboBox>
                                        
                                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                SelectCommand="SELECT * FROM [jobTypes]"></asp:SqlDataSource>
                                 
                                    </td>
                                    <td>
                                        Rig Name:<br />
                                        <telerik:RadComboBox ID="radcombo_rigtype" runat="server"  EmptyMessage="- Select -"  ></telerik:RadComboBox>
                                        <asp:LinkButton ID="lnk_rigtype" ForeColor="Blue" runat="server" OnClientClick="togglePopupModality1(); return false;">Create</asp:LinkButton>
                                        <telerik:RadWindowManager ID="RadWindowManager2"  runat="server"   
                                            Modal="true" Animation="Resize" >
                                        <Windows>
                                                <telerik:RadWindow ID="RadWindow2" runat="server"  Width="400px"
                                                    Height="400px" >
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_rigtypemsg" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Rig Name:<br />
                                                                    <telerik:RadTextBox ID="radtxt_tigtypename" runat="server"></telerik:RadTextBox> 
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Rig Description:<br />
                                                                    <telerik:RadTextBox ID="radtxt_rigtypedesc" Height="100px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <%--<telerik:RadButton ID="RadButton2" runat="server" Text="Save" AutoPostBack="true" OnClick="Button2_Click">
                                                                    </telerik:RadButton>--%>
                                                                    <asp:Button ID="btn_createrig" Text="Create"  runat="server" OnClick="btn_createrig_Click">
                                                                    </asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                        
                                                </telerik:RadWindow>
                                            </Windows>
                                            </telerik:RadWindowManager>
                                        <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                >
                                                
                                        </asp:SqlDataSource>--%>
                                        <%--SelectCommand="SELECT * FROM [RigTypes] where rigtypeid not in (select rigtypeid from managejoborders where status='Approved') "--%>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>Start&#160;Date: <br /><telerik:RadDatePicker ID="date_start"  BackColor="#FFFF99" runat="server" DateInput-DisplayDateFormat="MM/dd/yyyy"  Width="185px" SelectedDate="06/01/2013"></telerik:RadDatePicker></td>
                 
                                    <td>End&#160;Date:<br /><telerik:RadDatePicker ID="date_stop"  BackColor="#FFFF99" runat="server" DateInput-DisplayDateFormat="MM/dd/yyyy" Width="185px"></telerik:RadDatePicker></td>
                                    <td>
                                        Estimated Project Cost($):<br />
                                        <telerik:RadTextBox runat="server" ID="radtxt_cost"  BackColor="#FFFF99" />
                                    </td>
                                    <td>Customer:<br />
                                        <telerik:RadComboBox ID="radcombo_customer" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="ID" EmptyMessage="- Select -"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                SelectCommand="SELECT * FROM [PrsimCustomers]"></asp:SqlDataSource>
                                    </td>
                                    <td></td>
                                </tr>
                    
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                             <table border="0" width="100%">
                                <tr>
                                    <td valign="top" width="50%">
                                        <!-- start primary contact -->
                                        <fieldset class="gis-form">
                                            <legend>
                                                <asp:Label runat="server" ID="lblLabel" />Project Address
                                            </legend>
                                            <table width="100%"  border="0">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" style="">
                                                            <tr>
                                                                <td valign="top" >

                                                                    <table>
                                                                        <tr>
                                                                            <td valign="top" width="90px">
                                                                                Address 1: <span style="color:Red; font-style:bold;">*</span>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtprimaryAddress1" BackColor="#FFFF99" runat="server" LabelWidth="64px" 
                                                                                                     Width="120px" />
                                                                    
                                                                            </td>
                                                                            <td>
                                                                                Country:
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadDropDownList ID="ddlPrimaryCountry" runat="server"  
                                                                                                              DataSourceID="SqlGetCountry" DataTextField="name" 
                                                                                                         DataValueField="code" SelectedText="- Select -">
                                                                                    <Items>
                                                                                        <telerik:DropDownListItem Text="- Select -" Value="" Selected="True" />
                                                                                    </Items>
                                                                                </telerik:RadDropDownList>
                                                                                <asp:Label ID="lblSelectedPrimaryCountry" Visible="false" runat="server"  />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" class="style5">
                                                                                Address 2:
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtprimaryAddress2"  BackColor="#FFFF99" runat="server" LabelWidth="64px" 
                                                                                                     Width="120px" />
                                                                            </td>
                                                                            <td>
                                                                                State : <span style="color:Red; font-style:bold;">*</span>&nbsp;
                                                                                <asp:Label ID="lblSelectedPrimaryState" Visible="false" runat="server"  />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Panel ID="pnlUS" runat="server">
                                                                                    <%--<asp:DropDownList runat="server" id="ddlPrimaryState" AutoPostBack="True" 
                                                                                    SelectedValue='<%# Eval("primaryState") %>' 
                                                                                    AppendDataBoundItems="True" DataSourceID="SqlGetStates" DataTextField="name" 
                                                                                    DataValueField="code">
                                                                                    <asp:ListItem Text="- Select -" Value="" />
                                                                                    </asp:DropDownList>--%>
                                                                                    <telerik:RadDropDownList ID="ddlPrimaryState" runat="server" DataSourceID="SqlGetStates" DataTextField="name" 
                                                                                                                DataValueField="code" AppendDataBoundItems="true" AutoPostBack="true" DropDownHeight="200px" DropDownWidth="180px"
                                                                                                                SelectedText="- Select -"  
                                                                                                                SkinID="Hay">
                                                                                        <Items>
                                                                                            <telerik:DropDownListItem Selected="True" Text="- Select -" />
                                                                                        </Items>
                                                                                    </telerik:RadDropDownList>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="pnlNonUS" runat="server" Visible="false">
                                                                                    <telerik:RadTextBox ID="txtprimaryState" runat="server" LabelWidth="64px" 
                                                                                                        Width="160px" >
                                                                                    </telerik:RadTextBox>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                City :<span style="color:Red; font-style:bold;">*</span>
                                                                            </td>
                                                                            <td valign="top">

                                                                                <telerik:RadTextBox ID="txtprimaryCity" runat="server"  BackColor="#FFFF99" LabelWidth="64px" 
                                                                                                    Width="120px" />
                                                                    
                                                                            </td>
                                                                            <td>
                                                                                Zip/Postal:
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtprimaryPostalCode"  BackColor="#FFFF99" runat="server" LabelWidth="30px" 
                                                                                                    Width="75px" />

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                GIS:
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtPrimaryGIS" runat="server" 
                                                                                                    EmptyMessage=" Input address.." Height="20px" LabelWidth="64px" 
                                                                                                    style="padding:0px;"  Width="120px" />
                                                                            </td>
                                                                            <td>
                                                                            <%-- <telerik:RadWindow runat="server" ID="radWindowMap" Title="GIS Information">
                                                                            <ContentTemplate>
                                                                            MAP HERE...
                                                                            </ContentTemplate>
                                                                            </telerik:RadWindow>--%>
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                First : <span style="color:Red; font-style:bold;">*</span>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtPrimaryFirst"  BackColor="#FFFF99" runat="server" LabelWidth="48px" 
                                                                                                     Width="120px" />
                                                                    
                                                                            </td>
                                                                            <td>
                                                                                Last : &nbsp;<span style="color:Red; font-style:bold;">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtPrimaryLast"  BackColor="#FFFF99" runat="server" LabelWidth="48px" 
                                                                                                     Width="120px" />
                                                                    
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                Phone:<span style="color:Red; font-style:bold;">*</span>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtPrimaryPhone1"  BackColor="#FFFF99" runat="server" LabelWidth="48px" 
                                                                                                     Width="120px" />
                                                                    
                                                                            </td>
                                                                            <td>
                                                                                Fax:
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtPrimaryPhone2" runat="server"  BackColor="#FFFF99" LabelWidth="48px" 
                                                                                                     Width="120px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                Email:
                                                                            </td>
                                                                            <td colspan="3" valign="top">
                                                                                <telerik:RadTextBox ID="txtPrimaryEmail" runat="server"  BackColor="#FFFF99" LabelWidth="88px" 
                                                                                                     Width="220px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>

                                                        </table>

                                                    </td>
                                                </tr>

                                            </table>

                                            <asp:SqlDataSource ID="SqlGetCountry" 
                                                                ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                                SelectCommand="SELECT code, name FROM countryCode"></asp:SqlDataSource>

                                            <asp:SqlDataSource ID="SqlGetStates" runat="server" 
                                                                ConnectionString="<%$ databaseExpression:client_database %>" 
                                                                SelectCommand="SELECT code, name FROM stateCode"></asp:SqlDataSource>

                                        </fieldset>
                                        <!-- end primary contact -->
                                    </td>
                                    <td width="50%" valign="top">
                                        <!-- secondary -->
                                        <fieldset class="gis-form">
                                            <legend>Account Info</legend>
                                            <table width="100%" cellpadding="0" border="0">
                                                <tr>
                                                    <td valign="top">

                                                        <table border="0" style="" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td valign="top" colspan="3">
                                                                                &nbsp;
                                                                                <asp:CheckBox ID="chk_sameasprimary" runat="server" AutoPostBack="True" 
                                                                                                 Font-Names="Arial" 
                                                                                                Font-Size="10px" 
                                                                                                Text="Same As Project Address" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" width="76px">
                                                                                Address 1:
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtSecondaryAddress1"  BackColor="#FFFF99" runat="server" LabelWidth="64px" 
                                                                                                     Width="120px" />
                                                                            </td>
                                                                            <td>
                                                                                Country:
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadDropDownList ID="ddlSecondaryCountry" runat="server" 
                                                                                                  
                                                                                             
                                                                                             
                                                                                                          DataSourceID="SqlGetCountryInsert" DataTextField="name" 
                                                                                                         DataValueField="code" SelectedText="- Select -">
                                                                                    <Items>
                                                                                        <telerik:DropDownListItem 
                                                                                            Text="- Select -" Value="" Selected="True" />
                                                                                    </Items>
                                                                                </telerik:RadDropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" class="style5">
                                                                                Address 2:
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtSecondaryAddress2"  BackColor="#FFFF99" runat="server" LabelWidth="64px" 
                                                                                                     Width="120px" />
                                                                            </td>
                                                                            <td>
                                                                                State : &nbsp;
                                                                            </td>
                                                                            <td>
                                                                    
                                                                                    <telerik:RadDropDownList ID="ddlSecondaryState" runat="server" 
                                                                                                                SelectedText="- Select -" 
                                                                                        DataSourceID="SqlGetStatesInsert" AppendDataBoundItems="True" 
                                                                                        DataTextField="name" DataValueField="code" 
                                                                                                                SkinID="Hay">
                                                                                        <Items>
                                                                                            <telerik:DropDownListItem Selected="True" Text="- Select -" />
                                                                                        </Items>
                                                                                    </telerik:RadDropDownList>
                                                                    
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                City :
                                                                            </td>
                                                                            <td valign="top">

                                                                                <telerik:RadTextBox ID="txtSecondaryCity"  BackColor="#FFFF99" runat="server" LabelWidth="64px" 
                                                                                                    Width="120px" />
                                                                            </td>
                                                                            <td colspan="1">
                                                                                Zip/Postal:
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtSecondaryPostalCode"  BackColor="#FFFF99" runat="server" LabelWidth="30px" 
                                                                                                    Width="75px" />

                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                First :
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtSecondaryFirst"  BackColor="#FFFF99" runat="server" LabelWidth="48px" 
                                                                                                     Width="120px" />
                                                                            </td>
                                                                            <td colspan="1">
                                                                                Last :
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtSecondaryLast"  BackColor="#FFFF99" runat="server" LabelWidth="48px" 
                                                                                                     Width="120px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                Phone:
                                                                            </td>
                                                                            <td valign="top">
                                                                                <telerik:RadTextBox ID="txtSecondaryPhone1"  BackColor="#FFFF99" runat="server" LabelWidth="48px" 
                                                                                                    Width="120px" />
                                                                            </td>
                                                                            <td colspan="1">
                                                                                Fax:
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtSecondaryPhone2"  BackColor="#FFFF99" runat="server" LabelWidth="48px" 
                                                                                                     Width="120px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style5" valign="top">
                                                                                Email:
                                                                            </td>
                                                                            <td colspan="3" valign="top">
                                                                                <telerik:RadTextBox ID="txtSecondaryEMail"  BackColor="#FFFF99" runat="server" LabelWidth="88px" 
                                                                                                     Width="220px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>

                                            </table>
                                            <asp:SqlDataSource ID="SqlGetCountryInsert" 
                                                                ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                                                                SelectCommand="SELECT code, name FROM countryCode"></asp:SqlDataSource>

                                            <asp:SqlDataSource ID="SqlGetStatesInsert" runat="server" 
                                                                ConnectionString="<%$ databaseExpression:client_database %>" 
                                                                SelectCommand="SELECT code, name FROM stateCode"></asp:SqlDataSource>
                                        </fieldset>
                                        <!-- end secondary -->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <table>
                                <tr>
                                    
                                    <td>
                                        <fieldset >
                                        <legend>
                                            <asp:Label runat="server" ID="Label1" />Sales Documents
                                        </legend>
                                        <asp:Label ID="lbl_docid" runat="server" Visible="false"></asp:Label>
                                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" CssClass="mdmGrid active"
                                            CellSpacing="0" DataSourceID="eventsDocs" GridLines="None" Width="80%" OnItemCommand="RadGrid1_ItemCommand" >
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="jid" DataSourceID="eventsDocs">
                                                    <Columns>
                                                    <telerik:GridBoundColumn DataField="jid" Visible="false"
                                                        HeaderText="jid" ReadOnly="True" SortExpression="eventCode"
                                                        UniqueName="jid">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DocumentDisplayName"
                                                        HeaderText="Sale Order Documents" SortExpression="DocumentDisplayName" UniqueName="DocumentDisplayName">
                                                    </telerik:GridBoundColumn> 
                                        
                    
                                        
                                        
                                                    <telerik:GridTemplateColumn HeaderText="Download" >
                                                        <ItemTemplate> 
                                                         <asp:LinkButton ID="lnk_download" runat="server"  OnClientClick='<%# String.Format("downloadFile(\"{0}\",\"{1}\");", Eval("DocumentName"),this) %>' Text="Download"></asp:LinkButton> 
                                                        <a id="downloadFileLink"> </a>                          
                                                        <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("jid") %>' style="display:none;"></asp:Label>
                                                        <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("documentid") %>' style="display:none;"></asp:Label>
                                                        <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentName") %>' style="display:none;"></asp:Label>                         
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>  
                                                    
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            </MasterTableView>
                                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            <FilterMenu EnableImageSprites="False">
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                         <asp:SqlDataSource ID="eventsDocs" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                                            SelectCommand="SELECT  etod.jid,etod.documentid,etod.UserID,etod.UploadedDate,(u.firstName+' '+u.lastName) as UserName, e.jid,d.DocumentDisplayName,d.DocumentName from 
                                            JobOrderDocuments etod, manageJobOrders e, documents d, Users u where u.userID=etod.UserID and e.jid=etod.jid and d.DocumentID=etod.DocumentID and 
                                            e.jid=@jid and etod.type='SA'  order by etod.jid desc">
                                             <SelectParameters>
                                                <asp:ControlParameter ControlID="lbl_docid" Name="jid" DbType="Int32" />
                                                 <%--<asp:QueryStringParameter Name="jid" QueryStringField="jid" DefaultValue="1" DbType="Int32" />--%>
                                             </SelectParameters>
                                        </asp:SqlDataSource>
                                        </fieldset>
                                    </td>
                                    <td  valign="top">
                                        <fieldset >
                                            <legend>
                                                <asp:Label runat="server" ID="Label3" />Sale Notes
                                            </legend>
                            
                                        <telerik:RadTextBox id="radtxt_notes" runat="server" Height="70px" Width="380px" TextMode="MultiLine"></telerik:RadTextBox>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td align="left" valign="top">
                                        <br />
                                        Project Manager:<br />
                                        <telerik:RadComboBox ID="radcombo_pmanager" runat="server" DataSourceID="SqlDataSource4" DataTextField="Name" DataValueField="userID" EmptyMessage="- Select -"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                                SelectCommand="select distinct userID,(firstName+' '+lastName) as Name from users, PrsimProjectInfo where userRoleID=Project_MGMT"></asp:SqlDataSource>
                                    </td>
                                    <td  valign="top">
                                        <fieldset >
                                            <legend>
                                                <asp:Label runat="server" ID="Label4" />Operations Manager Notes
                                            </legend>
                            
                                        <telerik:RadTextBox id="radtxt_opnotes" runat="server" Height="70px" Width="380px" TextMode="MultiLine"></telerik:RadTextBox>
                                        </fieldset>
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="padding-left:10px; width:920px">
                        <legend>
                            <asp:Label runat="server" ID="Label2" />Operations Manager Documents
                        </legend>
                            <table>
                                <tr>
                                    <td  align="left">
                            
                                        <telerik:RadUpload ID="radupload_docs" runat="server" InputSize="60" Width="600px" ControlObjectsVisibility="CheckBoxes,RemoveButtons,AddButton">
                                            <Localization Add="Add More" />
                                        </telerik:RadUpload>
                            
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_opmgrdocid" runat="server" Visible="false"></asp:Label>
                                        <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" CssClass="mdmGrid active"
                                            CellSpacing="0"  GridLines="None" Width="80%" OnItemCommand="RadGrid2_ItemCommand" >
                                            
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="jid" >
                                                    <Columns>
                                                    <telerik:GridBoundColumn DataField="jid" Visible="false"
                                                        HeaderText="jid" ReadOnly="True" SortExpression="jid"
                                                        UniqueName="jid">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DocumentDisplayName" FilterControlAltText="Filter categoryName column"
                                                        HeaderText="Op. Manager Documents" SortExpression="DocumentDisplayName" UniqueName="DocumentDisplayName">
                                                    </telerik:GridBoundColumn> 
                                        
                    
                                        
                                        
                                                    <telerik:GridTemplateColumn HeaderText="Download" >
                                                        <ItemTemplate> 
                                                         <asp:LinkButton ID="lnk_download_new" runat="server"  OnClientClick='<%# String.Format("downloadFile_opmng(\"{0}\",\"{1}\");", Eval("DocumentName"),this) %>' Text="Download"></asp:LinkButton> 
                                                        <a id="downloadFileLink_opmng"> </a>                        
                                                        <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("jid") %>' style="display:none;"></asp:Label>
                                                        <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("documentid") %>' style="display:none;"></asp:Label>
                                                        <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentName") %>' style="display:none;"></asp:Label>                         
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>  
                                                    
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            </MasterTableView>
                                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            <FilterMenu EnableImageSprites="False">
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                         <%--<asp:SqlDataSource ID="opmngdocs" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                                            SelectCommand="SELECT  etod.jid,etod.documentid,etod.UserID,etod.UploadedDate,(u.firstName+' '+u.lastName) as UserName, e.jid,d.DocumentDisplayName,d.DocumentName from 
                                            JobOrderDocuments etod, manageJobOrders e, documents d, Users u where u.userID=etod.UserID and e.jid=etod.jid and d.DocumentID=etod.DocumentID and 
                                            e.jid=@jid and etod.type='JO'  order by etod.jid desc">
                                             <SelectParameters>
                                                <asp:ControlParameter ControlID="lbl_opmgrdocid" Name="jid" DbType="Int32" />
                                                 
                                             </SelectParameters>
                                        </asp:SqlDataSource>--%>
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                        </td>
                    </tr>

                    <tr>
                        
                       <td align="left">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lbl_message_pm" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td align="center">
                                        <table >
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btn_approve" runat="server" Text="Approve" OnClientClick="javascript:return validation_approve();" OnClick="btn_approve_Click" />
                                                </td>
                                                <td align="center">
                                                    <asp:Button ID="btn_reject" runat="server" Text="Reject" OnClick="btn_reject_Click"  />
                                                </td>
                                                 <td align="center">
                                                    <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" />
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
        
        
    </table>
    </ContentTemplate>
    <Triggers>
         <asp:PostBackTrigger ControlID="btn_approve" />       
        <asp:AsyncPostBackTrigger ControlID="btn_reject" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click"></asp:AsyncPostBackTrigger>
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

