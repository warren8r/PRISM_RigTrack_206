using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

public partial class Modules_Configuration_Manager_ViewWinSurvData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            date_start.SelectedDate = DateTime.Now.AddMonths(-1);
            date_stop.SelectedDate = DateTime.Now.AddMonths(1);
            SqlGetJobs.SelectCommand = "select 0 as ID,'Select JobName' as jobname union select [ID],CurveGroupName as Jobname from[RigTrack].[tblCurveGroup] where isActive!= '0'";
            combo_job.DataBind();
            grdJobList.DataSource = jobOrders("", "",0);
            grdJobList.DataBind();
        }
    }
    protected void RadGridBHAInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (combo_job.SelectedValue == "0")
        {
            bindBHADetails(0);
        }
        else
        {
            bindBHADetails(Convert.ToInt32(combo_job.SelectedValue));
        }
    }
    public void bindBHADetails(int JOBID)
    {
        string query = "select J.CurveGroupName+' '+Convert(varchar,J.ID) as JobName,[BHANumber],[BHADesc],[BHAType],[BitSno],[BitDesc],[ODFrac]" +
",[BitLength],[Connection],[BitType],[BearingType],[BitMfg],[BitNumber],[NUMJETS],[InnerRow],[OuterRow],[DullChar],[Location],[BearingSeals],[Guage]" +
",[OtherDullChar],[ReasonPulled],[MotorDesc],[MotorMFG],[NBStabilizer],[Model],[Revolutions],[Bend],[RotorJet]" +
",[BittoBend],[PropBUR],[RealBUR],[PadOD],[AverageDifferential],[Lobes],[OffBottomDifference],[Stages],[StallPressure]" +
",[BittoSensor],[BittoGamma],[BittoResistivity],[BittoPorosity],[BittoDNSC],[BittoGyro],B.[CreateDate] as BHACreatedDate" +
" from [RigTrack].[tblBHADataInfo]B,[RigTrack].[tblCurveGroup] J where B.JOBID=J.ID";
        if (JOBID != 0)
            query += " and  j.ID=" + JOBID + "";



        DataTable dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        if (dt_users.Rows.Count > 0)
        {
            //DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ViewState["TargetID"].ToString()));
            RadGridBHAInfo.DataSource = dt_users;
            RadGridBHAInfo.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            RadGridBHAInfo.DataSource = dt;
        }
    }
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {

        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource = jobOrders("", "",0);
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
            string query_getassets = "select a.AssetName from PrismKitAssetFromKitName k,PrismAssetName a where a.ID=k.assetids and k.assetkitid=" + lbl_kitid.Text + " ";
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
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        info.Visible = true;
        grdJobList.DataSource = jobOrders("", "", Convert.ToInt32(combo_job.SelectedValue));
        grdJobList.DataBind();
        bindBHADetails(Convert.ToInt32(combo_job.SelectedValue));
        //radgrid_repairstatus.Rebind();
    }
    protected void date_stop_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {
        SqlGetJobs.SelectCommand = "select 0 as ID,'Select JobName' as jobname union select [ID],CurveGroupName as Jobname from[RigTrack].[tblCurveGroup] where isActive!= '0' and CreatedDate between '" + date_start.SelectedDate + "' and '" + date_stop.SelectedDate + "'";
        combo_job.DataBind();
    }
    protected void date_start_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {
        SqlGetJobs.SelectCommand = "select 0 as ID,'Select JobName' as jobname union select [ID],CurveGroupName as Jobname from[RigTrack].[tblCurveGroup] where isActive!= '0' and CreatedDate between '" + date_start.SelectedDate + "' and '" + date_stop.SelectedDate + "'";
        combo_job.DataBind();
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
    public DataTable jobOrders(string startdate, string stopdate,int jobid)
    {
        string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            " RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active')";
        if (jobid != 0)
        {
            query += " and JO.ID="+ jobid + "";
        }
        query += " order by JO.CreateDate desc";
        return SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
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
            grdJobList.DataSource = jobOrders("", "",0);
            grdJobList.DataBind();
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders("", "",0);
            grdJobList.DataBind();
        }
    }
}