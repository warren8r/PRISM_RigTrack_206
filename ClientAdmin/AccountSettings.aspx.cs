using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class ClientAdmin_AccountSettings : System.Web.UI.Page
{
    string userid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientMaster userMaster = new ClientMaster();
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
            else
            {
                lbl_email.Text = userMaster.Email;
                userid = userMaster.UserID.ToString();
                // Show the user's name
                //lbl_welcomename.Text = userMaster.FirstName + " " + userMaster.LastName;
            }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string encryptedoldPassword = EncryptDecrypt.Encrypt(radtxtold_password.Text.TrimStart().TrimEnd(), true);
        DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users where Password='" + encryptedoldPassword + "' and UserID=" + userid + "").Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (userid != "")
            {
                int newPasswordCount = radtxt_password.Text.Count();
                if (newPasswordCount <= 6)
                {
                    lbl_message.Text = "Password must be at least 7 characters long !";
                    return;
                }
                string encryptedPassword = EncryptDecrypt.Encrypt(radtxt_password.Text.TrimStart().TrimEnd(), true);
                int sucess = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update Users set Password='" + encryptedPassword + "' where UserID=" + userid + "");
                if (sucess == 1)
                {
                    lbl_message.Text = "Password changed successfully";
                    lbl_message.ForeColor = Color.Green;
                    btn_submit.Enabled = false;
                }
            }
        }
        else
        {
            lbl_message.Text = "Password doesnot match with existing records";
        }
    }
}