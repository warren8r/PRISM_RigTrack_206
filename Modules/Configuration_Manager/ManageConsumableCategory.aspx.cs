using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

using Telerik.Web.UI;

public partial class Modules_Configuration_Manager_ManageConsumableCategory : System.Web.UI.Page
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
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("ConCatID"));
            // Select the item
            item.Selected = true;
            lblMode.Text = "Edit";
            hidden_serviceid.Value = dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM ConsumableCategory where ConCatID=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {

        txt_description.Text = dt.Rows[0]["Description"].ToString();
        txt_Consumablecategory.Text = dt.Rows[0]["ConCatName"].ToString();
        //txt_servicename.Enabled = false;
    }
    public void clearSelection()
    {

        txt_description.Text = string.Empty;
        txt_Consumablecategory.Text = string.Empty;
        lblMode.Text = "Create";
    }
    protected void radgrdService_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        radgrdService.CurrentPageIndex = e.NewPageIndex;

    }
    protected void radbtnSaveMeter_Click(object sender, EventArgs e)
    {

         if (lblMode.Text != "Edit")
        {
            try
            {
                string queryInsert = "Insert into ConsumableCategory (ConCatName,Description) values('" + txt_Consumablecategory.Text + "','" + txt_description.Text + "')";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                lbl_message.Text = "Consumable Category Details Inserted Successfully";
                lbl_message.ForeColor = Color.Green;
                
            }
            catch (Exception ex) { }

        }
        else
        {
            try
            {
                string queryUpdate = "Update ConsumableCategory set ConCatName='" + txt_Consumablecategory.Text + "',Description='" + txt_description.Text + "'" +
                    "  where ConCatID=" + hidden_serviceid.Value + "";

                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "Consumable Category Details Updated Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            catch (Exception ex) { }
        }
        //clearSelection();
        radgrdService.Rebind();
    }
    protected void radbtnCancelMeter_Click(object sender, EventArgs e)
    {
        //clearSelection();
        lbl_message.Text = "";
    }
    }
  
