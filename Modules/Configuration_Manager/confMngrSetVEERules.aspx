<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="confMngrSetVEERules.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrSetVEERules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <h2>(Super/Client Admin) Set VEE Routines</h2>
    <asp:SqlDataSource ID="sqlVeeRoutineList" runat="server" 
        ConnectionString="<%$ databaseExpression:client_database  %>" 
        SelectCommand="SELECT veeRoutinesID, veeRoutineName, veeRoutineDescription FROM veeRoutines">
    </asp:SqlDataSource>
    <br>
    <div id="container" style="width:800px; height:400px;">
        &nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="selectRoutine" runat="server" 
            DataSourceID="sqlVeeRoutineList" DataTextField="veeRoutineName" 
            DataValueField="veeRoutinesID" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged1">
        </asp:DropDownList>
        <br><br>
        <form name="addAsset" action="html_form_action.asp" method="get">            
            <div id="plantNameAndType" style="width:200px; height:200px; margin-left:20px; border:3px solid grey; border-radius:5px;">
                <div style="height:18px; background:#B7DDE8; width: 200px;">
                    <div style="margin-left:3px; float:left; width: 177px;">VEE Rules 1:</div>
                </div>
                <div style="height:18px; background:#DBEDF4; width: 200px;">
                    <div style="margin-left:10px; float:left;">Use?</div>
                </div>
               <div style="height:18px; width:200px; "><input id="timeCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;Meter Time Check</div>
               <div style="height:18px; width:200px; background:#f0f0f0;"><input id="idCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;Meter Identification Check</div>
               <div style="height:18px; width:200px; "><input id="pulseCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;Pulse Overflow Check</div>
               <div style="height:18px; width:200px; background:#f0f0f0;"><input id="testModeCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;Test Mode Check</div>
               <div style="height:18px; width:200px; "><input id="sumCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;Sum Check</div>
               <div style="height:18px; width:200px; background:#f0f0f0;"><input id="spikeCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;Spike Check</div>
               <div style="height:18px; width:200px; "><input id="kvarCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;kVARh Check</div>
               <div style="height:18px; width:200px; background:#f0f0f0;"><input id="hiLowCheck" style="margin-left:10px;" type="checkbox" runat="server" />&nbsp;&nbsp;Hi / Low Check</div>
            </div><br /><br />
            <div id="Div1" style="width:200px; height:200px; margin-left:20px; float:left; border:3px solid grey; border-radius:5px;">
                <div style="height:25px; width: 199px;">
                    <div style="margin-left:3px; float:left; width: 177px;">Routine Name<asp:TextBox ID="txtRoutineName" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div style="height:18px; width: 200px;">
                    <div style="margin-left:10px; float:left;"></div>
                </div>
                <div style="margin-left:3px; height:75px; width:200px;">Routine Description<asp:TextBox ID="txtRoutineDescription" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                <div style="height:18px; width:200px;"></div>
                <div style="height:18px; width:200px;">
                    <asp:Button ID="btnCreate" runat="server" Text="Create" /></div>
                <div style="height:18px; width:200px;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="save" /></div>
                <div style="height:18px; width:200px;"></div>
            </div>
        </form>
    </div>
</asp:Content>

