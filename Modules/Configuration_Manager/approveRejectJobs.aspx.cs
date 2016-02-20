using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;

public partial class Modules_Configuration_Manager_approveRejectJobs : System.Web.UI.Page
{
    MDM.Collector Collector = new MDM.Collector();
    ClientMaster userMaster = new ClientMaster();
    public static DataTable dt_Rignames = new DataTable();
    public static DataTable dt_users = new DataTable();
    string notificationWays = "";
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction transaction;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            userMaster = (ClientMaster)Session["UserMasterDetails"];
        }
        catch (Exception ex)
        {
            userMaster = null;
        }

        if (userMaster == null)
        {
            Response.Redirect("~/ClientLogin.aspx");
        }
        if (!IsPostBack)
        {
            dt_Rignames = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from RigTypes").Tables[0];
            radtxt_from.SelectedDate = DateTime.Now.AddMonths(-1);
            radtxt_to.SelectedDate = DateTime.Now.AddDays(5);
            dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users").Tables[0];
        }
        string lable = lbl_docid.Text;
        if (lable != "")
        {
            radgrid2bind();
        }
    }
    protected void radgrid_managejobs_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "viewedit")
        {
            id_viewpart.Visible = true;

            GridDataItem editedItem = e.Item as GridDataItem;
            //editedItem.BackColor = System.Drawing.Color.Green;
            Label lbl_jobid = (Label)editedItem.FindControl("lbl_jobid");
            lbl_docid.Text = lbl_jobid.Text;
            RadGrid1.Rebind();
            DataTable dt=new DataTable();
            string query="";
            if (radcombo_userstatus.SelectedValue == "Approved")
            {
                query = "SELECT * FROM [RigTypes] where rigtypeid  in (select rigtypeid from managejoborders where status='Approved') ";
            }
            else
            {
                query = "SELECT * FROM [RigTypes] where rigtypeid not in (select rigtypeid from managejoborders where status='Approved') ";
            }
            dt=SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_rigtype, dt, "rigtypename", "rigtypeid", "0");
            //DataSourceID="SqlDataSource3" DataTextField="rigtypename" DataValueField="rigtypeid"
            DataTable dt_existjobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from manageJobOrders where jid=" + lbl_jobid.Text + "").Tables[0];
            if (dt_existjobs.Rows.Count > 0)
            {
                if (dt_existjobs.Rows[0]["bitActive"].ToString() == "True")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }
                radtxt_jobname.Text = dt_existjobs.Rows[0]["jobname"].ToString();
                txtAssetNumber.Text = dt_existjobs.Rows[0]["jobid"].ToString();
                radcombo_jobtype.SelectedValue = dt_existjobs.Rows[0]["jobtype"].ToString();
                date_start.SelectedDate = Convert.ToDateTime(dt_existjobs.Rows[0]["startdate"].ToString());
                date_stop.SelectedDate = Convert.ToDateTime(dt_existjobs.Rows[0]["enddate"].ToString());
                radtxt_cost.Text = dt_existjobs.Rows[0]["cost"].ToString();
                radcombo_customer.SelectedValue = dt_existjobs.Rows[0]["Customer"].ToString();
                txtprimaryAddress1.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                ddlPrimaryCountry.SelectedText = dt_existjobs.Rows[0]["primaryCountry"].ToString();
                txtprimaryAddress2.Text = dt_existjobs.Rows[0]["primaryAddress2"].ToString();
                ddlPrimaryState.SelectedText = dt_existjobs.Rows[0]["primaryState"].ToString();
                txtprimaryCity.Text = dt_existjobs.Rows[0]["primaryCity"].ToString();
                txtprimaryPostalCode.Text = dt_existjobs.Rows[0]["primaryPostalCode"].ToString();
                txtPrimaryFirst.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                txtPrimaryLast.Text = dt_existjobs.Rows[0]["primaryLast"].ToString();
                txtPrimaryPhone1.Text = dt_existjobs.Rows[0]["primaryPhone1"].ToString();
                txtPrimaryPhone2.Text = dt_existjobs.Rows[0]["primaryPhone2"].ToString();
                txtPrimaryEmail.Text = dt_existjobs.Rows[0]["primaryEmail"].ToString();


                //SECONDARY INFO
                txtSecondaryAddress1.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                ddlSecondaryCountry.SelectedText = dt_existjobs.Rows[0]["primaryCountry"].ToString();
                txtSecondaryAddress2.Text = dt_existjobs.Rows[0]["primaryAddress2"].ToString();
                ddlSecondaryState.SelectedText = dt_existjobs.Rows[0]["primaryState"].ToString();
                txtSecondaryCity.Text = dt_existjobs.Rows[0]["primaryCity"].ToString();
                txtSecondaryPostalCode.Text = dt_existjobs.Rows[0]["primaryPostalCode"].ToString();
                txtSecondaryFirst.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                txtSecondaryLast.Text = dt_existjobs.Rows[0]["primaryLast"].ToString();
                txtSecondaryPhone1.Text = dt_existjobs.Rows[0]["primaryPhone1"].ToString();
                txtSecondaryPhone2.Text = dt_existjobs.Rows[0]["primaryPhone2"].ToString();
                txtSecondaryEMail.Text = dt_existjobs.Rows[0]["primaryEmail"].ToString();
                radtxt_notes.Text = dt_existjobs.Rows[0]["salesnotes"].ToString();
                //radcombo_opmngers.SelectedValue = dt_existjobs.Rows[0]["opManagerId"].ToString();
                radtxt_opnotes.Text = dt_existjobs.Rows[0]["opmgrnotes"].ToString();
                radcombo_rigtype.SelectedValue = dt_existjobs.Rows[0]["rigtypeid"].ToString(); 
                if (dt_existjobs.Rows[0]["status"].ToString() == "Approved")
                {
                    radcombo_pmanager.SelectedValue = dt_existjobs.Rows[0]["programManagerId"].ToString();
                    btn_approve.Enabled = false;
                    btn_reject.Enabled = true;


                }
                else if (dt_existjobs.Rows[0]["status"].ToString() == "Rejected")
                {
                    btn_approve.Enabled = true;
                    btn_reject.Enabled = false;
                }
                else
                {
                    btn_approve.Enabled = true;
                    btn_reject.Enabled = true;
                }
                radgrid2bind();
            }

        }
    }
    protected void btn_createrig_Click(object sender, EventArgs e)
    {
        string str_jobtype_insert = "insert into RigTypes(rigtypename,rigtypedesc)values('" + radtxt_tigtypename.Text + "','" + radtxt_rigtypedesc.Text + "')";
        int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_jobtype_insert);
        if (cnt > 0)
        {
            lbl_rigtypemsg.Text = "Rig Type Created Successfully";
            lbl_rigtypemsg.ForeColor = Color.Green;
        }
        else
        {
            lbl_rigtypemsg.Text = "Rig Type not created";
            lbl_rigtypemsg.ForeColor = Color.Red;
        }
        radcombo_rigtype.DataBind();
        RadWindow2.VisibleOnPageLoad = true;
        radtxt_tigtypename.Text = "";
        radtxt_rigtypedesc.Text = "";

    }
    public void radgrid2bind()
    {
        string qradgrid = "SELECT  etod.jid,etod.documentid,etod.UserID,etod.UploadedDate,(u.firstName+' '+u.lastName) as UserName, e.jid,d.DocumentDisplayName,d.DocumentName from" +
                            " JobOrderDocuments etod, manageJobOrders e, documents d, Users u where u.userID=etod.UserID and e.jid=etod.jid and d.DocumentID=etod.DocumentID and " +
                            " e.jid=" + lbl_docid.Text + " and etod.type='JO'  order by etod.jid desc";
        DataTable dt_radgrid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, qradgrid).Tables[0];
        RadGrid2.DataSource = dt_radgrid;
        RadGrid2.DataBind();
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        id_viewpart.Visible = false;
        reset();
    }
    protected void radgrid_managejobs_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            //  GridColumn column = RadGrid1.MasterTableView.GetColumn("GIs");
            Label lbl_rigname = (Label)dataItem.FindControl("lbl_rigname");
            Label lbl_rigtypeid = (Label)dataItem.FindControl("lbl_rigtypeid");

            if (lbl_rigtypeid.Text != "" && lbl_rigtypeid.Text != "&nbsp;") 
            {
                DataRow[] row_rigname = dt_Rignames.Select("rigtypeid=" + lbl_rigtypeid.Text);
                lbl_rigname.Text = row_rigname[0]["rigtypename"].ToString();
            }
            else
            {
                lbl_rigname.Text = "";
            }
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        id_viewpart.Visible = false;
        //if (radcombo_userstatus.SelectedValue == "Approved")
        //{
        //    SqlDataSource3.SelectCommand = "SELECT * FROM [RigTypes] where rigtypeid  in (select rigtypeid from managejoborders where status='Approved') ";
        //}
        //else
        //{
        //    SqlDataSource3.SelectCommand = "SELECT * FROM [RigTypes] where rigtypeid not in (select rigtypeid from managejoborders where status='Approved') ";
        //}
        
        reset();
        string fromdate = String.Format("{0:MM/dd/yyyy}", radtxt_from.SelectedDate);
        string todate = String.Format("{0:MM/dd/yyyy}", radtxt_to.SelectedDate);
        string query_select = "SELECT (firstName+' '+lastName) as Username,j.jobType as Jobtypename,* from manageJobOrders m,PrsimCustomers c,Users u," +
            "jobTypes j where c.ID=m.Customer and m.opManagerId=u.userID  and " +
            "m.jobtype=j.jobtypeid and m.status='" + radcombo_userstatus.SelectedValue + "' and " +
            " m.salecreateddate between '" + fromdate + "' and '" + todate + "'";
        if (userMaster.UserTypeID != 1)
        {
            query_select += " and m.opManagerId=" + userMaster.UserID + "";
        }
            query_select+="order by jid desc";
        DataTable dt_jobbind = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
        radgrid_managejobs.DataSource = dt_jobbind;
        radgrid_managejobs.DataBind();

    }
    public void reset()
    {
        CheckBox1.Checked = false;
        radtxt_jobname.Text = "";
        txtAssetNumber.Text = "";
        radcombo_jobtype.SelectedIndex = -1;
        date_start.SelectedDate = DateTime.Now;
        date_stop.SelectedDate = DateTime.Now;
        radtxt_cost.Text = "";
        radcombo_customer.SelectedIndex = -1;
        txtprimaryAddress1.Text = "";
        ddlPrimaryCountry.SelectedIndex = -1;
        txtprimaryAddress2.Text = "";
        ddlPrimaryState.SelectedIndex = -1;
        txtprimaryCity.Text = "";
        txtprimaryPostalCode.Text = "";
        txtPrimaryFirst.Text = "";
        txtPrimaryLast.Text = "";
        txtPrimaryPhone1.Text = "";
        txtPrimaryPhone2.Text = "";
        txtPrimaryEmail.Text = "";


        //SECONDARY INFO
        txtSecondaryAddress1.Text = "";
        ddlSecondaryCountry.SelectedText = "";
        txtSecondaryAddress2.Text = "";
        ddlSecondaryState.SelectedIndex = -1;
        txtSecondaryCity.Text = "";
        txtSecondaryPostalCode.Text = "";
        txtSecondaryFirst.Text = "";
        txtSecondaryLast.Text = "";
        txtSecondaryPhone1.Text = "";
        txtSecondaryPhone2.Text = "";
        txtSecondaryEMail.Text = "";
        btn_approve.Text = "Approve";
        radcombo_pmanager.SelectedIndex = -1;
        //RadGrid1.Visible = false;
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        string jobgenid = "J" + Collector.GenerateNewAccountID(0);
        string approveid = lbl_docid.Text;
        db.Open();
        transaction = db.BeginTransaction();

        string queryUpdate = "update manageJobOrders set jobordercreatedid='" + jobgenid + "'," +
                " approveddatetime='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," +
            " status='Approved',programManagerId='" + radcombo_pmanager.SelectedValue + "'," +
                " startdate='" + DatesbetweenDatatable.getdatetimeformat(date_start.SelectedDate.ToString()) + "',enddate='" + DatesbetweenDatatable.getdatetimeformat(date_stop.SelectedDate.ToString()) + "'" +
            ",cost=" + radtxt_cost.Text + ",primaryFirst='" + txtPrimaryFirst.Text + "',primaryLast='" + txtPrimaryLast.Text + "'," +
                               "primaryAddress1='" + txtprimaryAddress1.Text + "',primaryAddress2='" + txtprimaryAddress2.Text + "',primaryCity='" + txtprimaryCity.Text + "'" +
            ",primaryState='" + ddlPrimaryState.SelectedText + "',primaryCountry='" + ddlPrimaryCountry.SelectedText + "',primaryPostalCode='" + txtprimaryPostalCode.Text + "'" +
            ",primaryPhone1='" + txtPrimaryPhone1.Text + "',primaryPhone2='" + txtPrimaryPhone2.Text + "',primaryEmail='" + txtPrimaryEmail.Text + "',primaryLatLong='" + txtPrimaryGIS.Text + "'," +
                               "secondaryFirst='" + txtSecondaryFirst.Text + "',secondaryLast='" + txtSecondaryLast.Text + "',secondaryAddress1='" + txtSecondaryAddress1.Text + "'" +
            ",secondaryAddress2='" + txtSecondaryAddress2.Text + "',secondaryCity='" + txtSecondaryCity.Text + "',secondaryState='" + ddlSecondaryState.SelectedText + "'" +
            ",secondaryCountry='" + ddlSecondaryCountry.SelectedText + "',secondaryPostalCode='" + txtSecondaryPostalCode.Text + "'," +
                               "secondaryPhone1='" + txtSecondaryPhone1.Text + "',secondaryEmail='" + txtSecondaryEMail.Text + "'" +
            ",secondaryPhone2='" + txtSecondaryPhone2.Text + "',opmgrnotes='" + radtxt_opnotes.Text + "',rigtypeid='" + radcombo_rigtype.SelectedValue + "'" +
            " where jid=" + lbl_docid.Text + "";


        //string updatequery = "update manageJobOrders set jobordercreatedid='" + jobgenid + "'," +
        //    " approveddatetime='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," +
        //    " programManagerId=" + radcombo_pmanager.SelectedValue + ",status='Approved' where jid=" + approveid + "";


        try
        {
            int updatecnt = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryUpdate);
            if (updatecnt > 0)
            {
                uploadeddocs(approveid);

                string notificationsendtowhome = eventNotification.sendEventNotification("JB01");
                if (notificationsendtowhome != "")
                {
                 bool status= eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JB01", "JOB", txtAssetNumber.Text, radtxt_jobname.Text,
                        date_start.SelectedDate.ToString(), date_stop.SelectedDate.ToString(),"Approve","","");
                 if (status)
                 {
                     lbl_message.Text = "Sale Created Successfully";
                     lbl_message.ForeColor = Color.Green;
                     btn_approve.Enabled = false;
                 }
                 else
                 {
                     lbl_message.Text = "Sale Created Failed";
                     lbl_message.ForeColor = Color.Red;
                     btn_approve.Enabled = true;
                 }

                    id_viewpart.Visible = false;
                    reset();
                    radgrid_managejobs.Rebind();
                }
            }
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
        }
    }
    protected void btn_reject_Click(object sender, EventArgs e)
    {
        string approveid = lbl_docid.Text;

        string updatequery = "update manageJobOrders set status='Rejected' where jid=" + approveid + "";
        int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
       
         string notificationsendtowhome = eventNotification.sendEventNotification("JR01");
                if (notificationsendtowhome != "")
                {
                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JR01", "JOB", txtAssetNumber.Text, radtxt_jobname.Text,
                        date_start.SelectedDate.ToString(), date_stop.SelectedDate.ToString(), "Reject","","");
                 if (status)
                 {
                     lbl_message.Text = "Sale Rejected Successfully";
                     lbl_message.ForeColor = Color.Green;
                     btn_approve.Enabled = false;
                 }
                 else
                 {
                     lbl_message.Text = "Sale Rejected Failed";
                     lbl_message.ForeColor = Color.Red;
                     btn_approve.Enabled = true;
                 }
        //string notificationsendtowhome = eventNotification.sendEventNotification("JR01");
        //if (notificationsendtowhome != "")
        //{
        //    string notoficationToUserId = notificationsendtowhome.Split('~')[0];
        //    string notoficationSendWay = notificationsendtowhome.Split('~')[1];
        //    string[] arrUserID = notoficationToUserId.Split(',');
        //    string[] arrnotificationway = notoficationSendWay.Split(',');
        //    for (int user = 0; user < arrUserID.Length; user++)
        //    {
        //        switch (arrnotificationway[user].ToString())
        //        {
        //            case "2":
        //                {
        //                    DataRow[] rowuser = dt_users.Select("userID=" + notoficationToUserId[user].ToString());
        //                    string message = "Sale Order Details<br />" +
        //                               "JobId: " + txtAssetNumber.Text + "<br />" +
        //                               "Job Name: " + radtxt_jobname.Text + "<br />" +
        //                               "Start Date: " + date_start.SelectedDate.ToString() + "<br />" +
        //                               "Stop Date: " + date_stop.SelectedDate.ToString() + "<br />" +
        //                               "Customer: " + rowuser[0]["firstName"].ToString() + "  " + rowuser[0]["lastName"].ToString() + "<br />" +
        //                               "";
        //                    string subject = "Sale Order Notification";
        //                    //DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users where userID=" + radcombo_pmanager.SelectedValue + "").Tables[0];

        //                    bool mailsentornot = MailSending.SendMail(rowuser[0]["email"].ToString(), subject, message);
        //                    if (mailsentornot)
        //                    {
        //                        lbl_message.Text = "Sale Rejected Successfully";
        //                        string info = "Sale " + radtxt_jobname.Text + " Rejected Successfully";
        //                        string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedID,userAssignedTimeStamp,Source)values(" +
        //                     "'JR01','" + info + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," + radcombo_pmanager.SelectedValue + "" +
        //                 ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','SALE')";
        //                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
        //                        lbl_message.ForeColor = Color.Green;
        //                        btn_approve.Enabled = false;
        //                    }
        //                    else
        //                    {

        //                    }
        //                    break;
        //                }
        //        }
        //    }

            id_viewpart.Visible = false;
            reset();
        }
       
    }
    public void uploadeddocs(string jobid)
    {
        string query = "", filename = "", uniqueFilename = "", query_EventTaskOrderDocuments;
        string cnt = radupload_docs.InitialFileInputsCount.ToString();

        for (int file = 0; file < radupload_docs.UploadedFiles.Count; file++)
        {
            try
            {
                query = "";
                query_EventTaskOrderDocuments = "";
                filename = radupload_docs.UploadedFiles[file].GetName();
                uniqueFilename = radupload_docs.UploadedFiles[file].GetNameWithoutExtension() + "_" + DateTime.Now.ToString("MMddyyyyhhmmss") + "" + radupload_docs.UploadedFiles[file].GetExtension();
                radupload_docs.UploadedFiles[file].SaveAs(Server.MapPath("../../Documents/") + uniqueFilename);
                query = "Insert into Documents(DocumentDisplayName,DocumentName) values('" + filename + "','" + uniqueFilename + "')";
                int documentinsert = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query);
                if (documentinsert > 0)
                {
                    DataTable dt_docs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    Documents WHERE  DocumentID = IDENT_CURRENT('Documents')").Tables[0];
                    query_EventTaskOrderDocuments = "Insert into JobOrderDocuments(jid,DocumentID,UserID,UploadedDate,type) values " +
                        " ('" + jobid + "','" + dt_docs.Rows[0]["DocumentID"].ToString() + "','" + Session["userId"].ToString() + "','" + DateTime.Now.ToString() + "','JO')";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query_EventTaskOrderDocuments);
                    radgrid_managejobs.Rebind();
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
            }

        }
    }
    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "downloaddoc")
        {
            //byte[] binaryData = (byte[])data.Tables[0].Rows[0]["BinaryData"];
            Label lbl_docname = (Label)e.Item.FindControl("lbl_docname");

            //Response.Clear();
            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("content-disposition", "attachment; filename=" + lbl_docname.Text);
            //Response.Flush();
            //Response.End();
            string path = Server.MapPath("../../Documents/" + lbl_docname.Text);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_docname.Text);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();

        }

        RadGrid1.Rebind();
    }
    protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "downloaddoc")
        {
            //byte[] binaryData = (byte[])data.Tables[0].Rows[0]["BinaryData"];
            Label lbl_docname = (Label)e.Item.FindControl("lbl_docname");

            //Response.Clear();
            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("content-disposition", "attachment; filename=" + lbl_docname.Text);
            //Response.Flush();
            //Response.End();
            string path = Server.MapPath("../../Documents/" + lbl_docname.Text);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_docname.Text);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();

        }

        RadGrid2.Rebind();
    }
}