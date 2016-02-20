using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt_getalreadyexistinfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnectionString(radtxt_fp_clientcode.Text), CommandType.Text, "select * from Users where  email='" + radtxt_email.Text + "'").Tables[0];
        if (dt_getalreadyexistinfo.Rows.Count > 0)
        {
            string authcode = RandomCodegenerator.randomauthcode(20);
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update Users set activationCode='" + authcode + "',lastUpdatedDate='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "' where Email='" + radtxt_email.Text + "'");
            string message = "By Clicking on the below link you can reset your Account<br/>" +
                       ConfigurationManager.AppSettings["AppURL"].ToString() + "/ClientAdmin/ClientActivation.aspx?authcode=" + authcode + "&cc=" + radtxt_fp_clientcode.Text + "";
            string subject = "Confirmation Email From Limitless Healthcare IT's Meter Data Management System";
            bool mailsentornot = MailSending.SendMail(radtxt_email.Text, subject, message);
            if (mailsentornot)
            {
                lbl_message.Text = "Activation link is sent to your email";
            }
            else
            {
                lbl_message.Text = "Mail not sent";
            }
        }
        else
        {
            lbl_message.Text = "Email ID does not exist";
        }
    }
    protected void btn_submitlogin_Click(object sender, EventArgs e)
    {
        DataTable dt_getalreadyexistinfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnectionString(radtxt_clientcode.Text), CommandType.Text, "select * from Users where  email='" + radtxt_loginame.Text + "'").Tables[0];
        if (dt_getalreadyexistinfo.Rows.Count > 0)
        {
            string authcode = RandomCodegenerator.randomauthcode(20);
            //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "update Users set ActivationCode='" + authcode + "',LastUpdatedDate='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "' where Email='" + radtxt_email.Text + "'");
            string message = "Your Login Name as per records<br/>" +
                       "Login Name: " + dt_getalreadyexistinfo.Rows[0]["loginName"].ToString() + "";
            string subject = "Confirmation Email From Limitless Healthcare IT's Meter Data Management System";
            bool mailsentornot = MailSending.SendMail(radtxt_loginame.Text, subject, message);
            if (mailsentornot)
            {
                lbl_message.Text = "Login Name sent to your email";
            }
            else
            {
                lbl_message.Text = "Mail not sent";
            }
        }
        else
        {
            lbl_message.Text = "Email ID does not exist";
        }
    }
}