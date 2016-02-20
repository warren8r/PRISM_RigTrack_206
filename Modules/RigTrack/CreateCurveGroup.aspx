<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateCurveGroup.aspx.cs" Inherits="Modules_RigTrack_CreateCurveGroup" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        var $ = $telerik.$;
        var radwindow;
        var surveyWindow;

        function pageLoad() {
            radwindow = $find("<%= RadWindow1.ClientID%>");
            surveyWindow = $find("<%= SurveyWindow.ClientID%>");
        }

        function openRadWindow(CallingButton) {
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
            radwindow.show();
            setWindowsize(CallingButton);
        }

        $(window).resize(function () {
            if (radwindow.isVisible()) {
                setWindowsize();
            }
            if (surveyWindow.isVisible()) {

            }
        });

        function setWindowsize(CallingButton) {
            var viewportWidth = $(window).width();
            var viewportHeight = $(window).height();
            if (CallingButton == 1) {
                radwindow.setSize(1080, 800);
                radwindow.setUrl("AddSurvey_B.aspx");
            }
            else if (CallingButton == 2) {
                radwindow.setSize(780, 910);
                radwindow.setUrl("GenerateMosquito_B.aspx");
            }
            else if (CallingButton == 3) {
                radwindow.setSize(1312, 707);
                radwindow.setUrl("CreateTargets_B.aspx");
            }
            else if (CallingButton == 4) {
                radwindow.setSize(1048, 819);
                radwindow.setUrl("CreateGraph.aspx");
            }
            else {
                radwindow.setSize(760, 860);
                radwindow.setUrl("AddPlot_B.aspx");
            }
            radwindow.center();
            radwindow.set_modal(true);
        }


        function toggle_visibility() {
            event.preventDefault();
            var e = document.getElementById('subseas');
            var f = document.getElementById('regular');
            if (e.style.display == 'none') {
                e.style.display = 'block';
                f.style.display = 'none';
            }
            else {
                e.style.display = 'none';
                f.style.display = 'block';
            }
        }

        //nfv-Survey Window Logic
        function OpenSurveyWindow() {
            //Need to send CurveGroupID To Popup?
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
            var curveID = document.getElementById("<%= CurveNumberHidden.ClientID %>").value;
            alert(curveID);
            if (curveID == '') {
                //alert("No Curve Selected");
                return false;

            }
            else {
                surveyWindow.show();

                var viewportWidth = $(window).width();
                var viewportHeight = $(window).height();

                surveyWindow.setSize(1080, 800);
                surveyWindow.setUrl("AddSurvey_B.aspx");
                surveyWindow.center();
                surveyWindow.set_modal(true);
            }
        }

        function OnClientClose(oWnd, eventArgs) {

            var arg = eventArgs.get_argument();
            var secretButton = document.getElementById("<%= secretButton.ClientID %>");
            if (arg != null) {
                document.getElementById("<%= MeasurementDepthHidden.ClientID %>").value = arg.Arg1;

                document.getElementById("<%= InclinationHidden.ClientID %>").value = arg.Arg2;
                document.getElementById("<%= AzimuthHidden.ClientID %>").value = arg.Arg3;
                
                secretButton.click();
            }

        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 
            return oWindow;
        }





    </script>

    
   

    <telerik:RadWindow ID="RadWindow1" runat="server">
    </telerik:RadWindow>

    <telerik:RadWindow ID="SurveyWindow" runat="server" OnClientClose="OnClientClose">
    </telerik:RadWindow>



    <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>

                 <div style="visibility: hidden">
                     <asp:HiddenField ID="MeasurementDepthHidden" runat="server" />
                    <asp:HiddenField ID="InclinationHidden" runat="server" />
                    <asp:HiddenField ID="AzimuthHidden" runat="server" />
                    <asp:HiddenField ID="CurveIDHidden" runat="server" Value="" />
                    <asp:HiddenField ID="CurveNumberHidden" runat="server" Value="" />
                    <asp:Button ID="secretButton" runat="server" OnClick="secretButton_Click" />
                </div>


                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Create Curve Group</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>

                </asp:Table>




                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table3" runat="server" HorizontalAlign="Center"  BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group ID
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group Name
                                    </asp:TableHeaderCell>

                                </asp:TableRow>

                                <asp:TableRow> 

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtCurveGroupID" Width="160px" ReadOnly="true" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtCurveGroupName" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>

                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>

                 <asp:Table ID="Table13" runat="server" HorizontalAlign="Left" Width="100%">



                    <asp:TableRow>

                        <asp:TableCell Width="17%">
                            &nbsp;
                        </asp:TableCell>

                        <asp:TableCell>

                            <asp:Table ID="Table4" runat="server" HorizontalAlign="Left" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
						             <h6>Job Information </h6>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                      
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Job Number
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtJobNumber" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Company
                                    </asp:TableHeaderCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtCompany" Width="160px" />
                                    </asp:TableCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Lease/Well
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLeaseWell" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Location
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLocation" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Rig Name
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtRigName" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Country
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <%--<telerik:RadTextBox runat="server" ID="txtCountry" Width="160px" />--%>
                                        <telerik:RadDropDownList runat="server" ID="ddlCountry" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						State
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <%--<telerik:RadTextBox runat="server" ID="txtState" Width="160px" />--%>
                                        <telerik:RadDropDownList runat="server" ID="ddlState" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" >
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Declination
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtDeclination" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Grid
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtGrid" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						RKB
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtRKB" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Gl or MSL
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <%--<telerik:RadTextBox runat="server" ID="txtMlorMSL" Width="160px" />--%>
                                        <telerik:RadDropDownList runat="server" ID="ddlGLorMSL" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>

                            <asp:Table runat="server" ID="Table5" HorizontalAlign="Left" Width="3%">

                                <asp:TableRow>
                                    <asp:TableCell>
                                         &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <asp:Table runat="server" ID="Table6" HorizontalAlign="Left" Width="210px" BackColor="#F1F1F1" BorderColor="#3a4f63" BorderStyle="Double">

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center">
						            <h6>Method Of Calculation</h6>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>

                                    </asp:TableCell>
                                </asp:TableRow>




                                <asp:TableRow>
                                    <asp:TableCell CssClass="HeaderCenter">
						Minimum Curvature
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnMinimumCurvature" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="Radios" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="tooltipDemoMinimumCurvature" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                 
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        Radius Of Curvature
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnRadiusOfCurvature" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="Radios" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="tooltipDemoRadiusCurvature" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        Average Angle
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnAvergaeAngle" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="Radios" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="tooltipDemoAverageAngle" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                      Tangential
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnTangentail" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="Radios" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="tooltipDemoTangential" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        Balanced Tangential
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnBalancedTangentail" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="Radios" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="tooltipDemoBalancedTangential" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                    <b>Output Direction</b>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>

                                        <asp:Table ID="Table14" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>

                                                <asp:TableCell>
                                    Decimal (350.0)
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnOutputDecimal" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="OutInput" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableCell>

                                        <asp:Table ID="Table15" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>

                                                <asp:TableCell>
                                    Quadrant (N 10 W)
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnOutputQuadrant" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="OutInput" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>

                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                      &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                      <b>Vertical Section Reference</b>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell>
                                    Wellhead
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnWellhead" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="RadioVerticalsection" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                       &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                    Other Reference
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnOtherReference" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="RadioVerticalsection" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell>
                                       
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                       
                                    </asp:TableCell>
                                </asp:TableRow>




                            </asp:Table>

                            <asp:Table runat="server" ID="Table8" HorizontalAlign="Left" Width="1%">

                                <asp:TableRow>
                                    <asp:TableCell>
                                             &nbsp;
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>



                            <asp:Table runat="server" ID="Table2" HorizontalAlign="Left" Width="238px" BackColor="#F1F1F1" BorderColor="#3a4f63" BorderStyle="Double">

                                
                              

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
						         <h6>Measurement Units</h6>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell BorderColor="#597791" BorderStyle="Solid" BackColor="#ffffcc">
                                        First Select Yes or No if you want the current data converted.
                                        Then Select the Units. All Data will be converted
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                      
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                      <b>Convert</b>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell>

                                        <asp:Table ID="Table10" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>


                                                <asp:TableCell>
                                        Yes
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnYes" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="YesNo" AutoPostBack="True" BackColor="transparent" OnCheckedChanged="btnYes_CheckedChanged" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>



                                                <asp:TableCell>
                                        No
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnNo" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="YesNo" AutoPostBack="True" BackColor="transparent"  CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>

                                                <asp:TableCell>&nbsp;</asp:TableCell>


                                                <asp:TableCell>
                                        
                                                   Meters
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnMeters" Enabled="false" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="FeetMeter" AutoPostBack="True" BackColor="transparent" OnCheckedChanged="btnMeters_CheckedChanged" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>



                                                <asp:TableCell>
                                        Feet
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnFeet" Enabled="false" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="FeetMeter" AutoPostBack="True" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>

                                            </asp:TableRow>

                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <b>Dog leg Severity</b>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>

                                        <asp:Table ID="Table7" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>


                                                <asp:TableCell>
                                        Per 30 Meters
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnPer30Meters" Enabled="false" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="PerMeters" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>

                                                <asp:TableCell>&nbsp;</asp:TableCell>

                                                <asp:TableCell>
                                        Per 10 Meters
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnPer10meters" runat="server" Enabled="false" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="PerMeters" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>


                                            </asp:TableRow>

                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>






                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>





                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <b>Input Direction</b>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell>

                                        <asp:Table ID="Table9" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>


                                                <asp:TableCell>
                                        Decimal (350.0)
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnInputDecimal" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="InInput" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>


                                            </asp:TableRow>

                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                      
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>

                                        <asp:Table ID="Table11" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>


                                                <asp:TableCell>
                                        Quadrant (N 10 W)
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadButton ID="btnInputQuadrant" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                                        GroupName="InInput" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                </asp:TableCell>


                                            </asp:TableRow>



                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                       &nbsp;
                                    </asp:TableCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                     <b>Vertical Section Reference</b>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Table ID="Table30" runat="server" HorizontalAlign="Center">

                                            <asp:TableRow>
                                                <asp:TableCell>
                                       EW Offset
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtEWOffset" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Table ID="Table23" runat="server" HorizontalAlign="Center">

                                            <asp:TableRow>
                                                <asp:TableCell>
                                       NS Offset
                                                </asp:TableCell>


                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtNSOffset" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>

                                </asp:TableRow>


                                <%-- // new spacing remmember--%>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        &nbsp
                                    </asp:TableCell>
                                </asp:TableRow>




                                <asp:TableRow>
                                    <asp:TableCell>
                                       
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>

                                    </asp:TableCell>
                                </asp:TableRow>



                            </asp:Table>

                            <asp:Table ID="Table12" runat="server" HorizontalAlign="Left" Width="3%">

                                <asp:TableRow>
                                    <asp:TableCell>
                                         &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>





                            <asp:Table runat="server" ID="Table18" HorizontalAlign="Left" Width="10px" BackColor="#F1F1F1" BorderColor="#3a4f63" BorderStyle="Double">

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
						         <h6>Related Curve Information</h6>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>

                                        <asp:Table ID="Table19" runat="server">


                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Work Number 
                                                </asp:TableHeaderCell>


                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						DLS
                                                </asp:TableHeaderCell>


                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtWorkNumber" Width="160px" />
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtDLS" Width="160px" />
                                                </asp:TableCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Plan Number
                                                </asp:TableHeaderCell>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						TFO
                                                </asp:TableHeaderCell>


                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtPlanNumber" Width="160px" />
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtTFO" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>


                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						MD
                                                </asp:TableHeaderCell>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Closure
                                                </asp:TableHeaderCell>


                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtMD" Width="160px" />
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtClosure" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>


                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Incl.
                                                </asp:TableHeaderCell>


                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Location 
                                                </asp:TableHeaderCell>


                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtIncl" Width="160px" />
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtLocationCurveGroup" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>


                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Azimuth
                                                </asp:TableHeaderCell>


                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Bit To Sensor
                                                </asp:TableHeaderCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtAzimuth" Width="160px" />
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtBitToSensor" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>


                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						TVD
                                                </asp:TableHeaderCell>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Least Distance
                                                </asp:TableHeaderCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtTVD" Width="160px" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <telerik:RadDropDownList runat="server" ID="ddlLeastDistance" Width="160px">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="0" Text="Off" />
                                                            <telerik:DropDownListItem Value="1" Text="On" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </asp:TableCell>
                                                
                                            </asp:TableRow>




                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						N-S Coord
                                                </asp:TableHeaderCell>


                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Least Distance
                                                </asp:TableHeaderCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtNSCoord" Width="160px" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtLeastDistance" Width="160px" />
                                                </asp:TableCell>
                                                
                                            </asp:TableRow>


                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						E-W Coord
                                                </asp:TableHeaderCell>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						H.Side
                                                </asp:TableHeaderCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtEWCoord" Width="160px" />
                                    </asp:TableCell>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtHSide" Width="160px" />
                                                </asp:TableCell>

                                    
                                </asp:TableRow>


                                <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						V-Section
                                                </asp:TableHeaderCell>


                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						TVD Comp
                                                </asp:TableHeaderCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtvsection" Width="160px" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtTVDComp" Width="160px" />
                                    </asp:TableCell>
                                                
                                            </asp:TableRow>


                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						W Rate
                                                </asp:TableHeaderCell>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						Comparison Curve
                                                </asp:TableHeaderCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtWRate" Width="160px" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtComparisonCurve" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
						B Rate 
                                                </asp:TableHeaderCell>

                                                <asp:TableHeaderCell CssClass="HeaderCenter">
                        @
                                                </asp:TableHeaderCell>

                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtBRate" Width="160px" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <telerik:RadTextBox runat="server" ID="txtAT" Width="160px" />
                                                </asp:TableCell>
                                            </asp:TableRow>



                                        </asp:Table>



                                                </asp:TableCell>




                                            </asp:TableRow>


                                        </asp:Table>




                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>

                <asp:Table ID="btnTable" runat="server" Width="100%">

                    <asp:TableRow>

                        <asp:TableCell Width="41%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnApply" runat="server" Text="Apply" OnClick="BtnApply_Click" />
                        </asp:TableCell>

                        <asp:TableCell Width="59%"></asp:TableCell>

                    </asp:TableRow>

                </asp:Table>


                <telerik:RadGrid ID="RadGrid1" AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30"
                    OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView EditMode="InPlace" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added.">
                        <Columns>
                            <%--<telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderText="Edit" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection" AutoPostBack="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridEditCommandColumn ButtonType="LinkButton" HeaderText="Edit"></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderText="ID" DataField="ID" UniqueName="ID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Group ID" DataField="CurveGroupID" UniqueName="CurveGroupID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve #" DataField="Number" UniqueName="Number" SortExpression="Number"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Name" DataField="Name" UniqueName="Name" SortExpression="Name"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn HeaderText="Curve Type" DataField="CurveTypeName" UniqueName="CurveTypeName" SortExpression="CurveTypeName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn HeaderText="Curve Type ID" DataField="CurveTypeID" UniqueName="CurveTypeID" SortExpression="CurveTypeID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CurveType" HeaderText="Curve Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurveType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurveTypeName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCurveType" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="North Offset" DataField="NorthOffset" UniqueName="NorthOffset" SortExpression="NorthOffset"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="East Offset" DataField="EastOffset" UniqueName="EastOffset" SortExpression="EastOffset"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="VS Direction" DataField="VSDirection" UniqueName="VSDirection" SortExpression="VSDirection"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="RKB Elevation" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

                  <asp:Table ID="Table24" runat="server" Width="100%">

                    <asp:TableRow>

                  
                        <asp:TableCell>
                           &nbsp;
                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>

                <telerik:RadGrid ID="RadGridSurveys" AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="15"
                    OnNeedDataSource="RadGridSurveys_NeedDataSource" OnUpdateCommand="RadGridSurveys_UpdateCommand" OnItemDataBound="RadGridSurveys_ItemDataBound">
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView EditMode="InPlace" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data hase been added.">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="MD" DataField="MD" UniqueName="MD" SortExpression="MD"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Inclination" DataField="Inclination" UniqueName="Inclination" SortExpression="Inclination"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Azimuth" DataField="Azimuth" UniqueName="Azimuth" SortExpression="Azimuth"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="TVD" DataField="TVD" UniqueName="TVD" SortExpression="TVD"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Subseas-TVD" DataField="SubseasTVD" UniqueName="SubseasTVD" SortExpression="SubseasTVD"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="NS" DataField="NS" UniqueName="NS" SortExpression="NS"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="EW" DataField="EW" UniqueName="EW" SortExpression="EW"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Vertical Section" DataField="VerticalSection" UniqueName="VerticalSection" SortExpression="VerticalSection"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="CL" DataField="CL" UniqueName="CL" SortExpression="CL"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Closure Distance" DataField="ClosureDistance" UniqueName="ClosureDistance" SortExpression="ClosureDistance"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Closure Direction" DataField="ClosureDirection" UniqueName="ClosureDirection" SortExpression="ClosureDirection"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="DLS" DataField="DLS" UniqueName="DLS" SortExpression="DLS"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="BR" DataField="BR" UniqueName="BR" SortExpression="BR"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="WR" DataField="WR" UniqueName="WR" SortExpression="WR"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="TFO" DataField="TFO" UniqueName="TFO" SortExpression="TFO"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>






                <asp:Table ID="ButtonTable" runat="server" Width="100%" HorizontalAlign="Center">


                    <asp:TableRow>

                        <asp:TableCell Width="40%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnAddSurvey" runat="server" OnClientClick="OpenSurveyWindow();" AutoPostBack="false" Text="Add Survey to Selected Curve"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnEditSelectedSurvey" runat="server" OnClientClick="openRadWindow(1);" AutoPostBack="false" Text="Edit Selected Survey"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnRemoveSurvey" runat="server" OnClick="btnRemoveSurvey_Click" Text="Remove Survey"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnSubSeas" runat="server" OnClick="btnSubSeas_Click1" Text="SubSeas" AutoPostBack="false"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnNormal" runat="server" OnClientClick="openRadWindow(2);" AutoPostBack="false" Text="Normal"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="40%"></asp:TableCell>

                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="TableSpacing" runat="server" Width="100%">
                   <asp:TableRow>
                       <asp:TableCell>
                           &nbsp;
                       </asp:TableCell>
                   </asp:TableRow>
                </asp:Table>

                  <telerik:RadGrid ID="RadGridTargets" ShowHeadersWhenNoRecords="true" AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30"
                    OnNeedDataSource="RadGridTargets_NeedDataSource" OnUpdateCommand="RadGridTargets_UpdateCommand" OnItemDataBound="RadGridTargets_ItemDataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView EditMode="InPlace" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added.">
                        <Columns>
                           
                            <telerik:GridEditCommandColumn ButtonType="LinkButton" HeaderText="Edit"></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderText="ID" DataField="ID" UniqueName="ID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Group ID" DataField="CurveGroupID" UniqueName="CurveGroupID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Target #" DataField="Number" UniqueName="Number" SortExpression="Number"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Target Shape" DataField="Name" UniqueName="Name" SortExpression="Name"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                           
                            <telerik:GridBoundColumn HeaderText="TVD" DataField="CurveTypeID" UniqueName="CurveTypeID" SortExpression="CurveTypeID"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="NS" HeaderText="N-S Coordinate">
                               <%-- <ItemTemplate>
                                    <asp:Label ID="lblCurveType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurveTypeName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCurveType" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>--%>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="E-W Coordinate" DataField="NorthOffset" UniqueName="NorthOffset" SortExpression="NorthOffset"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Polar Direction" DataField="EastOffset" UniqueName="EastOffset" SortExpression="EastOffset"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Polar Distance" DataField="VSDirection" UniqueName="VSDirection" SortExpression="VSDirection"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="INC From Last Target" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="AZM From Last Target" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="Inclination At Target" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="Azimuth At Target" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="Number Vertices" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="Rotation" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="Target Thickness" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="Target Comment" DataField="RKBElevation" UniqueName="RKBElevation" SortExpression="RKBElevation"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

               

                <asp:Table ID="Table17" runat="server" Width="100%" HorizontalAlign="Center">
                    <asp:TableRow>

                        <asp:TableCell Width="35%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnAddTarget" runat="server" OnClientClick="openRadWindow(3);" AutoPostBack="false" Text="Add Target"></asp:Button>
                        </asp:TableCell>


                        <asp:TableCell Width="5%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnAddTargetFromSelectedSurvey" runat="server" OnClientClick="openRadWindow(3);" AutoPostBack="false" Text="Add Target from Selected Survey"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>


                        <asp:TableCell>
                            <asp:Button ID="btnEditSelectedTarget" runat="server" OnClientClick="openRadWindow(3);" AutoPostBack="false" Text="Edit Selected Target"></asp:Button>

                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>


                        <asp:TableCell>
                            <asp:Button ID="btnViewTarget" runat="server" OnClientClick="openRadWindow(3);" AutoPostBack="false" Text="View Target"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>


                        <asp:TableCell>
                            <asp:Button ID="btnRemoveTarget" runat="server" OnClick="btnRemoveTarget_Click" Text="Remove Target"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>


                        <asp:TableCell>
                            <asp:Button ID="btnCreateGraphToSelectedTarget" runat="server" OnClientClick="openRadWindow(4);" AutoPostBack="false" Text="Create Graph to Selected Target"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>


                        <asp:TableCell>
                            <asp:Button ID="btnGeneratePlot" runat="server" OnClientClick="openRadWindow(5);" AutoPostBack="false" Text="Generate Plot"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="35%"></asp:TableCell>



                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="Table20" runat="server" Width="100%" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell>
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>

                        <asp:TableCell Width="45%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="5%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="45%"></asp:TableCell>

                    </asp:TableRow>
                </asp:Table>


            </ContentTemplate>
            <Triggers>

                <%-- <asp:PostBackTrigger ControlID="btnSubSeas" />--%>

                 <asp:PostBackTrigger ControlID="RadGrid1" />
                <asp:PostBackTrigger ControlID="secretButton" />

                <%--   <asp:AsyncPostBackTrigger ControlID="RadGridSurveys" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnRemoveSurvey" EventName="Click" />
                <%-- <asp:AsyncPostBackTrigger ControlID="btnSubSeas" EventName="Click" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnRemoveTarget" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCreateGraphToSelectedTarget" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

