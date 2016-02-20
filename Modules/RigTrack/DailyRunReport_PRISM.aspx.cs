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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
public partial class Modules_Configuration_Manager_DailyRunReport : System.Web.UI.Page
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

            radcombo_job.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            radcombo_job.DataTextField = "CurveGroupName";
            radcombo_job.DataValueField = "ID";
            radcombo_job.DataBind();
            
            dt24hours = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT [TimeId], convert(varchar,[Time]) as [Time] FROM [Prism24Hours]").Tables[0];
            dt24hoursActivity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT [HourActivityId], [24HourActivity] FROM [Prism24HourActivity]").Tables[0];
        }
        dt_Users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Users").Tables[0];
        dt_rig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from RigTypes").Tables[0];
        dt_mwdoperators = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId").Tables[0];
        string a = radtxt_start.SelectedDate.ToString();

        if (a != "")
        {
            bindtextboxesdates();
        }
        lbl_message.Text = "";
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            radcombo_job.Items.Clear();
            //radcombo_job.Items.Add(new RadComboBoxItem("Select", "0"));
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
            //radcombo_job.Items.Clear();
            
            radcombo_job.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            radcombo_job.DataTextField = "CurveGroupName";
            radcombo_job.DataValueField = "ID";
            radcombo_job.DataBind();
            radcombo_job.Items.Add(new RadComboBoxItem("Select", "0"));
            radcombo_job.SelectedValue = "0";
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
                    DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                    if (dt_getrunidnextval.Rows.Count > 0)
                    {
                        string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
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
                    DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                    if (dt_getrunidnextval.Rows.Count > 0)
                    {
                        string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
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
        //clear();
        txtEnteredUser.Text = ((Label)Master.FindControl("lbl_welcomename")).Text;
        //lbl_message.Text = "";
        hidd_runno.Value = "0";
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select state.Name as StateName,country.Name as CountryName,cmp.CompanyName as CompanyName, * from [RigTrack].tblCurveGroup m,[RigTrack].[tlkpState] state,[RigTrack].[tlkpCountry] country,[RigTrack].[tblCompany] cmp where state.ID=m.StateID and country.ID=m.CountryID and " +
            " m.CompanyID=cmp.ID and m.ID=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + radcombo_job.SelectedValue;
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
            " and pp.JobId=" + radcombo_job.SelectedValue + "";


            //RadComboBoxFill.FillRadcombobox(combo_ex_personal, dt_personals, "Personname", "userid", "0");
            pnl_addjobs.Visible = true;
            td_dailylog.Visible = true;
            tdbottmombuttons.Visible = true;
            tdtopbuttons.Visible = true;
            btn_save.Visible = true;
            btn_saveupdate2.Visible = true;
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            string existrunid = "";
            if (dt_getdailyrun.Rows.Count > 0)
            {
                txt_day.Text = dt_getdailyrun.Rows[0]["MwdHandDay"].ToString();
                txt_night.Text = dt_getdailyrun.Rows[0]["MwdHandNight"].ToString();
                runIdExist = true;
                existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();
                hidd_runno.Value = existrunid;
                // SqlDataSource1.SelectCommand = "SELECT [ActivityId], [RunID], [Time], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=" + existrunid;
                lbl_runnumber.Text = dt_getdailyrun.Rows[0]["runnumber"].ToString();
                hid_runno.Value = dt_getdailyrun.Rows[0]["runnumber"].ToString();
                //lnk_download.Text = dt_getdailyrun.Rows[0]["attachmentname"].ToString();
                //string path = Server.MapPath("~/Documents/");
                //string docnamewithpath = path + dt_getdailyrun.Rows[0]["attachmentname"].ToString();
                //lnk_download.Attributes.Add("onclick", "return downloadFile('" + docnamewithpath + "','" + dt_getdailyrun.Rows[0]["attachmentname"].ToString() + "');");
                DataTable dt_getdaynumber = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                    "select * from PrismJobRunDetails where Date<'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + " and runnumber=" + dt_getdailyrun.Rows[0]["runnumber"].ToString() + "").Tables[0];
                //DataRow[] dr_dayno = dt_getdailyrun.Select("runnumber=" + lbl_runnumber.Text + "");
                if (dt_getdaynumber.Rows.Count > 0)
                {
                    lbl_daynumber.Text = (Convert.ToInt32(dt_getdaynumber.Rows.Count) + 1).ToString();
                }
                else
                {
                    lbl_daynumber.Text = "1";
                }
                txt_dailycharges.Text = dt_getdailyrun.Rows[0]["dailycharges"].ToString();
                if (dt_getdailyrun.Rows[0]["finished"].ToString() == "True")
                {
                    chk_runfinish.Checked = true;
                }
                //chk_runfinish.
                //bindFormInfo(existrunid);
                //activitylogbind(existrunid);
                //binddailyprogress(existrunid);
                //binddrillingparams(existrunid);
                //bindothersuppliesneeded(existrunid);
                //bindassetsneeded(existrunid);

            }
            else
            {
                runIdExist = false;
                string selectq = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date in (select MAX(Date) from " +
                    " PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and " +
                    " Date  <= '" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "')";
                DataTable dt_getruncnt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
                if (dt_getruncnt.Rows.Count > 0)
                {
                    if (dt_getruncnt.Rows[0]["runid"].ToString() != "")
                    {
                        bindFormInfo(dt_getruncnt.Rows[0]["runid"].ToString());

                        string runnumber = "", runstatus = "";
                        int addrunval = 1;
                        //string selectq = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date in (select MAX(Date) from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date  <= '" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "')";
                        //DataTable dt_getruncnt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
                        //if (dt_getruncnt.Rows.Count > 0)
                        //{
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
                        //}
                        //string path = Server.MapPath("~/Documents/");
                        //string filename = "", completepath = "";

                        string insertquery_jobrundet = "insert into PrismJobRunDetails(jid,Date,runnumber,updateddate)values(" +
                        "" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(radtxt_start.SelectedDate.ToString()) + "'," +
                        "" + addrunval + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";

                        int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertquery_jobrundet);
                        DataTable dt_runid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    PrismJobRunDetails WHERE  runid = IDENT_CURRENT('PrismJobRunDetails')").Tables[0];

                        string runid =  dt_runid.Rows[0]["runid"].ToString();
                        hidd_runno.Value = runid;

                        // hidd_runno.Value = dt_getruncnt.Rows[0]["runid"].ToString();
                        //SqlDataSource1.SelectCommand = "SELECT [ActivityId], [RunID], [Time], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=" + dt_getruncnt.Rows[0]["runid"].ToString();
                    }
                    if (dt_getruncnt.Rows[0]["finished"].ToString() == "True")
                    {
                        lbl_runnumber.Text = (Convert.ToInt32(dt_getruncnt.Rows[0]["runnumber"].ToString()) + 1).ToString();
                    }
                    else
                    {
                        lbl_runnumber.Text = dt_getruncnt.Rows[0]["runnumber"].ToString();
                    }
                    DataTable dt_getdaynumber = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date<'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + " and runnumber=" + lbl_runnumber.Text + "").Tables[0];
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
                else
                {
                    lbl_runnumber.Text = "1";
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
            bindtextboxesdates();
            binddocuments();
            radgrdMeterList.Visible = true;
            radgrdMeterList.DataSource = RefreshMeterList();
            radgrdMeterList.DataBind();
            //
        }
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
        string query = "select p.Id,AssetId,PPA.BHAGroupName as AssetName,P.BHAGroupId as asset_id,c.Name as warehouse,ca.ToolTypeName as assetcategory,SerialNumber," +
             "Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.PinTop,p.PinBottom,p.Comments,p.Description from [RigTrack].[Prism_BHAAssets] p,PrsimWarehouses c,[RigTrack].[BHAToolTypes] ca,[RigTrack].[tblCreateBHAToolGroup] PPA" +
              " where p.WarehouseId=c.ID and p.BHATypeId=ca.ToolTypeID and P.[BHAGroupId]=PPA.Id" +
              " and p.ID in(SELECT b.ToolID FROM [RigTrack].[tblBHADataInfo] a,[RigTrack].[tblBHADataItemsInfo] b where a.ID=b.BHAID and a.JOBID="+radcombo_job.SelectedValue+")";
        query += " order by p.Id desc";
        
        DataTable dt_PrismJobRunDetailsFormInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        

        // Set the RADGrid's DataSource to the DataTable
        return dt_PrismJobRunDetailsFormInfo;

    }
    public void bindtextboxesdates()
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
            if (dt_getdailyrun.Rows.Count > 0)
            {
                existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

            }
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


                DataTable dt_gettotalrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(dailyrunhrs) as TotalRunHrs from PrismJobRunHourdetails pjr,PrismJobRunDetails r where r.runid=pjr.runid and pjr.assetid=" + dt_getassetcategories.Rows[cat]["AID"].ToString() + " and r.Date<='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
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
        
    }

    public void activitylogbind(string existrunid)
    {
        DataTable dt_getdetactiveloghrvalues = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobRunActivityLog where runid=" + existrunid + " order by hour asc").Tables[0];
        if (dt_getdetactiveloghrvalues.Rows.Count > 0)
        {
            


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
                        DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
                        if (dt_getrunidnextval.Rows.Count > 0)
                        {
                            string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + ")";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, addvalueexist);
                        }
                    }
                }
                else
                {
                    if (finished == "False")
                    {
                        DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                        if (dt_getrunidnextval.Rows.Count > 0)
                        {
                            string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber-1 where runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + ")";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, addvalueexist);
                        }
                    }
                }
                string notificationsendtowhome = eventNotification.sendEventNotification("DRR01");
                if (notificationsendtowhome != "")
                {
                    //bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "DRR01", "JOB", radcombo_job.SelectedValue, radcombo_job.SelectedItem.Text,
                    //       "", "", "DailyRunInsert", "", "");
                }
            }
            else
            {
                string notificationsendtowhome = eventNotification.sendEventNotification("DRR02");
                if (notificationsendtowhome != "")
                {
                    //bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "DRR02", "JOB", radcombo_job.SelectedValue, radcombo_job.SelectedItem.Text,
                    //       "", "", "DailyRunUpdate", "", "");
                }
                if (finished == "True")
                {
                    DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                    if (dt_getrunidnextval.Rows.Count > 0)
                    {
                        string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
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
                

            }
            //ATTACHMENT

            DataTable dt_PrismJobRunDetailsFormInfo = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "Select * from PrismJobRunDetailsFormInfo").Tables[0];
            DataRow[] row_form = dt_PrismJobRunDetailsFormInfo.Select("Jobid=" + radcombo_job.SelectedValue + " and RunId=" + runid + "");
            if (row_form.Length == 0)
            {
                string inset_PrismJobRunDetailsFormInfo = "Insert into PrismJobRunDetailsFormInfo(Jobid,RunId,CompanyRep,POPPET_SIZE,ORIFICE_SIZE,PULSE_WIDTH,PULSE_AMPLITUDE,TOTAL_CONNECTED," +
                "TOTAL_CIRC,DEPTH_START,DEPTH_END,INCStart,AZMStart,MAGFStart,GRAVStart,DIPStart,INCEnd,AZMEnd,MAGFEnd,GRAVEnd,DIPEnd,TEMPERATURE_C,TEMPERATURE_F,AVER_PUMP_PRESSURE,AVER_FLOW_RATE,MUD_WEIGHT,SOLIDS,SAND,"+
                "HoleSize,DayStartDepth,MidnightDepth,AgitatorInUse,AgitatorDistance,DayStartAMP,AMPHrsUsedToday,AmpHrsRemaining,DayEndVoltsBAT1,DayStartVoltsBAT1,DayEndVoltsBAT2,DayStartVoltsBAT2)" +
                " Values(" + radcombo_job.SelectedValue + "," + runid + ",'" + txt_companyrep.Text + "','" + txt_poppet.Text + "','" + txt_orifice.Text + "','" + txt_pulsewidth.Text + "','" + txt_pulseamplitude.Text + "'," +
                "'" + txt_totalconnected.Text + "','" + txt_totalcirc.Text + "','" + txt_depthstart.Text + "','" + txt_depthend1.Text + "','" + txt_incstart.Text + "','" + txt_azmstart.Text + "','" + txt_magfstart.Text + "','" + txt_gravstart.Text + "','" + txt_dipstart.Text + "'," +
                "'" + txt_incend.Text + "','" + txt_azmend.Text + "','" + txt_magfend.Text + "','" + txt_gravend.Text + "','" + txt_dipend.Text + "','" + txt_temp_c.Text + "','" + txt_temp_f.Text + "','" + txt_avr_pump_tressure.Text + "'," +
                "'" + txt_avre_flow_rate.Text + "','" + txt_mud_weight.Text + "','" + txt_solids.Text + "','" + txt_sand1.Text + "',"+
                "'" + txtHoleSize.Text + "','" + txtDayStartDepth.Text + "','" + txtMidNightDepth.Text + "','" + ddlAgrigatorInUse.SelectedValue.ToString() + "',"+
                "'" + txtAgrigatorDistance.Text + "','" + txtDayStartAmpHrs.Text + "','" + txtAmpHrsUsedTodat.Text + "','"+txtAmpHrsRemaining.Text+"','"+txtDayEndVoltsBat1.Text+"'"+
                ",'" + txtDayStartVoltsBat1.Text + "','" + txtDayEndVoltsBat1.Text + "','" + txtDayEndVoltsBat2.Text + "')";
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
                        " SOLIDS='" + txt_solids.Text + "',SAND='" + txt_sand1.Text + "'"+
                        "HoleSize='"+txtHoleSize.Text+"',DayStartDepth='"+txtDayStartDepth.Text+"',MidnightDepth='"+txtMidNightDepth.Text+"'," +
                        "AgitatorInUse='"+ddlAgrigatorInUse.SelectedValue+"',AgitatorDistance='"+txtAgrigatorDistance.Text+"',DayStartAMP='"+txtDayStartAmpHrs.Text+"',AMPHrsUsedToday='"+txtAmpHrsUsedTodat.Text+"'," +
                        "AmpHrsRemaining='" + txtAmpHrsRemaining.Text + "',DayEndVoltsBAT1='" + txtDayEndVoltsBat1.Text + "',DayStartVoltsBAT1='" + txtDayStartVoltsBat1.Text + "'" +
                        "DayEndVoltsBAT2='" + txtDayEndVoltsBat1.Text + "',DayStartVoltsBAT2='" + txtDayEndVoltsBat2.Text + "'" +
                        " where Jobid=" + radcombo_job.SelectedValue + " and RunId=" + runid + "";
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btn_export_OnClick(object sender, EventArgs e)
    {
        //ScriptManager sm = ScriptManager.GetCurrent(Page);
        //sm.RegisterScriptControl(txt_companyrep);
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //StringWriter stringWriter = new StringWriter();
        //HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        ////this.EnableViewState = false;
        ////Response.Charset = String.Empty;
        //divContent.RenderControl(htmlTextWriter);

        //StringReader stringReader = new StringReader(stringWriter.ToString());
        //Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(Doc);
        //PdfWriter.GetInstance(Doc, Response.OutputStream);

        //Doc.Open();
        //htmlparser.Parse(stringReader);
        //Doc.Close();
        //Response.Write(Doc);
        //Response.End();


        //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //excelApp.ScreenUpdating = false;
        //excelApp.DisplayAlerts = false;

        //object misValue = System.Reflection.Missing.Value;
        //if (File.Exists(Server.MapPath("ExportDailyRunReport_copy.xlsx")))
        //{
        //    File.Delete(Server.MapPath("ExportDailyRunReport_copy.xlsx"));
        //}
        //File.Copy(Server.MapPath("ExportDailyRunReport.xlsx"), Server.MapPath("ExportDailyRunReport_copy.xlsx"));
        ////Opening Excel file(myData.xlsx)
        //Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("ExportDailyRunReport_copy.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
        //string currentSheet = "Sheet2";
        //Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
        //Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;

        //excelWorksheet.Cells[4, 1] = lbl_jname.Text;
        //excelWorksheet.Cells[4, 2] = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");
        //excelWorksheet.Cells[4, 3] = lbl_sdate.Text;
        //excelWorksheet.Cells[4, 4] = lbl_edate.Text;
        //excelWorksheet.Cells[4, 5] = hid_runno.Value;

        //excelWorksheet.Cells[4, 6] = lbl_daynumber.Text;

        //excelWorksheet.Cells[6, 2] = lbl_operator.Text;
        //excelWorksheet.Cells[7, 2] = lbl_well.Text;
        //excelWorksheet.Cells[8, 2] = lbl_location.Text;
        //excelWorksheet.Cells[9, 2] = lbl_rig.Text;
        //excelWorksheet.Cells[10, 2] = txt_companyrep.Text;
        //excelWorksheet.Cells[11, 2] = lbl_jobsno.Text;
        //excelWorksheet.Cells[12, 2] = lbl_dir_drillers.Text;
        //excelWorksheet.Cells[13, 2] = getMWDOperators();

        //excelWorksheet.Cells[15, 2] = txt_poppet.Text;
        //excelWorksheet.Cells[16, 2] = txt_orifice.Text;
        //excelWorksheet.Cells[17, 2] = txt_pulsewidth.Text;
        //excelWorksheet.Cells[18, 2] = txt_pulseamplitude.Text;
        //excelWorksheet.Cells[19, 2] = txt_totalconnected.Text;
        //excelWorksheet.Cells[20, 2] = txt_totalcirc.Text;

        //excelWorksheet.Cells[23, 2] = txt_depthstart.Text;
        //excelWorksheet.Cells[23, 3] = txt_depthend1.Text;
        //excelWorksheet.Cells[24, 2] = txt_incstart.Text;
        //excelWorksheet.Cells[24, 3] = txt_incend.Text;
        //excelWorksheet.Cells[25, 2] = txt_azmstart.Text;
        //excelWorksheet.Cells[25, 3] = txt_azmend.Text;
        //excelWorksheet.Cells[26, 2] = txt_magfstart.Text;
        //excelWorksheet.Cells[26, 3] = txt_magfend.Text;
        //excelWorksheet.Cells[27, 2] = txt_gravstart.Text;
        //excelWorksheet.Cells[27, 3] = txt_gravend.Text;
        //excelWorksheet.Cells[28, 2] = txt_dipstart.Text;
        //excelWorksheet.Cells[28, 3] = txt_dipend.Text;

        //excelWorksheet.Cells[30, 2] = txt_temp_c.Text;
        //excelWorksheet.Cells[31, 2] = txt_temp_f.Text;
        //excelWorksheet.Cells[32, 2] = txt_avr_pump_tressure.Text;
        //excelWorksheet.Cells[33, 2] = txt_avre_flow_rate.Text;
        //excelWorksheet.Cells[34, 2] = txt_mud_weight.Text;
        //excelWorksheet.Cells[35, 2] = txt_solids.Text;
        //excelWorksheet.Cells[36, 2] = txt_sand1.Text;


        ////24 Hour Activity
        //DataTable dt_24hoursInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //    "SELECT [ActivityId], [RunID], [StartTime],[EndTime], [24HourActivity],[Comments] FROM [PrismJobRun24HourActivityLog] where RunID=" + hidd_runno.Value + "").Tables[0];
        //for (int cat = 0; cat < dt_24hoursInfo.Rows.Count; cat++)
        //{
        //    int categno = 8 + cat;

        //    DataRow[] rowtime = dt24hours.Select("TimeId=" + dt_24hoursInfo.Rows[cat]["StartTime"].ToString());
        //    DataRow[] rowactivity = dt24hoursActivity.Select("HourActivityId=" + dt_24hoursInfo.Rows[cat]["24HourActivity"].ToString());
        //    if (rowtime != null)
        //    {
        //        excelWorksheet.Cells[categno, 5] = rowtime[0]["Time"].ToString();
        //    }
        //    if (rowactivity != null)
        //    {
        //        excelWorksheet.Cells[categno, 6] = rowactivity[0]["24HourActivity"].ToString();
        //    }

        //    excelWorksheet.Cells[categno, 7] = dt_24hoursInfo.Rows[cat]["Comments"].ToString();
        //}

        //DataTable dtGetAssets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select AN.AssetName,A.Id as AssetID,* from PrismJobAssignedAssets JA,PrismAssetName AN,Prism_Assets A where JA.AssetId=A.Id" +
        //                                     " and A.AssetName=AN.Id and  JA.JobId=" + radcombo_job.SelectedValue).Tables[0];
        //DataTable dtAssetQuantity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT [AssetQntID],[RunID], [AssetID], [JobId],[AQntty] FROM [PrismJobRunAssetsRequired] where RunID=" + hidd_runno.Value).Tables[0];
        //for (int cat = 0; cat < dtAssetQuantity.Rows.Count; cat++)
        //{
        //    int categno = 6 + cat;
        //    DataRow[] rowasset = dtGetAssets.Select("AssetID=" + dtAssetQuantity.Rows[cat]["AssetID"].ToString());

        //    if (rowasset != null)
        //    {
        //        excelWorksheet.Cells[categno, 9] = rowasset[0]["AssetName"].ToString();
        //    }

        //    excelWorksheet.Cells[categno, 10] = dtAssetQuantity.Rows[cat]["AQntty"].ToString();
        //}


        //DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        //string existrunid = "";
        //if (dt_getdailyrun.Rows.Count > 0)
        //{
        //    existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

        //}
        ////DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca,PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.jobid=" + radcombo_job.SelectedValue + " and pj.AssetStatus=3").Tables[0];
        //DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        //    "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,pa.Id as AID,pj.kitname as KitName, * from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca," +
        //    " PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.assignmentstatus='Active' and " +
        //    " pj.jobid=" + radcombo_job.SelectedValue + "  and pj.AssetStatus=3").Tables[0];
        //for (int cat = 0; cat < dt_getassetcategories.Rows.Count; cat++)
        //{
        //    int categno = 40 + cat;
        //    //for (int exc = 1; exc < 9; exc++)
        //    //{

        //    excelWorksheet.Cells[categno, 1] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
        //    excelWorksheet.Cells[categno, 2] = dt_getassetcategories.Rows[cat]["NameofAsset"].ToString();
        //    excelWorksheet.Cells[categno, 3] = dt_getassetcategories.Rows[cat]["SerialNumber"].ToString();
        //    excelWorksheet.Cells[categno, 4] = dt_getassetcategories.Rows[cat]["KitName"].ToString();
        //    if (existrunid != "")
        //    {
        //        DataTable dt_getdetfrm = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "").Tables[0];
        //        if (dt_getdetfrm.Rows.Count > 0)
        //        {
        //            if (dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString() != "")
        //            {
        //                excelWorksheet.Cells[categno, 5] = "Yes";
        //                excelWorksheet.Cells[categno, 9] = dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString();
        //            }
        //            else
        //            {
        //                excelWorksheet.Cells[categno, 5] = "No";
        //            }

        //        }
        //    }
        //    else
        //    {
        //        //query_runhrdet = "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "";
        //    }


        //    excelWorksheet.Cells[categno, 6] = dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString();

        //    DataTable dt_gettotalrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(dailyrunhrs) as TotalRunHrs from PrismJobRunHourdetails pjr,PrismJobRunDetails r where r.runid=pjr.runid and pjr.assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and r.Date<='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
        //    if (dt_gettotalrunhrs.Rows.Count > 0)
        //    {
        //        excelWorksheet.Cells[categno, 7] = dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString();
        //        decimal d;
        //        if (dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString() != "")
        //        {
        //            d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString()) + Convert.ToDecimal(dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString());
        //        }
        //        else
        //        {
        //            d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString());
        //        }
        //        excelWorksheet.Cells[categno, 8] = d.ToString();
        //        //string existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

        //    }
        //    //= dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
        //    //excelWorksheet.Cells[cat, 7] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
        //    //excelWorksheet.Cells[cat, 8] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
        //    //}
        //}

        ////txt_dailycharges
        //workbook.Save();

        ////workbook.SaveAs(Server.MapPath("ExportDailyRunReport.xlsx"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //workbook.Close(true, misValue, misValue);
        //excelApp.Quit();
        ////while (Marshal.ReleaseComObject(excelApp) != 0) { }
        ////excelApp = null;
        ////GC.Collect();
        ////GC.WaitForPendingFinalizers();

        //Process[] pros = Process.GetProcesses();
        //for (int i = 0; i < pros.Count(); i++)
        //{
        //    if (pros[i].ProcessName.ToLower().Contains("excel"))
        //    {
        //        pros[i].Kill();
        //    }
        //}




        //string FilePath = Server.MapPath("ExportDailyRunReport_copy.xlsx");
        //FileInfo fileInfo = new FileInfo(FilePath);
        //Response.Clear();
        //Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
        //Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        //Response.ContentType = "application/octet-stream";
        //Response.Flush();
        //Response.WriteFile(fileInfo.FullName);
        //Response.End();

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
                DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                if (dt_getrunidnextval.Rows.Count > 0)
                {
                    string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + " and jid=" + radcombo_job.SelectedValue + "')";
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