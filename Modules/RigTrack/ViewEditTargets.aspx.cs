using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Telerik.Web.UI;

using System.Linq;

using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;



public partial class Modules_RigTrack_ViewEditTargets : System.Web.UI.Page
{

    protected void BtnEditSeletedGraph_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateTargets.aspx",true);
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }

    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        //((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;

        //if ((sender as CheckBox).Checked)
        //{
        //    //BtnEditTimesheet.CssClass = "button-medium";
        //    // BtnEditTimesheet.Enabled = true;
        //}
        //else
        //{
        //    // BtnEditTimesheet.CssClass = "button-medium-disabled";
        //    //BtnEditTimesheet.Enabled = false;
        //}

        //foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        //{
        //    if ((dataItem.FindControl("CheckBox1") as CheckBox) != (sender as CheckBox))
        //    {
        //        (dataItem.FindControl("CheckBox1") as CheckBox).Checked = false;
        //    }
        //}
        //foreach (GridDataItem selectedItem in RadGrid1.SelectedItems)
        //{
        //    ViewState["SubmissionId"] = Server.HtmlDecode(selectedItem["SubmissionID"].Text);
        //}

    }
}