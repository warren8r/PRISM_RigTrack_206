using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string encryptenteredpwd = EncryptDecrypt.Encrypt(radtxt_pwd.Text.ToString().TrimStart().TrimEnd(), true);
        DataTable dt_getdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUsers where loginName='" + radtxt_username.Text + "' and password='" + encryptenteredpwd + "'").Tables[0];
        if (dt_getdetails.Rows.Count > 0)
        {
            UserMaster userMaster = new UserMaster();
            userMaster = readUserDetails(userMaster, dt_getdetails);
            Session["UserMasterDetails"] = userMaster;
            Response.Redirect("Home.aspx");

        }
        else
        {
            lbl_message.Text = "Login Name or Password are incorrect";
        }
        
    }
    public UserMaster readUserDetails(UserMaster userMaster,DataTable dt_results)
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
        Response.Redirect("ForgotPassword.aspx");
    }
}