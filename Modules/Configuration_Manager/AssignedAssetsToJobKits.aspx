<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="AssignedAssetsToJobKits.aspx.cs" Inherits="Modules_Configuration_Manager_AssignedAssetsToJobKits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="../../js/underscore.js" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript">
        var obj = new Array();
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var item = eventArgs.get_item();
            if (item.get_value() != "Select" && obj[item.get_value()] == undefined) {
                removePropertyValue(sender.get_id());
                obj[item.get_value()] = sender.get_id();
            }
            //else if (keyExists(item.get_value())) {
            //    removePropertyValue(sender.get_id());
            //    obj[item.get_value()] = sender.get_id()
            //}
            else {
                if (item.get_value() != "Select") {
                    alert("This Serial Number is already assigned in the current page");
                    sender.set_text("Select");
                    removePropertyValue(sender.get_id());
                }

                //obj[item.get_value()] = sender.get_id();

            }

        }
        var getProperty = function (propertyName) {
            return obj[propertyName];
        };
        var removePropertyValue = function (inValue) {


            for (var i in obj) {
                if (obj[i] == inValue) {
                    //alert(i + '-' + inValue);
                    //obj.splice(i, 1);
                    delete obj[i];
                }
            }
        };

        var keyExists = function (inValue) {


            for (var i in obj) {
                if (i == inValue) {

                    return true;
                }
            }
            return false;
        };


        function validationfortype() {

            var combo = $find("<%= combo_job.ClientID %>");
            var text = combo.get_text();
            if (text == "Select JobName" || text == "") {
                radalert('Please Select Job', 330, 180, 'Alert Box', null, null);
                return false;

            }
            

        }

</script>
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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lbl_message" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Select Job:<br />
                            <telerik:RadComboBox runat="server" ID="combo_job"  DataSourceID="SqlGetJobs" 
                            DataTextField="jobname" DataValueField="jid"  EmptyMessage="Select JobName" Width="200px"
                                ></telerik:RadComboBox>
                            <asp:SqlDataSource ID="SqlGetJobs" ConnectionString="<%$ databaseExpression:client_database %>"  runat="server" 
                               SelectCommand="select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where status!='Closed' and (jid in(select jobid from PrismJobKits) or jid in (select jobid from PrismJobConsumables))"></asp:SqlDataSource>
                            <%--SelectCommand="select [jid],jobname + ' ('+jobordercreatedid+')' as Jobname from manageJobOrders where (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''--%>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_view" runat="server" Text="View" OnClientClick="javascript:return validationfortype();" OnClick="btn_view_OnClick" />
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td align="center" valign="top">
                            <asp:Panel ID="pnl_adddet" runat="server"></asp:Panel>
                        </td>
                        <%--<td style="width:10px"></td>
                        <td valign="top">
                            <asp:Panel ID="pnl_consumables" runat="server"></asp:Panel>
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn_saving" runat="server" Text="Save" OnClick="btn_saving_OnClick" />
                        </td>
                        <td>
                            <asp:Button ID="btn_save" runat="server" Text="Save/Finalize" OnClick="btn_save_OnClick" />
                        </td>
                        
                    </tr>
                </table>
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
</asp:Content>

