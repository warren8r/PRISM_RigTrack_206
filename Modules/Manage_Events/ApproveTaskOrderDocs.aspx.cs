using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using eis = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI.GridExcelBuilder;

public partial class Modules_Manage_Events_ApproveTaskOrderDocs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StartDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //DateTime.Today.AddDays(-1);
            EndDate.SelectedDate = DateTime.Today;

            //ViewFilterSelection_Click(sender, e);
            //return;
        }
    }
    
    protected void RadGrid1_ItemCommand(object obj, GridCommandEventArgs e)
    {

        if (e.CommandName == "Update")
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
                MasterTableData.UpdateParameters.Add("Event_Id", eventIdItem);
                MasterTableData.Update();
                MasterTableData.DataBind();

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
        //if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName)
        //    RadGrid1.MasterTableView.ExportToExcel();

    }


    protected void ViewFilterSelection_Click(object sender, EventArgs e)
    {
        //Hashtable dataBind = new Hashtable();
        List<string> wheres = new List<string>();

        //string query = "SELECT distinct userAction.actionName, CAST(CASE WHEN containsTaskOrder = 'True' THEN 'Yes' ELSE 'No' END AS varchar) AS containsTaskOrder, category.id AS catId, category.categoryName, primaryLatLong, events.eventName, eventAMI.id, eventAMI.TimeStamp, DiscoveredAt, Source, EndTime, eventAMI.State, Ongoing, Phase, Counter, eventAMI.EventCode, AlarmTrigger, EventInfo, Event_Id, EventData_Id, eventAMI_Id, ElsterMeter_Id, ElsterMeterSerialNumber, " +
        //               "userAssignedId, userAssignedTimestamp, userCompletedTimestamp, events.eventName, flag.flagName, Users.firstName + ' ' + Users.lastName AS fullName, meter.manufacturer, meter.meterIRN, meter.primaryLatLong " +
        //               "FROM eventAMI " +
        //               "LEFT JOIN events ON eventAMI.EventCode = events.eventCode " +
        //               "LEFT JOIN flag ON events.flagId = flag.id " +
        //               "LEFT JOIN Users ON Users.userID = eventAMI.userAssignedId " +
        //               "LEFT JOIN eventCategory ON eventCategory.eventId = events.id " +
        //               "LEFT JOIN category ON eventCategory.categoryId = category.id " +
        //               "LEFT JOIN userAction ON userAction.id = eventAMI.userActionId " +
        //               "LEFT JOIN meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber";

        string query = "SELECT  userAction.actionName, events.id as eventcodeid,eventAMI.id as eventamiid, CAST(CASE WHEN containsTaskOrder = 'True' THEN 'Yes' ELSE 'No' END AS varchar) AS containsTaskOrder, category.id AS catId, category.categoryName, events.eventName, eventAMI.id, TimeStamp, DiscoveredAt, Source, EndTime, eventAMI.State, Ongoing, Phase, Counter, eventAMI.EventCode, AlarmTrigger, EventInfo, Event_Id, EventData_Id, eventAMI_Id, ElsterMeter_Id, ElsterMeterSerialNumber, " +
                       "userAssignedId, userAssignedTimestamp, userCompletedTimestamp, events.eventName, flag.flagName, Users.firstName + ' ' + Users.lastName AS fullName, meter.manufacturer, meter.meterIRN, meter.primaryLatLong " +
                       "FROM eventAMI " +
                       "INNER JOIN events ON eventAMI.EventCode = events.eventCode " +
                       "INNER JOIN flag ON events.flagId = flag.id " +
                       "LEFT JOIN Users ON Users.userID = eventAMI.userAssignedId " +
                       "INNER JOIN eventCategory ON eventCategory.eventId = events.id " +
                       "INNER JOIN category ON eventCategory.categoryId = category.id " +
                       "LEFT JOIN userAction ON userAction.id = eventAMI.userActionId " +
                       "LEFT JOIN meter ON eventAMI.ElsterMeterSerialNumber = meter.serialNumber";

        wheres.Add("events.eventCode IS NOT NULL");

        if (!String.IsNullOrEmpty(ActionList.SelectedValue))
        {
            wheres.Add("userAction.id = '" + ActionList.SelectedValue + "'");
            //dataBind.Add("userActionid", ActionList.SelectedValue);
        }
        if (!String.IsNullOrEmpty(FlagList.SelectedValue))
        {
            wheres.Add("flag.id = '" + FlagList.SelectedValue + "'");
            //dataBind.Add("FlagList", FlagList.SelectedValue);
        }

        if (!String.IsNullOrEmpty(StartDate.SelectedDate.ToString()) || !String.IsNullOrEmpty(EndDate.SelectedDate.ToString()))
        {
            List<string> dates = new List<string>();

            if (!String.IsNullOrEmpty(StartDate.SelectedDate.ToString()))
            {
                //query += " category.id = @categoryId";
                //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
                //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";
                dates.Add("TimeStamp >= Convert(datetime, '" + StartDate.SelectedDate.ToString() + "' )");
                //dataBind.Add("StartDate", StartDate.SelectedDate.ToString());
            }
            if (!String.IsNullOrEmpty(EndDate.SelectedDate.ToString()))
            {
                //query += " category.id = @categoryId";
                //dataBind.Add("@categoryId", CategoryFilter.SelectedValue);
                //query += " AND category.id = '" + CategoryFilter.SelectedValue + "'";
                dates.Add("TimeStamp <= Convert(datetime, '" + EndDate.SelectedDate.ToString() + "' )");
                //dataBind.Add("EndDate", EndDate.SelectedDate.ToString());
            }
            if (!String.IsNullOrEmpty(dates[0]))
                wheres.Add(" (" + String.Join(" AND ", dates.Select(x => x.ToString()).ToArray()) + ")");
        }
        try
        {
            if (!String.IsNullOrEmpty(wheres[0]))
                query += " WHERE " + String.Join(" AND ", wheres.Select(x => x.ToString()).ToArray());
            //Response.Write(query);
            //MasterTableData.SelectCommand = query;
            query += "  order by eventAMI.id desc";
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

    protected void UploadCompletedTaskOrder_Click(object sender, EventArgs e)
    {

        //RadUpload.TaskOrderCompletedUpload.Visible = true;

    }

    protected void ResetFilter1(object sender, EventArgs e)
    {
        RadGrid1.DataSource = null;
        RadGrid1.DataSourceID = "MasterTableData";
        RadGrid1.DataBind();

        StartDate.SelectedDate = DateTime.Today.AddDays(-1);
        EndDate.SelectedDate = DateTime.Today;
        ActionList.SelectedIndex = -1;
        FlagList.SelectedIndex = -1;
        
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

    protected void UploadCompletedTaskOrder(object sender, EventArgs e)
    {
        //upload template, if necessary
        //if (TaskOrderCompletedUpload.HasFile)
        //{
        //    TaskOrderCompletedUpload.PostedFile.SaveAs(MapPath("~/App_ClientData/TaskOrders/OrdersCompleted/" + TaskOrderCompletedUpload.FileName + DateTime.Now));
        //}
    }


    protected void UploadCompletedTaskOrder_clicked(object sender, EventArgs e)
    {
        //if (TaskOrderCompletedUpload.Visible == false)
        //{
        //    TaskOrderCompletedUpload.Visible = true;
        //}
        //else
        //{
        //    TaskOrderCompletedUpload.Visible = false;
        //}
    }
    protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        //ViewFilterSelection_Click(sender, e);
        RadGrid1.Rebind();
    }
}