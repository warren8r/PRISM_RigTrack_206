<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="JobAssignments.aspx.cs" Inherits="Modules_Configuration_Manager_JobAssignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function openwin() {


            window.radopen(null, "window_service");

        }


        function assetcheck() {


            document.getElementById('<%=chk_kit.ClientID %>').checked = false;
            var combo = $find("<%= radcombo_Assetkit.ClientID %>");
            //the combo return null
            combo.set_enabled(false);
            var combo = $find("<%= combo_assetcategory.ClientID %>");
            //the combo return null
            combo.set_enabled(true);
            var combo = $find("<%= combo_assets.ClientID %>");
            //the combo return null
            combo.set_enabled(true);

        }

        function kitcheck() {
            document.getElementById('<%=chk_asset.ClientID %>').checked = false;
        var combo = $find("<%= radcombo_Assetkit.ClientID %>");
        //the combo return null
        combo.set_enabled(true);
        var combo = $find("<%= combo_assetcategory.ClientID %>");
        //the combo return null
        combo.set_enabled(false);
        var combo = $find("<%= combo_assets.ClientID %>");
        //the combo return null
        combo.set_enabled(false);
    }


    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function OnClientShow(sender, args) {
                var btn = sender.getManualCloseButton();
                btn.style.left = "0px";
            }
            function validAssign() {
                var comboBox1 = $find('<%=combo_job.ClientID %>');
                var catvalue = comboBox1.get_value();
                if (catvalue == "0") {
                    radalert('Please select job', 330, 180, 'Client RadAlert');

                    return false;
                }

            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadNotification ID="radnotMessage" runat="server" Text="Initial text" Position="BottomRight"
        AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
        EnableRoundedCorners="true">
    </telerik:RadNotification>

    <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Always">

            <ContentTemplate>
                <asp:Label ID="lbl_message" runat="server" Visible="false" />
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Assign Tools to Job</h2>
                        </asp:TableCell>
                    </asp:TableRow>


                </asp:Table>


                  <asp:Table ID="Table4" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table5" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>


                                    <asp:TableHeaderCell CssClass="HeaderCenter" Width="440px">
						  Search
                                    </asp:TableHeaderCell>

                               
                          

                                

                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>


                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table3" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						   Select Company
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Select Job
                                        
                                    </asp:TableHeaderCell>


                                </asp:TableRow>


                                <asp:TableRow>


                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlCompanysearch" Width="220px" runat="server" OnSelectedIndexChanged="ddlCompanysearch_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="Select All" />
                                            </Items>
                                        </telerik:RadDropDownList>

                                    </asp:TableCell>

                                    <asp:TableCell>

                                        <telerik:RadComboBox runat="server" ID="comboJobSearch" Width="220px" OnSelectedIndexChanged="combo_job_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" DropDownHeight="220px">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="Select All" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </asp:TableCell>

                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>


                <asp:Table ID="Table11" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table12" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						   Select Company
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						 Select Job
                                        
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter" Visible="false">
						
                                        
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						 Select Tool Category
                                        
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        Select Tool(s)  
                                        <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                        <telerik:RadToolTip ID="RadToolTip3" runat="server" Position="MiddleRight" RelativeTo="Element"
                                            TargetControlID="ImageButton3" Width="200px" HideEvent="ManualClose"
                                            OnClientShow="OnClientShow">
                                            Shows list of Tools not assigned to any Job
                                        </telerik:RadToolTip>

                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell CssClass="HeaderCenter" Visible="false">
						
                                        
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        Select Tool Group  
                                        <asp:ImageButton ID="ImageButton4" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                        <telerik:RadToolTip ID="RadToolTip4" runat="server" Position="MiddleRight" RelativeTo="Element"
                                            TargetControlID="ImageButton4" Width="200px" HideEvent="ManualClose"
                                            OnClientShow="OnClientShow">
                                            Tool group is a Template,Shows list of Groups.
                                        </telerik:RadToolTip>

                                    </asp:TableHeaderCell>

                                    
                               

                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlCompany" runat="server" Width="220px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="Select All" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadComboBox runat="server" ID="combo_job" Width="220px" AppendDataBoundItems="true" DropDownHeight="220px">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="Select All" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </asp:TableCell>

                                    <asp:TableCell Visible="false">
                                        <asp:CheckBox ID="chk_asset" OnClick="javascript:return assetcheck();" runat="server" />
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadComboBox runat="server" ID="combo_assetcategory" AutoPostBack="true" CheckBoxes="true" DataSourceID="SqlGetAssetcategory" Width="220px"
                                            EmptyMessage="Please Select Tool Category(s)"
                                            DataTextField="clientAssetName" DataValueField="clientAssetID" EnableCheckAllItemsCheckBox="true" OnSelectedIndexChanged="combo_assetcategory_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetAssetcategory" ConnectionString="<%$ databaseExpression:client_database %>" runat="server"
                                            SelectCommand="select clientAssetName ,clientAssetID from clientAssets where active='True' order by clientAssetID"></asp:SqlDataSource>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadComboBox runat="server" ID="combo_assets" CheckBoxes="true" Width="220px" EmptyMessage="Please Select Tool(s)" DataTextField="AssetName" DataValueField="Id" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlGetAssets" ConnectionString="<%$ databaseExpression:client_database %>" runat="server"
                                            SelectCommand="select  P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA where  P.AssetName=PA.ID and  P.id not in (select AssetId from PrismJobAssignedAssets where AssignmentStatus='Active')  and P.repairstatus<>'Maintenance'  order by PA.AssetName DESC"></asp:SqlDataSource>
                                    </asp:TableCell>


                                    <asp:TableCell Visible="false">
                                        <asp:CheckBox ID="chk_kit" OnClick="javascript:return kitcheck();" runat="server" />
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadComboBox runat="server" ID="radcombo_Assetkit" DataSourceID="SqlDataSource1" CheckBoxes="true" Width="220px" EmptyMessage="Please Select Tool Group" DataTextField="kitname" DataValueField="assetkitid" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ databaseExpression:client_database %>" runat="server"
                                            SelectCommand="select * from PrismAssetKitDetails"></asp:SqlDataSource>
                                    </asp:TableCell>

                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>

                    </asp:TableRow>

                </asp:Table>


                <asp:Table ID="Table2" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>

                        <asp:TableCell Width="50%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btn_save" runat="server" Text="Assign"
                                OnClick="btn_save_Click" OnClientClick="return validAssign();" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_Click"></asp:Button>
                        </asp:TableCell>

                        <asp:TableCell Width="50%"></asp:TableCell>

                    </asp:TableRow>

                </asp:Table>

                


                <asp:Table ID="RadGridTable" runat="server" Width="100%" HorizontalAlign="Center">

                    <asp:TableRow>

                        <asp:TableCell HorizontalAlign="Center">


                            <b>Existing Job Assignments</b><br />
                            <telerik:RadGrid ID="grdJobList" runat="server" CellSpacing="0"
                                AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" OnSortCommand="grdJobList_SortCommand"
                                OnPageIndexChanged="grdJobList_PageIndexChanged" GridLines="None" OnItemCommand="grdJobList_ItemCommand">
                                <%--OnItemDataBound="grdJobList_ItemDataBound"--%>
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>

                                <MasterTableView DataKeyNames="ID" CommandItemStyle-HorizontalAlign="Left" CommandItemDisplay="Top" EditMode="InPlace">

                                    <ItemStyle VerticalAlign="Top" />
                                    <AlternatingItemStyle VerticalAlign="Top" />
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                        ShowExportToCsvButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" FilterControlAltText="Filter jid column"
                                            ReadOnly="True" SortExpression="ID" UniqueName="IntervalMeterDataId" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Job/Curve Group Name" FilterControlAltText="Filter JOB column"
                                            SortExpression="JOB" UniqueName="JOB">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_jobname" runat="server" Text='<%# Eval("JOB") %>' />
                                                <asp:Label ID="lbl_jobid" runat="server" Text='<%# Eval("ID") %>' Visible="false" />
                                                <asp:Label ID="lbl_message" runat="server" Text='<%# Eval("JOB") %>' Visible="false" />
                                            </ItemTemplate>

                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Job Number" FilterControlAltText="Filter jobid column"
                                            SortExpression="JobNumber" UniqueName="JobNumber">
                                            <ItemTemplate>

                                                <asp:Label ID="lbl_jobnumber" runat="server" Text='<%# Eval("JobNumber") %>' />
                                            </ItemTemplate>

                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Job Location" FilterControlAltText="Filter jobtype column"
                                            SortExpression="JobLocation" UniqueName="JobLocation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_joblocation" runat="server" Text='<%# Eval("JobLocation") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Lease/Well" FilterControlAltText="Filter RigName column"
                                            SortExpression="LeaseWell" UniqueName="LeaseWell">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_LeaseWell" runat="server" Text='<%# Eval("LeaseWell") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="RigName" FilterControlAltText="Filter RigName column"
                                            SortExpression="RigName" UniqueName="RigName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_RigName" runat="server" Text='<%# Eval("RigName") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%-- <telerik:GridTemplateColumn   HeaderText="Operation Manager"   FilterControlAltText="Filter operationsManager column"
                            SortExpression="operationsManager" UniqueName="operationsManager">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_operationsManager" runat="server" Text='<%# Eval("operationsManager") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn> 
                             <telerik:GridTemplateColumn   HeaderText="Project Manager"   FilterControlAltText="Filter projectManager column"
                            SortExpression="projectManager" UniqueName="projectManager">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_projectManager" runat="server" Text='<%# Eval("projectManager") %>'/>
                                </ItemTemplate>                               
                            </telerik:GridTemplateColumn>  --%>
                                        <telerik:GridTemplateColumn HeaderText="Job Start Date" FilterControlAltText="Filter startdate column"
                                            SortExpression="JobStartDate" UniqueName="JobStartDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_startdate" runat="server" Text='<%#DateTime.Parse(Eval("JobStartDate").ToString()).ToString("MM/dd/yyyy")%>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>



                                    </Columns>
                                    <NestedViewTemplate>
                                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black" SelectedIndex="0"
                                            Height="100%" MultiPageID="RadMultiPage1">
                                            <Tabs>
                                                <telerik:RadTab runat="server" PageViewID="t1" Text="Tools" Selected="True">
                                                </telerik:RadTab>
                                                <%-- <telerik:RadTab runat="server" PageViewID="t2" Text="Personnel" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t3" Text="Services" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="t4" Text="Consumables" >
                            </telerik:RadTab>--%>
                                                <telerik:RadTab runat="server" PageViewID="t5" Text="Tool Group">
                                                </telerik:RadTab>
                                            </Tabs>
                                        </telerik:RadTabStrip>

                                        <telerik:RadMultiPage ID="RadMultiPage1" RenderSelectedPageOnly="false"
                                            runat="server" SelectedIndex="0" CssClass="multiPage">
                                            <telerik:RadPageView ID="t1" runat="server">
                                                <telerik:RadGrid ID="gridJobAssets" runat="server" CellSpacing="0"
                                                    GridLines="None" AutoGenerateColumns="False" OnItemDataBound="gridJobAssets_ItemDataBound" AllowSorting="True" AllowPaging="True" PageSize="10">
                                                    <MasterTableView Width="100%" EditMode="InPlace">
                                                        <Columns>

                                                            <telerik:GridBoundColumn DataField="AssetId" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                                                                ReadOnly="True" SortExpression="AssetId" UniqueName="AssetId" Display="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn HeaderText="ToolName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Serial&#160;Number" FilterControlAltText="Filter SerialNumber column"
                                                                SortExpression="SerialNumber" UniqueName="SerialNumber">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Warehouse" FilterControlAltText="Filter Warehouse column"
                                                                SortExpression="Warehouse" UniqueName="Warehouse">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Warehouse" runat="server" Text='<%# Eval("Warehouse") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Tool&#160;Category" FilterControlAltText="Filter clientAssetName column"
                                                                SortExpression="clientAssetName" UniqueName="clientAssetName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_clientAssetName" runat="server" Text='<%# Eval("clientAssetName") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Tool Group&#160;Name" Visible="false" FilterControlAltText="Filter clientAssetName column"
                                                                UniqueName="KitName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_KitName" runat="server" Text='<%#Eval("KitName") %>' />
                                                                </ItemTemplate>

                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Tool&#160;Status" FilterControlAltText="Filter AssetStatus column"
                                                                SortExpression="AssetStatus" UniqueName="AssetStatus">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AssetStatus" runat="server" Text='<%#Eval("AssetStatus") %>' />
                                                                </ItemTemplate>

                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Modified&#160;BY" FilterControlAltText="Filter ModifiedBY column"
                                                                SortExpression="username" UniqueName="username">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("username") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Modified&#160;Date" FilterControlAltText="Filter ModifiedDate column"
                                                                SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ModifiedDate" runat="server" Text='<%#DateTime.Parse(Eval("ModifiedDate").ToString()).ToString("MM/dd/yyyy")%>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </telerik:RadPageView>




                                            <%-- <telerik:RadPageView ID="t2" runat="server">
                             <telerik:RadGrid ID="gridJobPersonals" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
                                          <MasterTableView    EditMode="InPlace" >                   
                                             <Columns>
                                     
                        <telerik:GridBoundColumn DataField="Id" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                            ReadOnly="True" SortExpression="Id" UniqueName="Id" Display="False">
                        </telerik:GridBoundColumn>  
                              
                        <telerik:GridTemplateColumn HeaderText="User&#160;Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Username" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                        <telerik:GridTemplateColumn HeaderText="User&#160;Role">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("userRole") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                         
                    </Columns>
                                            </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>
                         <telerik:RadPageView ID="t3" runat="server">
                             <telerik:RadGrid ID="gridServices" runat="server" CellSpacing="0" 
                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
                                          <MasterTableView    EditMode="InPlace" >                   
                                             <Columns>
                                     
                        <telerik:GridBoundColumn DataField="AssignID" DataType="System.Int32" FilterControlAltText="Filter AssetId column"
                            ReadOnly="True" SortExpression="AssignID" UniqueName="AssignID" Display="False">
                        </telerik:GridBoundColumn>  
                              
                        <telerik:GridTemplateColumn HeaderText="Service&#160;Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_ServiceName" runat="server" Text='<%# Eval("ServiceName") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                          <telerik:GridTemplateColumn HeaderText="Service&#160;Description">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_ServiceDescription" runat="server" Text='<%# Eval("ServiceDescription") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn>  
                        <telerik:GridNumericColumn dataFormatString="{0:$###,##0.00}" DataField="Cost" DataType="System.Decimal"
                         NumericType="Currency" HeaderText="Suggested Daily Rate($)" SortExpression="Cost" UniqueName="Cost"  FooterAggregateFormatString="{0:C}">
                        </telerik:GridNumericColumn> 
                         
                    </Columns>
                </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="t4" runat="server">
                             <telerik:RadGrid ID="RadGrid_con" runat="server" CellSpacing="0" 
                                GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"  PageSize="10" Width="100%" >      
                            <MasterTableView     >                   
                                <Columns>
                                     
                        <telerik:GridBoundColumn DataField="jobconsumableid" DataType="System.Int32" FilterControlAltText="Filter jobconsumableid column"
                            ReadOnly="True" SortExpression="jobconsumableid" UniqueName="jobconsumableid" Display="False">
                        </telerik:GridBoundColumn>  
                              
                        
                        <telerik:GridTemplateColumn HeaderText="Consumable Name">
                           <ItemTemplate >                              
                            <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("ConName") %>'></asp:Label>
                           </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                        <telerik:GridTemplateColumn HeaderText="Qty">
                            <ItemTemplate >                              
                            <asp:Label ID="lbl_Username" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </telerik:GridTemplateColumn> 
                         
                    </Columns>
                    </MasterTableView>     
                        </telerik:RadGrid>
                        </telerik:RadPageView>--%>
                                            <telerik:RadPageView ID="t5" runat="server">
                                                <telerik:RadGrid ID="RadGrid_kits" OnItemDataBound="RadGrid_kits_ItemDataBound" runat="server" CellSpacing="0"
                                                    GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="10" Width="100%">
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn DataField="jobkitid" DataType="System.Int32" FilterControlAltText="Filter jobconsumableid column"
                                                                ReadOnly="True" SortExpression="jobkitid" UniqueName="jobkitid" Display="False">
                                                            </telerik:GridBoundColumn>


                                                            <telerik:GridTemplateColumn HeaderText="Kit Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Userrole" runat="server" Text='<%# Eval("kitname") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_kitid" runat="server" Text='<%# Eval("kitid") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_jobid" runat="server" Text='<%# Eval("jobid") %>' Visible="false"></asp:Label>

                                                                </ItemTemplate>

                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Kit Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_kitstatusid" runat="server" Text='<%# Eval("KitDeliveryStatus") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_kitstatus" runat="server"></asp:Label>
                                                                </ItemTemplate>

                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Tools">
                                                                <ItemTemplate>

                                                                    <telerik:RadGrid ID="radgridkitassets" runat="server" ShowHeader="false" AutoGenerateColumns="true">
                                                                    </telerik:RadGrid>
                                                                </ItemTemplate>

                                                            </telerik:GridTemplateColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </telerik:RadPageView>
                                        </telerik:RadMultiPage>


                                    </NestedViewTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>


                        </asp:TableCell>

                    </asp:TableRow>
                </asp:Table>

                </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadWindowManager ID="radwin" runat="server">
                                <Windows></Windows>
                            </telerik:RadWindowManager>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

