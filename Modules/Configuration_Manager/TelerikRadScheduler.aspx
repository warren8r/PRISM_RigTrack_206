<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="TelerikRadScheduler.aspx.cs" Inherits="Modules_Configuration_Manager_TelerikRadScheduler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css"> 
        #ConfigurationPanel1 ul { 
            list-style: none; 
            padding: 0; 
            margin: 0; 
        } 
  
        #ConfigurationPanel1 ul li { 
            line-height: 24px; 
            float: left; 
            padding-left: 11px; 
            margin-left: 10px; 
        } 
        
    </style> 

    <style type="text/css">  
        
        .RadScheduler_Default .rsMonthView div.rsWrap   
        {  
          height:20px;  
        } 


    </style> 
     

       <style type="text/css">
        .backcolorstr{
        background-color:#FAEDA0 !important;
         
        }
        .backcolorstr:hover{
        background-color: lightgreen !important;
        }
 
    </style> 
    <telerik:RadCodeBlock runat="server" ID="rdbScripts">
  
        </telerik:RadCodeBlock>
        <script type="text/javascript">
            function onAppointmentClick(sender, eventArgs) {
                //$find("<%=RadWindow2.ClientID %>").show();
               var oWnd = $find("<%=RadWindow2.ClientID %>");
//                oWnd.show();
               //window.radopen("SchedulerWindow.aspx?jobid=" + eventArgs.get_appointment().get_subject(), "RadWindow2");
                var url = "SchedulerWindow.aspx?jobid=" + eventArgs.get_appointment().get_subject() + "";
                oWnd.setUrl(url);
                oWnd.setSize(1350, 500);
                oWnd.show();

            }

      
    </script> 

       
        
            <asp:Panel ID="TimelineConfigPanel" runat="server"> 
                <ul> 
                    <li style="border-left: none"> 
                        <asp:Label AssociatedControlID="DurationList" runat="server" ID="lblSlotDuration">Slot duration:</asp:Label> 
                        <asp:DropDownList runat="server" ID="DurationList" AutoPostBack="true" Style="margin-left: 27px"
                            Width="90px"> 
                            <asp:ListItem Text="15 Minutes" Value="00:15:00"></asp:ListItem> 
                            <asp:ListItem Text="1 Hour" Value="01:00:00"></asp:ListItem> 
                            <asp:ListItem Text="12 Hours" Value="12:00:00"></asp:ListItem> 
                            <asp:ListItem Text="1 Day" Value="1.00:00:00"></asp:ListItem> 
                            <asp:ListItem Text="3 Days" Value="3.00:00:00"></asp:ListItem> 
                            <asp:ListItem Text="5 Days" Value="5.00:00:00"></asp:ListItem> 
                            <asp:ListItem Text="7 Days" Value="7.00:00:00"></asp:ListItem> 
                            <asp:ListItem Text="14 Days" Value="14.00:00:00"  Selected="True"></asp:ListItem> 
                        </asp:DropDownList> 
                    </li> 
                    <li style="border-left: none"> 
                        <asp:Label AssociatedControlID="TimeLabelSpan" runat="server" ID="lblTimeLabelSpan">Time label span:</asp:Label> 
                        <asp:DropDownList runat="server" ID="TimeLabelSpan" AutoPostBack="true" Style="margin-left: 27px"
                            Width="40px"> 
                            <asp:ListItem Text="1" Value="1"></asp:ListItem> 
                            <asp:ListItem Text="2" Value="2" Selected="True"></asp:ListItem> 
                            <asp:ListItem Text="4" Value="4" ></asp:ListItem> 
                        </asp:DropDownList> 
                    </li> 
                    <li> 
                        <asp:Label AssociatedControlID="NumberOfSlotsList" runat="server" ID="lblNumberOfSlotsList">Number of slots:</asp:Label> 
                        <asp:DropDownList runat="server" ID="NumberOfSlotsList" AutoPostBack="true" Style="margin-left: 10px"
                            Width="80px"> 
                            <asp:ListItem Text="1 Slot" Value="1"></asp:ListItem> 
                            <asp:ListItem Text="2 Slots" Value="2"></asp:ListItem> 
                            <asp:ListItem Text="3 Slots" Value="3"></asp:ListItem> 
                            <asp:ListItem Text="5 Slots" Value="5"></asp:ListItem> 
                            <asp:ListItem Text="8 Slots" Value="8"></asp:ListItem> 
                            <asp:ListItem Text="16 Slots" Value="16"></asp:ListItem> 
                            <asp:ListItem Text="24 Slots" Value="24" Selected="True"></asp:ListItem>
                        </asp:DropDownList> 
                    </li> 
                    <li> 
                        <asp:Label AssociatedControlID="GroupingDirection" runat="server" ID="lblGroupingDirection">Grouping direction:</asp:Label> 
                        <asp:DropDownList runat="server" ID="GroupingDirection" AutoPostBack="true" Style="margin-left: 10px"
                            Width="90px"> 
                            <asp:ListItem Text="Vertical" Value="Vertical" Selected="True"></asp:ListItem> 
                            <asp:ListItem Text="Horizontal" Value="Horizontal"></asp:ListItem> 
                        </asp:DropDownList> 
                    </li> 
                    <li style="border-left: none"> 
                        <asp:Label AssociatedControlID="ColumnHeaderDateFormat" runat="server" ID="lblColumnHeaderDateFormat">Column header date format:</asp:Label> 
                        <asp:DropDownList runat="server" ID="ColumnHeaderDateFormat" AutoPostBack="true"
                            Style="margin-left: 27px" Width="110px"> 
                            <asp:ListItem Text="HH:mm" Value="HH:mm" ></asp:ListItem> 
                            <asp:ListItem Text="MM/dd/yyyy" Value="MM/dd/yyyy" Selected="True"></asp:ListItem> 
                        </asp:DropDownList> 
                    </li> 
                </ul> 
            </asp:Panel> 
            <asp:Panel ID="MultidayConfigPanel" runat="server"> 
                <ul> 
                    <li style="border-left: none"> 
                        <asp:Label AssociatedControlID="FirstDayOfWorkWeekList" runat="server" ID="Label1">First day of work week: </asp:Label> 
                        <asp:DropDownList runat="server" ID="FirstDayOfWorkWeekList" AutoPostBack="true"
                            Style="margin-left: 27px" Width="100px"> 
                            <asp:ListItem Text="Monday" Value="1" Selected="True"></asp:ListItem> 
                            <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem> 
                            <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem> 
                            <asp:ListItem Text="Thursday" Value="4"></asp:ListItem> 
                            <asp:ListItem Text="Friday" Value="5"></asp:ListItem> 
                            <asp:ListItem Text="Saturday" Value="6"></asp:ListItem> 
                            <asp:ListItem Text="Sunday" Value="0"></asp:ListItem> 
                        </asp:DropDownList> 
                    </li> 
                    <li style="border-left: none"> 
                        <asp:Label AssociatedControlID="NumberOfDaysList" runat="server" ID="Label2">Number of days in work week:</asp:Label> 
                        <asp:DropDownList runat="server" ID="NumberOfDaysList" AutoPostBack="true" Style="margin-left: 27px"
                            Width="40px"> 
                            <asp:ListItem Text="2" Value="2"></asp:ListItem> 
                            <asp:ListItem Text="3" Value="3"></asp:ListItem> 
                            <asp:ListItem Text="4" Value="4" Selected="True"></asp:ListItem> 
                            <asp:ListItem Text="5" Value="5"></asp:ListItem> 
                            <asp:ListItem Text="6" Value="6"></asp:ListItem> 
                            <asp:ListItem Text="7" Value="7"></asp:ListItem> 
                        </asp:DropDownList> 
                    </li> 
                </ul> 
            </asp:Panel> 
            <div style="clear: both;"> 
            </div> 
        <table width="100%">
            <tr>
                <td align="right">
                    <telerik:RadButton ID="RadButton1" runat="server" Text="Export to PDF" OnClick="RadButton1_Click">
                    <Icon PrimaryIconUrl="Image/pdf.gif"></Icon>
                </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <telerik:RadScheduler runat="server" ID="RadScheduler1" SelectedView="TimelineView" Skin="Glow" AllowDelete="false" AllowEdit="false" AllowInsert="false" 
                         DataSourceID="EventsDataSource" SelectedDate="10/21/2013" Height="700px"
                        DataKeyField="jid" DataSubjectField="jobname" DataStartField="startdate" DataEndField="enddate" OnTimeSlotCreated="RadScheduler1_TimeSlotCreated" OnClientAppointmentClick="onAppointmentClick"
                        Localization-HeaderMultiDay="Work Week" OnNavigationComplete="RadScheduler1_NavigationComplete"  > 
                        <AdvancedForm Modal="true"></AdvancedForm> 
                        <ResourceTypes > 
                            <telerik:ResourceType KeyField="jid"  Name="jobname" TextField="jobname" ForeignKeyField="jid"
                                DataSourceID="RoomsDataSource"></telerik:ResourceType> 
                        </ResourceTypes> 
                        <TimelineView UserSelectable="true"  GroupBy="jobname" GroupingDirection="Vertical">
                
                        </TimelineView> 
                        <MultiDayView UserSelectable="false"></MultiDayView> 
                        <DayView UserSelectable="false"></DayView> 
                        <WeekView UserSelectable="false"></WeekView> 
                        <MonthView UserSelectable="false"></MonthView> 
                        <ExportSettings OpenInNewWindow="true" FileName="SchedulerExport">
                            <Pdf PageTitle="Schedule" Author="Telerik" Creator="Telerik" Title="Schedule"></Pdf>
                        </ExportSettings>
                    </telerik:RadScheduler> 
                </td>
            </tr>
        </table>
        
        
        <%--<telerik:RadToolTipManager runat="server" ID="RadToolTipManager1" Width="320" Height="170" ManualClose="true"
                    Animation="None" HideEvent="Default" Text="Loading..." OnAjaxUpdate="RadToolTipManager1_AjaxUpdate"> 
                </telerik:RadToolTipManager> --%>

    
    <asp:SqlDataSource ID="EventsDataSource" runat="server" 
         ProviderName="System.Data.SqlClient" ConnectionString="<%$ databaseExpression:client_database %>" 
        SelectCommand="SELECT jid, jobname+' - Click here to view details' as jobname, jobtype,startdate, enddate, cost FROM manageJobOrders where status='Approved' and status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''"> 
       
    </asp:SqlDataSource> 

   
    <asp:SqlDataSource ID="RoomsDataSource" runat="server" 
        ProviderName="System.Data.SqlClient" ConnectionString="<%$ databaseExpression:client_database %>" 
        SelectCommand="SELECT jid, jobname FROM manageJobOrders  where status='Approved' and status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''"> 
    </asp:SqlDataSource> 


    <telerik:RadWindowManager ID="RadWindowManager2"  runat="server"   
                                 >
                            <Windows>
                                   <telerik:RadWindow ID="RadWindow2" runat="server"   >
                                        
                                        
                                   </telerik:RadWindow>
                              </Windows>
                              </telerik:RadWindowManager>
        
            
                                    <br/>
           
</asp:Content>

