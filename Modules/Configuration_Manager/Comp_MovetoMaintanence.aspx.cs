using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;
using System.Data.SqlClient;
public partial class Modules_Configuration_Manager_Comp_Re_assignment : System.Web.UI.Page
{
    public static DataTable dt_Comp, dt_assetcomp, dt_compinfo;
    SqlConnection con = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction trans;
    DataRow[] row_comp = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        dt_assetcomp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select *,CC.CompID as componentid,AN.AssetName as Aname,AN.Id as assetsqid,CC.repairstatus as comrstatus,AA.Id as AID from PrismAssetComponents AC,Prism_Assets AA,PrismAssetName AN," +
                " clientAssets ACC,Prism_Components CC,Prism_ComponentNames CN " +
                " where AC.AssetId=AA.Id and AA.AssetName=AN.Id and ACC.clientAssetID=AN.AssetCategoryId and CC.Componentid=CN.componet_id and CC.CompID=AC.CompID  and AC.AssignmentStatus='Active' and AC.componentstatus<>'No'").Tables[0];
        dt_Comp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select *,CC.repairstatus as comrstatus from Prism_Components CC,Prism_ComponentNames CN where  CC.Componentid=CN.componet_id").Tables[0];
        if (!IsPostBack)
        {
            
            btn_viewasset_Click(null, null);
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        radcombo_rstatus.SelectedValue = "0";
        txt_assetserialno2.Text = "";
        txt_assetserialno.Text = "";
        combo_asset.SelectedValue = "0";
        dt_compinfo = dt_Comp;
        radgrid_repairstatus.DataSource = dt_Comp;
        radgrid_repairstatus.DataBind();
    }
    protected void btn_viewasset_Click(object sender, EventArgs e)
    {
        txt_assetserialno2.Text = "";
        radcombo_rstatus.SelectedIndex = 0;
        lbl_grid.Text = "";
        // string queryselect = "select * from Prism_Components CC,Prism_ComponentNames CN where  CC.Componentid=CN.componet_id";
        if (combo_asset.SelectedIndex > 0 && txt_assetserialno.Text == "")
        {
            // queryselect += " and CN.componet_id=" + combo_asset.SelectedValue;
            row_comp = dt_assetcomp.Select("assetsqid=" + combo_asset.SelectedValue);
            if (row_comp != null && row_comp.Length > 0)
            {
                dt_compinfo = row_comp.CopyToDataTable();
                radgrid_repairstatus.DataSource = dt_compinfo;
                radgrid_repairstatus.DataBind();
            }
            else
            {
                lbl_grid.Text = "No Records Found";
                radgrid_repairstatus.DataSource = null;
                radgrid_repairstatus.DataBind();
            }


        }
        else if (txt_assetserialno.Text != "")
        {
            //queryselect += " and CC.Serialno='" + txt_assetserialno.Text + "'";

            row_comp = dt_assetcomp.Select("assetsqid=" + combo_asset.SelectedValue + " and SerialNumber LIKE '%" + txt_assetserialno.Text + "%'");
            if (row_comp != null && row_comp.Length > 0)
            {
                dt_compinfo = row_comp.CopyToDataTable();
            }
            radgrid_repairstatus.DataSource = dt_compinfo;
            radgrid_repairstatus.DataBind();
        }
        else
        {
            dt_compinfo = dt_Comp;
            radgrid_repairstatus.DataSource = dt_Comp;
            radgrid_repairstatus.DataBind();
        }
        
       // dt_Comp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, queryselect).Tables[0];
       

    }
    protected void isChecked_CheckedChanged(object sender, EventArgs e)
    {
        if (hidd_acc.Value == "1")
        {
            string updatequery = "", insertquery = "", updatequery_Repair = "";
            GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
            Label lbl_compid = (Label)row.FindControl("lbl_compid");
            Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans = con.BeginTransaction();


                if (lbl_statuscheck.Text != "Ok")
                {
                    updatequery = "update Prism_Components set repairstatus='Ok' where Id=" + lbl_compid.Text + "";
                    DataTable dt_exist = SqlHelper.ExecuteDataset(trans, CommandType.Text,
                        "select max(ComponentRid) as ComponentRid from PrismComponentRepairStatus where Componentid=" + lbl_compid.Text + "").Tables[0];
                    updatequery_Repair = "update PrismComponentRepairStatus set repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' " +
                        " where ComponentRid=" + dt_exist.Rows[0]["ComponentRid"].ToString() + "";
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, updatequery_Repair);
                }
                else
                {
                    updatequery = "update Prism_Components set repairstatus='Maintenance' where CompID=" + lbl_compid.Text + "";
                    insertquery = "insert into PrismComponentRepairStatus(Componentid,repairdate,status)values(" + lbl_compid.Text + "," +
                            "'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Pending')";
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, insertquery);
                }

                int updatecnt = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, updatequery);
                if (updatecnt > 0)
                {
                    string notificationsendtowhome = eventNotification.sendEventNotification("ASR01");
                    if (notificationsendtowhome != "")
                    {
                        bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "ASR01", "ASSET", "", row["clientAssetName"].Text,
                            "", "", "Repair", lbl_statuscheck.Text, "");
                    }
                }
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
            }
        }
            dt_Comp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select *,CC.repairstatus as comrstatus from Prism_Components CC,Prism_ComponentNames CN where  CC.Componentid=CN.componet_id").Tables[0];

            radgrid_repairstatus.DataSource = dt_Comp;
            radgrid_repairstatus.DataBind();
        
        
    }
    protected void radgrid_repairstatus_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        radgrid_repairstatus.CurrentPageIndex = e.NewPageIndex;
        btn_viewasset_Click(null, null);
    }
    protected void radgrid_repairstatus_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            // Yes, get the item
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_assetname = (Label)item.FindControl("lbl_assetname");
            Label lbl_assetcategory = (Label)item.FindControl("lbl_assetcategory");
            Label lbl_assetserialno = (Label)item.FindControl("lbl_assetserialno");
            Label lbl_compid = (Label)item.FindControl("lbl_compid");
            Label lbl_statuscheck = (Label)item.FindControl("lbl_statuscheck");
            Label lbl_AID = (Label)item.FindControl("lbl_AID");
            
            CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            RadButton btn_maintain = (RadButton)item.FindControl("btn_maintain");

            switch (lbl_statuscheck.Text)
            {
                case "Ok":
                    {
                        lbl_statuscheck.ForeColor = Color.Green;
                        lbl_statuscheck.Visible = false;
                        // isChecked.Enabled = true;
                        btn_maintain.Enabled = true;
                        break;
                    }
                case "Maintenance":
                    {
                        lbl_statuscheck.ForeColor = Color.Red;
                        //isChecked.Enabled = false;
                        //isChecked.Checked = true;
                        btn_maintain.Enabled = false;
                        btn_maintain.Visible = false;
                        lbl_statuscheck.Text = "Under&#160;Maintenance";
                        break;
                    }
            }
            DataRow[] row_asset = dt_assetcomp.Select("CompID=" + lbl_compid.Text);
            if (row_asset.Length > 0)
            {
                lbl_AID.Text = row_asset[0]["AID"].ToString();
                lbl_assetname.Text = row_asset[0]["Aname"].ToString();
                lbl_assetcategory.Text = row_asset[0]["clientAssetName"].ToString();
                lbl_assetserialno.Text = row_asset[0]["SerialNumber"].ToString();
            }

        }
    }
    protected void radgrid_repairstatus_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        combo_asset.SelectedIndex = 0;
        txt_assetserialno.Text = "";
        DataTable dt_compinfo = null;
        // string queryselect = "select * from Prism_Components CC,Prism_ComponentNames CN where  CC.Componentid=CN.componet_id";
        if (radcombo_rstatus.SelectedValue != "")
        {
            string query="comrstatus='" + radcombo_rstatus.SelectedValue + "'";

            if (txt_assetserialno2.Text != "")
            {
               query+=" and Serialno LIKE '%" + txt_assetserialno2.Text + "%'";
               
            }
            row_comp = dt_Comp.Select(query);
            if (row_comp != null && row_comp.Length > 0)
            {
                dt_compinfo = row_comp.CopyToDataTable();
            }
          
        }
        else if (txt_assetserialno2.Text != "")
        {
            row_comp = dt_assetcomp.Select(" Serialno LIKE '%" + txt_assetserialno2.Text + "%'");
            if (row_comp != null && row_comp.Length > 0)
            {
                dt_compinfo = row_comp.CopyToDataTable();
            }
        }
        else
        {
            dt_compinfo = dt_Comp;
        }

        // dt_Comp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, queryselect).Tables[0];
        radgrid_repairstatus.DataSource = dt_compinfo;
        radgrid_repairstatus.DataBind();
        if (dt_compinfo != null)
            lbl_message.Text = "";
        else
            lbl_message.Text = "No Records exist";

    }
    protected void btn_maintain_Click(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((RadButton)sender).NamingContainer);
        Label lbl_compid = (Label)row.FindControl("lbl_compid");
        Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");
        Label lbl_AID = (Label)row.FindControl("lbl_AID");
        
        RadButton btn_maintain = (RadButton)row.FindControl("btn_maintain");
        string updatequery = "", updatequery_Repair = "", insertquery = "",updateAssetComponent="";
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            trans = con.BeginTransaction();


            if (lbl_statuscheck.Text != "Ok")
            {
                updatequery = "update Prism_Components set repairstatus='Ok' where Id=" + lbl_compid.Text + "";
                DataTable dt_exist = SqlHelper.ExecuteDataset(trans, CommandType.Text,
                    "select max(ComponentRid) as ComponentRid from PrismComponentRepairStatus where Componentid=" + lbl_compid.Text + "").Tables[0];
                updatequery_Repair = "update PrismComponentRepairStatus set repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' " +
                    " where ComponentRid=" + dt_exist.Rows[0]["ComponentRid"].ToString() + "";
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, updatequery_Repair);
            }
            else
            {

                updatequery = "update Prism_Components set repairstatus='Maintenance' where CompID=" + lbl_compid.Text + "";
                if (lbl_AID.Text != "")
                {
                    updateAssetComponent = "Update PrismAssetComponents set componentstatus='No' where CompID=" + lbl_compid.Text + " and  AssetId=" + lbl_AID.Text + "";
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, updateAssetComponent);
                }
                insertquery = "insert into PrismComponentRepairStatus(Componentid,repairdate,status)values(" + lbl_compid.Text + "," +
                        "'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Pending')";
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, insertquery);
            }

            int updatecnt = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, updatequery);
            if (updatecnt > 0)
            {
                string notificationsendtowhome = eventNotification.sendEventNotification("ASR01");
                if (notificationsendtowhome != "")
                {
                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "ASR01", "ASSET", "", row["clientAssetName"].Text,
                        "", "", "Repair", lbl_statuscheck.Text, "");
                }
            }
            trans.Commit();
            con.Close();
        }
        catch (Exception ex)
        {
            trans.Rollback();
        }
        dt_Comp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select *,CC.repairstatus as comrstatus from Prism_Components CC,Prism_ComponentNames CN where  CC.Componentid=CN.componet_id").Tables[0];

        radgrid_repairstatus.DataSource = dt_Comp;
        radgrid_repairstatus.DataBind();
        dt_assetcomp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select *,CC.CompID as componentid,AN.AssetName as Aname,AN.Id as assetsqid,CC.repairstatus as comrstatus,AA.Id as AID from PrismAssetComponents AC,Prism_Assets AA,PrismAssetName AN," +
                " clientAssets ACC,Prism_Components CC,Prism_ComponentNames CN " +
                " where AC.AssetId=AA.Id and AA.AssetName=AN.Id and ACC.clientAssetID=AN.AssetCategoryId and CC.Componentid=CN.componet_id and CC.CompID=AC.CompID  and AC.AssignmentStatus='Active' and AC.componentstatus<>'No'").Tables[0];
        dt_Comp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select *,CC.repairstatus as comrstatus from Prism_Components CC,Prism_ComponentNames CN where  CC.Componentid=CN.componet_id").Tables[0];
       
        btn_viewasset_Click(null, null);
    }
}