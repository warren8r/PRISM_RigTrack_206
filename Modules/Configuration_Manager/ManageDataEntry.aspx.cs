using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using System.Globalization;

public partial class Modules_Configuration_Manager_ManageDataEntry : System.Web.UI.Page
{
    ClientMaster cl = new ClientMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from manageJobOrders where (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "jobname", "jid", "0");
        }
        //else
        //{
        bindtextboxesdates();
        //}
    }
    protected void radcombo_job_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            radtxt_start.SelectedDate = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            radtxt_end.SelectedDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());
            radtxt_start.MinDate = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            radtxt_start.MaxDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());

            radtxt_end.MinDate = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString());
            radtxt_end.MaxDate = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString());

        }
        //radtxt_start.MinDate=
    }

    public void bindtextboxesdates()
    {
        pnl_addjobs.Controls.Clear();

        Double d_assettotal = 0.00;
        Double d_servicetotal = 0.00;
        DateTime dtstart = new DateTime();
        DateTime dtend = new DateTime();
        DateTime dtstart_service = new DateTime();
        DateTime dtend_service = new DateTime();
        DateTime dtexist = new DateTime();
        DateTime dtexist_service = new DateTime();

        DateTime dtassettotal = new DateTime();
        DateTime dtservicetotal = new DateTime();

        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        DataTable dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select pa.AssetName AS AssetName1,* from Prism_Assets p,PrismAssetName pa where p.AssetName=pa.ID and P.id  in (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved' and PrismJobAssignedAssets.JobId=" + radcombo_job.SelectedValue + ") ").Tables[0];
        if (dtdates.Rows.Count > 0 && radtxt_start.SelectedDate != null)
        {
            pnl_addjobs.Controls.Add(new LiteralControl("<table border='0' style='width:500px'><tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='width:200px'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff;'>Daily&#160;Charge&#40;&#36;&#41;</td>"));
            lbl_jname.Text = dtdates.Rows[0]["jobname"].ToString();
            lbl_jtype.Text = dtdates.Rows[0]["jtypename"].ToString();
            lbl_sdate.Text = Convert.ToDateTime(dtdates.Rows[0]["startdate"].ToString()).ToString("MM/dd/yyyy");
            lbl_edate.Text = Convert.ToDateTime(dtdates.Rows[0]["enddate"].ToString()).ToString("MM/dd/yyyy");
            dtstart = Convert.ToDateTime(radtxt_start.SelectedDate);
            dtend = Convert.ToDateTime(radtxt_end.SelectedDate);

            dtstart_service = Convert.ToDateTime(radtxt_start.SelectedDate);
            dtend_service = Convert.ToDateTime(radtxt_end.SelectedDate);
            TimeSpan ts = new TimeSpan();
            ts = dtend - dtstart;
            int days = ts.Days;
            for (int column = 0; column <= days; column++)
            {
                pnl_addjobs.Controls.Add(new LiteralControl("<td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>"));
                string datecol = dtstart.AddDays(column).ToString("MM/dd/yyyy");
                pnl_addjobs.Controls.Add(new LiteralControl(datecol));
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
            }
            pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td  style='border:solid 1px #000000;text-align:left;background-color:#597791; color:#ffffff; width:200px'>Asset&#160;Names</td></tr>"));
            Double tot = 0.00;
            for (int i = 0; i < dt_Table.Rows.Count; i++)
            {
                pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'>" + dt_Table.Rows[i]["AssetName1"].ToString() + "</td>"));

                //string price = string.Format(cultureInfo, "{0:C}", dt_Table.Rows[i]["Cost"].ToString());

                if (dt_Table.Rows[i]["Cost"].ToString() != "")
                {
                    decimal d = Convert.ToDecimal(dt_Table.Rows[i]["Cost"].ToString());
                    string html = String.Format("{0:C}", d);
                    pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='color:white;background-color:#528ED4'>" + html + "</td>"));
                    d_assettotal += Convert.ToDouble(dt_Table.Rows[i]["Cost"].ToString());
                }
                else
                {
                    pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='color:white;background-color:#528ED4'></td>"));
                    d_assettotal += 0.00;
                }


                for (int j = 0; j <= days; j++)
                {

                    dtexist = dtstart.AddDays(j);
                    DataTable dt_existingjobrates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataRates where AssetId=" + dt_Table.Rows[i]["Id"].ToString() + " and Date='" + dtexist.ToString("yyyy-MM-dd") + "'").Tables[0];
                    pnl_addjobs.Controls.Add(new LiteralControl("<td align='center'>"));
                    TextBox txt1 = new TextBox();
                    txt1.TabIndex = (short)j;

                    txt1.ID = "txt_asset_" + dt_Table.Rows[i]["Id"].ToString() + "_" + i + "_" + j + "";
                    txt1.Width = 70;
                    DateTime dtexistford = dtexist;
                    DateTime current = DateTime.Now.Date;
                    TimeSpan ts_datenw = current - dtexistford;
                    int d = ts_datenw.Days;
                    if (d < 0)
                    {
                        txt1.Enabled = false;
                    }
                    else
                    {
                        txt1.Enabled = true;
                        
                    }
                    if (dt_existingjobrates.Rows.Count == 0)
                    {
                        txt1.Text = "";
                    }
                    else
                    {
                        txt1.Text = dt_existingjobrates.Rows[0]["rate"].ToString();
                        tot += Convert.ToDouble(dt_existingjobrates.Rows[0]["rate"].ToString());

                    }

                    pnl_addjobs.Controls.Add(txt1);
                    pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                }



                pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            }
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Asset Charge Total</td><td ></td>"));
            dtassettotal = dtstart;
            for (int total = 0; total <= days; total++)
            {

                DataTable dt_existtotals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(rate) as total from JobDataRates where Date='" + dtassettotal.AddDays(total).ToString("yyyy-MM-dd") + "' and AssetId<>''").Tables[0];
                pnl_addjobs.Controls.Add(new LiteralControl("<td  style='background-color:Gray'>"));
                if (dt_existtotals.Rows.Count > 0)
                {
                    if (dt_existtotals.Rows[0]["total"].ToString() != "")
                    {
                        pnl_addjobs.Controls.Add(new LiteralControl("$" + dt_existtotals.Rows[0]["total"].ToString()));
                    }
                    else
                    {
                        decimal d = 0;
                        pnl_addjobs.Controls.Add(new LiteralControl("$0.00"));
                    }
                }
                else
                {
                    pnl_addjobs.Controls.Add(new LiteralControl("$0.00"));
                }
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
            }
            pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Services</td></tr>"));
            DataTable dt_Table1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismService").Tables[0];
            for (int k = 0; k < dt_Table1.Rows.Count; k++)
            {
                pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left' style='color:white;background-color:#528ED4'>" + dt_Table1.Rows[k]["ServiceName"].ToString() + "</td>"));
                //pnl_addjobs.Controls.Add(new LiteralControl("<td align='center'>" + dt_Table1.Rows[k]["Cost"].ToString() + "</td>"));
                if (dt_Table1.Rows[k]["Cost"].ToString() != "")
                {
                    decimal d = Convert.ToDecimal(dt_Table1.Rows[k]["Cost"].ToString());
                    string html = String.Format("{0:C}", d);
                    pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='color:white;background-color:#528ED4'>" + html + "</td>"));
                    d_servicetotal += Convert.ToDouble(dt_Table1.Rows[k]["Cost"].ToString());
                }
                else
                {
                    pnl_addjobs.Controls.Add(new LiteralControl("<td align='center' style='color:white;background-color:#528ED4'>$0.00</td>"));
                    d_servicetotal += 0.00;
                }
                for (int l = 0; l <= days; l++)
                {
                    dtexist_service = dtstart_service.AddDays(l);
                    DataTable dt_existingjobrates1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataRates where ServiceId=" + dt_Table1.Rows[k]["Id"].ToString() + " and Date='" + dtexist_service.ToString("yyyy-MM-dd") + "'").Tables[0];
                    pnl_addjobs.Controls.Add(new LiteralControl("<td align='center'>"));
                    TextBox txt2 = new TextBox();
                    txt2.ID = "txt_Service_" + dt_Table1.Rows[k]["ID"].ToString() + "_" + k + "_" + l + "";
                    txt2.Width = 70;
                    txt2.TabIndex = (short)l;
                    if (dt_existingjobrates1.Rows.Count == 0)
                    {
                        txt2.Text = "";
                    }
                    else
                    {
                        txt2.Text = dt_existingjobrates1.Rows[0]["rate"].ToString();
                    }


                    pnl_addjobs.Controls.Add(txt2);
                    pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
                    //pnl_addjobs.Controls.Add(new LiteralControl("<td align='center'>"));


                    //txt1.ID = k + "_" + l + "_txt_Service";

                }
                pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));

            }
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Services Total</td><td ></td>"));
            dtassettotal = dtstart;
            for (int total = 0; total <= days; total++)
            {

                DataTable dt_existtotals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select sum(rate) as total from JobDataRates where Date='" + dtassettotal.AddDays(total).ToString("yyyy-MM-dd") + "' and ServiceId<>''").Tables[0];
                pnl_addjobs.Controls.Add(new LiteralControl("<td  style='background-color:Gray'>"));
                if (dt_existtotals.Rows[0]["total"].ToString() != "")
                {
                    //string name = cl.FirstName + " " + cl.LastName;
                    pnl_addjobs.Controls.Add(new LiteralControl("$" + dt_existtotals.Rows[0]["total"].ToString()));
                    //string insertapproval = "insert into JobDataApproval(jid,Date,submittedby)values(" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(dtassettotal.AddDays(total).ToString()) + "','" + name + "')";

                }
                else
                {

                    pnl_addjobs.Controls.Add(new LiteralControl("$0.00"));
                }
                pnl_addjobs.Controls.Add(new LiteralControl("</td>"));
            }
            pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='height:15px'></td></tr>"));
            //Last Submitted By
            //pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Submitted By</td><td></td>"));
            //DateTime dtassettotal_submby = dtstart;
            //for (int total = 0; total <= days; total++)
            //{


            //}

            //pnl_addjobs.Controls.Add(new LiteralControl("</tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("<tr><td ><table width='100%'  cellspacing='5' cellpadding='2'><tr><td align='center' style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Submitted By</td></tr><tr><td align='center' style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Approved</td></tr><tr><td align='center' style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Approved by</td></tr><tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Select Activity</td></tr><tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Add Notes</td></tr></table></td><td></td>"));
            DateTime dtassettotal_submbyapprove = dtstart;
            DateTime dtassettotal_notes = dtstart;
            for (int total = 0; total <= days; total++)
            {

                DataTable dt_existtotals_submittedby = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataApproval where Date='" + dtassettotal_submbyapprove.AddDays(total).ToString("yyyy-MM-dd") + "' and Approvedby<>''").Tables[0];
                pnl_addjobs.Controls.Add(new LiteralControl("<td  style='background-color:white'><table cellspacing='5' cellpadding='2'>"));
                LinkButton lnk_approve = new LinkButton();
                LinkButton lnk_notes = new LinkButton();
                RadComboBox cmb = new RadComboBox();
                cmb.EmptyMessage = "- Select -";
                cmb.DataSource = sql_status;
                cmb.DataTextField = "rigstatuses";
                cmb.DataValueField = "sid";
                cmb.DataBind();

                lnk_approve.ID = "lnk_approve_" + total;
                lnk_notes.ID = "lnk_notes_" + total;
                lnk_notes.Text = "View/Add Notes";
                cmb.ID = "radcombo_" + total;
                cmb.Width = 100;
                string approvedby = "";
                if (dt_existtotals_submittedby.Rows.Count > 0)
                {
                    lnk_approve.Text = "Approved";
                    lnk_approve.Enabled = false;
                    approvedby = dt_existtotals_submittedby.Rows[0]["approvedby"].ToString();
                    //string insertapproval = "insert into JobDataApproval(jid,Date,submittedby)values(" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(dtassettotal.AddDays(total).ToString()) + "','" + name + "')";

                }
                else
                {
                    lnk_approve.Text = "Approve";
                    lnk_approve.Enabled = true;
                    lnk_approve.OnClientClick = String.Format("togglePopupModality1(\"{0}\"); return false;", dtassettotal_submbyapprove.AddDays(total).ToString("yyyy-MM-dd"));
                }



                DataTable dt_existtotals_submittedby1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataApproval where Date='" + dtassettotal_submbyapprove.AddDays(total).ToString("yyyy-MM-dd") + "'").Tables[0];
                pnl_addjobs.Controls.Add(new LiteralControl("<tr><td  style='background-color:LightBlue;height:15px'>"));
                if (dt_existtotals_submittedby1.Rows.Count > 0)
                {
                    //string name = cl.FirstName + " " + cl.LastName;
                    pnl_addjobs.Controls.Add(new LiteralControl(dt_existtotals_submittedby1.Rows[0]["submittedby"].ToString()));
                    cmb.SelectedValue = dt_existtotals_submittedby1.Rows[0]["rigStatusid"].ToString();
                    //string insertapproval = "insert into JobDataApproval(jid,Date,submittedby)values(" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(dtassettotal.AddDays(total).ToString()) + "','" + name + "')";

                }

                pnl_addjobs.Controls.Add(new LiteralControl("</td></tr>"));


                pnl_addjobs.Controls.Add(new LiteralControl("<tr><td>"));
                pnl_addjobs.Controls.Add(lnk_approve);
                pnl_addjobs.Controls.Add(new LiteralControl("</td></tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='background-color:Gray;height:15px'>"));
                pnl_addjobs.Controls.Add(new LiteralControl(approvedby));
                pnl_addjobs.Controls.Add(new LiteralControl("</td></tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<tr><td>"));
                pnl_addjobs.Controls.Add(cmb);
                pnl_addjobs.Controls.Add(new LiteralControl("</td></tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<tr><td>"));
                pnl_addjobs.Controls.Add(lnk_notes);
                lnk_notes.OnClientClick = String.Format("togglePopupModality1_notes(\"{0}\",\"{1}\"); return false;", dtassettotal_submbyapprove.AddDays(total).ToString("yyyy-MM-dd"), radcombo_job.SelectedValue);
                lnk_approve.OnClientClick = String.Format("togglePopupModality1(\"{0}\",\"{1}\"); return false;", dtassettotal_submbyapprove.AddDays(total).ToString("yyyy-MM-dd"), radcombo_job.SelectedValue);
                //radwindow_notes.NavigateUrl = "AddNotestoDataEntry.aspx?jid=" + radcombo_job.SelectedValue + "&date=" + dtassettotal_notes.AddDays(total).ToString("yyyy-MM-dd") + "";
                pnl_addjobs.Controls.Add(new LiteralControl("</td></tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("</table></td>"));
            }


            //pnl_addjobs.Controls.Add(new LiteralControl("<tr><td style='border:solid 1px #000000;text-align:center;background-color:#597791; color:#ffffff'>Service Cost Total</td><td>" + d_servicetotal + "</td></tr>"));
            pnl_addjobs.Controls.Add(new LiteralControl("</table>"));
        }


    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        pnl_addjobs.Controls.Clear();
        td_jobdet.Attributes.Add("style", "display:block");
        td_button.Attributes.Add("style", "display:block");
        bindtextboxesdates();
        pnl_addjobs.Visible = true;

    }

    protected void btn_saveupdate_Click(object sender, EventArgs e)
    {


        DateTime dtstart = new DateTime();
        DateTime dtend = new DateTime();
        DataTable dtdates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select j.jobtype as jtypename,* from manageJobOrders m,jobTypes j where m.jobtype=j.jobtypeid and jid=" + radcombo_job.SelectedValue + "").Tables[0];
        DataTable dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select pa.AssetName AS AssetName1,* from Prism_Assets p,PrismAssetName pa where p.AssetName=pa.ID and P.id  in (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved' and PrismJobAssignedAssets.JobId=" + radcombo_job.SelectedValue + ") ").Tables[0];
        if (dtdates.Rows.Count > 0)
        {
            dtstart = Convert.ToDateTime(radtxt_start.SelectedDate);
            dtend = Convert.ToDateTime(radtxt_end.SelectedDate);
            TimeSpan ts = new TimeSpan();
            ts = dtend - dtstart;
            int days = ts.Days;
            //for (int column = 0; column <= days; column++)
            //{

            //    string datecol = dtstart.AddDays(column).ToString("MM/dd/yyyy");

            //}
            DateTime dt_current = new DateTime();
            DateTime dt_current_service = new DateTime();
            dt_current = dtstart;
            dt_current_service = dtstart;
            ClientMaster userMaster = new ClientMaster();
            userMaster = (ClientMaster)Session["UserMasterDetails"];
            string name = userMaster.FirstName + " " + userMaster.LastName;
            for (int i = 0; i < dt_Table.Rows.Count; i++)
            {

                for (int j = 0; j <= days; j++)
                {
                    dt_current = dtstart.AddDays(j);
                    string txt_id = "txt_asset_" + dt_Table.Rows[i]["Id"].ToString() + "_" + i + "_" + j + "";
                    string str_rigstatus = "radcombo_" + j;
                    RadComboBox radcombo_rigstatus = pnl_addjobs.FindControl(str_rigstatus) as RadComboBox;
                    TextBox text = pnl_addjobs.FindControl(txt_id) as TextBox;
                    //TextBox txt1 = (TextBox)pnl_addjobs.FindControl(txt_id);
                    string insert_rigstatusdate = "";
                    if (radcombo_rigstatus.Text != "")
                    {
                        DataTable dt_rigstatus = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataApproval where Date='" + dt_current.ToString("yyyy-MM-dd") + "'").Tables[0];
                        if (dt_rigstatus.Rows.Count == 0)
                        {
                            insert_rigstatusdate = "insert into JobDataApproval(jid,Date,rigStatusid,submittedby)values(" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(dt_current.ToString()) + "'," + radcombo_rigstatus.SelectedValue + ",'" + name + "')";
                        }
                        else
                        {
                            insert_rigstatusdate = "update JobDataApproval set rigStatusid=" + radcombo_rigstatus.SelectedValue + " where jid=" + radcombo_job.SelectedValue + " and Date='" + DatesbetweenDatatable.getdatetimeformat(dt_rigstatus.Rows[0]["Date"].ToString()) + "'";
                        }
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insert_rigstatusdate);
                    }
                    if (text.Text != "")
                    {

                        DataTable dt_existingjobrates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataRates where AssetId=" + dt_Table.Rows[i]["Id"].ToString() + " and Date='" + dt_current.ToString("yyyy-MM-dd") + "'").Tables[0];
                        //DataRow[] dr_exist=dt_existingjobrates.Select("AssetId=" + dt_Table.Rows[i]["Id"].ToString() + " and Date='" + DatesbetweenDatatable.getdatetimeformat(dt_current.AddDays(j).ToString()) + "'");
                        if (dt_existingjobrates.Rows.Count == 0)
                        {
                            string insert_Query = "insert into JobDataRates(jid,Date,AssetId,rate)values(" + radcombo_job.SelectedValue + "," +
                                " '" + DatesbetweenDatatable.getdatetimeformat(dt_current.ToString()) + "'," + dt_Table.Rows[i]["Id"].ToString() + "," + text.Text + ")";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insert_Query);



                        }
                        else
                        {
                            string update_Query = "update JobDataRates set rate=" + text.Text + " where AssetId=" + dt_Table.Rows[i]["Id"].ToString() + " and Date='" + DatesbetweenDatatable.getdatetimeformat(dt_existingjobrates.Rows[0]["Date"].ToString()) + "'";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_Query);
                        }

                    }
                    else
                    {

                    }

                }

            }
            DataTable dt_Table1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismService").Tables[0];
            for (int k = 0; k < dt_Table1.Rows.Count; k++)
            {
                pnl_addjobs.Controls.Add(new LiteralControl("<tr>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='left'>" + dt_Table1.Rows[k]["ServiceName"].ToString() + "</td>"));
                pnl_addjobs.Controls.Add(new LiteralControl("<td align='center'>" + dt_Table1.Rows[k]["Cost"].ToString() + "</td>"));
                for (int l = 0; l <= days; l++)
                {
                    dt_current_service = dtstart.AddDays(l);
                    string txt_id2 = "txt_Service_" + dt_Table1.Rows[k]["ID"].ToString() + "_" + k + "_" + l + "";

                    TextBox text_service = pnl_addjobs.FindControl(txt_id2) as TextBox;
                    if (text_service.Text != "")
                    {
                        DataTable dt_existingjobrates = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataRates where ServiceId=" + dt_Table1.Rows[k]["Id"].ToString() + " and Date='" + dt_current_service.ToString("yyyy-MM-dd") + "'").Tables[0];
                        //DataRow[] dr_exist=dt_existingjobrates.Select("AssetId=" + dt_Table.Rows[i]["Id"].ToString() + " and Date='" + DatesbetweenDatatable.getdatetimeformat(dt_current.AddDays(j).ToString()) + "'");
                        if (dt_existingjobrates.Rows.Count == 0)
                        {
                            string insert_Query = "insert into JobDataRates(jid,Date,ServiceId,rate)values(" + radcombo_job.SelectedValue + "," +
                                " '" + DatesbetweenDatatable.getdatetimeformat(dt_current_service.ToString()) + "'," + dt_Table1.Rows[k]["ID"].ToString() + "," + text_service.Text + ")";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insert_Query);
                            //DataTable dtgetexist=
                            //string insertapproval = "insert into JobDataApproval(jid,Date,submittedby)values(" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(dt_current.ToString()) + "','" + name + "')";
                            //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertapproval);
                        }
                        else
                        {
                            string update_Query = "update JobDataRates set rate=" + text_service.Text + " where ServiceId=" + dt_Table1.Rows[k]["ID"].ToString() + " and Date='" + DatesbetweenDatatable.getdatetimeformat(dt_existingjobrates.Rows[0]["Date"].ToString()) + "'";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_Query);
                        }

                    }
                    else
                    {

                    }
                }
            }

            //

            //DataTable dt_existingjobrates12 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataRates").Tables[0];
            //for (int tot = 0; tot < dt_existingjobrates12.Rows.Count; tot++)
            //{
            //    string insertapproval = "insert into JobDataApproval(jid,Date,submittedby)values(" + radcombo_job.SelectedValue + ",'" + DatesbetweenDatatable.getdatetimeformat(dt_existingjobrates12.Rows[tot]["Date"].ToString()) + "','" + name + "')";
            //    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertapproval);
            //}

        }
        lbl_message.Text = "Data Saved Successfully";
        lbl_message.ForeColor = Color.Green;
        bindtextboxesdates();
    }
}