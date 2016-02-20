using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ManageBHAMotorData : System.Web.UI.Page
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
            lbl_message.Text = "";
            RadGridBHAInfo.DataSource= bindBHADetails();
            RadGridBHAInfo.DataBind();
            //string query = "select ID, (BHANumber+'-'+BHADesc) as BHANumber from [RigTrack].[tblBHADataInfo] order by CreateDate desc";
            //DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            //radBHANumber.DataSource = dtJobs;
            //radBHANumber.DataBind();
        }
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
    
    protected void RadGridBHAInfo_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "EditMeter")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("MotorID"));
            // Select the item
            item.Selected = true;
            btnSaveMotorData.Text = "Update";
            hidden_serviceid.Value = dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM [RigTrack].[tblBHAMotorData] where ID=" + dataKeyValue + "").Tables[0]);
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            //string curveGroupName = ViewState["CurveGroupName"].ToString();
            //curveGroupName = curveGroupName.Substring(curveGroupName.IndexOf('-') + 2);
            //string targetName = ViewState["TargetName"].ToString();
            //targetName = targetName.Substring(targetName.IndexOf('-') + 2);

           // RadGridBHAInfo.ExportSettings.FileName = "BHA Details " + DateTime.Now.Date.ToString("MM/dd/yyyy");
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
        txtMotorDesc.Text = dt.Rows[0]["MotorDesc"].ToString();
        txtMotorMFG.Text = dt.Rows[0]["MotorMFG"].ToString();
        txtNBStabilizer.Text = dt.Rows[0]["NBStabilizer"].ToString();
        txtModel.Text = dt.Rows[0]["Model"].ToString();
        txtRevolutions.Text = dt.Rows[0]["Revolutions"].ToString();
        txtBend.Text = dt.Rows[0]["Bend"].ToString();
        txtRotorJet.Text = dt.Rows[0]["RotorJet"].ToString();
        txtBittoBend.Text = dt.Rows[0]["BittoBend"].ToString();
        txtPropBUR.Text = dt.Rows[0]["PropBUR"].ToString();
        txtRealBUR.Text = dt.Rows[0]["RealBUR"].ToString();
        txtPadOD.Text = dt.Rows[0]["PadOD"].ToString();
        txtAverageDifferential.Text = dt.Rows[0]["AverageDifferential"].ToString();
        txtLobes.Text = dt.Rows[0]["Lobes"].ToString();
        txtOffBottomDifference.Text = dt.Rows[0]["OffBottomDifference"].ToString();
        txtStages.Text = dt.Rows[0]["Stages"].ToString();
        txtStallPressure.Text = dt.Rows[0]["StallPressure"].ToString();
        txtClearence.Text = dt.Rows[0]["Clearence"].ToString();
        txtAvgOnBottomSPP.Text = dt.Rows[0]["AvgOnBottomSPP"].ToString();
        txtAvgOffBottomSPP.Text = dt.Rows[0]["AvgOffBottomSPP"].ToString();
        txtNoOfStalls.Text = dt.Rows[0]["NoOfStalls"].ToString();
        txtStallTime.Text = dt.Rows[0]["StallTime"].ToString();
        txtFormation.Text = dt.Rows[0]["Formation"].ToString();
        txtBentSubDeg.Text = dt.Rows[0]["BentSubDeg"].ToString();
        txtElastomer.Text = dt.Rows[0]["Elastomer"].ToString();
        txtBendType.Text = dt.Rows[0]["BendType"].ToString();
        txtClearenceRng.Text = dt.Rows[0]["ClearenceRng"].ToString();
        txtMEDCompany.Text = dt.Rows[0]["MEDCompany"].ToString();
        txtNoOfMWDRuns.Text = dt.Rows[0]["NoOfMWDRuns"].ToString();
        txtInspectionCmpny.Text = dt.Rows[0]["InspectionCmpny"].ToString();
        txtReasonPulled.Text = dt.Rows[0]["ReasonPulled"].ToString();
        txtBHAObjectives.Text = dt.Rows[0]["BHAObjectives"].ToString();
        txtBHAPerformance.Text = dt.Rows[0]["BHAPerformance"].ToString();
        txtAdditionalComments.Text = dt.Rows[0]["AdditionalComments"].ToString();
        txtBotStabilizerType.Text = dt.Rows[0]["BotStabilizerType"].ToString();
        txtBotStabBladeType.Text = dt.Rows[0]["BotStabBladeType"].ToString();
        txtBotStabLength.Text = dt.Rows[0]["BotStabLength"].ToString();
        txtLowerStabOD.Text = dt.Rows[0]["LowerStabOD"].ToString();
        txtEvenWall.Text = dt.Rows[0]["EvenWall"].ToString();
        txtTopStabilizerType.Text = dt.Rows[0]["TopStabilizerType"].ToString();
        txtTopStabBladeType.Text = dt.Rows[0]["TopStabBladeType"].ToString();
        txtTopStabLength.Text = dt.Rows[0]["TopStabLength"].ToString();
        txtUpperStabOD.Text = dt.Rows[0]["UpperStabOD"].ToString();
        txtInterferenceFit.Text = dt.Rows[0]["InterferenceFit"].ToString();
        txtInterferenceTol.Text = dt.Rows[0]["InterferenceTol"].ToString();
        comboWearPad.SelectedValue = dt.Rows[0]["WearPad"].ToString();
        txtWearPadType.Text = dt.Rows[0]["WearPadType"].ToString();
        txtWearpadlength.Text = dt.Rows[0]["Wearpadlength"].ToString();
        txtWearpadHeight.Text = dt.Rows[0]["WearpadHeight"].ToString();
        txtWearpadWidth.Text = dt.Rows[0]["WearpadWidth"].ToString();
        txtWearpadGuage.Text = dt.Rows[0]["WearpadGuage"].ToString();
        txtBitToWearpad.Text = dt.Rows[0]["BitToWearpad"].ToString();
        txtMaxSurfRPM.Text = dt.Rows[0]["MaxSurfRPM"].ToString();
        txtMaxDLRotating.Text = dt.Rows[0]["MaxDLRotating"].ToString();
        txtMaxDLSliding.Text = dt.Rows[0]["MaxDLSliding"].ToString();
        txtMaxDiffPress.Text = dt.Rows[0]["MaxDiffPress"].ToString();
        txtMaxFlowRate.Text = dt.Rows[0]["MaxFlowRate"].ToString();
        txtMaxTorque.Text = dt.Rows[0]["MaxTorque"].ToString();
        
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
        string query = "select a.ID as MotorID,b.BHANumber,b.BhaDesc,J.CurveGroupName as JobName,* from [RigTrack].[tblBHAMotorData] a,[RigTrack].[tblBHADataInfo] b," +
"  [RigTrack].[tblCurveGroup] J,tblBHAType BT where a.BHAID=b.ID and B.JOBID=J.ID and B.BHAType=BT.ID";
        if (ddlCompany.SelectedValue != "0" && ids != "")
            query += " and  J.ID in (" + ids + ")";

        if (ddlCurveGroup.SelectedValue != "0")
            query += " and  J.ID=" + ddlCurveGroup.SelectedValue + "";



        DataTable dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        //if (dt_users.Rows.Count > 0)
        //{
        //    //DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ViewState["TargetID"].ToString()));
        //    RadGridBHAInfo.DataSource = dt_users;
        //    //RadGridBHAInfo.DataBind();
        //}
        //else
        //{
        //    DataTable dt = new DataTable();
        //    RadGridBHAInfo.DataSource = dt;
        //}
        return dt_users;
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearAll();
        lbl_message.Text = "";
    }
    protected void btnSaveMotorData_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSaveMotorData.Text == "Create")
            {
                RigTrack.DatabaseTransferObjects.BHAMotorDataDTO bhaMotorDTO = new RigTrack.DatabaseTransferObjects.BHAMotorDataDTO();
                bhaMotorDTO.ID = 0;
                bhaMotorDTO.CompanyID = col.IntUtilParse(ddlCompany.SelectedValue);
                bhaMotorDTO.JOBID = col.IntUtilParse(ddlCurveGroup.SelectedValue);
                bhaMotorDTO.BHAID = col.IntUtilParse(radBHANumber.SelectedValue);
                bhaMotorDTO.MotorDesc = txtMotorDesc.Text;
                bhaMotorDTO.MotorMFG = txtMotorMFG.Text;
                bhaMotorDTO.NBStabilizer = txtNBStabilizer.Text;
                bhaMotorDTO.Model = txtModel.Text;
                bhaMotorDTO.Revolutions = txtRevolutions.Text;
                bhaMotorDTO.Bend = txtBend.Text;
                bhaMotorDTO.RotorJet = txtRotorJet.Text;
                bhaMotorDTO.BittoBend = txtBittoBend.Text;
                bhaMotorDTO.PropBUR = txtPropBUR.Text;
                bhaMotorDTO.RealBUR = txtRealBUR.Text;
                bhaMotorDTO.PadOD = txtPadOD.Text;
                bhaMotorDTO.AverageDifferential = txtAverageDifferential.Text;
                bhaMotorDTO.Lobes = txtLobes.Text;
                bhaMotorDTO.OffBottomDifference = txtOffBottomDifference.Text;
                bhaMotorDTO.Stages = txtStages.Text;
                bhaMotorDTO.StallPressure = txtStallPressure.Text;
                bhaMotorDTO.Clearence = txtClearence.Text;
                bhaMotorDTO.AvgOnBottomSPP = txtAvgOnBottomSPP.Text;
                bhaMotorDTO.AvgOffBottomSPP = txtAvgOffBottomSPP.Text;
                bhaMotorDTO.NoOfStalls = txtNoOfStalls.Text;
                bhaMotorDTO.StallTime = txtStallTime.Text;
                bhaMotorDTO.Formation = txtFormation.Text;
                bhaMotorDTO.BentSubDeg = txtBentSubDeg.Text;
                bhaMotorDTO.Elastomer = txtElastomer.Text;
                bhaMotorDTO.BendType = txtBendType.Text;
                bhaMotorDTO.ClearenceRng = txtClearenceRng.Text;
                bhaMotorDTO.MEDCompany = txtMEDCompany.Text;
                bhaMotorDTO.NoOfMWDRuns = txtNoOfMWDRuns.Text;
                bhaMotorDTO.InspectionCmpny = txtInspectionCmpny.Text;
                if (chkMotorFailure.Checked)
                    bhaMotorDTO.MotorFailure = true;
                else
                    bhaMotorDTO.MotorFailure = false;
                if (chkExtendedPowerSection.Checked)
                    bhaMotorDTO.ExtendedPowerSection = true;
                else
                    bhaMotorDTO.ExtendedPowerSection = true;
                if (chkInspected.Checked)
                    bhaMotorDTO.Inspected = true;
                else
                    bhaMotorDTO.Inspected = true;
                bhaMotorDTO.ReasonPulled = txtReasonPulled.Text;
                bhaMotorDTO.BHAObjectives = txtBHAObjectives.Text;
                bhaMotorDTO.BHAPerformance = txtBHAPerformance.Text;
                bhaMotorDTO.AdditionalComments = txtAdditionalComments.Text;
                bhaMotorDTO.BotStabilizerType = txtBotStabilizerType.Text;
                bhaMotorDTO.BotStabBladeType = txtBotStabBladeType.Text;
                bhaMotorDTO.BotStabLength = txtBotStabLength.Text;
                bhaMotorDTO.LowerStabOD = txtLowerStabOD.Text;
                bhaMotorDTO.EvenWall = txtEvenWall.Text;
                bhaMotorDTO.TopStabilizerType = txtTopStabilizerType.Text;
                bhaMotorDTO.TopStabBladeType = txtTopStabBladeType.Text;
                bhaMotorDTO.TopStabLength = txtTopStabLength.Text;
                bhaMotorDTO.UpperStabOD = txtUpperStabOD.Text;
                bhaMotorDTO.InterferenceFit = txtInterferenceFit.Text;
                bhaMotorDTO.InterferenceTol = txtInterferenceTol.Text;
                bhaMotorDTO.WearPad = comboWearPad.SelectedItem.Text;
                bhaMotorDTO.WearPadType = txtWearPadType.Text;
                bhaMotorDTO.Wearpadlength = txtWearpadlength.Text;
                bhaMotorDTO.WearpadHeight = txtWearpadHeight.Text;
                bhaMotorDTO.WearpadWidth = txtWearpadWidth.Text;
                bhaMotorDTO.WearpadGuage = txtWearpadGuage.Text;
                bhaMotorDTO.BitToWearpad = txtBitToWearpad.Text;
                bhaMotorDTO.MaxSurfRPM = txtMaxSurfRPM.Text;
                bhaMotorDTO.MaxDLRotating = txtMaxDLRotating.Text;
                bhaMotorDTO.MaxDLSliding = txtMaxDLSliding.Text;
                bhaMotorDTO.MaxDiffPress = txtMaxDiffPress.Text;
                bhaMotorDTO.MaxFlowRate = txtMaxFlowRate.Text;
                bhaMotorDTO.MaxTorque = txtMaxTorque.Text;
                int insertMotorDataID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHAMotorData(bhaMotorDTO);
                lbl_message.Text = "BHA Motor Data Created Successfully";
                lbl_message.ForeColor = Color.Green;
            }
            else
            {
                RigTrack.DatabaseTransferObjects.BHAMotorDataDTO bhaMotorDTO = new RigTrack.DatabaseTransferObjects.BHAMotorDataDTO();
                bhaMotorDTO.ID = col.IntUtilParse(hidden_serviceid.Value);
                bhaMotorDTO.CompanyID = col.IntUtilParse(ddlCompany.SelectedValue);
                bhaMotorDTO.JOBID = col.IntUtilParse(ddlCurveGroup.SelectedValue);
                bhaMotorDTO.BHAID = col.IntUtilParse(radBHANumber.SelectedValue);
                bhaMotorDTO.MotorDesc = txtMotorDesc.Text;
                bhaMotorDTO.MotorMFG = txtMotorMFG.Text;
                bhaMotorDTO.NBStabilizer = txtNBStabilizer.Text;
                bhaMotorDTO.Model = txtModel.Text;
                bhaMotorDTO.Revolutions = txtRevolutions.Text;
                bhaMotorDTO.Bend = txtBend.Text;
                bhaMotorDTO.RotorJet = txtRotorJet.Text;
                bhaMotorDTO.BittoBend = txtBittoBend.Text;
                bhaMotorDTO.PropBUR = txtPropBUR.Text;
                bhaMotorDTO.RealBUR = txtRealBUR.Text;
                bhaMotorDTO.PadOD = txtPadOD.Text;
                bhaMotorDTO.AverageDifferential = txtAverageDifferential.Text;
                bhaMotorDTO.Lobes = txtLobes.Text;
                bhaMotorDTO.OffBottomDifference = txtOffBottomDifference.Text;
                bhaMotorDTO.Stages = txtStages.Text;
                bhaMotorDTO.StallPressure = txtStallPressure.Text;
                bhaMotorDTO.Clearence = txtClearence.Text;
                bhaMotorDTO.AvgOnBottomSPP = txtAvgOnBottomSPP.Text;
                bhaMotorDTO.AvgOffBottomSPP = txtAvgOffBottomSPP.Text;
                bhaMotorDTO.NoOfStalls = txtNoOfStalls.Text;
                bhaMotorDTO.StallTime = txtStallTime.Text;
                bhaMotorDTO.Formation = txtFormation.Text;
                bhaMotorDTO.BentSubDeg = txtBentSubDeg.Text;
                bhaMotorDTO.Elastomer = txtElastomer.Text;
                bhaMotorDTO.BendType = txtBendType.Text;
                bhaMotorDTO.ClearenceRng = txtClearenceRng.Text;
                bhaMotorDTO.MEDCompany = txtMEDCompany.Text;
                bhaMotorDTO.NoOfMWDRuns = txtNoOfMWDRuns.Text;
                bhaMotorDTO.InspectionCmpny = txtInspectionCmpny.Text;
                if (chkMotorFailure.Checked)
                    bhaMotorDTO.MotorFailure = true;
                else
                    bhaMotorDTO.MotorFailure = false;
                if (chkExtendedPowerSection.Checked)
                    bhaMotorDTO.ExtendedPowerSection = true;
                else
                    bhaMotorDTO.ExtendedPowerSection = true;
                if (chkInspected.Checked)
                    bhaMotorDTO.Inspected = true;
                else
                    bhaMotorDTO.Inspected = true;
                bhaMotorDTO.ReasonPulled = txtReasonPulled.Text;
                bhaMotorDTO.BHAObjectives = txtBHAObjectives.Text;
                bhaMotorDTO.BHAPerformance = txtBHAPerformance.Text;
                bhaMotorDTO.AdditionalComments = txtAdditionalComments.Text;
                bhaMotorDTO.BotStabilizerType = txtBotStabilizerType.Text;
                bhaMotorDTO.BotStabBladeType = txtBotStabBladeType.Text;
                bhaMotorDTO.BotStabLength = txtBotStabLength.Text;
                bhaMotorDTO.LowerStabOD = txtLowerStabOD.Text;
                bhaMotorDTO.EvenWall = txtEvenWall.Text;
                bhaMotorDTO.TopStabilizerType = txtTopStabilizerType.Text;
                bhaMotorDTO.TopStabBladeType = txtTopStabBladeType.Text;
                bhaMotorDTO.TopStabLength = txtTopStabLength.Text;
                bhaMotorDTO.UpperStabOD = txtUpperStabOD.Text;
                bhaMotorDTO.InterferenceFit = txtInterferenceFit.Text;
                bhaMotorDTO.InterferenceTol = txtInterferenceTol.Text;
                bhaMotorDTO.WearPad = comboWearPad.SelectedItem.Text;
                bhaMotorDTO.WearPadType = txtWearPadType.Text;
                bhaMotorDTO.Wearpadlength = txtWearpadlength.Text;
                bhaMotorDTO.WearpadHeight = txtWearpadHeight.Text;
                bhaMotorDTO.WearpadWidth = txtWearpadWidth.Text;
                bhaMotorDTO.WearpadGuage = txtWearpadGuage.Text;
                bhaMotorDTO.BitToWearpad = txtBitToWearpad.Text;
                bhaMotorDTO.MaxSurfRPM = txtMaxSurfRPM.Text;
                bhaMotorDTO.MaxDLRotating = txtMaxDLRotating.Text;
                bhaMotorDTO.MaxDLSliding = txtMaxDLSliding.Text;
                bhaMotorDTO.MaxDiffPress = txtMaxDiffPress.Text;
                bhaMotorDTO.MaxFlowRate = txtMaxFlowRate.Text;
                bhaMotorDTO.MaxTorque = txtMaxTorque.Text;
                int insertMotorDataID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHAMotorData(bhaMotorDTO);
                lbl_message.Text = "BHA Motor Data Updated Successfully";
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
        ddlCompany.SelectedValue="0";
        ddlCurveGroup.SelectedValue="0";
        txtMotorDesc.Text="";
        txtMotorMFG.Text="";
        txtNBStabilizer.Text="";
        txtModel.Text="";
        txtRevolutions.Text="";
        txtBend.Text="";
        txtRotorJet.Text="";
        txtBittoBend.Text="";
        txtPropBUR.Text="";
        txtRealBUR.Text="";
        txtPadOD.Text="";
        txtAverageDifferential.Text="";
        txtLobes.Text="";
        txtOffBottomDifference.Text="";
        txtStages.Text="";
        txtStallPressure.Text="";
        txtClearence.Text="";
        txtAvgOnBottomSPP.Text="";
        txtAvgOffBottomSPP.Text="";
        txtNoOfStalls.Text="";
        txtStallTime.Text="";
        txtFormation.Text="";
        txtBentSubDeg.Text="";
        txtElastomer.Text="";
        txtBendType.Text="";
        txtClearenceRng.Text="";
        txtMEDCompany.Text="";
        txtNoOfMWDRuns.Text="";
        txtInspectionCmpny.Text="";
        txtReasonPulled.Text="";
        txtBHAObjectives.Text="";
        txtBHAPerformance.Text="";
        txtAdditionalComments.Text="";
        txtBotStabilizerType.Text="";
        txtBotStabBladeType.Text="";
        txtBotStabLength.Text="";
        txtLowerStabOD.Text="";
        txtEvenWall.Text="";
        txtTopStabilizerType.Text="";
        txtTopStabBladeType.Text="";
        txtTopStabLength.Text="";
        txtUpperStabOD.Text="";
        txtInterferenceFit.Text="";
        txtInterferenceTol.Text="";
        comboWearPad.SelectedValue="0";
        txtWearPadType.Text="";
        txtWearpadlength.Text="";
        txtWearpadHeight.Text="";
        txtWearpadWidth.Text="";
        txtWearpadGuage.Text="";
        txtBitToWearpad.Text="";
        txtMaxSurfRPM.Text="";
        txtMaxDLRotating.Text="";
        txtMaxDLSliding.Text="";
        txtMaxDiffPress.Text="";
        txtMaxFlowRate.Text="";
        txtMaxTorque.Text="";
        btnSaveMotorData.Text = "Create";
        RadGridBHAInfo.DataSource = bindBHADetails();
        RadGridBHAInfo.DataBind();
    }

}