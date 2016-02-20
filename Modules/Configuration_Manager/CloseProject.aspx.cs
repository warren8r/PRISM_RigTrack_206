using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;

public partial class Modules_Configuration_Manager_CloseProject : System.Web.UI.Page
{
    public static DataTable dt_close, dt_getassetrunhrs, dt_prism_assets;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //radwin.RadConfirm("Are you sure?", "confirmCallBackFn", 300, 100, null, "My Confirm", "myConfirmImage.png");
            bindgridview();

            //" Run.runid=RunHours.runid   and Run.jid=1 group by RunHours.assetid").Tables[0];
        }
        dt_close = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select  RunHours.assetid, sum(dailyrunhrs) as RunHrs from PrismJobRunHourdetails as RunHours,PrismJobRunDetails Run where " +
                " Run.runid=RunHours.runid   and Run.jid=1 group by RunHours.assetid").Tables[0];
        string lable = lbl_docid.Text;
        if (lable != "")
        {
            radgrid2bind();
        }
    }
    protected void RadGrid_Assets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
            Label lbl_totalrunhrs = (Label)item.FindControl("lbl_totalrunhrs");
            DataRow[] dr_runhrval = dt_getassetrunhrs.Select("AssetId=" + lbl_assetid.Text + "");
            string obj_sum = dt_getassetrunhrs.Compute("sum(dailyrunhrs)", "AssetId=" + lbl_assetid.Text + "").ToString();
            DataRow[] dr_prismupdate = dt_prism_assets.Select("ID=" + lbl_assetid.Text + "");
            if (dr_runhrval.Length > 0)
            {
                lbl_totalrunhrs.Text = dr_runhrval[0]["dailyrunhrs"].ToString();
            }
            else
            {
                lbl_totalrunhrs.Text = "0";
            }
            if (dr_prismupdate.Length > 0)
            {
                if (dr_prismupdate[0]["LastMaintanenceDate"].ToString() == "")
                {
                    if (dr_prismupdate[0]["previoususedhrs"].ToString() != "")
                    {
                        if (obj_sum != "")
                        {
                            lbl_totalrunhrs.Text = (Convert.ToDecimal(obj_sum) + Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"])).ToString();
                        }
                        else
                        {
                            lbl_totalrunhrs.Text = Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"]).ToString();
                        }
                    }
                }
                else
                {
                    string obj_sumofdate = dt_getassetrunhrs.Compute("sum(dailyrunhrs)", "Date>'" + Convert.ToDateTime(dr_prismupdate[0]["LastMaintanenceDate"].ToString()) + "' and AssetId=" + lbl_assetid.Text + "").ToString();
                    if (obj_sumofdate != "")
                    {
                        //DataRow[] dr_prismupdatedate = dt_prism_assets.Select("Date>" + Convert.ToDateTime(dr_prismupdate[0]["LastMaintanenceDate"].ToString()) + "");
                        lbl_totalrunhrs.Text = obj_sumofdate;
                    }
                }
            }
            //DataRow[] row_job = dt_close.Select("jid='" + lbl_docid.Text + "'");
            //if (row_job.Length > 0)
            //{

            //}
        }
    }
    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "downloaddoc")
        {
            Label lbl_docname = (Label)e.Item.FindControl("lbl_docname");
            string path = Server.MapPath("../../Documents/" + lbl_docname.Text);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_docname.Text);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();

        }

        RadGrid1.Rebind();
    }
    protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "downloaddoc")
        {
            Label lbl_docname = (Label)e.Item.FindControl("lbl_docname");
            string path = Server.MapPath("../../Documents/" + lbl_docname.Text);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_docname.Text);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();

        }

        RadGrid2.Rebind();
    }
    protected void radgrid_managejobs_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "viewedit")
        {
            id_viewpart.Visible = true;
            GridDataItem editedItem = e.Item as GridDataItem;

            foreach (GridDataItem dataItem in radgrid_managejobs.Items)
            {
                dataItem.BackColor = System.Drawing.Color.White;
            }
            editedItem.BackColor = System.Drawing.Color.Green;
            Label lbl_jobid = (Label)editedItem.FindControl("lbl_jobid");
            Label lbl_statuscheck = (Label)editedItem.FindControl("lbl_statuscheck");
            //SUM(RunHours.dailyrunhrs)as AssetRunHours, RunHours.assetid as AssetID       group by RunHours.assetid
            string selectq = "select * from PrismJobRunDetails Run, PrismJobRunHourdetails RunHours" +
                " where Run.runid=RunHours.runid and Run.jid=" + lbl_jobid.Text + " ";
            dt_getassetrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
            dt_prism_assets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from prism_assets").Tables[0];
            if (lbl_statuscheck.Text == "Closed")
            {
                btn_close.Enabled = false;
            }
            else
            {
                btn_close.Enabled = true;
            }
            lbl_docid.Text = lbl_jobid.Text;
            RadGrid1.Rebind();
            DataTable dt_existjobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from manageJobOrders where jid=" + lbl_jobid.Text + "").Tables[0];
            if (dt_existjobs.Rows.Count > 0)
            {
                if (dt_existjobs.Rows[0]["bitActive"].ToString() == "True")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }
                radtxt_jobname.Text = dt_existjobs.Rows[0]["jobname"].ToString();
                txtAssetNumber.Text = dt_existjobs.Rows[0]["jobid"].ToString();
                radcombo_jobtype.SelectedValue = dt_existjobs.Rows[0]["jobtype"].ToString();
                date_start.SelectedDate = Convert.ToDateTime(dt_existjobs.Rows[0]["startdate"].ToString());
                date_stop.SelectedDate = Convert.ToDateTime(dt_existjobs.Rows[0]["enddate"].ToString());
                radtxt_cost.Text = dt_existjobs.Rows[0]["cost"].ToString();
                radcombo_customer.SelectedValue = dt_existjobs.Rows[0]["Customer"].ToString();
                txtprimaryAddress1.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                ddlPrimaryCountry.SelectedText = dt_existjobs.Rows[0]["primaryCountry"].ToString();
                txtprimaryAddress2.Text = dt_existjobs.Rows[0]["primaryAddress2"].ToString();
                ddlPrimaryState.SelectedText = dt_existjobs.Rows[0]["primaryState"].ToString();
                txtprimaryCity.Text = dt_existjobs.Rows[0]["primaryCity"].ToString();
                txtprimaryPostalCode.Text = dt_existjobs.Rows[0]["primaryPostalCode"].ToString();
                txtPrimaryFirst.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                txtPrimaryLast.Text = dt_existjobs.Rows[0]["primaryLast"].ToString();
                txtPrimaryPhone1.Text = dt_existjobs.Rows[0]["primaryPhone1"].ToString();
                txtPrimaryPhone2.Text = dt_existjobs.Rows[0]["primaryPhone2"].ToString();
                txtPrimaryEmail.Text = dt_existjobs.Rows[0]["primaryEmail"].ToString();


                //SECONDARY INFO
                txtSecondaryAddress1.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                ddlSecondaryCountry.SelectedText = dt_existjobs.Rows[0]["primaryCountry"].ToString();
                txtSecondaryAddress2.Text = dt_existjobs.Rows[0]["primaryAddress2"].ToString();
                ddlSecondaryState.SelectedText = dt_existjobs.Rows[0]["primaryState"].ToString();
                txtSecondaryCity.Text = dt_existjobs.Rows[0]["primaryCity"].ToString();
                txtSecondaryPostalCode.Text = dt_existjobs.Rows[0]["primaryPostalCode"].ToString();
                txtSecondaryFirst.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                txtSecondaryLast.Text = dt_existjobs.Rows[0]["primaryLast"].ToString();
                txtSecondaryPhone1.Text = dt_existjobs.Rows[0]["primaryPhone1"].ToString();
                txtSecondaryPhone2.Text = dt_existjobs.Rows[0]["primaryPhone2"].ToString();
                txtSecondaryEMail.Text = dt_existjobs.Rows[0]["primaryEmail"].ToString();
                radtxt_notes.Text = dt_existjobs.Rows[0]["salesnotes"].ToString();
                //radcombo_opmngers.SelectedValue = dt_existjobs.Rows[0]["opManagerId"].ToString();
                radtxt_opnotes.Text = dt_existjobs.Rows[0]["opmgrnotes"].ToString();
                if (dt_existjobs.Rows[0]["status"].ToString() == "Approved")
                {
                    radcombo_pmanager.SelectedValue = dt_existjobs.Rows[0]["programManagerId"].ToString();
                }
                radgrid2bind();//+' - '+Convert(varchar,Asset.runhrmaintenance)
                string select_query = "select clientAssetName as AssetCategory,Name.AssetName as AssetFullName,Name.Id as AssetId,Asset.SerialNumber,Job.jid, Asset.runhrmaintenance as hrs, Asset.maintenancepercentage as per,Asset.Id as AID, " +
                    " Convert(varchar,(Asset.runhrmaintenance-(Asset.runhrmaintenance*Asset.maintenancepercentage/100))) as maintenanceRange" +
                    " from clientAssets Category,PrismAssetName Name,Prism_Assets Asset,manageJobOrders Job,PrismJobAssignedAssets jobassets where " +
                    " Category.clientAssetID=Name.AssetCategoryId and Name.Id=Asset.AssetName and jobassets.AssetId=Asset.Id and jobassets.JobId=Job.jid and " +
                    " jobassets.AssignmentStatus='Active' and jid=" + lbl_jobid.Text + "";
                DataTable dt_existingassetstojobid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, select_query).Tables[0];
                RadGrid_Assets.DataSource = dt_existingassetstojobid;
                RadGrid_Assets.DataBind();
            }

        }
    }
    public void radgrid2bind()
    {
        string qradgrid = "SELECT  etod.jid,etod.documentid,etod.UserID,etod.UploadedDate,(u.firstName+' '+u.lastName) as UserName, e.jid,d.DocumentDisplayName,d.DocumentName from" +
                            " JobOrderDocuments etod, manageJobOrders e, documents d, Users u where u.userID=etod.UserID and e.jid=etod.jid and d.DocumentID=etod.DocumentID and " +
                            " e.jid=" + lbl_docid.Text + " and etod.type='JO'  order by etod.jid desc";
        DataTable dt_radgrid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, qradgrid).Tables[0];
        RadGrid2.DataSource = dt_radgrid;
        RadGrid2.DataBind();
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {

        bindgridview();
    }
    public void bindgridview()
    {
        id_viewpart.Visible = false;
        reset();
        //string fromdate = String.Format("{0:MM/dd/yyyy}", radtxt_from.SelectedDate);
        //string todate = String.Format("{0:MM/dd/yyyy}", radtxt_to.SelectedDate);
        string query_select = "SELECT (firstName+' '+lastName) as Username,j.jobType as Jobtypename,* from manageJobOrders m,PrsimCustomers c,Users u," +
            "jobTypes j ,RigTypes rt where c.ID=m.Customer and m.opManagerId=u.userID and " +
            "m.jobtype=j.jobtypeid and m.status!='Pending' and rt.rigtypeid=m.rigtypeid  order by jid desc";
        DataTable dt_jobbind = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
        radgrid_managejobs.DataSource = dt_jobbind;
        radgrid_managejobs.DataBind();
    }
    protected void radgrid_managejobs_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_statuscheck = (Label)item.FindControl("lbl_statuscheck");
            //CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            //isChecked.Attributes.Add("onclick", "javascript:return confirmclose();");

            if (lbl_statuscheck.Text == "Closed")
            {
                lbl_statuscheck.ForeColor = Color.Red;

                //isChecked.Enabled = false;
            }
            else
            {
                lbl_statuscheck.Text = "Open";
                lbl_statuscheck.ForeColor = Color.Blue;

                //isChecked.Enabled = true;
            }
        }
    }
    protected void btn_close_OnClick(object sender, EventArgs e)
    {
        string updatequery = "update manageJobOrders set status='Closed' where jid=" + lbl_docid.Text + "";
        int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
        foreach (GridItem item in RadGrid_Assets.Items)
        {
            Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
            RadComboBox radcombo_moveto = (RadComboBox)item.FindControl("radcombo_moveto");
            DataTable dt_AssetCurrLoc = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetCurrentLocation").Tables[0];
            string query_select = "select pn.AssetName as AssetName,* from Prism_Assets pa,PrismAssetName pn,clientAssets pc where pa.AssetName=pn.Id and pa.AssetCategoryId=pc.clientAssetID and pa.id=" + lbl_assetid.Text + "";
            DataTable dt_asset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            int maxassetID = 0;
            maxassetID = Convert.ToInt32(dt_AssetCurrLoc.Compute("Max ( AssetCurrentLocationID ) ", "AssetID = " + lbl_assetid.Text + "").ToString());
            DataRow[] row_location = dt_AssetCurrLoc.Select("AssetCurrentLocationID=" + maxassetID);
            //lbl_locationame.Text = row_location[0]["CurrentLocationType"].ToString();
            string fromLocType = "", fromLocId = "";
            string ToLocType = "", ToLocId = "";
            string currLocType = "", currLocId = "";
            string status = "";

            fromLocType = row_location[0]["CurrentLocationType"].ToString();
            fromLocId = row_location[0]["CurrentLocationID"].ToString();
            currLocType = "Warehouse";
            currLocId = dt_asset.Rows[0]["WarehouseId"].ToString();
            ToLocType = "Warehouse";
            ToLocId = dt_asset.Rows[0]["WarehouseId"].ToString();
            status = "Available";

            string insertCurrentLocationQuery = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                "" + lbl_assetid.Text + ",'" + fromLocType + "'," + fromLocId + ",'" + ToLocType + "'," + ToLocId + ",'" + status + "','" + currLocType + "','" + currLocId + "')";

            int insertcnt1 = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertCurrentLocationQuery);
            if (radcombo_moveto.SelectedValue == "Maintenance")
            {
                string query_insertmaintenance = "insert into PrismAssetRepairStatus(assetid,repairdate,status)values(" + lbl_assetid.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Pending')";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query_insertmaintenance);
                //repairstatus
                string updatemaintenancefromprism_assets = "update Prism_Assets set repairstatus='Maintenance' where ID=" + lbl_assetid.Text + "";
                //string updatemaintenancefromprism_assets = "update Prism_Assets set repairstatus='Maintenance' where ID=" + combo_ex_assets.Items[asset].Value + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatemaintenancefromprism_assets);
            }
        }
        
        //DataTable dt_getassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedAssets where JobId=" + lbl_docid.Text + "").Tables[0];
        
        //for (int i = 0; i < dt_getassets.Rows.Count; i++)
        //{
            


        //}
        string deleteassignedassets = "update PrismJobAssignedAssets set AssignmentStatus='InActive' where JobId=" + lbl_docid.Text + "";
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, deleteassignedassets);
        string deleteassignedpersonals = "update PrismJobAssignedPersonals set AssignmentStatus='InActive' where JobId=" + lbl_docid.Text + "";
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, deleteassignedpersonals);
        lbl_message.Text = "Project Closed Successfully";
        lbl_message.ForeColor = Color.Green;
        btn_close.Enabled = false;
        bindgridview();
    }
    protected void CheckChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label lbl_jobid = (Label)row.FindControl("lbl_jobid");
        string updatequery = "update manageJobOrders set status='Closed' where jid=" + lbl_jobid.Text + "";
        int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
        DataTable dt_getassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedAssets where JobId=" + lbl_jobid.Text + "").Tables[0];
        DataTable dt_AssetCurrLoc = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetCurrentLocation").Tables[0];
        for (int i = 0; i < dt_getassets.Rows.Count; i++)
        {
            string query_select = "select pn.AssetName as AssetName,* from Prism_Assets pa,PrismAssetName pn,clientAssets pc where pa.AssetName=pn.Id and pa.AssetCategoryId=pc.clientAssetID and pa.id=" + dt_getassets.Rows[i]["AssetId"].ToString() + "";
            DataTable dt_asset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            int maxassetID = 0;
            maxassetID = Convert.ToInt32(dt_AssetCurrLoc.Compute("Max ( AssetCurrentLocationID ) ", "AssetID = " + dt_getassets.Rows[i]["AssetId"].ToString() + "").ToString());
            DataRow[] row_location = dt_AssetCurrLoc.Select("AssetCurrentLocationID=" + maxassetID);
            //lbl_locationame.Text = row_location[0]["CurrentLocationType"].ToString();
            string fromLocType = "", fromLocId = "";
            string ToLocType = "", ToLocId = "";
            string currLocType = "", currLocId = "";
            string status = "";

            fromLocType = row_location[0]["CurrentLocationType"].ToString();
            fromLocId = row_location[0]["CurrentLocationID"].ToString();
            currLocType = "Warehouse";
            currLocId = dt_asset.Rows[0]["WarehouseId"].ToString();
            ToLocType = "Warehouse";
            ToLocId = dt_asset.Rows[0]["WarehouseId"].ToString();
            status = "Available";

            string insertCurrentLocationQuery = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                "" + dt_getassets.Rows[i]["AssetId"].ToString() + ",'" + fromLocType + "'," + fromLocId + ",'" + ToLocType + "'," + ToLocId + ",'" + status + "','" + currLocType + "','" + currLocId + "')";

            int insertcnt1 = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertCurrentLocationQuery);



        }
        string deleteassignedassets = "update PrismJobAssignedAssets set AssignmentStatus='InActive' where JobId=" + lbl_jobid.Text + "";
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, deleteassignedassets);
        string deleteassignedpersonals = "update PrismJobAssignedPersonals set AssignmentStatus='InActive' where JobId=" + lbl_jobid.Text + "";
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, deleteassignedpersonals);

        bindgridview();
    }
    public void reset()
    {
        CheckBox1.Checked = false;
        radtxt_jobname.Text = "";
        txtAssetNumber.Text = "";
        radcombo_jobtype.SelectedIndex = -1;
        date_start.SelectedDate = DateTime.Now;
        date_stop.SelectedDate = DateTime.Now;
        radtxt_cost.Text = "";
        radcombo_customer.SelectedIndex = -1;
        txtprimaryAddress1.Text = "";
        ddlPrimaryCountry.SelectedIndex = -1;
        txtprimaryAddress2.Text = "";
        ddlPrimaryState.SelectedIndex = -1;
        txtprimaryCity.Text = "";
        txtprimaryPostalCode.Text = "";
        txtPrimaryFirst.Text = "";
        txtPrimaryLast.Text = "";
        txtPrimaryPhone1.Text = "";
        txtPrimaryPhone2.Text = "";
        txtPrimaryEmail.Text = "";


        //SECONDARY INFO
        txtSecondaryAddress1.Text = "";
        ddlSecondaryCountry.SelectedText = "";
        txtSecondaryAddress2.Text = "";
        ddlSecondaryState.SelectedIndex = -1;
        txtSecondaryCity.Text = "";
        txtSecondaryPostalCode.Text = "";
        txtSecondaryFirst.Text = "";
        txtSecondaryLast.Text = "";
        txtSecondaryPhone1.Text = "";
        txtSecondaryPhone2.Text = "";
        txtSecondaryEMail.Text = "";
        //btn_approve.Text = "Approve";
        radcombo_pmanager.SelectedIndex = -1;
        //RadGrid1.Visible = false;
    }
}