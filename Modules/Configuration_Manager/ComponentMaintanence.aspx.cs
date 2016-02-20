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
    Label lbl_ComponentRid_new = new Label();
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_save.Visible = false;
        btn_savefinalize.Visible = false;
        search.Visible = false;
        if (!IsPostBack)
        {
            radcombo_status.SelectedValue = "Pending";
            radtxt_start.SelectedDate = DateTime.Now.AddMonths(-1);
            radtxt_end.SelectedDate = DateTime.Now.AddDays(1);
            //RadGrid_Assets.DataSource = null;
            //RadGrid_Assets.DataBind();
            btn_view_Click(null, null);
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    
    {

        if (radcombo_status.SelectedValue == "Finished")
        {
            btn_save.Enabled = false;
            btn_savefinalize.Enabled = false;
            btn_save.Visible = true;
            btn_savefinalize.Visible = true;
        }
        else
        {
            btn_save.Enabled = true;
            btn_savefinalize.Enabled = true;
            btn_save.Visible = true;
            btn_savefinalize.Visible = true;
            search.Visible = true;
        }
        string query="";
        query = "select a.ComponentName as ComponentFullName,pa.CompID as PrismCompId,* from PrismComponentRepairStatus s,Prism_Components pa," +
            " Prism_ComponentNames a where s.Componentid=pa.CompID and pa.Componentid=a.componet_id  ";
        if (radcombo_status.SelectedValue == "Select All")
        {
            query+=" and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "'";
        }
        else
        {
            query += " and s.status='" + radcombo_status.SelectedValue + "' and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "'";
        }
        query += " order by s.repairfixdate desc";
        DataTable dtgetassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        RadGrid_Assets.DataSource = dtgetassets;
        RadGrid_Assets.DataBind();
        RadGrid_Assets.Visible = true;
        string search_query = "select distinct a.componet_id,a.ComponentName from PrismComponentRepairStatus s,Prism_Components pa," +
            " Prism_ComponentNames a where s.Componentid=pa.CompID and pa.Componentid=a.componet_id  ";
        DataTable dtgetassets_search = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, search_query).Tables[0];
        radcombo_searchassets.DataSource = dtgetassets_search;
        radcombo_searchassets.DataBind();
        txtassetserialno.Text = "";
        if (dtgetassets.Rows.Count > 0)
        {
            btn_save.Visible = true;
            btn_savefinalize.Visible = true;
            search.Visible = true;
        }
        else
        {
            btn_save.Visible = false;
            btn_savefinalize.Visible = false;
            search.Visible = false;
        }
        
    }
    protected void btn_search_OnClick(object sender, EventArgs e)
    {
        btn_save.Visible = true;
        btn_savefinalize.Visible = true;
        search.Visible = true;
        string q = "select a.ComponentName as ComponentFullName,pa.CompID as PrismCompId,* from PrismComponentRepairStatus s,Prism_Components pa,Prism_ComponentNames a where s.Componentid=pa.CompID and pa.Componentid=a.componet_id and a.componet_id=" + radcombo_searchassets.SelectedValue + " and Serialno like '%" + txtassetserialno.Text + "%'";
        DataTable dtgetassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, q).Tables[0];
        RadGrid_Assets.DataSource = dtgetassets;
        RadGrid_Assets.DataBind();
        RadGrid_Assets.Visible = true;
        
    }
    protected void RadGrid_Assets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;

            
            //RadComboBox radcombo_consumables = (RadComboBox)item.FindControl("radcombo_consumables");
            Label lbl_ComponentRid = (Label)item.FindControl("lbl_ComponentRid");

            //lbl_ComponentRid_new.Text = lbl_ComponentRid.Text;
            //CheckBox chk_select_finalize = (CheckBox)item.FindControl("chk_select_finalize");
            //DataTable dt_existdet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where ComponentRid=" + lbl_ComponentRid.Text + "").Tables[0];
            //if (dt_existdet.Rows.Count > 0)
            //{
            //    chk_select_finalize.Checked = true;
            //}
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
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_conid = (Label)item.FindControl("lbl_conid");
            //Label lbl_ComponentRid = (Label)item.FindControl("lbl_ComponentRid");
            CheckBox chk_select = (CheckBox)item.FindControl("chk_select");
            TextBox txt_qty = (TextBox)item.FindControl("txt_qty");
            DataTable dt_existdet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where  ConId=" + lbl_conid.Text + " and ComponentRid=" + lbl_ComponentRid_new.Text + "").Tables[0];
            if (dt_existdet.Rows.Count > 0)
            {
                chk_select.Checked = true;
                txt_qty.Text = dt_existdet.Rows[0]["qty"].ToString();
            }
        }
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        foreach (GridDataItem item in RadGrid_Assets.Items)
        {
            CheckBox chk_select_finalize = (CheckBox)item.FindControl("chk_select_finalize");
            Label lbl_ComponentRid = (Label)item.FindControl("lbl_ComponentRid");
            //TextBox txt_notes=(TextBox)item.FindControl("txt_notes");
            TextBox txt_cost = (TextBox)item.FindControl("txt_cost");
            if (chk_select_finalize.Checked)
            {
                //RadComboBox radcombo_consumables = (RadComboBox)item.FindControl("radcombo_consumables");
                //RadGrid grid = radcombo_consumables.Items[0].FindControl("RadGrid1") as RadGrid;
                //foreach (GridDataItem newitem in grid.Items)
                //{
                //    CheckBox chk_select = (CheckBox)newitem.FindControl("chk_select");
                //    Label lbl_conid = (Label)newitem.FindControl("lbl_conid");
                //    TextBox txt_qty = (TextBox)newitem.FindControl("txt_qty");
                //    DataTable dt_exist = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where ComponentRid=" + lbl_ComponentRid.Text + " and ConId=" + lbl_conid.Text + "").Tables[0];
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
                //                strinsert = "insert into PrismAssetMaintenanceDetails(ComponentRid,ConId,qty,updateddate)values(" + lbl_ComponentRid.Text + "," + lbl_conid.Text + "," + txt_qty.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                //            }
                //            else
                //            {
                //                strinsert = "insert into PrismAssetMaintenanceDetails(ComponentRid,ConId,updateddate)values(" + lbl_ComponentRid.Text + "," + lbl_conid.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                //            }
                //            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, strinsert);
                //        }
                        
                //        //PrismAssetMaintenanceDetails
                //    }
                //}
                string updatedateandstatus = "update PrismComponentRepairStatus set Cost='"+txt_cost.Text+"' where ComponentRid=" + lbl_ComponentRid.Text + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatedateandstatus);
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
            Label lbl_ComponentRid = (Label)item.FindControl("lbl_ComponentRid");
            //TextBox txt_notes = (TextBox)item.FindControl("txt_notes");
            Label lbl_prismassetid = (Label)item.FindControl("lbl_prismassetid");
            TextBox txt_cost = (TextBox)item.FindControl("txt_cost");
            if (chk_select_finalize.Checked)
            {
               
                string updatedateandstatus = "update PrismComponentRepairStatus set status='Finished',Final='True',Cost='" + txt_cost.Text + "',repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where ComponentRid=" + lbl_ComponentRid.Text + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatedateandstatus);
                string updateprism_assets = "update Prism_Components set LastMaintanenceDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',repairstatus='Ok' where CompID=" + lbl_prismassetid.Text + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateprism_assets);
                lbl_message.Text = "Record Finalized Successfully";
                lbl_message.ForeColor = Color.Green;
                string query = "";
                query = "select a.ComponentName as ComponentFullName,pa.CompID as PrismCompId,* from PrismComponentRepairStatus s,Prism_Components pa," +
                    " Prism_ComponentNames a where s.Componentid=pa.CompID and pa.Componentid=a.componet_id  ";
                if (radcombo_status.SelectedValue == "Select All")
                {
                    query += " and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "'";
                }
                else
                {
                    query += " and s.status='" + radcombo_status.SelectedValue + "' and repairdate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_end.SelectedDate + "'";
                }
                query += " order by s.repairfixdate desc";
                DataTable dtgetassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
                RadGrid_Assets.DataSource = dtgetassets;
                RadGrid_Assets.DataBind();
                if (dtgetassets.Rows.Count > 0)
                {
                    btn_save.Visible = true;
                    btn_savefinalize.Visible = true;
                    search.Visible = true;
                }
                else {
                    btn_save.Visible = false;
                    btn_savefinalize.Visible = false;
                    search.Visible = false;
                }
            }
        }
       
    }
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        btn_save.Visible = false;
        btn_savefinalize.Visible = false;
        search.Visible = false;
        radcombo_status.SelectedValue = "Pending";
        RadGrid_Assets.DataSource = null;
        //RadGrid_Assets.DataBind();
        RadGrid_Assets.Visible = false;
        string search_query = "select distinct a.componet_id,a.ComponentName from PrismComponentRepairStatus s,Prism_Components pa," +
            " Prism_ComponentNames a where s.Componentid=pa.CompID and pa.Componentid=a.componet_id  ";
        DataTable dtgetassets_search = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, search_query).Tables[0];
        radcombo_searchassets.DataSource = dtgetassets_search;
        radcombo_searchassets.DataBind();
        txtassetserialno.Text = "";
        radcombo_searchassets.SelectedValue = "0";
    }
   
}