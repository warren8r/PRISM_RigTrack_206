using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_Configuration_Manager_AssetMaintanence : System.Web.UI.Page
{
    Label lbl_assetrid_new = new Label();
    Decimal sum = 0;
    string clientID;
    public static DataTable dtConsumables;
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_save.Visible = true;
        btn_savefinalize.Visible = true;
        search.Visible = true;
        dtConsumables = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Consumables").Tables[0];
        if (!IsPostBack)
        {
            
            radcombo_status.SelectedValue = "Pending";
            radtxt_start.SelectedDate = DateTime.Now.AddMonths(-1);
            radtxt_end.SelectedDate = DateTime.Now.AddDays(1);
            btn_view_Click(null, null);
            //string q = "";
            ////if (radcombo_status.SelectedValue == "Select All")
            ////{
            //q = "select a.AssetName as AssetFullName,pa.Id as PrismAssetId,* from PrismAssetRepairStatus s,Prism_Assets pa,PrismAssetName a where s.assetid=pa.Id and pa.AssetName=a.Id and s.status='Pending' order by AssetRid desc";
            ////}
            ////else
            ////{
            ////    q = "select a.AssetName as AssetFullName,pa.Id as PrismAssetId,* from PrismAssetRepairStatus s,Prism_Assets pa,PrismAssetName a where s.assetid=pa.Id and pa.AssetName=a.Id and s.status='" + radcombo_status.SelectedValue + "' and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "'  order by AssetRid desc";
            ////}
            //DataTable dtgetassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, q).Tables[0];
            //RadGrid_Assets.DataSource = dtgetassets;
            //RadGrid_Assets.DataBind();
            //RadGrid_Assets.DataSource = null;
            //RadGrid_Assets.DataBind();
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        txtassetserialno.Text = "";
        //radcombo_searchassets.SelectedValue = "0";
        //if (radcombo_status.SelectedValue == "Finished")
        //{
        //    btn_save.Enabled = false;
        //    btn_savefinalize.Enabled = false;
        //    btn_save.Visible = true;
        //    btn_savefinalize.Visible = true;
        //    RadGrid_Assets.Visible = true;
        //}
        //else
        //{
        //    btn_save.Enabled = true;
        //    btn_savefinalize.Enabled = true;
        //    btn_save.Visible = true;
        //    btn_savefinalize.Visible = true;
        //    search.Visible = true;
        //    RadGrid_Assets.Visible = true;
        //}
        string q="";
        if (radcombo_status.SelectedValue == "Select All")
        {
            q = "select a.AssetName as AssetFullName,pa.Id as PrismAssetId,* from PrismAssetRepairStatus s,Prism_Assets pa,PrismAssetName a where s.assetid=pa.Id and pa.AssetName=a.Id and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "' order by AssetRid desc";
        }
        else
        {
            q = "select a.AssetName as AssetFullName,pa.Id as PrismAssetId,* from PrismAssetRepairStatus s,Prism_Assets pa,PrismAssetName a where s.assetid=pa.Id and pa.AssetName=a.Id and s.status='" + radcombo_status.SelectedValue + "' and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "'  order by AssetRid desc";
        }
        DataTable dtgetassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, q).Tables[0];
        RadGrid_Assets.DataSource = dtgetassets;
        RadGrid_Assets.DataBind();
        radcombo_searchassets.DataSource = dtgetassets;
        radcombo_searchassets.DataBind();
        if (dtgetassets.Rows.Count > 0)
        {
            btn_save.Enabled = true;
            btn_savefinalize.Enabled = true;
            btn_save.Visible = true;
            btn_savefinalize.Visible = true;
            RadGrid_Assets.Visible = true;

        }
        else {
            btn_save.Enabled = false;
            btn_savefinalize.Enabled = false;
            btn_save.Visible = false;
            btn_savefinalize.Visible = false;
            RadGrid_Assets.Visible = true;
        }
    }
    protected void btn_search_OnClick(object sender, EventArgs e)
    {
        btn_save.Visible = true;
        btn_savefinalize.Visible = true;
        search.Visible = true;
        string q = "select a.AssetName as AssetFullName,pa.Id as PrismAssetId,* from PrismAssetRepairStatus s,Prism_Assets pa,PrismAssetName a where s.assetid=pa.Id and pa.AssetName=a.Id and s.assetid=" + radcombo_searchassets.SelectedValue + " and SerialNumber like '%" + txtassetserialno.Text + "%'";
        DataTable dtgetassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, q).Tables[0];
        RadGrid_Assets.DataSource = dtgetassets;
        RadGrid_Assets.DataBind();
        
    }
    public decimal getTotCost(DataTable dtasset)
    {
        decimal cost = 0;
        for (int i = 0; i < dtasset.Rows.Count; i++)
        {
            DataRow[] rowcons = dtConsumables.Select("ConID=" + dtasset.Rows[i]["ConId"].ToString());
            if (rowcons.Length > 0)
            {
                cost += WebUtility.NullToZero(rowcons[0]["ConCost"].ToString()) * WebUtility.NullToZero(dtasset.Rows[i]["qty"].ToString());
            }
        }
        return cost;
    }
    protected void RadGrid_Assets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;

            
            RadComboBox radcombo_consumables = (RadComboBox)item.FindControl("radcombo_consumables");
            Label lbl_assetrid = (Label)item.FindControl("lbl_assetrid");
            lbl_assetrid_new.Text = lbl_assetrid.Text;
            CheckBox chk_select_finalize = (CheckBox)item.FindControl("chk_select_finalize");
            Label lbl_totcost = (Label)item.FindControl("lbl_totcost");

            DataTable dt_existdet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where AssetRid=" + lbl_assetrid.Text + "").Tables[0];

            if (dt_existdet.Rows.Count > 0)
            {
                lbl_totcost.Text = getTotCost(dt_existdet).ToString();
                chk_select_finalize.Checked = true;
            }
            else
            {
                lbl_totcost.Text = "0.00";
                chk_select_finalize.Checked = false;
            }
            //DataTable dtgetsno = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where AssetName=" + lbl_assetid.Text + "").Tables[0];
            //if (dtgetsno.Rows.Count > 0)
            //{
            //    Label lbl_sno = (Label)item.FindControl("lbl_sno");
            //    lbl_sno.Text = dtgetsno.Rows[0]["SerialNumber"].ToString();
            //}
            ClientMaster userMaster = new ClientMaster();
            userMaster = (ClientMaster)Session["UserMasterDetails"];
            Label lbl_verifiedby = (Label)item.FindControl("lbl_verifiedby");
            lbl_verifiedby.Text = userMaster.FirstName + " " + userMaster.LastName;
            //RadGrid grid = radcombo_consumables.Items[0].FindControl("RadGrid1") as RadGrid;
            //grid.Rebind();
            //radcombo_consumables.DataBind();
            //DataTable dtgetcon = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT 0 AS [ConId], 'Select Consumable' AS [ConName] UNION select top(1) ConID,ConName from Consumables").Tables[0];
            //radcombo_consumables.DataSource = dtgetcon;
            //radcombo_consumables.DataBind();
            //RadGrid grid = radcombo_consumables.Items[0].FindControl("RadGrid1") as RadGrid;
            //DataTable dt_con = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Consumables").Tables[0];
            //grid.DataSource = dt_con;
            //grid.DataBind();
        }
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        //if (e.Item is GridDataItem)
        //{
        //    GridDataItem item = (GridDataItem)e.Item;
        //    Label lbl_conid = (Label)item.FindControl("lbl_conid");
        //    Label lbl_totcost = (Label)item.FindControl("lbl_totcost");
        //    Label lbl_ConCost = (Label)item.FindControl("lbl_ConCost");
            
            
            
        //    //Label lbl_assetrid = (Label)item.FindControl("lbl_assetrid");
        //    CheckBox chk_select = (CheckBox)item.FindControl("chk_select");
        //    TextBox txt_qty = (TextBox)item.FindControl("txt_qty");
        //    DataTable dt_existdet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where  ConId=" + lbl_conid.Text + " and AssetRid=" + lbl_assetrid_new.Text + "").Tables[0];
        //    if (dt_existdet.Rows.Count > 0)
        //    {
        //        chk_select.Checked = true;
        //        txt_qty.Text = dt_existdet.Rows[0]["qty"].ToString();
        //        lbl_totcost.Text = (WebUtility.NullToZero(lbl_ConCost.Text) * WebUtility.NullToZero(txt_qty.Text)).ToString();
        //    }
        //    else
        //    {
        //        lbl_totcost.Text = "0";
        //    }
        //    if (lbl_totcost.Text != "")
        //    {
        //        sum += Convert.ToDecimal(lbl_totcost.Text);
        //    }
        //    //lbl_ConCost.Text = lbl_ConCost.Text + ".00";
        //    //lbl_totcost.Text = lbl_totcost.Text + ".00";
        //}
        //else if (e.Item is GridFooterItem)
        //{


        //    GridFooterItem footer = (GridFooterItem)e.Item;
        //    Label lbl_footot = (Label)footer.FindControl("lbl_footot");
        //    lbl_footot.Text =sum.ToString();
        //    //(footer["ConCost"].FindControl("lbl_footot") as Label).Text = sum.ToString();
        //    //clientID = (footer["lbl_footot"].FindControl("lbl_footot") as Label).ClientID;
        //    sum = 0;
        //}
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        foreach (GridDataItem item in RadGrid_Assets.Items)
        {
            CheckBox chk_select_finalize = (CheckBox)item.FindControl("chk_select_finalize");
            Label lbl_assetrid = (Label)item.FindControl("lbl_assetrid");
            //TextBox txt_notes=(TextBox)item.FindControl("txt_notes");
            if (chk_select_finalize.Checked)
            {
                //RadComboBox radcombo_consumables = (RadComboBox)item.FindControl("radcombo_consumables");
                //RadGrid grid = radcombo_consumables.Items[0].FindControl("RadGrid1") as RadGrid;
                //foreach (GridDataItem newitem in grid.Items)
                //{
                //    CheckBox chk_select = (CheckBox)newitem.FindControl("chk_select");
                //    Label lbl_conid = (Label)newitem.FindControl("lbl_conid");
                //    TextBox txt_qty = (TextBox)newitem.FindControl("txt_qty");
                //    DataTable dt_exist = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where AssetRid=" + lbl_assetrid.Text + " and ConId=" + lbl_conid.Text + "").Tables[0];
                //    if (chk_select.Checked)
                //    {
                //        if (dt_exist.Rows.Count > 0)
                //        {
                //            if (dt_exist.Rows[0]["Pmaintenanceid"].ToString() != txt_qty.Text)
                //            {
                //                string str_update = "update PrismAssetMaintenanceDetails set qty=" + txt_qty.Text + " where Pmaintenanceid=" + dt_exist.Rows[0]["Pmaintenanceid"].ToString() + "";
                //                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_update);
                //            }
                //        }
                //        else
                //        {
                //            string strinsert = "";
                //            if (txt_qty.Text != "")
                //            {
                //                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,qty,updateddate)values(" + lbl_assetrid.Text + "," + lbl_conid.Text + "," + txt_qty.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                //            }
                //            else
                //            {
                //                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,updateddate)values(" + lbl_assetrid.Text + "," + lbl_conid.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                //            }
                //            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, strinsert);
                //        }
                        
                //        //PrismAssetMaintenanceDetails
                //    }
                //}
                //string updatedateandstatus = "update PrismAssetRepairStatus set  where AssetRid=" + lbl_assetrid.Text + "";
                //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatedateandstatus);
                lbl_message.Text = "Record Saved Successfully";
                lbl_message.ForeColor = Color.Green;
                
            }
        }
        btn_save.Visible = true;
        btn_savefinalize.Visible = true;
        search.Visible = true;
    }

    protected void btn_savefinalize_OnClick(object sender, EventArgs e)
    {
        foreach (GridDataItem item in RadGrid_Assets.Items)
        {
            CheckBox chk_select_finalize = (CheckBox)item.FindControl("chk_select_finalize");
            Label lbl_assetrid = (Label)item.FindControl("lbl_assetrid");
            //TextBox txt_notes = (TextBox)item.FindControl("txt_notes");
            Label lbl_prismassetid = (Label)item.FindControl("lbl_prismassetid");
            if (chk_select_finalize.Checked)
            {
                //RadComboBox radcombo_consumables = (RadComboBox)item.FindControl("radcombo_consumables");
                //RadGrid grid = radcombo_consumables.Items[0].FindControl("RadGrid1") as RadGrid;
                //foreach (GridDataItem newitem in grid.Items)
                //{
                //    CheckBox chk_select = (CheckBox)newitem.FindControl("chk_select");
                //    Label lbl_conid = (Label)newitem.FindControl("lbl_conid");
                //    TextBox txt_qty = (TextBox)newitem.FindControl("txt_qty");
                //    DataTable dt_exist = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where AssetRid=" + lbl_assetrid.Text + " and ConId=" + lbl_conid.Text + "").Tables[0];
                //    if (chk_select.Checked)
                //    {
                //        if (dt_exist.Rows.Count > 0)
                //        {
                //            if (dt_exist.Rows[0]["Pmaintenanceid"].ToString() != txt_qty.Text)
                //            {
                //                string str_update = "update PrismAssetMaintenanceDetails set qty=" + txt_qty.Text + " where Pmaintenanceid=" + dt_exist.Rows[0]["Pmaintenanceid"].ToString() + "";
                //                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_update);
                //            }
                //        }
                //        else
                //        {
                //            string strinsert = "";
                //            if (txt_qty.Text != "")
                //            {
                //                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,qty,updateddate)values(" + lbl_assetrid.Text + "," + lbl_conid.Text + "," + txt_qty.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                //            }
                //            else
                //            {
                //                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,updateddate)values(" + lbl_assetrid.Text + "," + lbl_conid.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                //            }
                //            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, strinsert);
                //        }
                //        //PrismAssetMaintenanceDetails
                //    }
                //}
                string updatedateandstatus = "update PrismAssetRepairStatus set status='Finished',repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetRid=" + lbl_assetrid.Text + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatedateandstatus);
                string updateprism_assets = "update Prism_Assets set LastMaintanenceDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',repairstatus='Ok' where Id=" + lbl_prismassetid.Text + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateprism_assets);
                lbl_message.Text = "Record Finalized Successfully";
                lbl_message.ForeColor = Color.Green;

                string q = "";
                if (radcombo_status.SelectedValue == "Select All")
                {
                    q = "select a.AssetName as AssetFullName,pa.Id as PrismAssetId,* from PrismAssetRepairStatus s,Prism_Assets pa,PrismAssetName a where s.assetid=pa.Id and pa.AssetName=a.Id and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "' order by AssetRid desc";
                }
                else
                {
                    q = "select a.AssetName as AssetFullName,pa.Id as PrismAssetId,* from PrismAssetRepairStatus s,Prism_Assets pa,PrismAssetName a where s.assetid=pa.Id and pa.AssetName=a.Id and s.status='" + radcombo_status.SelectedValue + "' and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "'  order by AssetRid desc";
                }
                DataTable dtgetassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, q).Tables[0];
                RadGrid_Assets.DataSource = dtgetassets;
                RadGrid_Assets.DataBind();
                radcombo_searchassets.DataSource = dtgetassets;
                radcombo_searchassets.DataBind();
            }
        }
        btn_save.Visible = true;
        btn_savefinalize.Visible = true;
        search.Visible = true;
        
    }
     protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        btn_save.Visible = false;
        btn_savefinalize.Visible = false;
        search.Visible = false;
        RadGrid_Assets.DataSource = null;
        //RadGrid_Assets.Visible = false;
        RadGrid_Assets.Visible = false;
        radcombo_status.SelectedValue = "Pending";
        txtassetserialno.Text = "";
    }
}