<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createjobtypes.aspx.cs" Inherits="createjobtypes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet/less" type="text/css" href="css/mdm.less" />
    <link rel="stylesheet/less" type="text/css" href="css/jquery-ui-1.10.3.custom.css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="rad1" runat="server"></telerik:RadScriptManager>

    <div>
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>


   <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>

        <table style="font-family:Verdana;font-size:10px">
                <tr>
                    <td>
                        <asp:Label ID="lbl_jcrt" runat="server"></asp:Label>
                        <asp:Label ID="lbl_updateid" runat="server" style="display:none"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Job Type:<br />
                        <telerik:RadTextBox ID="radtxt_jname" runat="server"></telerik:RadTextBox> 
                    </td>
                </tr>
                <tr>
                    <td>
                        Job Type Description:<br />
                        <telerik:RadTextBox ID="radtxt_jobdesc" Height="100px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<telerik:RadButton ID="RadButton2" runat="server" Text="Save" AutoPostBack="true" OnClick="Button2_Click">
                        </telerik:RadButton>--%>
                        <asp:Button ID="btn_crtjob" Text="Create"  runat="server" OnClick="btn_crtjob_Click">
                        </asp:Button>
                    </td>
                </tr>
                <tr><td style="height:10px"></td></tr>
                <tr>
                    <td>
                        <b>Existing Job Types:</b>


                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="radgrid_existingJobs" runat="server" AllowPaging="True" PageSize="3" AllowSorting="True" 
                        CssClass="mdmGrid active" 
                            CellSpacing="0" DataSourceID="SqlDataSource1" GridLines="None"   >
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="jobtypeid" DataSourceID="SqlDataSource1">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="jobtype" 
                                        HeaderText="Job Type"  SortExpression="jobtype"
                                        UniqueName="jobtype">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="jobtypedescription" 
                                        HeaderText="Job Description"  SortExpression="jobtypedescription"
                                        UniqueName="jobtypedescription">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="View/Edit" >
                                        <ItemTemplate> 
                                        <asp:LinkButton ID="lnk_edit" runat="server" OnClick="lnk_edit_Click" Text="View/Edit"></asp:LinkButton>
                                        <asp:Label ID="lbl_jobtypeid" runat="server" Text='<%# Eval("jobtypeid") %>' style="display:none;"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                        SelectCommand="SELECT * FROM [jobTypes]"></asp:SqlDataSource>--%>
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ databaseExpression:client_database %>"
                                    SelectCommand="SELECT * FROM [jobTypes]"></asp:SqlDataSource>


        </ContentTemplate>
            <Triggers>
                
                
                <asp:AsyncPostBackTrigger ControlID="btn_crtjob" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
