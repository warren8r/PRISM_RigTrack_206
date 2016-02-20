using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
/// <summary>
/// Summary description for eventNotification
/// </summary>
public class eventNotification
{
    public static DataTable dt_users = new DataTable();

    public eventNotification()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string sendEventNotification(string eventCode)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        ClientMaster userMaster = new ClientMaster();
        string notoficationway = "", userid = "", way = "";
        db.Open();
        transaction = db.BeginTransaction();
        try
        {
            string query_users = "select * from Users";
            DataTable dt_users = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_users).Tables[0];

            string query = " select * from Prism_UserRole_Notification PUR,events ev where PUR.ID=ev.id";
            DataTable dt_notification = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query).Tables[0];

            string querynotification = " select * from Prism_User_EventNotificationType PUEN,Prism_NotificationType PN,events ev where PUEN.notificationid=PN.notificationid and ev.id=PUEN.EventID";
            DataTable dt_notificationtype = SqlHelper.ExecuteDataset(transaction, CommandType.Text, querynotification).Tables[0];

            DataRow[] rownotific = dt_notification.Select("eventCode='" + eventCode + "'");
            for (int role = 0; role < rownotific.Length; role++)
            {
                DataRow[] row_user = dt_users.Select("userRoleID=" + rownotific[role]["userRoleID"].ToString());
                for (int user = 0; user < row_user.Length; user++)
                {
                    DataRow[] rownotificuser = dt_notificationtype.Select("UserId='" + row_user[user]["userID"].ToString() + "' and eventCode='" + eventCode + "'");
                    if (rownotificuser.Length > 0)
                    {
                        for (int notf = 0; notf < rownotificuser.Length; notf++)
                        {
                            userid += rownotificuser[user]["userID"].ToString() + ",";
                            way += rownotificuser[0]["notificationid"].ToString() + ",";
                        }
                    }

                }
            }
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
        }
        if (userid != "")
        {
            notoficationway = userid.Remove(userid.Length - 1, 1) + "~" + way.Remove(way.Length - 1, 1);
        }

        return notoficationway;
    }
    public static bool sendEventNotificationWithEmail(string notificationsendtowhome, string eventCode, string source,
        string sourceid, string sourcename, string startdate, string stopdate, string statusTowhat, string condition, string Assetname)
    {
        bool status = true;
        try
        {
            dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Users").Tables[0];
            
            string message = "", subject = "";
            string notoficationToUserId = notificationsendtowhome.Split('~')[0];
            string notoficationSendWay = notificationsendtowhome.Split('~')[1];
            string[] arrUserID = notoficationToUserId.Split(',');
            string[] arrnotificationway = notoficationSendWay.Split(',');
            for (int user = 0; user < arrUserID.Length; user++)
            {
                switch (arrnotificationway[user].ToString())
                {
                    case "2":
                        {
                            DataRow[] rowuser = dt_users.Select("userID=" + arrUserID[user].ToString());
                            try
                            {
                                message = mailmessageText(source, eventCode, sourceid, sourcename, startdate, stopdate,
                                    rowuser[0]["firstName"].ToString() + "  " + rowuser[0]["lastName"].ToString(), statusTowhat, condition, Assetname);
                                bool mailsentornot = MailSending.SendMail(rowuser[0]["email"].ToString(), subject, message);

                                if (mailsentornot)
                                {
                                    string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedID,userAssignedTimeStamp,Source)values(" +
                                 "'" + eventCode + "','" + getInfo(source, sourcename, statusTowhat, condition, Assetname) + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," + rowuser[0]["userID"].ToString() + "" +
                             ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + source + "')";
                                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
                                }
                            }
                            catch (Exception ex)
                            {
                                status = false;
                            }
                            break;
                        }
                }
            }
        }
        catch (Exception ex)
        {
            status = false;
        }
        finally
        {
        }
        return status;
    }
    public static string getInfo(string source, string sourcename, string statusTowhat, string condition, string Assetname)
    {
        string infomessage = "";
        switch (source)
        {
            case "JOB":
                {
                    switch (statusTowhat)
                    {
                        case "Approve":
                            {
                                infomessage = "Following Job" + Assetname + "Created Successfully";
                                break;
                            }
                        case "AssetCreate":
                            {
                                infomessage = "Following Asset" + Assetname + "Created Successfully";
                                break;
                            }
                        case "Reject":
                            {
                                infomessage = "Following Job" + Assetname + "Rejected Successfully";
                                break;
                            }
                        case "Assign":
                            {
                                infomessage = "Following Job" + Assetname + "Assignment Successfully";
                                break;
                            }
                        case "MoveToWarehouse":
                            {
                                infomessage = "Following Asset" + Assetname + " is moved to Warehouse Successfully From Job:" + sourcename;
                                break;

                            }
                        case "AssetNotInUse":
                            {
                                infomessage = "Following Asset" + Assetname + " is Not In Use in Job:" + sourcename;
                                break;
                            }
                        case "PersonNotInUse":
                            {
                                infomessage = "Following Person" + Assetname + " is Not In Use in Job:" + sourcename;
                                break;
                            }
                        case "ServiceNotInUse":
                            {
                                infomessage = "Following Service" + Assetname + " is Not In Use in Job:" + sourcename;
                                break;
                            }
                        case "PersonReassigned":
                            {
                                infomessage = "Following Person" + Assetname + " is Re-Assigned to Job:" + sourcename;

                                break;
                            }
                        case "ServiceReassigned":
                            {
                                infomessage = "Following Service" + Assetname + " is Re-Assigned to Job:" + sourcename;

                                break;
                            }
                        case "Assetassigned":
                            {
                                infomessage = "Following Asset" + Assetname + " is Assigned to Job:" + sourcename;
                                break;
                            }
                        case "Serviceassigned":
                            {
                                infomessage = "Following Service" + Assetname + " is Assigned to Job:" + sourcename;
                                break;
                            }
                        case "AssetStatusAvailable":
                            {
                                infomessage = "Following Asset" + Assetname + " is Available From Job:" + sourcename;
                                break;
                            }
                        case "AssetStatusNotInUse":
                            {
                                infomessage = "Following Asset" + Assetname + " is Not In Use From Job:" + sourcename;
                                break;
                            }
                        case "ComponentMoveToWarehouse":
                            {
                                infomessage = "Following Component" + Assetname + " is Moved To Warehouse From Job:" + sourcename;
                                break;
                            }
                        case "ComponentReassigned":
                            {
                                infomessage = "Following Component" + Assetname + " is Re-assigned To Job:" + sourcename;
                                break;
                            }
                        case "Componentassigned":
                            {
                                infomessage = "Following Component" + Assetname + " is assigned To Job:" + sourcename;
                                break;
                            }
                        case "DailyRunInsert":
                            {
                                infomessage = "Following Job" + sourcename + " is Runned and Data Inserted Successfully";
                                break;
                            }
                        case "DailyRunUpdate":
                            {
                                infomessage = "Following Job" + sourcename + " is Runned and Data Updated Successfully";
                                break;
                            }
                    }

                    break;

                }
            case "ASSET":
                {
                    switch (statusTowhat)
                    {
                        case "Create":
                            {
                                infomessage = "Asset " + sourcename + " Created Successfully";
                                break;
                            }
                        case "Repair":
                            {
                                infomessage = "Asset " + sourcename + " Repaired Successfully And Condition is " + condition;
                                break;
                            }
                    }
                    break;
                }
            case "COM":
                {
                    infomessage = "Component " + sourcename + " Created Successfully";
                    break;
                }
            case "SALE":
                {
                    switch (statusTowhat)
                    {
                        case "Insert":
                            {
                                infomessage = "SALE " + sourcename + " Created Successfully";
                                break;
                            }
                        case "Update":
                            {
                                infomessage = "SALE " + sourcename + " Update Successfully";
                                break;
                            }
                    }
                    break;
                }
            case "PROJECT":
                {
                    switch (statusTowhat)
                    {
                        case "Insert":
                            {
                                infomessage = "Project " + sourcename + " Created Successfully";
                                break;
                            }
                        case "Update":
                            {
                                infomessage = "Project " + sourcename + " Update Successfully";
                                break;
                            }
                    }
                    break;
                }
        }
        return infomessage;
    }
    public static string mailmessageText(string source, string eventCode, string sourceid, string sourcename, string startdate,
        string stopdate, string customername, string statusTowhat, string condition, string Assetname)
    {
        string strmessage = "", message = "";

        switch (source)
        {
            case "JOB":
                {
                    switch (statusTowhat)
                    {
                        case "Assign":
                            {
                                message = "Job Assignment Details<br />" +
                                            "JobId: " + sourceid + "<br />" +
                                            "Job Name: " + sourcename + "<br />" +
                                            "Customer: " + customername + "<br />" +
                                            "";
                                break;
                            }
                        case "MoveToWarehouse":
                            {
                                message = "Job Asset Move To Warehose Details<br />" +
                                           "JobId: " + sourceid + "<br />" +
                                           "Job Name: " + sourcename + "<br />" +
                                           "Asset Name: " + Assetname + "<br />" +
                                           "Customer: " + customername + "<br />" +
                                           "";
                                break;
                            }
                        case "AssetNotInUse":
                            {
                                message = "Job Asset Not In Use Details<br />" +
                                          "JobId: " + sourceid + "<br />" +
                                          "Job Name: " + sourcename + "<br />" +
                                          "Asset Name: " + Assetname + "<br />" +
                                          "Customer: " + customername + "<br />" +
                                          "";
                                break;

                            }
                        case "PersonNotInUse":
                            {
                                message = "Job Person Not In USe Details<br />" +
                                        "JobId: " + sourceid + "<br />" +
                                        "Job Name: " + sourcename + "<br />" +
                                        "Asset Name: " + Assetname + "<br />" +
                                        "Customer: " + customername + "<br />" +
                                        "";
                                break;

                            }
                        case "ServiceNotInUse":
                            {
                                message = "Job Service Not In USe Details<br />" +
                                        "JobId: " + sourceid + "<br />" +
                                        "Job Name: " + sourcename + "<br />" +
                                        "Asset Name: " + Assetname + "<br />" +
                                        "Customer: " + customername + "<br />" +
                                        "";
                                break;
                            }
                        case "PersonReassigned":
                            {
                                message = "Job Person Re-Assignment Details<br />" +
                                       "JobId: " + sourceid + "<br />" +
                                       "Job Name: " + sourcename + "<br />" +
                                       "Customer: " + customername + "<br />" +
                                       "";
                                break;
                            }
                        case "Assetassigned":
                            {
                                message = "Job order Details<br />" +
                                        "JobId: " + sourceid + "<br />" +
                                        "Job Name: " + sourcename + "<br />" +
                                        "Asset Name: " + Assetname + "<br />" +
                                        "Customer: " + customername + "<br />" +
                                        "";
                                break;

                            }
                        case "Serviceassigned":
                            {
                                message = "Job order Details<br />" +
                                        "JobId: " + sourceid + "<br />" +
                                        "Job Name: " + sourcename + "<br />" +
                                        "Service Name: " + Assetname + "<br />" +
                                        "Customer: " + customername + "<br />" +
                                        "";
                                break;
                            }
                        case "AssetStatusAvailable":
                        case "AssetStatusNotInUse":
                            {
                                message = "Asset Shipping Status  Details<br />" +
                                      "JobId: " + sourceid + "<br />" +
                                      "Job Name: " + sourcename + "<br />" +
                                      "Asset Name: " + Assetname + "<br />" +
                                      "Customer: " + customername + "<br />" +
                                      "";
                                break;
                            }
                        case "ComponentStatusNotInUse":
                        case "ComponentStatusAvailable":
                            {
                                message = "Component Shipping Status  Details<br />" +
                                     "JobId: " + sourceid + "<br />" +
                                     "Job Name: " + sourcename + "<br />" +
                                     "Component Name: " + Assetname + "<br />" +
                                     "Customer: " + customername + "<br />" +
                                     "";
                                break;
                            }
                        case "ComponentMoveToWarehouse":
                        case "Componentassigned":
                            {
                                message = "Component   Details<br />" +
                                     "JobId: " + sourceid + "<br />" +
                                     "Job Name: " + sourcename + "<br />" +
                                     "Component Name: " + Assetname + "<br />" +
                                     "Customer: " + customername + "<br />" +
                                     "";
                                break;
                            }
                        case "DailyRunInsert":
                        case "DailyRunUpdate":
                            {
                                message = "Daily Run Report   Details<br />" +
                                     "JobId: " + sourceid + "<br />" +
                                     "Job Name: " + sourcename + "<br />" +
                                     "Customer: " + customername + "<br />" +
                                     "";
                                break;
                            }

                        default:
                            {
                                message = "Job Order Details<br />" +
                                            "JobId: " + sourceid + "<br />" +
                                            "Job Name: " + sourcename + "<br />" +
                                            "Start Date: " + startdate + "<br />" +
                                            "Stop Date: " + stopdate + "<br />" +
                                            "Customer: " + customername + "<br />" +
                                            "";
                                break;
                            }
                    }

                    break;
                }
            case "ASSET":
                {
                    switch (statusTowhat)
                    {
                        case "Repair":
                            {
                                message = "Asset  Status: " + condition + "<br />" +
                                    "Asset ID: " + sourceid + "<br />" +
                                                             "Asset Name: " + sourcename + "<br />" +
                                                             "Customer: " + customername + "<br />" +
                                                             "";
                                break;
                            }
                        case "Create":
                            {
                                message = "Asset Create Details<br />" +
                                    "Asset ID: " + sourceid + "<br />" +
                                                             "Asset Name: " + sourcename + "<br />" +
                                                             "Customer: " + customername + "<br />" +
                                                             "";
                                break;
                            }
                    }
                    break;
                }
            case "COM":
                {
                    message = "Component Creation Details<br />" +
                                                 "Component Name: " + sourcename + "<br />" +
                                                 "Customer: " + customername + "<br />" +
                                                 "";
                    break;
                }
            case "SALE":
                {
                    message = "Sale Order Details<br />" +
                                             "SaleId: " + sourceid + "<br />" +
                                             "Sale Name: " + sourcename + "<br />" +
                                             "Start Date: " + startdate + "<br />" +
                                             "Stop Date: " + stopdate + "<br />" +
                                             "Customer: " + customername + "<br />" +
                                             "";
                    break;
                }
            case "PROJECT":
                {
                    message = "Project Order Details<br />" +
                                             "ProjectId: " + sourceid + "<br />" +
                                             "Project Name: " + sourcename + "<br />" +
                                             "Start Date: " + startdate + "<br />" +
                                             "Stop Date: " + stopdate + "<br />" +
                                             "Customer: " + customername + "<br />" +
                                             "";
                    break;
                }
        }
        return strmessage;
    }
}