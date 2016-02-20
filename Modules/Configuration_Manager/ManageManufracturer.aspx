<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="ManageManufracturer.aspx.cs" Inherits="Modules_Configuration_Manager_ManageManufracturer" %>

<%@ Register src="../../controls/confgMangrAsset/mngManufracturer.ascx" tagname="mngManufracturer" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
    
    <uc1:mngManufracturer ID="mngManufracturer1" runat="server" />
    
</asp:Content>

