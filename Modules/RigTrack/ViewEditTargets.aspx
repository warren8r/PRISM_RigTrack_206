<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ViewEditTargets.aspx.cs" Inherits="Modules_RigTrack_ViewEditTargets" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999; ">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


    <fieldset>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>View/ Edit Target</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>

           
                        <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Target ID
                                        </asp:TableHeaderCell>


                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Target Name 
                                        </asp:TableHeaderCell>

                                           <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group ID
                                        </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve ID
                                        </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Number
                                        </asp:TableHeaderCell>
                                     

                                    </asp:TableRow>
                                    <asp:TableRow>

                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtTargetID" runat="server" Width="200px" ></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtTargetName" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtCurveGroupID" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        
                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtCurveID" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                           <asp:TableCell>
                                             <telerik:RadTextBox ID="txtCurveNumber" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                      
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>

                </asp:Table>




             
              





                <asp:Table ID="TableButtons" runat="server" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell>
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>

                        <asp:TableCell Width="36%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnEditSeletedGraph" runat="server" Text="Edit Selected Graph" OnClick="BtnEditSeletedGraph_Click"/>
                        </asp:TableCell>

                        <asp:TableCell Width="10%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                        </asp:TableCell>

                        <asp:TableCell Width="10%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnCancel" runat="server"  Text="Cancel" OnClick="BtnCancel_Click" />
                        </asp:TableCell>

                         <asp:TableCell Width="10%"></asp:TableCell>

                         <asp:TableCell>
                            <asp:Button ID="BtnClear" runat="server"  Text="Clear" OnClick="BtnClear_Click" />
                        </asp:TableCell>

                        <asp:TableCell Width="36%"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>



                <telerik:RadGrid ID="RadGrid1" AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" ShowFooter="true">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplaceColumn" HeaderText="Edit" AllowFiltering="false" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection" AutoPostBack="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Target ID" DataField="SubmissionID" UniqueName="SubmissionID" SortExpression="SubmissionID"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Target Name" DataField="UserID" UniqueName="UserID" SortExpression="UserID"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Group ID" DataField="FirstLastName" UniqueName="FirstLastName" SortExpression="FirstLastName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve ID" DataField="Project" UniqueName="Project" SortExpression="Project"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Curve Number" DataField="CostCenter" UniqueName="CostCenter" SortExpression="CostCenter"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                        
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>


            </ContentTemplate>
          
        </asp:UpdatePanel>

    </fieldset>

</asp:Content>

