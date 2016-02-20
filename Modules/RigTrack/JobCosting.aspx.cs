using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;

public partial class Modules_Configuration_Manager_JobCosting : System.Web.UI.Page
{
    double total;
    MDM.Collector col = new MDM.Collector();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            //btnSubmit.Visible = false;
            RadcomboCostGroup.Items.Add(new RadComboBoxItem("Select", "0"));
            RadComboChargeBy.Items.Add(new RadComboBoxItem("Select", "0"));

            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                 "select * from [RigTrack].[tblCurveGroup] where isActive=1 and ID in (select jobid from PrismJobAssignedAssets)").Tables[0];
            DataTable dt_search = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from [RigTrack].[tblCurveGroup] where isActive=1 and ID in (select jobid from PrismJobAssignedAssets)").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "CurveGroupName", "ID", "0");

            RadComboBoxFill.FillRadcombobox(comboSearchJob, dt_search, "CurveGroupName", "ID", "0");
            bindGrid();

        }
    }
    public void bindGrid()
    {
        string query = "select JC.ID,J.[CurveGroupName] as JobName,JC.Date,CH.ChargeByDesc,CG.CostGroupName,JC.[PriceperUnit],JC.[NumberOfUnits],JC.Total from [dbo].[tblJobCosting] JC "+
            " inner join [dbo].[tlkpChargeByDetails] CH on JC.ChargeBYID=CH.ChargeBYID"+
            " inner join [dbo].[tlkpCostGroupDetails] CG on JC.CostGroupID=CG.CostGroupID"+
            " inner join [RigTrack].[tblCurveGroup] J on JC.JOBID=J.ID";
        DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        RadGridJobCosting.DataSource = dtJobs;
        RadGridJobCosting.DataBind();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            radcombo_job.Items.Clear();
            radcombo_job.Items.Add(new RadComboBoxItem("Select", "0"));
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                 "select * from [RigTrack].[tblCurveGroup] where isActive=1 and ID in (select jobid from PrismJobAssignedAssets) and CompanyID="+ ddlCompany.SelectedValue+ "").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "CurveGroupName", "ID", "0");
            //radtxt_start.SelectedDate = Convert.ToDateTime(dt.Rows[0]["JobStartDate"].ToString());
            //radtxt_start.MinDate = Convert.ToDateTime(dt.Rows[0]["JobStartDate"].ToString());

        }
        else
        {


        }

    }
    protected void radcombo_job_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radcombo_job.SelectedValue != "0")
        {
            string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            " RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and JO.ID =" + radcombo_job.SelectedValue + " order by JO.CreateDate desc";
            DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            radtxt_start.SelectedDate = Convert.ToDateTime(dtJobs.Rows[0]["JobStartDate"].ToString());
            radtxt_start.MinDate = Convert.ToDateTime(dtJobs.Rows[0]["JobStartDate"].ToString());
        }
        else
        {
           
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                RigTrack.DatabaseTransferObjects.JObCostingDTO costingDTO = new RigTrack.DatabaseTransferObjects.JObCostingDTO();
                costingDTO.ID = 0;
                costingDTO.CompanyID = col.IntUtilParse(ddlCompany.SelectedValue);
                costingDTO.JOBID = col.IntUtilParse(radcombo_job.SelectedValue);
                costingDTO.Date = radtxt_start.SelectedDate;
                costingDTO.CostGroupID = col.IntUtilParse(RadcomboCostGroup.SelectedValue);
                costingDTO.ChargeBYID = col.IntUtilParse(RadComboChargeBy.SelectedValue);
                costingDTO.PriceperUnit = col.DecimalUtilParse(txtPriceperUnit.Text);
                costingDTO.NumberOfUnits =  col.IntUtilParse(txtQty.Text);
                costingDTO.Total = col.DecimalUtilParse(txtTotal.Text);

                int insertMotorDataID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateJobCostDetails(costingDTO);
                lbl_message.Text = "Job Costing Details Created Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            else
            {
                RigTrack.DatabaseTransferObjects.JObCostingDTO costingDTO = new RigTrack.DatabaseTransferObjects.JObCostingDTO();
                costingDTO.ID = col.IntUtilParse(hidden_serviceid.Value);
                costingDTO.CompanyID = col.IntUtilParse(ddlCompany.SelectedValue);
                costingDTO.JOBID = col.IntUtilParse(radcombo_job.SelectedValue);
                costingDTO.Date = radtxt_start.SelectedDate;
                costingDTO.CostGroupID = col.IntUtilParse(RadcomboCostGroup.SelectedValue);
                costingDTO.ChargeBYID = col.IntUtilParse(RadComboChargeBy.SelectedValue);
                costingDTO.PriceperUnit = col.DecimalUtilParse(txtPriceperUnit.Text);
                costingDTO.NumberOfUnits = col.IntUtilParse(txtQty.Text);
                costingDTO.Total = col.DecimalUtilParse(txtTotal.Text);
                int insertMotorDataID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateJobCostDetails(costingDTO);
                lbl_message.Text = "Job Costing Details Updated Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            clearAll();
            bindGrid();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        string query = "select JC.ID,J.[CurveGroupName] as JobName,JC.Date,CH.ChargeByDesc,CG.CostGroupName,JC.[PriceperUnit],JC.[NumberOfUnits],JC.Total from [dbo].[tblJobCosting] JC " +
            " inner join [dbo].[tlkpChargeByDetails] CH on JC.ChargeBYID=CH.ChargeBYID" +
            " inner join [dbo].[tlkpCostGroupDetails] CG on JC.CostGroupID=CG.CostGroupID" +
            " inner join [RigTrack].[tblCurveGroup] J on JC.JOBID=J.ID";
        if (comboSearchJob.SelectedValue != "0")
        {
            query += " where JC.JOBID=" + comboSearchJob.SelectedValue + "";
        }
        if (comboSearchJob.SelectedValue == "0" && DateSearch.SelectedDate.ToString() != "")
        {
            query += " where JC.Date='" + Convert.ToDateTime(DateSearch.SelectedDate).ToString("MM/dd/yyyy") + "'";
        }
        DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        RadGridJobCosting.DataSource = dtJobs;
        RadGridJobCosting.DataBind();
        //RadGridBHAInfo.DataSource = bindBHADetails();
        //RadGridBHAInfo.Rebind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        comboSearchJob.ClearSelection();
        DateSearch.Clear();
        bindGrid();
    }
    public void clearAll()
    {
        //radcombo_job.SelectedValue = "0";
        //radtxt_start.SelectedDate = "";
        RadcomboCostGroup.SelectedValue = "0";
        RadComboChargeBy.SelectedValue = "0";
        txtPriceperUnit.Text = "";
        txtQty.Text = "";
        txtTotal.Text = "";
        
    }
    protected void RadGridJobCosting_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "EditMeter")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("ID"));
            // Select the item
            item.Selected = true;
            btnSave.Text = "Update";
            hidden_serviceid.Value = dataKeyValue;
            string query = "select * from [dbo].[tblJobCosting] where ID=" + dataKeyValue + "";
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0]);
        }
        

    }
    public void BindDataToEdit(DataTable dt)
    {
        ddlCompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString();
        DataTable dtJob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                  "select * from [RigTrack].[tblCurveGroup] where isActive=1 and ID in (select jobid from PrismJobAssignedAssets) and CompanyID=" + ddlCompany.SelectedValue + "").Tables[0];
        RadComboBoxFill.FillRadcombobox(radcombo_job, dtJob, "CurveGroupName", "ID", "0");
        radcombo_job.SelectedValue = dt.Rows[0]["JOBID"].ToString();
        radtxt_start.SelectedDate=Convert.ToDateTime(dt.Rows[0]["Date"].ToString());
        RadcomboCostGroup.SelectedValue = dt.Rows[0]["CostGroupID"].ToString();
        RadComboChargeBy.SelectedValue = dt.Rows[0]["ChargeBYID"].ToString();
        txtPriceperUnit.Text = dt.Rows[0]["PriceperUnit"].ToString();
        txtQty.Text = dt.Rows[0]["NumberOfUnits"].ToString();
        txtTotal.Text = dt.Rows[0]["Total"].ToString();

    }
    
   
}