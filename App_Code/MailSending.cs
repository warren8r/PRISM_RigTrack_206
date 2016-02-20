using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;

using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Data;


/// <summary>
/// Summary description for MailSending
/// </summary>
public class MailSending
{
	public MailSending()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool SendMail(string to, string subject,
        string message)
    {
        try
        {
            
            MailMessage mailMsg;
            mailMsg = new MailMessage();
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["emailfrom"].ToString());
            //to += ",kethireddy.anil@gmail.com";
            mailMsg.From = from;
            mailMsg.To.Add(to);
            // mailMsg.CC.Add("corpmail@Limitless Healthcare ITsw.com");
            mailMsg.Subject = subject;
            mailMsg.Body = message;// "Name: " + Name.Text + "" + Environment.NewLine + "Address: " + Address.Text + "" + Environment.NewLine + "Phone: " + Phone.Text + "" + Environment.NewLine + " Postion Applied For : " + txt_position.Text + "" + Environment.NewLine + "";
            mailMsg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["emailhost"].ToString());
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["emailusername"].ToString(), ConfigurationManager.AppSettings["emailpassword"].ToString());
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["emailssl"]);
            smtp.Send(mailMsg);
            return true;

        }
        catch (Exception ex)
        {
            //lblError.Text = ex.Message; ;
            //Response.Write(ex.Message);
            return false;
        }
    }
    public static void SendMailwithAttachemnet(MailAddress from,string to, string mail_attachemnet,string message)
    {
        try
        {
            System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            //System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(Utility.getSMTPUsername(), Utility.getSMTPPassword());
            mailMsg.From = from;
            mailMsg.To.Add(to);
            mailMsg.Subject = "";
            mailMsg.Body = message;
            System.Net.Mail.Attachment attach =new System.Net.Mail.Attachment(mail_attachemnet);
            mailMsg.Attachments.Add(attach);
            mailMsg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["emailhost"].ToString());
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["emailusername"].ToString(), ConfigurationManager.AppSettings["emailpassword"].ToString());
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["emailssl"]);

            smtp.Send(mailMsg);
        }
        catch (Exception ex)
        {

        }
        finally
        {
        }
    }
    public static string getAccessName(string id)
    {
        string accessType = "";
        switch (id)
        {
            case "1":
                {
                    accessType = "Read";
                    break;
                }
            case "2":
                {
                    accessType = "Read/Write";
                    break;
                }
            case "3":
                {
                    accessType = "No Access";
                    break;
                }
        }
        return accessType;
    }
    public static string getAccessNameShort(string id)
    {
        string accessType = "";
        switch (id)
        {
            case "1":
                {
                    accessType = "R";
                    break;
                }
            case "2":
                {
                    accessType = "R/W";
                    break;
                }

        }
        return accessType;
    }
}