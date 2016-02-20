<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageSales.aspx.cs" Inherits="Modules_Configuration_Manager_assetsCreatejobOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
    .RadUpload .ruCheck 
        { 
            display:none; 
        }
        .RadUpload .ruAdd
        {
            width:200px;
        }
        .SelectedStyle 
        { 
            background-color: Green !important; 
        }
</style>
<telerik:RadCodeBlock runat="server" ID="rdbScripts">
          <script type="text/javascript">
              function togglePopupModality() {

                  
                  $find("<%=RadWindow_ContentTemplate.ClientID %>").show();
              } 

              
              
          </script>
          <script type="text/javascript">
              // on upload button click temporarily disables ajax to perform
              // upload actions
              function conditionalPostback(sender, args) {
                  if (args.get_eventTarget() == "<%= btnInsert.UniqueID %>") {
                      args.set_enableAjax(false);
                  }
              }
              function OnPopupClosed(sender, args) {
                  $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
              } 
 </script>
 
     </telerik:RadCodeBlock>
     <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
          <style type="text/css">
               div.infoBox
               {
                    width: 400px;
                    margin-right: 15px;
                    vertical-align: top;
               }
               #ContentTemplateZone, #NavigateUrlZone
               {
                    height: 400px;
               }
               .contText, .contButton
               {
                    margin: 0;
                    padding: 0 0 10px 5px;
                    font-size: 12px;
               }
               .RadWindow_Black .contText
               {
                    color: #fff;
               }
          </style>
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

    function sameaddress(objcheckbox) {
        if (objcheckbox.checked) {
            $find("<%=txtSecondaryAddress1.ClientID %>").set_value($find("<%=txtprimaryAddress1.ClientID %>").get_textBoxValue());
            $find("<%=txtSecondaryAddress2.ClientID %>").set_value($find("<%=txtprimaryAddress2.ClientID %>").get_textBoxValue());

            $find("<%=txtSecondaryCity.ClientID %>").set_value($find("<%=txtprimaryCity.ClientID %>").get_textBoxValue());
            $find("<%=txtSecondaryPostalCode.ClientID %>").set_value($find("<%=txtprimaryPostalCode.ClientID %>").get_textBoxValue());
            $find("<%=txtSecondaryFirst.ClientID %>").set_value($find("<%=txtPrimaryFirst.ClientID %>").get_textBoxValue());
            $find("<%=txtSecondaryLast.ClientID %>").set_value($find("<%=txtPrimaryLast.ClientID %>").get_textBoxValue());
            $find("<%=txtSecondaryPhone1.ClientID %>").set_value($find("<%=txtPrimaryPhone1.ClientID %>").get_textBoxValue());
            $find("<%=txtSecondaryPhone2.ClientID %>").set_value($find("<%=txtPrimaryPhone2.ClientID %>").get_textBoxValue());
            $find("<%=txtSecondaryEMail.ClientID %>").set_value($find("<%=txtPrimaryEmail.ClientID %>").get_textBoxValue());
            var c1 = $find("<%= ddlPrimaryCountry.ClientID %>");
            var c2 = $find("<%= ddlSecondaryCountry.ClientID %>");
            c2.findItemByValue(c1.get_selectedItem().get_value()).select();

            var s1 = $find("<%= ddlPrimaryState.ClientID %>");
            var s2 = $find("<%= ddlSecondaryState.ClientID %>");
            s2.findItemByValue(s1.get_selectedItem().get_value()).select();

            //c2.get_selectedItem().set_value() = c1.get_selectedItem().get_value();
            //document.getElementById('ctl00_ContentPlaceHolder1_ddlSecondaryCountry').value = document.getElementById('ctl00_ContentPlaceHolder1_ddlPrimaryCountry').value;

        }
        else {
            $find("<%=txtSecondaryAddress1.ClientID %>").set_value("");
            $find("<%=txtSecondaryAddress2.ClientID %>").set_value("");

            $find("<%=txtSecondaryCity.ClientID %>").set_value("");
            $find("<%=txtSecondaryPostalCode.ClientID %>").set_value("");
            $find("<%=txtSecondaryFirst.ClientID %>").set_value("");
            $find("<%=txtSecondaryLast.ClientID %>").set_value("");
            $find("<%=txtSecondaryPhone1.ClientID %>").set_value("");
            $find("<%=txtSecondaryPhone2.ClientID %>").set_value("");
            $find("<%=txtSecondaryEMail.ClientID %>").set_value("");

            var c2 = $find("<%= ddlSecondaryCountry.ClientID %>");
            c2.findItemByValue("AD").select();


            var s2 = $find("<%= ddlSecondaryState.ClientID %>");

            s2.findItemByValue("AK").select();
        }
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
            <td>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" onajaxrequest="RadAjaxManager1_AjaxRequest"> 
                  <AjaxSettings> 
                        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1"> 
                        <UpdatedControls> 
                               <telerik:AjaxUpdatedControl ControlID="radcombo_jobtype" /> 
                        </UpdatedControls> 
                        </telerik:AjaxSetting> 
                  </AjaxSettings> 
            </telerik:RadAjaxManager>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0">
                    <tr>
                        <td>
                            Active:
                            <asp:CheckBox ID="CheckBox1" Checked="true" runat="server" 
                                             />
                        </td>
            
                        <td>
                            Job Name: <span style="color:Red; font-style:bold;">*</span><br />
                            <telerik:RadTextBox runat="server" ID="radtxt_jobname"></telerik:RadTextBox>
                                                 
                           
                        </td>
            
                        <td runat="server" visible="false">
                            Job ID: <span style="color:Red; font-style:bold;">*</span><br />
                            <telerik:RadMaskedTextBox runat="server" ID="txtAssetNumber"
                                                      LabelWidth="64px" 
                                                      Width="100px" EmptyMessage="0000000000" Mask="##########"/>
                        <%--    
                            <asp:LinkButton ID="lnl_generateassetid" CausesValidation="false" ForeColor="Blue" runat="server" onclick="lnl_generateassetid_Click" 
                                            >Generate ID #</asp:LinkButton>--%>
                            
                        </td>
            
                        <td>
                            Job Type:<br />
                            <telerik:RadComboBox ID="radcombo_jobtype" DataSourceID="SqlDataSource1" DataTextField="jobtype" DataValueField="jobtypeid" runat="server" EmptyMessage="- Select -"></telerik:RadComboBox>
                            
                                 
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton2" ForeColor="Blue" runat="server" OnClientClick="togglePopupModality(); return false;">Create New Job Type</asp:LinkButton>
                            
                         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="SELECT * FROM [jobTypes]"></asp:SqlDataSource>
                        </td>
                        
                        
                    </tr>
                    <tr>
                        <td>Start&#160;Date: <br /><telerik:RadDatePicker ID="date_start" runat="server" DateInput-DisplayDateFormat="MM/dd/yyyy"  Width="185px" ></telerik:RadDatePicker></td>
                 
                        <td>End&#160;Date:<br /><telerik:RadDatePicker ID="date_stop" runat="server" DateInput-DisplayDateFormat="MM/dd/yyyy" Width="185px"></telerik:RadDatePicker></td>
                        <td>
                            Estimated Project Cost($):<br />
                            <telerik:RadTextBox runat="server" ID="radtxt_cost" />
                        </td>
                        <td>Customer:<br />
                            <telerik:RadComboBox ID="radcombo_customer" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="ID" EmptyMessage="- Select -"></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="SELECT * FROM [PrsimCustomers]"></asp:SqlDataSource>
                        </td>
                        <td>
                            Operation Manager:<br />
                            <telerik:RadComboBox ID="radcombo_opmngers" runat="server" DataSourceID="SqlDataSource3" DataTextField="Name" DataValueField="uservalue" EmptyMessage="- Select -"></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="select distinct (CAST(userID AS VARCHAR(11))+'~'+email) as uservalue,(firstName+' '+lastName) as Name from users, PrsimProjectInfo where userRoleID=Senior_MGMT"></asp:SqlDataSource>
                        </td>
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
                                                                    <telerik:RadTextBox ID="txtprimaryAddress1" runat="server" LabelWidth="64px" 
                                                                                         Width="120px" />
                                                                    
                                                                </td>
                                                                <td>
                                                                    Country:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="ddlPrimaryCountry" runat="server" 
                                                                                                  DataSourceID="SqlGetCountry" DataTextField="name" 
                                                                                             DataValueField="code" DropDownHeight="200px" SelectedText="United States">
                                                                        
                                                                    </telerik:RadComboBox>
                                                                    <asp:Label ID="lblSelectedPrimaryCountry" Visible="false" runat="server"  />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" class="style5">
                                                                    Address 2:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtprimaryAddress2" runat="server" LabelWidth="64px" 
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
                                                                                                    DataValueField="code"  DropDownHeight="200px" DropDownWidth="180px"
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

                                                                    <telerik:RadTextBox ID="txtprimaryCity" runat="server" LabelWidth="64px" 
                                                                                        Width="120px" />
                                                                    
                                                                </td>
                                                                <td>
                                                                    Zip/Postal:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtprimaryPostalCode" runat="server" LabelWidth="30px" 
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
                                                                <td>
                                                                    <telerik:RadButton ID="lnkBtnGatherGISPrimary" runat="server" 
                                                                                        ButtonType="LinkButton" CausesValidation="false" 
                                                                                        onclick="lnkBtnGatherGIS_Click" RegisterWithScriptManager="true" 
                                                                                       SingleClick="true" SingleClickText="Please wait..." style="top:3px;"
                                                                                        Text="Gather GIS" />
                                                                    <asp:TextBox ID="txtMatchDB" runat="server" 
                                                                                     Visible="False"></asp:TextBox>
                                                                    <asp:HyperLink ID="hypPrimaryMapLink" runat="server" 
                                                                         
                                                                                    style="color:Blue;" Target="_new" Visible="False">View Map</asp:HyperLink>
                                                                    <asp:Label ID="lblPrimaryMatch" runat="server" Font-Size="10px" 
                                                                                ></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    First : <span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryFirst" runat="server" LabelWidth="48px" 
                                                                                         Width="120px" />
                                                                    
                                                                </td>
                                                                <td>
                                                                    Last : &nbsp;<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtPrimaryLast" runat="server" LabelWidth="48px" 
                                                                                         Width="120px" />
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Phone:<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryPhone1" runat="server" LabelWidth="48px" 
                                                                                         Width="120px" />
                                                                    
                                                                </td>
                                                                <td>
                                                                    Fax:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtPrimaryPhone2" runat="server" LabelWidth="48px" 
                                                                                         Width="120px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Email:<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td colspan="3" valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryEmail" runat="server" LabelWidth="88px" 
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
                            <fieldset class="gis-form" style="background-color:#f1f1f1">
                                <legend>Account Info</legend>
                                <table width="100%" cellpadding="0"  border="0">
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
                                                                    <asp:CheckBox ID="chk_sameasprimary" runat="server" onclick="return sameaddress(this);" 
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
                                                                    <telerik:RadTextBox ID="txtSecondaryAddress1" runat="server" LabelWidth="64px" 
                                                                                         Width="120px" />
                                                                </td>
                                                                <td>
                                                                    Country:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="ddlSecondaryCountry" runat="server" 
                                                                                                  
                                                                                             
                                                                                             
                                                                                              DataSourceID="SqlGetCountryInsert" DataTextField="name" 
                                                                                             DataValueField="code" SelectedText="United States">
                                                                        
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" class="style5">
                                                                    Address 2:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryAddress2" runat="server" LabelWidth="64px" 
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

                                                                    <telerik:RadTextBox ID="txtSecondaryCity" runat="server" LabelWidth="64px" 
                                                                                        Width="120px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Zip/Postal:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryPostalCode" runat="server" LabelWidth="30px" 
                                                                                        Width="75px" />

                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    First :<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryFirst" runat="server" LabelWidth="48px" 
                                                                                         Width="120px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Last :<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryLast" runat="server" LabelWidth="48px" 
                                                                                         Width="120px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Phone:<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryPhone1" runat="server" LabelWidth="48px" 
                                                                                        Width="120px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Fax:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryPhone2" runat="server" LabelWidth="48px" 
                                                                                         Width="120px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Email:<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td colspan="3" valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryEMail" runat="server" LabelWidth="88px" 
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
            <td style="background-color:#f1f1f1">
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <fieldset class="gis-form">
                        <legend>
                            <asp:Label runat="server" ID="Label1" />Attachments
                        </legend>
                            <table>
                                <tr>
                                    <td  align="left" style="width:400px">
                            
                                        <telerik:RadUpload ID="radupload_docs" runat="server" InputSize="60" Width="400px" ControlObjectsVisibility="CheckBoxes,RemoveButtons,AddButton">
                                            <Localization Add="Add More" />
                                        </telerik:RadUpload>
                            
                                    </td>
                                    
                                    

                                </tr>
                            </table>
                            </fieldset>
                        </td>
                        <td  valign="top">
                            <asp:Label ID="lbl_docid" runat="server" Visible="false"></asp:Label>
                            
                                <fieldset class="gis-form">
                                <legend>
                                    <asp:Label runat="server" ID="Label2" />Existing Documents
                                </legend>
                            <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" 
                            CssClass="mdmGrid active"
                                CellSpacing="0" DataSourceID="eventsDocs" GridLines="None"   >
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="jid" DataSourceID="eventsDocs">
                                        <Columns>
                                        <telerik:GridBoundColumn DataField="jid" Visible="false"
                                            HeaderText="jid" ReadOnly="True" SortExpression="eventCode"
                                            UniqueName="jid">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DocumentDisplayName" FilterControlAltText="Filter categoryName column"
                                            HeaderText="Task Order Documents" SortExpression="DocumentDisplayName" UniqueName="DocumentDisplayName">
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
                                e.jid=@jid and etod.type='SA' order by etod.jid desc">
                                    <SelectParameters>
                                    <asp:ControlParameter ControlID="lbl_docid" Name="jid" DbType="Int32" />
                                        <%--<asp:QueryStringParameter Name="jid" QueryStringField="jid" DefaultValue="1" DbType="Int32" />--%>
                                    </SelectParameters>
                            </asp:SqlDataSource>
                            </fieldset>
                        </td>
                        <td  valign="top" align="right" style="text-align:left">
                            Add Notes:<br />
                            
                            <telerik:RadTextBox id="radtxt_notes" runat="server" Height="70px" Width="280px" TextMode="MultiLine"></telerik:RadTextBox>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                 <table width="100%">
                    <tr>
                        <td valign="top" align="center">
                            <!-- this is where the edit / new / delete etc go.. -->
                            <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click"  Text="Save/Submit" />
                            <%-- <asp:Button ID="btnReset" style="position:relative;top:-18px;" runat="server"  CausesValidation="false" Text="Reset" OnClientClick="document.forms[0].reset();return false;" UseSubmitBehavior="false"  />--%>
                            <asp:Button ID="UpdateCancelButton" runat="server" OnClick="UpdateCancelButton_Click"  Text="Reset" />

                        </td>
                        <td>
                            <asp:Label ID="lblError" runat="server" CssClass="error" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <telerik:RadGrid ID="radgrid_managejobs" runat="server" CellSpacing="0" GridLines="None"  DataSourceID="MasterTableData" AutoGenerateColumns="False"
                  AllowPaging="true" AllowSorting="true" OnItemCommand="radgrid_managejobs_ItemCommand" OnItemDataBound="radgrid_managejobs_ItemDataBound" 
                >
                
                
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView  DataSourceID="MasterTableData">
                                 <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
                        <Columns>
                                    <telerik:GridBoundColumn DataField="status"
                                        HeaderText="Status" SortExpression="status" UniqueName="status" 
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
                                        HeaderText="Job Start Date" SortExpression="startdate" UniqueName="startdate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="enddate" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="Job End Date" SortExpression="enddate" UniqueName="enddate">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="cost" 
                                        HeaderText="Project Cost($)" SortExpression="cost" UniqueName="cost">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Name"
                                        HeaderText="Customer" SortExpression="Name" UniqueName="Name" 
                                        >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Username"
                                        HeaderText="Op. Manager" SortExpression="Username" UniqueName="Username" 
                                        >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="View/Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_viewedit" runat="server" CommandName="viewedit" Text="View/Edit"></asp:LinkButton>
                                            <asp:Label ID="lbl_jobid" runat="server" Visible="false" Text='<%# Bind("jid") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridHyperLinkColumn AllowSorting="False" 
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
                <asp:SqlDataSource ID="MasterTableData" runat="server" connectionString="<%$ databaseExpression:client_database %>" 
                            providerName="System.Data.SqlClient"
                            SelectCommand="SELECT (firstName+' '+lastName) as Username,j.jobType as Jobtypename,* from manageJobOrders m,PrsimCustomers c,
                            Users u,jobTypes j where c.ID=m.Customer and m.opManagerId=u.userID and m.jobtype=j.jobtypeid and m.status!='Closed'  order by jid desc"
                            
                    >
                    <%--and u.userID=@userID--%>
                    <SelectParameters>
                        <asp:SessionParameter Name="userID" SessionField="userId" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
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
                                    <br/>



            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadWindowManager ID="RadWindowManager1"  runat="server"   
                Modal="true" Animation="Resize" >
            <Windows>
                    <telerik:RadWindow ID="RadWindow_ContentTemplate" NavigateUrl="~/createjobtypes.aspx" OnClientClose="OnPopupClosed" runat="server"  Width="400px"
                        Height="400px" >
                                        
                                        
                    </telerik:RadWindow>
                </Windows>
                </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
     
   </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnInsert" />
                
                <asp:AsyncPostBackTrigger ControlID="UpdateCancelButton" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>
   
</asp:Content>

