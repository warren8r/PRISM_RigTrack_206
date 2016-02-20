using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_RigTrack_ManageJobToolInventory : System.Web.UI.Page
{
    public string DBConnectionString
    {
        get { return Session["client_database"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        Response.ContentType = "text/css";
        if (!IsPostBack)
        {
            //ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            //ddlCurveGroup.DataTextField = "CurveGroupName";
            //ddlCurveGroup.DataValueField = "ID";
            //ddlCurveGroup.DataBind();

            SqlConnection sqlConn = new SqlConnection(DBConnectionString);

            // Instantiate a DataTable to hold the resultset
            DataTable dt = new DataTable();

            try
            {
                // Open the database connection
                sqlConn.Open();
                string query = "select top(10) p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
                 "Status,p.Cost as DailyCharge,p.runhrmaintenance as RunHrsToMaintenance,p.maintenancepercentage,p.previoususedhrs from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
                  " where p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID and P.AssetName=PPA.Id order by p.Id desc";
                // Create a SqlCommand with the SELECT statement to retrieve all the meters
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlConn))
                {
                    // Run the SELECT statement to fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                sqlConn.Close();
            }

            // Set the RADGrid's DataSource to the DataTable
            radgrdMeterList.DataSource = dt;
        }
    }
    protected void ddl_assetcategory_OnSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dt_getassetnamesbycat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrismAssetName where AssetCategoryId=" + ddl_assetcategory.SelectedValue + "").Tables[0];
        //ddl_assetname
        if (ddl_assetcategory.SelectedIndex != 0)
        {
            RadComboBoxFill.FillRadcombobox(ddl_assetname, dt_getassetnamesbycat, "AssetName", "Id", "0");
        }
    }
}