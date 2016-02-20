<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="touManagePeakRates.aspx.cs" Inherits="Modules_TOU_touManagePeakRates" Debug="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/jscript" src="/js/jquery-1.9.1.js"></script>
<style type="text/css">
.placeholder table
    {
        width:100%;
        padding:0px;
    }
    
.placeholder table table table td
    {
        padding: 0px 10px;
        border : 0px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <script language="javascript" type="text/javascript">


        var isShift = false;

        function keyUP(keyCode) {

            if (keyCode == 16)

                isShift = false;

        }


        function isNumeric(keyCode) {

            if (keyCode == 16)

                isShift = true;



            return ((keyCode >= 46 && keyCode <= 57 || keyCode == 8 || keyCode == 190 || keyCode == 110 ||

                                (keyCode >= 96 && keyCode <= 105)) && isShift == false);

        }

        function getnextvalue(stoptext, startid) {
            if (document.getElementById(stoptext).value != "") {

                var x = parseInt(document.getElementById(stoptext).value);


                document.getElementById(startid).value = x + 1;

            }




        }


        function validation() {

            if (document.getElementById('ddl_programname').value == "-- Select --") {
                document.getElementById('ddl_programname').focus();
                return false;
            }

        }       
    
    </script>
    <fieldset style="width:310px;">
    <legend style="margin-bottom:-20px;">Time of Use >> Define Peak Rates</legend>
        <table>
            <tr>
                <td>
                    Program Type:<br />
                    <telerik:RadComboBox ID="txtProgType" runat="server" Width="150px" Height="140px" 
                                            EmptyMessage="Please select" DataSourceID="SqlDataSource3" AutoPostBack="true"
                                            DataTextField="typeName" DataValueField="ID" CausesValidation="false"
                                            MarkFirstMatch="true" onselectedindexchanged="txtProgType_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                       ConnectionString="<%$ databaseExpression:client_database %>"
                                       SelectCommand="SELECT [ID], [typeName] FROM [progType]"></asp:SqlDataSource>
                </td>
                <td>
                    Program Name:<br />
                    <telerik:RadComboBox ID="txtProgName" runat="server" Width="150px" Height="140px" Enabled="false"
                                            EmptyMessage="Please select" DataSourceID="SqlDataSource4" AutoPostBack="true" CausesValidation="false"
                                            DataTextField="progName" DataValueField="ID" MarkFirstMatch="true" OnSelectedIndexChanged="txtProgName_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ databaseExpression:client_database %>" 
                                        SelectCommand="SELECT [progName], [ID] FROM [touPrograms] WHERE ([progTypeID] = @progTypeID) AND isActive='true' AND ID IN (SELECT programID FROM touPeaksEP) AND ID NOT IN (SELECT program_id FROM et_slab_rates)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtProgType" DefaultValue="" Name="progTypeID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </fieldset>



    <table style="margin: 0 auto;">    
    <tr><td align="center"><asp:Label ID="error" runat="server" ForeColor="red" Font-Size="10px" Visible="false"></asp:Label></td></tr>
    </table>
    <table style="margin: 0 auto;">
        <tr>
            <td align="center">
                <asp:Panel ID="panel_cont" runat="server"></asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <telerik:RadButton runat="server" ID="btn_add" Text="Create" onclick="btn_add_Click" Visible="false"></telerik:RadButton>
                <telerik:RadButton runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" Visible="false"></telerik:RadButton><br /><br /><br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="Gv_Prog_Peaks" runat="server" AutoGenerateColumns="False" CssClass="label" style="min-width:500px;" OnRowCommand="Gv_Prog_Peaks_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Program Name">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbl_Program_Name" runat="server" Font-Names="Verdana" Font-Size="10px"
                                                Text='<%# Eval("progName") %>'></asp:Label>
                                            <asp:Label ID="lbl_Program_id" runat="server" Text='<%# Eval("programID") %>' Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle  CssClass="label" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#4B6C9E" Font-Names="Verdana" Font-Bold="true" ForeColor="#D1DEB6" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program Type">
                            <ItemTemplate>
                                <asp:Label ID="lbl_prog_type" runat="server" Text='<%# Eval("PROGRAM_type") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#4B6C9E"  Font-Names="Verdana" Font-Bold="true" ForeColor="#D1DEB6" />
                            <ItemStyle HorizontalAlign="Center"  CssClass="label" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="ed" Font-Names="Verdana"
                                    Font-Size="X-Small" ForeColor="Blue" Text="View Peaks"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#4B6C9E" Font-Bold="true" ForeColor="#D1DEB6"  CssClass="label" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center" class="label">
                <asp:Label runat="server" ID="lbl_message" ForeColor="Red"></asp:Label>
            </td>
        </tr>
                
    </table>
    
    

    <center>
    <asp:PlaceHolder runat="server" ID="placeholder_master"></asp:PlaceHolder>
    </center>

    <center>
    <asp:PlaceHolder runat="server" ID="placeholder_barchart" ViewStateMode="Disabled"></asp:PlaceHolder>
    </center>


    <table align="center">
        <tr align="center">
            <td>
                <asp:Panel ID="panel_seasons" runat="Server" CssClass="placeholder"></asp:Panel>
            </td>
        </tr>
    </table>
    
    
    

    </telerik:RadAjaxPanel>
</asp:Content>

