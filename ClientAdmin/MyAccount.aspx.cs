using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
public partial class Modules_MyAccount : System.Web.UI.Page
{
    public static DataTable dt_rolenotification;
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction transaction;
    ClientMaster userMaster = new ClientMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_errormsg.Text = "";
       
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
            this.Session["userRoleID"] = userMaster.UserTypeID;
        }

        if (!IsPostBack)
        {
            //changes.SelectParameters.Add("userID", "0");
            //changes.Select(DataSourceSelectArguments.Empty);
            fillUserinfo();
            dt_rolenotification = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                " select * from Prism_User_EventNotificationType PUEN,Prism_NotificationType PN where PUEN.notificationid=PN.notificationid").Tables[0];
        }
    }
    public void fillUserinfo()
    {
      DataTable   dt_getClinetdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,"select * from Users").Tables[0];
       DataRow[] row_user=dt_getClinetdetails.Select("UserID="+Session["userId"].ToString()+"");
       if (row_user.Length > 0)
       {
           try
           {
               radtxt_fname.Text = row_user[0]["FirstName"].ToString();
               radtxt_lastname.Text = row_user[0]["LastName"].ToString();
               radtxt_title.Text = row_user[0]["Title"].ToString();
               radtxt_email.Text = row_user[0]["Email"].ToString();
               radtxt_address1.Text = row_user[0]["Address"].ToString();
               radtxt_address2.Text = row_user[0]["Address2"].ToString();
               radtxt_city.Text = row_user[0]["City"].ToString();
               radcombo_state.SelectedValue = row_user[0]["state"].ToString();
               radtxt_country.Text = row_user[0]["Country"].ToString();
               radtxt_zip.Text = row_user[0]["Zip"].ToString();
               radtxt_phone.Text = row_user[0]["Phone"].ToString();
               radtxt_cellno.Text = row_user[0]["Cellno"].ToString();
               radtxt_login.Text = row_user[0]["LoginName"].ToString();
               radcombo_role.SelectedValue = row_user[0]["userRoleID"].ToString();
               //if (row_user[0]["eventNotification"] != null)
               //    rb_event.SelectedValue = row_user[0]["eventNotification"].ToString();
               //else
               //    rb_event.SelectedValue = "N";
           }
           catch (Exception ex)
           {
               lbl_errormsg.Text = ex.Message;
           }
       }
    }
    protected void btn_create_Click(object sender, EventArgs e)
    {
        //GET SESSION INFORMATION

        UserMaster userMaster = new UserMaster();
        userMaster = readUserDetails(userMaster);

        string msg = updateUserDetails(userMaster);
        if (msg == "Success")
        {
            lbl_errormsg.Text = "Record Updated Successfully";
            lbl_errormsg.ForeColor = Color.Green;
            up1.DataBind();
            radgrid_notificationstatus.Rebind();
            fillUserinfo();
          //  btn_reset_Click(sender, e);
        }
        else
        {
            lbl_errormsg.Text = msg;
        }
        lbl_errormsg.Visible = true;



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
        //rb_event.SelectedValue = null;
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
        userMaster.State = radcombo_state.SelectedValue;
        userMaster.Country = radtxt_country.Text;
        userMaster.Zip = radtxt_zip.Text;
        userMaster.Phone = radtxt_phone.Text;
        userMaster.Cellno = radtxt_cellno.Text;
        userMaster.LoginName = radtxt_login.Text;
        userMaster.UserTypeID = Convert.ToInt32(radcombo_role.SelectedValue);
        userMaster.Status = radcombo_status.SelectedValue;

        return userMaster;

    }
    public String insertUserDetails(UserMaster userMaster)
    {
        string returnvalue = "";
        string insertSql = "insert into Users (userRoleID,firstName,lastName,title,loginName,email,address," +
            "address2,city,state,country,zip,phone,lastUpdatedDate,cellNo,activationCode,activationExpiry,createDate,status)values(" +
            "" + userMaster.UserTypeID + ",'" + userMaster.FirstName + "','" + userMaster.LastName + "','" + userMaster.Title + "','" + userMaster.LoginName + "'" +
            ",'" + userMaster.Email + "','" + userMaster.Address + "','" + userMaster.Address2 + "','" + userMaster.City + "','" + userMaster.State + "'," +
            "'" + userMaster.Country + "','" + userMaster.Zip + "','" + userMaster.Phone + "','" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','" + userMaster.Cellno + "','" + userMaster.Activationcode + "'," +
            "" + userMaster.ActivationExpiry + ",'" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','" + radcombo_status.SelectedValue + "')";
        try
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertSql);
            returnvalue = "Success";


            //LOG CREATE USER
            auditLog logChange = new auditLog(Session["client_database"].ToString());
            logChange.addValue(new Dictionary<string, string> { 
                { "pageId", "15" }, 
                { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
                { "attributeName", "Account Details" }, 
                { "description", "user created" }
                //{ "system", "false" },
                //{ "supperseded", "1" } 
            }, true);
        }
        catch (Exception ex)
        {
            returnvalue = ex.Message;
        }

        return returnvalue;
    }
    public String updateUserDetails(UserMaster userMaster)
    {
        db.Open();
        transaction = db.BeginTransaction();
        string returnvalue = "";
        string insertSql = "update Users set userRoleID=" + userMaster.UserTypeID + ",firstName='" + userMaster.FirstName + "',lastName='" + userMaster.LastName + "',title='" + userMaster.Title + "',loginName='" + userMaster.LoginName + "',email='" + userMaster.Email + "',address='" + userMaster.Address + "'," +
            "address2='" + userMaster.Address2 + "',city='" + userMaster.City + "',state='" + userMaster.State + "',country='" + userMaster.Country + "',zip='" + userMaster.Zip + "'," +
            "phone='" + userMaster.Phone + "',lastUpdatedDate='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "',"+
            " cellNo='" + userMaster.Cellno + "',status='" + userMaster.Status + "' where userID=" + Session["userId"].ToString() + "";
        try
        {

            //LOG CREATE USER
            auditLog logChange = new auditLog(Session["client_database"].ToString());
            logChange.addValue(new Dictionary<string, string> { 
                { "pageId", "15" }, 
                { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
                { "attributeName", "Account Details" }, 
                { "description", "user updated" },
                { "columnId", hid_edituserid.Value },
                { "query", "SELECT * FROM Users WHERE userID = '" + hid_edituserid.Value + "'" } 
            }, true);

            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertSql);
          
            foreach (GridDataItem item in radgrid_notificationstatus.Items)
            {
                DropDownList ddl_notification = (DropDownList)item.FindControl("ddl_notification");
                Label lbl_eventid = (Label)item.FindControl("lbl_eventid");
                
                    DataRow[] row_role = dt_rolenotification.Select("UserId="+Session["userId"].ToString()+" and userRoleID=" + userMaster.UserTypeID + " and EventID=" + lbl_eventid.Text + "");
                    if (row_role.Length == 0)
                    {
                        string queryinsertrolenoti = "Insert into Prism_User_EventNotificationType(UserId,EventID,userRoleID,notificationid) values" +
                            "(" + Session["userId"].ToString() + "," + lbl_eventid.Text + ","+ userMaster.UserTypeID+","+ddl_notification.SelectedValue+")";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryinsertrolenoti);
                    }
                    else
                    {
                        string updatenottype = "Update Prism_User_EventNotificationType set EventID=" + lbl_eventid.Text + ",userRoleID=" + userMaster.UserTypeID + "," +
                            " notificationid=" + ddl_notification.SelectedValue + " where UserId=" + Session["userId"].ToString() + " and " +
                            " EventID=" + lbl_eventid.Text + " and userRoleID=" + userMaster.UserTypeID + "";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updatenottype);
                    }
            }
            transaction.Commit();
            returnvalue = "Success";
        }
        catch (Exception ex)
        {
            returnvalue = ex.Message;
            transaction.Rollback();
        }

        return returnvalue;
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        clearitems();
       
        btn_create.Enabled = true;
        btn_create.Text = "Create";
    }  
    protected void radcombo_role_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dt_getuserpermissiondetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(),
                   CommandType.Text, "select * from UserTypePermissions ut,Modules m where userRoleID=" + radcombo_role.SelectedValue + "" +
               " and ut.moduleID=m.moduleID and accessTypeID <> 3  order by m.moduleID").Tables[0];
        if (dt_getuserpermissiondetails.Rows.Count > 0)
        {
            panel_modules.Controls.Add(new LiteralControl("<Table border='1'; cellpadding='0' cellspacing='0'><tr><td colspan=" + dt_getuserpermissiondetails.Rows.Count + " align='center'>" +
            " <strong>Module Access to User Type <span style='color:green'> " + radcombo_role.SelectedItem.Text + "</span></strong></td></tr><tr>"));

            for (int module = 0; module < dt_getuserpermissiondetails.Rows.Count; module++)
            {
                if (module % 8 == 0 && module != 0)
                    panel_modules.Controls.Add(new LiteralControl("<td>&#160;&#160;" + dt_getuserpermissiondetails.Rows[module]["moduleName"].ToString() + "(" + MailSending.getAccessNameShort(dt_getuserpermissiondetails.Rows[module]["accessTypeID"].ToString()) + ")&#160;&#160;</td></tr><tr>"));
                else
                    panel_modules.Controls.Add(new LiteralControl("<td>&#160;&#160;" + dt_getuserpermissiondetails.Rows[module]["moduleName"].ToString() + "(" + MailSending.getAccessNameShort(dt_getuserpermissiondetails.Rows[module]["accessTypeID"].ToString()) + ")&#160;&#160;</td>"));
            }
            panel_modules.Controls.Add(new LiteralControl("</tr></Table>"));
        }
    }
     protected void radgrid_notificationstatus_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        radgrid_notificationstatus.CurrentPageIndex = e.NewPageIndex;
        radgrid_notificationstatus.Rebind();
    }
     protected void radgrid_notificationstatus_ItemDataBound(object sender, GridItemEventArgs e)
     {
         if (e.Item is GridDataItem)
         {
             GridDataItem dataBoundItem = e.Item as GridDataItem;
             Label lbl_eventid = (Label)dataBoundItem.FindControl("lbl_eventid");
             DropDownList ddl_notification = (DropDownList)dataBoundItem.FindControl("ddl_notification");
              DataRow[] row_role = dt_rolenotification.Select("UserId="+Session["userId"].ToString()+" and userRoleID=" + userMaster.UserTypeID + " and EventID=" + lbl_eventid.Text + "");
              if (row_role.Length > 0)
              {
                  ddl_notification.SelectedValue = row_role[0]["notificationid"].ToString();
              }
         }
     }

}