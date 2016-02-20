using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;
public partial class CreateAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //radtxt_country.Items.Add(new RadComboBoxItem("Select", "0"));
        //radtxt_country.Items.Insert(0, new RadComboBoxItem("Select","0"));
        if (!IsPostBack)
        {
            DataTable dt_fillcountries = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [CountryID], [CountryName] FROM [Countries] ORDER BY [CountryID]").Tables[0];
            RadComboBoxFill.FillRadcombobox(radtxt_country, dt_fillcountries, "CountryName", "CountryID", "0");
            DataTable dt_fillroles = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [userRoleID], [userRole] FROM [SuperUserRoles] ORDER BY [userRoleID]").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_role, dt_fillroles, "userRole", "userRoleID", "0");
        }
    }
    protected void radtxt_country_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dt_fillstates = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [StateID], [StateName] FROM [States] where CountryID=" + radtxt_country.SelectedValue + "").Tables[0];
        RadComboBoxFill.FillRadcombobox(radcombo_state, dt_fillstates, "StateName", "StateID", "0");
    }
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument == "Rebind")
        {
            //RadGrid2.MasterTableView.SortExpressions.Clear();
            //RadGrid2.MasterTableView.GroupByExpressions.Clear();
            //radcombo_state.
        }

    }
    protected void btn_create_Click(object sender, EventArgs e)
    {
        UserMaster userMaster = new UserMaster();
        userMaster = readUserDetails(userMaster);
        if (btn_create.Text == "Create")
        {
            DataTable dt_getalreadyexistinfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUsers where loginName='" + userMaster.LoginName + "' or email='" + userMaster.Email + "'").Tables[0];
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
                        lbl_errormsg.ForeColor = Color.Green;
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
        else
        {
            string msg = updateUserDetails(userMaster);
            if (msg == "Success")
            {
                lbl_errormsg.Text = "Record Updated Successfully";
                lbl_errormsg.ForeColor = Color.Green;
                radgrid_admindetails.Rebind();
            }
            else
            {
                lbl_errormsg.Text = msg;
            }
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
        userMaster.Status = radcombo_status.SelectedValue;

        return userMaster;

    }
    public String insertUserDetails(UserMaster userMaster)
    {
        string returnvalue = "";
        string insertSql = "insert into SuperUsers (userRoleID,firstName,lastName,title,loginName,email,address," +
            "address2,city,state,country,zip,phone,lastUpdatedDate,cellNo,activationCode,activationExpiry,createDate,status)values(" +
            "" + userMaster.UserTypeID + ",'" + userMaster.FirstName + "','" + userMaster.LastName + "','" + userMaster.Title + "','" + userMaster.LoginName + "'" +
            ",'" + userMaster.Email + "','" + userMaster.Address + "','" + userMaster.Address2 + "','" + userMaster.City + "','" + userMaster.State + "'," +
            "'" + userMaster.Country + "','" + userMaster.Zip + "','" + userMaster.Phone + "','" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','" + userMaster.Cellno + "','" + userMaster.Activationcode + "'," +
            "" + userMaster.ActivationExpiry + ",'" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','" + radcombo_status.SelectedValue + "')";
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
    public String updateUserDetails(UserMaster userMaster)
    {
        string returnvalue = "";
        string insertSql = "update SuperUsers set userRoleID=" + userMaster.UserTypeID + ",firstName='" + userMaster.FirstName + "',lastName='" + userMaster.LastName + "',title='" + userMaster.Title + "',loginName='" + userMaster.LoginName + "',email='" + userMaster.Email + "',address='" + userMaster.Address + "'," +
            "address2='" + userMaster.Address2 + "',city='" + userMaster.City + "',state='" + userMaster.State + "',country='" + userMaster.Country + "',zip='" + userMaster.Zip + "'," +
            "phone='" + userMaster.Phone + "',lastUpdatedDate='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "',cellNo='" + userMaster.Cellno + "',status='" + userMaster.Status + "' where userID=" + hid_edituserid.Value + "";
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
        btn_create.Text = "Create";
    }
    protected void radgrid_admindetails_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.EditCommandName)
        {
            lbl_errormsg.Text = "";
            GridDataItem editedItem = e.Item as GridDataItem;

            string editid = (e.Item as GridDataItem).GetDataKeyValue("userID").ToString();
            DataTable dt_geteditdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUsers where userID=" + editid + "").Tables[0];
            if (dt_geteditdetails.Rows.Count > 0)
            {
                //DataTable dt_fillcountries = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [CountryID], [CountryName] FROM [Countries] ORDER BY [CountryID]").Tables[0];
                DataTable dt_fillroles = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [userRoleID], [userRole] FROM [SuperUserRoles] ORDER BY [userRoleID]").Tables[0];
                //
                //RadComboBoxFill.FillRadcombobox(radtxt_country, dt_fillcountries, "CountryName", "CountryID", "0");
                RadComboBoxFill.FillRadcombobox(radcombo_role, dt_fillroles, "userRole", "userRoleID", "0");
                DataTable dt_fillstates = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [StateID], [StateName] FROM [States]").Tables[0];
                RadComboBoxFill.FillRadcombobox(radcombo_state, dt_fillstates, "StateName", "StateID", "0");
                hid_edituserid.Value = editid;
                radtxt_fname.Text = dt_geteditdetails.Rows[0]["firstName"].ToString();
                radtxt_lastname.Text = dt_geteditdetails.Rows[0]["LastName"].ToString();
                radtxt_title.Text = dt_geteditdetails.Rows[0]["title"].ToString();
                radtxt_email.Text = dt_geteditdetails.Rows[0]["email"].ToString();
                radtxt_address1.Text = dt_geteditdetails.Rows[0]["address"].ToString();
                radtxt_address2.Text = dt_geteditdetails.Rows[0]["address2"].ToString();
                radtxt_city.Text = dt_geteditdetails.Rows[0]["city"].ToString();

                radcombo_state.SelectedItem.Text = dt_geteditdetails.Rows[0]["state"].ToString();
                radtxt_country.SelectedItem.Text = dt_geteditdetails.Rows[0]["country"].ToString();
                radtxt_zip.Text = dt_geteditdetails.Rows[0]["zip"].ToString();
                radtxt_phone.Text = dt_geteditdetails.Rows[0]["phone"].ToString();
                radtxt_cellno.Text = dt_geteditdetails.Rows[0]["cellNo"].ToString();
                radtxt_login.Text = dt_geteditdetails.Rows[0]["loginName"].ToString();
                radcombo_role.SelectedValue = dt_geteditdetails.Rows[0]["userRoleID"].ToString();
            }
            radgrid_admindetails.Rebind();
            btn_create.Text = "Update";
            e.Canceled = true;
        }

    }
}