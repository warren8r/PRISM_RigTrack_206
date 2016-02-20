<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="ManageComponents.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageMeters" %>

<%@ Register TagPrefix="PRISM" TagName="Component" Src="~/Controls/confgMangeComponent/mngComponent.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <telerik:RadAjaxPanel ID="radajxPanel1" runat="server">
        <div style="padding-left:200px">
      <PRISM:Component ID="component" runat="server" />
         </div>
    </telerik:RadAjaxPanel>
   
</asp:Content>

