using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ManageBHAItems : System.Web.UI.Page
{
    MDM.Collector col = new MDM.Collector();
    public string DBConnectionString
    {
        get { return Session["client_database"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            Session["selectedID"] = null;

        }
    }
    protected void radgrdMeterList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        RememberSelected();
    }

    private void RememberSelected()
    {
        string strIds = ",";

        if (Session["selectedID"] != null)
        {
            strIds = Convert.ToString(Session["selectedID"]);
        }

        foreach (GridDataItem item in radgrdMeterList.MasterTableView.Items)
        {
            CheckBox checkColumn = item.FindControl("chkActive") as CheckBox;
            int Id = Convert.ToInt32(item.GetDataKeyValue("ID").ToString());

            if (checkColumn != null && checkColumn.Checked)
            {
                strIds += Id.ToString() + ",";
            }
            else
            {
                strIds = strIds.Replace("," + Id.ToString() + ",", ",");
            }
        }

        Session["selectedID"] = strIds;
    }

    protected void radgrdMeterList_PreRender(object sender, EventArgs e)
    {
        string strIds = ",";

        if (Session["selectedID"] != null)
        {
            strIds = Convert.ToString(Session["selectedID"]);
        }

        foreach (GridDataItem item in radgrdMeterList.MasterTableView.Items)
        {
            CheckBox checkColumn = item.FindControl("chkActive") as CheckBox;
            int Id = Convert.ToInt32(item.GetDataKeyValue("ID").ToString());

            if (checkColumn != null)
            {
                if (checkColumn.Checked)
                    checkColumn.Checked = true;
                else
                {
                    if (strIds.IndexOf("," + Id.ToString() + ",") >= 0)
                    {
                        checkColumn.Checked = true;
                    }
                    else
                    {
                        checkColumn.Checked = false;
                    }
                }
            }
        }
    }
    protected void radgrdMeterList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        radgrdMeterList.DataSource= RefreshMeterList();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            combo_job.Items.Clear();
            combo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            combo_job.DataSource = dtJobDetails;
            combo_job.DataTextField = "CurveGroupName";
            combo_job.DataValueField = "ID";
            combo_job.DataBind();
            //string ids = "";
            //for (int i = 0; i < dtJobDetails.Rows.Count; i++)
            //{
            //    ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
            //}
            //if (ids != "")
            //{
            //    ids = ids.Remove(ids.Length - 1, 1);
            //    string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            //" RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            //" where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and JO.ID in(" + ids + ") order by JO.CreateDate desc";
            //    DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            //    grdJobList.DataSource = dtJobs;
            //    grdJobList.DataBind();
            //}
        }
        else
        {


        }

    }
    protected void combo_job_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (combo_job.SelectedValue != "0")
        {
            radBHANumber.Items.Clear();
            radBHANumber.Items.Add(new RadComboBoxItem("-Select-", "0"));
            string query = "select ID, (BHANumber+'-'+BHADesc) as BHANumber from [RigTrack].[tblBHADataInfo] where JOBID=" + combo_job.SelectedValue + " order by CreateDate desc";
            DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            radBHANumber.DataSource = dtJobs;
           radBHANumber.DataBind();
            
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (radBHANumber.SelectedValue != "0")
        {
            //string query = "select p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
            // "Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.PinTop,p.PinBottom,p.Comments,p.Description from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
            //  " where p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID and P.AssetName=PPA.Id";
            //if (ddl_warehouse.SelectedValue != "")
            //{
            //    query += " and c.ID=" + ddl_warehouse.SelectedValue + "";
            //}
            //query += " order by p.Id desc";
            //DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            //radgrdMeterList.DataSource = dt;
            radgrdMeterList.DataSource = RefreshMeterList();
            radgrdMeterList.DataBind();
            DataTable dt_bhaItemsInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from [RigTrack].[tblBHADataItemsInfo] where BHAID=" + radBHANumber.SelectedValue + "").Tables[0];
            foreach (GridItem item in radgrdMeterList.MasterTableView.Items)
            {
                int dataKeyValue = col.IntUtilParse(((GridDataItem)(item)).GetDataKeyValue("ID").ToString());
                DataRow[] dr_det = dt_bhaItemsInfo.Select("ToolID=" + dataKeyValue + "");
                CheckBox chkActive = (CheckBox)item.FindControl("chkActive");
                if (dr_det.Length > 0)
                {
                    chkActive.Checked = true;
                }
            }
        }

    }
    private DataTable RefreshMeterList()
    {
        // Instantiate a DataBase connection
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // Instantiate a DataTable to hold the resultset
        DataTable dt = new DataTable();

        try
        {
            // Open the database connection
            sqlConn.Open();
            //string query = "select p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
            // "Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.PinTop,p.PinBottom,p.Comments,p.Description from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
            //  " where p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID and P.AssetName=PPA.Id";
            string query = "select p.Id,AssetId,PPA.BHAGroupName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.ToolTypeName as assetcategory,SerialNumber," +
             "Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.PinTop,p.PinBottom,p.Comments,p.Description from"+
            " [RigTrack].[Prism_BHAAssets] p,PrsimWarehouses c, [RigTrack].[BHAToolTypes] ca,[RigTrack].[tblCreateBHAToolGroup] PPA " +
              " where p.WarehouseId=c.ID and p.BHATypeId=ca.ToolTypeID and P.[BHAGroupId]=PPA.Id";
            if (ddl_warehouse.SelectedValue != "" && ddl_warehouse.SelectedValue != "0")
            {
                query += " and c.ID=" + ddl_warehouse.SelectedValue + "";
            }
            query += " order by p.Id desc";
            // Create a SqlCommand with the SELECT statement to retrieve all the meters
            using (SqlCommand sqlcmd = new SqlCommand(query, sqlConn))
            {
                // Run the SELECT statement to fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            sqlConn.Close();
        }

        // Set the RADGrid's DataSource to the DataTable
        return dt;

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        int BHANumber = col.IntUtilParse(radBHANumber.SelectedValue);
        DataTable dt_bhaItemsInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from [RigTrack].[tblBHADataItemsInfo] where BHAID=" + radBHANumber.SelectedValue + "").Tables[0];
        foreach (GridItem item in radgrdMeterList.MasterTableView.Items)
        {
            CheckBox chkActive = (CheckBox)item.FindControl("chkActive");
            int dataKeyValue = col.IntUtilParse(((GridDataItem)(item)).GetDataKeyValue("ID").ToString());
            DataRow[] dr_det = dt_bhaItemsInfo.Select("ToolID=" + dataKeyValue + "");
            if (dr_det.Length > 0)
            {
                if (!chkActive.Checked)
                {
                    string queryInsert = "Delete [RigTrack].[tblBHADataItemsInfo] where ToolID=" + dataKeyValue + "";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);

                }
            }
            else
            {
                //RadDropDownList ddlTargetShape = (RadDropDownList)item.FindControl("ddlTargetShape");
               
                if (chkActive.Checked)
                {
                    string queryInsert = "Insert into [RigTrack].[tblBHADataItemsInfo](BHAID,ToolID) values(" + BHANumber + "," + dataKeyValue + ")";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                    
                }
            }
        }
        lbl_message.Text = "BHA Type Created Successfully";
        lbl_message.ForeColor = Color.Green;
        clear();

    }
    protected void radBHANumber_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        lbl_message.Text = "";
    }
    public void clear()
    {
        radBHANumber.SelectedValue = "0";
        ddlCompany.SelectedValue = "0";
        combo_job.SelectedValue = "0";
        radgrdMeterList.Rebind();
    }
}