<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="confMngrManageSDPAccounts.aspx.cs" Inherits="Modules_Configuration_Manager_confMngrManageSDPAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <h2>(Super/Client Admin) Manage SDPs/Accounts &gt;&gt; View/Edit/Add SDPs/Account</h2><br>
    <div id="container" style="width:600px; height:400px;">
            <div style="height: 180px;">
                <div style="">
                    SDP Name: <input type="text" name="NAMEME" style="height:16px; width:200px;"><br>
                    Country:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="text" name="NAMEME" style="height:16px; width:200px;"><br>
                    Address1: &nbsp;&nbsp;<input type="text" name="NAMEME" style="height:16px; width:200px;"><br>
                    Address2: &nbsp;&nbsp;<input type="text" name="NAMEME" style="height:16px; width:200px;"><br>
                    City:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="text" name="NAMEME" style="height:16px; width:200px;"><br>
                    State:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <select name="NAMEME">
                        <option value="0">(Select State)</option>
                        <option value="1">Alabama</option>
                        <option value="2">Arkansas</option>
                        <option value="3">Louisiana</option>
                    </select><br>
                <br><input type="submit" value="Add SDP" style="border-radius:5px; background-color:Lightgreen;"/>&nbsp;&nbsp;&nbsp;<input type="submit" value="Reset Fields" style="border-radius:5px; background-color:Orange;"/><br>
                </div>
            </div>
            <div style="height:5%; margin-top: 1em; background:gray; color:White;">
                <div style="margin-left:30px; float:left; width: 75px;">
                    Plant Name
                </div>
                <div style="margin-left:20px; float:left;">
                    Address
                </div>
                <div style="margin-left:20px; float:left;">
                    City
                </div>
                <div style="margin-left:20px; float:left;">
                    State
                </div>
                <div style="margin-left:20px; float:left;">
                    Zip
                </div>
                <div style="margin-left:20px; float:left;">
                    Phone
                </div>
                <div style="margin-left:20px; float:left;">
                    Email
                </div>
                <div style="margin-left:20px; float:left;">
                    Active
                </div>
            </div>
            <div style="height:5%;"><a href="#">Edit</a>&nbsp;&nbsp;<a href="#">Add Account</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Data Data Data Data Data Data Data Data Data Data Data Data Data 
                <input type=checkbox style="text-align:right; margin-left:5%"/>
            </div>
            <div style="height:5%; background:#f0f0f0;"><a href="#">Edit</a>&nbsp;&nbsp;<a href="#">Add Account</a>&nbsp;&nbsp; This grid will have "accordion" drop down to 
                <input type=checkbox style="text-align:right; margin-left:85%"/>
            </div>
            <div style="height:5%;"><a href="#">Edit</a>&nbsp;&nbsp;<a href="#">Add Account</a>&nbsp;&nbsp;show accounts/meters associated per SDP
                <input type=checkbox style="text-align:right; margin-left:85%"/>
            </div>
            <div style="height:5%; background:#f0f0f0;"><a href="#">Edit</a>&nbsp;&nbsp;<a href="#">Add Account</a>&nbsp;&nbsp;
                <input type=checkbox style="text-align:right; margin-left:85%"/>
            </div>
    </div>
</asp:Content>

