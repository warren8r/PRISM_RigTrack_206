using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
public partial class Modules_Configuration_Manager_AssetConsumableParts : System.Web.UI.Page
{
    Decimal sum = 0;
    Label lbl_assetrid_new = new Label();
    DataTable dt_existdet,dt_Assets;
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        if (Request.QueryString["Assetid"] != "")
        {
            lbl_assetrid_new.Text = Request.QueryString["Assetid"].ToString();
            
             //dt_Assets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Prism_Assets").Tables[0];
             //DataRow[] roasset = dt_Assets.Select("Id=" + lbl_assetrid_new.Text+" and repairstatus='Ok' ");
             if (Request.QueryString["status"].ToString() == "Finished")
             {
                 btn_save.Visible = false;
             }
             else
             {
                 btn_save.Visible = true;
             }
            // where  ConId=" + lbl_conid.Text + " and AssetRid=" + lbl_assetrid_new.Text + "").Tables[0];
          
        }
    }
    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_conid = (Label)item.FindControl("lbl_conid");
            Label lbl_totcost = (Label)item.FindControl("lbl_totcost");
            Label lbl_ConCost = (Label)item.FindControl("lbl_ConCost");



            //Label lbl_assetrid_new = (Label)item.FindControl("lbl_assetrid_new");
            CheckBox chk_select = (CheckBox)item.FindControl("chk_select");
            RadTextBox txt_qty = (RadTextBox)item.FindControl("txt_qty");
            dt_existdet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails").Tables[0];
            DataRow[] rowasset = dt_existdet.Select("ConId=" + lbl_conid.Text + " and AssetRid=" + lbl_assetrid_new.Text + "");
            if (rowasset.Length > 0)
            {
                chk_select.Checked = true;
                txt_qty.Text = rowasset[0]["qty"].ToString();
                lbl_totcost.Text = (WebUtility.NullToZero(lbl_ConCost.Text) * WebUtility.NullToZero(txt_qty.Text)).ToString();
            }
            else
            {
                lbl_totcost.Text = "0";
            }
            if (lbl_totcost.Text != "")
            {
                sum += Convert.ToDecimal(lbl_totcost.Text);
            }
            //lbl_ConCost.Text = lbl_ConCost.Text + ".00";
            //lbl_totcost.Text = lbl_totcost.Text + ".00";
        }
        else if (e.Item is GridFooterItem)
        {


            GridFooterItem footer = (GridFooterItem)e.Item;
            Label lbl_footot = (Label)footer.FindControl("lbl_footot");
            lbl_footot.Text = sum.ToString();
            //(footer["ConCost"].FindControl("lbl_footot") as Label).Text = sum.ToString();
            //clientID = (footer["lbl_footot"].FindControl("lbl_footot") as Label).ClientID;
            sum = 0;
        }
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        //foreach (GridDataItem item in RadGrid_Assets.Items)
        //{
        //    CheckBox chk_select_finalize = (CheckBox)item.FindControl("chk_select_finalize");
        //    Label lbl_assetrid_new = (Label)item.FindControl("lbl_assetrid_new");
        //    TextBox txt_notes = (TextBox)item.FindControl("txt_notes");
        //    if (chk_select_finalize.Checked)
        //    {
        //        RadComboBox radcombo_consumables = (RadComboBox)item.FindControl("radcombo_consumables");
        //        RadGrid grid = radcombo_consumables.Items[0].FindControl("RadGrid1") as RadGrid;
                foreach (GridDataItem newitem in RadGrid1.Items)
                {
                    CheckBox chk_select = (CheckBox)newitem.FindControl("chk_select");
                    Label lbl_conid = (Label)newitem.FindControl("lbl_conid");
                    RadTextBox txt_qty = (RadTextBox)newitem.FindControl("txt_qty");
                    DataTable dt_exist = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where AssetRid=" + lbl_assetrid_new.Text + " and ConId=" + lbl_conid.Text + "").Tables[0];
                    if (chk_select.Checked)
                    {
                        if (dt_exist.Rows.Count > 0)
                        {
                            if (dt_exist.Rows[0]["Pmaintenanceid"].ToString() != txt_qty.Text)
                            {
                                string str_update = "update PrismAssetMaintenanceDetails set qty=" + txt_qty.Text + " where Pmaintenanceid=" + dt_exist.Rows[0]["Pmaintenanceid"].ToString() + "";
                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_update);
                            }
                        }
                        else
                        {
                            string strinsert = "";
                            if (txt_qty.Text != "")
                            {
                                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,qty,updateddate)values(" + lbl_assetrid_new.Text + "," + lbl_conid.Text + "," + txt_qty.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                            }
                            else
                            {
                                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,updateddate)values(" + lbl_assetrid_new.Text + "," + lbl_conid.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                            }
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, strinsert);
                        }

                        //PrismAssetMaintenanceDetails
                    }
                }
                //string updatedateandstatus = "update PrismAssetRepairStatus set Notes='" + txt_notes.Text + "' where AssetRid=" + lbl_assetrid_new.Text + "";
                //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatedateandstatus);
                lbl_message.Text = "Record Saved Successfully";
                lbl_message.ForeColor = Color.Green;
                RadGrid1.Rebind();
        //    }
        //}
        btn_save.Visible = true;
        //btn_savefinalize.Visible = true;
       
    }

    protected void btn_savefinalize_OnClick(object sender, EventArgs e)
    {
        //foreach (GridDataItem item in RadGrid_Assets.Items)
        //{
        //    CheckBox chk_select_finalize = (CheckBox)item.FindControl("chk_select_finalize");
        //    Label lbl_assetrid_new = (Label)item.FindControl("lbl_assetrid_new");
        //    TextBox txt_notes = (TextBox)item.FindControl("txt_notes");
        //    Label lbl_prismassetid = (Label)item.FindControl("lbl_prismassetid");
        //    if (chk_select_finalize.Checked)
        //    {
        //        RadComboBox radcombo_consumables = (RadComboBox)item.FindControl("radcombo_consumables");
        //        RadGrid grid = radcombo_consumables.Items[0].FindControl("RadGrid1") as RadGrid;
                foreach (GridDataItem newitem in RadGrid1.Items)
                {
                    CheckBox chk_select = (CheckBox)newitem.FindControl("chk_select");
                    Label lbl_conid = (Label)newitem.FindControl("lbl_conid");
                    TextBox txt_qty = (TextBox)newitem.FindControl("txt_qty");
                    DataTable dt_exist = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetMaintenanceDetails where AssetRid=" + lbl_assetrid_new.Text + " and ConId=" + lbl_conid.Text + "").Tables[0];
                    if (chk_select.Checked)
                    {
                        if (dt_exist.Rows.Count > 0)
                        {
                            if (dt_exist.Rows[0]["Pmaintenanceid"].ToString() != txt_qty.Text)
                            {
                                string str_update = "update PrismAssetMaintenanceDetails set qty=" + txt_qty.Text + " where Pmaintenanceid=" + dt_exist.Rows[0]["Pmaintenanceid"].ToString() + "";
                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_update);
                            }
                        }
                        else
                        {
                            string strinsert = "";
                            if (txt_qty.Text != "")
                            {
                                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,qty,updateddate)values(" + lbl_assetrid_new.Text + "," + lbl_conid.Text + "," + txt_qty.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                            }
                            else
                            {
                                strinsert = "insert into PrismAssetMaintenanceDetails(AssetRid,ConId,updateddate)values(" + lbl_assetrid_new.Text + "," + lbl_conid.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                            }
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, strinsert);
                        }
                        //PrismAssetMaintenanceDetails
                    }
                }
                //string updatedateandstatus = "update PrismAssetRepairStatus set status='Finished',Notes='" + txt_notes.Text + "',repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetRid=" + lbl_assetrid_new.Text + "";
                //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatedateandstatus);
                //string updateprism_assets = "update Prism_Assets set LastMaintanenceDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',repairstatus='Ok' where Id=" + lbl_prismassetid.Text + "";
                //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateprism_assets);
                lbl_message.Text = "Record Finalized Successfully";
                lbl_message.ForeColor = Color.Green;
        //    }
        //}
        btn_save.Visible = true;
        //btn_savefinalize.Visible = true;
        
    }
}