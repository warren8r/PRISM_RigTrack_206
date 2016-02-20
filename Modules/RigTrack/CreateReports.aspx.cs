using System;
using System.Web.UI;
using System.Data;
using Telerik.Web.UI;


public partial class Modules_RigTrack_CreateReports : System.Web.UI.Page
{
    #region Page Behavior
    //Coming from View/Edit
    void Page_PreInit(Object sender, EventArgs e)
    {
        int ReportID = 0;
        try
        {
            ReportID = Convert.ToInt32(Request.QueryString["ReportID"]);
        }
        catch (Exception) { }
        if (ReportID != 0)
            this.MasterPageFile = "~/Masters/RigTrack_editreport.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownLists();
            //Test to see if there is attribute in URL string (Coming from View/Edit)
            int ReportID = 0;
            try
            {
                ReportID = Convert.ToInt32(Request.QueryString["ReportID"]);
            }
            catch (Exception) { }
            if (ReportID != 0)
                BindPage(ReportID);
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            int CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
            DataTable CurveGroupDT = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(CompanyID);

            if (CurveGroupDT.Rows.Count > 0)
            {
                ddlCurveGroupID.DataTextField = "CurveGroupName";
                ddlCurveGroupID.DataValueField = "ID";
                DataRow toInsert = CurveGroupDT.NewRow();
                toInsert[0] = 0;
                toInsert[1] = "--Select--";
                CurveGroupDT.Rows.InsertAt(toInsert, 0);
                ddlCurveGroupID.DataSource = CurveGroupDT;
                ddlCurveGroupID.DataBind();
                ddlCurveGroupID.Enabled = true;
            }
            else
            {
                ddlCurveGroupID.SelectedIndex = 0;
                ddlCurveGroupID.Enabled = false;
                ddlTarget.SelectedIndex = 0;
                ddlTarget.Enabled = false;
                ddlCurve.SelectedIndex = 0;
                ddlCurve.Enabled = false;
            }
        }
        else
        {
            ddlCurveGroupID.SelectedIndex = 0;
            ddlCurveGroupID.Enabled = false;
            ddlTarget.SelectedIndex = 0;
            ddlTarget.Enabled = false;
            ddlCurve.SelectedIndex = 0;
            ddlCurve.Enabled = false;
        }
    }
    protected void ddlTarget_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlTarget.SelectedIndex > 0)
        {
            int TargetID = Int32.Parse(ddlTarget.SelectedValue);
            DataTable CurveDT = RigTrack.DatabaseObjects.RigTrackDO.GetCurveID_CurveNameForTarget(TargetID);
            if(CurveDT.Rows.Count > 0)
            {
                ddlCurve.DataTextField = "CurveID_CurveName";
                ddlCurve.DataValueField = "ID";
                DataRow CurveToInsert = CurveDT.NewRow();
                CurveToInsert[0] = 0;
                CurveToInsert[1] = "--Select--";
                CurveDT.Rows.InsertAt(CurveToInsert, 0);
                ddlCurve.DataSource = CurveDT;
                ddlCurve.DataBind();
                ddlCurve.Enabled = true;
            }
            else
            {
                ddlCurve.SelectedIndex = 0;
                ddlCurve.Enabled = false;
            }
        }
        else
        {
            ddlCurve.SelectedIndex = 0;
            ddlCurve.Enabled = false;
        }
    }
    protected void ddlCurveGroupID_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCurveGroupID.SelectedIndex > 0)
        {
            int CurveGroupID = Convert.ToInt32(ddlCurveGroupID.SelectedValue);
            DataTable TargetDT = RigTrack.DatabaseObjects.RigTrackDO.GetTargetID_TargetName(CurveGroupID);
            if(TargetDT.Rows.Count > 0)
            {
                ddlTarget.DataTextField = "TargetID_TargetName";
                ddlTarget.DataValueField = "ID";
                DataRow TCtoInsert = TargetDT.NewRow();
                TCtoInsert[0] = 0;
                TCtoInsert[2] = "--Select--";
                TargetDT.Rows.InsertAt(TCtoInsert, 0);
                ddlTarget.DataSource = TargetDT;
                ddlTarget.DataBind();
                ddlTarget.Enabled = true;
            }
            else
            {
                ddlTarget.SelectedIndex = 0;
                ddlTarget.Enabled = false;
                ddlCurve.SelectedIndex = 0;
                ddlCurve.Enabled = false;
            }
        }
        else
        {
            ddlTarget.SelectedIndex = 0;
            ddlTarget.Enabled = false;
            ddlCurve.SelectedIndex = 0;
            ddlCurve.Enabled = false;
        }
    }
    protected void ddlCurve_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0" && ddlCurveGroupID.SelectedValue != "0" && ddlTarget.SelectedValue != "0" && ddlCurve.SelectedValue != "0")
        {
            btnSaveReport.Enabled = true;
        }
        else
        {
            btnSaveReport.Enabled = false;
        }
    }
    #endregion
    #region Buttons
    protected void btnSaveReport_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.ReportDTO CreateReports = new RigTrack.DatabaseTransferObjects.ReportDTO();
        CreateReports = AssignUIToClass();

        TxtReportID.Text = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateReports(CreateReports).ToString();

        // attachment save
        int CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
        if (AttachmentUpload.UploadedFiles.Count > 0)
        {
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
                int AttachmentID = RigTrack.DatabaseObjects.RigTrackDO.AttachCompanyLogo(Filename1, FileBytes, FileType, CompanyID);
            }
        }

        string script = "SuccessDialog(\" Report ID " + TxtReportID.Text + " Successfully Created \");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoRecords", script, true);
    }
    #endregion
    #region Utility Methods
    private void Clear()
    {
        //Company Drop Down
        DataTable CompanyDT = RigTrack.DatabaseObjects.RigTrackDO.GetEveryCompanyThatHasCurveGroup();
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyID";
        DataRow CompanytoInsert = CompanyDT.NewRow();
        CompanytoInsert[0] = 0;
        CompanytoInsert[1] = "--Select--";
        CompanyDT.Rows.InsertAt(CompanytoInsert, 0);
        ddlCompany.DataSource = CompanyDT;
        ddlCompany.DataBind();

        ddlCompany.SelectedIndex = 0;
        ddlCurveGroupID.SelectedIndex = 0;
        ddlCurveGroupID.Enabled = false;
        ddlTarget.SelectedIndex = 0;
        ddlTarget.Enabled = false;
        ddlCurve.SelectedIndex = 0;
        ddlCurve.Enabled = false;

        ddlModes.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllModeReports();
        ddlModes.DataTextField = "Name";
        ddlModes.DataValueField = "ID";
        ddlModes.DataBind();

        ddlModes.SelectedIndex = 0;

        ddlReferences.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllEWNSReferences();
        ddlReferences.DataTextField = "Name";
        ddlReferences.DataValueField = "ID";
        ddlReferences.DataBind();

        ddlReferences.SelectedIndex = 0;

        TxtReportID.Text = "";
    }
    //Bind values from Report to Create Reports page
    private void BindPage(int ReportID)
    {
        TCSave.Visible = false;
        TCUpdate.Visible = true;
        TCClose.Visible = true;
        TCCancel.Visible = true;

        DataTable ReportValues = RigTrack.DatabaseObjects.RigTrackDO.PullAllReportValues(ReportID);
        if (ReportValues.Rows.Count > 0)
        {
            foreach (DataRow row in ReportValues.Rows)
            {
                TxtReportID.Text = Convert.ToString(row["ID"]);
                txtReportName.Text = Convert.ToString(row["Name"]);
                TxtHeaderComments.Text = Convert.ToString(row["HeaderComments"]);
                ddlCompany.SelectedValue = Convert.ToString(row["CompanyID"]);
                ddlCurveGroupID.SelectedValue = Convert.ToString(row["CurveGroupID"]);
                ddlCurveGroupID_SelectedIndexChanged(null, null);
                ddlTarget.SelectedValue = Convert.ToString(row["TargetID"]);
                ddlTarget_SelectedIndexChanged(null, null);
                ddlCurve.SelectedValue = Convert.ToString(row["CurveID"]);
                //.Checked = Convert.ToInt32(row["CurveID"]);
                //.Checked = Convert.ToInt32(row["TargetID"]);
                CBMeasuredDepth.Checked = Convert.ToBoolean(row["MeasuredDepth"]);
                CBInclination.Checked = Convert.ToBoolean(row["Inclination"]);
                CBAzimuth.Checked = Convert.ToBoolean(row["Azimuth"]);
                CBTrueVerticalDepth.Checked = Convert.ToBoolean(row["TrueVerticalDepth"]);
                CBNSCoordinates.Checked = Convert.ToBoolean(row["N_SCoordinates"]);
                CBEWCoordinates.Checked = Convert.ToBoolean(row["E_WCoordinates"]);
                CBVerticalSection.Checked = Convert.ToBoolean(row["VerticalSection"]);
                CBClosureDistance.Checked = Convert.ToBoolean(row["ClosureDistance"]);
                CBClosureDirection.Checked = Convert.ToBoolean(row["ClosureDirection"]);
                CBDogLegSeverity.Checked = Convert.ToBoolean(row["DogLegSeverity"]);
                CBCourseLength.Checked = Convert.ToBoolean(row["CourseLength"]);
                CBWalkRate.Checked = Convert.ToBoolean(row["WalkRate"]);
                CBBuildRate.Checked = Convert.ToBoolean(row["BuildRate"]);
                CBToolFace.Checked = Convert.ToBoolean(row["ToolFace"]);
                CBComment.Checked = Convert.ToBoolean(row["Comment"]);
                CBSubSeaDepth.Checked = Convert.ToBoolean(row["SubseaDepth"]);
            }

        }
    }
    private void BindDropDownLists()
    {
        //Company Drop Down
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
        ddlCurveGroupID.DataTextField = "CurveGroupName";
        ddlCurveGroupID.DataValueField = "ID";
        DataRow toInsert = CurveGroupDT.NewRow();
        toInsert[0] = 0;
        toInsert[1] = "--Select--";
        CurveGroupDT.Rows.InsertAt(toInsert, 0);
        ddlCurveGroupID.DataSource = CurveGroupDT;
        ddlCurveGroupID.DataBind();

        ddlModes.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllModeReports();
        ddlModes.DataTextField = "Name";
        ddlModes.DataValueField = "ID";
        ddlModes.DataBind();

        ddlReferences.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllEWNSReferences();
        ddlReferences.DataTextField = "Name";
        ddlReferences.DataValueField = "ID";
        ddlReferences.DataBind();

        //Target Drop Down
        DataTable TargetDT = RigTrack.DatabaseObjects.RigTrackDO.GetTargetID_TargetName(0);
        ddlTarget.DataTextField = "TargetID_TargetName";
        ddlTarget.DataValueField = "ID";
        DataRow TCtoInsert = TargetDT.NewRow();
        TCtoInsert[0] = 0;
        TCtoInsert[2] = "--Select--";
        TargetDT.Rows.InsertAt(TCtoInsert, 0);
        ddlTarget.DataSource = TargetDT;
        ddlTarget.DataBind();

        //Curve Drop Down
        DataTable CurveDT = RigTrack.DatabaseObjects.RigTrackDO.GetCurveID_CurveNameForTarget(0);
        ddlCurve.DataTextField = "CurveID_CurveName";
        ddlCurve.DataValueField = "ID";
        DataRow CurveToInsert = CurveDT.NewRow();
        CurveToInsert[0] = 0;
        CurveToInsert[1] = "--Select--";
        CurveDT.Rows.InsertAt(CurveToInsert, 0);
        ddlCurve.DataSource = CurveDT;
        ddlCurve.DataBind();
    }
    public RigTrack.DatabaseTransferObjects.ReportDTO AssignUIToClass()
    {


        RigTrack.DatabaseTransferObjects.ReportDTO CreateReports = new RigTrack.DatabaseTransferObjects.ReportDTO();

        // Report ID
        if (TxtReportID.Text == "")
        {
            CreateReports.ID = 0;
        }
        else
        {

            CreateReports.ID = Convert.ToInt32(TxtReportID.Text);
        }


        // report Name 
        if (txtReportName.Text == "")
        {
            CreateReports.ReportName = null;
        }
        else
        {
            CreateReports.ReportName = txtReportName.Text;
        }

        if (TxtHeaderComments.Text == "")
        {
            CreateReports.HeaderComments = null;
        }
        else
        {
            CreateReports.HeaderComments = TxtHeaderComments.Text;
        }


        // Curve Group ID
        if (ddlCurveGroupID.SelectedValue == "0")
        {
            CreateReports.CurveGroupID = null;

        }

        else
        {
            CreateReports.CurveGroupID = Convert.ToInt32(ddlCurveGroupID.SelectedValue);
        }
        // Company ID
        if (ddlCompany.SelectedValue == "0")
        {
            CreateReports.CompanyID = null;

        }

        else
        {
            CreateReports.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
        }


        // Target ID
        if (ddlTarget.SelectedValue == "0")
        {
            CreateReports.TargetID = null;

        }

        else
        {
            CreateReports.TargetID = Convert.ToInt32(ddlTarget.SelectedValue);
        }


        // Curve ID
        if (ddlCurve.SelectedValue == "0")
        {
            CreateReports.CurveID = null;

        }

        else
        {
            CreateReports.CurveID = Convert.ToInt32(ddlCurve.SelectedValue);
        }

        // Measured Depth 
        if (CBMeasuredDepth.Checked == true)
        {
            CreateReports.MeasuredDepth = true;
        }
        else
        {
            CreateReports.MeasuredDepth = false;
        }


        // Inclination
        if (CBInclination.Checked == true)
        {
            CreateReports.Inclination = true;
        }
        else
        {
            CreateReports.Inclination = false;
        }


        // Azimuth
        if (CBAzimuth.Checked == true)
        {
            CreateReports.Azimuth = true;
        }
        else
        {
            CreateReports.Azimuth = false;
        }

        // True Vertical Depth
        if (CBTrueVerticalDepth.Checked == true)
        {
            CreateReports.TrueVerticalDepth = true;
        }
        else
        {
            CreateReports.TrueVerticalDepth = false;
        }

        // N-S Coordinates
        if (CBNSCoordinates.Checked == true)
        {
            CreateReports.NSCoordinates = true;
        }
        else
        {
            CreateReports.NSCoordinates = false;
        }

        // E-W Coordinates
        if (CBEWCoordinates.Checked == true)
        {
            CreateReports.EWCoordinates = true;
        }
        else
        {
            CreateReports.EWCoordinates = false;
        }

        // Vertical Section
        if (CBVerticalSection.Checked == true)
        {
            CreateReports.VerticalSection = true;
        }
        else
        {
            CreateReports.VerticalSection = false;
        }


        // Closure Distance
        if (CBClosureDistance.Checked == true)
        {
            CreateReports.ClosureDistance = true;
        }
        else
        {
            CreateReports.ClosureDistance = false;
        }

        // Closure Direction
        if (CBClosureDirection.Checked == true)
        {
            CreateReports.ClosureDirection = true;
        }
        else
        {
            CreateReports.ClosureDirection = false;
        }


        // DogLeg Severity
        if (CBDogLegSeverity.Checked == true)
        {
            CreateReports.DogLegSeverity = true;
        }
        else
        {
            CreateReports.DogLegSeverity = false;
        }

        // Course Length
        if (CBCourseLength.Checked == true)
        {
            CreateReports.CourseLength = true;
        }
        else
        {
            CreateReports.CourseLength = false;
        }

        // Walk Rate
        if (CBWalkRate.Checked == true)
        {
            CreateReports.WalkRate = true;
        }
        else
        {
            CreateReports.WalkRate = false;
        }

        // Build Rate
        if (CBBuildRate.Checked == true)
        {
            CreateReports.BuildRate = true;
        }
        else
        {
            CreateReports.BuildRate = false;
        }

        // Tool Face
        if (CBToolFace.Checked == true)
        {
            CreateReports.ToolFace = true;
        }
        else
        {
            CreateReports.ToolFace = false;
        }

        // Comment
        if (CBComment.Checked == true)
        {
            CreateReports.Comment = true;
        }
        else
        {
            CreateReports.Comment = false;
        }

        // SubSea Depth
        if (CBSubSeaDepth.Checked == true)
        {
            CreateReports.SubSeaDepth = true;
        }
        else
        {
            CreateReports.SubSeaDepth = false;
        }
        int n;
        bool isNumeric = int.TryParse(txtGrouping.Text, out n);

        if (txtGrouping.Text == "")
        {
            CreateReports.Grouping = null;
        }
        else if (isNumeric)
        {
            CreateReports.Grouping = n;
        }
        else
        {
            CreateReports.Grouping = 0;
        }
        if (ddlBoxedComments.SelectedValue == "1")
        {
            CreateReports.BoxedComments = true;
        }
        else
        {
            CreateReports.BoxedComments = false;
        }


        if (ddlProjectionToBit.SelectedValue == "1")
        {
            CreateReports.ProjectToBit = true;
        }
        else
        {
            CreateReports.ProjectToBit = false;
        }

        if (ddlProjTVD.SelectedValue == "1")
        {
            CreateReports.ProjToTVD = true;
        }
        else
        {
            CreateReports.ProjToTVD = false;
        }

        if (ddlExtraHeader.SelectedValue == "1")
        {
            CreateReports.ExtraHeader = true;
        }
        else
        {
            CreateReports.ExtraHeader = false;
        }

        if (ddlInterpolated.SelectedValue == "1")
        {
            CreateReports.InterpolatedReports = true;
        }
        else
        {
            CreateReports.InterpolatedReports = false;
        }


        // Mode Reports
        if (ddlModes.SelectedValue != "0")
        {
            CreateReports.ModeReport = Convert.ToInt32(ddlModes.SelectedValue);

        }

        else
        {
            CreateReports.ModeReport = null;
        }

        // Mode Reports
        if (ddlReferences.SelectedValue != "0")
        {
            CreateReports.EWNSReferences = Convert.ToInt32(ddlReferences.SelectedValue);

        }

        else
        {
            CreateReports.EWNSReferences = null;
        }


        if (CBFELFWL.Checked == true)
        {
            CreateReports.FELFWL = true;
        }
        else
        {
            CreateReports.FELFWL = false;
        }


        CreateReports.isActive = true;


        return CreateReports;
    }
    #endregion
}