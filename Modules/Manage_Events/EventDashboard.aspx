<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" Trace="false" CodeFile="EventDashboard.aspx.cs" Inherits="Modules_Manage_Events_EventDashboard" %>
<%@ Register TagPrefix="SDP" TagName="Top10Alerts" Src="~/controls/manage_events/Top10AlertsbyCategory.ascx" %>
<%@ Register TagPrefix="SDP" TagName="GaugeWidget" Src="~/controls/manage_events/6GuageWidget.ascx" %>
<%@ Register TagPrefix="SDP" TagName="DayWeekWidget" Src="~/controls/manage_events/DayWeekMonthYearAlertTypes.ascx"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="~/css/kendo.default.min.css" rel="stylesheet" />
    <script type="text/javascript" src="~/js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            

            <table width="100%">
                <tr>
                    <td align="center">
                        <table  border="0">
            <tr>
                <td valign="top">
                    <SDP:GaugeWidget runat="server" id="gaugewidget1" />
                </td>
            </tr>
                <tr>
                    <td style="font-size:14px;  color:White; padding:4px; background-color:Black;" align="left" valign="top">
                        <b>Alert Types</b>
                    </td>

                </tr>

                <tr>
                    <td valign="top">
                       <!-- include day week month year display pies -->
                           <SDP:DayWeekWidget runat="server" ID="dayweek" />
                       <!-- end include day week month year display pies -->
                    </td>
                </tr>

                <tr>

                    <td style="font-size:14px; color:White; padding:4px; background-color:Black;" 
                        align="left" valign="top">
                        <b>Top 5 Alerts</b>
                    </td>

                </tr>

                <tr>
                    <td align="left" valign="top">
                        <!-- include top 10 graph -->
                        <SDP:Top10Alerts runat="server" ID="top10chart" />
                        <!-- end include top 10 graph -->
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
            </table>
                    </td>
                </tr>
            </table>


</asp:Content>

