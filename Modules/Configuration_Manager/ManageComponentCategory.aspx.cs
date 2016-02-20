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
        lbl_message.Text = "";
    }
    protected void radgrdService_ItemCommand(object sender, GridCommandEventArgs e)
    {
        // Did the user click the "Edit" button?
        if (e.CommandName == "EditMeter")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("comp_categoryid"));
            // Select the item
            item.Selected = true;
            lblMode.Text = "Edit";
            hidden_serviceid.Value = dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM Prism_ComponentCategory where comp_categoryid=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {
        
        txt_description.Text = dt.Rows[0]["Description"].ToString();
        txt_servicename.Text = dt.Rows[0]["comp_categoryname"].ToString();
        //txt_servicename.Enabled = false;
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
    protected void radbtnSaveMeter_Click(object sender, EventArgs e)
    {

        if (lblMode.Text != "Edit")
        {
            try
            {
                string queryInsert = "Insert into Prism_ComponentCategory(comp_categoryname,Description) values('" + txt_servicename.Text + "','" + txt_description.Text + "')";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                lbl_message.Text = "Component Category Details Inserted Successfully";
                lbl_message.ForeColor = Color.Green;
                
            }
            catch (Exception ex) { }

        }
        else
        {
            try
            {
                string queryUpdate = "Update Prism_ComponentCategory set comp_categoryname='" + txt_servicename.Text + "',Description='" + txt_description.Text + "'" +
                    "  where comp_categoryid=" + hidden_serviceid.Value + "";

                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "Component Category Details Updated Successfully";
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