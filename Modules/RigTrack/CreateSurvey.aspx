<%@ Page Language="C#" Title="Manage Surveys" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateSurvey.aspx.cs" Inherits="Modules_RigTrack_CreateSurvey" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //This is where javascript will go. 
    </script>
    <telerik:RadWindow ID="SurveyWindow" runat="server" Behaviors="Close"></telerik:RadWindow>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position:fixed; text-align:center; height: 100%; width:100%; top:0; right:0; left:0; padding-top:15%; z-index:9999999;">
                <div class="loader2">Loading...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hiddenSurveyID" runat="server" Value="" />

                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Manage Surveys</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                  
                </asp:Table>

                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table3" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        Company:
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        Curve Group:
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        Target: 
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        Curve:
                                    </asp:TableHeaderCell>
                                     <asp:TableHeaderCell CssClass="HeaderCenter">
                                       
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCompany" Width="160px" AppendDataBoundItems="true" DropDownWidth="200px" DropDownHeight="200px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="160px" AppendDataBoundItems="true" DropDownWidth="200px" DropDownHeight="200px" OnSelectedIndexChanged="ddlCurveGroup_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlTarget" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" OnSelectedIndexChanged="ddlTarget_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCurve" Width="160px" AppendDataBoundItems="true" DropDownHeight="200px" OnSelectedIndexChanged="ddlCurve_SelectedIndexChanged" AutoPostBack="true" >
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnReset" Text="Clear All" OnClick="btnReset_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="CurveGroupTable" Height="500"  runat="server" HorizontalAlign="Left" Width="100" BorderStyle="Double">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <h4> Curve Group Info</h4>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Curve Group ID:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCurveGroupID" runat="server" Width="140px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Curve Group Name:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCurveGroupName" runat="server" Width="140px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Job Number:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtJobNumber" runat="server" Width="140px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Method of Calculation:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList Enabled="false" runat="server" ID="ddlMethodOfCalculation" Width="140px" DropDownWidth="180px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                           <asp:TableCell ColumnSpan="2" Width="100px" > 
                               <p><small>FIRST, select YES or NO if you want the current data converted, THEN select the units. ALL data will be Converted</small></p>
                           </asp:TableCell>

                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Convert Units:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList Enabled="false" runat="server" ID="ddlUnitsConvert" Width="140px" DropDownWidth="180px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                    <telerik:DropDownListItem Value="1" Text="Yes" />
                                    <telerik:DropDownListItem Value="2" Text="No" />
                                </Items>
                            </telerik:RadDropDownList>
                           
                        </asp:TableCell>
                     
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Measurement Units:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList Enabled="false" runat="server" ID="ddlMeasurementUnits" Width="140px" DropDownWidth="180px" AppendDataBoundItems="true" DropDownHeight="200px" OnSelectedIndexChanged="ddlMeasurementUnits_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="DogLegSeverityRow" runat="server" Visible="false">
                        <asp:TableCell>Dog Leg Severity:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList Enabled="false" runat="server" ID="ddlDoglegSeverity" Width="140px" DropDownWidth="180px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Input Direction:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList Enabled="false" runat="server" ID="ddlInputDirection" Width="140px" DropDownWidth="180px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Output Direction:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList Enabled="false" runat="server" ID="ddlOutputDirection" Width="140px" DropDownWidth="180px" AppendDataBoundItems="true" DropDownHeight="200px">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell>Vertical Section Ref:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadDropDownList Enabled="false" runat="server" ID="ddlVerticalSection" Width="140px" AppendDataBoundItems="true" DropDownWidth="180px" DropDownHeight="200px" OnSelectedIndexChanged="ddlVerticalSection_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>
                        
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>EW Offset
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEWOffset" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CurveGroupInfo"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox Enabled="false" runat="server" ID="txtEWOffset" Width="140px"></telerik:RadTextBox>
                        </asp:TableCell>
                        
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>NS Offset
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNSOffset" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CurveGroupInfo"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox Enabled="false" runat="server" ID="txtNSOffset" Width="140px"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <%--<asp:TableCell></asp:TableCell>--%>
                        <asp:TableCell>
                            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtEWOffset" ForeColor="Red" ValidationGroup="CurveGroupInfo" 
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                      <%--  <asp:TableCell></asp:TableCell>--%>
                        <asp:TableCell>
                            <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtNSOffset" ForeColor="Red" ValidationGroup="CurveGroupInfo"
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnEditCurveGroup" runat="server" OnClick="btnEditCurveGroup_Click" Text="Edit Calculations" Enabled="false" CausesValidation="false" />
                            <asp:Button ID="btnSaveCurveGroup" runat="server" OnClick="btnSaveCurveGroup_Click" Text="Update" Visible="false" ValidationGroup="CurveGroupInfo" />
                         </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnCancelCurveGroup" runat="server" OnClick="btnCancelCurveGroup_Click" Text="Cancel" Visible="false" Enabled="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                    
                </asp:Table>


                <asp:Table ID="TargetTable" Height="500" runat="server" HorizontalAlign="Left" Width="100" BorderStyle="Double">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <h4>Target Info</h4>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Target ID:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtTargetID" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                     </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Target Name:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtTargetName" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Target Shape:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtTargetShape" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Target TVD:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtTargetTVD" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="CurveTable" Height="500" runat="server" HorizontalAlign="Left" Width="100" BorderStyle="Double">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <h4>Curve Info</h4>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Curve ID:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCurveID" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Curve Name:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCurveName" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Curve Type:</asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtCurveType" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>North Offset:
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNorthOffset" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CurveInfo"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtNorthOffset" runat="server" Width="120px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                       
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CompareValidator ID="CompareValidator3" ControlToValidate="txtNorthOffset" ForeColor="Red" ValidationGroup="CurveInfo" 
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>East Offset:
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEastOffset" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CurveInfo"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtEastOffset" runat="server" Width="120px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                       
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CompareValidator ID="CompareValidator4" ControlToValidate="txtEastOffset" ForeColor="Red" ValidationGroup="CurveInfo" 
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>VS Direction:
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtVSDirection" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CurveInfo"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtVSDirection" runat="server" Width="120px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                      
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CompareValidator ID="CompareValidator5" ControlToValidate="txtVSDirection" ForeColor="Red" ValidationGroup="CurveInfo" 
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>RKB Elevation
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRKBElevation" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="CurveInfo"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtRKBElevation" runat="server" Width="120px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                       
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CompareValidator ID="CompareValidator6" ControlToValidate="txtRKBElevation" ForeColor="Red" ValidationGroup="CurveInfo" 
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnEditCurve" runat="server" OnClick="btnEditCurve_Click" Text="Edit Curve Info" Enabled="false" CausesValidation="false" />
                            <asp:Button ID="btnSaveCurve" runat="server" OnClick="btnSaveCurve_Click" Text="Update Curve Info" Visible="false" ValidationGroup="CurveInfo" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnCancelCurve" runat="server" OnClick="btnCancelCurve_Click" Text="Cancel" Visible="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                   <%-- <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                  --%>
                  
                </asp:Table>

                <asp:Table ID="NextSurveyInformation" Height="500" runat="server" HorizontalAlign="Left" Width="100" BorderStyle="Double">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <h4>Next Survey Information</h4>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label  Text="MD" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalMD" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label  Text="Incl." runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalIncl" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="Azimuth" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalAzimuth" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="TVD" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalTVD" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="N-S Coord" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalNS" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="E-W Coord" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalEW" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="V-Section" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalVS" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="W-Rate" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtWRate" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="B-Rate" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalBRate" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="DLS" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalDLS" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="TFO" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtTFO" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label Text="Closure" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalClosure" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                       
                    </asp:TableRow>
                    <asp:TableRow>
                         <asp:TableCell><asp:Label ID="Label1" Text="@" runat="server"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtAdditionalAT" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                        </asp:TableCell>
                       
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnNormal" runat="server" Text="Normal" />
                        </asp:TableCell>
                    </asp:TableRow>
                    
                </asp:Table>

                <asp:Table ID="LocationLeastDistance" Height="500" runat="server" HorizontalAlign="Left" Width="100" BorderStyle="Double">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <h4>Location</h4>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Sensor"></asp:Label>

                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:RadioButton ID="rbSensor" GroupName="Location" runat="server" Enabled="false" />
                        </asp:TableCell>
                        <asp:TableCell ><asp:Label runat="server" Text="BHL"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:RadioButton ID="rbBHL" GroupName="Location" runat="server" Enabled="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Bit to Sensor"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtBitToSensor" Width="100px" runat="server" Enabled ="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                         <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                             <h4>Least Distance</h4>
                         </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="ON"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:RadioButton ID="rbOn" GroupName="OnOff"  runat="server" Enabled="false" />
                        </asp:TableCell>
                        <asp:TableCell><asp:Label runat="server" Text="OFF"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:RadioButton ID="rbOff" GroupName="OnOff" runat="server" Enabled="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Least Dist"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtLeastDist" runat="server" Width="100px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="@ H. Side"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtHSide" runat="server" Width="100px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="TVD Comp"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtTVDComp" runat="server" Width="100px" Enabled ="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadTextBox ID="txtComparisonCurve" runat="server" Width="60px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label Text="Comparison Curve" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <telerik:RadTextBox ID="txtLeastDistanceBottom" runat="server" Width="100px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnEditLocation" Text="Edit Location" OnClick="btnEditLocation_Click" />
                            <asp:Button runat="server" ID="btnSaveLocation" Text="Save Location" OnClick="btnSaveLocation_Click" Visible="false" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnCancelLocation" Text="Cancel" OnClick="btnCancelLocation_Click" Visible="false" />
                        </asp:TableCell>
                    </asp:TableRow>

                    
                    
                </asp:Table>

                

                <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <h4>New Survey Entry</h4>
                        </asp:TableCell>
                        
                    </asp:TableRow>

               
                    <asp:TableRow>
                     
                        <asp:TableHeaderCell CssClass="HeaderCenter" >
                            <font style="color:black !important; font-size:smaller"> Survey Name:</font>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                           <font style="color:black !important; font-size:smaller"> Measurement Depth:</font>   
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtMeasurementDepth" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="SurveyGroup"></asp:RequiredFieldValidator>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                          <font style="color:black !important; font-size:smaller"> Inclination:</font>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtInclination" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="SurveyGroup"></asp:RequiredFieldValidator>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                           <font style="color:black !important; font-size:smaller"> Azimuth:</font>  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtAzimuth" Display="Static"
                                ErrorMessage="*" ForeColor="Red" runat="server" ValidationGroup="SurveyGroup"></asp:RequiredFieldValidator>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter">
                             <font style="color:black !important; font-size:smaller"> Survey Comments:</font>  
                        </asp:TableHeaderCell>
                                                                
                    </asp:TableRow>
                  
                                             
                                              
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtName" Width="160px" Enabled="false"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>

                            <telerik:RadTextBox runat="server" ID="txtMeasurementDepth" Width="160px" Text="0.00" Enabled="false" autocomplete="off" ></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtInclination" Width="160px" Text="0.00" Enabled="false" autocomplete="off"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtAzimuth" Width="160px" Text="0.00" Enabled="false" autocomplete="off"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ID="txtSurveyComments" Width="160px" Enabled="false" autocomplete="off"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnAddSurvey" runat="server" OnClick="btnAddSurvey_Click" Text="Add New Survey" Enabled="false" ValidationGroup="SurveyGroup" />
                            <asp:Button ID="btnEditSurvey" runat="server" OnClick="btnEditSurvey_Click" Text="Update Survey" Visible="false" CausesValidation="false" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnDeleteSurvey" runat="server" OnClick="btnDeleteSurvey_Click" Text="Remove Survey" Visible="false" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnAddLastTargetSurvey" runat="server" OnClick="btnAddLastTargetSurvey_Click" Text="Get Targets Last Survey" Visible="false" Enabled="true" />
                        </asp:TableCell>
                        <asp:TableCell ID="TieInRow3" runat="server" Visible="false">
                            <p><small>When entering Tie in data, Offsets will be set after entering information</small></p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                             <asp:Label ID="MeasurementDepthError" ForeColor="Red" runat="server" Visible="false" Text="Depth cannot be smaller than previous"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="doubleValidator1" ControlToValidate="txtMeasurementDepth" ForeColor="Red" ValidationGroup="SurveyGroup"
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="doubleValidator2" ControlToValidate="txtInclination" ForeColor="Red" ValidationGroup="SurveyGroup"
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="doubleValidator3" ControlToValidate="txtAzimuth" ForeColor="Red" ValidationGroup="SurveyGroup"
                                 Type="Double" Display="Static" Operator="DataTypeCheck" ErrorMessage="Not a valid Number" runat="server"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TieInRow1" runat="server" Visible="false">
                        <asp:TableHeaderCell></asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter" Width="100px" HorizontalAlign="Left">
                            TVD:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter" Width="100px" HorizontalAlign="Left">
                            NS:
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="HeaderCenter" Width="100px" HorizontalAlign="Left">
                            EW:
                        </asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TieInRow2" Visible="false" runat="server">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <telerik:RadTextBox runat="server" ID="txtTieInTVD" Width="100px" Text="0.00"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <telerik:RadTextBox runat="server" ID="txtTieInNS" Width="100px" Text="0.00"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <telerik:RadTextBox runat="server" ID="txtTieInEW" Width="100px" Text="0.00"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnSubseas" runat="server" OnClick="btnSubseas_Click" Text="Subseas" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblSurveyCount" runat="server" Text="Survey Count: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblWellPlanError" runat="server" Text="Cannot add Survey to Well Plan" Visible="false" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                        
                    </asp:TableRow>
                </asp:Table>

                <telerik:RadGrid ID="RadGridSurveys" AllowFilteringByColumn="false" PageSize="900" AllowSorting="false" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" 
                     OnNeedDataSource="RadGridSurveys_NeedDataSource" >
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No Data to be displayed" DataKeyNames="ID, RowNumber">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="checkboxTemplate" HeaderText="Select/Edit" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkbx" runat="server" OnCheckedChanged="chkbx_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridBoundColumn HeaderText="Survey Name" DataField="Name" UniqueName="Name" 
                                 AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn HeaderText="MD" DataField="MD" UniqueName="MD" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Inclination" DataField="Inclination" UniqueName="Inclination" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Azimuth" DataField="Azimuth" UniqueName="Azimuth" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="TVD" DataField="TVD" UniqueName="TVD" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Red" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Subseas-TVD" DataField="SubseasTVD" UniqueName="SubseasTVD" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="NS" DataField="NS" UniqueName="NS" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="EW" DataField="EW" UniqueName="EW" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Vertical Section" DataField="VerticalSection" UniqueName="VerticalSection" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="CL" DataField="CL" UniqueName="CL" SortExpression="CL"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Closure Distance" DataField="ClosureDistance" UniqueName="ClosureDistance" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Closure Direction" DataField="ClosureDirection" UniqueName="ClosureDirection" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="DLS" DataField="DLS" UniqueName="DLS" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText ="DLA" DataField="DLA" UniqueName="DLA" 
                                 AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="BR" DataField="BR" UniqueName="BR" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="WR" DataField="WR" UniqueName="WR" 
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="TFO" DataField="TFO" UniqueName="TFO"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-ForeColor="Blue">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Comments" DataField="SurveyComment" UniqueName="SurveyComment" 
                                 AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:Table ID="Bottom" runat="server" HorizontalAlign="Right" Width="100">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" ID="lblExportError" ForeColor="Red" Text="No data to export!" Visible="true"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            
                            <Telerik:RadButton ID="btnExportToText" Skin="Black" runat="server" Text="Export To ASCII" OnClick="btnExportToText_Click" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnCloseCurve" runat="server" Text="Close Curve" OnClick="btnCloseCurve_Click"  />
                        </asp:TableCell>
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