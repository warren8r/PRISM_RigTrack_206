<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SuperAdmin.master" AutoEventWireup="true" CodeFile="ManageClients.aspx.cs" Inherits="ManageClients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validview() {

                if ($find("<%=radcombo_userstatus.ClientID %>").get_value() == "Select") {
                    document.getElementById("<%=lbl_errormsg.ClientID %>").innerHTML = "Select User Status";

                    return false;
                }
            }

            function validateEmail(elementValue) {
                var emailRegEx = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
                return emailRegEx.test(elementValue);

            }

             
        </script>
 </telerik:RadCodeBlock>
     <script src="../Js/jquery.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         function formtextboxes(obj) {
             var existval = document.getElementById('<%=hid_existvaluesforassets.ClientID %>').value;
             if (existval == "") {
                 var total = parseInt(obj.value) + 1;
                 $(".inputs").empty();

                 for (counter = 1; counter < total; counter++) {



                     $('<table cellpadding="0" cellspacing="0" border="0"><tr><td>Asset' + counter + ' </td><td><input type="text" class="field" name="dynamic[]" style="border:solid 1px Gray"  /></td></tr></table>').fadeIn('slow').appendTo('.inputs');

                 }
             }
             else {
                 var total = parseInt(obj.value);
                 $(".inputs").empty();
                 var splitofhid = existval.split(',');
                 for (counter = 0; counter < total; counter++) {


                     if (splitofhid.length > counter) {
                         $('<table cellpadding="0" cellspacing="0" border="0"><tr><td>Asset' + (counter + 1) + ' </td><td><input type="text" class="field" name="dynamic[]" value=' + splitofhid[counter] + ' style="border:solid 1px Gray"  /></td></tr></table>').fadeIn('slow').appendTo('.inputs');
                     }
                     else {
                         $('<table cellpadding="0" cellspacing="0" border="0"><tr><td>Asset' + (counter + 1) + ' </td><td><input type="text" class="field" name="dynamic[]" style="border:solid 1px Gray"  /></td></tr></table>').fadeIn('slow').appendTo('.inputs');
                     }

                 }
             }




         }
         function formtextboxescodebehind(obj, objval) {

             var total = parseInt(obj);
             var valuesintxt = objval.split(',');
             $(".inputs").empty();

             for (counter = 0; counter < total; counter++) {



                 $('<table cellpadding="0" cellspacing="0" border="0"><tr><td>Asset' + (counter + 1) + ' </td><td><input type="text" class="field" name="dynamic[]" value=' + valuesintxt[counter] + ' style="border:solid 1px Gray"  /></td></tr></table>').fadeIn('slow').appendTo('.inputs');

             }




         }

         function multiplesofn(objx, objy) {
             var remainder = objx % objy;
             if (remainder == 0) {
                 return true;
             } else {
                 return false;
                 //x is not a multiple of y
             }
         }



         function getassetvalues() {










             if (document.getElementById("<%=txt_nofassets.ClientID %>").value == "") {
                 document.getElementById("<%=lbl_message_configdetails.ClientID %>").innerHTML = "Enter Number of Assets";

                 return false;
             }

             if ($find("<%=radtxt_allowedusers.ClientID %>").get_textBoxValue() == "") {
                 document.getElementById("<%=lbl_message_configdetails.ClientID %>").innerHTML = "Enter Allowed Users";

                 return false;
             }
             if ($find("<%=radtxt_usertypes.ClientID %>").get_textBoxValue() == "") {
                 document.getElementById("<%=lbl_message_configdetails.ClientID %>").innerHTML = "Enter UserTypes";

                 return false;
             }
             if ($find("<%=radtxt_subperiod.ClientID %>").get_textBoxValue() == "") {
                 document.getElementById("<%=lbl_message_configdetails.ClientID %>").innerHTML = "Enter Subscription Period in Months";

                 return false;
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
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="color:#4B6C9E; font-size:13px; font-weight:bold" align="left">
                            Manage Clients &gt;&gt; Client Configuration</td>
                        <td align="right">
                            (<span class="star">*</span>) indicates mandatory fields
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lbl_errormsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr><td style="height:10px"></td></tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" class="label_display">
                                        Select User Status<span class="star">*</span><br />
                                        <telerik:RadComboBox ID="radcombo_userstatus" runat="server" Width="160px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Select" Value="Select" />
                                                <telerik:RadComboBoxItem Text="Approved" Value="Approved" />
                                                <telerik:RadComboBoxItem Text="Denied" Value="Denied" />
                                                <telerik:RadComboBoxItem Text="Pending" Value="Pending" />
                                                
                                            </Items>
                                        </telerik:RadComboBox>
                                            
                                    </td>
                                    <td align="left">
                                        From Date<span class="star">*</span><br />
                                        <telerik:RadDatePicker runat="server" ID="radtxt_from" Width="130px">
                                            <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                            </Calendar>
                                            <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy" runat="server"></DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td align="left">
                                        To Date<span class="star">*</span><br />
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
                        <td colspan="2" align="center">
                            <table border="0" cellpadding="0" cellspacing="0" width="740px">
                                <tr>
                                    <td align="left">
                                        <telerik:RadGrid  ID="radgrid_clientdetails"
                                          AllowPaging="true" AllowSorting="true"  PageSize="10" OnPageIndexChanged="radgrid_clientdetails_PageIndexChanged"
                                             runat="server" AllowFilteringByColumn="false" OnPageSizeChanged="radgrid_clientdetails_PageSizeChanged" 
                                             OnItemCommand="radgrid_clientdetails_ItemCommand" OnSortCommand="radgrid_clientdetails_SortCommand">
                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                            <ClientSettings EnableRowHoverStyle="false">
                                             <%--<Scrolling AllowScroll="True"   UseStaticHeaders="True" ></Scrolling>--%>

                                                <Selecting AllowRowSelect="true"></Selecting>
                                            </ClientSettings>
                                            <SelectedItemStyle BackColor="Gray"></SelectedItemStyle>
                                            <EditItemStyle CssClass="EditedItem" Height="25px"></EditItemStyle> 
                                            <MasterTableView DataKeyNames="clientID" GridLines="Horizontal" AutoGenerateColumns="false" NoMasterRecordsText="No Data have been added."   Width="740px" >             
                                              <HeaderStyle HorizontalAlign="left" />
                                              <ItemStyle HorizontalAlign="Left" />
                                              
                                              <AlternatingItemStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                             <%--<telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn">
                                                            </telerik:GridClientSelectColumn>--%>
                                                            <telerik:GridBoundColumn DataField="clientCode" HeaderText="Client Code" UniqueName="clientCode" ItemStyle-Width="60px"
                                                                SortExpression="clientCode">
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="loginName" HeaderText="Client Name" UniqueName="loginName" ItemStyle-Width="60px"
                                                                SortExpression="loginName">
                                                            </telerik:GridBoundColumn>                                                            
                                                            <telerik:GridBoundColumn DataField="firstName" HeaderText="FirstName" UniqueName="firstName" ItemStyle-Width="100px"
                                                                SortExpression="firstName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="lastName" HeaderText="LastName" UniqueName="lastName" ItemStyle-Width="100px"
                                                                SortExpression="lastName">
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="clientStatus" HeaderText="Status" UniqueName="clientStatus" ItemStyle-Width="50px"
                                                                SortExpression="clientStatus">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="phone" HeaderText="Phone" UniqueName="Phone" ItemStyle-Width="70px"
                                                                SortExpression="Phone">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="email" HeaderText="Email" UniqueName="email" ItemStyle-Width="50px"
                                                                SortExpression="email">
                                                            </telerik:GridBoundColumn>                                                          
                                                            <telerik:GridBoundColumn DataField="caddress" HeaderText="Address" UniqueName="caddress" ItemStyle-Width="100px"
                                                                SortExpression="caddress">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn DataField="sec_firstName" HeaderText="Sec.FirstName" UniqueName="sec_firstName" ItemStyle-Width="100px"
                                                                SortExpression="sec_firstName">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="sec_lastName" HeaderText="Sec.LastName" UniqueName="sec_lastName" ItemStyle-Width="100px"
                                                                SortExpression="sec_lastName">
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="sec_Phone" HeaderText="Sec.Phone" UniqueName="sec_Phone" ItemStyle-Width="50px"
                                                                SortExpression="sec_Phone">
                                                            </telerik:GridBoundColumn>
                                                           <%-- <telerik:GridBoundColumn DataField="sec_email" HeaderText="Sec.Email" UniqueName="sec_email" ItemStyle-Width="50px"
                                                                SortExpression="sec_email">
                                                            </telerik:GridBoundColumn>--%>                                                          
                                                            <telerik:GridBoundColumn DataField="seccaddress" HeaderText="Sec.Address" UniqueName="seccaddress" ItemStyle-Width="100px"
                                                                SortExpression="seccaddress">
                                                            </telerik:GridBoundColumn>
                                                                <telerik:GridButtonColumn CommandName="Edit" Text="View" UniqueName="EditCommandColumn"
                                                                    ButtonType="LinkButton" >
                                                              </telerik:GridButtonColumn>                                                          
                                                        
                                                        </Columns> 
                                             </MasterTableView> 
                                            <%--<ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True"> </ClientSettings> --%>
                                        </telerik:RadGrid> 
                                        <%--<asp:SqlDataSource ID="SqlDataSource3" ConnectionString="<%$ ConnectionStrings:MDMConnectionString %>"
                                            SelectCommand="select *,(address+' '+address2) as caddress from clients" runat="server"></asp:SqlDataSource>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td style="height:20px"></td></tr>
                    <tr>
                        <td colspan="2" align="center" id="td_viewdetails" runat="server" visible="false">
                            <fieldset class="register"  style="width:1200px">
                            <legend><asp:Label ID="lbl_clientname" ForeColor="Green" runat="server"></asp:Label> Subscription Details</legend>
                            <table border="0" cellpadding="0" cellspacing="0px" width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lbl_message_configdetails" runat="server"  ForeColor="Red"></asp:Label>
                                        <asp:Label ID="lbl_subscriptionid" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr><td style="height:5px"></td></tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <table border="0">
                                            <tr>
                                                <td style="border:solid 1px #000000">
                                                    Client Code:<asp:Label ID="lbl_clcode" ForeColor="Green" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td style="border:solid 1px #000000">
                                                    Client Name:<asp:Label ID="lbl_company" ForeColor="Green" runat="server"></asp:Label>
                                                </td>
                                               
                                                <td style="border:solid 1px #000000">
                                                    FirstName:<asp:Label ID="lbl_fname" ForeColor="Green" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td style="border:solid 1px #000000">
                                                    LastName:<asp:Label ID="lbllname" ForeColor="Green" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td style="border:solid 1px #000000">
                                                    Email:<asp:Label ID="lbl_email" ForeColor="Green" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr><td style="height:5px"></td></tr>
                                <tr>
                                    <%--<td valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0px">
                                            <tr>
                                                <td align="right">
                                                    Application Use:
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList id="rad_appuse" RepeatDirection="Horizontal"  runat="server">
                                                       
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>--%>
                                            
                                            <%--<tr>
                                                <td>
                                        
                                                </td>
                                                <td align="left">
                                                    <div class="dynamic-form">
                                                        <div class="inputs">
                                                        </div>
                                                </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>--%>
                                    <td valign="top">
                                        <table border="0" cellpadding="0" cellspacing="2px" width="100%">
                                            <tr>
                                               
                                                <td align="left">
                                                    No. of Assets for the data collection:<br />
                                                    <%--<asp:TextBox ID="txt_nofassets" runat="server" onchange="return formtextboxes(this);"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txt_nofassets" runat="server" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    Allowed Users:<br />
                                                    <telerik:RadTextBox ID="radtxt_allowedusers" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left">
                                                     No. of Users Types:<br />
                                                    <telerik:RadTextBox ID="radtxt_usertypes" runat="server"  ></telerik:RadTextBox>
                                                </td>
                                                <td align="left">
                                                    Subscription Period(Months):<br />
                                                    <telerik:RadTextBox ID="radtxt_subperiod" runat="server" ></telerik:RadTextBox>
                                                </td>
                                                <td align="left">
                                                    Status:<br />
                                                    <telerik:RadComboBox ID="radcombo_clientstatus" Width="130px" runat="server">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Active" Value="Active" />
                                                            <telerik:RadComboBoxItem Text="InActive" Value="InActive" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                   
                                                </td>
                                                <td align="left">
                                                    Select Meter Type Association:<br />
                                                    <asp:CheckBoxList ID="chk_metertype" TextAlign="Right" RepeatDirection="Horizontal" runat="server">
                                                    
                                                    </asp:CheckBoxList>
                                                   
                                                </td>
                                            </tr>
                                            
                                           
                                            
                                        </table>
                                    </td>
                                </tr>
                                
                                
                                
                                 
                            </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr><td style="height:10px"></td></tr>
                    <tr>
                        <td colspan="2" align="center">
                            <table border="0" cellpadding="0" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_Approve" runat="server" Text="Approve" 
                                            onclick="btn_Approve_Click" OnClientClick="return getassetvalues();"   />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Update" runat="server" Text="Save" 
                                            onclick="btn_Update_Click"  OnClientClick="return getassetvalues();"  />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Deny" runat="server" Text="Deny" 
                                            onclick="btn_Deny_Click"   />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_reset" runat="server" Text="Clear" 
                                            onclick="btn_reset_Click"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hid_1" runat="server" />
    <asp:HiddenField ID="hid_viewuserid" runat="server" />
    <asp:HiddenField ID="hid_existvaluesforassets" runat="server" />
     </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_view" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_Approve" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_Update" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_Deny" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>
            
</asp:Content>

