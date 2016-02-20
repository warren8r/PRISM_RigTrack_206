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

public partial class Modules_RigTrack_ViewEditJobs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MethodOfCalMessage.Visible = false;
        btnView.Enabled = false;

        if (!IsPostBack)
        {
            RadGrid1.PageSize = 20; 
            datepicker_JobStartDate.DbSelectedDate = DateTime.Now.AddYears(-1);
            datepicker_JobStartEnd.DbSelectedDate = DateTime.Now;
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
        }

    }


   

     protected void LoadGridByID()
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO DTOReturn = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
        DTOReturn = AssignUIToClass();
        DataTable dt = new DataTable();
        dt = RigTrack.DatabaseObjects.RigTrackDO.SearchJobsCurveGroups(DTOReturn);

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }

    
    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveGroupDTO DTOReturn = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
        DTOReturn = AssignUIToClass();
        DataTable dt = new DataTable();
        dt = RigTrack.DatabaseObjects.RigTrackDO.SearchJobsCurveGroups(DTOReturn);

        RadGrid1.DataSource = dt;
        

        
    }

    protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {

    }

    public RigTrack.DatabaseTransferObjects.CurveGroupDTO AssignUIToClass()
    {

        double NSOffsetInit = 0.0;
        double declinationInit = 0.0;
        double EWOffsetInit = 0.0;
      

        RigTrack.DatabaseTransferObjects.CurveGroupDTO ManageJobsCurveGroups = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();


        DateTime JStartDate = DateTime.Now;
        DateTime JStartEnd = DateTime.Now;
      

        if (datepicker_JobStartDate.DbSelectedDate != null)
        {
            if (DateTime.TryParse(datepicker_JobStartDate.DbSelectedDate.ToString(), out JStartDate))
            {
                ManageJobsCurveGroups.JobStartDate = JStartDate;
            }
        }

        else
        {
           
            ManageJobsCurveGroups.JobStartDate = null;
        }


        if (datepicker_JobStartEnd.DbSelectedDate != null)
        {
            if (DateTime.TryParse(datepicker_JobStartEnd.DbSelectedDate.ToString(), out JStartEnd))
            {
                ManageJobsCurveGroups.JobStartDateEnd = JStartEnd;
            }
        }

        else
        {

            ManageJobsCurveGroups.JobStartDateEnd = null;
        }


        ManageJobsCurveGroups.CurveGroupName = TxtCurveGroupName.Text;
        ManageJobsCurveGroups.Company = Int32.Parse(ddlCompany.SelectedValue);
        ManageJobsCurveGroups.LeaseWell = txtLeaseWell.Text;
        ManageJobsCurveGroups.JobLocation = txtLocation.Text;
        ManageJobsCurveGroups.RigName = txtRigName.Text;

    

        ManageJobsCurveGroups.JobNumber = txtJobNumber.Text;
        


        return ManageJobsCurveGroups;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        // Get All Manage Jobs And Curve Groups Load
        RigTrack.DatabaseTransferObjects.CurveGroupDTO DTOReturn = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
        DTOReturn = AssignUIToClass();
        DataTable dt = new DataTable();
        dt = RigTrack.DatabaseObjects.RigTrackDO.SearchJobsCurveGroups(DTOReturn);

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        TxtCurveGroupName.Text = "";
        ddlCompany.SelectedValue = "0";
        txtLeaseWell.Text = "";
        txtLocation.Text = "";
        txtRigName.Text = "";
       
        txtJobNumber.Text = "";
        MethodOfCalMessage.Visible = false;
       

        datepicker_JobStartDate.DbSelectedDate = DateTime.Now.AddYears(-1);
        datepicker_JobStartEnd.DbSelectedDate = DateTime.Now;
       

       // RadGrid1.MasterTableView.IsItemInserted = true;

        RadGrid1.MasterTableView.ClearEditItems();
       

        // Cleat The values Then reload The get Store Procudure 
        LoadGridByID();

        
    }
   

    protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        MethodOfCalMessage.Visible = false;
        RigTrack.DatabaseTransferObjects.CurveGroupDTO curveGroupDTO = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            int company, CurveGroupID, stateID, countryID, GLorMSLID, methodOfCalculationID, measurementUnitsID, outputDirectionID, inputDirectionID, dogLegSeverityID, verticalSectionRef;
            string curveGroupName, jobNumber,  leaseWell, jobLocation, rigName, grid, rkb, LatLong;
            double declination, ewOffset, nsOffset;
            

            DateTime jobStart;
            DateTime jobEnd;

          


            //Int32.TryParse((item["ID"].Controls[0] as TextBox).Text, out ID);



            RadTextBox CurveGroupID2 = item.FindControl("CurveGroupID2") as RadTextBox;
            Int32.TryParse(CurveGroupID2.Text, out CurveGroupID);


            curveGroupName = (item["CurveGroupName"].Controls[0] as TextBox).Text;

             


            RadDatePicker JobStartDate2 = item.FindControl("datepicker_JobStartDateEdit") as RadDatePicker;
            DateTime.TryParse(JobStartDate2.DbSelectedDate.ToString(), out jobStart);


            LatLong = (item["primaryLatLong"].Controls[0] as TextBox).Text;
            leaseWell = (item["LeaseWell"].Controls[0] as TextBox).Text;
            jobLocation = (item["JobLocation"].Controls[0] as TextBox).Text;
            rigName = (item["RigName"].Controls[0] as TextBox).Text;
            //double.TryParse((item["NSOffset"].Controls[0] as TextBox).Text, out nsOffset);
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
            //double.TryParse((item["Declination"].Controls[0] as TextBox).Text, out declination);
            RadDropDownList ddlOutputName = item.FindControl("ddlOutputName") as RadDropDownList;
            Int32.TryParse(ddlOutputName.SelectedValue, out outputDirectionID);
            RadDropDownList ddlInputName = item.FindControl("ddlInputName") as RadDropDownList;
            Int32.TryParse(ddlInputName.SelectedValue, out inputDirectionID);
            RadDropDownList ddlVerticalSection = item.FindControl("ddlVerticalSection") as RadDropDownList;
            Int32.TryParse(ddlVerticalSection.SelectedValue, out verticalSectionRef);
            //double.TryParse((item["EWOffset"].Controls[0] as TextBox).Text, out ewOffset);

            if (double.TryParse((item["NSOffset"].Controls[0] as TextBox).Text, out nsOffset) && double.TryParse((item["Declination"].Controls[0] as TextBox).Text, out declination)
                && double.TryParse((item["EWOffset"].Controls[0] as TextBox).Text, out ewOffset))
            {
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
                curveGroupDTO.LatitudeLongitude = LatLong;

                curveGroupDTO.isActive = true;
                curveGroupDTO.JobStartDate = jobStart;

                int newCurveGroupID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateJobsCurveGroups(curveGroupDTO);

                RigTrack.DatabaseTransferObjects.CurveGroupDTO DTOReturn = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
                DTOReturn = AssignUIToClass();
                DataTable dt = new DataTable();
                dt = RigTrack.DatabaseObjects.RigTrackDO.SearchJobsCurveGroups(DTOReturn);

                RadGrid1.DataSource = dt;
                
            }
            else
            {
                string script = "ErrorDialog(\" Please enter a valid number \");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoRecords", script, true);
            }
        }
        
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
        {
            string itemDate = item.GetDataKeyValue("JobEndDate").ToString();
            itemDate = HttpUtility.HtmlDecode(itemDate);
            if (string.IsNullOrEmpty(itemDate) || string.IsNullOrWhiteSpace(itemDate))
            {
                
            }
            else
            {
                item["EditButton"].Enabled = false;
                item["EditButton"].Text = "Closed";
                
            }
        }

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            TextBox txtCurveGroupName = (TextBox)item["CurveGroupName"].Controls[0];
            txtCurveGroupName.Width = 160;

            RadTextBox CurveGroupID2 = item.FindControl("CurveGroupID2") as RadTextBox;
            CurveGroupID2.Text = (item["ID"].Controls[0] as TextBox).Text;

            RadDatePicker DatePickerStartDate = item.FindControl("datepicker_JobStartDateEdit") as RadDatePicker;
            DatePickerStartDate.DbSelectedDate = (item["JobStartDate"].Controls[0] as TextBox).Text;


            RadDatePicker DatePickerEndDate = item.FindControl("datepicker_JobEndDateEdit") as RadDatePicker;
            DatePickerEndDate.DbSelectedDate = (item["JobEndDate"].Controls[0] as TextBox).Text;

            RadDropDownList ddlCompany = item.FindControl("ddlCompanyGrid") as RadDropDownList;
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            ddlCompany.SelectedText = (item["CompanyName"].Controls[0] as TextBox).Text;

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
            ddlMethodOfCalculation.Enabled = false;

           

            RadDropDownList ddlMetersFeet = item.FindControl("ddlMetersFeet") as RadDropDownList;
            ddlMetersFeet.SelectedText = (item["MetersFeet"].Controls[0] as TextBox).Text;
            ddlMetersFeet.Enabled = false;
            RadDropDownList ddlDogLeg = item.FindControl("ddlDogLeg") as RadDropDownList;
            DataTable dtDogLeg = RigTrack.DatabaseObjects.RigTrackDO.GetAllDogLeg();
            ddlDogLeg.DataSource = dtDogLeg;
            ddlDogLeg.DataTextField = "Name";
            ddlDogLeg.DataValueField = "ID";
            ddlDogLeg.DataBind();
            if (ddlMetersFeet.SelectedText == "Feet")
            {
                ddlDogLeg.Items.Add(new DropDownListItem("-NA-", "0"));
                ddlDogLeg.SelectedText = "-NA-";
            }
            else
            {
                 ddlDogLeg.SelectedText = (item["DogLegName"].Controls[0] as TextBox).Text;
            }
            ddlDogLeg.Enabled = false;

            TextBox txtLatLong = (TextBox)item["primaryLatLong"].Controls[0];
            txtLatLong.Width = 130;
           
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
            ddlOutputName.Enabled = false;
            RadDropDownList ddlInputName = item.FindControl("ddlInputName") as RadDropDownList;
            ddlInputName.DataSource = dtInputOutput;
            ddlInputName.DataTextField = "Name";
            ddlInputName.DataValueField = "ID";
            ddlInputName.DataBind();
            ddlInputName.SelectedText = (item["InputName"].Controls[0] as TextBox).Text;
            ddlInputName.Enabled = false;
            RadDropDownList ddlVerticalSection = item.FindControl("ddlVerticalSection") as RadDropDownList;
            DataTable dtVerticalSection = RigTrack.DatabaseObjects.RigTrackDO.GetAllVerticalSectionRefs();
            ddlVerticalSection.DataSource = dtVerticalSection;
            ddlVerticalSection.DataTextField = "Name";
            ddlVerticalSection.DataValueField = "ID";
            ddlVerticalSection.DataBind();
            ddlVerticalSection.SelectedText = (item["VSectionName"].Controls[0] as TextBox).Text;
            ddlVerticalSection.Enabled = false;

            TextBox txtNSOffset = (TextBox)item["NSOffset"].Controls[0];
            txtNSOffset.Width = 70;
            txtNSOffset.Enabled = false;
            TextBox txtEWOffset = (TextBox)item["EWOffset"].Controls[0];
            txtEWOffset.Width = 70;
            txtEWOffset.Enabled = false;
            MethodOfCalMessage.Visible = true;

            btnView.Enabled = false;
        }
    }
    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridDataItem item = (GridDataItem)RadGrid1.SelectedItems[0];
        
        foreach (GridDataItem item2 in RadGrid1.MasterTableView.Items)
        {

            if (item2.IsInEditMode)
            {
                item2.Edit = false;
            }
            
            
            string itemDate = item2.GetDataKeyValue("JobEndDate").ToString();
            itemDate = HttpUtility.HtmlDecode(itemDate);
            if (string.IsNullOrEmpty(itemDate) || string.IsNullOrWhiteSpace(itemDate))
            {

            }
            else
            {
                item2["EditButton"].Enabled = false;
                item2["EditButton"].Text = "Closed";

            }
        }
        hiddenField.Value = (item.FindControl("lblCurveGroupID2") as Label).Text;
        btnView.Enabled = true;
        
        RadGrid1.Rebind();
        item.Selected = true;
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {
        //RadGrid1.PageSize = e.NewPageSize;
        //RadGrid1.Rebind();
    }
}