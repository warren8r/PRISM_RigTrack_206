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
using System.Reflection;


public partial class Modules_RigTrack_CreateCurveGroup : System.Web.UI.Page
{
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {

            //if (btnYes.Checked == true)
            //{
            //    btnMeters.Enabled = true;
            //    btnFeet.Enabled = true;
            //}

            //else
            //{
            //    btnMeters.Enabled = false;
            //    btnFeet.Enabled = false;
            //}

            //if (btnMeters.Checked == true)
            //{
            //    btnPer30Meters.Enabled = true;
            //    btnPer10meters.Enabled = true;
            //}


            RadGridTargets.DataSource = string.Empty;
            BindDropDownLists();

            if (Request.QueryString["CurveGroupID"] == null)
            {
                RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
                curveGroupDTO.ID = 0;
                //ViewState["CurveGroupID"] = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCurveGroup(curveGroupDTO);
                ViewState["CurveGroupID"] = 1003;
                ViewState["FromViewPage"] = false;
            }
            else
            {
                RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
                int curveGroupID = Int32.Parse(Request.QueryString["CurveGroupID"].ToString());
                //int curveGroupID = 1003;
                ViewState["CurveGroupID"] = curveGroupID;
                ViewState["FromViewPage"] = true;
                DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupFromID(curveGroupID);

                DisplayValues(dt);
            }

            TxtCurveGroupID.Text = ViewState["CurveGroupID"].ToString();

            ddlCountry_SelectedIndexChanged(null, null);
            btnYes_CheckedChanged(null, null);
            btnMeters_CheckedChanged(null, null);
        }
    }

    private void DisplayValues(DataTable dt)
    {
        TxtCurveGroupName.Text = dt.Rows[0]["CurveGroupName"].ToString();
        txtJobNumber.Text = dt.Rows[0]["JobNumber"].ToString();
        txtCompany.Text = dt.Rows[0]["Company"].ToString();
        txtLeaseWell.Text = dt.Rows[0]["LeaseWell"].ToString();
        txtLocation.Text = dt.Rows[0]["JobLocation"].ToString();
        txtRigName.Text = dt.Rows[0]["RigName"].ToString();
        ddlCountry.SelectedValue = dt.Rows[0]["CountryID"].ToString();
        ddlState.SelectedValue = dt.Rows[0]["StateID"].ToString();
        txtDeclination.Text = dt.Rows[0]["Declination"].ToString();
        txtGrid.Text = dt.Rows[0]["Grid"].ToString();
        txtRKB.Text = dt.Rows[0]["RKB"].ToString();
        ddlGLorMSL.SelectedValue = dt.Rows[0]["GLorMSLID"].ToString();

        txtEWOffset.Text = dt.Rows[0]["EWOffset"].ToString();
        txtNSOffset.Text = dt.Rows[0]["NSOffset"].ToString();
        txtWorkNumber.Text = dt.Rows[0]["WorkNumber"].ToString();
        txtPlanNumber.Text = dt.Rows[0]["PlanNumber"].ToString();
        txtMD.Text = dt.Rows[0]["MD"].ToString();
        txtIncl.Text = dt.Rows[0]["Incl"].ToString();
        txtAzimuth.Text = dt.Rows[0]["Azimuth"].ToString();
        txtTVD.Text = dt.Rows[0]["TVD"].ToString();
        txtNSCoord.Text = dt.Rows[0]["NSCoord"].ToString();
        txtEWCoord.Text = dt.Rows[0]["EWCoord"].ToString();
        txtvsection.Text = dt.Rows[0]["VSection"].ToString();
        txtWRate.Text = dt.Rows[0]["WRate"].ToString();
        txtBRate.Text = dt.Rows[0]["BRate"].ToString();

        txtDLS.Text = dt.Rows[0]["DLS"].ToString();
        txtTFO.Text = dt.Rows[0]["TFO"].ToString();
        txtClosure.Text = dt.Rows[0]["Closure"].ToString();
        txtLocationCurveGroup.Text = dt.Rows[0]["LocationID"].ToString();
        txtBitToSensor.Text = dt.Rows[0]["BitToSensor"].ToString();
        txtLeastDistance.Text = dt.Rows[0]["LeastDistanceText"].ToString();
        txtHSide.Text = dt.Rows[0]["AtHSide"].ToString();
        txtTVDComp.Text = dt.Rows[0]["TVDComp"].ToString();
        txtComparisonCurve.Text = dt.Rows[0]["ComparisonCurveText"].ToString();
        txtAT.Text = dt.Rows[0]["At"].ToString();

        int methodOfCalculation;
        Int32.TryParse(dt.Rows[0]["MethodOfCalculationID"].ToString(), out methodOfCalculation);
        switch (methodOfCalculation)
        {
            case 1000:
                btnMinimumCurvature.Checked = true;
                break;
            case 1001:
                btnRadiusOfCurvature.Checked = true;
                break;
            case 1002:
                btnAvergaeAngle.Checked = true;
                break;
            case 1003:
                btnTangentail.Checked = true;
                break;
            case 1004:
                btnBalancedTangentail.Checked = true;
                break;
        }

        int measurementUnits;
        Int32.TryParse(dt.Rows[0]["MeasurementUnitsID"].ToString(), out measurementUnits);
        if (measurementUnits == 1000)
        {
            btnMeters.Checked = false;
            btnFeet.Checked = true;
        }
        else if (measurementUnits == 1001)
        {
            btnFeet.Checked = false;
            btnMeters.Checked = true;
        }

        bool unitsConvert;
        bool.TryParse(dt.Rows[0]["UnitsConvert"].ToString(), out unitsConvert);
        if (unitsConvert)
        {
            btnNo.Checked = false;
            btnYes.Checked = true;
        }
        else if(!unitsConvert)
        {
            btnYes.Checked = false;
            btnNo.Checked = true;
        }

        int dogLegSeverity;
        Int32.TryParse(dt.Rows[0]["DogLegSeverityID"].ToString(), out dogLegSeverity);
        if (dogLegSeverity == 1000)
        {
            btnPer10meters.Checked = false;
            btnPer30Meters.Checked = true;
        }
        else if (dogLegSeverity == 1001)
        {
            btnPer30Meters.Checked = false;
            btnPer10meters.Checked = true;
        }

        int outputDirectionID;
        Int32.TryParse(dt.Rows[0]["OutputDirectionID"].ToString(), out outputDirectionID);
        if (outputDirectionID == 1000)
        {
            btnOutputQuadrant.Checked = false;
            btnOutputDecimal.Checked = true;
        }
        else if (outputDirectionID == 1001)
        {
            btnOutputDecimal.Checked = false;
            btnOutputQuadrant.Checked = true;
        }

        int inputDirectionID;
        Int32.TryParse(dt.Rows[0]["InputDirectionID"].ToString(), out inputDirectionID);
        if (inputDirectionID == 1000)
        {
            btnInputQuadrant.Checked = false;
            btnInputDecimal.Checked = true;
        }
        else if (inputDirectionID == 1001)
        {
            btnInputDecimal.Checked = false;
            btnInputQuadrant.Checked = true;
        }

        int verticalSectionRef;
        Int32.TryParse(dt.Rows[0]["VerticalSectionReferenceID"].ToString(), out verticalSectionRef);
        if (verticalSectionRef == 1000)
        {
            btnOtherReference.Checked = false;
            btnWellhead.Checked = true;
        }
        else if (verticalSectionRef == 1001)
        {
            btnWellhead.Checked = false;
            btnOtherReference.Checked = true;
        }

        if (dt.Rows[0]["LeastDistanceOnOff"].ToString() == "True")
        {
            ddlLeastDistance.SelectedValue = "1";
    }
        else
        {
            ddlLeastDistance.SelectedValue = "0";
        }
    }

    private void BindDropDownLists()
    {
        ddlCountry.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCountries();
        ddlCountry.DataTextField = "Name";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();

        ddlState.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllStates();
        ddlState.DataTextField = "Name";
        ddlState.DataValueField = "ID";
        ddlState.DataBind();
        ddlState.Enabled = false;

        ddlGLorMSL.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllGLMSLs();
        ddlGLorMSL.DataTextField = "Name";
        ddlGLorMSL.DataValueField = "ID";
        ddlGLorMSL.DataBind();
    }
    #endregion
    #region top bottons
    protected void btnRemoveSurvey_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddSurvey.aspx");
    }
    protected void btnNormal_Click(object sender, EventArgs e)
    {
        Response.Redirect("GenerateMosquito.aspx");
    }
    #endregion
    #region middle target buttons
    protected void btnRemoveTarget_Click(object sender, EventArgs e)
    {
        //Response.Redirect("CreateTargets.aspx");
    }
    protected void btnCreateGraphToSelectedTarget_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateGraph.aspx");
    }
    #endregion
    #region bottom buttons
    protected void btnSave_Click(object sender, EventArgs e)
    {
        double declination, ewOffset, nsOffset, md, incl, azimuth, tvd, nsCoord, ewCoord, vSection, wRate, bRate, dls, tfo, closure, at, bitToSensor;

        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
        curveGroupDTO.ID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        curveGroupDTO.CurveGroupName = TxtCurveGroupName.Text;
        curveGroupDTO.JobNumber = txtJobNumber.Text;
        curveGroupDTO.Company = 0;
        curveGroupDTO.LeaseWell = txtLeaseWell.Text;
        curveGroupDTO.JobLocation = txtLocation.Text;
        curveGroupDTO.RigName = txtRigName.Text;
        curveGroupDTO.CountryID = Int32.Parse(ddlCountry.SelectedValue);
        curveGroupDTO.StateID = Int32.Parse(ddlState.SelectedValue);
        double.TryParse(txtDeclination.Text, out declination);
        curveGroupDTO.Declination = declination;
        curveGroupDTO.Grid = txtGrid.Text;
        curveGroupDTO.RKB = txtRKB.Text;
        curveGroupDTO.GLorMSLID = Int32.Parse(ddlGLorMSL.SelectedValue);

        int methodOfCalculation = 0;
        if (btnMinimumCurvature.Checked)
        {
            methodOfCalculation = 1000;
        }
        else if (btnRadiusOfCurvature.Checked)
        {
            methodOfCalculation = 1001;
        }
        else if (btnAvergaeAngle.Checked)
        {
            methodOfCalculation = 1002;
        }
        else if (btnTangentail.Checked)
        {
            methodOfCalculation = 1003;
        }
        else if (btnBalancedTangentail.Checked)
        {
            methodOfCalculation = 1004;
        }

        curveGroupDTO.MethodOfCalculationID = methodOfCalculation;

        int dogLegSeverity = 0;
        if (btnPer30Meters.Checked)
        {
            dogLegSeverity = 1000;
        }
        else if (btnPer10meters.Checked)
        {
            dogLegSeverity = 1001;
        }

        curveGroupDTO.DogLegSeverityID = dogLegSeverity;

        int outputDirection = 0;
        if (btnOutputDecimal.Checked)
        {
            outputDirection = 1000;
        }
        else if (btnOutputQuadrant.Checked)
        {
            outputDirection = 1001;
        }

        curveGroupDTO.OutputDirectionID = outputDirection;

        int inputDirection = 0;
        if (btnInputDecimal.Checked)
        {
            inputDirection = 1000;
        }
        else if (btnInputQuadrant.Checked)
        {
            inputDirection = 1001;
        }

        curveGroupDTO.InputDirectionID = inputDirection;

        int verticalSectionRef = 0;
        if (btnWellhead.Checked)
        {
            verticalSectionRef = 1000;
        }
        else if (btnOtherReference.Checked)
        {
            verticalSectionRef = 1001;
        }

        curveGroupDTO.VerticalSectionReferenceID = verticalSectionRef;

        double.TryParse(txtEWOffset.Text, out ewOffset);
        double.TryParse(txtNSOffset.Text, out nsOffset);
        curveGroupDTO.EWOffset = ewOffset;
        curveGroupDTO.NSOffset = nsOffset;

        bool unitsConvert = false;
        if (btnYes.Checked)
        {
            unitsConvert = true;
        }

        curveGroupDTO.UnitsConvert = unitsConvert;

        int measurementUnits = 1000;
        if (btnMeters.Checked)
        {
            measurementUnits = 1001;
        }

        curveGroupDTO.MeasurementUnitsID = measurementUnits;

        if (ddlLeastDistance.SelectedValue == "0")
        {
            curveGroupDTO.LeastDistanceOnOff = false;
        }
        else if (ddlLeastDistance.SelectedValue == "1")
        {
            curveGroupDTO.LeastDistanceOnOff = true;
        }

        curveGroupDTO.WorkNumber = Int32.Parse(txtWorkNumber.Text);
        curveGroupDTO.PlanNumber = Int32.Parse(txtPlanNumber.Text);
        double.TryParse(txtMD.Text, out md);
        curveGroupDTO.MD = md;
        double.TryParse(txtIncl.Text, out incl);
        curveGroupDTO.Incl = incl;
        double.TryParse(txtAzimuth.Text, out azimuth);
        curveGroupDTO.Azimuth = azimuth;
        double.TryParse(txtTVD.Text, out tvd);
        curveGroupDTO.TVD = tvd;
        double.TryParse(txtNSCoord.Text, out nsCoord);
        curveGroupDTO.NSCoord = nsCoord;
        double.TryParse(txtEWCoord.Text, out ewCoord);
        curveGroupDTO.EWCoord = ewCoord;
        double.TryParse(txtvsection.Text, out vSection);
        curveGroupDTO.VSection = vSection;
        double.TryParse(txtWRate.Text, out wRate);
        curveGroupDTO.WRate = wRate;
        double.TryParse(txtBRate.Text, out bRate);
        curveGroupDTO.BRate = bRate;
        double.TryParse(txtDLS.Text, out dls);
        curveGroupDTO.DLS = dls;
        double.TryParse(txtTFO.Text, out tfo);
        curveGroupDTO.TFO = tfo;
        double.TryParse(txtClosure.Text, out closure);
        curveGroupDTO.Closure = closure;
        double.TryParse(txtBitToSensor.Text, out bitToSensor);
        curveGroupDTO.BitToSensor = bitToSensor; 
        curveGroupDTO.LeaseDistanceText = txtLeastDistance.Text;
        curveGroupDTO.AtHSide = txtHSide.Text;
        curveGroupDTO.TVDComp = txtTVDComp.Text;
        curveGroupDTO.ComparisonCurveText = txtComparisonCurve.Text;
        curveGroupDTO.At = double.Parse(txtAT.Text);
        curveGroupDTO.isActive = true;

        RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCurveGroup(curveGroupDTO);

        int curveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupFromID(curveGroupID);
        DisplayValues(dt);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddSurvey.aspx");
        if (bool.Parse(ViewState["FromViewPage"].ToString()))
        {
            int curveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupFromID(curveGroupID);
            DisplayValues(dt);
        }
        else
        {
            ClearFields();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(ViewState["FromViewPage"]))
        {
            int curveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
            RigTrack.DatabaseObjects.RigTrackDO.DeleteCurveGroupAndCurves(curveGroupID);
        }

        Response.Redirect("~/ClientAdmin/ClientHome.aspx", true);
    }

    #endregion

    //protected void ToggleRowSelection(object sender, EventArgs e)
    //{
    //    ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;

    //    foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
    //    {
    //        if ((dataItem.FindControl("CheckBox1") as CheckBox) != (sender as CheckBox))
    //        {
    //            (dataItem.FindControl("CheckBox1") as CheckBox).Checked = false;
    //        }
    //    }
    //}

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        int curveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurvesForCurveGroup(curveGroupID);
        RadGrid1.DataSource = dt;
    }
    protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveDTO curveDTO = new RigTrack.DatabaseTransferObjects.CurveDTO();

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;


            int curveID, curveGroupID, number, curveTypeID;
            string curveName;
            double northOffset, eastOffset, VSDirection, RKBElevation;

            Int32.TryParse((item["ID"].Controls[0] as TextBox).Text, out curveID);
            Int32.TryParse((item["CurveGroupID"].Controls[0] as TextBox).Text, out curveGroupID);
            Int32.TryParse((item["Number"].Controls[0] as TextBox).Text, out number);
            //Int32.TryParse((item["CurveType"].Controls[0] as TextBox).Text, out curveTypeID);
            DropDownList ddl = item.FindControl("ddlCurveType") as DropDownList;
            Int32.TryParse(ddl.SelectedValue, out curveTypeID);

            curveName = (item["Name"].Controls[0] as TextBox).Text;

            double.TryParse((item["NorthOffset"].Controls[0] as TextBox).Text, out northOffset);
            double.TryParse((item["EastOffset"].Controls[0] as TextBox).Text, out eastOffset);
            double.TryParse((item["VSDirection"].Controls[0] as TextBox).Text, out VSDirection);
            double.TryParse((item["RKBElevation"].Controls[0] as TextBox).Text, out RKBElevation);

            curveDTO.ID = curveID;
            curveDTO.CurveGroupID = curveGroupID;
            curveDTO.Number = number;
            curveDTO.CurveTypeID = curveTypeID;
            curveDTO.Name = curveName;
            curveDTO.NorthOffset = northOffset;
            curveDTO.EastOffset = eastOffset;
            curveDTO.VSDirection = VSDirection;
            curveDTO.RKBElevation = RKBElevation;
            curveDTO.isActive = true;

            RigTrack.DatabaseObjects.RigTrackDO.UpdateCurve(curveDTO);


            CurveIDHidden.Value = "";
            CurveNumberHidden.Value = "";
        }
        
    }

    protected void RadGridTargets_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        //int curveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        //DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurvesForCurveGroup(curveGroupID);
        //RadGrid1.DataSource = dt;
    }


    protected void RadGridTargets_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        //RigTrack.DatabaseTransferObjects.CurveDTO curveDTO = new RigTrack.DatabaseTransferObjects.CurveDTO();

        //if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //{
        //    GridEditableItem item = e.Item as GridEditableItem;


        //    int curveID, curveGroupID, number, curveTypeID;
        //    string curveName;
        //    double northOffset, eastOffset, VSDirection, RKBElevation;

        //    Int32.TryParse((item["ID"].Controls[0] as TextBox).Text, out curveID);
        //    Int32.TryParse((item["CurveGroupID"].Controls[0] as TextBox).Text, out curveGroupID);
        //    Int32.TryParse((item["Number"].Controls[0] as TextBox).Text, out number);
        //    //Int32.TryParse((item["CurveType"].Controls[0] as TextBox).Text, out curveTypeID);
        //    DropDownList ddl = item.FindControl("ddlCurveType") as DropDownList;
        //    Int32.TryParse(ddl.SelectedValue, out curveTypeID);

        //    curveName = (item["Name"].Controls[0] as TextBox).Text;

        //    double.TryParse((item["NorthOffset"].Controls[0] as TextBox).Text, out northOffset);
        //    double.TryParse((item["EastOffset"].Controls[0] as TextBox).Text, out eastOffset);
        //    double.TryParse((item["VSDirection"].Controls[0] as TextBox).Text, out VSDirection);
        //    double.TryParse((item["RKBElevation"].Controls[0] as TextBox).Text, out RKBElevation);

        //    curveDTO.ID = curveID;
        //    curveDTO.CurveGroupID = curveGroupID;
        //    curveDTO.Number = number;
        //    curveDTO.CurveTypeID = curveTypeID;
        //    curveDTO.Name = curveName;
        //    curveDTO.NorthOffset = northOffset;
        //    curveDTO.EastOffset = eastOffset;
        //    curveDTO.VSDirection = VSDirection;
        //    curveDTO.RKBElevation = RKBElevation;
        //    curveDTO.isActive = true;

        //    RigTrack.DatabaseObjects.RigTrackDO.UpdateCurve(curveDTO);


        //    CurveIDHidden.Value = "";
        //    CurveNumberHidden.Value = "";
        //}

    }

    protected void RadGridTargets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //{
        //    GridEditableItem item = e.Item as GridEditableItem;
        //    DropDownList ddl = item.FindControl("ddlCurveType") as DropDownList;
        //    DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveTypes();
        //    DataView dv = dt.DefaultView;
        //    dv.Sort = "ID desc";
        //    DataTable sortedDT = dv.ToTable();
        //    ddl.DataSource = sortedDT;
        //    ddl.DataTextField = "Name";
        //    ddl.DataValueField = "ID";
        //    ddl.DataBind();
        //    ddl.SelectedValue = (item["CurveTypeID"].Controls[0] as TextBox).Text;


        //    //Send command the survey grid 
        //    CurveIDHidden.Value = (item["ID"].Controls[0] as TextBox).Text;
        //    CurveNumberHidden.Value = (item["Number"].Controls[0] as TextBox).Text;


        //}

        //if (e.Item is GridDataItem && !e.Item.IsInEditMode)
        //{
        //    GridDataItem item = (GridDataItem)e.Item;

        //    Label lbl = (Label)item.FindControl("lblCurveType");

        //    if (lbl.Text == "-Select-")
        //    {
        //        lbl.Text = "";
        //    }

        //}
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            DropDownList ddl = item.FindControl("ddlCurveType") as DropDownList;
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveTypes();
            DataView dv = dt.DefaultView;
            dv.Sort = "ID desc";
            DataTable sortedDT = dv.ToTable();
            ddl.DataSource = sortedDT;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.SelectedValue = (item["CurveTypeID"].Controls[0] as TextBox).Text;

            
            //Send command the survey grid 
            CurveIDHidden.Value = (item["ID"].Controls[0] as TextBox).Text;
            CurveNumberHidden.Value = (item["Number"].Controls[0] as TextBox).Text;
            
            
        }

        if (e.Item is GridDataItem && !e.Item.IsInEditMode)
        {
            GridDataItem item = (GridDataItem)e.Item;

            Label lbl = (Label)item.FindControl("lblCurveType");

            if (lbl.Text == "-Select-")
            {
                lbl.Text = "";
            }
            
        }
    }
    // nfv- Used to create a new row for datatable. 
    //Fake survey is just that. For testing only.
    struct FakeSurvey
    {
        public int      MD;
        public double   Inclination;
        public double   Azimuth;
        public double   TVD;
        public double   SubseasTVD;
        public double   NS;
        public double   EW;
        public double   VerticalSection;
        public double   CL;
        public double   ClosureDistance;
        public double   ClosureDirection;
        public double   DLS;
        public double   BR;
        public double   WR;
        public double   TFO;
    };
    protected void RadGridSurveys_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        //nfv- fake survey bind. 'Generic Tie in' 
        FakeSurvey newSurvey = new FakeSurvey();
        newSurvey.MD = 0;
        newSurvey.Inclination = 0.00;
        newSurvey.Azimuth = 0.00;
        newSurvey.TVD = 0.00;
        newSurvey.SubseasTVD = 0.00;
        newSurvey.NS = 0.00;
        newSurvey.EW =0.00;
        newSurvey.VerticalSection = 0.00;
        newSurvey.CL = 0.00;
        newSurvey.ClosureDistance = 0.00;
        newSurvey.ClosureDirection = 0.00;
        newSurvey.DLS = 0.00;
        newSurvey.BR = 0.00;
        newSurvey.WR = 0.00;
        
        

        DataTable newTable = new DataTable();
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
        newTable.Columns.Add("BR");
        newTable.Columns.Add("WR");
        newTable.Columns.Add("TFO");

        DataRow newRow;

        newRow = newTable.NewRow();
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
        newRow["BR"] = newSurvey.BR;
        newRow["WR"] = newSurvey.WR;
        newRow["TFO"] = newSurvey.TFO;

        newTable.Rows.Add(newRow);

        ViewState["SurveyTable"] = newTable;
        RadGridSurveys.DataSource = newTable;

        (RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display = false;
        

    }
    protected void RadGridSurveys_UpdateCommand(object sender, GridCommandEventArgs e)
    {

    }
    protected void RadGridSurveys_ItemDataBound(object sender, GridItemEventArgs e)
    {

    }
   
    protected void btnSubSeas_Click1(object sender, EventArgs e)
    {

        if ((RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display == true)
        {
            (RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display = false;
        
        }
        else
        {
            (RadGridSurveys.MasterTableView.GetColumn("SubseasTVD") as GridBoundColumn).Display = true;
        }

        //updPnl1.Update();
    }



        



    protected void BtnApply_Click(object sender, EventArgs e)
    {

    }
        

    protected void secretButton_Click(object sender, EventArgs e)
    {

        //RigTrack.DatabaseTransferObjects.SurveyDTO DTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
        


        //string md = MeasurementDepthHidden.Value;
        //string inc = InclinationHidden.Value;
        //string az = AzimuthHidden.Value;
        
        //int rows = RadGridSurveys.MasterTableView.Items.Count;
        //GridDataItem row = RadGridSurveys.MasterTableView.Items[rows-1];
        //string _md = row["MD"].Text;
        //string _inc = row["Inclination"].Text;
        //string _az = row["Azimuth"].Text;

        //SurveyBuilder newSurvey = new SurveyBuilder();
        //newSurvey.Depth = Int32.Parse(md);
        //newSurvey.Inclination = double.Parse(inc);
        //newSurvey.Azimuth = double.Parse(az);
        //SurveyBuilder oldSurvey = new SurveyBuilder();
        //oldSurvey.Depth = Int32.Parse(_md);
        //oldSurvey.Inclination = double.Parse(_inc);
        //oldSurvey.Azimuth = double.Parse(_az);
        //MinimumCurvationResults results = CurvatureCalculations.MinimumCurvation(oldSurvey, newSurvey);
        //newSurvey.Inclination = CurvatureConversions.ConvertToDegree(newSurvey.Inclination);
        //newSurvey.Azimuth = CurvatureConversions.ConvertToDegree(newSurvey.Azimuth);

        ////Save new Curve Info. 
        ////Get all Survey's for this curve
        ////Then recalculate 
        //DataTable dt = new System.Data.DataTable();
        //if (ViewState["SurveyTable"] != null)
        //{
        //    dt = (DataTable)ViewState["SurveyTable"];
        //}
        //else
        //{
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
        //    dt.Columns.Add("BR");
        //    dt.Columns.Add("WR");
        //    dt.Columns.Add("TFO");
        //}
        //double TVD_Total = 0.0;
        //double SubSeasTVD_Total = 0.0;
        //double NS_Total = 0.0;
        //double EW_Total = 0.0;
        //double VerticalSection_Total= 0.0;
        //double CL_Total = 0.0;//Not sure if add.
        //double ClosureDistance_Total = 0.0; //Not sure if add.
        //double ClosureDirection_Total = 0.0;//Not sure if Add
        //double DLS_Total = 0.0;//Not sure if add
        //double BR_Total = 0.0;//not sure if add
        //double WR_Total = 0.0;//not sure if add
        //double TFO_Total = 0.0;//not sure if add 

        //for (int i = 0; i < RadGridSurveys.MasterTableView.Items.Count; i++)
        //{
        //    //Find rows numbers.
        //    GridDataItem gdi = RadGridSurveys.MasterTableView.Items[i];
        //    TVD_Total += double.Parse(gdi["TVD"].Text);
        //    NS_Total += double.Parse(gdi["NS"].Text);
        //    EW_Total += double.Parse(gdi["EW"].Text);
        //    VerticalSection_Total += double.Parse(gdi["VerticalSection"].Text);
        //    ClosureDirection_Total += double.Parse(gdi["ClosureDirection"].Text);
        //    ClosureDistance_Total += double.Parse(gdi["ClosureDistance"].Text);
        //    //Store previous row.
        //    //Add pprevious row to new row to get totals.

        //}

        //DataRow newRow;
        //newRow = dt.NewRow();
        
        //newRow["MD"] = newSurvey.Depth;
        //newRow["Inclination"] = newSurvey.Inclination;
        //newRow["Azimuth"] = newSurvey.Azimuth;
        //newRow["TVD"] = results.TVD + TVD_Total;
        //newRow["SubseasTVD"] = results.TVD + TVD_Total;
        //newRow["NS"] = results.North + NS_Total;
        //newRow["EW"] = results.East + EW_Total;
        //newRow["VerticalSection"] = results.North + NS_Total;
        //newRow["CL"] = results.CL;
        //newRow["ClosureDistance"] = results.ClosureDistance + ClosureDistance_Total;
        //newRow["ClosureDirection"] = results.ClosureDirection + ClosureDirection_Total; 
        //newRow["DLS"] = results.dogLeg_Degrees;
        //newRow["BR"] = results.BR;
        //newRow["WR"] = results.WR;

        //dt.Rows.Add(newRow);
       
        //ViewState["SurveyTable"] = dt;
        //RadGridSurveys.DataSource = dt;
        //RadGridSurveys.Rebind();



        //DTO.ID = 0;
        //DTO.CurveID = Int32.Parse(CurveIDHidden.Value);
        ////DTO.CurveNumber = Int32.Parse(CurveNumberHidden.Value);
        //DTO.Name = "";
        //DTO.MD = newSurvey.Depth;
        //DTO.INC = newSurvey.Inclination;
        //DTO.Azimuth = newSurvey.Azimuth;
        //DTO.TVD = results.TVD;
        //DTO.TVDTotal = results.TVD + TVD_Total;
        //DTO.NS = results.North;
        //DTO.NSTotal = results.North + NS_Total;
        //DTO.EW = results.East + EW_Total;
        //DTO.VerticalSection = results.North;
        //DTO.VerticalSectionTotal = results.North + NS_Total;
        //DTO.CL = results.CL;
        //DTO.ClosureDistance = results.ClosureDistance;
        //DTO.ClosureDistanceTotal = results.ClosureDistance + ClosureDistance_Total;
        //DTO.ClosureDirection = results.ClosureDirection;
        //DTO.ClosureDirectionTotal = results.ClosureDirection + ClosureDirection_Total;
        //DTO.DLS = results.dogLeg_Degrees;
        //DTO.BR = results.BR;
        //DTO.WR = results.WR;
        //DTO.TFO = 0.0;
        //DTO.SurveyComment = "Get Comments from Add screen";
        //DTO.isActive = true;


        
        //RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(DTO);
        ////updPnl1.Update();

    }

    private void ClearFields()
    {
        TxtCurveGroupName.Text = "";
        txtJobNumber.Text = "";
        txtCompany.Text = "";
        txtLeaseWell.Text = "";
        txtLocation.Text = "";
        txtRigName.Text = "";
        ddlCountry.SelectedValue = "0";
        ddlState.SelectedValue = "0";
        txtDeclination.Text = "";
        txtGrid.Text = "";
        txtRKB.Text = "";
        ddlGLorMSL.SelectedValue = "0";

        btnMinimumCurvature.Checked = false;
        btnRadiusOfCurvature.Checked = false;
        btnAvergaeAngle.Checked = false;
        btnTangentail.Checked = false;
        btnBalancedTangentail.Checked = false;

        btnOutputDecimal.Checked = false;
        btnOutputQuadrant.Checked = false;

        btnInputDecimal.Checked = false;
        btnInputQuadrant.Checked = false;

        btnWellhead.Checked = false;
        btnOtherReference.Checked = false;

        btnNo.Checked = false;
        btnYes.Checked = false;
        btnMeters.Checked = false;
        btnFeet.Checked = false;
        btnPer10meters.Checked = false;
        btnPer30Meters.Checked = false;

        txtEWOffset.Text = "";
        txtNSOffset.Text = "";

        txtWorkNumber.Text = "";
        txtPlanNumber.Text = "";
        txtMD.Text = "";
        txtIncl.Text = "";
        txtAzimuth.Text = "";
        txtTVD.Text = "";
        txtNSCoord.Text = "";
        txtEWCoord.Text = "";
        txtvsection.Text = "";
        txtWRate.Text = "";
        txtBRate.Text = "";

        txtDLS.Text = "";
        txtTFO.Text = "";
        txtClosure.Text = "";
        txtLocationCurveGroup.Text = "";
        txtBitToSensor.Text = "";
        txtLeastDistance.Text = "";
        txtHSide.Text = "";
        txtTVDComp.Text = "";
        txtComparisonCurve.Text = "";
        txtAT.Text = "";
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCountry.SelectedValue == "1220")
        {
            ddlState.Enabled = true;
        }
        else
        {
            ddlState.Enabled = false;
            ddlState.SelectedValue = "0";
        }
    }
    protected void btnYes_CheckedChanged(object sender, EventArgs e)
    {
        if (btnYes.Checked == true)
        {
            btnMeters.Enabled = true;
            btnFeet.Enabled = true;

            if (btnMeters.Checked == true)
            {
                btnPer30Meters.Enabled = true;
                btnPer10meters.Enabled = true;
            }
        }

        else
        {

            btnMeters.Enabled = false;
            btnFeet.Enabled = false;



            btnPer30Meters.Enabled = false;
            btnPer10meters.Enabled = false;
        }
       
      
     
        
    }

    protected void btnMeters_CheckedChanged(object sender, EventArgs e)
    {
        if (btnMeters.Checked == true)
        {
            btnPer30Meters.Enabled = true;
            btnPer10meters.Enabled = true;
        }
        else
        {
            btnPer30Meters.Enabled = false;
            btnPer10meters.Enabled = false;

            btnPer30Meters.Checked = false;
            btnPer10meters.Checked = false;

        
        }

    }
}