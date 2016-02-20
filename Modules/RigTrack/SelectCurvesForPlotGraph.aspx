<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCurvesForPlotGraph.aspx.cs" Inherits="Modules_RigTrack_SelectCurvesForPlotGraph" MasterPageFile="~/Masters/2DPlotGraph.master" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        var listBox;
        var listBox2;
        function pageLoad() {
            listBox = $find("<%= ListBox1.ClientID %>");
            listBox2 = $find("<%= ListBox2.ClientID %>");
        }
        function Close() {
            GetRadWindow.close();
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;
        }
        function transferRight() {
            var selectedItem = listBox.get_selectedItem();
            if (selectedItem == null) {
                return false;
            }
            listBox.transferToDestination(selectedItem);
            return false;
        }
        function transferLeft() {
            var listBox2 = listBox.get_transferTo();
            var selectedItem = listBox2.get_selectedItem();
            if (selectedItem == null) {
                return false;
            }
            listBox.transferFromDestination(selectedItem);
            return false;
        }

        function returnValue() {
            var oWnd = GetRadWindow();
            //alert(oWnd);
            var dialog1 = oWnd.get_windowManager().getWindowByName("GraphWindow");
            var contentWin = dialog1.get_contentFrame().contentWindow;
            var returnList = [];
            for (var i = 0; i < listBox2.get_items().get_count() ; i++) {
                //alert(listBox2.get_items().getItem(i).get_value());

                
                var object = {};
                object['ID'] = listBox2.get_items().getItem(i).get_value();
                returnList.push(object);
                
            }
            contentWin.GraphSelectedCurves(returnList);
            oWnd.close();
        }
    </script>
    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>

            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                    <h2>Select Curves for Plot</h2>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
            <asp:TableRow>
                <asp:TableCell>
                    <telerik:RadListBox ID="ListBox1" runat="server" Height="230px" TransferToID="ListBox2" Width="300px"></telerik:RadListBox>
                    
                </asp:TableCell>
                <asp:TableCell>
                    <telerik:RadButton runat="server" ID="btnTransferRight" AutoPostBack="false" OnClientClicked="transferRight" Text=" >>"></telerik:RadButton>
                    <telerik:RadButton runat="server" ID="btnTransferLeft" AutoPostBack="false" OnClientClicked="transferLeft" Text =" <<"></telerik:RadButton>
                </asp:TableCell>
                <asp:TableCell>
                    <telerik:RadListBox ID="ListBox2" runat="server" Height="230px" Width="295px"></telerik:RadListBox>
                    <telerik:RadButton  runat="server" ID="btnApply" AutoPostBack="false" Text="Apply" OnClientClicked="returnValue" ></telerik:RadButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </fieldset>



</asp:Content>