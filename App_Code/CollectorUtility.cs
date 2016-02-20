using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for CollectorUtility
/// </summary>
namespace MDM
{
    public class Collector
    {
        public Collector()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string GenerateNewAccountID(Int32 StartingKey)
        {
            //following SRP Example under a billion...
            Random rnd = new Random();
            return (string) rnd.Next(1000000000, 2147483647).ToString();
        }

        public bool DoesIDExist(Int32 id)
        {

            string query = "SELECT TOP (1) Number FROM Collectors WHERE (Number = " + id + ")";
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
                    return true;
                }

                //Cleanup

                _SqlDataReader = null;

            }
            return false;

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

        public double DecimalUtilParse(string input)
        {
            double result = 0;

            double.TryParse(input, out result);


            return result;
        }


        public int IntUtilParse(string input)
        {
            int result = 0;

            Int32.TryParse(input, out result);


            return result;
        }
    }

  
}