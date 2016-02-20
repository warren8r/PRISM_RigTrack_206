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

public partial class Modules_Configuration_Manager_DailyrunReportPRM : System.Web.UI.Page
{
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlCommand cmdInsert;
    SqlTransaction transaction;
    public static DataTable dt_Users,dt_rig,dt_mwdoperators;
    string tdstyle = "style='border:solid 1px #000000;text-align:center;background-color:#C4D79B; color:#121212'";
    protected void Page_Load(object sender, EventArgs e)
    {
        table_top1.Visible = false;
        table_top3.Visible = false;
        grid_hours.Visible = false;
        if (!IsPostBack)
        {
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from manageJobOrders where (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "jobname", "jid", "0");
            dt_Users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Users").Tables[0];
            dt_rig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from RigTypes").Tables[0];
            dt_mwdoperators = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId").Tables[0];
            for (int run = 1; run <= 30; run++)
            {
                cmb_runno.Items.Insert(run-1, new RadComboBoxItem(run.ToString()));                    
            }
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        pnl_addjobs.Controls.Clear();

        DateTime dtstart = new DateTime();
        DateTime dtend = new DateTime();
        DateTime dtstart_service = new DateTime();
        DateTime dtend_service = new DateTime();
         DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j,RigTypes r where m.jobtype=j.jobtypeid and "+
            " m.rigtypeid=r.rigtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
         DataTable dt_getassetcategories = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select  pn.AssetName as NameofAsset,pn.Id as prismassetid,* from PrismJobAssignedAssets pj,Prism_Assets pa,clientAssets ca," +
            " PrismAssetName pn where pa.Id=pj.AssetID and ca.clientAssetID=pa.AssetCategoryId and pa.AssetName=pn.Id and " +
            " pj.jobid=" + radcombo_job.SelectedValue + " and pj.AssetStatus=1").Tables[0];
         if (dtdates.Rows.Count > 0)
         {
             table_top1.Visible = true;
             pnl_addjobs.Visible = true;
             table_top3.Visible = true;
             grid_hours.Visible = true;
             Sqlmwdopertors.SelectCommand = "select * from PrismJobAssignedPersonals jp,Users u where u.userID=jp.UserId and Jobid=" + radcombo_job.SelectedValue;
            DataRow[] row_operator=dt_Users.Select("userID="+dtdates.Rows[0]["opManagerId"].ToString());
            DataRow[] row_rig = dt_rig.Select("rigtypeid=" + dtdates.Rows[0]["rigtypeid"].ToString());
            lbl_operator.Text = row_operator[0]["firstname"].ToString() + " " + row_operator[0]["lastname"].ToString();
            lbl_location.Text = dtdates.Rows[0]["primaryAddress1"].ToString();
            lbl_rig.Text = row_rig[0]["rigtypename"].ToString();
            lbl_jobsno.Text = dtdates.Rows[0]["jobid"].ToString();
             //Services Display
            pnl_addjobs.Controls.Add(new LiteralControl("<table border='0' style='width:500px'>"));
            ////pnl_addjobs.Controls.Add(new LiteralControl("<td style='width:200px'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>"));
            ////pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff;'>Daily&#160;Charge&#40;&#36;&#41;</td>"));
            //lbl_jname.Text = dtdates.Rows[0]["jobname"].ToString();
            //lbl_jtype.Text = dtdates.Rows[0]["jtypename"].ToString();
            //lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString()).ToString("MM/dd/yyyy");
            //lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString()).ToString("MM/dd/yyyy");
            dtstart = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            dtend = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());

            dtstart_service = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            dtend_service = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());
            TimeSpan ts = new TimeSpan();
            ts = dtend - dtstart;
            int days = ts.Days;
            //pnl_addjobs.Controls.Add(new LiteralControl("<tr><td "+tdstyle+">"));
            //string datecol = dtstart_service.ToString("MM/dd/yyyy");
            //pnl_addjobs.Controls.Add(new LiteralControl(Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM/dd/yyyy")));
            //pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
            //pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td><table>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td "+tdstyle+">Serial Number</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Asset Category</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Asset Name</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Kit Name</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">In Use</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Previous Tools Hours</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">TOTAL RUN Hrs</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Cumilative Tools Hours</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td " + tdstyle + ">Daily Run Hours</td>"));

            //pnl_addjobs.Controls.Add(new LiteralControl("<td "+tdstyle+">Daily RUN Hrs</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            Double tot = 0.00;
            DataTable dt_getdailyrun = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, 
                "select * from PrismJobRunDetails where  jid=" + radcombo_job.SelectedValue + "").Tables[0];
            //Date='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "' and
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


                //DataTable dt_gettotalrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                //    "select sum(dailyrunhrs) as TotalRunHrs from PrismJobRunHourdetails pjr,PrismJobRunDetails r where "+
                //    " r.runid=pjr.runid and pjr.assetid=" + dt_getassetcategories.Rows[cat]["prismassetid"].ToString() + ""+
                //    " and r.Date<='" + Convert.ToDateTime(radtxt_start.SelectedDate).ToString("MM-dd-yyyy") + "'").Tables[0];
                //if (dt_gettotalrunhrs.Rows.Count > 0)
                //{
                //    txt_totrunhrs.Text = dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString();
                //    decimal d;
                //    if (dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString() != "")
                //    {
                //        d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString()) + Convert.ToDecimal(dt_gettotalrunhrs.Rows[0]["TotalRunHrs"].ToString());
                //    }
                //    else
                //    {
                //        d = Convert.ToDecimal(dt_getassetcategories.Rows[cat]["previoususedhrs"].ToString());
                //    }
                //    txt_cumulative.Text = d.ToString();
                //    //string existrunid = dt_getdailyrun.Rows[0]["runid"].ToString();

                //}
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
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'></td>"));
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
    protected void grid_hours_ItemCommand(object sender, GridCommandEventArgs e)
    {
        table_top1.Visible = true;
        pnl_addjobs.Visible = true;
        table_top3.Visible = true;
        grid_hours.Visible = true;
    }
}