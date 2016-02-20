using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
public partial class ManageClients : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            radtxt_from.SelectedDate = DateTime.Now.AddMonths(-1);
            radtxt_to.SelectedDate = DateTime.Now.AddDays(5);
            btn_Approve.Visible = false;
            btn_Update.Visible = false;
            btn_Deny.Visible = false;
            btn_reset.Visible = false;
        }
    }
    public void bindradiobuttons()
    {
        //DataTable dt_getrbbind = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from ClientAppType").Tables[0];
        //rad_appuse.DataSource = dt_getrbbind;
        //rad_appuse.DataTextField = "appType";
        //rad_appuse.DataValueField = "appTypeID";
        //rad_appuse.DataBind();
    }
    public void bindcheckboxes()
    {
        DataTable dt_getrbbind = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from meterTypes").Tables[0];
        chk_metertype.DataSource = dt_getrbbind;
        chk_metertype.DataTextField = "meterType";
        chk_metertype.DataValueField = "meterId";
        chk_metertype.DataBind();
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        gridbind();
        
        btn_Approve.Visible = false;
        btn_Update.Visible = false;
        btn_Deny.Visible = false;
        btn_reset.Visible = false;
        td_viewdetails.Visible = false;
        lbl_errormsg.Text = "";
    }
    public void gridbind()
    {
        string fromdate = String.Format("{0:MM/dd/yyyy}", radtxt_from.SelectedDate);
        string todate = String.Format("{0:MM/dd/yyyy}", radtxt_to.SelectedDate);
        DataTable dt_getclients = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select *,(address+' '+address2) as caddress,(sec_address+' '+sec_address2) as seccaddress from clients where approvalStatus='" + radcombo_userstatus.SelectedValue + "' and lastUpdatedDate between '" + fromdate + "' and '" + todate + "' order by clientID desc").Tables[0];
        if (dt_getclients.Rows.Count > 0)
        {
            radgrid_clientdetails.DataSource = dt_getclients;
            radgrid_clientdetails.DataBind();
        }
        else
        {

            radgrid_clientdetails.DataSource = dt_getclients;
            radgrid_clientdetails.DataBind();
        }
    }
    protected void radgrid_clientdetails_SortCommand(object source, GridSortCommandEventArgs e)
    {
        //Default sort order Descending


        if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Ascending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            
        }
        gridbind();
    }
    protected void radgrid_clientdetails_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        radgrid_clientdetails.CurrentPageIndex = e.NewPageIndex;
        gridbind();
    }
    protected void radgrid_clientdetails_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
    {
        //radgrid_clientdetails. = e.NewPageIndex;
        gridbind();
    }
    protected void radgrid_clientdetails_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.EditCommandName)
        {
            bindradiobuttons();
            bindcheckboxes();
            if (radcombo_userstatus.SelectedValue == "Approved")
            {
                btn_Approve.Visible = false;
                btn_Update.Visible = true;
                btn_Deny.Visible = true;
                btn_reset.Visible = true;
            }
            else if (radcombo_userstatus.SelectedValue == "Pending")
            {
                btn_Approve.Visible = true;
                btn_Update.Visible = false;
                btn_Deny.Visible = true;
                btn_reset.Visible = true;
            }
            else if (radcombo_userstatus.SelectedValue == "Denied")
            {
                btn_Approve.Visible = true;
                btn_Update.Visible = false;
                btn_Deny.Visible = false;
                btn_reset.Visible = true;
            }
            td_viewdetails.Visible = true;
            lbl_errormsg.Text = "";
            GridDataItem editedItem = e.Item as GridDataItem;

            string editid = (e.Item as GridDataItem).GetDataKeyValue("clientID").ToString();
            hid_viewuserid.Value = editid;
            DataTable dt_geteditdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from clients where clientID=" + editid + "").Tables[0];
            if (dt_geteditdetails.Rows.Count > 0)
            {
                radcombo_clientstatus.SelectedValue = dt_geteditdetails.Rows[0]["clientStatus"].ToString();
                lbl_clcode.Text = dt_geteditdetails.Rows[0]["clientCode"].ToString();
                lbl_clientname.Text = dt_geteditdetails.Rows[0]["clientCode"].ToString();
                lbl_company.Text = dt_geteditdetails.Rows[0]["company"].ToString();
                lbl_fname.Text = dt_geteditdetails.Rows[0]["firstName"].ToString();
                lbllname.Text = dt_geteditdetails.Rows[0]["lastName"].ToString();
                lbl_email.Text = dt_geteditdetails.Rows[0]["email"].ToString();

                DataTable dt_getexisting = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from ClientSubscription cs,ClientAppType ca where clientID=" + editid + "").Tables[0];
                if (dt_getexisting.Rows.Count > 0)
                {
                    hid_existvaluesforassets.Value = "";
                    string valuesofassets = "", finalvalcnt = "";
                    DataTable dt_getassetname = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from ClientAssets  where subscriptionID=" + dt_getexisting.Rows[0]["subscriptionID"].ToString() + "").Tables[0];
                    for (int assetcnt = 0; assetcnt < dt_getassetname.Rows.Count; assetcnt++)
                    {
                        valuesofassets += dt_getassetname.Rows[assetcnt]["clientAssetName"].ToString() + ",";
                    }
                    if (valuesofassets != "")
                    {
                        finalvalcnt = valuesofassets.Remove(valuesofassets.Length - 1, 1);
                        hid_existvaluesforassets.Value = finalvalcnt;
                    }
                    //string javaScript ="<script language=JavaScript>\n" +"alert('Button1_Click client-side');\n" +"</script>";
                    string str = "<script>formtextboxescodebehind(" + dt_getexisting.Rows[0]["maxClientAssets"].ToString() + ",\"" + finalvalcnt + "\");</script>";
                    //string str = "<script>formtextboxes('2');</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "JCall1", str, false);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", str, false);
                    //RegisterStartupScript("Button1_ClickScript", javaScript); 
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", "<script type='text/javascript'></script>", true);
                    lbl_subscriptionid.Text = dt_getexisting.Rows[0]["subscriptionID"].ToString();
                    radtxt_allowedusers.Text = dt_getexisting.Rows[0]["maxUsers"].ToString();
                    radtxt_subperiod.Text = dt_getexisting.Rows[0]["subDuration"].ToString();
                    radtxt_usertypes.Text = dt_getexisting.Rows[0]["maxUserTypes"].ToString();
                    txt_nofassets.Text = dt_getexisting.Rows[0]["maxClientAssets"].ToString();
                    DataTable dt_getclientMetertypes = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from clientMeterTypes where clientID=" + hid_viewuserid.Value + "").Tables[0];

                    for (int m = 0; m < dt_getclientMetertypes.Rows.Count; m++)
                    {
                        foreach (ListItem rb1 in chk_metertype.Items)
                        {
                            if (rb1.Value == dt_getclientMetertypes.Rows[m]["clientMeterTypeId"].ToString())
                            {
                                rb1.Selected = true;
                                //int insertclientmetertypes = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "insert into clientMeterTypes(clientID,clientMeterTypeId)values(" + hid_viewuserid.Value + "," + rb1.Value + ")");
                            }
                        }
                    }
                    //foreach (ListItem rb in rad_appuse.Items)
                    //{
                    //    if (dt_getexisting.Rows[0]["appTypeID"].ToString() == rb.Value)
                    //    {
                    //        rb.Selected = true;
                    //    }
                    //}
                }
                else
                {
                    hid_existvaluesforassets.Value = "";
                    radtxt_allowedusers.Text = "";
                    radtxt_subperiod.Text = "";
                    radtxt_usertypes.Text = "";
                    txt_nofassets.Text = "";
                    //foreach (ListItem rb in rad_appuse.Items)
                    //{
                    //    rb.Selected = false;
                    //}
                }

                //DataTable dt_fillcountries = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "SELECT [CountryID], [CountryName] FROM [Countries] ORDER BY [CountryID]").Tables[0];

            }


            e.Canceled = true;
        }

    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        string vieweduserid = hid_viewuserid.Value;
        string hidval = hid_1.Value;
        string insertquery = "";
        string appusetype = "";
        int clientstatuscount = 0;

        SqlTransaction transaction;
        SqlConnection cn = new SqlConnection(GlobalConnetionString.ConnectionString);
        cn.Open();
        transaction = cn.BeginTransaction();
        try
        {
            insertquery = "insert into ClientSubscription(clientID,subDuration,maxUsers,maxUserTypes,maxClientAssets)values(" + vieweduserid + ",'" + radtxt_subperiod.Text + "'," + radtxt_allowedusers.Text + "," + radtxt_usertypes.Text + "," + txt_nofassets.Text + ")";
            int successcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertquery);
            if (successcount == 1)
            {
                string authcode = RandomCodegenerator.randomauthcode(20);
                DataTable dtgetmaxrecord = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select max(subscriptionID) as subscriptionID from ClientSubscription").Tables[0];
                //string[] splitvalueofassets = hidval.Split(',');
                //for (int i = 0; i < splitvalueofassets.Length; i++)
                //{
                //    clientstatuscount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "insert into ClientAssets(subscriptionID,clientAssetName)values(" + dtgetmaxrecord.Rows[0]["subscriptionID"] + ",'" + splitvalueofassets[i].ToString() + "')");
                //}
                foreach (ListItem rb1 in chk_metertype.Items)
                {
                    if (rb1.Selected)
                    {
                        int insertclientmetertypes = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "insert into clientMeterTypes(clientID,clientMeterTypeId)values(" + hid_viewuserid.Value + "," + rb1.Value + ")");
                    }
                }

                int updatecount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "update Clients set approvalStatus='Approved',clientStatus='" + radcombo_clientstatus.SelectedValue + "',activationCode='" + authcode + "',activationExpiry=5 where clientID=" + vieweduserid + "");
                lbl_errormsg.Text = "Record Approved Successfully";
                string message = "By Clicking on the below link you can activate your Account<br/>" +
                           ConfigurationManager.AppSettings["AppURL"].ToString() + "NewClientActivation.aspx?authcode=" + authcode + "";
                string subject = "Confirmation Email From Limitless Healthcare IT's Meter Data Management System";
                bool mailsentornot = MailSending.SendMail(lbl_email.Text, subject, message);
                if (mailsentornot)
                {
                    lbl_errormsg.Text = "Record Approved and Activation link is sent to email";
                }
                else
                {
                    lbl_errormsg.Text = "Mail not sent";
                }
                //}
                //DataTable dt_getclientcode = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select clientCode from Clients where clientID='" + vieweduserid + "'").Tables[0];
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "Create DataBase [" + lbl_clcode.Text + "]");
                string _connectionsql = @"Persist Security Info=False;User ID=sa;pwd=E@gl3sFli;server=67.40.65.178;database=" + lbl_clcode.Text + "";//;integrated security=SSPI";
                ScriptDB.ExecuteSQLScript(Server.MapPath(ConfigurationManager.AppSettings["mapserverpath"].ToString()) + "/dbScripts/CreateTableScript.sql", _connectionsql);
                resetall();
                //_connectionsql = @"Persist Security Info=False;User ID=sa;pwd=sa;server=LUCKNOW;database=" + lbl_clientcode.Text + "";//;integrated security=SSPI";
                //con_create = new SqlConnection(_connectionsql);
            }
            transaction.Commit();
            cn.Close();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
        }
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        string vieweduserid = hid_viewuserid.Value;
        string hidval = hid_1.Value;
        string updatequery = "";
        string appusetype = "";
        int clientstatuscount = 0;
        //foreach (ListItem rb in rad_appuse.Items)
        //{
        //    if (rb.Selected)
        //    {
        //        DataTable dt_getappusetypeid = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select appTypeID from ClientAppType where appType='" + rb.Text + "'").Tables[0];
        //        appusetype = dt_getappusetypeid.Rows[0]["appTypeID"].ToString();
        //    }
        //}
        updatequery = "update ClientSubscription set subDuration='" + radtxt_subperiod.Text + "',maxUsers=" + radtxt_allowedusers.Text + ",maxUserTypes=" + radtxt_usertypes.Text + ",maxClientAssets=" + txt_nofassets.Text + " where subscriptionID=" + lbl_subscriptionid.Text + "";
        int successcount = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, updatequery);
        if (successcount == 1)
        {
            //lbl_subscriptionid.Text
            //DataTable dtgetmaxrecord = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select max(subscriptionID) as subscriptionID from ClientSubscription").Tables[0];
            //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "delete from ClientAssets where subscriptionID=" + Convert.ToInt32(lbl_subscriptionid.Text) + "");
            //string[] splitvalueofassets = hidval.Split(',');
            //for (int i = 0; i < splitvalueofassets.Length; i++)
            //{
            //    clientstatuscount = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "insert into ClientAssets(subscriptionID,clientAssetName)values(" + lbl_subscriptionid.Text + ",'" + splitvalueofassets[i].ToString() + "')");
            //}
            int truncate = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "delete from clientMeterTypes where clientID=" + hid_viewuserid.Value + "");
            foreach (ListItem rb1 in chk_metertype.Items)
            {
                if (rb1.Selected)
                {
                    int insertclientmetertypes = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "insert into clientMeterTypes(clientID,clientMeterTypeId)values(" + hid_viewuserid.Value + "," + rb1.Value + ")");
                }
            }
            //if (clientstatuscount > 0)
            //{
            int updatecount = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "update Clients set approvalStatus='Approved',clientStatus='" + radcombo_clientstatus.SelectedValue + "' where clientID=" + vieweduserid + "");
            lbl_errormsg.Text = "Record Saved Successfully";
            resetall();
            //}
        }
    }
    protected void btn_Deny_Click(object sender, EventArgs e)
    {
        string vieweduserid = hid_viewuserid.Value;
        int updatecount = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "update Clients set approvalStatus='Denied',clientStatus='" + radcombo_clientstatus.SelectedValue + "' where clientID=" + vieweduserid + "");
        if (updatecount == 1)
        {
            lbl_errormsg.Text = "Record Denied Successfully";
        }
        else
        {
            lbl_errormsg.Text = "Error!!! Record not Denied";
        }
        resetall();
    }
    public void resetall()
    {
        radcombo_userstatus.SelectedValue = "Select";
        radgrid_clientdetails.DataSource = null;
        radgrid_clientdetails.DataBind();
        td_viewdetails.Visible = false;
        btn_Approve.Visible = false;
        btn_Update.Visible = false;
        btn_Deny.Visible = false;
        btn_reset.Visible = false;
        td_viewdetails.Visible = false;
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        //string val = rad_appuse.SelectedValue;
        //foreach (ListItem rb1 in chk_metertype.Items)
        //{
        //    if (rb1.Selected)
        //    {
        //        int insertclientmetertypes = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "insert into clientMeterTypes(clientID,clientMeterTypeId)values(" + hid_viewuserid.Value + "," + rb1.Value + ")");
        //    }
        //}
        resetall();
    }
}