using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceProcess;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;

public partial class ClientAdmin_DAServices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt_servicename = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daService  where status='Active'").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_servicename, dt_servicename, "serviceName", "serviceId", "0");
            radcombo_time.DataSource = GetTimeIntervals();
            radcombo_time.DataBind();

            radcombo_date.DataSource = Getdate();
            radcombo_date.DataBind();
            //GRID BIND
            gridbind();
            //CLOSE


            //for (int intmonth = 1; intmonth < 13; intmonth++)
            //{
            //    drpMonth.add(intmonth);
            //}
            //try
            //{
            //    ServiceController service = new ServiceController();

            //    service.ServiceName = "MDMService";
            //    //service.MachineName = "IAS-INDIA-PC";
            //    string svcStatus = service.Status.ToString();
            //    if (svcStatus == "Stopped")
            //    {
            //        btn_stop.Enabled = false;
            //        btn_start.Enabled = true;
            //    }
            //    else
            //    {
            //        btn_stop.Enabled = true;
            //        btn_start.Enabled = false;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    lbl_message.Text="Service not found";
            //}
        }
    }
    public void gridbind()
    {
        DataTable dt_gridbind = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daScheduledServices ss,daService s,daServiceTemplates st where ss.serviceId=s.serviceId and ss.templateId=st.templateId").Tables[0];

        radgrid_daservice.DataSource = dt_gridbind;
        radgrid_daservice.DataBind();
    }
    protected void CheckChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label lbl_ScheduledServiceID = (Label)row.FindControl("lbl_ScheduledServiceID");
        Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");
        Label lbl_tempid = (Label)row.FindControl("lbl_tempid");
        int eventCategoryId = Convert.ToInt32(lbl_ScheduledServiceID.Text);
        if (lbl_statuscheck.Text == "Active")
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update daScheduledServices set status='Inactive' where ScheduledServiceID=" + eventCategoryId + " and templateId="+lbl_tempid.Text+"");
        }
        else
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update daScheduledServices set status='Active' where ScheduledServiceID=" + eventCategoryId + " and templateId=" + lbl_tempid.Text + "");
        }
        gridbind();
       
    }
    protected void radgrid_daservice_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            Label lbl_statuscheck = (Label)item.FindControl("lbl_statuscheck");
            if (lbl_statuscheck.Text == "Active")
            {
                lbl_statuscheck.ForeColor = System.Drawing.Color.Green;
                isChecked.Checked = true;
            }
            else
            {
                lbl_statuscheck.ForeColor = System.Drawing.Color.Red;
                isChecked.Checked = false;
            }
            //isChecked.ForeColor = isChecked.Checked ? Color.Red : Color.Blue;
        }
    }
    public List<string> GetTimeIntervals()
    {
        List<string> timeIntervals = new List<string>();
        TimeSpan startTime = new TimeSpan(0, 0, 0);
        DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
        timeIntervals.Add("Select");
        for (int i = 0; i < 48; i++)
        {
            int minutesToBeAdded = 30 * i;      // Increasing minutes by 30 minutes interval
            TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
            TimeSpan t = startTime.Add(timeToBeAdded);
            DateTime result = startDate + t;
            timeIntervals.Add(result.ToShortTimeString());      // Use Date.ToShortTimeString() method to get the desired format                
        }
        return timeIntervals;
    }
    public List<string> Getdate()
    {
        List<string> date = new List<string>();

        date.Add("Select");
        for (int i = 1; i < 29; i++)
        {
            
            //DateTime result = startDate + t;
            date.Add(i.ToString());      // Use Date.ToShortTimeString() method to get the desired format                
        }
        return date;
    }
    

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceController service = new ServiceController();

            service.ServiceName = "MDMService";
            //service.MachineName = "IAS-INDIA-PC";
            string svcStatus = service.Status.ToString();
            if (svcStatus == "Stopped")
            {
                service.Start();
                //btn_stop.Enabled = true;
                //btn_start.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lbl_message.Text = "Service error";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceController service = new ServiceController();

            service.ServiceName = "MDMService";
            //service.MachineName = "IAS-INDIA-PC";
            string svcStatus = service.Status.ToString();
            if (svcStatus == "Running")
            {
                service.Stop();
                //btn_stop.Enabled = false;
                //btn_start.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lbl_message.Text = "Service error";
        }
    }
    protected void radcombo_servicename_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radcombo_servicename.Text != "Select")
        {
            DataTable dt_templatename = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceTemplates where serviceId=" + radcombo_servicename.SelectedValue + " and status='Active' and templateId not in(select templateId from daScheduledServices where serviceId=" + radcombo_servicename.SelectedValue + ")").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_temname, dt_templatename, "templateName", "templateId", "0");
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //DataTable dt_uniquetemplate = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daScheduledServices where templateId=" + radcombo_temname.SelectedValue + "").Tables[0];
        //if (dt_uniquetemplate.Rows.Count == 0)
        //{
        string sqlinsert="";
        if (radcombo_dwm.SelectedValue == "Daily")
        {
            sqlinsert = "insert into daScheduledServices(serviceId,templateId,interval,time,status)values(" +
                "" + radcombo_servicename.SelectedValue + "," + radcombo_temname.SelectedValue + ",'" + radcombo_dwm.Text + "','" + radcombo_time.Text + "','" + radcombo_status.Text + "')";

        }
        else if (radcombo_dwm.SelectedValue == "Weekly")
        {
            sqlinsert = "insert into daScheduledServices(serviceId,templateId,interval,time,weekname,status)values(" +
                "" + radcombo_servicename.SelectedValue + "," + radcombo_temname.SelectedValue + ",'" + radcombo_dwm.Text + "','" + radcombo_time.Text + "','" + radcombo_weeknames.Text + "','" + radcombo_status.Text + "')";
        }
        else
        {
            sqlinsert = "insert into daScheduledServices(serviceId,templateId,interval,time,datenumber,Status)values(" +
                "" + radcombo_servicename.SelectedValue + "," + radcombo_temname.SelectedValue + ",'" + radcombo_dwm.Text + "','" + radcombo_time.Text + "'," + radcombo_date.SelectedValue + ",'" + radcombo_status.Text + "')";
        }
        int insertcnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, sqlinsert);
        if (insertcnt == 1)
        {
            lbl_message.Text = "Record saved successfully";
            lbl_message.ForeColor = Color.Green;
        }
        else
        {
            lbl_message.Text = "Error in saving record";
        }
        //}
        //else
        //{
        //    lbl_message.Text = "Template already assigned for this service try another..";
        //}
        gridbind();
    }
}