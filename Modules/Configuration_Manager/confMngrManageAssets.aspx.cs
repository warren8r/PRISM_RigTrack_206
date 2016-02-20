using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Diagnostics;
using Telerik.Web.UI;
using Artem.Google.Net;

public partial class Modules_Configuration_Manager_confMngrManageAssets : System.Web.UI.Page
{

    #region Properties
    
    // Asset type code - clientAssets.clientAssetsID
    public int AssetType
    {
        get 
        { 
            int rtnValue = 0;
            Int32.TryParse(hdnAssetType.Value, out rtnValue);
            return rtnValue;
        }

        set
        {
            hdnAssetType.Value = value.ToString();
        }
    }

    // Asset name - clientAssets.clientAssetsName
    public string AssetName
    {
        get { return hdnAssetName.Value; }
        set { hdnAssetName.Value = value; }
    }

    public string ConnectionString
    {
        get { return Session["client_database"].ToString(); }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear the page's message area
        lblMessage.Text = string.Empty;

        // Load JavaScripts for the page
        RegisterJavaScript();
        
        if (!Page.IsPostBack)
        {
            // Get the m= value from the URL's query string, if present
            string strM = GetQueryStringAssetType();

            // Set the asset type and name properties from the passed query string value
            if (strM != string.Empty)
            {
                AssetType = Convert.ToInt32(strM);
                AssetName = GetAssetName(AssetType);
            }
            else
            {
                AssetType = 0;
                AssetName = "";
            }

            // Default to Create xxxx mode
            lblPageMode.Text = "Create";

            // Refresh the grid and dropdowns
            grdAssetList.Rebind();
            RefreshCountryCodeLists();
            RefreshStateCodeLists();

            // Clear out the Create/Edit panel fields
            ClearSelectedAssetFields();
        }

    }

    #region Event Handlers

    protected void btnCancelAsset_Click(object sender, EventArgs e)
    {
        // If in "Edit" mode, switch to "Create" mode
        if (lblPageMode.Text == "Edit")
        {
            lblPageMode.Text = "Create";
            btnCancelAsset.Text = "Reset";
        }

        // Unselect any selected rows in the grid and clear the Create/Edit panel fields
        grdAssetList.SelectedIndexes.Clear();
        ClearSelectedAssetFields();
    }

    protected void btnSaveAsset_Click(object sender, EventArgs e)
    {
        // Are we editing an asset?
        if (lblPageMode.Text == "Edit")
        {
            // Yes.  Are the field contents valid?
            if (ValidEditAsset())
            {
                // Yes.  Update the asset row and if it succeeds change to "Create" mode
                if (UpdateSelectedAsset())
                {
                    // Change to "Create" mode
                    lblPageMode.Text = "Create";
                    btnCancelAsset.Text = "Reset";

                    // Unselect any rows in the Grid
                    grdAssetList.SelectedIndexes.Clear();
                    grdAssetList.Rebind();

                    // Clear the contends of the Create/Edit panel fields
                    ClearSelectedAssetFields();

                    // Notify the user
                    WriteMessage(lblMessage, AssetName + " changes were saved", true);
                    PopupNotify(AssetName + " changes were saved");
                }
            }
        }
        else
        {
            // No, in "Create" mode.  Are the field contents valid?
            if (ValidNewAsset())
            {
                // Yes, insert a new row in the assets table, rebind the grid to show the new ros and clear the Create/Edit panel fields
                InsertNewAsset();
                grdAssetList.Rebind();
                ClearSelectedAssetFields();

                // Notify the user
                WriteMessage(lblMessage, AssetName + " was created", true);
                PopupNotify(AssetName + " was created");
            }
        }

    }

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        // Set the text and color of the label next to the Status CheckBox to reflect the Checked state of the checkbox
        SetStatusAttributes(chkActive.Checked);

    }

    protected void chkSecSameAsPrimAddress_CheckedChanged(object sender, EventArgs e)
    {
        // Is the Secondary same as Primary checkbox checked?
        if (chkSecSameAsPrimAddress.Checked == true)
        {
            // Yes, copy contents of the primary fields to the secondary fields
            txtSecondaryAddress1.Text = txtPrimaryAddress1.Text;
            txtSecondaryAddress2.Text = txtPrimaryAddress2.Text;
            txtSecondaryCity.Text = txtPrimaryCity.Text;
            txtSecondaryPostalCode.Text = txtPrimaryPostalCode.Text;
            ddlSecondaryCountry.SelectedIndex = ddlPrimaryCountry.SelectedIndex;
            if (ddlSecondaryCountry.SelectedValue == "US")
            {
                ShowSecondaryStateList(true);
                ddlSecondaryState.SelectedText = ddlPrimaryState.SelectedText;
            }
            else
            {
                ShowSecondaryStateList(false);
            }
            
            txtSecondaryProvince.Text = txtPrimaryProvince.Text;
        }
    }

    protected void ddlPrimaryCountry_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        ShowPrimaryStateList(e.Value == "US" ? true : false);
    }

    protected void ddlSecondaryCountry_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        ShowSecondaryStateList(e.Value == "US" ? true : false);
    }

    protected void grdAssetList_ItemCommand(object sender, GridCommandEventArgs e)
    {
        // Was the grid row's Edit button clicked?
        if (e.CommandName == "EditAsset")
        {
            // Yes.  Get the item whose button was clicked
            GridDataItem item = (GridDataItem)e.Item;

            // Select the grid row
            item.Selected = true;

            // Change to "Edit" mode and clear the Create/Edit panel fields
            lblPageMode.Text = "Edit";
            btnCancelAsset.Text = "Cancel";
            ClearSelectedAssetFields();

            // Save the selected asset's ID in a hidden field
            hdnSelectedAssetID.Value = item["ID"].Text;

            // Get the selected asset's data
            DataTable dt = GetSelectedAssetFields();
            if (dt != null &&
                dt.Rows.Count > 0)
            {
                // Load the asset data into the Edit panel fields
                LoadSelectedAssetFields(dt);
            }
        }
    }

    protected void grdAssetList_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox chkActive = (CheckBox)item.FindControl("chkActive");

            // if the grouping is In-Active, gray out the row and make the text red
            if (chkActive != null
                && !chkActive.Checked)
            {
                // Set the row backgroun color to gray
                item.Style.Add("background-color", "#CECECE");

                // Set the text color of the checkbox's label to red
                Label lblActive = (Label)item.FindControl("lblActive");
                if (lblActive != null)
                {
                    lblActive.Style.Add("color", "Red");
                }
            }
        }

    }

    protected void grdAssetList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        grdAssetList.CurrentPageIndex = e.NewPageIndex;

    }

    protected void grdAssetList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        RefreshAssetList();
    }


    protected void lnkbtnPrimaryGatherGIS_Click(object sender, EventArgs e)
    {
        // Initialize the GIS return fields
        string latLong = string.Empty;
        string latLongAccuracy = string.Empty;

        // Format the address on which to gather GIS data
        string addressToCheck = txtPrimaryAddress1.Text
                              + " " + txtPrimaryAddress2.Text
                              + " " + txtPrimaryCity.Text
                              + ", " + ddlPrimaryState.SelectedValue
                              + " " + txtPrimaryPostalCode.Text
                              + " " + ddlPrimaryCountry.SelectedValue;

        // If the address is non-empty, call the get GIS function to find the latitude, longitude and accuracy.
        if (addressToCheck != String.Empty)
        {
            // Call the function that gets GIS data
            GetGISData(addressToCheck, out latLong, out latLongAccuracy);
            
            // If latitude and longitude were found, show the view map links
            if (latLong != string.Empty)
            {
                hypPrimaryMapLink.Visible = true;
                hypPrimaryMapLink.NavigateUrl = "http://maps.google.com/?q=" + latLong;
            }
        }

        // Return the latitude, longitude and accuracy found by the get GIS function
        txtPrimaryLatLong.Text = latLong;
        hdnPrimaryLatLongAccuracy.Value = latLongAccuracy;

    }

    #endregion

    #region Functions

    private void ClearSelectedAssetFields()
    {
        // Initialize all the fields on the Create/Edit panel

        hdnSelectedAssetID.Value = "0";

        chkActive.Checked = true;
        lblActive.Text = "Active";
        lblActive.Attributes.Remove("style");
        txtAssetName.Text = string.Empty;

        txtPrimaryAddress1.Text = string.Empty;
        txtPrimaryAddress2.Text = string.Empty;
        txtPrimaryCity.Text = string.Empty;
        txtPrimaryPostalCode.Text = string.Empty;
        ddlPrimaryCountry.SelectedValue = "US";
        ShowPrimaryStateList(true);
        ddlPrimaryState.SelectedIndex = -1; 
        txtPrimaryProvince.Text = string.Empty;
        txtPrimaryLatLong.Text = string.Empty;
        hdnPrimaryLatLongAccuracy.Value = string.Empty;
        hypPrimaryMapLink.Visible = false;
        hypPrimaryMapLink.NavigateUrl = string.Empty;
        txtPrimaryFirst.Text = string.Empty;
        txtPrimaryLast.Text = string.Empty;
        txtPrimaryPhone1.Text = string.Empty;
        txtPrimaryPhone2.Text = string.Empty;
        txtPrimaryEmail.Text = string.Empty;

        txtSecondaryAddress1.Text = string.Empty;
        txtSecondaryAddress2.Text = string.Empty;
        txtSecondaryCity.Text = string.Empty;
        txtSecondaryPostalCode.Text = string.Empty;
        ddlSecondaryCountry.SelectedValue = "US";
        ShowSecondaryStateList(true);
        ddlSecondaryState.SelectedIndex = -1;
        txtSecondaryProvince.Text = string.Empty;
        chkSecSameAsPrimAddress.Checked = false;
        txtSecondaryFirst.Text = string.Empty;
        txtSecondaryLast.Text = string.Empty;
        txtSecondaryPhone1.Text = string.Empty;
        txtSecondaryPhone2.Text = string.Empty;
        txtSecondaryEmail.Text = string.Empty;
    }

    private string GetAssetName(int assetType)
    {
        // Instantiate a database connection
        SqlConnection sqlConn = new SqlConnection(ConnectionString);

        // Get the name for the asset type from the database
        try
        {
            return SqlHelper.ExecuteScalar(sqlConn, CommandType.Text,
                                           "SELECT [clientAssetName] FROM [clientAssets] WHERE [clientAssetID] = " + assetType.ToString()).ToString();
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    private bool GetGISData(string address, out string latLong, out string latLongAccuracy)
    {
        // Initialize results
        latLong = string.Empty;
        latLongAccuracy = string.Empty;

        // Make a request to the Google API
        GeoRequest gr = new GeoRequest(address);
        GeoResponse gRes = gr.GetResponse();

        // If the request returned results, put them into the LatLong and LatLongAccuracy fields
        if (gRes.Results.Count > 0)
        {
            // Set the returned latLong value to the location found
            latLong = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();

            // Get the accuracy
            string resultLocationMatch = gRes.Results[0].Geometry.LocationType.ToString();

            // Add a translation of the accuracy to our values
            string resultAccuracy = "";
            switch (resultLocationMatch)
            {
                // See: http://stackoverflow.com/questions/3015370/how-to-get-the-equivalent-of-the-accuracy-in-google-map-geocoder-v3
                case "ROOFTOP":
                    resultAccuracy = "Precise";
                    break;
                case "RANGE_INTERPOLATED":
                    resultAccuracy = "Approximate";
                    break;
                case "GEOMETRIC_CENTER":
                    resultAccuracy = "Approximate";
                    break;
                case "APPROXIMATE":
                    resultAccuracy = "Approximate";
                    break;
                default:
                    resultAccuracy = resultLocationMatch;
                    break;
            }

            // Set the returned latLongAccuracy value to the translated accuracy string
            latLongAccuracy = gRes.Results[0].Types[0].ToString().ToUpper() + " (" + resultAccuracy + ")";

            // Return true to indicate success
            return true;
        }
        else
        {
            // Show and error message on the page and return false to indicate failure
            WriteMessage(lblMessage, "Could not gather GIS info with the location data supplied", false);
            return false;
        }

    }

    private string GetQueryStringAssetType()
    {
        // Initilize the field used to return the QueryString's "m=" value
        string strM = string.Empty;

        // If the query string has an "m" parameter, set strM to its value
        if (Request.QueryString["m"] != null)
        {
            strM = Request.QueryString["m"].ToString();
        }

        // Return the value
        return strM;
    }

    private DataTable GetSelectedAssetFields()
    {
        // Instantiate a DataTable for the resultset
        DataTable dt = new DataTable();

        // Instantiate a database connection
        SqlConnection sqlConn = new SqlConnection(ConnectionString);
        
        // Build a SELECT statement to get the fields for the selected asset
        string selectStmt = "SELECT [ID], [bitActive], [assetLabelID], [Name], [primFirstName], [primLastName], [primAddress1], [primAddress2], [primCity], [primState]"
                          + ",[primCountry], [primPostalCode], [primPhone], [primFax], [primEmail], [primLatLong], [primLatLongAccuracy]"
                          + ", [secFirstName], [secLastName], [secAddress1], [secAddress2], [secCity], [secState], [secCountry], [secPostalCode]"
                          + ", [secPhone], [secFax], [secEmail], [bitSecSamePrimAddress] FROM [assets] WHERE ([ID] = @ID)";
        try
        {
            // Open the database
            sqlConn.Open();

            using (SqlCommand sqlcmd = new SqlCommand(selectStmt, sqlConn))
            {
                // Add the @ID parameter for the SELECT statement
                sqlcmd.Parameters.AddWithValue("@ID", hdnSelectedAssetID.Value);

                // Get the asset's data
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            // Show an error message on the page
            WriteMessage(lblMessage, "Could not find the data for the selected asset in the database.", false);
        }
        finally
        {
            // Close the DB connection, if it's open
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        return dt;
    }

    private string GetStatusText(bool isActive)
    {
        // Set the Status text
        if (isActive)
        {
            return "Active";
        }
        else
        {
            return "In-Active";
        }
    }

    private bool InsertNewAsset()
    {
        // Instantiate a database connection
        SqlConnection sqlConn = new SqlConnection(ConnectionString);

        // Build and INSERT statement to create a new asset row from the fields entered by the user
        string insertStmt = "INSERT INTO assets([bitActive], [assetLabelID], [Name], [primFirstName], [primLastName], [primAddress1], [primAddress2], [primCity], [primState]"
                          + ", [primCountry], [primPostalCode], [primPhone], [primFax], [primEmail], [primLatLong], [primLatLongAccuracy]"
                          + ", [secFirstName], [secLastName], [secAddress1], [secAddress2], [secCity], [secState], [secCountry], [secPostalCode]"
                          + ", [secPhone], [secFax], [secEmail], [bitSecSamePrimAddress]) "
                          + "VALUES (1, @AssetCategoryID, @Name, @PrimFirstname, @PrimLastName, @PrimAddress1, @PrimAddress2, @PrimCity, @PrimState"
                          + ", @PrimCountry, @PrimPostalCode, @PrimPhone, @PrimFax, @PrimEmail, @PrimLatLong, @PrimLatLongAccuracy"
                          + ", @SecFirstName, @SecLastName, @SecAddress1, @SecAddress2, @SecCity, @SecState, @SecCountry, @SecPostalCode"
                          + ", @SecPhone, @SecFax, @SecEmail, @SecSamePrimAddress)";
        try
        {
            // Open the DB connection
            sqlConn.Open();

            using (SqlCommand sqlCmd = new SqlCommand(insertStmt, sqlConn))
            {
                // Add parameters to supply the user entered data to the INSERT statement
                sqlCmd.Parameters.AddWithValue("@AssetCategoryID", AssetType);
                sqlCmd.Parameters.AddWithValue("@Name", txtAssetName.Text);
                sqlCmd.Parameters.AddWithValue("@PrimFirstName", txtPrimaryFirst.Text);
                sqlCmd.Parameters.AddWithValue("@PrimLastName", txtPrimaryLast.Text);
                sqlCmd.Parameters.AddWithValue("@PrimAddress1", txtPrimaryAddress1.Text);
                sqlCmd.Parameters.AddWithValue("@PrimAddress2", txtPrimaryAddress2.Text);
                sqlCmd.Parameters.AddWithValue("@PrimCity", txtPrimaryCity.Text);

                sqlCmd.Parameters.AddWithValue("@PrimCountry", ddlPrimaryCountry.SelectedValue);
                if (ddlPrimaryCountry.SelectedValue == "US")
                {
                    sqlCmd.Parameters.AddWithValue("@PrimState", ddlPrimaryState.SelectedValue);
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PrimState", txtPrimaryProvince.Text);
                }

                sqlCmd.Parameters.AddWithValue("@PrimPostalCode", txtPrimaryPostalCode.Text);
                sqlCmd.Parameters.AddWithValue("@PrimPhone", txtPrimaryPhone1.Text);
                sqlCmd.Parameters.AddWithValue("@PrimFax", txtPrimaryPhone2.Text);
                sqlCmd.Parameters.AddWithValue("@PrimEmail", txtPrimaryEmail.Text);
                sqlCmd.Parameters.AddWithValue("@PrimLatLong", txtPrimaryLatLong.Text);
                sqlCmd.Parameters.AddWithValue("@PrimLatLongAccuracy", hdnPrimaryLatLongAccuracy.Value);

                sqlCmd.Parameters.AddWithValue("@SecFirstName", txtSecondaryFirst.Text);
                sqlCmd.Parameters.AddWithValue("@SecLastName", txtSecondaryLast.Text);
                sqlCmd.Parameters.AddWithValue("@SecAddress1", txtSecondaryAddress1.Text);
                sqlCmd.Parameters.AddWithValue("@SecAddress2", txtSecondaryAddress2.Text);
                sqlCmd.Parameters.AddWithValue("@SecCity", txtSecondaryCity.Text);

                sqlCmd.Parameters.AddWithValue("@SecCountry", ddlSecondaryCountry.SelectedValue);
                if (ddlSecondaryCountry.SelectedValue == "US")
                {
                    sqlCmd.Parameters.AddWithValue("@SecState", ddlSecondaryState.SelectedValue);
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@SecState", txtSecondaryProvince.Text);
                }

                sqlCmd.Parameters.AddWithValue("@SecPostalCode", txtSecondaryPostalCode.Text);
                sqlCmd.Parameters.AddWithValue("@SecPhone", txtSecondaryPhone1.Text);
                sqlCmd.Parameters.AddWithValue("@SecFax", txtSecondaryPhone2.Text);
                sqlCmd.Parameters.AddWithValue("@SecEmail", txtSecondaryEmail.Text);
                sqlCmd.Parameters.AddWithValue("@SecSamePrimAddress", chkSecSameAsPrimAddress.Checked);

                // Execute the INSERT and return tru if it succeeds
                int rtnCode = sqlCmd.ExecuteNonQuery();
                if (rtnCode > 0)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            WriteMessage(lblMessage, "Could not create the asset.  A database error occurred.", false);
            return false;
        }
        finally
        {
            // Close the DB connection, if its open
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        WriteMessage(lblMessage, "Could not create the asset.", false);
        return false;
    }

    private void LoadSelectedAssetFields(DataTable dt)
    {
        // Load the fields from the selected asset into the Create/Edit panel's fields
        chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["bitActive"]);
        SetStatusAttributes(chkActive.Checked);
        txtAssetName.Text = dt.Rows[0]["name"].ToString();

        txtPrimaryAddress1.Text = dt.Rows[0]["primAddress1"].ToString();
        txtPrimaryAddress2.Text = dt.Rows[0]["primAddress2"].ToString();
        txtPrimaryCity.Text = dt.Rows[0]["primCity"].ToString();

        string countryCode;
        if (!Convert.IsDBNull(dt.Rows[0]["primCountry"]))
        {
            countryCode = dt.Rows[0]["primCountry"].ToString();
        }
        else
        {
            countryCode = "US";
        }

        SetRadDDLSelectedValue(ddlPrimaryCountry, countryCode);

        if (countryCode == "US")
        {
            ShowPrimaryStateList(true);
            SetRadDDLSelectedValue(ddlPrimaryState, dt.Rows[0]["primState"].ToString());
        }
        else
        {
            ShowPrimaryStateList(false);
            txtPrimaryProvince.Text = dt.Rows[0]["primState"].ToString();
        }

        txtPrimaryPostalCode.Text = dt.Rows[0]["primPostalCode"].ToString();

        txtPrimaryLatLong.Text = dt.Rows[0]["primLatLong"].ToString();
        if (txtPrimaryLatLong.Text != string.Empty)
        {
            hypPrimaryMapLink.Visible = true;
            hypPrimaryMapLink.NavigateUrl = "http://maps.google.com/?q=" + txtPrimaryLatLong.Text;
        }

        hdnPrimaryLatLongAccuracy.Value = dt.Rows[0]["primLatLongAccuracy"].ToString();

        txtPrimaryFirst.Text = dt.Rows[0]["primFirstName"].ToString();
        txtPrimaryLast.Text = dt.Rows[0]["primLastName"].ToString();
        txtPrimaryPhone1.Text = dt.Rows[0]["primPhone"].ToString();
        txtPrimaryPhone2.Text = dt.Rows[0]["primFax"].ToString();
        txtPrimaryEmail.Text = dt.Rows[0]["primEmail"].ToString();

        txtSecondaryAddress1.Text = dt.Rows[0]["secAddress1"].ToString();
        txtSecondaryAddress2.Text = dt.Rows[0]["secAddress2"].ToString();
        txtSecondaryCity.Text = dt.Rows[0]["secCity"].ToString();

        if (!Convert.IsDBNull(dt.Rows[0]["secCountry"]))
        {
            countryCode = dt.Rows[0]["secCountry"].ToString();
        }
        else
        {
            countryCode = "US";
        }

        SetRadDDLSelectedValue(ddlSecondaryCountry, countryCode);

        if (countryCode == "US")
        {
            ShowSecondaryStateList(true);
            SetRadDDLSelectedValue(ddlSecondaryState, dt.Rows[0]["secState"].ToString());
        }
        else
        {
            ShowSecondaryStateList(false);
            txtSecondaryProvince.Text = dt.Rows[0]["secState"].ToString();
        }

        if (!Convert.IsDBNull(dt.Rows[0]["secCountry"]))
        {
            SetRadDDLSelectedValue(ddlSecondaryCountry, dt.Rows[0]["secCountry"].ToString());
        }

        txtSecondaryPostalCode.Text = dt.Rows[0]["secPostalCode"].ToString();
        txtSecondaryFirst.Text = dt.Rows[0]["secFirstName"].ToString();
        txtSecondaryLast.Text = dt.Rows[0]["secLastName"].ToString();
        txtSecondaryPhone1.Text = dt.Rows[0]["secPhone"].ToString();
        txtSecondaryPhone2.Text = dt.Rows[0]["secFax"].ToString();
        txtSecondaryEmail.Text = dt.Rows[0]["secEmail"].ToString();

        if (!Convert.IsDBNull(dt.Rows[0]["bitSecSamePrimAddress"]))
        {
            chkSecSameAsPrimAddress.Checked = Convert.ToBoolean(dt.Rows[0]["bitSecSamePrimAddress"]);
        }

    }

    private void PopupNotify(string msg)
    {
        // Popup a window to show the user the passed message
        radnotMessage.Title = "Manage " + AssetName + " Notice";
        radnotMessage.Text = msg;
        radnotMessage.Show();
    }

    private void RegisterJavaScript()
    {
        // Add JavaScript functions to the page
        StringBuilder script = new StringBuilder();

        // Build a script to popup a confirm dialog in response to a RadButton's onclick event
        script.Append("<script type=\"text/javascript\">" + Environment.NewLine);
        script.Append(" function ConfirmSave(button, args)" + Environment.NewLine);
        script.Append(" {" + Environment.NewLine);
        script.Append("     if (!Page_IsValid)" + Environment.NewLine);
        script.Append("     {" + Environment.NewLine);
        script.Append("         button.set_autoPostBack(false);" + Environment.NewLine);
        script.Append("     }" + Environment.NewLine);
        script.Append("     else " + Environment.NewLine);
        script.Append("     {" + Environment.NewLine);
        script.Append("         if(!confirm('Click \"OK\" to save your changes'))" + Environment.NewLine);
        script.Append("         {" + Environment.NewLine);
        script.Append("             button.set_autoPostBack(false);" + Environment.NewLine);
        script.Append("         }" + Environment.NewLine);
        script.Append("         else " + Environment.NewLine);
        script.Append("         {" + Environment.NewLine);
        script.Append("             button.set_autoPostBack(true);" + Environment.NewLine);
        script.Append("         }" + Environment.NewLine);
        script.Append("     }" + Environment.NewLine);
        script.Append(" }" + Environment.NewLine);
        script.Append(" </script>");

        // Get the type of this module
        Type csType = this.GetType();

        // Get a ClientScriptManager reference from the Page class.
        ClientScriptManager cs = Page.ClientScript;

        // Check to see if the client script is already registered.
        if (!cs.IsClientScriptBlockRegistered(csType, "ConfirmSave"))
        {
            // Register the script
            cs.RegisterClientScriptBlock(csType, "ConfirSave", script.ToString());
        }

    }

    private void RefreshAssetList()
    {
        // Instantiate a DataTable for the resultset
        DataTable dt = new DataTable();

        // Instantiate a database connection
        SqlConnection sqlConn = new SqlConnection(ConnectionString);

        // Build a SELECT statement to get the lists of assets for display in the grid
        string selectStmt = "SELECT ast.[id], IsNull(ast.[bitActive], 'false') AS [bitActive], ast.[assetLabelID], ast.[name]"
                          + ", ast.[primFirstName], ast.[primLastName], ast.[primAddress1], ast.[primAddress2], ast.[primCity]"
                          + ", CASE IsNull(ast.[primCountry], 'US') WHEN 'US' THEN stat.[name] ELSE ast.[primState] END AS [primStateName] "
                          + ", CASE IsNull(ast.[primCountry], 'US') WHEN 'US' THEN 'United States' ELSE ctry.[name] END AS [primCountryName]"
                          + ", [primPostalCode], [primPhone], [primFax], [primLatLong]"
                          + " FROM [assets] AS ast"
                          + " LEFT OUTER JOIN [countryCode] AS ctry ON ctry.[code] = ast.[primCountry]"
                          + " LEFT OUTER JOIN [stateCode] AS stat ON stat.[code] = ast.[primState]"
                          + " WHERE ast.[assetLabelId] = @AssetType";
        try
        {
            // Open the DB connection
            sqlConn.Open();

            using (SqlCommand sqlcmd = new SqlCommand(selectStmt, sqlConn))
            {
                // Add parameter definitions to the SELECT
                sqlcmd.Parameters.AddWithValue("@AssetType", AssetType);

                // Execute the SELECT statement and fill a DataTable with its resultset
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            // Close the DB connection, it its open
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        // Return the DataTable
        grdAssetList.DataSource = dt;

    }

    private void RefreshCountryCodeLists()
    {
        // Refresh the Primary and Secondary Country dropdownlists
        ddlPrimaryCountry.DataBind();
        SetRadDDLSelectedValue(ddlPrimaryCountry, "US");

        ddlSecondaryCountry.DataBind();
        SetRadDDLSelectedValue(ddlSecondaryCountry, "US");
    }

    private void RefreshStateCodeLists()
    {
        // Refresh the primary and secondary State dropdownlists
        ddlPrimaryState.DataBind();
        ddlPrimaryState.SelectedIndex = -1;

        ddlSecondaryState.DataBind();
        ddlSecondaryState.SelectedIndex = -1;
    }

    private void SetRadDDLSelectedValue(RadDropDownList ddl, string selectValue)
    {
        // Set the selectedvalue of the specified RadDropDownList to the value passed
        // using Try/Catch to avoid application crashes if the value isn't in the list
        try
        {
            if (ddl.Items.Count > 0)
            {
                ddl.SelectedValue = selectValue;
                if (ddl.SelectedIndex < 0)
                {
                    ddl.SelectedIndex = -1;
                }
            }

        }
        catch (Exception ex)
        {
        }
    }

    private void SetStatusAttributes(bool isActive)
    {
        // Set the color of the lblActive field based on the passed boolean value
        lblActive.Text = GetStatusText(isActive);

        if (!isActive)
        {
            lblActive.Attributes.Add("style", "color: red;");
        }
        else
        {
            lblActive.Attributes.Remove("style");
        }
    }

    private void ShowPrimaryStateList(bool showDropDownList)
    {
        // Show either the Primary State dropdownlist or its textbox, based on the passed boolean value
        pnlPrimaryUS.Visible = showDropDownList;
        pnlPrimaryNonUS.Visible = !showDropDownList;
    }

    private void ShowSecondaryStateList(bool showDropDownList)
    {
        // Show either the Secondary State dropdownlist or its textbox, based on the passed boolean value
        pnlSecondaryUS.Visible = showDropDownList;
        pnlSecondaryNonUS.Visible = !showDropDownList;
    }

    private bool UpdateSelectedAsset()
    {
        // Instantiate a database connection
        SqlConnection sqlConn = new SqlConnection(ConnectionString);

        // Build an UPDATE statement to update the selected asset with the values entered by the user
        string updateStmt = "UPDATE assets SET [bitActive] = @Active, [Name] = @Name, [primFirstName] = @PrimFirstName, [primLastName] = @PrimLastName"
                          + ", [primAddress1] = @PrimAddress1, [primAddress2] = @PrimAddress2, [primCity] = @PrimCity, [primState] = @PrimState"
                          + ", [primCountry] = @PrimCountry, [primPostalCode] = @PrimPostalCode, [primPhone] = @PrimPhone, [primFax] = @PrimFax"
                          + ", [primEmail] = @PrimEmail, [primLatLong] = @PrimLatLong, [primLatLongAccuracy] = @PrimLatLongAccuracy"
                          + ", [secFirstName] = @SecFirstName, [secLastName] = @SecLastName, [secAddress1] = @SecAddress1, [secAddress2] = @SecAddress2"
                          + ", [secCity] = @SecCity, [secState] = @SecState, [secCountry] = @SecCountry, [secPostalCode] = @SecPostalCode"
                          + ", [secPhone] = @SecPhone, [secFax] = @SecFax, [secEmail] = @SecEmail, [bitSecSamePrimAddress] = @SecSamePrimAddress"
                          + " WHERE (id = @ID)";

        try
        {
            // Open the DB connection
            sqlConn.Open();

            using (SqlCommand sqlCmd = new SqlCommand(updateStmt, sqlConn))
            {
                // Add parameters with the user entered data to the UPDATE statement
                sqlCmd.Parameters.AddWithValue("@ID", hdnSelectedAssetID.Value);
                sqlCmd.Parameters.AddWithValue("@Active", chkActive.Checked);
                sqlCmd.Parameters.AddWithValue("@Name", txtAssetName.Text);
                sqlCmd.Parameters.AddWithValue("@PrimFirstName", txtPrimaryFirst.Text);
                sqlCmd.Parameters.AddWithValue("@PrimLastName", txtPrimaryLast.Text);
                sqlCmd.Parameters.AddWithValue("@PrimAddress1", txtPrimaryAddress1.Text);
                sqlCmd.Parameters.AddWithValue("@PrimAddress2", txtPrimaryAddress2.Text);
                sqlCmd.Parameters.AddWithValue("@PrimCity", txtPrimaryCity.Text);
                sqlCmd.Parameters.AddWithValue("@PrimCountry", ddlPrimaryCountry.SelectedValue);
                if (ddlPrimaryCountry.SelectedValue == "US")
                {
                    sqlCmd.Parameters.AddWithValue("@PrimState", ddlPrimaryState.SelectedValue);
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PrimState", txtPrimaryProvince.Text);
                }

                sqlCmd.Parameters.AddWithValue("@PrimPostalCode", txtPrimaryPostalCode.Text);
                sqlCmd.Parameters.AddWithValue("@PrimPhone", txtPrimaryPhone1.Text);
                sqlCmd.Parameters.AddWithValue("@PrimFax", txtPrimaryPhone2.Text);
                sqlCmd.Parameters.AddWithValue("@PrimEmail", txtPrimaryEmail.Text);
                sqlCmd.Parameters.AddWithValue("@PrimLatLong", txtPrimaryLatLong.Text);
                sqlCmd.Parameters.AddWithValue("@PrimLatLongAccuracy", hdnPrimaryLatLongAccuracy.Value);

                sqlCmd.Parameters.AddWithValue("@SecFirstName", txtSecondaryFirst.Text);
                sqlCmd.Parameters.AddWithValue("@SecLastName", txtSecondaryLast.Text);
                sqlCmd.Parameters.AddWithValue("@SecAddress1", txtSecondaryAddress1.Text);
                sqlCmd.Parameters.AddWithValue("@SecAddress2", txtSecondaryAddress2.Text);
                sqlCmd.Parameters.AddWithValue("@SecCity", txtSecondaryCity.Text);

                sqlCmd.Parameters.AddWithValue("@SecCountry", ddlSecondaryCountry.SelectedValue);
                if (ddlSecondaryCountry.SelectedValue == "US")
                {
                    sqlCmd.Parameters.AddWithValue("@SecState", ddlSecondaryState.SelectedValue);
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@SecState", txtSecondaryProvince.Text);
                }

                sqlCmd.Parameters.AddWithValue("@SecPostalCode", txtSecondaryPostalCode.Text);
                sqlCmd.Parameters.AddWithValue("@SecPhone", txtSecondaryPhone1.Text);
                sqlCmd.Parameters.AddWithValue("@SecFax", txtSecondaryPhone2.Text);
                sqlCmd.Parameters.AddWithValue("@SecEmail", txtSecondaryEmail.Text);
                sqlCmd.Parameters.AddWithValue("@SecSamePrimAddress", chkSecSameAsPrimAddress.Checked);

                // Execute the UPDATE and return true if it succeeds
                int rtnCode = sqlCmd.ExecuteNonQuery();
                if (rtnCode > 0)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            // Show an error message on the page and return false to indicate failure
            WriteMessage(lblMessage, "Could not update the selected asset.  A database error occurred.", false);
            return false;
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        // Show an error message on the page and return false to indicate failure
        WriteMessage(lblMessage, "Could not update the selected asset.", false);
        return false;
    }

    private bool ValidEditAsset()
    {
        // Is the user entered data valid?
        Page.Validate();
        if (!Page.IsValid)
        {
            // No, exit returning false to indicate failure
            return false;
        }

        // Instantiate a database connection
        SqlConnection sqlConn = new SqlConnection(ConnectionString);

        // Initialise the return value, assume success (true)
        bool rtnValue = true;
        try
        {
            // Open the DB connection
            sqlConn.Open();

            // Build a SELECT statement to see if the asset name already exists
            using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM assets WHERE [name] = @Name AND [assetLabelID] = @AssetCategoryID AND [ID] <> @ID", sqlConn))
            {
                // Add parameters with values to the SELECT
                sqlCmd.Parameters.AddWithValue("@ID", hdnSelectedAssetID.Value);
                sqlCmd.Parameters.AddWithValue("@AssetCategoryID", AssetType);
                sqlCmd.Parameters.AddWithValue("@Name", txtAssetName.Text);

                // Instantiate an integer variable to hold the returned count and run the SELECT Statement to get its value
                int rtn = 0;
                rtn = (Int32)sqlCmd.ExecuteScalar();

                // If the meter IRN already exists, show an error message and set the return value to 'false' (not valid)
                if (rtn != 0)
                {
                    WriteMessage(lblMessage, "This asset name already exists", false);
                    rtnValue = false;
                }
            }
        }

        catch (Exception ex)
        {
            WriteMessage(lblMessage, "Could not validate the asset name.  A database error ocurred.", false);
            rtnValue = false;
        }
        finally
        {
            // Close the database connection
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        // Return the rtnValue (true or false)
        return rtnValue;
    }

    private bool ValidNewAsset()
    {
        // Is the user entered data valid?
        Page.Validate();
        if (!Page.IsValid)
        {
            // No, exit returning false to indicate failure
            return false;
        }

        // Instantiate a database connection
        SqlConnection sqlConn = new SqlConnection(ConnectionString);

        // Initialise the return value, assume success (true)
        bool rtnValue = true;
        try
        {
            // Open a the database connection
            sqlConn.Open();

            // Build a SELECT statement to see if the asset name already exists
                using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM assets WHERE [assetLabelID] = @AssetCategoryID AND [name] = @Name", sqlConn))
                {
                    // Add parameters with values to the SELECT
                    sqlCmd.Parameters.AddWithValue("@AssetCategoryID", AssetType);
                    sqlCmd.Parameters.AddWithValue("@Name", txtAssetName.Text);

                    // Instantiate an integer variable to hold the returned count and run the SELECT Statement to get its value
                    int rtn = 0;
                    rtn = (Int32)sqlCmd.ExecuteScalar();

                    // If the asset name already exists, show an error message and set the return value to 'false' (not valid)
                    if (rtn != 0)
                    {
                        WriteMessage(lblMessage, "This asset name already exists", false);
                        rtnValue = false;
                    }
                }
        }
        catch (Exception ex)
        {
            // Show an error message on the page and set the return value to false to indicate failure
            WriteMessage(lblMessage, "Could not validate the asset name.  A database error ocurred.", false);
            rtnValue = false;
        }
        finally
        {
            // Close the database connection
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        // Return the rtnValue (true or false)
        return rtnValue;
    }

    private void WriteMessage(Label lbl, string msg, bool success)
    {
        // Put the passed msg text in the page's error message field, appending a <br /> first if the field already has a message in it.
        if (lbl.Text == String.Empty)
        {
            lbl.Text = msg;
        }
        else
        {
            lbl.Text += "<br />" + msg;
        }

        // Set the font color, size and weight of the message based on the passed success flag
        if (success)
        {
            lbl.Attributes.Add("Style", "color: Green; font-size: 10pt; font-weight: bold;");
        }
        else
        {
            lbl.Attributes.Add("Style", "color: Red; font-size: 10pt; font-weight: bold;");

        }
    }

    #endregion
}