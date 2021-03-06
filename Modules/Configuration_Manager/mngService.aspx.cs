﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
public partial class Modules_Configuration_Manager_mngService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblMode.Text = "Create";
        }
        txt_servicename.Enabled = true ;
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
            hidden_serviceid.Value=dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM [PrismService] where ID=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {
        txt_cost.Text=dt.Rows[0]["Cost"].ToString();
        txt_description.Text = dt.Rows[0]["ServiceDescription"].ToString();
        txt_servicename.Text = dt.Rows[0]["ServiceName"].ToString();
        txt_servicename.Enabled = false;
    }
    public void clearSelection()
    {
        txt_cost.Text = string.Empty;
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
                string queryInsert = "Insert into PrismService(ServiceName,ServiceDescription,Cost) values('" + txt_servicename.Text + "','" + txt_description.Text + "','" + txt_cost.Text + "')";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                lbl_message.Text = "Service Details Inserted Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            catch (Exception ex) { }

        }
        else
        {
            try
            {
                string queryUpdate = "Update PrismService set ServiceName='" + txt_servicename.Text + "',ServiceDescription='" + txt_description.Text + "',"+
                    " Cost='" + txt_cost.Text + "' where Id="+hidden_serviceid.Value+"";

                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "Service Details Updated Successfully";
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
    }
}