using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Telerik.Web.UI;
public partial class Modules_Configuration_Manager_JobAssignments : System.Web.UI.Page
{
    string notificationWays = "";
    public static DataTable dt_users = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindAsets("");
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
            dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users").Tables[0];

           

            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();

            combo_job.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            combo_job.DataTextField = "CurveGroupName";
            combo_job.DataValueField = "ID";
            combo_job.DataBind();

            ddlCompanysearch.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompanysearch.DataTextField = "Name";
            ddlCompanysearch.DataValueField = "ID";
            ddlCompanysearch.DataBind();

            comboJobSearch.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            comboJobSearch.DataTextField = "CurveGroupName";
            comboJobSearch.DataValueField = "ID";
            comboJobSearch.DataBind();
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            combo_job.Items.Clear();
            combo_job.Items.Add(new RadComboBoxItem("Select All", "0"));
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
        if (comboJobSearch.SelectedValue != "0")
        {
            string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            " RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and JO.ID =" + comboJobSearch.SelectedValue + " order by JO.CreateDate desc";
            DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            grdJobList.DataSource = dtJobs;
            grdJobList.DataBind();
        }
        else
        {
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }
    }
    protected void ddlCompanysearch_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompanysearch.SelectedValue != "0")
        {

            comboJobSearch.Items.Clear();
            comboJobSearch.Items.Add(new RadComboBoxItem("Select All", "0"));
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompanysearch.SelectedValue));
            comboJobSearch.DataSource = dtJobDetails;
            comboJobSearch.DataTextField = "CurveGroupName";
            comboJobSearch.DataValueField = "ID";
            comboJobSearch.DataBind();
            string ids = "";
            for (int i = 0; i < dtJobDetails.Rows.Count; i++)
            {
                ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
            }
            if (ids != "")
            {
                ids = ids.Remove(ids.Length - 1, 1);
                string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            " RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and JO.ID in(" + ids + ") order by JO.CreateDate desc";
                DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
                grdJobList.DataSource = dtJobs;
                grdJobList.DataBind();
            }
        }
        else
        {

            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }

    }
    public string returnassetsfromkit(string assetkitid)
    {
        string strval = "", finalassets = "";
        DataTable dt_getexistkitnames = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismKitAssetFromKitName where assetkitid=" + assetkitid + "").Tables[0];
        for (int i = 0; i < dt_getexistkitnames.Rows.Count; i++)
        {
            strval += dt_getexistkitnames.Rows[i]["assetids"].ToString() + ",";
        }
        if (strval != "")
        {
            finalassets = strval.Remove(strval.Length - 1, 1);
        }
        return finalassets;
    }

    protected void RadGrid_kits_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_kitid = (Label)item.FindControl("lbl_kitid");
            Label lbl_jobid = (Label)item.FindControl("lbl_jobid");
            Label lbl_kitstatusid = (Label)item.FindControl("lbl_kitstatusid");
            Label lbl_kitstatus = (Label)item.FindControl("lbl_kitstatus");
            if (lbl_kitstatusid.Text == "2")
            {
                lbl_kitstatus.Text = "In Transit";
            }
            else if (lbl_kitstatusid.Text == "3")
            {
                lbl_kitstatus.Text = "Delivered";
            }
            else
            {
                lbl_kitstatus.Text = "Pending Shipmet";
            }
            RadGrid radgridkitassets = (RadGrid)item.FindControl("radgridkitassets");
            string query_getassets = "select a.AssetName,c.SerialNumber,qty from PrismKitAssetFromKitName k,PrismAssetName a,Prism_Assets c where c.ID=k.assetids and c.AssetName=a.Id and k.assetkitid=" + lbl_kitid.Text + " ";
            DataTable dt_getassetids = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_getassets).Tables[0];
            radgridkitassets.DataSource = dt_getassetids;
            radgridkitassets.DataBind();
            //string assets = "";
            //for (int i = 0; i < dt_getassetids.Rows.Count; i++)
            //{

            //}

            
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        combo_job.ClearSelection();
        combo_job.Items.Clear();
        combo_job.Items.Add(new RadComboBoxItem("Select All", "0"));
        ddlCompany.ClearSelection();
        //combo_personal.ClearCheckedItems();
        //combo_personal.ClearSelection();
        //combo_service.ClearCheckedItems();
        //combo_service.ClearSelection();
        //radcosumable.ClearCheckedItems();
        //radcosumable.ClearSelection();
        combo_assetcategory.ClearCheckedItems();
        combo_assetcategory.ClearSelection();
        combo_assets.ClearCheckedItems();
        combo_assets.ClearSelection();
        radcombo_Assetkit.ClearCheckedItems();
        radcombo_Assetkit.ClearSelection();
        lbl_message.Text = "";
        bindAsets("");
        grdJobList.DataSource = jobOrders("", "");
        grdJobList.DataBind();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        string notavailassets = "", assignedassetnames = "";
        string queryjobassetInsert = "", queryjobpersonalInsert = "";
        try
        {
            //if (chk_asset.Checked)
            //{
                //Jobs with Assets insertion
                for (int asset = 0; asset < combo_assets.Items.Count; asset++)
                {
                    if (combo_assets.Items[asset].Checked)
                    {
                        try
                        {
                            string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
                            queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,AssignmentStatus)" +
                               " values('" + combo_job.SelectedValue + "','" + combo_assets.Items[asset].Value + "','1','" + Session["userId"].ToString() + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Active')";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);
                            string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE   AssetID=" + combo_assets.Items[asset].Value + " order by AssetCurrentLocationID desc";
                            DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                            DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Assets where ID=" + combo_assets.Items[asset].Value + "").Tables[0];
                            if (dt_existassetlocation.Rows.Count == 0)
                            {
                                fromlocationtype = "WareHouse";
                                fromlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                                currentlocationtype = "WareHouse";
                                currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                            }
                            else
                            {
                                fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                                fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                                currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                                currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                            }
                            tolocationtype = "Job";
                            tolocationid = combo_job.SelectedValue.ToString();
                            assetstatus = "Available";

                            string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                                "" + combo_assets.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);

                            //      string info = "Following " + combo_assets.Items[asset].Text + " is Assigned to Job:" + combo_job.SelectedItem.Text;
                            //      string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedID,userAssignedTimeStamp,Source)values(" +
                            //    "'PA01','" + info + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," + combo_assets.Items[asset].Value + "" +
                            //",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','SALE')";
                            //      SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
                            //string notificationsendtowhome = eventNotification.sendEventNotification("AC02");
                            //if (notificationsendtowhome != "")
                            //{
                            //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "AC02", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                            //           "", "", "Assetassigned", "", combo_assets.Items[asset].Text);

                            //}
                        }
                        catch (Exception ex) { transaction.Rollback(); }
                        finally
                        {
                        }

                    }
                }
            //}
            for (int assetkit = 0; assetkit < radcombo_Assetkit.Items.Count; assetkit++)
            {
                if (radcombo_Assetkit.Items[assetkit].Checked)
                {
                    string insert_kitsexist = "insert into PrismJobKits(jobid,kitid)values(" + combo_job.SelectedValue + "," + radcombo_Assetkit.Items[assetkit].Value + ")";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_kitsexist);
                }
            }
            //for (int c = 0; c < radcosumable.Items.Count; c++)
            //{
            //    if (radcosumable.Items[c].Checked)
            //    {
            //        string insert_conexist = "insert into PrismJobConsumables(jobid,consumableid,AssignedDate)values(" + combo_job.SelectedValue + "," + radcosumable.Items[c].Value + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
            //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_conexist);
            //    }
            //}
            
            //else if(chk_kit.Checked)
            //{
            //    if (radcombo_Assetkit.CheckedItems.Count>0)
            //    {
            //        for (int assetkit = 0; assetkit < radcombo_Assetkit.Items.Count; assetkit++)
            //        {
            //            if (radcombo_Assetkit.Items[assetkit].Checked)
            //            {
            //                //string returnassetidsfromkit = returnassetsfromkit(radcombo_Assetkit.Items[assetkit].Value);

            //                //string[] arr_searchassets = returnassetidsfromkit.Split(',');
            //                DataTable dt_getexistkitnames = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismKitAssetFromKitName where assetkitid=" + radcombo_Assetkit.Items[assetkit].Value + "").Tables[0];
            //                for (int asset = 0; asset < dt_getexistkitnames.Rows.Count; asset++)
            //                {
            //                    string selectquery = "select  top(" + dt_getexistkitnames.Rows[asset]["qty"] + ") P.Id,pa.Id  as assignid,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA" +
            //                        " where  P.AssetName=PA.ID and  P.id not in (select AssetId from PrismJobAssignedAssets) and PA.Id =" + dt_getexistkitnames.Rows[asset]["assetids"].ToString() + "  order by P.Id";
            //                    DataTable dt_existassets = SqlHelper.ExecuteDataset(transaction, CommandType.Text, selectquery).Tables[0];
            //                    for (int i=0;i<dt_existassets.Rows.Count;i++)
            //                    {
            //                        try
            //                        {
            //                            string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
            //                            queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,KitName)" +
            //                                " values('" + combo_job.SelectedValue + "','" + dt_existassets.Rows[i]["ID"].ToString() + "','1','" + Session["userId"].ToString() + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + radcombo_Assetkit.Items[assetkit].Text + "')";
            //                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);
            //                            string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE   AssetID=" + dt_existassets.Rows[i]["ID"].ToString() + " order by AssetCurrentLocationID desc";
            //                            DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
            //                            DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Assets where ID=" + dt_existassets.Rows[i]["ID"].ToString() + "").Tables[0];
            //                            if (dt_existassetlocation.Rows.Count == 0)
            //                            {
            //                                fromlocationtype = "WareHouse";
            //                                fromlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
            //                                currentlocationtype = "WareHouse";
            //                                currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
            //                            }
            //                            else
            //                            {
            //                                fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
            //                                fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

            //                                currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
            //                                currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

            //                            }
            //                            tolocationtype = "Job";
            //                            tolocationid = combo_job.SelectedValue.ToString();
            //                            assetstatus = "Available";

            //                            string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
            //                                "" + dt_existassets.Rows[i]["ID"].ToString() + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
            //                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);

            //                            string info = "Following " + dt_existassets.Rows[0]["ID"].ToString() + " is Assigned to Job:" + combo_job.SelectedItem.Text;
            //                            string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedID,userAssignedTimeStamp,Source)values(" +
            //                            "'PA01','" + info + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," + dt_existassets.Rows[i]["ID"].ToString() + "" +
            //                        ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','SALE')";
            //                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insert_q);
            //                            assignedassetnames += dt_existassets.Rows[i]["AssetName"].ToString();
            //                        }
            //                        catch (Exception ex) { transaction.Rollback(); }
            //                        finally
            //                        {
            //                        }
            //                    }
            //                    //else
            //                    //{
            //                    //    DataTable dt_as = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismAssetName where Id=" + dt_getexistkitnames.Rows[asset]["assetids"].ToString() + "").Tables[0];
            //                    //    notavailassets += dt_as.Rows[0]["AssetName"].ToString();
            //                    //}
            //                }

            //            }
            //        }

            //    }
            //}
            // Jobs with Personal Insertion
            //for (int personal = 0; personal < combo_personal.Items.Count; personal++)
            //{
            //    if (combo_personal.Items[personal].Checked)
            //    {
            //        try
            //        {
            //            queryjobpersonalInsert = "Insert into PrismJobAssignedPersonals(JobId,UserId,AssignmentStatus)" +
            //               " values('" + combo_job.SelectedValue + "','" + combo_personal.Items[personal].Value + "','Active')";
            //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalInsert);

            //            string notificationsendtowhome = eventNotification.sendEventNotification("PA01");
            //            if (notificationsendtowhome != "")
            //            {
            //                bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "PA01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
            //                       "", "", "Assign", "", "");

            //            }

            //        }
            //        catch (Exception ex) { transaction.Rollback(); }
            //        finally
            //        {
            //        }

            //    }
            //}
            //// Jobs with Service Insertion
            //for (int personal = 0; personal < combo_service.Items.Count; personal++)
            //{
            //    if (combo_service.Items[personal].Checked)
            //    {
            //        try
            //        {
            //            queryjobpersonalInsert = "Insert into PrismJobServiceAssignment(JobId,ServiceID)" +
            //               " values('" + combo_job.SelectedValue + "','" + combo_service.Items[personal].Value + "')";
            //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalInsert);
            //            string notificationsendtowhome = eventNotification.sendEventNotification("JSA01");
            //            if (notificationsendtowhome != "")
            //            {
            //                bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JSA01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
            //                       "", "", "Serviceassigned", "", combo_service.Items[personal].Text);

            //            }
            //            //      string info = "Following " + combo_service.Items[personal].Text + " is Assigned to Job:" + combo_job.SelectedItem.Text;
            //            //      string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedID,userAssignedTimeStamp,Source)values(" +
            //            //    "'PA01','" + info + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," + combo_service.Items[personal].Value + "" +
            //            //",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','SALE')";
            //            //      SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
            //        }
            //        catch (Exception ex) { transaction.Rollback(); }
            //        finally
            //        {
            //        }

            //    }
            //}
            //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "Update manageJobOrders set JobAssignedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where jid='" + combo_job.SelectedValue + "'");
            transaction.Commit();
            lbl_message.ForeColor = Color.Green;
            lbl_message.Text = "Job Assignment Successfully Completed with Tool(s) and Created shipping ticket";
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            lbl_message.ForeColor = Color.Red;
            lbl_message.Text = "Job assignment Failed";
        }
    }
    protected void gridJobAssets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            // Yes, get the item
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_KitName = (Label)item.FindControl("lbl_KitName");
            if (lbl_KitName.Text == "")
            {
                lbl_KitName.Text = "--NA--";
            }

        }
    }
    public DataTable jobOrders(string startdate, string stopdate)
    {
        string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            " RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') order by JO.CreateDate desc";
        return SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
    }
    
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        
        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource = jobOrders("", "");
        grdJobList.DataBind();
    }
    
    protected void grdJobList_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        {

            GridDataItem item = e.Item as GridDataItem;

            if (!item.Expanded)
            {

                GridNestedViewItem nestedItem = (GridNestedViewItem)item.ChildItem;

                string dataKeyValue = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("ID"));

                //RadGrid gridJobPersonals = (RadGrid)nestedItem.FindControl("gridJobPersonals");
                RadGrid gridJobAssets = (RadGrid)nestedItem.FindControl("gridJobAssets");
                //RadGrid gridServices = (RadGrid)nestedItem.FindControl("gridServices");
                //RadGrid RadGrid_con = (RadGrid)nestedItem.FindControl("RadGrid_con");
                RadGrid RadGrid_kits = (RadGrid)nestedItem.FindControl("RadGrid_kits");

            //    gridJobPersonals.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //"select (u.firstName+' '+u.lastName) as username,r.userRole from PrismJobAssignedPersonals PA,Users u,UserRoles r where u.userRoleID=r.userRoleID and PA.UserId=u.UserId  and PA.JobId=" + dataKeyValue + "").Tables[0];

            //    gridJobPersonals.DataBind();
                string selectq = "select MJ.ID,A.AssetId,PA.AssetName,JA.KitName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus   " +
            " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,[RigTrack].tblCurveGroup MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
            " WA.ID=A.WarehouseId  and MJ.ID=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and" +
            " JA.ModifiedBy=u.userID and A.AssetName=PA.ID And JA.JobId=" + dataKeyValue + " and JA.kitname is null and JA.AssignmentStatus='Active'";
                gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
                gridJobAssets.DataBind();

            //    gridServices.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //"select * from PrismJobServiceAssignment PS,PrismService S where PS.ServiceID=S.ID  and PS.JobId=" + dataKeyValue + "").Tables[0];

            //    gridServices.DataBind();

            //    RadGrid_con.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //"select * from PrismJobConsumables Pc,manageJobOrders S,Consumables c where Pc.jobid=S.jid and Pc.consumableid=c.ConID and Pc.jobid=" + dataKeyValue + "").Tables[0];

            //    RadGrid_con.DataBind();

                RadGrid_kits.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobKits Pk,PrismAssetKitDetails k  where Pk.kitid=k.assetkitid and Pk.jobid=" + dataKeyValue + "").Tables[0];

                RadGrid_kits.DataBind();

            }

        }

    }
    protected void grdJobList_SortCommand(object source, GridSortCommandEventArgs e)
    {
        //Default sort order Descending


        if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Ascending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }
    }
    protected void combo_assetcategory_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //string val = combo_assets.SelectedValue;
        string category = "";
        for (int asset = 0; asset < combo_assetcategory.Items.Count; asset++)
        {
            if (combo_assetcategory.Items[asset].Checked)
            {
                category += combo_assetcategory.Items[asset].Value + ",";
            }
        }
        if (category != "")

            bindAsets(category.Remove(category.Length - 1, 1));
        else
            bindAsets("");
    }
    public void bindAsets(string category)
    {
        string query = "select  P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA  where  P.AssetName=PA.ID and  P.id not in" +
           " (select AssetId from PrismJobAssignedAssets) ";
        if (category != "")
        {
            query += " and PA.AssetCategoryId in(" + category + ")";
        }
        query += "  order by AssetName ASC";

        combo_assets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query);
        combo_assets.DataBind();
    }

}