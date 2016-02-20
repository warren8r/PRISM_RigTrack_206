using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
public partial class CreateAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //changes.SelectParameters.Add("userID", "0");
            //changes.Select(DataSourceSelectArguments.Empty);
        }
    }

    protected void btn_create_Click(object sender, EventArgs e)
    {
        //GET SESSION INFORMATION

        UserMaster userMaster = new UserMaster();
        userMaster = readUserDetails(userMaster);
        if (btn_create.Text == "Create")
        {
            DataTable dt_getalreadyexistinfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users where loginName='" + userMaster.LoginName + "' or email='" + userMaster.Email + "'").Tables[0];
            if (dt_getalreadyexistinfo.Rows.Count == 0)
            {
                string authcode = RandomCodegenerator.randomauthcode(20);

                userMaster.Activationcode = authcode;
                userMaster.ActivationExpiry = 5;

                String Msg = insertUserDetails(userMaster);
                if (Msg == "Success")
                {
                    string message = "By Clicking on the below link you can activate your Account<br/>" +
                       ConfigurationManager.AppSettings["AppURL"].ToString() + "/ClientActivation.aspx?authcode=" + authcode + "&cCode=" + Session["ClientCode"].ToString() + "";
                    string subject = "Confirmation Email From Limitless Healthcare IT's Meter Data Management System";
                    bool mailsentornot = MailSending.SendMail(radtxt_email.Text, subject, message);

                    lbl_errormsg.Text = (mailsentornot)? "Activation link is sent to your email": "Mail not sent";
                }
                else
                    lbl_errormsg.Text = "Record Not Inserted";

                btn_create.Enabled = false;
                clearitems();
            }
            else
            {
                lbl_errormsg.Text = "LoginName or Email already exist please try another.";
            }
        }
        else
        {
            string msg = updateUserDetails(userMaster);
            if (msg == "Success")
            {
                lbl_errormsg.Text = "Record Updated Successfully";
                radgrid_admindetails.Rebind();
                up1.DataBind();
                btn_reset_Click(sender, e);
            }
            else
            {
                lbl_errormsg.Text = msg;
            }
            lbl_errormsg.Visible = true;
        }
        radgrid_admindetails.Rebind();

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
        radgrid_admindetails.Rebind();
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
        string returnvalue = "";
        string insertSql = "update Users set userRoleID=" + userMaster.UserTypeID + ",firstName='" + userMaster.FirstName + "',lastName='" + userMaster.LastName + "',title='" + userMaster.Title + "',loginName='" + userMaster.LoginName + "',email='" + userMaster.Email + "',address='" + userMaster.Address + "'," +
            "address2='" + userMaster.Address2 + "',city='" + userMaster.City + "',state='" + userMaster.State + "',country='" + userMaster.Country + "',zip='" + userMaster.Zip + "'," +
            "phone='" + userMaster.Phone + "',lastUpdatedDate='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "',cellNo='" + userMaster.Cellno + "',status='" + userMaster.Status + "' where userID=" + hid_edituserid.Value + "";
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
            
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertSql);
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
        btn_create.Text = "Create";
    }
    protected void radgrid_admindetails_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.EditCommandName)
        {
            GridDataItem editedItem = e.Item as GridDataItem;

            string editid = (e.Item as GridDataItem).GetDataKeyValue("userID").ToString();
            DataTable dt_geteditdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users where userID=" + editid + "").Tables[0];
            if (dt_geteditdetails.Rows.Count > 0)
            {
                hid_edituserid.Value = editid;
                radtxt_fname.Text = dt_geteditdetails.Rows[0]["firstName"].ToString();
                radtxt_lastname.Text = dt_geteditdetails.Rows[0]["LastName"].ToString();
                radtxt_title.Text = dt_geteditdetails.Rows[0]["title"].ToString();
                radtxt_email.Text = dt_geteditdetails.Rows[0]["email"].ToString();
                radtxt_address1.Text = dt_geteditdetails.Rows[0]["address"].ToString();
                radtxt_address2.Text = dt_geteditdetails.Rows[0]["address2"].ToString();
                radtxt_city.Text = dt_geteditdetails.Rows[0]["city"].ToString();
                radcombo_state.SelectedValue = dt_geteditdetails.Rows[0]["state"].ToString();
                radtxt_country.Text = dt_geteditdetails.Rows[0]["country"].ToString();
                radtxt_zip.Text = dt_geteditdetails.Rows[0]["zip"].ToString();
                radtxt_phone.Text = dt_geteditdetails.Rows[0]["phone"].ToString();
                radtxt_cellno.Text = dt_geteditdetails.Rows[0]["cellNo"].ToString();
                radtxt_login.Text = dt_geteditdetails.Rows[0]["loginName"].ToString();
                radcombo_role.SelectedValue = dt_geteditdetails.Rows[0]["userRoleID"].ToString();
            }
            radgrid_admindetails.Rebind();
            btn_create.Text = "Save";
            e.Canceled = true;
        }
        if (e.CommandName == "ExpandCollapse")
        {
            GridDataItem row = (GridDataItem)e.Item;
            string userID = row.GetDataKeyValue("userID").ToString();

            radgrid_admindetails.MasterTableView.Items[0].Expanded = false;
            foreach (GridItem item in radgrid_admindetails.MasterTableView.Items)
            {
                item.Expanded = false;
            }

            changes.SelectParameters.Clear();
            changes.SelectCommand += " AND transactionLog.columnId = @userID";
            changes.SelectParameters.Add("userID", userID);
            changes.Select(DataSourceSelectArguments.Empty);
            changes.DataBind();

        }
    }
    protected void radcombo_role_SeelselectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
    protected void radgrid_admindetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
        {
            GridDataItem editedItem = e.Item as GridDataItem;

            string editid = (e.Item as GridDataItem).GetDataKeyValue("userID").ToString();
            string userType_Tooltiptext = "";
            DataTable dt_geteditdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users where userID=" + editid + "").Tables[0];
            if (dt_geteditdetails.Rows.Count > 0)
            {
                DataTable dt_getuserpermissiondetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(),
                    CommandType.Text, "select * from UserTypePermissions ut,Modules m where userRoleID=" + dt_geteditdetails.Rows[0]["userID"].ToString() + "" +
                " and ut.moduleID=m.moduleID").Tables[0];
                if (dt_getuserpermissiondetails.Rows.Count > 0)
                {
                    for (int module = 0; module < dt_getuserpermissiondetails.Rows.Count; module++)
                    {
                        userType_Tooltiptext += dt_getuserpermissiondetails.Rows[module]["moduleName"].ToString() + ",";
                    }
                }

            }
            if (userType_Tooltiptext != "")
            {
                e.Item.Cells[4].ToolTip = userType_Tooltiptext.Remove(userType_Tooltiptext.Length - 1);
            }

        }
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

}