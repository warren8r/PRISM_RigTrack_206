using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Telerik.Web.UI;
using System.Diagnostics;

public partial class Modules_Configuration_Manager_confMngrSetAssetLabels : System.Web.UI.Page
{
    public void Page_Load(object sender, EventArgs e)
    {
    }

    #region Event Handlers

    protected void radgrdAssetNames_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "EditAsset")
        {
            GridDataItem item = (GridDataItem)e.Item;
            item.Selected = true;
            hdnID.Value = item["clientAssetID"].Text;

            // Get the selected asset and show it in the edit panel
            RefreshSelectedAsset(Convert.ToInt32(hdnID.Value));

            // Make the edit asset panel visible
            panEditAsset.Visible = true;
        }
    }

    protected void radgrdAssetNames_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lblActive = (Label)item.FindControl("lblActive");

            // if the asset category name is disabled we will gray out the row for the user
            if (lblActive != null
                && lblActive.Text == "In-Active")
            {
                // Set the row background to gray
                item.Style.Add("background-color", "#CECECE");

                // Set the text color to red
                lblActive.Style.Add("color", "Red");
            }
        }

    }

    protected void chkEditActive_CheckedChanged(object sender, EventArgs e)
    {
        // Set the text that appears next to the editable Active checkbox to match its checked state
        SetEditStatusAttribs();
    }

    public void btnSaveAssetName_Click(object sender, EventArgs e)
    {
        int rtn = 0;
        try
        {
            // Set the sqlds Update statement to the value in the hidden client asset label and update() it
            rtn = sqlClientAssetsUpdate.Update();
        }
        catch (Exception ex)
        { 
        }

        if (rtn > 0)
        {
            // Hide the edit asset panel
            panEditAsset.Visible = false;

            // Clear out the selected asset fields
            ClearSelectedAsset();

            // Notify the user
            PopupNotify("Asset Category Name changes were saved");

            // Refresh the client assets grid
            RefreshClientAssetsGrid();
        }
    }

    protected void btnCancelAssetName_Click(object sender, EventArgs e)
    {
        // Hide the edit asset panel
        panEditAsset.Visible = false;

        // Clear out the selected asset fields
        ClearSelectedAsset();

        // Clear selected on the grid
        radgrdAssetNames.SelectedIndexes.Clear();
    }

    protected void sqlClientAssets_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        // Call the master page's function to update the Manage Assets child items to reflect the changes made on this page.
        Master.RefreshManageAssetsMenu();
    }

    #endregion

    #region Functions

    private void ClearSelectedAsset()
    {
        chkEditActive.Checked = true;
        //chkEditActive.Text = GetStatusText(chkEditActive.Checked);

        SetEditStatusAttribs();

        radtxtEditAssetName.Text = string.Empty;
        radtxtEditRank.Text = string.Empty;
    }
    
    private DataTable GetSelectedAsset(int ID)
    {
        SqlCommand cmd = new SqlCommand(sqlClientAssetsUpdate.SelectCommand, new SqlConnection(sqlClientAssetsUpdate.ConnectionString));
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@ID", ID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            da.Fill(dt);
        }
        catch (Exception ex)
        { }

        return dt;
    }

    private void RefreshClientAssetsGrid()
    {
        // (Re-)Bind the Asset Labels grid
        radgrdAssetNames.DataBind();
    }

    private void RefreshSelectedAsset(int ID)
    {
        // Get the client asset row and put it into a DataTable
        DataTable dt = GetSelectedAsset(ID);

        // Clear out the client asset fields in the edit panel
        ClearSelectedAsset();

        if (dt.Rows.Count < 1)
        {
            return;
        }

        // Show the selected asset in the edit panel
        if (!Convert.IsDBNull(dt.Rows[0]["active"]))
        {
            chkEditActive.Checked = Convert.ToBoolean(dt.Rows[0]["active"]);
        }

        //chkEditActive.Text = GetStatusText(chkEditActive.Checked);

        SetEditStatusAttribs();

        if (!Convert.IsDBNull(dt.Rows[0]["clientAssetName"]))
        {
            radtxtEditAssetName.Text = dt.Rows[0]["clientAssetName"].ToString();
        }

        if (!Convert.IsDBNull(dt.Rows[0]["rank"]))
        {
            radtxtEditRank.Text = Convert.ToInt32(dt.Rows[0]["rank"]).ToString("#");
        }
    
    }

    private string GetStatusText(bool isActive)
    {
        // Set the Status text
        if (isActive)
        {
            return "Active";
        }
        else
        {
            return "In-Active";
        }
    }


    private void SetEditStatusAttribs()
    {
        lblEditActive.Text = GetStatusText(chkEditActive.Checked);

        if (!chkEditActive.Checked)
        {
            lblEditActive.Attributes.Add("style", "color: red;");
        }
        else 
        {
            lblEditActive.Attributes.Remove("style");
        }
    }

    private void PopupNotify(string msg)
    {
        // Popup a window to show the user the passed message
        n1.Title = "Set Asset Category Names Notice";
        n1.Text = msg;
        n1.Show();
    }

    #endregion
}