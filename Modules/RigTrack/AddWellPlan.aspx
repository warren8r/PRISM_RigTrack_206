<%@ Page Title="Add Well Plan" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="AddWellPlan.aspx.cs" Inherits="Modules_RigTrack_AddWellPlan" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; text-align:center; height:100%; width:100%; top: 0; right:0; left:0; padding-top:15%; z-index: 9999999;">
                <div class="loader2">Loading...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
         <script type="text/javascript">
             var $ = $telerik.$;
             function Close() {
                 //var choice = confirm("Are you sure you want to cancel Well Plan upload? This will delete job info");
                 //if (choice) {
                 //    GetRadWindow().close();
                 //}
                 //else {
                 //    return false;
                 //}
                 GetRadWindow().close();
             }
             function GetRadWindow() {
                 var oWindow = null;
                 if (window.radWindow) oWindow = window.radWindow;
                 else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

                 return oWindow;
             }
             $(window).resize(function () {
                 GetRadWindow().autoSize(true);
             });

             function pageLoad() {
                 GetRadWindow().autoSize(true);
                 
                 //var width = $(window).width();
                 //var height = $(window).height();
                 //GetRadWindow().setSize(Math.ceil(width - 90), Math.ceil(height - 90));
             }

             function PlanLoaded() {
                 //alert("true");
                 GetRadWindow().argument = "true";
             }
            
             
        </script>

   


    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddWellPlan" />
            </Triggers>
            <ContentTemplate>

<%--                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" style="z-index:9999999;">
                    <Windows>
                        <telerik:RadWindow ID="WellPlanWindow" runat="server" Behaviors="Close"></telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>--%>


                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Add Well Plan</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


                <asp:Table ID="table5" runat="server" HorizontalAlign="Left" Width="25%" Caption="Job/Curve Group Information" BorderStyle="Double">
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Company:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCompany" runat="server" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Curve Group Name:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCurveGroupName" runat="server" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Lease/Well:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtLeaseWell" runat="server" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Location:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtLocation" runat="server" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Rig Name:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtRigName" runat="server" Enabled ="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Job Number:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtJobNumber" runat="server" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


                <asp:Table ID="table1" runat="server" HorizontalAlign="Left" Width="25%" Caption="Curve Infomation" BorderStyle="Double">
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Curve Name:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCurveName" runat="server"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">Curve Type:</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList ID="ddlCurveType" runat="server" AppendDataBoundItems="true" >
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">NS-Offset</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtNSOffset" runat="server"></telerik:RadTextBox>
                            <asp:CompareValidator ID="Validator1"  ControlToValidate="txtNSOffset" Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">EW-Offset</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtEWOffset" runat="server"></telerik:RadTextBox>
                            <asp:CompareValidator ID="CompareValidator1"  ControlToValidate="txtEWOffset" Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">VS Direction</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtVSDirection" runat="server"></telerik:RadTextBox>
                            <asp:CompareValidator ID="CompareValidator2"  ControlToValidate="txtVSDirection" Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>
                            <font style="color:#597791 !important;">RKB Elevation</font>
                        </asp:TableHeaderCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtSubseasOffset" runat="server"></telerik:RadTextBox>
                            <asp:CompareValidator ID="CompareValidator3"  ControlToValidate="txtSubseasOffset" Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="ShowImport" runat="server" HorizontalAlign="Left" Width="25%" Caption="Imported Surveys">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ReadOnly="true" TextMode="MultiLine" runat="server" ID="txtSurveysWindow" Width="440px" Height ="178px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="table4" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:FileUpload ID="FileUpload1" runat="server"  />
                             <asp:Label ID="lblFailedtoUpload" runat="server" Text="No File Uploaded" ForeColor="Red" Visible="false"></asp:Label>
                        </asp:TableCell>
                           <asp:TableCell>
                            <asp:Button ID="btnImport" runat="server" Text="Import To Curve" OnClick="btnImport_Click" OnClientClick="PlanLoaded(); " />
                        </asp:TableCell>
                        
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnAddWellPlan" runat="server" Text="Upload Well Plan" OnClick="btnAddWellPlan_Click"  />
                        </asp:TableCell>
                        <asp:TableCell>
                            
                            <asp:Label ID="lblImportSuccessful" runat="server" Text="Well Plan Curve Created! Close Window to View Curve" ForeColor="Green" Visible="false"></asp:Label>
                        
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        
                      
                    </asp:TableRow>
                </asp:Table>




                <div style="display: block; float: right;">
                   <asp:Button ID="btnClose" runat="server" CssClass="button-CloseRed" Width="60px" ForeColor="Black" Text="Close" OnClientClick="Close(); return false;" Visible="false"/>
                    
                </div>
            </ContentTemplate>
            
        </asp:UpdatePanel>



    </fieldset>
    <div style="text-align: center;" class="DivFooter">
        <hr style="width: 777px" />
        Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
    </div>
</asp:Content>