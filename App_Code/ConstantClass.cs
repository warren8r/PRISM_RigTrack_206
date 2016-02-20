using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConstantClass
/// </summary>
public class ConstantClass
{
	public ConstantClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string ReturnConstant(int id)
    {

        string query = "SELECT TOP (1) id, formName, lblName, saved FROM formLabel WHERE id = " + id;
        string connection = ConfigurationManager.ConnectionStrings["local_database"].ConnectionString;



        System.Data.SqlClient.SqlDataReader _SqlDataReader = FunctionSqlDataReader(
            // Pass database connection string to function
        connection,
            // Pass SQL statement to create SqlDataReader
        query);

        // Check we have data in SqlDataReader
        if (_SqlDataReader != null && _SqlDataReader.HasRows)
        {
            // We have data in SqlDataReader, lets loop through and display the data
            while (_SqlDataReader.Read())
            {
                //Hashtable hashtable = new Hashtable();
                //   Console.WriteLine(_SqlDataReader["url"].ToString());
                //hashtable["OrgID"] = _SqlDataReader["OrgID"].ToString();
                //hashtable["OrgName"] = _SqlDataReader["OrgName"].ToString();
                //masterList.Add(hashtable);
                return _SqlDataReader["lblName"].ToString();
            }

            //Cleanup

            _SqlDataReader = null;

        }
        return "";
    }
    public string ReturnConstantWarehouse(int id)
    {

        string query = "SELECT TOP (1) id, formName, lblName, saved FROM WarehouseformLabel WHERE id = " + id;
        string connection = ConfigurationManager.ConnectionStrings["local_database"].ConnectionString;



        System.Data.SqlClient.SqlDataReader _SqlDataReader = FunctionSqlDataReader(
            // Pass database connection string to function
        connection,
            // Pass SQL statement to create SqlDataReader
        query);

        // Check we have data in SqlDataReader
        if (_SqlDataReader != null && _SqlDataReader.HasRows)
        {
            // We have data in SqlDataReader, lets loop through and display the data
            while (_SqlDataReader.Read())
            {
                //Hashtable hashtable = new Hashtable();
                //   Console.WriteLine(_SqlDataReader["url"].ToString());
                //hashtable["OrgID"] = _SqlDataReader["OrgID"].ToString();
                //hashtable["OrgName"] = _SqlDataReader["OrgName"].ToString();
                //masterList.Add(hashtable);
                return _SqlDataReader["lblName"].ToString();
            }

            //Cleanup

            _SqlDataReader = null;

        }
        return "";
    }
    public System.Data.SqlClient.SqlDataReader FunctionSqlDataReader(string _ConnectionString, string _SQL)
    {
        // Set temporary variable to create data reader
        System.Data.SqlClient.SqlDataReader _SqlDataReader = null;

        // set temporary variable for database connection
        System.Data.SqlClient.SqlConnection _SqlConnection = new System.Data.SqlClient.SqlConnection();

        // assign database connection string
        _SqlConnection.ConnectionString = _ConnectionString;

        // Connect to database
        try
        {
            _SqlConnection.Open();
        }
        catch (Exception _Exception)
        {
            // Error occurred while trying to connect to database
            // send error message to console (change below line to customize error handling)
            //   Console.WriteLine(_Exception.Message);

            // failed connection, lets return null
            return null;
        }

        // Database connection successful
        // lets run the given SQL statement and get the data to SqlDataReader
        try
        {
            // Pass the connection to a command object
            System.Data.SqlClient.SqlCommand _SqlCommand =
                    new System.Data.SqlClient.SqlCommand(_SQL, _SqlConnection);

            // get query results
            _SqlDataReader = _SqlCommand.ExecuteReader();

        }
        catch (Exception _Exception)
        {
            // Error occurred while trying to execute reader
            // send error message to console (change below line to customize error handling)
            // Console.WriteLine(_Exception.Message);

            // failed SQL execution, lets return null
            return null;
        }

        // SQL successfully executed, lets return the SqlDataReader
        return _SqlDataReader;
    }
}