using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_Configuration_Manager_AssetReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            radtxt_start.SelectedDate = DateTime.Now.AddMonths(-2);
            radtxt_end.SelectedDate = DateTime.Now.AddDays(2);
            RadGrid1.Visible = false;
            RadGrid2.Visible = false;
            RadGrid3.Visible = false;
            //RadGrid4.Visible = false;
        }

    }
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        RadGrid1.Visible = false;
        RadGrid2.Visible = false;
        RadGrid3.Visible = false;
        radcombo_consumables.DataBind();
        radcombo_componentcat.DataBind();
        radcombo_assetcat.DataBind();
        radcombo_type.SelectedValue = "Select";
        txt_sno.Text = "";

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
    protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footer = (GridFooterItem)e.Item;
            string strTotal = footer["TOTAL"].Text;
            footer["TOTAL"].Text = strTotal;
        }
    }
    protected void RadGrid3_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footer = (GridFooterItem)e.Item;
            string strTotal = footer["TOTAL"].Text;
            footer["TOTAL"].Text = strTotal;
        }
    }
   
    
    //if (e.Item is GridFooterItem)
    //{
    //    GridFooterItem footerItem = e.Item as GridFooterItem;
    //    footerItem["Quantity"].Text = "total: " + total.ToString();
    //}
    protected void btn_viewassetcat_OnClick(object sender, EventArgs e)
    {
        binddataforassetcat();

    }
    protected void btn_viewforcomponents_OnClick(object sender, EventArgs e)
    {
        binddataforcomponentcat();

    }
    protected void btn_viewsrno_OnClick(object sender, EventArgs e)
    {
        binddataforassetwithserialnumbers();

    }
    public void binddataforcomponentcat()
    {
        
        RadGrid1.Visible = false;
        RadGrid2.Visible = false;
        RadGrid3.Visible = true;
        txt_sno.Text = "";
        radcombo_type.SelectedValue = "Select";
        string assetids = "", str_assecatall = "", query_select = "";
        for (int assetcat = 0; assetcat < radcombo_componentcat.Items.Count; assetcat++)
        {
            if (radcombo_componentcat.Items[assetcat].Checked)
            {
                assetids += radcombo_componentcat.Items[assetcat].Value + ",";
            }
        }
        if (assetids != "")
        {
            str_assecatall = assetids.Remove(assetids.Length - 1, 1);
            query_select = "select rep.Cost as Total,pc.Componentid as MainComponentID,* from Prism_Components pc," +
            " PrismComponentRepairStatus rep, " +
                " Prism_ComponentNames Name, " +
                " Prism_ComponentCategory cat where cat.comp_categoryid=Name.comp_categoryid " +
                " and Name.componet_id=pc.Componentid and pc.CompID=rep.Componentid and  cat.comp_categoryid in (" + str_assecatall + ")  order by rep.repairfixdate desc";
            DataTable dt_assetdetfromcat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            RadGrid3.GroupingEnabled = true;
            RadGrid3.DataSource = dt_assetdetfromcat;
            RadGrid3.DataBind();
            RadGrid1.Visible = false;
            RadGrid2.Visible = false;
        }
    }
    public void binddataforassetcat()
    {
        radcombo_consumables.DataBind();
        RadGrid1.Visible = true;
        RadGrid2.Visible = false;
        RadGrid3.Visible = false;
        txt_sno.Text = "";
        radcombo_type.SelectedValue = "Select";
        string assetids = "", str_assecatall = "", query_select = "";
        for (int assetcat = 0; assetcat < radcombo_assetcat.Items.Count; assetcat++)
        {
            if (radcombo_assetcat.Items[assetcat].Checked)
            {
                assetids += radcombo_assetcat.Items[assetcat].Value + ",";
            }
        }
        if (assetids != "")
        {
            str_assecatall = assetids.Remove(assetids.Length - 1, 1);
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
                " AND MAIN.ConId=CON.ConID AND CON.ConCatID=CONCATE.ConCatID and CAT.clientAssetID in (" + str_assecatall + ")    order by REP.AssetRid desc";
            DataTable dt_assetdetfromcat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            RadGrid1.GroupingEnabled = true;
            RadGrid1.DataSource = dt_assetdetfromcat;
            RadGrid1.DataBind();
            RadGrid2.Visible = false;
        }
    }
    public void binddataforassetwithserialnumbers()
    {
        if (radcombo_type.SelectedValue == "Asset")
        {
            RadGrid1.Visible = true;
            RadGrid3.Visible = false;
            string query_select = "SELECT CAT.clientAssetID as AssetCategoryID1,REP.AssetRid as AssetRid, NAME.Id AS AssetNameID, ASSET.Id as AssetID, concate.ConCatID as ConCatID,REP.repairdate,REP.repairfixdate,REP.Notes," +
            " con.ConID as ConID, CAT.clientAssetName, NAME.AssetName, ASSET.SerialNumber, CONCATE.ConCatName, CON.ConName, CON.ConCost, MAIN.qty,(Convert(DECIMAL,CON.ConCost) * MAIN.qty) as Total FROM " +
                " Prism_assets ASSET, " +
                " PrismAssetMaintenanceDetails MAIN, " +
                " PrismAssetRepairStatus REP, " +
                " PrismAssetName NAME, " +
                "clientAssets CAT," +
                "ConsumableCategory CONCATE," +
                "Consumables CON" +
                " WHERE CAT.clientAssetID=NAME.AssetCategoryId AND NAME.Id=ASSET.AssetName AND ASSET.Id=REP.assetid AND REP.AssetRid=MAIN.AssetRid" +
                " AND MAIN.ConId=CON.ConID AND CON.ConCatID=CONCATE.ConCatID and ASSET.SerialNumber like '%" + txt_sno.Text.Trim().TrimEnd().TrimStart() + "%'   order by REP.AssetRid desc";
            DataTable dt_assetdetfromcat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            RadGrid1.GroupingEnabled = true;
            RadGrid1.DataSource = dt_assetdetfromcat;
            RadGrid1.DataBind();
        }
        else if (radcombo_type.SelectedValue == "Component")
        {
            RadGrid3.Visible = true;
            RadGrid1.Visible = false;
            string query_select = "select rep.Cost as Total,pc.Compid as MainComponentId,* from Prism_Components pc," +
            " PrismComponentRepairStatus rep, " +
                " Prism_ComponentNames Name, " +
                " Prism_ComponentCategory cat where cat.comp_categoryid=Name.comp_categoryid " +
                " and Name.componet_id=pc.Componentid and pc.CompID=rep.Componentid and  pc.Serialno like '%" + txt_sno.Text.Trim().TrimEnd().TrimStart() + "%'";
            DataTable dt_assetdetfromcat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            RadGrid3.GroupingEnabled = true;
            RadGrid3.DataSource = dt_assetdetfromcat;
            RadGrid3.DataBind();
        }
        
    }
    
    protected void RadGrid2_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
    {
        RadGrid2.GroupingSettings.ShowUnGroupButton = true;
        if (e.Action == Telerik.Web.UI.GridGroupsChangingAction.Ungroup)
        {
            if (e.TableView.GroupByExpressions.Count == 1)
            {
                e.Canceled = true;
                RadGrid2.GroupingSettings.ShowUnGroupButton = false;
            }
            if (e.TableView.GroupByExpressions.Count == 2)
            {
                RadGrid2.GroupingSettings.ShowUnGroupButton = false;
            }
        }
        compbind();
    }
    protected void RadGrid3_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
    {
        RadGrid3.GroupingSettings.ShowUnGroupButton = true;
        if (e.Action == Telerik.Web.UI.GridGroupsChangingAction.Ungroup)
        {
            if (e.TableView.GroupByExpressions.Count == 1)
            {
                e.Canceled = true;
                RadGrid3.GroupingSettings.ShowUnGroupButton = false;
            }
            if (e.TableView.GroupByExpressions.Count == 2)
            {
                RadGrid3.GroupingSettings.ShowUnGroupButton = false;
            }
        }
        binddataforcomponentcat();
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
        binddataforassetcat();
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
    
    protected void RadGrid3_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.FileName = "Components";
        }
    }
    protected void RadGrid2_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.FileName = "ConsumableCategory";
        }
    }
    protected void btn_conview_OnClick(object sender, EventArgs e)
    {

        compbind();
    }
    public void compbind()
    {
        radcombo_assetcat.DataBind();
        RadGrid1.Visible = false;
        RadGrid2.Visible = true;
        RadGrid3.Visible = false;
        string assetids = "", str_assecatall = "", query_select = "";
        for (int assetcat = 0; assetcat < radcombo_consumables.Items.Count; assetcat++)
        {
            if (radcombo_consumables.Items[assetcat].Checked)
            {
                assetids += radcombo_consumables.Items[assetcat].Value + ",";
            }
        }
        if (assetids != "")
        {
            str_assecatall = assetids.Remove(assetids.Length - 1, 1);
            query_select = "SELECT REP.AssetRid,CAT.clientAssetID as AssetCategoryID1, NAME.Id AS AssetNameID, ASSET.Id as AssetID, concate.ConCatID as ConCatID,REP.repairdate,REP.repairfixdate,REP.Notes," +
            " con.ConID as ConID, CAT.clientAssetName, NAME.AssetName, ASSET.SerialNumber, CONCATE.ConCatName, CON.ConName, CON.ConCost, MAIN.qty,(Convert(DECIMAL,CON.ConCost) * MAIN.qty) as Total FROM " +
                " Prism_assets ASSET, " +
                " PrismAssetMaintenanceDetails MAIN, " +
                " PrismAssetRepairStatus REP, " +
                " PrismAssetName NAME, " +
                "clientAssets CAT," +
                "ConsumableCategory CONCATE," +
                "Consumables CON" +
                " WHERE CAT.clientAssetID=NAME.AssetCategoryId AND NAME.Id=ASSET.AssetName AND ASSET.Id=REP.assetid AND REP.AssetRid=MAIN.AssetRid" +
                " AND MAIN.ConId=CON.ConID AND CON.ConCatID=CONCATE.ConCatID and con.ConID in (" + str_assecatall + ")";
            DataTable dt_condetfromcat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            RadGrid2.DataSource = dt_condetfromcat;
            RadGrid2.DataBind();
        }
    }

}