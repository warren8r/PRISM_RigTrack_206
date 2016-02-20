using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for GlobalConnetionString
/// </summary>
public class GlobalConnetionString
{
	public GlobalConnetionString()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string ConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["MDMConnectionString"].ConnectionString;
        }
    }
    public static string ClientConnectionString(string clientCode)
    {

        // string connection="";
        if (clientCode == "Client_1")
        {
            return "uid=sa;pwd=j3nk1ns1;database='" + ConfigurationManager.AppSettings["DB_Partial_Name"].ToString() + "';server=" + ConfigurationManager.AppSettings["DatabaseServer"].ToString();
        }
        else
        {
            return "uid=sa;pwd=j3nk1ns1;database='" + clientCode + "';server=" + ConfigurationManager.AppSettings["DatabaseServer"].ToString();
        }

    }
    public static string ConnectionStringAttributes
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["MDMConnectionStringAttributes"].ConnectionString;
        }
    }
    public static string ClientConnection()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["local_database"].ConnectionString;
    }
}