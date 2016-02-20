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
    protected void Page_Load(object sender, EventArgs e)
    {

        // combo_job.SelectedIndex = 1;
        if (!IsPostBack)
        {
            date_end.SelectedDate = DateTime.Now;
            date_start.SelectedDate = DateTime.Now.AddDays(-60);
            grdJobList.DataSource = grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
            grdJobList.DataBind();
        }
    }
    //protected void btn_save_Click(object sender, EventArgs e)
    //{
    //    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    //    SqlTransaction transaction;
    //    db.Open();
    //    transaction = db.BeginTransaction();
    //    string queryjobassetInsert = "", queryjobpersonalInsert = "";
    //    try
    //    {
    //        //Jobs with Assets insertion
    //        for (int asset = 0; asset < combo_assets.Items.Count; asset++)
    //        {
    //            if (combo_assets.Items[asset].Checked)
    //            {
    //                try
    //                {
    //                    queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate)" +
    //                       " values('" + combo_job.SelectedValue + "','" + combo_assets.Items[asset].Value + "','Active','" + Session["userId"].ToString() + "','" + DateTime.Now + "')";
    //                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);
    //                }
    //                catch (Exception ex) { transaction.Rollback(); }
    //                finally
    //                {
    //                }

    //            }
    //        }
    //        // Jobs with Personal Insertion
    //        for (int personal = 0; personal < combo_personal.Items.Count; personal++)
    //        {
    //            if (combo_personal.Items[personal].Checked)
    //            {
    //                try
    //                {
    //                    queryjobpersonalInsert = "Insert into PrismJobAssignedPersonals(JobId,UserId)" +
    //                       " values('" + combo_job.SelectedValue + "','" + combo_personal.Items[personal].Value + "')";
    //                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobpersonalInsert);
    //                }
    //                catch (Exception ex) { transaction.Rollback(); }
    //                finally
    //                {
    //                }

    //            }
    //        }
    //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "Update manageJobOrders set JobAssignedDate='" + DateTime.Now + "' where jid='" + combo_job.SelectedValue + "'");
    //        transaction.Commit();
    //        lbl_message.ForeColor = Color.Green;
    //        lbl_message.Text = "Job Assignment Successfully Completed with Asset(s) and Personal(s)";
    //        grdJobList.DataSource = jobOrders("", "");
    //        grdJobList.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        transaction.Rollback();
    //        lbl_message.ForeColor = Color.Red;
    //        lbl_message.Text = "Job assignment Failed";
    //    }
    //}
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        combo_job.SelectedValue = "0";
       
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
            string query_getassets = "select distinct (pn.AssetName+'('+pa.SerialNumber+')') assetdet,pa.Id,pja.Id as jobassetid from Prism_Assets pa,PrismAssetName pn,PrismJobAssignedKitAssetDetails pj,PrismJobAssignedAssets pja where " +
                "pa.Id=pj.assetid and pn.Id=pj.assetnameid and pja.JobId=pj.jobid and pa.id=pja.AssetId and pj.kitid=" + lbl_kitid.Text + " and pj.jobid=" + lbl_jobid.Text + " and pa.repairstatus<>'Maintenance' and pja.AssignmentStatus='Active' ";

            //string query_getassets = "select (pn.AssetName+'('+pa.SerialNumber+')') assetdet ,pa.Id,pja.Id as jobassetid from Prism_Assets pa,PrismAssetName pn,PrismJobAssignedKitAssetDetails pj where " +
            //    "pa.Id=pj.assetid and pn.Id=pj.assetnameid and pj.kitid=" + lbl_kitid.Text + " and pj.jobid=" + lbl_jobid.Text + " ";
            DataTable dt_getassetids = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_getassets).Tables[0];
            radgridkitassets.DataSource = dt_getassetids;
            radgridkitassets.DataBind();
            //string assets = "";
            //for (int i = 0; i < dt_getassetids.Rows.Count; i++)
            //{

            //}


        }
    }
    protected void gridJobAssets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        RadGrid grid = (RadGrid)sender;
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            DropDownList cmb = (DropDownList)item["StatusText"].Controls[0];
            string ss = cmb.SelectedValue;
        }
    }

    protected void gridJobAssets_ItemCommand(object source, GridCommandEventArgs e)
    {
        //Code for During Add,Edit can no happen and vice-versa 
        bool editMode = e.Item.OwnerTableView.OwnerGrid.EditIndexes.Count > 0;
        bool insertMode = e.Item.OwnerTableView.IsItemInserted;
        RadGrid gridJobAssets = (source as RadGrid);
        GridEditableItem item = e.Item as GridEditableItem;
        string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("AssetAssignid"));

        Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
        Label lbl_jid = (Label)item.FindControl("lbl_jid");


        if (e.CommandName == "Update")
        {

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                DropDownList cmb = (DropDownList)item["StatusText"].Controls[0];

                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update PrismJobAssignedAssets set" +
                    " ModifiedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',ModifiedBy='" + Session["userId"].ToString() + "',AssetStatus=" + cmb.SelectedValue + " where Id=" + dataKeyValue + "");


                string fromlocationtype = "", fromlocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetcurrentlocid = "";

                string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE  AssetID=" + lbl_assetid.Text + " order by AssetCurrentLocationID desc";
                DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];
                if (dt_existassetlocation.Rows.Count > 0)
                {
                    if (cmb.SelectedValue == "3")
                    {
                        assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                        currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                        currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                        assetstatus = "In Use";

                        string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                            " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' , AssetMovedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                    }
                    else
                    {
                        assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                        currentlocationtype = dt_existassetlocation.Rows[0]["FromLocationType"].ToString();
                        currentlocationid = dt_existassetlocation.Rows[0]["FromLocationID"].ToString();

                        assetstatus = "Available";

                        string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                            " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' ,AssetMovedDate='' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                    }

                }
                //}
            }

        }
        gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select MJ.jid,A.AssetId,JA.id as AssetAssignid,A.ID as Assetuniqueid,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JA.AssetStatus as StatusId   " +
            " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS,PrismAssetName PA  where AT.clientAssetID=A.AssetCategoryId and" +
            " WA.ID=A.WarehouseId  and MJ.jid=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and A.AssetName=PA.ID and" +
             " JA.ModifiedBy=u.userID and JA.JobId=" + lbl_jid.Text + " and JA.AssignMentStatus='Active'").Tables[0];
        gridJobAssets.DataBind();


    }
    public DataTable jobOrders(string startdate, string stopdate, string jobid)
    {
        string query = "select  JO.jid,JO.jobid,jo.jobname,JT.jobtype,JO.startdate,JO.enddate,JO.JobAssignedDate, (opuser.firstName+' '+opuser.lastName) as operationsManager,(puser.firstName+' '+puser.lastName) as projectManager," +
            " JO.approveddatetime as JoborderDate,cost from manageJobOrders JO,jobTypes JT,Users opuser, Users puser where puser.userID = JO.programManagerId " +
            " and opuser.userID = JO.opManagerId and JO.jobtype=JT.jobtypeid and jid in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active')"; //+
            //" and JO.JobAssignedDate between '" + startdate + "' and '" + stopdate + "'";
        if (jobid != "0" && jobid != "")
        {
            //query += " and JO.jid=" + jobid + "";
            query += " and JO.jid=" + jobid + "";
        }
        return SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
    }
    protected void grdJobList_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //if (e.Item is GridNestedViewItem)
        //{
        //    GridNestedViewItem item = e.Item as GridNestedViewItem;
        //}


    }
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
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
    protected void grdJobList_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        {

            GridDataItem item = e.Item as GridDataItem;

            if (!item.Expanded)
            {

                GridNestedViewItem nestedItem = (GridNestedViewItem)item.ChildItem;

                string dataKeyValue = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("jid"));

                RadGrid gridJobPersonals = (RadGrid)nestedItem.FindControl("gridJobPersonals");
                RadGrid gridJobAssets = (RadGrid)nestedItem.FindControl("gridJobAssets");
                RadGrid gridServices = (RadGrid)nestedItem.FindControl("gridServices");
                RadGrid RadGrid_con = (RadGrid)nestedItem.FindControl("RadGrid_con");
                RadGrid RadGrid_kits = (RadGrid)nestedItem.FindControl("RadGrid_kits");
                hidd_jobid.Value = dataKeyValue;
                gridJobPersonals.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select (u.firstName+' '+u.lastName) as username,r.userRole from PrismJobAssignedPersonals PA,Users u,UserRoles r where u.userRoleID=r.userRoleID and PA.UserId=u.UserId  and PA.JobId=" + dataKeyValue + "").Tables[0];

                gridJobPersonals.DataBind();

                gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
           "select MJ.jid,A.AssetId,JA.id as AssetAssignid,A.ID as Assetuniqueid,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JA.AssetStatus as StatusId   " +
           " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
           " WA.ID=A.WarehouseId  and MJ.jid=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and A.AssetName=PA.ID and " +
            " JA.ModifiedBy=u.userID and JA.JobId=" + dataKeyValue + " and JA.kitname is null and JA.AssignmentStatus='Active'").Tables[0];
                gridJobAssets.DataBind();
                gridServices.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        "select * from PrismJobServiceAssignment PS,PrismService S where PS.ServiceID=S.ID  and PS.JobId=" + dataKeyValue + "").Tables[0];

                gridServices.DataBind();

                RadGrid_con.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobConsumables Pc,manageJobOrders S,Consumables c where Pc.jobid=S.jid and Pc.consumableid=c.ConID and Pc.jobid=" + dataKeyValue + "").Tables[0];

                RadGrid_con.DataBind();

                RadGrid_kits.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobKits Pk,PrismAssetKitDetails k  where Pk.kitid=k.assetkitid and Pk.jobid=" + dataKeyValue + "").Tables[0];

                RadGrid_kits.DataBind();


            }

        }


    }
    protected void btn_kitstatus_OnClick(object sender, EventArgs e)
    {
        foreach (GridNestedViewItem item1 in grdJobList.MasterTableView.GetItems(GridItemType.NestedView)) // loop through the nested items of a NestedView Template 
        {
            RadGrid grid = (RadGrid)item1.FindControl("RadGrid_kits");
            RadComboBox radcombo_kitstatus = (RadComboBox)item1.FindControl("radcombo_kitstatus");
            string dataKeyValue = Convert.ToString(((GridDataItem)(item1.ParentItem)).GetDataKeyValue("jid"));
            foreach (GridDataItem i in grid.Items)
            {
                Label lbl_jobkitid = (Label)i.FindControl("lbl_jobkitid");
                CheckBox chk_select_kit = (CheckBox)i.FindControl("chk_select_kit");
                if (chk_select_kit.Checked)
                {
                string updateprismjobkits = "update PrismJobKits set KitDeliveryStatus=" + radcombo_kitstatus.SelectedValue + " where jobkitid=" + lbl_jobkitid.Text + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateprismjobkits);
                RadGrid radgridkitassets = (RadGrid)i.FindControl("radgridkitassets");
                Label lbl_kitname = (Label)i.FindControl("lbl_Userrole");
                
                    foreach (GridDataItem assetitems in radgridkitassets.Items)
                    {
                        Label lbl_assetid = (Label)assetitems.FindControl("lbl_assetid");
                        //Label lbl_assetid = (Label)assetitems.FindControl("lbl_assetid");

                        string updateprismjobassignedassets = "update PrismJobAssignedAssets set AssetStatus=" + radcombo_kitstatus.SelectedValue + " where jobid=" + dataKeyValue + " and AssetId=" + lbl_assetid.Text + " and KitName='" + lbl_kitname.Text + "'";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateprismjobassignedassets);

                        if (radcombo_kitstatus.SelectedValue == "3")
                        {
                            string updateeprismAssets = "update Prism_Assets set repairstatus='job' where ID=" + lbl_assetid.Text + "";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateeprismAssets);

                        }
                        else
                        {

                            string updateeprismAssets = "update Prism_Assets set repairstatus='Ok' where ID=" + lbl_assetid.Text + "";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateeprismAssets);

                        }
                        string fromlocationtype = "", fromlocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetcurrentlocid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE  AssetID=" + lbl_assetid.Text + " order by AssetCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];
                        if (dt_existassetlocation.Rows.Count > 0)
                        {
                            if (radcombo_kitstatus.SelectedValue == "3")
                            {
                                assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                                currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                                currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                                assetstatus = "In Use";

                                string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                                    " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' , AssetMovedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                            }
                            else
                            {

                                assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                                currentlocationtype = dt_existassetlocation.Rows[0]["FromLocationType"].ToString();
                                currentlocationid = dt_existassetlocation.Rows[0]["FromLocationID"].ToString();

                                assetstatus = "Available";

                                string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                                    " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' ,AssetMovedDate='' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);

                            }

                        }
                    }

                }
            }
            grid.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
           "select * from PrismJobKits Pk,PrismAssetKitDetails k  where Pk.kitid=k.assetkitid and Pk.jobid=" + dataKeyValue + "").Tables[0];

            grid.DataBind();
        }


    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        foreach (GridNestedViewItem item1 in grdJobList.MasterTableView.GetItems(GridItemType.NestedView)) // loop through the nested items of a NestedView Template 
        {
            RadGrid grid = (RadGrid)item1.FindControl("gridJobAssets");
            string dataKeyValue = Convert.ToString(((GridDataItem)(item1.ParentItem)).GetDataKeyValue("jid"));
            RadComboBox radcombo_type = (RadComboBox)item1.FindControl("radcombo_type");
            if (radcombo_type.SelectedValue != "")
            {
                foreach (GridDataItem i in grid.Items)
                {
                    CheckBox chk_select = (CheckBox)i.FindControl("chk_select");
                    Label AssetAssignid = (Label)i.FindControl("lbl_assetassignedid");
                    Label lbl_assetid = (Label)i.FindControl("lbl_assetid");
                    if (chk_select.Checked)
                    {
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update PrismJobAssignedAssets set" +
                        " ModifiedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',ModifiedBy='" + Session["userId"].ToString() + "',AssetStatus=" + radcombo_type.SelectedValue + " where Id=" + AssetAssignid.Text + "");
                        if (radcombo_type.SelectedValue == "3")
                        {
                            string updateeprismAssets = "update Prism_Assets set repairstatus='job' where ID=" + lbl_assetid.Text + "";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateeprismAssets);

                        }
                        else
                        {
                            string updateeprismAssets = "update Prism_Assets set repairstatus='Ok' where ID=" + lbl_assetid.Text + "";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateeprismAssets);
                        }


                        string fromlocationtype = "", fromlocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetcurrentlocid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE  AssetID=" + lbl_assetid.Text + " order by AssetCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];
                        if (dt_existassetlocation.Rows.Count > 0)
                        {
                            if (radcombo_type.SelectedValue == "3")
                            {
                                assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                                currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                                currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                                assetstatus = "In Use";

                                string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                                    " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' , AssetMovedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                            }
                            else
                            {
                                assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                                currentlocationtype = dt_existassetlocation.Rows[0]["FromLocationType"].ToString();
                                currentlocationid = dt_existassetlocation.Rows[0]["FromLocationID"].ToString();

                                assetstatus = "Available";

                                string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                                    " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' ,AssetMovedDate='' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                            }

                        }



                    }
                }
            }
            //grid.Rebind();
            grid.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
           "select MJ.jid,A.AssetId,JA.id as AssetAssignid,A.ID as Assetuniqueid,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JA.AssetStatus as StatusId   " +
           " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
           " WA.ID=A.WarehouseId  and MJ.jid=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and A.AssetName=PA.ID and " +
            " JA.ModifiedBy=u.userID and JA.JobId=" + dataKeyValue + " and JA.kitname is null").Tables[0];
            grid.DataBind();
            //radcombo_type.SelectedIndex=-1;
        }

        //GridNestedViewItem nesteditem = (GridNestedViewItem)grdJobList.MasterTableView.GetItems(GridItemType.NestedView)[0];
        //CheckBox chk_select = (CheckBox)nesteditem.FindControl("chk_select");

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
            grdJobList.DataSource = grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
            grdJobList.DataBind();
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
            grdJobList.DataBind();
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
        grdJobList.DataBind();
    }
}