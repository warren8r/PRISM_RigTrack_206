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

public partial class Modules_Configuration_Manager_DailyRunReport : System.Web.UI.Page
{
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlCommand cmdInsert;
    SqlTransaction transaction;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from manageJobOrders where (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "jobname", "jid", "0");
        }
        string a = radtxt_start.SelectedDate.ToString();
        if (a != "")
        {
            bindtextboxesdates();
        }
    }
    protected void radcombo_job_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
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
        clear();
        lbl_message.Text = "";
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j,RigTypes r where m.jobtype=j.jobtypeid and m.rigtypeid=r.rigtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {

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
            btn_save.Visible = true;
            btn_saveupdate2.Visible = true;
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
            string existrunid = "";
            if (dt_getdailyrun.Rows.Count > 0)
            {

                existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();
                lbl_runnumber.Text = dt_getdailyrun.Rows[0]["runnumber"].ToString();
                //lnk_download.Text = dt_getdailyrun.Rows[0]["attachmentname"].ToString();
                //string path = Server.MapPath("~/Documents/");
                //string docnamewithpath = path + dt_getdailyrun.Rows[0]["attachmentname"].ToString();
                //lnk_download.Attributes.Add("onclick", "return downloadFile('" + docnamewithpath + "','" + dt_getdailyrun.Rows[0]["attachmentname"].ToString() + "');");
                DataTable dt_getdaynumber = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date<'" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + " and runnumber=" + dt_getdailyrun.Rows[0]["runnumber"].ToString() + "").Tables[0];
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
                activitylogbind(existrunid);
                binddailyprogress(existrunid);
                binddrillingparams(existrunid);
                bindothersuppliesneeded(existrunid);
                bindassetsneeded(existrunid);

            }
            else
            {
                string selectq = "select * from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date in (select MAX(Date) from PrismJobRunDetails where jid=" + radcombo_job.SelectedValue + " and Date  <= '" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "')";
                DataTable dt_getruncnt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
                if (dt_getruncnt.Rows.Count > 0)
                {
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
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        DataTable dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select pa.AssetName AS AssetName1,* from Prism_Assets p,PrismAssetName pa where p.AssetName=pa.ID and P.id  in (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved' and PrismJobAssignedAssets.JobId=" + radcombo_job.SelectedValue + ") ").Tables[0];
        DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca,PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.jobid=" + radcombo_job.SelectedValue + " and pj.AssetStatus=3").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
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
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>"));
            string datecol = dtstart_service.ToString("MM/dd/yyyy");
            pnl_addjobs.Controls.Add(new LiteralControl(Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM/dd/yyyy")));
            pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td><table>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Serial Number</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Asset Category</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Asset Name</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>In Use</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Previous Tools Hours</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>TOTAL RUN Hrs</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Cumilative Tools Hours</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Daily Run Hours</td>"));

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
                    decimal d;
                    if (dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString() != "")
                    {
                        d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString()) + Convert.ToDecimal(dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString());
                    }
                    else
                    {
                        d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString());
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
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'>" + dt_getassetcategories.Rows[cat]["SerialNumber"].ToString() + "</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'>" + dt_getassetcategories.Rows[cat]["clientAssetName"].ToString() + "</td>"));

                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'>" + dt_getassetcategories.Rows[cat]["NameofAsset"].ToString() + "</td>"));
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
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td colspan='8' align='left'><table>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Services</td><td  style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>In-Use</td></tr>"));
            DataTable dt_Table1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismService").Tables[0];
            for (int k = 0; k < dt_Table1.Rows.Count; k++)
            {
                CheckBox chk_serviceinuse = new CheckBox();
                chk_serviceinuse.ID = "chk_service_"+dt_Table1.Rows[k]["ID"].ToString();
                if (existrunid != "")
                {
                    DataTable dt_Table_existser = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunServiceDetails where runid=" + existrunid + " and serviceId=" + dt_Table1.Rows[k]["ID"].ToString() + "").Tables[0];
                    if (dt_Table_existser.Rows.Count > 0)
                    {
                        if (dt_Table_existser.Rows[0]["inuse"].ToString() == "True")
                        {

                            chk_serviceinuse.Checked = true;
                        }
                        else
                        {
                            chk_serviceinuse.Checked = false;
                        }
                    }
                }

                pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'>" + dt_Table1.Rows[k]["ServiceName"].ToString() + "</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='border:solid 1px #000000'>"));
                pnl_addjobs.Controls.Add(chk_serviceinuse);
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                //pnl_addjobs.Controls.Add(new LiteralControl("<td align='center'>" + dt_Table1.Rows[k]["Cost"].ToString() + "</td>"));


                pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));

            }
            pnl_addjobs.Controls.Add(new LiteralControl("</table></td></tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("</table></table>"));
        }
    }
    public void binddailyprogress(string existrunid)
    {
        DataTable dt_getdetdailyprogresss = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDailyProgress where runid=" + existrunid + "").Tables[0];
        if (dt_getdetdailyprogresss.Rows.Count > 0)
        {
            txt_cmts.Text = dt_getdetdailyprogresss.Rows[0]["comments"].ToString();
            txt_depthstrt.Text = dt_getdetdailyprogresss.Rows[0]["depthstart"].ToString();
            txt_depthend.Text = dt_getdetdailyprogresss.Rows[0]["depthend"].ToString();
            txt_lastinc.Text = dt_getdetdailyprogresss.Rows[0]["LastInc"].ToString();
            txt_lastazm.Text = dt_getdetdailyprogresss.Rows[0]["lastazm"].ToString();
            txt_lasttemp.Text = dt_getdetdailyprogresss.Rows[0]["lasttemp"].ToString();
        }
    }
    public void binddrillingparams(string existrunid)
    {
        DataTable dt_getdetdrillingparams = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDrillingParameters where runid=" + existrunid + "").Tables[0];
        if (dt_getdetdrillingparams.Rows.Count > 0)
        {
            txt_pumppressure.Text = dt_getdetdrillingparams.Rows[0]["pumppressure"].ToString();
            txt_flowrate.Text = dt_getdetdrillingparams.Rows[0]["flowrate"].ToString();
            txt_mudwght.Text = dt_getdetdrillingparams.Rows[0]["mudweight"].ToString();
            txt_floride.Text = dt_getdetdrillingparams.Rows[0]["chlorides"].ToString();
            txt_pulseamp.Text = dt_getdetdrillingparams.Rows[0]["pulseamp"].ToString();
            txt_sand.Text = dt_getdetdrillingparams.Rows[0]["sand"].ToString();
            txt_solid.Text = dt_getdetdrillingparams.Rows[0]["solid"].ToString();
        }
    }
    public void bindothersuppliesneeded(string existrunid)
    {
        DataTable dt_getdetothersuppliesneeded = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunOtherSuppliesNeeded where runid=" + existrunid + "").Tables[0];
        if (dt_getdetothersuppliesneeded.Rows.Count > 0)
        {
            txt_othersuppliesneeded1.Text = dt_getdetothersuppliesneeded.Rows[0]["suppliesneeded"].ToString();
            txt_othersuppliesneeded2.Text = dt_getdetothersuppliesneeded.Rows[1]["suppliesneeded"].ToString();
            txt_othersuppliesneeded3.Text = dt_getdetothersuppliesneeded.Rows[2]["suppliesneeded"].ToString();
            txt_othersuppliesneeded4.Text = dt_getdetothersuppliesneeded.Rows[3]["suppliesneeded"].ToString();
            txt_othersuppliesneeded5.Text = dt_getdetothersuppliesneeded.Rows[4]["suppliesneeded"].ToString();

        }
    }
    public void binddocuments()
    {
        string selectq = "SELECT  etod.runid,etod.DocumentID,d.DocumentDisplayName,d.DocumentName from" +
                " DailyRunReportDocs etod, documents d where d.DocumentID=etod.DocumentID and etod.Uploadeddate='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'";
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
    public void bindassetsneeded(string existrunid)
    {
        DataTable dt_getdetassetsneeded = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunAssetsNeeded where runid=" + existrunid + "").Tables[0];
        if (dt_getdetassetsneeded.Rows.Count > 0)
        {
            if (dt_getdetassetsneeded.Rows[0]["assetid"].ToString() != "")
            {
                radcombo_assetneeded1.SelectedValue = dt_getdetassetsneeded.Rows[0]["assetid"].ToString();
            }
            else
            {
                radcombo_assetneeded1.SelectedValue = string.Empty;
                radcombo_assetneeded1.ClearSelection();
            }
            if (dt_getdetassetsneeded.Rows[1]["assetid"].ToString() != "")
            {
                radcombo_assetneeded2.SelectedValue = dt_getdetassetsneeded.Rows[1]["assetid"].ToString();
            }
            else
            {
                radcombo_assetneeded2.SelectedValue = string.Empty;
                radcombo_assetneeded2.ClearSelection();
            }
            if (dt_getdetassetsneeded.Rows[2]["assetid"].ToString() != "")
            {
                radcombo_assetneeded3.SelectedValue = dt_getdetassetsneeded.Rows[2]["assetid"].ToString();
            }
            else
            {
                radcombo_assetneeded3.SelectedValue = string.Empty;
                radcombo_assetneeded3.ClearSelection();
            }
            if (dt_getdetassetsneeded.Rows[3]["assetid"].ToString() != "")
            {
                radcombo_assetneeded4.SelectedValue = dt_getdetassetsneeded.Rows[3]["assetid"].ToString();
            }
            else
            {
                radcombo_assetneeded4.SelectedValue = string.Empty;
                radcombo_assetneeded4.ClearSelection();
            }
            if (dt_getdetassetsneeded.Rows[4]["assetid"].ToString() != "")
            {
                radcombo_assetneeded5.SelectedValue = dt_getdetassetsneeded.Rows[4]["assetid"].ToString();
            }
            else
            {
                radcombo_assetneeded5.SelectedValue = string.Empty;
                radcombo_assetneeded5.ClearSelection();
            }

        }
    }
    public void activitylogbind(string existrunid)
    {
        DataTable dt_getdetactiveloghrvalues = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + existrunid + " order by hour asc").Tables[0];
        if (dt_getdetactiveloghrvalues.Rows.Count > 0)
        {
            if (dt_getdetactiveloghrvalues.Rows[0]["activityassetid"].ToString() != "")
                radcombo_hr1.SelectedValue = dt_getdetactiveloghrvalues.Rows[0]["activityassetid"].ToString();
            else
            {
                radcombo_hr1.SelectedValue = string.Empty;
                radcombo_hr1.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[1]["activityassetid"].ToString() != "")
                radcombo_hr2.SelectedValue = dt_getdetactiveloghrvalues.Rows[1]["activityassetid"].ToString();
            else
            {
                radcombo_hr2.SelectedValue = string.Empty;
                radcombo_hr2.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[2]["activityassetid"].ToString() != "")
                radcombo_hr3.SelectedValue = dt_getdetactiveloghrvalues.Rows[2]["activityassetid"].ToString();
            else
            {
                radcombo_hr3.SelectedValue = string.Empty;
                radcombo_hr3.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[3]["activityassetid"].ToString() != "")
                radcombo_hr4.SelectedValue = dt_getdetactiveloghrvalues.Rows[3]["activityassetid"].ToString();
            else
            {
                radcombo_hr4.SelectedValue = string.Empty;
                radcombo_hr4.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[4]["activityassetid"].ToString() != "")
                radcombo_hr5.SelectedValue = dt_getdetactiveloghrvalues.Rows[4]["activityassetid"].ToString();
            else
            {
                radcombo_hr5.SelectedValue = string.Empty;
                radcombo_hr5.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[5]["activityassetid"].ToString() != "")
                radcombo_hr6.SelectedValue = dt_getdetactiveloghrvalues.Rows[5]["activityassetid"].ToString();
            else
            {
                radcombo_hr6.SelectedValue = string.Empty;
                radcombo_hr6.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[6]["activityassetid"].ToString() != "")
                radcombo_hr7.SelectedValue = dt_getdetactiveloghrvalues.Rows[6]["activityassetid"].ToString();
            else
            {
                radcombo_hr7.SelectedValue = string.Empty;
                radcombo_hr7.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[7]["activityassetid"].ToString() != "")
                radcombo_hr8.SelectedValue = dt_getdetactiveloghrvalues.Rows[7]["activityassetid"].ToString();
            else
            {
                radcombo_hr8.SelectedValue = string.Empty;
                radcombo_hr8.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[8]["activityassetid"].ToString() != "")
                radcombo_hr9.SelectedValue = dt_getdetactiveloghrvalues.Rows[8]["activityassetid"].ToString();
            else
            {
                radcombo_hr9.SelectedValue = string.Empty;
                radcombo_hr9.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[9]["activityassetid"].ToString() != "")
                radcombo_hr10.SelectedValue = dt_getdetactiveloghrvalues.Rows[9]["activityassetid"].ToString();
            else
            {
                radcombo_hr10.SelectedValue = string.Empty;
                radcombo_hr10.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[10]["activityassetid"].ToString() != "")
                radcombo_hr11.SelectedValue = dt_getdetactiveloghrvalues.Rows[10]["activityassetid"].ToString();
            else
            {
                radcombo_hr11.SelectedValue = string.Empty;
                radcombo_hr11.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[11]["activityassetid"].ToString() != "")
                radcombo_hr12.SelectedValue = dt_getdetactiveloghrvalues.Rows[11]["activityassetid"].ToString();
            else
            {
                radcombo_hr12.SelectedValue = string.Empty;
                radcombo_hr12.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[12]["activityassetid"].ToString() != "")
                radcombo_hr13.SelectedValue = dt_getdetactiveloghrvalues.Rows[12]["activityassetid"].ToString();
            else
            {
                radcombo_hr13.SelectedValue = string.Empty;
                radcombo_hr13.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[13]["activityassetid"].ToString() != "")
                radcombo_hr14.SelectedValue = dt_getdetactiveloghrvalues.Rows[13]["activityassetid"].ToString();
            else
            {
                radcombo_hr14.SelectedValue = string.Empty;
                radcombo_hr14.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[14]["activityassetid"].ToString() != "")
                radcombo_hr15.SelectedValue = dt_getdetactiveloghrvalues.Rows[14]["activityassetid"].ToString();
            else
            {
                radcombo_hr15.SelectedValue = string.Empty;
                radcombo_hr15.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[15]["activityassetid"].ToString() != "")
                radcombo_hr16.SelectedValue = dt_getdetactiveloghrvalues.Rows[15]["activityassetid"].ToString();
            else
            {
                radcombo_hr16.SelectedValue = string.Empty;
                radcombo_hr16.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[16]["activityassetid"].ToString() != "")
                radcombo_hr17.SelectedValue = dt_getdetactiveloghrvalues.Rows[16]["activityassetid"].ToString();
            else
            {
                radcombo_hr17.SelectedValue = string.Empty;
                radcombo_hr17.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[17]["activityassetid"].ToString() != "")
                radcombo_hr18.SelectedValue = dt_getdetactiveloghrvalues.Rows[17]["activityassetid"].ToString();
            else
            {
                radcombo_hr18.SelectedValue = string.Empty;
                radcombo_hr18.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[18]["activityassetid"].ToString() != "")
                radcombo_hr19.SelectedValue = dt_getdetactiveloghrvalues.Rows[18]["activityassetid"].ToString();
            else
            {
                radcombo_hr19.SelectedValue = string.Empty;
                radcombo_hr19.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[19]["activityassetid"].ToString() != "")
                radcombo_hr20.SelectedValue = dt_getdetactiveloghrvalues.Rows[19]["activityassetid"].ToString();
            else
            {
                radcombo_hr20.SelectedValue = string.Empty;
                radcombo_hr20.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[20]["activityassetid"].ToString() != "")
                radcombo_hr21.SelectedValue = dt_getdetactiveloghrvalues.Rows[20]["activityassetid"].ToString();
            else
            {
                radcombo_hr21.SelectedValue = string.Empty;
                radcombo_hr21.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[21]["activityassetid"].ToString() != "")
                radcombo_hr22.SelectedValue = dt_getdetactiveloghrvalues.Rows[21]["activityassetid"].ToString();
            else
            {
                radcombo_hr22.SelectedValue = string.Empty;
                radcombo_hr22.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[22]["activityassetid"].ToString() != "")
                radcombo_hr23.SelectedValue = dt_getdetactiveloghrvalues.Rows[22]["activityassetid"].ToString();
            else
            {
                radcombo_hr23.SelectedValue = string.Empty;
                radcombo_hr23.ClearSelection();
            }
            if (dt_getdetactiveloghrvalues.Rows[23]["activityassetid"].ToString() != "")
                radcombo_hr24.SelectedValue = dt_getdetactiveloghrvalues.Rows[23]["activityassetid"].ToString();
            else
            {
                radcombo_hr24.SelectedValue = string.Empty;
                radcombo_hr24.ClearSelection();
            }


        }
        //}
    }
    protected void btn_save_OnClick(object sender, EventArgs s)
    {
        try
        {
            db.Open();
            transaction = db.BeginTransaction();
            string runid = "", dailycharge = "";
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
                string update_new = "update PrismJobRunDetails set finished='" + finished + "',dailycharges=" + dailycharge + ", updateddate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "";
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

                string insertquery_jobrundet = "insert into PrismJobRunDetails(jid,Date,runnumber,finished,dailycharges,updateddate)values(" +
                "" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(radtxt_start.SelectedDate.ToString()) + "'," +
                "" + addrunval + ",'" + finished + "'," + dailycharge + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";

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
            DataTable dt_getrunpersonals = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunPersonals where runid=" + runid + "").Tables[0];
            if (dt_getrunpersonals.Rows.Count == 0)
            {
                for (int asset = 0; asset < combo_ex_personal.Items.Count; asset++)
                {
                    if (combo_ex_personal.Items[asset].Checked)
                    {
                        string insert_personals = "insert into PrismJobRunPersonals(runid,personid)values(" + runid + "," + combo_ex_personal.Items[asset].Value + ")";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_personals);
                    }
                }
            }

            if (runid != "")
            {
                //PrismJobRunServiceDetails
                DataTable dt_Table1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismService").Tables[0];
                for (int k = 0; k < dt_Table1.Rows.Count; k++)
                {
                    string insertser = "";
                    string chkserviceinuse = "chk_service_" + dt_Table1.Rows[k]["ID"].ToString();
                    CheckBox chk_serviceinuse = pnl_addjobs.FindControl(chkserviceinuse) as CheckBox;
                    if (chk_serviceinuse.Checked)
                    {
                        insertser = "insert into PrismJobRunServiceDetails(runid,serviceId,inuse)values(" + runid + "," + dt_Table1.Rows[k]["ID"].ToString() + ",'True')";
                        
                    }
                    else
                    {
                        insertser = "insert into PrismJobRunServiceDetails(runid,serviceId,inuse)values(" + runid + "," + dt_Table1.Rows[k]["ID"].ToString() + ",'False')";
                    }
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertser);
                    //chk_serviceinuse.ID = "chk_service_" + dt_Table1.Rows[k]["ID"].ToString();
                }
                DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select  pn.AssetName as NameofAsset,pn.id as MainAssetId,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca,PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.jobid=" + radcombo_job.SelectedValue + " and pj.AssetStatus=3").Tables[0];
                for (int cat = 0; cat < dt_getassetcategories.Rows.Count; cat++)
                {
                    string chkinuseid = "chk_" + cat;
                    CheckBox chk_inuse = pnl_addjobs.FindControl(chkinuseid) as CheckBox;
                    string dailyrunhrs = "txt_dailyrunhrs" + cat;
                    TextBox txt_dailyrunhrs = pnl_addjobs.FindControl(dailyrunhrs) as TextBox;
                    string dailyrunvalue = "";
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

                        DataTable dt_jobhrdet = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunHourDetails where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["MainAssetId"].ToString() + "").Tables[0];
                        if (dt_jobhrdet.Rows.Count == 0)
                        {

                            insert_jobrunhrdet = "insert into PrismJobRunHourDetails(runid,assetid,inuse,dailyrunhrs)values" +
                                "(" + runid + "," + dt_getassetcategories.Rows[cat]["MainAssetId"].ToString() + ",'True'," +
                            "" + dailyrunvalue + ")";

                        }
                        else
                        {
                            insert_jobrunhrdet = "update PrismJobRunHourDetails set inuse='True',dailyrunhrs=" + dailyrunvalue + " where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["MainAssetId"].ToString() + "";
                        }
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_jobrunhrdet);
                    }
                    else
                    {
                        string insert_jobrunhrdet = "";
                        //string totrunhrs = "txt_totrunhrs" + cat;
                        //string cumulativehrs = "txt_cumulativehrs" + cat;

                        DataTable dt_jobhrdet = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunHourDetails where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["MainAssetId"].ToString() + "").Tables[0];
                        if (dt_jobhrdet.Rows.Count == 0)
                        {

                            insert_jobrunhrdet = "insert into PrismJobRunHourDetails(runid,assetid,inuse,dailyrunhrs)values" +
                                "(" + runid + "," + dt_getassetcategories.Rows[cat]["MainAssetId"].ToString() + ",'False'," +
                            "" + dailyrunvalue + ")";

                        }
                        else
                        {
                            insert_jobrunhrdet = "update PrismJobRunHourDetails set inuse='False',dailyrunhrs=" + dailyrunvalue + " where runid=" + runid + " and assetid=" + dt_getassetcategories.Rows[cat]["MainAssetId"].ToString() + "";
                        }
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_jobrunhrdet);
                    }
                }
                //ACTIVITY LOG INSERTION
                string hr1 = "", hr2 = "", hr3 = "", hr4 = "", hr5 = "", hr6 = "", hr7 = "", hr8 = "", hr9 = "", hr10 = "", hr11 = "", hr12 = "", hr13 = ""
                    , hr14 = "", hr15 = "", hr16 = "", hr17 = "", hr18 = "", hr19 = "", hr20 = "", hr21 = "", hr22 = "", hr23 = "", hr24 = "";
                string hr1val = "", hr2val = "", hr3val = "", hr4val = "", hr5val = "", hr6val = "", hr7val = "", hr8val = "", hr9val = "", hr10val = "", hr11val = "", hr12val = "", hr13val = ""
                    , hr14val = "", hr15val = "", hr16val = "", hr17val = "", hr18val = "", hr19val = "", hr20val = "", hr21val = "", hr22val = "", hr23val = "", hr24val = "";
                DataTable dt_gethr1 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=1").Tables[0];
                DataTable dt_gethr2 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=2").Tables[0];
                DataTable dt_gethr3 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=3").Tables[0];
                DataTable dt_gethr4 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=4").Tables[0];
                DataTable dt_gethr5 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=5").Tables[0];
                DataTable dt_gethr6 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=6").Tables[0];
                DataTable dt_gethr7 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=7").Tables[0];
                DataTable dt_gethr8 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=8").Tables[0];
                DataTable dt_gethr9 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=9").Tables[0];
                DataTable dt_gethr10 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=10").Tables[0];
                DataTable dt_gethr11 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=11").Tables[0];
                DataTable dt_gethr12 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=12").Tables[0];
                DataTable dt_gethr13 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=13").Tables[0];
                DataTable dt_gethr14 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=14").Tables[0];
                DataTable dt_gethr15 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=15").Tables[0];
                DataTable dt_gethr16 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=16").Tables[0];
                DataTable dt_gethr17 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=17").Tables[0];
                DataTable dt_gethr18 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=18").Tables[0];
                DataTable dt_gethr19 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=19").Tables[0];
                DataTable dt_gethr20 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=20").Tables[0];
                DataTable dt_gethr21 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=21").Tables[0];
                DataTable dt_gethr22 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=22").Tables[0];
                DataTable dt_gethr23 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=23").Tables[0];
                DataTable dt_gethr24 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunActivityLog where runid=" + runid + " and hour=24").Tables[0];
                if (radcombo_hr1.SelectedValue != "")
                    hr1val = radcombo_hr1.SelectedValue;
                else
                    hr1val = "NULL";

                if (radcombo_hr2.SelectedValue != "")
                    hr2val = radcombo_hr2.SelectedValue;
                else
                    hr2val = "NULL";

                if (radcombo_hr3.SelectedValue != "")
                    hr3val = radcombo_hr3.SelectedValue;
                else
                    hr3val = "NULL";

                if (radcombo_hr4.SelectedValue != "")
                    hr4val = radcombo_hr4.SelectedValue;
                else
                    hr4val = "NULL";

                if (radcombo_hr5.SelectedValue != "")
                    hr5val = radcombo_hr5.SelectedValue;
                else
                    hr5val = "NULL";

                if (radcombo_hr6.SelectedValue != "")
                    hr6val = radcombo_hr6.SelectedValue;
                else
                    hr6val = "NULL";

                if (radcombo_hr7.SelectedValue != "")
                    hr7val = radcombo_hr7.SelectedValue;
                else
                    hr7val = "NULL";

                if (radcombo_hr8.SelectedValue != "")
                    hr8val = radcombo_hr8.SelectedValue;
                else
                    hr8val = "NULL";

                if (radcombo_hr9.SelectedValue != "")
                    hr9val = radcombo_hr9.SelectedValue;
                else
                    hr9val = "NULL";

                if (radcombo_hr10.SelectedValue != "")
                    hr10val = radcombo_hr10.SelectedValue;
                else
                    hr10val = "NULL";

                if (radcombo_hr11.SelectedValue != "")
                    hr11val = radcombo_hr11.SelectedValue;
                else
                    hr11val = "NULL";

                if (radcombo_hr12.SelectedValue != "")
                    hr12val = radcombo_hr12.SelectedValue;
                else
                    hr12val = "NULL";

                if (radcombo_hr13.SelectedValue != "")
                    hr13val = radcombo_hr13.SelectedValue;
                else
                    hr13val = "NULL";


                if (radcombo_hr14.SelectedValue != "")
                    hr14val = radcombo_hr14.SelectedValue;
                else
                    hr14val = "NULL";

                if (radcombo_hr15.SelectedValue != "")
                    hr15val = radcombo_hr15.SelectedValue;
                else
                    hr15val = "NULL";

                if (radcombo_hr16.SelectedValue != "")
                    hr16val = radcombo_hr16.SelectedValue;
                else
                    hr16val = "NULL";

                if (radcombo_hr17.SelectedValue != "")
                    hr17val = radcombo_hr17.SelectedValue;
                else
                    hr17val = "NULL";

                if (radcombo_hr18.SelectedValue != "")
                    hr18val = radcombo_hr18.SelectedValue;
                else
                    hr18val = "NULL";

                if (radcombo_hr19.SelectedValue != "")
                    hr19val = radcombo_hr19.SelectedValue;
                else
                    hr19val = "NULL";

                if (radcombo_hr20.SelectedValue != "")
                    hr20val = radcombo_hr20.SelectedValue;
                else
                    hr20val = "NULL";

                if (radcombo_hr21.SelectedValue != "")
                    hr21val = radcombo_hr21.SelectedValue;
                else
                    hr21val = "NULL";

                if (radcombo_hr22.SelectedValue != "")
                    hr22val = radcombo_hr22.SelectedValue;
                else
                    hr22val = "NULL";

                if (radcombo_hr23.SelectedValue != "")
                    hr23val = radcombo_hr23.SelectedValue;
                else
                    hr23val = "NULL";

                if (radcombo_hr24.SelectedValue != "")
                    hr24val = radcombo_hr24.SelectedValue;
                else
                    hr24val = "NULL";

                if (dt_gethr1.Rows.Count == 0)
                {


                    hr1 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",1," + hr1val + ")";

                }
                else
                {
                    hr1 = "update PrismJobRunActivityLog set activityassetid=" + hr1val + " where runid=" + runid + " and hour=1";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr1);
                if (dt_gethr2.Rows.Count == 0)
                {
                    hr2 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",2," + hr2val + ")";

                }
                else
                {
                    hr2 = "update PrismJobRunActivityLog set activityassetid=" + hr2val + " where runid=" + runid + " and hour=2";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr2);
                if (dt_gethr3.Rows.Count == 0)
                {
                    hr3 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",3," + hr3val + ")";

                }
                else
                {
                    hr3 = "update PrismJobRunActivityLog set activityassetid=" + hr3val + " where runid=" + runid + " and hour=3";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr3);
                if (dt_gethr4.Rows.Count == 0)
                {
                    hr4 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",4," + hr4val + ")";

                }
                else
                {
                    hr4 = "update PrismJobRunActivityLog set activityassetid=" + hr4val + " where runid=" + runid + " and hour=4";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr4);
                if (dt_gethr5.Rows.Count == 0)
                {
                    hr5 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",5," + hr5val + ")";

                }
                else
                {
                    hr5 = "update PrismJobRunActivityLog set activityassetid=" + hr5val + " where runid=" + runid + " and hour=5";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr5);
                if (dt_gethr6.Rows.Count == 0)
                {
                    hr6 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",6," + hr6val + ")";

                }
                else
                {
                    hr6 = "update PrismJobRunActivityLog set activityassetid=" + hr6val + " where runid=" + runid + " and hour=6";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr6);
                if (dt_gethr7.Rows.Count == 0)
                {
                    hr7 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",7," + hr7val + ")";

                }
                else
                {
                    hr7 = "update PrismJobRunActivityLog set activityassetid=" + hr7val + " where runid=" + runid + " and hour=7";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr7);
                if (dt_gethr8.Rows.Count == 0)
                {
                    hr8 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",8," + hr8val + ")";

                }
                else
                {
                    hr8 = "update PrismJobRunActivityLog set activityassetid=" + hr8val + " where runid=" + runid + " and hour=8";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr8);
                if (dt_gethr9.Rows.Count == 0)
                {
                    hr9 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",9," + hr9val + ")";

                }
                else
                {
                    hr9 = "update PrismJobRunActivityLog set activityassetid=" + hr9val + " where runid=" + runid + " and hour=9";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr9);
                if (dt_gethr10.Rows.Count == 0)
                {
                    hr10 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",10," + hr10val + ")";

                }
                else
                {
                    hr10 = "update PrismJobRunActivityLog set activityassetid=" + hr10val + " where runid=" + runid + " and hour=10";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr10);
                if (dt_gethr11.Rows.Count == 0)
                {
                    hr11 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",11," + hr11val + ")";

                }
                else
                {
                    hr11 = "update PrismJobRunActivityLog set activityassetid=" + hr11val + " where runid=" + runid + " and hour=11";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr11);
                if (dt_gethr12.Rows.Count == 0)
                {
                    hr12 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",12," + hr12val + ")";

                }
                else
                {
                    hr12 = "update PrismJobRunActivityLog set activityassetid=" + hr12val + " where runid=" + runid + " and hour=12";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr12);
                if (dt_gethr13.Rows.Count == 0)
                {
                    hr13 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",13," + hr13val + ")";

                }
                else
                {
                    hr13 = "update PrismJobRunActivityLog set activityassetid=" + hr13val + " where runid=" + runid + " and hour=13";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr13);
                if (dt_gethr14.Rows.Count == 0)
                {
                    hr14 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",14," + hr14val + ")";

                }
                else
                {
                    hr14 = "update PrismJobRunActivityLog set activityassetid=" + hr14val + " where runid=" + runid + " and hour=14";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr14);
                if (dt_gethr15.Rows.Count == 0)
                {
                    hr15 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",15," + hr15val + ")";

                }
                else
                {
                    hr15 = "update PrismJobRunActivityLog set activityassetid=" + hr15val + " where runid=" + runid + " and hour=15";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr15);
                if (dt_gethr16.Rows.Count == 0)
                {
                    hr16 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",16," + hr16val + ")";

                }
                else
                {
                    hr16 = "update PrismJobRunActivityLog set activityassetid=" + hr16val + " where runid=" + runid + " and hour=16";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr16);
                if (dt_gethr17.Rows.Count == 0)
                {
                    hr17 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",17," + hr17val + ")";

                }
                else
                {
                    hr17 = "update PrismJobRunActivityLog set activityassetid=" + hr17val + " where runid=" + runid + " and hour=17";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr17);
                if (dt_gethr18.Rows.Count == 0)
                {
                    hr18 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",18," + hr18val + ")";

                }
                else
                {
                    hr18 = "update PrismJobRunActivityLog set activityassetid=" + hr18val + " where runid=" + runid + " and hour=18";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr18);
                if (dt_gethr19.Rows.Count == 0)
                {
                    hr19 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",19," + hr19val + ")";

                }
                else
                {
                    hr19 = "update PrismJobRunActivityLog set activityassetid=" + hr19val + " where runid=" + runid + " and hour=19";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr19);
                if (dt_gethr20.Rows.Count == 0)
                {
                    hr20 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",20," + hr20val + ")";

                }
                else
                {
                    hr20 = "update PrismJobRunActivityLog set activityassetid=" + hr20val + " where runid=" + runid + " and hour=20";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr20);
                if (dt_gethr21.Rows.Count == 0)
                {
                    hr21 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",21," + hr21val + ")";

                }
                else
                {
                    hr21 = "update PrismJobRunActivityLog set activityassetid=" + hr21val + " where runid=" + runid + " and hour=21";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr21);
                if (dt_gethr22.Rows.Count == 0)
                {
                    hr22 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",22," + hr22val + ")";

                }
                else
                {
                    hr22 = "update PrismJobRunActivityLog set activityassetid=" + hr22val + " where runid=" + runid + " and hour=22";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr22);
                if (dt_gethr23.Rows.Count == 0)
                {
                    hr23 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",23," + hr23val + ")";

                }
                else
                {
                    hr23 = "update PrismJobRunActivityLog set activityassetid=" + hr23val + " where runid=" + runid + " and hour=23";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr23);
                if (dt_gethr24.Rows.Count == 0)
                {
                    hr24 = "insert into PrismJobRunActivityLog(runid,hour,activityassetid)values(" +
                        "" + runid + ",24," + hr24val + ")";

                }
                else
                {
                    hr24 = "update PrismJobRunActivityLog set activityassetid=" + hr24val + " where runid=" + runid + " and hour=24";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, hr24);
                //END OF INSERT ACTIVITY LOH HRS

                //START DAILY PROGRESS INSERT
                string depthstart = "", depthend = "", lastinc = "", lastazm = "", lasttemp = "";
                if (txt_depthstrt.Text != "")
                {
                    depthstart = txt_depthstrt.Text;
                }
                else
                {
                    depthstart = "NULL";
                }
                if (txt_depthend.Text != "")
                {
                    depthend = txt_depthend.Text;
                }
                else
                {
                    depthend = "NULL";
                }
                if (txt_lastinc.Text != "")
                {
                    lastinc = txt_lastinc.Text;
                }
                else
                {
                    lastinc = "NULL";
                }
                if (txt_lastazm.Text != "")
                {
                    lastazm = txt_lastazm.Text;
                }
                else
                {
                    lastazm = "NULL";
                }
                if (txt_lasttemp.Text != "")
                {
                    lasttemp = txt_lasttemp.Text;
                }
                else
                {
                    lasttemp = "NULL";
                }
                string insert_dailyprogress = "";
                DataTable dt_dailyprogress = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunDailyProgress where runid=" + runid + "").Tables[0];
                if (dt_dailyprogress.Rows.Count == 0)
                {
                    insert_dailyprogress = "insert into PrismJobRunDailyProgress(runid,depthstart,depthend,LastInc,lastazm,lasttemp,comments)values(" +
                        "" + runid + "," + depthstart + "," + depthend + "," + lastinc + "," + lastazm + "," + lasttemp + ",'" + txt_cmts.Text + "')";
                }
                else
                {
                    insert_dailyprogress = "update PrismJobRunDailyProgress set depthstart=" + depthstart + ",depthend=" + depthend + "," +
                        "LastInc=" + lastinc + ",lastazm=" + lastazm + ",lasttemp=" + lasttemp + ",comments='" + txt_cmts.Text + "' where runid=" + runid + "";
                }
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_dailyprogress);
                //END DAILY PROGRESS
                //INSERT DRILLING PARAMETERS

                string pumppressure = "", flowrate = "", mudwght = "", floride = "", pulseamp = "", sand = "", solid = "";
                if (txt_pumppressure.Text != "")
                {
                    pumppressure = txt_pumppressure.Text;
                }
                else
                {
                    pumppressure = "NULL";
                }
                if (txt_flowrate.Text != "")
                {
                    flowrate = txt_flowrate.Text;
                }
                else
                {
                    flowrate = "NULL";
                }
                if (txt_mudwght.Text != "")
                {
                    mudwght = txt_mudwght.Text;
                }
                else
                {
                    mudwght = "NULL";
                }
                if (txt_floride.Text != "")
                {
                    floride = txt_floride.Text;
                }
                else
                {
                    floride = "NULL";
                }
                if (txt_pulseamp.Text != "")
                {
                    pulseamp = txt_pulseamp.Text;
                }
                else
                {
                    pulseamp = "NULL";
                }
                if (txt_sand.Text != "")
                {
                    sand = txt_sand.Text;
                }
                else
                {
                    sand = "NULL";
                }
                if (txt_solid.Text != "")
                {
                    solid = txt_solid.Text;
                }
                else
                {
                    solid = "NULL";
                }
                string insert_drillingparameters = "";
                DataTable dt_drillingparameters = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunDrillingParameters where runid=" + runid + "").Tables[0];
                if (dt_drillingparameters.Rows.Count == 0)
                {
                    insert_drillingparameters = "insert into PrismJobRunDrillingParameters(runid,pumppressure,flowrate,mudweight,chlorides,pulseamp,sand,solid)values(" +
                        "" + runid + "," + pumppressure + "," + flowrate + "," + mudwght + "," + floride + "," + pulseamp + "," + sand + "," + solid + ")";
                }
                else
                {
                    insert_drillingparameters = "update PrismJobRunDrillingParameters set pumppressure=" + pumppressure + "" +
                    ",flowrate=" + flowrate + ",mudweight=" + mudwght + ",chlorides=" + floride + "" +
                    ",pulseamp=" + pulseamp + ",sand=" + sand + ",solid=" + solid + " where runid=" + runid + "";
                }

                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_drillingparameters);
                //END OF DRILLING PARAMETERS

                //INSERT ASSETS NEEDED
                string assetneededval1 = "", assetneededval2 = "", assetneededval3 = "", assetneededval4 = "", assetneededval5 = "";
                string query_assetsneeded1 = "", query_assetsneeded2 = "", query_assetsneeded3 = "", query_assetsneeded4 = "", query_assetsneeded5 = "";
                if (radcombo_assetneeded1.SelectedValue != "")
                {
                    assetneededval1 = radcombo_assetneeded1.SelectedValue;


                }
                else
                {
                    assetneededval1 = "NULL";

                }
                if (radcombo_assetneeded2.SelectedValue != "")
                {
                    assetneededval2 = radcombo_assetneeded2.SelectedValue;

                }
                else
                {
                    assetneededval2 = "NULL";
                }
                if (radcombo_assetneeded3.SelectedValue != "")
                {
                    assetneededval3 = radcombo_assetneeded3.SelectedValue;

                }
                else
                {
                    assetneededval3 = "NULL";
                }
                if (radcombo_assetneeded4.SelectedValue != "")
                {
                    assetneededval4 = radcombo_assetneeded4.SelectedValue;

                }
                else
                {
                    assetneededval4 = "NULL";
                }
                if (radcombo_assetneeded5.SelectedValue != "")
                {
                    assetneededval5 = radcombo_assetneeded5.SelectedValue;

                }
                else
                {
                    assetneededval5 = "NULL";
                }
                DataTable dt_assetsneeded1 = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunAssetsNeeded where runid=" + runid + "").Tables[0];
                if (dt_assetsneeded1.Rows.Count > 0)
                {

                    if (assetneededval1 != "NULL")
                    {
                        query_assetsneeded1 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval1 + " where runid=" + runid + "  and serialnumber=1";

                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded1);
                    }

                    if (assetneededval2 != "NULL")
                    {
                        query_assetsneeded2 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval2 + " where runid=" + runid + "  and serialnumber=2";

                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded2);
                    }

                    if (assetneededval3 != "NULL")
                    {
                        query_assetsneeded3 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval3 + " where runid=" + runid + "  and serialnumber=3";

                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded3);
                    }

                    if (assetneededval4 != "NULL")
                    {
                        query_assetsneeded4 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval4 + " where runid=" + runid + " and serialnumber=4";

                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded4);
                    }

                    if (assetneededval5 != "NULL")
                    {
                        query_assetsneeded5 = "update PrismJobRunAssetsNeeded set assetid=" + assetneededval5 + " where runid=" + runid + " and serialnumber=5";

                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded5);
                    }
                }
                else
                {
                    query_assetsneeded1 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval1 + ",1)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded1);
                    query_assetsneeded2 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval2 + ",2)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded2);
                    query_assetsneeded3 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval3 + ",3)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded3);
                    query_assetsneeded4 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval4 + ",4)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded4);
                    query_assetsneeded5 = "insert into PrismJobRunAssetsNeeded(runid,assetid,serialnumber)values(" + runid + "," + assetneededval5 + ",5)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_assetsneeded5);
                }

                //END

                //OTHER SUPPLIES NEEDED
                //string query_otherassetsneeded1="",query_otherassetsneeded2="",query_otherassetsneeded3="",query_otherassetsneeded4="",query_otherassetsneeded5="";
                DataTable dt_othersupplyneeded = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobRunOtherSuppliesNeeded where runid=" + runid + "").Tables[0];
                if (dt_othersupplyneeded.Rows.Count > 0)
                {
                    if (txt_othersuppliesneeded1.Text != "")
                    {
                        string query_otherassetsneeded1 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded1.Text + "' where runid=" + runid + " and serialnumber=1";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded1);
                    }
                    else
                    {
                        if (dt_othersupplyneeded.Rows[0]["suppliesneeded"].ToString() != "")
                        {
                            string query_otherassetsneeded1 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded1.Text + "' where runid=" + runid + " and and serialnumber=1";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded1);
                        }
                    }
                    if (txt_othersuppliesneeded2.Text != "")
                    {
                        string query_otherassetsneeded2 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded2.Text + "' where runid=" + runid + " and serialnumber=2";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded2);
                    }
                    else
                    {
                        if (dt_othersupplyneeded.Rows[1]["suppliesneeded"].ToString() != "")
                        {
                            string query_otherassetsneeded2 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded2.Text + "' where runid=" + runid + " and serialnumber=2";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded2);
                        }
                    }
                    if (txt_othersuppliesneeded3.Text != "")
                    {
                        string query_otherassetsneeded3 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded3.Text + "' where runid=" + runid + " and serialnumber=3";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded3);
                    }
                    else
                    {
                        if (dt_othersupplyneeded.Rows[2]["suppliesneeded"].ToString() != "")
                        {
                            string query_otherassetsneeded3 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded3.Text + "' where runid=" + runid + " and serialnumber=3";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded3);
                        }
                    }
                    if (txt_othersuppliesneeded4.Text != "")
                    {
                        string query_otherassetsneeded4 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded4.Text + "' where runid=" + runid + " and serialnumber=4";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded4);
                    }
                    else
                    {
                        if (dt_othersupplyneeded.Rows[3]["suppliesneeded"].ToString() != "")
                        {
                            string query_otherassetsneeded4 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded4.Text + "' where runid=" + runid + " and serialnumber=4";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded4);
                        }
                    }
                    if (txt_othersuppliesneeded5.Text != "")
                    {
                        string query_otherassetsneeded5 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded5.Text + "' where runid=" + runid + " and serialnumber=5";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded5);
                    }
                    else
                    {
                        if (dt_othersupplyneeded.Rows[4]["suppliesneeded"].ToString() != "")
                        {
                            string query_otherassetsneeded5 = "update PrismJobRunOtherSuppliesNeeded set suppliesneeded='" + txt_othersuppliesneeded5.Text + "' where runid=" + runid + " and serialnumber=5";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded5);
                        }
                    }
                }
                else
                {
                    string query_otherassetsneeded1 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded1.Text + "',1)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded1);
                    string query_otherassetsneeded2 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded2.Text + "',2)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded2);
                    string query_otherassetsneeded3 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded3.Text + "',3)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded3);
                    string query_otherassetsneeded4 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded4.Text + "',4)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded4);
                    string query_otherassetsneeded5 = "insert into PrismJobRunOtherSuppliesNeeded(runid,suppliesneeded,serialnumber)values(" + runid + ",'" + txt_othersuppliesneeded5.Text + "',5)";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_otherassetsneeded5);
                }

            }
            //ATTACHMENT

            transaction.Commit();
            string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedTimeStamp,Source)values(" +
                    "'DDR01','Daily Run Report','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'" +
                ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','JOB')";
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
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
        //clear();
    }
    protected void btn_export_OnClick(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        excelApp.ScreenUpdating = false;
        excelApp.DisplayAlerts = false;

        object misValue = System.Reflection.Missing.Value;
        if (File.Exists(Server.MapPath("RunReport_copy.xlsx")))
        {
            File.Delete(Server.MapPath("RunReport_copy.xlsx"));
        }
        File.Copy(Server.MapPath("RunReport.xlsx"), Server.MapPath("RunReport_copy.xlsx"));
        //Opening Excel file(myData.xlsx)
        Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("RunReport_copy.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
        string currentSheet = "Sheet1";
        Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;
        excelWorksheet.Cells[2, 1] = lbl_jname.Text;
        excelWorksheet.Cells[2, 2] = Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy");
        excelWorksheet.Cells[2, 3] = txt_dailycharges.Text;
        excelWorksheet.Cells[2, 4] = lbl_sdate.Text;
        excelWorksheet.Cells[2, 5] = lbl_edate.Text;
        excelWorksheet.Cells[2, 6] = lbl_runnumber.Text;
        excelWorksheet.Cells[2, 7] = lbl_daynumber.Text;

        //DATES
        excelWorksheet.Cells[6, 1] = radcombo_hr1.Text;
        excelWorksheet.Cells[6, 2] = radcombo_hr2.Text;
        excelWorksheet.Cells[6, 3] = radcombo_hr3.Text;
        excelWorksheet.Cells[6, 4] = radcombo_hr4.Text;
        excelWorksheet.Cells[6, 5] = radcombo_hr5.Text;
        excelWorksheet.Cells[6, 6] = radcombo_hr6.Text;

        excelWorksheet.Cells[8, 1] = radcombo_hr7.Text;
        excelWorksheet.Cells[8, 2] = radcombo_hr8.Text;
        excelWorksheet.Cells[8, 3] = radcombo_hr9.Text;
        excelWorksheet.Cells[8, 4] = radcombo_hr10.Text;
        excelWorksheet.Cells[8, 5] = radcombo_hr11.Text;
        excelWorksheet.Cells[8, 6] = radcombo_hr12.Text;

        excelWorksheet.Cells[10, 1] = radcombo_hr13.Text;
        excelWorksheet.Cells[10, 2] = radcombo_hr14.Text;
        excelWorksheet.Cells[10, 3] = radcombo_hr15.Text;
        excelWorksheet.Cells[10, 4] = radcombo_hr16.Text;
        excelWorksheet.Cells[10, 5] = radcombo_hr17.Text;
        excelWorksheet.Cells[10, 6] = radcombo_hr18.Text;

        excelWorksheet.Cells[12, 1] = radcombo_hr19.Text;
        excelWorksheet.Cells[12, 2] = radcombo_hr20.Text;
        excelWorksheet.Cells[12, 3] = radcombo_hr21.Text;
        excelWorksheet.Cells[12, 4] = radcombo_hr22.Text;
        excelWorksheet.Cells[12, 5] = radcombo_hr23.Text;
        excelWorksheet.Cells[12, 6] = radcombo_hr24.Text;

        excelWorksheet.Cells[5, 9] = txt_depthstrt.Text;
        excelWorksheet.Cells[6, 9] = txt_depthend.Text;
        excelWorksheet.Cells[7, 9] = txt_lastinc.Text;
        excelWorksheet.Cells[8, 9] = txt_lastazm.Text;
        excelWorksheet.Cells[9, 9] = txt_lasttemp.Text;

        excelWorksheet.Cells[5, 12] = txt_pumppressure.Text;
        excelWorksheet.Cells[6, 12] = txt_flowrate.Text;
        excelWorksheet.Cells[7, 12] = txt_mudwght.Text;
        excelWorksheet.Cells[8, 12] = txt_floride.Text;
        excelWorksheet.Cells[9, 12] = txt_pulseamp.Text;
        excelWorksheet.Cells[10, 12] = txt_sand.Text;
        excelWorksheet.Cells[11, 12] = txt_solid.Text;


        excelWorksheet.Cells[14, 9] = radcombo_assetneeded1.Text;
        excelWorksheet.Cells[15, 9] = radcombo_assetneeded2.Text;
        excelWorksheet.Cells[16, 9] = radcombo_assetneeded3.Text;
        excelWorksheet.Cells[17, 9] = radcombo_assetneeded4.Text;
        excelWorksheet.Cells[18, 9] = radcombo_assetneeded5.Text;

        excelWorksheet.Cells[14, 11] = txt_othersuppliesneeded1.Text;
        excelWorksheet.Cells[15, 11] = txt_othersuppliesneeded2.Text;
        excelWorksheet.Cells[16, 11] = txt_othersuppliesneeded3.Text;
        excelWorksheet.Cells[17, 11] = txt_othersuppliesneeded4.Text;
        excelWorksheet.Cells[18, 11] = txt_othersuppliesneeded5.Text;

        DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunDetails where Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        string existrunid = "";
        if (dt_getdailyrun.Rows.Count > 0)
        {
            existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

        }
        DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca,PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and pj.jobid=" + radcombo_job.SelectedValue + " and pj.AssetStatus=3").Tables[0];
        for (int cat = 0; cat < dt_getassetcategories.Rows.Count; cat++)
        {
            int categno = 18 + cat;
            //for (int exc = 1; exc < 9; exc++)
            //{
            excelWorksheet.Cells[categno, 1] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            excelWorksheet.Cells[categno, 2] = dt_getassetcategories.Rows[cat]["NameofAsset"].ToString();
            excelWorksheet.Cells[categno, 3] = dt_getassetcategories.Rows[cat]["SerialNumber"].ToString();

            if (existrunid != "")
            {
                DataTable dt_getdetfrm = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "").Tables[0];
                if (dt_getdetfrm.Rows.Count > 0)
                {
                    if (dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString() != "")
                    {
                        excelWorksheet.Cells[categno, 4] = "Yes";
                        excelWorksheet.Cells[categno, 8] = dt_getdetfrm.Rows[0]["dailyrunhrs"].ToString();
                    }
                    else
                    {
                        excelWorksheet.Cells[categno, 4] = "No";
                    }

                }
            }
            else
            {
                //query_runhrdet = "select * from PrismJobRunHourDetails where assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and runid=" + existrunid + "";
            }


            excelWorksheet.Cells[categno, 5] = dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString();

            DataTable dt_gettotalrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(dailyrunhrs) as TotalRunHrs from PrismJobRunHourdetails pjr,PrismJobRunDetails r where r.runid=pjr.runid and pjr.assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + " and r.Date<='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
            if (dt_gettotalrunhrs.Rows.Count > 0)
            {
                excelWorksheet.Cells[categno, 6] = dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString();
                decimal d;
                if (dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString() != "")
                {
                    d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString()) + Convert.ToDecimal(dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString());
                }
                else
                {
                    d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString());
                }
                excelWorksheet.Cells[categno, 7] = d.ToString();
                //string existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

            }
            //= dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            //excelWorksheet.Cells[cat, 7] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            //excelWorksheet.Cells[cat, 8] = dt_getassetcategories.Rows[cat]["clientAssetName"].ToString();
            //}
        }

        //txt_dailycharges
        workbook.Save();

        //workbook.SaveAs(Server.MapPath("RunReport.xlsx"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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




        string FilePath = Server.MapPath("RunReport_copy.xlsx");
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
        //radcombo_job.ClearSelection();
        pnl_addjobs.Controls.Clear();
        radcombo_hr1.ClearSelection();
        radcombo_hr2.ClearSelection();
        radcombo_hr3.ClearSelection();
        radcombo_hr4.ClearSelection();
        radcombo_hr5.ClearSelection();
        radcombo_hr6.ClearSelection();
        radcombo_hr7.ClearSelection();
        radcombo_hr8.ClearSelection();
        radcombo_hr9.ClearSelection();
        radcombo_hr10.ClearSelection();
        radcombo_hr11.ClearSelection();
        radcombo_hr12.ClearSelection();
        radcombo_hr13.ClearSelection();
        radcombo_hr14.ClearSelection();
        radcombo_hr15.ClearSelection();
        radcombo_hr16.ClearSelection();
        radcombo_hr17.ClearSelection();
        radcombo_hr18.ClearSelection();
        radcombo_hr19.ClearSelection();
        radcombo_hr20.ClearSelection();
        radcombo_hr21.ClearSelection();
        radcombo_hr22.ClearSelection();
        radcombo_hr23.ClearSelection();
        radcombo_hr24.ClearSelection();
        txt_depthstrt.Text = "";
        txt_depthend.Text = "";
        txt_lastinc.Text = "";
        txt_lastazm.Text = "";
        txt_lasttemp.Text = "";
        txt_cmts.Text = "";
        txt_pumppressure.Text = "";
        txt_flowrate.Text = "";
        txt_mudwght.Text = "";
        txt_floride.Text = "";
        txt_pulseamp.Text = "";
        txt_sand.Text = "";
        txt_solid.Text = "";
        radcombo_assetneeded1.ClearSelection();
        radcombo_assetneeded2.ClearSelection();
        radcombo_assetneeded3.ClearSelection();
        radcombo_assetneeded4.ClearSelection();
        radcombo_assetneeded5.ClearSelection();
        txt_othersuppliesneeded1.Text = "";
        txt_othersuppliesneeded2.Text = "";
        txt_othersuppliesneeded3.Text = "";
        txt_othersuppliesneeded4.Text = "";
        txt_othersuppliesneeded5.Text = "";
        txt_dailycharges.Text = "";
    }
}