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

public partial class Modules_Configuration_Manager_Comp_Maintanence : System.Web.UI.Page
{
    public static DataTable dt_Comp, dt_assetcomp, dt_compinfo;
    SqlConnection con = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction trans;
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        if (!IsPostBack)
        {
        //    dt_assetcomp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //        "select *,AN.AssetName as Aname,AN.Id as assetsqid,CC.repairstatus as comrstatus from PrismAssetComponents AC,Prism_Assets AA,PrismAssetName AN," +
        //        " clientAssets ACC,Prism_Components CC,Prism_ComponentNames CN " +
        //        " where AC.AssetId=AA.Id and AA.AssetName=AN.Id and ACC.clientAssetID=AN.AssetCategoryId and CC.CompID=CN.componet_id and CC.CompID=AC.CompID").Tables[0];
            SqlDataSource1.SelectCommand = "select *,AN.AssetName as Aname,AN.Id as assetsqid,CC.repairstatus as comrstatus ,"+
                " CC.Type as CType, CC.Make as CMake, CC.Cost as CCost from PrismAssetComponents AC,Prism_Assets AA,PrismAssetName AN," +
                " clientAssets ACC,Prism_Components CC,Prism_ComponentNames CN " +
                " where AC.AssetId=AA.Id and AA.AssetName=AN.Id and ACC.clientAssetID=AN.AssetCategoryId and CC.CompID=CN.componet_id and CC.CompID=AC.CompID  and AC.AssignmentStatus='Active' ";
        }
    }
    protected void ddl_assetcategory_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {        
        SqlAssetName.SelectCommand = "SELECT 0 AS [AID], 'Select Asset Name' AS [AssetName] UNION  select AN.Id as AID,AN.AssetName from PrismAssetName AN "+
         " where  AN.AssetCategoryId=" + ddl_assetcategory.SelectedValue;
    }
    protected void ddl_asset_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlAssetserialno.SelectCommand = "SELECT  Id as AID, SerialNumber from Prism_Assets  where  AssetName=" + ddl_asset.SelectedValue;
    }
    protected void ddl_assetserialno_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlComponent.SelectCommand = "select ComponentName,componet_id  from PrismAssetComponents AC,Prism_Assets AA,PrismAssetName AN," +
              " clientAssets ACC,Prism_Components CC,Prism_ComponentNames CN  where AC.AssetId=AA.Id and AA.AssetName=AN.Id and ACC.clientAssetID=AN.AssetCategoryId and" +
              " CC.CompID=CN.componet_id and CC.CompID=AC.CompID and AC.AssignmentStatus='Active' and  AC.AssetId=" + ddl_assetserialno.SelectedValue;
        //SqlAssetserialnoto

    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        string query = "";
        query = "select *,AN.AssetName as Aname,AN.Id as assetsqid,CC.repairstatus as comrstatus ,CC.Type as CType, CC.Make as CMake," +
               " CC.Cost as CCost from PrismAssetComponents AC,Prism_Assets AA,PrismAssetName AN,clientAssets ACC,Prism_Components CC," +
               " Prism_ComponentNames CN  where AC.AssetId=AA.Id and AA.AssetName=AN.Id and ACC.clientAssetID=AN.AssetCategoryId and " +
               " CC.CompID=CN.componet_id and CC.CompID=AC.CompID  and AC.AssignmentStatus='Active' ";
        if (ddl_assetcategory.SelectedIndex > 0)
        {
            query += " and AN.AssetCategoryId="+ddl_assetcategory.SelectedValue+"";
        }
        if (ddl_asset.SelectedIndex > 0)
        {
            query += " and AN.Id="+ddl_asset.SelectedValue+"";
        }
        if (ddl_assetserialno.SelectedIndex > 0)
        {
            query += " and AA.Id="+ddl_assetserialno.SelectedValue+"";
        }
        if (ddl_component.SelectedIndex > -1)
        {
            query += " and CC.CompID=" + ddl_component.SelectedValue + "";
        }
        SqlDataSource1.SelectCommand = query;
    }

    protected void btn_movewarehouse_Click(object sender, EventArgs e)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            trans = con.BeginTransaction();
            string updateAssetComponent = "Update PrismAssetComponents set componentstatus='No' where CompID=" + ddl_component.SelectedValue + " and  AssetId=" + ddl_assetserialno.SelectedValue + "";
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, updateAssetComponent);
            trans.Commit();
            con.Close();
            lbl_message.Text = "Component Moved to Warehouse Successfully";
            lbl_message.ForeColor = Color.Green;
        }
        catch (Exception ex) {
            trans.Rollback();
        }

    }
    protected void btn_move_Click(object sender, EventArgs e)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            trans = con.BeginTransaction();
            //string queryremoverepair = "update PrismComponentRepairStatus set repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',status='InActive' where status='Active' and Componentid=" + ddl_component.SelectedValue;
            //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, queryremoverepair);
            //string queryInsertrepair = "Insert into PrismComponentRepairStatus(Componentid,repairdate,status) values (" + ddl_component.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Active')";
            //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, queryInsertrepair);

            string queryremoverel = "update PrismAssetComponents set Modifieddate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',AssignmentStatus='InActive'" +
                " where AssetId='"+ddl_assetserialno.SelectedValue+"' and CompID=" + ddl_component.SelectedValue;
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, queryremoverel);

            string queryInsertrel = "Insert into PrismAssetComponents(AssetId,CompID,AssignmentStatus,Assigneddate) values"+
                " (" + ddl_serial_to.SelectedValue + "," + ddl_component.SelectedValue + ",'Active','" +  DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, queryInsertrel);

            lbl_message.Text = "Component Moved to other location successfully";
            lbl_message.ForeColor = Color.Green;
            trans.Commit();
            con.Close();
        }
        catch (Exception ex)
        {
            lbl_message.Text = "Component Moved to other location Failed";
            lbl_message.ForeColor = Color.Red;
            trans.Rollback();
        }
    }
    protected void ddl_cat_to_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlAssetName.SelectCommand = "SELECT 0 AS [AID], 'Select Asset Name' AS [AssetName] UNION  select AN.Id as AID,AN.AssetName from PrismAssetName AN " +
        " where  AN.AssetCategoryId=" + ddl_cat_to.SelectedValue;
    }
    protected void ddl_asset_to_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlAssetserialnoto.SelectCommand = "SELECT  Id as AID, SerialNumber from Prism_Assets  where  AssetName=" + ddl_asset_to.SelectedValue;
    }
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Comp_Re_assignment.aspx");
        //ddl_assetcategory.SelectedValue = "0";
        //ddl_asset.SelectedValue = "0";
        //ddl_assetserialno.SelectedValue = "Select Asset Serial#";
        //ddl_assetserialno.DataBind();
      //  ddl_component.SelectedValue = "0";
      //  ddl_cat_to.SelectedValue = "0";
      //  ddl_asset_to.SelectedValue = "0";
      //ddl_serial_to.SelectedValue = "Select Asset Serial#";
      //SqlAssetserialnoto.SelectCommand = "SELECT  Id as AID, SerialNumber from Prism_Assets  where  AssetName=" + ddl_asset_to.SelectedValue;
        // ddl_serial_to.DataBind();
    }
}