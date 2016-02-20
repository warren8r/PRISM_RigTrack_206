using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ViewCompanyReports : System.Web.UI.Page
{
    #region Page Behavior
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDowns();
            BindDates();
            BindGrid();
        }
    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        this.RadGrid1.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {
        BindGrid();
    }
    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridCommandItem)
        {
            RadScriptManager RadScriptManager1 = (RadScriptManager)(Page.Master.FindControl("RadScriptManager1"));
            Button btnExcel = (e.Item as GridCommandItem).FindControl("ExportToExcelButton") as Button;
            Button btnPDF = (e.Item as GridCommandItem).FindControl("ExportToPdfButton") as Button;
            Button btnCSV = (e.Item as GridCommandItem).FindControl("ExportToCsvButton") as Button;
            Button btnWord = (e.Item as GridCommandItem).FindControl("ExportToWordButton") as Button;
            RadScriptManager1.RegisterPostBackControl(btnExcel);
            RadScriptManager1.RegisterPostBackControl(btnPDF);
            RadScriptManager1.RegisterPostBackControl(btnCSV);
            RadScriptManager1.RegisterPostBackControl(btnWord);
        }
    }
    #endregion
    #region Buttons
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (CompanyActive.Checked)
            CompanySearch();
        else if (StateActive.Checked)
            StateSearch();
        else
            DateSearch();
    }
    #endregion
    #region Utility Methods
    protected void BindDates()
    {
        date_StartDate.SelectedDate = DateTime.Now.AddMonths(-3);
        date_EndDate.SelectedDate = DateTime.Now;
    }
    protected void BindGrid()
    {
        int CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
        int StateID = Convert.ToInt32(ddlState.SelectedValue);
        DateTime StartDate = Convert.ToDateTime(date_StartDate.SelectedDate);
        DateTime EndDate = Convert.ToDateTime(date_EndDate.SelectedDate);
        bool Status = true;
        if (StatusActive.Checked)
            Status = true;
        else if (StatusInActive.Checked)
            Status = false;
        else
            Status = true;
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(CompanyID, StateID, StartDate, EndDate, Status);
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void CompanySearch()
    {
        int CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
        int StateID = 0;
        DateTime StartDate = Convert.ToDateTime("1753-01-01 00:00:00.000");
        DateTime EndDate = Convert.ToDateTime("9999-12-31 23:59:59.997");
        bool Status = true;
        if (StatusActive.Checked)
            Status = true;
        else if (StatusInActive.Checked)
            Status = false;
        else
            Status = true;
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(CompanyID, StateID, StartDate, EndDate, Status);
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void StateSearch()
    {
        int CompanyID = 0;
        int StateID = Convert.ToInt32(ddlState.SelectedValue);
        DateTime StartDate = Convert.ToDateTime("1753-01-01 00:00:00.000");
        DateTime EndDate = Convert.ToDateTime("9999-12-31 23:59:59.997");
        bool Status = true;
        if (StatusActive.Checked)
            Status = true;
        else if (StatusInActive.Checked)
            Status = false;
        else
            Status = true;
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(CompanyID, StateID, StartDate, EndDate, Status);
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void DateSearch()
    {
        int CompanyID = 0;
        int StateID = 0;
        DateTime StartDate = Convert.ToDateTime(date_StartDate.SelectedDate);
        DateTime EndDate = Convert.ToDateTime(date_EndDate.SelectedDate);
        bool Status = true;
        if (StatusActive.Checked)
            Status = true;
        else if (StatusInActive.Checked)
            Status = false;
        else
            Status = true;
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(CompanyID, StateID, StartDate, EndDate, Status);
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void BindDropDowns()
    {
        DataTable CompanyDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanies2();
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyID";
        DataRow CompanytoInsert = CompanyDT.NewRow();
        CompanytoInsert[0] = 0;
        CompanytoInsert[1] = "--Select--";
        CompanyDT.Rows.InsertAt(CompanytoInsert, 0);
        ddlCompany.DataSource = CompanyDT;
        ddlCompany.DataBind();

        DataTable StatesDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllStates();
        ddlState.DataTextField = "Name";
        ddlState.DataValueField = "ID";
        DataRow StateToInsert = StatesDT.NewRow();
        StateToInsert[0] = 0;
        StateToInsert[1] = "--Select--";
        StatesDT.Rows.InsertAt(StateToInsert, 0);
        ddlState.DataSource = StatesDT;
        ddlState.DataBind();
    }
    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        string OpenClose = "";
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

        foreach (GridDataItem selectedItem in RadGrid1.SelectedItems)
        {
            OpenClose = Server.HtmlDecode(selectedItem["Status"].Text);
        }

        if (!(sender as CheckBox).Checked || OpenClose == "Closed")
        {
            //btnCloseCurveGroup.Enabled = false;
            //ClosureItems.Visible = false;
        }
        else
        {
            //btnCloseCurveGroup.Enabled = true;
            //dateJobEndDate.SelectedDate = DateTime.Now;
            //ClosureItems.Visible = true;
        }

        if ((sender as CheckBox).Checked)
        {
            btnView.Enabled = true;
            btnView2D.Enabled = true;
            btnView3D.Enabled = true;
        }
        else
        {
            btnView.Enabled = false;
            btnView2D.Enabled = false;
            btnView3D.Enabled = false;
        }
    }
    #endregion
    protected void btnView2D_Click(object sender, EventArgs e)
    {
        if (ViewState["CurveGroupID"] != null)
        {
            Response.Redirect("PlotGraph.aspx?CurveGroupID=" + ViewState["CurveGroupID"].ToString());
        }
    }
    protected void btnView3D_Click(object sender, EventArgs e)
    {
        if (ViewState["CurveGroupID"] != null)
        {
            Response.Redirect("Create3DGraph.aspx?CurveGroupID=" + ViewState["CurveGroupID"].ToString());
        }
    }
}