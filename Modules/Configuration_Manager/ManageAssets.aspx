<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="ManageAssets.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageMeters" %>

<%@ Register TagPrefix="MDM" TagName="Meters" Src="~/Controls/confgMangrAsset/mngAsset.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <telerik:RadAjaxPanel ID="radajxPanel1" runat="server">
        <div >
      <MDM:Meters id="mtrPage" runat="server"></MDM:Meters>
         </div>
    </telerik:RadAjaxPanel>
   
</asp:Content>

