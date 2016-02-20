using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ManageBHAInfo : System.Web.UI.Page
{
    MDM.Collector col = new MDM.Collector();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BHAbindTypes();
            //ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            //ddlCurveGroup.DataTextField = "CurveGroupName";
            //ddlCurveGroup.DataValueField = "ID";
            //ddlCurveGroup.DataBind();
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();

            ddlsearchcompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlsearchcompany.DataTextField = "Name";
            ddlsearchcompany.DataValueField = "ID";
            ddlsearchcompany.DataBind();
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
    protected void ddlsearchcompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlsearchcompany.SelectedValue != "0")
        {

            combo_job.Items.Clear();
            combo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlsearchcompany.SelectedValue));
            combo_job.DataSource = dtJobDetails;
            combo_job.DataTextField = "CurveGroupName";
            combo_job.DataValueField = "ID";
            combo_job.DataBind();

        }
        else
        {


        }

    }
    public void BHAbindTypes()
    {
        DataTable dt_getassetnamesbycat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT [ID],[BHAType] from tblBHAType").Tables[0];
        RadComboBoxFill.FillRadcombobox(comboBHAType, dt_getassetnamesbycat, "BHAType", "ID", "0");
    }
    protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {
        comboBHAType.Items.Clear();
        BHAbindTypes();
    }
    protected void btnSaveAssetName_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.BHAInfoDTO bhaDTO = new RigTrack.DatabaseTransferObjects.BHAInfoDTO();
        bhaDTO.ID = 0;
        bhaDTO.JOBID=col.IntUtilParse(ddlCurveGroup.SelectedValue);
	    bhaDTO.BHANumber=txtBHANumber.Text;
	    bhaDTO.BHADesc=txtBHADesc.Text;
	    bhaDTO.BHAType=col.IntUtilParse(comboBHAType.SelectedValue);
	    bhaDTO.CreateDate=DateTime.Now;
        bhaDTO.LastModifyDate = DateTime.Now;
	    bhaDTO.isActive=true;
        int insertTargetDetailsID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateBHAInfoDetails(bhaDTO);
        lbl_message.Text = "BHA Info Created Successfully";
        lbl_message.ForeColor = Color.Green;
        clearAll();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearAll();
        lbl_message.Text = "";
    }
    public void clearAll()
    {
        ddlCurveGroup.SelectedValue = "0" ;
         txtBHANumber.Text="";
         txtBHADesc.Text="";
         comboBHAType.SelectedValue="0";
         
    }
    protected void RadGridBHAInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {

        RadGridBHAInfo.DataSource = bindBHADetails();
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        RadGridBHAInfo.DataSource = bindBHADetails();
        RadGridBHAInfo.Rebind();
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
        string query = "select B.ID as BHAID,J.CurveGroupName as JobName,[BHANumber],B.[BHADesc],BT.[BHAType] as BHATypeName,B.BHAType as BHATypeID,B.[CreateDate] as BHACreatedDate" +
" from [RigTrack].[tblBHADataInfo] B,[RigTrack].[tblCurveGroup] J,tblBHAType BT where B.JOBID=J.ID and B.BHAType=BT.ID";
        if (ddlCompany.SelectedValue != "0" && ids != "")
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
                
                bhaDTO.LastModifyDate = DateTime.Now;
                bhaDTO.isActive = true;

                string query = "UPDATE [RigTrack].[tblBHADataInfo] SET BHADesc = '" + bhaDTO.BHADesc + "',"+
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
            //GridEditFormItem item = (GridEditFormItem)e.Item;
           RadComboBox combo = (RadComboBox)item.FindControl("comboBHAType");
           
           Label txt = (Label)item.FindControl("lblBHATypeEdit");
           combo.SelectedValue = txt.Text;
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