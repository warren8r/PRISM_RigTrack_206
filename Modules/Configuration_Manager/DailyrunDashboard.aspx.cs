using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
public partial class Modules_Configuration_Manager_DailyrunDashboard : System.Web.UI.Page
{
    public static DataTable dt_runpersons = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dt_runpersons = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from PrismJobRunPersonals PRA,PrismJobRunDetails PJRD,Users U where PRA.runid=PJRD.runid and PRA.personid=U.userid").Tables[0];
        }
    }
    protected void grid_run_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            grid_run.ExportSettings.ExportOnlyData = true;
            grid_run.ExportSettings.OpenInNewWindow = true;
            grid_run.ExportSettings.IgnorePaging = true;
            grid_run.ExportSettings.FileName = "Export";
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        string query = "select *,(U.firstName+' '+U.lastName) as Operationmanager, pc.Name as ClientName from manageJobOrders MJ,PrsimCustomers pc,PrismJobRunDetails PJRD,Users U,RigTypes RT,PrismJobRunDailyProgress PRP" +
            " where MJ.jid=PJRD.jid and MJ.opManagerId=U.userid and RT.rigtypeid=MJ.rigtypeid and  PRP.runid=PJRD.runid and pc.ID=MJ.Customer ";
        string run = "", jobName = "";

        foreach (RadComboBoxItem radcbiSource in radcombo_job.CheckedItems)
        {
            jobName += radcbiSource.Value + ",";
        }
        foreach (RadComboBoxItem radcbiSource in combo_run.CheckedItems)
        {
            run += radcbiSource.Value + ",";
        }
       
        if (run != "")
        {
            run = run.Remove(run.Length - 1, 1);
            query += " and PJRD.runnumber in(" + run + ")";
        }
        if (jobName != "")
        {
            jobName = jobName.Remove(jobName.Length - 1, 1);
            query += " and MJ.jid in(" + jobName + ")";
        }
        query += "order by MJ.jobname,DATE,PJRD.runnumber";
        grid_run.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        grid_run.DataBind();
    }
    protected void radcombo_job_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string jobName = "";

        foreach (RadComboBoxItem radcbiSource in radcombo_job.CheckedItems)
        {
            jobName += radcbiSource.Value + ",";
        }
        string query = "select distinct runnumber from PrismJobRunDetails";
        if (jobName != "")
        {
            jobName = jobName.Remove(jobName.Length - 1, 1);
            query += " where jid in("+jobName+")";
        }
        combo_run.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        combo_run.DataBind();
       
    }
    protected void grid_run_ItemDataBound(object sender, GridItemEventArgs e)
    {
         
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            string str_person = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            string dataKeyValue = (dataItem).GetDataKeyValue("runid").ToString();
            DataRow[] row_persons = dt_runpersons.Select("runid=" + dataKeyValue);
            Label lbl_Operators = (Label)dataItem.FindControl("lbl_Operators");
             Label lbl_Activity = (Label)dataItem.FindControl("lbl_Activity");
             Label lbl_jid = (Label)dataItem.FindControl("lbl_jid");
             Label lbl_Date = (Label)dataItem.FindControl("lbl_Date");
             Label lbl_DayforJob = (Label)dataItem.FindControl("lbl_DayforJob");
             Label lbl_DaysLeftforJob = (Label)dataItem.FindControl("lbl_DaysLeftforJob");
            for (int person = 0; person < row_persons.Length; person++)
            {
                str_person += row_persons[person]["firstName"].ToString() + " " + row_persons[person]["lastName"].ToString() + ",";
            }
            if (str_person != "")
            {
                lbl_Operators.Text = str_person.Remove(str_person.Length - 1, 1);
            }


            string query = "select Top(1) rigstatuses as LastActivity from PrismJobRunActivityLog, RigStatusDet where sid=activityassetid and runid=" + dataKeyValue + "  and activityassetid <>0 and activityassetid is not null order by hour desc";
            DataTable lastActivity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            if (lastActivity.Rows.Count > 0)
            {
                lbl_Activity.Text = lastActivity.Rows[0]["LastActivity"].ToString();
            }
            else
            {
                lbl_Activity.Text = "No Activity";
            }
            DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + lbl_jid.Text + "").Tables[0];
            DateTime dtstart = new DateTime();
            DateTime dtend1 = new DateTime();
            DateTime dtend_main = new DateTime();
            if (dtdates.Rows.Count > 0)
            {
                dtstart = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
                dtend_main = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());
                dtend1 = Convert.ToDateTime(lbl_Date.Text);
                TimeSpan ts = dtend1 - dtstart;
                TimeSpan ts_end = dtend_main - dtend1;
                lbl_DayforJob.Text = (Convert.ToInt32(ts.Days) + 1).ToString();
                lbl_DaysLeftforJob.Text = (Convert.ToInt32(ts_end.Days)).ToString();
                //dtstart = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
                //dtend = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());
            }


        }
         
    }
}