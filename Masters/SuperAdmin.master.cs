using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class SuperAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            RadMenu1.LoadContentFile("~/Menu/SuperAdminMenu.xml");
            UserMaster userMaster = new UserMaster();
            userMaster = (UserMaster)Session["UserMasterDetails"];
            if (userMaster == null)
            {
                Response.Redirect("~/AdminLogin.aspx");
            }
            else
            {
                lbl_welcomename.Text = userMaster.FirstName + " " + userMaster.LastName;
                DataTable dt_role = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select userRole from SuperUserRoles where userRoleID=" + userMaster.UserTypeID + "").Tables[0];
                if (dt_role.Rows.Count > 0)
                {
                    lbl_role.Text = dt_role.Rows[0]["userRole"].ToString(); ;
                }
                DataTable dt_rolePermission = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUserTypePermissions where userRoleID=" + userMaster.UserTypeID + "").Tables[0];
                if (dt_rolePermission.Rows.Count > 0)
                {
                    for (int i = RadMenu1.Items.Count - 1; i > -1; i--)
                    {
                        if (dt_rolePermission.Rows[i]["accessTypeID"].ToString() == "3" &&
                           RadMenu1.Items[i].Value == dt_rolePermission.Rows[i]["moduleID"].ToString())
                        {
                            RadMenu1.Items[i].Remove();
                        }
                        else
                        {
                            RadMenu1.Items[i].Target = MailSending.getAccessName(dt_rolePermission.Rows[i]["accessTypeID"].ToString());
                        }
                    }
                }
            }
           
        }
       
       
    }
    protected void lnk_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/AdminLogin.aspx");
    }
}
