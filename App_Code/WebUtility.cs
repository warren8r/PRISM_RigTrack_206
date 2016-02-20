using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for WebUtility
/// </summary>
public class WebUtility
{
	public WebUtility()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static decimal NullToZero(string strDouble)
    {


        if (strDouble == null || strDouble == "")
        {
            return 0;
        }
        else
        {
            return Convert.ToDecimal(strDouble);
        }
    }
    public static string  isNotificationreq(string eventcode,string userid)
    {
        string notificationSendWays = "";
        bool status = true;
        DataTable dt_notification = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from events where eventCode='" + eventcode + "'").Tables[0];
        if (dt_notification.Rows.Count > 0)
        {
            if (dt_notification.Rows[0]["eventnotification"] != null)
            {
                if (dt_notification.Rows[0]["eventnotification"].ToString() == "True")
                    status = true;
                else
                    status = false;
            
            }
            else
                status = false;
        }
        else
            status = false;

        if (status == true)
        {
            DataTable dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Users where userID='" + userid + "'").Tables[0];
            if (dt_users.Rows.Count > 0)
            {
                if (dt_users.Rows[0]["eventNotification"] != null)
                {

                    notificationSendWays = dt_users.Rows[0]["eventNotification"].ToString();

                }

            }
        }
        return notificationSendWays;
    }
}