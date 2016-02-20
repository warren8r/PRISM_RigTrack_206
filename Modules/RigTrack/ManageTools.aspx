<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true"
    CodeFile="ManageTools.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageMeters" %>

<%@ Register TagPrefix="MDM" TagName="Meters" Src="~/Modules/RigTrack/mngAsset.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <telerik:RadAjaxPanel ID="radajxPanel1" runat="server">
        <div>
      <MDM:Meters id="mtrPage" runat="server"></MDM:Meters>
        </div>
    </telerik:RadAjaxPanel>
    
</asp:Content>

