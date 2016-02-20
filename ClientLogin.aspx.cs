using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    DataTable dt_getClinetdetails, dt_subscription;
    string str_registerdate = "", str_subscription_duration = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        radtxt_username.Focus();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dt_getClinetdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text,
         "select * from Clients where clientCode='" + radtxt_clientcode.Text + "'").Tables[0];

       

        if (dt_getClinetdetails.Rows.Count > 0)
        {
            if (dt_getClinetdetails.Rows[0]["approvalStatus"].ToString().ToLower() == "approved")
            {
                if (dt_getClinetdetails.Rows[0]["clientStatus"].ToString().ToLower() == "active")
                {
                    dt_subscription = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text,
                                "select * from ClientSubscription where clientID='" + dt_getClinetdetails.Rows[0]["clientID"].ToString() + "'").Tables[0];
                    if (dt_subscription.Rows.Count > 0)
                    {
                        str_registerdate = dt_getClinetdetails.Rows[0]["createDate"].ToString();
                        str_subscription_duration = dt_subscription.Rows[0]["subDuration"].ToString();
                        System.TimeSpan diffResult = Convert.ToDateTime(str_registerdate).AddMonths(Convert.ToInt32(str_subscription_duration)).Subtract(DateTime.Now);
                        if (diffResult.Days > 0)
                        {
                            string encryptenteredpwd = EncryptDecrypt.Encrypt(radtxt_pwd.Text.ToString().TrimStart().TrimEnd(), true);
                            DataTable dt_getdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnectionString(radtxt_clientcode.Text), CommandType.Text, "select * from Users where loginName='" + radtxt_username.Text + "' and password='" + encryptenteredpwd + "'").Tables[0];
                            if (dt_getdetails.Rows.Count > 0)
                            {
                                ClientMaster userMaster = new ClientMaster();
                                userMaster = readUserDetails(userMaster, dt_getdetails);
                                Session["UserMasterDetails"] = userMaster;
                                Session["ClientCode"] = radtxt_clientcode.Text;
                                Session["clientID"] = dt_getClinetdetails.Rows[0]["clientID"].ToString();
                                Session["userId"] = userMaster.UserID;
                                // Instantiate the connectionstring for global use. JB 5/17
                                Session["client_database"] = GlobalConnetionString.ClientConnectionString(radtxt_clientcode.Text);
                                Session["ami_database"] = System.Configuration.ConfigurationManager.ConnectionStrings["elster_database"].ConnectionString;



                                Response.Redirect("ClientAdmin/ClientHome.aspx");

                            }
                            else
                            {
                                lbl_message.Text = "Login Name or Password are incorrect";

                            }
                        }
                        else
                        {
                            lbl_message.Text = "Subscription Period was Expired, please contact system admin";
                        }
                    }
                    else
                    {
                        lbl_message.Text = "There is no Subscription exist for this Client, please contact system admin";
                    }
                }
                else
                {
                    lbl_message.Text = "Client status is in active, please contact system admin";
                }
            }
            else
            {
                lbl_message.Text = "Client details was not yet Approved, please contact system admin";
            }
        }
        else
        {
            lbl_message.Text = "Client code doesn't exist";
        }
    }
    public ClientMaster readUserDetails(ClientMaster userMaster, DataTable dt_results)
    {

        userMaster.FirstName = dt_results.Rows[0]["firstName"].ToString();
        userMaster.LastName = dt_results.Rows[0]["lastName"].ToString();
        userMaster.UserID = Convert.ToInt32(dt_results.Rows[0]["userID"]);
        userMaster.Email = dt_results.Rows[0]["email"].ToString();

        userMaster.UserTypeID = Convert.ToInt32(dt_results.Rows[0]["userRoleID"]);


        return userMaster;

    }
    protected void lnk_forgotpwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClientAdmin/ClientForgotPassword.aspx");
    }
}