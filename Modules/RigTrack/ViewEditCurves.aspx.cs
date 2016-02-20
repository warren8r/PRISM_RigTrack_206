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
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Reflection;

public partial class Modules_RigTrack_ViewEditCurves : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            ddlCurveGroup.DataTextField = "CurveGroupName";
            ddlCurveGroup.DataValueField = "ID";
            ddlCurveGroup.DataBind();

            ddlCurveType.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveTypes();
            ddlCurveType.DataTextField = "Name";
            ddlCurveType.DataValueField = "ID";
            ddlCurveType.DataBind();
        }
    }

    protected void ddlCurveGroup_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        if (ddlCurveGroup.SelectedValue != "0")
        {
            var firstItem = ddlTarget.Items[0];
            ddlTarget.Items.Clear();
            ddlTarget.Items.Add(firstItem);
            ddlTarget.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(Convert.ToInt32(ddlCurveGroup.SelectedValue));
            ddlTarget.DataTextField = "TargetName";
            ddlTarget.DataValueField = "TargetID";
            ddlTarget.DataBind();
            ddlTarget.Enabled = true;
        }
        else
        {
            ddlTarget.SelectedValue = "0";
            ddlTarget.Enabled = false;
            btnSearchCurves.Enabled = false;
        }
    }

    protected void ddlTarget_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        if (ddlTarget.SelectedValue != "0")
        {
            btnSearchCurves.Enabled = true;
        }
        else
        {
            btnSearchCurves.Enabled = false;
        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;

        foreach (GridDataItem item in RadGridCurves.Items)
        {
            CheckBox chkbx = (CheckBox)item.FindControl("CheckBox1");

            if (chkbx != (sender as CheckBox))
            {
                chkbx.Checked = false;
            }
        }

        if ((sender as CheckBox).Checked)
        {
            btnUpdate.Enabled = true;
        }
        else
        {
            btnUpdate.Enabled = false;
            ClearText();
        }

        foreach (GridDataItem selectedItem in RadGridCurves.SelectedItems)
        {
            ViewState["CurveID"] = Server.HtmlDecode(selectedItem["ID"].Text);
            ViewState["CurveNumber"] = Server.HtmlDecode(selectedItem["Number"].Text);
            txtCurveName.Text = Server.HtmlDecode(selectedItem["Name"].Text);
            ddlCurveType.SelectedText = Server.HtmlDecode(selectedItem["CurveTypeName"].Text);
            txtNorthOffset.Text = Server.HtmlDecode(selectedItem["NorthOffset"].Text);
            txtEastOffset.Text = Server.HtmlDecode(selectedItem["EastOffset"].Text);
            txtVSDirection.Text = Server.HtmlDecode(selectedItem["VSDirection"].Text);
            txtRKBElevation.Text = Server.HtmlDecode(selectedItem["RKBElevation"].Text);
        }
    }

    protected void btnSearchCurves_Click(object sender, EventArgs e)
    {
        ViewState["CurveGroupID"] = Convert.ToInt32(ddlCurveGroup.SelectedValue);
        ViewState["TargetID"] = Convert.ToInt32(ddlTarget.SelectedValue);
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ddlTarget.SelectedValue));
        RadGridCurves.DataSource = dt;
        RadGridCurves.DataBind();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveDTO curveDTO = new RigTrack.DatabaseTransferObjects.CurveDTO();

        curveDTO.ID = Convert.ToInt32(ViewState["CurveID"].ToString());
        curveDTO.CurveGroupID = Convert.ToInt32(ViewState["CurveGroupID"].ToString());
        curveDTO.TargetID = Convert.ToInt32(ViewState["TargetID"].ToString());
        curveDTO.Number = Convert.ToInt32(ViewState["CurveNumber"].ToString());
        curveDTO.Name = txtCurveName.Text;
        curveDTO.CurveTypeID = Convert.ToInt32(ddlCurveType.SelectedValue);
        curveDTO.NorthOffset = Convert.ToDouble(txtNorthOffset.Text);
        curveDTO.EastOffset = Convert.ToDouble(txtEastOffset.Text);
        curveDTO.VSDirection = Convert.ToDouble(txtVSDirection.Text);
        curveDTO.RKBElevation = Convert.ToDouble(txtRKBElevation.Text);

        RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCurve(curveDTO);

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ViewState["TargetID"].ToString()));
        RadGridCurves.DataSource = dt;
        RadGridCurves.DataBind();

        ClearText();
    }

    protected void ClearText()
    {
        ddlCurveGroup.SelectedValue = "0";
        ddlCurveGroup_SelectedIndexChanged(null, null);
        txtCurveName.Text = "";
        ddlCurveType.SelectedValue = "0";
        txtNorthOffset.Text = "";
        txtEastOffset.Text = "";
        txtVSDirection.Text = "";
        txtRKBElevation.Text = "";
    }
}