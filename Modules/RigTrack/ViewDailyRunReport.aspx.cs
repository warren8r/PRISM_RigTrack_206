using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public partial class Modules_Configuration_Manager_ViewDailyRunReport : System.Web.UI.Page
{
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlCommand cmdInsert;
    SqlTransaction transaction;
    public static DataTable dt_Users, dt_rig, dt_mwdoperators;
    string tdstyle = "style='border:solid 1px #000000;text-align:center;background-color:#FFFFFF; color:#121212'";
    string tdfiledstyle = "style='border:solid 1px #000000;text-align:center;background-color:#C4D79B; color:#121212'";
    bool runIdExist = false;
    public static DataTable dt24hours, dt24hoursActivity;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            dt_Users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Users").Tables[0];
        dt_rig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from RigTypes").Tables[0];
        dt_mwdoperators = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId").Tables[0];
            if (Request.QueryString["jobID"] != null && Request.QueryString["jobID"] != "")
            {
                string jobID=Request.QueryString["jobID"].ToString();
                string jobrunID=Request.QueryString["runid"].ToString();
                DataTable dtJobDetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from [RigTrack].tblCurveGroup where isActive=1 and ID=" + jobID + "").Tables[0];
                DataTable dtRunDetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where runid=" + jobrunID + "").Tables[0];
                if (dtJobDetails.Rows.Count > 0)
                {
                    ddlCompany.SelectedValue = dtJobDetails.Rows[0]["CompanyID"].ToString();
                    DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                    "select * from [RigTrack].tblCurveGroup where isActive=1 and ID in (select jobid from PrismJobAssignedAssets) and CompanyID=" + Convert.ToInt32(dtJobDetails.Rows[0]["CompanyID"].ToString()) + "").Tables[0];
                    RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "CurveGroupName", "ID", "0");
                    radtxt_start.SelectedDate = Convert.ToDateTime(dtRunDetails.Rows[0]["Date"]);
                    radcombo_job.SelectedValue = jobID;
                    viewDetails(jobID, jobrunID);
                    lbl_runnumber.Text = dtRunDetails.Rows[0]["runnumber"].ToString();
                }

            }
            dt24hours = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT [TimeId], convert(varchar,[Time]) as [Time] FROM [Prism24Hours]").Tables[0];
            dt24hoursActivity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT [HourActivityId], [24HourActivity] FROM [Prism24HourActivity]").Tables[0];
        }
        
       
        lbl_message.Text = "";
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            radcombo_job.Items.Clear();
            radcombo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from [RigTrack].tblCurveGroup where isActive=1 and ID in (select jobid from PrismJobAssignedAssets) and CompanyID=" + Int32.Parse(ddlCompany.SelectedValue) + "").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "CurveGroupName", "ID", "0");
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
    protected void radcombo_job_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from [RigTrack].tblCurveGroup m where ID=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            radtxt_start.MinDate = Convert.ToDateTime(dtdates.Rows[0]["JobStartDate"].ToString());
            radtxt_start.MaxDate = DateTime.Now.AddDays(-1);//Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());
            radtxt_start.SelectedDate = DateTime.Now.AddDays(-1);
            //radtxt_start.SelectedDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());


            //radtxt_start.MinDate = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            //radtxt_start.MaxDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());

        }
    }
    public void viewDetails(string jobid, string runid)
    {
        clear();

        //lbl_message.Text = "";
        hidd_runno.Value = "0";
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select state.Name as StateName,country.Name as CountryName,cmp.CompanyName as CompanyName, * from [RigTrack].tblCurveGroup m,[RigTrack].[tlkpState] state,[RigTrack].[tlkpCountry] country,[RigTrack].[tblCompany] cmp where state.ID=m.StateID and country.ID=m.CountryID and " +
            " m.CompanyID=cmp.ID and m.ID=" + jobid + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + jobid;
            td_jobdet.Attributes.Add("style", "display:block");
            lbl_jname.Text = dtdates.Rows[0]["CurveGroupName"].ToString();
            lblCompany.Text = dtdates.Rows[0]["CompanyName"].ToString();
            //lbl_jtype.Text = dtdates.Rows[0]["jtypename"].ToString();
            lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["JobStartDate"].ToString()).ToString("MM/dd/yyyy");
            if (dtdates.Rows[0]["JobEndDate"] != null && dtdates.Rows[0]["JobEndDate"].ToString() != "")
            {
                lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["JobEndDate"].ToString()).ToString("MM/dd/yyyy");
            }
            lbl_rigtype.Text = dtdates.Rows[0]["RigName"].ToString();

            lbl_address.Text = dtdates.Rows[0]["JobLocation"].ToString();
            //lbl_city.Text = dtdates.Rows[0]["primaryCity"].ToString();
            lbl_state.Text = dtdates.Rows[0]["StateName"].ToString();
            lbl_country.Text = dtdates.Rows[0]["CountryName"].ToString();
            //lbl_zip.Text = dtdates.Rows[0]["primaryPostalCode"].ToString();
            string query_personals = " select u.userid, (firstname+lastname)+' ('+userRole+')' as Personname from users u, UserRoles ur,PrismJobAssignedPersonals pp" +
            " where  u.userRoleID=ur.userRoleID and pp.UserId=u.userID" +
            " and pp.JobId=" + jobid + "";


            //RadComboBoxFill.FillRadcombobox(combo_ex_personal, dt_personals, "Personname", "userid", "0");
            pnl_addjobs.Visible = true;
            td_dailylog.Visible = true;
            btn_save.Visible = false;
            btn_saveupdate2.Visible = false;
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + jobid + "").Tables[0];
            string existrunid = "";
            if (dt_getdailyrun.Rows.Count > 0)
            {
                txt_day.Text = dt_getdailyrun.Rows[0]["MwdHandDay"].ToString();
                txt_night.Text = dt_getdailyrun.Rows[0]["MwdHandNight"].ToString();
                runIdExist = true;
                existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();
                hidd_runno.Value = existrunid;
                // SqlDataSource1.SelectCommand = "SELECT [ActivityId], [RunID], [Time], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=" + existrunid;
                //lbl_runnumber.Text = dt_getdailyrun.Rows[0]["runnumber"].ToString();
                hid_runno.Value = dt_getdailyrun.Rows[0]["runnumber"].ToString();
                //lnk_download.Text = dt_getdailyrun.Rows[0]["attachmentname"].ToString();
                //string path = Server.MapPath("~/Documents/");
                //string docnamewithpath = path + dt_getdailyrun.Rows[0]["attachmentname"].ToString();
                //lnk_download.Attributes.Add("onclick", "return downloadFile('" + docnamewithpath + "','" + dt_getdailyrun.Rows[0]["attachmentname"].ToString() + "');");
                DataTable dt_getdaynumber = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                    "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + jobid + " and runid=" + runid + "").Tables[0];
                //DataRow[] dr_dayno = dt_getdailyrun.Select("runnumber=" + lbl_runnumber.Text + "");
                if (dt_getdaynumber.Rows.Count > 0)
                {
                    lbl_daynumber.Text = dt_getdaynumber.Rows.Count.ToString();
                }
                else
                {
                    lbl_daynumber.Text = "1";
                }
                //txt_dailycharges.Text = dt_getdailyrun.Rows[0]["dailycharges"].ToString();
                //if (dt_getdailyrun.Rows[0]["finished"].ToString() == "True")
                //{
                //    chk_runfinish.Checked = true;
                //}
                //chk_runfinish.
                bindFormInfo(runid);
                //activitylogbind(existrunid);
                //binddailyprogress(existrunid);
                //binddrillingparams(existrunid);
                //bindothersuppliesneeded(existrunid);
                //bindassetsneeded(existrunid);

            }
            else
            {
                bindFormInfo(runid);
                hidd_runno.Value = runid;
                //lbl_runnumber.Text = runid;
                DataTable dt_getdaynumber = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + jobid + " and runnumber=" + runid + "").Tables[0];
                //DataRow[] dr_dayno = dt_getdailyrun.Select("runnumber=" + lbl_runnumber.Text + "");
                if (dt_getdaynumber.Rows.Count > 0)
                {
                    lbl_daynumber.Text = (Convert.ToInt32(dt_getdaynumber.Rows.Count) + 1).ToString();
                }
                else
                {
                    lbl_daynumber.Text = "1";
                }

            }
            DataTable dt_personals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_personals).Tables[0];
            combo_ex_personal.DataSource = dt_personals;
            combo_ex_personal.DataBind();
            if (existrunid != "")
            {
                DataTable dt_existpersonals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunPersonals where runid=" + existrunid + "").Tables[0];
                for (int p = 0; p < dt_existpersonals.Rows.Count; p++)
                {
                    foreach (RadComboBoxItem item in combo_ex_personal.Items)
                    {
                        if (item.Value == dt_existpersonals.Rows[p]["personid"].ToString())
                        {
                            item.Checked = true;
                        }
                    }
                }
            }
            bindtextboxesdates(jobid, runid);
            binddocuments();
            radgrdMeterList.Visible = true;
            radgrdMeterList.DataSource = RefreshMeterList();
            radgrdMeterList.DataBind();
            //
        }
    }
    public void bindFormInfo(string runid)
    {
        DataTable dt_PrismJobRunDetailsFormInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrismJobRunDetailsFormInfo").Tables[0];
        DataRow[] row_form = dt_PrismJobRunDetailsFormInfo.Select("Jobid=" + radcombo_job.SelectedValue + " and RunId=" + runid + "");
        if (row_form.Length > 0)
        {
            txt_companyrep.Text = row_form[0]["CompanyRep"].ToString();
            txt_poppet.Text = row_form[0]["POPPET_SIZE"].ToString();
            txt_orifice.Text = row_form[0]["ORIFICE_SIZE"].ToString();
            txt_pulsewidth.Text = row_form[0]["PULSE_WIDTH"].ToString();
            txt_pulseamplitude.Text = row_form[0]["PULSE_AMPLITUDE"].ToString();
            txt_totalconnected.Text = row_form[0]["TOTAL_CONNECTED"].ToString();
            txt_totalcirc.Text = row_form[0]["TOTAL_CIRC"].ToString();
            txt_depthstart.Text = row_form[0]["DEPTH_START"].ToString();
            txt_depthend1.Text = row_form[0]["DEPTH_END"].ToString();
            txt_incstart.Text = row_form[0]["INCStart"].ToString();
            txt_azmstart.Text = row_form[0]["AZMStart"].ToString();
            txt_magfstart.Text = row_form[0]["MAGFStart"].ToString();
            txt_gravstart.Text = row_form[0]["GRAVStart"].ToString();
            txt_dipstart.Text = row_form[0]["DIPStart"].ToString();

            txt_incend.Text = row_form[0]["INCEnd"].ToString();
            txt_azmend.Text = row_form[0]["AZMEnd"].ToString();
            txt_magfend.Text = row_form[0]["MAGFEnd"].ToString();
            txt_gravend.Text = row_form[0]["GRAVEnd"].ToString();
            txt_dipend.Text = row_form[0]["DIPEnd"].ToString();
            txt_temp_c.Text = row_form[0]["TEMPERATURE_C"].ToString();
            txt_temp_f.Text = row_form[0]["TEMPERATURE_F"].ToString();
            txt_avr_pump_tressure.Text = row_form[0]["AVER_PUMP_PRESSURE"].ToString();
            txt_avre_flow_rate.Text = row_form[0]["AVER_FLOW_RATE"].ToString();
            txt_mud_weight.Text = row_form[0]["MUD_WEIGHT"].ToString();
            txt_solids.Text = row_form[0]["SOLIDS"].ToString();
            txt_sand1.Text = row_form[0]["SAND"].ToString();
        }
    }
    protected void grid_reAsset_ItemCommand(object sender, GridCommandEventArgs e)
    {
        //table_top1.Visible = true;
        pnl_addjobs.Visible = true;
        grid_hours.Visible = true;
        if (e.CommandName == "PerformInsert")
        {
            var gridDataItem = e.Item as GridDataItem;
            // DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            //  string existrunid = "";
            if (dt_getdailyrun.Rows.Count == 0)
            {
                //if (runIdExist == false)
                //{

                if (chk_runfinish.Checked)
                {
                    DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                    if (dt_getrunidnextval.Rows.Count > 0)
                    {
                        string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, addvalueexist);
                    }
                }

                string runnumber = "", runstatus = "";
                int addrunval = 1;
                string selectq = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date in (select MAX(Date) from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date = '" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "')";
                DataTable dt_getruncnt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
                if (dt_getruncnt.Rows.Count > 0)
                {
                    runnumber = dt_getruncnt.Rows[0]["runnumber"].ToString();
                    runstatus = dt_getruncnt.Rows[0]["finished"].ToString();
                    if (runstatus == "True")
                    {
                        addrunval = Convert.ToInt32(runnumber) + 1;
                    }
                    else
                    {
                        addrunval = Convert.ToInt32(runnumber);
                    }
                }
                //string path = Server.MapPath("~/Documents/");
                //string filename = "", completepath = "";

                string insertquery_jobrundet = "insert into PrismJobRunDetails(jid,Date,runnumber,updateddate)values(" +
                "" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(radtxt_start.SelectedDate.ToString()) + "'," +
                "" + addrunval + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";

                int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertquery_jobrundet);
                DataTable dt_runid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    PrismJobRunDetails WHERE  runid = IDENT_CURRENT('PrismJobRunDetails')").Tables[0];

                string runid = dt_runid.Rows[0]["runid"].ToString();
                hidd_runno.Value = runid;
                //hid_runid.Value = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");
                // grid_hours.Rebind();
            }


        }
        btn_view_Click(null, null);
    }
    protected void grid_hours_ItemCommand(object sender, GridCommandEventArgs e)
    {
        // table_top1.Visible = true;
        pnl_addjobs.Visible = true;
        grid_hours.Visible = true;
        if (e.CommandName == "PerformInsert")
        {
            var gridDataItem = e.Item as GridDataItem;
            // DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            //  string existrunid = "";
            if (dt_getdailyrun.Rows.Count == 0)
            {
                //if (runIdExist == false)
                //{

                if (chk_runfinish.Checked)
                {
                    DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                    if (dt_getrunidnextval.Rows.Count > 0)
                    {
                        string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, addvalueexist);
                    }
                }

                string runnumber = "", runstatus = "";
                int addrunval = 1;
                string selectq = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date in (select MAX(Date) from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date  <= '" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "')";
                DataTable dt_getruncnt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
                if (dt_getruncnt.Rows.Count > 0)
                {
                    runnumber = dt_getruncnt.Rows[0]["runnumber"].ToString();
                    runstatus = dt_getruncnt.Rows[0]["finished"].ToString();
                    if (runstatus == "True")
                    {
                        addrunval = Convert.ToInt32(runnumber) + 1;
                    }
                    else
                    {
                        addrunval = Convert.ToInt32(runnumber);
                    }
                }
                //string path = Server.MapPath("~/Documents/");
                //string filename = "", completepath = "";

                string insertquery_jobrundet = "insert into PrismJobRunDetails(jid,Date,runnumber,updateddate)values(" +
                "" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(radtxt_start.SelectedDate.ToString()) + "'," +
                "" + addrunval + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";

                int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertquery_jobrundet);
                DataTable dt_runid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    PrismJobRunDetails WHERE  runid = IDENT_CURRENT('PrismJobRunDetails')").Tables[0];

                string runid = dt_runid.Rows[0]["runid"].ToString();
                hidd_runno.Value = runid;
                //hid_runid.Value = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");
                // grid_hours.Rebind();
            }


        }
        btn_view_Click(null, null);
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        
    }
    protected void radgrdMeterList_CustomAggregate(object sender, GridCustomAggregateEventArgs e)
    {
        if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "Length")
        {

            Double rooms = 0;
            Double revenue = 0;
            foreach (GridDataItem item in radgrdMeterList.MasterTableView.Items)
            {
                rooms += Convert.ToDouble(item["Length"].Text);
                //revenue += Convert.ToDouble(item["revenue"].Text);

            }
            e.Result =rooms;
        }
    }
    private DataTable RefreshMeterList()
    {
        string query = "select p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
             "Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.PinTop,p.PinBottom,p.Comments,p.Description from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
              " where p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID and P.AssetName=PPA.Id"+
              " and p.ID in(SELECT b.ToolID FROM [RigTrack].[tblBHADataInfo] a,[RigTrack].[tblBHADataItemsInfo] b where a.ID=b.BHAID and a.JOBID="+radcombo_job.SelectedValue+")";
        query += " order by p.Id desc";
        
        DataTable dt_PrismJobRunDetailsFormInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        

        // Set the RADGrid's DataSource to the DataTable
        return dt_PrismJobRunDetailsFormInfo;

    }
    public void bindtextboxesdates(string jobid, string runid)
    {
        pnl_addjobs.Controls.Clear();

        DateTime dtstart = new DateTime();
        DateTime dtend = new DateTime();
        DateTime dtstart_service = new DateTime();
        DateTime dtend_service = new DateTime();
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from [RigTrack].tblCurveGroup where ID=" + radcombo_job.SelectedValue + "").Tables[0];
        DataTable dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select pa.AssetName AS AssetName1,* from Prism_Assets p,PrismAssetName pa where p.AssetName=pa.ID and P.id  in" +
            " (select AssetId from PrismJobAssignedAssets,[RigTrack].tblCurveGroup j where j.ID=PrismJobAssignedAssets.JobId and isActive=1 " +
            " and PrismJobAssignedAssets.JobId=" + radcombo_job.SelectedValue + ") ").Tables[0];
        DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,pa.Id as AID,pj.kitname as KitName, * from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca," +
            " PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.assignmentstatus='Active' and " +
            " pj.jobid=" + radcombo_job.SelectedValue + " ").Tables[0];
        //and pj.AssetStatus=3
        if (dtdates.Rows.Count > 0)
        {
            Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + radcombo_job.SelectedValue;
            //DataRow[] row_operator = dt_Users.Select("userID=" + dtdates.Rows[0]["opManagerId"].ToString());
            //DataRow[] row_rig = dt_rig.Select("rigtypeid=" + dtdates.Rows[0]["rigtypeid"].ToString());
            //lbl_operator.Text = row_operator[0]["firstname"].ToString() + " " + row_operator[0]["lastname"].ToString();
            lbl_location.Text = dtdates.Rows[0]["JobLocation"].ToString();
            lbl_rig.Text = dtdates.Rows[0]["RigName"].ToString();
            lbl_jobsno.Text = dtdates.Rows[0]["JobNumber"].ToString();
            lbl_well.Text = dtdates.Rows[0]["LeaseWell"].ToString();
            pnl_addjobs.Controls.Add(new LiteralControl("<table border='0' style='width:500px; vertical-align:top'>"));
            //pnl_addjobs.Controls.Add(new LiteralControl("<td style='width:200px'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>"));
            //pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff;'>Daily&#160;Charge&#40;&#36;&#41;</td>"));
            lbl_jname.Text = dtdates.Rows[0]["CurveGroupName"].ToString();
            //lbl_jtype.Text = dtdates.Rows[0]["jtypename"].ToString();
            lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["JobStartDate"].ToString()).ToString("MM/dd/yyyy");
            dtstart = Convert.ToDateTime(dtdates.Rows[0]["JobStartDate"].ToString());
            if (dtdates.Rows[0]["JobEndDate"] != null && dtdates.Rows[0]["JobEndDate"].ToString() != "")
            {
                lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["JobEndDate"].ToString()).ToString("MM/dd/yyyy");
                dtend = Convert.ToDateTime(dtdates.Rows[0]["JobEndDate"].ToString());
                dtend_service = Convert.ToDateTime(dtdates.Rows[0]["JobEndDate"].ToString());
            }

            dtstart_service = Convert.ToDateTime(dtdates.Rows[0]["JobStartDate"].ToString());
            
            TimeSpan ts = new TimeSpan();
            ts = dtend - dtstart;
            int days = ts.Days;
            lbl_date.Text = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM/dd/yyyy");
            //pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#C4D79B; color:#121212'>"));
            //string datecol = dtstart_service.ToString("MM/dd/yyyy");
            //pnl_addjobs.Controls.Add(new LiteralControl(Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM/dd/yyyy")));
            //pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
            //pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td><table>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Serial Number</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Tool&#160;Category</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Tool&#160;Name</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Tool Group&#160;Name</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">In&#160;Use</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Previous Tools Hours</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Job&#160;Run&#160;Hrs</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Cumilative Tools Hours</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Daily Run Hours</td>"));

            //pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Daily RUN Hrs</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            Double tot = 0.00;
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            string existrunid = "";
            existrunid = runid;
            
            for (int cat = 0; cat < dt_getassetcategories.Rows.Count; cat++)
            {
                CheckBox chk_inuse = new CheckBox();

                Label lbl_prevtoolhrs = new Label();
                TextBox txt_totrunhrs = new TextBox();
                TextBox txt_cumulative = new TextBox();
                TextBox txt_dailyrunhrs = new TextBox();
                txt_totrunhrs.ReadOnly = true;
                txt_cumulative.ReadOnly = true;
                txt_dailyrunhrs.ID = "txt_dailyrunhrs" + cat;
                txt_dailyrunhrs.Width = 70;
                string query_runhrdet = "";
                if (existrunid != "")
                {
                    DataTable dt_getdetfrm = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["AID"].ToString() + " and runid=" + existrunid + "").Tables[0];
                    if (dt_getdetfrm.Rows.Count > 0)
                    {
                        if (dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString() != "")
                        {
                            chk_inuse.Checked = true;
                            txt_dailyrunhrs.Text = dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString();
                        }

                    }

                }
                else
                {
                    //query_runhrdet = "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "";
                }


                DataTable dt_gettotalrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(dailyrunhrs) as TotalRunHrs from PrismJobRunHourdetails pjr,PrismJobRunDetails r where r.runid=pjr.runid and pjr.assetid=" + dt_getassetcategories.Rows[cat]["AID"].ToString() + " and r.Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
                if (dt_gettotalrunhrs.Rows.Count > 0)
                {
                    txt_totrunhrs.Text = dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString();
                    decimal d = 0;
                    if (dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString() != "")
                    {
                        if (dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString() != "")
                        {
                            d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString()) + Convert.ToDecimal(dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString());
                        }
                        else
                        {
                            d = Convert.ToDecimal(dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString());
                        }
                    }
                    else
                    {
                        if (dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString() != "")
                        {
                            d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString());
                        }
                    }
                    txt_cumulative.Text = d.ToString();
                    //string existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

                }
                lbl_prevtoolhrs.ID = "lbl_prevtoolshr" + cat;
                lbl_prevtoolhrs.Text = dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString();
                txt_cumulative.Width = 70;
                txt_totrunhrs.ID = "txt_totrunhrs" + cat;
                txt_totrunhrs.Width = 70;
                txt_cumulative.ID = "txt_cumulativehrs" + cat;
                chk_inuse.ID = "chk_" + cat;
                pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' " + tdfiledstyle + ">" + dt_getassetcategories.Rows[cat]["SerialNumber"].ToString() + "</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' " + tdfiledstyle + ">" + dt_getassetcategories.Rows[cat]["clientAssetName"].ToString() + "</td>"));

                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' " + tdfiledstyle + ">" + dt_getassetcategories.Rows[cat]["NameofAsset"].ToString() + "</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' " + tdfiledstyle + ">" + dt_getassetcategories.Rows[cat]["KitName"].ToString() + "</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='border:solid 1px #000000'>"));
                pnl_addjobs.Controls.Add(chk_inuse);
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='border:solid 1px #000000'>"));
                pnl_addjobs.Controls.Add(lbl_prevtoolhrs);
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='border:solid 1px #000000'>"));
                pnl_addjobs.Controls.Add(txt_totrunhrs);
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='border:solid 1px #000000'>"));
                pnl_addjobs.Controls.Add(txt_cumulative);
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='border:solid 1px #000000'>"));
                pnl_addjobs.Controls.Add(txt_dailyrunhrs);
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));

                //}
            }
            pnl_addjobs.Controls.Add(new LiteralControl("</table></td></tr></table>"));


        }
    }

    public void binddailyprogress(string existrunid)
    {
        //DataTable dt_getdetdailyprogresss = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDailyProgress where runid=" + existrunid + "").Tables[0];
        //if (dt_getdetdailyprogresss.Rows.Count > 0)
        //{
        //    txt_cmts.Text = dt_getdetdailyprogresss.Rows[0]["comments"].ToString();
        //    txt_depthstrt.Text = dt_getdetdailyprogresss.Rows[0]["depthstart"].ToString();
        //    txt_depthend.Text = dt_getdetdailyprogresss.Rows[0]["depthend"].ToString();
        //    txt_lastinc.Text = dt_getdetdailyprogresss.Rows[0]["LastInc"].ToString();
        //    txt_lastazm.Text = dt_getdetdailyprogresss.Rows[0]["lastazm"].ToString();
        //    txt_lasttemp.Text = dt_getdetdailyprogresss.Rows[0]["lasttemp"].ToString();
        //}
    }
    public void binddrillingparams(string existrunid)
    {
        //DataTable dt_getdetdrillingparams = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDrillingParameters where runid=" + existrunid + "").Tables[0];
        //if (dt_getdetdrillingparams.Rows.Count > 0)
        //{
        //    txt_pumppressure.Text = dt_getdetdrillingparams.Rows[0]["pumppressure"].ToString();
        //    txt_flowrate.Text = dt_getdetdrillingparams.Rows[0]["flowrate"].ToString();
        //    txt_mudwght.Text = dt_getdetdrillingparams.Rows[0]["mudweight"].ToString();
        //    txt_floride.Text = dt_getdetdrillingparams.Rows[0]["chlorides"].ToString();
        //    txt_pulseamp.Text = dt_getdetdrillingparams.Rows[0]["pulseamp"].ToString();
        //    txt_sand.Text = dt_getdetdrillingparams.Rows[0]["sand"].ToString();
        //    txt_solid.Text = dt_getdetdrillingparams.Rows[0]["solid"].ToString();
        //}
    }
    public void bindothersuppliesneeded(string existrunid)
    {
        //DataTable dt_getdetothersuppliesneeded = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunOtherSuppliesNeeded where runid=" + existrunid + "").Tables[0];
        //if (dt_getdetothersuppliesneeded.Rows.Count > 0)
        //{
        //    txt_othersuppliesneeded1.Text = dt_getdetothersuppliesneeded.Rows[0]["suppliesneeded"].ToString();
        //    txt_othersuppliesneeded2.Text = dt_getdetothersuppliesneeded.Rows[1]["suppliesneeded"].ToString();
        //    txt_othersuppliesneeded3.Text = dt_getdetothersuppliesneeded.Rows[2]["suppliesneeded"].ToString();
        //    txt_othersuppliesneeded4.Text = dt_getdetothersuppliesneeded.Rows[3]["suppliesneeded"].ToString();
        //    txt_othersuppliesneeded5.Text = dt_getdetothersuppliesneeded.Rows[4]["suppliesneeded"].ToString();

        //}
    }
    public void binddocuments()
    {
        if (hidd_runno.Value != "")
        {
            string selectq = "SELECT  etod.runid,etod.DocumentID,d.DocumentDisplayName,d.DocumentName from" +
                    " DailyRunReportDocs etod, documents d where d.DocumentID=etod.DocumentID and etod.runid=" + hidd_runno.Value + "";
            // etod.Uploadeddate='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'";

            DataTable dt_binddocs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
            if (dt_binddocs.Rows.Count > 0)
            {
                RadGrid2.DataSource = dt_binddocs;
                RadGrid2.DataBind();
            }
            else
            {
                RadGrid2.DataSource = null;
                RadGrid2.DataBind();
            }
        }

    }
    public void bindassetsneeded(string existrunid)
    {
        //DataTable dt_getdetassetsneeded = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunAssetsNeeded where runid=" + existrunid + "").Tables[0];
        //if (dt_getdetassetsneeded.Rows.Count > 0)
        //{
        //    if (dt_getdetassetsneeded.Rows[0]["assetid"].ToString() != "")
        //    {
        //        radcombo_assetneeded1.SelectedValue = dt_getdetassetsneeded.Rows[0]["assetid"].ToString();
        //    }
        //    else
        //    {
        //        radcombo_assetneeded1.SelectedValue = string.Empty;
        //        radcombo_assetneeded1.ClearSelection();
        //    }
        //    if (dt_getdetassetsneeded.Rows[1]["assetid"].ToString() != "")
        //    {
        //        radcombo_assetneeded2.SelectedValue = dt_getdetassetsneeded.Rows[1]["assetid"].ToString();
        //    }
        //    else
        //    {
        //        radcombo_assetneeded2.SelectedValue = string.Empty;
        //        radcombo_assetneeded2.ClearSelection();
        //    }
        //    if (dt_getdetassetsneeded.Rows[2]["assetid"].ToString() != "")
        //    {
        //        radcombo_assetneeded3.SelectedValue = dt_getdetassetsneeded.Rows[2]["assetid"].ToString();
        //    }
        //    else
        //    {
        //        radcombo_assetneeded3.SelectedValue = string.Empty;
        //        radcombo_assetneeded3.ClearSelection();
        //    }
        //    if (dt_getdetassetsneeded.Rows[3]["assetid"].ToString() != "")
        //    {
        //        radcombo_assetneeded4.SelectedValue = dt_getdetassetsneeded.Rows[3]["assetid"].ToString();
        //    }
        //    else
        //    {
        //        radcombo_assetneeded4.SelectedValue = string.Empty;
        //        radcombo_assetneeded4.ClearSelection();
        //    }
        //    if (dt_getdetassetsneeded.Rows[4]["assetid"].ToString() != "")
        //    {
        //        radcombo_assetneeded5.SelectedValue = dt_getdetassetsneeded.Rows[4]["assetid"].ToString();
        //    }
        //    else
        //    {
        //        radcombo_assetneeded5.SelectedValue = string.Empty;
        //        radcombo_assetneeded5.ClearSelection();
        //    }

        //}
    }

    public void activitylogbind(string existrunid)
    {
        DataTable dt_getdetactiveloghrvalues = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobRunActivityLog where runid=" + existrunid + " order by hour asc").Tables[0];
        if (dt_getdetactiveloghrvalues.Rows.Count > 0)
        {
            //if (dt_getdetactiveloghrvalues.Rows[0]["activityassetid"].ToString() != "")
            //    radcombo_hr1.SelectedValue = dt_getdetactiveloghrvalues.Rows[0]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr1.SelectedValue = string.Empty;
            //    radcombo_hr1.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[1]["activityassetid"].ToString() != "")
            //    radcombo_hr2.SelectedValue = dt_getdetactiveloghrvalues.Rows[1]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr2.SelectedValue = string.Empty;
            //    radcombo_hr2.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[2]["activityassetid"].ToString() != "")
            //    radcombo_hr3.SelectedValue = dt_getdetactiveloghrvalues.Rows[2]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr3.SelectedValue = string.Empty;
            //    radcombo_hr3.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[3]["activityassetid"].ToString() != "")
            //    radcombo_hr4.SelectedValue = dt_getdetactiveloghrvalues.Rows[3]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr4.SelectedValue = string.Empty;
            //    radcombo_hr4.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[4]["activityassetid"].ToString() != "")
            //    radcombo_hr5.SelectedValue = dt_getdetactiveloghrvalues.Rows[4]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr5.SelectedValue = string.Empty;
            //    radcombo_hr5.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[5]["activityassetid"].ToString() != "")
            //    radcombo_hr6.SelectedValue = dt_getdetactiveloghrvalues.Rows[5]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr6.SelectedValue = string.Empty;
            //    radcombo_hr6.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[6]["activityassetid"].ToString() != "")
            //    radcombo_hr7.SelectedValue = dt_getdetactiveloghrvalues.Rows[6]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr7.SelectedValue = string.Empty;
            //    radcombo_hr7.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[7]["activityassetid"].ToString() != "")
            //    radcombo_hr8.SelectedValue = dt_getdetactiveloghrvalues.Rows[7]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr8.SelectedValue = string.Empty;
            //    radcombo_hr8.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[8]["activityassetid"].ToString() != "")
            //    radcombo_hr9.SelectedValue = dt_getdetactiveloghrvalues.Rows[8]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr9.SelectedValue = string.Empty;
            //    radcombo_hr9.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[9]["activityassetid"].ToString() != "")
            //    radcombo_hr10.SelectedValue = dt_getdetactiveloghrvalues.Rows[9]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr10.SelectedValue = string.Empty;
            //    radcombo_hr10.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[10]["activityassetid"].ToString() != "")
            //    radcombo_hr11.SelectedValue = dt_getdetactiveloghrvalues.Rows[10]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr11.SelectedValue = string.Empty;
            //    radcombo_hr11.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[11]["activityassetid"].ToString() != "")
            //    radcombo_hr12.SelectedValue = dt_getdetactiveloghrvalues.Rows[11]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr12.SelectedValue = string.Empty;
            //    radcombo_hr12.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[12]["activityassetid"].ToString() != "")
            //    radcombo_hr13.SelectedValue = dt_getdetactiveloghrvalues.Rows[12]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr13.SelectedValue = string.Empty;
            //    radcombo_hr13.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[13]["activityassetid"].ToString() != "")
            //    radcombo_hr14.SelectedValue = dt_getdetactiveloghrvalues.Rows[13]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr14.SelectedValue = string.Empty;
            //    radcombo_hr14.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[14]["activityassetid"].ToString() != "")
            //    radcombo_hr15.SelectedValue = dt_getdetactiveloghrvalues.Rows[14]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr15.SelectedValue = string.Empty;
            //    radcombo_hr15.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[15]["activityassetid"].ToString() != "")
            //    radcombo_hr16.SelectedValue = dt_getdetactiveloghrvalues.Rows[15]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr16.SelectedValue = string.Empty;
            //    radcombo_hr16.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[16]["activityassetid"].ToString() != "")
            //    radcombo_hr17.SelectedValue = dt_getdetactiveloghrvalues.Rows[16]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr17.SelectedValue = string.Empty;
            //    radcombo_hr17.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[17]["activityassetid"].ToString() != "")
            //    radcombo_hr18.SelectedValue = dt_getdetactiveloghrvalues.Rows[17]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr18.SelectedValue = string.Empty;
            //    radcombo_hr18.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[18]["activityassetid"].ToString() != "")
            //    radcombo_hr19.SelectedValue = dt_getdetactiveloghrvalues.Rows[18]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr19.SelectedValue = string.Empty;
            //    radcombo_hr19.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[19]["activityassetid"].ToString() != "")
            //    radcombo_hr20.SelectedValue = dt_getdetactiveloghrvalues.Rows[19]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr20.SelectedValue = string.Empty;
            //    radcombo_hr20.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[20]["activityassetid"].ToString() != "")
            //    radcombo_hr21.SelectedValue = dt_getdetactiveloghrvalues.Rows[20]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr21.SelectedValue = string.Empty;
            //    radcombo_hr21.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[21]["activityassetid"].ToString() != "")
            //    radcombo_hr22.SelectedValue = dt_getdetactiveloghrvalues.Rows[21]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr22.SelectedValue = string.Empty;
            //    radcombo_hr22.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[22]["activityassetid"].ToString() != "")
            //    radcombo_hr23.SelectedValue = dt_getdetactiveloghrvalues.Rows[22]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr23.SelectedValue = string.Empty;
            //    radcombo_hr23.ClearSelection();
            //}
            //if (dt_getdetactiveloghrvalues.Rows[23]["activityassetid"].ToString() != "")
            //    radcombo_hr24.SelectedValue = dt_getdetactiveloghrvalues.Rows[23]["activityassetid"].ToString();
            //else
            //{
            //    radcombo_hr24.SelectedValue = string.Empty;
            //    radcombo_hr24.ClearSelection();
            //}


        }
        //}
    }
    protected void btn_save_OnClick(object sender, EventArgs s)
    {
        try
        {
            db.Open();
            transaction = db.BeginTransaction();
            string runid = "", dailycharge = "0.00";
            if (txt_dailycharges.Text != "")
            {
                dailycharge = txt_dailycharges.Text;
            }
            else
            {
                dailycharge = "0.00";
            }
            string finished = "";
            if (chk_runfinish.Checked)
                finished = "True";
            else
                finished = "False";
            string existingrunid = "";
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            int labelrunval = 1;
            if (lbl_runnumber.Text != "")
            {
                labelrunval = Convert.ToInt32(lbl_runnumber.Text);
            }
            if (dt_getdailyrun.Rows.Count > 0)
            {
                runid = dt_getdailyrun.Rows[0]["runid"].ToString();
                hid_runid.Value = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");
                string finvalue = dt_getdailyrun.Rows[0]["finished"].ToString();
                string path = Server.MapPath("~/Documents/");
                string filename = "", completepath = "";
                //foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
                //{
                //    f.SaveAs(path + "DailyRunRpt_" + f.GetName(), true);
                //    filename = "DailyRunRpt_" + f.GetName();
                //    completepath = path + "DailyRunRpt_" + f.GetName();
                //}
                string update_new = "update PrismJobRunDetails set finished='" + finished + "',dailycharges=" + dailycharge + ",MwdHandDay='"+txt_day.Text+"',MwdHandNight='"+txt_night.Text+"', updateddate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "";
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, update_new);
                foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
                {
                    f.SaveAs(path + "DailyRunRpt_" + f.GetName(), true);
                    filename = "DailyRunRpt_" + f.GetName();
                    completepath = path + "DailyRunRpt_" + f.GetName();



                    string query = "Insert into Documents(DocumentDisplayName,DocumentName) values('" + filename + "','" + f.GetName() + "')";
                    int documentinsert = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query);
                    if (documentinsert > 0)
                    {
                        DataTable dt_docs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    Documents WHERE  DocumentID = IDENT_CURRENT('Documents')").Tables[0];
                        string query_EventTaskOrderDocuments = "Insert into DailyRunReportDocs(DocumentID,UserID,UploadedDate,runid) values " +
                        " (" + dt_docs.Rows[0]["DocumentID"].ToString() + ",'" + Session["userId"].ToString() + "','" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'," + runid + ")";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query_EventTaskOrderDocuments);

                    }
                }
                if (finvalue == "False")
                {
                    if (finished == "True")
                    {
                        DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
                        if (dt_getrunidnextval.Rows.Count > 0)
                        {
                            string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + ")";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, addvalueexist);
                        }
                    }
                }
                else
                {
                    if (finished == "False")
                    {
                        DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                        if (dt_getrunidnextval.Rows.Count > 0)
                        {
                            string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber-1 where runid in(select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + ")";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, addvalueexist);
                        }
                    }
                }
                string notificationsendtowhome = eventNotification.sendEventNotification("DRR01");
                if (notificationsendtowhome != "")
                {
                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "DRR01", "JOB", radcombo_job.SelectedValue, radcombo_job.SelectedItem.Text,
                           "", "", "DailyRunInsert", "", "");
                }
            }
            else
            {
                string notificationsendtowhome = eventNotification.sendEventNotification("DRR02");
                if (notificationsendtowhome != "")
                {
                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "DRR02", "JOB", radcombo_job.SelectedValue, radcombo_job.SelectedItem.Text,
                           "", "", "DailyRunUpdate", "", "");
                }
                if (finished == "True")
                {
                    DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                    if (dt_getrunidnextval.Rows.Count > 0)
                    {
                        string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, addvalueexist);
                    }
                }

                string runnumber = "", runstatus = "";
                int addrunval = 1;
                string selectq = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date in (select MAX(Date) from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date  <= '" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "')";
                DataTable dt_getruncnt = SqlHelper.ExecuteDataset(transaction, CommandType.Text, selectq).Tables[0];
                if (dt_getruncnt.Rows.Count > 0)
                {
                    runnumber = dt_getruncnt.Rows[0]["runnumber"].ToString();
                    runstatus = dt_getruncnt.Rows[0]["finished"].ToString();
                    if (runstatus == "True")
                    {
                        addrunval = Convert.ToInt32(runnumber) + 1;
                    }
                    else
                    {
                        addrunval = Convert.ToInt32(runnumber);
                    }
                }
                string path = Server.MapPath("~/Documents/");
                string filename = "", completepath = "";

                string insertquery_jobrundet = "insert into PrismJobRunDetails(jid,Date,runnumber,finished,dailycharges,updateddate,MwdHandDay,MwdHandNight)values(" +
                "" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(radtxt_start.SelectedDate.ToString()) + "'," +
                "" + addrunval + ",'" + finished + "'," + dailycharge + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + txt_day.Text + "','"+txt_night.Text+"')";

                int cnt = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertquery_jobrundet);
                DataTable dt_runid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    PrismJobRunDetails WHERE  runid = IDENT_CURRENT('PrismJobRunDetails')").Tables[0];

                runid = dt_runid.Rows[0]["runid"].ToString();
                hid_runid.Value = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");

                foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
                {
                    f.SaveAs(path + "DailyRunRpt_" + f.GetName(), true);
                    filename = "DailyRunRpt_" + f.GetName();
                    completepath = path + "DailyRunRpt_" + f.GetName();



                    string query = "Insert into Documents(DocumentDisplayName,DocumentName) values('" + filename + "','" + f.GetName() + "')";
                    int documentinsert = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query);
                    if (documentinsert > 0)
                    {
                        DataTable dt_docs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    Documents WHERE  DocumentID = IDENT_CURRENT('Documents')").Tables[0];
                        string query_EventTaskOrderDocuments = "Insert into DailyRunReportDocs(DocumentID,UserID,UploadedDate,runid) values " +
                        " (" + dt_docs.Rows[0]["DocumentID"].ToString() + ",'" + Session["userId"].ToString() + "','" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'," + runid + ")";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query_EventTaskOrderDocuments);

                    }
                }


            }
            //DataTable dt_getrunpersonals = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunPersonals where runid=" + runid + "").Tables[0];
            //if (dt_getrunpersonals.Rows.Count == 0)
            //{
            //    for (int asset = 0; asset < combo_ex_personal.Items.Count; asset++)
            //    {
            //        if (combo_ex_personal.Items[asset].Checked)
            //        {
            //            string insert_personals = "insert into PrismJobRunPersonals(runid,personid)values(" + runid + "," + combo_ex_personal.Items[asset].Value + ")";
            //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_personals);
            //        }
            //    }
            //}

            if (runid != "")
            {
                //PrismJobRunServiceDetails
                //DataTable dt_Table1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismService").Tables[0];
                //for (int k = 0; k < dt_Table1.Rows.Count; k++)
                //{
                //    string insertser = "";
                //    string chkserviceinuse = "chk_service_" + dt_Table1.Rows[k]["ID"].ToString();
                //    CheckBox chk_serviceinuse = pnl_addjobs.FindControl(chkserviceinuse) as CheckBox;
                //    if (chk_serviceinuse != null)
                //    {
                //        if (chk_serviceinuse.Checked)
                //        {
                //            insertser = "insert into PrismJobRunServiceDetails(runid,serviceId,inuse)values(" + runid + "," + dt_Table1.Rows[k]["ID"].ToString() + ",'True')";

                //        }
                //        else
                //        {
                //            insertser = "insert into PrismJobRunServiceDetails(runid,serviceId,inuse)values(" + runid + "," + dt_Table1.Rows[k]["ID"].ToString() + ",'False')";
                //        }
                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertser);
                //        //chk_serviceinuse.ID = "chk_service_" + dt_Table1.Rows[k]["ID"].ToString();
                //    }
                //}
                DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(transaction, CommandType.Text,
                    "select  pn.AssetName as NameofAsset,pn.id as MainAssetId,pa.id as AID,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca,PrismAssetName pn " +
                    " where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.assignmentstatus='Active' and pj.jobid=" + radcombo_job.SelectedValue + "").Tables[0];
                for (int cat = 0; cat < dt_getassetcategories.Rows.Count; cat++)
                {
                    string chkinuseid = "chk_" + cat;
                    CheckBox chk_inuse = pnl_addjobs.FindControl(chkinuseid) as CheckBox;
                    string dailyrunhrs = "txt_dailyrunhrs" + cat;
                    TextBox txt_dailyrunhrs = pnl_addjobs.FindControl(dailyrunhrs) as TextBox;
                    string dailyrunvalue = "";
                    if (chk_inuse != null)
                    {
                        if (txt_dailyrunhrs.Text != "")
                        {
                            dailyrunvalue = txt_dailyrunhrs.Text;
                        }
                        else
                        {
                            dailyrunvalue = "NULL";
                        }
                        if (chk_inuse.Checked)
                        {
                            string insert_jobrunhrdet = "";
                            //string totrunhrs = "txt_totrunhrs" + cat;
                            //string cumulativehrs = "txt_cumulativehrs" + cat;

                            //TextBox txt_totrunhrs = pnl_addjobs.FindControl(totrunhrs) as TextBox;
                            //TextBox txt_cumulativehrs = pnl_addjobs.FindControl(cumulativehrs) as TextBox;

                            DataTable dt_jobhrdet = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunHourDetails where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["AssetId"].ToString() + "").Tables[0];
                            if (dt_jobhrdet.Rows.Count == 0)
                            {

                                insert_jobrunhrdet = "insert into PrismJobRunHourDetails(runid,assetid,inuse,dailyrunhrs)values" +
                                    "(" + runid + "," + dt_getassetcategories.Rows[cat]["AID"].ToString() + ",'True'," +
                                "" + dailyrunvalue + ")";

                            }
                            else
                            {
                                insert_jobrunhrdet = "update PrismJobRunHourDetails set inuse='True',dailyrunhrs=" + dailyrunvalue + " where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["AID"].ToString() + "";
                            }
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_jobrunhrdet);
                        }
                        else
                        {
                            string insert_jobrunhrdet = "";
                            //string totrunhrs = "txt_totrunhrs" + cat;
                            //string cumulativehrs = "txt_cumulativehrs" + cat;

                            DataTable dt_jobhrdet = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunHourDetails where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["AID"].ToString() + "").Tables[0];
                            if (dt_jobhrdet.Rows.Count == 0)
                            {

                                insert_jobrunhrdet = "insert into PrismJobRunHourDetails(runid,assetid,inuse,dailyrunhrs)values" +
                                    "(" + runid + "," + dt_getassetcategories.Rows[cat]["AID"].ToString() + ",'False'," +
                                "" + dailyrunvalue + ")";

                            }
                            else
                            {
                                insert_jobrunhrdet = "update PrismJobRunHourDetails set inuse='False',dailyrunhrs=" + dailyrunvalue + " where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["AID"].ToString() + "";
                            }
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_jobrunhrdet);
                        }
                    }
                }
                ////ACTIVITY LOG INSERTION
                //string hr1 = "", hr2 = "", hr3 = "", hr4 = "", hr5 = "", hr6 = "", hr7 = "", hr8 = "", hr9 = "", hr10 = "", hr11 = "", hr12 = "", hr13 = ""
                //    , hr14 = "", hr15 = "", hr16 = "", hr17 = "", hr18 = "", hr19 = "", hr20 = "", hr21 = "", hr22 = "", hr23 = "", hr24 = "";
                //string hr1val = "", hr2val = "", hr3val = "", hr4val = "", hr5val = "", hr6val = "", hr7val = "", hr8val = "", hr9val = "", hr10val = "", hr11val = "", hr12val = "", hr13val = ""
                //    , hr14val = "", hr15val = "", hr16val = "", hr17val = "", hr18val = "", hr19val = "", hr20val = "", hr21val = "", hr22val = "", hr23val = "", hr24val = "";
                //DataTable dt_gethr1 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=1").Tables[0];
                //DataTable dt_gethr2 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=2").Tables[0];
                //DataTable dt_gethr3 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=3").Tables[0];
                //DataTable dt_gethr4 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=4").Tables[0];
                //DataTable dt_gethr5 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=5").Tables[0];
                //DataTable dt_gethr6 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=6").Tables[0];
                //DataTable dt_gethr7 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=7").Tables[0];
                //DataTable dt_gethr8 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=8").Tables[0];
                //DataTable dt_gethr9 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=9").Tables[0];
                //DataTable dt_gethr10 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=10").Tables[0];
                //DataTable dt_gethr11 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=11").Tables[0];
                //DataTable dt_gethr12 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=12").Tables[0];
                //DataTable dt_gethr13 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=13").Tables[0];
                //DataTable dt_gethr14 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=14").Tables[0];
                //DataTable dt_gethr15 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=15").Tables[0];
                //DataTable dt_gethr16 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=16").Tables[0];
                //DataTable dt_gethr17 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=17").Tables[0];
                //DataTable dt_gethr18 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=18").Tables[0];
                //DataTable dt_gethr19 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=19").Tables[0];
                //DataTable dt_gethr20 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=20").Tables[0];
                //DataTable dt_gethr21 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=21").Tables[0];
                //DataTable dt_gethr22 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=22").Tables[0];
                //DataTable dt_gethr23 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=23").Tables[0];
                //DataTable dt_gethr24 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=24").Tables[0];
                //if (radcombo_hr1.SelectedValue != "")
                //    hr1val = radcombo_hr1.SelectedValue;
                //else
                //    hr1val = "NULL";

                //if (radcombo_hr2.SelectedValue != "")
                //    hr2val = radcombo_hr2.SelectedValue;
                //else
                //    hr2val = "NULL";

                //if (radcombo_hr3.SelectedValue != "")
                //    hr3val = radcombo_hr3.SelectedValue;
                //else
                //    hr3val = "NULL";

                //if (radcombo_hr4.SelectedValue != "")
                //    hr4val = radcombo_hr4.SelectedValue;
                //else
                //    hr4val = "NULL";

                //if (radcombo_hr5.SelectedValue != "")
                //    hr5val = radcombo_hr5.SelectedValue;
                //else
                //    hr5val = "NULL";

                //if (radcombo_hr6.SelectedValue != "")
                //    hr6val = radcombo_hr6.SelectedValue;
                //else
                //    hr6val = "NULL";

                //if (radcombo_hr7.SelectedValue != "")
                //    hr7val = radcombo_hr7.SelectedValue;
                //else
                //    hr7val = "NULL";

                //if (radcombo_hr8.SelectedValue != "")
                //    hr8val = radcombo_hr8.SelectedValue;
                //else
                //    hr8val = "NULL";

                //if (radcombo_hr9.SelectedValue != "")
                //    hr9val = radcombo_hr9.SelectedValue;
                //else
                //    hr9val = "NULL";

                //if (radcombo_hr10.SelectedValue != "")
                //    hr10val = radcombo_hr10.SelectedValue;
                //else
                //    hr10val = "NULL";

                //if (radcombo_hr11.SelectedValue != "")
                //    hr11val = radcombo_hr11.SelectedValue;
                //else
                //    hr11val = "NULL";

                //if (radcombo_hr12.SelectedValue != "")
                //    hr12val = radcombo_hr12.SelectedValue;
                //else
                //    hr12val = "NULL";

                //if (radcombo_hr13.SelectedValue != "")
                //    hr13val = radcombo_hr13.SelectedValue;
                //else
                //    hr13val = "NULL";


                //if (radcombo_hr14.SelectedValue != "")
                //    hr14val = radcombo_hr14.SelectedValue;
                //else
                //    hr14val = "NULL";

                //if (radcombo_hr15.SelectedValue != "")
                //    hr15val = radcombo_hr15.SelectedValue;
                //else
                //    hr15val = "NULL";

                //if (radcombo_hr16.SelectedValue != "")
                //    hr16val = radcombo_hr16.SelectedValue;
                //else
                //    hr16val = "NULL";

                //if (radcombo_hr17.SelectedValue != "")
                //    hr17val = radcombo_hr17.SelectedValue;
                //else
                //    hr17val = "NULL";

                //if (radcombo_hr18.SelectedValue != "")
                //    hr18val = radcombo_hr18.SelectedValue;
                //else
                //    hr18val = "NULL";

                //if (radcombo_hr19.SelectedValue != "")
                //    hr19val = radcombo_hr19.SelectedValue;
                //else
                //    hr19val = "NULL";

                //if (radcombo_hr20.SelectedValue != "")
                //    hr20val = radcombo_hr20.SelectedValue;
                //else
                //    hr20val = "NULL";

                //if (radcombo_hr21.SelectedValue != "")
                //    hr21val = radcombo_hr21.SelectedValue;
                //else
                //    hr21val = "NULL";

                //if (radcombo_hr22.SelectedValue != "")
                //    hr22val = radcombo_hr22.SelectedValue;
                //else
                //    hr22val = "NULL";

                //if (radcombo_hr23.SelectedValue != "")
                //    hr23val = radcombo_hr23.SelectedValue;
                //else
                //    hr23val = "NULL";

                //if (radcombo_hr24.SelectedValue != "")
                //    hr24val = radcombo_hr24.SelectedValue;
                //else
                //    hr24val = "NULL";

                //if (dt_gethr1.Rows.Count == 0)
                //{


                //    hr1 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",1," + hr1val + ")";

                //}
                //else
                //{
                //    hr1 = "update PrismJobRunActivityLog set activityassetid=" + hr1val + " where runid=" + runid + " and hour=1";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr1);
                //if (dt_gethr2.Rows.Count == 0)
                //{
                //    hr2 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",2," + hr2val + ")";

                //}
                //else
                //{
                //    hr2 = "update PrismJobRunActivityLog set activityassetid=" + hr2val + " where runid=" + runid + " and hour=2";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr2);
                //if (dt_gethr3.Rows.Count == 0)
                //{
                //    hr3 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",3," + hr3val + ")";

                //}
                //else
                //{
                //    hr3 = "update PrismJobRunActivityLog set activityassetid=" + hr3val + " where runid=" + runid + " and hour=3";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr3);
                //if (dt_gethr4.Rows.Count == 0)
                //{
                //    hr4 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",4," + hr4val + ")";

                //}
                //else
                //{
                //    hr4 = "update PrismJobRunActivityLog set activityassetid=" + hr4val + " where runid=" + runid + " and hour=4";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr4);
                //if (dt_gethr5.Rows.Count == 0)
                //{
                //    hr5 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",5," + hr5val + ")";

                //}
                //else
                //{
                //    hr5 = "update PrismJobRunActivityLog set activityassetid=" + hr5val + " where runid=" + runid + " and hour=5";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr5);
                //if (dt_gethr6.Rows.Count == 0)
                //{
                //    hr6 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",6," + hr6val + ")";

                //}
                //else
                //{
                //    hr6 = "update PrismJobRunActivityLog set activityassetid=" + hr6val + " where runid=" + runid + " and hour=6";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr6);
                //if (dt_gethr7.Rows.Count == 0)
                //{
                //    hr7 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",7," + hr7val + ")";

                //}
                //else
                //{
                //    hr7 = "update PrismJobRunActivityLog set activityassetid=" + hr7val + " where runid=" + runid + " and hour=7";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr7);
                //if (dt_gethr8.Rows.Count == 0)
                //{
                //    hr8 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",8," + hr8val + ")";

                //}
                //else
                //{
                //    hr8 = "update PrismJobRunActivityLog set activityassetid=" + hr8val + " where runid=" + runid + " and hour=8";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr8);
                //if (dt_gethr9.Rows.Count == 0)
                //{
                //    hr9 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",9," + hr9val + ")";

                //}
                //else
                //{
                //    hr9 = "update PrismJobRunActivityLog set activityassetid=" + hr9val + " where runid=" + runid + " and hour=9";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr9);
                //if (dt_gethr10.Rows.Count == 0)
                //{
                //    hr10 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",10," + hr10val + ")";

                //}
                //else
                //{
                //    hr10 = "update PrismJobRunActivityLog set activityassetid=" + hr10val + " where runid=" + runid + " and hour=10";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr10);
                //if (dt_gethr11.Rows.Count == 0)
                //{
                //    hr11 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",11," + hr11val + ")";

                //}
                //else
                //{
                //    hr11 = "update PrismJobRunActivityLog set activityassetid=" + hr11val + " where runid=" + runid + " and hour=11";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr11);
                //if (dt_gethr12.Rows.Count == 0)
                //{
                //    hr12 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",12," + hr12val + ")";

                //}
                //else
                //{
                //    hr12 = "update PrismJobRunActivityLog set activityassetid=" + hr12val + " where runid=" + runid + " and hour=12";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr12);
                //if (dt_gethr13.Rows.Count == 0)
                //{
                //    hr13 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",13," + hr13val + ")";

                //}
                //else
                //{
                //    hr13 = "update PrismJobRunActivityLog set activityassetid=" + hr13val + " where runid=" + runid + " and hour=13";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr13);
                //if (dt_gethr14.Rows.Count == 0)
                //{
                //    hr14 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",14," + hr14val + ")";

                //}
                //else
                //{
                //    hr14 = "update PrismJobRunActivityLog set activityassetid=" + hr14val + " where runid=" + runid + " and hour=14";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr14);
                //if (dt_gethr15.Rows.Count == 0)
                //{
                //    hr15 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",15," + hr15val + ")";

                //}
                //else
                //{
                //    hr15 = "update PrismJobRunActivityLog set activityassetid=" + hr15val + " where runid=" + runid + " and hour=15";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr15);
                //if (dt_gethr16.Rows.Count == 0)
                //{
                //    hr16 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",16," + hr16val + ")";

                //}
                //else
                //{
                //    hr16 = "update PrismJobRunActivityLog set activityassetid=" + hr16val + " where runid=" + runid + " and hour=16";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr16);
                //if (dt_gethr17.Rows.Count == 0)
                //{
                //    hr17 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",17," + hr17val + ")";

                //}
                //else
                //{
                //    hr17 = "update PrismJobRunActivityLog set activityassetid=" + hr17val + " where runid=" + runid + " and hour=17";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr17);
                //if (dt_gethr18.Rows.Count == 0)
                //{
                //    hr18 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",18," + hr18val + ")";

                //}
                //else
                //{
                //    hr18 = "update PrismJobRunActivityLog set activityassetid=" + hr18val + " where runid=" + runid + " and hour=18";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr18);
                //if (dt_gethr19.Rows.Count == 0)
                //{
                //    hr19 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",19," + hr19val + ")";

                //}
                //else
                //{
                //    hr19 = "update PrismJobRunActivityLog set activityassetid=" + hr19val + " where runid=" + runid + " and hour=19";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr19);
                //if (dt_gethr20.Rows.Count == 0)
                //{
                //    hr20 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",20," + hr20val + ")";

                //}
                //else
                //{
                //    hr20 = "update PrismJobRunActivityLog set activityassetid=" + hr20val + " where runid=" + runid + " and hour=20";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr20);
                //if (dt_gethr21.Rows.Count == 0)
                //{
                //    hr21 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",21," + hr21val + ")";

                //}
                //else
                //{
                //    hr21 = "update PrismJobRunActivityLog set activityassetid=" + hr21val + " where runid=" + runid + " and hour=21";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr21);
                //if (dt_gethr22.Rows.Count == 0)
                //{
                //    hr22 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",22," + hr22val + ")";

                //}
                //else
                //{
                //    hr22 = "update PrismJobRunActivityLog set activityassetid=" + hr22val + " where runid=" + runid + " and hour=22";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr22);
                //if (dt_gethr23.Rows.Count == 0)
                //{
                //    hr23 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",23," + hr23val + ")";

                //}
                //else
                //{
                //    hr23 = "update PrismJobRunActivityLog set activityassetid=" + hr23val + " where runid=" + runid + " and hour=23";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr23);
                //if (dt_gethr24.Rows.Count == 0)
                //{
                //    hr24 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                //        "" + runid + ",24," + hr24val + ")";

                //}
                //else
                //{
                //    hr24 = "update PrismJobRunActivityLog set activityassetid=" + hr24val + " where runid=" + runid + " and hour=24";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr24);
                //END OF INSERT ACTIVITY LOH HRS

                //START DAILY PROGRESS INSERT
                //string depthstart = "", depthend = "", lastinc = "", lastazm = "", lasttemp = "";
                //if (txt_depthstrt.Text != "")
                //{
                //    depthstart = txt_depthstrt.Text;
                //}
                //else
                //{
                //    depthstart = "NULL";
                //}
                //if (txt_depthend.Text != "")
                //{
                //    depthend = txt_depthend.Text;
                //}
                //else
                //{
                //    depthend = "NULL";
                //}
                //if (txt_lastinc.Text != "")
                //{
                //    lastinc = txt_lastinc.Text;
                //}
                //else
                //{
                //    lastinc = "NULL";
                //}
                //if (txt_lastazm.Text != "")
                //{
                //    lastazm = txt_lastazm.Text;
                //}
                //else
                //{
                //    lastazm = "NULL";
                //}
                //if (txt_lasttemp.Text != "")
                //{
                //    lasttemp = txt_lasttemp.Text;
                //}
                //else
                //{
                //    lasttemp = "NULL";
                //}
                //string insert_dailyprogress = "";
                //DataTable dt_dailyprogress = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunDailyProgress where runid=" + runid + "").Tables[0];
                //if (dt_dailyprogress.Rows.Count == 0)
                //{
                //    insert_dailyprogress = "insert into PrismJobRunDailyProgress(runid,depthstart,depthend,LastInc,lastazm,lasttemp,comments)values(" +
                //        "" + runid + "," + depthstart + "," + depthend + "," + lastinc + "," + lastazm + "," + lasttemp + ",'" + txt_cmts.Text + "')";
                //}
                //else
                //{
                //    insert_dailyprogress = "update PrismJobRunDailyProgress set depthstart=" + depthstart + ",depthend=" + depthend + "," +
                //        "LastInc=" + lastinc + ",lastazm=" + lastazm + ",lasttemp=" + lasttemp + ",comments='" + txt_cmts.Text + "' where runid=" + runid + "";
                //}
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_dailyprogress);
                ////END DAILY PROGRESS
                ////INSERT DRILLING PARAMETERS

                //string pumppressure = "", flowrate = "", mudwght = "", floride = "", pulseamp = "", sand = "", solid = "";
                //if (txt_pumppressure.Text != "")
                //{
                //    pumppressure = txt_pumppressure.Text;
                //}
                //else
                //{
                //    pumppressure = "NULL";
                //}
                //if (txt_flowrate.Text != "")
                //{
                //    flowrate = txt_flowrate.Text;
                //}
                //else
                //{
                //    flowrate = "NULL";
                //}
                //if (txt_mudwght.Text != "")
                //{
                //    mudwght = txt_mudwght.Text;
                //}
                //else
                //{
                //    mudwght = "NULL";
                //}
                //if (txt_floride.Text != "")
                //{
                //    floride = txt_floride.Text;
                //}
                //else
                //{
                //    floride = "NULL";
                //}
                //if (txt_pulseamp.Text != "")
                //{
                //    pulseamp = txt_pulseamp.Text;
                //}
                //else
                //{
                //    pulseamp = "NULL";
                //}
                //if (txt_sand.Text != "")
                //{
                //    sand = txt_sand.Text;
                //}
                //else
                //{
                //    sand = "NULL";
                //}
                //if (txt_solid.Text != "")
                //{
                //    solid = txt_solid.Text;
                //}
                //else
                //{
                //    solid = "NULL";
                //}
                //string insert_drillingparameters = "";
                //DataTable dt_drillingparameters = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunDrillingParameters where runid=" + runid + "").Tables[0];
                //if (dt_drillingparameters.Rows.Count == 0)
                //{
                //    insert_drillingparameters = "insert into PrismJobRunDrillingParameters(runid,pumppressure,flowrate,mudweight,chlorides,pulseamp,sand,solid)values(" +
                //        "" + runid + "," + pumppressure + "," + flowrate + "," + mudwght + "," + floride + "," + pulseamp + "," + sand + "," + solid + ")";
                //}
                //else
                //{
                //    insert_drillingparameters = "update PrismJobRunDrillingParameters set pumppressure=" + pumppressure + "" +
                //    ",flowrate=" + flowrate + ",mudweight=" + mudwght + ",chlorides=" + floride + "" +
                //    ",pulseamp=" + pulseamp + ",sand=" + sand + ",solid=" + solid + " where runid=" + runid + "";
                //}

                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_drillingparameters);
                ////END OF DRILLING PARAMETERS

                ////INSERT ASSETS NEEDED
                //string assetneededval1 = "", assetneededval2 = "", assetneededval3 = "", assetneededval4 = "", assetneededval5 = "";
                //string query_assetsneeded1 = "", query_assetsneeded2 = "", query_assetsneeded3 = "", query_assetsneeded4 = "", query_assetsneeded5 = "";
                //if (radcombo_assetneeded1.SelectedValue != "")
                //{
                //    assetneededval1 = radcombo_assetneeded1.SelectedValue;


                //}
                //else
                //{
                //    assetneededval1 = "NULL";

                //}
                //if (radcombo_assetneeded2.SelectedValue != "")
                //{
                //    assetneededval2 = radcombo_assetneeded2.SelectedValue;

                //}
                //else
                //{
                //    assetneededval2 = "NULL";
                //}
                //if (radcombo_assetneeded3.SelectedValue != "")
                //{
                //    assetneededval3 = radcombo_assetneeded3.SelectedValue;

                //}
                //else
                //{
                //    assetneededval3 = "NULL";
                //}
                //if (radcombo_assetneeded4.SelectedValue != "")
                //{
                //    assetneededval4 = radcombo_assetneeded4.SelectedValue;

                //}
                //else
                //{
                //    assetneededval4 = "NULL";
                //}
                //if (radcombo_assetneeded5.SelectedValue != "")
                //{
                //    assetneededval5 = radcombo_assetneeded5.SelectedValue;

                //}
                //else
                //{
                //    assetneededval5 = "NULL";
                //}
                //DataTable dt_assetsneeded1 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunAssetsNeeded where runid=" + runid + "").Tables[0];
                //if (dt_assetsneeded1.Rows.Count > 0)
                //{

                //    if (assetneededval1 != "NULL")
                //    {
                //        query_assetsneeded1 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval1 + " where runid=" + runid + "  and serialnumber=1";

                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded1);
                //    }

                //    if (assetneededval2 != "NULL")
                //    {
                //        query_assetsneeded2 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval2 + " where runid=" + runid + "  and serialnumber=2";

                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded2);
                //    }

                //    if (assetneededval3 != "NULL")
                //    {
                //        query_assetsneeded3 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval3 + " where runid=" + runid + "  and serialnumber=3";

                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded3);
                //    }

                //    if (assetneededval4 != "NULL")
                //    {
                //        query_assetsneeded4 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval4 + " where runid=" + runid + " and serialnumber=4";

                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded4);
                //    }

                //    if (assetneededval5 != "NULL")
                //    {
                //        query_assetsneeded5 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval5 + " where runid=" + runid + " and serialnumber=5";

                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded5);
                //    }
                //}
                //else
                //{
                //    query_assetsneeded1 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval1 + ",1)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded1);
                //    query_assetsneeded2 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval2 + ",2)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded2);
                //    query_assetsneeded3 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval3 + ",3)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded3);
                //    query_assetsneeded4 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval4 + ",4)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded4);
                //    query_assetsneeded5 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval5 + ",5)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded5);
                //}

                ////END

                ////OTHER SUPPLIES NEEDED
                ////string query_otherassetsneeded1="",query_otherassetsneeded2="",query_otherassetsneeded3="",query_otherassetsneeded4="",query_otherassetsneeded5="";
                //DataTable dt_othersupplyneeded = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunOtherSuppliesNeeded where runid=" + runid + "").Tables[0];
                //if (dt_othersupplyneeded.Rows.Count > 0)
                //{
                //    if (txt_othersuppliesneeded1.Text != "")
                //    {
                //        string query_otherassetsneeded1 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded1.Text + "' where runid=" + runid + " and serialnumber=1";
                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded1);
                //    }
                //    else
                //    {
                //        if (dt_othersupplyneeded.Rows[0]["suppliesneeded"].ToString() != "")
                //        {
                //            string query_otherassetsneeded1 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded1.Text + "' where runid=" + runid + " and and serialnumber=1";
                //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded1);
                //        }
                //    }
                //    if (txt_othersuppliesneeded2.Text != "")
                //    {
                //        string query_otherassetsneeded2 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded2.Text + "' where runid=" + runid + " and serialnumber=2";
                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded2);
                //    }
                //    else
                //    {
                //        if (dt_othersupplyneeded.Rows[1]["suppliesneeded"].ToString() != "")
                //        {
                //            string query_otherassetsneeded2 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded2.Text + "' where runid=" + runid + " and serialnumber=2";
                //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded2);
                //        }
                //    }
                //    if (txt_othersuppliesneeded3.Text != "")
                //    {
                //        string query_otherassetsneeded3 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded3.Text + "' where runid=" + runid + " and serialnumber=3";
                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded3);
                //    }
                //    else
                //    {
                //        if (dt_othersupplyneeded.Rows[2]["suppliesneeded"].ToString() != "")
                //        {
                //            string query_otherassetsneeded3 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded3.Text + "' where runid=" + runid + " and serialnumber=3";
                //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded3);
                //        }
                //    }
                //    if (txt_othersuppliesneeded4.Text != "")
                //    {
                //        string query_otherassetsneeded4 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded4.Text + "' where runid=" + runid + " and serialnumber=4";
                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded4);
                //    }
                //    else
                //    {
                //        if (dt_othersupplyneeded.Rows[3]["suppliesneeded"].ToString() != "")
                //        {
                //            string query_otherassetsneeded4 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded4.Text + "' where runid=" + runid + " and serialnumber=4";
                //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded4);
                //        }
                //    }
                //    if (txt_othersuppliesneeded5.Text != "")
                //    {
                //        string query_otherassetsneeded5 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded5.Text + "' where runid=" + runid + " and serialnumber=5";
                //        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded5);
                //    }
                //    else
                //    {
                //        if (dt_othersupplyneeded.Rows[4]["suppliesneeded"].ToString() != "")
                //        {
                //            string query_otherassetsneeded5 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded5.Text + "' where runid=" + runid + " and serialnumber=5";
                //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded5);
                //        }
                //    }
                //}
                //else
                //{
                //    string query_otherassetsneeded1 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded1.Text + "',1)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded1);
                //    string query_otherassetsneeded2 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded2.Text + "',2)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded2);
                //    string query_otherassetsneeded3 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded3.Text + "',3)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded3);
                //    string query_otherassetsneeded4 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded4.Text + "',4)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded4);
                //    string query_otherassetsneeded5 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded5.Text + "',5)";
                //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded5);
                //}

            }
            //ATTACHMENT

            DataTable dt_PrismJobRunDetailsFormInfo = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "Select * from PrismJobRunDetailsFormInfo").Tables[0];
            DataRow[] row_form = dt_PrismJobRunDetailsFormInfo.Select("Jobid=" + radcombo_job.SelectedValue + " and RunId=" + runid + "");
            if (row_form.Length == 0)
            {
                string inset_PrismJobRunDetailsFormInfo = "Insert into PrismJobRunDetailsFormInfo(Jobid,RunId,CompanyRep,POPPET_SIZE,ORIFICE_SIZE,PULSE_WIDTH,PULSE_AMPLITUDE,TOTAL_CONNECTED," +
                "TOTAL_CIRC,DEPTH_START,DEPTH_END,INCStart,AZMStart,MAGFStart,GRAVStart,DIPStart,INCEnd,AZMEnd,MAGFEnd,GRAVEnd,DIPEnd,TEMPERATURE_C,TEMPERATURE_F,AVER_PUMP_PRESSURE,AVER_FLOW_RATE,MUD_WEIGHT,SOLIDS,SAND)" +
                " Values(" + radcombo_job.SelectedValue + "," + runid + ",'" + txt_companyrep.Text + "','" + txt_poppet.Text + "','" + txt_orifice.Text + "','" + txt_pulsewidth.Text + "','" + txt_pulseamplitude.Text + "'," +
                "'" + txt_totalconnected.Text + "','" + txt_totalcirc.Text + "','" + txt_depthstart.Text + "','" + txt_depthend1.Text + "','" + txt_incstart.Text + "','" + txt_azmstart.Text + "','" + txt_magfstart.Text + "','" + txt_gravstart.Text + "','" + txt_dipstart.Text + "'," +
                "'" + txt_incend.Text + "','" + txt_azmend.Text + "','" + txt_magfend.Text + "','" + txt_gravend.Text + "','" + txt_dipend.Text + "','" + txt_temp_c.Text + "','" + txt_temp_f.Text + "','" + txt_avr_pump_tressure.Text + "'," +
                "'" + txt_avre_flow_rate.Text + "','" + txt_mud_weight.Text + "','" + txt_solids.Text + "','" + txt_sand1.Text + "')";
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, inset_PrismJobRunDetailsFormInfo);
            }
            else
            {
                string Update_PrismJobRunDetailsFormInfo = "Update PrismJobRunDetailsFormInfo set CompanyRep='" + txt_companyrep.Text + "',POPPET_SIZE='" + txt_poppet.Text + "'," +
                    " ORIFICE_SIZE='" + txt_orifice.Text + "',PULSE_WIDTH='" + txt_pulsewidth.Text + "',PULSE_AMPLITUDE='" + txt_pulseamplitude.Text + "',TOTAL_CONNECTED='" + txt_totalconnected.Text + "'," +
                        "TOTAL_CIRC='" + txt_totalcirc.Text + "',DEPTH_START='" + txt_depthstart.Text + "',DEPTH_END='" + txt_depthend1.Text + "',INCStart='" + txt_incstart.Text + "',AZMStart='" + txt_azmstart.Text + "'," +
                        " MAGFStart='" + txt_magfstart.Text + "',GRAVStart='" + txt_gravstart.Text + "',DIPStart='" + txt_dipstart.Text + "',INCEnd='" + txt_incend.Text + "',AZMEnd='" + txt_azmend.Text + "'," +
                        " MAGFEnd='" + txt_magfend.Text + "',GRAVEnd='" + txt_gravend.Text + "',DIPEnd='" + txt_dipend.Text + "',TEMPERATURE_C='" + txt_temp_c.Text + "',TEMPERATURE_F='" + txt_temp_f.Text + "'," +
                        " AVER_PUMP_PRESSURE='" + txt_avr_pump_tressure.Text + "',AVER_FLOW_RATE='" + txt_avre_flow_rate.Text + "',MUD_WEIGHT='" + txt_mud_weight.Text + "'," +
                        " SOLIDS='" + txt_solids.Text + "',SAND='" + txt_sand1.Text + "' where Jobid=" + radcombo_job.SelectedValue + " and RunId=" + runid + "";
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, Update_PrismJobRunDetailsFormInfo);
            }
            string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedTimeStamp,Source)values(" +
                   "'DDR01','Daily Run Report','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'" +
               ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','JOB')";
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insert_q);

            transaction.Commit();

            db.Close();
            lbl_message.Text = "Daily Run Report Saved.";
            lbl_message.ForeColor = Color.Green;

        }
        catch (Exception ex)
        {
            transaction.Rollback();

            lbl_message.Text = "Daily Run Report Error " + ex.Message + "";
            lbl_message.ForeColor = Color.Red;
        }
        btn_view_Click(null, null);
        //clear();

    }
    public string getMWDOperators()
    {
        string getnames = "", vaue = "";
        foreach (GridDataItem item in radgrid_mwdopertors.Items)
        {

            //(item["TemplateColumn"].Controls["Textbox1"] as TextBox).Text = "bbbbb";
            vaue = item["firstName"].Text + " " + item["lastName"].Text + ",";

        }
        if (vaue != "")
        {
            getnames = vaue.Remove(vaue.Length - 1, 1);
        }

        return getnames;
    }
    protected void btn_export_OnClick(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        excelApp.ScreenUpdating = false;
        excelApp.DisplayAlerts = false;

        object misValue = System.Reflection.Missing.Value;
        if (File.Exists(Server.MapPath("ExportDailyRunReport_copy.xlsx")))
        {
            File.Delete(Server.MapPath("ExportDailyRunReport_copy.xlsx"));
        }
        File.Copy(Server.MapPath("ExportDailyRunReport.xlsx"), Server.MapPath("ExportDailyRunReport_copy.xlsx"));
        //Opening Excel file(myData.xlsx)
        Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("ExportDailyRunReport_copy.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
        string currentSheet = "Sheet2";
        Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;

        excelWorksheet.Cells[4, 1] = lbl_jname.Text;
        excelWorksheet.Cells[4, 2] = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");
        excelWorksheet.Cells[4, 3] = lbl_sdate.Text;
        excelWorksheet.Cells[4, 4] = lbl_edate.Text;
        excelWorksheet.Cells[4, 5] = hid_runno.Value;

        excelWorksheet.Cells[4, 6] = lbl_daynumber.Text;

        excelWorksheet.Cells[6, 2] = lbl_operator.Text;
        excelWorksheet.Cells[7, 2] = lbl_well.Text;
        excelWorksheet.Cells[8, 2] = lbl_location.Text;
        excelWorksheet.Cells[9, 2] = lbl_rig.Text;
        excelWorksheet.Cells[10, 2] = txt_companyrep.Text;
        excelWorksheet.Cells[11, 2] = lbl_jobsno.Text;
        excelWorksheet.Cells[12, 2] = lbl_dir_drillers.Text;
        excelWorksheet.Cells[13, 2] = getMWDOperators();

        excelWorksheet.Cells[15, 2] = txt_poppet.Text;
        excelWorksheet.Cells[16, 2] = txt_orifice.Text;
        excelWorksheet.Cells[17, 2] = txt_pulsewidth.Text;
        excelWorksheet.Cells[18, 2] = txt_pulseamplitude.Text;
        excelWorksheet.Cells[19, 2] = txt_totalconnected.Text;
        excelWorksheet.Cells[20, 2] = txt_totalcirc.Text;

        excelWorksheet.Cells[23, 2] = txt_depthstart.Text;
        excelWorksheet.Cells[23, 3] = txt_depthend1.Text;
        excelWorksheet.Cells[24, 2] = txt_incstart.Text;
        excelWorksheet.Cells[24, 3] = txt_incend.Text;
        excelWorksheet.Cells[25, 2] = txt_azmstart.Text;
        excelWorksheet.Cells[25, 3] = txt_azmend.Text;
        excelWorksheet.Cells[26, 2] = txt_magfstart.Text;
        excelWorksheet.Cells[26, 3] = txt_magfend.Text;
        excelWorksheet.Cells[27, 2] = txt_gravstart.Text;
        excelWorksheet.Cells[27, 3] = txt_gravend.Text;
        excelWorksheet.Cells[28, 2] = txt_dipstart.Text;
        excelWorksheet.Cells[28, 3] = txt_dipend.Text;

        excelWorksheet.Cells[30, 2] = txt_temp_c.Text;
        excelWorksheet.Cells[31, 2] = txt_temp_f.Text;
        excelWorksheet.Cells[32, 2] = txt_avr_pump_tressure.Text;
        excelWorksheet.Cells[33, 2] = txt_avre_flow_rate.Text;
        excelWorksheet.Cells[34, 2] = txt_mud_weight.Text;
        excelWorksheet.Cells[35, 2] = txt_solids.Text;
        excelWorksheet.Cells[36, 2] = txt_sand1.Text;


        //24 Hour Activity
        DataTable dt_24hoursInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "SELECT [ActivityId], [RunID], [StartTime],[EndTime], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=" + hidd_runno.Value + "").Tables[0];
        for (int cat = 0; cat < dt_24hoursInfo.Rows.Count; cat++)
        {
            int categno = 8 + cat;

            DataRow[] rowtime = dt24hours.Select("TimeId=" + dt_24hoursInfo.Rows[cat]["StartTime"].ToString());
            DataRow[] rowactivity = dt24hoursActivity.Select("HourActivityId=" + dt_24hoursInfo.Rows[cat]["24HourActivity"].ToString());
            if (rowtime != null)
            {
                excelWorksheet.Cells[categno, 5] = rowtime[0]["Time"].ToString();
            }
            if (rowactivity != null)
            {
                excelWorksheet.Cells[categno, 6] = rowactivity[0]["24HourActivity"].ToString();
            }

            excelWorksheet.Cells[categno, 7] = dt_24hoursInfo.Rows[cat]["Comments"].ToString();
        }

        DataTable dtGetAssets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select AN.AssetName,A.Id as AssetID,* from PrismJobAssignedAssets JA,PrismAssetName AN,Prism_Assets A where JA.AssetId=A.Id" +
                                             " and A.AssetName=AN.Id and  JA.JobId=" + radcombo_job.SelectedValue).Tables[0];
        DataTable dtAssetQuantity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT [AssetQntID],[RunID], [AssetID], [JobId],[AQntty] FROM [PrismJobRunAssetsRequired] where RunID=" + hidd_runno.Value).Tables[0];
        for (int cat = 0; cat < dtAssetQuantity.Rows.Count; cat++)
        {
            int categno = 6 + cat;
            DataRow[] rowasset = dtGetAssets.Select("AssetID=" + dtAssetQuantity.Rows[cat]["AssetID"].ToString());

            if (rowasset != null && rowasset.Length>0)
            {
                excelWorksheet.Cells[categno, 9] = rowasset[0]["AssetName"].ToString();
            }

            excelWorksheet.Cells[categno, 10] = dtAssetQuantity.Rows[cat]["AQntty"].ToString();
        }


        DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        string existrunid = "";
        if (dt_getdailyrun.Rows.Count > 0)
        {
            existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

        }
        //DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca,PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.jobid=" + radcombo_job.SelectedValue + " and pj.AssetStatus=3").Tables[0];
        DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,pa.Id as AID,pj.kitname as KitName, * from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca," +
            " PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.assignmentstatus='Active' and " +
            " pj.jobid=" + radcombo_job.SelectedValue + "  and pj.AssetStatus=3").Tables[0];
        for (int cat = 0; cat < dt_getassetcategories.Rows.Count; cat++)
        {
            int categno = 40 + cat;
            //for (int exc = 1; exc < 9; exc++)
            //{

            excelWorksheet.Cells[categno, 1] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            excelWorksheet.Cells[categno, 2] = dt_getassetcategories.Rows[cat]["NameofAsset"].ToString();
            excelWorksheet.Cells[categno, 3] = dt_getassetcategories.Rows[cat]["SerialNumber"].ToString();
            excelWorksheet.Cells[categno, 4] = dt_getassetcategories.Rows[cat]["KitName"].ToString();
            if (existrunid != "")
            {
                DataTable dt_getdetfrm = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "").Tables[0];
                if (dt_getdetfrm.Rows.Count > 0)
                {
                    if (dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString() != "")
                    {
                        excelWorksheet.Cells[categno, 5] = "Yes";
                        excelWorksheet.Cells[categno, 9] = dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString();
                    }
                    else
                    {
                        excelWorksheet.Cells[categno, 5] = "No";
                    }

                }
            }
            else
            {
                //query_runhrdet = "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "";
            }


            excelWorksheet.Cells[categno, 6] = dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString();

            DataTable dt_gettotalrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(dailyrunhrs) as TotalRunHrs from PrismJobRunHourdetails pjr,PrismJobRunDetails r where r.runid=pjr.runid and pjr.assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and r.Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
            if (dt_gettotalrunhrs.Rows.Count > 0)
            {
                excelWorksheet.Cells[categno, 7] = dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString();
                decimal d;
                if (dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString() != "")
                {
                    d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString()) + Convert.ToDecimal(dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString());
                }
                else
                {
                    d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString());
                }
                excelWorksheet.Cells[categno, 8] = d.ToString();
                //string existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

            }
            //= dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            //excelWorksheet.Cells[cat, 7] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            //excelWorksheet.Cells[cat, 8] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            //}
        }

        //txt_dailycharges
        workbook.Save();

        //workbook.SaveAs(Server.MapPath("ExportDailyRunReport.xlsx"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        workbook.Close(true, misValue, misValue);
        excelApp.Quit();
        //while (Marshal.ReleaseComObject(excelApp) != 0) { }
        //excelApp = null;
        //GC.Collect();
        //GC.WaitForPendingFinalizers();

        Process[] pros = Process.GetProcesses();
        for (int i = 0; i < pros.Count(); i++)
        {
            if (pros[i].ProcessName.ToLower().Contains("excel"))
            {
                pros[i].Kill();
            }
        }




        string FilePath = Server.MapPath("ExportDailyRunReport_copy.xlsx");
        FileInfo fileInfo = new FileInfo(FilePath);
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.Flush();
        Response.WriteFile(fileInfo.FullName);
        Response.End();

        //excelApp.Quit();

    }
    public void clear()
    {
        chk_runfinish.Checked = false;
        runIdExist = false;
        //radcombo_job.ClearSelection();
        pnl_addjobs.Controls.Clear();
        //radcombo_hr1.ClearSelection();
        //radcombo_hr2.ClearSelection();
        //radcombo_hr3.ClearSelection();
        //radcombo_hr4.ClearSelection();
        //radcombo_hr5.ClearSelection();
        //radcombo_hr6.ClearSelection();
        //radcombo_hr7.ClearSelection();
        //radcombo_hr8.ClearSelection();
        //radcombo_hr9.ClearSelection();
        //radcombo_hr10.ClearSelection();
        //radcombo_hr11.ClearSelection();
        //radcombo_hr12.ClearSelection();
        //radcombo_hr13.ClearSelection();
        //radcombo_hr14.ClearSelection();
        //radcombo_hr15.ClearSelection();
        //radcombo_hr16.ClearSelection();
        //radcombo_hr17.ClearSelection();
        //radcombo_hr18.ClearSelection();
        //radcombo_hr19.ClearSelection();
        //radcombo_hr20.ClearSelection();
        //radcombo_hr21.ClearSelection();
        //radcombo_hr22.ClearSelection();
        //radcombo_hr23.ClearSelection();
        //radcombo_hr24.ClearSelection();
        //txt_depthstrt.Text = "";
        //txt_depthend.Text = "";
        //txt_lastinc.Text = "";
        //txt_lastazm.Text = "";
        //txt_lasttemp.Text = "";
        //txt_cmts.Text = "";
        //txt_pumppressure.Text = "";
        //txt_flowrate.Text = "";
        //txt_mudwght.Text = "";
        //txt_floride.Text = "";
        //txt_pulseamp.Text = "";
        //txt_sand.Text = "";
        //txt_solid.Text = "";
        //radcombo_assetneeded1.ClearSelection();
        //radcombo_assetneeded2.ClearSelection();
        //radcombo_assetneeded3.ClearSelection();
        //radcombo_assetneeded4.ClearSelection();
        //radcombo_assetneeded5.ClearSelection();
        //txt_othersuppliesneeded1.Text = "";
        //txt_othersuppliesneeded2.Text = "";
        //txt_othersuppliesneeded3.Text = "";
        //txt_othersuppliesneeded4.Text = "";
        //txt_othersuppliesneeded5.Text = "";
        txt_dailycharges.Text = "";
        txt_companyrep.Text = string.Empty;
        txt_poppet.Text = string.Empty;
        txt_orifice.Text = string.Empty;
        txt_pulsewidth.Text = string.Empty;
        txt_pulseamplitude.Text = string.Empty;
        txt_totalconnected.Text = string.Empty;
        txt_totalcirc.Text = string.Empty;
        txt_depthstart.Text = string.Empty;
        txt_depthend1.Text = string.Empty;
        txt_incstart.Text = string.Empty;
        txt_azmstart.Text = string.Empty;
        txt_magfstart.Text = string.Empty;
        txt_gravstart.Text = string.Empty;
        txt_dipstart.Text = string.Empty;

        txt_incend.Text = string.Empty;
        txt_azmend.Text = string.Empty;
        txt_magfend.Text = string.Empty;
        txt_gravend.Text = string.Empty;
        txt_dipend.Text = string.Empty;
        txt_temp_c.Text = string.Empty;
        txt_temp_f.Text = string.Empty;
        txt_avr_pump_tressure.Text = string.Empty;
        txt_avre_flow_rate.Text = string.Empty;
        txt_mud_weight.Text = string.Empty;
        txt_solids.Text = string.Empty;
        txt_sand1.Text = string.Empty;

    }
    protected void btn_Create_Click(object sender, EventArgs e)
    {
        DataTable dt_PrismJobRunDetailsFormInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Prism24HourActivity").Tables[0];
        DataRow[] row_form = dt_PrismJobRunDetailsFormInfo.Select("[24HourActivity]='" + txt_activity.Text + "'");
        if (row_form.Length == 0)
        {
            string queryInsert = "Insert into Prism24HourActivity (24HourActivity) values ('" + txt_activity.Text + "')";
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
        }
        else
        {

            lbl_mes.Text = "Activity already Exist, try with another name";
            window_activity.Visible = true;
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {

    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        //  string existrunid = "";
        if (dt_getdailyrun.Rows.Count == 0)
        {
            //if (runIdExist == false)
            //{

            if (chk_runfinish.Checked)
            {
                DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                if (dt_getrunidnextval.Rows.Count > 0)
                {
                    string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, addvalueexist);
                }
            }

            string runnumber = "", runstatus = "";
            int addrunval = 1;
            string selectq = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date in (select MAX(Date) from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date  <= '" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "')";
            DataTable dt_getruncnt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
            if (dt_getruncnt.Rows.Count > 0)
            {
                runnumber = dt_getruncnt.Rows[0]["runnumber"].ToString();
                runstatus = dt_getruncnt.Rows[0]["finished"].ToString();
                if (runstatus == "True")
                {
                    addrunval = Convert.ToInt32(runnumber) + 1;
                }
                else
                {
                    addrunval = Convert.ToInt32(runnumber);
                }
            }
            //string path = Server.MapPath("~/Documents/");
            //string filename = "", completepath = "";

            string insertquery_jobrundet = "insert into PrismJobRunDetails(jid,Date,runnumber,updateddate)values(" +
            "" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(radtxt_start.SelectedDate.ToString()) + "'," +
            "" + addrunval + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";

            int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertquery_jobrundet);
            DataTable dt_runid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    PrismJobRunDetails WHERE  runid = IDENT_CURRENT('PrismJobRunDetails')").Tables[0];

            string runid = dt_runid.Rows[0]["runid"].ToString();
            hidd_runno.Value = runid;
            //hid_runid.Value = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");
            // grid_hours.Rebind();
        }
        //string insertActivity = "Insert into PrismJobRun24HourActivityLog([RunID], [Time], [24HourActivity],[Comments])" +
        //    " values (" + hidd_runno.Value + ",'" + combo_time.SelectedValue + "','" + combo_activity.SelectedValue + "','" + txt_comments.Text + "')";
        //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertActivity);
        //lbl_success.Text = "Activity info inserted successfully";
        btn_view_Click(null, null);

        grid_hours.Rebind();
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {

    }
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DailyRunReport_PRISM.aspx");
        ////bind();
        //txt_assetsno.Text = "";
        //radcombo_rstatus.SelectedValue = "Select Job";
        //combo_job.ClearSelection();
        //combo_job.DataBind();
        //// radgrid_repairstatus.DataBind();
        //radcombo_rstatus.SelectedValue = "0";
        //// radcombo_assetcat.DataBind();

    }
}