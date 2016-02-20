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
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from manageJobOrders where status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>'' and getdate() between startdate and enddate").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "jobname", "jid", "0");
            radcombo_job.SelectedIndex = 1;
            bindgrid();

        }
    }
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        if (radcombo_job.SelectedValue != "0")
        {
            RadGrid1.Visible = true;
            bindgrid();
        }

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
    }
    public void bindgrid()
    {
        //string str_querystring = radcombo_job.SelectedValue.ToString();
        //string strjname = str_querystring.Substring(0, str_querystring.LastIndexOf('-') - 1);
        string query = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " order by Date desc";
        DataTable dt_getresult = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        RadGrid1.DataSource = dt_getresult;
        RadGrid1.DataBind();
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j,RigTypes r where m.jobtype=j.jobtypeid and " +
            " m.rigtypeid=r.rigtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            //Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + radcombo_job.SelectedValue;
            td_jobdet.Attributes.Add("style", "display:block");
            lbl_jname.Text = dtdates.Rows[0]["jobname"].ToString();
            lbl_jtype.Text = dtdates.Rows[0]["jtypename"].ToString();
            lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString()).ToString("MM/dd/yyyy");
            lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString()).ToString("MM/dd/yyyy");
            //lbl_rigtype.Text = dtdates.Rows[0]["rigtypename"].ToString();

            lbl_address.Text = dtdates.Rows[0]["primaryAddress1"].ToString() + " " + dtdates.Rows[0]["primaryAddress2"].ToString();
            lbl_city.Text = dtdates.Rows[0]["primaryCity"].ToString();
            lbl_state.Text = dtdates.Rows[0]["primaryState"].ToString();
            lbl_country.Text = dtdates.Rows[0]["primaryCountry"].ToString();
            lbl_zip.Text = dtdates.Rows[0]["primaryPostalCode"].ToString();
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
            //GridTableView tablegridactivities = (GridTableView)item.FindControl("tablegridactivities");
            if (lbl_finished.Text == "True")
            {
                lbl_finished.Text = "Yes";
            }
            else
            {
                lbl_finished.Text = "No";
            }
            string query = "select pa.[24HourActivity] as Activity,ph.Time as Timedet from PrismJobRun24HourActivityLog p,Prism24HourActivity pa,Prism24Hours ph where p.Time=ph.TimeId and p.[24HourActivity]=pa.HourActivityId and p.RunId=" + lbl_runid.Text + "  order by p.Time Desc";
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