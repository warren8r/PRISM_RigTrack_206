using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ViewEditBHAInfo : System.Web.UI.Page
{
    MDM.Collector col = new MDM.Collector();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //combo_job.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            //combo_job.DataTextField = "CurveGroupName";
            //combo_job.DataValueField = "ID";
            //combo_job.DataBind();
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            combo_job.Items.Clear();
            combo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            combo_job.DataSource = dtJobDetails;
            combo_job.DataTextField = "CurveGroupName";
            combo_job.DataValueField = "ID";
            combo_job.DataBind();
            RadGridBHAInfo.DataSource = bindBHADetails();
            RadGridBHAInfo.Rebind();
        }
        else
        {


        }

    }
    protected void RadGridBHAInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {

        RadGridBHAInfo.DataSource = bindBHADetails();
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
            string query = "select B.ID as BHAID,J.CurveGroupName+' '+Convert(varchar,J.ID) as JobName,[BHANumber],[BHADesc],[BHAType],[BitSno],[BitDesc],[ODFrac]" +
",[BitLength],[Connection],[BitType],[BearingType],[BitMfg],[BitNumber],[NUMJETS],[InnerRow],[OuterRow],[DullChar],[Location],[BearingSeals],[Guage]" +
",[OtherDullChar],[ReasonPulled],[MotorDesc],[MotorMFG],[NBStabilizer],[Model],[Revolutions],[Bend],[RotorJet]" +
",[BittoBend],[PropBUR],[RealBUR],[PadOD],[AverageDifferential],[Lobes],[OffBottomDifference],[Stages],[StallPressure]" +
",[BittoSensor],[BittoGamma],[BittoResistivity],[BittoPorosity],[BittoDNSC],[BittoGyro],B.[CreateDate] as BHACreatedDate" +
" from [RigTrack].[tblBHADataInfo] B,[RigTrack].[tblCurveGroup] J where B.JOBID=J.ID";
        if (ddlCompany.SelectedValue != "0" && ids!="")
            query += " and  J.ID in (" + ids + ")";

        if (combo_job.SelectedValue != "0")
            query += " and  J.ID=" + combo_job.SelectedValue + "";



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
    protected void btn_view_Click(object sender, EventArgs e)
    {
        RadGridBHAInfo.DataSource = bindBHADetails();
        RadGridBHAInfo.Rebind();
    }
    protected void RadGridBHAInfo_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.BHAInfoDTO bhaDTO = new RigTrack.DatabaseTransferObjects.BHAInfoDTO();

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            TextBox txtbx = (TextBox)item.FindControl("BHADesc");
            try
            {
                bhaDTO.ID = col.IntUtilParse((item["BHAID"].Controls[0] as TextBox).Text);
                ////bhaDTO.JOBID = col.IntUtilParse(ddlCurveGroup.SelectedValue);
                ////bhaDTO.BHANumber = txtBHANumber.Text;
                bhaDTO.BHADesc = (item["BHADesc"].Controls[0] as TextBox).Text;
                ////bhaDTO.BHAType = col.IntUtilParse((item["BHAType"].Controls[0] as TextBox).Text);
                bhaDTO.BitSno = (item["BitSno"].Controls[0] as TextBox).Text;
                bhaDTO.BitDesc = (item["BitDesc"].Controls[0] as TextBox).Text;
                bhaDTO.ODFrac = (item["ODFrac"].Controls[0] as TextBox).Text;
                bhaDTO.BitLength = col.DecimalUtilParse((item["BitLength"].Controls[0] as TextBox).Text);
                bhaDTO.Connection = (item["Connection"].Controls[0] as TextBox).Text;
                bhaDTO.BitType = (item["BitType"].Controls[0] as TextBox).Text;
                bhaDTO.BearingType = (item["BearingType"].Controls[0] as TextBox).Text;
                bhaDTO.BitMfg = (item["BitMfg"].Controls[0] as TextBox).Text;
                bhaDTO.BitNumber = (item["BitNumber"].Controls[0] as TextBox).Text;
                bhaDTO.NUMJETS = (item["NUMJETS"].Controls[0] as TextBox).Text;
                bhaDTO.InnerRow = (item["InnerRow"].Controls[0] as TextBox).Text;
                bhaDTO.OuterRow = (item["OuterRow"].Controls[0] as TextBox).Text;
                bhaDTO.DullChar = (item["DullChar"].Controls[0] as TextBox).Text;
                bhaDTO.Location = (item["Location"].Controls[0] as TextBox).Text;
                bhaDTO.BearingSeals = (item["BearingSeals"].Controls[0] as TextBox).Text;
                bhaDTO.Guage = (item["Guage"].Controls[0] as TextBox).Text;
                bhaDTO.OtherDullChar = (item["OtherDullChar"].Controls[0] as TextBox).Text;
                bhaDTO.ReasonPulled = (item["ReasonPulled"].Controls[0] as TextBox).Text;
                bhaDTO.MotorDesc = (item["MotorDesc"].Controls[0] as TextBox).Text;
                bhaDTO.MotorMFG = (item["MotorMFG"].Controls[0] as TextBox).Text;
                bhaDTO.NBStabilizer = (item["NBStabilizer"].Controls[0] as TextBox).Text;
                bhaDTO.Model = (item["Model"].Controls[0] as TextBox).Text;
                bhaDTO.Revolutions = (item["Revolutions"].Controls[0] as TextBox).Text;
                bhaDTO.Bend = (item["Bend"].Controls[0] as TextBox).Text;
                bhaDTO.RotorJet = (item["RotorJet"].Controls[0] as TextBox).Text;
                bhaDTO.BittoBend = (item["BittoBend"].Controls[0] as TextBox).Text;
                //bhaDTO.PropBUR = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.RealBUR = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.PadOD = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.AverageDifferential = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.Lobes = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.OffBottomDifference = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.Stages = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.StallPressure = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.BittoSensor = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.BittoGamma = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.BittoResistivity = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.BittoPorosity = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.BittoDNSC = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.BittoGyro = (item["ID"].Controls[0] as TextBox).Text;
                //bhaDTO.CreateDate = DateTime.Now;
                bhaDTO.LastModifyDate = DateTime.Now;
                bhaDTO.isActive = true;

                string query = "UPDATE [RigTrack].[tblBHADataInfo] SET BHADesc = '"+bhaDTO.BHADesc+"',BitSno = '"+ bhaDTO.BitSno + "',BitDesc = '" + bhaDTO.BitDesc + "'," +
    "ODFrac = '" + bhaDTO.ODFrac + "',BitLength = '" + bhaDTO.BitLength + "',Connection = '" + bhaDTO.Connection + "',BitType = '" + bhaDTO.BitType + "',BearingType = '" + bhaDTO.BearingType + "',BitMfg = '" + bhaDTO.BitMfg + "'," +
    "BitNumber = '" + bhaDTO.BitNumber + "',NUMJETS = '" + bhaDTO.NUMJETS + "',InnerRow = '" + bhaDTO.InnerRow + "' ,OuterRow = '" + bhaDTO.OuterRow + "',DullChar = '" + bhaDTO.DullChar + "',Location ='" + bhaDTO.Location + "'," +
    "BearingSeals = '" + bhaDTO.BearingSeals + "',Guage ='" + bhaDTO.Guage + "',OtherDullChar = '" + bhaDTO.OtherDullChar + "',ReasonPulled = '" + bhaDTO.ReasonPulled + "' ,MotorDesc = '" + bhaDTO.MotorDesc + "',MotorMFG = '" + bhaDTO.MotorMFG + "' ," +
    "NBStabilizer = '" + bhaDTO.NBStabilizer + "',Model = '" + bhaDTO.Model + "',Revolutions = '" + bhaDTO.Revolutions + "',Bend = '" + bhaDTO.Bend + "',RotorJet = '" + bhaDTO.RotorJet + "',BittoBend = '" + bhaDTO.BittoBend + "'," +
    "LastModifyDate = getDate(),isActive = '" + bhaDTO.isActive + "' WHERE [ID] = " + bhaDTO.ID + "";
                //SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query);
                //int insertTargetDetailsID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHAInfoDetails(bhaDTO);
            }
            catch (Exception ex)
            {
            }
            //lbl_message.Text = "BHA Info Created Successfully";
            //lbl_message.ForeColor = Color.Green;
        }
    }
    protected void RadGridBHAInfo_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            //string curveGroupName = ViewState["CurveGroupName"].ToString();
            //curveGroupName = curveGroupName.Substring(curveGroupName.IndexOf('-') + 2);
            //string targetName = ViewState["TargetName"].ToString();
            //targetName = targetName.Substring(targetName.IndexOf('-') + 2);

            RadGridBHAInfo.ExportSettings.FileName = "BHA Details " + DateTime.Now.Date.ToString("MM/dd/yyyy");
        }
        
    }
    protected void RadGridBHAInfo_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            //RadDropDownList ddl = item.FindControl("ddlCurveType") as RadDropDownList;
            //DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveTypes();
            //DataView dv = dt.DefaultView;
            //dv.Sort = "ID desc";
            //DataTable sortedDT = dv.ToTable();
            //ddl.DataSource = sortedDT;
            //ddl.DataTextField = "Name";
            //ddl.DataValueField = "ID";
            //ddl.DataBind();
            //ddl.SelectedText = (item["CurveTypeName"].Controls[0] as TextBox).Text;
        }

        if (e.Item is GridDataItem && !e.Item.IsInEditMode)
        {
            GridDataItem item = (GridDataItem)e.Item;

            //Label lbl = (Label)item.FindControl("lblCurveType");

            //if (lbl.Text == "-Select-")
            //{
            //    lbl.Text = "";
            //}
        }
    }
}