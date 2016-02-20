using System;
using System.Web;
using System.Web.UI;
using System.Data;
using Telerik.Web.UI;

public partial class Masters_RigTrack_noheading : System.Web.UI.MasterPage
{
    const bool DisableAjax = false;
    const bool DisableGlobal = false;

    protected void PageInit(object sender, EventArgs e)
    {
        HttpContext.Current.ClearError();

        if (DisableAjax == true)
        {
           // RadScriptManager1.EnablePartialRendering = false;
        }
    }
    protected void lnk_myaccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ClientAdmin/MyAccount.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DisableAjax == true)
        {
            GetUserControls(Page.Controls);
        }

        if (DisableGlobal == false && IsPostBack == false)
            globalSetting(Page.Controls);

        //IF HOMEPAGE HIDE THE BREADCRUMBS - But Catch Errors
        try
        {
            sitemap_path.Visible = (SiteMap.CurrentNode != SiteMap.RootNode);
        }
        catch (Exception ex)
        {
        }

        //if (!Page.IsPostBack)
        //{
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
            // Show the user's name

            // Refresh the child items in the "Manage Assets" menu
            RefreshManageAssetsMenu();

            // Get the user's role
            DataTable dt_role = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnectionString(Session["ClientCode"].ToString()), CommandType.Text,
          "select userRole from UserRoles where userRoleID=" + userMaster.UserTypeID + "").Tables[0];

            // Get the role's permissions
            DataTable dt_rolePermission = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnectionString(Session["ClientCode"].ToString()), CommandType.Text,
                "select * from UserTypePermissions utp,Modules m where  m.moduleID=utp.moduleID and " +
                " parentid=0 and userRoleID=" + userMaster.UserTypeID + " order by m.moduleID").Tables[0];

        }
        //}
    }

    protected void lnk_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/ClientLogin.aspx");
    }
    protected void RadMenu1_PreRender(object sender, EventArgs e)
    {

        //foreach (RadMenuItem rootItem in RadMenu1.Items)
        //{

        //    foreach (RadMenuItem childItem in rootItem.Items)
        //    {
        //        if ( !String.IsNullOrWhiteSpace( childItem.NavigateUrl ))
        //            childItem.NavigateUrl = childItem.NavigateUrl + ( String.IsNullOrWhiteSpace(  rootItem.Target )? "": "?&a=" + rootItem.Target + "");// "employee_login/doctors_area/forms.aspx";
        //        else
        //            childItem.NavigateUrl = "javascript:void('Menu')";
        //    }
        //}
    }

    public void GetUserControls(ControlCollection controls)
    {
        foreach (Control ctl in controls)
        {
            if (ctl is RadAjaxPanel)
                ((RadAjaxPanel)ctl).EnableAJAX = false;

            if (ctl.Controls.Count > 0)
                GetUserControls(ctl.Controls);
        }
    }

    /**FIND SPECIFIC CONTROLS AND APPLY CUSTOM SETTINGS GLOBALLY THROUGHOUT THE SOFTWARE
     * SOME OF THESE SETTINGS COULD COME FROM THE DATABASE THERE IS A DATA SOURCE ON THE FRONT END FOR THIS. gridSettings
     **/
    public void globalSetting(ControlCollection controls)
    {
        //GET SETTINGS
        DataView radGridSettings = (DataView)gridSettings.Select(DataSourceSelectArguments.Empty);
        int defaultPageSize = 10;
        string[] pageSize = new string[] { };

        foreach (DataRowView row in radGridSettings)
        {
            switch ((string)row["name"])
            {
                case "PageSize":
                    pageSize = ((string)row["value"]).Split(',');
                    break;
                case "DefaultPageSize":
                    defaultPageSize = Convert.ToInt32(row["value"]);
                    break;
            }
        }

        //APPLY THE SETTINGS HERE
        foreach (Control ctl in controls)
        {
            if (ctl is GridTableView)
            {
                try
                {
                    if (pageSize.Length > 0)
                    {
                        ((GridTableView)ctl).PagerStyle.PageSizes = Array.ConvertAll<string, int>(pageSize, new Converter<string, int>(Convert.ToInt32));
                        ((GridTableView)ctl).PageSize = defaultPageSize;
                        ((GridTableView)ctl).DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            if (ctl is RadGrid)
            {
                try
                {
                    if (pageSize.Length > 0)
                    {
                        ((RadGrid)ctl).PagerStyle.PageSizes = Array.ConvertAll<string, int>(pageSize, new Converter<string, int>(Convert.ToInt32));
                        ((RadGrid)ctl).PageSize = defaultPageSize;
                        ((RadGrid)ctl).DataBind();
                    }
                }
                catch (Exception ex)
                {
                }
            }


            if (ctl.Controls.Count > 0)
                globalSetting(ctl.Controls);
        }
    }

    public void RefreshManageAssetsMenu()
    {
        // Get the database connection string from the web.config
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString;

        // Get the Asset labels from the database
        try
        {
            DataTable dt_asset = SqlHelper.ExecuteDataset(connString, CommandType.Text,
                "SELECT [clientAssetID], [clientAssetName] FROM [clientAssets] WHERE IsNull([active], 'false') = 'true' ORDER BY [clientAssetID]").Tables[0];

            // If asset labels were retrieved from the database use them to add child items to the "Manage Assets" menu, if it exists
            int assetCount = dt_asset.Rows.Count;
            if (assetCount > 0)
            {
                // Get the "Manage Assets" menu item
            }
        }
        catch (Exception ex)
        { }

        //MenuUpatePanel.Update();
    }
}
