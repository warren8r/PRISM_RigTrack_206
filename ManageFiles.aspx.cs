using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Web.Services;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /** link files to the events 
     * first check to see if the file exsist in the database if not( by path ) then insert the record.
     * after that is completed we will have the file id
     * 
     * next we link the file to the event. just by inserting the fileId and eventId into the eventFilename table.
     * 
     **/
    [WebMethod]
    public static string MyMethod(string name)
    {
        return "Hellpjguer9g we5ryti4w " + name;
    }

    private string connectionString()
    {
        //return Session["client_database"].ToString();
        return "Data Source=172.18.12.200;Initial Catalog=MDM_Staging_Client_1;Persist Security Info=True;Trusted_Connection=True;";
    }

    [WebMethod]
    public static void linkFileEvent(int eventId, string path)
    {
        Hashtable bindParams = new Hashtable();;
        int resultId = 0;// <-- this will be returned fromt he database

        //CHECK THE DATABASE TO SEE IF THE FILEPATH EXSIST.. 
        string query = "SELECT top(1) id FROM files WHERE cast(path as nvarchar) = @path";
        bindParams = new Hashtable();
        bindParams.Add("path", path);
        DataTable results = RunQuery(query, bindParams);

        if (results.Rows.Count != 0) 
            resultId = Convert.ToInt32( results.Rows[0][0] ); //resultsIdFromQuery;
        else{
            bindParams = new Hashtable();
            string queryInsertFile = "INSERT INTO files ( name, path, md5, user_id, created ) values ( @name, @path, @md5, @user_id, @created )";

            bindParams.Add("name", "name");
            bindParams.Add("path", path);
            bindParams.Add("md5", "md5");
            bindParams.Add("user_id", 5);
            bindParams.Add("created", DateTime.Now);

            results = RunQuery(queryInsertFile, bindParams);

            //resultId= Convert.ToInt32( results.Rows[0][0] ); // insert id 
        }

        bindParams = new Hashtable();
        string queryInsertLink = "INSERT INTO EventFiles( eventId, fileId ) values ( @eventID, @fileId )";
        bindParams.Add("eventId", eventId);
        bindParams.Add( "fileId", resultId );
        results = RunQuery(queryInsertLink, bindParams, "insert");
    }

    //Execute query and return results
    private static DataTable RunQuery(string queryStr, Hashtable dataBind = null, string type = "select" )
    {
        if (type == "select")
        {
            string connetionString = null;
            SqlConnection connection;
            SqlDataAdapter command;
            DataTable tblData = new DataTable();

            Download connString = new Download();

            connetionString = connString.connectionString();
            connection = new SqlConnection(connetionString);
            connection.Open();
            command = new SqlDataAdapter(queryStr, connection);
            foreach (DictionaryEntry pair in dataBind)
            {
                command.SelectCommand.Parameters.AddWithValue(pair.Key as String, pair.Value);
            }

            if (type == "select")
                command.Fill(tblData);
            connection.Close();

            return tblData;
        }
        else
        {
            //insert code
            
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;

            Download connString = new Download();

            connetionString = connString.connectionString();
            connection = new SqlConnection(connetionString);
            connection.Open();
            command = new SqlCommand(queryStr, connection);
            
            foreach (DictionaryEntry pair in dataBind)
            {
                command.Parameters.AddWithValue(pair.Key as String, pair.Value);
            }

            command.ExecuteNonQuery();

            return new DataTable();
        }
    }

}