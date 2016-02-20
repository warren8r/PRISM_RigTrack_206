using System;
using System.Web;
using System.Web.UI;
using System.Data;
using Telerik.Web.UI;

public partial class Masters_ViewAllReports : System.Web.UI.MasterPage
{
    #region Page Behavior
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
            // Refresh the child items in the "Manage Assets" menu
            RefreshManageAssetsMenu();
        }
    }
    #endregion
    #region Buttons
    #endregion
    #region Utility Methods
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
                        ((GridTableView)ctl).PageSize = 100;
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
                        ((RadGrid)ctl).PageSize = 100;
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
        }
        catch (Exception ex)
        { }

        //MenuUpatePanel.Update();
    }
    #endregion
}
