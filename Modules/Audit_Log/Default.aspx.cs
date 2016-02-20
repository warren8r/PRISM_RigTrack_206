using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Audit_Log_Default : System.Web.UI.Page
{
    protected string select = "";
    protected string orderBy = " ORDER BY logged DESC ";
    protected string whereSuperseded = " WHERE superseded = '0' or superseded is null ";


    protected void Page_Load(object sender, EventArgs e)
    {

        select = logData.SelectCommand;
        if (!IsPostBack)
        {
            
            auditLog log = new auditLog(Session["client_database"].ToString());
            DataTable td = log.getLog();

            logData.SelectCommand += whereSuperseded + orderBy;

            //YESTERDAY.. 
            //DateTime.Today.AddDays(-1);
            StartDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); 
            EndDate.SelectedDate = DateTime.Today.AddDays(1);

            gridTask.DataSourceID = null;
            gridTask.DataSource = td.Select("output = 'true'");
            gridTask.DataBind();
            //filter(sender, e);
        }


        int selectedValue = UsernameList.SelectedIndex;
        UsernameList.Items.Clear();
        UsernameList.Items.Add( new DropDownListItem( "- Select User -", "-1" ));
        UsernameList.DataBind();
        UsernameList.SelectedIndex = selectedValue;
    }

    protected void filter(object sender, EventArgs e)
    {
        string query = select; 
        List<string> wheres = new List<string>();
        string query_where = "";

        DataTable tmpTable = new DataTable();
        SqlConnection sqlcon = new SqlConnection(Session["client_database"].ToString());
        sqlcon.Open();

        //CHECK FOR USERNAME
        if (!String.IsNullOrWhiteSpace(UsernameList.SelectedValue) && UsernameList.SelectedValue != "-1")
        {
            wheres.Add("transactionLog.userId = " + UsernameList.SelectedValue + "");
        }
        if (!String.IsNullOrWhiteSpace(moduleSort.SelectedValue) && moduleSort.SelectedValue != "-1")
        {
            wheres.Add("Modules.parentId= '" + moduleSort.SelectedValue + "'");
        }   

        //CHECK DATE RANGE
        if (!String.IsNullOrEmpty(StartDate.SelectedDate.ToString()) || !String.IsNullOrEmpty(EndDate.SelectedDate.ToString()))
        {
            List<string> dates = new List<string>();

            if (!String.IsNullOrEmpty(StartDate.SelectedDate.ToString() ))
                dates.Add("transactionLog.created >= '" + StartDate.SelectedDate.ToString() + "'");

            if (!String.IsNullOrEmpty(EndDate.SelectedDate.ToString() ))
                dates.Add("transactionLog.created <= '" + EndDate.SelectedDate.ToString() + "'");

            if (!String.IsNullOrEmpty(dates[0]))
                wheres.Add(" (" + String.Join(" AND ", dates) + ") ");
        }   

        if (wheres.Count > 0)
            query_where = " AND " + String.Join(" AND ", wheres);

        //GET THE DATA
        SqlDataAdapter sqladapter = new SqlDataAdapter(query + " " + whereSuperseded + query_where + orderBy, sqlcon);
        sqladapter.Fill(tmpTable);
        sqlcon.Close();

        //SET THE DATSOURCE TO THE NEW DATATABLE AND REBIND.
        gridTask.DataSourceID = null;
        gridTask.DataSource = tmpTable;
        ajaxGrid.DataBind();
    }

    //RESET THE FILTER INPUTS.
    protected void ResetFilter(object sender, EventArgs e)
    {
        StartDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        EndDate.SelectedDate = DateTime.Today.AddDays(1);

        UsernameList.Items.Clear();
        UsernameList.Items.Add(new DropDownListItem("- Select User -", "-1"));
        UsernameList.DataBind();
        UsernameList.SelectedIndex = 0;
    }

    //INSTEAD OF CALL BACK USE THIS FUNCTION.
    protected void SelectedIndexChanged(object sender, EventArgs e)
    {
        filter(sender, e);

        int selectedValue = UsernameList.SelectedIndex;
        UsernameList.Items.Clear();
        UsernameList.Items.Add(new DropDownListItem("- Select User -", "-1"));
        UsernameList.DataBind();
        UsernameList.SelectedIndex = selectedValue;

        selectedValue = moduleSort.SelectedIndex;
        moduleSort.Items.Clear();
        moduleSort.Items.Add(new DropDownListItem("- Select Module -", "-1"));
        moduleSort.DataBind();
        moduleSort.SelectedIndex = selectedValue;
    }

    protected DataTable buildResponse(DataTable values, string id, DataTable results)
    {
        DataRow insertedRow = results.NewRow();
        foreach (DataRow dr in values.Rows)
        {
            if (dr["id"] as String == id)
            {
                for (int i = 0; i < results.Columns.Count; i++)
                {
                    insertedRow[i] = dr[i];
                }
                results.Rows.Add(insertedRow);
            }
        }

        if (!String.IsNullOrEmpty(insertedRow[0] as String))
        {
            results = buildResponse(values, insertedRow["superseded"].ToString(), results);
        }

        return results;
    }

    protected void gridTask_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "ExpandCollapse")
        {
            auditLog log = new auditLog(Session["client_database"].ToString());
            var td = log.getLog();

            //GET CURRENT ROW.
            GridDataItem currentRow = e.Item as GridDataItem;
            string id = ((Label)currentRow.FindControl("labelID")).Text;

            DataTable td2 = new DataTable();
            foreach (DataColumn column in td.Columns)
                td2.Columns.Add(column.ColumnName);

            DataTable test = buildResponse(td, id, td2);

            RadGrid historyBox = (RadGrid)currentRow.ChildItem.FindControl("history");

            historyBox.DataSourceID = "";
            historyBox.DataSource = test;// td.Select("id = '" + id + "'");
            historyBox.DataBind();
        }
    }
}