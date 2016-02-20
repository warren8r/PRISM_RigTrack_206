<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="confMngrManageMeters.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageMeters" %>

<%@ Register TagPrefix="MDM" TagName="Meters" Src="~/Controls/confgMangrMeter/mngMeter.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="star">(<span class="star">*</span>) indicates mandatory fields</div>
    <telerik:RadAjaxPanel ID="radajxPanel1" runat="server">
        <MDM:Meters id="mtrPage" runat="server"></MDM:Meters>
    </telerik:RadAjaxPanel>
</asp:Content>

