using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_Configuration_Manager_AssetRptPopup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string assetid = Request.QueryString["AssetId"].ToString();
            binddataforassetcat(assetid);
        }
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footer = (GridFooterItem)e.Item;
            string strTotal = footer["TOTAL"].Text;
            footer["TOTAL"].Text = strTotal;
        }
    }
    public void binddataforassetcat(string assetid)
    {

        string assetids = "", str_assecatall = "", query_select = "";

        //str_assecatall = assetids.Remove(assetids.Length - 1, 1);
        query_select = "SELECT CAT.clientAssetID as AssetCategoryID1,REP.AssetRid as AssetRid,NAME.Id AS AssetNameID, ASSET.Id as AssetID, concate.ConCatID as ConCatID,REP.repairdate,REP.repairfixdate,REP.Notes," +
        " con.ConID as ConID, CAT.clientAssetName, NAME.AssetName, ASSET.SerialNumber, CONCATE.ConCatName, CON.ConName, CON.ConCost, MAIN.qty,(Convert(DECIMAL,CON.ConCost) * MAIN.qty) as Total FROM " +
            " Prism_assets ASSET, " +
            " PrismAssetMaintenanceDetails MAIN, " +
            " PrismAssetRepairStatus REP, " +
            " PrismAssetName NAME, " +
            "clientAssets CAT," +
            "ConsumableCategory CONCATE," +
            "Consumables CON" +
            " WHERE CAT.clientAssetID=NAME.AssetCategoryId AND NAME.Id=ASSET.AssetName AND ASSET.Id=REP.assetid AND REP.AssetRid=MAIN.AssetRid" +
            " AND MAIN.ConId=CON.ConID AND CON.ConCatID=CONCATE.ConCatID and ASSET.Id in (" + assetid + ")    order by REP.AssetRid desc";
        DataTable dt_assetdetfromcat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
        RadGrid1.GroupingEnabled = true;
        RadGrid1.DataSource = dt_assetdetfromcat;
        RadGrid1.DataBind();


    }
    protected void RadGrid1_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
    {
        RadGrid1.GroupingSettings.ShowUnGroupButton = true;
        if (e.Action == Telerik.Web.UI.GridGroupsChangingAction.Ungroup)
        {
            if (e.TableView.GroupByExpressions.Count == 1)
            {
                e.Canceled = true;
                RadGrid1.GroupingSettings.ShowUnGroupButton = false;
            }
            if (e.TableView.GroupByExpressions.Count == 2)
            {
                RadGrid1.GroupingSettings.ShowUnGroupButton = false;
            }
        }
        RadGrid1.Visible = true;
        string assetid = Request.QueryString["AssetId"].ToString();
        binddataforassetcat(assetid);
    }
    protected void RadGrid1_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.FileName = "Export";
        }
    }
}