<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="confMngrManageEventAssignments.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageEventAssignments" %>
<%@ Register TagPrefix="mdm" TagName="configTabs" Src="~/controls/confgMangrEvntNotif/configTabs.ascx" %>
<%@ Register TagPrefix="tab1" TagName="confgMngrTab1" Src="~/controls/confgMangrEvntNotif/confgMngrTab1.ascx" %>
<%@ Register TagPrefix="tab2" TagName="confgMngrTab2" Src="~/controls/confgMangrEvntNotif/confgMngrTab2.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/js/cmManageEvents.js" type="text/javascript"></script>
    <script type="text/javascript">
        function tmp() {
            $(".taskShowHideTab1").click(function () {
                if ($(".taskShowHideTab1 > input[type='checkbox']").is(":checked"))
                    $("#fileUploadShowTab1").show();
                else
                    $("#fileUploadShowTab1").hide();
            });

            $(".taskShowHideTab2").click(function () {
                if ($(".taskShowHideTab2 > input[type='checkbox']").is(":checked"))
                    $("#fileUploadShowTab2").show();
                else
                    $("#fileUploadShowTab2").hide();
            });

        }
        window.setInterval(function () {
            tmp();
        }, 100);
        $( function () {
            tmp();
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="customCss" Runat="Server">
<style type="text/css">
    
.tab  
{ 
    background:  transparent url(i/navigation-sprite.png) no-repeat;
    width:95px; 
    height:26px; 
    text-decoration:none; 
    font-size:13px;
    color:white !important; 
    background-position:0 0; 
    float:left; 
    text-align:center; 
    margin:0 2px; 
    line-height:26px; 
} 
.selectedTab  
{ 
    background-position:0 -26px; 
    font-weight:bold; 
    font-size:13px;     
    color:#187ecc !important; 
    background-image:  transparent url(i/navigation-sprite.png) no-repeat !important; 
} 
.hoveredTab  
{ 
     background-image:url(i/navigation-sprite.png);
} 

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="star">(<span class="star">*</span>) indicates mandatory fields</div>
    <div id="tabPage">
    <tab1:confgMngrTab1 runat="server" id="tab1" />
         <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Silk"  SelectedIndex="0"  Visible="false"
            Height="100%"  MultiPageID="RadMultiPage1">
            <Tabs>
                <telerik:RadTab runat="server" PageViewID="t1"  Text="Assign Events To Categories" Selected="True">
                </telerik:RadTab>
               <%-- <telerik:RadTab runat="server" PageViewID="t2" Text="Assign Users To Events" 
                    >
                </telerik:RadTab>--%>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" RenderSelectedPageOnly="false" 
            runat="server" SelectedIndex="0" CssClass="multiPage">
            <telerik:RadPageView ID="t1" runat="server" >
                <%--<tab1:confgMngrTab1 runat="server" id="tab1" />--%>
            </telerik:RadPageView>

            <%--<telerik:RadPageView ID="t2" runat="server">
                <tab2:confgMngrTab2 runat="server" id="tab2"/>
            </telerik:RadPageView>--%>
        </telerik:RadMultiPage>
    </div>
</asp:Content>

