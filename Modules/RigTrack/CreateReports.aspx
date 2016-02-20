<%@ Page Language="C#" Title="Create Report" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateReports.aspx.cs" Inherits="Modules_RigTrack_CreateReports" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Close() {
                GetRadWindow().close(); // Close the window 
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

                return oWindow;
            }


        </script>

        <script type="text/javascript">
            function Demo(sender, args) {
                var upload = $find("<%= AttachmentUpload.ClientID %>");

                if (upload.getUploadedFiles().length != 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        </script>
    </telerik:RadCodeBlock>



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
                            <h2>Create Report Templates</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="70%"></asp:TableCell>
                        <asp:TableCell>
<%--                            <asp:CustomValidator runat="server" ID="CustomValidator" ValidationGroup="validateUpload"
                                ClientValidationFunction="Demo" ForeColor="Red" ErrorMessage="Select a Logo"
                                ValidateEmptyText="true">
                            </asp:CustomValidator>--%>
                        </asp:TableCell>
                        <asp:TableCell Width="20%"></asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

                <asp:Table ID="Table6" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table7" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Report ID
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Company 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Job/Curve Group 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Target
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter" Visible="false">
						Upload Logo 
                                    </asp:TableHeaderCell>



                                </asp:TableRow>


                                <asp:TableRow>


                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtReportID" DisabledStyle-BackColor="#D3D3D3" Enabled="false" Width="160px" />
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" ID="ddlCompany" DropDownWidth="200px"  Width="160px">
                                        </telerik:RadDropDownList>                                        
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="350px" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlCurveGroupID_SelectedIndexChanged" ID="ddlCurveGroupID"  Width="160px">
                                        </telerik:RadDropDownList>                                        
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlTarget" Width="160px" Enabled="false" DropDownHeight="200px" OnSelectedIndexChanged="ddlTarget_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCurve" Enabled="false" OnSelectedIndexChanged="ddlCurve_SelectedIndexChanged"  Width="210px" DropDownHeight="200px" AutoPostBack="true">
                                        </telerik:RadDropDownList>                                        
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadAsyncUpload runat="server" Visible="false" ID="AttachmentUpload" AllowedFileExtensions=".gif,.jpg,.jpeg,.png" MultipleFileSelection="Automatic" Width="210px" PostbackTriggers="btnSaveReport" />
                                    </asp:TableCell>


                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>



                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table2" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Report Name
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Header Comments
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Grouping 
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Boxed Comments
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Include Projection To Bit
                                    </asp:TableHeaderCell>


                                </asp:TableRow>


                                <asp:TableRow>


                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtReportName" Width="160px" />
                                        <asp:RequiredFieldValidator ID="RFReportName" ForeColor="Red" runat="server" ErrorMessage="*" ControlToValidate="txtReportName" ValidationGroup="validateUpload" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtHeaderComments" Width="160px" />
                                       <%-- <asp:RequiredFieldValidator ID="RFHeaderComments" ControlToValidate="TxtHeaderComments" ForeColor="Red" ValidationGroup="validateUpload" runat="server" ErrorMessage="*" Display="Dynamic" />--%>
                                    </asp:TableCell>



                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtGrouping" Width="160px" />
                                        <asp:RequiredFieldValidator ID="RFGrouping" ForeColor="Red" runat="server" ErrorMessage="*" ControlToValidate="txtGrouping" ValidationGroup="validateUpload" Display="Dynamic"></asp:RequiredFieldValidator>
                                     <%--   <asp:RegularExpressionValidator ID="REGrouping" ForeColor="Red" ValidationGroup="validateUpload" ErrorMessage="Value must be numeric." ControlToValidate="txtGrouping" runat="server" ValidationExpression="^-?\d+$" Display="Dynamic" />--%>
                                    </asp:TableCell>



                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="80px" ID="ddlBoxedComments" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                <telerik:DropDownListItem Value="1" Text="Yes" />
                                                <telerik:DropDownListItem Value="2" Text="No" />

                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>




                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="80px" ID="ddlProjectionToBit" Width="210px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                <telerik:DropDownListItem Value="1" Text="Yes" />
                                                <telerik:DropDownListItem Value="2" Text="No" />

                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>




                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>




                <asp:Table ID="Table5" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table8" runat="server" HorizontalAlign="Center">


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Show Proj/TVD
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Extra Header
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Modes
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						EW-NS Reference
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Interpolated Reports
                                    </asp:TableHeaderCell>



                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="80px" ID="ddlProjTVD" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                <telerik:DropDownListItem Value="1" Text="Yes" />
                                                <telerik:DropDownListItem Value="2" Text="No" />

                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="80px" ID="ddlExtraHeader" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                <telerik:DropDownListItem Value="1" Text="Yes" />
                                                <telerik:DropDownListItem Value="2" Text="No" />

                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="80px" ID="ddlModes" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />


                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="80px" ID="ddlReferences" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />


                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>




                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" DropDownHeight="80px" ID="ddlInterpolated" Width="210px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                <telerik:DropDownListItem Value="1" Text="On" />
                                                <telerik:DropDownListItem Value="2" Text="Off" />

                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>




                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table4" runat="server" HorizontalAlign="Center">


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter" Width="865px">
						Available Fields
                                    </asp:TableHeaderCell>

                                </asp:TableRow>
                            </asp:Table>

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


                <asp:Table ID="Table9" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>

                        <asp:TableCell Width="26%"></asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBMeasuredDepth" runat="server" Text="Measured Depth" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBDogLegSeverity" runat="server" Text="DogLeg Severity" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBAzimuth" runat="server" Text="Azimuth" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBInclination" runat="server" Text="Inclination" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBClosureDistance" runat="server" Text="Closure Distance" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBTrueVerticalDepth" runat="server" Text="True Vertical Depth" />
                        </asp:TableCell>
                        <asp:TableCell Width="26%"></asp:TableCell>

                    </asp:TableRow>


                    <asp:TableRow>

                        <asp:TableCell Width="26%"></asp:TableCell>
                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBNSCoordinates" runat="server" Text="N-S Coordinates" />
                        </asp:TableCell>


                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBVerticalSection" runat="server" Text="Vertical Section" />
                        </asp:TableCell>


                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBFELFWL" runat="server" Text="FEL-FWL" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBToolFace" runat="server" Text="Tool Face" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBBuildRate" runat="server" Text="Build Rate" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBEWCoordinates" runat="server" Text="E-W Coordinates" />
                        </asp:TableCell>
                        <asp:TableCell Width="26%"></asp:TableCell>

                    </asp:TableRow>


                    <asp:TableRow>

                        <asp:TableCell Width="26%"></asp:TableCell>
                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBClosureDirection" runat="server" Text="Closure Direction" />
                        </asp:TableCell>


                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBWalkRate" runat="server" Text="Walk Rate" />
                        </asp:TableCell>


                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBComment" runat="server" Text="Comment" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBCourseLength" runat="server" Text="Course Length" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            <asp:CheckBox ID="CBSubSeaDepth" runat="server" Text="SubSea Depth" />
                        </asp:TableCell>

                        <asp:TableCell BackColor="White">
                            
                        </asp:TableCell>
                        <asp:TableCell Width="26%"></asp:TableCell>

                    </asp:TableRow>

                </asp:Table>








                <asp:Table ID="ButtonTable" runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell Width="50%"></asp:TableCell>

                        <asp:TableCell ID="TCSave" runat="server">
                            <asp:Button ID="btnSaveReport" runat="server" Text="Save Report" OnClick="btnSaveReport_Click" Enabled="false" ValidationGroup="validateUpload" CausesValidation="true" />
                        </asp:TableCell>

                        <asp:TableCell ID="TCUpdate" Visible="false" runat="server">
                            <asp:Button ID="btnUpdateReport" runat="server" Text="Update Report" OnClick="btnSaveReport_Click" CausesValidation="true" />
                        </asp:TableCell>



                        <asp:TableCell ID="TCCancel" Visible="false" runat="server">
                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClientClick="Close();" CausesValidation="false" />
                        </asp:TableCell>

                        <asp:TableCell ID="TCClose" Visible="false" runat="server">
                            <asp:Button ID="BtnClose" runat="server" Text="Close" OnClientClick="Close();" CausesValidation="false" />
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
