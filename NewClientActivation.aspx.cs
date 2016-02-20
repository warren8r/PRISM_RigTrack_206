using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class NewClientActivation : System.Web.UI.Page
{
    string authcode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            authcode = Request.QueryString["authcode"].ToString();
            //RpG5z6KHmNfWFbtqQ0SB
            if (authcode == "")
            {
                div_pwdvisible.Style.Add("display", "none");
                lbl_message.Text = "User already activated";
            }
            else
            {
                DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from Clients where activationCode='" + authcode + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int expirydays = Convert.ToInt32(dt.Rows[0]["activationExpiry"]);
                    DateTime dt_createdate = Convert.ToDateTime(dt.Rows[0]["lastUpdatedDate"]).AddDays(6);
                    DateTime today = DateTime.Now;
                    TimeSpan dif = dt_createdate - today;
                    if (dif.Days > expirydays)
                    {
                        div_pwdvisible.Style.Add("display", "none");
                        lbl_message.Text = "The link you are trying to visit has expired";
                    }
                    else
                    {
                        div_pwdvisible.Style.Add("display", "block");
                    }

                }
            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string encryptedPassword = EncryptDecrypt.Encrypt(radtxt_password.Text.TrimStart().TrimEnd(), true);
        int sucess = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "update Clients set Password='" + encryptedPassword + "',ActivationCode='' where ActivationCode='" + authcode + "'");
        if (sucess == 1)
        {
            lbl_message.Text = "Account activated successfully";
            btn_submit.Enabled = false;
        }
    }
}