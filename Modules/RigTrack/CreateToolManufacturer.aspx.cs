using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_CreateToolManufacturer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM [RigTrack].[tblCreateToolManufacturer] where ID=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {

        //txt_description.Text = dt.Rows[0]["AssetDescription"].ToString();
        txt_servicename.Text = dt.Rows[0]["Manufacturer"].ToString();
        txt_servicename.Enabled = false;
    }
    public void clearSelection()
    {

        //txt_description.Text = string.Empty;
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
        DataTable dt_prismasset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from [RigTrack].[tblCreateToolManufacturer]").Tables[0];
        if (dt_prismasset.Rows.Count > 0)
        {
            DataRow[] row_asset = dt_prismasset.Select("Manufacturer='" + assetname + "'");
            if (row_asset.Length == 0)
                status = true;
            else
                status = false;
        }
        return status;
    }
    protected void radbtnSaveMeter_Click(object sender, EventArgs e)
    {
        //string categoryvalue = Request.QueryString["catval"].ToString();
        if (lblMode.Text != "Edit")
        {
            try
            {
                if (uniqueAssetNameInsert(txt_servicename.Text))
                {
                    string queryInsert = "Insert into [RigTrack].[tblCreateToolManufacturer](Manufacturer,CreateDate,LastModifyDate) values('" + txt_servicename.Text + "','" + DateTime.Now + "','"+DateTime.Now+"')";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                    lbl_message.Text = "Manufacturer Details Inserted Successfully";
                    lbl_message.ForeColor = Color.Green;
                }
                else
                {
                    lbl_message.Text = "Manufacturer Name already exist try another";
                    lbl_message.ForeColor = Color.Red;
                }
            }
            catch (Exception ex) { }

        }
        else
        {
            try
            {
                string queryUpdate = "Update [RigTrack].[tblCreateToolManufacturer] set Manufacturer='" + txt_servicename.Text + "'" +
                    "  where Id=" + hidden_serviceid.Value + "";

                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "Manufacturer Details Updated Successfully";
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