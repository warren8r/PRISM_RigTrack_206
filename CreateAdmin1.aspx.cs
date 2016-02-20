using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Data;

public partial class CreateAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_create_Click(object sender, EventArgs e)
    {
        UserMaster userMaster = new UserMaster();
        userMaster = readUserDetails(userMaster);
        DataTable dt_getalreadyexistinfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUsers where LoginName='" + userMaster.LoginName + "' or Email='" + userMaster.Email + "'").Tables[0];
        if (dt_getalreadyexistinfo.Rows.Count == 0)
        {
            string authcode = RandomCodegenerator.randomauthcode(20);

            userMaster.Activationcode = authcode;
            userMaster.ActivationExpiry = 5;

            String Msg = insertUserDetails(userMaster);
            if (Msg == "Success")
            {
                string message = "By Clicking on the below link you can activate your Account<br/>" +
                   ConfigurationManager.AppSettings["AppURL"].ToString() + "Activation.aspx?authcode=" + authcode + "";
                string subject = "Confirmation Email From Limitless Healthcare IT's Meter Data Management System";
                bool mailsentornot = MailSending.SendMail(radtxt_email.Text, subject, message);
                if (mailsentornot)
                {
                    lbl_errormsg.Text = "Activation link is sent to your email";
                }
                else
                {
                    lbl_errormsg.Text = "Mail not sent";
                }
            }
            else
            {
                lbl_errormsg.Text = "Record Not Inserted";
            }
            btn_create.Enabled = false;
            clearitems();
        }
        else
        {
            lbl_errormsg.Text = "LoginName or Email already exist please try another.";
        }


    }
    public void clearitems()
    {
        radtxt_fname.Text = "";
        radtxt_lastname.Text = "";
        radtxt_title.Text = "";
        radtxt_email.Text = "";
        radtxt_address1.Text = "";
        radtxt_address2.Text = "";
        radtxt_city.Text = "";
        radtxt_country.Text = "";
        radtxt_zip.Text = "";
        radtxt_phone.Text = "";
        radtxt_cellno.Text = "";
        radtxt_login.Text = "";
        //lbl_errormsg.Text = "";
    }

    public UserMaster readUserDetails(UserMaster userMaster)
    {

        userMaster.FirstName = radtxt_fname.Text;
        userMaster.LastName = radtxt_lastname.Text;
        userMaster.Title = radtxt_title.Text;
        userMaster.Email = radtxt_email.Text;
        userMaster.Address = radtxt_address1.Text;
        userMaster.Address2 = radtxt_address2.Text;
        userMaster.City = radtxt_city.Text;
        userMaster.State = radcombo_state.Text;
        userMaster.Country = radtxt_country.Text;
        userMaster.Zip = radtxt_zip.Text;
        userMaster.Phone = radtxt_phone.Text;
        userMaster.Cellno = radtxt_cellno.Text;
        userMaster.LoginName = radtxt_login.Text;
        userMaster.UserTypeID = Convert.ToInt32(radcombo_role.SelectedValue);


        return userMaster;

    }
    public String insertUserDetails(UserMaster userMaster)
    {
        string returnvalue = "";
        string insertSql = "insert into SuperUsers (UserTypeID,FirstName,LastName,Title,LoginName,Email,Address," +
            "Address2,City,State,Country,Zip,Phone,LastUpdatedDate,Cell_no,ActivationCode,ActivationExpiry,Create_Date)values(" +
            "" + userMaster.UserTypeID + ",'" + userMaster.FirstName + "','" + userMaster.LastName + "','" + userMaster.Title + "','" + userMaster.LoginName + "'" +
            ",'" + userMaster.Email + "','" + userMaster.Address + "','" + userMaster.Address2 + "','" + userMaster.City + "','" + userMaster.State + "'," +
            "'" + userMaster.Country + "','" + userMaster.Zip + "','" + userMaster.Phone + "','" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','" + userMaster.Cellno + "','" + userMaster.Activationcode + "'," +
            "" + userMaster.ActivationExpiry + ",'" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "')";
        try
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, insertSql);
            returnvalue = "Success";
        }
        catch (Exception ex)
        {
            returnvalue = ex.Message;
        }

        return returnvalue;
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        clearitems();
        lbl_errormsg.Text = "";
        btn_create.Enabled = true;
    }
}