<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageConsumables.aspx.cs" Inherits="Modules_Configuration_Manager_ManageConsumables" %>
<%@ Register TagPrefix="PRISM" TagName="Consumable" Src="~/Controls/confgMangeComponent/mngConsumable.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<telerik:RadAjaxPanel ID="radajxPanel1" runat="server">
        <div style="padding-left:200px">
      <PRISM:Consumable ID="Consumable" runat="server" />
       </div>
    </telerik:RadAjaxPanel>
    
</asp:Content>

 