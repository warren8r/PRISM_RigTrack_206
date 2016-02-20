<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    Trace="false" CodeFile="mngSDP.aspx.cs" Inherits="Modules_Configuration_Manager_SDP" Debug="true" %>
<%@ Register TagPrefix="MDM" TagName="SDP" Src="~/Controls/configMangrSDP/mngSDP.ascx" %>
<%@ Register TagPrefix="MDM" TagName="Accounts" Src="~/Controls/configMangrSDP/mngAccounts.ascx" %>
<%@ Register TagPrefix="MDM" TagName="Collectors" Src="~/Controls/configMangrSDP/mngCollectors.ascx" %>
<%@ Register Assembly="Artem.Google" Namespace="Artem.Google.UI" TagPrefix="cc1" %> 
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="star">(<span class="star">*</span>) indicates mandatory fields</div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="all" runat="server"
        DecorationZoneID="ZoneID1"></telerik:RadFormDecorator>
    <telerik:RadAjaxPanel runat="server" ID="radAjaxpnlMAster">
        <!-- this fixes some performance DO-NOT remove -->
        <div id="ZoneID1">
            <telerik:RadTabStrip CausesValidation="False" ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                AutoPostBack="True" Style="padding: 20px 0px 0px 10px;" SelectedIndex="0" CssClass="tabStrip"
                Skin="Office2010Black">
                <Tabs>
                    <telerik:RadTab Text="Collectors" ForeColor="White" Selected="True" PageViewID="pvCollectors">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Service Delivery Points (SDP)" ForeColor="White" PageViewID="pvSDP">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Accounts" ForeColor="White" PageViewID="pvAccounts">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage RenderSelectedPageOnly="true" ID="RadMultiPage1" runat="server"
                SelectedIndex="0" CssClass="multiPage">
                <telerik:RadPageView ID="pvCollectors" runat="server">
                    <MDM:Collectors runat="Server" ID="collectors" />
                </telerik:RadPageView>
                <telerik:RadPageView ID="pvSDP" runat="server">
                    <MDM:SDP runat="Server" ID="SDP" />
                </telerik:RadPageView>
                <telerik:RadPageView ID="pvAccounts" runat="server" Width="100%">
                    <MDM:Accounts runat="server" ID="accounts" />
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </telerik:RadAjaxPanel>
    
    <script type="text/javascript">
        function resizeField() {
            var maxHeight = -1;
           // console.log("test");

            $('.gis-form')
                .height("auto")
                .each(function () {
                    maxHeight = maxHeight > $(this).height() ? maxHeight : $(this).height();
                });

            $('.gis-form').each(function () {
                $(this).height(maxHeight);
            });
           // console.log(maxHeight);
        }

//        window.setInterval(function () {
//            resizeField();
//        }, 100);

        $(document).bind("ready, click", function () {
            resizeField();
        });
    </script>
</asp:Content>
