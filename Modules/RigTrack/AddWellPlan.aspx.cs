using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_AddWellPlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblFailedtoUpload.Visible = false;
        lblImportSuccessful.Visible = false;
        if (!IsPostBack)
        {
            btnImport.Enabled = false;
            ddlCurveType.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveTypes();
            ddlCurveType.DataTextField = "Name";
            ddlCurveType.DataValueField = "ID";
            ddlCurveType.DataBind();
            ddlCurveType.SelectedText = "Survey";
            string curveGroupID = string.Empty;
            if (Request.QueryString["CurveGroupID"] != null)
            {
                ImageButton imButton = (ImageButton)Page.Master.FindControl("btnLogo");
                Button btn = (Button)Page.Master.FindControl("btnClose");
                Label lb1 = (Label)Page.Master.FindControl("lbl_welcomename");
                LinkButton lb2 = (LinkButton)Page.Master.FindControl("lnk_logout");
                Label lb3 = (Label)Page.Master.FindControl("lbl_role");
                LinkButton lb4 = (LinkButton)Page.Master.FindControl("lnk_myaccount");
                Label lb5 = (Label)Page.Master.FindControl("lbl_welcomeStatic");
                Label lb6 = (Label)Page.Master.FindControl("lbl_roleStatic");
                RadMenu menu = (RadMenu)Page.Master.FindControl("RadMenu1");
                imButton.Enabled = false;
                btn.Visible = true;
                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                menu.Visible = false;
                Page.Master.FindControl("lnk_password").Visible = false;


                curveGroupID = Request.QueryString["CurveGroupID"].ToString();
                ViewState["CurveGroupID"] = curveGroupID;

                DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupInfo(Int32.Parse(curveGroupID));
                //Curve Group Info needed 
                //Company
                txtCompany.Text = dt.Rows[0]["CompanyName"].ToString();
                //Curve group Name
                txtCurveGroupName.Text = dt.Rows[0]["CurveGroupName"].ToString();
                //Lease /Well
                txtLeaseWell.Text = dt.Rows[0]["LeaseWell"].ToString();
                //Location 
                txtLocation.Text = dt.Rows[0]["JobLocation"].ToString();
                //Rig Name
                txtRigName.Text = dt.Rows[0]["RigName"].ToString();
                //Job Number
                txtJobNumber.Text = dt.Rows[0]["JobNumber"].ToString();

                txtCurveName.Text = "Well Plan Curve";
                txtNSOffset.Text = "0.00";
                txtEWOffset.Text = "0.00";
                txtSubseasOffset.Text = "0.00";
                txtVSDirection.Text = "0.00";
                
                ViewState["Calculation"] = dt.Rows[0]["MethodOfCalculation"].ToString();
                ViewState["Units"] = dt.Rows[0]["MeasurementUnits"].ToString();
                ViewState["DogLeg"] = dt.Rows[0]["DogLeg"].ToString();

            }
        }
    }
    #region Buttons
    protected void btnAddWellPlan_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.SurveyDTO parsedSurvey = new RigTrack.DatabaseTransferObjects.SurveyDTO();
        List<RigTrack.DatabaseTransferObjects.SurveyDTO> surveyList = new List<RigTrack.DatabaseTransferObjects.SurveyDTO>();
        if (FileUpload1.HasFile)
        {
            List<string> returnedData = ParseContent(FileUpload1.PostedFile.InputStream);
            returnedData.RemoveAll(string.IsNullOrWhiteSpace);
            foreach (string s in returnedData)
            {
                parsedSurvey = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                string[] values = s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                parsedSurvey.MD = double.Parse(values[0].ToString());
                parsedSurvey.INC = double.Parse(values[1].ToString());
                parsedSurvey.Azimuth = double.Parse(values[2].ToString());
                parsedSurvey.TVD = double.Parse(values[3].ToString());
                parsedSurvey.NS = double.Parse(values[4].ToString());
                parsedSurvey.EW = double.Parse(values[5].ToString());
                surveyList.Add(parsedSurvey);



            }
        }
        else
        {
            lblFailedtoUpload.Visible = true;
            return;
        }
        bool looped = false;
        foreach (RigTrack.DatabaseTransferObjects.SurveyDTO survey in surveyList)
        {

            string surveyLine = string.Empty;
            surveyLine += survey.MD.ToString();
            surveyLine += "         " + survey.INC.ToString();
            surveyLine += "         " + survey.Azimuth.ToString();
            surveyLine += "         " + survey.TVD.ToString();
            surveyLine += "         " + survey.NS.ToString();
            surveyLine += "         " + survey.EW.ToString();
            if (looped)
            {
                txtSurveysWindow.Text += "\r\n" + surveyLine;
            }
            else
            {
                txtSurveysWindow.Text += surveyLine;
                looped = true;
            }
        }
        HttpContext.Current.Session["SurveyList"] = surveyList;
        btnImport.Enabled = true;

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.SurveyDTO lastSurvey = new RigTrack.DatabaseTransferObjects.SurveyDTO();
        //Get Surveys from Well Plan
        List<RigTrack.DatabaseTransferObjects.SurveyDTO> surveyList = (List<RigTrack.DatabaseTransferObjects.SurveyDTO>)HttpContext.Current.Session["SurveyList"];
        if (surveyList.Count <= 0)
        {
            lblFailedtoUpload.Visible = true;
            return;
        }
        double TVDForTarget = surveyList.ElementAt(surveyList.Count - 1).TVD;
        double NSForTarget = surveyList.ElementAt(surveyList.Count - 1).NS;
        double EWForTarget = surveyList.ElementAt(surveyList.Count - 1).EW;
        //Create Target
        //Square 1001
        RigTrack.DatabaseTransferObjects.TargetDTO PlanTarget = new RigTrack.DatabaseTransferObjects.TargetDTO();
        PlanTarget.ID = 0;
        PlanTarget.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        PlanTarget.ReferenceOptionID = 1000;
        PlanTarget.CreateDate = DateTime.Now;
        PlanTarget.LastModifyDate = DateTime.Now;
        PlanTarget.isActive = true;
        PlanTarget.Name = "Well Plan Target";
        PlanTarget.TargetShapeID = 1001;
        PlanTarget.TVD = TVDForTarget;
        PlanTarget.NSCoordinate = NSForTarget;
        PlanTarget.EWCoordinate = EWForTarget;
        PlanTarget.DiameterOfCircleXoffset = 100;
        PlanTarget.DrawingPattern = 1008;
        //Create Target For Wellplan
        int targetID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateManageTarget(PlanTarget);
        

        RigTrack.DatabaseTransferObjects.CurveDTO curveInfo = new RigTrack.DatabaseTransferObjects.CurveDTO();
        curveInfo.ID = 0;
        curveInfo.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        curveInfo.TargetID = targetID;
        curveInfo.Number = 0;
        curveInfo.Name = txtCurveName.Text;
        curveInfo.CurveTypeID = Int32.Parse(ddlCurveType.SelectedValue);
        curveInfo.NorthOffset = double.Parse(txtNSOffset.Text);
        curveInfo.EastOffset = double.Parse(txtEWOffset.Text);
        curveInfo.VSDirection = double.Parse(txtVSDirection.Text);
        curveInfo.RKBElevation = double.Parse(txtSubseasOffset.Text);
        curveInfo.hexColor = "#FF0000";
        //Create Curve For WellPlan
        int curveID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCurve(curveInfo);
        curveInfo.ID = curveID;
        curveInfo.Name = curveInfo.Name + "- #" + curveID.ToString();
        //Update Name with ID
        RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCurve(curveInfo);
        int curveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        string methodOfCal = ViewState["Calculation"].ToString();
        methodOfCal = methodOfCal[0].ToString();
        string units = ViewState["Units"].ToString();
        units = units[0].ToString();

       //End surveys from Wellplan against our curve.
        for (int i = 0; i < surveyList.Count; i++)
        {
            if (i == 0)
            {
                RigTrack.DatabaseTransferObjects.SurveyDTO firstSurvey = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                firstSurvey = surveyList.ElementAt(i);

                firstSurvey.ID = 0;
                firstSurvey.CurveID = curveID;
                firstSurvey.CurveGroupID = curveGroupID;
                firstSurvey.Name = "Well Plan Tie In";
                firstSurvey.TieInSubsea = firstSurvey.TVD;
                firstSurvey.SubseaTVD = firstSurvey.TVD - curveInfo.RKBElevation;
                firstSurvey.TieInNS = firstSurvey.NS;
                firstSurvey.NS = firstSurvey.NS + curveInfo.NorthOffset;
                firstSurvey.TieInEW = firstSurvey.EW;
                firstSurvey.EW = firstSurvey.EW + curveInfo.EastOffset;
                firstSurvey.TieInVerticalSection = 0.00;
                firstSurvey.VerticalSection = 0.00;
                firstSurvey.CL = 0.00;
                firstSurvey.ClosureDistance = 0.00;
                firstSurvey.ClosureDirection = 0.00;
                firstSurvey.DLS = 0.00;
                firstSurvey.DLA = 0.00;
                firstSurvey.BR = 0.00;
                firstSurvey.WR = 0.00;
                firstSurvey.TFO = 0.00;
                firstSurvey.SurveyComment = "Tie in Survey for Well Plan / Curve Group: " + txtCurveGroupName.Text;
                firstSurvey.isActive = true;
                firstSurvey.RowNumber = i;
                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(firstSurvey);
            }
            else
            {
                RigTrack.DatabaseTransferObjects.SurveyDTO currentSurvey = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                currentSurvey = surveyList.ElementAt(i);
                RigTrack.DatabaseTransferObjects.SurveyDTO previousSurvey = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                previousSurvey = surveyList.ElementAt(i - 1);
                SurveyBuilder a = new SurveyBuilder();
                a.Depth = previousSurvey.MD;
                a.Inclination = previousSurvey.INC;
                a.Azimuth = previousSurvey.Azimuth;
                a.dogLegServerity = previousSurvey.DLS;
                a.dogLegAngle = previousSurvey.DLA;
                a.North = previousSurvey.NS;
                a.East = previousSurvey.EW;
                a.TVD = previousSurvey.TVD;
                a.VerticalSection = previousSurvey.VerticalSection;
                a.ClosureDistance = previousSurvey.ClosureDistance;
                a.ClosureDirection = previousSurvey.ClosureDirection;
                a.BR = previousSurvey.BR;
                a.WR = previousSurvey.WR;
                a.TFO = previousSurvey.TFO;
                SurveyBuilder b = new SurveyBuilder();
                b.Depth = currentSurvey.MD;
                b.Inclination = currentSurvey.INC;
                b.Azimuth = currentSurvey.Azimuth;
                b.TVD = currentSurvey.TVD;
                b.North = currentSurvey.NS;
                b.East = currentSurvey.EW;

                b.dogLegServerity = CurvatureCalculations.DogLegServerity(a, b);
                currentSurvey.DLS = b.dogLegServerity;
                b.dogLegAngle = CurvatureCalculations.DogLegAngle(a, b);
                currentSurvey.DLA = b.dogLegAngle;
                b.SubseasTVD = b.TVD - curveInfo.RKBElevation;
                currentSurvey.SubseaTVD = b.SubseasTVD;
                b.CL = CurvatureCalculations.FindCL(a, b);
                currentSurvey.CL = b.CL;
                b.ClosureDistance = CurvatureCalculations.FindClosureDistance(b.North, b.East);
                currentSurvey.ClosureDistance = b.ClosureDistance;
                b.ClosureDirection = CurvatureCalculations.FindClosureDirection(b.North, b.East);
                currentSurvey.ClosureDirection = b.ClosureDirection;
                b.VerticalSection = CurvatureCalculations.FindVerticalSection(b, curveInfo.VSDirection);
                currentSurvey.VerticalSection = b.VerticalSection;
                b.BR = CurvatureCalculations.FindBR(a, b);
                currentSurvey.BR = b.BR;
                b.WR = CurvatureCalculations.FindWR(a, b);
                currentSurvey.WR = b.WR;
                currentSurvey.TFO = 0.00;



                if (units == "M")
                {
                    string dogleg = ViewState["DogLeg"].ToString();
                    dogleg = Regex.Match(dogleg, @"\d+").Value;
                    currentSurvey.DLS = CurvatureCalculations.FeetToMeters(currentSurvey.DLS);
                    if (dogleg == "30")
                    {
                        currentSurvey.DLS = currentSurvey.DLS * 3;
                    }
                    currentSurvey.MD = CurvatureCalculations.FeetToMeters(currentSurvey.MD);
                    currentSurvey.DLA = CurvatureCalculations.FeetToMeters(currentSurvey.DLA);
                    currentSurvey.NS = CurvatureCalculations.FeetToMeters(currentSurvey.NS);
                    currentSurvey.EW = CurvatureCalculations.FeetToMeters(currentSurvey.EW);
                    currentSurvey.TVD = CurvatureCalculations.FeetToMeters(currentSurvey.TVD);
                    currentSurvey.SubseaTVD = CurvatureCalculations.FeetToMeters(currentSurvey.SubseaTVD);
                    currentSurvey.VerticalSection = CurvatureCalculations.FeetToMeters(currentSurvey.VerticalSection);
                    currentSurvey.CL = CurvatureCalculations.FeetToMeters(currentSurvey.CL);
                    currentSurvey.ClosureDistance = CurvatureCalculations.FeetToMeters(currentSurvey.ClosureDistance);
                    currentSurvey.ClosureDirection = CurvatureCalculations.FeetToMeters(currentSurvey.ClosureDirection);
                    currentSurvey.BR = CurvatureCalculations.FeetToMeters(currentSurvey.BR);
                    currentSurvey.WR = CurvatureCalculations.FeetToMeters(currentSurvey.WR);
                    currentSurvey.TFO = CurvatureCalculations.FeetToMeters(currentSurvey.TFO);
                }

                currentSurvey.ID = 0;
                currentSurvey.CurveID = curveID;
                currentSurvey.CurveGroupID = curveGroupID;
                currentSurvey.Name = "Well Plan Survey #: " + i;
                currentSurvey.isActive = true;
                currentSurvey.SurveyComment = "";
                currentSurvey.isActive = true;
                currentSurvey.RowNumber = i;
                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(currentSurvey);

                lastSurvey = currentSurvey;

            }
        }
        
        RigTrack.DatabaseObjects.RigTrackDO.CurveGroupHasWellPlan(curveGroupID);
        
        PlanTarget.ID = targetID;
        PlanTarget.TVD = lastSurvey.TVD;
        PlanTarget.NSCoordinate = lastSurvey.NS;
        PlanTarget.EWCoordinate = lastSurvey.VerticalSection;
        PlanTarget.LastModifyDate = DateTime.Now;
        targetID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateManageTarget(PlanTarget);
        lblImportSuccessful.Visible = true;
        
    }
    #endregion
    #region File Parsing
    private List<string> ParseContent(System.IO.Stream stream)
    {

        string survey;
        bool foundSurveys = false;
        bool header = false;
        List<string> surveyList = new List<string>();
        using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.Encoding.ASCII))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (foundSurveys)
                {
                    //Deal with Header row for surveys
                    if (!header)
                    {
                        header = true;
                    }
                    else
                    {
                        survey = line;
                        survey.Trim('\t');
                        surveyList.Add(survey);
                    }
                }
                if (line == "H SURVEY LIST")
                {
                    foundSurveys = true;
                }
            }
        }
        if (surveyList.Count <= 0)
        {
            lblFailedtoUpload.Text = "File Not in Correct Format";
            lblFailedtoUpload.Visible = true;
        }
        return surveyList;
    }
    #endregion
   

   
}