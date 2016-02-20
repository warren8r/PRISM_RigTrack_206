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
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("sid"));
            // Select the item
            item.Selected = true;
            lblMode.Text = "Edit";
            hidden_serviceid.Value = dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM [RigStatusDet] where sid=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {
        
        rb_status.SelectedValue = dt.Rows[0]["Status"].ToString();
        txt_eventname.Text = dt.Rows[0]["rigstatuses"].ToString();
        txt_eventname.Enabled = false;
    }
    public void clearSelection()
    {

        rb_status.SelectedIndex = 0;
        txt_eventname.Text = string.Empty;
        lblMode.Text = "Create";
        txt_eventname.Enabled = true;
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
                string queryInsert = "Insert into RigStatusDet(rigstatuses,Status) values('" + txt_eventname.Text + "','" + rb_status.SelectedValue + "')";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                lbl_message.Text = "Event  Details Inserted Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            catch (Exception ex) { }

        }
        else
        {
            try
            {
                string queryUpdate = "Update RigStatusDet set rigstatuses='" + txt_eventname.Text + "',Status='" + rb_status.SelectedValue + "'" +
                    "  where Id=" + hidden_serviceid.Value + "";

                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "Event  Details Updated Successfully";
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
        lblMode.Text = "Create";
        txt_eventname.Enabled = true;
    }
}