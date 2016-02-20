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
using System.Data.OleDb;
using System.Globalization;

public partial class Modules_Configuration_Manager_ImportDailyRunReport : System.Web.UI.Page
{
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    //SqlCommand cmdInsert;
    SqlTransaction transaction;
    public static DataTable dt_Users, dt_rig, dt_mwdoperators, dt_Prism24HourActivity, dt_AssetReq, dt_Prism24Hours;
    
    string tdstyle = "style='border:solid 1px #000000;text-align:center;background-color:#658851; color:#121212'";
    string tdfiledstyle = "style='border:solid 1px #000000;text-align:center;background-color:#C4D79B; color:#121212'";
    bool runIdExist = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               "select * from manageJobOrders where status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>'' and getdate() between startdate and enddate").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "jobname", "jid", "0");
            dt_Prism24HourActivity=SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(),CommandType.Text,"Select * from Prism24HourActivity").Tables[0];
            dt_AssetReq = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
              //" select AN.AssetName,A.Id as AssetID,* from PrismJobAssignedAssets JA,PrismAssetName AN,Prism_Assets A where JA.AssetId=A.Id and A.AssetName=AN.Id ").Tables[0];
              " select AssetName,Id as AssetID,* from PrismAssetName  ").Tables[0];
            dt_Prism24Hours = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Prism24Hours").Tables[0];
            dt_Users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Users").Tables[0];
            dt_rig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from RigTypes").Tables[0];
            dt_mwdoperators = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId").Tables[0];
        }
        string a = radtxt_start.SelectedDate.ToString();
        if (a != "")
        {
            bindtextboxesdates();
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
            if (runIdExist == false)
            {

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
            }
        }
    }
   public void bindImportedData()
    {
      

        lbl_message.Text = "";
        hidd_runno.Value = "0";
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j,RigTypes r where m.jobtype=j.jobtypeid and " +
            " m.rigtypeid=r.rigtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + radcombo_job.SelectedValue;
            td_jobdet.Attributes.Add("style", "display:block");
            lbl_jname.Text = dtdates.Rows[0]["jobname"].ToString();
            lbl_jtype.Text = dtdates.Rows[0]["jtypename"].ToString();
            lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString()).ToString("MM/dd/yyyy");
            lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString()).ToString("MM/dd/yyyy");
            lbl_rigtype.Text = dtdates.Rows[0]["rigtypename"].ToString();

            lbl_address.Text = dtdates.Rows[0]["primaryAddress1"].ToString() + " " + dtdates.Rows[0]["primaryAddress2"].ToString();
            lbl_city.Text = dtdates.Rows[0]["primaryCity"].ToString();
            lbl_state.Text = dtdates.Rows[0]["primaryState"].ToString();
            lbl_country.Text = dtdates.Rows[0]["primaryCountry"].ToString();
            lbl_zip.Text = dtdates.Rows[0]["primaryPostalCode"].ToString();
            string query_personals = " select u.userid, (firstname+lastname)+' ('+userRole+')' as Personname from users u, UserRoles ur,PrismJobAssignedPersonals pp" +
            " where  u.userRoleID=ur.userRoleID and pp.UserId=u.userID" +
            " and pp.JobId=" + radcombo_job.SelectedValue + "";


            //RadComboBoxFill.FillRadcombobox(combo_ex_personal, dt_personals, "Personname", "userid", "0");
            pnl_addjobs.Visible = true;
            td_dailylog.Visible = true;
            
            
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            string existrunid = "";
            if (dt_getdailyrun.Rows.Count > 0)
            {
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
                //txt_dailycharges.Text = dt_getdailyrun.Rows[0]["dailycharges"].ToString();
                if (dt_getdailyrun.Rows[0]["finished"].ToString() == "True")
                {
                    chk_runfinish.Checked = true;
                }
                //chk_runfinish.
                bindFormInfo(existrunid);
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
                        hidd_runno.Value = dt_getruncnt.Rows[0]["runid"].ToString();
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
                }

            }
            //DataTable dt_personals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_personals).Tables[0];
            //combo_ex_personal.DataSource = dt_personals;
            //combo_ex_personal.DataBind();
            //if (existrunid != "")
            //{
            //    DataTable dt_existpersonals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunPersonals where runid=" + existrunid + "").Tables[0];
            //    for (int p = 0; p < dt_existpersonals.Rows.Count; p++)
            //    {
            //        foreach (RadComboBoxItem item in combo_ex_personal.Items)
            //        {
            //            if (item.Value == dt_existpersonals.Rows[p]["personid"].ToString())
            //            {
            //                item.Checked = true;
            //            }
            //        }
            //    }
            //}
            bindtextboxesdates();
            //binddocuments();
            //
        }
    }
    public void bindtextboxesdates()
    {
        pnl_addjobs.Controls.Clear();

        DateTime dtstart = new DateTime();
        DateTime dtend = new DateTime();
        DateTime dtstart_service = new DateTime();
        DateTime dtend_service = new DateTime();
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        DataTable dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select pa.AssetName AS AssetName1,* from Prism_Assets p,PrismAssetName pa where p.AssetName=pa.ID and P.id  in" +
            " (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved' " +
            " and PrismJobAssignedAssets.JobId=" + radcombo_job.SelectedValue + ") ").Tables[0];
        DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca," +
            " PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and " +
            " pj.jobid=" + radcombo_job.SelectedValue + " and pj.AssignmentStatus='Active'").Tables[0];// and pj.AssetStatus=3").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + radcombo_job.SelectedValue;
            DataRow[] row_operator = dt_Users.Select("userID=" + dtdates.Rows[0]["opManagerId"].ToString());
            DataRow[] row_rig = dt_rig.Select("rigtypeid=" + dtdates.Rows[0]["rigtypeid"].ToString());
            lbl_operator.Text = row_operator[0]["firstname"].ToString() + " " + row_operator[0]["lastname"].ToString();
            lbl_location.Text = dtdates.Rows[0]["primaryAddress1"].ToString();
            lbl_rig.Text = row_rig[0]["rigtypename"].ToString();
            lbl_jobsno.Text = dtdates.Rows[0]["jobid"].ToString();
            pnl_addjobs.Controls.Add(new LiteralControl("<table border='0' style='width:500px'>"));
            //pnl_addjobs.Controls.Add(new LiteralControl("<td style='width:200px'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>"));
            //pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff;'>Daily&#160;Charge&#40;&#36;&#41;</td>"));
            lbl_jname.Text = dtdates.Rows[0]["jobname"].ToString();
            lbl_jtype.Text = dtdates.Rows[0]["jtypename"].ToString();
            lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString()).ToString("MM/dd/yyyy");
            lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString()).ToString("MM/dd/yyyy");
            dtstart = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            dtend = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());

            dtstart_service = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            dtend_service = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());
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
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Asset Category</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Asset Name</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Kit Name</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">In Use</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Previous Tools Hours</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">TOTAL RUN Hrs</td>"));
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
                    DataTable dt_getdetfrm = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "").Tables[0];
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


                DataTable dt_gettotalrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(dailyrunhrs) as TotalRunHrs from PrismJobRunHourdetails pjr,PrismJobRunDetails r where r.runid=pjr.runid and pjr.assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and r.Date<='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
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
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' " + tdfiledstyle + "></td>"));
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
            //pnl_addjobs.Controls.Add(new LiteralControl("<tr><td colspan='8' align='left'><table>"));
            //pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Services</td><td  style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>In-Use</td></tr>"));
            //DataTable dt_Table1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismService").Tables[0];
            //for (int k = 0; k < dt_Table1.Rows.Count; k++)
            //{
            //    CheckBox chk_serviceinuse = new CheckBox();
            //    chk_serviceinuse.ID = "chk_service_"+dt_Table1.Rows[k]["ID"].ToString();
            //    if (existrunid != "")
            //    {
            //        DataTable dt_Table_existser = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunServiceDetails where runid=" + existrunid + " and serviceId=" + dt_Table1.Rows[k]["ID"].ToString() + "").Tables[0];
            //        if (dt_Table_existser.Rows.Count > 0)
            //        {
            //            if (dt_Table_existser.Rows[0]["inuse"].ToString() == "True")
            //            {

            //                chk_serviceinuse.Checked = true;
            //            }
            //            else
            //            {
            //                chk_serviceinuse.Checked = false;
            //            }
            //        }
            //    }

            //    pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
            //    pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'>" + dt_Table1.Rows[k]["ServiceName"].ToString() + "</td>"));
            //    pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='border:solid 1px #000000'>"));
            //    pnl_addjobs.Controls.Add(chk_serviceinuse);
            //    pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
            //    //pnl_addjobs.Controls.Add(new LiteralControl("<td align='center'>" + dt_Table1.Rows[k]["Cost"].ToString() + "</td>"));


            //    pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));

            //}
            pnl_addjobs.Controls.Add(new LiteralControl("</table></td></tr></table>"));


        }
    }

   
    //public void binddocuments()
    //{
    //    string selectq = "SELECT  etod.runid,etod.DocumentID,d.DocumentDisplayName,d.DocumentName from" +
    //            " DailyRunReportDocs etod, documents d where d.DocumentID=etod.DocumentID and etod.Uploadeddate='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'";
    //    DataTable dt_binddocs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
    //    if (dt_binddocs.Rows.Count > 0)
    //    {
    //        RadGrid2.DataSource = dt_binddocs;
    //        RadGrid2.DataBind();
    //    }
    //    else
    //    {
    //        RadGrid2.DataSource = null;
    //        RadGrid2.DataBind();
    //    }

    //}
    protected void radcombo_job_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            radtxt_start.SelectedDate = DateTime.Now.AddDays(-1);
            //radtxt_start.SelectedDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());
            radtxt_start.MinDate = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            radtxt_start.MaxDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());

            //radtxt_start.MinDate = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            //radtxt_start.MaxDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());

        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        string filenamese = "", filename = "",path = "",runid = "", dailycharge = "0.00", finished = "";
        filenamese = DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss"); // 2007-07-21T15:12:57
        filename = filenamese + "_" + file_dayrun.FileName;
        file_dayrun.PostedFile.SaveAs(Server.MapPath("~/DailyRunReportSheets\\") + filename);
        path = Server.MapPath("~/DailyRunReportSheets\\") + filename; //string path = fileuploadExcel.PostedFile.FileName;

        Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook book;

        CultureInfo ci = new CultureInfo("en-US");
        System.Threading.Thread thisThread = System.Threading.Thread.CurrentThread;
        thisThread.CurrentCulture = new CultureInfo("en-US");
        //string Sheetname = "";
        if (chk_runfinish.Checked)
            finished = "True";
        else
            finished = "False";
        try
        {
            ObjExcel.DisplayAlerts = false;
            book = ObjExcel.Workbooks.Open(path);
            if(ConnectionState.Closed==db.State)
            db.Open();
            transaction = db.BeginTransaction();

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

                string update_new = "update PrismJobRunDetails set finished='" + finished + "',dailycharges=" + dailycharge + "," +
                    " updateddate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where " +
                    " Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "";
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, update_new);

                if (finvalue == "False")
                {
                    if (finished == "True")
                    {
                        DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text,
                            "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
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
                        DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text,
                            "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and " +
                            " jid=" + radcombo_job.SelectedValue + "").Tables[0];
                        if (dt_getrunidnextval.Rows.Count > 0)
                        {
                            string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber-1 where" +
                                " runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and" +
                                " jid=" + radcombo_job.SelectedValue + ")";
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
                    DataTable dt_getrunidnextval = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
                    if (dt_getrunidnextval.Rows.Count > 0)
                    {
                        string addvalueexist = "UPDATE PrismJobRunDetails SET runnumber = runnumber+1 where runid in(select runid from PrismJobRunDetails where Date>'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + ")";
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
                //string path = Server.MapPath("~/Documents/");
                //string filename = "", completepath = "";

                string insertquery_jobrundet = "insert into PrismJobRunDetails(jid,Date,runnumber,finished,dailycharges,updateddate)values(" +
                "" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(radtxt_start.SelectedDate.ToString()) + "'," +
                "" + addrunval + ",'" + finished + "'," + dailycharge + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";

                int cnt = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertquery_jobrundet);
                DataTable dt_runid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    PrismJobRunDetails WHERE  runid = IDENT_CURRENT('PrismJobRunDetails')").Tables[0];

                runid = dt_runid.Rows[0]["runid"].ToString();
                hid_runid.Value = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");

            }
            
            foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in book.Worksheets)
            {

                if (sheet.Name == "Sheet2")
                //match exists rename
                {
                    //First Section
                    DataTable dt_PrismJobRunDetailsFormInfo = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "Select * from PrismJobRunDetailsFormInfo").Tables[0];
                    DataRow[] row_form = dt_PrismJobRunDetailsFormInfo.Select("Jobid=" + radcombo_job.SelectedValue + " and RunId=" + runid + "");
                    if (row_form.Length == 0)
                    {
                        string inset_PrismJobRunDetailsFormInfo = "Insert into PrismJobRunDetailsFormInfo(Jobid,RunId,CompanyRep,POPPET_SIZE,ORIFICE_SIZE,PULSE_WIDTH,PULSE_AMPLITUDE,TOTAL_CONNECTED," +
                        "TOTAL_CIRC,DEPTH_START,DEPTH_END,INCStart,AZMStart,MAGFStart,GRAVStart,DIPStart,INCEnd,AZMEnd,MAGFEnd,GRAVEnd,DIPEnd,TEMPERATURE_C,TEMPERATURE_F,AVER_PUMP_PRESSURE,AVER_FLOW_RATE,MUD_WEIGHT,SOLIDS,SAND)" +
                        " Values(" + radcombo_job.SelectedValue + "," + runid + ",'" + sheet.get_Range("B8", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B13", Missing.Value).Value2 + "','" + sheet.get_Range("B14", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B15", Missing.Value).Value2 + "','" + sheet.get_Range("B16", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B17", Missing.Value).Value2 + "','" + sheet.get_Range("B18", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B21", Missing.Value).Value2 + "','" + sheet.get_Range("B22", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B23", Missing.Value).Value2 + "','" + sheet.get_Range("B24", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B25", Missing.Value).Value2 + "','" + sheet.get_Range("B26", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("C21", Missing.Value).Value2 + "','" + sheet.get_Range("C22", Missing.Value).Value2 + "',"+
                        "'" + sheet.get_Range("C23", Missing.Value).Value2 + "','" + sheet.get_Range("C24", Missing.Value).Value2 + "',"+
                        "'" + sheet.get_Range("C25", Missing.Value).Value2 + "','" + sheet.get_Range("C26", Missing.Value).Value2 + "',"+
                        "'" + sheet.get_Range("B28", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B29", Missing.Value).Value2 + "','" + sheet.get_Range("B30", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B31", Missing.Value).Value2 + "','" + sheet.get_Range("B32", Missing.Value).Value2 + "'," +
                        " '" + sheet.get_Range("B33", Missing.Value).Value2 + "','" + sheet.get_Range("B34", Missing.Value).Value2 + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, inset_PrismJobRunDetailsFormInfo);
                    }
                    else
                    {
                        string Update_PrismJobRunDetailsFormInfo = "Update PrismJobRunDetailsFormInfo set CompanyRep='" + sheet.get_Range("B8", Missing.Value).Value2 + "'," +
                            " POPPET_SIZE='" + sheet.get_Range("B13", Missing.Value).Value2 + "', ORIFICE_SIZE='" + sheet.get_Range("B14", Missing.Value).Value2 + "'," +
                            " PULSE_WIDTH='" + sheet.get_Range("B15", Missing.Value).Value2 + "',PULSE_AMPLITUDE='" + sheet.get_Range("B16", Missing.Value).Value2 + "'," +
                            " TOTAL_CONNECTED='" + sheet.get_Range("B17", Missing.Value).Value2 + "',TOTAL_CIRC='" + sheet.get_Range("B18", Missing.Value).Value2 + "'," +
                            " DEPTH_START='" + sheet.get_Range("B21", Missing.Value).Value2 + "',DEPTH_END='" + sheet.get_Range("B22", Missing.Value).Value2 + "'," +
                            " INCStart='" + sheet.get_Range("B23", Missing.Value).Value2 + "',AZMStart='" + sheet.get_Range("B24", Missing.Value).Value2 + "'," +
                            " MAGFStart='" + sheet.get_Range("B25", Missing.Value).Value2 + "',GRAVStart='" + sheet.get_Range("B26", Missing.Value).Value2 + "'," +
                            " DIPStart='" + sheet.get_Range("B27", Missing.Value).Value2 + "',INCEnd='" + sheet.get_Range("C23", Missing.Value).Value2 + "',"+
                            " AZMEnd='" + sheet.get_Range("C24", Missing.Value).Value2 + "'," +
                            " MAGFEnd='" + sheet.get_Range("C25", Missing.Value).Value2 + "',GRAVEnd='" + sheet.get_Range("C26", Missing.Value).Value2 + "'," +
                            " DIPEnd='" + sheet.get_Range("C27", Missing.Value).Value2 + "',TEMPERATURE_C='" + sheet.get_Range("B28", Missing.Value).Value2 + "'," +
                            " TEMPERATURE_F='" + sheet.get_Range("B29", Missing.Value).Value2 + "',AVER_PUMP_PRESSURE='" + sheet.get_Range("B30", Missing.Value).Value2 + "'," +
                            " AVER_FLOW_RATE='" + sheet.get_Range("B31", Missing.Value).Value2 + "',MUD_WEIGHT='" + sheet.get_Range("B32", Missing.Value).Value2 + "'," +
                            " SOLIDS='" + sheet.get_Range("B33", Missing.Value).Value2 + "',SAND='" + sheet.get_Range("B34", Missing.Value).Value2 + "'" +
                            " where Jobid=" + radcombo_job.SelectedValue + " and RunId=" + runid + "";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, Update_PrismJobRunDetailsFormInfo);
                    }

                    //Close First Section

                    //24 Hour Activity Details                    
                    for (int cell = 6; cell <= 28; cell++)
                    {
                        string queryHourActivity = "";
                        var Timeitem = sheet.get_Range("E" + cell, Missing.Value);
                        var Activityitem = sheet.get_Range("F" + cell, Missing.Value);
                        var Commentsitem = sheet.get_Range("G" + cell, Missing.Value);
                        if (Timeitem.Value2.ToString() != "Select" && Activityitem.Value2.ToString() != "Select")
                        {
                            DataRow[] rowactivity = dt_Prism24HourActivity.Select("[24HourActivity]='" + Activityitem.Value2.ToString() + "'");
                            if (rowactivity.Length > 0)
                            {
                                DataRow[] row_time=dt_Prism24Hours.Select("Time='"+Timeitem.Value2.ToString()+"'");
                                queryHourActivity = "Insert into PrismJobRun24HourActivityLog([RunID],[Time],[24HourActivity],[Comments])" +
                                    " Values(" + runid + ",'" + row_time[0]["TimeId"].ToString()+ "','" + rowactivity[0]["HourActivityId"].ToString() + "','" + Commentsitem.Value2.ToString() + "')";
                                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryHourActivity);
                            }
                        }
                    }
                    //Close 24 Hour Activity Details

                    //Assets Required
                    for (int cell = 6; cell <= 28; cell++)
                    {
                        string queryassetreq = "";
                        var Assetnameitem = sheet.get_Range("I" + cell, Missing.Value);
                        var Qunatityitem = sheet.get_Range("J" + cell, Missing.Value);
                        if (Assetnameitem.Value2 != null && Qunatityitem.Value2 != null)
                        {
                            DataRow[] rowaassetreq = dt_AssetReq.Select("AssetName='" + Assetnameitem.Value2.ToString() + "'");
                            if (rowaassetreq.Length > 0)
                            {
                                queryassetreq = "Insert into PrismJobRunAssetsRequired([AssetID],[JobId],[RunID],[AQntty])" +
                                    " Values(" + rowaassetreq[0]["AssetID"].ToString() + "," + radcombo_job.SelectedValue + ",'" + runid + "','" + Qunatityitem.Value2.ToString() + "')";
                                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryassetreq);
                            }
                        }
                    }

                    if (runid != "")
                    {

                        DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(transaction, CommandType.Text,
                            "select  pn.AssetName as NameofAsset,pn.id as MainAssetId,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca,PrismAssetName pn" +
                            " where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.jobid=" + radcombo_job.SelectedValue + " ").Tables[0];

                        for (int cell = 38; cell <= 57; cell++)
                        {

                            var Dayrunhouritem = sheet.get_Range("I" + cell, Missing.Value);
                            var Snoitem = sheet.get_Range("C" + cell, Missing.Value);
                            if (Dayrunhouritem.Value2 != null && Snoitem.Value2 != null)
                            {
                                DataRow[] rowsno = dt_getassetcategories.Select("SerialNumber='" + Snoitem.Value2.ToString()+"'");
                                if (rowsno.Length > 0)
                                {
                                    string insert_jobrunhrdet = "";
                                    DataTable dt_jobhrdet = SqlHelper.ExecuteDataset(transaction, CommandType.Text,
                                        "select * from PrismJobRunHourDetails where runid=" + runid + " and" +
                                        " assetid=" + rowsno[0]["MainAssetId"].ToString() + "").Tables[0];
                                    if (dt_jobhrdet.Rows.Count == 0)
                                    {

                                        insert_jobrunhrdet = "insert into PrismJobRunHourDetails(runid,assetid,inuse,dailyrunhrs)values" +
                                            "(" + runid + "," + rowsno[0]["MainAssetId"].ToString() + ",'True'," +
                                        "" + Dayrunhouritem.Value2.ToString() + ")";

                                    }
                                    else
                                    {
                                        insert_jobrunhrdet = "update PrismJobRunHourDetails set inuse='True',dailyrunhrs=" + Dayrunhouritem.Value2.ToString() + " " +
                                            " where runid=" + runid + " and assetid=" + rowsno[0]["MainAssetId"].ToString() + "";
                                    }
                                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_jobrunhrdet);
                                }
                            }

                        }
                        transaction.Commit();
                        string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedTimeStamp,Source)values(" +
                                "'DDR01','Daily Run Report','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'" +
                            ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','JOB')";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
                        db.Close();
                        lbl_message.Text = "Daily Run Report Saved.";
                        lbl_message.ForeColor = Color.Green;
                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            lbl_message.Text = "Daily Run Report Error " + ex.Message + "";
            lbl_message.ForeColor = Color.Red;
        }
        bindImportedData();
        //clear();
    }
}