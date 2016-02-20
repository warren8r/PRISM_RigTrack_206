<%@ Page Language="C#" Title="View Reports" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewReports.aspx.cs" Inherits="Modules_RigTrack_ViewReports" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var $ = $telerik.$;
            var radwindow;

            function pageLoad() {
                radwindow = $find("<%= RadWindow1.ClientID%>");
            }


            $(window).resize(function () {
                if (radwindow.isVisible()) {
                    SetSizeForWindow()
                }
            });

            function SetSizeForWindow() {
                var viewportWidth = $(window).width();
                var viewportHeight = $(window).height();
                radwindow.setSize(Math.ceil(viewportWidth - 90), Math.ceil(viewportHeight - 90));
            }

            function openRadWindow() {

                var raddropdownlist = $find('<%=ddlCurveGroupID.ClientID %>');
                var CurveGroupID = raddropdownlist.get_selectedItem().get_value();

                var raddropdownlist2 = $find('<%=ddlTarget.ClientID %>');
                var TargetID = raddropdownlist2.get_selectedItem().get_value();
               
             
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
                  setWindowsize(CurveGroupID,TargetID);
              }

          

            function setWindowsize(CurveGroupID, TargetID) {
                SetSizeForWindow()
                radwindow.setUrl("ViewAllReports.aspx?CurveGroupID=" + CurveGroupID + "&" + "TargetID=" + TargetID);
                radwindow.center();
                radwindow.set_modal(true);
            }

          
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadWindow ID="RadWindow1" runat="server">
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

                    <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>View Reports</h2>
                            </asp:TableCell>
                        </asp:TableRow>



                    </asp:Table>


                    <asp:Table ID="Table6" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table7" runat="server" HorizontalAlign="Center"  BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Job/Curve Group 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Target
                                    </asp:TableHeaderCell>

                                       <asp:TableHeaderCell CssClass="HeaderCenter">
						View Report
                                    </asp:TableHeaderCell>


                                </asp:TableRow>


                                <asp:TableRow>


                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="350px" AutoPostBack="true" DropDownWidth="200px" OnSelectedIndexChanged="ddlCurveGroupID_SelectedIndexChanged" ID="ddlCurveGroupID"  Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                       
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlTarget" Width="160px"  AppendDataBoundItems="true"  DropDownHeight="200px" OnSelectedIndexChanged="ddlTarget_SelectedIndexChanged"   AutoPostBack="true">
                                             <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                       
                                    </asp:TableCell>

                                    <asp:TableCell>
                                         <Telerik:RadButton ID="btnViewReport" runat="server" Text="View Report" OnClientClicked="openRadWindow" AutoPostBack="false"   />
                                    </asp:TableCell>

                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>



                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </fieldset>
        <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
