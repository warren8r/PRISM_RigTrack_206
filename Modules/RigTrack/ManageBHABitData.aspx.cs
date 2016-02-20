using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ManageBHABitData : System.Web.UI.Page
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
            RadGridBITInfo.Rebind();

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
    public bool uniqueAssetNameInsert(string assetname)
    {
        bool status = true;
        DataTable dt_prismasset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from [RigTrack].[tblBHABitData]").Tables[0];
        if (dt_prismasset.Rows.Count > 0)
        {
            DataRow[] row_asset = dt_prismasset.Select("BitSno='" + assetname + "'");
            if (row_asset.Length == 0)
                status = true;
            else
                status = false;
        }
        return status;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearItems();
        lbl_message.Text = "";
    }
    protected void btnSaveBitData_Click(object sender, EventArgs e)
    {
        if (btnSaveBitData.Text != "Update")
        {
            try
            {
                if (uniqueAssetNameInsert(txtBitSno.Text))
                {
                    RigTrack.DatabaseTransferObjects.BHABitDataDTO bhaBitDTO = new RigTrack.DatabaseTransferObjects.BHABitDataDTO();
                    bhaBitDTO.ID = 0;
                    bhaBitDTO.BearingSeals = txtBearingSeals.Text;
                    bhaBitDTO.BearingType = txtBearingType.Text;
                    bhaBitDTO.BHAID = radBHANumber.SelectedValue;
                    bhaBitDTO.BitDesc = txtBitDesc.Text;
                    bhaBitDTO.BitLength = col.DecimalUtilParse(txtBitLength.Text);
                    bhaBitDTO.BitMfg = txtBitMfg.Text;
                    bhaBitDTO.BitNumber = txtBitNumber.Text;
                    bhaBitDTO.BitSno = txtBitSno.Text;
                    bhaBitDTO.BittoDNSC = txtBittoDNSC.Text;
                    bhaBitDTO.BittoGamma = txtBittoGamma.Text;
                    bhaBitDTO.BittoGyro = txtBittoGyro.Text;
                    bhaBitDTO.BittoPorosity = txtBittoPorosity.Text;
                    bhaBitDTO.BittoResistivity = txtBittoResistivity.Text;
                    bhaBitDTO.BittoSensor = txtBittoSensor.Text;
                    bhaBitDTO.BitType = txtBitType.Text;
                    bhaBitDTO.Connection = txtConnection.Text;
                    bhaBitDTO.CreateDate = DateTime.Now;
                    bhaBitDTO.DullChar = txtDullChar.Text;
                    bhaBitDTO.Guage = txtGuage.Text;
                    bhaBitDTO.InnerRow = txtInnerRow.Text;
                    bhaBitDTO.isActive = true;
                    bhaBitDTO.LastModifyDate = DateTime.Now;
                    bhaBitDTO.Location = txtLocation.Text;
                    bhaBitDTO.NUMJETS = txtNUMJETS.Text;
                    bhaBitDTO.ODFrac = txtODFrac.Text;
                    bhaBitDTO.OtherDullChar = txtOtherDullChar.Text;
                    bhaBitDTO.OuterRow = txtOuterRow.Text;
                    bhaBitDTO.ReasonPulled = txtReasonPulled.Text;
                    string jetvalues = hdnvalue.Value;
                    string[] arr = jetvalues.Split(',');
                    bhaBitDTO.Jet1 = col.DecimalUtilParse(arr[0]);
                    bhaBitDTO.Jet2 = col.DecimalUtilParse(arr[1]);
                    bhaBitDTO.Jet3 = col.DecimalUtilParse(arr[2]);
                    bhaBitDTO.Jet4 = col.DecimalUtilParse(arr[3]);
                    bhaBitDTO.Jet5 = col.DecimalUtilParse(arr[4]);
                    bhaBitDTO.Jet6 = col.DecimalUtilParse(arr[5]);
                    bhaBitDTO.Jet7 = col.DecimalUtilParse(arr[6]);
                    bhaBitDTO.Jet8 = col.DecimalUtilParse(arr[7]);
                    bhaBitDTO.Jet9 = col.DecimalUtilParse(arr[8]);
                    bhaBitDTO.Jet10 = col.DecimalUtilParse(arr[9]);

                    int insertTargetDetailsID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHABitData(bhaBitDTO);
                    
                    lbl_message.Text = "BHA Bit Data Created Successfully";
                    lbl_message.ForeColor = Color.Green;
                }
                else
                {
                    lbl_message.Text = "Manufacturer Name already exist try another";
                    lbl_message.ForeColor = Color.Red;
                }
            }
            catch (Exception ex) { }

        }
        else
        {
            try
            {
                RigTrack.DatabaseTransferObjects.BHABitDataDTO bhaBitDTO = new RigTrack.DatabaseTransferObjects.BHABitDataDTO();
                bhaBitDTO.ID = col.IntUtilParse(hidden_serviceid.Value);
                bhaBitDTO.BearingSeals = txtBearingSeals.Text;
                bhaBitDTO.BearingType = txtBearingType.Text;
                bhaBitDTO.BHAID = radBHANumber.SelectedValue;
                bhaBitDTO.BitDesc = txtBitDesc.Text;
                bhaBitDTO.BitLength = col.DecimalUtilParse(txtBitLength.Text);
                bhaBitDTO.BitMfg = txtBitMfg.Text;
                bhaBitDTO.BitNumber = txtBitNumber.Text;
                bhaBitDTO.BitSno = txtBitSno.Text;
                bhaBitDTO.BittoDNSC = txtBittoDNSC.Text;
                bhaBitDTO.BittoGamma = txtBittoGamma.Text;
                bhaBitDTO.BittoGyro = txtBittoGyro.Text;
                bhaBitDTO.BittoPorosity = txtBittoPorosity.Text;
                bhaBitDTO.BittoResistivity = txtBittoResistivity.Text;
                bhaBitDTO.BittoSensor = txtBittoSensor.Text;
                bhaBitDTO.BitType = txtBitType.Text;
                bhaBitDTO.Connection = txtConnection.Text;
                bhaBitDTO.CreateDate = DateTime.Now;
                bhaBitDTO.DullChar = txtDullChar.Text;
                bhaBitDTO.Guage = txtGuage.Text;
                bhaBitDTO.InnerRow = txtInnerRow.Text;
                bhaBitDTO.isActive = true;
                bhaBitDTO.LastModifyDate = DateTime.Now;
                bhaBitDTO.Location = txtLocation.Text;
                bhaBitDTO.NUMJETS = txtNUMJETS.Text;
                bhaBitDTO.ODFrac = txtODFrac.Text;
                bhaBitDTO.OtherDullChar = txtOtherDullChar.Text;
                bhaBitDTO.OuterRow = txtOuterRow.Text;
                bhaBitDTO.ReasonPulled = txtReasonPulled.Text;
                string jetvalues = hdnvalue.Value;
                string[] arr = jetvalues.Split(',');
                bhaBitDTO.Jet1 = col.DecimalUtilParse(arr[0]);
                bhaBitDTO.Jet2 = col.DecimalUtilParse(arr[1]);
                bhaBitDTO.Jet3 = col.DecimalUtilParse(arr[2]);
                bhaBitDTO.Jet4 = col.DecimalUtilParse(arr[3]);
                bhaBitDTO.Jet5 = col.DecimalUtilParse(arr[4]);
                bhaBitDTO.Jet6 = col.DecimalUtilParse(arr[5]);
                bhaBitDTO.Jet7 = col.DecimalUtilParse(arr[6]);
                bhaBitDTO.Jet8 = col.DecimalUtilParse(arr[7]);
                bhaBitDTO.Jet9 = col.DecimalUtilParse(arr[8]);
                bhaBitDTO.Jet10 = col.DecimalUtilParse(arr[9]);

                int insertTargetDetailsID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHABitData(bhaBitDTO);
                //string queryUpdate = "Update [RigTrack].[tblCreateToolManufacturer] set Manufacturer='" + txt_servicename.Text + "'" +
                //    "  where Id=" + hidden_serviceid.Value + "";

                //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "BHA Bit Details Updated Successfully";
                lbl_message.ForeColor = Color.Green;
                
            }
            catch (Exception ex) { }
        }
        clearItems();
    }
    protected void RadGridBITInfo_ItemCommand(object sender, GridCommandEventArgs e)
    {
        // Did the user click the "Edit" button?
        if (e.CommandName == "EditMeter")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("ID"));
            // Select the item
            item.Selected = true;
            btnSaveBitData.Text = "Update";
            hidden_serviceid.Value = dataKeyValue;
            BindDataToEdit(SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM [RigTrack].[tblBHABitData] bit,[RigTrack].[tblBHADataInfo] bha where bha.ID=bit.BHAID and bit.ID=" + dataKeyValue + "").Tables[0]);
        }
    }
    public void BindDataToEdit(DataTable dt)
    {
        ddlCompany.SelectedValue= dt.Rows[0]["CompanyID"].ToString();
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
        txtBitSno.Text = dt.Rows[0]["BitSno"].ToString();
        txtBitSno.Enabled = false;
        txtBitDesc.Text = dt.Rows[0]["BitDesc"].ToString();
        txtODFrac.Text = dt.Rows[0]["ODFrac"].ToString();
        txtBitLength.Text = dt.Rows[0]["BitLength"].ToString();
        txtConnection.Text = dt.Rows[0]["Connection"].ToString();
        txtBitType.Text = dt.Rows[0]["BitType"].ToString();
        txtBearingType.Text = dt.Rows[0]["BearingType"].ToString();
        txtBitMfg.Text = dt.Rows[0]["BitMfg"].ToString();
        txtBitNumber.Text = dt.Rows[0]["BitNumber"].ToString();
        txtReasonPulled.Text = dt.Rows[0]["ReasonPulled"].ToString();
        txtInnerRow.Text = dt.Rows[0]["InnerRow"].ToString();
        txtOuterRow.Text = dt.Rows[0]["OuterRow"].ToString();
        txtDullChar.Text = dt.Rows[0]["DullChar"].ToString();
        txtLocation.Text = dt.Rows[0]["Location"].ToString();
        txtBearingSeals.Text = dt.Rows[0]["BearingSeals"].ToString();
        txtGuage.Text = dt.Rows[0]["Guage"].ToString();
        txtOtherDullChar.Text = dt.Rows[0]["OtherDullChar"].ToString();
        txtNUMJETS.Text = dt.Rows[0]["NUMJETS"].ToString();
        txtBittoSensor.Text = dt.Rows[0]["BittoSensor"].ToString();
        txtBittoGamma.Text = dt.Rows[0]["BittoGamma"].ToString();
        txtBittoResistivity.Text = dt.Rows[0]["BittoResistivity"].ToString();
        txtBittoPorosity.Text = dt.Rows[0]["BittoPorosity"].ToString();
        txtBittoDNSC.Text = dt.Rows[0]["BittoDNSC"].ToString();
        txtBittoGyro.Text = dt.Rows[0]["BittoGyro"].ToString();
    }
    public void clearItems()
    {
        radBHANumber.SelectedValue = "0";
        radBHANumber.Enabled = true;
        txtBitSno.Text = "";
        txtBitDesc.Text = "";
        txtODFrac.Text = "";
        txtBitLength.Text = "";
        txtConnection.Text = "";
        txtBitType.Text = "";
        txtBearingType.Text = "";
        txtBitMfg.Text = "";
        txtBitNumber.Text = "";
        txtReasonPulled.Text = "";
        txtInnerRow.Text = "";
        txtOuterRow.Text = "";
        txtDullChar.Text = "";
        txtLocation.Text = "";
        txtBearingSeals.Text = "";
        txtGuage.Text = "";
        txtOtherDullChar.Text = "";
        txtNUMJETS.Text = "";
        txtBittoSensor.Text = "";
        txtBittoGamma.Text = "";
        txtBittoResistivity.Text = "";
        txtBittoPorosity.Text = "";
        txtBittoDNSC.Text = "";
        txtBittoGyro.Text = "";
        btnSaveBitData.Text = "Create";
        RadGridBITInfo.Rebind();
        //lbl_message.Text = "";
    }
}