<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mngWarehouse.ascx.cs" Inherits="Controls_configMangrSDP_mngCollectors" %>
<%--<telerik:RadWindowManager runat="server" ShowContentDuringLoad="true" ID="radwindowmanager" />--%><%--<asp:UpdatePanel runat="server" ID="updPnl1" ChildrenAsTriggers="true">

<ContentTemplate>--%>
<%--<script type="text/javascript" language="javascript">
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
</script>--%>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="50">
<%-- <div class="loading">
<asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading3.gif" AlternateText="loading">
</asp:Image>
</div>--%>
</telerik:RadAjaxLoadingPanel>
<%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Width="100%">--%>
<%--<telerik:RadSplitter ID="RadSplitter1" Width="100%" Height="500px" runat="server" Orientation="Vertical">--%>
    <%--<telerik:RadPane ID="gridPane" runat="server" 
                     Scrolling="None">--%>
        <asp:FormView ID="FormView1" Width="100%" BorderStyle="None" BackColor="#f1f1f1" 
                      DefaultMode="Insert" DataSourceID="sqlGetUpdateCollector" runat="server" 
                      DataKeyNames="ID" onitemcommand="FormView1_ItemCommand" 
                      ondatabound="FormView1_DataBound" 
            onitemcreated="FormView1_ItemCreated" oniteminserting="FormView1_ItemInserting">
            <InsertItemTemplate>
                <!-- BEGIN INSERT -->
                <!-- BEGIN INSERT -->
                <!-- BEGIN INSERT -->   
                <h2 style="text-align: center;">Create <%=new ConstantClass().ReturnConstantWarehouse(3)%></h2>
               
              
                <table border="0" align="center">
                    <tr>
                        <td>
                            Active
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" 
                                          Checked='<%# Bind("bitActive") %>' />
                        </td>
                        <td>
                            Warehouse Name : <span style="color:Red; font-style:bold;">*</span>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtCollectorName"
                                                LabelWidth="64px" Text='<%# Bind("Name") %>' 
                                                Width="100px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  SetFocusOnError="true" Display="Dynamic" ErrorMessage="" ControlToValidate="txtCollectorName" />
                        </td>
                        <td style="display:none">
                            Warehouse ID : <span style="color:Red; font-style:bold;">*</span>
                        </td>
                        <td style="display:none">
                            <telerik:RadMaskedTextBox runat="server" ID="txtSDPNumber"
                                                      LabelWidth="64px" Text='<%# Bind("Number") %>' 
                                                      Width="100px" EmptyMessage="0000000000" Mask="##########"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ErrorMessage="" runat="server"  SetFocusOnError="true" ControlToValidate="txtSDPNumber" />
                            &nbsp;&nbsp;

                            <!-- less than a billion -->
                        </td>
                        <td style="display:none">
                            <asp:LinkButton ID="LinkButton1" CausesValidation="false" ForeColor="Blue" runat="server" onclick="LinkButton1_Click1" 
                                            >Generate ID #</asp:LinkButton>
                        </td>
                        
                    </tr>
                </table>
                <table border="0" width="100%">
                    <tr>
                        <td valign="top" width="50%">
                            <!-- start primary contact -->
                            <fieldset class="gis-form">
                                <legend>
                                    <asp:Label runat="server" ID="lblLabel" /> Primary Address
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
                                                                                        Text='<%# Bind("primaryAddress1") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                                        ControlToValidate="txtprimaryAddress1" Display="Dynamic" ErrorMessage="" 
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Country:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDropDownList ID="ddlPrimaryCountry" runat="server" Width="180px" CausesValidation="False" 
                                                                                             AutoPostBack="True" DropDownHeight="200px" DropDownWidth="180px" 
                                                                                             onselectedindexchanged="ddlPrimaryCountry_SelectedIndexChanged" 
                                                                                             SelectedValue='<%# Bind("primaryCountry") %>' 
                                                                                             AppendDataBoundItems="True" DataSourceID="SqlGetCountry" DataTextField="name" 
                                                                                             DataValueField="code" SelectedText="- Select -">
                                                                        <Items>
                                                                            <telerik:DropDownListItem Text="- Select -" Value="" Selected="True" />
                                                                        </Items>
                                                                    </telerik:RadDropDownList>
                                                                    <asp:Label ID="lblSelectedPrimaryCountry" Visible="false" runat="server" Text='<%# Bind("primaryCountry") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" class="style5">
                                                                    Address 2:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtprimaryAddress2" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("primaryAddress2") %>' Width="180px" />
                                                                </td>
                                                                <td>
                                                                    State : <span style="color:Red; font-style:bold;">*</span>&nbsp;
                                                                    <asp:Label ID="lblSelectedPrimaryState" Visible="false" runat="server" Text='<%# Bind("primaryState") %>' />
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
                                                                                                 DataValueField="code" Width="180px" AppendDataBoundItems="true" AutoPostBack="true" DropDownHeight="200px" DropDownWidth="180px"
                                                                                                 SelectedText="- Select -" SelectedValue='<%# Bind("primaryState") %>' 
                                                                                                 SkinID="Hay">
                                                                            <Items>
                                                                                <telerik:DropDownListItem Selected="True" Text="- Select -" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlNonUS" runat="server" Visible="false">
                                                                        <telerik:RadTextBox ID="txtprimaryState" runat="server" LabelWidth="64px" 
                                                                                            Width="180px" Text='<%# Eval("primaryState") %>'>
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
                                                                                        Text='<%# Bind("primaryCity") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                                        ControlToValidate="txtprimaryCity" Display="Dynamic" ErrorMessage="" 
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Zip/Postal:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtprimaryPostalCode" runat="server" LabelWidth="30px" 
                                                                                        Text='<%# Bind("primaryPostalCode") %>' Width="180px" />

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    GIS:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryGIS" runat="server" 
                                                                                        EmptyMessage=" Input address.." Height="20px" LabelWidth="64px" 
                                                                                        style="padding:0px;" Text='<%# Bind("primaryLatLong") %>' Width="180px" />
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
                                                                                       Text="Gather GIS" Width="170px" />
                                                                    <asp:TextBox ID="txtMatchDB" runat="server" 
                                                                                 Text='<%# Bind("primaryLatLongAccuracy") %>' Visible="False"></asp:TextBox>
                                                                    <asp:HyperLink ID="hypPrimaryMapLink" runat="server" 
                                                                                   NavigateUrl='<%# Eval("primaryLatLong", "http://maps.google.com/?q={0}") %>' 
                                                                                   style="color:Blue;" Target="_new" Visible="False">View Map</asp:HyperLink>
                                                                    <asp:Label ID="lblPrimaryMatch" runat="server" Font-Size="10px" 
                                                                               Text='<%# Bind("primaryLatLongAccuracy") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    First : <span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryFirst" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryFirst") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                                        ControlToValidate="txtPrimaryFirst" Display="Dynamic" ErrorMessage="" 
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Last : &nbsp;<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtPrimaryLast" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryLast") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                                        ControlToValidate="txtPrimaryLast" Display="Dynamic" ErrorMessage=""
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Phone:<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryPhone1" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryPhone1") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                                        ControlToValidate="txtPrimaryPhone1" Display="Dynamic" ErrorMessage=""
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Fax:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtPrimaryPhone2" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryPhone2") %>' Width="180px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Email:
                                                                </td>
                                                                <td colspan="3" valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryEmail" runat="server" LabelWidth="88px" 
                                                                                        Text='<%# Bind("primaryEmail") %>' Width="180px">
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
                                                   SelectCommand="SELECT code, name FROM countryCode order by name"></asp:SqlDataSource>

                                <asp:SqlDataSource ID="SqlGetStates" runat="server" 
                                                   ConnectionString="<%$ databaseExpression:client_database %>" 
                                                   SelectCommand="SELECT code, name FROM stateCode"></asp:SqlDataSource>

                            </fieldset>
                            <!-- end primary contact -->
                        </td>
                        <td width="50%" valign="top">
                            <!-- secondary -->
                            <fieldset class="gis-form">
                                <legend>Secondary Contact</legend>
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
                                                                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" 
                                                                                  Checked='<%# Bind("bitSecondarySamePrimaryAddress") %>' Font-Names="Arial" 
                                                                                  Font-Size="10px" oncheckedchanged="CheckBox2_CheckedChanged" 
                                                                                  Text="Same As Primary Address" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" width="76px">
                                                                    Address 1:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryAddress1" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("secondaryAddress1") %>' Width="180px" />
                                                                </td>
                                                                <td>
                                                                    Country:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDropDownList ID="ddlSecondaryCountry" runat="server" 
                                                                                             AutoPostBack="True" Width="180px" AppendDataBoundItems="true" DropDownHeight="200px" DropDownWidth="180px" 
                                                                                             onselectedindexchanged="ddlPrimaryCountry2_SelectedIndexChanged" 
                                                                                             SelectedValue='<%# Bind("primaryCountry") %>' 
                                                                                             CausesValidation="False" DataSourceID="SqlGetCountryInsert" DataTextField="name" 
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
                                                                    <telerik:RadTextBox ID="txtSecondaryAddress2" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("secondaryAddress2") %>' Width="180px" />
                                                                </td>
                                                                <td>
                                                                    State : &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="pnlUS0" runat="server">
                                                                        <telerik:RadDropDownList ID="ddlSecondaryState" runat="server" Width="180px"  DropDownWidth="180px"
                                                                                                 SelectedText="- Select -" 
                                                                            DataSourceID="SqlGetStatesInsert" AppendDataBoundItems="True" 
                                                                            DataTextField="name" DataValueField="code" SelectedValue='<%# Bind("secondaryState") %>' 
                                                                                                 SkinID="Hay">
                                                                            <Items>
                                                                                <telerik:DropDownListItem Selected="True" Text="- Select -" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                    <%--<asp:DropDownList runat="server" id="ddlSecondaryState" AutoPostBack="True" 
                                                                    SelectedValue='<%# Eval("secondaryState") %>' 
                                                                    DataSourceID="SqlGetStates" DataTextField="name" DataValueField="code">
                                                                    <asp:ListItem Text="- Select -" Value="" />
                                                                    </asp:DropDownList>--%>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlNonUS0" runat="server" Visible="false">
                                                                        <telerik:RadTextBox ID="txtStateNonUS" runat="server" LabelWidth="64px" 
                                                                                            Width="180px" Text='<%# Eval("secondaryState") %>'>
                                                                        </telerik:RadTextBox>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    City :
                                                                </td>
                                                                <td valign="top">

                                                                    <telerik:RadTextBox ID="txtSecondaryCity" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("secondaryCity") %>' Width="180px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Zip/Postal:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryPostalCode" runat="server" LabelWidth="30px" 
                                                                                        Text='<%# Bind("secondaryPostalCode") %>' Width="180px" />

                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    First :
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryFirst" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryFirst") %>' Width="180px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Last :
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryLast" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryLast") %>' Width="180px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Phone:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryPhone1" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryPhone1") %>' Width="180px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Fax:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryPhone2" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryPhone2") %>' Width="180px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Email:
                                                                </td>
                                                                <td colspan="3" valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryEMail" runat="server" LabelWidth="88px" 
                                                                                        Text='<%# Bind("secondaryEmail") %>' Width="180px">
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

                <asp:Table ID="TBSaveCancel" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>

                        <asp:TableCell Width="50%"></asp:TableCell>
                        <asp:TableCell>
                             <telerik:RadButton ID="btnInsert" runat="server"  CausesValidation="true" onclientclick="if (!confirm('Click &quot;OK&quot; to save your changes'))
                                return false;" CommandName="Insert" Text="Save" />
                        </asp:TableCell>
                        <asp:TableCell>
                              <telerik:RadButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Reset" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Label ID="lblError" runat="server" CssClass="error" />
                        </asp:TableCell>

                        <asp:TableCell Width="49%"></asp:TableCell>

                    </asp:TableRow>

                </asp:Table>
               
                <!-- END INSERT -->
                <!-- END INSERT -->
                <!-- END INSERT -->
            </InsertItemTemplate>
            <EditItemTemplate>
                <!-- BEGIN INSERT -->
                <!-- BEGIN INSERT -->
                <!-- BEGIN INSERT -->
               

                 <h2 style="text-align: center;">Edit <%=new ConstantClass().ReturnConstantWarehouse(3)%></h2>

                <table border="0" align="center">
                    <tr>
                        <td>
                            Active
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" 
                                          Checked='<%# Bind("bitActive") %>' />
                        </td>
                        <td>
                            Warehouse Name : <span style="color:Red; font-style:bold;">*</span>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtCollectorName"
                                                LabelWidth="64px" Text='<%# Bind("Name") %>' 
                                                Width="100px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  SetFocusOnError="true" ControlToValidate="txtCollectorName" Display="Dynamic" ErrorMessage="" />
                        </td>
                        <td style="display:none">
                            Warehouse ID : <span style="color:Red; font-style:bold;">*</span>
                        </td>
                        <td style="display:none">
                            <telerik:RadMaskedTextBox runat="server" ID="txtSDPNumber"
                                                      LabelWidth="64px" Text='<%# Bind("Number") %>' 
                                                      Width="100px" EmptyMessage="0000000000" Mask="##########"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  SetFocusOnError="true" ControlToValidate="txtSDPNumber" Display="Dynamic" ErrorMessage="" />
                            &nbsp;&nbsp;

                            <!-- less than a billion -->
                        </td>
                        <td style="display:none">
                            <asp:LinkButton ID="LinkButton1" CausesValidation="false" ForeColor="Blue" runat="server" onclick="LinkButton1_Click1" 
                                            >Generate ID #</asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label ID="lblError" runat="server" style="color:Red;" />
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%">
                    <tr>
                        <td valign="top" width="50%">
                            <!-- start primary contact -->
                            <fieldset class="gis-form">
                                <legend>
                                    <asp:Label runat="server" ID="lblLabel" /> Primary Address
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
                                                                                        Text='<%# Bind("primaryAddress1") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                                        ControlToValidate="txtprimaryAddress1" Display="Dynamic" ErrorMessage="" 
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Country:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDropDownList ID="ddlPrimaryCountry" runat="server" 
                                                                                             AutoPostBack="True" AppendDataBoundItems="true" Width="180px" DropDownHeight="200px" DropDownWidth="180px" 
                                                                                             onselectedindexchanged="ddlPrimaryCountry_SelectedIndexChanged" 
                                                                                             SelectedValue='<%# Bind("primaryCountry") %>' 
                                                                                             CausesValidation="False" DataSourceID="SqlGetCountryInsert" DataTextField="name" 
                                                                                             DataValueField="code" SelectedText="- Select -">
                                                                        <Items>
                                                                            <telerik:DropDownListItem 
                                                                                Text="- Select -" Value="" Selected="True" />
                                                                        </Items>
                                                                    </telerik:RadDropDownList>
                                                                    <asp:Label ID="primaryCountry" runat="server" 
                                                                               Text='<%# Eval("primaryCountry") %>' Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" class="style5">
                                                                    Address 2:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtprimaryAddress2" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("primaryAddress2") %>' Width="180px" />
                                                                </td>
                                                                <td>
                                                                    State : <span style="color:Red; font-style:bold;">*</span>&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="pnlUS" runat="server">
                                                                      <telerik:RadDropDownList ID="ddlPrimaryState" Width="180px" runat="server" 
                                                                            DataSourceID="SqlGetStates" DataTextField="name" 
                                                                                                 DataValueField="code" AppendDataBoundItems="true" 
                                                                            AutoPostBack="true" DropDownHeight="200px" DropDownWidth="180px"
                                                                                                 SelectedText="- Select -" SelectedValue='<%# Bind("primaryState") %>' 
                                                                                                 SkinID="Hay" 
                                                                            onselectedindexchanged="ddlPrimaryState_SelectedIndexChanged">
                                                                            <Items>
                                                                                <telerik:DropDownListItem Selected="True" Text="- Select -" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                        <asp:Label ID="primaryState" runat="server" Text='<%# Eval("primaryState") %>' 
                                                                                   Visible="False"></asp:Label>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlNonUS" runat="server" Visible="false">
                                                                        <telerik:RadTextBox ID="txtprimaryState" runat="server" LabelWidth="64px" 
                                                                                            Width="180px" Text='<%# Eval("primaryState") %>'>
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
                                                                                        Text='<%# Bind("primaryCity") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                                        ControlToValidate="txtprimaryCity" Display="Dynamic" ErrorMessage="" 
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Zip/Postal:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtprimaryPostalCode" runat="server" LabelWidth="30px" 
                                                                                        Text='<%# Bind("primaryPostalCode") %>' Width="180px" />

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    GIS:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryGIS" runat="server" 
                                                                                        EmptyMessage=" Input address.." Height="20px" LabelWidth="64px" 
                                                                                        style="padding:0px;" Text='<%# Bind("primaryLatLong") %>' Width="180px" />
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
                                                                                       SingleClick="true" SingleClickText="Please wait..." Width="170px" style="top:3px;" 
                                                                                       Text="Gather GIS" />
                                                                    <asp:TextBox ID="txtMatchDB" runat="server" 
                                                                                 Text='<%# Bind("primaryLatLongAccuracy") %>' Visible="False"></asp:TextBox>
                                                                    <asp:HyperLink ID="hypPrimaryMapLink" runat="server" 
                                                                                   NavigateUrl='<%# Eval("primaryLatLong", "http://maps.google.com/?q={0}") %>' 
                                                                                   style="color:Blue;" Target="_new" Visible="False">View Map</asp:HyperLink>
                                                                    <asp:Label ID="lblPrimaryMatch" runat="server" Font-Size="10px" Visible="false" 
                                                                               Text='<%# Bind("primaryLatLongAccuracy") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    First : <span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryFirst" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryFirst") %>' Width="180px" 
                                                                        AutoPostBack="True" ontextchanged="txtPrimaryFirst_TextChanged" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                                        ControlToValidate="txtPrimaryFirst" Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Last : &nbsp;<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtPrimaryLast" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryLast") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                                                        ControlToValidate="txtPrimaryLast" Display="Dynamic" ErrorMessage="" 
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Phone:<span style="color:Red; font-style:bold;">*</span>
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryPhone1" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryPhone1") %>' Width="180px" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                                                        ControlToValidate="txtPrimaryPhone1" Display="Dynamic" ErrorMessage="" 
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    Fax:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtPrimaryPhone2" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("primaryPhone2") %>' Width="180px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Email:
                                                                </td>
                                                                <td colspan="3" valign="top">
                                                                    <telerik:RadTextBox ID="txtPrimaryEmail" runat="server" LabelWidth="88px" 
                                                                                        Text='<%# Bind("primaryEmail") %>' Width="180px">
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
                                <legend>Secondary Contact</legend>
                                <table width="100%" cellpadding="0" border="0">
                                    <tr>
                                        <td valign="top">

                                            <table border="0" style="" width="100%">
                                                <tr>
                                                    <td valign="top">
                                                        <table width="100%">
                                                            <tr>
                                                                <td  valign="top">
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="top" colspan="3">
                                                                    &nbsp;
                                                                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" 
                                                                                  Checked='<%# Bind("bitSecondarySamePrimaryAddress") %>' Font-Names="Arial" 
                                                                                  Font-Size="10px" oncheckedchanged="CheckBox2_CheckedChanged" 
                                                                                  Text="Same As Primary Address" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" width="76px">
                                                                    Address 1:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryAddress1" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("secondaryAddress1") %>' Width="180px" />
                                                                </td>
                                                                <td>
                                                                    Country:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDropDownList ID="ddlSecondaryCountry" runat="server" 
                                                                                             AutoPostBack="True" AppendDataBoundItems="true" Width="180px" DropDownHeight="200px" DropDownWidth="180px" 
                                                                                             onselectedindexchanged="ddlPrimaryCountry2_SelectedIndexChanged" 
                                                                                             SelectedValue='<%# Bind("secondaryCountry") %>' 
                                                                                             CausesValidation="False" DataSourceID="SqlGetCountryInsert" DataTextField="name" 
                                                                                             DataValueField="code" SelectedText="- Select -">
                                                                        <Items>
                                                                            <telerik:DropDownListItem 
                                                                                Text="- Select -" Value="" Selected="True" />
                                                                        </Items>
                                                                    </telerik:RadDropDownList>
                                                                    <asp:Label ID="secondaryCountry" runat="server" 
                                                                               Text='<%# Eval("secondaryCountry") %>' Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" class="style5">
                                                                    Address 2:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryAddress2" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("secondaryAddress2") %>' Width="180px" />
                                                                </td>
                                                                <td>
                                                                    State : &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="pnlUS0" runat="server">
                                                                        <telerik:RadDropDownList ID="ddlSecondaryState" runat="server" 
                                                                                                 SelectedText="- Select -" DataSourceID="SqlGetStatesInsert" AppendDataBoundItems="true" DataTextField="name" DataValueField="code" SelectedValue='<%# Bind("secondaryState") %>' 
                                                                                                 SkinID="Hay" Width="180px" DropDownWidth="180px">
                                                                            <Items>
                                                                                <telerik:DropDownListItem Selected="True" Text="- Select -" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                        <%--<asp:DropDownList runat="server" id="ddlSecondaryState" AutoPostBack="True" 
                                                                        SelectedValue='<%# Eval("secondaryState") %>' 
                                                                        DataSourceID="SqlGetStates" DataTextField="name" DataValueField="code">
                                                                        <asp:ListItem Text="- Select -" Value="" />
                                                                        </asp:DropDownList>--%>
                                                                        <asp:Label ID="secondaryState" runat="server" 
                                                                                   Text='<%# Eval("secondaryState") %>' Visible="False"></asp:Label>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlNonUS0" runat="server" Visible="false">
                                                                        <telerik:RadTextBox ID="txtStateNonUS" runat="server" LabelWidth="64px" 
                                                                                            Width="180px" Text='<%# Eval("secondaryState") %>'>
                                                                        </telerik:RadTextBox>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    City :
                                                                </td>
                                                                <td valign="top">

                                                                    <telerik:RadTextBox ID="txtSecondaryCity" runat="server" LabelWidth="64px" 
                                                                                        Text='<%# Bind("secondaryCity") %>' Width="180px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Zip/Postal:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryPostalCode" runat="server" LabelWidth="30px" 
                                                                                        Text='<%# Bind("secondaryPostalCode") %>' Width="180px" />

                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    First :
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryFirst" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryFirst") %>' Width="180px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Last :
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryLast" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryLast") %>' Width="180px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Phone:
                                                                </td>
                                                                <td valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryPhone1" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryPhone1") %>' Width="180px" />
                                                                </td>
                                                                <td colspan="1">
                                                                    Fax:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSecondaryPhone2" runat="server" LabelWidth="48px" 
                                                                                        Text='<%# Bind("secondaryPhone2") %>' Width="180px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" valign="top">
                                                                    Email:
                                                                </td>
                                                                <td colspan="3" valign="top">
                                                                    <telerik:RadTextBox ID="txtSecondaryEMail" runat="server" LabelWidth="88px" 
                                                                                        Text='<%# Bind("secondaryEmail") %>' Width="180px">
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

                  <asp:Table ID="TBUpdateButtons" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>

                        <asp:TableCell Width="50%"></asp:TableCell>
                        <asp:TableCell>
                             <telerik:RadButton  ID="UpdateButton" runat="server" SingleClick="true" 
                                                SingleClickText="Please wait." CausesValidation="True" 
                                                CommandName="Update" Text="Update" 
                                                onclick="UpdateButton_Click" />
                        </asp:TableCell>
                      

                        <asp:TableCell>
                         <telerik:RadButton ID="UpdateCancelButton" runat="server" 
                                               CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                        </asp:TableCell>

                        <asp:TableCell Width="50%"></asp:TableCell>

                    </asp:TableRow>

                </asp:Table>
               
              
                <!-- END EDIT -->
                <!-- END EDIT -->
                <!-- END EDIT -->
            </EditItemTemplate>
            <ItemTemplate>
                ID:
                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />

                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Edit" />
                &nbsp;
                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" 
                                CommandName="Delete" Text="Delete" />
                &nbsp;
                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                                CommandName="New" Text="New" />
            </ItemTemplate>

        </asp:FormView>
        <asp:SqlDataSource ID="sqlGetUpdateCollector" runat="server" 
                           ConnectionString="<%$ databaseExpression:client_database %>" 
                           SelectCommand="SELECT id, bitActive, Number, Name, primaryFirst, primaryLast, primaryAddress1, primaryAddress2, primaryCity, primaryState, primaryCountry, primaryPostalCode, primaryPhone1, primaryPhone2, primaryEmail, primaryLatLong, primaryLatLongAccuracy, secondaryFirst, secondaryLast, secondaryAddress1, secondaryAddress2, secondaryCity, secondaryState, secondaryCountry, secondaryPostalCode, secondaryPhone1, secondaryEmail, secondaryPhone2, secondaryLatLong, secondaryLatLongAccuracy, bitSecondarySamePrimaryAddress FROM PrsimWarehouses WHERE (id = @ID)" 
                           oninserted="sqlGetUpdateSDP_Inserted" 
                           onupdated="sqlGetUpdateSDP_Updated" 
                           DeleteCommand="DELETE FROM [PrsimWarehouses] WHERE [Number] = @Number" 
                           InsertCommand="INSERT INTO PrsimWarehouses(bitActive, Number, Name, primaryFirst, primaryLast, primaryAddress1, primaryAddress2, primaryCity, primaryState, primaryCountry, primaryPostalCode, primaryPhone1, primaryPhone2, primaryEmail, primaryLatLong, primaryLatLongAccuracy, secondaryFirst, secondaryLast, secondaryAddress1, secondaryAddress2, secondaryCity, secondaryState, secondaryCountry, secondaryPostalCode, secondaryPhone1, secondaryEmail, secondaryPhone2, secondaryLatLong, secondaryLatLongAccuracy, bitSecondarySamePrimaryAddress) VALUES (@bitActive, @Number, @Name, @primaryFirst, @primaryLast, @primaryAddress1, @primaryAddress2, @primaryCity, @primaryState, @primaryCountry, @primaryPostalCode, @primaryPhone1, @primaryPhone2, @primaryEmail, @primaryLatLong, @primaryLatLongAccuracy, @secondaryFirst, @secondaryLast, @secondaryAddress1, @secondaryAddress2, @secondaryCity, @secondaryState, @secondaryCountry, @secondaryPostalCode, @secondaryPhone1, @secondaryEmail, @secondaryPhone2, @secondaryLatLong, @secondaryLatLongAccuracy, @bitSecondarySamePrimaryAddress)" 
                    
                               
                           
            UpdateCommand="UPDATE PrsimWarehouses SET bitActive = @bitActive, Name = @Name, primaryFirst = @primaryFirst, primaryLast = @primaryLast, primaryAddress1 = @primaryAddress1, primaryAddress2 = @primaryAddress2, primaryCity = @primaryCity, primaryState = @primaryState, primaryCountry = @primaryCountry, primaryPostalCode = @primaryPostalCode, primaryPhone1 = @primaryPhone1, primaryPhone2 = @primaryPhone2, primaryEmail = @primaryEmail, primaryLatLong = @primaryLatLong, primaryLatLongAccuracy = @primaryLatLongAccuracy, secondaryFirst = @secondaryFirst, secondaryLast = @secondaryLast, secondaryAddress1 = @secondaryAddress1, secondaryAddress2 = @secondaryAddress2, secondaryCity = @secondaryCity, secondaryState = @secondaryState, secondaryCountry = @secondaryCountry, secondaryPostalCode = @secondaryPostalCode, secondaryPhone1 = @secondaryPhone1, secondaryEmail = @secondaryEmail, secondaryPhone2 = @secondaryPhone2, secondaryLatLong = @secondaryLatLong, secondaryLatLongAccuracy = @secondaryLatLongAccuracy, bitSecondarySamePrimaryAddress = @bitSecondarySamePrimaryAddress WHERE (id = @ID)" 
            >
            <DeleteParameters>
                <asp:Parameter Name="Number" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="bitActive" Type="Boolean" />
                <asp:Parameter Name="Number" Type="String" />
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="primaryFirst" Type="String" />
                <asp:Parameter Name="primaryLast" Type="String" />
                <asp:Parameter Name="primaryAddress1" Type="String" />
                <asp:Parameter Name="primaryAddress2" Type="String" />
                <asp:Parameter Name="primaryCity" Type="String" />
                <asp:Parameter Name="primaryState" Type="String" />
                <asp:Parameter Name="primaryCountry" Type="String" />
                <asp:Parameter Name="primaryPostalCode" Type="String" />
                <asp:Parameter Name="primaryPhone1" Type="String" />
                <asp:Parameter Name="primaryPhone2" Type="String" />
                <asp:Parameter Name="primaryEmail" Type="String" />
                <asp:Parameter Name="primaryLatLong" Type="String" />
                <asp:Parameter Name="primaryLatLongAccuracy" Type="String" />
                <asp:Parameter Name="bitSecondarySamePrimaryAddress" />
                <asp:Parameter Name="secondaryFirst" Type="String" />
                <asp:Parameter Name="secondaryLast" Type="String" />
                <asp:Parameter Name="secondaryAddress1" Type="String" />
                <asp:Parameter Name="secondaryAddress2" Type="String" />
                <asp:Parameter Name="secondaryCity" Type="String" />
                <asp:Parameter Name="secondaryState" Type="String" />
                <asp:Parameter Name="secondaryCountry" Type="String" />
                <asp:Parameter Name="secondaryPostalCode" Type="String" />
                <asp:Parameter Name="secondaryPhone1" Type="String" />
                <asp:Parameter Name="secondaryEmail" Type="String" />
                <asp:Parameter Name="secondaryPhone2" Type="String" />
                <asp:Parameter Name="secondaryLatLong" Type="String" />
                <asp:Parameter Name="secondaryLatLongAccuracy" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="radGrid1" Name="ID" 
                                      PropertyName="SelectedValue" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="bitActive" Type="Boolean" />
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="primaryFirst" Type="String" />
                <asp:Parameter Name="primaryLast" Type="String" />
                <asp:Parameter Name="primaryAddress1" Type="String" />
                <asp:Parameter Name="primaryAddress2" Type="String" />
                <asp:Parameter Name="primaryCity" Type="String" />
                <asp:Parameter Name="primaryState" Type="String" />
                <asp:Parameter Name="primaryCountry" Type="String" />
                <asp:Parameter Name="primaryPostalCode" Type="String" />
                <asp:Parameter Name="primaryPhone1" Type="String" />
                <asp:Parameter Name="primaryPhone2" Type="String" />
                <asp:Parameter Name="primaryEmail" Type="String" />
                <asp:Parameter Name="primaryLatLong" Type="String" />
                <asp:Parameter Name="primaryLatLongAccuracy" Type="String" />
                <asp:Parameter Name="secondaryFirst" Type="String" />
                <asp:Parameter Name="secondaryLast" Type="String" />
                <asp:Parameter Name="secondaryAddress1" Type="String" />
                <asp:Parameter Name="secondaryAddress2" Type="String" />
                <asp:Parameter Name="secondaryCity" Type="String" />
                <asp:Parameter Name="secondaryState" Type="String" />
                <asp:Parameter Name="secondaryCountry" Type="String" />
                <asp:Parameter Name="secondaryPostalCode" Type="String" />
                <asp:Parameter Name="secondaryPhone1" Type="String" />
                <asp:Parameter Name="secondaryEmail" Type="String" />
                <asp:Parameter Name="secondaryPhone2" Type="String" />
                <asp:Parameter Name="secondaryLatLong" Type="String" />
                <asp:Parameter Name="secondaryLatLongAccuracy" Type="String" />
                <asp:Parameter Name="Number" Type="String" />
                <asp:Parameter Name="bitSecondarySamePrimaryAddress" />
                <asp:ControlParameter ControlID="lblSelectedRecord" Name="ID" 
                                      PropertyName="Text" />
            </UpdateParameters>
        </asp:SqlDataSource>
    <%--</telerik:RadPane>--%>

<%--</telerik:RadSplitter>--%>
<asp:Label ID="lblSelectedRecord" runat="server" Visible="False" />
<!-- dg here -->
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

<telerik:RadGrid ID="radGrid1" runat="server" DataSourceID="sqldsViewEditGIS" 
                 Width="100%" AllowSorting="True" 
    AutoGenerateColumns="False" CellSpacing="0" 
                 GridLines="None" 
    onselectedindexchanged="radGrid1_SelectedIndexChanged" ShowStatusBar="True">
    <ClientSettings EnablePostBackOnRowClick="True" AllowKeyboardNavigation="True" 
        EnableRowHoverStyle="True">
        <Selecting AllowRowSelect="True" />
        <KeyboardNavigationSettings AllowActiveRowCycle="True" />
    </ClientSettings>
    <MasterTableView DataKeyNames="id" DataSourceID="sqldsViewEditGIS">
        <CommandItemSettings ExportToPdfText="Export to PDF" 
                             ShowAddNewRecordButton="False" />
        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                              Visible="True">
            <HeaderStyle Width="20px" />
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridButtonColumn ButtonType="LinkButton" Text="Edit" CommandName="Select"></telerik:GridButtonColumn>
            <telerik:GridTemplateColumn DataField="bitActive" 
                                        FilterControlAltText="Filter bitActive column" HeaderText="Status" 
                                        UniqueName="bitActive">
                <ItemTemplate>
                    <span style='<%# (((bool)Eval( "bitActive" ) == true)? "color: green": "color: red") %>'><%# (((bool)Eval( "bitActive" ) == true)? "": "In-") %>Active</span>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="Name" 
                                     FilterControlAltText="Filter Name column" HeaderText="Name" ReadOnly="True" 
                                     SortExpression="Name" UniqueName="Name">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Number" 
                                     FilterControlAltText="Filter Number column" HeaderText="Warehouse ID" ReadOnly="True" visible="false"
                                     SortExpression="Number" UniqueName="Number">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="primaryFirst" 
                                     FilterControlAltText="Filter primaryFirst column" HeaderText="Prim. First" 
                                     SortExpression="primaryFirst" UniqueName="primaryFirst">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="primaryLast" 
                                     FilterControlAltText="Filter primaryLast column" HeaderText="Prim. Last" 
                                     SortExpression="primaryLast" UniqueName="primaryLast">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="primaryAddress1" 
                                     FilterControlAltText="Filter primaryAddress1 column" 
                                     HeaderText="Prim. Address1" SortExpression="primaryAddress1" 
                                     UniqueName="primaryAddress1">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="primaryCity" 
                                     FilterControlAltText="Filter primaryCity column" HeaderText="Prim. City" 
                                     SortExpression="primaryCity" UniqueName="primaryCity">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="primaryState" 
                                     FilterControlAltText="Filter primaryState column" HeaderText="Prim. State" 
                                     SortExpression="primaryState" UniqueName="primaryState">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="primaryCountry" 
                                     FilterControlAltText="Filter primaryCountry column" HeaderText="Prim. Country" 
                                     SortExpression="primaryCountry" UniqueName="primaryCountry">
            </telerik:GridBoundColumn>
            <telerik:GridHyperLinkColumn AllowSorting="False" 
                                         DataNavigateUrlFields="primaryLatLong" 
                                         DataNavigateUrlFormatString="http://maps.google.com/?q={0}" 
                                         FilterControlAltText="Filter gisLink column" Target="_new" Text="View Map" 
                                         UniqueName="gisLink">
                <ItemStyle ForeColor="Blue" />
            </telerik:GridHyperLinkColumn>
        <%--   <telerik:GridClientSelectColumn FilterControlAltText="Filter column column" 
        UniqueName="column">
        </telerik:GridClientSelectColumn>--%>
        </Columns>
        <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
            </EditColumn>
        </EditFormSettings>
        <PagerStyle PageSizeControlType="RadComboBox" />
    </MasterTableView>
    <PagerStyle PageSizeControlType="RadComboBox" />
    <FilterMenu EnableImageSprites="False">
    </FilterMenu>
</telerik:RadGrid>

<asp:SqlDataSource ID="sqldsViewEditGIS" runat="server" 
                   ConnectionString="<%$ databaseExpression:client_database %>" 
                                           
                            
                       
                   SelectCommand="SELECT [id], [bitActive], [Number], [Name], [primaryFirst], [primaryLast], [primaryAddress1], [primaryAddress2], [primaryCity], [primaryState], [primaryCountry], [primaryPostalCode], [primaryPhone1], [primaryPhone2], [primaryEmail], [primaryLatLong], [primaryLatLongAccuracy], [secondaryFirst], [secondaryLast], [secondaryAddress1], [secondaryAddress2], [secondaryCity], [secondaryState], [secondaryCountry], [secondaryPostalCode], [secondaryPhone1], [secondaryEmail], [secondaryPhone2], [secondaryLatLong], [secondaryLatLongAccuracy] FROM [PrsimWarehouses] order by ID desc" 
                   DeleteCommand="DELETE FROM [PrsimWarehouses] WHERE [Number] = @Number" 
                   InsertCommand="INSERT INTO [PrsimWarehouses] ([bitActive], [Number], [Name], [primaryFirst], [primaryLast], [primaryAddress1], [primaryAddress2], [primaryCity], [primaryState], [primaryCountry], [primaryPostalCode], [primaryPhone1], [primaryPhone2], [primaryEmail], [primaryLatLong], [primaryLatLongAccuracy], [secondaryFirst], [secondaryLast], [secondaryAddress1], [secondaryAddress2], [secondaryCity], [secondaryState], [secondaryCountry], [secondaryPostalCode], [secondaryPhone1], [secondaryEmail], [secondaryPhone2], [secondaryLatLong], [secondaryLatLongAccuracy]) VALUES (@bitActive, @Number, @Name, @primaryFirst, @primaryLast, @primaryAddress1, @primaryAddress2, @primaryCity, @primaryState, @primaryCountry, @primaryPostalCode, @primaryPhone1, @primaryPhone2, @primaryEmail, @primaryLatLong, @primaryLatLongAccuracy, @secondaryFirst, @secondaryLast, @secondaryAddress1, @secondaryAddress2, @secondaryCity, @secondaryState, @secondaryCountry, @secondaryPostalCode, @secondaryPhone1, @secondaryEmail, @secondaryPhone2, @secondaryLatLong, @secondaryLatLongAccuracy)" 
                   UpdateCommand="UPDATE [PrsimWarehouses] SET [id] = @id, [bitActive] = @bitActive, [Name] = @Name, [primaryFirst] = @primaryFirst, [primaryLast] = @primaryLast, [primaryAddress1] = @primaryAddress1, [primaryAddress2] = @primaryAddress2, [primaryCity] = @primaryCity, [primaryState] = @primaryState, [primaryCountry] = @primaryCountry, [primaryPostalCode] = @primaryPostalCode, [primaryPhone1] = @primaryPhone1, [primaryPhone2] = @primaryPhone2, [primaryEmail] = @primaryEmail, [primaryLatLong] = @primaryLatLong, [primaryLatLongAccuracy] = @primaryLatLongAccuracy, [secondaryFirst] = @secondaryFirst, [secondaryLast] = @secondaryLast, [secondaryAddress1] = @secondaryAddress1, [secondaryAddress2] = @secondaryAddress2, [secondaryCity] = @secondaryCity, [secondaryState] = @secondaryState, [secondaryCountry] = @secondaryCountry, [secondaryPostalCode] = @secondaryPostalCode, [secondaryPhone1] = @secondaryPhone1, [secondaryEmail] = @secondaryEmail, [secondaryPhone2] = @secondaryPhone2, [secondaryLatLong] = @secondaryLatLong, [secondaryLatLongAccuracy] = @secondaryLatLongAccuracy WHERE [Number] = @Number">
    <DeleteParameters>
        <asp:Parameter Name="Number" Type="String" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="bitActive" Type="Boolean" />
        <asp:Parameter Name="Number" Type="String" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="primaryFirst" Type="String" />
        <asp:Parameter Name="primaryLast" Type="String" />
        <asp:Parameter Name="primaryAddress1" Type="String" />
        <asp:Parameter Name="primaryAddress2" Type="String" />
        <asp:Parameter Name="primaryCity" Type="String" />
        <asp:Parameter Name="primaryState" Type="String" />
        <asp:Parameter Name="primaryCountry" Type="String" />
        <asp:Parameter Name="primaryPostalCode" Type="String" />
        <asp:Parameter Name="primaryPhone1" Type="String" />
        <asp:Parameter Name="primaryPhone2" Type="String" />
        <asp:Parameter Name="primaryEmail" Type="String" />
        <asp:Parameter Name="primaryLatLong" Type="String" />
        <asp:Parameter Name="primaryLatLongAccuracy" Type="String" />
        <asp:Parameter Name="secondaryFirst" Type="String" />
        <asp:Parameter Name="secondaryLast" Type="String" />
        <asp:Parameter Name="secondaryAddress1" Type="String" />
        <asp:Parameter Name="secondaryAddress2" Type="String" />
        <asp:Parameter Name="secondaryCity" Type="String" />
        <asp:Parameter Name="secondaryState" Type="String" />
        <asp:Parameter Name="secondaryCountry" Type="String" />
        <asp:Parameter Name="secondaryPostalCode" Type="String" />
        <asp:Parameter Name="secondaryPhone1" Type="String" />
        <asp:Parameter Name="secondaryEmail" Type="String" />
        <asp:Parameter Name="secondaryPhone2" Type="String" />
        <asp:Parameter Name="secondaryLatLong" Type="String" />
        <asp:Parameter Name="secondaryLatLongAccuracy" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="id" Type="Int32" />
        <asp:Parameter Name="bitActive" Type="Boolean" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="primaryFirst" Type="String" />
        <asp:Parameter Name="primaryLast" Type="String" />
        <asp:Parameter Name="primaryAddress1" Type="String" />
        <asp:Parameter Name="primaryAddress2" Type="String" />
        <asp:Parameter Name="primaryCity" Type="String" />
        <asp:Parameter Name="primaryState" Type="String" />
        <asp:Parameter Name="primaryCountry" Type="String" />
        <asp:Parameter Name="primaryPostalCode" Type="String" />
        <asp:Parameter Name="primaryPhone1" Type="String" />
        <asp:Parameter Name="primaryPhone2" Type="String" />
        <asp:Parameter Name="primaryEmail" Type="String" />
        <asp:Parameter Name="primaryLatLong" Type="String" />
        <asp:Parameter Name="primaryLatLongAccuracy" Type="String" />
        <asp:Parameter Name="secondaryFirst" Type="String" />
        <asp:Parameter Name="secondaryLast" Type="String" />
        <asp:Parameter Name="secondaryAddress1" Type="String" />
        <asp:Parameter Name="secondaryAddress2" Type="String" />
        <asp:Parameter Name="secondaryCity" Type="String" />
        <asp:Parameter Name="secondaryState" Type="String" />
        <asp:Parameter Name="secondaryCountry" Type="String" />
        <asp:Parameter Name="secondaryPostalCode" Type="String" />
        <asp:Parameter Name="secondaryPhone1" Type="String" />
        <asp:Parameter Name="secondaryEmail" Type="String" />
        <asp:Parameter Name="secondaryPhone2" Type="String" />
        <asp:Parameter Name="secondaryLatLong" Type="String" />
        <asp:Parameter Name="secondaryLatLongAccuracy" Type="String" />
        <asp:Parameter Name="Number" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>
<telerik:RadNotification ID="n1" runat="server" Text="Initial text" Position="BottomRight"
                         AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time" EnableRoundedCorners="true">
</telerik:RadNotification>
<%-- <asp:Button ID="btnReset" style="position:relative;top:-18px;" runat="server"  CausesValidation="false" Text="Reset" OnClientClick="document.forms[0].reset();return false;" UseSubmitBehavior="false"  />--%>
