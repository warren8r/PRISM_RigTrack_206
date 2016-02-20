using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
public partial class Modules_Configuration_Manager_InsertProjectDetails : System.Web.UI.Page
{

    public static DataTable dt_Table = new DataTable();
    DataTable dt_roles = new DataTable();
    public static DataTable dt_rolepermission;
    public static DataTable dt_modules = new DataTable();
    public static DataTable dt_rolenotification = new DataTable();
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction transaction;
    string queryInsert = "", queryvalues = "";
    ClientMaster userMaster = new ClientMaster();
    //string[] arr_fields = new[] {"jobname", "status", "JobRef", "SubJob", "JobType", "RoadClass", "ScheduleItem", "AssetNo.",
    //    "OrderReference", "SuperiorOrder", "FLMCode", "ReceivedDate", "Address","PostCode", "Material(Original)", "Qty(Original)",
    //    "Length(Original)", "Width(Original)", "Depth(Original)", "Pub/Priv(Original)", "Position(Original)", "Int/Perm", "ECVDate",
    //    "NoticeStart", "Notice End", "Contractor", "Location", "Material(Actual)", "Qty(Actual)", "Length(Actual)", "Width(Actual)",
    //    "Depth(Actual)", "Pub/Priv(Actual)", "Position(Actual)", "Int/Perm(Actual)", "PlannedDate", "Team","CompletionDate", "Comments", "Notes"};
    string[] arr_fields = new[] {"jobname","Material (Original)", "Material (Actual)", "Qty (Original)", "Qty (Actual)",
        "Length (Original)", "Length (Actual)", "Width (Original)", "Width (Actual)", "Depth (Original)","Depth (Actual)", "Pub/Priv (Original)", "Pub/Priv (Actual)",
        "Position (Original)", "Position (Actual)", "Int/Perm","Int/Perm (Actual)", "ECV Date","Notice Start", "Notice End", "Contractor", "Location",
          "status", "Job Ref", "SubJob", "Job Type", "Road Class", "Schedule Item", "Asset No.",
        "Order Reference", "Superior Order", "FLM Code", "Received Date", "Address","Post Code",  "Planned Date", "Team","Completion Date", "Comments", "Notes"};
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            userMaster = (ClientMaster)Session["UserMasterDetails"];
        }
        catch (Exception ex)
        {
            userMaster = null;
        }

        if (userMaster == null)
        {
            Response.Redirect("~/ClientLogin.aspx");
        }
        else
        {
            this.Session["userRoleID"] = userMaster.UserTypeID;
        }
        //SQLDataSourceUserAccess.ConnectionString = GlobalConnetionString.ClientConnection();
        SqlDataSource1.ConnectionString = GlobalConnetionString.ClientConnection();
        //radtxt_rolename.Enabled = true;

        dt_rolepermission = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from UserRoles ur  left outer join UserTypePermissions up " +
                " on  ur.userRoleID=up.userRoleID left outer join Modules m on up.moduleID=m.moduleID WHERE m.parentId = '0' order by ur.userRoleID ,m.moduleID").Tables[0];
        dt_rolenotification = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_UserRole_Notification").Tables[0];
        if (!IsPostBack)
        {

            dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from manageJobOrders where datasource='IMPORT' order by jid").Tables[0];
        }
        //dt_subscription = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text,
        //                       "select * from ClientSubscription where clientID='" + Session["clientID"].ToString() + "'").Tables[0];

        //}
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        //    //GridBoundColumn boundcolumn1 = new GridBoundColumn();
        //    //boundcolumn1.HeaderText = "UserType";
        //    //boundcolumn1.DataField = "userRole";
        //    ////boundcolumn1.Visible = false;
        //    //boundcolumn1.ReadOnly = true;
        //    //grid_project.MasterTableView.Columns.Add(boundcolumn1);

        //    dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //        "select * from UserRoles ur right outer join Modules m on ur.userRoleID=m.moduleID WHERE m.parentId = '0' order by m.moduleID").Tables[0];
        //    //for (int column = 0; column < dt_Table.Rows.Count; column++)
        GridHyperLinkColumn mapcolumn = new GridHyperLinkColumn();
        mapcolumn.HeaderText = "View&#160;Map";
        mapcolumn.DataTextField = "primaryLatLong";
        grid_project.MasterTableView.Columns.AddAt(0,mapcolumn);
        for (int column = 0; column < arr_fields.Length; column++)
        {
            GridTemplateColumn tmpColumn = new GridTemplateColumn();
            grid_project.MasterTableView.Columns.AddAt(column+1, tmpColumn); //Add column before setting properties
            tmpColumn.HeaderText = arr_fields[column].ToString();
            tmpColumn.UniqueName = column.ToString();
            // tmpColumn.DataField = datafield;
            string id = column.ToString() + "_" + arr_fields[column].ToString();
            switch (arr_fields[column].ToString())
            {
                //case "Notes":
                //case "Comments":
                //    {
                //        tmpColumn.ItemTemplate = new TextBoxItemTemplate(id,TextBoxMode.MultiLine);
                //        break;
                //    }
                //case "Material (Original)":
                case "Material (Actual)":
                //case  "Qty (Original)":
                case "Qty (Actual)":
                // case "Length (Original)":
                case "Length (Actual)":
                // case "Width (Original)":
                case "Width (Actual)":
                // case "Depth (Original)":
                case "Depth (Actual)":
                // case  "Pub/Priv (Original)":
                case "Pub/Priv (Actual)":
                // case "Position (Original)":
                case "Position (Actual)":
                    {
                        tmpColumn.ItemTemplate = new TextBoxItemTemplate(id, TextBoxMode.SingleLine);
                        break;
                    }
                default:
                    {
                        tmpColumn.ItemTemplate = new TextBoxItemTemplate(id, TextBoxMode.SingleLine);
                        break;
                    }
            }

        }
        //grid_project.MasterTableView.Columns.Add(new  GridEditCommandColumn());

    }
    public class TextBoxItemTemplate : ITemplate
    {
        public string TextBoxID;
        public TextBox _textBox;
        public TextBoxMode txtmode;

        public TextBoxItemTemplate(string textBoxID, TextBoxMode mode)
        {
            this.TextBoxID = textBoxID;
            txtmode = mode;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            this._textBox = new TextBox();
            this._textBox.ID = this.TextBoxID;
            this._textBox.TextMode = this.txtmode;
            // _textBox.Text = this.TextBoxID;
            //this._textBox.AutoPostBack = false;
            container.Controls.Add(this._textBox);
        }
    }
    protected void grid_project_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            string datakey = dataItem.GetDataKeyValue("jid").ToString();
            DataRow[] row_job = dt_Table.Select("jid=" + datakey + "");


            string val1 = dataItem["primaryLatLong"].Text;
            HyperLink hLink = (HyperLink)dataItem["primaryLatLong"].Controls[0];
            var requestUrl = string.Format("http://maps.google.com/?q={0}", hLink.Text);

            hLink.NavigateUrl = requestUrl;// "http://maps.google.com/?q=" + hLink.Text;// "VolunteerEdit.aspx?Id=" + val1 + "&type=edit";  
            hLink.Text = "View";
             //DataNavigateUrlFields="primaryLatLong" 
             //                            DataNavigateUrlFormatString="http://maps.google.com/?q={0}" 
             //                            FilterControlAltText="Filter gisLink column" Target="_new" Text="View Map" 
             //                            UniqueName="gisLink">
            for (int column = 0; column < arr_fields.Length; column++)
            {

                //TextBox txt_job = (TextBox)dataItem.FindControl(column.ToString() + "_" + arr_fields[column].ToString());
                switch (arr_fields[column].ToString())
                {
                    case "startdate":
                    case "enddate":
                    case "Received Date":
                        {

                            //  txt_job.Text = DatesbetweenDatatable.getdatetimeformatMMDDYYYY(row_job[0][arr_fields[column].ToString()].ToString());
                            Label lbl_job = (Label)dataItem.FindControl(column.ToString() + "_" + arr_fields[column].ToString());
                            lbl_job.Text = DatesbetweenDatatable.getdatetimeformat(row_job[0][arr_fields[column].ToString()].ToString());
                            break;
                        }
                    //case "Material (Original)":
                    case "Material (Actual)":
                    //case  "Qty (Original)":
                    case "Qty (Actual)":
                    // case "Length (Original)":
                    case "Length (Actual)":
                    // case "Width (Original)":
                    case "Width (Actual)":
                    // case "Depth (Original)":
                    case "Depth (Actual)":
                    // case  "Pub/Priv (Original)":
                    case "Pub/Priv (Actual)":
                    // case "Position (Original)":
                    case "Position (Actual)":
                        {
                            TextBox txt_job = (TextBox)dataItem.FindControl(column.ToString() + "_" + arr_fields[column].ToString());
                            txt_job.Text = row_job[0][arr_fields[column].ToString()].ToString();
                            break;

                        }
                    case "Notes":
                    case "Comments":
                        {
                            Label lbl_job = (Label)dataItem.FindControl(column.ToString() + "_" + arr_fields[column].ToString());
                            lbl_job.Text = row_job[0][arr_fields[column].ToString()].ToString();
                            break;
                        }
                    default:
                        {
                            Label lbl_job = (Label)dataItem.FindControl(column.ToString() + "_" + arr_fields[column].ToString());
                            lbl_job.Text = row_job[0][arr_fields[column].ToString()].ToString();
                            break;
                        }

                }

            }

        }
    }
    protected void grid_project_ItemCommand(object sender, GridCommandEventArgs e)
    {
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        db.Open();
        transaction = db.BeginTransaction();
        foreach (GridDataItem item in grid_project.Items)
        {
            queryInsert = "Insert into manageJobOrders (jobname,status,[Job Ref],SubJob,[Job Type],[Road Class],[Schedule Item],[Asset No.],[Order Reference]," +
                                          "[Superior Order],[FLM Code],[Received Date],Address,[Post Code],[Material (Original)],[Qty (Original)],[Length (Original)],[Width (Original)]," +
                                          "[Depth (Original)],[Pub/Priv (Original)],[Position (Original)],[Int/Perm],[ECV Date],[Notice Start],[Notice End],Contractor,Location,[Material (Actual)]," +
                                          "[Qty (Actual)],[Length (Actual)],[Width (Actual)],[Depth (Actual)],[Pub/Priv (Actual)],[Position (Actual)],[Int/Perm (Actual)],[Planned Date],Team," +
                                          "[Completion Date],Comments,Notes) values (";

            for (int column = 0; column < arr_fields.Length; column++)
            {
                TextBox txtproject = (TextBox)item.FindControl(column.ToString() + "_" + arr_fields[column].ToString());
                string ss = txtproject.Text;
                queryvalues += txtproject.Text.TrimEnd().TrimStart() + "',";
            }
            queryInsert += queryvalues.Remove(queryvalues.Length - 1, 1) + ")";
            queryInsert = "";
            queryvalues = "";
            try
            {
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryInsert);

            }
            catch (Exception ex)
            {
            }

        }
        transaction.Commit();
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {

    }
    //protected void btn_view_Click(object sender, EventArgs e)
    //{
    //    string query = "select * from manageJobOrders where datasource='IMPORT'";

    //    query += "order by jid";
    //    SqlDataSource1.SelectCommand = query;
    //}

}