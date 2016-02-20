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
public partial class Modules_RigTrack_ViewEditReports : System.Web.UI.Page
{
    #region Page Behavior
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDowns();
            PullGrid();
        }
    }
    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports();
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if(ddlCompany.SelectedIndex > 0)
        { 
            int companyID = Int32.Parse(ddlCompany.SelectedValue);
            DataTable CurveGroupDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupID_Names(companyID);
            ddlCurveGroupID_Name.DataTextField = "CurveGroupName";
            ddlCurveGroupID_Name.DataValueField = "ID";
            DataRow toInsert = CurveGroupDT.NewRow();
            toInsert[0] = 0;
            toInsert[1] = "--Select--";
            CurveGroupDT.Rows.InsertAt(toInsert, 0);
            ddlCurveGroupID_Name.DataSource = CurveGroupDT;
            ddlCurveGroupID_Name.DataBind();

            DataTable TargetDT = RigTrack.DatabaseObjects.RigTrackDO.GetTargetID_Names(companyID);
            ddlTarget.DataTextField = "TargetID_TargetName";
            ddlTarget.DataValueField = "ID";
            DataRow TargetToInsert = TargetDT.NewRow();
            TargetToInsert[0] = 0;
            TargetToInsert[1] = "--Select--";
            TargetDT.Rows.InsertAt(TargetToInsert, 0);
            ddlTarget.DataSource = TargetDT;
            ddlTarget.DataBind();
        }
        else
        {
            ClearValues();
        }
    }
    protected void ddlCurveGroupID_Name_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if(ddlCurveGroupID_Name.SelectedIndex > 0)
        {
            int CurveGroupID = Convert.ToInt32(ddlCurveGroupID_Name.SelectedValue);
            DataTable CompanyDT = RigTrack.DatabaseObjects.RigTrackDO.GetCompanyID_CompanyName(CurveGroupID);
            int CompanyID = 0;
            foreach (DataRow row in CompanyDT.Rows)
            {
                CompanyID = Convert.ToInt32(row["ID"]);
            }
            ddlCompany.SelectedValue = CompanyID.ToString();
            DataTable TargetDT = RigTrack.DatabaseObjects.RigTrackDO.GetTargetID_TargetNameWithCompanyID(CurveGroupID, CompanyID);
            ddlTarget.DataTextField = "TargetID_TargetName";
            ddlTarget.DataValueField = "ID";
            DataRow TCtoInsert = TargetDT.NewRow();
            TCtoInsert[0] = 0;
            TCtoInsert[2] = "--Select--";
            TargetDT.Rows.InsertAt(TCtoInsert, 0);
            ddlTarget.DataSource = TargetDT;
            ddlTarget.DataBind();
        }
        else
        {
            ClearValues();
        }
    }
    protected void ddlTarget_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlTarget.SelectedIndex > 0)
        {
            int TargetID = Convert.ToInt32(ddlTarget.SelectedValue);
            DataTable CompanyDT = RigTrack.DatabaseObjects.RigTrackDO.GetCompanyID_CompanyNameWithTargetID(TargetID);
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "ID";
            DataRow CompanyToInsert = CompanyDT.NewRow();
            CompanyToInsert[0] = 0;
            CompanyToInsert[1] = "--Select--";
            CompanyDT.Rows.InsertAt(CompanyToInsert, 0);
            ddlCompany.DataSource = CompanyDT;
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;

            DataTable CurveGroupDT = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupID_CurveGroupNameWithTarget(TargetID);
            ddlCurveGroupID_Name.DataTextField = "CurveGroupName";
            ddlCurveGroupID_Name.DataValueField = "ID";
            DataRow toInsert = CurveGroupDT.NewRow();
            toInsert[0] = 0;
            toInsert[1] = "--Select--";
            CurveGroupDT.Rows.InsertAt(toInsert, 0);
            ddlCurveGroupID_Name.DataSource = CurveGroupDT;
            ddlCurveGroupID_Name.DataBind();
            ddlCurveGroupID_Name.SelectedIndex = 1;
        }
        else
        {
            ClearValues();
        }
    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        this.RadGrid1.CurrentPageIndex = e.NewPageIndex;
        RigTrack.DatabaseTransferObjects.ReportSearchDTO ReportSearchDTO = AssignValues();

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchReports(ReportSearchDTO);

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.ReportSearchDTO ReportSearchDTO = AssignValues();

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchReports(ReportSearchDTO);

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridCommandItem)
        {
            ScriptManager ScriptManager1 = (ScriptManager)(Page.Master.FindControl("ScriptManager1"));
            Button btnExcel = (e.Item as GridCommandItem).FindControl("ExportToExcelButton") as Button;
            Button btnPDF = (e.Item as GridCommandItem).FindControl("ExportToPdfButton") as Button;
            Button btnCSV = (e.Item as GridCommandItem).FindControl("ExportToCsvButton") as Button;
            Button btnWord = (e.Item as GridCommandItem).FindControl("ExportToWordButton") as Button;
            ScriptManager1.RegisterPostBackControl(btnExcel);
            ScriptManager1.RegisterPostBackControl(btnPDF);
            ScriptManager1.RegisterPostBackControl(btnCSV);
            ScriptManager1.RegisterPostBackControl(btnWord);
        }
    }
    #endregion
    #region Buttons
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.ReportSearchDTO ReportSearchDTO = AssignValues();

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchReports(ReportSearchDTO);

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
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports();
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    #endregion
    #region Utility Methods
    protected RigTrack.DatabaseTransferObjects.ReportSearchDTO AssignValues()
    {
        RigTrack.DatabaseTransferObjects.ReportSearchDTO ReportSearchDTO = new RigTrack.DatabaseTransferObjects.ReportSearchDTO();

        int TargetID;
        int curveGroupID;
        if (ddlCompany.SelectedIndex == 0)
        {
            ReportSearchDTO.CompanyID = 0;
        }
        else
        {
            ReportSearchDTO.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
        }
        if (ddlCurveGroupID_Name.SelectedIndex == 0)
        {
            ReportSearchDTO.CurveGroupID = 0;
            ReportSearchDTO.CurveGroupName = "";
        }
        else
        {
            Int32.TryParse(ddlCurveGroupID_Name.SelectedValue, out curveGroupID);
            ReportSearchDTO.CurveGroupID = curveGroupID;
            ReportSearchDTO.CurveGroupName = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupName(curveGroupID);
        }
        if (ddlTarget.SelectedIndex == 0)
        {
            ReportSearchDTO.TargetID = 0;
            ReportSearchDTO.TargetName = "";
        }
        else
        {
            Int32.TryParse(ddlTarget.SelectedValue, out TargetID);
            ReportSearchDTO.TargetID = TargetID;
            DataTable GetTarget = RigTrack.DatabaseObjects.RigTrackDO.GetTargetID_TargetName_WithTarget(TargetID);
            foreach (DataRow row in GetTarget.Rows)
            {
                ReportSearchDTO.TargetID = Convert.ToInt32(row["ID"]);
                ReportSearchDTO.TargetName = row["Name"].ToString();
            }
        }
        return ReportSearchDTO;
    }
    private void ClearValues()
    {
        BindDropDowns();
        ddlCurveGroupID_Name.SelectedIndex = 0;
        ddlCompany.SelectedIndex = 0;
        ddlTarget.SelectedIndex = 0;
    }
    private void PullGrid()
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports();
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
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
            btnViewReport.Enabled = false;
            btnEditReport.Enabled = false;
            btnViewTargetReport.Enabled = false;
        }
        else
        {
            btnViewReport.Enabled = true;
            btnEditReport.Enabled = true;
            btnViewTargetReport.Enabled = true;
        }
    }
    protected void BindDropDowns()
    {
        DataTable CompanyDT = RigTrack.DatabaseObjects.RigTrackDO.GetEveryCompanyThatHasCurveGroup();
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyID";
        DataRow CompanytoInsert = CompanyDT.NewRow();
        CompanytoInsert[0] = 0;
        CompanytoInsert[1] = "--Select--";
        CompanyDT.Rows.InsertAt(CompanytoInsert, 0);
        ddlCompany.DataSource = CompanyDT;
        ddlCompany.DataBind();

        DataTable CurveGroupDT = RigTrack.DatabaseObjects.RigTrackDO.GetEveryCurveGroupName();
        ddlCurveGroupID_Name.DataTextField = "CurveGroupName";
        ddlCurveGroupID_Name.DataValueField = "ID";
        DataRow toInsert = CurveGroupDT.NewRow();
        toInsert[0] = 0;
        toInsert[1] = "--Select--";
        CurveGroupDT.Rows.InsertAt(toInsert, 0);
        ddlCurveGroupID_Name.DataSource = CurveGroupDT;
        ddlCurveGroupID_Name.DataBind();

        DataTable TargetDT = RigTrack.DatabaseObjects.RigTrackDO.GetTargetID_TargetName(0);
        ddlTarget.DataTextField = "TargetID_TargetName";
        ddlTarget.DataValueField = "ID";
        DataRow TCtoInsert = TargetDT.NewRow();
        TCtoInsert[0] = 0;
        TCtoInsert[2] = "--Select--";
        TargetDT.Rows.InsertAt(TCtoInsert, 0);
        ddlTarget.DataSource = TargetDT;
        ddlTarget.DataBind();
    }
    #endregion
}