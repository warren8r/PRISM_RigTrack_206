<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="AddSurvey.aspx.cs" Inherits="Modules_RigTrack_AddSurvey" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function openwin() {


            window.radopen(null, "window_service");

        }


    </script>
    <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Always">

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
                    <div align="left" class="contactmain">
                        <img src="../../images/ManageCurveGroups/AddSurvey.png" alt="AddSurvey" /><br />
                    </div>
                </tr>
                <tr>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"></asp:Button>
                    &nbsp;
                        &nbsp;
                       <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear"></asp:Button>
                    &nbsp;
                        &nbsp;
                       <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"></asp:Button>
                </tr>

            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

