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
       
        if (!IsPostBack)
        {
            ddl_status.SelectedValue= "1";
            grdJobList.DataSource = grdJobList.DataSource = jobOrders(getSelectedAssets(), combo_job.SelectedValue, "1");//ddl_status.SelectedValue);
            grdJobList.DataBind();
            
        }
    }
    public string getSelectedAssets()
    {
        string strAssets = "", strAssets1 = "";
        for (int asset = 0; asset < combo_assets.Items.Count; asset++)
        {
            if (combo_assets.Items[asset].Checked)
            {
                strAssets1 += combo_assets.Items[asset].Value + ",";
            }
        }
        if (strAssets1 != "")
        {
            strAssets = strAssets1.Remove(strAssets1.Length - 1, 1);
        }
        else
        {
            strAssets = "";
        }
        return strAssets;
    }
    public DataTable jobOrders(string assets, string jobid, string status)
    {
        DataTable dt_JobAsset;
        string query = " select MJ.jobname,MJ.jid,JA.id as AssetAssignid,MJ.jobid,A.AssetId,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse," +
            " (u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JAS.StatusText as StatusText,JA.AssetStatus as StatusId," +
            " WA.primaryLatLong  as WarehouseGIS,MJ.primaryLatLong as JoborderGIS,GIs =  CASE JA.AssetStatus  WHEN '1'  THEN WA.primaryLatLong" +
             " WHEN '2' THEN WA.primaryLatLong     WHEN '3' THEN MJ.primaryLatLong END "+
             "  ,(MJ.primaryAddress1+', '+MJ.primaryAddress2+', '+MJ.primaryCity+', '+MJ.primaryState+', '+MJ.primaryCountry+'.') as Address "+
             "from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT," +
             " Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where  AT.clientAssetID=A.AssetCategoryId and A.Id=JA.AssetId and WA.ID=A.WarehouseId  and  " +
             "MJ.jid=JA.JobId and  JA.ModifiedBy=u.userID and JA.AssetStatus=JAS.Id and A.AssetName=PA.ID  ";

        if (jobid != "" && jobid != "0")
        {
            query += " and MJ.jid=" + jobid + "";
        }
        if (assets != "" && assets != "0")
        {
            query += " and A.Id in (" + assets + ")";
        }
        if (status != "" && status != "0")
        {
            query += " and JA.AssetStatus=" + status + "";
        }
        dt_JobAsset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        return dt_JobAsset;
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        grdJobList.DataSource = grdJobList.DataSource = jobOrders(getSelectedAssets(), combo_job.SelectedValue,ddl_status.SelectedValue);
        grdJobList.DataBind();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
    }
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource = jobOrders(getSelectedAssets(), combo_job.SelectedValue,ddl_status.SelectedValue);
        grdJobList.DataBind();
    }    
    protected void grdJobList_ItemCommand(object source, GridCommandEventArgs e)
    {

        //if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        //{

        //    GridDataItem item = e.Item as GridDataItem;

        //    if (!item.Expanded)
        //    {

        //        GridNestedViewItem nestedItem = (GridNestedViewItem)item.ChildItem;

        //        string dataKeyValue = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("jid"));

        //        RadGrid gridJobPersonals = (RadGrid)nestedItem.FindControl("gridJobPersonals");
        //        RadGrid gridJobAssets = (RadGrid)nestedItem.FindControl("gridJobAssets");
                
        //        gridJobPersonals.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //    "select (u.firstName+' '+u.lastName) as username from PrismJobAssignedPersonals PA,Users u where PA.UserId=u.UserId  and PA.JobId=" + dataKeyValue + "").Tables[0];

        //        gridJobPersonals.DataBind();

        //        gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //    "select MJ.jid,A.AssetId,AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus   " +
        //    " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS where AT.clientAssetID=A.AssetCategoryId and" +
        //    " WA.ID=A.WarehouseId  and MJ.jid=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and" +
        //    " JA.ModifiedBy=u.userID and JA.JobId=" + dataKeyValue + "").Tables[0];
        //       gridJobAssets.DataBind();



        //    }

        //}

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
            grdJobList.DataSource = jobOrders(getSelectedAssets(), combo_job.SelectedValue,ddl_status.SelectedValue);
            grdJobList.DataBind();
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders(getSelectedAssets(), combo_job.SelectedValue,ddl_status.SelectedValue);
            grdJobList.DataBind();
        }
    }
}