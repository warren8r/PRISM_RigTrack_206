using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MDM;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using Artem.Google.Net;
using System.Diagnostics;
using System.Configuration;
public partial class Modules_Configuration_Manager_assetsCreatejobOrder : System.Web.UI.Page
{
    MDM.Collector Collector = new MDM.Collector();
    public static DataTable dt_users = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        if (!IsPostBack)
        {
            date_start.SelectedDate = DateTime.Now;
            RadGrid1.DataSource = null;
            RadGrid1.DataBind();
            ddlPrimaryCountry.SelectedValue = "US";
            ddlSecondaryCountry.SelectedValue = "US";
            dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users").Tables[0];
        }
        //RadWindowManager1.Windows[0].VisibleOnPageLoad = false;
    }
    protected void lnl_generateassetid_Click(object sender, EventArgs e)
    {


        //txtAssetNumber.Text = Collector.GenerateNewAccountID(0);
    }
    public bool isChecked(CheckBox chk)
    {
        if (chk.Checked)
            return true;
        else
            return false;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (btnInsert.Text != "Update")
        {
            txtAssetNumber.Text = Collector.GenerateNewAccountID(0);
            string queryInsert = "Insert into manageJobOrders(bitActive,jobname,jobid,jobtype,startdate,enddate,cost,Customer,primaryFirst,primaryLast," +
                               "primaryAddress1,primaryAddress2,primaryCity,primaryState,primaryCountry,primaryPostalCode,primaryPhone1,primaryPhone2,primaryEmail,primaryLatLong," +
                               "primaryLatLongAccuracy,bitSecondarySamePrimaryAddress,secondaryFirst,secondaryLast,secondaryAddress1,secondaryAddress2,secondaryCity,secondaryState,secondaryCountry,secondaryPostalCode," +
                               "secondaryPhone1,secondaryEmail,secondaryPhone2,secondaryLatLong,secondaryLatLongAccuracy,status,opManagerId,salesnotes,salecreateddate,jobsource) values ('" + isChecked(CheckBox1) + "','" + radtxt_jobname.Text + "'," +
                               "'" + txtAssetNumber.Text + "','" + radcombo_jobtype.SelectedValue + "','" + DatesbetweenDatatable.getdatetimeformat(date_start.SelectedDate.ToString()) + "','" + DatesbetweenDatatable.getdatetimeformat(date_stop.SelectedDate.ToString()) + "'," + radtxt_cost.Text + ",'" + radcombo_customer.SelectedValue + "','" + txtPrimaryFirst.Text + "'," +
                               "'" + txtPrimaryLast.Text + "','" + txtprimaryAddress1.Text + "','" + txtprimaryAddress2.Text + "','" + txtprimaryCity.Text + "','" + ddlPrimaryState.SelectedText + "'," +
                               "'" + ddlPrimaryCountry.Text + "','" + txtprimaryPostalCode.Text + "','" + txtPrimaryPhone1.Text + "','" + txtPrimaryPhone2.Text + "','" + txtPrimaryEmail.Text + "'," +
                               "'" + txtPrimaryGIS.Text + "','','" + isChecked(chk_sameasprimary) + "','" + txtSecondaryFirst.Text + "','" + txtSecondaryLast.Text + "'," +
                               "'" + txtSecondaryAddress1.Text + "','" + txtSecondaryAddress2.Text + "','" + txtSecondaryCity.Text + "','" + ddlSecondaryState.SelectedText + "','" + ddlSecondaryCountry.Text + "'," +
                               "'" + txtSecondaryPostalCode.Text + "','" + txtSecondaryPhone1.Text + "','" + txtSecondaryEMail.Text + "','" + txtSecondaryPhone2.Text + "','',''," +
                               "'Pending'," + radcombo_opmngers.SelectedValue.Split('~')[0].ToString() + ",'" + radtxt_notes.Text + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Sales')";
            int insertcnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
            if (insertcnt > 0)
            {
                DataTable dt_maxid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM  manageJobOrders WHERE  jid = IDENT_CURRENT('manageJobOrders')").Tables[0];
                string jid = dt_maxid.Rows[0]["jid"].ToString();
                uploadeddocs(jid);

                string notificationsendtowhome = eventNotification.sendEventNotification("SC01");
                if (notificationsendtowhome != "")
                {
                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "SC01", "SALE", txtAssetNumber.Text, radtxt_jobname.Text,
                           date_start.SelectedDate.ToString(), date_stop.SelectedDate.ToString(), "Insert", "","");
                    if (status)
                    {
                        lbl_message.Text = "Sale created successfully";
                        lbl_message.ForeColor = Color.Green;
                    }
                    else
                    {
                        lbl_message.Text = "Sale Created Failed";
                        lbl_message.ForeColor = Color.Red;
                    }

                    reset();
                }
                else
                {
                    lbl_message.Text = "Error on page";
                    lbl_message.ForeColor = Color.Red;
                }
            }
        }
        else
        {
            string queryUpdate = "update manageJobOrders set bitActive='" + isChecked(CheckBox1) + "',jobname='" + radtxt_jobname.Text + "',jobtype='" + radcombo_jobtype.SelectedValue + "'," +
                " startdate='" + DatesbetweenDatatable.getdatetimeformat(date_start.SelectedDate.ToString()) + "',enddate='" + DatesbetweenDatatable.getdatetimeformat(date_stop.SelectedDate.ToString()) + "'" +
            ",cost=" + radtxt_cost.Text + ",Customer='" + radcombo_customer.SelectedValue + "',primaryFirst='" + txtPrimaryFirst.Text + "',primaryLast='" + txtPrimaryLast.Text + "'," +
                               "primaryAddress1='" + txtprimaryAddress1.Text + "',primaryAddress2='" + txtprimaryAddress2.Text + "',primaryCity='" + txtprimaryCity.Text + "'" +
            ",primaryState='" + ddlPrimaryState.SelectedText + "',primaryCountry='" + ddlPrimaryCountry.Text + "',primaryPostalCode='" + txtprimaryPostalCode.Text + "'" +
            ",primaryPhone1='" + txtPrimaryPhone1.Text + "',primaryPhone2='" + txtPrimaryPhone2.Text + "',primaryEmail='" + txtPrimaryEmail.Text + "',primaryLatLong='" + txtPrimaryGIS.Text + "'," +
                               "secondaryFirst='" + txtSecondaryFirst.Text + "',secondaryLast='" + txtSecondaryLast.Text + "',secondaryAddress1='" + txtSecondaryAddress1.Text + "'" +
            ",secondaryAddress2='" + txtSecondaryAddress2.Text + "',secondaryCity='" + txtSecondaryCity.Text + "',secondaryState='" + ddlSecondaryState.SelectedText + "'" +
            ",secondaryCountry='" + ddlSecondaryCountry.Text + "',secondaryPostalCode='" + txtSecondaryPostalCode.Text + "'," +
                               "secondaryPhone1='" + txtSecondaryPhone1.Text + "',secondaryEmail='" + txtSecondaryEMail.Text + "'" +
            ",secondaryPhone2='" + txtSecondaryPhone2.Text + "',opManagerId=" + radcombo_opmngers.SelectedValue.Split('~')[0].ToString() + ",salesnotes='" + radtxt_notes.Text + "'" +
            " where jid=" + lbl_docid.Text + "";
            int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
            if (updatecnt > 0)
            {
                uploadeddocs(lbl_docid.Text);

                string notificationsendtowhome = eventNotification.sendEventNotification("SC01");
                if (notificationsendtowhome != "")
                {
                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "SC02", "SALE", txtAssetNumber.Text, radtxt_jobname.Text,
                           date_start.SelectedDate.ToString(), date_stop.SelectedDate.ToString(), "Update", "","");
                    if (status)
                    {
                        lbl_message.Text = "Sale created successfully";
                        lbl_message.ForeColor = Color.Green;
                    }
                    else
                    {
                        lbl_message.Text = "Sale Creation Failed";
                        lbl_message.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lbl_message.Text = "Error on page";
                    lbl_message.ForeColor = Color.Red;
                }

            }
            reset();
        }
    }
    protected void radgrid_managejobs_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            //GridDataItem item = (GridDataItem)e.Item;
            //TableCell cell = (TableCell)item["UniqueName"];
            //cell.BackColor = System.Drawing.Color.Red;
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
                        " ('" + jobid + "','" + dt_docs.Rows[0]["DocumentID"].ToString() + "','" + Session["userId"].ToString() + "','" + DateTime.Now.ToString() + "','SA')";
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
    protected void lnkBtnGatherGIS_Click(object sender, EventArgs e)
    {
        //Telerik.Web.UI.RadTextBox txtAddress1 = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryAddress1");
        //Telerik.Web.UI.RadTextBox txtAddress2 = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryAddress2");
        //Telerik.Web.UI.RadTextBox txtCity = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryCity");
        ////Telerik.Web.UI.RadDropDownList ddlRadState = (Telerik.Web.UI.RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        //RadDropDownList ddlRadState = (RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        //Telerik.Web.UI.RadDropDownList ddlCountry = (Telerik.Web.UI.RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");
        //Telerik.Web.UI.RadTextBox txtPostal = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryPostalCode");
        //Telerik.Web.UI.RadTextBox txtCoordLatLong = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtPrimaryGIS");

        //HyperLink hypPrimaryMap = (HyperLink)FormView1.FindControl("hypPrimaryMapLink");
        //Label lblPrimaryMatch = (Label)FormView1.FindControl("lblPrimaryMatch");
        //TextBox txtMatchDB = (TextBox)FormView1.FindControl("txtMatchDB");

        //RadButton lnkBtnGatherGISPrimary = (RadButton)FormView1.FindControl("lnkBtnGatherGISPrimary");

        //Telerik.Web.UI.RadWindow WindowPrimary = (Telerik.Web.UI.RadWindow)FormView1.FindControl("WindowPrimary");



        //Artem.Google.UI.GoogleMap GoogleMap1 = (Artem.Google.UI.GoogleMap)FindControl("GoogleMap1");

        string addressToCheck = txtprimaryAddress1.Text + " " + txtprimaryAddress2.Text + " " + txtprimaryCity.Text + ", " + ddlPrimaryState.SelectedItem.Value + " " + txtprimaryPostalCode.Text + " " + ddlPrimaryCountry.SelectedItem.Value;


        try
        {
            if (txtprimaryAddress1.Text != String.Empty)
            {
                // Make request to Google API
                GeoRequest gr = new GeoRequest(addressToCheck);
                GeoResponse gRes = gr.GetResponse();

                Debug.WriteLine(addressToCheck);
                // Set Latitude/Longitude in textbox
                txtPrimaryGIS.Text = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
                hypPrimaryMapLink.NavigateUrl = "http://maps.google.com/?q=" + gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();

                GoogleMap1.Latitude = gRes.Results[0].Geometry.Location.Latitude;
                GoogleMap1.Longitude = gRes.Results[0].Geometry.Location.Longitude;

                Debug.WriteLine("Lat:" + GoogleMap1.Latitude.ToString());
                Debug.WriteLine("Lon:" + GoogleMap1.Longitude.ToString());

                string resultLocationMatch = gRes.Results[0].Geometry.LocationType.ToString();
                Debug.WriteLine(resultLocationMatch);

                string resultAccuracy = "";
                switch (resultLocationMatch)
                {
                    // See: http://stackoverflow.com/questions/3015370/how-to-get-the-equivalent-of-the-accuracy-in-google-map-geocoder-v3
                    case "ROOFTOP":
                        resultAccuracy = "Precise";
                        break;
                    case "RANGE_INTERPOLATED":
                        resultAccuracy = "Approximate";
                        break;
                    case "GEOMETRIC_CENTER":
                        resultAccuracy = "Approximate";
                        break;
                    case "APPROXIMATE":
                        resultAccuracy = "Approximate";
                        break;
                    default:
                        resultAccuracy = resultLocationMatch;
                        break;
                }
                Debug.WriteLine(resultLocationMatch);

                lblPrimaryMatch.Text = gRes.Results[0].Types[0].ToString().ToUpper() + " (" + resultAccuracy + ")";
                txtMatchDB.Text = lblPrimaryMatch.Text;
                lblPrimaryMatch.Visible = false;
                hypPrimaryMapLink.Visible = true;


            }
        }
        catch (Exception ex)
        {
            lnkBtnGatherGISPrimary.Enabled = true;
            throw;
        }

    }
    
    protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {
        //Bind the RadComboBod 
        radcombo_jobtype.DataBind();
    } 
    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        reset();
    }
    public void reset()
    {
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
        ddlSecondaryCountry.Text = "";
        txtSecondaryAddress2.Text = "";
        ddlSecondaryState.SelectedIndex = -1;
        txtSecondaryCity.Text = "";
        txtSecondaryPostalCode.Text = "";
        txtSecondaryFirst.Text = "";
        txtSecondaryLast.Text = "";
        txtSecondaryPhone1.Text = "";
        txtSecondaryPhone2.Text = "";
        txtSecondaryEMail.Text = "";
        //btnInsert.Text = "Save";
        RadGrid1.Visible = false;
        radtxt_notes.Text = "";
        btnInsert.Text = "Save/Submit";
    }
    protected void radgrid_managejobs_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "viewedit")
        {
            GridDataItem editedItem = e.Item as GridDataItem;
            //TableCell cell = (TableCell)editedItem["UniqueName"];
            //editedItem.BackColor = System.Drawing.Color.Green;
            //cell.BackColor = System.Drawing.Color.Red;
            Label lbl_jobid = (Label)editedItem.FindControl("lbl_jobid");
            lbl_docid.Text = lbl_jobid.Text;
            RadGrid1.Rebind();
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
                txtprimaryAddress1.Text = dt_existjobs.Rows[0]["primaryAddress1"].ToString();
                ddlPrimaryCountry.Text = dt_existjobs.Rows[0]["primaryCountry"].ToString();
                txtprimaryAddress2.Text = dt_existjobs.Rows[0]["primaryAddress2"].ToString();
                ddlPrimaryState.SelectedText = dt_existjobs.Rows[0]["primaryState"].ToString();
                txtprimaryCity.Text = dt_existjobs.Rows[0]["primaryCity"].ToString();
                txtprimaryPostalCode.Text = dt_existjobs.Rows[0]["primaryPostalCode"].ToString();
                txtPrimaryFirst.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                txtPrimaryLast.Text = dt_existjobs.Rows[0]["primaryLast"].ToString();
                txtPrimaryPhone1.Text = dt_existjobs.Rows[0]["primaryPhone1"].ToString();
                txtPrimaryPhone2.Text = dt_existjobs.Rows[0]["primaryPhone2"].ToString();
                txtPrimaryEmail.Text = dt_existjobs.Rows[0]["primaryEmail"].ToString();
                txtPrimaryGIS.Text = dt_existjobs.Rows[0]["primaryLatLong"].ToString();

                //SECONDARY INFO
                txtSecondaryAddress1.Text = dt_existjobs.Rows[0]["primaryFirst"].ToString();
                ddlSecondaryCountry.Text = dt_existjobs.Rows[0]["primaryCountry"].ToString();
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
                DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users where userID=" + dt_existjobs.Rows[0]["opManagerId"].ToString() + "").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    radcombo_opmngers.SelectedValue = dt_existjobs.Rows[0]["opManagerId"].ToString() + "~" + dt.Rows[0]["email"].ToString();
                }
                btnInsert.Text = "Update";
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
}