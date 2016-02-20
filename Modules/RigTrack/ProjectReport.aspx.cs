using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class Modules_Configuration_Manager_ProjectReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            RadButton1.Visible = false;
            RadGrid1.Visible = false;
            radcombo_job.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            radcombo_job.DataTextField = "CurveGroupName";
            radcombo_job.DataValueField = "ID";
            radcombo_job.DataBind();

        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            radcombo_job.Items.Clear();
            //radcombo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                 "select * from [RigTrack].[tblCurveGroup] where isActive=1 and ID in (select jobid from PrismJobAssignedAssets)").Tables[0];
            RadComboBoxFill.FillRadcomboboxSelectALL(radcombo_job, dt, "CurveGroupName", "ID", "0");
            //DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            //radcombo_job.DataSource = dtJobDetails;
            //radcombo_job.DataTextField = "CurveGroupName";
            //radcombo_job.DataValueField = "ID";
            //radcombo_job.DataBind();

        }
        else
        {


        }

    }
    protected void radcombo_job_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //if (radcombo_job.SelectedValue != "0")
        //{

        //    string query = "";
        //    if (radcombo_job.SelectedValue == "0")
        //        query = "select * from PrismJobRunDetails order by Date desc";
        //    else
        //        query = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " order by Date desc";
        //    DataTable dt_getresult = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        //    if (dt_getresult.Rows.Count > 0)
        //    {
        //        radtxt_start.SelectedDate = Convert.ToDateTime(dt_getresult.Rows[0]["Date"].ToString());
        //    }


        //}
        //else
        //{


        //}

    }
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        RadButton1.Visible = true;
        RadGrid1.Visible = true;
        bindgrid();

    }

    //protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    //{
    //    if (e.CommandName == RadGrid.ExportToExcelCommandName)
    //    {
    //        RadGrid1.ExportSettings.ExportOnlyData = true;
    //        RadGrid1.ExportSettings.HideStructureColumns = true;
    //        //RadGrid1.ExportSettings.
    //        RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
    //        RadGrid1.MasterTableView.ExportToExcel();
    //    }
    //}
    protected void exl_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.HideStructureColumns = true;
        //RadGrid1.ExportSettings.
        RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
        RadGrid1.MasterTableView.ExportToExcel();
        RadButton1.Visible = true;
        RadGrid1.Visible = true;
    }
    public void bindgrid()
    {
        //string str_querystring = radcombo_job.SelectedValue.ToString();
        //string strjname = str_querystring.Substring(0, str_querystring.LastIndexOf('-') - 1);
        string query = "";
        if (radcombo_job.SelectedValue == "0")
        {
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            if (dtJobDetails.Rows.Count > 0)
            {
                string ids = "";
                for (int i = 0; i < dtJobDetails.Rows.Count; i++)
                {
                    ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
                }
                if (ids != "")
                {
                    ids = ids.Remove(ids.Length - 1, 1);
                    query = "select b.ID as JOBID,* from PrismJobRunDetails a,[RigTrack].[tblCurveGroup] b where a.jid=b.ID and jid in(" + ids + ") order by Date desc";
                }
            }
            else
            {
                query = "select b.ID as JOBID,* from PrismJobRunDetails a,[RigTrack].[tblCurveGroup] b where a.jid=b.ID order by Date desc";
            }
        }
        else
            query = "select b.ID as JOBID,* from PrismJobRunDetails a,[RigTrack].[tblCurveGroup] b where a.jid=b.ID and jid=" + radcombo_job.SelectedValue + " order by Date desc";

        
        //query = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM/dd/yyyy")+ "' order by Date desc";
        DataTable dt_getresult = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        RadGrid1.DataSource = dt_getresult;
        RadGrid1.DataBind();
        //DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //    "select * from [RigTrack].[tblCurveGroup] where " +
        //    "  ID=" + radcombo_job.SelectedValue + "").Tables[0];
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select state.Name as StateName,country.Name as CountryName,* from [RigTrack].tblCurveGroup m,[RigTrack].[tlkpState] state,[RigTrack].[tlkpCountry] country where state.ID=m.StateID and country.ID=m.CountryID and " +
            " m.ID=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            //Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + radcombo_job.SelectedValue;
            td_jobdet.Attributes.Add("style", "display:block");
            lbl_jname.Text = dtdates.Rows[0]["CurveGroupName"].ToString();
            //lbl_jtype.Text = "";// dtdates.Rows[0]["jtypename"].ToString();
            lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["JobStartDate"].ToString()).ToString("MM/dd/yyyy");
            if (dtdates.Rows[0]["JobEndDate"] != null && dtdates.Rows[0]["JobEndDate"].ToString() != "")
            {
                lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["JobEndDate"].ToString()).ToString("MM/dd/yyyy");
            }
            //lbl_rigtype.Text = dtdates.Rows[0]["rigtypename"].ToString();
            //lbl_rigtype.Text = dtdates.Rows[0]["RigName"].ToString();
            lbl_address.Text = dtdates.Rows[0]["JobLocation"].ToString();
            lbl_state.Text = dtdates.Rows[0]["StateName"].ToString();
            lbl_country.Text = dtdates.Rows[0]["CountryName"].ToString();
        }
            

    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            RadGrid radgridactivities = (RadGrid)item.FindControl("radgridactivities");
            RadGrid radgridAssetsRequired = (RadGrid)item.FindControl("radgridAssetsRequired");
            RadGrid griddocuments = (RadGrid)item.FindControl("griddocuments");
            Label lbl_runid = (Label)item.FindControl("lbl_runid");
            Label lbl_finished = (Label)item.FindControl("lbl_finished");
            Button btnViewDetails = (Button)item.FindControl("btnView");
            Label lblJOBID = (Label)item.FindControl("lblJOBID");
            btnViewDetails.Attributes.Add("onclick", "openWindowDetails(" + lblJOBID.Text + "," + lbl_runid.Text + ");return false;");
            //GridTableView tablegridactivities = (GridTableView)item.FindControl("tablegridactivities");
            if (lbl_finished.Text == "True")
            {
                lbl_finished.Text = "Yes";
            }
            else
            {
                lbl_finished.Text = "No";
            }
            string query = "select PHRA.[24HourActivity] as Activity, PHS.TIME AS STARTTIME,PHE.TIME AS ENDTIME" +
                             " from PrismJobRun24HourActivityLog PAL" +
                             " LEFT join Prism24Hours PHS on PAL.StartTime = PHS.TimeId" +
                             " LEFT JOIN Prism24Hours PHE ON PAL.EndTime = PHE.TIMEID " +
                             " LEFT JOIN Prism24HourActivity PHRA on PAL.[24HourActivity]=PHRA.HourActivityId" +
                             " where PAL.RunID=" + lbl_runid.Text + "  order by PAL.StartTime Desc";
            DataTable lastActivity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            radgridactivities.DataSource = lastActivity;
            radgridactivities.DataBind();


            //
            DataTable dt_getsupplyneeded = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select a.AssetName,b.Aqntty from PrismAssetName a,PrismJobRunAssetsRequired b where a.id=b.assetid and b.RunID=" + lbl_runid.Text + "").Tables[0];
            radgridAssetsRequired.DataSource = dt_getsupplyneeded;
            radgridAssetsRequired.DataBind();

            //


            string selectq = "SELECT  etod.runid,etod.DocumentID,d.DocumentDisplayName,d.DocumentName from " +
                " DailyRunReportDocs etod, documents d where d.DocumentID=etod.DocumentID and runid=" + lbl_runid.Text + "";// etod.Uploadeddate='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'";
            DataTable dt_binddocs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];

            griddocuments.DataSource = dt_binddocs;
            griddocuments.DataBind();



        }
    }
}