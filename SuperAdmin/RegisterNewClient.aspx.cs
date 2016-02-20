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
using System.Data.SqlClient;

public partial class RegisterNewClient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt_fillcountries = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [CountryID], [CountryName] FROM [Countries] ORDER BY [CountryID]").Tables[0];
            DataTable dt_fillcountries1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [CountryID], [CountryName] FROM [Countries] ORDER BY [CountryID]").Tables[0];
            RadComboBoxFill.FillRadcombobox(radtxt_country, dt_fillcountries, "CountryName", "CountryID", "0");
            RadComboBoxFill.FillRadcombobox(rad2_country, dt_fillcountries1, "CountryName", "CountryID", "0");
        }
    }

    protected void btn_create_Click(object sender, EventArgs e)
    {
        Clients userMaster = new Clients();
        userMaster = readUserDetails(userMaster);
        DataTable dt_getalreadyexistinfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from Clients where clientCode='" + userMaster.ClientCode + "'").Tables[0];
        if (dt_getalreadyexistinfo.Rows.Count == 0)
        {
            string authcode = RandomCodegenerator.randomauthcode(20);

            userMaster.Activationcode = authcode;
            userMaster.ActivationExpiry = 5;

            String Msg = insertUserDetails(userMaster);
            if (Msg == "Success")
            {
                lbl_errormsg.Text = "Record Inserted";
                //string message = "By Clicking on the below link you can activate your Account<br/>" +
                //   ConfigurationManager.AppSettings["AppURL"].ToString() + "/NewClientActivation.aspx?authcode=" + authcode + "";
                //string subject = "Confirmation Email From Limitless Healthcare IT's Meter Data Management System";
                //bool mailsentornot = MailSending.SendMail(radtxt_email.Text, subject, message);
                //if (mailsentornot)
                //{
                //    lbl_errormsg.Text = "Activation link is sent to your email";
                //}
                //else
                //{
                //    lbl_errormsg.Text = "Mail not sent";
                //}
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
            lbl_errormsg.Text = "Client Code already exist please try another.";
        }


    }
    public void clearitems()
    {
        radtxt_fname.Text = "";
        radtxt_lastname.Text = "";
        radtxt_company.Text = "";
        radtxt_email.Text = "";
        radtxt_address1.Text = "";
        radtxt_address2.Text = "";
        radtxt_city.Text = "";
        radtxt_country.Text = "";
        radtxt_zip.Text = "";
        radtxt_phone.Text = "";
        radtxt_cellno.Text = "";
        radtxt_login.Text = "";
        radtxt_clientcode.Text = "";
        radtxt_fax.Text = "";
        txt_nofassets.Text = "";
        radtxt_allowedusers.Text = "";
        radtxt_usertypes.Text = "";
        radtxt_subperiod.Text = "";
        //lbl_errormsg.Text = "";
    }

    public Clients readUserDetails(Clients userMaster)
    {

        userMaster.FirstName = radtxt_fname.Text;
        userMaster.LastName = radtxt_lastname.Text;
        userMaster.Company = radtxt_company.Text;
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
        userMaster.UserTypeID = 1;
        userMaster.ClientCode = radtxt_clientcode.Text;

        return userMaster;

    }
    protected void radtxt_country_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dt_fillstates = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [StateID], [StateName] FROM [States] where CountryID=" + radtxt_country.SelectedValue + "").Tables[0];
        RadComboBoxFill.FillRadcombobox(radcombo_state, dt_fillstates, "StateName", "StateID", "0");
    }
    protected void rad2_country_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dt_fillstates = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [StateID], [StateName] FROM [States] where CountryID=" + rad2_country.SelectedValue + "").Tables[0];
        RadComboBoxFill.FillRadcombobox(rad2_state, dt_fillstates, "StateName", "StateID", "0");
    }
    
    public String insertUserDetails(Clients userMaster)
    {
        string returnvalue = "";
        string insertSql = "insert into Clients(clientCode,firstName,lastName,company,loginName,email,address," +
            "address2,city,state,country,zip,phone,lastUpdatedDate,cellNo,activationCode,activationExpiry,createDate,approvalStatus,"+
        "sec_firstName,sec_lastName,sec_email,sec_address,sec_address2,sec_city,sec_state,sec_country,sec_zip,sec_phone,sec_cellNo,"+
        "NoOfAssets,NoOfUsers,NoOfUserTypes,SubscriptionPeriod)values(" +
            "'" + userMaster.ClientCode + "','" + userMaster.FirstName + "','" + userMaster.LastName + "','" + userMaster.Company + "','" + userMaster.LoginName + "'" +
            ",'" + userMaster.Email + "','" + userMaster.Address + "','" + userMaster.Address2 + "','" + userMaster.City + "','" + userMaster.State + "'," +
            "'" + userMaster.Country + "','" + userMaster.Zip + "','" + userMaster.Phone + "','" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','" + userMaster.Cellno + "','" + userMaster.Activationcode + "'," +
            "" + userMaster.ActivationExpiry + ",'" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','Pending','" + rad2_firstname.Text + "','" + rad2_lastname.Text + "'," +
        "'" + rad2_email.Text + "','" + rad2_address.Text + "','" + rad2_address2.Text + "','" + rad2_city.Text + "','" + rad2_state.SelectedValue + "','" + rad2_country.SelectedValue + "'," +
        "'" + rad2_zip.Text + "','" + rad2_phone.Text + "','" + rad2_mobile.Text + "'," + txt_nofassets.Text + "," + radtxt_allowedusers.Text + "," + radtxt_usertypes.Text + "," + radtxt_subperiod.Text + ")";
        SqlConnection db = new SqlConnection(GlobalConnetionString.ConnectionString);
        SqlTransaction transaction;
        db.Open();
        //db.ConnectionTimeout(120);
        transaction = db.BeginTransaction();
        try
        {
           
            
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertSql);
            DataTable dtClients = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    Clients WHERE  clientID = IDENT_CURRENT('Clients')").Tables[0];
            string clientID = dtClients.Rows[0]["clientID"].ToString();
            string insertSuscription = "Insert into ClientSubscription(clientID,maxClientAssets,maxUsers,maxUserTypes,subDuration)" +
                " values("+clientID+"," + txt_nofassets.Text + "," + radtxt_allowedusers.Text + "," + radtxt_usertypes.Text + ",'" + radtxt_subperiod.Text + "')";
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,insertSuscription);
            returnvalue = "Success";
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
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