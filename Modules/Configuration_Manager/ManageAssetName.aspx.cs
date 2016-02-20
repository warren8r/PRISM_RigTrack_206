using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;
using System.Data;
public partial class Modules_Configuration_Manager_ManageManufracturer : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string categoryname = Request.QueryString["catname"].ToString();
        //string categoryvalue = Request.QueryString["catval"].ToString();
        lbl_assetcatname.Text = categoryname;
        lbl_message.Text = "";
    }
    protected void radgrdService_ItemCommand(object sender, GridCommandEventArgs e)
    {
        // Did the user click the "Edit" button?
        if (e.CommandName == "EditMeter")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("ID"));
            // Select the item
            item.Selected = true;
            lblMode.Text = "Edit";
            hidden_serviceid.Value = dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM [PrismAssetName] where ID=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {

        txt_description.Text = dt.Rows[0]["AssetDescription"].ToString();
        txt_servicename.Text = dt.Rows[0]["AssetName"].ToString();
        txt_servicename.Enabled = false;
    }
    public void clearSelection()
    {

        txt_description.Text = string.Empty;
        txt_servicename.Text = string.Empty;
        lblMode.Text = "Create";
    }
    protected void radgrdService_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        radgrdService.CurrentPageIndex = e.NewPageIndex;

    }
    public bool uniqueAssetNameInsert(string assetname)
    {
        bool status = true;
        DataTable dt_prismasset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrismAssetName").Tables[0];
        if (dt_prismasset.Rows.Count > 0)
        {
            DataRow[] row_asset = dt_prismasset.Select("AssetName='" + assetname + "'");
            if (row_asset.Length == 0)
                status = true;
            else
                status = false;
        }
        return status;
    }
    protected void radbtnSaveMeter_Click(object sender, EventArgs e)
    {
        string categoryvalue = Request.QueryString["catval"].ToString();
        if (lblMode.Text != "Edit")
        {
            try
            {
                if (uniqueAssetNameInsert(txt_servicename.Text))
                {
                    string queryInsert = "Insert into PrismAssetName(AssetName,AssetDescription,AssetCategoryId) values('" + txt_servicename.Text + "','" + txt_description.Text + "'," + categoryvalue + ")";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                    lbl_message.Text = "Asset Details Inserted Successfully";
                    lbl_message.ForeColor = Color.Green;
                }
                else
                {
                    lbl_message.Text = "Asset Name already exist try another";
                    lbl_message.ForeColor = Color.Red;
                }
            }
            catch (Exception ex) { }

        }
        else
        {
            try
            {
                string queryUpdate = "Update PrismAssetName set AssetName='" + txt_servicename.Text + "',AssetDescription='" + txt_description.Text + "'" +
                    "  where Id=" + hidden_serviceid.Value + "";

                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "Asset Details Updated Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            catch (Exception ex) { }
        }
        clearSelection();
        radgrdService.Rebind();
    }
    protected void radbtnCancelMeter_Click(object sender, EventArgs e)
    {
        clearSelection();
        lbl_message.Text = "";
    }
}