using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;
using System.Web.Services;
using System.Web.UI;
using System.Collections.Generic;


public partial class Modules_RigTrack_CreateJobs : System.Web.UI.Page
{
    #region Page Behavior
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["CurveGroupID"] != null)
        {
            LoadGridByID();
            if (RadGrid1.MasterTableView.Items.Count <= 0)
            {
                btnClear_Click(null, null);
            }
        }


        if (!IsPostBack)
        {
            datepicker_JobStartDate.SelectedDate = DateTime.Now;

            BindDropDownLists();

            ddlMethodOfCalculation.SelectedText = "Minimum Curvature";
            ddlMetersFeet.SelectedText = "Feet";
            ddlOutPutDirection.SelectedText = "Decimal (350.00)";
            ddlInputDirection.SelectedText = "Decimal (350.00)";
            ddlVerticalSectionReference.SelectedText = "Other Reference";
            txtNSOffset.Enabled = true;
            txtEWOffset.Enabled = true;
           
            

        }
       
    }
    #endregion
    #region Utility Methods
   
    protected void LoadGridByID()
    {

        if (ViewState["CurveGroupID"] != null)
        {
            int newCurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
            DataTable dt = new DataTable();
            dt = RigTrack.DatabaseObjects.RigTrackDO.GetManageJobsCurveGroupsFromID(newCurveGroupID);
          
            RadGrid1.DataSource = dt;
            RadGrid1.Rebind();
            //btnAddWellPlan.Enabled = true;
        }
        else
        {
            DataTable dt = new DataTable();
            RadGrid1.DataSource = dt;
            RadGrid1.Rebind();
            
        }
    }
    private void BindDropDownLists()
    {

        ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
        ddlCompany.DataTextField = "Name";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();

        ddlCountry.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCountries();
        ddlCountry.DataTextField = "Name";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.SelectedValue = "1220";
        ddlCountry_SelectedIndexChanged(null, null);

        ddlState.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllStates();
        ddlState.DataTextField = "Name";
        ddlState.DataValueField = "ID";
        ddlState.DataBind();

        ddlGLorMSL.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllGLMSLs();
        ddlGLorMSL.DataTextField = "Name";
        ddlGLorMSL.DataValueField = "ID";
        ddlGLorMSL.DataBind();

        ddlMethodOfCalculation.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllMethodsOfCalculation();
        ddlMethodOfCalculation.DataTextField = "Name";
        ddlMethodOfCalculation.DataValueField = "ID";
        ddlMethodOfCalculation.DataBind();

        ddlMetersFeet.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllMeasurementUnits();
        ddlMetersFeet.DataTextField = "Name";
        ddlMetersFeet.DataValueField = "ID";
        ddlMetersFeet.DataBind();

        ddlDogLegSeverity.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllDogLeg();
        ddlDogLegSeverity.DataTextField = "Name";
        ddlDogLegSeverity.DataValueField = "ID";
        ddlDogLegSeverity.DataBind();

        ddlOutPutDirection.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllInputOutputDirections();
        ddlOutPutDirection.DataTextField = "Name";
        ddlOutPutDirection.DataValueField = "ID";
        ddlOutPutDirection.DataBind();

        ddlInputDirection.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllInputOutputDirections();
        ddlInputDirection.DataTextField = "Name";
        ddlInputDirection.DataValueField = "ID";
        ddlInputDirection.DataBind();

        ddlVerticalSectionReference.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllVerticalSectionRefs();
        ddlVerticalSectionReference.DataTextField = "Name";
        ddlVerticalSectionReference.DataValueField = "ID";
        ddlVerticalSectionReference.DataBind();

    }
    public RigTrack.DatabaseTransferObjects.CurveGroupDTO AssignUIToClass()
    {

        double NSOffsetInit = 0.0;
        double declinationInit = 0.0;
        double EWOffsetInit = 0.0;
        //int countryID = 0;
        //int stateID = 0;

        RigTrack.DatabaseTransferObjects.CurveGroupDTO ManageJobsCurveGroups = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();


        DateTime JStartDate;

        if (datepicker_JobStartDate.DbSelectedDate != null)
        {
            if (DateTime.TryParse(datepicker_JobStartDate.DbSelectedDate.ToString(), out JStartDate))
            {
                ManageJobsCurveGroups.JobStartDate = JStartDate;
            }
        }

        ManageJobsCurveGroups.CurveGroupName = TxtCurveGroupName.Text;
        ManageJobsCurveGroups.Company = Int32.Parse(ddlCompany.SelectedValue);
        ManageJobsCurveGroups.LeaseWell = txtLeaseWell.Text;
        ManageJobsCurveGroups.JobLocation = txtLocation.Text;
        ManageJobsCurveGroups.RigName = txtRigName.Text;
        ManageJobsCurveGroups.LatitudeLongitude = txtLatitude.Text + ',' + txtLongitude.Text;

        if (double.TryParse(txtNSOffset.Text, out NSOffsetInit))
        {
            ManageJobsCurveGroups.NSOffset = NSOffsetInit;
        }
        else
        {
            ManageJobsCurveGroups.NSOffset = null;
        }



        ManageJobsCurveGroups.JobNumber = txtJobNumber.Text;
        ManageJobsCurveGroups.Grid = txtGrid.Text;
        ManageJobsCurveGroups.RKB = txtRKB.Text;

        if (ddlCountry.SelectedValue == "0")
        {
            ManageJobsCurveGroups.CountryID = null;

        }

        else
        {
            ManageJobsCurveGroups.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        }


        // State 
        if (ddlState.SelectedValue == "0")
        {
            ManageJobsCurveGroups.StateID = null;

        }

        else
        {
            ManageJobsCurveGroups.StateID = Convert.ToInt32(ddlState.SelectedValue);
        }

        // Method Of Calculation
        if (ddlMethodOfCalculation.SelectedValue == "0")
        {
            ManageJobsCurveGroups.MethodOfCalculationID = null;

        }

        else
        {
            ManageJobsCurveGroups.MethodOfCalculationID = Convert.ToInt32(ddlMethodOfCalculation.SelectedValue);
        }

      

        if (ddlMetersFeet.SelectedValue == "0")
        {
            ManageJobsCurveGroups.MeasurementUnitsID = null;
        }
        else
        {
            ManageJobsCurveGroups.MeasurementUnitsID = Convert.ToInt32(ddlMetersFeet.SelectedValue);
        }

        if (ddlDogLegSeverity.SelectedValue == "0")
        {
            ManageJobsCurveGroups.DogLegSeverityID = null;
        }

        else
        {
            ManageJobsCurveGroups.DogLegSeverityID = Convert.ToInt32(ddlDogLegSeverity.SelectedValue);
        }

        // gl or msl  

        if (ddlGLorMSL.SelectedValue == "0")
        {
            ManageJobsCurveGroups.GLorMSLID = null;
        }

        else
        {
            ManageJobsCurveGroups.GLorMSLID = Convert.ToInt32(ddlGLorMSL.SelectedValue);
        }


        if (double.TryParse(txtDeclination.Text, out declinationInit))
        {
            ManageJobsCurveGroups.Declination = declinationInit;
        }
        else
        {
            ManageJobsCurveGroups.Declination = null;
        }

        if (ddlOutPutDirection.SelectedValue == "0")
        {
            ManageJobsCurveGroups.OutputDirectionID = null;
        }

        else
        {
            ManageJobsCurveGroups.OutputDirectionID = Convert.ToInt32(ddlOutPutDirection.SelectedValue);
        }

        if (ddlInputDirection.SelectedValue == "0")
        {
            ManageJobsCurveGroups.InputDirectionID = null;
        }

        else
        {
            ManageJobsCurveGroups.InputDirectionID = Convert.ToInt32(ddlInputDirection.SelectedValue);
        }

        if (ddlVerticalSectionReference.SelectedValue == "0")
        {
            ManageJobsCurveGroups.VerticalSectionReferenceID = null;
        }

        else
        {
            ManageJobsCurveGroups.VerticalSectionReferenceID = Convert.ToInt32(ddlVerticalSectionReference.SelectedValue);
        }

        if (double.TryParse(txtEWOffset.Text, out EWOffsetInit))
        {
            ManageJobsCurveGroups.EWOffset = EWOffsetInit;
        }
        else
        {
            ManageJobsCurveGroups.EWOffset = null;
        }

        ManageJobsCurveGroups.isActive = true;




        return ManageJobsCurveGroups;
    }

    #endregion
    #region Markup Elements
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        GridTable1.Visible = true;// set gridstable visible to true when save button click 


        RigTrack.DatabaseTransferObjects.CurveGroupDTO DTOReturn = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
        DTOReturn = AssignUIToClass();
        ViewState["CurveGroupID"] = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateJobsCurveGroups(DTOReturn);
        int newCurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
        hiddenField.Value = newCurveGroupID.ToString();
        DataTable dt = new DataTable();
        dt = RigTrack.DatabaseObjects.RigTrackDO.GetManageJobsCurveGroupsFromID(newCurveGroupID);
       
        RadGrid1.DataSource = dt;
        RadGrid1.Rebind();
        //btnAddWellPlan.Enabled = true;

        
        //RadScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "OpenWellPlan();", true);
        WellPlanWindow.VisibleOnPageLoad = true;
        WellPlanWindow.NavigateUrl = "AddWellPlan.aspx?CurveGroupID=" + newCurveGroupID.ToString();
        WellPlanWindow.Modal = true;
        WellPlanWindow.CenterIfModal = true;
        WellPlanWindow.Height = Unit.Pixel(700);
        WellPlanWindow.Width = Unit.Pixel(800);

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "OpenWellPlan();", true);
        //LoadGridGet();
    }
   
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //if (ViewState["CurveGroupID"] != null)
        //{
        //    //Check if record has well plan
        //    bool hasWellPlan = false;
        //    if (!hasWellPlan)
        //    {
        //        //lblWellplanNeeded.Visible = true;
        //        return;
        //    }

        //}

        ViewState["CurveGroupID"] = null;
        TxtCurveGroupName.Text = "";
        ddlCompany.SelectedValue = "0";
        txtLeaseWell.Text = "";
        txtLocation.Text = "";
        txtRigName.Text = "";
        txtNSOffset.Text = "";
        txtJobNumber.Text = "";
        txtGrid.Text = "";
        txtRKB.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";


        ddlCountry.SelectedText = "United States";
        ddlState.SelectedValue = "0";
      
       
       
        ddlGLorMSL.SelectedValue = "0";
        txtDeclination.Text = "";



        //btnAddWellPlan.Enabled = false;

        

        ddlDogLegSeverity.Enabled = false;

       

      


        RadGrid1.MasterTableView.ClearEditItems();

        GridTable1.Visible = false;

        // Cleat The values Then reload The get Store Procudure 
        LoadGridByID();
        
    }
   
    protected void ddlMetersFeet_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlMetersFeet.SelectedValue == "1001")
        {
            ddlDogLegSeverity.Enabled = true;
        }
        else
        {
            ddlDogLegSeverity.Enabled = false;
        }
    }
    //protected void BtnSearch_Click(object sender, EventArgs e)
    //{

    //    // Get All Manage Jobs And Curve Groups Load
    //    RigTrack.DatabaseTransferObjects.CurveGroupDTO DTOReturn = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
    //    DTOReturn = AssignUIToClass();
    //    DataTable dt = new DataTable();
    //    dt = RigTrack.DatabaseObjects.RigTrackDO.SearchJobsCurveGroups(DTOReturn);


    //    dt.Columns.Add("Convert", typeof(string));

    //    foreach (DataRow row in dt.Rows)
    //    {

    //        if (row["UnitsConvert"].ToString() == "True")
    //        {
    //            row["Convert"] = "Yes";
    //        }
    //        else if (row["UnitsConvert"].ToString() == "False")
    //        {
    //            row["Convert"] = "No";
    //        }

    //        else
    //        {
    //            row["Convert"] = "";
    //        }

    //    }


    //    RadGrid1.DataSource = dt;
    //    RadGrid1.DataBind();

    //}
    #endregion
    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        //DataTable dt = new DataTable();
        //dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllMangeJobsCurveGroups();


        //dt.Columns.Add("Convert", typeof(string));

        //foreach (DataRow row in dt.Rows)
        //{

        //    if (row["UnitsConvert"].ToString() == "True")
        //    {
        //        row["Convert"] = "Yes";
        //    }
        //    else if (row["UnitsConvert"].ToString() == "False")
        //    {
        //        row["Convert"] = "No";
        //    }

        //    else
        //    {
        //        row["Convert"] = "";
        //    }

        //}
        //RadGrid1.DataSource = dt;

        if (ViewState["CurveGroupID"] != null)
        {
            int newCurveGroupID = Int32.Parse(ViewState["CurveGroupID"].ToString());
            DataTable dt = new DataTable();
            dt = RigTrack.DatabaseObjects.RigTrackDO.GetManageJobsCurveGroupsFromID(newCurveGroupID);
            
            RadGrid1.DataSource = dt;
            //btnAddWellPlan.Enabled = true;
        }
        else
        {
            DataTable dt = new DataTable();
            RadGrid1.DataSource = dt;
        }
    }
    protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            int CurveGroupID, stateID, countryID, GLorMSLID, methodOfCalculationID, measurementUnitsID, outputDirectionID, inputDirectionID, dogLegSeverityID, verticalSectionRef,company;
            string curveGroupName, jobNumber,  leaseWell, jobLocation, rigName, grid, rkb;
            double declination, ewOffset, nsOffset;
            bool unitsConvert;

            DateTime jobStart;
            DateTime jobEnd;

            //Int32.TryParse((item["ID"].Controls[0] as TextBox).Text, out ID);

            RadTextBox CurveGroupID2 = item.FindControl("CurveGroupID2") as RadTextBox;
            Int32.TryParse(CurveGroupID2.Text, out CurveGroupID);




            curveGroupName = (item["CurveGroupName"].Controls[0] as TextBox).Text;


            RadDatePicker JobStartDate2 = item.FindControl("datepicker_JobStartDateEdit") as RadDatePicker;
            DateTime.TryParse(JobStartDate2.DbSelectedDate.ToString(), out jobStart);


            //company = (item["Company"].Controls[0] as TextBox).Text;
            
            leaseWell = (item["LeaseWell"].Controls[0] as TextBox).Text;
            jobLocation = (item["JobLocation"].Controls[0] as TextBox).Text;
            rigName = (item["RigName"].Controls[0] as TextBox).Text;
            double.TryParse((item["NSOffset"].Controls[0] as TextBox).Text, out nsOffset);
            jobNumber = (item["JobNumber"].Controls[0] as TextBox).Text;
            grid = (item["Grid"].Controls[0] as TextBox).Text;
            rkb = (item["RKB"].Controls[0] as TextBox).Text;
            RadDropDownList ddlCountry = item.FindControl("ddlCountry") as RadDropDownList;
            Int32.TryParse(ddlCountry.SelectedValue, out countryID);
            RadDropDownList ddlState = item.FindControl("ddlState") as RadDropDownList;
            Int32.TryParse(ddlState.SelectedValue, out stateID);
            RadDropDownList ddlMethodOfCalculation = item.FindControl("ddlMethodOfCalculation") as RadDropDownList;
            Int32.TryParse(ddlMethodOfCalculation.SelectedValue, out methodOfCalculationID);

            RadDropDownList ddlCompany = item.FindControl("ddlCompanyGrid") as RadDropDownList;
            Int32.TryParse(ddlCompany.SelectedValue, out company);


            RadDropDownList ddlMetersFeet = item.FindControl("ddlMetersFeet") as RadDropDownList;
            Int32.TryParse(ddlMetersFeet.SelectedValue, out measurementUnitsID);
            RadDropDownList ddlDogLeg = item.FindControl("ddlDogLeg") as RadDropDownList;
            Int32.TryParse(ddlDogLeg.SelectedValue, out dogLegSeverityID);
            RadDropDownList ddlGLorMSL = item.FindControl("ddlGLorMSL") as RadDropDownList;
            Int32.TryParse(ddlGLorMSL.SelectedValue, out GLorMSLID);
            double.TryParse((item["Declination"].Controls[0] as TextBox).Text, out declination);
            RadDropDownList ddlOutputName = item.FindControl("ddlOutputName") as RadDropDownList;
            Int32.TryParse(ddlOutputName.SelectedValue, out outputDirectionID);
            RadDropDownList ddlInputName = item.FindControl("ddlInputName") as RadDropDownList;
            Int32.TryParse(ddlInputName.SelectedValue, out inputDirectionID);
            RadDropDownList ddlVerticalSection = item.FindControl("ddlVerticalSection") as RadDropDownList;
            Int32.TryParse(ddlVerticalSection.SelectedValue, out verticalSectionRef);
            double.TryParse((item["EWOffset"].Controls[0] as TextBox).Text, out ewOffset);

            curveGroupDTO.ID = CurveGroupID;
            curveGroupDTO.StateID = stateID;
            curveGroupDTO.CountryID = countryID;
            curveGroupDTO.GLorMSLID = GLorMSLID;
            curveGroupDTO.MethodOfCalculationID = methodOfCalculationID;
            curveGroupDTO.MeasurementUnitsID = measurementUnitsID;
            curveGroupDTO.OutputDirectionID = outputDirectionID;
            curveGroupDTO.InputDirectionID = inputDirectionID;
            curveGroupDTO.DogLegSeverityID = dogLegSeverityID;
            curveGroupDTO.VerticalSectionReferenceID = verticalSectionRef;
            curveGroupDTO.CurveGroupName = curveGroupName;
            curveGroupDTO.JobNumber = jobNumber;
            curveGroupDTO.Company = company;
            curveGroupDTO.LeaseWell = leaseWell;
            curveGroupDTO.JobLocation = jobLocation;
            curveGroupDTO.RigName = rigName;
            curveGroupDTO.Grid = grid;
            curveGroupDTO.RKB = rkb;
            curveGroupDTO.Declination = declination;
            curveGroupDTO.EWOffset = ewOffset;
            curveGroupDTO.NSOffset = nsOffset;
          
            curveGroupDTO.isActive = true;
            curveGroupDTO.JobStartDate = jobStart;
            

            int newCurveGroupID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateJobsCurveGroups(curveGroupDTO);
            DataTable dt = new DataTable();
            dt = RigTrack.DatabaseObjects.RigTrackDO.GetManageJobsCurveGroupsFromID(newCurveGroupID);

          
            RadGrid1.DataSource = dt;
        }
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {


            GridEditableItem item = e.Item as GridEditableItem;

            TextBox txtCurveGroupName = (TextBox)item["CurveGroupName"].Controls[0];
            txtCurveGroupName.Width = 160;


            RadTextBox CurveGroupID2 = item.FindControl("CurveGroupID2") as RadTextBox;
            CurveGroupID2.Text = (item["ID"].Controls[0] as TextBox).Text;

            RadDatePicker DatePickerStartDate = item.FindControl("datepicker_JobStartDateEdit") as RadDatePicker;
            DatePickerStartDate.DbSelectedDate = (item["JobStartDate"].Controls[0] as TextBox).Text;


            RadDropDownList ddlCountry = item.FindControl("ddlCountry") as RadDropDownList;
            DataTable dtCountry = RigTrack.DatabaseObjects.RigTrackDO.GetAllCountries();
            ddlCountry.DataSource = dtCountry;
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataBind();
            ddlCountry.SelectedText = (item["CountryName"].Controls[0] as TextBox).Text;

            RadDropDownList ddlState = item.FindControl("ddlState") as RadDropDownList;
            DataTable dtState = RigTrack.DatabaseObjects.RigTrackDO.GetAllStates();
            ddlState.DataSource = dtState;
            ddlState.DataTextField = "Name";
            ddlState.DataValueField = "ID";
            ddlState.DataBind();
            ddlState.SelectedText = (item["StateName"].Controls[0] as TextBox).Text;

            RadDropDownList ddlMethodOfCalculation = item.FindControl("ddlMethodOfCalculation") as RadDropDownList;
            DataTable dtMoC = RigTrack.DatabaseObjects.RigTrackDO.GetAllMethodsOfCalculation();
            ddlMethodOfCalculation.DataSource = dtMoC;
            ddlMethodOfCalculation.DataTextField = "Name";
            ddlMethodOfCalculation.DataValueField = "ID";
            ddlMethodOfCalculation.DataBind();
            ddlMethodOfCalculation.SelectedText = (item["MethodName"].Controls[0] as TextBox).Text;

        

            RadDropDownList ddlMetersFeet = item.FindControl("ddlMetersFeet") as RadDropDownList;
            ddlMetersFeet.SelectedText = (item["MetersFeet"].Controls[0] as TextBox).Text;

            RadDropDownList ddlDogLeg = item.FindControl("ddlDogLeg") as RadDropDownList;
            DataTable dtDogLeg = RigTrack.DatabaseObjects.RigTrackDO.GetAllDogLeg();
            ddlDogLeg.DataSource = dtDogLeg;
            ddlDogLeg.DataTextField = "Name";
            ddlDogLeg.DataValueField = "ID";
            ddlDogLeg.DataBind();
            ddlDogLeg.SelectedText = (item["DogLegName"].Controls[0] as TextBox).Text;

            TextBox txtDeclination = (TextBox)item["Declination"].Controls[0];
            txtDeclination.Width = 70;

            RadDropDownList ddlGLorMSL = item.FindControl("ddlGLorMSL") as RadDropDownList;
            DataTable dtGLorMSL = RigTrack.DatabaseObjects.RigTrackDO.GetAllGLMSLs();
            ddlGLorMSL.DataSource = dtGLorMSL;
            ddlGLorMSL.DataTextField = "Name";
            ddlGLorMSL.DataValueField = "ID";
            ddlGLorMSL.DataBind();
            ddlGLorMSL.SelectedText = (item["GLName"].Controls[0] as TextBox).Text;

            RadDropDownList ddlOutputName = item.FindControl("ddlOutputName") as RadDropDownList;
            DataTable dtInputOutput = RigTrack.DatabaseObjects.RigTrackDO.GetAllInputOutputDirections();
            ddlOutputName.DataSource = dtInputOutput;
            ddlOutputName.DataTextField = "Name";
            ddlOutputName.DataValueField = "ID";
            ddlOutputName.DataBind();
            ddlOutputName.SelectedText = (item["OutPutName"].Controls[0] as TextBox).Text;

            RadDropDownList ddlInputName = item.FindControl("ddlInputName") as RadDropDownList;
            ddlInputName.DataSource = dtInputOutput;
            ddlInputName.DataTextField = "Name";
            ddlInputName.DataValueField = "ID";
            ddlInputName.DataBind();
            ddlInputName.SelectedText = (item["InputName"].Controls[0] as TextBox).Text;

            RadDropDownList ddlVerticalSection = item.FindControl("ddlVerticalSection") as RadDropDownList;
            DataTable dtVerticalSection = RigTrack.DatabaseObjects.RigTrackDO.GetAllVerticalSectionRefs();
            ddlVerticalSection.DataSource = dtVerticalSection;
            ddlVerticalSection.DataTextField = "Name";
            ddlVerticalSection.DataValueField = "ID";
            ddlVerticalSection.DataBind();
            ddlVerticalSection.SelectedText = (item["VSectionName"].Controls[0] as TextBox).Text;


            TextBox txtNSOffset = (TextBox)item["NSOffset"].Controls[0];
            txtNSOffset.Width = 70;

            TextBox txtEWOffset = (TextBox)item["EWOffset"].Controls[0];
            txtEWOffset.Width = 70;
        }
    }
    protected void ddlVerticalSectionReference_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlVerticalSectionReference.SelectedValue == "1001")
        {
            txtNSOffset.Enabled = true;
            txtEWOffset.Enabled = true;

            //txtNSOffset.BackColor =  System.Drawing.Color.White;
            //txtEWOffset.BackColor = System.Drawing.Color.White;
        }

        else
        {
            txtNSOffset.Enabled = false;
            txtEWOffset.Enabled= false;

            //txtNSOffset.BackColor = System.Drawing.Color.LightGray;
            //txtEWOffset.BackColor = System.Drawing.Color.LightGray;

            
        }
    }

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.CompanyDTO> RebindCompany()
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
        List<RigTrack.DatabaseTransferObjects.CompanyDTO> companyList = new List<RigTrack.DatabaseTransferObjects.CompanyDTO>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            RigTrack.DatabaseTransferObjects.CompanyDTO DTO = new RigTrack.DatabaseTransferObjects.CompanyDTO();
            DTO.CompanyName = dt.Rows[i]["Name"].ToString();
            DTO.ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
            companyList.Add(DTO);
        }
        return companyList;
    }

    [WebMethod]
    public static string DeleteCurveGroupInfo(string CurveGroupID)
    {
        string m = string.Empty;
        int curveGroupID_int;
        curveGroupID_int = Int32.Parse(CurveGroupID);
        RigTrack.DatabaseObjects.RigTrackDO.DeleteCurveGroupAndCurves(curveGroupID_int);
        return m;

    }
}