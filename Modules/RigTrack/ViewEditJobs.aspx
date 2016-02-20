<%@ Page Title ="View/Edit Jobs" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewEditJobs.aspx.cs" Inherits="Modules_RigTrack_ViewEditJobs" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        var $ = $telerik.$;
        var graphWindow;

        function pageLoad() {
            graphWindow = $find("<%=GraphWindow.ClientID %>");
        }

        function OpenGraphWindow() {
            <%--var curveGroupID = $find("<%= ddlCurveGroup.ClientID %>");--%>
            var curveGroupID = $('#<%= hiddenField.ClientID %>').val();
            //curveGroupID = curveGroupID.get_selectedItem().get_value();
            
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
            //event.preventDefault();
            graphWindow.show();
            //setGraphWindowSize(curveGroupID);
            setGraphWindowSize(curveGroupID);
        }

        $(window).resize(function () {
            if (graphWindow.isVisible()) {
                SetWindowSize();
            }
        });

        function SetWindowSize(){
            var viewportWidth = $(window).width();
            var viewportHeight = $(window).height();
            graphWindow.setSize(Math.ceil(viewportWidth -  90), Math.ceil(viewportHeight - 90));
        }
        function setGraphWindowSize(curveGroupID) {
            SetWindowSize();
            //graphWindow.setUrl("CreateGraph.aspx?CurveGroupID=" + CurveGroupID);
            graphWindow.setUrl("PlotGraph.aspx?CurveGroupID=" + curveGroupID + "&Modal=True");
            graphWindow.center();
            graphWindow.set_modal(true);
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
                <input type="hidden" id="hiddenField" runat="server"/>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" style="z-index:99999999;">
                    <Windows>
                <telerik:RadWindow ID="GraphWindow" runat="server" Behaviors="Close"></telerik:RadWindow>
                        <telerik:RadWindow ID="CurveSelectionWindow" runat="server" Width="725" Height="500" Behaviors="Close"></telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
                
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>View/Edit Jobs/Curve Groups</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                   

                </asp:Table>

                <asp:Table ID="Table11" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table12" runat="server" HorizontalAlign="Center"  BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>



                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                          <font style="color:#f5c739 !important;">Curve Group Name:</font>
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                          <font style="color:#f5c739 !important;">Job Start Date Begin:</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        <font style="color:#f5c739 !important;">Job Start Date End:</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                            <font style="color:#f5c739 !important;">Company:</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        <font style="color:#f5c739 !important;">Lease/Well:</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                         <font style="color:#f5c739 !important;">Location:</font>
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                          <font style="color:#f5c739 !important;">Rig Name:</font>
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        <font style="color:#f5c739 !important;">Job Number:</font>
                                    </asp:TableHeaderCell>


                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtCurveGroupName" Width="150px" />
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="datepicker_JobStartDate" runat="server" Width="150px" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>

                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="datepicker_JobStartEnd" runat="server" Width="150px" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>

                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" Width="150px" DropDownWidth="200px">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLeaseWell" Width="150px" />
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtLocation" Width="150px" />
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtRigName" Width="150px" />
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtJobNumber" Width="150px" />
                                    </asp:TableCell>



                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

                



                <asp:Table ID="ButtonTable" runat="server" Width="100%">
                    <asp:TableRow>

                        <asp:TableCell Width="49%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnSearch" CssClass="button-SearchMain" ForeColor="Black" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                        </asp:TableCell>

                     

                        <asp:TableCell>
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <telerik:RadButton ID="btnView" runat="server" Text="View Plot" OnClientClicked="OpenGraphWindow" AutoPostBack="false" />
                        </asp:TableCell>

                        <asp:TableCell Width="50%"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell ID="MethodOfCalMessage" runat="server" Visible="false" ColumnSpan="4">
                            <p style="color:blue"><small >* To Change Method of Calculation Information, Please Edit in 'Manage Surveys' to prevent data loss.</small></p>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>



                <asp:Table ID="GridTable1" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>

                        <asp:TableCell Width="50%"></asp:TableCell>

                        <asp:TableCell>

                            <telerik:RadGrid ID="RadGrid1" AllowFilteringByColumn="false" AllowPaging="true"   AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" 
                                OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"  OnItemDataBound="RadGrid1_ItemDataBound" Width="1230px" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" OnPageSizeChanged="RadGrid1_PageSizeChanged" PageSize="20">
                                
                                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                    
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="true"  UseStaticHeaders="true"  />
                                    <%--<ClientEvents OnRowClick="RowClick" />--%>
                                </ClientSettings>
                                
                                <EditItemStyle CssClass="EditedItem" Height="25px" />
                                <MasterTableView EditMode="InPlace"  AutoGenerateColumns="false" GridLines="Horizontal" DataKeyNames="JobEndDate" NoMasterRecordsText="No data has been added.">
                                    <Columns>
                                        <telerik:GridEditCommandColumn UniqueName="EditButton" ButtonType="LinkButton" HeaderStyle-Width="100px" HeaderText="Edit / Select"></telerik:GridEditCommandColumn>


                                        <telerik:GridBoundColumn HeaderText="Curve Group ID" HeaderStyle-Width="120px"  DataField="ID" SortExpression="ID"
                                            UniqueName="ID" Visible="false">

                                        </telerik:GridBoundColumn>

                                         <telerik:GridTemplateColumn UniqueName="ID2" HeaderStyle-Width="120px"  HeaderText="Curve Group ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurveGroupID2"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <telerik:RadTextBox runat="server" Enabled="false" ID="CurveGroupID2" Width="90px" />
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>




                                        <telerik:GridBoundColumn HeaderText="Curve Group Name" HeaderStyle-Width="170px"  DataField="CurveGroupName" SortExpression="CurveGroupName"
                                            UniqueName="CurveGroupName">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Job Start Date" DataField="JobStartDate" UniqueName="JobStartDate"
                                            SortExpression="JobStartDate" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="JobStartDate2" HeaderStyle-Width="100px"  HeaderText="Job Start Date" SortExpression="JobStartDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "JobStartDate")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDatePicker ID="datepicker_JobStartDateEdit" Width="92px"   runat="server" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>


                                        <telerik:GridBoundColumn HeaderText="Job End Date" DataField="JobEndDate" UniqueName="JobEndDate"
                                            SortExpression="JobEndDate" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="JobEndDate2" HeaderStyle-Width="100px"  HeaderText="Job End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "JobEndDate")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDatePicker ID="datepicker_JobEndDateEdit" Enabled="false" Width="92px"  runat="server" SkipMinMaxDateValidationOnServer="true"></telerik:RadDatePicker>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>



                                        <telerik:GridboundColumn HeaderText="Company" DataField="CompanyName" UniqueName="CompanyName" Visible="false"></telerik:GridboundColumn>
                                        <telerik:GridTemplateColumn UniqueName="Company" HeaderStyle-Width="155px" HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlCompanyGrid" DropDownHeight="270px" DropDownWidth="150px" Width="135px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Lease/Well" HeaderStyle-Width="180px"  DataField="LeaseWell" UniqueName="LeaseWell"
                                            SortExpression="LeaseWell">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Location" HeaderStyle-Width="180px"  DataField="JobLocation" UniqueName="JobLocation"
                                            SortExpression="JobLocation">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Rig Name" HeaderStyle-Width="180px"  DataField="RigName" UniqueName="RigName"
                                            SortExpression="RigName">
                                        </telerik:GridBoundColumn>




                                        <telerik:GridBoundColumn HeaderText="Job Number" HeaderStyle-Width="180px"  DataField="JobNumber" UniqueName="JobNumber"
                                            SortExpression="JobNumber">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="GL Or MSL" DataField="GLName" UniqueName="GLName"
                                            SortExpression="GLName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="GLorMSL" HeaderStyle-Width="80px"  HeaderText="GL or MSL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGLorMSL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlGLorMSL" Width="60px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Grid" HeaderStyle-Width="180px"  DataField="Grid" UniqueName="Grid"
                                            SortExpression="Grid">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="RKB" HeaderStyle-Width="180px"  DataField="RKB" UniqueName="RKB"
                                            SortExpression="RKB">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Country" DataField="CountryName" UniqueName="CountryName"
                                            SortExpression="CountryName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="Country" HeaderStyle-Width="180px"  HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CountryName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlCountry" DropDownHeight="270px" DropDownWidth="200px" Width="170px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="State" DataField="StateName" UniqueName="StateName"
                                            SortExpression="StateName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="State" HeaderStyle-Width="155px"  HeaderText="State">
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StateName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlState"  DropDownHeight="270px" DropDownWidth="150px" Width="135px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Latitude, Longitude" HeaderStyle-Width="140px" DataField="primaryLatLong" UniqueName="primaryLatLong" SortExpression="primaryLatLong"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Declination" HeaderStyle-Width="80px"  DataField="Declination" UniqueName="Declination"
                                            SortExpression="Declination">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Method Of Calculation" DataField="MethodName" SortExpression="MethodName"
                                            UniqueName="MethodName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="MethodOfCalculation" HeaderStyle-BackColor="Blue" HeaderStyle-Width="160px"  HeaderText="Method Of Calculation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMethodOfCalculation" ForeColor="Blue"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MethodName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlMethodOfCalculation" Width="145px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>



                                        <telerik:GridBoundColumn HeaderText="Meters/Feet" DataField="MetersFeet" UniqueName="MetersFeet"
                                            SortExpression="MetersFeet" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="MetersFeet" HeaderStyle-Width="82px"  HeaderText="Meters/Feet">
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
                                                <telerik:RadDropDownList ID="ddlDogLeg" Width="100px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>



                                        

                                        <telerik:GridBoundColumn HeaderText="Output Direction" DataField="OutPutName" UniqueName="OutPutName"
                                            SortExpression="OutPutName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="OutPutName" HeaderStyle-Width="140px"  HeaderText="Output Direction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOutputName" runat="server" ForeColor="Blue" Text='<%# DataBinder.Eval(Container.DataItem, "OutPutName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlOutputName" Width="130px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Input Direction" DataField="InputName" UniqueName="InputName"
                                            SortExpression="InputName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="InputName" HeaderStyle-Width="140px"  HeaderText="Input Direction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInputName" runat="server" ForeColor="Blue"  Text='<%# DataBinder.Eval(Container.DataItem, "InputName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlInputName" Width="130px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="Vertical Reference" DataField="VSectionName" UniqueName="VSectionName"
                                            SortExpression="VSectionName" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="VSectionName" HeaderStyle-Width="135px"  HeaderText="Vertical Reference">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVerticalSection" runat="server" ForeColor="Blue"  Text='<%# DataBinder.Eval(Container.DataItem, "VSectionName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadDropDownList ID="ddlVerticalSection" Width="125px" runat="server"></telerik:RadDropDownList>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn HeaderText="NS Offset" ItemStyle-ForeColor="Blue"  HeaderStyle-Width="80px"  DataField="NSOffset" UniqueName="NSOffset"
                                            SortExpression="NSOffset">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="EW Offset" ItemStyle-ForeColor="Blue"  HeaderStyle-Width="80px"  DataField="EWOffset" UniqueName="EWOffset"
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RadGrid1" />
            </Triggers>
        </asp:UpdatePanel>

    </fieldset>
    <div style="text-align: center;" class="DivFooter">
        <hr style="width: 777px" />
        Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
    </div>
</asp:Content>
