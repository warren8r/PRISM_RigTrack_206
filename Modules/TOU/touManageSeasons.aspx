<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="touManageSeasons.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" src="../../js/jquery-1.9.1.js"></script>
<link rel="stylesheet" href="../../css/jquery-ui-1.10.2.custom.css" />
<link rel="stylesheet" href="../../css/style_Sean.css" />
<style type="text/css">
 .detailTable 
    { 
      margin-left:10px !important;
    } 
 .redBorder td
    { 
      box-shadow: 0px 0px 0px 1px red inset;
    } 
 table.TableStyle td
    {
      text-align:center;
      border:1px;
      border-style: none solid none none;
    }
</style>
<script type="text/javascript">

    function isEmpty() {
        var text1;
        text1 = $('#<%=txtSeasonName1.ClientID%>').val();
        var text2;
        text2 = $('#<%=txtSeasonName2.ClientID%>').val();
        var text3;
        text3 = $('#<%=txtSeasonName3.ClientID%>').val();
        var text4;
        text4 = $('#<%=txtSeasonName4.ClientID%>').val();

        if (text1 == "") {
            $('#<%=chk_Season_Jan1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Feb1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Mar1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Apr1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_May1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jun1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jul1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Aug1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Sep1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Oct1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Nov1.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Dec1.ClientID%>').attr('disabled', 'disabled');
        }
        else {
            $('#<%=chk_Season_Jan1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Feb1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Mar1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Apr1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_May1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jun1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jul1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Aug1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Sep1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Oct1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Nov1.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Dec1.ClientID%>').removeAttr('disabled');
        }
        if (text2 == "") {
            $('#<%=chk_Season_Jan2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Feb2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Mar2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Apr2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_May2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jun2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jul2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Aug2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Sep2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Oct2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Nov2.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Dec2.ClientID%>').attr('disabled', 'disabled');
        }
        else {
            $('#<%=chk_Season_Jan2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Feb2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Mar2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Apr2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_May2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jun2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jul2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Aug2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Sep2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Oct2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Nov2.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Dec2.ClientID%>').removeAttr('disabled');
        }
        if (text3 == "") {
            $('#<%=chk_Season_Jan3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Feb3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Mar3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Apr3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_May3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jun3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jul3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Aug3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Sep3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Oct3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Nov3.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Dec3.ClientID%>').attr('disabled', 'disabled');
        }
        else {
            $('#<%=chk_Season_Jan3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Feb3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Mar3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Apr3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_May3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jun3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jul3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Aug3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Sep3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Oct3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Nov3.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Dec3.ClientID%>').removeAttr('disabled');
        }
        if (text4 == "") {
            $('#<%=chk_Season_Jan4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Feb4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Mar4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Apr4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_May4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jun4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Jul4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Aug4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Sep4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Oct4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Nov4.ClientID%>').attr('disabled', 'disabled');
            $('#<%=chk_Season_Dec4.ClientID%>').attr('disabled', 'disabled');
        }
        else {
            $('#<%=chk_Season_Jan4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Feb4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Mar4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Apr4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_May4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jun4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Jul4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Aug4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Sep4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Oct4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Nov4.ClientID%>').removeAttr('disabled');
            $('#<%=chk_Season_Dec4.ClientID%>').removeAttr('disabled');
        }
    }

    $(window).load(function () {
        isEmpty();
    });
</script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" onload="RadAjaxPanel1_Load">
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"/>
<fieldset style="width:725px; margin: 0 auto;">
<legend style="margin-bottom:-20px;">Time of Use >> Manage Seasons</legend>
    <table>
        <tr style="height:39px;">
            <td>
                Program Type:<br />
                <div style="width:152px; height:24px;">
                <telerik:RadComboBox ID="txtProgType" runat="server" Width="150px" Height="140px" 
                                        EmptyMessage="- Select -" DataSourceID="SqlDataSource3" AutoPostBack="true"
                                        DataTextField="typeName" DataValueField="ID" CausesValidation="false"
                                        MarkFirstMatch="true" onselectedindexchanged="txtProgType_SelectedIndexChanged">
                </telerik:RadComboBox>
                </div>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                    ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="SELECT [ID], [typeName] FROM [progType]"></asp:SqlDataSource>
            </td>
            <td>
                Program Name:<br />
                <div style="width:152px; height:24px;">
                <telerik:RadComboBox ID="txtProgName" runat="server" Width="150px" Height="140px" Enabled="false"
                                        EmptyMessage="- Select -" DataSourceID="SqlDataSource4" AutoPostBack="true" CausesValidation="false"
                                        DataTextField="progName" DataValueField="ID" MarkFirstMatch="true" OnSelectedIndexChanged="txtProgName_SelectedIndexChanged">
                </telerik:RadComboBox>
                </div>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" 
                                    SelectCommand="SELECT [progName], [ID] FROM [touPrograms] WHERE ([progTypeID] = @progTypeID) AND isActive='true' AND ID NOT IN (SELECT PROGRAM_id FROM touSeasonsEP)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtProgType" DefaultValue="" Name="progTypeID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>

    <table border="1" cellpadding="0" cellspacing="0" width="725px" class="TableStyle">
        <tr style="font-size:12px;font-family:Verdana;background-color:#4B6C9E; color:#ffffff; border-color:#4B6C9E;">
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Season Name"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Jan"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Feb"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Mar"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Apr"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="May"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Jun"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Jul"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Aug"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Sep"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Oct"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;">
                <asp:Label runat="server" Text="Nov"></asp:Label>
            </td>
            <td style="border-color:#4B6C9E;border-style:none;">
                <asp:Label runat="server" Text="Dec"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <telerik:RadTextBox ID="txtSeasonName1" runat="server" OnMouseOut="isEmpty();" OnKeyPress="isEmpty();" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jan1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Feb1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Mar1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Apr1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_May1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jun1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jul1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Aug1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Sep1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Oct1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Nov1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td style="border-style:none;">
                <asp:CheckBox ID="chk_Season_Dec1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <telerik:RadTextBox ID="txtSeasonName2" runat="server" OnMouseOut="isEmpty();" OnKeyPress="isEmpty();" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jan2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Feb2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Mar2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Apr2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_May2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jun2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jul2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Aug2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Sep2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Oct2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Nov2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td style="border-style:none;">
                <asp:CheckBox ID="chk_Season_Dec2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <telerik:RadTextBox ID="txtSeasonName3" runat="server" OnMouseOut="isEmpty();" OnKeyPress="isEmpty();" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jan3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Feb3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Mar3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Apr3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_May3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jun3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jul3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Aug3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Sep3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Oct3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Nov3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td style="border-style:none;">
                <asp:CheckBox ID="chk_Season_Dec3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <telerik:RadTextBox ID="txtSeasonName4" runat="server" OnMouseOut="isEmpty();" OnKeyPress="isEmpty();" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jan4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            <td>
                <asp:CheckBox ID="chk_Season_Feb4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            <td>
                <asp:CheckBox ID="chk_Season_Mar4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Apr4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_May4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jun4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Jul4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Aug4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Sep4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Oct4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td>
                <asp:CheckBox ID="chk_Season_Nov4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
            <td style="border-style:none;">
                <asp:CheckBox ID="chk_Season_Dec4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chk_Season_CheckedChanged" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td valign="top">
                <asp:Button runat="server" ID="btnCreateSeason" Width="85px" Text="Create" OnClick="btnCreateSeason_Click" />
                <asp:Button runat="server" ID="btnCancel" Width="85px" Text="Clear" CausesValidation="false"  onclick="btnCancel_Click" /><br />
                <asp:Label runat="server" ID="lbl_message_tab1" ForeColor="Red"></asp:Label>
            </td>
            <td style="height:45px;">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="#F4F4F4" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Names="Arial" Font-Size="X-Small" BorderColor="Black" ForeColor="Red" DisplayMode="List"
                                        Style="padding-top:2px;padding-bottom:2px;vertical-align:middle;min-height:0px;max-height:40px;overflow:hidden; width:190px;" />
            </td>
        </tr>
    </table>
</fieldset><br />

    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtProgType" 
            onservervalidate="txtProgType_ServerValidate" Display="None" ErrorMessage="&bull;&nbsp;You did not select a Program Type." 
            ForeColor="#ABADB3" ValidateEmptyText="True"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtProgName" 
            onservervalidate="txtProgName_ServerValidate" Display="None" ErrorMessage="&bull;&nbsp;You did not select a Program Name." 
            ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator3" runat="server"
            onservervalidate="txtdefineSeason_ServerValidate" Display="None" ErrorMessage="&bull;&nbsp;You must define a season to continue." 
            ForeColor="Red" ValidateEmptyText="True"></asp:CustomValidator>


    <div runat="server" id="divView">
        <telerik:RadGrid runat="server" ID="rgPrograms_View" CellSpacing="0" DataSourceID="SqlDataSource1"
            AllowSorting="True" GridLines="None" OnItemDataBound="rgPrograms_ItemDataBound" style="margin: 0px 10px 0px 10px;">
            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
            <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>

            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
            <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>

                <DetailTables>
                    <telerik:GridTableView DataKeyNames="season_no" DataSourceID="SqlDataSource2" runat="server" AutoGenerateColumns="false" CssClass="detailTable" BorderWidth="2px">
                        <ParentTableRelation>
                            <telerik:GridRelationFields DetailKeyField="ID" MasterKeyField="ID"></telerik:GridRelationFields>
                        </ParentTableRelation>
                        <Columns>
                            <telerik:GridBoundColumn DataField="season_name"
                                FilterControlAltText="Filter season_name column" HeaderText="Season Name" 
                                SortExpression="season_name" UniqueName="season_name">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Months"
                                FilterControlAltText="Filter Months column" HeaderText="Months" 
                                SortExpression="Months" UniqueName="Months">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="Status">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkActive" Checked='<%# true %>' />
                                    <asp:Label ID="Label1" runat="server" ForeColor=<%# true ? System.Drawing.Color.Green : System.Drawing.Color.Red %> Text=<%# string.Format("{0}", true ? "Active" : "Inactive") %>></asp:Label>
                                    <asp:Label runat="server" ID="hidd_ID" Text='<%# Eval("season_no") %>' style="display:none;"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>

                <Columns>
                    <telerik:GridBoundColumn DataField="progName" 
                        FilterControlAltText="Filter progName column" HeaderText="Program Name" 
                        SortExpression="progName" UniqueName="progName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Seasons" 
                        FilterControlAltText="Filter Seasons column" HeaderText="Seasons" 
                        SortExpression="Seasons" UniqueName="Seasons">
                    </telerik:GridBoundColumn>
                </Columns>

            <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
            </EditFormSettings>

            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
            </MasterTableView>

            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

            <FilterMenu EnableImageSprites="False"></FilterMenu>
        </telerik:RadGrid>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" 
        SelectCommand="SELECT r.ID, r.progName, STUFF((SELECT ', '+ a.season_name FROM touSeasonsEP a WHERE a.PROGRAM_ID= r.id GROUP BY a.season_name FOR XML PATH(''), TYPE).value('.','VARCHAR(max)'), 1, 1, '') as Seasons FROM touPrograms r WHERE r.ID IN (SELECT PROGRAM_id FROM touSeasonsEP)">
    </asp:SqlDataSource>

    
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" 
        SelectCommand="SELECT PROGRAM_id, season_no, season_name,
                       substring((CASE WHEN jan='Y' THEN 'January, ' ELSE '' END + 
                       CASE WHEN feb='Y' THEN 'February, ' ELSE '' END  + 
                       CASE WHEN mar='Y' THEN 'March, ' ELSE '' END  + 
                       CASE WHEN apr='Y' THEN 'April, ' ELSE '' END  + 
                       CASE WHEN may='Y' THEN 'May, ' ELSE '' END  + 
                       CASE WHEN jun='Y' THEN 'June, ' ELSE '' END  + 
                       CASE WHEN jul='Y' THEN 'July, ' ELSE '' END  + 
                       CASE WHEN aug='Y' THEN 'August, ' ELSE '' END  + 
                       CASE WHEN sep='Y' THEN 'September, ' ELSE '' END  + 
                       CASE WHEN oct='Y' THEN 'October, ' ELSE '' END   + 
                       CASE WHEN nov='Y' THEN 'November, ' ELSE '' END   + 
                       CASE WHEN dec='Y' THEN 'December, ' ELSE '' END), 1, 
                       len(CASE WHEN jan='Y' THEN 'January, ' ELSE '' END + 
                       CASE WHEN feb='Y' THEN 'February, ' ELSE '' END  + 
                       CASE WHEN mar='Y' THEN 'March, ' ELSE '' END  + 
                       CASE WHEN apr='Y' THEN 'April, ' ELSE '' END  + 
                       CASE WHEN may='Y' THEN 'May, ' ELSE '' END  + 
                       CASE WHEN jun='Y' THEN 'June, ' ELSE '' END  + 
                       CASE WHEN jul='Y' THEN 'July, ' ELSE '' END  + 
                       CASE WHEN aug='Y' THEN 'August, ' ELSE '' END  + 
                       CASE WHEN sep='Y' THEN 'September, ' ELSE '' END  + 
                       CASE WHEN oct='Y' THEN 'October, ' ELSE '' END   + 
                       CASE WHEN nov='Y' THEN 'November, ' ELSE '' END   + 
                       CASE WHEN dec='Y' THEN 'December, ' ELSE '' END)-1) as Months
                       FROM touSeasonsEP
                       WHERE PROGRAM_id=@ID">
        <SelectParameters>
            <asp:SessionParameter Name="ID" SessionField="ID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</telerik:RadAjaxPanel>
</asp:Content>
