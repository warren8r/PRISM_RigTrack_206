using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;

public partial class Modules_Configuration_Manager_confMngrSetAttributeInterval : System.Web.UI.Page
{
    int refreshLevels = 0;
    
    enum MessageTypeEnum
    { 
        Success, Error
    }

    #region Properties

    public string DBConnectionString
    {
        get { return Session["client_database"].ToString(); }
    }

    #endregion

    #region Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        // Blank out the message field 
        lblMessage.Text = string.Empty;

        // Add javascript functions to the page
        RegisterJavaScript();
        
        if (!Page.IsPostBack)
        {
            RefreshAssetCategoryList();
            RefreshAttributeList();
            ddlIntervalList.SelectedIndex = 0;
        }
    }

    //protected void chkEditActive_CheckChanged(object sender, EventArgs e)
    //{
    //    // Set the text and color of the label next to the Edit Status CheckBox to reflect its Checked state
    //    SetStatusAttributes(chkEditActive.Checked);
    //}

    protected void ddlAssetCategoryList_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        // Increment the refresh level count
        refreshLevels = 1;

        // Refresh the Assets lits
        RefreshAssetList();

        // Reset the refesh level count
        refreshLevels = 0;

        // Show the attribute data intervals
        grdAssetAttributes.Rebind();
        
    }

    protected void ddlAssetList_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        // Increment the refresh level count
        refreshLevels++;

        // Refresh the Attributes list to reflect the newly selected Asset
        RefreshAttributeList();

        // Decrement the refresh level count
        refreshLevels--;

        // If not doing a higher level refresh, rebind the Attribute Data Interval grid
        if (refreshLevels < 1)
        {
            grdAssetAttributes.Rebind();
        }
    }

    //protected void ddlAttributeList_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    //{
    //    // If not doing a higher level refresh, rebind the Attribute Data Interval grid
    //    if (refreshLevels < 1)
    //    {
    //        grdAssetAttributes.Rebind();
    //    }
    //}

    protected void cbxAttriubteList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        // If not doing a higher level refresh, rebind the Attribute Data Interval grid
        if (refreshLevels < 1)
        {
            grdAssetAttributes.Rebind();
        }
    }

    protected void lnkNewAttribute_Click(object sender, EventArgs e)
    {
        //if (ddlAssetList.SelectedValue == string.Empty)
        //{
        //    WriteMessage("You must select an Asset first", false);
        //}
        //else
        //{
        //    pnlNewAttribute.Visible = true;
        //}

        pnlNewAttribute.Visible = true;
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        // Reset the criteria lists and refresh the grid
        refreshLevels = 1;
        RefreshAssetCategoryList();
        refreshLevels = 0;
        RefreshAttributeList();
        grdAssetAttributes.Rebind();
    }

    protected void btnSaveAssetAttribute_Click(object sender, EventArgs e)
    {
        // Code to save the asset attriute entered in the NewAssetAttribute panel
        if (ValidNewAssetAttribute())
        {
            if (InsertNewAssetAttribute())
            {
                // Refresh the list of Attributes and the Asset Attribute grid to show the new one
                RefreshAttributeList();
                grdAssetAttributes.Rebind();

                // Notify the user
                WriteMessage("New attribute was added to the asset", true);
                PopupNotify("New attribute was added to the asset");
            }
        }
    }

    protected void btnSaveNewAttribute_Click(object sender, EventArgs e)
    {
        // Code to save the asset attriute entered in the NewAssetAttribute panel
        if (ValidNewAttribute())
        {
            if (InsertNewAttribute())
            {
                // Clear the new attribute panel fields
                ClearNewFields();

                // Refresh the list of Attributes and the Attribute data interval grid to show the new one
                RefreshAttributeList();
                grdAssetAttributes.Rebind();

                // Notify the user
                WriteMessage("Attribute data interval was created", true);
                PopupNotify("Attribute data invterval was created");
            }
        }

        pnlNewAttribute.Visible = false;
    }

    protected void btnCancelNewAttribute_Click(object sender, EventArgs e)
    {
        pnlNewAttribute.Visible = false;
        ClearNewFields();
    }

    protected void btnCancelEditAttribute_Click(object sender, EventArgs e)
    {
        hdnEditAttributeID.Value = "0";
        grdAssetAttributes.SelectedIndexes.Clear();
        ClearEditAttributeFields();
        pnlEditAttribute.Visible = false;
        pnlTopHalf.Enabled = true;
    }

    protected void btnSaveEditAttribute_Click(object sender, EventArgs e)
    {
        if (UpdateEditAttribute())
        {
            grdAssetAttributes.SelectedIndexes.Clear();
            hdnEditAttributeID.Value = "0";
            grdAssetAttributes.Rebind();

            // Notify the user
            WriteMessage("Attribute data interval changes were saved", true);
            PopupNotify("Attribute data interval changes were saved");
        }

        ClearEditAttributeFields();
        pnlEditAttribute.Visible = false;
        pnlTopHalf.Enabled = true;
    }

    //protected void btnViewFilter_Click(object sender, EventArgs e)
    //{
    //    grdAssetAttributes.Rebind();
    //}

    protected void grdAssetAttributes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        RefreshAssetAttributesDataSource();
    }

    protected void grdAssetAttributes_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "EditAttribute")
        {
            GridDataItem item = (GridDataItem)e.Item;
            item.Selected = true;
            hdnEditAttributeID.Value = item.GetDataKeyValue("attributeID").ToString();
            ClearEditAttributeFields();
            DataTable dt = GetEditAttributeFields();
            if (dt != null &&
                dt.Rows.Count > 0)
            {
                LoadEditAttributeFields(dt);
            }

            pnlEditAttribute.Visible = true;
            pnlTopHalf.Enabled = false;
        }
    }

    protected void grdAssetAttributes_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //if (e.Item is GridDataItem)
        //{
        //    GridDataItem item = (GridDataItem)e.Item;
        //    CheckBox chkActive = (CheckBox)item.FindControl("chkActive");

        //    // if the grouping is In-Active, gray out the row and make the text red
        //    if (chkActive != null
        //        && !chkActive.Checked)
        //    {
        //        // Set the row backgroun color to gray
        //        item.Style.Add("background-color", "#CECECE");

        //        // Set the text color of the checkbox's label to red
        //        Label lblActive = (Label)item.FindControl("lblActive");
        //        if (lblActive != null)
        //        {
        //            lblActive.Style.Add("color", "Red");
        //        }
        //    }
        //}

    }

    protected void grdAssetAttributes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        grdAssetAttributes.CurrentPageIndex = e.NewPageIndex;
    }

    //protected void grdAssetAttributes_CheckChanged(object sender, EventArgs e)
    //{
    //    GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
    //    CheckBox chk = (CheckBox)sender;
    //    //string strID = row["attributeID"].Text;
    //    string strID = row.GetDataKeyValue("attributeID").ToString();

    //    sqlUpdAttributeStatus.UpdateParameters["ID"].DefaultValue = strID;
    //    sqlUpdAttributeStatus.UpdateParameters["bitActive"].DefaultValue = chk.Checked.ToString();
    //    sqlUpdAttributeStatus.Update();

    //    string msgText = "Attribute data interval status changed to " + (chk.Checked ? "Active" : "Inactive");
    //    WriteMessage(msgText, true);
    //    PopupNotify(msgText);

    //    grdAssetAttributes.Rebind();

    //}

    #endregion

    #region Functions

    // Add JavaScript functions to the page
    private void RegisterJavaScript()
    {
        // Check to see if the client script is already registered.
        StringBuilder script = new StringBuilder();
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

    // Reset functions for New mode dropdown lists
    private void RefreshAssetCategoryList()
    {
        ddlAssetCategoryList.DataBind();
        ddlAssetCategoryList.SelectedIndex = 0;

        RefreshAssetList();
    }

    private void RefreshAssetList()
    {
        ddlAssetList.DataBind();
        ddlAssetList.SelectedIndex = -1;
    }

    private void RefreshAttributeList()
    {
        cbxAttributeList.Items.Clear();
        cbxAttributeList.Items.Add(new RadComboBoxItem("-Select-", "0"));
        cbxAttributeList.DataBind();
        cbxAttributeList.SelectedIndex = 0;
    }

    private void RefreshAssetAttributesDataSource()
    {
        // Instantiate a connection to the database using the DBConnectionString
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // Instantiate a SqlCommand object for the SELECT statement
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.Connection = sqlConn;
        sqlCmd.CommandType = CommandType.Text;

        // Build a SQL SELECT statement to retrieve the Asset Attributes using the criteria values entered, if any.
        // First add the resultset columns
        string selectStatement = string.Empty;
        string resultCols = "SELECT cat.[clientAssetID] AS [clientAssetID], cat.[clientAssetName] AS [clientAssetName]"
                          + ", ast.[ID] AS [assetID], ast.[name] AS [assetName], aatr.[ID] AS [assetAttributeID], atrb.[ID] AS [attributeID]"
                          + ", atrb.[name] AS [attributeName], lklti.[itemValue] AS [intervalValue]"
                          + ", atrb.[upperControlLimit] AS [attributeUpperControlLimit], atrb.[lowerControlLimit] AS [attributeLowerControlLimit]"
                          + " FROM [attributes] AS atrb"
                          + " JOIN [assetsAttributes] AS aatr ON aatr.[attributeID] = atrb.[ID]"
                          + " LEFT OUTER JOIN [assets] AS ast ON ast.[ID] = aatr.[assetID]"
                          + " LEFT OUTER JOIN [clientAssets] AS cat ON cat.[clientAssetID] = ast.[assetLabelID]"
                          + " LEFT OUTER JOIN [lookupList] AS lklti ON lklti.[itemKey] = atrb.[interval] AND lklti.[listName] = 'TimeInterval'";

        // Add the ORDER BY clause
        string orderBy = " ORDER BY cat.[clientAssetName], ast.[name], atrb.[name], atrb.[interval]";

        // Instantiate a StringBuilder to hold the WHERE clause, if any
        StringBuilder criteria = new StringBuilder();
        string and = string.Empty;
        string where = " WHERE";

        // If an Asset Category filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (ddlAssetCategoryList.SelectedValue != string.Empty)
        {
            criteria.Append(where + "(cat.[clientAssetID] = @AssetCategoryID)");
            sqlCmd.Parameters.Add(new SqlParameter("AssetCategoryID", DbType.Int32));
            sqlCmd.Parameters["AssetCategoryID"].Value = Convert.ToInt32(ddlAssetCategoryList.SelectedValue);
            where = string.Empty;
            and = " AND ";
        }

        // If an Asset filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (ddlAssetList.SelectedValue != string.Empty)
        {
            criteria.Append(where + and + "(ast.[ID] = @AssetID)");
            sqlCmd.Parameters.Add(new SqlParameter("AssetID", DbType.Int32));
            sqlCmd.Parameters["AssetID"].Value = Convert.ToInt32(ddlAssetList.SelectedValue);
            where = string.Empty;
            and = " AND ";
        }

        // If an Attribute filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (cbxAttributeList.SelectedValue != string.Empty
            && cbxAttributeList.SelectedValue != "0")
        {
            criteria.Append(where + and + "(atrb.[ID] = @AttributeID)");
            sqlCmd.Parameters.Add(new SqlParameter("AttributeID", DbType.Int32));
            sqlCmd.Parameters["AttributeID"].Value = Convert.ToInt32(cbxAttributeList.SelectedValue);
            where = string.Empty;
            and = " AND ";
        }

        // Build the SELECT statement including the WHERE clause, if needed, and the ORDER BY clause
        if (criteria.Length > 0)
        {
            selectStatement = resultCols + criteria.ToString() + orderBy;
        }
        else
        {
            selectStatement = resultCols + orderBy;
        }

        // Put the SELECT statement into the SQL Command object
        sqlCmd.CommandText = selectStatement;

        // Instatiate a DataAdapter and add the SQL Command object asits SELECT command
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlCmd;

        // Instantiate a DataTable to hold the resultset from the SELECT execution
        DataTable dt = new DataTable();

        try
        {
            // Open the database connection
            sqlConn.Open();

            // Fill the DataTable with the SELECT results
            da.Fill(dt);
        }
        finally
        {
            // Close the connection.
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close(); 
            }
        }

        // Set the All Groupings grid's datasource to the DataTable
        grdAssetAttributes.DataSource = dt;

    }

    private void SetDDLSelectedValue(RadDropDownList ddl, string selectValue)
    {
        try
        {
            if (ddl.Items.Count > 0)
            {
                ddl.SelectedValue = selectValue;
            }
        }
        catch (Exception ex)
        {
        }
    }

    //private void SetStatusAttributes(bool isActive)
    //{
    //    lblEditActive.Text = GetStatusText(isActive);

    //    if (!isActive)
    //    {
    //        lblEditActive.Attributes.Add("style", "color: red;");
    //    }
    //    else
    //    {
    //        lblEditActive.Attributes.Remove("style");
    //    }
    //}

    private void ClearNewFields()
    {
        // Clear common Create/Edit fields
        txtAttributeName.Text = string.Empty;
        txtAttributeDescription.Text = string.Empty;
        SetDDLSelectedValue(ddlIntervalList, string.Empty);
        txtUpperLimit.Text = string.Empty;
        txtLowerLimit.Text = string.Empty;
    }


    private void ClearEditAttributeFields()
    {
        //chkEditActive.Checked = true;
        //SetStatusAttributes(true);
        //txtEditAssetCategory.Text = string.Empty;
        txtEditAttribute.Text = string.Empty;
        ddlEditIntervalList.SelectedIndex = 0;
        txtEditUpperLimit.Text = string.Empty;
        txtEditLowerLimit.Text = string.Empty;
    }


    private DataTable GetEditAttributeFields()
    {
        // Instantiate a connection object using the DBConnectionString property
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // Instantiate a DataTable for the resultset
        DataTable dt = new DataTable();

        try
        {
            sqlConn.Open();
            using (SqlCommand sqlcmd = new SqlCommand("SELECT atrb.[ID] AS [ID], atrb.[name] AS [attributeName], atrb.[interval] AS [interval]"
                                     + ", atrb.[upperControlLimit] AS [upperControlLimit], atrb.[lowerControlLimit] AS [lowerControlLimit]"
                                     + " FROM [attributes] AS atrb"
                                     + " WHERE (atrb.[ID] = @ID)", sqlConn)) 
            {
                sqlcmd.Parameters.AddWithValue("@ID", hdnEditAttributeID.Value);
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
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

    private bool InsertNewAssetAttribute()
    {
        try
        {
            sqlInsertAssetAttribute.Insert();
            return true;
        }
        //catch (SqlException sqlex)
        //{
        //    if (sqlex.Number == 2627)
        //        return ("A tax adjustment with the code " + taxCode + " already exists.");
        //    else
        //        return ("Please contact your system administrator.");
        //}
        catch (Exception ex)
        {
            WriteMessage("Could not add the attribute to the asset.  A database error occurred", false);
            return false;
        }
    }

    private bool InsertNewAttribute()
    {
        try
        {
            sqlInsertAttribute.Insert();
            return true;
        }
        //catch (SqlException sqlex)
        //{
        //    if (sqlex.Number == 2627)
        //        return ("A tax adjustment with the code " + taxCode + " already exists.");
        //    else
        //        return ("Please contact your system administrator.");
        //}
        catch (Exception ex)
        {
            WriteMessage("Could not add the new attribute.  A database error occurred", false);
            return false;
        }
    }

    private void LoadEditAttributeFields(DataTable dt)
    {

        txtEditAttribute.Text = dt.Rows[0]["attributeName"].ToString();

        if (!Convert.IsDBNull(dt.Rows[0]["interval"]))
        { 
            SetDDLSelectedValue(ddlEditIntervalList, dt.Rows[0]["interval"].ToString());
        }
        
        txtEditUpperLimit.Text = dt.Rows[0]["upperControlLimit"].ToString();
        txtEditLowerLimit.Text = dt.Rows[0]["lowerControlLimit"].ToString();

    }

    private void PopupNotify(string msg)
    {
        // Popup a window to show the user the passed message
        radnotMessage.Title = "Asset Attribute Notice";
        radnotMessage.Text = msg;
        radnotMessage.Show();
    }

    private bool UpdateEditAttribute()
    {

        try
        {
            sqlUpdateAttribute.Update();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool ValidEditAttibute()
    {

        return true;
    }

    private bool ValidNewAssetAttribute()
    {

        if (ddlAssetList.SelectedValue == string.Empty)
        {
            WriteMessage("You must select an Asset", false);
            return false;
        }

        if (cbxAttributeList.SelectedValue == string.Empty
            || cbxAttributeList.SelectedValue == "0")
        {
            WriteMessage("You must select an Attribute", false);
            return false;
        }

        // Instantiate a connection object using the DBConnectionString property
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);
        try
        {
            // Open the database connection
            sqlConn.Open();

            // Build a SELECT statement to get see if the asset attribute is already defined
            using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM assetsAttributes WHERE [assetID] = @AssetID AND [attributeID] = @AttributeID", sqlConn))
            {
                // Add parameters with values to the SELECT
                sqlCmd.Parameters.AddWithValue("@AssetID", ddlAssetList.SelectedValue);
                sqlCmd.Parameters.AddWithValue("@AttributeID", cbxAttributeList.SelectedValue);

                // Instantiate an integer variable to hold the returned ID value and run the SELECT Statement to get its value
                int rtn = 0;
                rtn = (Int32)sqlCmd.ExecuteScalar();

                // If the ID already exists, show an error message and return 'false' (not valid)
                if (rtn != 0)
                {
                    WriteMessage("The attribute is already associated with the asset you selected", false);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            WriteMessage("Could not validate the new attribute name.  A database error ocurred.", false);
            return false;
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                // Close the database connection
                sqlConn.Close();
            }
        } 

        return true;
    }

    private bool ValidNewAttribute()
    {
        Page.Validate();
        if (!Page.IsValid)
        {
            return false;
        }
        // Instantiate a connection object using the DBConnectionString property
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);
        try
        {
            // Open the database connection
            sqlConn.Open();

            // Build a SELECT statement to get see if the attribute name is already defined
            using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM attributes WHERE [name] = @Name", sqlConn))
            {
                // Add parameters with values to the SELECT
                sqlCmd.Parameters.AddWithValue("@Name", txtAttributeName.Text);

                // Instantiate an integer variable to hold the returned ID value and run the SELECT Statement to get its value
                int rtn = 0;
                rtn = (Int32)sqlCmd.ExecuteScalar();

                // If the ID already exists, show an error message and return 'false' (not valid)
                if (rtn != 0)
                {
                    WriteMessage("The attribute is already defined", false);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            WriteMessage("Could not validate the new attribute name.  A database error ocurred.", false);
            return false;
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                // Close the database connection
                sqlConn.Close();
            }
        } 

        // No errors so, return 'true' (valid)
        return true;
    }

    private void WriteMessage(string msg, bool success)
    {
        // Put the passed msg text in the page's error message field
        if (lblMessage.Text == String.Empty)
        {
            lblMessage.Text = msg;
        }
        else
        {
            lblMessage.Text += Environment.NewLine + msg;
        }

        // Set the font color, size and weight of the message based on the passed msgType
        if (success)
        { 
           lblMessage.Attributes.Add("Style", "color: Green; font-size: 10pt; font-weight: bold;");
        }
        else
        {
            lblMessage.Attributes.Add("Style", "color: Red; font-size: 10pt; font-weight: bold;");
        }

    }

    #endregion
}