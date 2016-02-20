<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="ManageDepartments.aspx.cs" Inherits="Modules_Configuration_Manager_ManageDepartments" %>

<%@ Register src="../../controls/confgMangrAsset/mngDepartment.ascx" tagname="mngDepartment" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
    <uc1:mngDepartment ID="mngDepartment1" runat="server" />
</asp:Content>

