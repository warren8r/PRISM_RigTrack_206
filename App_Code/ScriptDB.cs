using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for ScriptDB
/// </summary>
public class ScriptDB
{
	public ScriptDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool ExecuteSQLScript(string SQlFilePath, string ConStr)
    {

        //        string DBServer = "";
        //string DBName = "";
        //string DBCreateDataFile = "";
        //string SQL_CONNECTION_STRING = "Server=" + DBServer + ";" + "DataBase=;" + "Integrated Security=SSPI;uid=sa;pwd=sa";
        //bool bolDidPreviouslyConnect = false;
        // string connectionString = SQL_CONNECTION_STRING;
        string fileUrl = "";

        //------ microcroseconds * seconds * minutes * hours
        int timeout = 100 * 60 * 60 * 1;

        SqlConnection conn = null;
        try
        {
            // read file 
            fileUrl = SQlFilePath;
            System.Net.WebRequest request = System.Net.WebRequest.Create(fileUrl);
            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {


                // Create new connection to database 
                conn = new SqlConnection(ConStr);

                conn.Open();

                while (!sr.EndOfStream)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    SqlCommand cmd = conn.CreateCommand();

                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        if (s != null && s.ToUpper().Trim().Equals("GO"))
                        {
                            break; // TODO: might not be correct. Was : Exit While
                        }

                        sb.AppendLine(s);
                    }

                    // Execute T-SQL against the target database 
                    cmd.CommandText = sb.ToString();
                    cmd.CommandTimeout = timeout;

                    cmd.ExecuteNonQuery();

                }
            }
            //' Done
            return true;
        }
        catch (Exception ex)
        {
            // Error            
            return false;
        }
        finally
        {
        }
    }
    public static void CreateClientDb(string Databasename)
    {
        //SqlConnection cn = new SqlConnection(DbInfo.ConnectionString);
        //SqlCommand cmd = new SqlCommand(Databasename, cn);
        //cmd.CommandType = CommandType.Text;
        //cn.Open();
        //cmd.ExecuteNonQuery();
        //cn.Close();
    }
}