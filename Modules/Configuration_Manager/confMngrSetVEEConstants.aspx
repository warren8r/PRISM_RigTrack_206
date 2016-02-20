<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="confMngrSetVEEConstants.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrSetVEEConstants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        &nbsp;&nbsp;&nbsp;
     <h2>(Super/Client Admin) Set VEE Constants</h2><br>
    <div id="container" style="width:800px; height:400px;">
        &nbsp;&nbsp;&nbsp;
     <h4>Sum Check Constants</h4><br>
        <br>
        &nbsp;&nbsp;&nbsp;
         Current Transformer Ratio (CTR):&nbsp;&nbsp;&nbsp; <input type="dropdown" name="ctr" 
            style="visibility: hidden; display: none;"><asp:TextBox ID="txtCTR" 
            runat="server" Height="16px" Width="95px"></asp:TextBox>
        <br><br>
                 &nbsp;&nbsp;&nbsp;
 Voltage Transformer Ratio (VTR):&nbsp;&nbsp;&nbsp; <input type="dropdown" name="vtr" 
            style="visibility: hidden; display: none;"><asp:TextBox ID="txtVTR" 
            runat="server" Height="16px" Width="95px"></asp:TextBox>
        <br><br>
 <h4>Spike Check Constant</h4><br>
        <br>
        &nbsp;&nbsp;&nbsp;
         Peak Ratio Constant:&nbsp;&nbsp;&nbsp; <input type="dropdown" name="spike" 
            style="visibility: hidden; display: none;"><asp:TextBox ID="txtSpike" 
            runat="server" Height="16px" Width="95px"></asp:TextBox>
        <br><br>
       <br><br>
       <br><br>

        &nbsp;&nbsp;&nbsp;
        <br>
                &nbsp;&nbsp;&nbsp;
 <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
            Text="Commit Constants" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" />

    </div>
</asp:Content>

