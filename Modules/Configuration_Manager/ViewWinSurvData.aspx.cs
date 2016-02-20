using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;

public partial class Modules_Configuration_Manager_ViewWinSurvData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            date_start.SelectedDate = DateTime.Now.AddDays(-1);
            date_stop.SelectedDate = DateTime.Now.AddMonths(1);
            SqlGetJobs.SelectCommand = "select 0 as [jobname],'Select JobName' as Jobtextname union select [jobname],jobname + ' ('+jobordercreatedid+')' as Jobtextname from manageJobOrders where status!='Closed' and jobfrom='WinSurv' and salecreateddate between '" + date_start.SelectedDate + "' and '" + date_stop.SelectedDate + "'";
            combo_job.DataBind();
        }
    }
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        info.Visible = true;
        //radgrid_repairstatus.Rebind();
    }
    protected void date_stop_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {
        SqlGetJobs.SelectCommand = "select 0 as [jobname],'Select JobName' as Jobtextname union select [jobname],jobname + ' ('+jobordercreatedid+')' as Jobtextname from manageJobOrders where status!='Closed' and jobfrom='WinSurv' and salecreateddate between '" + date_start.SelectedDate + "' and '" + date_stop.SelectedDate + "'";
        combo_job.DataBind();
    }
    protected void date_start_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {
        SqlGetJobs.SelectCommand = "select 0 as [jobname],'Select JobName' as Jobtextname union select [jobname],jobname + ' ('+jobordercreatedid+')' as Jobtextname from manageJobOrders where status!='Closed' and jobfrom='WinSurv' and salecreateddate between '" + date_start.SelectedDate + "' and '" + date_stop.SelectedDate + "'";
        combo_job.DataBind();
    }
}