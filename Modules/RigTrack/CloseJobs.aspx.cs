using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
public partial class Modules_RigTrack_CloseJobs : System.Web.UI.Page
{
    #region Page Behavior
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDowns();
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupsNotClosed_WithParm(Convert.ToInt32(ddlStatus.SelectedValue));
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
    }
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            if (ddlStatus.SelectedText == "Closed")
                RadGrid1.ExportSettings.FileName = "ClosedJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else if (ddlStatus.SelectedText == "Open")
                RadGrid1.ExportSettings.FileName = "OpenJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else
                RadGrid1.ExportSettings.FileName = "Jobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
        }
        if (e.CommandName == RadGrid.ExportToCsvCommandName)
        {
            if (ddlStatus.SelectedText == "Closed")
                RadGrid1.ExportSettings.FileName = "ClosedJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else if (ddlStatus.SelectedText == "Open")
                RadGrid1.ExportSettings.FileName = "OpenJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else
                RadGrid1.ExportSettings.FileName = "Jobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
        }
        if (e.CommandName == RadGrid.ExportToPdfCommandName)
        {
            if (ddlStatus.SelectedText == "Closed")
                RadGrid1.ExportSettings.FileName = "ClosedJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else if (ddlStatus.SelectedText == "Open")
                RadGrid1.ExportSettings.FileName = "OpenJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else
                RadGrid1.ExportSettings.FileName = "Jobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
        }
        if (e.CommandName == RadGrid.ExportToWordCommandName)
        {
            if (ddlStatus.SelectedText == "Closed")
                RadGrid1.ExportSettings.FileName = "ClosedJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else if (ddlStatus.SelectedText == "Open")
                RadGrid1.ExportSettings.FileName = "OpenJobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            else
                RadGrid1.ExportSettings.FileName = "Jobs" + DateTime.Now.Date.ToString("MM/dd/yyyy");
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        int CompanyID = Int32.Parse(ddlCompany.SelectedValue);
        DataTable CurveGroupDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupID_Names(CompanyID);
        ddlCurveGroupID_Name.DataTextField = "CurveGroupName";
        ddlCurveGroupID_Name.DataValueField = "ID";
        DataRow toInsert = CurveGroupDT.NewRow();
        toInsert[0] = 0;
        toInsert[1] = "--Select--";
        CurveGroupDT.Rows.InsertAt(toInsert, 0);
        ddlCurveGroupID_Name.DataSource = CurveGroupDT;
        ddlCurveGroupID_Name.DataBind();
    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        this.RadGrid1.CurrentPageIndex = e.NewPageIndex;
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = AssignValues();

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchAllCurveGroupsForClose(curveGroupDTO);

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = AssignValues();

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchAllCurveGroupsForClose(curveGroupDTO);

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
    protected void ddlCurveGroupID_Name_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        int CurveGroupID = Convert.ToInt32(ddlCurveGroupID_Name.SelectedValue);
        DataTable StatusDT = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupStatus(CurveGroupID);
        string Status = "Open";
        foreach (DataRow row in StatusDT.Rows)
        {
            Status = row["Status"].ToString();
        }
        ddlStatus.SelectedText = Status;
    }
    #endregion
    #region Buttons
    protected void btnCloseCurveGroup_Click(object sender, EventArgs e)
    {
        int CurveGroupID = 0;
        int Complete = 0;
        DateTime CloseDate = Convert.ToDateTime(dateJobEndDate.SelectedDate);
        bool IsAttachment = false;
        string UserComments = Comments.Text;
        if (RadGrid1.SelectedItems.Count > 0)
        {
            foreach (GridDataItem item in RadGrid1.SelectedItems)
            {
                //access an integer value
                CurveGroupID = Convert.ToInt32(item.GetDataKeyValue("ID").ToString());
                if (AttachmentUpload.UploadedFiles.Count > 0)
                {
                    IsAttachment = true;

                    foreach (UploadedFile file in AttachmentUpload.UploadedFiles)
                    {
                        string Filename1 = file.GetName();
                        
                        byte[] FileBytes = new byte[file.InputStream.Length];
                        file.InputStream.Read(FileBytes, 0, FileBytes.Length);
                        string Extension1 = file.GetExtension();
                        string FileType = null;

                        switch (Extension1)
                        {
                            case ".doc":
                                FileType = "application/vnd.ms-word";
                                break;
                            case ".docx":
                                FileType = "application/vnd.ms-word";
                                break;
                            case ".xls":
                                FileType = "application/vnd.ms-excel";
                                break;
                            case ".xlsx":
                                FileType = "application/vnd.ms-excel";
                                break;
                            case ".jpg":
                                FileType = "image/jpg";
                                break;
                            case ".png":
                                FileType = "image/png";
                                break;
                            case ".gif":
                                FileType = "image/gif";
                                break;
                            case ".pdf":
                                FileType = "application/pdf";
                                break;
                        }
                        int AttachmentID = RigTrack.DatabaseObjects.RigTrackDO.UploadFiles(Filename1, FileBytes, FileType, CurveGroupID);
                    }
                }
                Complete = RigTrack.DatabaseObjects.RigTrackDO.CloseJob(CurveGroupID, CloseDate, UserComments, IsAttachment);
            }
        }
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupsNotClosed_WithParm(Convert.ToInt32(ddlStatus.SelectedValue));
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
        btnCloseCurveGroup.Enabled = false;
        ClosureItems.Visible = false;


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = AssignValues();

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchAllCurveGroupsForClose(curveGroupDTO);

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/ClientAdmin/ClientHome.aspx", true);
    //}
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearValues();
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupsNotClosed_WithParm(Convert.ToInt32(ddlStatus.SelectedValue));
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    #endregion
    #region Utility Methods
    protected RigTrack.DatabaseTransferObjects.CurveGroupDTO AssignValues()
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();

        int curveGroupID;
        if (ddlCompany.SelectedIndex == 0)
        {
            curveGroupDTO.Company = 0;
        }
        else
        {
            curveGroupDTO.Company = Int32.Parse(ddlCompany.SelectedValue);
        }
        if (ddlCurveGroupID_Name.SelectedIndex == 0)
        {
            curveGroupDTO.ID = 0;
            curveGroupDTO.CurveGroupName = "";
        }
        else
        {
            Int32.TryParse(ddlCurveGroupID_Name.SelectedValue, out curveGroupID);
            curveGroupDTO.ID = curveGroupID;
            curveGroupDTO.CurveGroupName = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupName(curveGroupID);
        }
        if (ddlStatus.SelectedValue == "0")
        {
            curveGroupDTO.isActive = false;
        }
        else if (ddlStatus.SelectedValue == "1")
        {
            curveGroupDTO.isActive = true;
        }
        else
        {
            curveGroupDTO.isActive = null;
        }

        //curveGroupDTO.JobNumber = TxtJobNumber.Text;
        //curveGroupDTO.JobLocation = TxtJobLocation.Text;
        //curveGroupDTO.LeaseWell = TxtLeaseWell.Text;
        //curveGroupDTO.RigName = TxtRigName.Text;

        return curveGroupDTO;
    }
    private void ClearValues()
    {
        BindDropDowns();
        ddlCurveGroupID_Name.SelectedValue = "0";
        ddlCompany.SelectedValue = "0";
        ddlStatus.SelectedIndex = 0;
    }
    protected void MultiRowSelection(object sender, EventArgs e)
    {
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        int count = 0;
        foreach (GridDataItem selectedItem in RadGrid1.SelectedItems)
        {
            ViewState["CurveGroupID"] = Server.HtmlDecode(selectedItem["ID"].Text);
            count++;
        }
        if (count > 0)
            btnCloseCurveGroup.Enabled = true;
        else
            btnCloseCurveGroup.Enabled = false;
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
            btnCloseCurveGroup.Enabled = false;
            ClosureItems.Visible = false;
        }
        else
        {
            btnCloseCurveGroup.Enabled = true;
            dateJobEndDate.SelectedDate = DateTime.Now;
            ClosureItems.Visible = true;
        }
    }
    protected void BindDropDowns()
    {
        if (ddlCompany.Items.Count > 1)
        {
        }
        else
        {
            DataTable CompanyDT = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataSource = CompanyDT;
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
        }

        DataTable CurveGroupDT = RigTrack.DatabaseObjects.RigTrackDO.GetEveryCurveGroupName();
        ddlCurveGroupID_Name.DataTextField = "CurveGroupName";
        ddlCurveGroupID_Name.DataValueField = "ID";
        DataRow toInsert = CurveGroupDT.NewRow();
        toInsert[0] = 0;
        toInsert[1] = "--Select--";
        CurveGroupDT.Rows.InsertAt(toInsert, 0);
        ddlCurveGroupID_Name.DataSource = CurveGroupDT;
        ddlCurveGroupID_Name.DataBind();
    }
    #endregion
}