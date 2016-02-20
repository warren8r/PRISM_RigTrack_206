using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using Telerik.Reporting;
using System.Data;

/// <summary>
/// Summary description for auditLog
/// </summary>
public class auditLog
{
    public string insert = "INSERT INTO transactionLog";
    public string select = "SELECT Users.firstName + ' ' + Users.lastName as loginName, transactionLog.systemNote, transactionLog.attributeName, transactionLog.description, transactionLog.created, transactionLog.columnId FROM transactionLog INNER JOIN Users ON transactionLog.userId = Users.userID INNER JOIN Modules ON transactionLog.pageId = Modules.moduleID WHERE (transactionLog.columnId IS NOT NULL AND NOT (transactionLog.columnId = 0 ))";
    private string globalConnString = "";

    protected Dictionary<string, string> values = new Dictionary<string, string>();
    protected Dictionary<string, string> addChanges = new Dictionary<string, string>();

    public auditLog(string connectionString)
    {
        globalConnString = connectionString;
    }

    //public int getUserId()
    //{

    //    ClientMaster userMaster = new ClientMaster();
    //    try
    //    {
    //        userMaster = (ClientMaster)Session["UserMasterDetails"];
    //    }
    //    catch (Exception ex)
    //    {
    //        userMaster = null;
    //    }

    //    return userMaster.UserID;
    //}

    public void addValue(string key, string value)
    {
        values.Add(key, value);
    }
    public void addChange(string key, string value)
    {
        addChanges.Add(key, value);
    }

    public void addValue(Dictionary<string, string> setValues, bool sendoff = false, Dictionary<string, string> addChange = null)
    {
        values = setValues;
        addChanges = addChange;

        if ((bool)sendoff == true)
            try
            {
                AddLog();
            }
            catch (Exception ex)
            {
                //log error
                Debug.WriteLine(ex.ToString());
            }
    }

    //TAKE IN AN ARRAY OF VALUES AND INSERT TO THE LOG TABLE
    public void test()
    {
        auditLog logChange = new auditLog(ConfigurationManager.ConnectionStrings["local_database"].ConnectionString);

        //ETHER ONE OF THESE METHODS FOR ADDING VALUES WILL WORK..
        logChange.addValue("pageId", "1");
        logChange.addValue("userId", "1");
        logChange.addValue("attributeName", "1");
        logChange.addValue("description", "1");

        //OR

        logChange.addValue(new Dictionary<string, string>
        {
            { "pageId", "1" },
            { "userId", "1" },
            { "attributeName", "1" },
            { "description", "1" },
            //{ "system", "false" },
            //{ "supperseded", "1" } 
        });

        //optional
        logChange.addValue("system", "false");
        logChange.addValue("superseded", "1");

        try
        {
            logChange.AddLog();
        }
        catch (Exception ex)
        {
            throw ex;
            //handle errors
        }
    }
    
    public void AddLog()
    {
        int pageId = 0;
        int userId = 0;
        int superseded = 0;
        int columnId = 0;
        bool system = false;
        string attributeName = "";
        string description = "";
        string changes = "";

        SqlConnection sqlcon = new SqlConnection(globalConnString);
        SqlCommand sqlcmd = new SqlCommand(insert, sqlcon);
        sqlcon.Open();
        List<string> column = new List<string>();

        //SET AND VERIFY VALUES
        if (values.Count <= 0)
            throw new Exception("pageId int, userId int, attributeName string, description string REQ  superseded int, system bool optional");

        //COMBINE ALL THE CHANGES INTO A STRING
        //foreach (var item in addChanges)
        //{
        //    changes += ";;" + item.Key + "=" + item.Value;
        //}

        if (!values.ContainsKey("pageId") && !int.TryParse(values["pageId"], out pageId))
            throw new Exception("missing or invalid pageId");
        else
        {
            pageId = Convert.ToInt32(values["pageId"]);
            sqlcmd.Parameters.AddWithValue("pageId", pageId.ToString());
            column.Add("pageId");
        }

        if (!values.ContainsKey("userId") && !int.TryParse(values["userId"], out userId))
            throw new Exception("missing or invalid userId");
        else
        {
            userId = Convert.ToInt32(values["userId"]);
            sqlcmd.Parameters.AddWithValue("userId", userId.ToString());
            column.Add("userId");
        }

        if (values.ContainsKey("superseded") && int.TryParse(values["superseded"], out superseded))
        {
            superseded = Convert.ToInt32(values["superseded"]);
            sqlcmd.Parameters.AddWithValue("superseded", superseded.ToString());
            column.Add("superseded");
        }

        if (values.ContainsKey("columnId") && int.TryParse(values["columnId"], out columnId))
        {
            columnId = Convert.ToInt32(values["columnId"]);
            sqlcmd.Parameters.AddWithValue("columnId", columnId.ToString());
            column.Add("columnId");
        }

        if (values.ContainsKey("system") && values["screenId"] is bool)
        {
            system = Convert.ToBoolean(values["screenId"]);
            sqlcmd.Parameters.AddWithValue("systemNote", system.ToString());
            column.Add("systemNote");
        }

        if (values.ContainsKey("attributeName"))
        {
            attributeName = values["attributeName"].ToString();
            sqlcmd.Parameters.AddWithValue("attributeName", attributeName.ToString());
            column.Add("attributeName");
        }
        else
            throw new Exception("missing or invalid attributeName");

        if (values.ContainsKey("description"))
        {
            description = values["description"].ToString();
        }
        else
            throw new Exception("missing or invalid description");

        //GET MORE FOR DESCRIPTION
        if (values.ContainsKey("query"))
        {
            DataTable dtOldValues = GetDataTable(values["query"]);

            changes += "[";
            List<string> arrchanges = new List<string>();
            foreach (DataColumn dbColumn in dtOldValues.Columns)
            {
                arrchanges.Add(dbColumn.ColumnName + ": " + dtOldValues.Rows[0][dbColumn.ColumnName]);
            }
            changes += string.Join( "; ", arrchanges );
            changes += "]";
        }

        description += changes;
        sqlcmd.Parameters.AddWithValue("description", description.ToString());
        column.Add("description");
        
        string queryCheck = "SELECT top(1) id FROM transactionLog WHERE pageId = '" + pageId + "' AND columnId = '" + columnId + "' ORDER BY superseded DESC";
        DataTable checkResults = GetDataTable(queryCheck);
        try{
            //TRY TO SET SUPERSEDED TO THE ID THAT IS RETURNED
            if (!String.IsNullOrEmpty(checkResults.Rows[0]["id"].ToString()))
            {
                column.Add("superseded");
                sqlcmd.Parameters.AddWithValue("superseded", checkResults.Rows[0]["id"].ToString());
            }
        }catch( Exception ex ){
            //SUPERSEDED IS BEING SET TO NULL
        }

        insert += " ( " + String.Join( ", ", column) + ") VALUES ( @" + String.Join( ", @", column ) + ")";

        sqlcmd.CommandText = insert;
        sqlcmd.ExecuteNonQuery();
        sqlcon.Close();
    }

    public DataTable GetDataTable(string query)
    {
        String connString = globalConnString;
        SqlConnection conn = new SqlConnection(connString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(query, conn);

        DataTable myDataTable = new DataTable();

        try
        {
            conn.Open();
            adapter.Fill(myDataTable);
        }
        finally
        {
            conn.Close();
        }

        return myDataTable;
    }

    //GET THE RESULTS
    public DataTable getLog()
    {
        string query = "SELECT Users.firstName + ' ' + Users.lastName + '(' + Users.loginName + ')' AS username, transactionLog.systemNote, transactionLog.attributeName, transactionLog.description, transactionLog.created AS logged, (SELECT moduleName FROM Modules AS parent WHERE (moduleID = Modules.parentId)) AS moduleName, Modules.moduleName AS Pagename, 'false' as output, transactionLog.superseded, transactionLog.id FROM transactionLog INNER JOIN Users ON transactionLog.userId = Users.userID INNER JOIN Modules ON transactionLog.pageId = Modules.moduleID ORDER BY pageId, columnId, superseded DESC";
        
        DataTable results = GetDataTable(query);

        DataTable organizedTable = new DataTable();
        int rowCount = 0;

        //COLUMN NAMES
        foreach (DataColumn column in results.Columns)
        {
            organizedTable.Columns.Add(column.ColumnName);
        }

        //ROW DATA
        foreach (DataRow row in results.Rows)
        {
            DataRow drClone = organizedTable.NewRow();
            drClone.ItemArray = row.ItemArray;

            organizedTable.Rows.Add(drClone.ItemArray);


            //int superseded = Convert.ToInt32(row["superseded"]);
            //if (superseded > 0)
            //{
            //    //OLDEST CHANGE..
            //    organizedTable.Rows.Add(drClone.ItemArray);
            //}
            //else
            //{
            //    rowCount = 0;
            //    foreach (DataRow rowTest in organizedTable.Rows)
            //    {
            //        if (rowTest["id"] == row["superseded"])
            //        {
            //            //NEWEST CHANGE
            //            organizedTable.Rows.InsertAt(drClone, rowCount);
            //        }
            //        rowCount++;
            //    }

            //}

            organizedTable.AcceptChanges();
        }

        DataRow previouseRow = organizedTable.NewRow();
        int outputRowCount = 0;
        foreach (DataRow row in organizedTable.Rows)
        {
            row["output"] = "true";
            try{
                int prevId = Convert.ToInt32(previouseRow["superseded"]);
                int currentId = Convert.ToInt32( row["id"] );

                if (prevId.Equals( currentId ))
                {
                    row["output"] = "false";
                }
            }catch( Exception ex ) {
                Debug.Write(ex);
            }   

            previouseRow = row;
            outputRowCount++;
        }

        return organizedTable;
    }
}