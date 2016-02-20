using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_CreateSurvey : System.Web.UI.Page
{

    struct FakeSurvey
    {
        public double MD;
        public double Inclination;
        public double Azimuth;
        public double TVD;
        public double SubseasTVD;
        public double NS;
        public double EW;
        public double VerticalSection;
        public double CL;
        public double ClosureDistance;
        public double ClosureDirection;
        public double DLS;
        public double DLA;
        public double BR;
        public double WR;
        public double TFO;
    };
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.btnAddSurvey.UniqueID;
        lblWellPlanError.Visible = false;
        ScriptManager manager = ScriptManager.GetCurrent(this.Page);
        manager.RegisterPostBackControl(this.btnExportToText);
        manager.RegisterPostBackControl(this.btnCloseCurve);
        lblExportError.Visible = false;
        if (!IsPostBack)
        {
            

            txtEWOffset.Text = "0.00";
            txtNSOffset.Text = "0.00";

            txtTargetID.Text = "";
            txtTargetName.Text = "";
            txtTargetShape.Text = "";
            txtTargetTVD.Text = "0.00";

            txtCurveID.Text = "";
            txtCurveName.Text = "";
            txtCurveType.Text = "";
            txtNorthOffset.Text = "0.00";
            txtEastOffset.Text = "0.00";
            txtVSDirection.Text = "0.00";
            txtRKBElevation.Text = "0.00";
            //RadGridSurveys_NeedDataSource(null, null);
            //RadGridSurveys.DataBind();

            txtAdditionalMD.Text = "0.00";
            txtAdditionalIncl.Text = "0.00";
            txtAdditionalAzimuth.Text = "0.00";
            txtAdditionalTVD.Text = "0.00";
            txtAdditionalNS.Text = "0.00";
            txtAdditionalEW.Text = "0.00";
            txtAdditionalVS.Text = "0.00";
            txtWRate.Text = "0.00";
            txtAdditionalBRate.Text = "0.00";
            txtAdditionalDLS.Text = "0.00";
            txtTFO.Text = "0.00";
            txtAdditionalClosure.Text = "0.00";
            txtAdditionalAT.Text = "0.00";

            rbSensor.Checked = true;
            rbOff.Checked = true;



            ViewState["CurveID"] = null;

            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompanyWithJob();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();

            //ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            //ddlCurveGroup.DataTextField = "CurveGroupName";
            //ddlCurveGroup.DataValueField = "ID";
            //ddlCurveGroup.DataBind();

            ddlMethodOfCalculation.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllMethodsOfCalculation();
            ddlMethodOfCalculation.DataTextField = "Name";
            ddlMethodOfCalculation.DataValueField = "ID";
            ddlMethodOfCalculation.DataBind();

            ddlMeasurementUnits.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllMeasurementUnits();
            ddlMeasurementUnits.DataTextField = "Name";
            ddlMeasurementUnits.DataValueField = "ID";
            ddlMeasurementUnits.DataBind();

            ddlDoglegSeverity.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllDogLeg();
            ddlDoglegSeverity.DataTextField = "Name";
            ddlDoglegSeverity.DataValueField = "ID";
            ddlDoglegSeverity.DataBind();

            ddlOutputDirection.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllInputOutputDirections();
            ddlOutputDirection.DataTextField = "Name";
            ddlOutputDirection.DataValueField = "ID";
            ddlOutputDirection.DataBind();

            ddlInputDirection.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllInputOutputDirections();
            ddlInputDirection.DataTextField = "Name";
            ddlInputDirection.DataValueField = "ID";
            ddlInputDirection.DataBind();

            ddlVerticalSection.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllVerticalSectionRefs();
            ddlVerticalSection.DataTextField = "Name";
            ddlVerticalSection.DataValueField = "ID";
            ddlVerticalSection.DataBind();

            ddlCurveGroup.Enabled = false;
            ddlTarget.Enabled = false;
            ddlCurve.Enabled = false;

            if (Request.QueryString["CurveGroupID"] != null)
            {
                string curveGroupID = Request.QueryString["CurveGroupID"].ToString();
                int curveGroupID_int = Int32.Parse(curveGroupID);
                DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForWellPlan(curveGroupID_int);
                ViewState["CompanyID"] = dt.Rows[0]["CompanyID"].ToString();
                ViewState["CurveID"] = dt.Rows[0]["ID"].ToString();
                ViewState["TargetID"] = dt.Rows[0]["TargetID"].ToString();
                ddlCompany.SelectedValue = ViewState["CompanyID"].ToString();
                ddlCompany_SelectedIndexChanged(null, null);
                ddlCurveGroup.SelectedValue = curveGroupID;
                ddlCurveGroup_SelectedIndexChanged(null, null);
                ddlTarget.SelectedValue = ViewState["TargetID"].ToString();
                ddlTarget_SelectedIndexChanged(null, null);
                ddlCurve.SelectedValue = ViewState["CurveID"].ToString();
                ddlCurve_SelectedIndexChanged(null, null);
            }
        }

    }
    #region Internal Functions
    private void RebindSurveys()
    {
        DataTable dt = new DataTable();
        int curveID = Int32.Parse(ViewState["CurveID"].ToString());

        dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForCurve(curveID);
        //IF no surveys, make a generic tie in. 

        ViewState["SurveyTable"] = dt;
        RadGridSurveys.DataSource = dt;
        RadGridSurveys.Rebind();

        lblSurveyCount.Text = String.Format("Survey Count :{0}",  dt.Rows.Count - 1);


        if (dt.Rows.Count > 1)
        {
            DataRow row2 = dt.Rows[dt.Rows.Count - 1];
            DataRow row1 = dt.Rows[dt.Rows.Count - 2];

            double row2MD = double.Parse(row2["MD"].ToString());
            double row2Inc = double.Parse(row2["Inclination"].ToString());
            double row2AZ = double.Parse(row2["Azimuth"].ToString());
            double row2TVD = double.Parse(row2["TVD"].ToString());
            double row2NS = double.Parse(row2["NS"].ToString());
            double row2EW = double.Parse(row2["EW"].ToString());
            double row2VS = double.Parse(row2["VerticalSection"].ToString());
            double row2WR = double.Parse(row2["WR"].ToString());
            double row2BR = double.Parse(row2["BR"].ToString());
            double row2DLS = double.Parse(row2["DLS"].ToString());
            double row2TFO = double.Parse(row2["TFO"].ToString());
            double row2Distance = double.Parse(row2["ClosureDistance"].ToString());
            double row2Direction = double.Parse(row2["ClosureDirection"].ToString());

            double row1MD = double.Parse(row1["MD"].ToString());
            double row1Inc = double.Parse(row1["Inclination"].ToString());
            double row1AZ = double.Parse(row1["Azimuth"].ToString());
            double row1TVD = double.Parse(row1["TVD"].ToString());
            double row1NS = double.Parse(row1["NS"].ToString());
            double row1EW = double.Parse(row1["EW"].ToString());
            double row1VS = double.Parse(row1["VerticalSection"].ToString());
            double row1WR = double.Parse(row1["WR"].ToString());
            double row1BR = double.Parse(row1["BR"].ToString());
            double row1DLS = double.Parse(row1["DLS"].ToString());
            double row1TFO = double.Parse(row1["TFO"].ToString());
            double row1Distance = double.Parse(row1["ClosureDistance"].ToString());
            double row1Direction = double.Parse(row1["ClosureDirection"].ToString());
            double bitToSensor = 0.0;
            if (rbBHL.Checked)
            {
                try
                {
                    bitToSensor = double.Parse(txtBitToSensor.Text);
                }
                catch (Exception ex)
                {
                    bitToSensor = 0.00;
                    txtBitToSensor.Text = "0.00";
                }
                txtAdditionalMD.Text = (Math.Round((row2MD + bitToSensor), 4)).ToString();
                txtAdditionalIncl.Text = (Math.Round((row2Inc - row1Inc) + row2Inc, 4)).ToString();
                txtAdditionalAzimuth.Text = (Math.Round((row2AZ - row1AZ) + row2AZ, 4)).ToString();
                txtAdditionalTVD.Text = (Math.Round((row2TVD - row1TVD) + row2TVD, 4)).ToString();
                txtAdditionalNS.Text = (Math.Round((row2NS - row1NS) + row2NS, 4)).ToString();
                txtAdditionalEW.Text = (Math.Round((row2EW - row1EW) + row2EW, 4)).ToString();
                txtAdditionalVS.Text = (Math.Round((row2VS - row1VS) + row2VS, 4)).ToString();
                txtWRate.Text = (Math.Round((row2WR - row1WR) + row2WR, 4)).ToString();
                txtAdditionalBRate.Text = (Math.Round((row2BR - row1BR) + row2BR, 4)).ToString();
                txtAdditionalDLS.Text = (Math.Round((row2DLS - row1DLS) + row2DLS, 4)).ToString();
                txtTFO.Text = (Math.Round((row2TFO - row1TFO) + row1TFO, 4)).ToString();
                txtAdditionalClosure.Text = (Math.Round((row2Distance + row1Distance) + row2Distance, 4)).ToString();
                txtAdditionalAT.Text = (Math.Round((row2Direction + row1Direction) + row2Direction, 4)).ToString();
            }
            else
            {
                txtAdditionalMD.Text = row2MD.ToString();
                txtAdditionalIncl.Text = row2Inc.ToString();
                txtAdditionalAzimuth.Text = row2AZ.ToString();
                txtAdditionalTVD.Text = row2TVD.ToString();
                txtAdditionalNS.Text = row2NS.ToString();
                txtAdditionalEW.Text = row2EW.ToString();
                txtAdditionalVS.Text = row2VS.ToString();
                txtWRate.Text = row2WR.ToString();
                txtAdditionalBRate.Text = row2BR.ToString();
                txtAdditionalDLS.Text = row2DLS.ToString();
                txtTFO.Text = row2TFO.ToString();
                txtAdditionalClosure.Text = row2Distance.ToString();
                txtAdditionalAT.Text = row2Direction.ToString();
            }


        }
        else
        {
            txtAdditionalMD.Text = "0.00";
            txtAdditionalIncl.Text = "0.00";
            txtAdditionalAzimuth.Text = "0.00";
            txtAdditionalTVD.Text = "0.00";
            txtAdditionalNS.Text = "0.00";
            txtAdditionalEW.Text = "0.00";
            txtAdditionalVS.Text = "0.00";
            txtWRate.Text = "0.00";
            txtAdditionalBRate.Text = "0.00";
            txtAdditionalDLS.Text = "0.00";
            txtTFO.Text = "0.00";
            txtAdditionalClosure.Text = "0.00";
            txtAdditionalAT.Text = "0.00";
        }

        //dt.Rows[dt.Rows.Count]["MD"]
        //ViewState["LastMD"] = dt.Rows[dt.Rows.Count]["MD"];
        txtName.Text = "";
        txtMeasurementDepth.Text = "0.00";
        txtInclination.Text = "0.00";
        txtAzimuth.Text = "0.00";
        txtTieInTVD.Text = "0.00";
        txtTieInNS.Text = "0.00";
        txtTieInEW.Text = "0.00";
        txtSurveyComments.Text = "";
        btnEditSurvey.Visible = false;
        btnDeleteSurvey.Visible = false;
        btnAddSurvey.Visible = true;
        MeasurementDepthError.Visible = false;

    }
    #endregion
    #region Button Events
    protected void btnAddSurvey_Click(object sender, EventArgs e)
    {
        DataTable curveInfo = RigTrack.DatabaseObjects.RigTrackDO.GetCurveInfo(Int32.Parse(ViewState["CurveID"].ToString()));
        if (curveInfo.Rows[0]["Number"].ToString() == "0")
        {
            lblWellPlanError.Visible = true;
            return;
        }
        
        if (ViewState["CurveID"] == null)
            return;
        
        SurveyBuilder surveyB = new SurveyBuilder();
        surveyB.Depth = double.Parse(txtMeasurementDepth.Text);
        surveyB.Inclination = double.Parse(txtInclination.Text);
        surveyB.Azimuth = double.Parse(txtAzimuth.Text);

        int rows = RadGridSurveys.MasterTableView.Items.Count;
        GridDataItem row = RadGridSurveys.MasterTableView.Items[rows - 1];
        //DataRowView row2 = (DataRowView)RadGridSurveys.MasterTableView.Items[rows - 1].DataItem;
        string _md = row["MD"].Text;
        string _inc = row["Inclination"].Text;
        string _az = row["Azimuth"].Text;
        string _tvd = row["TVD"].Text;
        
        string _north = row["NS"].Text;
        string _east = row["EW"].Text;
        string _dls = row["DLS"].Text;
        string _dla = row["DLA"].Text;
        string _VS = row["VerticalSection"].Text;
        string _distance = row["ClosureDistance"].Text;
        string _direction = row["ClosureDirection"].Text;
        string _br = row["BR"].Text;
        string _wr = row["WR"].Text;
        string _tfo = row["TFO"].Text;
        

        SurveyBuilder surveyA = new SurveyBuilder();
        surveyA.Depth = double.Parse(_md);
        surveyA.Inclination = double.Parse(_inc);
        surveyA.Azimuth = double.Parse(_az);
        surveyA.dogLegServerity = double.Parse(_dls);
        surveyA.dogLegAngle = double.Parse(_dla);
        surveyA.North = double.Parse(_north);
        surveyA.East = double.Parse(_east);
        surveyA.TVD = double.Parse(_tvd);
        surveyA.VerticalSection = double.Parse(_VS);
        surveyA.ClosureDistance = double.Parse(_distance);
        surveyA.ClosureDirection = double.Parse(_direction);
        surveyA.BR = double.Parse(_br);
        surveyA.WR = double.Parse(_wr);
        surveyA.TFO = double.Parse(_tfo);


        if (surveyB.Depth <= surveyA.Depth)
        {
            //do not allow for reverse depths. 
            MeasurementDepthError.Visible = true;
            return;
        }
        else
        {
            MeasurementDepthError.Visible = false;
        }

        string calculation;
        if (ViewState["Calculations"] != null)
        {
            calculation = ViewState["Calculations"].ToString();
            calculation = calculation[0].ToString();
        }
        else
        {
            calculation = "M";
        }

        double RKB = double.Parse(ViewState["RKBElevation"].ToString());
        double NorthOffset = double.Parse(ViewState["NorthOffset"].ToString());
        double EastOffset = double.Parse(ViewState["EastOffset"].ToString());
        double VSDirection = double.Parse(ViewState["VSDirection"].ToString());
        //If user is in Meteres, just take there measurement depth and convert it to feet for calculations.
        //We will convert all results later.
        //if (ViewState["Units"] != null)
        //{
        //    string u = ViewState["Units"].ToString();
        //    u = u[0].ToString();
        //    if (u == "M")
        //    {
        //        surveyA.Depth = CurvatureCalculations.MetersToFeet(surveyA.Depth);
        //        surveyB.Depth = CurvatureCalculations.MetersToFeet(surveyB.Depth);
        //    }
        //}
        switch (calculation)
        {
                //Minimum Curvature New
            case "M":
                surveyB.dogLegServerity = CurvatureCalculations.DogLegServerity(surveyA, surveyB);
                surveyB.dogLegAngle = CurvatureCalculations.DogLegAngle(surveyA, surveyB);
                surveyB.North = CurvatureCalculations.MinCurve_North(surveyA, surveyB);
                
                //surveyB.North = surveyB.North + NorthOffset;
                surveyB.East = CurvatureCalculations.MinCurve_East(surveyA, surveyB);
                //surveyB.East = surveyB.East + EastOffset;
                surveyB.TVD = CurvatureCalculations.MinCurve_TVD(surveyA, surveyB);
                surveyB.SubseasTVD = surveyB.TVD - RKB;
                surveyB.CL = CurvatureCalculations.FindCL(surveyA, surveyB);
                surveyB.ClosureDistance = CurvatureCalculations.FindClosureDistance(surveyB.North, surveyB.East);
                surveyB.ClosureDirection = CurvatureCalculations.FindClosureDirection(surveyB.North, surveyB.East);
                surveyB.VerticalSection = CurvatureCalculations.FindVerticalSection(surveyB, double.Parse(ViewState["VSDirection"].ToString()));
                surveyB.BR = CurvatureCalculations.FindBR(surveyA, surveyB);
                surveyB.WR = CurvatureCalculations.FindWR(surveyA, surveyB);
                surveyB.TFO = 0.0;
                break;
                //Angular Average
            case "A":
                surveyB.dogLegServerity = CurvatureCalculations.DogLegServerity(surveyA, surveyB);
                surveyB.dogLegAngle = CurvatureCalculations.DogLegAngle(surveyA, surveyB);
                surveyB.North = CurvatureCalculations.AngleAvg_North(surveyA, surveyB);
                //surveyB.North = surveyB.North + NorthOffset;
                surveyB.East = CurvatureCalculations.AngleAvg_East(surveyA, surveyB);
                //surveyB.East = surveyB.East + EastOffset;
                surveyB.TVD = CurvatureCalculations.AngleAvg_TVD(surveyA, surveyB);
                surveyB.SubseasTVD = surveyB.TVD - RKB;
                surveyB.CL = CurvatureCalculations.FindCL(surveyA, surveyB);
                surveyB.ClosureDistance = CurvatureCalculations.FindClosureDistance(surveyB.North, surveyB.East);
                surveyB.ClosureDirection = CurvatureCalculations.FindClosureDirection(surveyB.North, surveyB.East);
                surveyB.VerticalSection = CurvatureCalculations.FindVerticalSection(surveyB, double.Parse(ViewState["VSDirection"].ToString()));
                surveyB.BR = CurvatureCalculations.FindBR(surveyA, surveyB);
                surveyB.WR = CurvatureCalculations.FindWR(surveyA, surveyB);
                surveyB.TFO = 0.0;
                
                break;
                //Radius of Curvature
            case "R":
                surveyB.dogLegServerity = CurvatureCalculations.DogLegServerity(surveyA, surveyB);
                surveyB.dogLegAngle = CurvatureCalculations.DogLegAngle(surveyA, surveyB);
                surveyB.North = CurvatureCalculations.Radius_North(surveyA, surveyB);
                //surveyB.North = surveyB.North + NorthOffset;
                surveyB.East = CurvatureCalculations.Radius_East(surveyA, surveyB);
                //surveyB.East = surveyB.East + EastOffset;
                surveyB.TVD = CurvatureCalculations.RadiusTVD(surveyA, surveyB);
                surveyB.SubseasTVD = surveyB.TVD - RKB;
                surveyB.CL = CurvatureCalculations.FindCL(surveyA, surveyB);
                surveyB.ClosureDistance = CurvatureCalculations.FindClosureDistance(surveyB.North, surveyB.East);
                surveyB.ClosureDirection = CurvatureCalculations.FindClosureDirection(surveyB.North, surveyB.East);
                surveyB.VerticalSection = CurvatureCalculations.FindVerticalSection(surveyB, double.Parse(ViewState["VSDirection"].ToString()));
                surveyB.BR = CurvatureCalculations.FindBR(surveyA, surveyB);
                surveyB.WR = CurvatureCalculations.FindWR(surveyA, surveyB);
                surveyB.TFO = 0.0;
                break;
                //Tangential
            case "T":
                surveyB.dogLegServerity = CurvatureCalculations.DogLegServerity(surveyA, surveyB);
                surveyB.dogLegAngle = CurvatureCalculations.DogLegAngle(surveyA, surveyB);
                surveyB.North = CurvatureCalculations.Tangential_North(surveyA, surveyB);
                //surveyB.North = surveyB.North + NorthOffset;
                surveyB.East = CurvatureCalculations.Tangential_East(surveyA, surveyB);
                //surveyB.East = surveyB.East + EastOffset;
                surveyB.TVD = CurvatureCalculations.Tangential_TVD(surveyA, surveyB);
                surveyB.SubseasTVD = surveyB.TVD - RKB;
                surveyB.CL = CurvatureCalculations.FindCL(surveyA, surveyB);
                surveyB.ClosureDistance = CurvatureCalculations.FindClosureDistance(surveyB.North , surveyB.East);
                surveyB.ClosureDirection = CurvatureCalculations.FindClosureDirection(surveyB.North, surveyB.East);
                surveyB.VerticalSection = CurvatureCalculations.FindVerticalSection(surveyB, double.Parse(ViewState["VSDirection"].ToString()));
                surveyB.BR = CurvatureCalculations.FindBR(surveyA, surveyB);
                surveyB.WR = CurvatureCalculations.FindWR(surveyA, surveyB);
                surveyB.TFO = 0.0;
                break;
                //Balanced Tangential
            case "B":
                surveyB.dogLegServerity = CurvatureCalculations.DogLegServerity(surveyA, surveyB);
                surveyB.dogLegAngle = CurvatureCalculations.DogLegAngle(surveyA, surveyB);
                surveyB.North = CurvatureCalculations.BalancedTangential_North(surveyA, surveyB);
                //surveyB.North = surveyB.North + NorthOffset;

                surveyB.East = CurvatureCalculations.BalancedTangential_East(surveyA, surveyB);
               // surveyB.East = surveyB.East + EastOffset;
                surveyB.TVD = CurvatureCalculations.BalancedTangential_TVD(surveyA, surveyB);
                surveyB.SubseasTVD = surveyB.TVD - RKB;
                surveyB.CL = CurvatureCalculations.FindCL(surveyA, surveyB);
                surveyB.ClosureDistance = CurvatureCalculations.FindClosureDistance(surveyB.North, surveyB.East);
                surveyB.ClosureDirection = CurvatureCalculations.FindClosureDirection(surveyB.North, surveyB.East);
                surveyB.VerticalSection = CurvatureCalculations.FindVerticalSection(surveyB, double.Parse(ViewState["VSDirection"].ToString()));
                surveyB.BR = CurvatureCalculations.FindBR(surveyA, surveyB);
                surveyB.WR = CurvatureCalculations.FindWR(surveyA, surveyB);
                surveyB.TFO = 0.0;  
                break;

            default:
                break;
        }



        

        //If we are using Meters, Convert from Feet. else keep as feet
        if (ViewState["Units"] != null)
        {

            string units = ViewState["Units"].ToString();
            units = units[0].ToString();
            if (units == "M")
            {
                string dogLeg =  ViewState["Dogleg"].ToString();
                dogLeg = Regex.Match(dogLeg, @"\d+").Value;
                //Convert all of our calculations
                surveyB.dogLegServerity = CurvatureCalculations.FeetToMeters(surveyB.dogLegServerity);
                if (dogLeg == "30")
                {
                    surveyB.dogLegServerity = surveyB.dogLegServerity * 3;
                }
                surveyB.Depth = CurvatureCalculations.FeetToMeters(surveyB.Depth);
                surveyB.dogLegAngle = CurvatureCalculations.FeetToMeters(surveyB.dogLegAngle);
                surveyB.North = CurvatureCalculations.FeetToMeters(surveyB.North);
                surveyB.East = CurvatureCalculations.FeetToMeters(surveyB.East);
                surveyB.TVD = CurvatureCalculations.FeetToMeters(surveyB.TVD);
                surveyB.SubseasTVD = CurvatureCalculations.FeetToMeters(surveyB.SubseasTVD);
                surveyB.VerticalSection = CurvatureCalculations.FeetToMeters(surveyB.VerticalSection);
                surveyB.CL = CurvatureCalculations.FeetToMeters(surveyB.CL);
                surveyB.ClosureDistance = CurvatureCalculations.FeetToMeters(surveyB.ClosureDistance);
                surveyB.ClosureDirection = CurvatureCalculations.FeetToMeters(surveyB.ClosureDirection);
                surveyB.BR = CurvatureCalculations.FeetToMeters(surveyB.BR);
                surveyB.WR = CurvatureCalculations.FeetToMeters(surveyB.WR);
                surveyB.TFO = CurvatureCalculations.FeetToMeters(surveyB.TFO);
            }
        }


        //surveyB.North = Math.Round(surveyB.North, 2);
        //surveyB.East = Math.Round(surveyB.East, 2);
        //surveyB.TVD = Math.Round(surveyB.TVD, 2);
        //surveyB.dogLegServerity = Math.Round(surveyB.dogLegServerity);
        //surveyB.dogLegAngle = Math.Round(surveyB.dogLegAngle);
        //surveyB.ClosureDirection = Math.Round(surveyB.ClosureDirection, 2);
        //surveyB.ClosureDistance = Math.Round(surveyB.ClosureDistance, 2);


        

        //MinimumCurvationResults results = CurvatureCalculations.MinimumCurvation(surveyA, surveyB);

        //DataTable dt = new DataTable();
        //if (ViewState["SurveyTable"] != null)
        //{
        //    dt = (DataTable)ViewState["SurveyTable"];
        //}
        //else
        //{
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("MD");
        //    dt.Columns.Add("Inclination");
        //    dt.Columns.Add("Azimuth");
        //    dt.Columns.Add("TVD");
        //    dt.Columns.Add("SubseasTVD");
        //    dt.Columns.Add("NS");
        //    dt.Columns.Add("EW");
        //    dt.Columns.Add("VerticalSection");
        //    dt.Columns.Add("CL");
        //    dt.Columns.Add("ClosureDistance");
        //    dt.Columns.Add("ClosureDirection");
        //    dt.Columns.Add("DLS");
        //    dt.Columns.Add("DLA");
        //    dt.Columns.Add("BR");
        //    dt.Columns.Add("WR");
        //    dt.Columns.Add("TFO");
        //    dt.Columns.Add("SurveyComment");
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("RowNumber");
        //}


        //DataRow newRow;
        //newRow = dt.NewRow();
        //MinimumCurvationResults results = new MinimumCurvationResults();
        //newRow["MD"] = surveyB.Depth;
        //newRow["Inclination"] = surveyB.Inclination;
        //newRow["Azimuth"] = surveyB.Azimuth;
        //newRow["TVD"] = Math.Round(surveyB.TVD,2);
        //newRow["SubseasTVD"] = Math.Round(surveyB.SubseasTVD,2);
        //newRow["NS"] = Math.Round(surveyB.North, 2);
        //newRow["EW"] = Math.Round(surveyB.East, 2);
        //newRow["VerticalSection"] = Math.Round(surveyB.North, 2);
        //newRow["CL"] = surveyB.CL;
        //newRow["ClosureDistance"] = Math.Round(surveyB.ClosureDistance, 2);
        //newRow["ClosureDirection"] = Math.Round(surveyB.ClosureDirection, 2);
        //newRow["DLS"] = Math.Round(surveyB.dogLegServerity, 2);
        //newRow["DLA"] = Math.Round(surveyB.dogLegAngle, 2);
        //newRow["BR"] = surveyB.BR;
        //newRow["WR"] = surveyB.WR;
        //newRow["TFO"] = surveyB.TFO;
        //newRow["RowNumber"] = rows;
        //newRow["Name"] = txtName.Text;


        //dt.Rows.Add(newRow);
        //ViewState["SurveyTable"] = dt;
        //RadGridSurveys.DataSource = dt;
        //RadGridSurveys.Rebind();

        RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
        DTO.ID = 0;
        DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
        DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        DTO.Name = txtName.Text;
        DTO.MD = surveyB.Depth;
        DTO.INC = surveyB.Inclination;
        DTO.Azimuth = surveyB.Azimuth;
        DTO.TVD = surveyB.TVD;

        //DTO.TVDTotal = results.TVD + TVD_Total;
        DTO.SubseaTVD = surveyB.SubseasTVD;
        // DTO.SubseaTVDTotal = results.TVD + TVD_Total;
        DTO.NS = surveyB.North;
        // DTO.NSTotal = results.North + NS_Total;
        DTO.EW = surveyB.East;
        //  DTO.EWTotal = results.East + EW_Total;
        DTO.VerticalSection = surveyB.VerticalSection;
        //  DTO.VerticalSectionTotal = results.North + NS_Total;
        DTO.CL = surveyB.CL;
        DTO.ClosureDistance = surveyB.ClosureDistance;
        //  DTO.ClosureDistanceTotal = results.ClosureDistance + ClosureDistance_Total;
        DTO.ClosureDirection = surveyB.ClosureDirection;
        //  DTO.ClosureDirectionTotal = results.ClosureDirection + ClosureDirection_Total;
        DTO.DLS = surveyB.dogLegServerity;
        DTO.DLA = surveyB.dogLegAngle;
        DTO.BR = surveyB.BR;
        DTO.WR = surveyB.WR;
        DTO.TFO = surveyB.TFO;
        DTO.SurveyComment = txtSurveyComments.Text;
        DTO.isActive = true;
        DTO.RowNumber = rows;
        RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
        txtName.Text = "";
        txtMeasurementDepth.Text = "0.00";
        txtInclination.Text = "0.00";
        txtAzimuth.Text = "0.00";
        txtSurveyComments.Text = "";
        MeasurementDepthError.Visible = false;

        
        RebindSurveys();


        
    }
    protected void btnSubseas_Click(object sender, EventArgs e)
    {
        MeasurementDepthError.Visible = false;
        if ((RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display == true)
        {
            (RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display = false;

        }
        else
        {
            (RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display = true;
        }

    }
    protected void btnEditSurvey_Click(object sender, EventArgs e)
    {


        //Get Survey ID To be edited.
        int surveyID = Int32.Parse(hiddenSurveyID.Value);
        GridDataItem previousRow = null;
        bool foundSurvey = false;
        SurveyBuilder newlyFormedSurvey = new SurveyBuilder();
        SurveyBuilder previousSurvey = new SurveyBuilder();
        double RKB = double.Parse(ViewState["RKBElevation"].ToString());
        double NorthOffset = double.Parse(ViewState["NorthOffset"].ToString());
        double EastOffset = double.Parse(ViewState["EastOffset"].ToString());
        double VSDirection = double.Parse(ViewState["VSDirection"].ToString());
        string calculation;
        int count = 0;
        //Get Method Of Calculation
        if (ViewState["Calculations"] != null)
        {
            calculation = ViewState["Calculations"].ToString();
            calculation = calculation[0].ToString();
        }
        else
        {
            calculation = "M";
        }

        foreach (GridDataItem item in RadGridSurveys.MasterTableView.Items)
        {
            //get previous row
            //check if current row is our selected row. 
            if (previousRow != null)
            {
                if (foundSurvey == true)
                {
                    //We just need to update the rest of the records, with new calculations based upon the change.
                    newlyFormedSurvey = new SurveyBuilder();
                    newlyFormedSurvey.Depth = double.Parse(item["MD"].Text);
                    newlyFormedSurvey.Inclination = double.Parse(item["Inclination"].Text);
                    newlyFormedSurvey.Azimuth = double.Parse(item["Azimuth"].Text);
                    int newSurveyID = Int32.Parse(item.GetDataKeyValue("ID").ToString());
                    RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();

                    if (newlyFormedSurvey.Depth <= previousSurvey.Depth)
                    {
                        MeasurementDepthError.Visible = true;
                        return;
                    }
                    else
                    {
                        MeasurementDepthError.Visible = false;
                    }
                    switch (calculation)
                    {
                        case "M":
                            DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.MinCurve_North(previousSurvey, newlyFormedSurvey);
                            //newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.MinCurve_East(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.MinCurve_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "A":
                            DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.AngleAvg_North(previousSurvey, newlyFormedSurvey);
                            //newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.AngleAvg_East(previousSurvey, newlyFormedSurvey);
                            //newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.AngleAvg_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "R":
                             DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.Radius_North(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.Radius_East(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.RadiusTVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "T":
                             DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.Tangential_North(previousSurvey, newlyFormedSurvey);
                            //newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.Tangential_East(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.Tangential_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "B":
                             DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.BalancedTangential_North(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.BalancedTangential_East(previousSurvey, newlyFormedSurvey);
                         //   newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.BalancedTangential_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment =item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        default:
                            break;
                    }

                    previousSurvey = newlyFormedSurvey;
 
                }
                if (item.GetDataKeyValue("ID").ToString() == surveyID.ToString())
                {
                    //Check if values have changed. if no calculation values changed, then dont redo any calculations
                    if (txtMeasurementDepth.Text == item["MD"].Text && txtAzimuth.Text == item["Azimuth"].Text
                        && txtInclination.Text == item["Inclination"].Text)
                    {
                        if (txtName.Text != item["Name"].Text)
                        {
                            //Update survey Name Only.
                            RigTrack.DatabaseObjects.RigTrackDO.UpdateSurveyName(surveyID, txtName.Text);
                            
                        }
                        if (txtSurveyComments.Text != item["SurveyComment"].Text)
                        {
                            //Update Survey Comments Only. 
                            RigTrack.DatabaseObjects.RigTrackDO.UpdateSurveyComments(surveyID, txtSurveyComments.Text);
                            
                        }
                        RebindSurveys();
                        return;
                    }
                    else
                    {
                        foundSurvey = true;
                        SurveyBuilder a = new SurveyBuilder();
                        a.Depth = double.Parse(previousRow["MD"].Text);
                        a.Azimuth = double.Parse(previousRow["Azimuth"].Text);
                        a.Inclination = double.Parse(previousRow["Inclination"].Text);
                        a.TVD = double.Parse(previousRow["TVD"].Text);
                        a.North = double.Parse(previousRow["NS"].Text);
                        a.East = double.Parse(previousRow["EW"].Text);
                        a.dogLegAngle = double.Parse(previousRow["DLA"].Text);
                        a.dogLegServerity = double.Parse(previousRow["DLS"].Text);

                        SurveyBuilder b = new SurveyBuilder();
                        b.Depth = double.Parse(txtMeasurementDepth.Text);
                        b.Azimuth = double.Parse(txtAzimuth.Text);
                        b.Inclination = double.Parse(txtInclination.Text);

                        RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();

                        switch (calculation)
                        {
                            
                            case "M":
                                //Need to assign two. One for calculations, One for Database.
                                //RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                                DTO.MD = b.Depth;
                                DTO.INC = b.Inclination;
                                DTO.Azimuth = b.Azimuth;
                                b.dogLegServerity = CurvatureCalculations.DogLegServerity(a, b);
                                DTO.DLS = b.dogLegServerity;
                                b.dogLegAngle = CurvatureCalculations.DogLegAngle(a, b);
                                DTO.DLA = b.dogLegAngle;
                                b.North = CurvatureCalculations.MinCurve_North(a, b);
                                //b.North = b.North + NorthOffset;
                                DTO.NS = b.North;
                                b.East = CurvatureCalculations.MinCurve_East(a, b);
                               // b.East = b.East + EastOffset;
                                DTO.EW = b.East;
                                b.TVD = CurvatureCalculations.MinCurve_TVD(a, b);
                                DTO.TVD = b.TVD;
                                b.SubseasTVD = b.TVD - RKB;
                                DTO.SubseaTVD = b.TVD + a.SubseasTVD;
                                b.CL = CurvatureCalculations.FindCL(a, b);
                                DTO.CL = b.CL;
                                b.ClosureDistance = CurvatureCalculations.FindClosureDistance(b.North, b.East);
                                DTO.ClosureDistance = b.ClosureDistance;
                                b.ClosureDirection = CurvatureCalculations.FindClosureDirection(b.North, b.East);
                                DTO.ClosureDirection = b.ClosureDirection;
                                b.VerticalSection = CurvatureCalculations.FindVerticalSection(b, double.Parse(ViewState["VSDirection"].ToString()));
                                DTO.VerticalSection = b.VerticalSection;
                                b.BR = CurvatureCalculations.FindBR(a, b);
                                DTO.BR = b.BR;
                                b.WR = CurvatureCalculations.FindWR(a, b);
                                DTO.WR = b.WR;
                                b.TFO = 0.0;
                                DTO.TFO = b.TFO;
                                DTO.ID = surveyID;
                                DTO.Name = item["Name"].Text;
                                DTO.SurveyComment = item["SurveyComment"].Text;
                                DTO.isActive = true;
                                DTO.RowNumber = count;
                                DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                                DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                                previousSurvey = b;
                                break;
                            case "A":
                                //RigTrack.DatabaseTransferObjects.SurveyDTO DTO2 = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                                DTO.MD = b.Depth;
                                DTO.INC = b.Inclination;
                                DTO.Azimuth = b.Azimuth;
                                b.dogLegServerity = CurvatureCalculations.DogLegServerity(a, b);
                                DTO.DLS = b.dogLegServerity;
                                b.dogLegAngle = CurvatureCalculations.DogLegAngle(a, b);
                                DTO.DLA = b.dogLegAngle;
                                b.North = CurvatureCalculations.AngleAvg_North(a,b);
                               // b.North = b.North + NorthOffset;
                                DTO.NS = b.North;
                                b.East = CurvatureCalculations.AngleAvg_East(a,b);
                               // b.East = b.East + EastOffset;
                                DTO.EW = b.East;
                                b.TVD = CurvatureCalculations.AngleAvg_TVD(a, b);
                                DTO.TVD = b.TVD;
                                b.SubseasTVD = b.TVD - RKB;
                                DTO.SubseaTVD = b.SubseasTVD;
                                b.CL = CurvatureCalculations.FindCL(a, b);
                                DTO.CL = b.CL;
                                b.ClosureDistance = CurvatureCalculations.FindClosureDistance(b.North,b.East);
                                DTO.ClosureDistance = b.ClosureDistance;
                                b.ClosureDirection = CurvatureCalculations.FindClosureDirection(b.North, b.East);
                                DTO.ClosureDirection = b.ClosureDirection;
                                b.VerticalSection = CurvatureCalculations.FindVerticalSection(b, double.Parse(ViewState["VSDirection"].ToString()));
                                DTO.VerticalSection = b.VerticalSection;
                                b.BR = CurvatureCalculations.FindBR(a, b);
                                DTO.BR = b.BR;
                                b.WR = CurvatureCalculations.FindWR(a, b);
                                DTO.WR = b.WR;
                                b.TFO = 0.0;
                                DTO.TFO = b.TFO;
                                DTO.ID = surveyID;
                                DTO.Name = item["Name"].Text;
                                DTO.SurveyComment = item["SurveyComment"].Text;
                                DTO.isActive = true;
                                DTO.RowNumber = count;
                                DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                                DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                                previousSurvey = b;
                                break;
                            case "R":
                                DTO.MD = b.Depth;
                                DTO.INC = b.Inclination;
                                DTO.Azimuth = b.Azimuth;
                                b.dogLegServerity = CurvatureCalculations.DogLegServerity(a, b);
                                DTO.DLS = b.dogLegServerity;
                                b.dogLegAngle = CurvatureCalculations.DogLegAngle(a, b);
                                DTO.DLA = b.dogLegAngle;
                                b.North = CurvatureCalculations.Radius_North(a, b);
                               // b.North = b.North + NorthOffset;
                                DTO.NS = b.North;
                                b.East = CurvatureCalculations.Radius_East(a, b);
                               // b.East = b.East + EastOffset;
                                DTO.EW = b.East;
                                b.TVD = CurvatureCalculations.RadiusTVD(a, b);
                                DTO.TVD = b.TVD;
                                b.SubseasTVD = b.TVD - RKB;
                                DTO.SubseaTVD = b.SubseasTVD;
                                b.CL = CurvatureCalculations.FindCL(a, b);
                                DTO.CL = b.CL;
                                b.ClosureDistance = CurvatureCalculations.FindClosureDistance(b.North, b.East);
                                DTO.ClosureDistance = b.ClosureDistance;
                                b.ClosureDirection = CurvatureCalculations.FindClosureDirection(b.North, b.East);
                                DTO.ClosureDirection = b.ClosureDirection;
                                b.VerticalSection = CurvatureCalculations.FindVerticalSection(b, double.Parse(ViewState["VSDirection"].ToString()));
                                DTO.VerticalSection = b.VerticalSection;
                                b.BR = CurvatureCalculations.FindBR(a, b);
                                DTO.BR = b.BR;
                                b.WR = CurvatureCalculations.FindWR(a, b);
                                DTO.WR = b.WR;
                                b.TFO = 0.0;
                                DTO.TFO = b.TFO;
                                DTO.ID = surveyID;
                                DTO.Name = item["Name"].Text;
                                DTO.SurveyComment = item["SurveyComment"].Text;
                                DTO.isActive = true;
                                DTO.RowNumber = count;
                                DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                                DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                                previousSurvey = b;
                                break;
                            case "T":
                                DTO.MD = b.Depth;
                                DTO.INC = b.Inclination;
                                DTO.Azimuth = b.Azimuth;
                                b.dogLegServerity = CurvatureCalculations.DogLegServerity(a, b);
                                DTO.DLS = b.dogLegServerity;
                                b.dogLegAngle = CurvatureCalculations.DogLegAngle(a, b);
                                DTO.DLA = b.dogLegAngle;
                                b.North = CurvatureCalculations.Tangential_North(a, b);
                               // b.North = b.North + NorthOffset;
                                DTO.NS = b.North;
                                b.East = CurvatureCalculations.Tangential_East(a, b);
                               // b.East = b.East + EastOffset;
                                DTO.EW = b.East;
                                b.TVD = CurvatureCalculations.Tangential_TVD(a, b);
                                DTO.TVD = b.TVD;
                                b.SubseasTVD = b.TVD - RKB;
                                DTO.SubseaTVD = b.SubseasTVD;
                                b.CL = CurvatureCalculations.FindCL(a, b);
                                DTO.CL = b.CL;
                                b.ClosureDistance = CurvatureCalculations.FindClosureDistance(b.North, b.East);
                                DTO.ClosureDistance = b.ClosureDistance;
                                b.ClosureDirection = CurvatureCalculations.FindClosureDirection(b.North, b.East);
                                DTO.ClosureDirection = b.ClosureDirection;
                                b.VerticalSection = CurvatureCalculations.FindVerticalSection(b, double.Parse(ViewState["VSDirection"].ToString()));
                                DTO.VerticalSection = b.VerticalSection;
                                b.BR = CurvatureCalculations.FindBR(a, b);
                                DTO.BR = b.BR;
                                b.WR = CurvatureCalculations.FindWR(a, b);
                                DTO.WR = b.WR;
                                b.TFO = 0.0;
                                DTO.TFO = b.TFO;
                                DTO.ID = surveyID;
                                DTO.isActive = true;
                                DTO.Name = item["Name"].Text;
                                DTO.SurveyComment = item["SurveyComment"].Text;
                                DTO.RowNumber = count;
                                DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                                DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                                previousSurvey = b;
                                break;
                            case "B":
                                DTO.MD = b.Depth;
                                DTO.INC = b.Inclination;
                                DTO.Azimuth = b.Azimuth;
                                b.dogLegServerity = CurvatureCalculations.DogLegServerity(a, b);
                                DTO.DLS = b.dogLegServerity;
                                b.dogLegAngle = CurvatureCalculations.DogLegAngle(a, b);
                                DTO.DLA = b.dogLegAngle;
                                b.North = CurvatureCalculations.BalancedTangential_North(a, b);
                              //  b.North = b.North + NorthOffset;
                                DTO.NS = b.North;
                                b.East = CurvatureCalculations.BalancedTangential_East(a, b);
                              //  b.East = b.East + EastOffset;
                                DTO.EW = b.East;
                                b.TVD = CurvatureCalculations.BalancedTangential_TVD(a, b);
                                DTO.TVD = b.TVD;
                                b.SubseasTVD = b.TVD - RKB;
                                DTO.SubseaTVD = b.SubseasTVD;
                                b.CL = CurvatureCalculations.FindCL(a, b);
                                DTO.CL = b.CL;
                                b.ClosureDistance = CurvatureCalculations.FindClosureDistance(b.North, b.East);
                                DTO.ClosureDistance = b.ClosureDistance;
                                b.ClosureDirection = CurvatureCalculations.FindClosureDirection(b.North, b.East);
                                DTO.ClosureDirection = b.ClosureDirection;
                                b.VerticalSection = CurvatureCalculations.FindVerticalSection(b, double.Parse(ViewState["VSDirection"].ToString()));
                                DTO.VerticalSection = b.VerticalSection;
                                b.BR = CurvatureCalculations.FindBR(a, b);
                                DTO.BR = b.BR;
                                b.WR = CurvatureCalculations.FindWR(a, b);
                                DTO.WR = b.WR;
                                b.TFO = 0.0;
                                DTO.TFO = b.TFO;
                                DTO.ID = surveyID;
                                DTO.Name = item["Name"].Text;
                                DTO.SurveyComment = item["SurveyComment"].Text;
                                DTO.isActive = true;
                                DTO.RowNumber = count;
                                DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                                DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                                previousSurvey = b;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else if (item.GetDataKeyValue("ID").ToString() == surveyID.ToString())
            {
                //Tie in has been edited 
                if (txtMeasurementDepth.Text == item["MD"].Text && txtAzimuth.Text == item["Azimuth"].Text
                    && txtInclination.Text == item["Inclination"].Text && txtTieInTVD.Text == item["TVD"].Text
                    && txtTieInNS.Text == item["NS"].Text && txtTieInEW.Text == item["EW"].Text)
                {
                    //No Data has changed. Check to see if names have changed
                    if (txtName.Text != item["Name"].Text)
                    {
                        RigTrack.DatabaseObjects.RigTrackDO.UpdateSurveyName(surveyID, txtName.Text);
                    }
                    if (txtSurveyComments.Text != item["SurveyComment"].Text)
                    {
                        RigTrack.DatabaseObjects.RigTrackDO.UpdateSurveyComments(surveyID, txtSurveyComments.Text);
                    }
                }
                else
                {
                    foundSurvey = true;
                    SurveyBuilder tieIn = new SurveyBuilder();
                    tieIn.Depth = double.Parse(txtMeasurementDepth.Text);
                    
                    tieIn.Azimuth = double.Parse(txtAzimuth.Text);
                    tieIn.Inclination = double.Parse(txtInclination.Text);
                    tieIn.TVD = double.Parse(txtTieInTVD.Text);
                    tieIn.North = double.Parse(txtTieInNS.Text);
                    tieIn.North = tieIn.North + double.Parse(ViewState["NorthOffset"].ToString());

                    tieIn.East = double.Parse(txtTieInEW.Text);
                    tieIn.East = tieIn.East + double.Parse(ViewState["EastOffset"].ToString());
                    tieIn.SubseasTVD = tieIn.TVD - double.Parse(ViewState["RKBElevation"].ToString());
                    tieIn.ClosureDistance = CurvatureCalculations.FindClosureDistance(tieIn.North, tieIn.East);
                    tieIn.ClosureDirection = CurvatureCalculations.FindClosureDirection(tieIn.North, tieIn.East);
                    tieIn.VerticalSection = CurvatureCalculations.FindVerticalSection(tieIn, double.Parse(ViewState["VSDirection"].ToString()));

                    RigTrack.DatabaseTransferObjects.SurveyDTO tieInDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                    tieInDTO.MD = tieIn.Depth;
                    tieInDTO.INC = tieIn.Inclination;
                    tieInDTO.Azimuth = tieIn.Azimuth;
                    tieInDTO.TVD = tieIn.TVD;
                    tieInDTO.SubseaTVD = tieIn.SubseasTVD;
                    tieInDTO.TieInSubsea = tieIn.SubseasTVD + double.Parse(ViewState["RKBElevation"].ToString());
                    tieInDTO.NS = tieIn.North;
                    tieInDTO.TieInNS = tieIn.North;
                    tieInDTO.EW = tieIn.East;
                    tieInDTO.TieInEW = tieIn.East;
                    tieInDTO.ClosureDistance = tieIn.ClosureDistance;
                    tieInDTO.ClosureDirection = tieIn.ClosureDirection;
                    tieInDTO.VerticalSection = tieIn.VerticalSection;
                    tieInDTO.ID = surveyID;
                    tieInDTO.Name = txtName.Text;
                    tieInDTO.SurveyComment = txtSurveyComments.Text;
                    tieInDTO.isActive = true;
                    tieInDTO.RowNumber = count;
                    tieInDTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                    tieInDTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                    RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(tieInDTO);
                    previousSurvey = tieIn;

                }
            }
            previousRow = item;
            count++;
            //if it is our selected row,
            //true
            //recalculate everything after. update records 
        }
        //txtTieInTVD.Visible = false;
        //txtTieInNS.Visible = false;
        //txtTieInEW.Visible = false;
        btnAddLastTargetSurvey.Visible = false;
        TieInRow1.Visible = false;
        TieInRow2.Visible = false;
        TieInRow3.Visible = false;
        
        RebindSurveys();
    }
    protected void btnDeleteSurvey_Click(object sender, EventArgs e)
    {
        //Get Survey ID to be deleted. 
        int surveyID = Int32.Parse(hiddenSurveyID.Value);
        GridDataItem previousRow = null;
        bool foundSurvey = false;
        SurveyBuilder newlyFormedSurvey = new SurveyBuilder();
        SurveyBuilder previousSurvey = new SurveyBuilder();
        double RKB = double.Parse(ViewState["RKBElevation"].ToString());
        double NorthOffset = double.Parse(ViewState["NorthOffset"].ToString());
        double EastOffset = double.Parse(ViewState["EastOffset"].ToString());
        double VSDirection = double.Parse(ViewState["VSDirection"].ToString());
        string calculation;
        int count = 0;
        //Get Method Of Calculation
        if (ViewState["Calculations"] != null)
        {
            calculation = ViewState["Calculations"].ToString();
            calculation = calculation[0].ToString();
        }
        else
        {
            calculation = "M";
        }
        foreach (GridDataItem item in RadGridSurveys.MasterTableView.Items)
        {
            if (previousRow != null)
            {
                if (foundSurvey == true)
                {
                    newlyFormedSurvey = new SurveyBuilder();
                    newlyFormedSurvey.Depth = double.Parse(item["MD"].Text);
                    newlyFormedSurvey.Inclination = double.Parse(item["Inclination"].Text);
                    newlyFormedSurvey.Azimuth = double.Parse(item["Azimuth"].Text);
                    int newSurveyID = Int32.Parse(item.GetDataKeyValue("ID").ToString());
                    RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                    switch (calculation)
                    {
                        case "M":
                            DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.MinCurve_North(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.MinCurve_East(previousSurvey, newlyFormedSurvey);
                            //newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.MinCurve_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "A":
                            DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.AngleAvg_North(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.AngleAvg_East(previousSurvey, newlyFormedSurvey);
                          //  newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.AngleAvg_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "R":
                            DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.Radius_North(previousSurvey, newlyFormedSurvey);
                          //  newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.Radius_East(previousSurvey, newlyFormedSurvey);
                          //  newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.RadiusTVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "T":
                            DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.Tangential_North(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.Tangential_East(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.Tangential_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        case "B":
                            DTO.MD = newlyFormedSurvey.Depth;
                            DTO.INC = newlyFormedSurvey.Inclination;
                            DTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            DTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            DTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.BalancedTangential_North(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            DTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.BalancedTangential_East(previousSurvey, newlyFormedSurvey);
                           // newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            DTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.BalancedTangential_TVD(previousSurvey, newlyFormedSurvey);
                            DTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            DTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            DTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            DTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            DTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            DTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            DTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            DTO.TFO = newlyFormedSurvey.TFO;
                            DTO.ID = newSurveyID;
                            DTO.Name = item["Name"].Text;
                            DTO.SurveyComment = item["SurveyComment"].Text;
                            DTO.isActive = true;
                            DTO.RowNumber = count;
                            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                            break;
                        default:
                            break;

                    }
                    previousSurvey = newlyFormedSurvey;
                    count++;
                    continue;
                }
            }
            if (item.GetDataKeyValue("ID").ToString() == surveyID.ToString())
            {
                //found our survey to delete. 
                //Delete survey, then take previous and next. 
                foundSurvey = true;
                previousSurvey.Depth = double.Parse(previousRow["MD"].Text);
                previousSurvey.Inclination = double.Parse(previousRow["Inclination"].Text);
                previousSurvey.Azimuth = double.Parse(previousRow["Azimuth"].Text);
                previousSurvey.TVD = double.Parse(previousRow["TVD"].Text);
                previousSurvey.SubseasTVD = double.Parse(previousRow["SubseasTVD"].Text);
                previousSurvey.North = double.Parse(previousRow["NS"].Text);
                previousSurvey.East = double.Parse(previousRow["EW"].Text);
                previousSurvey.VerticalSection = double.Parse(previousRow["VerticalSection"].Text);
                previousSurvey.CL = double.Parse(previousRow["CL"].Text);
                previousSurvey.ClosureDistance = double.Parse(previousRow["ClosureDistance"].Text);
                previousSurvey.ClosureDirection = double.Parse(previousRow["ClosureDirection"].Text);
                previousSurvey.dogLegServerity = double.Parse(previousRow["DLS"].Text);
                previousSurvey.dogLegAngle = double.Parse(previousRow["DLA"].Text);
                previousSurvey.BR = double.Parse(previousRow["BR"].Text);
                previousSurvey.WR = double.Parse(previousRow["WR"].Text);
                previousSurvey.TFO = double.Parse(previousRow["TFO"].Text);
                continue;
            }
            else
            {
                
                previousRow = item;
                count++;
                continue;
            }
        }
        //Delete Survey ID 
        RigTrack.DatabaseObjects.RigTrackDO.DeleteSurvey(surveyID);
        RebindSurveys();
        //Get All Surveys
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtMeasurementDepth.Text = "0.00";
        txtInclination.Text = "0.00";
        txtAzimuth.Text = "0.00";
        txtSurveyComments.Text = "";
        MeasurementDepthError.Visible = false;
        foreach (GridDataItem dataItem in RadGridSurveys.MasterTableView.Items)
        {
            if ((dataItem.FindControl("chkbx") as CheckBox).Checked)
            {
                (dataItem.FindControl("chkbx") as CheckBox).Checked = false;
                btnAddSurvey.Visible = true;
                btnEditSurvey.Visible = false;
                btnDeleteSurvey.Visible = false;
            }
        }
    }
    protected void btnEditCurveGroup_Click(object sender, EventArgs e)
    {
        if (ddlCurveGroup.SelectedValue == "0")
        {
            return;
        }
        ddlMethodOfCalculation.Enabled = true;
        ddlMeasurementUnits.Enabled = true;
        ddlUnitsConvert.Enabled = true;
        ddlInputDirection.Enabled = true;
        ddlOutputDirection.Enabled = true;
        ddlDoglegSeverity.Enabled = true;
        ddlVerticalSection.Enabled = true;
        if (ddlVerticalSection.SelectedText == "Wellhead")
        {
            txtEWOffset.ReadOnly = true;
            txtNSOffset.ReadOnly = true;
        }
        else
        {
            txtEWOffset.ReadOnly = false;
            txtNSOffset.ReadOnly = false;
            txtEWOffset.Enabled = true;
            txtNSOffset.Enabled = true;
        }

        btnEditCurveGroup.Visible = false;
        btnSaveCurveGroup.Visible = true;
       
        btnCancelCurveGroup.Visible = true;
        btnCancelCurveGroup.Enabled = true;
    }
    protected void btnSaveCurveGroup_Click(object sender, EventArgs e)
    {

        if (ddlMethodOfCalculation.SelectedText != ViewState["Calculations"].ToString() || ddlMeasurementUnits.SelectedText != ViewState["Units"].ToString()
            || ddlInputDirection.SelectedText != ViewState["Input"].ToString()
            || ddlOutputDirection.SelectedText != ViewState["Output"].ToString() || ddlDoglegSeverity.SelectedText != ViewState["Dogleg"].ToString()
            || ddlVerticalSection.SelectedText != ViewState["VerticalSectionRef"].ToString() || txtEWOffset.Text != ViewState["VSEWOffset"].ToString()
            || txtNSOffset.Text != ViewState["VSNSOffset"].ToString()) //|| ddlUnitsConvert.SelectedValue != ViewState["Convert"].ToString())
        {
        
            //Update
            ViewState["Calculations"] = ddlMethodOfCalculation.SelectedText;
            ViewState["Units"] = ddlMeasurementUnits.SelectedText;
            ViewState["Input"] = ddlInputDirection.SelectedText;
            ViewState["Output"] = ddlOutputDirection.SelectedText;
            ViewState["Dogleg"] = ddlDoglegSeverity.SelectedText;
            string dogleg = ViewState["Dogleg"].ToString();
            dogleg = Regex.Match(dogleg, @"\d+").Value;
            ViewState["VSEWOffset"] = txtEWOffset.Text;
            ViewState["VSNSOffset"] = txtNSOffset.Text;
            ViewState["Convert"] = ddlUnitsConvert.SelectedValue;
            RigTrack.DatabaseTransferObjects.MethodOfCalculationsDTO DTO = new RigTrack.DatabaseTransferObjects.MethodOfCalculationsDTO();
            DTO.MethodOfCalculationID = Int32.Parse(ddlMethodOfCalculation.SelectedValue);
            DTO.MeasurementUnitsID = Int32.Parse(ddlMeasurementUnits.SelectedValue);
            if (ddlUnitsConvert.SelectedValue == "1")
            {
                DTO.UnitsConverted = true;
                //We want to convert records we already have. 
                string units = ddlMeasurementUnits.SelectedText;
                units = units[0].ToString();
                RigTrack.DatabaseTransferObjects.SurveyDTO surveyDTO;
                switch (units)
                {
                    case "F":
                        foreach (GridDataItem item in RadGridSurveys.MasterTableView.Items)
                        {
                            surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                            surveyDTO.MD = CurvatureCalculations.MetersToFeet(double.Parse(item["MD"].Text));
                            surveyDTO.INC = double.Parse(item["Inclination"].Text);
                            surveyDTO.Azimuth = double.Parse(item["Azimuth"].Text);
                            surveyDTO.TVD = CurvatureCalculations.MetersToFeet(double.Parse(item["TVD"].Text));
                            surveyDTO.SubseaTVD = CurvatureCalculations.MetersToFeet(double.Parse(item["SubseasTVD"].Text));
                            surveyDTO.VerticalSection = CurvatureCalculations.MetersToFeet(double.Parse(item["VerticalSection"].Text));
                            surveyDTO.NS = CurvatureCalculations.MetersToFeet(double.Parse(item["NS"].Text));
                            surveyDTO.EW = CurvatureCalculations.MetersToFeet(double.Parse(item["EW"].Text));
                            surveyDTO.CL = CurvatureCalculations.MetersToFeet(double.Parse(item["CL"].Text));
                            surveyDTO.ClosureDistance = CurvatureCalculations.MetersToFeet(double.Parse(item["ClosureDistance"].Text));
                            surveyDTO.ClosureDirection = CurvatureCalculations.MetersToFeet(double.Parse(item["ClosureDirection"].Text));
                            surveyDTO.DLS = CurvatureCalculations.MetersToFeet(double.Parse(item["DLS"].Text));
                            surveyDTO.DLA = CurvatureCalculations.MetersToFeet(double.Parse(item["DLA"].Text));
                            surveyDTO.BR = CurvatureCalculations.MetersToFeet(double.Parse(item["BR"].Text));
                            surveyDTO.WR = CurvatureCalculations.MetersToFeet(double.Parse(item["WR"].Text));
                            surveyDTO.TFO = CurvatureCalculations.MetersToFeet(double.Parse(item["TFO"].Text));
                            surveyDTO.Name = item["Name"].Text;
                            surveyDTO.SurveyComment = item["SurveyComment"].Text;
                            surveyDTO.isActive = true;
                            surveyDTO.ID = Int32.Parse(item.GetDataKeyValue("ID").ToString());
                            surveyDTO.RowNumber = Int32.Parse(item.GetDataKeyValue("RowNumber").ToString());
                            surveyDTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            surveyDTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                        }
                        break;
                    case "M":
                        foreach (GridDataItem item in RadGridSurveys.MasterTableView.Items)
                        {
                            surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                            surveyDTO.MD = CurvatureCalculations.FeetToMeters(double.Parse(item["MD"].Text));
                            surveyDTO.INC = double.Parse(item["Inclination"].Text);
                            surveyDTO.Azimuth = double.Parse(item["Azimuth"].Text);
                            surveyDTO.TVD = CurvatureCalculations.FeetToMeters(double.Parse(item["TVD"].Text));
                            surveyDTO.SubseaTVD = CurvatureCalculations.FeetToMeters(double.Parse(item["SubseasTVD"].Text));
                            surveyDTO.VerticalSection = CurvatureCalculations.FeetToMeters(double.Parse(item["VerticalSection"].Text));
                            surveyDTO.NS = CurvatureCalculations.FeetToMeters(double.Parse(item["NS"].Text));
                            surveyDTO.EW = CurvatureCalculations.FeetToMeters(double.Parse(item["EW"].Text));
                            surveyDTO.CL = CurvatureCalculations.FeetToMeters(double.Parse(item["CL"].Text));
                            surveyDTO.ClosureDistance = CurvatureCalculations.FeetToMeters(double.Parse(item["ClosureDistance"].Text));
                            surveyDTO.ClosureDirection = CurvatureCalculations.FeetToMeters(double.Parse(item["ClosureDirection"].Text));
                            surveyDTO.DLS = CurvatureCalculations.FeetToMeters(double.Parse(item["DLS"].Text));
                            if (dogleg == "30")
                            {
                                surveyDTO.DLS = surveyDTO.DLS * 3;
                            }
                            surveyDTO.DLA = CurvatureCalculations.FeetToMeters(double.Parse(item["DLA"].Text));
                            surveyDTO.BR = CurvatureCalculations.FeetToMeters(double.Parse(item["BR"].Text));
                            surveyDTO.WR = CurvatureCalculations.FeetToMeters(double.Parse(item["WR"].Text));
                            surveyDTO.TFO = CurvatureCalculations.FeetToMeters(double.Parse(item["TFO"].Text));
                            surveyDTO.Name = item["Name"].Text;
                            surveyDTO.SurveyComment = item["SurveyComment"].Text;
                            surveyDTO.isActive = true;
                            surveyDTO.ID = Int32.Parse(item.GetDataKeyValue("ID").ToString());
                            surveyDTO.RowNumber = Int32.Parse(item.GetDataKeyValue("RowNumber").ToString());
                            surveyDTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            surveyDTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                        }
                        break;
                }

                RebindSurveys();
            }
            else
            {
                DTO.UnitsConverted = false;
            }
            DTO.InputDirectionID = Int32.Parse(ddlInputDirection.SelectedValue);
            DTO.OutputDirectionID = Int32.Parse(ddlOutputDirection.SelectedValue);
            if (string.IsNullOrEmpty(txtEWOffset.Text))
            {
                DTO.EWOffset = 0.0;
            }
            else
            {
                DTO.EWOffset = double.Parse(txtEWOffset.Text);
            }
            if (string.IsNullOrEmpty(txtNSOffset.Text))
            {
                DTO.NSOffset = 0.0;
            }
            else
            {
                DTO.NSOffset = double.Parse(txtNSOffset.Text);
            }

            DTO.DoglegID = Int32.Parse(ddlDoglegSeverity.SelectedValue);
            DTO.VerticalSectionID = Int32.Parse(ddlVerticalSection.SelectedValue);

            if (ViewState["CurveGroupID"] != null)
            {

                DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
            }
            RigTrack.DatabaseObjects.RigTrackDO.UpdateMethodOfCalculation(DTO);

            //If our Measurement units have changed and convert is true.
            //Convert all surveys and update. 

        }
        else
        {
            //No Update
        }
        ddlMethodOfCalculation.Enabled = false;
        ddlMeasurementUnits.Enabled = false;
        ddlUnitsConvert.Enabled = false;
        ddlUnitsConvert.SelectedValue = "0";
        ddlInputDirection.Enabled = false;
        ddlOutputDirection.Enabled = false;
        ddlDoglegSeverity.Enabled = false;
        ddlVerticalSection.Enabled = false;
        txtEWOffset.ReadOnly = true;
        txtNSOffset.ReadOnly = true;

        btnEditCurveGroup.Visible = true;
        btnSaveCurveGroup.Visible = false;
        btnCancelCurveGroup.Visible = false;
    }
    protected void btnEditCurve_Click(object sender, EventArgs e)
    {
        if (ddlCurve.SelectedValue == "0")
        {
            return;
        }
        txtNorthOffset.Enabled = true;
        txtEastOffset.Enabled = true;
        txtVSDirection.Enabled = true;
        txtRKBElevation.Enabled = true;

        btnEditCurve.Visible = false;
        btnCancelCurve.Visible = true;
        btnSaveCurve.Visible = true;
        MeasurementDepthError.Visible = false;
    }
    protected void btnSaveCurve_Click(object sender, EventArgs e)
    {
        if (txtNorthOffset.Text != ViewState["NorthOffset"].ToString() || txtEastOffset.Text != ViewState["EastOffset"].ToString() ||
             txtVSDirection.Text != ViewState["VSDirection"].ToString() || txtRKBElevation.Text != ViewState["RKBElevation"].ToString())
        {
            ViewState["NorthOffset"] = txtNorthOffset.Text;
            ViewState["EastOffset"] = txtEastOffset.Text;
            ViewState["VSDirection"] = txtVSDirection.Text;
            ViewState["RKBElevation"] = txtRKBElevation.Text;

            RigTrack.DatabaseTransferObjects.CurveDTO DTO = new RigTrack.DatabaseTransferObjects.CurveDTO();
            DTO.ID = int.Parse(txtCurveID.Text);
            DTO.NorthOffset = double.Parse(txtNorthOffset.Text);
            DTO.EastOffset = double.Parse(txtEastOffset.Text);
            DTO.VSDirection = double.Parse(txtVSDirection.Text);
            DTO.RKBElevation = double.Parse(txtRKBElevation.Text);
            RigTrack.DatabaseObjects.RigTrackDO.UpdateCurveFromSurvey(DTO);

            RigTrack.DatabaseTransferObjects.SurveyDTO surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
            SurveyBuilder tieIn = new SurveyBuilder();
            SurveyBuilder previousSurvey = new SurveyBuilder();
            SurveyBuilder newlyFormedSurvey = new SurveyBuilder();

            double RKB = double.Parse(ViewState["RKBElevation"].ToString());
            double NorthOffset = double.Parse(ViewState["NorthOffset"].ToString());
            double EastOffset = double.Parse(ViewState["EastOffset"].ToString());
            double VSDirection = double.Parse(ViewState["VSDirection"].ToString());

            string calculation;
            if (ViewState["Calculations"] != null)
            {
                calculation = ViewState["Calculations"].ToString();
                calculation = calculation[0].ToString();
            }
            else
            {
                calculation = "M";
            }
            foreach (GridDataItem item in RadGridSurveys.MasterTableView.Items)
            {
                if (item.GetDataKeyValue("RowNumber").ToString() == "0")
                {
                    //NFV - need to get survey tie in original data
                    DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetTieInOriginalData(Int32.Parse(item.GetDataKeyValue("ID").ToString()));
                    double originalSubseaTVD = double.Parse(dt.Rows[0]["SubseaTVD"].ToString());
                    double originalNS = double.Parse(dt.Rows[0]["NS"].ToString());
                    double originalEW = double.Parse(dt.Rows[0]["EW"].ToString());
                    double originalVerticalSection = double.Parse(dt.Rows[0]["VerticalSection"].ToString());
                    
                    surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                    //This is our tie in. Add offset to it. 
                    int surveyID = Int32.Parse(item.GetDataKeyValue("ID").ToString());
                    double _md = double.Parse(item["MD"].Text);
                    tieIn.Depth = _md;
                    surveyDTO.MD = _md;
                    double _inc = double.Parse(item["Inclination"].Text);
                    tieIn.Inclination = _inc;
                    surveyDTO.INC = _inc;
                    double _az = double.Parse(item["Azimuth"].Text);
                    tieIn.Azimuth = _az;
                    surveyDTO.Azimuth = _az;
                    double _tvd = double.Parse(item["TVD"].Text);
                    surveyDTO.TVD = _tvd;
                    tieIn.TVD = _tvd;
                    //double _Subseas = double.Parse(item["SubseasTVD"].Text);
                    //double _ns = double.Parse(item["NS"].Text);
                    //double _ew = double.Parse(item["EW"].Text);
                    //double _vs = double.Parse(item["VerticalSection"].Text);
                    double _cl = double.Parse(item["CL"].Text);
                    surveyDTO.CL = _cl;
                    tieIn.CL = _cl;
                    double _distance = double.Parse(item["ClosureDistance"].Text);
                    surveyDTO.ClosureDistance = _distance;
                    tieIn.ClosureDistance = _distance;
                    double _direction = double.Parse(item["ClosureDirection"].Text);
                    surveyDTO.ClosureDirection = _direction;
                    tieIn.ClosureDirection = _direction;
                    double _dls = double.Parse(item["DLS"].Text);
                    surveyDTO.DLS = _dls;
                    tieIn.dogLegServerity = _dls;
                    double _dla = double.Parse(item["DLA"].Text);
                    surveyDTO.DLA = _dla;
                    tieIn.dogLegAngle = _dla;
                    double _br = double.Parse(item["BR"].Text);
                    surveyDTO.BR = _br;
                    tieIn.BR = _br;
                    double _tfo = double.Parse(item["TFO"].Text);
                    surveyDTO.TFO = _tfo;
                    tieIn.TFO = _tfo;
                    //_Subseas = 0.00 - RKB;
                    originalSubseaTVD = _tvd - RKB;
                    surveyDTO.SubseaTVD = originalSubseaTVD;
                    tieIn.SubseasTVD = originalSubseaTVD;
                    originalNS = originalNS + NorthOffset;
                    surveyDTO.NS = originalNS ;
                    tieIn.North = originalNS;
                    originalEW = originalEW + EastOffset;
                    surveyDTO.EW = originalEW;
                    tieIn.East = originalEW;
                    //originalVerticalSection = originalVerticalSection;
                    surveyDTO.VerticalSection = originalVerticalSection;
                    tieIn.VerticalSection = originalVerticalSection;
                    surveyDTO.Name = item["Name"].Text;
                    surveyDTO.SurveyComment = item["SurveyComment"].Text;
                    surveyDTO.isActive = true;
                    surveyDTO.RowNumber = Int32.Parse(item.GetDataKeyValue("RowNumber").ToString());
                    surveyDTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                    surveyDTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                    surveyDTO.ID = surveyID;
                    RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                    
                }
                else if (item.GetDataKeyValue("RowNumber").ToString() == "1")
                {
                    //First Calculation after tie in offset changed. 
                    newlyFormedSurvey = new SurveyBuilder();
                    newlyFormedSurvey.Depth = double.Parse(item["MD"].Text);
                    newlyFormedSurvey.Inclination = double.Parse(item["Inclination"].Text);
                    newlyFormedSurvey.Azimuth = double.Parse(item["Azimuth"].Text);
                    int newlyFormedSurveyID = Int32.Parse(item.GetDataKeyValue("ID").ToString());
                    surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                    switch (calculation)
                    {
                        case "M":
                            surveyDTO.MD = newlyFormedSurvey.Depth;
                            surveyDTO.INC = newlyFormedSurvey.Inclination;
                            surveyDTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(tieIn, newlyFormedSurvey);
                            surveyDTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(tieIn, newlyFormedSurvey);
                            surveyDTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.MinCurve_North(tieIn, newlyFormedSurvey);
                            //newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            surveyDTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.MinCurve_East(tieIn, newlyFormedSurvey);
                            //newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            surveyDTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.MinCurve_TVD(tieIn, newlyFormedSurvey);
                            surveyDTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            surveyDTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(tieIn, newlyFormedSurvey);
                            surveyDTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            surveyDTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            surveyDTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;

                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            surveyDTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(tieIn, newlyFormedSurvey);
                            surveyDTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(tieIn, newlyFormedSurvey);
                            surveyDTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            surveyDTO.TFO = newlyFormedSurvey.TFO;
                            surveyDTO.ID = newlyFormedSurveyID;
                            surveyDTO.Name = item["Name"].Text;
                            surveyDTO.SurveyComment = item["SurveyComment"].Text;
                            surveyDTO.isActive = true;
                            surveyDTO.RowNumber = Int32.Parse(item.GetDataKeyValue("RowNumber").ToString());
                            surveyDTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            surveyDTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                            previousSurvey = newlyFormedSurvey;
                            break;
                    }

                    //After set this Survey as previous.
                    //then next loop, will fall to else statement 
                    //From there we will use previous survey to recalculate 
                }
                else
                {
                    newlyFormedSurvey = new SurveyBuilder();
                    newlyFormedSurvey.Depth = double.Parse(item["MD"].Text);
                    newlyFormedSurvey.Inclination = double.Parse(item["Inclination"].Text);
                    newlyFormedSurvey.Azimuth = double.Parse(item["Azimuth"].Text);
                    int newSurveyID = Int32.Parse(item.GetDataKeyValue("ID").ToString());
                    surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                    switch (calculation)
                    {
                        case "M":
                            surveyDTO.MD = newlyFormedSurvey.Depth;
                            surveyDTO.INC = newlyFormedSurvey.Inclination;
                            surveyDTO.Azimuth = newlyFormedSurvey.Azimuth;
                            newlyFormedSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, newlyFormedSurvey);
                            surveyDTO.DLS = newlyFormedSurvey.dogLegServerity;
                            newlyFormedSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, newlyFormedSurvey);
                            surveyDTO.DLA = newlyFormedSurvey.dogLegAngle;
                            newlyFormedSurvey.North = CurvatureCalculations.MinCurve_North(previousSurvey, newlyFormedSurvey);
                            //newlyFormedSurvey.North = newlyFormedSurvey.North + NorthOffset;
                            surveyDTO.NS = newlyFormedSurvey.North;
                            newlyFormedSurvey.East = CurvatureCalculations.MinCurve_East(previousSurvey, newlyFormedSurvey);
                            //newlyFormedSurvey.East = newlyFormedSurvey.East + EastOffset;
                            surveyDTO.EW = newlyFormedSurvey.East;
                            newlyFormedSurvey.TVD = CurvatureCalculations.MinCurve_TVD(previousSurvey, newlyFormedSurvey);
                            surveyDTO.TVD = newlyFormedSurvey.TVD;
                            newlyFormedSurvey.SubseasTVD = newlyFormedSurvey.TVD - RKB;
                            surveyDTO.SubseaTVD = newlyFormedSurvey.SubseasTVD;
                            newlyFormedSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, newlyFormedSurvey);
                            surveyDTO.CL = newlyFormedSurvey.CL;
                            newlyFormedSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            surveyDTO.ClosureDistance = newlyFormedSurvey.ClosureDistance;
                            newlyFormedSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(newlyFormedSurvey.North, newlyFormedSurvey.East);
                            surveyDTO.ClosureDirection = newlyFormedSurvey.ClosureDirection;
                            newlyFormedSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(newlyFormedSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                            surveyDTO.VerticalSection = newlyFormedSurvey.VerticalSection;
                            newlyFormedSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, newlyFormedSurvey);
                            surveyDTO.BR = newlyFormedSurvey.BR;
                            newlyFormedSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, newlyFormedSurvey);
                            surveyDTO.WR = newlyFormedSurvey.WR;
                            newlyFormedSurvey.TFO = 0.0;
                            surveyDTO.TFO = newlyFormedSurvey.TFO;
                            surveyDTO.ID = newSurveyID;
                            surveyDTO.Name = item["Name"].Text;
                            surveyDTO.SurveyComment = item["SurveyComment"].Text;
                            surveyDTO.isActive = true;
                            surveyDTO.RowNumber = Int32.Parse(item.GetDataKeyValue("RowNumber").ToString());
                            surveyDTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
                            surveyDTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                            previousSurvey = newlyFormedSurvey;
                            break;
                        
                    }
                    //recalculate based on tie-in / offsets 
                }
            }
        }
        else
        {
            //no update
        }

        txtNorthOffset.Enabled = false;
        txtEastOffset.Enabled = false;
        txtVSDirection.Enabled = false;
        txtRKBElevation.Enabled = false;
        btnEditCurve.Visible = true;
        btnSaveCurve.Visible = false;
        btnCancelCurve.Visible = false;
        RebindSurveys();

    }
    protected void btnCancelCurveGroup_Click(object sender, EventArgs e)
    {
        //ViewState["Calculations"] = ddlMethodOfCalculation.SelectedText;
        ddlMethodOfCalculation.SelectedText = ViewState["Calculations"].ToString();
        //ViewState["Units"] = ddlUnitsConvert.SelectedText;
        ddlMeasurementUnits.SelectedText = ViewState["Units"].ToString();
        //ViewState["Input"] = ddlInputDirection.SelectedText;
        ddlInputDirection.SelectedText = ViewState["Input"].ToString();
        //ViewState["Output"] = ddlOutputDirection.SelectedText;
        ddlOutputDirection.SelectedText = ViewState["Output"].ToString();
        //ViewState["Dogleg"] = ddlDoglegSeverity.SelectedText;
        ddlDoglegSeverity.SelectedText = ViewState["Dogleg"].ToString();
        //ViewState["EWOffset"] = txtEWOffset.Text;
        txtEWOffset.Text = ViewState["VSEWOffset"].ToString();
        //ViewState["NSOffset"] = txtNSOffset.Text;
        txtNSOffset.Text = ViewState["VSNSOffset"].ToString();
        //ViewState["Convert"] = ddlUnitsConvert.SelectedValue;
        ddlVerticalSection.SelectedText = ViewState["VerticalSectionRef"].ToString();
        ddlUnitsConvert.SelectedValue = "0";
        btnEditCurveGroup.Visible = true;
        btnSaveCurveGroup.Visible = false;
        btnCancelCurveGroup.Visible = false;
        if (ddlMeasurementUnits.SelectedText == "Feet")
        {
            DogLegSeverityRow.Visible = false;
        }
        ddlMethodOfCalculation.Enabled = false;
        ddlUnitsConvert.Enabled = false;
        ddlMeasurementUnits.Enabled = false;
        //ddlDoglegSeverity.Enabled = false;
        ddlInputDirection.Enabled = false;
        ddlOutputDirection.Enabled = false;
        ddlVerticalSection.Enabled = false;
        txtNorthOffset.Enabled = false;
        txtEWOffset.Enabled = false;
    }
    protected void btnCancelCurve_Click(object sender, EventArgs e)
    {
        txtNorthOffset.Text = ViewState["NorthOffset"].ToString();
        txtEastOffset.Text = ViewState["EastOffset"].ToString();
        txtVSDirection.Text = ViewState["VSDirection"].ToString();
        txtRKBElevation.Text = ViewState["RKBElevation"].ToString();

        btnCancelCurve.Visible = false;
        btnSaveCurve.Visible = false;
        btnEditCurve.Visible = true;

        txtNorthOffset.Enabled = false;
        txtEastOffset.Enabled = false;
        txtVSDirection.Enabled = false;
        txtRKBElevation.Enabled = false;
    }
    protected void btnEditLocation_Click(object sender, EventArgs e)
    {
        rbSensor.Enabled = true;
        rbBHL.Enabled = true;
        txtBitToSensor.Enabled = true;
        rbOn.Enabled = true;
        rbOff.Enabled = true;
        txtLeastDist.Enabled = true;
        txtHSide.Enabled = true;
        txtTVDComp.Enabled = true;
        txtComparisonCurve.Enabled = true;
        txtLeastDistanceBottom.Enabled = true;
        btnEditLocation.Visible = false;
        btnSaveLocation.Visible = true;
        btnCancelLocation.Visible = true;
    }
    protected void btnSaveLocation_Click(object sender, EventArgs e)
    {
        int locationID = 0;
        double BitToSensor = 0.00;
        bool ListDistanceBool = false;
        int ComparisonCurve = 0;
        int CurveID = Int32.Parse(ViewState["CurveID"].ToString());
        double AtHSide = 0.00;
        double TVDComp = 0.00; 
        
        if(rbSensor.Checked)
        {
            locationID = 1000;
            ViewState["Location"] = "S";
        }
        else if(rbBHL.Checked)
        {
            locationID = 1001;
            ViewState["Location"] = "B";
        }
        if(!string.IsNullOrEmpty(txtBitToSensor.Text))
        {
            try
            {
                BitToSensor = double.Parse(txtBitToSensor.Text);
            }
            catch(Exception ex)
            {
                BitToSensor = 0.00;
            }
        }
        else
        {
            BitToSensor = 0.00;
        }
        if(rbOn.Checked)
        {
            ListDistanceBool = true;
            ViewState["ListDistance"] = "On";
        }
        else if(rbOff.Checked)
        {
            ListDistanceBool = false;
            ViewState["ListDistance"] = "Off";
        }
        if(!string.IsNullOrEmpty(txtComparisonCurve.Text))
        {
            try
            {
                ComparisonCurve = Int32.Parse(txtComparisonCurve.Text);
                
            }
            catch(Exception ex)
            {
                ComparisonCurve = 0;
                
            }
        }
        else
        {
            ComparisonCurve = 0;
            
        }

        if (!string.IsNullOrEmpty(txtHSide.Text))
        {
            try
            {
                AtHSide = double.Parse(txtHSide.Text);
                
            }
            catch (Exception ex)
            {
                AtHSide = 0.00;
                
            }
        }
        else
        {
            AtHSide = 0.00;
            
        }

        if (!string.IsNullOrEmpty(txtTVDComp.Text))
        {
            try
            {
                TVDComp = double.Parse(txtTVDComp.Text);
                
            }
            catch (Exception ex)
            {
                TVDComp = 0.00;
                
            }
        }
        else
        {
            TVDComp = 0.00;
        }
        ViewState["bitToSensor"] = BitToSensor;
        ViewState["ComparisonCurve"] = ComparisonCurve;
        ViewState["HSide"] = AtHSide;
        ViewState["TVDComp"] = TVDComp;
        rbSensor.Enabled = false;
        rbBHL.Enabled = false;

        txtBitToSensor.Enabled = false;
        rbOn.Enabled = false;
        rbOff.Enabled = false;
        txtLeastDist.Enabled = false;
        txtHSide.Enabled = false;
        txtTVDComp.Enabled = false;
        txtComparisonCurve.Enabled = false;
        txtLeastDistanceBottom.Enabled = false;
        btnEditLocation.Visible = true;
        btnSaveLocation.Visible = false;
        btnCancelLocation.Visible = false;

        RigTrack.DatabaseObjects.RigTrackDO.UpdateCurveLocationDetails(CurveID, locationID, BitToSensor, ListDistanceBool, ComparisonCurve, AtHSide, TVDComp);
        RebindSurveys();

    }
    protected void btnCancelLocation_Click(object sender, EventArgs e)
    {

        if (ViewState["Location"].ToString() == "S")
        {
            rbSensor.Checked = true;
        }
        else if (ViewState["Location"].ToString() == "B")
        {
            rbBHL.Checked = true;
        }

        txtBitToSensor.Text = ViewState["bitToSensor"].ToString();
        if (ViewState["ListDistance"].ToString() == "On")
        {
        }
        else if (ViewState["ListDistance"].ToString() == "Off")
        {
        }

        rbSensor.Enabled = false;
        rbBHL.Enabled = false;
        txtBitToSensor.Enabled = false;
        rbOn.Enabled = false;
        rbOff.Enabled = false;
        txtLeastDist.Enabled = false;
        txtHSide.Enabled = false;
        txtTVDComp.Enabled = false;
        txtComparisonCurve.Enabled = false;
        txtLeastDistanceBottom.Enabled = false;
        btnEditLocation.Visible = true;
        btnSaveLocation.Visible = false;
        btnCancelLocation.Visible = false;
    }
    protected void btnAddLastTargetSurvey_Click(object sender, EventArgs e)
    {
        int targetID = Convert.ToInt32(ViewState["TargetID"].ToString());
        int curveNumber = Convert.ToInt32(ViewState["CurveNumber"].ToString());
        if (curveNumber > 0)
        {
            RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();

            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsLastSurvey(targetID, curveNumber);
            //txtName.Text = dt.Rows[0]["Name"].ToString();
            //txtMeasurementDepth.Text = dt.Rows[0]["MD"].ToString();
            //txtInclination.Text = dt.Rows[0]["INC"].ToString();
            //txtAzimuth.Text = dt.Rows[0]["Azimuth"].ToString();
            //txtSurveyComments.Text = dt.Rows[0]["SurveyComment"].ToString();
            //txtTieInTVD.Text = dt.Rows[0]["TVD"].ToString();
            //txtTieInNS.Text = dt.Rows[0]["NS"].ToString();
            //txtTieInEW.Text = dt.Rows[0]["EW"].ToString();

            DTO.MD = double.Parse(dt.Rows[0]["MD"].ToString());
            DTO.Azimuth = double.Parse(dt.Rows[0]["Azimuth"].ToString());
            DTO.INC = double.Parse(dt.Rows[0]["INC"].ToString());
            DTO.TVD = double.Parse(dt.Rows[0]["TVD"].ToString());
            DTO.SubseaTVD = double.Parse(dt.Rows[0]["SubseaTVD"].ToString());
            DTO.TieInSubsea = DTO.SubseaTVD;
            DTO.NS = double.Parse(dt.Rows[0]["NS"].ToString());
            DTO.TieInNS = DTO.NS;
            DTO.EW = double.Parse(dt.Rows[0]["EW"].ToString());
            DTO.TieInEW = DTO.EW;
            DTO.VerticalSection = double.Parse(dt.Rows[0]["VerticalSection"].ToString());
            DTO.TieInVerticalSection = DTO.VerticalSection;
            DTO.CL = double.Parse(dt.Rows[0]["CL"].ToString());
            DTO.ClosureDistance = double.Parse(dt.Rows[0]["ClosureDistance"].ToString());
            DTO.ClosureDirection = double.Parse(dt.Rows[0]["ClosureDirection"].ToString());
            DTO.DLS = double.Parse(dt.Rows[0]["DLS"].ToString());
            DTO.DLA = double.Parse(dt.Rows[0]["DLA"].ToString());
            DTO.BR = double.Parse(dt.Rows[0]["BR"].ToString());
            DTO.WR = double.Parse(dt.Rows[0]["WR"].ToString());
            DTO.TFO = double.Parse(dt.Rows[0]["TFO"].ToString());
            DTO.SurveyComment = "Tie In Survey for Curve: " + ViewState["CurveID"].ToString();
            DTO.Name = dt.Rows[0]["Name"].ToString();
            //DTO.ID = //ID of row selected
            DTO.CurveID = Int32.Parse(ViewState["CurveID"].ToString());
            DTO.CurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
            DTO.RowNumber = 0;
            DTO.isActive = true;
            DTO.ID = Int32.Parse(ViewState["SurveyID"].ToString());
            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
            RebindSurveys();
            TieInRow1.Visible = false;
            TieInRow2.Visible = false;
            TieInRow3.Visible = false;
            btnAddSurvey.Visible = true;
            btnEditSurvey.Visible = false;
            btnDeleteSurvey.Visible = false;
            btnAddLastTargetSurvey.Visible = false;
        }
    }
    protected void btnCloseCurve_Click(object sender, EventArgs e)
    {
        if (ViewState["CurveID"] == null)
        {
            lblExportError.Text = "No Curve To Close!";
            lblExportError.Visible = true;
            return;
        }



        //Export();


        int curveID = Int32.Parse(ViewState["CurveID"].ToString());
        int returnCurveID = RigTrack.DatabaseObjects.RigTrackDO.CloseCurve(curveID);
        ddlTarget_SelectedIndexChanged(null, null);

        RadGridSurveys_NeedDataSource(null, null);
        RadGridSurveys.DataBind();
    }

    protected void Export()
    {
        string txt = string.Empty;
        string Version = ConfigurationManager.AppSettings["VersionNumber"].ToString();


        DateTime dateNow = DateTime.Now;

        GridDataItem startItem = RadGridSurveys.MasterTableView.Items[0];
        string startDepth = startItem["MD"].Text;
        GridDataItem endItem = RadGridSurveys.MasterTableView.Items[RadGridSurveys.MasterTableView.Items.Count - 1];
        string endDepth = endItem["MD"].Text;
        string measurement = ddlMeasurementUnits.SelectedText;
        measurement = measurement[0].ToString();

        txt += string.Format("# Exported with RigTrack Version : {0}  on {1}", Version, dateNow);
        txt += "\r\n";

        txt += "~Well Information Block";
        txt += "\r\n";
        txt += "Start Depth\t:" + "\t\t" + startDepth;
        txt += "\r\n";
        txt += "Stop Depth\t:" + "\t\t" + endDepth;
        txt += "\r\n";
        txt += "Company \t :" + "\t\t" + ddlCompany.SelectedText;
        txt += "\r\n";
        txt += "Job Number \t :" + "\t\t" + txtJobNumber.Text;
        txt += "\r\n";
        txt += "Well \t\t : " + "\t\t" + ViewState["LeaseWell"].ToString();
        txt += "\r\n";
        txt += "Rig Name \t :" + "\t\t" + ViewState["RigName"].ToString();
        txt += "\r\n";
        txt += "Location \t :" + "\t\t" + ViewState["JobLocation"].ToString();
        txt += "\r\n";
        txt += "State \t\t :" + "\t\t" + ViewState["State"].ToString();
        txt += "\r\n";
        txt += "Country \t :" + "\t\t" + ViewState["Country"].ToString();
        txt += "\r\n";
        txt += "Log Date \t :" + "\t\t" + dateNow.Date.ToShortDateString();
        txt += "\r\n";

        txt += "~Curve Information Block";
        txt += "\r\n";
        txt += "CurveID \t :" + "\t\t" + ddlCurve.SelectedValue;
        txt += "\r\n";
        txt += "Curve Name \t :" + "\t\t" + ViewState["CurveName"].ToString();
        txt += "\r\n";
        txt += "Curve Type \t :" + "\t\t" + ViewState["CurveType"].ToString();
        txt += "\r\n";
        txt += "Curve Number :" + "\t\t" + ViewState["CurveNumber"].ToString();
        txt += "\r\n";
        txt += "MD\t\t\t\t " +measurement + ":" + "\t\t" + "1 Measurement Depth";
        txt += "\r\n";
        txt += "Inc\t\t\t\t " + "DEG" + ":" + "\t\t" + "2 Inclination";
        txt += "\r\n";
        txt += "Az\t\t\t\t "+ "DEG" + ":" + "\t\t" + "3 Azimuth";
        txt += "\r\n";
        txt += "TVD\t\t\t\t " + measurement + ":" + "\t\t" + "4 Total Vertical Depth";
        txt += "\r\n";
        txt += "SubseaTVD\t\t " + measurement + ":" + "\t\t" + "5 Subseas Total Vertical Depth";
        txt += "\r\n";
        txt += "NS\t\t\t\t " +measurement+":" + "\t\t" + "6 North/South";
        txt += "\r\n";
        txt += "EW\t\t\t\t " + measurement+":" + "\t\t" + "7 East/West";
        txt += "\r\n";
        txt += "VS\t\t\t\t "+ measurement+":" + "\t\t" + "8 Vertical Section";
        txt += "\r\n";
        txt += "CL\t\t\t\t " + measurement+":" + "\t\t" + "9 CL";
        txt += "\r\n";
        txt += "Closure Distance " + measurement+":" + "\t\t" + "10 Closure Distance";
        txt += "\r\n";
        txt += "Closure Direction "+measurement+":" + "\t\t" + "11 Closure Direction";
        txt += "\r\n";
        txt += "DLS\t\t\t\t "+measurement+":" + "\t\t" + "12 Dog Leg Serverity";
        txt += "\r\n";
        txt += "DLA\t\t\t\t DEG:" + "\t\t" + "13 Dog Leg Angle";
        txt += "\r\n";
        txt += "BR\t\t\t\t "+ measurement+":" + "\t\t" + "14 B/Rate";
        txt += "\r\n";
        txt += "WR\t\t\t\t "+measurement+":" + "\t\t" + "15 Walk Rate";
        txt += "\r\n";
        txt += "TFO\t\t\t\t DEG:" + "\t\t" + "16 Toolface orientation";
        txt += "\r\n";

        txt += "~ASCII LOG DATA SECTION";
        txt += "\r\n";
        foreach (GridDataItem item in RadGridSurveys.MasterTableView.Items)
        {

            foreach (TableCell cell in item.Cells)
            {
                double outNum;
                if (double.TryParse(cell.Text, out outNum))
                {
                    txt += cell.Text + "\t\t";
                }
            }
            txt += "\r\n";
        }


        ViewState["CurveGroupName"] = ddlCurveGroup.SelectedText;
        ViewState["CurveName"] = ddlCurve.SelectedText;
        string fileName = string.Format("attachment;filename={0}_{1}_{2}_GammaExport.las", dateNow.Date.ToShortDateString(), ViewState["CurveGroupName"].ToString(), ViewState["CurveName"].ToString());

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", fileName);
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        Response.Flush();
        Response.End();
        
    }
    protected void btnExportToText_Click(object sender, EventArgs e)
    {
        if (ddlCurve.SelectedValue == "0")
        {
            lblExportError.Text = "No Data To Export, Please Select Data";
            lblExportError.Visible = true;
            return;
        }
        Export();

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblSurveyCount.Text = "Survey Count : ";
        ddlCompany.SelectedValue = "0";
        ddlCurveGroup.Items.Clear();
        //ddlCurveGroup.DataTextField = "CurveGroupName";
        //ddlCurveGroup.DataValueField = "ID";
        //ddlCurveGroup.DataBind();
        ddlCurveGroup.Items.Add(new DropDownListItem("-Select-", "0"));

        ddlTarget.Items.Clear();
        ddlCurve.Items.Clear();
        ddlTarget.Items.Add(new DropDownListItem("-Select-", "0"));
        ddlCurve.Items.Add(new DropDownListItem("-Select-", "0"));
        ddlCurveGroup.Enabled = false;
        ddlTarget.Enabled = false;
        ddlCurve.Enabled = false;
        

        txtCurveGroupID.Text = "";
        txtCurveGroupName.Text = "";
        txtJobNumber.Text = "";
        ddlMethodOfCalculation.SelectedValue = "0";
        ddlUnitsConvert.SelectedValue = "0";
        ddlMeasurementUnits.SelectedValue = "0";
        ddlInputDirection.SelectedValue = "0";
        ddlOutputDirection.SelectedValue = "0";
        ddlVerticalSection.SelectedValue = "0";

        txtEWOffset.Text = "0.00";
        txtNSOffset.Text = "0.00";

        txtTargetID.Text = "";
        txtTargetName.Text = "";
        txtTargetShape.Text = "";
        txtTargetTVD.Text = "0.00";

        txtCurveID.Text = "";
        txtCurveName.Text = "";
        txtCurveType.Text = "";
        txtNorthOffset.Text = "0.00";
        txtEastOffset.Text = "0.00";
        txtVSDirection.Text = "0.00";
        txtRKBElevation.Text = "0.00";
        RadGridSurveys_NeedDataSource(null, null);
        RadGridSurveys.DataBind();

        txtAdditionalMD.Text = "0.00";
        txtAdditionalIncl.Text = "0.00";
        txtAdditionalAzimuth.Text = "0.00";
        txtAdditionalTVD.Text = "0.00";
        txtAdditionalNS.Text = "0.00";
        txtAdditionalEW.Text = "0.00";
        txtAdditionalVS.Text = "0.00";
        txtWRate.Text = "0.00";
        txtAdditionalBRate.Text = "0.00";
        txtAdditionalDLS.Text = "0.00";
        txtTFO.Text = "0.00";
        txtAdditionalClosure.Text = "0.00";
        txtAdditionalAT.Text = "0.00";
        btnEditCurve.Enabled = false;
        btnEditCurveGroup.Enabled = false;
    }
    #endregion 
    #region Grid Events 
    protected void RadGridSurveys_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        //nfv- fake survey bind. 'Generic Tie in' 
        FakeSurvey newSurvey = new FakeSurvey();
        newSurvey.MD = 0.0;
        newSurvey.Inclination = 0.00;
        newSurvey.Azimuth = 0.00;
        newSurvey.TVD = 0.00;
        newSurvey.SubseasTVD = 0.00;
        newSurvey.NS = 0.00;
        newSurvey.EW = 0.00;
        newSurvey.VerticalSection = 0.00;
        newSurvey.CL = 0.00;
        newSurvey.ClosureDistance = 0.00;
        newSurvey.ClosureDirection = 0.00;
        newSurvey.DLS = 0.00;
        newSurvey.DLA = 0.00;
        newSurvey.BR = 0.00;
        newSurvey.WR = 0.00;



        DataTable newTable = new DataTable();
        newTable.Columns.Add("Name");
        newTable.Columns.Add("MD");
        newTable.Columns.Add("Inclination");
        newTable.Columns.Add("Azimuth");
        newTable.Columns.Add("TVD");
        newTable.Columns.Add("SubseasTVD");
        newTable.Columns.Add("NS");
        newTable.Columns.Add("EW");
        newTable.Columns.Add("VerticalSection");
        newTable.Columns.Add("CL");
        newTable.Columns.Add("ClosureDistance");
        newTable.Columns.Add("ClosureDirection");
        newTable.Columns.Add("DLS");
        newTable.Columns.Add("DLA");
        newTable.Columns.Add("BR");
        newTable.Columns.Add("WR");
        newTable.Columns.Add("TFO");
        newTable.Columns.Add("ID");
        newTable.Columns.Add("RowNumber");
        newTable.Columns.Add("SurveyComment");

        DataRow newRow;

        newRow = newTable.NewRow();
        newRow["Name"] = "";
        newRow["MD"] = newSurvey.MD;
        newRow["Inclination"] = newSurvey.Inclination;
        newRow["Azimuth"] = newSurvey.Azimuth;
        newRow["TVD"] = newSurvey.TVD;
        newRow["SubseasTVD"] = newSurvey.SubseasTVD;
        newRow["NS"] = newSurvey.NS;
        newRow["EW"] = newSurvey.EW;
        newRow["VerticalSection"] = newSurvey.VerticalSection;
        newRow["CL"] = newSurvey.CL;
        newRow["ClosureDistance"] = newSurvey.ClosureDistance;
        newRow["ClosureDirection"] = newSurvey.ClosureDirection;
        newRow["DLS"] = newSurvey.DLS;
        newRow["DLA"] = newSurvey.DLA;
        newRow["BR"] = newSurvey.BR;
        newRow["WR"] = newSurvey.WR;
        newRow["TFO"] = newSurvey.TFO;
        newRow["ID"] = 0;
        newRow["RowNumber"] = 0;
        newRow["SurveyComment"] = "";

        newTable.Rows.Add(newRow);

        ViewState["SurveyTable"] = newTable;
        RadGridSurveys.DataSource = newTable;

        (RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display = false;
    }
    protected void RadGridSurveys_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }
    #endregion
    #region Index Change Events 
    protected void ddlCurveGroup_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        lblSurveyCount.Text = "Survey Count : ";
        ViewState["CurveGroupID"] = ddlCurveGroup.SelectedValue;
        if (ddlCurveGroup.SelectedValue != "0")
        {
            ddlTarget.Items.Clear();
            ddlCurve.Items.Clear();
            ddlCurve.Items.Add(new DropDownListItem("-Select-", "0"));

            ddlTarget.Enabled = true;
            DropDownListItem item = new DropDownListItem("-Select-", "0");


            ddlTarget.Items.Add(item);
            ddlTarget.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(Int32.Parse(ddlCurveGroup.SelectedValue));
            ddlTarget.DataTextField = "TargetName";
            ddlTarget.DataValueField = "TargetID";
            ddlTarget.DataBind();


            ddlTarget.SelectedValue = "0";
            ddlCurve.SelectedValue = "0";
            txtTargetID.Text = "";
            txtTargetName.Text = "";
            txtTargetShape.Text = "";
            txtTargetTVD.Text = "0.00";

            txtCurveID.Text = "";
            txtCurveName.Text = "";
            txtCurveType.Text = "";
            txtNorthOffset.Text = "0.00";
            txtEastOffset.Text = "0.00";
            txtVSDirection.Text = "0.00";
            txtRKBElevation.Text = "0.00";
            RadGridSurveys_NeedDataSource(null, null);
            RadGridSurveys.DataBind();

            txtAdditionalMD.Text = "0.00";
            txtAdditionalIncl.Text = "0.00";
            txtAdditionalAzimuth.Text = "0.00";
            txtAdditionalTVD.Text = "0.00";
            txtAdditionalNS.Text = "0.00";
            txtAdditionalEW.Text = "0.00";
            txtAdditionalVS.Text = "0.00";
            txtWRate.Text = "0.00";
            txtAdditionalBRate.Text = "0.00";
            txtAdditionalDLS.Text = "0.00";
            txtTFO.Text = "0.00";
            txtAdditionalClosure.Text = "0.00";
            txtAdditionalAT.Text = "0.00";



            DataTable dt = new DataTable();
            dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupInfo(Int32.Parse(ddlCurveGroup.SelectedValue));
            if (dt.Rows.Count != 0)
            {
                DataRow row = dt.Rows[0];
                txtCurveGroupID.Text = row["ID"].ToString();
                txtCurveGroupName.Text = row["CurveGroupName"].ToString();
                txtJobNumber.Text = row["JobNumber"].ToString();
                ddlMethodOfCalculation.SelectedText = row["MethodOfCalculation"].ToString();
                ddlMeasurementUnits.SelectedText = row["MeasurementUnits"].ToString();

                ddlInputDirection.SelectedText = row["Input"].ToString();
                ddlOutputDirection.SelectedText = row["Output"].ToString();
                ddlDoglegSeverity.SelectedText = row["Dogleg"].ToString();
                ddlVerticalSection.SelectedText = row["VerticalSectionRef"].ToString();



                txtEWOffset.Text = row["EWOffset"].ToString();
                txtNSOffset.Text = row["NSOffset"].ToString();
                if (ddlMeasurementUnits.SelectedText == "Meters")
                {
                    DogLegSeverityRow.Visible = true;


                }
                if (ddlVerticalSection.SelectedText == "Wellhead")
                {
                    txtEWOffset.Enabled = false;
                    txtNSOffset.Enabled = false;

                }
                else
                {
                    //txtNSOffset.Enabled = true;
                    //txtEWOffset.Enabled = true;
                    txtNSOffset.ReadOnly = false;
                    txtEWOffset.ReadOnly = false;
                }


                ViewState["Calculations"] = row["MethodOfCalculation"].ToString();
                ViewState["Units"] = row["MeasurementUnits"].ToString();
                ViewState["Input"] = row["Input"].ToString();
                ViewState["Output"] = row["Output"].ToString();
                ViewState["Dogleg"] = row["Dogleg"].ToString();
                ViewState["VSEWOffset"] = row["EWOffset"].ToString();
                ViewState["VerticalSectionRef"] = row["VerticalSectionRef"].ToString();
                ViewState["VSNSOffset"] = row["NSOffset"].ToString();

                ViewState["LeaseWell"] = row["LeaseWell"].ToString();
                ViewState["JobLocation"] = row["JobLocation"].ToString();
                ViewState["RigName"] = row["RigName"].ToString();
                ViewState["Country"] = row["Country"].ToString();
                ViewState["State"] = row["State"].ToString();
            }
        }
        else
        {

            ddlTarget.Enabled = false;
            ddlCurve.Enabled = false;

            ddlTarget.Items.Clear();
            ddlCurve.Items.Clear();
            ddlCurve.Items.Add(new DropDownListItem("-Select-", "0"));
            DropDownListItem item = new DropDownListItem("-Select-", "0");
            ddlTarget.Items.Add(item);

            txtCurveGroupID.Text = "";
            txtCurveGroupName.Text = "";
            txtJobNumber.Text = "";
            ddlMethodOfCalculation.SelectedValue = "0";
            ddlUnitsConvert.SelectedValue = "0";
            ddlMeasurementUnits.SelectedValue = "0";
            ddlInputDirection.SelectedValue = "0";
            ddlOutputDirection.SelectedValue = "0";
            ddlVerticalSection.SelectedValue = "0";

            txtEWOffset.Text = "0.00";
            txtNSOffset.Text = "0.00";

            txtTargetID.Text = "";
            txtTargetName.Text = "";
            txtTargetShape.Text = "";
            txtTargetTVD.Text = "0.00";

            txtCurveID.Text = "";
            txtCurveName.Text = "";
            txtCurveType.Text = "";
            txtNorthOffset.Text = "0.00";
            txtEastOffset.Text = "0.00";
            txtVSDirection.Text = "0.00";
            txtRKBElevation.Text = "0.00";
            RadGridSurveys_NeedDataSource(null, null);
            RadGridSurveys.DataBind();

            txtAdditionalMD.Text = "0.00";
            txtAdditionalIncl.Text = "0.00";
            txtAdditionalAzimuth.Text = "0.00";
            txtAdditionalTVD.Text = "0.00";
            txtAdditionalNS.Text = "0.00";
            txtAdditionalEW.Text = "0.00";
            txtAdditionalVS.Text = "0.00";
            txtWRate.Text = "0.00";
            txtAdditionalBRate.Text = "0.00";
            txtAdditionalDLS.Text = "0.00";
            txtTFO.Text = "0.00";
            txtAdditionalClosure.Text = "0.00";
            txtAdditionalAT.Text = "0.00";
        }
        updPnl1.Update();
    }
    protected void ddlTarget_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        lblSurveyCount.Text = "Survey Count : ";
        if (ddlTarget.SelectedValue != "0")
        {
            ddlCurve.Enabled = true;
            ddlCurve.Items.Clear();
            DropDownListItem item = new DropDownListItem("-Select-", "0");
            ddlCurve.Items.Add(item);
            ddlCurve.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Int32.Parse(ddlTarget.SelectedValue));
            ddlCurve.DataTextField = "Name";
            ddlCurve.DataValueField = "ID";
            ddlCurve.DataBind();
            DataTable dt2 = RigTrack.DatabaseObjects.RigTrackDO.GetTargetForSurvey(Int32.Parse(ddlTarget.SelectedValue));
            if (dt2.Rows.Count != 0)
            {
                DataRow row = dt2.Rows[0];
                txtTargetID.Text = row["ID"].ToString();
                txtTargetName.Text = row["Name"].ToString();
                txtTargetShape.Text = row["Shape"].ToString();
                txtTargetTVD.Text = row["TVD"].ToString();
            }
        }
        else
        {
            ddlCurve.Enabled = false;
            ddlCurve.Items.Clear();
            ddlCurve.Items.Add(new DropDownListItem("-Select-", "0"));
            txtCurveID.Text = "";
            txtCurveName.Text = "";
            txtCurveType.Text = "";
            txtNorthOffset.Text = "0.00";
            txtEastOffset.Text = "0.00";
            txtVSDirection.Text = "0.00";
            txtRKBElevation.Text = "0.00";

            txtTargetID.Text = "";
            txtTargetName.Text = "";
            txtTargetShape.Text = "";
            txtTargetTVD.Text = "0.00";


            RadGridSurveys_NeedDataSource(null, null);
            RadGridSurveys.DataBind();


            txtAdditionalMD.Text = "0.00";
            txtAdditionalIncl.Text = "0.00";
            txtAdditionalAzimuth.Text = "0.00";
            txtAdditionalTVD.Text = "0.00";
            txtAdditionalNS.Text = "0.00";
            txtAdditionalEW.Text = "0.00";
            txtAdditionalVS.Text = "0.00";
            txtWRate.Text = "0.00";
            txtAdditionalBRate.Text = "0.00";
            txtAdditionalDLS.Text = "0.00";
            txtTFO.Text = "0.00";
            txtAdditionalClosure.Text = "0.00";
            txtAdditionalAT.Text = "0.00";
        }
    }
    protected void ddlCurve_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCurveGroup.SelectedValue == "0" || ddlCurve.SelectedValue == "0" || ddlTarget.SelectedValue=="0")
        {
            //Dont search
            ViewState["CurveID"] = null;
            btnEditCurveGroup.Enabled = false;

            txtName.Enabled = false;
            txtMeasurementDepth.Enabled = false;
            txtInclination.Enabled = false;
            txtAzimuth.Enabled = false;
            txtSurveyComments.Enabled = false;
            btnAddSurvey.Enabled = false;
            btnEditCurve.Enabled = false;


            if (ddlCurve.SelectedValue == "0")
            {
                txtCurveID.Text = "";
                txtCurveName.Text = "";
                txtCurveType.Text = "";
                txtNorthOffset.Text = "0.00";
                txtEastOffset.Text = "0.00";
                txtVSDirection.Text = "0.00";
                txtRKBElevation.Text = "0.00";


                RadGridSurveys_NeedDataSource(null, null);
                RadGridSurveys.DataBind();


                txtAdditionalMD.Text = "0.00";
                txtAdditionalIncl.Text = "0.00";
                txtAdditionalAzimuth.Text = "0.00";
                txtAdditionalTVD.Text = "0.00";
                txtAdditionalNS.Text = "0.00";
                txtAdditionalEW.Text = "0.00";
                txtAdditionalVS.Text = "0.00";
                txtWRate.Text = "0.00";
                txtAdditionalBRate.Text = "0.00";
                txtAdditionalDLS.Text = "0.00";
                txtTFO.Text = "0.00";
                txtAdditionalClosure.Text = "0.00";
                txtAdditionalAT.Text = "0.00";
            }
        }
        else
        {

            btnEditCurveGroup.Enabled = true;
            btnEditCurve.Enabled = true;
            txtName.Enabled = true;
            txtMeasurementDepth.Enabled = true;
            txtInclination.Enabled = true;
            txtAzimuth.Enabled = true;
            txtSurveyComments.Enabled = true;
            btnAddSurvey.Enabled = true;
            int curveID = Int32.Parse(ddlCurve.SelectedValue);
            int curveGroupID = Int32.Parse(ddlCurveGroup.SelectedValue);
            ViewState["CurveID"] = curveID;
            ViewState["CurveGroupID"] = curveGroupID;
            DataTable dt2 = new DataTable();
            dt2 = RigTrack.DatabaseObjects.RigTrackDO.GetCurveInfo(curveID);
            if (dt2.Rows.Count != 0)
            {
                DataRow row = dt2.Rows[0];
                txtCurveID.Text = row["ID"].ToString();
                
                txtCurveName.Text = row["Name"].ToString();
                ViewState["CurveName"] = row["Name"].ToString();
                txtCurveType.Text = row["CurveType"].ToString();
                ViewState["CurveType"] = row["CurveType"].ToString();
                ViewState["CurveNumber"] = row["Number"].ToString();
                txtNorthOffset.Text = row["NorthOffset"].ToString();
                txtEastOffset.Text = row["EastOffset"].ToString();
                txtVSDirection.Text = row["VSDirection"].ToString();
                txtRKBElevation.Text = row["RKBElevation"].ToString();

                string locationID = row["LocationID"].ToString();
                if (locationID == "1000" || string.IsNullOrEmpty(locationID))
                {
                    rbSensor.Checked = true;
                    rbBHL.Checked = false;
                    ViewState["Location"] = "S";
                }
                else
                {
                    rbBHL.Checked = true;
                    rbSensor.Checked = false;
                    ViewState["Location"] = "B";
                }

                string bitToSensor = row["bitToSensor"].ToString();
                txtBitToSensor.Text = bitToSensor;
                ViewState["bitToSensor"] = bitToSensor;
                bool listDistance = false;
                try
                {
                    listDistance = bool.Parse(row["ListDistanceBool"].ToString());
                }
                catch (Exception ex)
                {
                    listDistance = false;
                }
                
                if (listDistance == true)
                {
                    rbOn.Checked = true;
                    rbOff.Checked = false;
                    ViewState["ListDistance"] = "On";
                }
                else
                {
                    rbOff.Checked = true;
                    rbOn.Checked = false;
                    ViewState["ListDistance"] = "Off";
                }
                string comparisonCurve = row["ComparisonCurve"].ToString();
                txtComparisonCurve.Text = comparisonCurve;
                ViewState["ComparisonCurve"] = comparisonCurve;

                string HSide = row["AtHSide"].ToString();
                txtHSide.Text = HSide;
                ViewState["HSide"] = HSide;
                string tvdComp = row["TVDComp"].ToString();
                txtTVDComp.Text = tvdComp;
                ViewState["TVDComp"] = tvdComp;

                ViewState["NorthOffset"] = row["NorthOffset"].ToString();
                ViewState["EastOffset"] = row["EastOffset"].ToString();
                ViewState["VSDirection"] = row["VSDirection"].ToString();
                ViewState["RKBElevation"] = row["RKBElevation"].ToString();
                ViewState["CurveNumber"] = row["Number"].ToString();
                ViewState["TargetID"] = ddlTarget.SelectedValue;
                
            }



            DataTable dt = new DataTable();
            dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForCurve(curveID);
            //IF no surveys, make a generic tie in. 
            if (dt.Rows.Count == 0)
            {

                RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                DTO.ID = 0;
                DTO.CurveID = curveID;
                DTO.CurveGroupID = curveGroupID;
                DTO.Name = "Tie In";
                DTO.MD = 0.00;
                DTO.INC = 0.00;
                DTO.Azimuth = 0.00;
                DTO.TVD = 0.00;
                DTO.TieInSubsea = 0.00;
                DTO.SubseaTVD = 0.00 - double.Parse(ViewState["RKBElevation"].ToString()) ;
                DTO.TieInNS = 0.00;
                DTO.NS = 0.00 + double.Parse(ViewState["NorthOffset"].ToString()) ;
                DTO.TieInEW = 0.00;
                DTO.EW = 0.00 + double.Parse(ViewState["EastOffset"].ToString());
                DTO.TieInVerticalSection = 0.00;
                DTO.VerticalSection = 0.00 /*+ double.Parse(ViewState["RKBElevation"].ToString())*/;
                DTO.CL = 0.00;
                DTO.ClosureDistance = 0.00;
                DTO.ClosureDirection = 0.00;
                DTO.DLS = 0.00;
                DTO.DLA = 0.00;
                DTO.BR = 0.00;
                DTO.WR = 0.00;
                DTO.TFO = 0.00;
                DTO.SurveyComment = "Tie In Survey for Curve:" + curveID.ToString();
                DTO.isActive = true;
                DTO.RowNumber = 0;

                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
                dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForCurve(curveID);
            }
            ViewState["SurveyTable"] = dt;
            RadGridSurveys.DataSource = dt;
            RadGridSurveys.Rebind();

            lblSurveyCount.Text = String.Format("Survey Count :{0}", dt.Rows.Count - 1);
            if (dt.Rows.Count > 1)
            {
                DataRow row2 = dt.Rows[dt.Rows.Count - 1];
                DataRow row1 = dt.Rows[dt.Rows.Count - 2];

                double row2MD = double.Parse(row2["MD"].ToString());
                double row2Inc = double.Parse(row2["Inclination"].ToString());
                double row2AZ = double.Parse(row2["Azimuth"].ToString());
                double row2TVD = double.Parse(row2["TVD"].ToString());
                double row2NS = double.Parse(row2["NS"].ToString());
                double row2EW = double.Parse(row2["EW"].ToString());
                double row2VS = double.Parse(row2["VerticalSection"].ToString());
                double row2WR = double.Parse(row2["WR"].ToString());
                double row2BR = double.Parse(row2["BR"].ToString());
                double row2DLS = double.Parse(row2["DLS"].ToString());
                double row2TFO = double.Parse(row2["TFO"].ToString());
                double row2Distance = double.Parse(row2["ClosureDistance"].ToString());
                double row2Direction = double.Parse(row2["ClosureDirection"].ToString());

                double row1MD = double.Parse(row1["MD"].ToString());
                double row1Inc = double.Parse(row1["Inclination"].ToString());
                double row1AZ = double.Parse(row1["Azimuth"].ToString());
                double row1TVD = double.Parse(row1["TVD"].ToString());
                double row1NS = double.Parse(row1["NS"].ToString());
                double row1EW = double.Parse(row1["EW"].ToString());
                double row1VS = double.Parse(row1["VerticalSection"].ToString());
                double row1WR = double.Parse(row1["WR"].ToString());
                double row1BR = double.Parse(row1["BR"].ToString());
                double row1DLS = double.Parse(row1["DLS"].ToString());
                double row1TFO = double.Parse(row1["TFO"].ToString());
                double row1Distance = double.Parse(row1["ClosureDistance"].ToString());
                double row1Direction = double.Parse(row1["ClosureDirection"].ToString());
                double bitToSensor = 0.0;
                if (rbBHL.Checked)
                {
                    try
                    {
                        bitToSensor = double.Parse(txtBitToSensor.Text);
                    }
                    catch (Exception ex)
                    {
                        bitToSensor = 0.00;
                        txtBitToSensor.Text = "0.00";
                    }
                    txtAdditionalMD.Text = (Math.Round((row2MD + bitToSensor), 4)).ToString();
                    txtAdditionalIncl.Text = (Math.Round((row2Inc - row1Inc) + row2Inc, 2)).ToString();
                    txtAdditionalAzimuth.Text = (Math.Round((row2AZ - row1AZ) + row2AZ, 2)).ToString();
                    txtAdditionalTVD.Text = (Math.Round((row2TVD - row1TVD) + row2TVD, 2)).ToString();
                    txtAdditionalNS.Text = (Math.Round((row2NS - row1NS) + row2NS, 2)).ToString();
                    txtAdditionalEW.Text = (Math.Round((row2EW - row1EW) + row2EW, 2)).ToString();
                    txtAdditionalVS.Text = (Math.Round((row2VS - row1VS) + row2VS, 2)).ToString();
                    txtWRate.Text = (Math.Round((row2WR - row1WR) + row2WR, 2)).ToString();
                    txtAdditionalBRate.Text = (Math.Round((row2BR - row1BR) + row2BR, 2)).ToString();
                    txtAdditionalDLS.Text = (Math.Round((row2DLS - row1DLS) + row2DLS, 2)).ToString();
                    txtTFO.Text = (Math.Round((row2TFO - row1TFO) + row1TFO, 2)).ToString();
                    txtAdditionalClosure.Text = (Math.Round((row2Distance + row1Distance) + row2Distance, 2)).ToString();
                    txtAdditionalAT.Text = (Math.Round((row2Direction + row1Direction) + row2Direction, 2)).ToString();
                }
                else
                {
                    txtAdditionalMD.Text = row2MD.ToString();
                    txtAdditionalIncl.Text = row2Inc.ToString();
                    txtAdditionalAzimuth.Text = row2AZ.ToString();
                    txtAdditionalTVD.Text = row2TVD.ToString();
                    txtAdditionalNS.Text = row2NS.ToString();
                    txtAdditionalEW.Text = row2EW.ToString();
                    txtAdditionalVS.Text = row2VS.ToString();
                    txtWRate.Text = row2WR.ToString();
                    txtAdditionalBRate.Text = row2BR.ToString();
                    txtAdditionalDLS.Text = row2DLS.ToString();
                    txtTFO.Text = row2TFO.ToString();
                    txtAdditionalClosure.Text = row2Distance.ToString();
                    txtAdditionalAT.Text = row2Direction.ToString();

                }
            }
            else
            {
                txtAdditionalMD.Text = "0.00";
                txtAdditionalIncl.Text = "0.00";
                txtAdditionalAzimuth.Text = "0.00";
                txtAdditionalTVD.Text = "0.00";
                txtAdditionalNS.Text = "0.00";
                txtAdditionalEW.Text = "0.00";
                txtAdditionalVS.Text = "0.00";
                txtWRate.Text = "0.00";
                txtAdditionalBRate.Text = "0.00";
                txtAdditionalDLS.Text = "0.00";
                txtTFO.Text = "0.00";
                txtAdditionalClosure.Text = "0.00";
                txtAdditionalAT.Text = "0.00";
            }



            

        }
        
    }
    protected void chkbx_CheckedChanged(object sender, EventArgs e)
    {
        DataTable curveInfo = null;
        if (ViewState["CurveID"] != null)
        {
            curveInfo = RigTrack.DatabaseObjects.RigTrackDO.GetCurveInfo(Int32.Parse(ViewState["CurveID"].ToString()));
        }
        if (ddlCurve.SelectedValue == "0")
        {
            foreach (GridDataItem dataItem in RadGridSurveys.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkbx") as CheckBox) == (sender as CheckBox))
                {
                    (dataItem.FindControl("chkbx") as CheckBox).Checked = false;
                }
            }
            return;
        }
        MeasurementDepthError.Visible = false;
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;

        foreach (GridDataItem dataItem in RadGridSurveys.MasterTableView.Items)
        {

            if (curveInfo.Rows.Count > 0)
            {

                if (curveInfo.Rows[0]["Number"].ToString() == "0")
                {
                    if ((dataItem.FindControl("chkbx") as CheckBox) == (sender as CheckBox))
                    {
                        (dataItem.FindControl("chkbx") as CheckBox).Checked = false;
                    }
                    lblWellPlanError.Visible = true;

                }
            }


            if ((dataItem.FindControl("chkbx") as CheckBox) != (sender as CheckBox))
            {
                (dataItem.FindControl("chkbx") as CheckBox).Checked = false;
              
                //TieInRow1.Visible = false;
                //TieInRow2.Visible = false;
               // TieInRow3.Visible = false;
               // btnAddLastTargetSurvey.Visible = false;
               // btnDeleteSurvey.Visible = false;
            }
            else
            {
                if ((dataItem.FindControl("chkbx") as CheckBox).Checked == false)
                {

                    TieInRow1.Visible = false;
                    TieInRow2.Visible = false;
                    TieInRow3.Visible = false;
                    
                    btnAddSurvey.Visible = true;
                    btnEditSurvey.Visible = false;
                    btnDeleteSurvey.Visible = false;
                    btnAddLastTargetSurvey.Visible = false;
                    txtName.Text = "";
                    txtSurveyComments.Text = "";
                    txtMeasurementDepth.Text = "0.00";
                    txtInclination.Text = "0.00";
                    txtAzimuth.Text = "0.00";
                    hiddenSurveyID.Value = "";
                }
                else
                {
                    if (dataItem.GetDataKeyValue("RowNumber").ToString() == "0")
                    {
                        //We have the tie in
                        TieInRow1.Visible = true;
                        TieInRow2.Visible = true;
                        TieInRow3.Visible = true;
                        //btnAddLastTargetSurvey.Enabled = true;
                        btnAddLastTargetSurvey.Visible = true;
                        btnDeleteSurvey.Enabled = false;

                    }
                    else
                    {
                        if (curveInfo.Rows[0]["Number"].ToString() == "0")
                        {
                            lblWellPlanError.Visible = true;
                            break;
                        }
                        
                        TieInRow2.Visible = false;
                        TieInRow1.Visible = false;
                        TieInRow3.Visible = false;
                        btnAddLastTargetSurvey.Visible = false;
                        btnDeleteSurvey.Visible = true;
                        btnDeleteSurvey.Enabled = true;
                    }
                    txtMeasurementDepth.Text = dataItem["MD"].Text;
                    txtInclination.Text = dataItem["Inclination"].Text;
                    txtAzimuth.Text = dataItem["Azimuth"].Text;
                    txtName.Text = dataItem["Name"].Text;
                    txtSurveyComments.Text = dataItem["SurveyComment"].Text;
                    hiddenSurveyID.Value = dataItem.GetDataKeyValue("ID").ToString();
                    ViewState["SurveyID"] =  dataItem.GetDataKeyValue("ID").ToString();

                    btnAddSurvey.Visible = false;
                    btnEditSurvey.Visible = true;
                    btnDeleteSurvey.Visible = true;
                }

            }
        }

    }
    protected void ddlMeasurementUnits_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        
        MeasurementDepthError.Visible = false;
        if (ddlMeasurementUnits.SelectedText == "Meters")
        {
            DogLegSeverityRow.Visible = true;
        }
        else
        {
            DogLegSeverityRow.Visible = false;
        }
    }
    protected void ddlVerticalSection_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        MeasurementDepthError.Visible = false;
        if (ddlVerticalSection.SelectedText == "Wellhead")
        {
            txtEWOffset.Text = "0.0";
            txtEWOffset.Enabled = false;
            txtNSOffset.Text = "0.0";
            txtNSOffset.Enabled = false;
        }
        else
        {
            txtEWOffset.Enabled = true;
            txtNSOffset.Enabled = true;
            txtEWOffset.ReadOnly = false;
            txtNSOffset.ReadOnly = false;
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {
            ddlCurveGroup.Enabled = true;

            ddlCurveGroup.Items.Clear();
            ddlCurveGroup.Items.Add(new DropDownListItem("-Select-", "0"));

            ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            ddlCurveGroup.DataTextField = "CurveGroupName";
            ddlCurveGroup.DataValueField = "ID";
            ddlCurveGroup.DataBind();

            ddlCurveGroup.SelectedValue = "0";

            ddlTarget.Items.Clear();
            ddlTarget.Items.Add(new DropDownListItem("-Select-", "0"));
            ddlTarget.SelectedValue = "0";
            ddlTarget.Enabled = false;
        }
        else
        {
            ddlCurveGroup.Enabled = false;
            ddlTarget.Enabled = false;
            ddlCurve.Enabled = false;

            ddlCurveGroup.Items.Clear();
            //ddlCurveGroup.DataTextField = "CurveGroupName";
            //ddlCurveGroup.DataValueField = "ID";
            //ddlCurveGroup.DataBind();
            ddlCurveGroup.Items.Add(new DropDownListItem("-Select-","0"));

            ddlTarget.Items.Clear();
            ddlCurve.Items.Clear();
            ddlTarget.Items.Add(new DropDownListItem("-Select-", "0"));
            ddlCurve.Items.Add(new DropDownListItem("-Select-", "0"));

            
            
            txtCurveGroupID.Text = "";
            txtCurveGroupName.Text = "";
            txtJobNumber.Text = "";
            ddlMethodOfCalculation.SelectedValue = "0";
            ddlUnitsConvert.SelectedValue = "0";
            ddlMeasurementUnits.SelectedValue = "0";
            ddlInputDirection.SelectedValue = "0";
            ddlOutputDirection.SelectedValue = "0";
            ddlVerticalSection.SelectedValue = "0";

            txtEWOffset.Text = "0.00";
            txtNSOffset.Text = "0.00";

            txtTargetID.Text = "";
            txtTargetName.Text = "";
            txtTargetShape.Text = "";
            txtTargetTVD.Text = "0.00";

            txtCurveID.Text = "";
            txtCurveName.Text = "";
            txtCurveType.Text = "";
            txtNorthOffset.Text = "0.00";
            txtEastOffset.Text = "0.00";
            txtVSDirection.Text = "0.00";
            txtRKBElevation.Text = "0.00";
            RadGridSurveys_NeedDataSource(null, null);
            RadGridSurveys.DataBind();

            txtAdditionalMD.Text = "0.00";
            txtAdditionalIncl.Text = "0.00";
            txtAdditionalAzimuth.Text = "0.00";
            txtAdditionalTVD.Text = "0.00";
            txtAdditionalNS.Text = "0.00";
            txtAdditionalEW.Text = "0.00";
            txtAdditionalVS.Text = "0.00";
            txtWRate.Text = "0.00";
            txtAdditionalBRate.Text = "0.00";
            txtAdditionalDLS.Text = "0.00";
            txtTFO.Text = "0.00";
            txtAdditionalClosure.Text = "0.00";
            txtAdditionalAT.Text = "0.00";
        }
        updPnl1.Update();
    }
    #endregion 
    

    
    
    
    
    

    
   
    
    
}