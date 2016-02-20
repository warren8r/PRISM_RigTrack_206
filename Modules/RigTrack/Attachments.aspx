<%@ Page Title="Close Jobs" Language="C#" MasterPageFile="~/Masters/RigTrack_noheading.master" AutoEventWireup="true" CodeFile="Attachments.aspx.cs" Inherits="Modules_RigTrack_Attachments" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ViewAttachmentLinkButton") >= 0) {
                args.set_enableAjax(false);
            }
        }
        function Close() {
            GetRadWindow().close(); // Close the window 
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

            return oWindow;
        }
    </script>
</telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" ClientEvents-OnRequestStart="onRequestStart">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadGrid1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
<telerik:RadWindow ID="RadWindow1" runat="server">
</telerik:RadWindow>

        <fieldset style="width:95%">
                   <telerik:RadGrid  ID="RadGrid1" AllowFilteringByColumn="false" AllowPaging="true" PageSize="5" AllowSorting="true" OnItemDataBound="RadGrid1_ItemDataBound" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" ShowFooter="true" OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource">
                    <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                    <ExportSettings>
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView PagerStyle-AlwaysVisible="true" DataKeyNames="ID">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Attachment ID" DataField="ID" UniqueName="ID" SortExpression="ID"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Attachment Name" DataField="Name" UniqueName="Name" SortExpression="Name"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Attachment">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ViewAttachmentLinkButton" runat="server" CommandName="ViewAttachment">
                                        Download
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <table>
                    <tr>
                        <asp:Button ID="btnClose" runat="server" OnClientClick="Close();" Text="Close" > </asp:Button>
                    </tr>
                </table>
        </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>