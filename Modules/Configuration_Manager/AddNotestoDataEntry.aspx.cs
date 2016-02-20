using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class Modules_Configuration_Manager_AddNotestoDataEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_date.Text = Convert.ToDateTime(Request.QueryString["Date"].ToString()).ToString("yyyy-MM-dd");
        lbl_jid.Text = Request.QueryString["jid"].ToString();
    }
    protected void btn_addnotes_Click(object sender, EventArgs e)
    {
        lbl_notesdate.Text = "";
        string date = Request.QueryString["Date"].ToString();
        string jid=Request.QueryString["jid"].ToString();
        string insert_notesquery = "";
        //DataTable dt_existnotes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataApproval where Date='" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "'").Tables[0];
        //if (dt_existnotes.Rows.Count == 0)
        //{
            insert_notesquery = "insert into DataApprovalNotes(jid,Date,Notes)values(" + jid + ",'" + DatesbetweenDatatable.getdatetimeformat(date) + "','" + radtxt_Notes.Text + "')";
        //}
        //else
        //{
        //    insert_notesquery = "update JobDataApproval set Notes='" + radtxt_Notes.Text + "' where jobdataapprovalid=" + dt_existnotes.Rows[0]["jobdataapprovalid"].ToString() + "";
        //}
        int cnt=SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insert_notesquery);
        if (cnt > 0)
        {
            lbl_notesdate.Text = "Notes Added";
            lbl_notesdate.ForeColor = Color.Green;
        }
        else
        {
            lbl_notesdate.Text = "Error";
            lbl_notesdate.ForeColor = Color.Red;
        }
        RadGrid_notes.Rebind();
        radtxt_Notes.Text = "";
        //radwindow_notes.VisibleOnPageLoad = true;
        //ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
    }
}