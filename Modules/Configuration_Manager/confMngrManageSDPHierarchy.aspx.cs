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

public partial class Modules_Configuration_Manager_confMngrManageSDPHierarchy : System.Web.UI.Page
{

    #region Properties

    public string DBConnectionString
    {
        get { return Session["client_database"].ToString(); }
    }

    public string CollectorLabel
    {
        get
        {
            ConstantClass cc = new ConstantClass();
            return cc.ReturnConstant(3);
        }
    }

    public string SDPLabel
    {
        get
        {
            ConstantClass cc = new ConstantClass();
            return cc.ReturnConstant(4);
        }
    }

    public string AccountLabel
    {
        get
        {
            ConstantClass cc = new ConstantClass();
            return cc.ReturnConstant(5);
        }
    }
    
    #endregion

    #region Event Handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        // Blank out the message field 
        lblMessage.Text = string.Empty;


        if (!Page.IsPostBack)
        {
            // Initialize all the dropdowns
            RefreshCollectorsList();
            RefreshSDPList();
            RefreshAccountsList();
            RefreshMeterList();
            RefreshSelectedGroupingGrid();

            // Change labels and tooltips to the constants set in preferences
            SetLabelConstants();
            SetSelectedGroupingHeaders();
            SetAllGroupingHeaders();
        }
    }

    protected void ddlCollectorsList_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        // Refresh the current grouping grid to reflect the newly selected collector
        RefreshSelectedGroupingGrid();
    }

    protected void lnkNewCollector_Click(object sender, EventArgs e)
    {
        // Show the new Collector panel
        radmpAll.SelectedIndex = 1;
    }

    protected void btnCloseNewCollector_Click(object sender, EventArgs e)
    {
        // Return to the SDP grouping panel
        radmpAll.SelectedIndex = 0;

        // Refresh the collectors dropdown to reflect any changes that might have been made on the collectors panel
        RefreshCollectorsList();
    }

    protected void ddlSDPList_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        // Refresh the current grouping grid to reflect the newly selected SDP
        RefreshSelectedGroupingGrid();
    }

    protected void lnkNewSDP_Click(object sender, EventArgs e)
    {
        // Show the new SDP panel
        radmpAll.SelectedIndex = 2;
    }

    protected void btnCloseNewSDP_Click(object sender, EventArgs e)
    {
        // Return to the SDP grouping panel
        radmpAll.SelectedIndex = 0;

        // Refresh the SDP dropdown to reflect any changes that might have been made on the SDP panel
        RefreshSDPList();
    }

    protected void cbxAccountsList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        // Refresh the current grouping grid to reflect the newly selected account
        RefreshSelectedGroupingGrid();
    }

    protected void lnkNewAccount_Click(object sender, EventArgs e)
    {
        // Show the new account panel
        radmpAll.SelectedIndex = 3;
    }

    protected void btnCloseNewAccount_Click(object sender, EventArgs e)
    {
        // Return to the SDP grouping panel
        radmpAll.SelectedIndex = 0;

        // Refresh the account combobox to reflect any changes that might have been made on the accounts panel
        RefreshAccountsList();
    }

    protected void cbxMeterList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        // Refresh the current grouping grid to reflect the newly selected meter
        RefreshSelectedGroupingGrid();
    }

    protected void lnkNewMeter_Click(object sender, EventArgs e)
    {
        // Show the new meter panel
        radmpAll.SelectedIndex = 4;
    }

    protected void btnCloseNewMeter_Click(object sender, EventArgs e)
    {
        // Return to the SDP grouping panel
        radmpAll.SelectedIndex = 0;

        // Refresh the meter combobox to reflect any changes that might have been made on the accounts panel
        RefreshMeterList();
    }

    protected void btnSaveGrouping_Click(object sender, EventArgs e)
    {
        // If the selected grouping passes validations, create it
        if (ValidGrouping())
        {
            CreateGrouping();
        }
    }

    protected void btnResetSelecteDropdowns_Click(object sender, EventArgs e)
    {
        ResetSelectDropdownIndexes();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        // Clear the contents of all the criteria boxs
        radtxtCollectorCriteria.Text = string.Empty;
        radtxtSDPCriteria.Text = string.Empty;
        radtxtAccountNumberCriteria.Text = string.Empty;
        radtxtAccountNameCriteria.Text = string.Empty;
        radtxtAccountPrimaryFirstCriteria.Text = string.Empty;
        radtxtAccountPrimaryLastCriteria.Text = string.Empty;
        radtxtMeterCriteria.Text = string.Empty;
    }

    protected void btnViewFilter_Click(object sender, EventArgs e)
    {
        // Refresh the all hierarchies grid using the values in the criteria boxes as filters
        radgrdAllGrouping.Rebind();
    }

    protected void radgrdAllGrouping_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        SetAllGroupingDataSource();
    }

    protected void radgrdAllGrouping_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox chkActive = (CheckBox)item.FindControl("chkActive");
            //Label lblActive = (Label)item.FindControl("lblActive");

            // if the grouping is In-Active, gray out the row and make the text red
            if (chkActive != null
                && !chkActive.Checked)
            {
                // Set the row background color to gray
                item.Style.Add("background-color", "#CECECE");
            }
        }

    }

    protected void radgrdAllGrouping_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        radgrdAllGrouping.CurrentPageIndex = e.NewPageIndex;
        //RefreshAllGroupingGrid();
    }

    protected void radgrdAllGrouping_CheckChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        CheckBox chk = (CheckBox)sender;
        string strID = row["ID"].Text;

        sqlAllGrouping.UpdateParameters["ID"].DefaultValue = strID;
        sqlAllGrouping.UpdateParameters["bitActive"].DefaultValue = chk.Checked.ToString();
        sqlAllGrouping.Update();

        string msgText = "Grouping status changed to " + (chk.Checked ? "Active" : "Inactive");
        WriteMessage(msgText, true);
        PopupNotify(msgText);

        RefreshSelectedGroupingGrid();
        radgrdAllGrouping.Rebind();
        //RefreshAllGroupingGrid();

    }

    #endregion

    #region Functions

    private void CreateGrouping()
    {
        try
        {
            // Attempt to insert a new row in the grouping table
            sqlCreateGrouping.Insert();

            // Insert succeeded.  Show a success message on the page and in a popup.
            string successMsg = "The grouping was created";
            WriteMessage(successMsg, true);
            PopupNotify(successMsg);

            if (cbxMeterList.SelectedValue != "0")
            {
                RefreshMeterList();
            }

            // Refresh the Selected Grouping and All Grouping grids
            RefreshSelectedGroupingGrid();
            radgrdAllGrouping.Rebind();
        }
        catch (Exception ex)
        {
        }
    }

    private void RefreshCollectorsList()
    {
        ddlCollectorsList.Items.Clear();
        ddlCollectorsList.Items.Add(new Telerik.Web.UI.DropDownListItem("- Select -", "0"));

        ddlCollectorsList.DataBind();
    }

    private void RefreshSDPList()
    {
        ddlSDPList.Items.Clear();
        ddlSDPList.Items.Add(new Telerik.Web.UI.DropDownListItem("- Select -", "0"));

        sqlGetSDP.SelectCommand = "SELECT sdp.[ID], sdp.[bitActive], sdp.[Number], sdp.[Name] "
                                                + "FROM [SDP] sdp WHERE (IsNull(sdp.[bitActive], 'False') = 'True')"
                                                + " ORDER BY sdp.[Name]";
        ddlSDPList.DataBind();
    }

    protected void RefreshAccountsList()
    {
        cbxAccountsList.Items.Clear();
        cbxAccountsList.Items.Add(new Telerik.Web.UI.RadComboBoxItem("- Select -", "0"));

        cbxAccountsList.DataBind();
    }

    protected void RefreshMeterList()
    {
        cbxMeterList.Items.Clear();
        cbxMeterList.Items.Add(new Telerik.Web.UI.RadComboBoxItem("- Select -", "0"));

        cbxMeterList.DataBind();
    }

    private void RefreshSelectedGroupingGrid()
    {
        radgrdSelectedGrouping.DataBind();
    }

    private void ResetSelectDropdownIndexes()
    {
        try
        {
            ddlCollectorsList.SelectedIndex = 0;
        }
        catch (Exception ex)
        { }

        try
        {
            ddlSDPList.SelectedIndex = 0;
        }
        catch (Exception ex)
        { }

        try
        {
            cbxAccountsList.SelectedIndex = 0;
        }
        catch (Exception ex)
        { }

        try
        {
            cbxMeterList.SelectedIndex = 0;
        }
        catch (Exception ex)
        { }


    }

    private void SetAllGroupingDataSource()
    {
        // Instantiate a connection and command object for the SELECT statement
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.Connection = sqlConn;
        sqlCmd.CommandType = CommandType.Text;

        // Build a SQL SELECT statement to retrieve the groupings using the criteria values entered, if any.
        // First add the resultset columns
        string selectStatement = string.Empty;
        string resultCols = "SELECT grp.[ID], col.[Number] AS [CollectorsNumber], col.[Name] AS [CollectorsName], sdp.[Number] AS [SDPNumber], sdp.[Name] AS [SDPName]"
                         + ", act.[Number] AS [AccountsNumber], act.Name AS [AccountsName], act.[PrimaryFirst] AS [AccountsPrimaryFirst]"
                         + ", act.[PrimaryLast] AS [AccountsPrimaryLast], mtr.[meterName] as [meterName], ISNULL(grp.bitActive, 'false') AS bitActive"
                         + " FROM grouping AS grp LEFT OUTER JOIN Collectors AS col ON col.id = grp.CollectorsID LEFT OUTER JOIN SDP AS sdp ON sdp.ID = grp.SDPID"
                         + " LEFT OUTER JOIN Accounts AS act ON act.id = grp.AccountsID LEFT OUTER JOIN meter AS mtr ON mtr.[ID] = grp.[meterID]";

        // Add the ORDER BY clause
        string orderBy = " ORDER BY grp.createDate DESC, col.Name, sdp.Name, act.Number, mtr.meterName";

        // Instantiate a StringBuilder to hold the WHERE clause, if any
        StringBuilder criteria = new StringBuilder();
        string and = string.Empty;
        string where = " WHERE";

        // If a collector filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (radtxtCollectorCriteria.Text != string.Empty)
        {
            criteria.Append(where + "(col.Name LIKE @CollectorsName)");
            sqlCmd.Parameters.Add(new SqlParameter("CollectorsName", DbType.String));
            sqlCmd.Parameters["CollectorsName"].Value = radtxtCollectorCriteria.Text + "%";
            where = string.Empty;
            and = " AND ";
        }

        // If an SDP filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (radtxtSDPCriteria.Text != string.Empty)
        {
            criteria.Append(where + and + "(sdp.Name LIKE @SDPName)");
            sqlCmd.Parameters.Add(new SqlParameter("SDPName", DbType.String));
            sqlCmd.Parameters["SDPName"].Value = radtxtSDPCriteria.Text + "%";
            where = string.Empty;
            and = " AND ";
        }

        // If an Account Number filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (radtxtAccountNumberCriteria.Text != string.Empty)
        {
            criteria.Append(where + and + "(act.Number LIKE @AccountsNumber)");
            sqlCmd.Parameters.Add(new SqlParameter("AccountsNumber", DbType.String));
            sqlCmd.Parameters["AccountsNumber"].Value = radtxtAccountNumberCriteria.Text + "%";
            where = string.Empty;
            and = " AND ";
        }

        // If an Account Name filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (radtxtAccountNameCriteria.Text != string.Empty)
        {
            criteria.Append(where + and + "(act.Name LIKE @AccountsName)");
            sqlCmd.Parameters.Add(new SqlParameter("AccountsName", DbType.String));
            sqlCmd.Parameters["AccountsName"].Value = radtxtAccountNameCriteria.Text + "%";
            where = string.Empty;
            and = " AND ";
        }

        // If an Account Primary First filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (radtxtAccountPrimaryFirstCriteria.Text != string.Empty)
        {
            criteria.Append(where + and + "(act.PrimaryFirst LIKE @AccountsPrimaryFirst)");
            sqlCmd.Parameters.Add(new SqlParameter("AccountsPrimaryFirst", DbType.String));
            sqlCmd.Parameters["AccountsPrimaryFirst"].Value = radtxtAccountPrimaryFirstCriteria.Text + "%";
            where = string.Empty;
            and = " AND ";
        }

        // If an Account Primary Last filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (radtxtAccountPrimaryLastCriteria.Text != string.Empty)
        {
            criteria.Append(where + and + "(act.PrimaryLast LIKE @AccountsPrimaryLast)");
            sqlCmd.Parameters.Add(new SqlParameter("AccountsPrimaryLast", DbType.String));
            sqlCmd.Parameters["AccountsPrimaryLast"].Value = radtxtAccountPrimaryLastCriteria.Text + "%";
            where = string.Empty;
            and = " AND ";
        }

        // If a meter filter was entered, add it to the WHERE clause and create a parameter for the value in the SELECT command 
        if (radtxtMeterCriteria.Text != string.Empty)
        {
            criteria.Append(where + and + "(mtr.meterName LIKE @MeterName)");
            sqlCmd.Parameters.Add(new SqlParameter("MeterName", DbType.String));
            sqlCmd.Parameters["MeterName"].Value = radtxtMeterCriteria.Text + "%";
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
            sqlConn.Close();
        }

        // Set the All Groupings grid's datasource to the DataTable
        radgrdAllGrouping.DataSource = dt;

    }

    private void SetAllGroupingHeaders()
    {
        var masterTableView = radgrdAllGrouping.MasterTableView;
        var col = masterTableView.GetColumn("CollectorsName");
        col.HeaderText = CollectorLabel;

        col = masterTableView.GetColumn("SDPName");
        col.HeaderText = SDPLabel;

        col = masterTableView.GetColumn("AccountsNumber");
        col.HeaderText = AccountLabel + " Number";

        col = masterTableView.GetColumn("AccountsName");
        col.HeaderText = AccountLabel + " Name";
    }

    private void SetLabelConstants()
    {
        // Data Aggregator labels
        lblSelectCollector.Text = "Select " + CollectorLabel + ": ";
        ddlCollectorsList.ToolTip = "Select " + CollectorLabel;
        lnkNewCollector.ToolTip = "Create a new " + CollectorLabel;

        lblSelectSDP.Text = "Select " + SDPLabel + ": ";
        ddlSDPList.ToolTip = "Select " + SDPLabel;
        lnkNewSDP.ToolTip = "Create a new " + SDPLabel;

        lblSelectAccount.Text = "Select " + AccountLabel + ": ";
        cbxAccountsList.ToolTip = "Select " + AccountLabel;
        lnkNewAccount.ToolTip = "Create a new " + AccountLabel;

        // Filter Criteria labels
        lblCollectorCriteria.Text = CollectorLabel + ": ";
        radtxtCollectorCriteria.ToolTip = "Filter criteria for " + CollectorLabel;

        lblSDPCriteria.Text = SDPLabel + ": ";
        radtxtSDPCriteria.ToolTip = "Filter criteria for " + SDPLabel;

        lblAccountNumberCriteria.Text = AccountLabel + " #: ";
        radtxtAccountNumberCriteria.ToolTip = "Filter criteria for " + AccountLabel + " number";

        lblAccountNameCriteria.Text = AccountLabel + " Name: ";
        radtxtAccountNameCriteria.ToolTip = "Filter criteria for " + AccountLabel + " name";
    }

    private void SetSelectedGroupingHeaders()
    {
        var masterTableView = radgrdSelectedGrouping.MasterTableView;
        var col = masterTableView.GetColumn("CollectorsName");
        col.HeaderText = CollectorLabel;

        col = masterTableView.GetColumn("SDPName");
        col.HeaderText = SDPLabel;

        col = masterTableView.GetColumn("AccountsNumber");
        col.HeaderText = AccountLabel + " Number";

        col = masterTableView.GetColumn("AccountsName");
        col.HeaderText = AccountLabel + " Name";
    }

    private bool ValidGrouping()
    {
        // Initialize the count of dropdown box selections
        int levelCount = 0;

        // Count the number of dropdownlists that have a selection made
        levelCount += ddlCollectorsList.SelectedValue != "0" ? 1 : 0;
        levelCount += ddlSDPList.SelectedValue != "0" ? 1 : 0;
        levelCount += cbxAccountsList.SelectedValue != "0" ? 1 : 0;
        levelCount += cbxMeterList.SelectedValue != "0" ? 1 : 0;

        // If not enough criteria specified, show an error message and return 'false' (not valid)
        if (levelCount <= 1)
        {
            WriteMessage("Please select an item from at least two of the data aggregator and meter lists", false);
            return false;
        }
        else if (cbxMeterList.SelectedValue != "0"
                && levelCount > 2)
        {
            WriteMessage("To group a meter, only one data aggregator can be selected", false);
            return false;
        }

        // Make sure the grouping does not already exist
        if (!ValidNewGrouping())
        {
            return false;
        }

        // If this is an SDP grouping (no account or meter selected), make sure the SDP is not already grouped with a different collector.
        if (ddlSDPList.SelectedValue != string.Empty
            && ddlSDPList.SelectedValue != "0"
            && (cbxAccountsList.SelectedValue == string.Empty 
                || cbxAccountsList.SelectedValue == "0")
            && (cbxMeterList.SelectedValue == string.Empty
                || cbxMeterList.SelectedValue == "0"))
        {
            if (!ValidSDP())
            {
                return false;
            }
        }
        
        // if this is an Account grouping (no meter seleted), make sure the Account is not already grouped with a different parent
        if (cbxAccountsList.SelectedValue != string.Empty
                && cbxAccountsList.SelectedValue != "0"
                && (cbxMeterList.SelectedValue == string.Empty
                    || cbxMeterList.SelectedValue == "0"))
        {
            if (!ValidAccount())
            {
                return false;
            }
        }
        
        // If this is a Meter grouping, make sure the Meter is not already grouped with a different parent
        if (cbxMeterList.SelectedValue != string.Empty
                && cbxMeterList.SelectedValue != "0")
        {
            if (!ValidMeter())
            {
                return false;
            }
        }
        
        // No errors so, return 'true' (valid)
        return true;
    }

    private bool ValidAccount()
    {
        // Instantiate a database connection object 
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        try
        {
            // Open the database connection
            sqlConn.Open();

            // Build a SELECT statement to get the count of rows that inlude a collector and just this SDP
            using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM grouping WHERE ([collectorsID] IS NOT NULL OR [sDPID] IS NOT NULL) AND [accountsID] = @AccountID"
                                                     + " AND IsNull([meterID], 0) = 0", sqlConn))
            {
                // Add parameters with values to the SELECT
                sqlCmd.Parameters.AddWithValue("@AccountID", cbxAccountsList.SelectedValue);

                // Instantiate an integer variable to hold the returned ID value and run the SELECT Statement to get its value
                int id = 0;
                id = (Int32)sqlCmd.ExecuteScalar();

                // If the ID already exists, show an error message and return 'false' (not valid)
                if (id != 0)
                {
                    WriteMessage("This " + AccountLabel + " has already been grouped with a " + CollectorLabel + " or " + SDPLabel, false);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            WriteMessage("Could not validate the grouping.  A database error occurred.", false);
            return false;
        }
        finally
        {
            // Close the database connection
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        return true;
    }

    private bool ValidNewGrouping()
    {
        // Instantiate a database connection object 
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // See if the grouping is already defined.
        try
        {
            // Open the database connection
            sqlConn.Open();

            // Build a SELECT statement to get the first ID that matches the criteria specified by the items selected in the dropdownlists
            using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM grouping WHERE [collectorsID] = @CollectorsID AND [sDPID] = @SDPID AND [accountsID] = @AccountsID AND [meterID] = @MeterID", sqlConn))
            {
                // Add parameters with values to the SELECT
                sqlCmd.Parameters.Add("@CollectorsID", SqlDbType.Int);
                sqlCmd.Parameters["@CollectorsID"].Value = Convert.ToInt32(ddlCollectorsList.SelectedValue);
                sqlCmd.Parameters.Add("@SDPID", SqlDbType.Int);
                sqlCmd.Parameters["@SDPID"].Value = Convert.ToInt32(ddlSDPList.SelectedValue);
                sqlCmd.Parameters.Add("@AccountsID", SqlDbType.Int);
                sqlCmd.Parameters["@AccountsID"].Value = Convert.ToInt32(cbxAccountsList.SelectedValue);
                sqlCmd.Parameters.Add("@MeterID", SqlDbType.Int);
                sqlCmd.Parameters["@MeterID"].Value = Convert.ToInt32(cbxMeterList.SelectedValue);

                // Instantiate an integer variable to hold the returned ID value and run the SELECT Statement to get its value
                int id = 0;
                id = (Int32)sqlCmd.ExecuteScalar();

                // If the ID already exists, show an error message and return 'false' (not valid)
                if (id != 0)
                {
                    WriteMessage("This grouping already exists", false);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            WriteMessage("Could not validate the grouping.  A database error occurred.", false);
            return false;
        }
        finally
        {
            // Close the database connection
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        return true;
    }

    private bool ValidMeter()
    {
        // Instantiate a database connection object 
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        try
        {
            // Open the database connection
            sqlConn.Open();

            // Build a SELECT statement to get the count of rows that inlude a collector and just this SDP
            using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM grouping WHERE ([collectorsID] is not null OR [sDPID] IS NOT NULL OR [accountsID] IS NOT NULL)"
                                                     + " AND [meterID] = @MeterID", sqlConn))
            {
                // Add parameters with values to the SELECT
                sqlCmd.Parameters.AddWithValue("@MeterID", cbxMeterList.SelectedValue);

                // Instantiate an integer variable to hold the returned ID value and run the SELECT Statement to get its value
                int id = 0;
                id = (Int32)sqlCmd.ExecuteScalar();

                // If the ID already exists, show an error message and return 'false' (not valid)
                if (id != 0)
                {
                    WriteMessage("This Meter has already been grouped with a " + CollectorLabel + ", " + SDPLabel + " or " + AccountLabel, false);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            WriteMessage("Could not validate the meter grouping.  A database error occurred.", false);
            return false;
        }
        finally
        {
            // Close the database connection
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        return true;
    }

    private bool ValidSDP()
    {
        // Instantiate a database connection object 
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        try
        {
            // Open the database connection
            sqlConn.Open();

            // Build a SELECT statement to get the count of rows that inlude a collector and just this SDP
            using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM grouping WHERE ([collectorsID] is not null AND [sDPID] = @SDPID)"
                                                     + " AND (IsNull([accountsID], 0) = 0 AND IsNull([meterID], 0) = 0)", sqlConn))
            {
                // Add parameters with values to the SELECT
                sqlCmd.Parameters.AddWithValue("@SDPID", ddlSDPList.SelectedValue);

                // Instantiate an integer variable to hold the returned ID value and run the SELECT Statement to get its value
                int id = 0;
                id = (Int32)sqlCmd.ExecuteScalar();

                // If the ID already exists, show an error message and return 'false' (not valid)
                if (id != 0)
                {
                    WriteMessage("This " + SDPLabel + " has already been grouped with a " + CollectorLabel, false);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            WriteMessage("Could not validate the grouping.  A database error occurred.", false);
            return false;
        }
        finally
        {
            // Close the database connection
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

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

    private void PopupNotify(string msg)
    {
        // Popup a window on the users monitor with the passed msg
        n1.Title = "Set Data Aggregator Grouping Notice";
        n1.Text = msg;
        n1.Show();
    }

    #endregion
}
