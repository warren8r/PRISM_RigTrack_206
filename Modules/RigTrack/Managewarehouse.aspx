<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    Trace="false" CodeFile="Managewarehouse.aspx.cs" Inherits="Modules_Configuration_Manager_SDP" Debug="true" %>

<%@ Register TagPrefix="MDM" TagName="Warehouse" Src="~/Modules/RigTrack/mngWarehouse.ascx" %>
<%@ Register Assembly="Artem.Google" Namespace="Artem.Google.UI" TagPrefix="cc1" %> 
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="star">(<span class="star">*</span>) indicates mandatory fields</div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="all" runat="server"
        DecorationZoneID="ZoneID1"></telerik:RadFormDecorator>
    <telerik:RadAjaxPanel runat="server" ID="radAjaxpnlMAster">
    <table width="100%" >
        <tr>
        <td align="center">
        <!-- this fixes some performance DO-NOT remove -->
        <div id="ZoneID1">
           <%-- <telerik:RadTabStrip CausesValidation="False" ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                AutoPostBack="True" Style="padding: 20px 0px 0px 10px;" SelectedIndex="0" CssClass="tabStrip"
                Skin="Forest">
                <Tabs>
                    <telerik:RadTab Text="Warehouse" ForeColor="White" Selected="True" PageViewID="pvCollectors">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Customer" ForeColor="White" PageViewID="pvSDP">
                    </telerik:RadTab>
                  
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage RenderSelectedPageOnly="true" ID="RadMultiPage1" runat="server"
                SelectedIndex="0" CssClass="multiPage">
                <telerik:RadPageView ID="pvCollectors" runat="server">--%>
                    <MDM:Warehouse runat="Server" ID="Warehouse" />
               <%-- </telerik:RadPageView>
                <telerik:RadPageView ID="pvSDP" runat="server">
                    <MDM:Customer runat="Server" ID="Customer" />
                </telerik:RadPageView>
            
            </telerik:RadMultiPage>--%>
        </div>
        </td>
        </tr>
        </table>
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
