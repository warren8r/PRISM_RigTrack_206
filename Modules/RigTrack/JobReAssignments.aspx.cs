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
using System.IO;
using System.Reflection;
public partial class Modules_Configuration_Manager_JobAssignments : System.Web.UI.Page
{
    public static DataTable dt_users = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        }
        lbl_message.Text = "";
    }
    protected void btn_ex_cear_Click(object sender, EventArgs e)
    {
        combo_ex_assets.ClearCheckedItems();
        combo_ex_assets.ClearSelection();
        combo_ex_personal.ClearCheckedItems();
        combo_ex_personal.ClearSelection();
        combo_ex_service.ClearCheckedItems();
        combo_ex_service.ClearSelection();
        combo_newjob.ClearSelection();
        combo_job.ClearSelection();
        combo_job.Items.Clear();
        combo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
        ddlCompany.ClearSelection();
        grdJobList.DataSource = jobOrders("", "");
        grdJobList.DataBind();
        btn_print.Visible = false;
        lbl_message.Text = "";

    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        combo_assets.ClearCheckedItems();
        combo_assets.ClearSelection();
        combo_personal.ClearCheckedItems();
        combo_personal.ClearSelection();
        combo_service.ClearCheckedItems();
        combo_service.ClearSelection();
        radcosumable.ClearCheckedItems();
        radcosumable.ClearSelection();
        btn_print.Visible = false;
        lbl_message.Text = "";
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            combo_job.Items.Clear();
            combo_job.Items.Add(new RadComboBoxItem("Select", "0"));
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            combo_job.DataSource = dtJobDetails;
            combo_job.DataTextField = "CurveGroupName";
            combo_job.DataValueField = "ID";
            combo_job.DataBind();
            string ids = "";
            for (int i = 0; i < dtJobDetails.Rows.Count; i++)
            {
                ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
            }
            if (ids != "")
            {
                ids = ids.Remove(ids.Length - 1, 1);
                string query = "select  CurveGroupName as JOB,* from [RigTrack].tblCurveGroup" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and ID in(" + ids + ") order by CreateDate desc";
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
        btn_print.Visible = false;

    }
    protected void combo_job_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //Binding Existing Jobs excluding selected Job
        DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
        string ids = "";
        for (int i = 0; i < dtJobDetails.Rows.Count; i++)
        {
            ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
        }
        if (ids != "")
        {
            ids = ids.Remove(ids.Length - 1, 1);
            combo_newjob.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            " select [ID],(CAST([ID] as nvarchar(MAX)) + ' - ' + [CurveGroupName]) as CurveGroupName from [RigTrack].tblCurveGroup where  " +
            " ID <>" + combo_job.SelectedValue + " and ID in ("+ ids + ") and isActive=1 ").Tables[0];
            combo_newjob.DataBind();
        }
        //Binding Existing Assets with Selected Job
        combo_ex_assets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            " select P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA  where P.AssetName=PA.ID and  P.id  in" +
            " (select AssetId from PrismJobAssignedAssets,[RigTrack].tblCurveGroup j where j.ID=PrismJobAssignedAssets.JobId and j.isActive='1' and PrismJobAssignedAssets.JobId=" + combo_job.SelectedValue + " and AssignMentStatus='Active')").Tables[0];
        combo_ex_assets.DataBind();
        comboToolstoWH.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            " select P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA  where P.AssetName=PA.ID and  P.id  in" +
            " (select AssetId from PrismJobAssignedAssets,[RigTrack].tblCurveGroup j where j.ID=PrismJobAssignedAssets.JobId and j.isActive='1' and PrismJobAssignedAssets.JobId=" + combo_job.SelectedValue + " and AssignMentStatus='Active')").Tables[0];
        comboToolstoWH.DataBind();
        
        string query = "select  CurveGroupName as JOB,* from [RigTrack].tblCurveGroup" +
           " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and ID =" + combo_job.SelectedValue + " order by CreateDate desc";
        DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        grdJobList.DataSource = dtJobs;
        grdJobList.DataBind();
        btn_print.Visible = false;

    }
    protected void radbutton_movetowarehouse_Click(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        string ticketid = GenerateNewAccountID(0);
        string queryjobassetUpdate = "", insertquery="";
        try
        {
            //Jobs with Assets insertion
            string inserttickets = "insert into PrismShippingTicket(ShippingDate,Status,TicketId,CreatedDate,ModifiedDate)values('" + DateTime.Now.ToString("MM/dd/yyyy") + "','Open','" + ticketid + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "')";
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, inserttickets);

            DataTable dt_TicketId = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    PrismShippingTicket WHERE  ID = IDENT_CURRENT('PrismShippingTicket')").Tables[0];
            for (int asset = 0; asset < combo_ex_assets.Items.Count; asset++)
            {
                if (combo_ex_assets.Items[asset].Checked)
                {


                    queryjobassetUpdate = "update PrismJobAssignedAssets set AssignmentStatus='InActive'  where JobId=" + combo_job.SelectedValue + " and AssetId=" + comboToolstoWH.Items[asset].Value;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetUpdate);

                    string queryprismassetsUpdate = "update Prism_Assets set repairstatus='Ok'  where Id=" + comboToolstoWH.Items[asset].Value;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryprismassetsUpdate);

                    string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", assetstatus = "", assetcurrentlocid = "", currentlocationtype = "", currentlocationid = "";

                    string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE    AssetID=" + combo_assets.Items[asset].Value + " order by AssetCurrentLocationID desc";
                    DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                    DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Assets where ID=" + combo_assets.Items[asset].Value + "").Tables[0];

                    if (dt_existassetlocation.Rows.Count > 0)
                    {

                        fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                        fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();


                        assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                        tolocationtype = "WareHouse";
                        tolocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        currentlocationtype = "WareHouse";
                        currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        assetstatus = "Available";

                        string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                        "" + comboToolstoWH.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);

                    }
                    string insertticketdetails = "insert into PrismShippingTicketDetails(TicketId,AssetId,FromLocation,FromLocationID,ToLocation,ToLocationID,Status)" +
                            "values(" + dt_TicketId.Rows[0]["ID"].ToString() + "," + comboToolstoWH.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," +
                            "" + tolocationid + ",'Pending Shipmet')";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertticketdetails);
                    //string notificationsendtowhome = eventNotification.sendEventNotification("JAMWH01");
                    //if (notificationsendtowhome != "")
                    //{
                    //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JAMWH01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                    //           "", "", "MoveToWarehouse", "", combo_ex_assets.Items[asset].Text);

                    //}


                }
            }
            transaction.Commit();

            lbl_message.ForeColor = Color.Green;
            //lbl_message.Text = "Tool Moved to Warehouse Succefully";
            hid_ticketid.Value = ticketid;
            lbl_message.Text = "Tool Moved to Warehouse Succefully and Created shipping ticket";
            btn_print.Visible = true;
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
            combo_job_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            hid_ticketid.Value = "0";
            lbl_message.Text = "Error";
        }
        finally
        {
        }
    }
    protected void btn_notinuse_Click(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        string queryjobassetUpdate = "", queryjobpersonalUpdate = "", queryjobserviceUpdate = "";
        try
        {
            //Jobs with Assets insertion
            for (int asset = 0; asset < combo_ex_assets.Items.Count; asset++)
            {
                if (combo_ex_assets.Items[asset].Checked)
                {
                    try
                    {
                        queryjobassetUpdate = "update PrismJobAssignedAssets set AssignmentStatus='InActive'  where JobId=" + combo_job.SelectedValue + " and AssetId=" + combo_ex_assets.Items[asset].Value;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetUpdate);


                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", assetstatus = "", assetcurrentlocid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE    AssetID=" + combo_assets.Items[asset].Value + " order by AssetCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + combo_assets.Items[asset].Value + "").Tables[0];
                        if (dt_existassetlocation.Rows.Count > 0)
                        {
                            assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                            tolocationtype = "Job";
                            tolocationid = combo_job.SelectedValue.ToString();
                            assetstatus = "Available";

                            string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "',NotInUseSince='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, update_locationtype);

                        }

                        //string notificationsendtowhome = eventNotification.sendEventNotification("JANIU01");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JANIU01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                        //           "", "", "AssetNotInUse", "", combo_ex_assets.Items[asset].Text);

                        //}
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
            // Jobs with Personal Insertion
            for (int personal = 0; personal < combo_ex_personal.Items.Count; personal++)
            {
                if (combo_ex_personal.Items[personal].Checked)
                {
                    try
                    {
                        queryjobpersonalUpdate = "update PrismJobAssignedPersonals  set AssignmentStatus='InActive' where JobId=" + combo_job.SelectedValue + " and UserId =" + combo_ex_personal.Items[personal].Value;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalUpdate);
                        //string notificationsendtowhome = eventNotification.sendEventNotification("JPNIU01");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JPNIU01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                        //           "", "", "PersonNotInUse", "", combo_ex_personal.Items[personal].Text);

                        //}
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
            // Jobs with Service Insertion
            //for (int personal = 0; personal < combo_ex_service.Items.Count; personal++)
            //{
            //    if (combo_ex_service.Items[personal].Checked)
            //    {
            //        try
            //        {
            //            queryjobserviceUpdate = "Delete PrismJobServiceAssignment where JobId=" + combo_job.SelectedValue + " and ServiceID=" + combo_ex_service.Items[personal].Value;

            //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobserviceUpdate);
            //            string notificationsendtowhome = eventNotification.sendEventNotification("JSNIU01");
            //            if (notificationsendtowhome != "")
            //            {
            //                bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JSNIU01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
            //                       "", "", "ServiceNotInUse", "", combo_ex_personal.Items[personal].Text);

            //            }
            //        }
            //        catch (Exception ex) { transaction.Rollback(); }
            //        finally
            //        {
            //        }

            //    }
            //}
            transaction.Commit();
            lbl_message.ForeColor = Color.Green;
            lbl_message.Text = "Job Not In Use Items Removed Successfully For Selected Tools";
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
            combo_job_SelectedIndexChanged(null,null );

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            lbl_message.ForeColor = Color.Red;
            lbl_message.Text = "Job Re-Assignment Failed";
        }
    }
    protected void btn_movemaintenance_OnClick(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();

        transaction = db.BeginTransaction();
        string queryjobassetUpdate = "", queryjobpersonalUpdate = "", queryjobserviceUpdate="";
        try
        {
            //Jobs with Assets insertion
            for (int asset = 0; asset < combo_ex_assets.Items.Count; asset++)
            {
                if (combo_ex_assets.Items[asset].Checked)
                {

                    queryjobassetUpdate = "Update PrismJobAssignedAssets set AssignmentStatus='InActive' where JobId=" + combo_job.SelectedValue + " and AssetId=" + comboToolstoWH.Items[asset].Value;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetUpdate);
                        string query_insertmaintenance = "insert into PrismAssetRepairStatus(assetid,repairdate,status)values(" + comboToolstoWH.Items[asset].Value + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Pending')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_insertmaintenance);
                        //repairstatus
                        string updatemaintenancefromprism_assets = "update Prism_Assets set repairstatus='Maintenance' where ID=" + comboToolstoWH.Items[asset].Value + "";
                        //string updatemaintenancefromprism_assets = "update Prism_Assets set repairstatus='Maintenance' where ID=" + combo_ex_assets.Items[asset].Value + "";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updatemaintenancefromprism_assets);
                        
                    
                }
            }
            transaction.Commit();
            lbl_message.ForeColor = Color.Green;
            lbl_message.Text = "Tool Moved to Maintenance for the selected Job";
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }
        catch (Exception ex) { transaction.Rollback(); }
        finally
        {
        }
    }
    public string GenerateNewAccountID(Int32 StartingKey)
    {
        //following SRP Example under a billion...
        Random rnd = new Random();
        return (string)rnd.Next(10000000, 21474836).ToString();
    }
    protected void btn_reassign_Click(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        string queryjobassetUpdate = "", queryjobpersonalUpdate = "", queryjobserviceUpdate="";
        string ticketid = GenerateNewAccountID(0);
        try
        {
            //Jobs with Assets insertion
            string inserttickets = "insert into PrismShippingTicket(ShippingDate,Status,TicketId,CreatedDate,ModifiedDate)values('" + DateTime.Now.ToString("MM/dd/yyyy") + "','Open','" + ticketid + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "')";
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, inserttickets);

            DataTable dt_TicketId = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    PrismShippingTicket WHERE  ID = IDENT_CURRENT('PrismShippingTicket')").Tables[0];
            for (int asset = 0; asset < combo_ex_assets.Items.Count; asset++)
            {
                if (combo_ex_assets.Items[asset].Checked)
                {
                    try
                    {
                        queryjobassetUpdate = "Update PrismJobAssignedAssets set AssignmentStatus='InActive' where JobId=" + combo_job.SelectedValue + " and AssetId=" + combo_ex_assets.Items[asset].Value;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetUpdate);

                        string queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,AssignmentStatus)" +
                           " values('" + combo_newjob.SelectedValue + "','" + combo_ex_assets.Items[asset].Value + "','1','" + Session["userId"].ToString() + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','Active')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);

                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE AssetID=" + combo_ex_assets.Items[asset].Value + " order by AssetCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + combo_ex_assets.Items[asset].Value + "").Tables[0];
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
                        tolocationid = combo_newjob.SelectedValue.ToString();
                        assetstatus = "Available";

                        string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                            "" + combo_ex_assets.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);


                        string insertticketdetails = "insert into PrismShippingTicketDetails(TicketId,AssetId,FromLocation,FromLocationID,ToLocation,ToLocationID,Status)" +
                            "values(" + dt_TicketId.Rows[0]["ID"].ToString() + "," + combo_ex_assets.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," +
                            "" + tolocationid + ",'Pending Shipmet')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertticketdetails);

                        //string notificationsendtowhome = eventNotification.sendEventNotification("AC02");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "AC02", "JOB", combo_newjob.SelectedValue, combo_newjob.SelectedItem.Text,
                        //           "", "", "AssetReassigned", "", combo_ex_assets.Items[asset].Text);

                        //}

                        
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
            // Jobs with Personal Insertion
            for (int personal = 0; personal < combo_ex_personal.Items.Count; personal++)
            {
                if (combo_ex_personal.Items[personal].Checked)
                {
                    try
                    {
                        queryjobpersonalUpdate = "Update PrismJobAssignedPersonals set AssignmentStatus='InActive' where JobId=" + combo_job.SelectedValue + " and UserId =" + combo_ex_personal.Items[personal].Value;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalUpdate);

                        string queryjobpersonalInsert = "Insert into PrismJobAssignedPersonals(JobId,UserId,AssignmentStatus)" +
                           " values('" + combo_newjob.SelectedValue + "','" + combo_personal.Items[personal].Value + "','Active')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalInsert);

                        //string notificationsendtowhome = eventNotification.sendEventNotification("PA02");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "PA02", "JOB", combo_newjob.SelectedValue, combo_newjob.SelectedItem.Text,
                        //           "", "", "PersonReassigned", "", combo_ex_personal.Items[personal].Text);

                        //}                       

                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
            // Jobs with Service Insertion
            for (int personal = 0; personal < combo_ex_service.Items.Count; personal++)
            {
                if (combo_ex_service.Items[personal].Checked)
                {
                    try
                    {
                        queryjobserviceUpdate = "Update PrismJobServiceAssignment set JobId=" + combo_newjob.SelectedValue + " where JobId=" + combo_job.SelectedValue + " and ServiceID=" + combo_ex_service.Items[personal].Value;
                          
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobserviceUpdate);
                        //string notificationsendtowhome = eventNotification.sendEventNotification("SA02");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "SA02", "JOB", combo_newjob.SelectedValue, combo_newjob.SelectedItem.Text,
                        //           "", "", "ServiceReassigned", "", combo_ex_service.Items[personal].Text);

                        //}  
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
            transaction.Commit();
            lbl_message.ForeColor = Color.Green;
            hid_ticketid.Value = ticketid;
            lbl_message.Text = "Job Re-Assignment Successfully Completed with Tools and Created shipping ticket";
            btn_print.Visible = true;
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
            combo_job_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            hid_ticketid.Value = "0";
            transaction.Rollback();
            lbl_message.ForeColor = Color.Red;
            lbl_message.Text = "Job Re-Assignment Failed";
            btn_print.Visible = false;
        }
        combo_job_SelectedIndexChanged(null, null);
        btn_print.Visible = true;
    }
    protected void btn_print_OnClick(object sender, EventArgs e)
    {
        if (hid_ticketid.Value != "0")
        {
            DataTable dt_gettickets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismShippingTicket where TicketID=" + hid_ticketid.Value + "").Tables[0];


            string selectque = "select n.AssetName+'('+pa.SerialNumber+')' as AssetNameID,ptd.FromLocation,ptd.FromLocationID, " +
                        " ptd.ToLocation,ptd.ToLocationID,ptd.AssetId as AssetID,ptd.Status as AssetMainStatus,* from PrismShippingTicket pt,PrismShippingTicketDetails ptd, " +
                        " Prism_Assets pa,PrismAssetName n where  pt.ID=ptd.TicketId and pa.AssetName=n.Id and pa.Id=ptd.AssetId and pt.TicketID=" + hid_ticketid.Value + "";
            DataTable dt_ticketdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectque).Tables[0];
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.ScreenUpdating = false;
            excelApp.DisplayAlerts = false;
            string unique = DateTime.Now.ToString("MMddyyyyHHMMss");
            object misValue = System.Reflection.Missing.Value;
            if (File.Exists(Server.MapPath("ShippingTicketDocSheet_copy.xlsx")))
            {

                File.Delete(Server.MapPath("ShippingTicketDocSheet_copy.xlsx"));
            }
            File.Copy(Server.MapPath("ShippingTicketDocSheet.xlsx"), Server.MapPath("ShippingTicketDocSheet_copy.xlsx"));
            //Opening Excel file(myData.xlsx)
            Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("ShippingTicketDocSheet_copy.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
            string currentSheet = "Sheet1";
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;
            string strOperators = "", strOperators1 = "";
            int operatcol = 6;

            //Job Id
            excelWorksheet.Cells[6, 4] = dt_gettickets.Rows[0]["TicketID"].ToString();
            //KIt/AssetName
            excelWorksheet.Cells[8, 4] = dt_gettickets.Rows[0]["CreatedDate"].ToString();
            //Location
            excelWorksheet.Cells[6, 7] = dt_gettickets.Rows[0]["ShippingDate"].ToString();
            //Operator
            excelWorksheet.Cells[8, 7] = dt_gettickets.Rows[0]["Status"].ToString();


            //DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "").Tables[0];
            int row = 13;

            for (int r = 0; r < dt_ticketdetails.Rows.Count; r++)
            {

                string lbl_FromLocationAddress = "", lbl_JobName = "", lbl_ToLocationAddress = "", lbl_JobWarehouseName = "";
                if (dt_ticketdetails.Rows[r]["FromLocation"].ToString() == "Job")
                {
                    DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select JobLocation as Address,CurveGroupName as JobName from [RigTrack].tblCurveGroup where ID=" + dt_ticketdetails.Rows[r]["FromLocationID"].ToString() + "").Tables[0];
                    if (dtgetaddfromjob.Rows.Count > 0)
                    {
                        lbl_FromLocationAddress = dtgetaddfromjob.Rows[0]["Address"].ToString();
                        lbl_JobName = dtgetaddfromjob.Rows[0]["JobName"].ToString();
                    }
                }
                else if (dt_ticketdetails.Rows[r]["FromLocation"].ToString() == "WareHouse")
                {
                    DataTable dtgetaddfromwarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,Name from PrsimWarehouses where ID=" + dt_ticketdetails.Rows[r]["FromLocationID"].ToString() + "").Tables[0];
                    if (dtgetaddfromwarehouse.Rows.Count > 0)
                    {
                        lbl_FromLocationAddress = dtgetaddfromwarehouse.Rows[0]["Address"].ToString();
                        lbl_JobName = dtgetaddfromwarehouse.Rows[0]["Name"].ToString();
                    }
                }

                if (dt_ticketdetails.Rows[r]["ToLocation"].ToString() == "Job")
                {
                    DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select JobLocation as Address,CurveGroupName as JobName from [RigTrack].tblCurveGroup where ID=" + dt_ticketdetails.Rows[r]["ToLocationID"].ToString() + "").Tables[0];
                    if (dtgetaddfromjob.Rows.Count > 0)
                    {
                        lbl_ToLocationAddress = dtgetaddfromjob.Rows[0]["Address"].ToString();
                        lbl_JobWarehouseName = dtgetaddfromjob.Rows[0]["JobName"].ToString();
                    }
                }
                else if (dt_ticketdetails.Rows[r]["ToLocation"].ToString() == "WareHouse")
                {
                    DataTable dtgetaddfromwarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,Name from PrsimWarehouses where ID=" + dt_ticketdetails.Rows[r]["ToLocationID"].ToString() + "").Tables[0];
                    if (dtgetaddfromwarehouse.Rows.Count > 0)
                    {
                        lbl_ToLocationAddress = dtgetaddfromwarehouse.Rows[0]["Address"].ToString();
                        lbl_JobWarehouseName = dtgetaddfromwarehouse.Rows[0]["Name"].ToString();
                    }
                }

                int rownumber = row + r;
                int col1 = 3;
                int col2 = 4;
                int col3 = 5;
                int col4 = 6;
                int col5 = 7;
                int col6 = 8;
                excelWorksheet.Cells[rownumber, col1] = dt_ticketdetails.Rows[r]["AssetNameID"].ToString();
                excelWorksheet.Cells[rownumber, col2] = lbl_JobName;
                excelWorksheet.Cells[rownumber, col3] = lbl_FromLocationAddress;
                excelWorksheet.Cells[rownumber, col4] = lbl_JobWarehouseName;
                excelWorksheet.Cells[rownumber, col5] = lbl_ToLocationAddress;
                excelWorksheet.Cells[rownumber, col6] = dt_ticketdetails.Rows[r]["AssetMainStatus"].ToString();
            }



            workbook.Save();
            workbook.Close(true, misValue, misValue);
            excelApp.Quit();
            //Process[] pros = Process.GetProcesses();
            //for (int i = 0; i < pros.Count(); i++)
            //{
            //    if (pros[i].ProcessName.ToLower().Contains("excel"))
            //    {
            //        pros[i].Kill();
            //    }
            //}

            string FilePath = Server.MapPath("ShippingTicketDocSheet_copy.xlsx");
            FileInfo fileInfo = new FileInfo(FilePath);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.Flush();
            Response.WriteFile(fileInfo.FullName);
            Response.End();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        string queryjobassetInsert = "", queryjobpersonalInsert = "";
        try
        {
            //Jobs with Assets insertion
            for (int asset = 0; asset < combo_assets.Items.Count; asset++)
            {
                if (combo_assets.Items[asset].Checked)
                {
                    try
                    {
                        queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,AssignmentStatus)" +
                           " values('" + combo_job.SelectedValue + "','" + combo_assets.Items[asset].Value + "','1','" + Session["userId"].ToString() + "','" + DateTime.Now + "','Active')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);

                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE  AssetID=" + combo_assets.Items[asset].Value + " order by AssetCurrentLocationID desc";
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
                        //string notificationsendtowhome = eventNotification.sendEventNotification("AC02");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "AC02", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                        //           "", "", "Assetassigned", "", combo_assets.Items[asset].Text);

                        //}  
                    }
                    catch (Exception ex) {
                        lbl_message.Text = ex.Message;
                        transaction.Rollback(); }
                    finally
                    {
                        
                    }

                }
            }
            // Jobs with Personal Insertion
            for (int personal = 0; personal < combo_personal.Items.Count; personal++)
            {
                if (combo_personal.Items[personal].Checked)
                {
                    try
                    {
                        queryjobpersonalInsert = "Insert into PrismJobAssignedPersonals(JobId,UserId,AssignmentStatus)" +
                           " values('" + combo_job.SelectedValue + "','" + combo_personal.Items[personal].Value + "','Active')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalInsert);

                        //string notificationsendtowhome = eventNotification.sendEventNotification("PA01");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "PA01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                        //           "", "", "Assign", "", "");

                        //}


                    }
                    catch (Exception ex) { lbl_message.Text = ex.Message; transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
            // Jobs with Service Insertion
            for (int personal = 0; personal < combo_service.Items.Count; personal++)
            {
                if (combo_service.Items[personal].Checked)
                {
                    try
                    {
                        queryjobpersonalInsert = "Insert into PrismJobServiceAssignment(JobId,ServiceID)" +
                           " values('" + combo_job.SelectedValue + "','" + combo_service.Items[personal].Value + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalInsert);
                        //string notificationsendtowhome = eventNotification.sendEventNotification("JSA01");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JSA01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                        //           "", "", "Serviceassigned", "", combo_service.Items[personal].Text);

                        //}
                    }
                    catch (Exception ex) { lbl_message.Text = ex.Message; transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
            for (int c = 0; c < radcosumable.Items.Count; c++)
            {
                if (radcosumable.Items[c].Checked)
                {
                    string insert_conexist = "insert into PrismJobConsumables(jobid,consumableid,AssignedDate)values(" + combo_job.SelectedValue + "," + radcosumable.Items[c].Value + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_conexist);
                }
            }
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "Update [RigTrack].tblCurveGroup set LastModifyDate='" + DateTime.Now + "' where jid='" + combo_job.SelectedValue + "'");
            transaction.Commit();
            lbl_message.ForeColor = Color.Green;
            lbl_message.Text = "Job Assignment Successfully Completed with Tools";
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
            combo_job_SelectedIndexChanged(null, null);
          
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            lbl_message.ForeColor = Color.Red;
            lbl_message.Text = "Job assignment Failed";
        }
    }    
    public DataTable jobOrders(string startdate, string stopdate)
    {
        string query = "select  CurveGroupName as JOB,* from [RigTrack].tblCurveGroup" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') order by CreateDate desc";
        //string query = "select  JO.jid,JO.jobid,jo.jobname,JT.jobtype,JO.startdate,JO.enddate,JO.JobAssignedDate, (opuser.firstName+' '+opuser.lastName) as operationsManager,(puser.firstName+' '+puser.lastName) as projectManager,"+
        //    " JO.approveddatetime as JoborderDate,cost from manageJobOrders JO,jobTypes JT,Users opuser, Users puser where puser.userID = JO.programManagerId " +
        //    " and opuser.userID = JO.opManagerId and JO.jobtype=JT.jobtypeid and jid in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') order by JO.JobAssignedDate desc";
        return SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
    }
    //protected void grdJobList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    //if IsertAnother button is clicked 
    //    if (e.CommandName == "Cancel" || e.CommandName == "Edit")
    //    {
    //        grdJobList.DataSource = jobOrders("", "");
    //        grdJobList.DataBind();
    //    }
    //}
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource = jobOrders("", "");
        grdJobList.DataBind();
    }
    //protected void gridJobAssets_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
        
    //    GridDataItem parentItem = ((source as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;
    //    (source as RadGrid).DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
    //        "select A.AssetId,AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JA.AssetStatus "+
    //        " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,manageJobOrders MJ where AT.clientAssetID=A.AssetCategoryId and"+
    //        " WA.ID=A.WarehouseId  and MJ.jid=JA.JobId and JA.AssetId=A.Id and" +
    //        " JA.ModifiedBy=u.userID and JA.JobId=" + parentItem.GetDataKeyValue("jid").ToString() + "").Tables[0];
    //    lbl_message.Text = parentItem.GetDataKeyValue("jid").ToString();
        
    //}
    //protected void gridJobPersonals_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    GridDataItem parentItem = ((source as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;
    //    (source as RadGrid).DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
    //        "select (u.firstName+' '+u.lastName) as username from PrismJobAssignedPersonals PA,Users u where PA.UserId=u.UserId  and PA.JobId=" + parentItem.GetDataKeyValue("jid").ToString() + "").Tables[0];
    //    lbl_message.Text = parentItem.GetDataKeyValue("jid").ToString();
    //}
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
            //string query_getassets = "select (pn.AssetName+'('+pa.SerialNumber+')') assetdet from Prism_Assets pa,PrismAssetName pn,PrismJobAssignedKitAssetDetails pj,PrismJobAssignedAssets pja where " +
            //    "pa.Id=pj.assetid and pn.Id=pj.assetnameid and pja.JobId=pj.jobid and pa.id=pja.AssetId and pj.kitid=" + lbl_kitid.Text + " and pj.jobid=" + lbl_jobid.Text + "  and pa.repairstatus<>'Maintenance' and pja.AssignmentStatus='Active' ";
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
               // RadGrid RadGrid_con = (RadGrid)nestedItem.FindControl("RadGrid_con");
                RadGrid RadGrid_kits = (RadGrid)nestedItem.FindControl("RadGrid_kits");
                
            //    gridJobPersonals.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //"select (u.firstName+' '+u.lastName) as username,r.userRole from PrismJobAssignedPersonals PA,Users u,UserRoles r where u.userRoleID=r.userRoleID and PA.UserId=u.UserId  and PA.JobId=" + dataKeyValue + "").Tables[0];

            //    gridJobPersonals.DataBind();

                gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select MJ.ID,A.AssetId,PA.AssetName,SerialNumber,JA.KitName,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus   " +
            " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,[RigTrack].tblCurveGroup MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
            " WA.ID=A.WarehouseId  and MJ.ID=JA.JobId and JA.AssetId=A.Id  and A.AssetName=PA.ID And JAS.Id=JA.AssetStatus and" +
            " JA.ModifiedBy=u.userID and JA.JobId=" + dataKeyValue + " and JA.kitname is null and AssignmentStatus='Active'").Tables[0];
               gridJobAssets.DataBind();

          //     gridServices.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
          // "select * from PrismJobServiceAssignment PS,PrismService S where PS.ServiceID=S.ID  and PS.JobId=" + dataKeyValue + "").Tables[0];

          //     gridServices.DataBind();

          //     RadGrid_con.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
          //"select * from PrismJobConsumables Pc,manageJobOrders S,Consumables c where Pc.jobid=S.jid and Pc.consumableid=c.ConID and Pc.jobid=" + dataKeyValue + "").Tables[0];

          //     RadGrid_con.DataBind();

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
}