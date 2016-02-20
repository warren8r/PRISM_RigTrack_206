using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ManageRotatorySteerableData : System.Web.UI.Page
{
    MDM.Collector col = new MDM.Collector();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            RadGridBHARotatory.DataSource = bindBHADetails();
            RadGridBHARotatory.DataBind();
            //string query = "select ID, (BHANumber+'-'+BHADesc) as BHANumber from [RigTrack].[tblBHADataInfo] order by CreateDate desc";
            //DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            //radBHANumber.DataSource = dtJobs;
            //radBHANumber.DataBind();
        }
    }
    public DataTable bindBHADetails()
    {

        DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
        string ids = "";
        for (int i = 0; i < dtJobDetails.Rows.Count; i++)
        {
            ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
        }
        if (ids != "")
        {
            ids = ids.Remove(ids.Length - 1, 1);
        }
        string query = "select a.ID as RotatoryID,b.BHANumber,b.BhaDesc,J.CurveGroupName as JobName,* from [RigTrack].[tblBHARotatorySteerableData] a,[RigTrack].[tblBHADataInfo] b," +
"  [RigTrack].[tblCurveGroup] J,tblBHAType BT where a.BHAID=b.ID and B.JOBID=J.ID and B.BHAType=BT.ID";
        if (ddlCompany.SelectedValue != "0" && ids != "")
            query += " and  J.ID in (" + ids + ")";

        if (ddlCurveGroup.SelectedValue != "0")
            query += " and  J.ID=" + ddlCurveGroup.SelectedValue + "";



        DataTable dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        
        return dt_users;
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            ddlCurveGroup.Items.Clear();
            ddlCurveGroup.Items.Add(new DropDownListItem("-Select-", "0"));
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            ddlCurveGroup.DataSource = dtJobDetails;
            ddlCurveGroup.DataTextField = "CurveGroupName";
            ddlCurveGroup.DataValueField = "ID";
            ddlCurveGroup.DataBind();

        }
        else
        {


        }

    }
    protected void ddlCurveGroup_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCurveGroup.SelectedValue != "0")
        {

            radBHANumber.Items.Clear();
            radBHANumber.Items.Add(new RadComboBoxItem("-Select-", "0"));
            string query = "select ID, (BHANumber+'-'+BHADesc) as BHANumber from [RigTrack].[tblBHADataInfo] where JOBID=" + ddlCurveGroup.SelectedValue + "  order by CreateDate desc";
            DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            radBHANumber.DataSource = dtJobs;
            radBHANumber.DataBind();


        }
        else
        {


        }

    }
    protected void RadGridBHARotatory_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "EditMeter")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("RotatoryID"));
            // Select the item
            item.Selected = true;
            btnSaveRotatoryData.Text = "Update";
            hidden_serviceid.Value = dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM [RigTrack].[tblBHARotatorySteerableData] where ID=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {
        ddlCompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString();
        DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
        ddlCurveGroup.DataSource = dtJobDetails;
        ddlCurveGroup.DataTextField = "CurveGroupName";
        ddlCurveGroup.DataValueField = "ID";
        ddlCurveGroup.DataBind();
        ddlCurveGroup.SelectedValue = dt.Rows[0]["JOBID"].ToString();

        string query = "select ID, (BHANumber+'-'+BHADesc) as BHANumber from [RigTrack].[tblBHADataInfo] where JOBID=" + ddlCurveGroup.SelectedValue + "  order by CreateDate desc";
        DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        radBHANumber.DataSource = dtJobs;
        radBHANumber.DataBind();

        radBHANumber.SelectedValue = dt.Rows[0]["BHAID"].ToString();
        radBHANumber.Enabled = true;
        txtRSDesc.Text = dt.Rows[0]["RSDesc"].ToString();
        txtRSMfg.Text = dt.Rows[0]["RSMfg"].ToString();
        txtPushPointType.Text = dt.Rows[0]["PushPointType"].ToString();
        txtFirmWarever.Text = dt.Rows[0]["FirmWarever"].ToString();
        txtMaxDLS.Text = dt.Rows[0]["MaxDLS"].ToString();
        txtRSLowerStab.Text = dt.Rows[0]["RSLowerStab"].ToString();
        txtBitNB.Text = dt.Rows[0]["BitNB"].ToString();
        txtCtrBlades.Text = dt.Rows[0]["CtrBlades"].ToString();
        txtRSStabToTopStab.Text = dt.Rows[0]["RSStabToTopStab"].ToString();
        txtBatteryInAhOut.Text = dt.Rows[0]["BatteryInAhOut"].ToString();
        txtNumberOfBlades.Text = dt.Rows[0]["NumberOfBlades"].ToString();
        txtBladeTypes.Text = dt.Rows[0]["BladeTypes"].ToString();
        txtBladeProfile.Text = dt.Rows[0]["BladeProfile"].ToString();
        txtRSSno.Text = dt.Rows[0]["RSSno"].ToString();
        txtWakeupRPMDrill.Text = dt.Rows[0]["WakeupRPMDrill"].ToString();
        txtBladeCollapseTime.Text = dt.Rows[0]["BladeCollapseTime"].ToString();
        txtHardfacing.Text = dt.Rows[0]["Hardfacing"].ToString();
        txtRSGuageLength.Text = dt.Rows[0]["RSGuageLength"].ToString();
        txtRSPadOD.Text = dt.Rows[0]["RSPadOD"].ToString();
        if(dt.Rows[0]["RSFailure"]!=null)
            chkRSFailure.Checked=Convert.ToBoolean(dt.Rows[0]["RSFailure"]);
        if (dt.Rows[0]["RSFailure"] != null)
            chkRunWithMotor.Checked = Convert.ToBoolean(dt.Rows[0]["RunWithMotor"]);
        
        txtReasonPulled.Text = dt.Rows[0]["ReasonPulled"].ToString();
        txtRSPerformance.Text = dt.Rows[0]["RSPerformance"].ToString();
        txtAdditionalComments.Text = dt.Rows[0]["AdditionalComments"].ToString();
        

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearAll();
        lbl_message.Text = "";
    }
    protected void btnSaveRotatoryData_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSaveRotatoryData.Text == "Create")
            {
                RigTrack.DatabaseTransferObjects.BHARotatorySteerableDTO bhaMotorDTO = new RigTrack.DatabaseTransferObjects.BHARotatorySteerableDTO();
                bhaMotorDTO.ID = 0;
                bhaMotorDTO.CompanyID = col.IntUtilParse(ddlCompany.SelectedValue);
                bhaMotorDTO.JOBID = col.IntUtilParse(ddlCurveGroup.SelectedValue);
                bhaMotorDTO.BHAID = col.IntUtilParse(radBHANumber.SelectedValue);
                bhaMotorDTO.RSDesc = txtRSDesc.Text;
                bhaMotorDTO.RSMfg=txtRSDesc.Text;
                bhaMotorDTO.PushPointType=txtPushPointType.Text;
                bhaMotorDTO.FirmWarever=txtFirmWarever.Text;
                bhaMotorDTO.MaxDLS=txtMaxDLS.Text;
                bhaMotorDTO.RSLowerStab=txtRSLowerStab.Text;
                bhaMotorDTO.BitNB=txtBitNB.Text;
                bhaMotorDTO.CtrBlades=txtCtrBlades.Text;
                bhaMotorDTO.RSStabToTopStab=txtRSStabToTopStab.Text;
                bhaMotorDTO.BatteryInAhOut=txtBatteryInAhOut.Text;
                bhaMotorDTO.NumberOfBlades=txtNumberOfBlades.Text;
                bhaMotorDTO.BladeTypes=txtBladeTypes.Text;
                bhaMotorDTO.BladeProfile=txtBladeProfile.Text;
                bhaMotorDTO.RSSno=txtRSSno.Text;
                bhaMotorDTO.WakeupRPMDrill=txtWakeupRPMDrill.Text;
                bhaMotorDTO.BladeCollapseTime=txtBladeCollapseTime.Text;
                bhaMotorDTO.Hardfacing=txtHardfacing.Text;
                bhaMotorDTO.RSGuageLength=txtRSGuageLength.Text;
                bhaMotorDTO.RSPadOD=txtRSPadOD.Text;
                if(chkRSFailure.Checked)
                    bhaMotorDTO.RSFailure=true;
                if(chkRunWithMotor.Checked)
                    bhaMotorDTO.RunWithMotor=true;
                bhaMotorDTO.ReasonPulled=txtReasonPulled.Text;
                bhaMotorDTO.RSPerformance=txtRSPerformance.Text;
                bhaMotorDTO.AdditionalComments=txtAdditionalComments.Text;
                int insertMotorDataID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHARotatorySteerableData(bhaMotorDTO);
                lbl_message.Text = "BHA Rotatory Steerable Data Inserted Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            else
            {
                RigTrack.DatabaseTransferObjects.BHARotatorySteerableDTO bhaMotorDTO = new RigTrack.DatabaseTransferObjects.BHARotatorySteerableDTO();
                bhaMotorDTO.ID = col.IntUtilParse(hidden_serviceid.Value);
                bhaMotorDTO.CompanyID = col.IntUtilParse(ddlCompany.SelectedValue);
                bhaMotorDTO.JOBID = col.IntUtilParse(ddlCurveGroup.SelectedValue);
                bhaMotorDTO.BHAID = col.IntUtilParse(radBHANumber.SelectedValue);
                bhaMotorDTO.CompanyID = col.IntUtilParse(ddlCompany.SelectedValue);
                bhaMotorDTO.JOBID = col.IntUtilParse(ddlCurveGroup.SelectedValue);
                bhaMotorDTO.BHAID = col.IntUtilParse(radBHANumber.SelectedValue);
                bhaMotorDTO.RSDesc = txtRSDesc.Text;
                bhaMotorDTO.RSMfg = txtRSDesc.Text;
                bhaMotorDTO.PushPointType = txtPushPointType.Text;
                bhaMotorDTO.FirmWarever = txtFirmWarever.Text;
                bhaMotorDTO.MaxDLS = txtMaxDLS.Text;
                bhaMotorDTO.RSLowerStab = txtRSLowerStab.Text;
                bhaMotorDTO.BitNB = txtBitNB.Text;
                bhaMotorDTO.CtrBlades = txtCtrBlades.Text;
                bhaMotorDTO.RSStabToTopStab = txtRSStabToTopStab.Text;
                bhaMotorDTO.BatteryInAhOut = txtBatteryInAhOut.Text;
                bhaMotorDTO.NumberOfBlades = txtNumberOfBlades.Text;
                bhaMotorDTO.BladeTypes = txtBladeTypes.Text;
                bhaMotorDTO.BladeProfile = txtBladeProfile.Text;
                bhaMotorDTO.RSSno = txtRSSno.Text;
                bhaMotorDTO.WakeupRPMDrill = txtWakeupRPMDrill.Text;
                bhaMotorDTO.BladeCollapseTime = txtBladeCollapseTime.Text;
                bhaMotorDTO.Hardfacing = txtHardfacing.Text;
                bhaMotorDTO.RSGuageLength = txtRSGuageLength.Text;
                bhaMotorDTO.RSPadOD = txtRSPadOD.Text;
                if (chkRSFailure.Checked)
                    bhaMotorDTO.RSFailure = true;
                if (chkRunWithMotor.Checked)
                    bhaMotorDTO.RunWithMotor = true;
                bhaMotorDTO.ReasonPulled = txtReasonPulled.Text;
                bhaMotorDTO.RSPerformance = txtRSPerformance.Text;
                bhaMotorDTO.AdditionalComments = txtAdditionalComments.Text;
                int insertMotorDataID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHARotatorySteerableData(bhaMotorDTO);
                lbl_message.Text = "BHA Rotatory Steerable Data Updated Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            clearAll();
        }
        catch (Exception ex)
        {

        }

    }
    public void clearAll()
    {
        ddlCompany.SelectedValue = "0";
        ddlCurveGroup.SelectedValue = "0";
        txtRSDesc.Text = "";
        txtRSMfg.Text = "";
        txtPushPointType.Text = "";
        txtFirmWarever.Text = "";
        txtMaxDLS.Text = "";
        txtRSLowerStab.Text = "";
        txtBitNB.Text = "";
        txtCtrBlades.Text = "";
        txtRSStabToTopStab.Text = "";
        txtBatteryInAhOut.Text = "";
        txtNumberOfBlades.Text = "";
        txtBladeTypes.Text = "";
        txtBladeProfile.Text = "";
        txtRSSno.Text = "";
        txtWakeupRPMDrill.Text = "";
        txtBladeCollapseTime.Text = "";
        txtHardfacing.Text = "";
        txtRSGuageLength.Text = "";
        txtRSPadOD.Text = "";
        chkRSFailure.Checked = false;
        chkRunWithMotor.Checked=false;
        txtReasonPulled.Text = "";
        txtRSPerformance.Text = "";
        txtAdditionalComments.Text = "";
        btnSaveRotatoryData.Text = "Create";
    }
}