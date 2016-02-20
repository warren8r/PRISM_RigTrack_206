using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class Modules_Configuration_Manager_AddSignaturetoApproveDataEntry : System.Web.UI.Page
{
    string date = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        date = Convert.ToDateTime(Request.QueryString["Date"].ToString()).ToString("yyyy-MM-dd");
        lbl_jid.Text = Request.QueryString["jid"].ToString();
        lbl_date.Text = Convert.ToDateTime(Request.QueryString["Date"].ToString()).ToString("MM-dd-yyyy");
    }
    protected void btn_createrig_Click(object sender, EventArgs e)
    {
        string insert_approved = "";
        ClientMaster userMaster = new ClientMaster();
        userMaster = (ClientMaster)Session["UserMasterDetails"];
        string name = userMaster.FirstName + " " + userMaster.LastName;
        DataTable dt_existnotes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from JobDataApproval where Date='" + Convert.ToDateTime(Request.QueryString["Date"].ToString()).ToString("yyyy-MM-dd") + "'").Tables[0];
        if (name == radcombo_approveby.Text)
        {
            if (dt_existnotes.Rows.Count == 0)
            {
                insert_approved = "insert into JobDataApproval(jid,Date,approvedby,ApprovalDate)values(" + lbl_jid.Text + ",'" + DatesbetweenDatatable.getdatetimeformat(date) + "','" + name + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
            }
            else
            {
                insert_approved = "update JobDataApproval set approvedby='" + name + "',ApprovalDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where jid=" + lbl_jid.Text + " and Date='" + DatesbetweenDatatable.getdatetimeformat(date) + "'";
            }
            int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insert_approved);
            if (cnt > 0)
            {
                lbl_windowapproveddate.Text = "Approved Successfully";
                lbl_windowapproveddate.ForeColor = Color.Green;
            }
            else
            {
                lbl_windowapproveddate.Text = "Error";
                lbl_windowapproveddate.ForeColor = Color.Red;
            }
        }
        else
        {
            lbl_windowapproveddate.Text = "Signature is not matching";
            lbl_windowapproveddate.ForeColor = Color.Red;
        }
        radcombo_approveby.Text = "";
    }
}