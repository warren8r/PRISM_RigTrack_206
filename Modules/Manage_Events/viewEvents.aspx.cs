using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections;
using eis = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data;

public partial class Modules_Manage_Events_viewEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        if (!IsPostBack)
        {
            StartDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //DateTime.Today.AddDays(-1);
            EndDate.SelectedDate = DateTime.Today;
        }
        ViewFilterSelection_Click(sender, e);
    }

    protected void RadGrid1_ItemCommand(object obj, GridCommandEventArgs e)
    {

        if (e.CommandName == "Update1")
        {
            //GridDataItem dataItem = (GridDataItem)e.Item;
            if (e.Item is GridNestedViewItem)
            {
                GridNestedViewItem dataItem = e.Item as GridNestedViewItem;
                RadDropDownList list = (RadDropDownList)dataItem.FindControl("ChangeAction");
                TextBox txt = (TextBox)dataItem.FindControl("hidd_id");
                string eventIdItem = txt.Text;
                string actionId = list.SelectedValue.Trim();//error

                MasterTableData.UpdateParameters.Add("userActionId", actionId);
                MasterTableData.UpdateParameters.Add("Eventamiid", eventIdItem);
                MasterTableData.Update();
                //MasterTableData.DataBind();
                ViewFilterSelection_Click(obj, e);
                //Response.Write("? " + actionId);
                //Response.Write("???? " + eventIdItem);
            }
            //else
            //    Response.Write("I didnt update");


        }
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.FileName = "Export";
        }
    }
    protected void EditButton_Click(object sender, EventArgs e)
    {
        Button btnedit = (Button)sender;
        GridNestedViewItem nesteditem = (GridNestedViewItem)btnedit.NamingContainer;
        GridTableView parentGrid = (GridTableView)nesteditem.OwnerTableView;
        GridDataItem item = (GridDataItem)nesteditem.ParentItem;
        item.Edit = true; //make the grid item in edit mode
        RadDropDownList list = (RadDropDownList)item.FindControl("ChangeAction");
        TextBox txt = (TextBox)item.FindControl("hidd_id");
        string eventIdItem = txt.Text;
        string actionId = list.SelectedValue.Trim();//error

        MasterTableData.UpdateParameters.Add("userActionId", actionId);
        MasterTableData.UpdateParameters.Add("Event_Id", eventIdItem);
        MasterTableData.Update();
        //MasterTableData.DataBind();
        parentGrid.Rebind();
    }

    protected void ViewFilterSelection_Click(object sender, EventArgs e)
    {
        //string categorySelected = (string)CategoryList.SelectedValue;

        //List<string> wheres = new List<string>();

        ////string query = "SELECT userAction.actionName, CAST(CASE WHEN containsTaskOrder = 'True' THEN 'Yes' ELSE 'No' END AS varchar) AS containsTaskOrder, category.id AS catId, category.categoryName, primaryLatLong, events.eventName, eventAMI.id, TimeStamp, DiscoveredAt, Source, EndTime, eventAMI.State, Ongoing, Phase, Counter, eventAMI.EventCode, AlarmTrigger, EventInfo, Event_Id, EventData_Id, eventAMI_Id, ElsterMeter_Id, ElsterMeterSerialNumber, " +
        ////               "userAssignedId, userAssignedTimestamp, userCompletedTimestamp, events.eventName, flag.flagName, Users.firstName + ' ' + Users.lastName AS fullName, meter.manufacturer, meter.meterIRN, meter.primaryLatLong " +
        ////               "FROM eventAMI " +
        ////               "LEFT JOIN events ON eventAMI.EventCode = events.eventCode " +
        ////               "LEFT JOIN flag ON events.flagId = flag.id " +
        ////               "LEFT JOIN Users ON Users.userID = eventAMI.userAssignedId " +
        ////               "LEFT JOIN eventCategory ON eventCategory.eventId = events.id " +
        ////               "LEFT JOIN category ON eventCategory.categoryId = category.id " +
        ////               "LEFT JOIN userAction ON userAction.id = eventAMI.userActionId " + 
        ////               "RIGHT JOIN meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber";

        //string query = "SELECT  userAction.actionName,events.id as eventcodeid,eventAMI.id as eventamiid, CAST(CASE WHEN containsTaskOrder = 'True' THEN 'Yes' ELSE 'No' END AS varchar) AS containsTaskOrder, category.id AS catId, category.categoryName, events.eventName, eventAMI.id, TimeStamp, DiscoveredAt, Source, EndTime, eventAMI.State, Ongoing, Phase, Counter, eventAMI.EventCode, AlarmTrigger, EventInfo, Event_Id, EventData_Id, eventAMI_Id, ElsterMeter_Id, ElsterMeterSerialNumber, " +
        //               "userAssignedId, userAssignedTimestamp, userCompletedTimestamp, events.eventName, flag.flagName, Users.firstName + ' ' + Users.lastName AS fullName, meter.manufacturer, meter.meterIRN, meter.primaryLatLong " +
        //               "FROM eventAMI " +
        //               "INNER JOIN events ON eventAMI.EventCode = events.eventCode " +
        //               "INNER JOIN flag ON events.flagId = flag.id " +
        //               "LEFT JOIN Users ON Users.userID = eventAMI.userAssignedId " +
        //               "INNER JOIN eventCategory ON eventCategory.eventId = events.id " +
        //               "INNER JOIN category ON eventCategory.categoryId = category.id " +
        //               "LEFT JOIN userAction ON userAction.id = eventAMI.userActionId " +
        //               "LEFT JOIN meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber";
        //               //"LEFT JOIN meter ON eventAMI.ElsterMeter_ID = meter.meterIRN";

        //if (!String.IsNullOrEmpty(categorySelected) && categorySelected != "-1")
        //{
        //    //query += " category.id = @categoryId";
        //    //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
        //    wheres.Add("category.id = '" + CategoryList.SelectedValue + "'");
        //}
        //if (!String.IsNullOrEmpty(EventList.SelectedValue) && EventList.SelectedValue != "-1")
        //{
        //    //query += " category.id = @categoryId";
        //    //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
        //    //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";
        //    wheres.Add("events.id = '" + EventList.SelectedValue + "'");
        //}
        //if (!String.IsNullOrEmpty(UserList.SelectedValue) && UserList.SelectedValue != "-1")
        //{
        //    //query += " category.id = @categoryId";
        //    //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
        //    //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";

        //    //wheres.Add("Users.userID = '" + UserList.SelectedValue + "'");
        //    wheres.Add("events.id IN (select eventId FROM eventUser where userId='" + UserList.SelectedValue + "')");
        //}
        //if (!String.IsNullOrEmpty(ActionList.SelectedValue) && ActionList.SelectedValue != "-1")
        //{
        //    //query += " category.id = @categoryId";
        //    //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
        //    //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";
        //    wheres.Add("userAction.id = '" + ActionList.SelectedValue + "'");
        //}

        //if (!String.IsNullOrEmpty(FlagList.SelectedValue) && FlagList.SelectedValue != "-1")
        //{
        //    //query += " category.id = @categoryId";
        //    //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
        //    //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";
        //    wheres.Add("flag.id = '" + FlagList.SelectedValue + "'");
        //}

        //if (!String.IsNullOrEmpty(StartDate.SelectedDate.ToString()) || !String.IsNullOrEmpty(EndDate.SelectedDate.ToString()))
        //{
        //    List<string> dates = new List<string>();

        //    if (!String.IsNullOrEmpty(StartDate.SelectedDate.ToString()))
        //    {
        //        //query += " category.id = @categoryId";
        //        //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
        //        //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";
        //        dates.Add("TimeStamp >= '" + StartDate.SelectedDate.ToString() + "'");
        //    }
        //    if (!String.IsNullOrEmpty(EndDate.SelectedDate.ToString()))
        //    {
        //        //query += " category.id = @categoryId";
        //        //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
        //        //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";
        //        dates.Add("TimeStamp <= '" + EndDate.SelectedDate.ToString() + "'");
        //    }

        //    if (!String.IsNullOrEmpty(dates[0]))
        //        wheres.Add(" (" + String.Join(" AND ", dates.Select(x => x.ToString()).ToArray()) + ")");
        //}
        //try
        //{
        //    if (!String.IsNullOrEmpty(wheres[0]))
        //        query += " WHERE " + String.Join(" AND ", wheres.Select(x => x.ToString()).ToArray());
        //    //Response.Write(query);
        //    //MasterTableData.SelectCommand = query;
        //    query += "  order by eventAMI.id desc";
        try
        {
            string fromdate = String.Format("{0:MM/dd/yyyy}", StartDate.SelectedDate);
            string todate = String.Format("{0:MM/dd/yyyy}", EndDate.SelectedDate);
            string query = "SELECT * FROM events ev,PrismEvent pev where ev.eventCode=pev.eventCode";// and eventTime >='" + fromdate + "' and " +
           //     " eventTime <='" + todate + "'";
            if(combo_Event.SelectedIndex>0)
            {
                query += " and id=" + combo_Event.SelectedValue;
            }
            
            query+=" order by eventTime desc";
            DataTable tmpTable = new DataTable();
            SqlConnection sqlcon = new SqlConnection(Session["client_database"].ToString());
            sqlcon.Open();
            SqlDataAdapter sqladapter = new SqlDataAdapter(query, sqlcon);
            sqladapter.Fill(tmpTable);
            sqlcon.Close();
            RadGrid1.DataSourceID = null;
            RadGrid1.DataSource = tmpTable;
            RadGrid1.DataBind();
        }
        catch (Exception ex)
        {
            //nothing
        }
    }
    protected void PopUpGISData_Clicked(object sender, EventArgs e)
    {
        GridDataItem gdi = (GridDataItem)((RadButton)sender).NamingContainer;
        Label tmplbl = gdi.FindControl("hidd_id") as Label;

        //ResetFilterBtn.Text = gdi["id"].Text;
        SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand("SELECT [primaryLatLong] FROM meter WHERE [meterIRN]=@meterIRN", sqlConn);
        sqlcmd.Parameters.AddWithValue("@meterIRN", tmplbl.Text);
        sqlConn.Open();
        String latlon = sqlcmd.ExecuteScalar() as String;
        sqlConn.Close();

        //if (String.IsNullOrEmpty(latlon))
        //{
        //    gMap.Style.Add("display", "none");
        //    lbl_message.Text = "No location data for this Event.";
        //}
        //else
        //{
        //    gMap.Style.Add("display", "block");
        //    GoogleMap1.Address = latlon;
        //    GoogleMap1.DataBind();
        //}
    }

    protected void RadGrid1_BiffExporting(object sender, GridBiffExportingEventArgs e)
    {
        eis.Table newWorksheet = new eis.Table("My New Worksheet");
        
        //eis.Cell headerCell = newWorksheet.Cells[1, 1];
        //headerCell.Value = "Test";
        //headerCell.Style.BorderBottomColor = System.Drawing.Color.Black;
        //headerCell.Style.BorderBottomStyle = BorderStyle.Double;
        //headerCell.Style.Font.Bold = true;
        //headerCell.Colspan = 3;

        e.ExportStructure.Tables.Add(newWorksheet);
    }

    protected void ResetFilter(object sender, EventArgs e)
    {
        //StartDate.SelectedDate = DateTime.Today.AddDays(-1);
        StartDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //DateTime.Today.AddDays(-1);
        EndDate.SelectedDate = DateTime.Today;
        //EventList.Items.Clear();
        //EventList.Items.Add(new DropDownListItem("- Select Event -", "-1"));

        //UserList.Items.Clear();
        //UserList.Items.Add(new DropDownListItem("- Select User -", "-1"));
        //CategoryList.SelectedIndex = -1;
        //EventList.SelectedIndex = -1;
      //  UserList.SelectedIndex = -1;
        ActionList.SelectedIndex = -1;
        //FlagList.SelectedIndex = -1;
        //RadAjaxPanel1.RaisePostBackEvent("server");
        //RadAjaxPanel1.DataBind();

    }
    protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        ViewFilterSelection_Click(sender, e);
        //RadGrid1.Rebind();
    }

    protected void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //EventList.Items.Clear();
        //EventList.Items.Add(new DropDownListItem("- Select Event -", "-1"));
        //EventList.DataBind();
        //EventList.SelectedIndex = 0;

        //UserList.Items.Clear();
        //UserList.Items.Add(new DropDownListItem("- Select User -", "-1"));
        //UserList.DataBind();
        //UserList.SelectedIndex = 0;
    }

    protected void EventList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //UserList.Items.Clear();
        //UserList.Items.Add(new DropDownListItem("-Select User-", "-1"));
        //UserList.DataBind();
        //UserList.SelectedIndex = 0;
    }

    protected void UserList_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void EndDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
    }
}
