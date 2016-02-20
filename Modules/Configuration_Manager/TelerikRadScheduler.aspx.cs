using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

public partial class Modules_Configuration_Manager_TelerikRadScheduler : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (RadScheduler1.SelectedView == SchedulerViewType.TimelineView)
        {
            //TimelineConfigPanel.Visible = true;
            TimelineConfigPanel.Visible = false;
            MultidayConfigPanel.Visible = false;

            //RadScheduler1.TimelineView.SlotDuration = TimeSpan.Parse(DurationList.SelectedValue);
            //RadScheduler1.TimelineView.TimeLabelSpan = int.Parse(TimeLabelSpan.SelectedValue);
            //RadScheduler1.TimelineView.ColumnHeaderDateFormat = ColumnHeaderDateFormat.SelectedValue;
            //RadScheduler1.TimelineView.NumberOfSlots = int.Parse(NumberOfSlotsList.SelectedValue);
            //RadScheduler1.TimelineView.GroupingDirection = (GroupingDirection)Enum.Parse(typeof(GroupingDirection), GroupingDirection.SelectedValue);


            RadScheduler1.TimelineView.SlotDuration = TimeSpan.Parse("7.00:00:00");
            RadScheduler1.TimelineView.TimeLabelSpan = int.Parse("1");
            RadScheduler1.TimelineView.ColumnHeaderDateFormat = "MM/dd/yyyy";// ColumnHeaderDateFormat.SelectedValue;
            RadScheduler1.TimelineView.NumberOfSlots = int.Parse("24");
            RadScheduler1.TimelineView.GroupingDirection = (GroupingDirection)Enum.Parse(typeof(GroupingDirection), "Vertical");
        }
        else if (RadScheduler1.SelectedView == SchedulerViewType.MultiDayView)
        {
            TimelineConfigPanel.Visible = false;
            MultidayConfigPanel.Visible = true;

            RadScheduler1.MultiDayView.NumberOfDays = int.Parse(NumberOfDaysList.SelectedValue);
            RadScheduler1.FirstDayOfWeek = (DayOfWeek)int.Parse(FirstDayOfWorkWeekList.SelectedValue);

            //SelectedDate adjustment to make a Work Week view from Multi-day view 

            RadScheduler1.SelectedDate = RadScheduler1.SelectedDate.AddDays((int)RadScheduler1.FirstDayOfWeek - (int)RadScheduler1.SelectedDate.DayOfWeek);

        }
       
    }
    protected void RadScheduler1_TimeSlotCreated(object sender, TimeSlotCreatedEventArgs e)
    {
        RadScheduler scheduler = (RadScheduler)sender;

        if (scheduler.SelectedView == SchedulerViewType.TimelineView)
        {

            e.TimeSlot.CssClass = "backcolorstr";
        }

    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        RadScheduler1.ExportToPdf();
    }
    protected void RadScheduler1_NavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
    {
        if (RadScheduler1.SelectedView == SchedulerViewType.TimelineView)
        {
            //TimelineConfigPanel.Visible = true;
            TimelineConfigPanel.Visible = false;
            MultidayConfigPanel.Visible = false;
        }
        else if (RadScheduler1.SelectedView == SchedulerViewType.MultiDayView)
        {

            RadScheduler1.MultiDayView.NumberOfDays = int.Parse(NumberOfDaysList.SelectedValue);
            RadScheduler1.FirstDayOfWeek = (DayOfWeek)int.Parse(FirstDayOfWorkWeekList.SelectedValue);

            //SelectedDate adjustment to make a Work Week view from Multi-day view 
            int WorkWeekAdjustmentTimeShift = (int)RadScheduler1.FirstDayOfWeek - (int)RadScheduler1.SelectedDate.DayOfWeek;
            if (e.Command == SchedulerNavigationCommand.NavigateToNextPeriod)
            {
                if (WorkWeekAdjustmentTimeShift < 0)
                    WorkWeekAdjustmentTimeShift += 7;

            }
            else if (e.Command == SchedulerNavigationCommand.NavigateToPreviousPeriod)
            {
                if (WorkWeekAdjustmentTimeShift > 0)
                    WorkWeekAdjustmentTimeShift -= 7;
            }
            RadScheduler1.SelectedDate = RadScheduler1.SelectedDate.AddDays(WorkWeekAdjustmentTimeShift);


            TimelineConfigPanel.Visible = false;
            MultidayConfigPanel.Visible = true;
        }
    }
    

}