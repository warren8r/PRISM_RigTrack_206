<%@ Page Title="Create Jobs" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateJobs.aspx.cs" Inherits="Modules_RigTrack_CreateJobs" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        var $ = $telerik.$;
        var companyWindow;
        var WellPlanWindow;

        function pageLoad() {
            companyWindow = $find("<%= CompanyWindow.ClientID %>");
            WellPlanWindow = $find("<%= WellPlanWindow.ClientID %>");
        }
        //To create Company thru Job screen
        function OpenCompanyWindow() {
            var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
            // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
            var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
            var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
            // At least Safari 3+: "[object HTMLElementConstructor]"
            var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
            var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6


            if (isFirefox) {
            }
            else {
                event.preventDefault();
            }
            companyWindow.show();
            setWindowSize();
        }
        //To upload a Well Plan
        function OpenWellPlan() {
            //var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
            //// Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
            //var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
            //var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
            //// At least Safari 3+: "[object HTMLElementConstructor]"
            //var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
            //var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6
            //if (isFirefox) {
            //}
            //else {
            //    event.preventDefault();
            //}
            

            var CurveGroupID = $('#<%= hiddenField.ClientID %>').val();
            
            SetWellPlanWindow(CurveGroupID);
        }
        //Resize Window
        $(window).resize(function () {
            if (WellPlanWindow.isVisible()) {
                
                SetSizeForWindow2();
            }
          
            

            if (companyWindow.isVisible()) {
                
                SetSizeForWindow();
            }
           
        });

        function SetSizeForWindow() {
            var viewportWidth = $(window).width();
            var viewportHeight = $(window).height();
            companyWindow.setSize(Math.ceil(viewportWidth - 90), Math.ceil(viewportHeight - 90));
        }
        function SetSizeForWindow2() {
            var viewportwidth = $(window).width();
            var viewportheight = $(window).height();
            
            WellPlanWindow.setSize(Math.ceil(viewportwidth - 90), Math.ceil(viewportheight - 90));
            
            
        }

        function setWindowSize() {
            SetSizeForWindow();
            companyWindow.setUrl("ManageCompany.aspx?Modal=true");
            companyWindow.center();
            companyWindow.set_modal(true);
        }

        //function SetWellPlanWindow(curveGroupID) {
        //    WellPlanWindow.show();
        //    SetSizeForWindow2();
        //    WellPlanWindow.setUrl("AddWellPlan.aspx?CurveGroupID=" + curveGroupID);
        //    WellPlanWindow.center();
        //    WellPlanWindow.set_modal(true);
        //}
        //After closing Company Popup/rebind company dropdown
        function RefreshParentPage() {
            
            $.ajax({
                type: "POST",
                url: 'CreateJobs.aspx/RebindCompany',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var companies = data.d;
                    var raddropdownlist = $find("<%= ddlCompany.ClientID %>");
                    raddropdownlist.get_items().clear();
                    
                    for (i = 0; i < companies.length; i++) {
                        var comboItem = new Telerik.Web.UI.DropDownListItem
                        comboItem.set_text(companies[i].CompanyName);
                        
                        comboItem.set_value(companies[i].ID);
                        raddropdownlist.get_items().add(comboItem);
                    }
                    raddropdownlist.commitChanges();
                    
                },
                error: function (jqXHR, textStatus, error) {
                    alert(error);
                }

                
            });


           
        }
        

        function OnClientBeforeClose(sender,args) {
            var CurveGroupID = $('#<%= hiddenField.ClientID %>').val();
            
            if (WellPlanWindow.argument == 'true') {
                
                args.set_cancel(false);
                window.location = "CreateSurvey.aspx?CurveGroupID=" + CurveGroupID;
                
            }
            else {
                var choice = confirm("Are you sure you want to cancel Well Plan upload? This will delete job info");
                if (choice) {
                    //Close Window
                    $.ajax({
                        type: "POST",
                        url: 'CreateJobs.aspx/DeleteCurveGroupInfo',
                        data: '{CurveGroupID: "' + CurveGroupID + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                            args.set_cancel(false);
                            
                            __doPostBack();
                        },
                        error: function (jqXHR, textStatus, error) {
                            alert(error);

                        }

                    });
                    
                    
                }
                else {
                    args.set_cancel(true);
                }
            }
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


    <fieldset>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <input type="hidden" id="hiddenField" runat="server" />
                
               
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" style="z-index:99999999;" EnableViewState="false" >
                    <Windows>
                        <telerik:RadWindow Behaviors="Close" ID="CompanyWindow" runat="server" OnClientClose="RefreshParentPage"></telerik:RadWindow>
                        <telerik:RadWindow Behaviors="Close" ID="WellPlanWindow" runat="server" KeepInScreenBounds="true" OnClientBeforeClose="OnClientBeforeClose" ></telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>

                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Create Jobs/Curve Groups</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                   
                </asp:Table>


               

                <asp:Table ID="Table11" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table12" runat="server" HorizontalAlign="Center" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group Name <font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Company<font style="color:red !important; font-size:smaller">*</font> <asp:LinkButton ID="LinkButton1" runat="server" Text="Create New" Font-Size="XX-Small"  OnClientClick="OpenCompanyWindow(); return false;"  ></asp:LinkButton>
                                        
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Lease/Well<font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Location<font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Rig Name<font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Job Number<font style="color:red !important; font-size:smaller">*</font>
                                    </asp:TableHeaderCell>

                                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Job Start Date
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        GL or MSL
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>



                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtCurveGroupName" Width="160px" />
                                        <asp:RequiredFieldValidator ID="RFCurveGroupName" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="TxtCurveGroupName" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </asp:TableCell>

                                    <asp:TableCell>
                                        
                                        <telerik:RadDropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator ID="RFCompany" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="ddlCompany" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLeaseWell" Width="160px" />
                                        <asp:RequiredFieldValidator ID="RFLeaseWell" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="txtLeaseWell" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLocation" Width="160px" />
                                        <asp:RequiredFieldValidator ID="RFLocation" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="txtLocation" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtRigName" Width="160px" />
                                        <asp:RequiredFieldValidator ID="RFRigName" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="txtRigName" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtJobNumber" Width="160px" />
                                        <asp:RequiredFieldValidator ID="RFJobNumber" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="txtJobNumber" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="datepicker_JobStartDate" runat="server" Width="160px" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RFStartDate" ValidationGroup="GroupCurves" ForeColor="Red" runat="server" ErrorMessage="Required" ControlToValidate="datepicker_JobStartDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlGLorMSL" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>

                                </asp:TableRow>

                                  <asp:TableRow>

                                    
                                    <%--<asp:TableHeaderCell CssClass="HeaderCenter">
						Gl or MSL
                                    </asp:TableHeaderCell>--%>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						RKB
                                    </asp:TableHeaderCell>



                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Country
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						State
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                        Latitude
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                        Longitude
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Method Of Calculation 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Grid
                                    </asp:TableHeaderCell>




                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Meters/Feet
                                    </asp:TableHeaderCell>


                                </asp:TableRow>

                                
                                <asp:TableRow>

                                    <%--<asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlGLorMSL" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>--%>



                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtRKB" Width="160px" />
                                    </asp:TableCell>

                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlCountry" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>

                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlState" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLatitude" Width="160px"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLongitude" Width="160px"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlMethodOfCalculation" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtGrid" Width="160px" />
                                    </asp:TableCell>


                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlMetersFeet" AutoPostBack="true" OnSelectedIndexChanged="ddlMetersFeet_SelectedIndexChanged" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />

                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>

                                </asp:TableRow>


                                     <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Dog Leg Severity
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Declination
                                    </asp:TableHeaderCell>




                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Output Direction
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Input Direction
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Vertical Reference
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						NS Offset
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						EW Offset
                                    </asp:TableHeaderCell>

                                </asp:TableRow>

                                   <asp:TableRow>


                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlDogLegSeverity" Enabled="false" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />

                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtDeclination" Width="160px" />
                                        <asp:RegularExpressionValidator ID="RFDeclination" ForeColor="Red" ValidationGroup="GroupCurves" ErrorMessage="Value must be numeric." ControlToValidate="txtDeclination" runat="server" ValidationExpression="^-?[0-9]{0,6}(\.[0-9]{1,4})?$" Display="Dynamic" />
                                    </asp:TableCell>





                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlOutPutDirection" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>

                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlInputDirection" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>




                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlVerticalSectionReference" AutoPostBack="true" OnSelectedIndexChanged="ddlVerticalSectionReference_SelectedIndexChanged" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" Enabled="false" DisabledStyle-BackColor="#D3D3D3" ID="txtNSOffset" Width="160px" />
                                        <asp:RegularExpressionValidator ID="RFNSOffset" ForeColor="Red" ValidationGroup="GroupCurves" ErrorMessage="Value must be numeric." ControlToValidate="txtNSOffset" runat="server" ValidationExpression="^-?[0-9]{0,6}(\.[0-9]{1,4})?$" Display="Dynamic" />
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" Enabled="false" DisabledStyle-BackColor="#D3D3D3" ID="txtEWOffset" Width="160px" />
                                        <asp:RegularExpressionValidator ID="RFEWOffset" ForeColor="Red" ValidationGroup="GroupCurves" ErrorMessage="Value must be numeric." ControlToValidate="txtEWOffset" runat="server" ValidationExpression="^-?[0-9]{0,6}(\.[0-9]{1,4})?$" Display="Dynamic" />
                                    </asp:TableCell>


                                </asp:TableRow>
                                <%--<asp:TableRow>
                                    <asp:TableHeaderCell>
                        Latitude
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                        Longitude
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLatitude" Width="160px"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLongitude" Width="160px"></telerik:RadTextBox>
                                    </asp:TableCell>
                                </asp:TableRow>--%>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>



                <asp:Table ID="ButtonTable" runat="server" Width="100%">
                    <asp:TableRow>

                        <asp:TableCell Width="49%"></asp:TableCell>

                      
                        <asp:TableCell>
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="GroupCurves" CausesValidation="true" OnClick="btnSave_Click" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="false" OnClick="btnClear_Click" />
                        </asp:TableCell>
                        <%--<asp:TableCell>
                            <telerik:RadButton ID="btnAddWellPlan" runat="server" Text="Add Well Plan" OnClientClicked="OpenWellPlan" AutoPostBack="false" Enabled="false" />
                            
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblWellplanNeeded" runat="server" ForeColor="Red" Text="*Current Job Needs Well Plan" Visible="false"></asp:Label>
                        </asp:TableCell>--%>

                        <asp:TableCell Width="50%"></asp:TableCell>
                    </asp:TableRow>

                </asp:Table>


                <asp:Table ID="GridTable1" Visible="false" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>

                        <asp:TableCell Width="50%"></asp:TableCell>

                        <asp:TableCell>

                            <telerik:RadGrid ID="RadGrid1"  AllowPaging="True" AllowMultiRowSelection="false" AllowSorting="True" GridLines="None" PageSize="5" runat="server" AllowFilteringByColumn="false"
                                OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound" Height="80px"  Width="1140px">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="true"></Selecting>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <EditItemStyle CssClass="EditedItem" Height="25px"></EditItemStyle>

                                <MasterTableView EditMode="InPlace" AutoGenerateColumns="False" GridLines="Horizontal" DataKeyNames="ID" NoMasterRecordsText="No Data has been added.">
                                    <Columns>
                                        <telerik:GridEditCommandColumn ButtonType="LinkButton" HeaderStyle-Width="100px"  HeaderText="Edit"></telerik:GridEditCommandColumn>

                                        <telerik:GridBoundColumn HeaderText="Curve Group ID"  DataField="ID" SortExpression="ID"
                                            UniqueName="ID" Visible="false" >

                                        </telerik:GridBoundColumn>

                                         <telerik:GridTemplateColumn UniqueName="ID2" HeaderStyle-Width="120px"  HeaderText="Curve Group ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurveGroupID2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <telerik:RadTextBox runat="server" Enabled="false" ID="CurveGroupID2" Width="90px" />
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>


                                          <telerik:GridBoundColumn HeaderText="Curve Group Name" HeaderStyle-Width="170px" DataField="CurveGroupName" SortExpression="CurveGroupName"
                                            UniqueName="CurveGroupName">
                                        </telerik:GridBoundColumn>

                                        



                                        <telerik:GridBoundColumn HeaderText="Job Start Date" DataField="JobStartDate" UniqueName="JobStartDate"
                                            SortExpression="JobStartDate" Visible="false">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridTemplateColumn UniqueName="JobStartDate2" HeaderStyle-Width="100px" DataType="System.DateTime" HeaderText="Job Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "JobStartDate")) %>'></asp:Label>

                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDatePicker ID="datepicker_JobStartDateEdit" Width="92px" runat="server" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        


                                        <telerik:GridTemplateColumn UniqueName="Company" HeaderStyle-Width="180px" HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlCompanyGrid" DropDownHeight="270px" DropDownWidth="200px" Width="170px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Lease/Well" HeaderStyle-Width="180px" DataField="LeaseWell" UniqueName="LeaseWell"
                                            SortExpression="LeaseWell">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Location" HeaderStyle-Width="180px" DataField="JobLocation" UniqueName="JobLocation"
                                            SortExpression="JobLocation">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Rig Name" HeaderStyle-Width="180px" DataField="RigName" UniqueName="RigName"
                                            SortExpression="RigName">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Job Number" HeaderStyle-Width="180px" DataField="JobNumber" UniqueName="JobNumber"
                                            SortExpression="JobNumber">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="GL Or MSL"  DataField="GLName" UniqueName="GLName"
                                            SortExpression="GLName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="GLorMSL" HeaderStyle-Width="80px" HeaderText="GL or MSL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGLorMSL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlGLorMSL" Width="60px"  runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Grid" HeaderStyle-Width="180px" DataField="Grid" UniqueName="Grid"
                                            SortExpression="Grid">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="RKB" HeaderStyle-Width="180px" DataField="RKB" UniqueName="RKB"
                                            SortExpression="RKB">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Country" DataField="CountryName" UniqueName="CountryName"
                                            SortExpression="CountryName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="Country" HeaderStyle-Width="180px" HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CountryName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlCountry" DropDownHeight="270px" DropDownWidth="200px" Width="170px"  runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="State" DataField="StateName" UniqueName="StateName"
                                            SortExpression="StateName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="State" HeaderStyle-Width="155px" HeaderText="State">
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StateName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlState" DropDownHeight="270px" DropDownWidth="150px" Width="135px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>



                                        <telerik:GridBoundColumn HeaderText="Method Of Calculation" DataField="MethodName" SortExpression="MethodName"
                                            UniqueName="MethodName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="MethodOfCalculation" HeaderStyle-BackColor="Blue" HeaderStyle-Width="160px" HeaderText="Method of Calculation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMethodOfCalculation" runat="server" ForeColor="Blue" Text='<%# DataBinder.Eval(Container.DataItem, "MethodName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlMethodOfCalculation" Width="145px"  runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>





                                        <telerik:GridBoundColumn HeaderText="Meters/Feet" DataField="MetersFeet" UniqueName="MetersFeet"
                                            SortExpression="MetersFeet" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="MetersFeet" HeaderStyle-Width="82px" HeaderText="Meters/Feet">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMetersFeet" runat="server" ForeColor="Blue" Text='<%# DataBinder.Eval(Container.DataItem, "MetersFeet") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlMetersFeet" Width="70px" runat="server">
                                                    <Items>
                                                        <telerik:DropDownListItem Value="1000" Text="Feet" />
                                                        <telerik:DropDownListItem Value="1001" Text="Meters" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Dog Leg Severity" DataField="DogLegName" UniqueName="DogLegName"
                                            SortExpression="DogLegName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="DogLegName" HeaderStyle-Width="120px"  HeaderText="Dog Leg Severity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDogLeg" runat="server" ForeColor="Blue" Text='<%# DataBinder.Eval(Container.DataItem, "DogLegName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlDogLeg" Width="100px"  runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Declination" ItemStyle-ForeColor="Blue" HeaderStyle-Width="80px" DataField="Declination" UniqueName="Declination"
                                            SortExpression="Declination">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Output Direction" DataField="OutPutName" UniqueName="OutPutName"
                                            SortExpression="OutPutName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="OutPutName" HeaderStyle-Width="140px" HeaderText="Output Direction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOutputName" runat="server" ForeColor="Blue" Text='<%# DataBinder.Eval(Container.DataItem, "OutPutName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlOutputName" Width="130px"  runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Input Direction" DataField="InputName" UniqueName="InputName"
                                            SortExpression="InputName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="InputName" HeaderStyle-Width="140px" HeaderText="Input Direction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInputName" runat="server" ForeColor="Blue" Text='<%# DataBinder.Eval(Container.DataItem, "InputName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlInputName" Width="130px"  runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Vertical Reference" DataField="VSectionName" UniqueName="VSectionName"
                                            SortExpression="VSectionName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="VSectionName" HeaderStyle-Width="135px" HeaderText="Vertical Reference">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVerticalSection" runat="server" ForeColor="Blue" Text='<%# DataBinder.Eval(Container.DataItem, "VSectionName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlVerticalSection"  Width="125px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="NS Offset" ItemStyle-ForeColor="Blue" HeaderStyle-Width="80px" DataField="NSOffset" UniqueName="NSOffset"
                                            SortExpression="NSOffset">
                                        </telerik:GridBoundColumn>


                                        <telerik:GridBoundColumn HeaderText="EW Offset" ItemStyle-ForeColor="Blue" HeaderStyle-Width="80px" DataField="EWOffset" UniqueName="EWOffset"
                                            SortExpression="EWOffset">
                                        </telerik:GridBoundColumn>


                                    </Columns>

                                </MasterTableView>
                            </telerik:RadGrid>

                        </asp:TableCell>

                         <asp:TableCell Width="50%"></asp:TableCell>

                    </asp:TableRow>

                </asp:Table>


            </ContentTemplate>

        </asp:UpdatePanel>

    </fieldset>

        <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>

</asp:Content>
