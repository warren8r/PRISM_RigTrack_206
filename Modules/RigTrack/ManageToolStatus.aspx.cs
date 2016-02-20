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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
public partial class Modules_Configuration_Manager_JobAssignments : System.Web.UI.Page
{
    public DataTable dtShippingTicket = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        // combo_job.SelectedIndex = 1;
        if (!IsPostBack)
        {
            date_end.SelectedDate = DateTime.Now;
            date_start.SelectedDate = DateTime.Now.AddDays(-60);
            grdJobList.DataSource = grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
            grdJobList.DataBind();


            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            combo_job.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            combo_job.DataTextField = "CurveGroupName";
            combo_job.DataValueField = "ID";
            combo_job.DataBind();
        }
        //RegisterPostBackControl();
        dtShippingTicket = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select  *,pst.TicketId as Ticketno from  [dbo].[PrismShippingTicketDetails] ptd inner join [dbo].[PrismShippingTicket] pst on ptd.TicketId =pst.ID").Tables[0];
    }
    private void RegisterPostBackControl()
    {
        foreach (GridDataItem item in grdJobList.Items)
        {

            RadGrid gridJobAssets = (RadGrid)item.FindControl("gridJobAssets");
            //Button btn_print = item.FindControl("btn_print") as Button;
            if (gridJobAssets != null)
            {
                foreach (GridDataItem item1 in gridJobAssets.Items)
                {
                    Button btn_print = item1.FindControl("btn_print") as Button;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(btn_print);
                }
            }
            //LinkButton lnkFull = row.FindControl("lnkFull") as LinkButton;
            //ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
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
        ddlCompany.SelectedValue = "0";
        combo_job.Items.Clear();
        combo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
        grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
        grdJobList.DataBind();

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
            //string query_getassets = "select distinct (pn.AssetName+'('+pa.SerialNumber+')') assetdet,pa.Id,pja.Id as jobassetid from Prism_Assets pa,PrismAssetName pn,PrismJobAssignedKitAssetDetails pj,PrismJobAssignedAssets pja where " +
            //    "pa.Id=pj.assetid and pn.Id=pj.assetnameid and pja.JobId=pj.jobid and pa.id=pja.AssetId and pj.kitid=" + lbl_kitid.Text + " and pj.jobid=" + lbl_jobid.Text + " and pa.repairstatus<>'Maintenance' and pja.AssignmentStatus='Active' ";
            string query_getassets = "select a.AssetName from PrismKitAssetFromKitName k,PrismAssetName a where a.ID=k.assetids and k.assetkitid=" + lbl_kitid.Text + " ";

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
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            Button btnPrint = (Button)dataItem["TCPrint"].FindControl("btn_print");
            Label lbl_assetid = (Label)dataItem["TCSelect"].FindControl("lbl_assetid");
            DataRow[] rowprint = dtShippingTicket.Select("AssetId=" + lbl_assetid.Text + "");
            if (rowprint.Length > 0)
            {
                btnPrint.Text = "Print";
                btnPrint.Enabled = true;
                hid_ticketid.Value = rowprint[0]["Ticketno"].ToString();
            }
            else
            {
                btnPrint.Text = "No Ticket";
                btnPrint.Enabled = false;
            }
            // (dataItem["TCPrint"].FindControl("btn_print") as Button).Enabled=false;//.Text = "Custom text";  
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
            "select MJ.ID,A.AssetId,JA.id as AssetAssignid,A.ID as Assetuniqueid,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JA.AssetStatus as StatusId   " +
            " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,[RigTrack].tblCurveGroup MJ,PrsimJobAssetStatus JAS,PrismAssetName PA  where AT.clientAssetID=A.AssetCategoryId and" +
            " WA.ID=A.WarehouseId  and MJ.ID=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and A.AssetName=PA.ID and" +
             " JA.ModifiedBy=u.userID and JA.JobId=" + lbl_jid.Text + " and JA.kitname is null and JA.AssignMentStatus='Active'").Tables[0];
        gridJobAssets.DataBind();


    }
    public DataTable jobOrders(string startdate, string stopdate, string jobid)
    {
        string query = "select  * from [RigTrack].tblCurveGroup where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active')"; //+
            //" and JO.JobAssignedDate between '" + startdate + "' and '" + stopdate + "'";
        if (jobid != "0" && jobid != "")
        {
            //query += " and JO.jid=" + jobid + "";
            query += " and ID=" + jobid + "";
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

                string dataKeyValue = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("ID"));

                RadGrid gridJobPersonals = (RadGrid)nestedItem.FindControl("gridJobPersonals");
                RadGrid gridJobAssets = (RadGrid)nestedItem.FindControl("gridJobAssets");
                RadGrid gridServices = (RadGrid)nestedItem.FindControl("gridServices");
                RadGrid RadGrid_con = (RadGrid)nestedItem.FindControl("RadGrid_con");
                RadGrid RadGrid_kits = (RadGrid)nestedItem.FindControl("RadGrid_kits");
                hidd_jobid.Value = dataKeyValue;
            //    gridJobPersonals.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //"select (u.firstName+' '+u.lastName) as username,r.userRole from PrismJobAssignedPersonals PA,Users u,UserRoles r where u.userRoleID=r.userRoleID and PA.UserId=u.UserId  and PA.JobId=" + dataKeyValue + "").Tables[0];

            //    gridJobPersonals.DataBind();

                gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
           "select MJ.ID,A.AssetId,JA.id as AssetAssignid,A.ID as Assetuniqueid,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JA.AssetStatus as StatusId   " +
           " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,[RigTrack].tblCurveGroup MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
           " WA.ID=A.WarehouseId  and MJ.ID=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and A.AssetName=PA.ID and " +
            " JA.ModifiedBy=u.userID and JA.JobId=" + dataKeyValue + " and JA.kitname is null and JA.AssignmentStatus='Active'").Tables[0];
                gridJobAssets.DataBind();
                foreach (GridDataItem item1 in gridJobAssets.Items)
                {
                    Button btn_print = item1.FindControl("btn_print") as Button;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(btn_print);
                }
        //        gridServices.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //"select * from PrismJobServiceAssignment PS,PrismService S where PS.ServiceID=S.ID  and PS.JobId=" + dataKeyValue + "").Tables[0];

        //        gridServices.DataBind();

        //        RadGrid_con.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //    "select * from PrismJobConsumables Pc,manageJobOrders S,Consumables c where Pc.jobid=S.jid and Pc.consumableid=c.ConID and Pc.jobid=" + dataKeyValue + "").Tables[0];

        //        RadGrid_con.DataBind();

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
            string dataKeyValue = Convert.ToString(((GridDataItem)(item1.ParentItem)).GetDataKeyValue("ID"));
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
    public string GenerateNewAccountID(Int32 StartingKey)
    {
        //following SRP Example under a billion...
        Random rnd = new Random();
        return (string)rnd.Next(10000000, 21474836).ToString();
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        foreach (GridNestedViewItem item1 in grdJobList.MasterTableView.GetItems(GridItemType.NestedView)) // loop through the nested items of a NestedView Template 
        {
            RadGrid grid = (RadGrid)item1.FindControl("gridJobAssets");
            string dataKeyValue = Convert.ToString(((GridDataItem)(item1.ParentItem)).GetDataKeyValue("ID"));
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
                        string ticketid = GenerateNewAccountID(0);
                        string inserttickets = "insert into PrismShippingTicket(ShippingDate,Status,TicketId,CreatedDate,ModifiedDate)values('" + DateTime.Now.ToString("MM/dd/yyyy") + "','Open','" + ticketid + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "')";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, inserttickets);

                        DataTable dt_TicketId = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    PrismShippingTicket WHERE  ID = IDENT_CURRENT('PrismShippingTicket')").Tables[0];

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

                                string insertticketdetails = "insert into PrismShippingTicketDetails(TicketId,AssetId,FromLocation,FromLocationID,ToLocation,ToLocationID,Status)" +
                            "values(" + dt_TicketId.Rows[0]["ID"].ToString() + "," + lbl_assetid.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + fromlocationtype + "'," +
                            "" + fromlocationid + "," + radcombo_type.SelectedItem.Text + ")";
                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertticketdetails);
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
                                string insertticketdetails = "insert into PrismShippingTicketDetails(TicketId,AssetId,FromLocation,FromLocationID,ToLocation,ToLocationID,Status)" +
                            "values(" + dt_TicketId.Rows[0]["ID"].ToString() + "," + lbl_assetid.Text + ",'" + currentlocationtype + "'," + currentlocationid + ",'" + currentlocationtype + "'," +
                            "" + currentlocationid + "," + radcombo_type.SelectedItem.Text + ")";
                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertticketdetails);
                            }

                        }



                    }
                }
            }
            //grid.Rebind();
            grid.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
           "select MJ.ID,A.AssetId,JA.id as AssetAssignid,A.ID as Assetuniqueid,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JA.AssetStatus as StatusId   " +
           " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,[RigTrack].tblCurveGroup MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
           " WA.ID=A.WarehouseId  and MJ.ID=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and A.AssetName=PA.ID and " +
            " JA.ModifiedBy=u.userID and JA.JobId=" + dataKeyValue + " and JA.kitname is null and JA.AssignmentStatus='Active'").Tables[0];
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
        DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
        string ids = "";
        for (int i = 0; i < dtJobDetails.Rows.Count; i++)
        {
            ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
        }
        if (ids != "")
        {
            ids = ids.Remove(ids.Length - 1, 1);
            string query = "select  * from [RigTrack].tblCurveGroup where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and ID in(" + ids + ")";

            if (combo_job.SelectedValue != "0" && combo_job.SelectedValue != "")
            {
                //query += " and JO.jid=" + jobid + "";
                query += " and ID=" + combo_job.SelectedValue + "";
            }
            query += " order by CreateDate desc";
            DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            grdJobList.DataSource = dtJobs;
            grdJobList.DataBind();
        }
        else
        {
            grdJobList.DataSource = jobOrders(DatesbetweenDatatable.getdbFormateDateinput(date_start.SelectedDate.Value), DatesbetweenDatatable.getdbFormateDateinput(date_end.SelectedDate.Value), combo_job.SelectedValue);
            grdJobList.DataBind();
        }


    }
    protected void btn_print_Click(object sender, EventArgs e)
    {
       
        if (hid_ticketid.Value != "0" && hid_ticketid.Value != "")
        {
            DataTable dt_gettickets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismShippingTicket where TicketID=" + hid_ticketid.Value + "").Tables[0];

            if (dt_gettickets.Rows.Count > 0)
            {
                string selectque = "select n.AssetName+'('+pa.SerialNumber+')' as AssetNameID,ptd.FromLocation,ptd.FromLocationID, " +
                            " ptd.ToLocation,ptd.ToLocationID,ptd.AssetId as AssetID,ptd.Status as AssetMainStatus,* from PrismShippingTicket pt,PrismShippingTicketDetails ptd, " +
                            " Prism_Assets pa,PrismAssetName n where  pt.ID=ptd.TicketId and pa.AssetName=n.Id and pa.Id=ptd.AssetId and pt.TicketID=" + hid_ticketid.Value + "";
                DataTable dt_ticketdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectque).Tables[0];
                if (dt_ticketdetails.Rows.Count > 0)
                {
                    divPrintTicket.InnerHtml="<table style='font-size:9px'>";
                    
                    divPrintTicket.InnerHtml+="<tr><td>Ticket # : "+dt_gettickets.Rows[0]["TicketID"].ToString()+"</td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td style='height:10px'></td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td>Created Date : "+dt_gettickets.Rows[0]["CreatedDate"].ToString()+"</td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td style='height:10px'></td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td>Shipping Date : "+dt_gettickets.Rows[0]["ShippingDate"].ToString()+"</td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td style='height:10px'></td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td>Status : "+dt_gettickets.Rows[0]["Status"].ToString()+"</td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td style='height:20px'></td></tr>";

                    divPrintTicket.InnerHtml+="<tr><td>";
                    divPrintTicket.InnerHtml += "<table style='width:600px'><tr><td colspan='6' style='height:20px;border:solid 1px #2F4051;width:600px;text-align:middle;text-decoration:underline'> Asset Details</td></tr>";
                    divPrintTicket.InnerHtml+="<tr><td>Asset Name</td><td>From Location</td><td>From Location Address</td><td>To Location</td><td>To Location Address</td><td>Tool Status</td></tr>";
                    divPrintTicket.InnerHtml+="<tr>";
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
                        divPrintTicket.InnerHtml+="<td>"+dt_ticketdetails.Rows[r]["AssetNameID"].ToString()+"</td>";
                        divPrintTicket.InnerHtml+="<td>"+lbl_JobName+"</td>";
                        divPrintTicket.InnerHtml+="<td>"+lbl_FromLocationAddress+"</td>";
                        divPrintTicket.InnerHtml+="<td>"+lbl_JobWarehouseName+"</td>";
                        divPrintTicket.InnerHtml+="<td>"+lbl_ToLocationAddress+"</td>";
                        divPrintTicket.InnerHtml+="<td>"+dt_ticketdetails.Rows[r]["AssetMainStatus"].ToString()+"</td>";
                    }
                    divPrintTicket.InnerHtml+="</tr></table>";
                    divPrintTicket.InnerHtml+="</td></tr></table>";

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter stringWriter = new StringWriter();
                    HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                    //this.EnableViewState = false;
                    //Response.Charset = String.Empty;
                    divPrintTicket.RenderControl(htmlTextWriter);

                    StringReader stringReader = new StringReader(stringWriter.ToString());
                    Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(Doc);
                    PdfWriter.GetInstance(Doc, Response.OutputStream);

                    Doc.Open();
                    htmlparser.Parse(stringReader);
                    Doc.Close();
                    Response.Write(Doc);
                    Response.End();
                }

                    
            }

        }
    }
}
