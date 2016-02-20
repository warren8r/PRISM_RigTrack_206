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
public partial class Modules_RigTrack_ViewEditCurveGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDowns();

            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroups();
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
    }

    protected void btnEditCurveGroup_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateCurveGroup.aspx?CurveGroupID=" + ViewState["CurveGroupID"].ToString());
    }

    protected RigTrack.DatabaseTransferObjects.CurveGroupDTO AssignValues()
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();

        int curveGroupID;
        Int32.TryParse(TxtCurveGroupID.Text, out curveGroupID);
        curveGroupDTO.ID = curveGroupID;
        if (ddlCurveName.SelectedIndex == 0)
        {
            curveGroupDTO.CurveGroupName = "";
        }
        else
        {
            curveGroupDTO.CurveGroupName = ddlCurveName.SelectedText;
        }
        if (ddlJobNumber.SelectedIndex == 0)
        {
            curveGroupDTO.JobNumber = "";
        }
        else
        {
            curveGroupDTO.JobNumber = ddlJobNumber.SelectedText;
        }
        if (ddlLocation.SelectedIndex == 0)
        {
            curveGroupDTO.JobLocation = "";
        }
        else
        {
            curveGroupDTO.JobLocation = ddlLocation.SelectedText;
        }
        if (ddlCompany.SelectedIndex == 0)
        {
            curveGroupDTO.Company = 0;
        }
        else
        {
            curveGroupDTO.Company = Int32.Parse(ddlCompany.SelectedValue);
        }
        if (ddlLeaseWell.SelectedIndex == 0)
        {
            curveGroupDTO.LeaseWell = "";
        }
        else
        {
            curveGroupDTO.LeaseWell = ddlLeaseWell.SelectedText;
        }
        if (ddlRigName.SelectedIndex == 0)
        {
            curveGroupDTO.RigName = "";
        }
        else
        {
            curveGroupDTO.RigName = ddlRigName.SelectedText;
        }
        //curveGroupDTO.Status = ddlStatus.SelectedText;

        return curveGroupDTO;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = AssignValues();

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchAllCurveGroups(curveGroupDTO);

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ClientAdmin/ClientHome.aspx", true);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearValues();
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroups();
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }

    private void ClearValues()
    {
        TxtCurveGroupID.Text = "";
        ddlCurveName.SelectedIndex = 0;
        ddlJobNumber.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        ddlCompany.SelectedIndex = 0;
        ddlLeaseWell.SelectedIndex = 0;
        ddlRigName.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
    }

    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;

        foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        {
            if ((dataItem.FindControl("CheckBox1") as CheckBox) != (sender as CheckBox))
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = false;
            }
        }

        foreach (GridDataItem selectedItem in RadGrid1.SelectedItems)
        {
            ViewState["CurveGroupID"] = Server.HtmlDecode(selectedItem["ID"].Text);
        }

        if (!(sender as CheckBox).Checked)
        {
            btnEditCurveGroup.Enabled = false;
        }
        else
        {
            btnEditCurveGroup.Enabled = true;
        }
    }

    protected void BindDropDowns()
    {
        DataTable curveGroupNameDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
        ddlCurveName.DataSource = curveGroupNameDT;
        ddlCurveName.DataTextField = "CurveGroupName";
        ddlCurveName.DataValueField = "ID";
        ddlCurveName.DataBind();

        DataTable jobNumberDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllJobNumbers();
        ddlJobNumber.DataSource = jobNumberDT;
        ddlJobNumber.DataTextField = "JobNumber";
        //ddlJobNumber.DataValueField = "ID";
        ddlJobNumber.DataBind();

        DataTable locationDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllLocations();
        ddlLocation.DataSource = locationDT;
        ddlLocation.DataTextField = "Name";
        //ddlLocation.DataValueField = "ID";
        ddlLocation.DataBind();

        DataTable companyDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanies();
        ddlCompany.DataSource = companyDT;
        ddlCompany.DataTextField = "Company";
        //ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();

        DataTable leaseWellDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllLeaseWell();
        ddlLeaseWell.DataSource = leaseWellDT;
        ddlLeaseWell.DataTextField = "LeaseWell";
        //ddlLeaseWell.DataValueField = "ID";
        ddlLeaseWell.DataBind();

        DataTable rigNameDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllRigNames();
        ddlRigName.DataSource = rigNameDT;
        ddlRigName.DataTextField = "RigName";
        //ddlRigName.DataValueField = "ID";
        ddlRigName.DataBind();
    }
}